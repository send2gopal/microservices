using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.servicehealth
{
    public static class ServiceRegister
    {
        public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder) =>
       builder.AddCheck<DaprBuildingBlocksHealthCheck>("dapr");
    }
}
