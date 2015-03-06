using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ya.SolvingMethods;
using Ya.Support;
using Ya.Configuration;

namespace Ya.Solvers
{
    class TypeSolver : ServiceSolver
    {
        private readonly TypeDictionary<SolvingInfo> interfaceToImplementation = new TypeDictionary<SolvingInfo>();

        public void AddMethod(Type type, ISolvingMethod method, SolvingConfiguration configuration)
        {
            interfaceToImplementation.Add(type, new SolvingInfo(method, configuration, type));
        }

        public override ISolvingInfo Solve(Type type) 
        {
            SolvingInfo solvingInfo;
            interfaceToImplementation.TryGetValue(type, out solvingInfo);
            return solvingInfo;
        }
    }
}
