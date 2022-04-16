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
        this.catalogDatabaseContext = context ?? throw new Exception("DB context is null.");
        this.logger = logger;
        //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    //catalogDatabaseContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    [HttpGet("brands")]
    [ProducesResponseType(typeof(List<Brand>), (int)HttpStatusCode.OK)]
    public async Task<IEnumerable<Brand>> BrandsAsync()
    {
        return await catalogDatabaseContext.Brands.ToListAsync();
    }

    [HttpPost("brands")]
    [ProducesResponseType(typeof(Brand), 201)]
    public async Task<Brand> BrandsCreateAsync(BrandModel model)
    {
        var result = await catalogDatabaseContext.Brands.AddAsync(
            new Brand
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
            }
            );
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
            tags = model.tagsVal,
            CraetedDate = DateTime.Now,
            CreatedBy = "gthakur",
            UpdatedBy = "gthakur",
            UpdatedDate = DateTime.Now,
            Category = productCatagory,
            Brand = brand,
            collection = model.collectionVal,
            Variants = new List<ProductVariant> { 
                new ProductVariant {
                    sku="sku1",
                    size="s",
                    color="White"   
                },
                new ProductVariant {
                     sku="sku2",
                    size="m",
                    color="Yellow"
                },
                new ProductVariant {
                     sku="sku3",
                    size="m",
                    color="pink"
                },
            }
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
                        select new ProductViewModel
                        {
                            id = product.Id,
                            title = product.Title,
                            description = product.description,
                            price = product.price,
                            discount = product.discount,
                            stock = product.stock,
                            IsNew = product.IsNew,
                            sale = product.sale,
                            type = product.type,
                            tags = product.tags.Split(",", System.StringSplitOptions.None),
                            createdDate = product.CraetedDate,
                            createdBy = product.CreatedBy,
                            updatedBy = product.UpdatedBy,
                            updatedDate = product.UpdatedDate,
                            categoryName = category.Name,
                            brand = brand.Name,
                            collection = product.collection.Split(",", System.StringSplitOptions.None),
                            images = product.Images,
                            variants = product.Variants
                        }).ToListAsync();



        return products;
    }

    [HttpPut("UpdateProduct/{Id}")]
    [ProducesResponseType(typeof(Product), 200)]
    public async Task<IActionResult> Update(Product model)
    {
        var product = await catalogDatabaseContext.Products.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (product == null) return NotFound();
        product.Title = model.Title;
        product.description = model.description;
        product.price = model.price;
        product.discount = model.discount;
        product.stock = model.stock;
        product.IsNew = product.IsNew;
        product.sale = model.sale;
        product.type = model.type;
        product.tags = model.tags;

        //TBD mappings
        await catalogDatabaseContext.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("RemoveProduct/{Id}")]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    public IActionResult ProductDeleteAsync(int id)
    {
        var productTobeDeletetd = catalogDatabaseContext.Products.Find(id);
        if (productTobeDeletetd == null) return NotFound();
        catalogDatabaseContext.Products.Remove(productTobeDeletetd);
        catalogDatabaseContext.SaveChanges();
        return Accepted();
    }


    [HttpPost("MapProductImage")]
    [Consumes("multipart/form-data")]
    public async Task<ProductImages> UploadProdcutImageFile([FromForm] ImageModel model)
    {
        string fName = model.Image.FileName;
        string path = Path.Combine(Environment.CurrentDirectory, "Images/" + model.Image.FileName);
        var product = catalogDatabaseContext.Products.Where(x => x.Id == model.productId).FirstOrDefault();

        //Wil change to AWS s3
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await model.Image.CopyToAsync(stream);
        }

        var productImage = new ProductImages
        {
            src = path,
            CraetedDate = DateTime.Now,
            CreatedBy = "gthakur",
            UpdatedBy = "gthakur",
            UpdatedDate = DateTime.Now,
            product = product
        };

        var Prodctresult = await catalogDatabaseContext.ProductImages.AddAsync(productImage);

        await catalogDatabaseContext.SaveChangesAsync();
        return Prodctresult.Entity;
    }

}

#endregion


