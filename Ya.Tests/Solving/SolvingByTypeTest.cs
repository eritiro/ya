using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests.Solving
{
    [TestClass]
    public class SolvingByType
    {
        [TestMethod]
        public void SolveServicesByType()
        {
            var container = new Container();
            container.Add<Foo>();

            Assert.IsInstanceOfType(container.GetObject<Foo>(), typeof(Foo));
            Assert.IsInstanceOfType(container.GetObject<BaseFoo>(), typeof(Foo));
            Assert.IsInstanceOfType(container.GetObject<IFoo>(), typeof(Foo));
        }

        [TestMethod]
        public void SolveServicesByTypeWithPrivateConstructor()
        {
            var container = new Container();
            container.Add<FooWithPrivateConstructor>();

            Assert.IsInstanceOfType(container.GetObject<IFoo>(), typeof(FooWithPrivateConstructor));
        }

        [TestMethod]
        [ExpectedException(typeof(YaException))]
        public void CanNotAddTypesWithoutDefaultConstructor()
        {
            var container = new Container();
            container.Add<String>();
        }

        [TestMethod]
        [ExpectedException(typeof(YaException))]
        public void CanNotAddTypesOfInterfaces()
        {
            var container = new Container();
            container.Add<IFoo>();
        }

    }
}
