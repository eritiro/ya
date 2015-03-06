using System;

namespace Ya.Support
{
    /// <summary>
    /// The exception that is thrown when the container cannot find a component for a required service.
    /// </summary>
    public class CannotSolveException : YaException
    {
        internal CannotSolveException() { }
        internal CannotSolveException(string message) : base(message) { }
        internal CannotSolveException(string message, Exception inner) : base(message, inner) { }
    }
}
