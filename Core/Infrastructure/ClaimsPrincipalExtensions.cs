namespace Core.Infrastructure
{
    using System.Security.Claims;
    using static Core.Constants.Roles;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRole);
    }
}