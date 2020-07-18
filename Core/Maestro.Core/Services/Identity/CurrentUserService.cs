namespace Core.Services.Identity
{
    using Core.Infrastructure;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }

        public bool IsAdministrator => user.IsAdministrator();
    }
}