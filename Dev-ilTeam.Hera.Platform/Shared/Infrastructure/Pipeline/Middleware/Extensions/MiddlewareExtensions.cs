using Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Pipeline.Middleware.Components;

namespace Dev_ilTeam.Hera.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
