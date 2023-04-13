using Dapr.Client;
using microkart.order.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using static microkart.shared.Constants.Dapr;

namespace microkart.Order;
public static class StartupExtensions
{
    public const string AppName = "Order Service";

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

    public static void AddSwaggerWithAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Order API", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }

    public static void AddDbContext(this WebApplicationBuilder builder)
    {

        var defaultConnectionString = builder.Configuration["ConnectionStrings:OrderDB"];

        Console.WriteLine($"ConnectionString: {defaultConnectionString}");

        builder.Services.AddDbContext<OrderDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));
    }
    public static async Task AddDbContextAsync(this WebApplicationBuilder builder)
    {

        using var client = new DaprClientBuilder().Build();
        var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "microkart-app-secret");
        var defaultConnectionString = secret["ORDER_DB_CONNECTION"];

        Console.WriteLine($"Result: {string.Join(", ", secret)}");
        Console.WriteLine($"ConnectionString: {defaultConnectionString}");

        builder.Services.AddDbContext<OrderDatabaseContext>(
                   options => options.UseSqlServer(defaultConnectionString));


    }

    public static void ApplyDatabaseMigration(this WebApplication app)
    {

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OrderDatabaseContext>();
        context.Database.Migrate();
    }

    public static void AddIdentityServer(this WebApplicationBuilder builder)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(options =>
            {
                options.Audience = "order-api";
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "order");
            });
        });
    }
}