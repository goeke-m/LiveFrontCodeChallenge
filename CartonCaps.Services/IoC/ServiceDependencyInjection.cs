using CartonCaps.Data.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CartonCaps.Services.IoC;

/// <summary>
/// Provides extension methods for registering service dependencies in the dependency injection container.
/// </summary>
public static class ServiceDependencyInjection
{
    /// <summary>
    /// Registers all service dependencies in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the registered services.</returns>
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataDependencies(configuration);

        // Get all services that implement IScopedTpsService and register them as their interface with the IoC container
        var scopedServiceType = typeof(IScopedService);
        var scopedServiceTypes = Assembly.GetAssembly(typeof(ServiceDependencyInjection)).GetTypes()
            .Where(t => scopedServiceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
            .Select(t => new { Implementation = t, Interface = t.GetInterfaces().FirstOrDefault(i => i != scopedServiceType) })
            .Where(t => t.Interface != null)
            .ToList();

        foreach (var type in scopedServiceTypes)
        {
            services.AddScoped(type.Interface, type.Implementation);
        }

        return services;
    }
}

