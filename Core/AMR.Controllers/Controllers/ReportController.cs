using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AMR.Models;
using AMR.Core.Extensions;
using System.Web.Security;
using AMR.Core.Utilities;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    class ReportController : Base.BaseController
    {
        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpGet]
        public ActionResult ReportIndex()
        {
            ReportViewModels ReportLists = new ReportViewModels();
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetPatientForProviderList(Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               
                ProviderData = service.GetFacilityListForProviders(Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    ReportLists.Facilities = ProviderData.dt.ToFacilityModelList();
                }
            }
            return View(ReportLists);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpGet]
        public ActionResult AdminReportIndex()
        {
            List<ThirdPartyModel> ThirdParty = new List<ThirdPartyModel>();
            AdminViewModel Admin = new AdminViewModel();
            ReportViewModels ReportLists = new ReportViewModels();
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();
                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                ConfigDocument = service.GetThirdPartyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    Admin.ThirdParty = ConfigDocument.dt.ToThirdPartyModelList();
                    Admin.ThirdParty.Insert(0, new ThirdPartyModel { ThirdPartyId = Convert.ToInt64("0"), Value = "--Select--" });
                }

            }
            //    ProviderData = service.GetPatientForProviderList(Convert.ToInt64(RequestHelper.MyAdminGlobalVar.UserLogin), RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

            //    ProviderData = service.GetFacilityListForProviders(Convert.ToInt64(RequestHelper.MyAdminGlobalVar.UserLogin), RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
            //    if (ProviderData.Valid)
            //    {
            //        ReportLists.Facilities = ProviderData.dt.ToFacilityModelList();
            //    }
            //}
            return View(Admin);
        }
        [HttpPost]
        public JsonResult AdminReportParameters(AdminReportModel Model)
        {
            string html = "";
            Guid id = Guid.NewGuid();
            var reportParameters = new Dictionary<string, string>();
            string strFrom;
            string strTo;
            string strThirdParty;


            if (Model.FromDate == null)
            {
                strFrom = null;
            }
            else
            {
                strFrom = Model.FromDate;
            }

            if (Model.ToDate == null)
            {
                strTo = null;
            }
            else
            {
                strTo = Model.ToDate;
            }
            if (Model.ThirdParty == "--Select--")
            {
                strThirdParty = null;   
            }
            else
            {
                strThirdParty = Model.ThirdParty;
            }
            reportParameters.Add("ThirdParty", Model.ThirdParty);
            reportParameters.Add("AccountType", Model.AccountType);
            reportParameters.Add("FromDate", strFrom);
            reportParameters.Add("ToDate", strTo);
            //reportParameters.Add("MU", Model.MU.ToString());
            Session["reportParameters"] = reportParameters;
            Session["reportPath"] = Model.ReportPath.ToString();




            return Json(string.Format("~/Reports/ReportForm.aspx?r={0}", id.ToString()));

        }
        [HttpPost]
        public JsonResult ReportParameters(ReportModel Model)
        {
            string html = "";
           Guid id = Guid.NewGuid();
           var reportParameters = new Dictionary<string, string>();
           int intQuarter;
           string strFrom;
           string strTo;
 
            if (Model.Quarter.ToString() == "1st Quarter")
            {
                intQuarter = 1;
            }
            else if (Model.Quarter.ToString() == "2nd Quarter")
            {
                intQuarter = 2;
            }
            else if (Model.Quarter.ToString() == "3rd Quarter")
            {
                intQuarter = 3;
            }
            else if (Model.Quarter.ToString() == "4th Quarter")
            {
                intQuarter = 4;
            }
            else
            {
                intQuarter = 0;

            }

            if (Model.FromDate == null)
            {
                strFrom = "1/1/1900";
            }
            else
            {
                strFrom = Model.FromDate;
            }

            if (Model.ToDate == null)
            {
                strTo = "1/1/1900";
            }
            else
            {
                strTo = Model.ToDate;
            }

           
           reportParameters.Add("Facility", Model.FacilityId.ToString());
           reportParameters.Add("Year", Model.Year);
           reportParameters.Add("QYear", Model.QYear);
           reportParameters.Add("Quarter", intQuarter.ToString());
           reportParameters.Add("FromDate", strFrom);
           reportParameters.Add("ToDate", strTo);
           reportParameters.Add("MU", Model.MU.ToString());
           Session["reportParameters"] = reportParameters;
           Session["reportPath"] = Model.ReportPath.ToString();
        
               
        

                return Json(string.Format("~/Reports/ReportForm.aspx?r={0}", id.ToString()));
          
        }
         [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public ActionResult ProblemListReport(ReportModel Model)
        {
            var reportParameters =
              new Dictionary<string, string>();

            //if (form["Category"] == "All")
            //    form["Category"] = "%";

            reportParameters.Add("Facility",  Model.Id);
            reportParameters.Add("Year", Model.Year.ToString());
            reportParameters.Add("QYear", Model.QYear.ToString());
            reportParameters.Add("Quarter", Model.Quarter.ToString());
            reportParameters.Add("FromDate", Model.FromDate.ToString());
            reportParameters.Add("ToDate", Model.ToDate.ToString());
            reportParameters.Add("MU", Model.MU.ToString());

            Session["reportParameters"] = reportParameters;
            Session["reportPath"] = Model.ReportPath.ToString();
            return
               Redirect("~/Reports/ReportViewer.aspx");
        }
    }
}
