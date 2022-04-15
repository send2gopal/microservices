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

    #region Product

    [HttpPost("AddProduct")]
    [ProducesResponseType(typeof(Brand), 201)]
    public async Task<Product> ProductCreateAsync([FromBody] ProductViewModel model)
    {

        var productCatagory = catalogDatabaseContext.ProductCatagories.Where(x => x.Id == model.categoryID).FirstOrDefault();

        var brand = catalogDatabaseContext.Brands.Where(x => x.Id == model.brandID).FirstOrDefault();

        var productImage = catalogDatabaseContext.ProductImages.Where(x => x.Id == model.imageId).FirstOrDefault();

        var product = new Product
        {
            Title = model.title,
            description = model.description,
            price = model.price,
            discount = model.discount,
            stock = model.stock,
            IsNew = model.IsNew,
            sale = model.sale,
            type = model.type,
            tags = model.tags,
            CraetedDate = DateTime.Now,
            CreatedBy = "gthakur",
            UpdatedBy = "gthakur",
            UpdatedDate = DateTime.Now,
            Category = productCatagory,
            Brand = brand,
            collection = model.collection,
            images = productImage
        };

        var Prodctresult = await catalogDatabaseContext.Products.AddAsync(product);
        await catalogDatabaseContext.SaveChangesAsync();
        return Prodctresult.Entity;
    }

    [HttpGet("products")]
    [ProducesResponseType(typeof(List<Product>), (int)HttpStatusCode.OK)]
    public Task<List<ProductViewModel>> ProductsAsync()
    {
        var products = (from product in catalogDatabaseContext.Products
                        join brand in catalogDatabaseContext.Brands on product.Brand.Id equals brand.Id
                        join category in catalogDatabaseContext.ProductCatagories on product.Category.Id equals category.Id
                        join images in catalogDatabaseContext.ProductImages on product.images.Id equals images.Id
                        select new ProductViewModel
                        {
                            title = product.Title,
                            description = product.description,
                            price = product.price,
                            discount = product.discount,
                            stock = product.stock,
                            IsNew = product.IsNew,
                            sale = product.sale,
                            type = product.type,
                            tags = product.tags,
                            createdDate = product.CraetedDate,
                            createdBy = product.CreatedBy,
                            updatedBy = product.UpdatedBy,
                            updatedDate = product.UpdatedDate,
                            categoryName = category.Name,
                            brandName = brand.Name,
                            collection = product.collection,
                            imagesrc = images.src
                        }).ToListAsync();



        return products;
    }

    [HttpPut("UpdateProduct/{Id}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public async Task<bool> Update(Product model)
    {
        catalogDatabaseContext.Entry(await catalogDatabaseContext.Products.FirstOrDefaultAsync(x => x.Id == model.Id)).CurrentValues.SetValues(model);
        return (await catalogDatabaseContext.SaveChangesAsync()) > 0;
    }

    [HttpDelete("RemoveProduct/{Id}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public IActionResult ProductDeleteAsync(int id)
    {
        var productTobeDeletetd = catalogDatabaseContext.Products.Find(id);
        catalogDatabaseContext.Products.Remove(productTobeDeletetd);
        catalogDatabaseContext.SaveChanges();
        return Ok(productTobeDeletetd);
    }


    #endregion

}
