using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Caching;

namespace Ya.Configuration
{
    class SolvingConfiguration : ICloneable
    {
        public static SolvingConfiguration CreateDefault()
        {
            return new SolvingConfiguration { Cache = NullCache.Instance };
        }

        public ICache Cache { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
}
}
