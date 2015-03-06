using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Configuration;
using Ya.Caching;

namespace Ya.Fluent
{
    /// <summary>
    /// Configures the <see cref="Ya.Container"/> default behavior.
    /// </summary>
    public class SolvingAlterationFluent<TFluentContinue>
    {
        private readonly SolvingConfiguration configuration;
        protected Container Container { get; private set; }
        internal TFluentContinue FluentContinue { get; set; }

        internal SolvingAlterationFluent(SolvingConfiguration configuration, Container container, TFluentContinue fluentContinue)
            : this(configuration, container)
        {
            this.FluentContinue = fluentContinue;
        }

        internal SolvingAlterationFluent(SolvingConfiguration configuration, Container container)
        {
            this.configuration = configuration;
            this.Container = container;
        }

        public InstantiationType<TFluentContinue> UniquePer
        {
            get { return new InstantiationType<TFluentContinue>(configuration, Container, FluentContinue); }
        }

        public static implicit operator Container(SolvingAlterationFluent<TFluentContinue> solvingFluent)
        {
            return solvingFluent.Container;
        }
    }

    public class SolvingAlterationFluentItself : SolvingAlterationFluent<SolvingAlterationFluentItself>
    {
        internal SolvingAlterationFluentItself(SolvingConfiguration configuration, Container container)
            : base(configuration, container)
        {
            FluentContinue = this;
        }
    }

    public class InstantiationType<TFluentContinue>
    {
        private readonly SolvingConfiguration configuration;
        private readonly Container container;
        private readonly TFluentContinue fluentContinue;

        internal InstantiationType(SolvingConfiguration configuration, Container container, TFluentContinue fluentContinue)
        {
            this.configuration = configuration;
            this.container = container;
            this.fluentContinue = fluentContinue;
        }

#if !PocketPC
        public TFluentContinue Thread()
        {
            configuration.Cache = container.ActualContainer.Caches.ThreadCache;
            return fluentContinue;
        }
#endif

        public TFluentContinue Container()
        {
            configuration.Cache = container.ActualContainer.Caches.StaticCache;
            return fluentContinue;
        }

        public TFluentContinue Call()
        {
            configuration.Cache = NullCache.Instance;
            return fluentContinue;
        }

        public TFluentContinue Cache(ICache cache)
        {
            configuration.Cache = cache;
            return fluentContinue;
        }


    }
}
