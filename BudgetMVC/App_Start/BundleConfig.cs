using System.Web;
using System.Web.Optimization;

namespace BudgetMVC
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/pickadate").Include("~/Scripts/pickadate.*"));

            bundles.Add(new ScriptBundle("~/bundles/spin").Include("~/Scripts/spin.*"));

            bundles.Add(new StyleBundle("~/Content/foundation/css").Include(
                       "~/Content/foundation/foundation.css",
                       "~/Content/foundation/app.css"));

            bundles.Add(new ScriptBundle("~/bundles/foundation").Include(
                      "~/Scripts/foundation/foundation.*",
                        "~/Scripts/foundation/jquery.*",
                      "~/Scripts/foundation/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.8*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js").Include("~/Scripts/jquery.validate.custom.js").Include("~/Scripts/jquery.validate.unobtrusive.*"));

            bundles.Add(new ScriptBundle("~/bundles/ko").Include("~/Scripts/knockout-2.*"));
            bundles.Add(new ScriptBundle("~/bundles/app").Include("~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/pickadate").Include("~/Content/pickadate/pickadate.01.default.css"));
        }
    }
}