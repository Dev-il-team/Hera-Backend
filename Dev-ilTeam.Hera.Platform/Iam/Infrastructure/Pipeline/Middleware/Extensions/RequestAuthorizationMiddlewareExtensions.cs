using Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace Dev_ilTeam.Hera.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
