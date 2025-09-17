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
//using System.Web.Optimization;

namespace AMRAdminPortal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            RoutingManager.RegisterDefaultRoutesAdmin(routes);// RegisterDefaultRoutesPatients(routes);
            ControllerBuilder.Current.SetControllerFactory(new AdminControllerFactory());
        }
        public static void RegisterFilters(GlobalFilterCollection filters)
        {
            FilterManager.RegisterAdminFilters(filters);
            
            
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
           // FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           // RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterRoutes(RouteTable.Routes);
            RegisterFilters(GlobalFilters.Filters);
        }
    }
}