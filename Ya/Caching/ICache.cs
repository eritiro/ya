
namespace Ya.Caching
{
    ///<summary>
    /// Represents a cache used to manage the lifestyle of the components.
    ///</summary>
    public interface ICache
    {
        /// <summary>
        /// Gets the cached value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="item">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>
        ///   <c>true</c> if the cache contains an element with the specified key; otherwise, <c>false</c>.
        /// </returns>
        bool TryGetValue(object key, out object item);

        /// <summary>
        /// Adds the specified item to the cache.
        /// </summary>
        /// <param name="key">The key associated with the item.</param>
        /// <param name="item">The item.</param>
        void Add(object key, object item);
    }

}
