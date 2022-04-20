
using microkart.servicehealth;
using microkart.shared.Abstraction;
using microkart.shared.Daprbuildingblocks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddDapr();
builder.Services.AddDaprClient();

//builder.AddDbContextDevelopment();
await builder.AddDbContextAsync();

// Add Application services
builder.Services.AddScoped<IEventBus, DaprEventBus>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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
app.UseCloudEvents();
//app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();




app.ApplyDatabaseMigration();

app.Run();

