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

//Add servcies
var assembly = typeof(Program).Assembly.GetName().Name;
var defaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnectionStr");

if (seed)
{
    SeedData.EnsureSeedData(defaultConnectionString);
}


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

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.Run();
