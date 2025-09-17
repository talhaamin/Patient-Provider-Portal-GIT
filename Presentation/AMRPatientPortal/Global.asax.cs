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
using System.IO;
using AMR.Controllers.AuthenticationExtensions;
using AMR.Controllers.CustomActionFilter;
using System.Web.Optimization;

namespace AMRPatientPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RoutingManager.RegisterDefaultRoutesPatients(routes);
            ControllerBuilder.Current.SetControllerFactory(new PatientControllerFactory());
        }
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            FilterManager.RegisterPatientFilters(filters);


        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
           // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           //RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            RegisterFilters(GlobalFilters.Filters);
           // BundleTable.Bundles.IgnoreList.Clear();
           // BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery").Include("~/Content/js/jquery-1.9.1.js", "~/Content/js/jquery-1.9.2.custom.js", "~/Content/css/ui/jquery.ui.core.js", "~/Content/css/ui/jquery.ui.widget.js", "~/Content/css/ui/jquery.ui.accordion.js", "~/Content/js/jquery-ui.js", "~/Content/js/jquery.maskedinput-1.2.2.js", "~/Content/js/validator.js", "~/Content/js/jquery.maskedinput.min.js", "~/Content/js/jquery-ui-timepicker-addon.js", "~/Content/js/jquery.fileupload.js", "~/Content/js/jquery.fileupload-ui.js", "~/Content/js/jquery.iframe-transport.js", "~/Content/js/timeout-dialog.js", "~/Content/js/jquery.idle-timer.js", "~/Content/js/jquery.fixedheadertable.js", "~/Content/js/LayoutMaster.js"));
           // BundleTable.Bundles.Add(new StyleBundle("~/Bundle/css").Include("~/Content/css/bootstrap.css", "~/Content/css/justified-nav.css", "~/Content/css/themes/redmond/jquery-ui.css", "~/Content/css/themes/redmond/jquery-ui-1.10.3.custom.css", "~/Content/css/table-style.css", "~/Content/css/navigation-bar.css", "~/Content/css/style-main.css", "~/Content/css/jquery-ui-timepicker-addon.css", "~/Content/css/fixed-header/960.css", "~/Content/css/fixed-header/defaultTheme.css", "~/Content/css/fixed-header/myTheme.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/CheckBrowser").Include("~/Content/js/jquery.mb.browser.min.js", "~/Content/js/jquery.mb.browser.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/LayoutMaster").Include("~/Content/js/LayoutMaster.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery-timeoutdialog").Include("~/Content/js/timeout-dialog.js", "~/Content/js/jquery.idle-timer.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery-fileupload").Include("~/Content/js/jquery.fileupload.js", "~/Content/js/jquery.fileupload-ui.js", "~/Content/js/jquery.iframe-transport.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery-ui").Include("~/Content/css/ui/jquery.ui.core.js", "~/Content/css/ui/jquery.ui.widget.js", "~/Content/css/ui/jquery.ui.accordion.js", "~/Content/js/jquery-ui.js", "~/Content/js/jquery-ui-timepicker-addon.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/utility-script").Include("~/Content/js/jquery.maskedinput-1.2.2.js", "~/Content/js/validator.js", "~/Content/js/jquery.maskedinput.min.js", "~/Content/js/jquery.fixedheadertable.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundle/jquery").Include("~/Content/js/jquery-1.9.1.js", "~/Content/js/jquery-1.9.2.custom.js", "~/Content/js/CookieHandler.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/themes/redmond/css-jquery-ui.css").Include("~/Content/css/themes/redmond/jquery-ui.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/fixed-header/css").Include("~/Content/css/fixed-header/960.css", "~/Content/css/fixed-header/defaultTheme.css", "~/Content/css/fixed-header/myTheme.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/css-style-main.css").Include("~/Content/css/style-main.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/bootstrap").Include("~/Content/css/bootstrap.css", "~/Content/css/bootstrap.min-combo.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/justified").Include("~/Content/css/justified-nav.css", "~/Content/css/navigation-bar.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/timepicker").Include("~/Content/css/jquery-ui-timepicker-addon.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/table").Include("~/Content/css/table-style.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/Content/css/themes/redmond/custom").Include("~/Content/css/themes/redmond/jquery-ui-1.10.3.custom.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Index/css").Include("~/Content/css/Index.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/Index/jquery").Include("~/Content/js/IndexScript.js"));
         BundleTable.Bundles.Add(new ScriptBundle("~/MsgIndex/jquery").Include("~/Content/js/MsgIndex.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/MsgIndex/css").Include("~/Content/css/MsgIndex.css"));
            BundleTable.Bundles.Add(new StyleBundle("~/MedicalPortfolioIndex/css").Include("~/Content/css/MedicalPortfolioIndex.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/MedicalPortfolioIndex/jquery").Include("~/Content/js/MedicalPortfolioIndex.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/GeneralDocIndex/css").Include("~/Content/css/GeneralDocIndex.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/GeneralDocIndex/jquery").Include("~/Content/js/GeneralDocIndex.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/MedicationIndex/jquery").Include("~/Content/js/MedicationIndex.js"));


            BundleTable.Bundles.Add(new ScriptBundle("~/ShareMyRecIndex/jquery").Include("~/Content/js/ShareMyRecIndex.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/ShareMyRecIndex/css").Include("~/Content/css/ShareMyRecIndex.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/ManageDoctorIndex/jquery").Include("~/Content/js/ManageDoctorIndex.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/ManageDoctorIndex/css").Include("~/Content/css/ManageDoctorIndex.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/EmergencyContact/jquery").Include("~/Content/js/EmergencyContact.js"));
            BundleTable.Bundles.Add(new StyleBundle("~/EmergencyContact/css").Include("~/Content/css/EmergencyContact.css"));
            BundleTable.Bundles.Add(new ScriptBundle("~/AppointmentIndex/jquery").Include("~/Content/js/AppointmentIndex.js"));
           
            BundleTable.Bundles.Add(new ScriptBundle("~/ClinicalSummaryIndex/jquery").Include("~/Content/js/ClincialSummaryIndex.js"));
            BundleTable.Bundles.Add(new ScriptBundle("~/ProblemIndex/jquery").Include("~/Content/js/ProblemIndex.js"));
           BundleTable.EnableOptimizations = true;
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
            //Authentication.LoadAuthenticationInformation();
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