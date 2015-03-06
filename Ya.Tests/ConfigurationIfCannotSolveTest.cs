using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class ConfigurationIfCannotSolveTest
    {
        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void DuringAExplicitCallDontTryAnything() 
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnExplicitCall.DontTryAnything();

            container.GetObject<int>();
        }

        [TestMethod]
        public void DuringAInjectionReturnDefault()
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnInjection.ReturnDefault();
            container.AddGeneric<InjectableObjectOf<object>>();

            Assert.AreEqual(default(int), container.GetObject<InjectableObjectOf<int>>().Member);
        }

        [TestMethod]
        public void DuringAExplicitCallReturnDefault()
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnExplicitCall.ReturnDefault();

            Assert.AreEqual(default(int), container.GetObject<int>());
            Assert.AreEqual(default(Foo), container.GetObject<Foo>());
        }

        [TestMethod]
        public void DuringAExplicitCallTryCreateInstance()
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnExplicitCall.TryCreateInstance();

            Assert.IsNotNull(container.GetObject<Foo>());
        }

        [TestMethod]
        public void DuringAExplicitCallExecute()
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnExplicitCall.Execute(type => type.FullName);

            Assert.AreEqual(typeof(string).FullName, container.GetObject<string>());
        }

        [TestMethod]
        public void DuringAInjectionExecute()
        {
            Container container = new Container().Configure
                .IfCannotSolve.DuringAnInjection.Execute(prop => prop.Name);
            container.AddGeneric<InjectableObjectOf<object>>();

            Assert.IsNotNull("Member", container.GetObject<InjectableObjectOf<string>>().Member);
        }

        [TestMethod]
        public void FluentIntegrationShouldNotBreak()
        {
            Container container = new Container();

            // integration between two if-cannot-solve
            container.Configure
                .IfCannotSolve.DuringAnInjection.DontTryAnything()
                .IfCannotSolve.DuringAnExplicitCall.TryCreateInstance();

            // integration between solving-with-config and config
            container.Configure
                .IfCannotSolve.DuringAnExplicitCall.TryCreateInstance()
                .IfCannotSolve.DuringAnInjection.DontTryAnything();

            // integration between solving with alterations and config
            container.Configure
                .IfCannotSolve.DuringAnExplicitCall.TryCreateInstance()
                    .UniquePer.Call()
                .IfCannotSolve.DuringAnInjection.DontTryAnything();

            // integration between by-default and if-cannot-solve
            container.Configure
                .ByDefaultAllInstancesAre.UniquePer.Call()
                .IfCannotSolve.DuringAnInjection.DontTryAnything();

            // integration between if-cannot-solve and by-default
            container.Configure
                .ByDefaultAllInstancesAre.UniquePer.Call()
                .IfCannotSolve.DuringAnInjection.DontTryAnything();
        }
    }
}
