using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZenCache2
{
    public class CacheController : ApiController
    {
        // Shared Cache so all threads are dealing with the same information
        // Cache is built on a ConcurrentDictionary so it is thread-safe
        static Cache cache = new Cache();

        // GET cache
        // modify this to return all values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET cache
        public string Get(string key)
        {
            Console.WriteLine("Retrieving key: " + key + " with value: " + cache.Fetch(key));
            return cache.Fetch(key);
        }

        // POST cache
        public void Post(string key, string value)
        {
            Console.WriteLine("Setting key: " + key + " to value: " + value);
            cache.Add(key, value);
        }

        // PUT cache
        public void Put(string key, string value)
        {
            Console.WriteLine("Setting key: " + key + " to value: " + value);
            cache.Add(key, value);
        }

        // DELETE cache
        public void Delete(string key)
        {
            Console.WriteLine("Removing key: " + key);
            cache.Delete(key);
        }
    }
}
