using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServerPKCE
{
    public static class Context
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
           new IdentityResources.OpenId(),
           new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("api"),
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("api") {Scopes = new []{ "api" } }
        };

        public static IEnumerable<Client> Clients => new Client[]
        {new Client
            {
                ClientId = "pkce_client",
                ClientSecrets = {new Secret("secret".Sha256()) },
                AllowedScopes = { "api", StandardScopes.OpenId, StandardScopes.Profile },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:7052/signin-oidc" }
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
                Password = "nimda",
                IsActive = true,
                SubjectId = "admin"
            }
        };
    }
}
