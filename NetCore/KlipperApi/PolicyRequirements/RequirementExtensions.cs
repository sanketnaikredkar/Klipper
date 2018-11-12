using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;

namespace KlipperApi.Extensions
{
    public static class RequirementExtensions
    {
        public static IServiceCollection AddCustomPolicyRequirements(this IServiceCollection services)
        {
            //services.AddTransient<IAuthorizationHandler, LeaveApplicationRequirementHandler>();
            return services;
        }

    }
}
