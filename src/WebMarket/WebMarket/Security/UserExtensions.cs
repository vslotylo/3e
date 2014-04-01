using System.Security.Principal;
using WebMarket.Common;
using WebMarket.Filters;

namespace WebMarket.Security
{
    [InitializeSimpleMembership]
    public static class UserExtensions
    {
        public static bool IsAdministrator(this IPrincipal user)
        {
            UsersInitializer.Initialize();
            return user.IsInRole(Constants.AdminRoleName);
        }
    }
}