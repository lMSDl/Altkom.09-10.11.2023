using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer
{
    public static class Context
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
           new IdentityResources.OpenId(),
           new IdentityResources.Profile(),
           new IdentityResources.Email(),
           new IdentityResources.Phone(),
           new IdentityResource(JwtClaimTypes.Role, new []{JwtClaimTypes.Role, /*ClaimTypes.Role*/}) //dodajemy role do id_token
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("WeatherForecast") {UserClaims = new [] { /*ClaimTypes.Role,*/ JwtClaimTypes.Role } }, //dodajemy role do access_token
            new ApiScope("RainForecast"),
            new ApiScope("FutureForecast"),
            new ApiScope("SomeApi")
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("forecasts") {Scopes = ApiScopes.Select(x => x.Name).Where(x => x.Contains("Forecast")).ToList() }
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId = "forecast_client",
                ClientSecrets = {new Secret("secret".Sha256())},
                AllowedScopes = { "WeatherForecast" },
                AllowedGrantTypes = GrantTypes.ClientCredentials
            },
            new Client
            {
                ClientId = "future_client",
                ClientSecrets = {new Secret("secret".Sha256()), new Secret(BCrypt.Net.BCrypt.HashPassword("hardSecret")) { Type = "bcrypt" } },
                AllowedScopes = { "FutureForecast", "SomeApi" },
                AllowedGrantTypes = GrantTypes.ClientCredentials
            },
            new Client
            {
                ClientId = "ropc_client",
                ClientSecrets = {new Secret("secret".Sha256()), new Secret(BCrypt.Net.BCrypt.HashPassword("hardSecret")) { Type = "bcrypt" } },
                AllowedScopes = { "WeatherForecast", StandardScopes.Phone, StandardScopes.Email, StandardScopes.Profile, StandardScopes.OpenId, JwtClaimTypes.Role },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword
            }
        };


        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                Username = "admin",
                Claims = new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "alamakota"),
                        new Claim(JwtClaimTypes.Email, "aa@bb.cc"),
                        new Claim(JwtClaimTypes.WebSite, "https://aa.bb.cc"),
                        new Claim(JwtClaimTypes.PhoneNumber, "1234586789"),
                        new Claim(JwtClaimTypes.Address, "11-111 Waszawa, Krakowska 15"),

                        /*new Claim(ClaimTypes.Role, "Read"),*/
                        new Claim(JwtClaimTypes.Role, "Read"),
                        new Claim(JwtClaimTypes.Role, "Write")
                    },
                Password = "$2a$12$xX44w3DOfyZ6zMY9sbsidek01ydvqqlV/UHITSQIfaB8heWDipKy.",
                IsActive = true,
                SubjectId = "admin"
            }
        };
    }
}
