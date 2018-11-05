using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthServer.Configuration;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AuthServer.DataAccess.Stores
{
    public class ResourceStore : IResourceStore
    {
        List<ApiResource> _apiResources = new List<ApiResource>();
        List<IdentityResource> _identityResources = new List<IdentityResource>();

        public ResourceStore()
        {
            _apiResources = (List<ApiResource>)AuthConfig.GetApiResources();
            _identityResources = (List<IdentityResource>)AuthConfig.GetIdentityResources();
        }

        private IEnumerable<ApiResource> GetAllApiResources()
        {
            return _apiResources.Where(x => x.Name.Length > 0);
        }

        private IEnumerable<IdentityResource> GetAllIdentityResources()
        {
            return _identityResources.Where(x => x.Name.Length > 0);
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            return Task.FromResult(_apiResources.Single<ApiResource>(a => a.Name == name));
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var list = _apiResources.Where<ApiResource>(a => a.Scopes.Any(s => scopeNames.Contains(s.Name)));

            return Task.FromResult(list.AsEnumerable());
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var list = _identityResources.Where<IdentityResource>(e => scopeNames.Contains(e.Name));
            return Task.FromResult(list.AsEnumerable());
        }

        public Task<Resources> GetAllResources()
        {
            var result = new Resources(GetAllIdentityResources(), GetAllApiResources());
            return Task.FromResult(result);
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            var result = new Resources(GetAllIdentityResources(), GetAllApiResources());
            return Task.FromResult(result);
        }
    }
}
