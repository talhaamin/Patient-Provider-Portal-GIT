using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace AMR.Controllers
{
   public class AdminControllerFactory : DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            switch (controllerName)
            {
                case "Admin": return new Controllers.AdminController();
                case "Report": return new Controllers.ReportController();
            }
            return null;
        }
    }
}
