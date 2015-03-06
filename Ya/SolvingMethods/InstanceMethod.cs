namespace Ya.SolvingMethods
{
    class InstanceMethod : ISolvingMethod
    {
        private object instanceToReturn;

        public InstanceMethod(object instanceToReturn) 
        { 
            this.instanceToReturn = instanceToReturn;
        }

        public object Solve()
        {
            return instanceToReturn;
        }
    }
}
