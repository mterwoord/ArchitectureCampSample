using System.Web.Http;

namespace ServiceLayer
{
    public class Application : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
