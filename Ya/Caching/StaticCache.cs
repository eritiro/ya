using System.Collections.Generic;

namespace Ya.Caching
{
    class StaticCache : ICache
    {
        Dictionary<object, object> dictionary = new Dictionary<object, object>();

        public bool TryGetValue(object key, out object item)
        {
            return dictionary.TryGetValue(key, out item);
        }

        public void Add(object key, object item)
        {
            dictionary.Add(key, item);
        }
    }
}
