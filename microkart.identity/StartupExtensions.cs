using Serilog;
using static microkart.shared.Constants.Dapr;
namespace microkart.identity.Services.Identity.API;

public static class StartupExtensions
{
    private const string AppName = "Identity Server";

    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
    }

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
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

    public static void AddCustomMvc(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllersWithViews();
    }

    public static void AddNonBreakingSameSiteCookies(this WebApplicationBuilder builder)
    {
        // Add this
        //builder.Services.ConfigureNonBreakingSameSiteCookies();
    }

    public static async Task AddDbContextAsync(this WebApplicationBuilder builder)
    {

        using var client = new DaprClientBuilder().Build();
        var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "microkart-app-secret");
        var defaultConnectionString = secret["IDENTITY_DB_CONNECTION"];

        Console.WriteLine($"Result: {string.Join(", ", secret)}");
        Console.WriteLine($"ConnectionString: {defaultConnectionString}");

        builder.Services.AddDbContext<ApplicationDbContext>(
                   options => options.UseSqlServer(defaultConnectionString));
    }

    public static void AddDbContextAsyncDevelopment(this WebApplicationBuilder builder) =>
        builder.Services.AddDbContext<ApplicationDbContext>(
            options => options.UseSqlServer(builder.Configuration["ConnectionStrings:IdentityDB"]));

    public static void AddCustomIdentity(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
    }

    public static void AddCustomIdentityServer(this WebApplicationBuilder builder)
    {
        var identityServerBuilder = builder.Services.AddIdentityServer(options =>
        {
            options.IssuerUri = "null";
            options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
                .AddInMemoryIdentityResources(OAuthProviderConfig.IdentityResources)
                .AddInMemoryApiScopes(OAuthProviderConfig.ApiScopes)
                .AddInMemoryApiResources(OAuthProviderConfig.ApiResources)
                .AddInMemoryClients(OAuthProviderConfig.GetClients(builder.Configuration))
                .AddAspNetIdentity<ApplicationUser>();

        // not recommended for production - you need to store your key material somewhere secure
        identityServerBuilder.AddDeveloperSigningCredential();
    }

    public static void AddCustomAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
    }

    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddSqlServer(builder.Configuration["ConnectionStrings:IdentityDB"],
                    name: "IdentityDB-check",
                    tags: new string[] { "IdentityDB" });
    }

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IProfileService, ProfileService>();
    }
}