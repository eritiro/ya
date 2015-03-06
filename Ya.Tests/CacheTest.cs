using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class CacheTest
    {
        [TestMethod]
        public void WithoutCacheShouldGetDifferentInstances()
        {
            var container = new Container();
            container.Add<Foo>().UniquePer.Call();

            Assert.AreNotEqual(container.GetObject<Foo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void UniqueTypePerApplication() 
        {
            var container = new Container();
            container.Add<Foo>().UniquePer.Container();

            Assert.AreEqual(container.GetObject<Foo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void UniqueGenericPerApplication()
        {
            var container = new Container();
            container.AddGeneric<Foo<int>>().UniquePer.Container();

            Assert.AreEqual(container.GetObject<Foo<int>>(), container.GetObject<Foo<int>>());
        }

        [TestMethod]
        public void UniquePropertyPerApplication()
        {
            var container = new Container();
            container.When<InjectableObjectOf<Foo>>().NeedsA<Foo>().In(i => i.Member).YouShouldGive<Foo>()
                .UniquePer.Container();

            Foo foo1 = container.GetObject<InjectableObjectOf<Foo>>().Member;
            Foo foo2 = container.GetObject<InjectableObjectOf<Foo>>().Member;

            Assert.AreEqual(foo1, foo2);
        }

        [TestMethod]
        public void UniquePropertyShouldNotUseTypeCaches()
        {
            var container = new Container();
            container.Add<Foo>()
                .UniquePer.Container();
            container.When<InjectableObjectOf<Foo>>().NeedsA<Foo>().In(i => i.Member).YouShouldGive<Foo>()
                .UniquePer.Container();

            Foo foo1 = container.GetObject<Foo>();
            Foo foo2 = container.GetObject<InjectableObjectOf<Foo>>().Member;
            
            Assert.AreNotEqual(foo1, foo2);
        }

        [TestMethod]
        public void UniqueTypeShouldNotUsePropertyCaches()
        {
            var container = new Container();
            container.Add<Foo>()
                .UniquePer.Container();
            container.When<InjectableObjectOf<Foo>>().NeedsA<Foo>().In(i => i.Member).YouShouldGive<Foo>()
                .UniquePer.Container();

            Foo foo1 = container.GetObject<InjectableObjectOf<Foo>>().Member;
            Foo foo2 = container.GetObject<Foo>();
            
            Assert.AreNotEqual(foo1, foo2);
        }

        [TestMethod]
        public void UniqueTypePerThread()
        {
            var container = new Container();
            container.Add<Foo>().UniquePer.Thread();

            Assert.AreEqual(container.GetObject<Foo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void UniqueTypePerApplicationUsingInterfaces()
        {
            var container = new Container();
            container.Add<Foo>().UniquePer.Container();

            Assert.AreEqual(container.GetObject<IFoo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void ShouldResolveCircularReferences()
        {
            var container = new Container();
            container.Add<CircularReference>().UniquePer.Container();
            
            var circularReference = container.GetObject<CircularReference>();

            Assert.AreEqual(circularReference, circularReference.Member);
        }

        [TestMethod]
        public void CachedInstanceShouldNotBeReInjected()
        {
            const int INITIAL_VALUE = 15;
            const int FINAL_VALUE = 50;

            var container = new Container();
            container.Add<InjectableObjectOf<int>>().UniquePer.Container();
            container.When<InjectableObjectOf<int>>().NeedsA<int>().In(i => i.Member).YouShouldGive(INITIAL_VALUE);

            var first = container.GetObject<InjectableObjectOf<int>>();
            first.Member = FINAL_VALUE;

            // the same instance
            var second = container.GetObject<InjectableObjectOf<int>>();

            Assert.AreEqual(FINAL_VALUE, second.Member);
        }
    }
}
