using IdentityServer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Context.IdentityResources)
    .AddInMemoryApiResources(Context.ApiResources)
    .AddInMemoryApiScopes(Context.ApiScopes)
    .AddInMemoryClients(Context.Clients)

    .AddDeveloperSigningCredential(); //nie u¿ywaæ na produkcji




var app = builder.Build();

app.UseIdentityServer();

app.Run();
