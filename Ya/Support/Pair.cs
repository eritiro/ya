using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ya.Support
{
    struct Pair<T1, T2>
    {
        public T1 First;
        public T2 Second;
    }

    static class Pair 
    {
        public static Pair<RT1, RT2> Create<RT1, RT2>(RT1 first, RT2 second)
        {
            return new Pair<RT1, RT2>
            {
                First = first,
                Second = second
            };
        }    
    }
}
