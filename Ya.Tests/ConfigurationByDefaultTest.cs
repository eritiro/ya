using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Tests.Doubles;

namespace Ya.Tests
{
    [TestClass]
    public class ConfigurationByDefaultTest
    {
        [TestMethod]
        public void DefaultConfigurationShouldChangeBehavior()
        {
            var container = new Container();
            container.Configure
                .ByDefaultAllInstancesAre.UniquePer.Thread();
            container.Add<Foo>().UniquePer.Container();

            Assert.AreEqual(container.GetObject<Foo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void ParticularConfigurationOverridesDefaultForItsType()
        {
            var container = new Container();
            // set container default: unique per thread
            container.Configure
                .ByDefaultAllInstancesAre.UniquePer.Thread();
            // set Foo unique per call
            container.Add<Foo>().UniquePer.Call();

            Assert.AreNotEqual(container.GetObject<Foo>(), container.GetObject<Foo>());
        }

        [TestMethod]
        public void ParticularConfigurationDoesntChangeDefaultForOtherTypes()
        {
            var container = new Container();
            // set container default: unique per thread
            container.Configure
                .ByDefaultAllInstancesAre.UniquePer.Thread();
            // set Foo unique per call
            container.Add<Foo>().UniquePer.Call();
            // add AlternativeFoo
            container.Add<AlternativeFoo>();

            Assert.AreEqual(container.GetObject<AlternativeFoo>(), container.GetObject<AlternativeFoo>());
        }
    }
}
