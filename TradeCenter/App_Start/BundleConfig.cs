using System.Web;
using System.Web.Optimization;

namespace TradeCenter
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/tradeCenter").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery.validate.bootstrap.js",
                        "~/Scripts/jquery.validate.unobtrusive.bootstrap.js",
                        "~/Scripts/datejs.js",
                        "~/Scripts/tradeCenter.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
            //            "~/Scripts/jquery-ui-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate.js",
            //            "~/Scripts/jquery.validate.unobtrusive.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //            "~/Scripts/bootstrap.js",
            //            "~/Scripts/jquery.validate.bootstrap.js",
            //            "~/Scripts/jquery.validate.unobtrusive.bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/themes/bootstrap/superhero-bootstrap.css",
                        "~/Content/themes/jquery-ui/core.css",
                        "~/Content/themes/jquery-ui/datepicker.css",
                        "~/Content/themes/jquery-ui/theme.css",
                        "~/Content/tradeCenter.css"));

            //bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
            //            "~/Content/bootstrap.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/bootstrap/css").Include(
            //            "~/Content/themes/bootstrap/superhero-bootstrap.css"));

            //bundles.Add(new StyleBundle("~/Content/themes/jquery-ui/css").Include(
            //            "~/Content/themes/jquery-ui/jquery-ui.css"));

            //bundles.Add(new StyleBundle("~/Content/tradeCenter").Include(
            //            "~/Content/tradeCenter.css"));
        }
    }
}