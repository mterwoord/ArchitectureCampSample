using System.Net.Http.Formatting;
using System.Web.Http;

namespace Hosting
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatter = new JsonMediaTypeFormatter();

            config.Formatters.Clear();
            config.Formatters.Add(formatter);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; // TODO: remove after development

            config.DependencyResolver = ContainerConfiguration.GetWebApiDependencyResolver();
        }
    }
}
