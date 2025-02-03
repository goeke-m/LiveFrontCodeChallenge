using CartonCaps.Services;
using CartonCaps.Tests.Architecture.CustomRules;
using NetArchTest.Rules;

namespace CartonCaps.Tests.Architecture
{
    [TestFixture]
    [Category(TestCategories.Architecture)]
    [Parallelizable(ParallelScope.All)]
    public class ServiceArchitectureTests : ArchitectureBaseTests
    {
        [Test]
        public void Services_ShouldHaveDependencyOnData()
        {
            var result = Types.InAssembly(Service).That().AreClasses().And().ResideInNamespace(ServiceServicesNamespace)
                .Should().HaveDependencyOn(DataNamespace).GetResult();

            PrintFailedResults(result, nameof(Services_ShouldHaveDependencyOnData));
            result.IsSuccessful.ShouldBeTrue("Service assembly should have a dependency on the data project");
        }

        [Test]
        public void Service_ShouldNotHaveDependencyOnWeb()
        {
            var result = Types.InAssembly(Service).ShouldNot().HaveDependencyOn(WebNamespace).GetResult();

            PrintFailedResults(result, nameof(Service_ShouldNotHaveDependencyOnWeb));
            result.IsSuccessful.ShouldBeTrue("Service assembly should have a dependency on the Web project");
        }

        [Test]
        public void Service_Names_ShouldEndWithService()
        {
            var result = Types.InAssembly(Service).That().AreClasses().And().ResideInNamespace(ServiceServicesNamespace).Should().HaveNameEndingWith("Service").GetResult();

            PrintFailedResults(result, nameof(Service_Names_ShouldEndWithService));
            result.IsSuccessful.ShouldBeTrue("Service names should end with 'Service'");
        }

        [Test]
        public void Services_ShouldImplementDependencyInJectionLifeCycleInterface()
        {
            var result = Types.InAssembly(Service).That().AreClasses().And().ResideInNamespace(ServiceServicesNamespace).Should().ImplementInterface(typeof(IScopedService)).GetResult();

            PrintFailedResults(result, nameof(Services_ShouldImplementDependencyInJectionLifeCycleInterface));
            result.IsSuccessful.ShouldBeTrue($"Service classes should implement {nameof(IScopedService)}");
        }

        [Test]
        public void Services_ShouldImplementAnInterfaceSpecificToTheService()
        {
            var interfaceImplementationRule = new InterfaceImplementationRule();
            var result = Types.InAssembly(Service).That().AreClasses().And().ResideInNamespace(ServiceServicesNamespace)
                .Should().MeetCustomRule(interfaceImplementationRule).GetResult();

            PrintFailedResults(result, nameof(Services_ShouldImplementAnInterfaceSpecificToTheService));
            result.IsSuccessful.ShouldBeTrue("Service classes should implement an interface specific to the service");
        }

        [Test]
        public void Service_InterfaceNames_ShouldStartWithI()
        {
            var result = Types.InAssembly(Service).That().AreInterfaces().Should().HaveNameStartingWith("I").GetResult();

            PrintFailedResults(result, nameof(Service_InterfaceNames_ShouldStartWithI));
            result.IsSuccessful.ShouldBeTrue("Service interface names should start with 'I'");
        }
    }
}
