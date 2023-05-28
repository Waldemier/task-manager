using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Application;

public static class Dependencies
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(typeof(Dependencies).Assembly));

        return services;
    }
}