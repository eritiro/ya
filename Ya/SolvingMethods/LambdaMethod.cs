using System;

namespace Ya.SolvingMethods
{
    class LambdaMethod<T> : ISolvingMethod
    {
        readonly Delegate creationalExpression;

        public LambdaMethod(Func<T> creationalExpression) 
        {
            this.creationalExpression = creationalExpression;
        }
        
        public object Solve()
        {
            return creationalExpression.Method.Invoke(creationalExpression.Target, null);
        }
    }
}
