using System.Collections.Concurrent;

namespace ZenCache2
{
    class Cache
    {
        ConcurrentDictionary<string, string> contents;

        public Cache()
        {
            contents = new ConcurrentDictionary<string, string>();
        }

        // Add an item to cache if not already there; overwrite it if already there
        public void Add(string key, string value)
        {
            contents[key] = value;
        }

        // Retrieve item from cache if present; return empty string if not.
        public string Fetch(string key)
        {
            if (contents.ContainsKey(key))
            {
                return contents[key];
            }
            else
            {
                return "";
            }
        }

        // Remove item from cache if present
        public void Delete(string key)
        {
            // TryRemove returns the old value in an out parameter, so although we don't use it, need to provide it
            contents.TryRemove(key, out string oldValue);
        }
    }
}