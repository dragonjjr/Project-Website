using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "resetpwd",
               url: "ResetPwd/index/{email}/{code}",
               defaults: new { controller = "ResetPwd", action = "Index" }
           );

            routes.MapRoute(
                name: "login",
                url: "login/index/{email}/{code}",
                defaults: new { controller = "Login", action = "Index" }
            );

            routes.MapRoute(
                name: "detailProduct",
                url: "DetailProduct/index/{idProduct}",
                defaults: new { controller = "DetailProduct", action = "Index" }
            );

            routes.MapRoute(
                name: "cart",
                url: "Cart/index/{email}",
                defaults: new { controller = "Cart", action = "Index" }
            );

            routes.MapRoute(
            name: "orderSuccess",
            url: "ordersuccess/index/{idOrder}",
            defaults: new { controller = "OrderSuccess", action = "Index" }
        );
             routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
