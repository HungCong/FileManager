using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FileManager
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Thay đổi mk
            routes.MapRoute(
                name: "change password user",
                url: "doi-mat-khau/{userId}",
                defaults: new { controller = "Login", action = "ChangePass", id = UrlParameter.Optional }
            );

            //xác nhận tài khoản email
            routes.MapRoute(
                name: "active user",
                url: "active/user-{userSession}",
                defaults: new { controller = "Login", action = "Active_User", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
