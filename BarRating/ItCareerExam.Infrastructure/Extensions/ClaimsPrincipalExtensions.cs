using System.Security.Claims;
using static ItCareerExam.Common.GlobalConstants;

namespace ItCareerExam.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? GetId(this ClaimsPrincipal user) => user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static bool IsAdmin(this ClaimsPrincipal user) => user.IsInRole(AdministratorRole);

        public static bool IsUser(this ClaimsPrincipal user) => user.IsInRole(UserRole);
    }
}
