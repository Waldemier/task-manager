using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Business;

internal static class Dependencies
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        return services;
    }
}