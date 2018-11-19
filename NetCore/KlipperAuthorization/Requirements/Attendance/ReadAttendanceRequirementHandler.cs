using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Models.Core.Employment;

namespace KlipperAuthorization.Requirements.Attendance
{
    internal class ReadAttendanceRequirementHandler : AuthorizationHandler<ReadAttendanceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ReadAttendanceRequirement requirement)
        {
            var roleClaims = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            bool isHR = roleClaims.Where(c => c.Value == "HR").Count() > 0;
            bool isTeamLeader = roleClaims.Where(c => c.Value == "TeamLeader").Count() > 0;
            bool isEmployee = roleClaims.Where(c => c.Value == "Employee").Count() > 0;
            if(isHR)
            {
                context.Succeed(requirement);
            }
            else if(isTeamLeader)
            {
                var loggedInUser = SessionCache.CurrentEmployee;
                var employeeId = GetEmployeeId(context, loggedInUser);
                if(employeeId != -1 && loggedInUser.IsReportee(employeeId))
                {
                    context.Succeed(requirement);
                }
            }
            else
            {
                var loggedInUser = SessionCache.CurrentEmployee;
                var employeeId = GetEmployeeId(context, loggedInUser);
                if (employeeId != -1 && loggedInUser.ID == employeeId)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }

        private static int GetEmployeeId(AuthorizationHandlerContext context, Employee loggedInUser)
        {
            var mvcContext = context.Resource as
                        Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;
            var hasEmployeeId = mvcContext.RouteData.Values.ContainsKey("employeeId");
            if (hasEmployeeId)
            {
                var employeeId = int.Parse(mvcContext.RouteData.Values["employeeId"].ToString());
                return employeeId;
            }
            return -1;
        }
    }
}