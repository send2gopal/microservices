using Dapr.Client;
using IdentityServer4.EntityFramework.DbContexts;
using microkart.identity;
using microkart.identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var builder = WebApplication.CreateBuilder(args);

//IDENTITY_DB_CONNECTION
string SECRET_STORE_NAME = "microkartsecretstore";
using var client = new DaprClientBuilder().Build();

System.Console.WriteLine("Starting application, please hold tight");
int counter = 1;
var isDaprReady = await client.CheckHealthAsync();

while (!isDaprReady && counter < 20)
{
    System.Console.WriteLine("Dapr detected not to be ready, sleeping");
    await Task.Delay(100);

    isDaprReady = await client.CheckHealthAsync();
    counter++;
}

//Using Dapr SDK to get a secret
var secret = await client.GetSecretAsync(SECRET_STORE_NAME, "microkart-app-secret");
Console.WriteLine($"Result: {string.Join(", ", secret)}");

//Add servcies
var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = secret["IDENTITY_DB_CONNECTION"];
//var defaultConnectionString = "Server=127.0.0.1;Database=IdentityDb;User Id=sa;Password=In%^$India&8$";


builder.Services.AddDbContext<MicrokartIdentityDbContext>(options =>
    options.UseSqlServer(defaultConnectionString,
        mig => mig.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<MicrokartIdentityDbContext>();
builder.Services.AddIdentityServer()
    .AddAspNetIdentity<IdentityUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = db => db.UseSqlServer(defaultConnectionString, options => options.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = db => db.UseSqlServer(defaultConnectionString, options => options.MigrationsAssembly(assembly));
    }).AddDeveloperSigningCredential();

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var persistedGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
    persistedGrantDbContext.Database.Migrate();

    var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
    configurationDbContext.Database.Migrate();
    
    var microkartIdentityDbContext = scope.ServiceProvider.GetRequiredService<MicrokartIdentityDbContext>();
    microkartIdentityDbContext.Database.Migrate();
}

if (seed)
{
    SeedData.EnsureSeedData(defaultConnectionString);
}

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
