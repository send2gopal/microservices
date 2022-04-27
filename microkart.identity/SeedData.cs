namespace microkart.identity.Services.Identity.API;

public class SeedData
{
    public static async Task EnsureSeedData(IServiceScope scope, IConfiguration configuration, ILogger logger)
    {
        var retryPolicy = CreateRetryPolicy(configuration, logger);
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await retryPolicy.ExecuteAsync(async () =>
        {
            await context.Database.MigrateAsync();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = await userMgr.FindByNameAsync("gthakur");

            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "gthakur",
                    Email = "gopal.thakur@montclair.edu",
                    EmailConfirmed = true,
                    CardHolderName = "Gopal Thakur",
                    CardNumber = "4258965963256986",
                    CardType = 1,
                    City = "Clifton",
                    Country = "US",
                    Expiration = "01/26",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "Thakur",
                    Name = "Gopal",
                    PhoneNumber = "9586535896",
                    ZipCode = "08968",
                    State = "NJ",
                    Street = "19 Richland CT",
                    SecurityNumber = "123"
                };

                var result = userMgr.CreateAsync(alice, "India123$").Result;

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("gthakur created");
            }
            else
            {
                logger.LogDebug("gthakur already exists");
            }

            var bob = await userMgr.FindByNameAsync("someuser");

            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "someuser",
                    Email = "someuser@email.com",
                    EmailConfirmed = true,
                    CardHolderName = "Some User",
                    CardNumber = "4587968569863269",
                    CardType = 1,
                    City = "Montclair",
                    Country = "UA",
                    Expiration = "01/26",
                    Id = Guid.NewGuid().ToString(),
                    LastName = "Some",
                    Name = "User",
                    PhoneNumber = "5698659638",
                    ZipCode = "25396",
                    State = "NJ",
                    Street = "7 Richland CT",
                    SecurityNumber = "128"
                };

                var result = await userMgr.CreateAsync(bob, "India123$");

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                logger.LogDebug("someuser created");
            }
            else
            {
                logger.LogDebug("someuser already exists");
            }
        });
    }

    private static AsyncPolicy CreateRetryPolicy(IConfiguration configuration, ILogger logger)
    {
        var retryMigrations = false;
        bool.TryParse(configuration["RetryMigrations"], out retryMigrations);

        // Only use a retry policy if configured to do so.
        // When running in an orchestrator/K8s, it will take care of restarting failed services.
        if (retryMigrations)
        {
            return Policy.Handle<Exception>().
                WaitAndRetryForeverAsync(
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, retry, timeSpan) =>
                    {
                        logger.LogWarning(
                            exception,
                            "Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {retry})",
                            exception.GetType().Name,
                            exception.Message,
                            retry);
                    }
                );
        }

        return Policy.NoOpAsync();
    }
}
