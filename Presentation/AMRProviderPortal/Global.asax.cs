using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AMR.Core.Utilities;
using AMR.Controllers;
using AMR.Controllers.Routing;
using AMR.Controllers.AuthenticationExtensions;
using System.Web.Optimization;
using AMR.Controllers.CustomActionFilter;

namespace AMRProviderPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RoutingManager.RegisterDefaultRoutesProvider(routes);
        
            ControllerBuilder.Current.SetControllerFactory(new ProviderControllerFactory());
        }
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            FilterManager.RegisterProviderFilters(filters);


        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
           // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           // RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            RegisterFilters(GlobalFilters.Filters);
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery").Include("~/Content/css/ui/jquery.ui.core.js", "~/Content/css/ui/jquery.ui.widget.js", "~/Content/css/ui/jquery.ui.accordion.js", "~/Content/js/jquery-1.9.1.js", "~/Content/js/jquery-1.9.2.custom.js", "~/Content/js/jquery-ui.js", "~/Content/js/jquery.maskedinput-1.2.2.js", "~/Content/js/validator.js", "~/Content/js/timeout-dialog.js", "~/Content/js/jquery.idle-timer.js", "~/Content/js/jquery.fileupload.js", "~/Content/js/jquery.fileupload-ui.js", "~/Content/js/jquery.iframe-transport.js", "~/Content/js/jquery.fixedheadertable.js", "~/Content/js/MasterLayout.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/Bundle/css").Include("~/Content/css/bootstrap.css", "~/Content/css/fixed-header/960.css", "~/Content/css/fixed-header/defaultTheme.css", "~/Content/css/fixed-header/myTheme.css", "~/Content/css/jquery.treeview.css", "~/Content/css/justified-nav.css", "~/Content/css/themes/redmond/jquery-ui.css", "~/Content/css/themes/redmond/jquery-ui-1.10.3.custom.css", "~/Content/css/table-style.css", "~/Content/css/navigation-bar.css", "~/Content/css/style-main.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/ProviderMessage/jquery").Include("~/Content/js/ProviderMessage.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/ProviderMessage/css").Include("~/Content/css/ProviderMessage.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/AccountIndex/jquery").Include("~/Content/js/AccountIndex.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/RptIndex/jquery").Include("~/Content/js/jquery.treeview.js", "~/Content/js/RptIndex.js"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["UserId"] = "0";
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Authentication.LoadAuthenticationInformation();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Authentication objAuthentication = new Authentication();
            objAuthentication.LogOff(Convert.ToInt64(Session["UserId"]));
            Session["UserId"] = "0";
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}