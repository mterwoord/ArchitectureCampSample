﻿using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ServiceLayer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var formatter = new JsonMediaTypeFormatter();

            // TODO: nope... use clean DTOs
            //formatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
            config.Formatters.Clear();
            config.Formatters.Add(formatter);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
