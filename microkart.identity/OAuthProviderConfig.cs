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
                new ApiScope("order", "Access to Ordering API"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
                new ApiResource("basket-api", "Basket API")
                {
                    Scopes = { "basket" }

                },
                new ApiResource("", "Order API")
                {
                    Scopes = { "order" }
                }
        };

    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new List<Client>
            {
                new Client
                {
                    ClientId = "angular-client",
                    ClientName = "Angular Client Development",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    AllowedCorsOrigins = { configuration["FrontEndDev"], configuration["FrontEndProd"] },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration["FrontEndProd"]}/signin-callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                    {
                        $"{configuration["FrontEndProd"]}/signout-callback"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basket",
                        "order"
                    },
                },
                new Client
                {
                    ClientId = "angular-client-local",
                    ClientName = "Angular Client Local",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    AllowedCorsOrigins = { configuration["FrontEndDev"], configuration["FrontEndProd"] },

                    // where to redirect to after login
                    RedirectUris = { $"{configuration["FrontEndDev"]}/signin-callback" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris =
                    {
                        $"{configuration["FrontEndDev"]}/signout-callback"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "basket",
                        "order"
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
                        "order"
                    }
                }
            };
    }
}
