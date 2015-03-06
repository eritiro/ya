using System;
using System.Reflection;
using Ya.Fluent;
using Ya.Fluent.ContainerConfiguration;
using Ya.SolvingMethods;

namespace Ya
{
    /// <summary>
    /// Represents the container of all the components registered in the system.
    /// It is the root component of the framework.
    /// </summary>
    public class Container : IInjecter
    {
        internal ActualContainer ActualContainer { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        public Container()
        {
            ActualContainer = new ActualContainer();
            Add(this);
        }

        /// <summary>
        /// Configures the instance of the <see cref="Container"/> class through a fluent interface.
        /// </summary>
        public FluentContainerConfiguration Configure
        {
            get { return new FluentContainerConfiguration(this); }
        }

        /// <summary>
        /// Registers a new component type. The component will be registered for all the
        /// interfaces that implements and all the classes that inherits, including itself.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself Add<T>()
        {
            return AddTypeMethod<T>(new TypeMethod(typeof(T)));
        }

        /// <summary>
        /// Registers a new component instance. The component will be registered for all the
        /// interfaces that implements and all the classes that inherits, including itself.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <param name="serviceInstance">The component instance.</param>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself Add<T>(T serviceInstance)
        {
            return AddTypeMethod<T>(new InstanceMethod(serviceInstance));
        }

        /// <summary>
        /// Registers a new component instance. The component will be registered for all the
        /// interfaces that implements and all the classes that inherits, including itself.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <param name="creationalExpression">The creational expression of the component instance.</param>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself Add<T>(Func<T> creationalExpression)
        {
            return AddTypeMethod<T>(new LambdaMethod<T>(creationalExpression));
        }

        /// <summary>
        /// Registers a new component type as a generic type. The component will be registered for all the
        /// interfaces that implements and all the classes that inherits, including itself.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself AddGeneric<T>()
        {
            var configuration = ActualContainer.CreateDefaultConfiguration();
            ActualContainer.Generics.AddGeneric(typeof(T), configuration);
            return new SolvingAlterationFluentItself(configuration, this);
        }

#if !PocketPC
        /// <summary>
        /// Fluent interface to create a specific dependency injection for a specific class.
        /// </summary>
        /// <typeparam name="TypeToBeInjected">The 'victim' type to be injected.</typeparam>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        /// <example>
        /// container.When&lt;InjectableObject\&gt;().NeedsA&lt;IFoo&gt;().In(i => i.Member).YouShouldGive(foo1)
        /// </example>
        public NeedsType<TypeToBeInjected> When<TypeToBeInjected>()
        {
            return new NeedsType<TypeToBeInjected>(ActualContainer.Properties, this);
        }
#endif

        /// <summary>
        /// Searches and registers all the component classes marked with the [Implementation] attribute of the given assembly.
        /// </summary>
        /// <param name="assembly">The assembly to lookup.</param>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself AddAssembly(Assembly assembly)
        {
            var configuration = ActualContainer.CreateDefaultConfiguration();
            ActualContainer.AddAssembly(assembly, configuration);
            return new SolvingAlterationFluentItself(configuration, this);
        }

        /// <summary>
        /// Searches and registers all the component classes marked with the [Implementation] attribute of the assembly that contains the given type.
        /// </summary>
        /// <typeparam name="T">The type that will be used to reference the assembly.</typeparam>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself AddAssemblyOf<T>()
        {
            return AddAssembly(typeof(T).Assembly);
        }

        /// <summary>
        /// Searches and registers all the component classes marked with the [Implementation] attribute of the assembly that contains the type of the given object.
        /// </summary>
        /// <param name="o">The type that will be used to reference the assembly.</param>
        /// <returns>
        /// A fluent interface for modifying its registration behavior.
        /// </returns>
        public SolvingAlterationFluentItself AddAssemblyOf(object o)
        {
            return AddAssembly(o.GetType().Assembly);
        }

        /// <summary>
        /// Retrieves a specified service from the container.
        /// </summary>
        /// <typeparam name="T">The service type of the object.</typeparam>
        /// <returns>The implementation of the service.</returns>
        public T GetObject<T>()
        {
            return (T)ActualContainer.GetObject(typeof(T));
        }

        /// <summary>
        /// Takes an object and inserts all the services that requires.
        /// </summary>
        /// <typeparam name="T">The type of the victim.</typeparam>
        /// <param name="victim">The victim.</param>
        /// <returns>The same victim.</returns>
        public T Inject<T>(T victim)
        {
            ActualContainer.Inject(victim);
            return victim;
        }

        private SolvingAlterationFluentItself AddTypeMethod<T>(ISolvingMethod solvingMethod)
        {
            var configuration = ActualContainer.CreateDefaultConfiguration();
            ActualContainer.AddTypeMethod(typeof(T), solvingMethod, configuration);
            return new SolvingAlterationFluentItself(configuration, this);
        }

    }
}
