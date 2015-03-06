using System;

namespace Ya.Support
{
    /// <summary>
    /// The base exception that is thrown in Ya Framework.
    /// </summary>
    public class YaException : Exception
    {
        internal YaException() { }
        internal YaException(string message) : base(message) { }
        internal YaException(string message, Exception inner) : base(message, inner) { }
    }
}
