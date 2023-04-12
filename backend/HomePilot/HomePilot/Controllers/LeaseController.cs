using HomePilot.Controllers.Dtos;
using HomePilot.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HomePilot.Controllers;

[ApiController]
[Route("[controller]")]

public class LeaseController
{
    private readonly LeaseManager _leaseManager;

    public LeaseController(LeaseManager leaseManager)
    {
        _leaseManager = leaseManager;
    }

    [HttpGet("active")]
    public Task<List<LeaseDto>> GetActiveLeases() => _leaseManager.GetActiveLeasesAsync();
}

