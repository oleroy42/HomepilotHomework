using HomePilot.Controllers.Dtos;
using HomePilot.Managers;
using Microsoft.AspNetCore.Mvc;

namespace HomePilot.Controllers;

[ApiController]
[Route("amendments")]
public class AmendmentsController : ControllerBase
{
    private readonly AmendmentsManager _amendmentsManager;

    public AmendmentsController(AmendmentsManager amendmentsManager)
    {
        _amendmentsManager = amendmentsManager;
    }

    [HttpGet()]
    public Task<List<AmendmentDto>> GetAmendments() => _amendmentsManager.GetAmendmentsAsync();

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AddAmendmentRequest
    {
        public Guid LeaseId { get; init; } = default!;

        public Person[]? Exits { get; init; } = default!;
        public Person[]? Entries { get; init; } = default!;
        public int? NewRent { get; init; } = default!;
    }

    [HttpPost("add")]
    public Task<Guid> AddAmendment([FromBody] AddAmendmentRequest request) =>
        _amendmentsManager.AddAmendmentAsync(request.LeaseId, request.Exits?.ToList() ?? new List<Person>(), request.Entries?.ToList() ?? new List<Person>(), request.NewRent);
}

