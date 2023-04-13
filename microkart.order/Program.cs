
using microkart.servicehealth;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using microkart.Order;
using System.Text;
using AutoMapper;
using microkart.order;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddDapr();
builder.Services.AddEndpointsApiExplorer();


builder.AddSerilog();
builder.AddIdentityServer();
// Auto Mapper Configurations
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.AddSwaggerWithAuth();
builder.Services.AddHealthChecks().AddDapr();
builder.Services.AddDaprClient();
builder.AddDbContext();

// Add Application services
builder.Services.AddScoped<IEventBus, DaprEventBus>();
builder.Services.AddUserServcie();

var app = builder.Build();

ILogger logger = app.Services.GetService<ILogger<Program>>();

app.Use(async (context, next) =>
{
    var initialBody = context.Request.Body;

    using (var bodyReader = new StreamReader(context.Request.Body))
    {
        string body = await bodyReader.ReadToEndAsync();
        logger.LogWarning("Logging request Body {@body}", body);
        context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
        await next.Invoke();
        context.Request.Body = initialBody;
    }
});

app.MapHealthChecks("/health");


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCloudEvents();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapSubscribeHandler();




//app.ApplyDatabaseMigration();
try
{
    app.Logger.LogInformation("Starting web host ({ApplicationName})...", StartupExtensions.AppName);
    app.Run();
}
catch (Exception ex)
{
    app.Logger.LogCritical(ex, "Host terminated unexpectedly ({ApplicationName})...", StartupExtensions.AppName);
}
finally
{
    Serilog.Log.CloseAndFlush();
}

