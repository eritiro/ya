using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ya.Extensions;

namespace Ya.Support
{
    class TypeDictionary<T>
    {
        private readonly Dictionary<Type, T> dictionary = new Dictionary<Type, T>();

        public void Add(Type type, T value)
        {
            foreach (var relatedType in type.GetRelatedTypes())
            {
                dictionary[relatedType] = value;
            }
        }

        public bool TryGetValue(Type key, out T value)
        {
            return dictionary.TryGetValue(key, out value);
        }
    }
}
