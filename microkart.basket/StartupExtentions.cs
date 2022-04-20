using Dapr.Client;
using microkart.basket.Database;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using static microkart.shared.Constants.Dapr;
using Microsoft.Extensions.DependencyInjection;
public static class StartupExtensions
{
    private const string AppName = "Basket API";

    public static async Task AddDbContextAsync(this WebApplicationBuilder builder)
    {

        using var client = new DaprClientBuilder().Build();
        var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "microkart-app-secret");
        var defaultConnectionString = secret["BASKET_DB_CONNECTION"];

        Console.WriteLine($"Result: {string.Join(", ", secret)}");
        Console.WriteLine($"ConnectionString: {defaultConnectionString}");
        
        builder.Services.AddDbContext<BasketDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));

    }

    public static void AddDbContextDevelopment(this WebApplicationBuilder builder)
    {

        var defaultConnectionString = builder.Configuration["ConnectionStrings:BasketDB"];

        Console.WriteLine($"ConnectionString: {defaultConnectionString}");

        builder.Services.AddDbContext<BasketDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));
    }

    public static void ApplyDatabaseMigration(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BasketDatabaseContext>();
        context.Database.Migrate();
    }

    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        // Prevent mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Audience = "basket-api";
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });
    }

    public static void AddCustomAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "basket");
            });
        });
    }
}