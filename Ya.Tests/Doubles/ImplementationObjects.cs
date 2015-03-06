using System;

namespace Ya.Tests.Doubles
{
    public interface InterfaceWithOneImplementationWithImplementsAttribute
    {
        int Number { get; set; }
    }

    public interface InterfaceWithOnlyOneImplementationWithoutImplementsAttribute
    {
        int Number { get; set; }
    }

    public interface IGeneric<T> 
    {
        T Member { get; set; }
    }

    [Implementation]
    public class Generic<T> : IGeneric<T> 
    {
        public T Member { get; set; }
    }

    [Implementation]
    public class ClassWithImplementsAttribute : InterfaceWithOneImplementationWithImplementsAttribute
    {
        public int Number { get; set; }
    }

    public class ClassWithoutImplementsAttribute : InterfaceWithOnlyOneImplementationWithoutImplementsAttribute
    {
        public int Number { get; set; }
    }

    public class ClassWithExceptionThrowerDependency
    {
        [Inject]
        public ExceptionThrower Member { get; set; } 
    }

    public class ExceptionThrower
    {
        public ExceptionThrower() 
        {
            throw new Exception("ExceptionThrower forced a exception.");
        }

    }

    public class CircularReference
    {
        [Inject]
        public CircularReference Member { get; set; }
    }

}
