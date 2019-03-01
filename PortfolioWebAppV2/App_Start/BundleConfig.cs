using System.Web.Optimization;

namespace PortfolioWebAppV2
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Lib/Styles/default.css",
                "~/Lib/assets/css/ekko-lightbox.css"));

            bundles.Add(new StyleBundle("~/Content/js").Include(
                "~/Scripts/jquery-3.3.1.js",
                "~/Scripts/umd/popper.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Lib/assets/js/ekko-lightbox.js"));

            bundles.Add(new StyleBundle("~/Content/AdminPanelCss").Include(
                "~/Content/bootstrap.min.css",
             
                "~/Lib/assets/css/themify-icons.css",
               "~/Lib/assets/css/metisMenu.css",
                "~/Lib/assets/css/owl.carousel.min.css",
               "~/Lib/assets/css/slicknav.min.css",
               "~/Lib/assets/css/typography.css",
               "~/Lib/assets/css/default-css.css",
                "~/Lib/assets/css/styles.css",
               "~/Lib/assets/css/responsive.css"));


            bundles.Add(new StyleBundle("~/Content/AdminPanelBodyJs").Include(
                "~/Scripts/umd/popper.min.js",
                "~/Scripts/jquery-3.3.1.js",
                "~/Lib/assets/js/jquery.slimscroll.min.js",
                "~/Lib/assets/js/jquery.slicknav.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Lib/assets/js/vendor/modernizr-2.8.3.min.js",
                "~/Lib/assets/js/owl.carousel.min.js",
                "~/Lib/assets/js/metisMenu.min.js",
                "~/Scripts/jquery.dataTables.js",
                "~/Scripts/dataTables.bootstrap4.min.js",
                "~/Scripts/dataTables.responsive.min.js",
                "~/Scripts/responsive.bootstrap.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"
                ));

            bundles.Add(new StyleBundle("~/Content/AdminPanelBodyJsSecond").Include(
                "~/Lib/assets/js/plugins.js",
                "~/Lib/assets/js/scripts.js"));
        }
    }
}
