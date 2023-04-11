using Microsoft.AspNetCore.Mvc;

namespace HomePilot.Controllers;

[ApiController]
[Route("amendments")]
public class AmendmentsController : ControllerBase
{
    [HttpGet("hello")]
    public string Hello() =>
        "Hello world";
}

