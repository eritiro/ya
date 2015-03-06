using Ya.Configuration;

namespace Ya.Fluent.ContainerConfiguration
{
    public class SolvingAlterationWithFluentConfig : SolvingAlterationFluent<FluentContainerConfiguration>, IFluentContainerConfiguration
    {
        internal SolvingAlterationWithFluentConfig(SolvingConfiguration configuration, Container container, FluentContainerConfiguration fluent)
            : base(configuration, container)
        {
            FluentContinue = fluent;
        }

        public SolvingAlterationFluent<FluentContainerConfiguration> ByDefaultAllInstancesAre
        {
            get { return FluentContinue.ByDefaultAllInstancesAre; }
        }

        public IfCannotSolveFluent IfCannotSolve
        {
            get { return FluentContinue.IfCannotSolve; }
        }
    }
}
