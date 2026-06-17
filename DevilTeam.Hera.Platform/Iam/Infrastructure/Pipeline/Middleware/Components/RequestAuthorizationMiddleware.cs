using DevilTeam.Hera.Platform.Iam.Application.Internal.OutboundServices;
using DevilTeam.Hera.Platform.Iam.Application.QueryServices;
using DevilTeam.Hera.Platform.Iam.Domain.Model.Queries;
using DevilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;

namespace DevilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

/**
 * RequestAuthorizationMiddleware is a custom middleware.
 * This middleware is used to authorize requests.
 * It validates a token is included in the request header and that the token is valid.
 * If the token is valid then it sets the user in HttpContext.Items["User"].
 */
public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    /**
     * InvokeAsync is called by the ASP.NET Core runtime.
     * It is used to authorize requests.
     * It validates a token is included in the request header and that the token is valid.
     * If the token is valid then it sets the user in HttpContext.Items["User"].
     */
    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        // CancellationToken is taken from the request, since it cannot be injected via DI in middleware.
        var cancellationToken = context.RequestAborted;

        // Requests that do not map to an endpoint (e.g. Swagger UI, static files) are let through.
        var endpoint = context.GetEndpoint();
        var allowAnonymous = endpoint?.Metadata.Any(m => m.GetType() == typeof(AllowAnonymousAttribute)) ?? true;
        if (allowAnonymous)
        {
            // [AllowAnonymous] attribute is set (or no endpoint), so skip authorization
            await next(context);
            return;
        }

        // get token from request header
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        // If a valid token is present, resolve the user and store it in HttpContext.Items["User"].
        // We do NOT throw when the token is missing or invalid: the [Authorize] attribute will
        // return a clean 401 Unauthorized when no user was set.
        if (token != null)
        {
            var userId = await tokenService.ValidateToken(token);
            if (userId != null)
            {
                var getUserByIdQuery = new GetUserByIdQuery(userId.Value);
                var user = await userQueryService.Handle(getUserByIdQuery, cancellationToken);
                context.Items["User"] = user;
            }
        }

        // call next middleware (the [Authorize] filter enforces the 401 if no user was set)
        await next(context);
    }
}