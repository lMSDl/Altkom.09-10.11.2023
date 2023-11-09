using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

byte[] Key = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

/*builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Key)
    };
});*/

/*builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
        //options.LoginPath = "/login";
        options.AccessDeniedPath = "/bye";
    });*/

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7204";
    //options.Audience = "forecasts"; //ApiResource
    //options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/*app.MapGet("/login", async context =>
{
    if (context.Request.Query.TryGetValue("username", out var username) && context.Request.Query.TryGetValue("password", out var password))
    {
        if(username != password)
        {
            return;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor();

        var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, "Read"),
        new Claim(ClaimTypes.Role, "Write")
    };

        tokenDescriptor.Subject = new ClaimsIdentity(claims);
        tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);

        var token = tokenHandler.CreateToken(tokenDescriptor);

        await context.Response.WriteAsync(tokenHandler.WriteToken(token));
    }

});*/

/*app.MapGet("/login", async context =>
{
    var claims = new List<Claim> {
        new Claim(ClaimTypes.Name, "anonumouse"),
        new Claim(ClaimTypes.Role, "Read"),
        new Claim(ClaimTypes.Role, "Write") };

    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var claimPrincipal = new ClaimsPrincipal(claimIdentity);

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);
});
*/
app.Run();
