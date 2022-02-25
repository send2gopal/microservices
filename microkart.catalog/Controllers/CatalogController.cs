using Microsoft.AspNetCore.Mvc;

namespace microkart.catalog.Controllers;

[ApiController]
[Route("catalog")]
public class CatalogController : ControllerBase
{

    private readonly ILogger<CatalogController> _logger;

    public CatalogController(ILogger<CatalogController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {ServiceName= "Catalog Service" });
    }
}
