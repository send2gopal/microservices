using microkart.catalog;
using microkart.servicehealth;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var CorsPolicy = "allowedOrigin";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(CorsPolicy,
//                          policy =>
//                          {
//                              policy.WithOrigins("http://localhost:7000",
//                                                  "http://localhost:4200",
//                                                  "http://localhost:7001")
//                                                  .AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                          });
//});

builder.AddSerilog();
// Add services to the container.
builder.Services.AddControllers().AddDapr();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddDapr();
builder.Services.AddDaprClient();

builder.AddDbContextDevelopment();

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
app.UseMiddleware<LogHeaderMiddleware>();

//app.UseCors(CorsPolicy); 

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCloudEvents();
app.UseAuthorization();
app.MapControllers();
app.MapSubscribeHandler();

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

