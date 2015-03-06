using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Extensions;
using System.Reflection;
using Ya.Configuration;
using Ya.SolvingMethods;

namespace Ya.Solvers
{
    class AutoSolver : ServiceSolver
    {
        public SolvingConfiguration Configuration { get; set; }

        public AutoSolver()
        {
            Configuration = SolvingConfiguration.CreateDefault();
        }

        public override ISolvingInfo Solve(Type type)
        {
            if (type.CanActivate())
                return new SolvingInfo(new TypeMethod(type), Configuration, type);
            else
                return null;
        }
    }
}
