using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ya.Configuration;
using Ya.SolvingMethods;
using Ya.Support;
using Ya.Caching;

namespace Ya.Solvers
{
    class PropertySolver : ServiceSolver
    {
        private Dictionary<PropertyInfo, SolvingInfo> propertyToImplementation = new Dictionary<PropertyInfo, SolvingInfo>();

        public void AddMethod(PropertyInfo info, ISolvingMethod method, SolvingConfiguration configuration)
        {
            propertyToImplementation.Add(info, new SolvingInfo(method, configuration, info));
        }

        public override ISolvingInfo Solve(PropertyInfo propertyInfo) 
        {
            SolvingInfo solvingInfo;
            propertyToImplementation.TryGetValue(propertyInfo, out solvingInfo);
            return solvingInfo;
        }
    }
}
