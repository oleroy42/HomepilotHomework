using HomePilot.Controllers.Dtos;
using HomePilot.Db;
using HomePilot.Db.Model;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HomePilot.Managers
{
    public class LeaseManager
    {
        private readonly HomePilotDbContext _homePilotDbContext;

        public LeaseManager(HomePilotDbContext homePilotDbContext)
        {
            _homePilotDbContext = homePilotDbContext;
        }

        public async Task<List<LeaseDto>> GetActiveLeasesAsync()
        {
            List<LeaseModel> activesLeases = await _homePilotDbContext.Leases
                                                                .Where(l => l.EndDate == null)
                                                                .ToListAsync();

            return activesLeases.Select(l => new LeaseDto
            {
                Id = l.Id,
                Name = l.Name,
                Rent = l.RentInCents
            }).ToList();
        }
    }
}
