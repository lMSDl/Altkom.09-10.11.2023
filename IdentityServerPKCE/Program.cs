using IdentityServerPKCE;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseSuccessEvents = true;
})
    .AddInMemoryIdentityResources(Context.IdentityResources)
    .AddInMemoryApiResources(Context.ApiResources)
    .AddInMemoryApiScopes(Context.ApiScopes)
    .AddInMemoryClients(Context.Clients)
    .AddTestUsers(Context.Users)

    .AddDeveloperSigningCredential(); //nie u�ywa� na produkcji

var app = builder.Build();

app.UseIdentityServer();


app.MapDefaultControllerRoute();

app.Run();
