using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SimpleForum.Controllers;

namespace SimpleForum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "DDefault",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new
            //    {
            //        controller = "AjaxTest",
            //        action = "Index",
            //        id = UrlParameter.Optional
            //    }
            //);

            routes.MapRoute(
                name: "MainPage",
                url: "{controller}/{action}",
                defaults: new { controller = "Forum", action = "Index"}
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{model}",
                defaults: new { controller = "Forum", action = "Login", model = UrlParameter.Optional}
            );

            //var v = new ForumImageProvider().GetType();
            //routes.Add(new Route("ImageProvider/{UserId}/profile.png",

            //    new ForumImageRouteHandler()));
            //routes.MapRoute(
            //    name: "ThreadPage",
            //    url: "{controller}/{action}/{GuidId}",
            //    defaults: new { controller = "Forum", action = "ThreadsPage", GuidId = string.Empty }
            //);

            //routes.MapRoute(
            //    name: "CreateNewThreadPage",
            //    url: "{controller}/{action}/{GuidId}",
            //    defaults: new { controller = "Forum", action = "CreateNewThreadPage", GuidId = string.Empty }
            //);

            //routes.MapRoute(
            //    name: "NewUserRegistration",
            //    url: "Auth/{action}",
            //    defaults: new { controller = "Forum", action = "Registration" }
            //);
        }
    }
}