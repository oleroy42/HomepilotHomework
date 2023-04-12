using HomePilot.Controllers;
using HomePilot.Controllers.Dtos;
using HomePilot.Db;
using HomePilot.Db.Model;
using Microsoft.EntityFrameworkCore;

namespace HomePilot.Managers;

public class AmendmentsManager
{
    private readonly HomePilotDbContext _homePilotDbContext;

    public AmendmentsManager(HomePilotDbContext homePilotDbContext)
    {
        _homePilotDbContext = homePilotDbContext;
    }

    public async Task<List<AmendmentDto>> GetAmendments()
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
}

