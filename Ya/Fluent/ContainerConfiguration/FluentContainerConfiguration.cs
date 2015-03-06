namespace Ya.Fluent.ContainerConfiguration
{
    /// <summary>
    /// Configures the container behavior.
    /// </summary>
    public class FluentContainerConfiguration : IFluentContainerConfiguration
    {
        private readonly Container container;

        internal FluentContainerConfiguration(Container container)
        {
            this.container = container;
        }

        /// <summary>
        /// Configures the <see cref="Ya.Container"/> default behavior.
        /// </summary>
        public SolvingAlterationFluent<FluentContainerConfiguration> ByDefaultAllInstancesAre
        {
            get
            {
                return new SolvingAlterationFluent<FluentContainerConfiguration>(
                    container.ActualContainer.GlobalConfiguration, container, this);
            }
        }

        /// <summary>
        /// Configures the <see cref="Ya.Container"/> behavior when it can't solve a service.
        /// </summary>
        public IfCannotSolveFluent IfCannotSolve
        {
            get { return new IfCannotSolveFluent(container, this); }
        }

        /// <summary>
        /// Fluent Return to the related <see cref="Ya.Container"/>.
        /// </summary>
        /// <param name="configFluent">The config fluent.</param>
        /// <returns>
        /// The related <see cref="Ya.Container"/>.
        /// </returns>
        public static implicit operator Container(FluentContainerConfiguration configFluent)
        {
            return configFluent.container;
        }

    }



}
