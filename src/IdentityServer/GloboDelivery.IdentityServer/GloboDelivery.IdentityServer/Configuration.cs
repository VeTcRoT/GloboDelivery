using Duende.IdentityServer.Models;
using Duende.IdentityServer;

namespace GloboDelivery.IdentityServer
{
    public static class Configuration
    {
        public static string Admin = "admin";
        public static string Manager = "manager";
        public static string Customer = "customer";

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("GloboDelivery.API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("GloboDelivery.API")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "GloboDelivery.API",
                    ClientSecrets = { new Secret("GloboDelivery.API.Secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedCorsOrigins = { "https://localhost:7040" },
                    AllowedScopes = { "GloboDelivery.API",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },

                    RedirectUris = { "https://localhost:7040/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7040/signout-callback-oidc" }
                },
            };
    }
}
