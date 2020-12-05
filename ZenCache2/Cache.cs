using System;
using System.Collections.Concurrent;

namespace ZenCache2
{
    class Cache
    {
        // This is the storage for the most recent contents of the cache
        private ConcurrentDictionary<string, string> contents;

        // We use this to store the contents already in the cache while waiting for them to expire
        private ConcurrentDictionary<string, string> oldContents;

        // We use this to determine when we last moved the cache to oldCache (and removed prior oldCache)
        private DateTime lastSwap;

        // We use this lock to ensure that only a single thread swaps the cache
        private readonly object swapLock = new object();

        // Minimum length of time to hold an item in cache
        private int minutesToHold = 5;

        public Cache()
        {
            contents = new ConcurrentDictionary<string, string>();
            oldContents = null;
            lastSwap = DateTime.Now;
        }

        // Quick-and-dirty HTML output of the whole contents, showing current contents and old contents.
        // Basically an internal view for convenience while debugging or playing around with it.
        public string FetchAll()
        {
            string result = "<html><body>";

            result += "<h2>Cache contents in current area:</h2><ul>";
            foreach (var element in contents)
            {
                result += "<li><strong>Key:</strong> " + element.Key + " <strong>Value:</strong> " + element.Value;
            }

            if (oldContents != null)
            {
                result += "</ul><h2>Cache contents in oldContents:</h2><ul>";

                foreach (var element in oldContents)
                {
                    result += "<li><strong>Key:</strong> " + element.Key + " <strong>Value:</strong> " + element.Value;
                }
            }

            result += "</ul></body></html>";
            return result;
        }

        // Look at the current time and the last swap time and determine if we need to swap the contents and if so do it if no other thread has
        private void checkSwap()
        {
            if (DateTime.Now.Subtract(lastSwap).TotalMinutes > minutesToHold)
            {
                lock (swapLock)
                {
                    Console.WriteLine("swapping contents");
                    oldContents = contents;
                    contents = new ConcurrentDictionary<string, string>();
                    lastSwap = DateTime.Now;
                }
            }
        }

        // Add an item to cache if not already there; overwrite it if already there
        public void Add(string key, string value)
        {
            contents[key] = value;
            checkSwap();
        }

        // Retrieve item from cache if present; return empty string if not.
        public string Fetch(string key)
        {
            // If contents has the key, it will always be newer. If not, if oldContents has it, use that value.
            if (contents.ContainsKey(key))
            {
                return contents[key];
            }
            else if (oldContents.ContainsKey(key)) {
                return oldContents[key];
            }
            {
                return "";
            }
        }

        // Remove item from cache if present
        public void Delete(string key)
        {
            // TryRemove returns the old value in an out parameter, so although we don't use it, need to provide it
            contents.TryRemove(key, out string oldValue);
            oldContents.TryRemove(key, out oldValue);
        }
    }
}