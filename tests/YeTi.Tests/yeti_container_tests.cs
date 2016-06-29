using NUnit.Framework;
using Shouldly;
using Xunit;

namespace YeTi.Tests
{   
    public class yeti_container_tests
    {
        [Fact]
        public void resolves_registered_components()
        {
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestImplementation>();
            var resolved_object = container.Resolve<ITestInterface>();
            resolved_object.ShouldBeOfType<TestImplementation>();
        }

        [Fact]
        public void resolves_components_with_ctor_with_dependency()
        {
            var container = new YeTiContainer();
            container.Register<ITestInterface, TestImplementationWithDependency>();
            container.Register<Dependency, Dependency>();
            var resolved_object = container.Resolve<ITestInterface>();
            resolved_object.ShouldBeOfType<TestImplementationWithDependency>();
        }

        public interface ITestInterface
        {
            
        }

        public class TestImplementation : ITestInterface
        {
            
        }

        class Dependency
        {

        }

        class TestImplementationWithDependency:ITestInterface
        {
            public TestImplementationWithDependency(Dependency d)
            {
                    
            }

        }
    }
}