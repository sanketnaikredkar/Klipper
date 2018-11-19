using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace KlipperPolicy
{
    internal class ReadAttendanceRequirementHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}