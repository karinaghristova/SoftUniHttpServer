using System.Collections.Generic;

namespace SoftUniHttpServer.Server.Common
{
    public static class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "Value";
                throw new ArgumentNullException($"{name} cannot be null");
            }
        }

        public static void AgainstDuplicated<T, V>(IDictionary<T, V> dictionary, T key, string name)
        {
            if (dictionary.ContainsKey(key))
            {
                throw new ArgumentException($"{name} already contains key {key.ToString()}");
            }
        }
    }
}
