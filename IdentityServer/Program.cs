using IdentityServer;
using IdentityServer.Validators;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Context.IdentityResources)
    //.AddInMemoryApiResources(Context.ApiResources)
    .AddInMemoryApiScopes(Context.ApiScopes)
    .AddInMemoryClients(Context.Clients)
    .AddTestUsers(Context.Users)
    .AddSecretValidator<BcryptValidator>()
    .AddResourceOwnerValidator<UserValidator>()
   

    .AddDeveloperSigningCredential(); //nie u¿ywaæ na produkcji




var app = builder.Build();

app.UseIdentityServer();

app.Run();
