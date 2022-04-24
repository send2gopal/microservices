using Dapr.Client;
using microkart.basket.Database;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using static microkart.shared.Constants.Dapr;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Microkart.basket;
public static class StartupExtensions
{
    public const string AppName = "Basket Service";

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

    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];
        Console.WriteLine($"seqServerUrl: {seqServerUrl}");
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

        builder.Host.UseSerilog();
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

    public static void AddIdentityServerAuthentication(this WebApplicationBuilder builder)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Audience = "basket-api";
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });
    }

    public static void AddIdentityServerAuthorization(this WebApplicationBuilder builder)
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