using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;

namespace Hosting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatter = new JsonMediaTypeFormatter();
            //formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            config.Formatters.Clear();
            config.Formatters.Add(formatter);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.DependencyResolver = ContainerConfiguration.GetWebApiDependencyResolver();
        }
    }
}
