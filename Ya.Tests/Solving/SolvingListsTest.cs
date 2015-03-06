using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests.Solving
{
    [TestClass]
    public class SolvingListsTest
    {
        [TestMethod]
        public void GetDifferentTypesInAnIList() 
        {
            var container = new Container();
            container.Add<Foo>();
            container.Add<AlternativeFoo>();

            var list = container.GetObject<IList<IFoo>>();

            Assert.IsInstanceOfType(list[0], typeof(Foo));
            Assert.IsInstanceOfType(list[1], typeof(AlternativeFoo));
        }

        [TestMethod]
        public void GetDifferentInstancesInACollection()
        {
            var container = new Container();
            var first = new Foo();
            var second = new Foo();

            container.Add(first);
            container.Add(second);
            
            var list = container.GetObject<ICollection<Foo>>();

            CollectionAssert.AreEqual(new Foo[] { first, second }, (ICollection)list);
        }

        [TestMethod]
        public void WithNoInstanceShouldReturnEmptyIList()
        {
            var container = new Container();
           
            var list = container.GetObject<IList<Foo>>();

            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void GetConcreteListShouldThrowException()
        {
            var container = new Container();
            container.Configure
                .IfCannotSolve.DuringAnExplicitCall.DontTryAnything();
            var first = new Foo();
            var second = new Foo();

            container.Add(first);
            container.Add(second);

            var list = container.GetObject<List<Foo>>();
        }
    }
}
