using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Configuration;
using System.Reflection;
using Ya.SolvingMethods;

namespace Ya.Solvers
{
    class ExecutionSolver<T> : ServiceSolver
    {
        private Delegate method;

        public SolvingConfiguration Configuration { get; set; }

        public ExecutionSolver(Func<T, object> method)
        {
            this.method = method;
            this.Configuration = SolvingConfiguration.CreateDefault();
        }

        public override ISolvingInfo Solve(Type typeToSolve)
        {
            return new SolvingInfo(new ExecutionSolvingMethod<Type>(method, typeToSolve), Configuration, typeToSolve);
        }

        public override ISolvingInfo Solve(PropertyInfo propertyToSolve)
        {
            return new SolvingInfo(new ExecutionSolvingMethod<PropertyInfo>(method, propertyToSolve), SolvingConfiguration.CreateDefault(), propertyToSolve);
        }

        class ExecutionSolvingMethod<T2> : ISolvingMethod
        {
            private Delegate method;
            private T2 key;

            public ExecutionSolvingMethod(Delegate method, T2 key)
            {
                this.method = method;
                this.key = key;
            }

            public object Solve()
            {
                return method.Method.Invoke(method.Target, new object[]{key});
            }
        }

    }
}
