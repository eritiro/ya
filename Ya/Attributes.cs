using System;

namespace Ya
{
    /// <summary>
    /// Specifies that the flowing property must be injected by the container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
    }

    /// <summary>
    /// Specifies that the folowing class is an implementation (component) of a service.
    /// When the assembly is added to the container, 
    /// all the classes with this attribute will be registered as components.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ImplementationAttribute : Attribute
    {
    }
}
