using Application.Helpers;

namespace Presentation.Extensions;

public static class HttpContextExtensions
{
    public static  Guid UserId(this HttpContext context)
    {
        var userIdValue = context.GetClaimValue(ClaimsConstants.UserIdClaimName);
        return userIdValue is null ? Guid.Empty : Guid.Parse(userIdValue);
    }

    private static string? GetClaimValue(this HttpContext context, string claimType)
    {
        return context.User.Claims.FirstOrDefault(x => x.Type == claimType)?.Value;
    }
}