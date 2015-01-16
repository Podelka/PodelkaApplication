using System.Web;
using System.Web.Optimization;

namespace Podelka
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryMin_main")
                        .Include("~/Scripts/jquery-{version}.min.js")
                        .Include("~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryValidate").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css/css_style_font-awesome")
                .Include("~/Content/css/style.css")
                .Include("~/Content/css/font-awesome.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}