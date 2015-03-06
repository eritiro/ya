using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Ya.Support;

namespace Ya.Extensions
{
    static class TypeExtensions
    {
        public static bool ContainsDefaultConstructor(this Type type)
        {
            foreach (var constructor in type.GetConstructors())
            {
                if (constructor.GetParameters().Length == 0)
                {
                    return true;
                }
            }
            return type.IsValueType;
        }

        public static IEnumerable<PropertyInfo> GetInjectableProperties(this Type type) 
        {
            return type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(propertyInfo => propertyInfo.IsInjectable());
        }

        public static IEnumerable<Type> GetRelatedTypes(this Type instanceType)
        {
            var types = new List<Type>();
            types.Add(instanceType);
            foreach (var interfaceType in instanceType.GetInterfaces())
                types.Add(interfaceType);

            AddParents(types, instanceType.BaseType);

            return types;
        }

        private static void AddParents(List<Type> types, Type baseType)
        {
            if (baseType != null)
            {
                types.Add(baseType);
                AddParents(types, baseType.BaseType);
            }
        }

        public static object Activate(this Type type) 
        {
            try
            {
#if PocketPC
                return Activator.CreateInstance(type); 
#else
                return Activator.CreateInstance(type, true);
#endif
            }
            catch (MissingMethodException ex)
            {
                throw new YaException(String.Format("Cannot create service instance of {0}. Make sure it has a default constructor", type.FullName), ex);
            }
        }

        public static bool CanActivate(this Type type) 
        {
            return !type.IsAbstract && !type.IsInterface && type.ContainsDefaultConstructor();
        }
    }
}
