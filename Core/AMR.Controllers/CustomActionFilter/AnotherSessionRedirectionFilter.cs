using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AMR.Controllers.CustomActionFilter
{
    public class AnotherSessionRedirectionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            HttpCookie authCookie = filterContext.RequestContext.HttpContext.Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                if (filterContext.RequestContext.HttpContext.Request.UrlReferrer == null)
                {
                    if (!filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("another-session"))
                    {
                        if (!filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("Admin/PatientLogin"))
                        {
                           // if (filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath != "/")
                            //{
                                if (filterContext.RequestContext.HttpContext.Request.Browser.Browser.Contains("InternetExplorer") || filterContext.RequestContext.HttpContext.Request.Browser.Browser.Contains("IE"))
                                {
                                    //Grab the cookie
                                    HttpCookie cookie = filterContext.RequestContext.HttpContext.Request.Cookies["EditPatientData"];

                                    //Check to make sure the cookie exists
                                    if (cookie == null)
                                    {


                                        //if(filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath != "/")
                                        //{CCD-View, ccd, CustomizeCCD, Get-CCD-Data-XML, CCDHtmlDownload, index-patient-CCD-Send
                                        if (!filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("CCDLocation") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("CCD-View") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("CustomizeCCD") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("ccd") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("Get-CCD-Data-XML") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("CCDHtmlDownload") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("index-patient-CCD-Send") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("index-patient-CCD") && !filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath.Contains("logout"))
                                        {
                                            filterContext.Result = new RedirectResult("~/another-session");
                                        }

                                    }
                                    filterContext.RequestContext.HttpContext.Response.Cookies["EditPatientData"].Expires = DateTime.Now.AddDays(-1);
                                }
                                else
                                {
                                    filterContext.Result = new RedirectResult("~/another-session");
                                }
                            //}
                        }
                    }

                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage2 = "Custom Action Filter: Message from OnActionExecuted method.";
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage3 = "Custom Action Filter: Message from OnResultExecuting method.";
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage4 = "Custom Action Filter: Message from OnResultExecuted method.";
        }
    }
}
