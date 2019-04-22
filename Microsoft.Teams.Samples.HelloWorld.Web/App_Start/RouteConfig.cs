using System.Web.Mvc;
using System.Web.Routing;

namespace Microsoft.Teams.Samples.HelloWorld.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "MarkAsAnswered",     // Route name
                "{controller}/{action}/{key}/{messageId}",                           // URL with parameters
                new { controller = "Home", action = "MarkAsAnswered", key = "", messageId = "" }  // Parameter defaults
            );
        }
    }
}
