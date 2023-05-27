using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Database;

namespace TaskManager.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Dependencies).Assembly);
        
        services.AddDbContext<ITaskManagerDbContext, TaskManagerDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("TaskManagerDbContextSettings"), new MySqlServerVersion("8.0"), options => 
                options.MigrationsAssembly(typeof(Dependencies).Assembly.FullName)));
        
        return services;
    }

    public static IServiceCollection MigrateDatabase(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var context = provider.GetRequiredService<TaskManagerDbContext>();
        context.Database.Migrate();
        context.Database.EnsureCreated();
        
        return services;
    } 
}