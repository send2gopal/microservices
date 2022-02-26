using IdentityServer4.Models;

namespace microkart.identity
{
    public static class SeedConfig
    {

        public static IEnumerable<IdentityResource> IdentityResources =>
          new[]
          {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
              Name = "role",
              UserClaims = new List<string> {"role"}
            }
          };

        public static IEnumerable<ApiScope> ApiScopes =>
          new[]
          {
            new ApiScope("microkartapi.read"),
            new ApiScope("microkartapi.write"),
          };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
          new ApiResource("weatherapi")
          {
            Scopes = new List<string> { "microkartapi.read", "microkartapi.write"},
            ApiSecrets = new List<Secret> {new Secret("microkartapiSecret".Sha256())},
            UserClaims = new List<string> {"role"}
          }
        };

        public static IEnumerable<Client> Clients =>
          new[]
          {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedScopes = { "microkartapi.read", "microkartapi.write" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {"https://localhost:5444/signin-oidc"},
                FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                PostLogoutRedirectUris = {"https://localhost:5444/signout-callback-oidc"},

                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "microkartapi.read"},
                RequirePkce = true,
                RequireConsent = true,
                AllowPlainTextPkce = false
            },
          };
    }
}
