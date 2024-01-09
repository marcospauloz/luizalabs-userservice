namespace luizalabs.UserService.API.Authorization;

using luizalabs.UserService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            return;
        }

        if (context.HttpContext.Items["User"] is not User)
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }
    }
}