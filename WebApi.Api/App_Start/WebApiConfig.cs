using System.Web.Http;

namespace WebApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services


            //Remove XML formatter
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);


            // Web API routes
            config.MapHttpAttributeRoutes();
      
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
