using Microsoft.AspNetCore.Mvc;

namespace microkart.gateway.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{

    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {ServiceName= "Catalog Service" });
    }
}
