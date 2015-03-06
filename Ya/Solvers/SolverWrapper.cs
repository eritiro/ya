using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Configuration;
using System.Reflection;

namespace Ya.Solvers
{
    class SolverWrapper : IServiceSolver
    {
        public IServiceSolver TypeSolver { get; set; }
        public IServiceSolver PropertySolver { get; set; }

        public SolverWrapper() 
        {
            TypeSolver = new AutoSolver();
            PropertySolver = ServiceSolver.NullInstance;
        }

        public ISolvingInfo Solve(Type typeToSolve)
        {
            return TypeSolver.Solve(typeToSolve);
        }

        public ISolvingInfo Solve(PropertyInfo propertyToSolve)
        {
            return PropertySolver.Solve(propertyToSolve);
        }

    }
}
