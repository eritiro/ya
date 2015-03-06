using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class ImplementationFinderTest
    {

        [TestMethod]
        public void SolveFromAssemblyWithImplementAttribute()
        {
            var container = new Container();
            container.AddAssemblyOf(this);

            var implementation = container.GetObject<InterfaceWithOneImplementationWithImplementsAttribute>();

            Assert.IsInstanceOfType(implementation, typeof(ClassWithImplementsAttribute));
        }

        [TestMethod]
        public void ImplementationWithGenericsShouldRegisterServiceAsGeneric()
        {
            var container = new Container();
            container.AddAssemblyOf(this);

            var implementation = container.GetObject<IGeneric<string>>();

            Assert.IsInstanceOfType(implementation, typeof(Generic<string>));
        }


        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void SolveFromAssemblyWithoutImplementAttribute()
        {
            var container = new Container();
            container.AddAssemblyOf<Foo>();

            container.GetObject<InterfaceWithOnlyOneImplementationWithoutImplementsAttribute>();
        }

        
        [TestMethod]
        public void AddAssemblyOfObject()
        {
            var container = new Container();
            container.AddAssemblyOf(this);

            var implementation = container.GetObject<InterfaceWithOneImplementationWithImplementsAttribute>();

            Assert.IsInstanceOfType(implementation, typeof(ClassWithImplementsAttribute));
        }

        [TestMethod]
        public void AddAssembly()
        {
            var container = new Container();
            container.AddAssembly(Assembly.GetExecutingAssembly());

            var implementation = container.GetObject<InterfaceWithOneImplementationWithImplementsAttribute>();

            Assert.IsInstanceOfType(implementation, typeof(ClassWithImplementsAttribute));
        }

    }
}
