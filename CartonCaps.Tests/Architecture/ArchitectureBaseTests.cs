using CartonCaps.Data.IoC;
using CartonCaps.Services.IoC;
using CartonCaps.WebApi.IoC;
using System.Reflection;

namespace CartonCaps.Tests.Architecture;

/// <summary>
/// Provides base functionality for architecture tests. All Architecture Test classes should inherit from this class.
/// </summary>
public class ArchitectureBaseTests
{
    public static Assembly Data => typeof(DataDependencyInjection).Assembly;
    public static Assembly Service => typeof(ServiceDependencyInjection).Assembly;
    public static Assembly Web => typeof(WebDependencyInjection).Assembly;

    public const string DataNamespace = "CartonCaps.Data";
    public const string DataEntityNamespace = "CartonCaps.Data.Entities";
    public const string ServiceNamespace = "CartonCaps.Services";
    public const string ServiceServicesNamespace = "CartonCaps.Services.Services";
    public const string WebNamespace = "CartonCaps.WebApi";
    public const string WebControllerNamespace = "CartonCaps.WebApi.Controllers";

    /// <summary>
    /// Prints the failed results of an architecture test, displaying the architecture test name and the types that failed the check in the output window.
    /// </summary>
    public static void PrintFailedResults(NetArchTest.Rules.TestResult results, string testName)
    {
        if (results.IsSuccessful) return;

        Console.WriteLine($"{testName} had the following violations:");
        foreach (var failingType in results.FailingTypeNames)
        {
            Console.WriteLine($"     - {failingType}");
        }
    }
}
