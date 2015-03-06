namespace Ya.Fluent.ContainerConfiguration
{
    /// <summary>
    /// Container behavior fluent configuration.
    /// </summary>
    public interface IFluentContainerConfiguration
    {
        /// <summary>
        /// Configures the <see cref="Ya.Container"/> default behavior.
        /// </summary>
        SolvingAlterationFluent<FluentContainerConfiguration> ByDefaultAllInstancesAre { get; }

        /// <summary>
        /// Configures the <see cref="Ya.Container"/> behavior when it can't solve a service.
        /// </summary>
        IfCannotSolveFluent IfCannotSolve { get; }
    }}
