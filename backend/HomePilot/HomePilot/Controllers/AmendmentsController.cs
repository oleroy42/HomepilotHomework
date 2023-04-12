using HomePilot.Controllers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HomePilot.Controllers;

[ApiController]
[Route("amendments")]
public class AmendmentsController : ControllerBase
{
    [HttpGet("hello")]
    public string Hello() =>
        "Hello world";


    [HttpGet()]
    public Task<List<AmendmentDto>> GetAmendments() => Task.FromResult(
        new List<AmendmentDto>
        { GetDefaultAmendmentDto() });

    private static AmendmentDto GetDefaultAmendmentDto()
    {
        return new AmendmentDto
        {
            EffectiveDate = DateTime.Now,
            Entries = new() { new TenantDto { FirstName = "In", LastName = "LastName", Id = Guid.NewGuid() } },
            Id = Guid.NewGuid(),
            OldRent = 25000,
            Exits = new() { new TenantDto { FirstName = "Out", LastName = "LastName", Id = Guid.NewGuid() } },
            Lease = new LeaseDto { Id = Guid.NewGuid(), Name = "Lease 1", Rent = 20000 }
        };
    }
}

