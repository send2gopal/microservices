var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentityServer()
                .AddInMemoryClients(new List<Client>())
                .AddDeveloperSigningCredential();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

//https://www.youtube.com/watch?v=HJQ2-sJURvA