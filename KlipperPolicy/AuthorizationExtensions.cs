using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;

namespace KlipperPolicy
{
    public static class AuthorizationExtensions
    {
        public static IServiceCollection AddAuthorizationPolicyRequirements(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, ReadAttendanceRequirementHandler>();
            return services;
        }

    }
}
