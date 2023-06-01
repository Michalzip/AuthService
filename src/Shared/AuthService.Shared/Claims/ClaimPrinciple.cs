using System.Security.Claims;

namespace AuthService.Shared.Claims
{
    public static class ClaimPrinciple
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userId = user?.FindFirst(ClaimTypes.NameIdentifier).Value;

            return new Guid(userId);
        }
    }
}