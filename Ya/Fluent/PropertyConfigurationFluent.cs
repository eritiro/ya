using System;
using Ya.Solvers;
using System.Reflection;
using System.Linq.Expressions;
using Ya.SolvingMethods;

namespace Ya.Fluent
{
    public class NeedsType<TypeToBeInjected>
    {
        private readonly PropertySolver solver;
        private readonly Container container;

        internal NeedsType(PropertySolver solver, Container container)
        {
            this.solver = solver;
            this.container = container;
        }

        public InType<TypeToBeInjected, TypeOfQuery> NeedsA<TypeOfQuery>()
        {
            return new InType<TypeToBeInjected, TypeOfQuery>(solver, container);
        }
    }

    public class InType<TypeToBeInjected, TypeOfQuery>
    {
        private PropertySolver solver;
        private Container container;

        internal InType(PropertySolver solver, Container container)
        {
            this.solver = solver;
            this.container = container;
        }

        public YouShouldGiveType<TypeOfQuery> In(Expression<Func<TypeToBeInjected, TypeOfQuery>> propertyLambda)
        {
            var member = propertyLambda.Body as MemberExpression;
            var propertyInfo = (PropertyInfo)member.Member;
            return new YouShouldGiveType<TypeOfQuery>(propertyInfo, solver, container);
        }
    }

    public class YouShouldGiveType<TypeOfQuery>
    {
        private PropertyInfo propertyInfo;
        private PropertySolver solver;
        private Container container;

        internal YouShouldGiveType(PropertyInfo propertyInfo, PropertySolver solver, Container container)
        {
            this.propertyInfo = propertyInfo;
            this.solver = solver;
            this.container = container;
        }

        public SolvingAlterationFluentItself YouShouldGive<TypeImplementation>(TypeImplementation objectToInject) where TypeImplementation : TypeOfQuery
        {
            return AddTypeMethod(propertyInfo, new InstanceMethod(objectToInject));
        }

        public SolvingAlterationFluentItself YouShouldGive<TypeImplementation>()
        {
            return AddTypeMethod(propertyInfo, new TypeMethod(typeof(TypeImplementation)));
        }

        public SolvingAlterationFluentItself YouShouldGive<TypeImplementation>(Func<TypeImplementation> creationalExpression)
        {
            return AddTypeMethod(propertyInfo, new LambdaMethod<TypeImplementation>(creationalExpression));
        }

        private SolvingAlterationFluentItself AddTypeMethod(PropertyInfo propertyInfo, ISolvingMethod solvingMethod)
        {
            var configuration = container.ActualContainer.CreateDefaultConfiguration();
            solver.AddMethod(propertyInfo, solvingMethod, configuration);
            return new SolvingAlterationFluentItself(configuration, container);
        }
    }
}
