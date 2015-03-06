namespace Ya.Caching
{
    class NullCache : ICache
    {
        public static readonly ICache Instance = new NullCache();

        public bool TryGetValue(object key, out object item)
        {
            item = null;
            return false;
        }

        public void Add(object key, object item)
        {
            // do nothing
        }
    }
}
