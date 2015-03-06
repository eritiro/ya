using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Extensions;
using System.Collections;

namespace Ya.Configuration
{
    class CompositeSolvingInfo : ISolvingInfo
    {
        private IList<SolvingInfo> solvingList;
        private Type typeArgument;

        public CompositeSolvingInfo(Type typeArgument, IList<SolvingInfo> solvingList)
        {
            this.solvingList = solvingList;
            this.typeArgument = typeArgument;
        }

        public object Solve(ActualContainer container)
        {
            var listType = typeof(List<>).MakeGenericType(typeArgument);
            IList list = (IList)listType.Activate();
            foreach (var solvingInfo in solvingList)
            {
                list.Add(solvingInfo.Solve(container));
            }
            return list;
        }
    }
}
