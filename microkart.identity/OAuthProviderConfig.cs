namespace microkart.identity.Services.Identity.API;

public class OAuthProviderConfig
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
                new ApiScope("basket", "Access to Basket API"),
                new ApiScope("ordering", "Access to Ordering API"),
                new ApiScope("shoppingaggr", "Access to Shopping Aggregator API")
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
                new ApiResource("basket-api", "Basket API")
                {
                    Scopes = { "basket" }

                },
                new ApiResource("ordering-api", "Ordering API")
                {
                    Scopes = { "ordering" }
                },
                new ApiResource("shoppingaggr-api", "Shopping Aggregator API")
                {
                    Scopes = { "shoppingaggr" }
                }
        };

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "angular-client",
                    ClientName = "Angular Client App",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    AllowedCorsOrigins = { configuration["BlazorClientUrlExternal"] },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration["BlazorClientUrlExternal"]}/signin-callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                    {
                        $"{configuration["BlazorClientUrlExternal"]}/signout-callback"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basket",
                        "ordering",
                        "shoppingaggr"
                    },
                },
                new Client
                {
                    ClientId = "basketswaggerui",
                    ClientName = "Basket Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["BasketApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["BasketApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket"
                    }
                },
                new Client
                {
                    ClientId = "orderingswaggerui",
                    ClientName = "Ordering Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["OrderingApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "ordering"
                    }
                },
                new Client
                {
                    ClientId = "shoppingaggrswaggerui",
                    ClientName = "Shopping Aggregator Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["ShoppingAggregatorApiUrlExternal"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["ShoppingAggregatorApiUrlExternal"]}/swagger/" },

                    AllowedScopes =
                    {
                        "basket",
                        "shoppingaggr"
                    }
                }
            };
    }
}
