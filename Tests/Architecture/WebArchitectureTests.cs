using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;
using Shouldly;

namespace CartonCaps.Tests.Architecture;

[TestFixture]
[Category(TestCategories.Architecture)]
public class WebArchitectureTests : ArchitectureBaseTests
{
    [Test]
    public void Web_DoesNotHaveDependencyOnData()
    {
        var result = Types.InAssembly(Web).ShouldNot().HaveDependencyOn(DataNamespace).GetResult();

        PrintFailedResults(result, nameof(Web_DoesNotHaveDependencyOnData));
        result.IsSuccessful.ShouldBeTrue("Web assembly should not have a dependency on the data project");
    }

    [Test]
    public void Web_Controllers_ShouldHaveDependencyOnService()
    {
        var result = Types.InAssembly(Web).That().ResideInNamespace(WebControllerNamespace).Should().HaveDependencyOn(ServiceNamespace).GetResult();

        PrintFailedResults(result, nameof(Web_Controllers_ShouldHaveDependencyOnService));
        result.IsSuccessful.ShouldBeTrue("Web assembly should have a dependency on the service project");
    }

    [Test]
    public void Web_Controllers_ShouldNotReferenceEntities()
    {
        var result = Types.InAssembly(Web).That().HaveNameEndingWith("Controller").ShouldNot().HaveDependencyOn(DataEntityNamespace).GetResult();

        PrintFailedResults(result, nameof(Web_Controllers_ShouldNotReferenceEntities));
        result.IsSuccessful.ShouldBeTrue("Controllers should not reference entities");
    }

    [Test]
    public void Web_Controllers_MustEndWithController()
    {
        var result = Types.InAssembly(Web).That().Inherit(typeof(Controller)).Should().HaveNameEndingWith("Controller").GetResult();

        PrintFailedResults(result, nameof(Web_Controllers_MustEndWithController));
        result.IsSuccessful.ShouldBeTrue("All controllers should end with 'Controller'");
    }
}
