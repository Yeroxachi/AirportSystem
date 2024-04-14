using Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters;

public class AuthorizeRolesAttribute : TypeFilterAttribute
{
    public AuthorizeRolesAttribute(params string[] roles) : base(typeof(AuthorizeRolesFilter))
    {
        Arguments = new object[] { roles };
    }
}

public class AuthorizeRolesFilter : IAuthorizationFilter
{
    readonly string[] _roles;

    public AuthorizeRolesFilter(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roleClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimsConstants.UserRoleClaimName)?.Value;
        if (roleClaim == null || !_roles.Contains(roleClaim))
        {
            context.Result = new ForbidResult();
        }
    }
}