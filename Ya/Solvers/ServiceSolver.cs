using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Configuration;
using System.Reflection;

namespace Ya.Solvers
{
    class ServiceSolver : IServiceSolver
    {
        public static readonly ServiceSolver NullInstance = new ServiceSolver();
    
        public virtual ISolvingInfo Solve(Type typeToSolve)
        {
            return null;
        }

        public virtual ISolvingInfo Solve(PropertyInfo propertyToSolve)
        {
            return Solve(propertyToSolve.PropertyType);
        }

    }
}
