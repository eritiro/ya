using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class SpecificImplementationsTest
    {
        Container container;
        
        [TestInitialize]
        public void Initialize()
        {
            container = new Container();
            container.Add<Foo>();
        }

        [TestMethod]
        public void ImplementationsChangeByType()
        {
            var foo1 = new Foo();
            var foo2 = new Foo();

            container.When<InjectableObject>().NeedsA<IFoo>().In(i => i.Member).YouShouldGive(foo1);
            container.When<InjectableObject2>().NeedsA<IFoo>().In(i => i.Member).YouShouldGive(foo2);

            var objectToBeInjected1 = container.Inject(new InjectableObject());
            var objectToBeInjected2 = container.Inject(new InjectableObject2());

            Assert.AreEqual(foo1, objectToBeInjected1.Member);
            Assert.AreEqual(foo2, objectToBeInjected2.Member);
        }

        [TestMethod]
        public void ImplementationsChangeByProperty()
        {
            var foo1 = new Foo();
            var foo2 = new Foo();

            container.When<InjectableObject>().NeedsA<Foo>().In(i => i.Member1).YouShouldGive(foo1);
            container.When<InjectableObject>().NeedsA<Foo>().In(i => i.Member2).YouShouldGive(foo2);

            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(foo1, objectToBeInjected.Member1);
            Assert.AreEqual(foo2, objectToBeInjected.Member2);
        }

        [TestMethod]
        public void SpecificTypeForAParticularProperty()
        {
            container.When<InjectableObject2>().NeedsA<IFoo>().In(i => i.Member).YouShouldGive<AlternativeFoo>();
            
            var objectToBeInjected = container.Inject(new InjectableObject2());

            Assert.IsInstanceOfType(objectToBeInjected.Member, typeof(AlternativeFoo));
        }

        [TestMethod]
        public void SpecificLambdaForAParticularProperty()
        {
            container.When<InjectableObject2>().NeedsA<IFoo>().In(i => i.Member).YouShouldGive(() => new AlternativeFoo());

            var objectToBeInjected = container.Inject(new InjectableObject2());

            Assert.IsInstanceOfType(objectToBeInjected.Member, typeof(AlternativeFoo));
        }
    }
}
