using Owin;
using System.Web.Http;

namespace ZenCache2
{
    public class Startup
    {
         // This code configures Web API contained in the class Startup, which is additionally specified as the type parameter in WebApplication.Start
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for Self-Host
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{key}/{value}",
                defaults: new { key = RouteParameter.Optional, value = RouteParameter.Optional }
            );

            appBuilder.UseWebApi(config);
        }
    }
}
