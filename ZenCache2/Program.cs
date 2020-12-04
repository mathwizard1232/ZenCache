using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace ZenCache2
{
    public class Program
    {
        // The skeleton for this project comes from a Microsoft example for C# self-hosted API at https://github.com/aspnet/samples/tree/master/samples/aspnet/WebApi/OwinSelfhostSample
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            // Start OWIN host
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values
                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.GetAsync(baseAddress + "cache").Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                Console.ReadLine();
            }

            
        }
    }
}
