
using microkart.basket;
using microkart.servicehealth;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;
using Microkart.basket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddSerilog();
builder.AddIdentityServerAuthentication();
builder.AddIdentityServerAuthorization();



builder.Services.AddHealthChecks()
    .AddDapr();
builder.Services.AddDaprClient();

builder.AddDbContextDevelopment();
//await builder.AddDbContextAsync();

// Add Application services
builder.Services.AddScoped<IEventBus, DaprEventBus>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddUserServcie();

var app = builder.Build();


app.MapHealthChecks("/health");
app.UseMiddleware<LogHeaderMiddleware>();
// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

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

