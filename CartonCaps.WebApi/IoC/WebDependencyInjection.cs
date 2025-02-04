using Asp.Versioning;
using CartonCaps.Services.IoC;
using CartonCaps.Shared.Models;
using FluentValidation;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;

namespace CartonCaps.WebApi.IoC;

/// <summary>
/// Provides extension methods for registering web dependencies in the dependency injection container.
/// </summary>
public static class WebDependencyInjection
{
    /// <summary>
    /// Registers web dependencies in the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the web dependencies.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> to configure the services.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the registered services.</returns>
    public static void AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidation();
        services.AddServiceDependencies(configuration);
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);

        }).AddApiExplorer(options =>
         {
             options.GroupNameFormat = "'v'V";
             options.SubstituteApiVersionInUrl = true;
         });

        services.AddControllers().AddJsonOptions(static options =>
        {
            options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
            options.JsonSerializerOptions.AllowTrailingCommas = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Carton Caps Referrals API",
                Description = "Web API for managing referrals.",
                Version = "v1"
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            x.IncludeXmlComments(xmlPath);
        });
    }


    /// <summary>
    /// Adds FluentValidation services to the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the FluentValidation services.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the registered FluentValidation services.</returns>
    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        // By default these are registered as Scoped , but can be changed to Singleton or Transient
        services.AddValidatorsFromAssemblyContaining<CreateReferralRequestValidator>();

        // Only after all validators registrations
        var serviceDescriptors = services.Where(descriptor => typeof(IValidator) != descriptor.ServiceType
                   && typeof(IValidator).IsAssignableFrom(descriptor.ServiceType)
                   && descriptor.ServiceType.IsInterface).ToList();

        foreach (var descriptor in serviceDescriptors)
        {
            services.Add(new ServiceDescriptor(typeof(IValidator), p => p.GetRequiredService(descriptor.ServiceType), descriptor.Lifetime));
        }

        return services;
    }
}
