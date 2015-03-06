using System;
using System.Collections.Generic;

namespace Ya.Caching
{
    class ThreadCache : ICache
    {
        [ThreadStatic]
        static Dictionary<object, object> dictionary;

        public bool TryGetValue(object key, out object item)
        {
            EnsureDictionariesExist();
            return dictionary.TryGetValue(key, out item);
        }

        public void Add(object key, object item)
        {
            EnsureDictionariesExist();
            dictionary.Add(key, item);
        }

        private static void EnsureDictionariesExist()
        {
            if (dictionary == null)
                dictionary = new Dictionary<object, object>();
        }
    }
}
