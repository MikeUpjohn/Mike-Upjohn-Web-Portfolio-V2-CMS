using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MikeUpjohnWebPortfolioV2CMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Projects Pagination",
                url: "projects/page-{pageNumber}",
                defaults: new { controller = "Projects", action = "Index" }
            );

            routes.MapRoute(
                name: "Blogs Pagination",
                url: "blogs/page-{pageNumber}",
                defaults: new { controller = "Blogs", action = "Index" }
            );

            routes.MapRoute(
                name: "Images Pagination",
                url: "{controller}/page-{pageNumber}",
                defaults: new { controller = "Images", action = "Index" }
            );

            routes.MapRoute(
                name: "Dev Builds",
                url: "dev-builds/",
                defaults: new { controller = "Help", action = "Index" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
