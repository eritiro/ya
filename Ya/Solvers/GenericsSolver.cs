using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.SolvingMethods;
using Ya.Support;
using System.Reflection;
using Ya.Extensions;
using Ya.Configuration;

namespace Ya.Solvers
{
    class GenericsSolver : ServiceSolver
    {
        private List<Pair<Type, SolvingConfiguration>> interfaceToImplementation = new List<Pair<Type, SolvingConfiguration>>();

        public override ISolvingInfo Solve(Type typeToSolve)
        {
            if (typeToSolve.IsGenericType)
            {
                foreach (var generic in interfaceToImplementation)
	            {
                    var impl = TryCreateImplementation(generic.First, generic.Second, typeToSolve);
                    if(impl!= null)
                        return impl;
	            }
            }
            return null;
        }

        private SolvingInfo TryCreateImplementation(Type generic, SolvingConfiguration config, Type typeToSolve)
        {
            Type implementationType;
            try
            {
                implementationType = generic.MakeGenericType(typeToSolve.GetGenericArguments());
            }
            catch (ArgumentException) 
            {
                return null;
            }
            if (typeToSolve.IsAssignableFrom(implementationType))
                return new SolvingInfo(new TypeMethod(implementationType), config, implementationType);
            else 
                return null;
        }

        public void AddGeneric(Type type, SolvingConfiguration configuration)
        {
            if (!type.IsGenericType)
                throw new YaException("Cannot add a non-generic type as a generic one.");
            Type genericDefinition = type.GetGenericTypeDefinition();
            interfaceToImplementation.Add(Pair.Create(genericDefinition, configuration));
        }

    }
}
