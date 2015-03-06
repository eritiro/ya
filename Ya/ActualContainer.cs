using System;
using System.Reflection;
using Ya.Solvers;
using Ya.Extensions;
using Ya.Configuration;
using Ya.Caching;
using Ya.SolvingMethods;
using Ya.Support;

namespace Ya
{
    class ActualContainer
    {
        private readonly SolvingConfiguration globalConfiguration;
        private readonly Caches caches;
        private readonly IServiceSolver[] solvers;

        public readonly PropertySolver Properties;
        public readonly TypeSolver Types;
        public readonly GenericsSolver Generics;
        public readonly ListSolver Lists;
        public readonly SolverWrapper Defaults;

        public ActualContainer() 
        {
            Properties = new PropertySolver();
            Types = new TypeSolver();
            Generics = new GenericsSolver();
            Lists = new ListSolver();
            Defaults = new SolverWrapper();

            caches = new Caches();
            globalConfiguration = SolvingConfiguration.CreateDefault();
            solvers = new IServiceSolver[] { Properties, Types, Generics, Lists, Defaults };
        }

        public Caches Caches
        {
            get { return caches; }
        }

        public SolvingConfiguration GlobalConfiguration
        {
            get { return globalConfiguration; }
        }

        public void AddAssembly(Assembly assembly, SolvingConfiguration configuration)
        {
            foreach (var type in assembly.GetImplementations())
            {
                if (type.IsGenericTypeDefinition)
                    Generics.AddGeneric(type, configuration);
                else
                {
                    var typeMethod = new TypeMethod(type);
                    AddTypeMethod(type, typeMethod, configuration);
                }
            }
        }

        public void AddTypeMethod(Type type, ISolvingMethod typeMethod, SolvingConfiguration configuration)
        {
            Types.AddMethod(type, typeMethod, configuration);
            Lists.AddMethod(type, typeMethod, configuration);
        }

        public virtual object GetObject(Type type)
        {
            foreach (var solver in solvers)
            {
                var solvingInfo = solver.Solve(type);
                if (solvingInfo != null)
                    return solvingInfo.Solve(this);
            }
            throw new CannotSolveException("The container cannot solve the specified service. Service name: " + type.FullName);
        }

        public virtual object GetObject(PropertyInfo property)
        {
            foreach (var solver in solvers)
            {
                var solvingInfo = solver.Solve(property);
                if (solvingInfo != null)
                    return solvingInfo.Solve(this);
            }
            throw new CannotSolveException(string.Format("The container cannot solve the specified service for {0}.{1}. Service name: {2}", property.DeclaringType.Name, property.Name, property.PropertyType.FullName));
        }

        public object Inject(object victim)
        {
            foreach (var property in victim.GetType().GetInjectableProperties())
            {
                property.SetValue(victim, GetObject(property), null);
            }
            return victim;
        }

        public SolvingConfiguration CreateDefaultConfiguration()
        {
            return (SolvingConfiguration)globalConfiguration.Clone();
        }
    }
}
