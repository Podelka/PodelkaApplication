using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Podelka
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ////Вызов инициализации базы данных. Все исходные данные в БД будут удалены, включая ВСЕ таблицы
            //Database.SetInitializer<Context>(new AppDbInitializer());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
