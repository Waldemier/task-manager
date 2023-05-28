using System.Net;

namespace TaskManager.Presentation.Middlewares;

internal sealed class CatchErrorMiddleware : IMiddleware
{
    private readonly ILogger<CatchErrorMiddleware> _logger;

    public CatchErrorMiddleware(ILogger<CatchErrorMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There is an exception in a domain has been thrown: {ex.Message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"An exception has been thrown in a {context.Request.Method} operation");
        }
    }
}

internal static class MiddlewareExtensions
{
    public static IServiceCollection RegisterCustomMiddlewares(this IServiceCollection services)
    {
        services.AddSingleton<CatchErrorMiddleware>();
        return services;
    }  
    
    public static IApplicationBuilder UseCatchErrorMiddleware(this IApplicationBuilder app) =>
        app.UseMiddleware<CatchErrorMiddleware>();
}