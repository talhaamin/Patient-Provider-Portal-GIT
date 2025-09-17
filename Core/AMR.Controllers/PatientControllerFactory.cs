using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;


namespace AMR.Controllers
{
    public class PatientControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Home": return new Controllers.HomeController();
                case "Medication": return new Controllers.MedicationController();
                case "ClinicalSummary": return new Controllers.ClinicalSummaryController();
                case "Message": return new Controllers.MessageController();
                case "Account": return new Controllers.AccountController();
                case "Premium": return new Controllers.PremiumController();
                // default: return new HomeController();
               
            }
            return null;
        }
    }
}
