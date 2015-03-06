using System;
using System.Reflection;

namespace Ya.Fluent.ContainerConfiguration
{
    /// <summary>
    /// Configures the <see cref="Ya.Container"/> behavior when it can't solve a service.
    /// </summary>
    public class IfCannotSolveFluent
    {
        private readonly Container container;
        private readonly FluentContainerConfiguration configFluent;

        internal IfCannotSolveFluent(Container container, FluentContainerConfiguration configFluent)
        {
            this.container = container;
            this.configFluent = configFluent;
        }

        public DefaultSolvingFluent<PropertyInfo> DuringAnInjection
        {
            get { return new DefaultSolvingFluent<PropertyInfo>(container, configFluent, s => container.ActualContainer.Defaults.PropertySolver = s); }
        }

        public DefaultSolvingFluent<Type> DuringAnExplicitCall
        {
            get { return new DefaultSolvingFluent<Type>(container, configFluent, s => container.ActualContainer.Defaults.TypeSolver = s); }
        }
    }
}
