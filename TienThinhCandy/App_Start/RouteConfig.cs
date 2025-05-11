using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TienThinhCandy
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Orders",
             url: "don-hang",
             defaults: new { controller = "Orders", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
             name: "OrdersDetail",
             url: "chi-tiet/o{id}",
             defaults: new { controller = "Orders", action = "ViewDetail", alias = UrlParameter.Optional },
             namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
              name: "Products",
              url: "san-pham",
              defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
               name: "PostList",
               url: "bai-viet",
               defaults: new { controller = "Post", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
              name: "DetailPost",
              url: "{alias}-n{id}",
              defaults: new { controller = "Post", action = "Detail", alias = UrlParameter.Optional },
              namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
              name: "DetailProduct",
              url: "chi-tiet/{alias}-p{id}",
              defaults: new { controller = "Products", action = "Detail", alias = UrlParameter.Optional },
              namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
              name: "Contact",
              url: "lien-he",
              defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "TienThinhCandy.Controllers" }

            );
            routes.MapRoute(
              name: "AboutUs",
              url: "gioi-thieu",
              defaults: new { controller = "Home", action = "About", alias = UrlParameter.Optional },
              namespaces: new[] { "TienThinhCandy.Controllers" }

            );
            routes.MapRoute(
             name: "ShoppingCart",
             url: "gio-hang",
             defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "TienThinhCandy.Controllers" }

            );
            routes.MapRoute(
            name: "CheckOut",
            url: "thanh-toan",
            defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
            namespaces: new[] { "TienThinhCandy.Controllers" }

            );
            routes.MapRoute(
            name: "vnpay_return",
            url: "vnpay_return",
            defaults: new { controller = "ShoppingCart", action = "VnpayReturn", alias = UrlParameter.Optional },
            namespaces: new[] { "TienThinhCandy.Controllers" }
            );
            routes.MapRoute(
               name: "CategoryProduct",
               url: "danh-muc-san-pham/{alias}-{id}",
               defaults: new { controller = "Products", action = "ProductCategory", alias = UrlParameter.Optional },
               namespaces: new[] { "TienThinhCandy.Controllers" }
            );
           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TienThinhCandy.Controllers" }
            );
        }
    }
}
