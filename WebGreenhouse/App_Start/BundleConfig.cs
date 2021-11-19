using System.Web;
using System.Web.Optimization;

namespace WebGreenhouse
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/Scripts").Include(
                "~/Scripts/jquery-3.6.0.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap.js"
                ));
            bundles.Add(new ScriptBundle("~/assets/js").Include(
                      "~/assets/js/select2.min.js",
                      "~/assets/js/jquery.min.js",
                      "~/assets/js/popper.min.js",
                      "~/assets/js/ion.rangeSlider.min.js",
                      "~/assets/js/slick.js",
                      "~/assets/js/daterangepicker.js",
                      "~/assets/js/custom.js"
                ));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/assets/css/styles.css",
                      "~/assets/css/UserTheme.css",
                      "~/Areas/Admin/Resources/assets/css/Noticafition.css"
                      ));
        }
    }
}
