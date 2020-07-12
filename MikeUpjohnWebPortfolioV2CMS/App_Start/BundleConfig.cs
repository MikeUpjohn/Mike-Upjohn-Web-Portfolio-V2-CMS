using System.Web.Optimization;

namespace MikeUpjohnWebPortfolioV2CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.structure.css",
                      "~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/bundles/customcss").Include(
                "~/_includes/css/core.css",
                "~/_includes/css/mobile.css"));

            bundles.Add(new ScriptBundle("~/bundles/pluginsjs").Include(
                "~/Scripts/jquery-ui.js",
                "~/Scripts/sweetalert.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                "~/_includes/js/core.js"));

            //bundles.Add(new ScriptBundle("~/bundles/ckeditorjs").Include(
            //    "~/_includes/ckeditor/ckeditor.js"
            //    /*"~/_includes/js/config.js"*/));

            BundleTable.EnableOptimizations = true;
        }
    }
}
