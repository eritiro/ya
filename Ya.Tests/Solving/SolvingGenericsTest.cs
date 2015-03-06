using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests.Solving
{
    [TestClass]
    public class SolvingGenericsTest
    {
        [TestMethod]
        public void SolveGenerics()
        {
            var container = new Container();
            container.AddGeneric<Foo<object>>();

            Assert.IsInstanceOfType(container.GetObject<IFoo<string>>(), typeof(Foo<string>));
            Assert.IsInstanceOfType(container.GetObject<IFoo<int>>(), typeof(Foo<int>));
            Assert.IsInstanceOfType(container.GetObject<IFoo<Foo>>(), typeof(Foo<Foo>));
        }

        [TestMethod]
        public void ShouldWorkWithConstraints()
        {
            var container = new Container();
            container.AddGeneric<WhereFoo<IFoo>>();

            var whereFoo = container.GetObject<WhereFoo<Foo>>();
            var iwhereFoo = container.GetObject<IGeneric<Foo>>();

            Assert.IsInstanceOfType(whereFoo, typeof(WhereFoo<Foo>));
            Assert.IsInstanceOfType(iwhereFoo, typeof(WhereFoo<Foo>));
        }

        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void WithWrongConstraintsShouldThrowCannotSolveException()
        {
            var container = new Container();
            container.AddGeneric<WhereFoo<IFoo>>();

            var whereFoo = container.GetObject<IGeneric<int>>();
        }

        [TestMethod]
        public void WithDifferentImplementationsShouldDecideBetweenConstraints()
        {
            var container = new Container();
            container.AddGeneric<WhereFoo<IFoo>>();
            container.AddGeneric<WhereNonFoo<NonFoo>>();

            var whereFoo = container.GetObject<IGeneric<Foo>>();
            var whereNonFoo = container.GetObject<IGeneric<NonFoo>>();

            Assert.IsInstanceOfType(whereFoo, typeof(WhereFoo<Foo>));
            Assert.IsInstanceOfType(whereNonFoo, typeof(WhereNonFoo<NonFoo>));
        }

        [TestMethod]
        [ExpectedException(typeof(YaException))]
        public void ShouldNotAllowNonGenericTypes()
        {
            var container = new Container();
            container.AddGeneric<Foo>();
        }
    }
}
