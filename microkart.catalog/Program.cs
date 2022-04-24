using microkart.catalog;
using microkart.servicehealth;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.AddSerilog();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddDapr();
builder.Services.AddDaprClient();

builder.AddDbContextDevelopment();
//await builder.AddDbContextAsync();

var app = builder.Build();


app.MapHealthChecks("/health");

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();




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

