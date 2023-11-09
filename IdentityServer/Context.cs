using IdentityServer4.Models;

namespace IdentityServer
{
    public static class Context
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
           new IdentityResources.OpenId(),
           new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("WeatherForecast"),
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
            }
        };
    }
}
