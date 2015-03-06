using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ya.Support;
using Ya.Tests.Doubles;

namespace Ya.Tests.Solving
{
    [TestClass]
    public class AutoSolving
    {
        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void EmptyContainerCanNotResolveInstancesWithoutDefaultConstructor()
        {
            new Container().GetObject<string>();
        }

        [TestMethod]
        public void EmptyContainerCanResolveInstancesWithDefaultConstructor()
        {
            new Container().GetObject<Foo>();
            new Container().GetObject<int>();
        }

        [TestMethod]
        [ExpectedException(typeof(CannotSolveException))]
        public void EmptyContainerCanNotUseAutosolveInInject()
        {
            new Container().Inject(new InjectableObjectOf<int>());
        }
    }
}
