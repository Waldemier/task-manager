using System.Reflection;

namespace TaskManager.Business.Interfaces;

public static class Dependencies
{
    private const string BusinessAssemblyPath = "../TaskManager.Business/bin/Debug/net6.0/TaskManager.Business.dll";
    
    // public static IServiceCollection RegisterServices(this IServiceCollection services)
    // {
    //     services.AddScoped(typeof(IUserRepository), GetServiceImplementationType(typeof(IUserRepository)));
    //     services.AddScoped(typeof(ITaskRepository), GetServiceImplementationType(typeof(ITaskRepository)));
    //     
    //     return services;
    // }

    private static Type GetServiceImplementationType(Type interfaceType)
    {
        var businessAssembly = Assembly.LoadFrom(BusinessAssemblyPath);
        var implementationTypes = businessAssembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && interfaceType.IsAssignableFrom(t));
        return implementationTypes.Single();
    }
}