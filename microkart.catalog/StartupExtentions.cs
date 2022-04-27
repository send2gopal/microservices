using Dapr.Client;
using microkart.catalog.Database;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static microkart.shared.Constants.Dapr;

public static class StartupExtensions
{
    public const string AppName = "Catalog Service";

    public static async Task AddDbContextAsync(this WebApplicationBuilder builder)
    {

        using var client = new DaprClientBuilder().Build();
        var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "microkart-app-secret");
        var defaultConnectionString = secret["CALATOG_DB_CONNECTION"];

        Console.WriteLine($"Result: {string.Join(", ", secret)}");
        Console.WriteLine($"ConnectionString: {defaultConnectionString}");
        
        builder.Services.AddDbContext<CatalogDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));


    }

    public static void AddDbContextDevelopment(this WebApplicationBuilder builder)
    {

        var defaultConnectionString = builder.Configuration["ConnectionStrings:CatalogDB"];

        Console.WriteLine($"ConnectionString: {defaultConnectionString}");

        builder.Services.AddDbContext<CatalogDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));
    }

    public static void ApplyDatabaseMigration(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CatalogDatabaseContext>();
        context.Database.Migrate();
    }

    public static void AddCustomAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "ordering");
            });
        });
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
}