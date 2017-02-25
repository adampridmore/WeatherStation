using System.Configuration;
using System.Web.Optimization;

namespace WeatherStationServer
{
    public class Bundles
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                    .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                    .Include("~/Scripts/angular-ui/ui-bootstrap.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/angular")
                    .Include("~/Scripts/angular.js")
                    .Include("~/Scripts/angular-route.js")
            ); 

            bundles.Add(new ScriptBundle("~/bundles/app")
                    .IncludeDirectory("~/app", "*.js", true)
            );

            BundleTable.EnableOptimizations = IsBundleTableEnableOptimizationsEnabled();
        }

        private static bool IsBundleTableEnableOptimizationsEnabled()
        {
            bool value;
            if (bool.TryParse(ConfigurationManager.AppSettings["BundleTable.EnableOptimizations"], out value))
            {
                return value;
            }

            return false;
        }
    }
}