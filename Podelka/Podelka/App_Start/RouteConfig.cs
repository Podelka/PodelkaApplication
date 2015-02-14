using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Podelka
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Показывает все продукты выбранного раздела и категории + выбранный вариант сортировки.
            //ЛИБО Показывает все продукты выбранного раздела (по всем категориям) + выбранный вариант сортировки.
            //ДЛя сортировки добавить вконец /sort/id
            //routes.MapRoute(
            //name: "Products",
            //url: "{section}/{id}/{Category}/{CategoryId}/{product}",
            //defaults: new { controller = "Product", action = "Section", id = 1, Category = UrlParameter.Optional, CategoryId = UrlParameter.Optional }
            //);

            //Путь на любое изделие
            //routes.MapRoute(
            //name: "Product",
            //url: "{Section}/{SectionId}/{Category}/{CategoryId}/{controller}/{action}/{ProductId}",
            //defaults: new { controller = "Product", action = "Profile", SectionId = 1, CategoryId = 1, ProductId = 1 }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{menu}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, menu = UrlParameter.Optional }
            );
        }
    }
}
