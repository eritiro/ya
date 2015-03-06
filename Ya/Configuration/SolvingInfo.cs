using System;
using System.Reflection;
using Ya.SolvingMethods;

namespace Ya.Configuration
{
    class SolvingInfo : ISolvingInfo
    {
        private readonly ISolvingMethod solvingMethod;
        private readonly object key;

        internal SolvingConfiguration Configuration { get; private set; }

        private SolvingInfo(ISolvingMethod solvingMethod, SolvingConfiguration configuration, object key)
        {
            this.solvingMethod = solvingMethod;
            this.Configuration = configuration;
            this.key = key;
        }

        public SolvingInfo(ISolvingMethod solvingMethod, SolvingConfiguration configuration, Type type) 
            : this(solvingMethod, configuration, (object)type)
        { 
        }

        public SolvingInfo(ISolvingMethod solvingMethod, SolvingConfiguration configuration, PropertyInfo propertyInfo)
            : this(solvingMethod, configuration, (object)propertyInfo)
        {
        }

        public object Solve(ActualContainer container)
        {
            object result;
            if (!Configuration.Cache.TryGetValue(key, out result))
            {
                result = this.solvingMethod.Solve();
                if (result != null)
                {
                    Configuration.Cache.Add(key, result);
                    container.Inject(result);
                }
            }
            return result;
        }
    }
}
