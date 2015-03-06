using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ya.Configuration
{
    interface ISolvingInfo
    {
        object Solve(ActualContainer container);
    }
}
