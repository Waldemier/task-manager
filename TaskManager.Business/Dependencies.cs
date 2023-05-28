using Microsoft.Extensions.DependencyInjection;
using TaskManager.Business.Interfaces.Interfaces;
using TaskManager.Business.Services;

namespace TaskManager.Business;

internal static class Dependencies
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITaskService, TaskService>();
        
        return services;
    }
}