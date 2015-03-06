using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Ya.Extensions
{
    static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetImplementations(this Assembly assembly)
        {
            var implementations = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                object[] attributes = type.GetCustomAttributes(true);
                foreach (object attribute in attributes)
                {
                    ImplementationAttribute ia = attribute as ImplementationAttribute;
                    if (ia != null)
                    {
                        implementations.Add(type);
                    }
                }
            }
            return implementations;
        }
    }
}
