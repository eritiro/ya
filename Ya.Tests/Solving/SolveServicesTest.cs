using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Tests.Doubles;

namespace Ya.Tests.Solving
{
    [TestClass]
    public class SolveServicesTest
    {
        [TestMethod]
        public void SolveInstanceServices()
        {
            var container = new Container();
            container.Add<string>("hola");
            var myFoo = new Foo();
            container.Add<Foo>(myFoo);

            Assert.AreEqual("hola", container.GetObject<string>());
            Assert.AreEqual(myFoo, container.GetObject<Foo>());
        }

        [TestMethod]
        public void SolveLambdaExpresionServices()
        {
            const int myNumber = 15;
            const string myString = "Hello";
            var myFoo = new Foo();
            var container = new Container();
            
            container.Add<int>(() => myNumber);
            container.Add<string>(() => myString);
            container.Add<Foo>(() => myFoo);

            Assert.AreEqual(myNumber, container.GetObject<int>());
            Assert.AreEqual(myString, container.GetObject<string>());
            Assert.AreEqual(myFoo, container.GetObject<Foo>());
        }


        [TestMethod]
        public void SolveClassInheritedFromGenerics()
        {
            var container = new Container();
            container.Add<InheritedFromGenerics>(new InheritedFromGenerics { Number=5, Generic="injectedValue" });

            var specializedFoo = container.GetObject<Foo<string>>();

            Assert.AreEqual(5, specializedFoo.Number);
            Assert.AreEqual("injectedValue", specializedFoo.Generic);
        }

        [TestMethod]
        public void ShouldSolveInOrder()
        {
            var container = new Container();
            // without other definitions, container should use autosolve
            Assert.AreEqual(0, container.GetObject<Foo>().Number);

            // prefer typesolve rather than autosolve
            container.Add<FooNumber1>();
            Assert.AreEqual(1, container.GetObject<Foo>().Number);
        }
    }
}
