using System.Web.Http;

namespace StudentWebService
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "OtherApi",
                routeTemplate: "api/{controller}/{index}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}