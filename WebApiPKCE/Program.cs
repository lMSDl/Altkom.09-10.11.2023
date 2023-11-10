var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7069";
        options.ClientId = "pkce_client";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.UsePkce = true;
        options.SaveTokens = true;
        options.Scope.Add("api");
    });

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
