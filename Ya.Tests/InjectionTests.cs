using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class InjectionTests
    {
        Container container;
        Foo objectToInject;

        [TestInitialize]
        public void Initialize() 
        {
            container = new Container();
            objectToInject = new Foo();
            container.Add<Foo>(objectToInject);
        }

        [TestMethod]
        public void InjectProperty()
        {
            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(objectToInject, objectToBeInjected.Member);
        }

        [TestMethod]
        public void InjectPrivateProperty()
        {
            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(objectToInject, objectToBeInjected.GetPrivateMember());
        }

        [TestMethod]
        public void InjectParentProperty()
        {
            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(objectToInject, objectToBeInjected.ParentMember);
            Assert.AreEqual(objectToInject, InjectableObject.StaticMember);
        }

        [TestMethod]
        public void InjectStaticProperty()
        {
            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(objectToInject, InjectableObject.StaticMember);
        }

        [TestMethod]
        public void InjectParentStaticProperty()
        {
            var objectToBeInjected = container.Inject(new InjectableObject());

            Assert.AreEqual(objectToInject, InjectableObject.ParentStaticMember);
        }

        [TestMethod]
        public void GetThingsInjected()
        {
            container.Add<InjectableObject>();

            Assert.AreEqual(objectToInject, container.GetObject<InjectableObject>().Member);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void WithUnknownServicesShouldThrowException()
        {
            container.Inject(new InjectableObjectOf<int>());
        }
    }
}
