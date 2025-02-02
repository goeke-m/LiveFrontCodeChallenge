namespace CartonCaps.Services;

/// <summary>
/// This interface is used to register the Scoped Tps Services with the IoC container. Every service's interface within the Tps.Service namespace 
/// that should be registered within the IoC container under a Scoped Life Cycle should implement this interface in addition to any other interfaces.
/// See <seealso cref="IoC.ServiceDependencyInjection"/> for the registration of the services.
/// <para>Dependency Injection Life Cycle documentation: <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection#service-lifetimes" /></para>
/// <para>Dependency Injection in .NET Core documentation: <see href="https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection" /></para>
/// </summary>    
public interface IScopedService { }
