using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.SolvingMethods;
using Ya.Support;
using Ya.Configuration;
using Ya.Extensions;

namespace Ya.Solvers
{
    class ListSolver : ServiceSolver
    {
        private readonly Dictionary<Type, IList<SolvingInfo>> interfaceToList = new Dictionary<Type, IList<SolvingInfo>>();

        public void AddMethod(Type type, ISolvingMethod method, SolvingConfiguration configuration)
        {
            var solvingInfo = new SolvingInfo(method, configuration, type);
            foreach (var relatedType in type.GetRelatedTypes())
            {
                var list = GetList(relatedType);
                list.Add(solvingInfo);
            }
        }

        private IList<SolvingInfo> GetList(Type type)
        {
            IList<SolvingInfo> list;
            if (!interfaceToList.TryGetValue(type, out list))
            {
                list = new List<SolvingInfo>();
                interfaceToList.Add(type, list);
            }
            return list;
        }

        public override ISolvingInfo Solve(Type type)
        {
            if (!type.IsGenericType)
                return null;
            
            Type typeArgument = type.GetGenericArguments()[0];

            var iListType = typeof(IList<>).MakeGenericType(typeArgument);
            if (type.IsAssignableFrom(iListType))
                return new CompositeSolvingInfo(type.GetGenericArguments()[0], GetList(typeArgument));
            else
                return null;
        }
    }
}
