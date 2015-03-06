namespace Ya
{

    /// <summary>
    /// Injects dependencies to previously created instances.
    /// </summary>
    public interface IInjecter
    {
        /// <summary>
        /// Takes an object and inserts all the services that requires.
        /// </summary>
        /// <typeparam name="T">The type of the victim.</typeparam>
        /// <param name="victim">The victim.</param>
        /// <returns>The same victim.</returns>
        T Inject<T>(T victim);
    }
}
