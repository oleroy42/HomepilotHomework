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

    [HttpGet("hello")]
    public string Hello() =>
        "Hello world";


    [HttpGet()]
    public Task<List<AmendmentDto>> GetAmendments() => _amendmentsManager.GetAmendments();
}

