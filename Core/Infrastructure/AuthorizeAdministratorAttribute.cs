namespace Core.Infrastructure
{
    using Microsoft.AspNetCore.Authorization;
    using static Core.Constants.Roles;

    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute() => Roles = Administrator;
    }
}
