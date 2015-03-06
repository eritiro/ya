namespace Ya.Caching
{
    class Caches
    {
        public readonly ICache StaticCache = new StaticCache();
#if !PocketPC
        public readonly ICache ThreadCache = new ThreadCache();
#endif

    }

}
