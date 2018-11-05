using IdentityServer4.Validation;
using IdentityModel;
using System.Threading.Tasks;
using AuthServer.DataAccess.Database;

namespace AuthServer.DataAccess
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            if (_userRepository.ValidateCredentials(context.UserName, context.Password))
            {
                var user = _userRepository.GetByUserName(context.UserName).Result;
                context.Result = new GrantValidationResult(user.UserName, OidcConstants.AuthenticationMethods.Password);
            }

            return Task.FromResult(0);
        }
    }
}
