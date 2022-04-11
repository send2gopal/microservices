using microkart.catalog.Database;
using microkart.catalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace microkart.catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly ILogger<CatalogController> logger;
    private readonly CatalogDatabaseContext catalogDatabaseContext;
    public AdminController(CatalogDatabaseContext context, ILogger<CatalogController> logger)
    {
        this.catalogDatabaseContext = context;
        this.logger = logger;
        //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    [HttpGet("brands")]
    [ProducesResponseType(typeof(List<Brand>), (int)HttpStatusCode.OK)]
    public Task<List<Brand>> BrandsAsync()
    {
        return catalogDatabaseContext.Brands.ToListAsync();
    }

    [HttpPost("brands")]
    [ProducesResponseType(typeof(Brand), 201)]
    public async Task<Brand> BrandsCreateAsync(BrandModel model)
    {
        var brand = new Brand
        {
            Id = model.Id,
            BannerImage = model.BannerImage,
            Description = model.Description,
            Logo = model.Logo,
            Name = model.Name,
            CraetedDate = DateTime.Now,
            CreatedBy = "gthakur",
            UpdatedBy = "gthakur",
            UpdatedDate = DateTime.Now,
        };
        var result = await catalogDatabaseContext.Brands.AddAsync(brand);
        await catalogDatabaseContext.SaveChangesAsync();
        return result.Entity;
    }


    [HttpDelete("brands/{Id}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public Task<List<Brand>> BrandsDeleteAsync()
    {
        return catalogDatabaseContext.Brands.ToListAsync();
    }

    [HttpGet("products")]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
    public Task<List<Product>> ProductsAsync()
    {
        return catalogDatabaseContext.Products.ToListAsync();
    }

    [HttpPost("brands")]
    [ProducesResponseType(typeof(Brand), 201)]
    public async Task<Brand> ProductCreateAsync(ProductModel model)
    {
        var brand = new Brand
        {
            Id = model.Id,
            BannerImage = model.BannerImage,
            Description = model.Description,
            Logo = model.Logo,
            Name = model.Name,
            CraetedDate = DateTime.Now,
            CreatedBy = "gthakur",
            UpdatedBy = "gthakur",
            UpdatedDate = DateTime.Now,
        };
        var result = await catalogDatabaseContext.Brands.AddAsync(brand);
        await catalogDatabaseContext.SaveChangesAsync();
        return result.Entity;
    }


    [HttpDelete("brands/{Id}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public Task ProductDeleteAsync()
    {
        return catalogDatabaseContext.Products.ToListAsync();
    }
}
