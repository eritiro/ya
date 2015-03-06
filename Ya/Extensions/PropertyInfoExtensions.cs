using System.Reflection;
using Ya.Support;

namespace Ya.Extensions
{
    static class PropertyInfoExtensions
    {
        public static bool IsInjectable(this PropertyInfo propertyInfo) 
        {
            if (propertyInfo.GetCustomAttributes(typeof(InjectAttribute), true).Length > 0)
            {
                propertyInfo.ValidateInjection();
                return true;
            }
            else
                return false;
        }

        private const string INACCESSIBLE = "The object {0} has an InjectAttribute in the element {1} but its property set is inaccessible.";
        private static void ValidateInjection(this PropertyInfo propertyInfo) 
        {
            if (propertyInfo.GetSetMethod(true) == null) 
            {
                throw new YaException(string.Format(INACCESSIBLE, propertyInfo.DeclaringType.FullName, propertyInfo.Name));
            }
        }
    }
}
