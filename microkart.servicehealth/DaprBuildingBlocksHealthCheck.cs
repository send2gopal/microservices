using Dapr.Client;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace microkart.servicehealth
{
    public class DaprBuildingBlocksHealthCheck : IHealthCheck
    {
        private readonly DaprClient daprClient;

        public DaprBuildingBlocksHealthCheck(DaprClient daprClient)
        {
            this.daprClient = daprClient;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (await this.daprClient.CheckHealthAsync(cancellationToken))
                return HealthCheckResult.Healthy("Dapr sidecar is healthy.");
            return new HealthCheckResult(context.Registration.FailureStatus, "Dapr sidecar is rotten.");
        }
    }
}
