using System;
using Ya.Extensions;
using Ya.Support;

namespace Ya.SolvingMethods
{
    class TypeMethod : ISolvingMethod
    {
        Type typeImplementation;

        public TypeMethod(Type typeImplementation) 
        {
            if (!typeImplementation.CanActivate())
                throw new YaException("Cannot add a service type which isn't abstract and doesn't have a default constructor.");
            this.typeImplementation = typeImplementation;
        }

        public object Solve()
        {
            return typeImplementation.Activate();
        }
    }
}
