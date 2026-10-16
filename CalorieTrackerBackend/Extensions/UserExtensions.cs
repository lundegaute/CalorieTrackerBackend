using System.Security.Claims;

namespace CalorieTracker.Extensions;

public static class UserExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var stringID = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return int.TryParse(stringID, out var userID) ? userID : throw new UnauthorizedAccessException("UserID not found");
    }

    public static string GetUserEmail(this ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);
        return email ?? string.Empty;
    }
}