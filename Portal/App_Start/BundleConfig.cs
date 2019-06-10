using System.Web;
using System.Web.Optimization;

namespace Portal
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Base/css").Include(
                        "~/Content/css/normalize.css",
                        "~/Content/css/bootstrap.min.css",
                        "~/Content/css/font-awesome.min.css",
                        "~/Content/css/themify-icons.css",
                        "~/Content/css/flag-icon.min.css",
                        "~/Content/css/cs-skin-elastic.css",
                        "~/Content/scss/style.css",
                        "~/Content/css/lib/vector-map/jqvmap.min.css",
                        "~/Content/css/lib/chosen/chosen.css"//Carrega Select
                        ));

            bundles.Add(new ScriptBundle("~/Base/jquery").Include(
                        "~/Content/js/vendor/jquery-2.1.4.min.js"));

            bundles.Add(new ScriptBundle("~/Base/Dados").Include(
                        "~/Content/js/plugins.js",
                        "~/Content/js/jmask.js",
                        "~/Content/js/lib/chart-js/Chart.bundle.js",
                        "~/Content/js/dashboard.js",
                        "~/Content/js/widgets.js",
                        "~/Content/js/gmap.js",//Carregar Mapa
                        "~/Content/js/lib/vector-map/jquery.vmap.js",
                        "~/Content/js/lib/vector-map/jquery.vmap.min.js",
                        "~/Content/js/lib/vector-map/jquery.vmap.sampledata.js",
                        "~/Content/js/lib/vector-map/country/jquery.vmap.world.js",
                        "~/Content/js/lib/chosen/chosen.jquery.js",//Carrega select
                        "~/Content/js/main.js"
                ));
        }
    }
}
