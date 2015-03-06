using System;
using Ya.Solvers;

namespace Ya.Fluent.ContainerConfiguration
{
    /// <summary>
    /// Configures the behavior when the services cannot be solved.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultSolvingFluent<T>
    {
        private readonly Container container;
        private readonly Action<IServiceSolver> setMethod;
        private readonly FluentContainerConfiguration configFluent;

        internal DefaultSolvingFluent(Container container, FluentContainerConfiguration configFluent, Action<IServiceSolver> setMethod)
        {
            this.setMethod = setMethod;
            this.container = container;
            this.configFluent = configFluent;
        }

        /// <summary>
        /// In case you select this option, the container will returns the default value of the service.
        /// </summary>
        public FluentContainerConfiguration ReturnDefault()
        {
            setMethod.Invoke(new DefaultSolver());
            return configFluent;
        }

        /// <summary>
        /// In case you select this option, the container will try to invoke the default constructor of the service.
        /// Which can throw exceptions if the service is a interface, is abstract or it does not have a default constructor.
        /// </summary>
        public SolvingAlterationWithFluentConfig TryCreateInstance()
        {
            var autoSolver = new AutoSolver();
            setMethod.Invoke(autoSolver);
            return new SolvingAlterationWithFluentConfig(autoSolver.Configuration, container, configFluent);
        }

        /// <summary>
        /// In case you select this option, the container wont try anything and a exception will throw.
        /// </summary>
        /// <returns></returns>
        public FluentContainerConfiguration DontTryAnything()
        {
            setMethod.Invoke(ServiceSolver.NullInstance);
            return configFluent;
        }

        /// <summary>
        /// In case you select this option, the container will execute the specified method.
        /// </summary>
        /// <param name="method">The method to execute.</param>
        /// <returns></returns>
        public SolvingAlterationWithFluentConfig Execute(Func<T, object> method)
        {
            var executionSolver = new ExecutionSolver<T>(method);
            setMethod.Invoke(executionSolver);
            return new SolvingAlterationWithFluentConfig(executionSolver.Configuration, container, configFluent);
        }
    }

}
