using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ya.Configuration;

namespace Ya.Solvers
{
    interface IServiceSolver
    {
        ISolvingInfo Solve(Type typeToSolve);
        ISolvingInfo Solve(PropertyInfo propertyToSolve);

    }
}
