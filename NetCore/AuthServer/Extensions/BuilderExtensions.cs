using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Models;
using MongoDB.Bson.Serialization;
using AuthServer.DataAccess.Stores;
using AuthServer.DataAccess.Database;
using AuthServer.DataAccess;
using Microsoft.AspNetCore.Authorization;
using AuthServer.DataAccess.PolicyRequirements;
using Microsoft.AspNetCore.Authorization.Policy;

namespace AuthServer.Extensions
{
    public static class BuilderExtensions
    {
        public static IIdentityServerBuilder AddStorageServicesBackedByDatabase(this IIdentityServerBuilder builder)
        {
            //Currently unused 
            builder.Services.AddTransient<IRoleRepository, RoleRepository>();
            builder.Services.AddTransient<IPermissionRepository, PermissionRepository>();
            builder.Services.AddTransient<IGenericRepository, GenericRepository>();

            //Below transients are used
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();

            return builder;
        }

        public static IIdentityServerBuilder AddUsers(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            //builder.AddProfileService<ProfileService>();
            builder.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            return builder;
        }

        public static IServiceCollection AddCustomPolicyRequirements(this IServiceCollection services)
        {
            services.AddTransient<IPolicyEvaluator, PolicyEvaluator>();
            //services.AddTransient<IAuthorizationHandler, LeaveApplicationRequirementHandler>();
            return services;
        }

        public static IIdentityServerBuilder AddPersistedGrants(this IIdentityServerBuilder builder)
        {
            builder.Services.AddSingleton<IPersistedGrantStore, PersistedGrantStore>();
            return builder;
        }

    }
}
