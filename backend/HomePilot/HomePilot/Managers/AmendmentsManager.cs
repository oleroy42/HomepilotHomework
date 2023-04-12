using HomePilot.Controllers.Dtos;
using HomePilot.Db;
using HomePilot.Db.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using static HomePilot.Controllers.AmendmentsController;

namespace HomePilot.Managers;

public class AmendmentsManager
{
    private readonly HomePilotDbContext _homePilotDbContext;

    public AmendmentsManager(HomePilotDbContext homePilotDbContext)
    {
        _homePilotDbContext = homePilotDbContext;
    }

    public async Task<List<AmendmentDto>> GetAmendmentsAsync()
    {
        List<AmendmentModel> amendments = await _homePilotDbContext.Amendments
                                                                 .Include(a => a.Lease)
                                                                 .Include(a => a.Entries)
                                                                 .Include(a => a.Exits)
                                                                 .ToListAsync();

        var tenantIdsToFetch = new HashSet<Guid>();

        foreach(var amendment in amendments)
        {
            foreach(var entry in amendment.Entries)
            {
                tenantIdsToFetch.Add(entry.TenantId);
            }
            foreach (var exit in amendment.Exits)
            {
                tenantIdsToFetch.Add(exit.TenantId);
            }            
        }

        Dictionary<Guid, TenantModel> tenantDictionary = await _homePilotDbContext.Tenants
                                                                                 .Where(a => tenantIdsToFetch.Contains(a.Id))
                                                                                 .ToDictionaryAsync(t => t.Id);

        return amendments.Select(a => ConvertToDto(a, tenantDictionary)).ToList();
    }

    private AmendmentDto ConvertToDto(AmendmentModel amendment, Dictionary<Guid, TenantModel> tenantDictionary)
    {
        return new AmendmentDto
        {
            Id = amendment.Id,
            EffectiveDate = amendment.EffectiveDate,
            Lease = new LeaseDto
            {
                Id = amendment.Lease.Id,
                Name = amendment.Lease.Name,
                Rent = amendment.Lease.RentInCents
            },
            OldRent = amendment.OldRentInCents,
            Entries = amendment.Entries.Select(e => GetTenantDto(tenantDictionary, e)).ToList(),
            Exits = amendment.Exits.Select(e => GetTenantDto(tenantDictionary, e)).ToList(),

        };
    }

    private static TenantDto GetTenantDto(Dictionary<Guid, TenantModel> tenantDictionary, LeaseTenantModel e)
    {
        var tenant = tenantDictionary[e.TenantId];
        return new TenantDto
        {
            Id = tenant.Id,
            FirstName = tenant.FirstName,
            LastName = tenant.LastName
        };
    }

    public async Task<Guid> AddAmendmentAsync(Guid leaseId, List<Person> exits, List<Person> entries, int? newRent)
    {
        //TODO: check lease is still active
        var newAmendment = new AmendmentModel(leaseId, DateTime.Now, newRent, null);
        _homePilotDbContext.Amendments.Add(newAmendment);

        //TODO: check people exiting exists
        if (exits.Any())
        {
            //TODO: hacky : do it properly.
            var hackyList = exits.Select(e => e.FirstName + e.LastName).ToHashSet();
            var exitTenants = await _homePilotDbContext.LeaseTenants.Where(t => hackyList.Contains(t.Tenant.FirstName + t.Tenant.LastName) && t.LeaseId == leaseId).ToListAsync();

            foreach (var e in exitTenants)
            {
                e.AmendmentExitId = newAmendment.Id;
                _homePilotDbContext.Entry(e).State = EntityState.Modified;
            }

        }
        foreach(var entry in entries)
        {
            var newTenant = new TenantModel(entry.FirstName, entry.LastName);
            _homePilotDbContext.Tenants.Add(newTenant);
            _homePilotDbContext.LeaseTenants.Add(new LeaseTenantModel(leaseId, newTenant.Id, newAmendment.Id));
        }

        await _homePilotDbContext.SaveChangesAsync();
        return newAmendment.Id;
    }
}

