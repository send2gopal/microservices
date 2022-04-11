using microkart.catalog.Database;
using microkart.catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace microkart.catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> logger;
    private readonly CatalogDatabaseContext catalogDatabaseContext;
    public CatalogController(CatalogDatabaseContext context, ILogger<CatalogController> logger)
    {
        this.catalogDatabaseContext = context;
        this.logger = logger;
        //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    [HttpGet("brands")]
    [ProducesResponseType(typeof(List<Brand>), (int)HttpStatusCode.OK)]
    public Task<List<Brand>> BrandsAsync() {
        return catalogDatabaseContext.Brands.ToListAsync();
    }



    [HttpGet("ProductCatagories")]
    [ProducesResponseType(typeof(List<ProductCatagory>), (int)HttpStatusCode.OK)]
    public Task<List<ProductCatagory>> ProductCatagoriesAsync() {
        return catalogDatabaseContext.ProductCatagories.ToListAsync();
    }

    [HttpGet("items/by_ids")]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<List<Product>>> ItemsAsync([FromQuery] string ids)
    {
        if (!string.IsNullOrEmpty(ids))
        {
            var numIds = ids.Split(',').Select(id => (Ok: int.TryParse(id, out int x), Value: x));
            if (numIds.All(nid => nid.Ok))
            {
                var idsToSelect = numIds.Select(id => id.Value);

                var items = await catalogDatabaseContext.Products
                    .Where(ci => idsToSelect.Contains(ci.Id))
                    .ToListAsync();

                return Ok(items);
            }
        }

        return BadRequest("Ids value is invalid. Must be comma-separated list of numbers.");
    }

    [HttpGet("products/paged")]
    [ProducesResponseType(typeof(PagedProductView), (int)HttpStatusCode.OK)]
    public async Task<PagedProductView> ItemsAsync(
        [FromQuery] int typeId = -1,
        [FromQuery] int brandId = -1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int pageIndex = 0)
    {
        var query = (IQueryable<Product>)catalogDatabaseContext.Products;

        if (typeId > -1)
        {
            query = query.Where(ci => ci.Brand.Id == typeId);
        }

        if (brandId > -1)
        {
            query = query.Where(ci => ci.Brand.Id == brandId);
        }

        var totalItems = await query
            .LongCountAsync();

        var itemsOnPage = await query
            .OrderBy(item => item.Name)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PagedProductView(pageIndex, pageSize, totalItems, itemsOnPage);
    }
}
