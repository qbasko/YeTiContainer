using NUnit.Framework;
using Shouldly;
using Xunit;

namespace YeTi.Tests
{   
    public class yeti_container_tests
    {
        
        ITestInterface act()
        {
           return _container.Resolve<ITestInterface>();
        }

        [Fact]
        public void resolves_registered_components()
        {
            _container.Register<ITestInterface, TestImplementation>();

            var resolved_object = act();

            resolved_object.ShouldBeOfType<TestImplementation>();
        }

        [Fact]
        public void resolves_components_with_ctor_with_dependency()
        {
            _container.Register<ITestInterface, TestImplementationWithDependency>();
            _container.Register<Dependency, Dependency>();

            var resolved_object = act();

            resolved_object.ShouldBeOfType<TestImplementationWithDependency>();
        }

        [Fact]
        public void throws_when_component_has_multiple_ctors()
        {
            _container.Register<ITestInterface, TestImplementationWithMultipleCtors>();
            _container.Register<Dependency, Dependency>();

            var exc = Record.Exception(() => act());

            exc.ShouldNotBe(null);
            exc.ShouldBeOfType<ComponentHasMultipleCtorsException>();            
        }

        readonly YeTiContainer _container;

        public yeti_container_tests()
        {
            _container = new YeTiContainer();
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

        class TestImplementationWithMultipleCtors : ITestInterface
        {
            public TestImplementationWithMultipleCtors()
            {

            }

            public TestImplementationWithMultipleCtors(Dependency d)
            {

            }

        }
    }
}