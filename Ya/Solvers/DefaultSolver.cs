using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Configuration;
using Ya.SolvingMethods;

namespace Ya.Solvers
{
    class DefaultSolver : ServiceSolver
    {
        public override ISolvingInfo Solve(Type typeToSolve)
        {
            return new SolvingInfo(new DefaultSolvingMethod(typeToSolve), SolvingConfiguration.CreateDefault(), typeToSolve);
        }

        class DefaultSolvingMethod : ISolvingMethod
        {
            private Type typeToSolve;

            public DefaultSolvingMethod(Type typeToSolve)
            {
                this.typeToSolve = typeToSolve;
            }

            public object Solve()
            {
                if (typeToSolve.IsValueType)
                {
                    return Activator.CreateInstance(typeToSolve);
                }
                else
                    return null;

            }
        }
    }
}
