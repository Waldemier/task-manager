using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Infrastructure;

public static class Dependencies
{
    private const string CoreLayerPath = "../TaskManager.Core/bin/Debug/net6.0/TaskManager.Core.dll";
    private const string BusinessLayerPath = "../TaskManager.Business/bin/Debug/net6.0/TaskManager.Business.dll";
    
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Dependencies).Assembly);

        return services;
    }

    public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("TaskManager.Core.Dependencies");
        var methodInfo = type?.GetMethod("RegisterContext");
        methodInfo?.Invoke(null, new object[] { services, configuration });
        
        return services;
    }
    
    public static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("TaskManager.Core.Dependencies");
        var methodInfo = type?.GetMethod("MigrateDatabase");
        methodInfo?.Invoke(null, new object[] { services });
        
        return services;
    }
    
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(CoreLayerPath);
        var type = assembly.GetType("TaskManager.Core.Dependencies");
        var methodInfo = type?.GetMethod("RegisterRepositories");
        methodInfo?.Invoke(null, new object[] { services });
        
        return services;
    }
    
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        var assembly = Assembly.LoadFrom(BusinessLayerPath);
        var type = assembly.GetType("TaskManager.Business.Dependencies");
        var methodInfo = type?.GetMethod("RegisterServices");
        methodInfo?.Invoke(null, new object[] { services });
        
        return services;
    }
}