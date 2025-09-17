using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AMR.Core.Utilities
{
    public static class ViewHelper
    {
        public static string RenderRazorViewToString(string viewName, object model, Controller CurrentController)
        {
            ViewDataDictionary ViewData = new ViewDataDictionary();
            try
            {


                ViewData.Model = model;
                    using (var sw = new StringWriter())
                    {
                        var viewResult = ViewEngines.Engines.FindPartialView(CurrentController.ControllerContext, viewName);
                        var viewContext = new ViewContext(CurrentController.ControllerContext, viewResult.View, ViewData, CurrentController.TempData, sw);
                        viewResult.View.Render(viewContext, sw);
                        viewResult.ViewEngine.ReleaseView(CurrentController.ControllerContext, viewResult.View);
                        return sw.GetStringBuilder().ToString();
                    }
            }
            catch (Exception ex)
            {

                
            }
            return "";
        }
    }
}
