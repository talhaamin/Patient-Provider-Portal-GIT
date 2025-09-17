using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMR.Controllers.CustomActionFilter
{
    public static class FilterManager
    {
        public static void RegisterPatientFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AnotherSessionRedirectionFilter());
            filters.Add(new AdminAnotherSessionRedirectionFilter()); 
        }
        public static void RegisterProviderFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AnotherSessionRedirectionFilter());
            filters.Add(new AdminAnotherSessionRedirectionFilter()); 
        }
        public static void RegisterAdminFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AnotherSessionRedirectionFilter());
            filters.Add(new AdminAnotherSessionRedirectionFilter()); 
        }
    }
}
