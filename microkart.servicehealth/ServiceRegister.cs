using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using microkart.shared.Services;
using Microsoft.AspNetCore.Http;

namespace microkart.servicehealth
{
    public static class ServiceRegister
    {
        public static IHealthChecksBuilder AddDapr(this IHealthChecksBuilder builder) =>
       builder.AddCheck<DaprBuildingBlocksHealthCheck>("dapr");

        public static void AddUserServcie(this IServiceCollection services) {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserService, UserService>();
        }

    }
}
