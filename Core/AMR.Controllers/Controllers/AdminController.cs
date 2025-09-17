using AMR.Core.Extensions;
using AMR.Core.Utilities;
using AMR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using AMR.Controllers.CustomActionFilter;


namespace AMR.Controllers.Controllers
{
  
    class AdminController : Base.BaseController
    {
       // [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public ActionResult Index()
        {
            AdminViewModel AdminList = new AdminViewModel();

           //if (Request.Cookies["Admin"] != null)
           // {
                using (var ConService = new DataPatientConfigService.ConfigWSSoapClient())
                {
                    try
                    {
                        DataPatientConfigService.CodeTableData CarData = new DataPatientConfigService.CodeTableData();
                        DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();

                        CarData = ConService.GetThirdPartyCodes(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                        if (CarData.Valid)
                        {
                            AdminList.ThirdParty = CarData.dt.ToThirdPartyModelList();
                        }
                        ConfigDocument = ConService.GetSecurityQuestionCodes(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                        if (ConfigDocument.Valid)
                        {
                            AdminList.SecurityQuestion = ConfigDocument.dt.ToSecurityQuestionModelList();
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(ex.Message,  JsonRequestBehavior.AllowGet);
                    }

                }

                using (var Service = new DataPatientService.PatientWSSoapClient())
                {
                    try
                    {
                        DataPatientService.PatientPartnerTableData data = new DataPatientService.PatientPartnerTableData();
                        data = Service.GetPartnerCodes(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                        if (data.Valid)
                        {
                            AdminList.Partner = data.dt.ToPartnerModelList();
                             AdminList.Partner.Insert(0, new PartnerModelForAdmin { EMRSystemId = -1, Value = ""});
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        return Json(ex.Message);
                    }

                }
               

                //using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                //{
                //    DataPatientDocumentService.PatientDocTableData PatTableData = new DataPatientDocumentService.PatientDocTableData();
                //    DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                //    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                //    PatDocParms.PatientId = PatientId;
                //    PatTableData = service.GetPatientNoteData(PatDocParms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                //    if (PatTableData.Valid)
                //    {

                //        AdminList.PatientNotes = PatientDocument.dt.ToPatientNotesModelList();

                //    }


                //}

                return View(AdminList);
            //}
            //else
            //{
            //    return RedirectToAction("Login");
            //}
        }

        [Authorize]
        public ActionResult ProviderSearchIndex()
        {
            AdminViewModel AdminList = new AdminViewModel();
        using (var Service = new DataPatientService.PatientWSSoapClient())
            {
                try
                {
                    DataPatientService.PatientPartnerTableData data = new DataPatientService.PatientPartnerTableData();
                    data = Service.GetPartnerCodes(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (data.Valid)
                    {
                        AdminList.Partner = data.dt.ToPartnerModelList();
                           AdminList.Partner.Insert(0, new PartnerModelForAdmin { EMRSystemId = -1, Value = ""});
                    }

                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }

            }



            return View(AdminList);
        
        
        }

        public ActionResult AnotherSession()
        {
            return View();
        }
        
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        
        public ActionResult Login(UserModel model,string returnUrl)
        {
            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();
            objGlobalVar.FacilityId = 0;
            objGlobalVar.UserLogin = model.UserLogin;
            try
            {
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, objGlobalVar.UserLogin, model.Password);
                 if (ResponseData.Valid)
                    {

                        objGlobalVar.UserId = ResponseData.UserId;
                        objGlobalVar.Token = ResponseData.Token;
                      //  objGlobalVar.UserLogin = ResponseData.UserRoleLink.ToString();
                        if (ResponseData.LoginType == 1)
                        {
                            objGlobalVar.PortalType = "Admin";
                        }
                        if (ResponseData.LoginType == 1 && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                        {
                            TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                            //TicketHelper.CreateCareProviderAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true, "Admin");
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            objGlobalVar.Token = "";
                            objGlobalVar.UserId = 0;
                            model.ErrorMessage = "Invalid Login for Provider portal";
                        }
                    }
                    else
                    {
                        objGlobalVar.Token = "";
                        objGlobalVar.UserId = 0;
                       // model.ErrorMessage = "Invalid Login for Provider portal";
                        model.ErrorMessage = ResponseData.ErrorMessage;
                    }
                
            }


            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            HomeLists.CurrentUser = model;

            return View(HomeLists);
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult SearchPatients(PatientSearchModel Model)
        {
            List<PatientSearchModel> PatientList = new List<PatientSearchModel>();
          
            DataPatientService.PatientSearchParms SearchParams = new DataPatientService.PatientSearchParms();
            SearchParams.PatientId = Model.PatientId;
            SearchParams.EMRSystemId = Model.EMRSystemId;
          SearchParams.PageSize = Model.PageSize;
            SearchParams.PageNumber = Model.PageNumber;
            
            if (Model.FirstName != null)
                SearchParams.FirstName = Model.FirstName;
            else
                SearchParams.FirstName = "";
            if (Model.LastName != null)
                SearchParams.LastName = Model.LastName;
            else
                SearchParams.LastName = "";
            if (Model.dob != null)
                SearchParams.DOB = Convert.ToDateTime(Model.dob);
            else
                SearchParams.DOB = null;
         
            if (Model.Address1 != null)
                SearchParams.Address1 = Model.Address1;
            else
                SearchParams.Address1 = "";
            if (Model.City != null)
                SearchParams.City = Model.City;
            else
                SearchParams.City = "";
            if (Model.State != null)
                SearchParams.State = Model.State;
            else
                SearchParams.State = "";
            if (Model.Zip != null)
                SearchParams.Zip = Model.Zip;
            else
                SearchParams.Zip = "";
            if (Model.CountryCode != null)
                SearchParams.CountryCode = Model.CountryCode;
            else
                SearchParams.CountryCode = "";
            if (Model.EMail != null)
                SearchParams.EMail = Model.EMail;
            else
                SearchParams.EMail = "";
            if (Model.HomePhone != null)
                SearchParams.HomePhone = Model.HomePhone;
            else
                SearchParams.HomePhone = "";
            if (Model.FacilityId != null)
            {
                  SearchParams.FacilityId = Model.FacilityId;
            }
            else
                SearchParams.FacilityId = null;
            string error = "";
                   
                     string html = string.Empty;
                     Int64 count = 0;

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                    DataPatientService.PatientTableData PatData = new DataPatientService.PatientTableData();
                    PatData = service.PatientSearch(SearchParams, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatData.Valid)
                    {
                      //  PatientList = PatData.dt.ToPatientSearchList();
                        if (PatData.ErrorMessage == "The access id was deleted")
                        {
                            error = "The access id was deleted";
                         //   PatientList = PatData.dt.ToPatientSearchList();
                            html = ViewHelper.RenderRazorViewToString("_PatientTable", PatientList, this);

                        }
                        else
                        {
                            count = PatData.count;
                            
                            PatientList = PatData.dt.ToPatientSearchList();
                            html = ViewHelper.RenderRazorViewToString("_PatientTable", PatientList, this);

                        }
                    }
                               
            }
            return Json(new { html = html, error = error,count =count });
        }


        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult SearchProviders(ProviderSearchModel Model)
        {
            List<ProviderSearchModel> PatientList = new List<ProviderSearchModel>();

            DataProviderService.ProviderSearchParms parms = new DataProviderService.ProviderSearchParms();
          //  DataPatientService.PatientSearchParms SearchParams = new DataPatientService.PatientSearchParms();
            parms.ProviderId = Model.ProviderId;
            parms.EMRSystemId = Model.EMRSystemId;
            parms.PageSize = Model.PageSize;
          ///  parms.Country = Model.Country;
            parms.PageNumber = Model.PageNumber;

            if (Model.FirstName != null)
                parms.FirstName = Model.FirstName;
            else
                parms.FirstName = "";
            if (Model.LastName != null)
                parms.LastName = Model.LastName;
            else
                parms.LastName = "";
         
            if (Model.EMail != null)
                parms.EMail = Model.EMail;
            else
                parms.EMail = "";
            if (Model.Phone != null)
                parms.Phone = Model.Phone;
            else
                parms.Phone = "";
            if (Model.License != null)
            {
                parms.License = Model.License;
            }
            else
                parms.License = null;

            if (Model.FacilityId != null)
            {
                parms.FacilityId = Model.FacilityId;
            }
            else
                parms.FacilityId = null;

            string error = "";

            string html = string.Empty;
            Int64 count = 0;

            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
              //  DataPatientService.PatientTableData PatData = new DataPatientService.PatientTableData();
                DataProviderService.ProviderTableData PatData = new DataProviderService.ProviderTableData();
                PatData = service.ProviderSearch(parms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                if (PatData.Valid)
                {
                    //  PatientList = PatData.dt.ToPatientSearchList();
                    if (PatData.ErrorMessage == "The access id was deleted")
                    {
                        error = "The access id was deleted";
                        //   PatientList = PatData.dt.ToPatientSearchList();
                        html = ViewHelper.RenderRazorViewToString("_ProviderTable", PatientList, this);

                    }
                    else
                    {
                       count = PatData.count;

                        PatientList = PatData.dt.ToProviderSearchList();
                        html = ViewHelper.RenderRazorViewToString("_ProviderTable", PatientList, this);

                    }
                }

            }
            return Json(new { html = html, error = error, count = count });
        
        }
        [HttpPost]
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult GetPatient(PatientAdminModel Model)
        {
            PatientAdminModel PatientData = new PatientAdminModel();
            DataPatientService.PatientParms Params = new DataPatientService.PatientParms();
            Params.PatientId = Model.PatientId;

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientAdmin PatData = new DataPatientService.PatientAdmin();
                PatData = service.GetPatientAdminData(Params, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                if (PatData.Valid)
                {
                    Model.Name = PatData.Name;
                    Model.Password = PatData.Password;
                    Model.Address = PatData.Address;
                    Model.Address2 = PatData.Address2;
                    Model.CityStateZip = PatData.CityStateZip;
                    Model.RepresentativePwd = PatData.RepresentativePwd;
                    Model.EMail = PatData.EMail;
                    Model.PremiumFlag = PatData.PremiumFlag;
                    Model.PremiumExpireDate = PatData.PremiumExpireDate;
                    Model.Active = PatData.Active;
                    Model.ThirdPartyId = PatData.ThirdPartyId;
                    Model.ThirdPartyName = PatData.ThirdPartyName;
                    Model.EMRSystemId = PatData.EMRSystemId;
                    Model.EMRSystemName = PatData.EMRSystemName;
                    Model.RepresentativePwd = PatData.RepresentativePwd;
                }
               
            }
            DataPatientDocumentService.PatientNoteData PatNote = new DataPatientDocumentService.PatientNoteData();
            DataPatientDocumentService.PatientDocTableData PatTableData = new DataPatientDocumentService.PatientDocTableData();

           
            List<PatientNotesModel> NoteModel = new List<PatientNotesModel>();
          //  PatNote.Note = Note;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
              
                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                    PatDocParms.PatientId = Model.PatientId;
                    PatTableData = service.GetPatientNoteData(PatDocParms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatTableData.Valid)
                    {

                        NoteModel = PatTableData.dt.ToPatientNotesModelList();
                        if (NoteModel.Count > 0)
                        {
                            Model.ClientNote = true;
                        }
                        else
                        {
                            Model.ClientNote = false;

                        }

                    }
                    DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                    PatParms.PatientId = Model.PatientId;
                    List<FacilityModel> facility = new List<FacilityModel>();
                    using (var Service = new DataPatientService.PatientWSSoapClient())
                    {
                        FacilityData = Service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        if (FacilityData.Valid)
                        {
                            facility = FacilityData.dt.ToFacilityModelList();
                            //HomeLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                            foreach (var item in facility)
                            {
                                if (item.FacilityName != "Patient Entered")
                                {
                                    Model.Practices += item.FacilityName + ",";
                                }
                            }
                           // HomeLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                        }
                    }
            }
            string html = new JavaScriptSerializer().Serialize(Model);
            return Json(html);
        }

        [HttpPost]
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult GetProviderData(ProviderAdminModel Model)
        {
            ProviderAdminModel ProviderData = new ProviderAdminModel();
            DataProviderService.ProviderParms Parms = new DataProviderService.ProviderParms();
            Parms.ProviderId = Model.ProviderId;

            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderAdminData ProviderDat = new DataProviderService.ProviderAdminData();

                ProviderDat = service.GetProviderAdminData(Parms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                if (ProviderDat.Valid)
                {
                    Model.ProviderId = ProviderDat.ProviderId;
                    Model.Name = ProviderDat.FirstName +" "+ProviderDat.LastName;
                    Model.EMail = ProviderDat.Email;
                    Model.Password = ProviderDat.Password;
                    Model.Active = ProviderDat.Active;
                                       
                }
            }
            DataProviderService.ProviderTableData ProviderDatatable = new DataProviderService.ProviderTableData();
          //  DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
          //  PatParms.PatientId = Model.PatientId;
            Int64 ProviderId=Model.ProviderId;
            List<FacilityModel> facility = new List<FacilityModel>();
            using (var Service = new DataProviderService.ProviderWSSoapClient())
            {
                ProviderDatatable = Service.GetFacilityListForProviders(ProviderId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (ProviderDatatable.Valid)
                {
                    facility = ProviderDatatable.dt.ToFacilityModelList();
                    //HomeLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    foreach (var item in facility)
                    {
                        if (item.FacilityName != "Patient Entered")
                        {
                            Model.Practices += item.FacilityName + ",";
                        }
                    }
                    // HomeLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }
            }
            string html = new JavaScriptSerializer().Serialize(Model);
            return Json(html);

        }


       [AdminAnotherSessionRedirectionFilter]
       [Authorize]
       public ActionResult PatientLogin(UserModel model) {
            string url = "";
            if (HttpContext.Request.Url.Port != 80 && HttpContext.Request.Url.Port != 443)
            {
                if (HttpContext.Request.Url.Host == "localhost")
                {
                    url = "http://" + HttpContext.Request.Url.Host + ":60731/EditPatientDataLogin/?UserLogin=" + model.UserLogin + "&Password=" + model.Password;
                }
            }
            else
            {
                url = "http://" + HttpContext.Request.Url.Host + "/EditPatientDataLogin/?UserLogin=" + model.UserLogin + "&Password=" + model.Password;
            }
            //return RedirectToAction(
            //Create a new cookie, passing the name into the constructor
            HttpCookie cookie = new HttpCookie("EditPatientData");

            //Set the cookies value
            cookie.Value = "1";

            //Set the cookie to expire in 1 minute
            DateTime dtNow = DateTime.Now;
            TimeSpan tsMinute = new TimeSpan(0, 0, 1, 0);
            cookie.Expires = dtNow + tsMinute;

            //Add the cookie
            Response.Cookies.Add(cookie);

            return Redirect(url);
            //return View();
        }
        //[Authorize]
        //public ActionResult PatientLogin(UserModel model)
        //{
        //    HomeViewModel HomeLists = new HomeViewModel();
        //    PatientRepModel PatientRepresentatives = new PatientRepModel();
        //    GlobalVar objGlobalVar = new GlobalVar();


        //    objGlobalVar.FacilityId = 0;
        //    objGlobalVar.UserLogin = model.UserLogin;
        //    if (model.UserLogin.StartsWith("R"))
        //    { PatientRepresentatives.loginFlag = true; }
        //    else { PatientRepresentatives.loginFlag = false; }
        //    Session["loginRepFlag"] = PatientRepresentatives.loginFlag;
        //    DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

        //    try
        //    {
        //        using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
        //        {

        //            ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, model.UserLogin, model.Password);



        //            if (ResponseData.Valid)
        //            {

        //                objGlobalVar.UserId = ResponseData.UserId;
        //                objGlobalVar.Token = ResponseData.Token;
        //                if (ResponseData.LoginType == 5)
        //                {
        //                    objGlobalVar.PortalType = "Patient";
        //                    Session["LoginType"] = "PatientPortal";
        //                }
        //                else if (ResponseData.LoginType == 4)
        //                {
        //                    objGlobalVar.PortalType = "Provider";
        //                }
        //                else if (ResponseData.LoginType == 6)
        //                {
        //                    objGlobalVar.PortalType = "Patient";
        //                }
        //                Session.Add("UserId", ResponseData.UserId);


        //                DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
        //                if (model.UserLogin.ToString().StartsWith("R"))
        //                {
        //                    PatParams.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
        //                    objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
        //                }
        //                else
        //                {
        //                    PatParams.PatientId = Convert.ToInt64(model.UserLogin);
        //                    objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin);
        //                }
        //                DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
        //                using (var Pservice = new DataPatientService.PatientWSSoapClient())
        //                {
        //                    PatTable = Pservice.GetPatientData(PatParams, ResponseData.Token, ResponseData.UserId, 0);
        //                    objGlobalVar.PremiumFlag = PatTable.PremiumFlag;
        //                    objGlobalVar.ActiveFlag = PatTable.Active;
        //                    objGlobalVar.PatientName = PatTable.FirstName + " " + PatTable.LastName;

        //                    DataPatientService.PatientFacilityOptionsData FacTable = new DataPatientService.PatientFacilityOptionsData();

        //                    FacTable = Pservice.GetPatientFacilityOptions(Convert.ToInt64(model.UserLogin), ResponseData.Token, ResponseData.UserId, 0);
        //                    objGlobalVar.AppointmentMessageAvailable = FacTable.AppointmentMessageAvailable;
        //                    objGlobalVar.GeneralMessageAvailable = FacTable.GeneralMessageAvailable;
        //                    objGlobalVar.MedicationMessageAvailable = FacTable.MedicationMessageAvailable;
        //                    objGlobalVar.ReferralMessageAvailable = FacTable.ReferralMessageAvailable;

        //                    DataPatientService.PatientVisitData PatVisit = new DataPatientService.PatientVisitData();
        //                    DataPatientService.PatientVisitParms PatVisitParams = new DataPatientService.PatientVisitParms();
        //                    PatVisitParams.PatientId = Convert.ToInt64(model.UserLogin); ;

        //                    PatVisit = Pservice.GetLatestPatientVisit(PatVisitParams, ResponseData.Token, ResponseData.UserId, 0);
        //                    objGlobalVar.FacilitySelectId = PatVisit.FacilityId;
        //                    objGlobalVar.VisitId = PatVisit.VisitId;

        //                    DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
        //                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
        //                    PatParms.PatientId = Convert.ToInt64(model.UserLogin);
        //                    PatientImage = Pservice.GetPatientImageData(PatParms, ResponseData.Token, ResponseData.UserId, 0);
        //                    if (PatientImage.Image != null)
        //                    {
        //                        Session["PatDisp"] = System.Convert.ToBase64String(PatientImage.Image);
        //                    }
        //                    else
        //                    {
        //                        string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
        //                        byte[] bytes = System.IO.File.ReadAllBytes(filePath);
        //                        Session["PatDisp"] = System.Convert.ToBase64String(bytes);
        //                    }
        //                }


        //                //start
        //              //  if (model.UserLogin.ToString().StartsWith("R"))
        //              //  {
        //                    using (var service1 = new DataPatientService.PatientWSSoapClient())
        //                    {
        //                        DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();
        //                        DataPatientService.PatientRepData RepParms = new DataPatientService.PatientRepData();
        //                        try
        //                        {
        //                            PRParms.FacilityId = objGlobalVar.FacilityId;
        //                            PRParms.PatientId = Convert.ToInt64(model.UserLogin.Substring(1)); //objGlobalVar.PatientId; 
        //                            PRParms.Option = 1;
        //                            RepParms = service1.GetPatientRepData(PRParms, objGlobalVar.Token, objGlobalVar.UserId, objGlobalVar.FacilityId);
        //                            PatientRepresentatives.edit = RepParms.Valid;
        //                            if (RepParms.Valid)
        //                            {
        //                                PatientRepresentatives = base.ConvertToPatientRepDataModel(RepParms);
        //                                }
        //                                else
        //                                {
        //                                    PatientRepresentatives.Demographics = true;
        //                                    PatientRepresentatives.Allergy = true;
        //                                    PatientRepresentatives.FamilyHistory = true;
        //                                    PatientRepresentatives.LabResults = true;
        //                                    PatientRepresentatives.MedicalHistory = true;
        //                                    PatientRepresentatives.Medication = true;
        //                                    PatientRepresentatives.Problem = true;
        //                                    PatientRepresentatives.EmergencyContact = true;
        //                                    PatientRepresentatives.SocialHistory = true;
        //                                    PatientRepresentatives.SurgicalHistory = true;
        //                                    PatientRepresentatives.VitalSigns = true;
        //                                    PatientRepresentatives.Immunization = true;
        //                                    PatientRepresentatives.Organ = true;
        //                                    PatientRepresentatives.ClinicalDoc = true;
        //                                    PatientRepresentatives.Insurance = true;
        //                                    PatientRepresentatives.ClinicalSummary = true;
        //                                    PatientRepresentatives.Appointment = true;
        //                                    PatientRepresentatives.Visit = true;
        //                                    PatientRepresentatives.UploadDocs = true;
        //                                    PatientRepresentatives.PlanOfCare = true;
        //                                    PatientRepresentatives.Messaging = true;
        //                                    PatientRepresentatives.DownloadTransmit = true;
        //                                    PatientRepresentatives.Procedure = true;
        //                                    PatientRepresentatives.Enabled = true;
        //                                }
        //                                Session["PatRep"] = PatientRepresentatives;
        //                                var mylist = string.Join(", ", PatientRepresentatives);
        //                                string Model = new JavaScriptSerializer().Serialize(PatientRepresentatives);
        //                                objGlobalVar.PatientRep = Model;
        //                        }


        //                        catch (Exception ex)
        //                        {
        //                            return Json(ex.Message);
        //                        }

        //                    }

                        
        //                //end

        //                if (HomeLists.PatientRepresentatives.Enabled == false && model.UserLogin.ToString().StartsWith("R"))
        //                {
        //                   // LogOff();
        //                    objGlobalVar.Token = "";
        //                    objGlobalVar.UserId = 0;
        //                    model.ErrorMessage = "Patient Rep access is disabled";

        //                }
        //                else
        //                {
        //                    if ((ResponseData.LoginType == 5 || ResponseData.LoginType == 6) && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
        //                    {
        //                        //TicketHelper.CreateAuthCookiePatient(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
        //                       // TicketHelper.CreateCareProviderAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true, "Admin");
                       
        //                        //return Redirect("http://localhost:60731/");
        //                        string url = "";
        //                        if (HttpContext.Request.Url.Port != 80 && HttpContext.Request.Url.Port != 443)
        //                        {
        //                            if (HttpContext.Request.Url.Host == "localhost")
        //                            {
        //                                url = "http://" + HttpContext.Request.Url.Host + ":60731/EditPatientDataLogin/?UserLogin="+ model.UserLogin+"&Password="+ model.Password;
        //                            }
        //                        }
        //                        else
        //                        {
        //                            url = "http://" + HttpContext.Request.Url.Host + "/";
        //                        }
        //                        //return RedirectToAction(
        //                        return Redirect(url);
        //                    }
        //                    else
        //                    {
        //                        objGlobalVar.Token = "";
        //                        objGlobalVar.UserId = 0;
        //                        model.ErrorMessage = "Invalid Login for Patient portal";
        //                    }
        //                } 
        //            }
        //            else
        //            {
        //                objGlobalVar.Token = "";
        //                objGlobalVar.UserId = 0;
        //                model.ErrorMessage = ResponseData.ErrorMessage;
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        model.ErrorMessage = ex.Message;
        //    }
        //    HomeLists.CurrentUser = model;
        //    return View(HomeLists);
        //}

        public ActionResult Logout()
        {
          
                    FormsAuthentication.SignOut();
                    //DeleteAuthenticationCookie();
                    return RedirectToAction("Login", "Admin");
                   
        
            //if (Request.Cookies["Admin"] != null)
            //{
            //    HttpCookie myCookie = new HttpCookie("Admin");
            //    //Request.Cookies["CareProvider"].Expires 
            //    myCookie.Expires = DateTime.Now.AddDays(-5d);

            //    Response.Cookies.Add(myCookie);
            //    return RedirectToAction("Login", "Admin");
            //}
            //return View();
        }
        
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public ActionResult ClinicalSummaryIndex()
        {
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            if (Request.QueryString["id"] != null)
            {
                int test = 0;
                test = Int32.Parse(Request.QueryString["id"].ToString());
                ClinicalSummaryLists.Querystring.Flag = test;
            }
            else
            {
                ClinicalSummaryLists.Querystring.Flag = 0;
            }
            //Configure Dropdowns Data
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();

                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;

                ConfigDocument = service.GetRelationshipCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Relationships = ConfigDocument.dt.ToRelationshipModelList();
                    ClinicalSummaryLists.Relationships.Insert(0, new RelationshipModel { RelationShipID = -1, Value = "--Select--", SNOMED = "-1" });
                }
            }
            return View();
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult UpgradeDowngrade(PatientAdminModel Model)
        {
            PatientAdminModel PatientData = new PatientAdminModel();
            DataPatientService.PatientParms Params = new DataPatientService.PatientParms();
           // Params.PatientId = Convert.ToInt64(Model.PatientId);
           
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientResp PatData = new DataPatientService.PatientResp();
                    if (Model.PremiumFlag == false)
                    {
                        PatData = service.UpgradePatient(Convert.ToInt64(Model.PatientId), RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                        if (PatData.Valid)
                        {
                            
                        }
                    }
                    else
                    {
                        PatData = service.DowngradePatient(Convert.ToInt64(Model.PatientId), RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                        if (PatData.Valid)
                        {
                            
                        }
                    }
                }
            }

            catch (Exception ex)
            { 
            
            }

            return Json(Model);
        
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult AdminLinkedPatientId(PatientAccountLinkModel model)
        {

            HomeViewModel homelist = new HomeViewModel();
            DataPatientService.PatientParms patparm = new DataPatientService.PatientParms();
            DataPatientService.PatientParms patchkpara = new DataPatientService.PatientParms();
            DataPatientService.PatientTableData ParentData = new DataPatientService.PatientTableData();
            DataPatientService.PatientData patientdata = new DataPatientService.PatientData();
            patparm.PatientId =  model.PatientId;
            patchkpara.PatientId = model.PatientId_Linked;
            string htmls = "";
            bool message = true;
            string msg = "";
            DataPatientService.PatientAccountLinkData linkdata = new DataPatientService.PatientAccountLinkData();
            linkdata.PatientId = Convert.ToInt64(model.PatientId);
            linkdata.PatientId_Linked = Convert.ToInt64(model.PatientId_Linked);
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    patientdata = service.GetPatientData(patchkpara, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);

                    if (patientdata.Valid)
                    {
                        linkdata = service.SavePatientAccountLinkData(linkdata, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                       // ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);

                        if (linkdata.Valid)
                        {
                            //if (ParentData.Valid)
                            //{
                            //  //  htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                            //    //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                            //}

                            msg = "Family Link Account Added Successfully";
                        }
                        else
                        {
                            msg = "Family Link Account Already Added";
                           // htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                        }
                    }
                    else
                    {
                        message = false;
                        msg = "Paient Not Exist";
                        //ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);

                        //if (linkdata.Valid)
                        //{
                        //    if (ParentData.Valid)
                        //    {
                        //      //  htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                        //        //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                        //    }


                        //}
                        //else
                        //{
                        //  //  htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = htmls;

            return Json(new { html = html, message = message,msg = msg});

        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult ActivateDecativatePatient(long PatientId, bool Activate, bool Deactivate)
        {
            string msg = "";
            DataPatientService.PatientResp PatResp = new DataPatientService.PatientResp();
            if (Activate)
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatResp = service.ActivatePatient(PatientId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (PatResp.Valid)
                    {
                        msg = "Patient Activated Successfully";
                    }
                }
            }
            else if (Deactivate)
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatResp = service.DeactivatePatient(PatientId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (PatResp.Valid)
                    {
                        msg = "Patient Deactivated Successfully";

                    }
                }
            }
                return Json(msg);
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult ActivateDecativateProvider(long ProviderId, bool Activate, bool Deactivate)
        {
            string msg = "";
            DataProviderService.ProviderResponse ProviderResp=new DataProviderService.ProviderResponse();
          // .PatientResp PatResp = new DataPatientService.PatientResp();
            if (Activate)
            {
                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    ProviderResp = service.ActivateProvider(ProviderId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (ProviderResp.Valid)
                    {
                        msg = "Patient Activated Successfully";
                    }
                }
            }
            else if (Deactivate)
            {
                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    ProviderResp = service.DeactivateProvider(ProviderId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (ProviderResp.Valid)
                    {
                        msg = "Patient Deactivated Successfully";

                    }
                }
            }
            return Json(msg);
        }
       
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult RenewPatient(long PatientId)
        {
            string msg = "";
            DataPatientService.PatientResp PatResp = new DataPatientService.PatientResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                PatResp = service.RenewPatient(PatientId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                if (PatResp.Valid)
                {
                    msg = "Patient Renew Successfull";
                }
                else {
                    msg = "Patient Renew Failed";
                }
            }
            return Json(msg);

        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult ClientNotes(long PatientId, string Note)
        {
            AdminViewModel AdminList = new AdminViewModel();
            string html = "";
            DataPatientDocumentService.PatientNoteData PatNote = new DataPatientDocumentService.PatientNoteData();
            DataPatientDocumentService.PatientDocTableData PatTableData = new DataPatientDocumentService.PatientDocTableData();

            PatNote.PatientId = PatientId;
            PatNote.Note = Note;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatNote = service.SavePatientNoteData(PatNote, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                if (PatNote.Valid)
                {
                    html = "Note Added Successfully";
                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                    PatDocParms.PatientId = PatientId;
                    PatTableData = service.GetPatientNoteData(PatDocParms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatTableData.Valid)
                    {

                        AdminList.PatientNotes = PatTableData.dt.ToPatientNotesModelList();
                        
                    }
                }
                else {
                    html = "Note not Saved!";
                }
            }
            html = ViewHelper.RenderRazorViewToString("_PatientNotesTable", AdminList.PatientNotes, this);
            return Json(html);
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetClientNotesData(long PatientId)
        {

            AdminViewModel AdminList = new AdminViewModel();
            string html = "";
            DataPatientDocumentService.PatientNoteData PatNote = new DataPatientDocumentService.PatientNoteData();
            DataPatientDocumentService.PatientDocTableData PatTableData = new DataPatientDocumentService.PatientDocTableData();

            PatNote.PatientId = PatientId;
           
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
               
                    
                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                    PatDocParms.PatientId = PatientId;
                    PatTableData = service.GetPatientNoteData(PatDocParms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatTableData.Valid)
                    {

                        AdminList.PatientNotes = PatTableData.dt.ToPatientNotesModelList();

                    }
                
                
            }
            html = ViewHelper.RenderRazorViewToString("_PatientNotesTable", AdminList.PatientNotes, this);
            return Json(html);
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult UpdateRenewelDate(Int64 PatientId, DateTime Date)
        {

            DataPatientService.PatientParms Params = new DataPatientService.PatientParms();
            // Params.PatientId = Convert.ToInt64(Model.PatientId);

            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientResp PatData = new DataPatientService.PatientResp();

                    PatData = service.ChangePatientRenewDate(PatientId, Date, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatData.Valid)
                    {

                    }

                }
            }

            catch (Exception ex)
            {

            }
            String date = Date.ToShortDateString();
            return Json(date);

        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult SaveThirdParty(Int64 PatientId, Int64 ThirdPartyId, string ThirdPartyName)
        {
            DataPatientService.PatientParms Params = new DataPatientService.PatientParms();
            try
            {
                using (var Service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientResp PatData = new DataPatientService.PatientResp();
                    PatData = Service.UpdatePatientThirdParty(PatientId, Convert.ToInt16(ThirdPartyId), RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                    if (PatData.Valid)
                    {

                    }
                }

            }

            catch (Exception ex)
            {

            }

            return Json(ThirdPartyName);
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ResetLockedAccount(string PatientId, string Password)
        {
            string msg = "";
           // DataAuthenticationService.UserData usrData = new DataAuthenticationService.UserData();
            DataAuthenticationService.AuthenticationResponse auResp = new DataAuthenticationService.AuthenticationResponse();
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
               // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                auResp = service.AuthenticatePatientInterface(0, PatientId, Password);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (auResp.Valid)
                {
                    //auResp = service.ResetLockedAccount(RequestHelper.MyAdminGlobalVar.Token, usrData.UserId, 0);
                    auResp = service.ResetLockedAccount(auResp.Token, auResp.UserId, 0);
                    if (auResp.Valid)
                    {
                        msg = "Account Reset Successfully";
                    }
                    else
                    {
                        msg = "Account Reset Failed";
                    }
                }
                else {
                    msg = "User Data Not Found!";
                }
            }
            return Json(msg);
        }

         [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ResetLockedAccountProvider(string PatientId, string Password)
        {
            string msg = "";
           // DataAuthenticationService.UserData usrData = new DataAuthenticationService.UserData();
            DataAuthenticationService.AuthenticationResponse auResp = new DataAuthenticationService.AuthenticationResponse();
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
               // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                auResp = service.AuthenticateInterface(0, PatientId, Password);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (auResp.Valid)
                {
                    //auResp = service.ResetLockedAccount(RequestHelper.MyAdminGlobalVar.Token, usrData.UserId, 0);
                    auResp = service.ResetLockedAccount(auResp.Token, auResp.UserId, 0);
                    if (auResp.Valid)
                    {
                        msg = "Account Reset Successfully";
                    }
                    else
                    {
                        msg = "Account Reset Failed";
                    }
                }
                else {
                    msg = "User Data Not Found!";
                }
            }
            return Json(msg);
        }



        
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ChangePatientPasswordAdmin(Int64 PatientId)
        {
            string msg = "";
            DataAuthenticationService.AuthenticationResponse auResp = new DataAuthenticationService.AuthenticationResponse();
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                auResp = service.ChangePatientPasswordAdmin(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId,PatientId);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (auResp.Valid)
                {
                  
                        msg = "Password Reset Successfully";
                    
                   
                }
                else
                    {
                        msg = "Password Reset Failed";
                    }
            }
            return Json(msg);
        }


        
            [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ChangeProviderPasswordAdmin(Int64 PatientId)
        {
            string msg = "";
            DataAuthenticationService.AuthenticationResponse auResp = new DataAuthenticationService.AuthenticationResponse();
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                auResp = service.ChangeProviderPasswordAdmin(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId,PatientId);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (auResp.Valid)
                {
                  
                        msg = "Password Reset Successfully";
                    
                   
                }
                else
                    {
                        msg = "Password Reset Failed";
                    }
            }
            return Json(msg);
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ChangeEmail(Int64 PatientId, string Email)
        {
            string msg = "";
            DataPatientService.PatientResp Resp = new DataPatientService.PatientResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                Resp =  service.ChangePatientEmail(PatientId, Email, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (Resp.Valid)
                {

                    msg = "Email Changed Successfully";


                }
                else
                {
                    msg = "Email Change Failed";
                }
            }
            return Json(msg);
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public JsonResult ChangeEmailProvider(Int64 ProviderId, string Email)
        {
            string msg = "";

            DataProviderService.ProviderResponse Response = new DataProviderService.ProviderResponse();
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
               Response = service.ChangeProviderEmail(ProviderId, Email, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Response.Valid)
                {
                    msg = "Email Changed Successfully";
                }
                else {
                    msg = "Email Change Failed";
                
                }
            
            }
            return Json(msg);
        
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult SearchOrangization(OrangizationModel Model)
        {
            FacilitySetupModel facilitysetup = new FacilitySetupModel();
            DataPatientConfigService.OrganizationSearchParms param = new DataPatientConfigService.OrganizationSearchParms();
            DataPatientConfigService.OrganizationParms orgparm = new DataPatientConfigService.OrganizationParms();
            param.OrganizationName=Model.OrganizationName;
            DataPatientConfigService.OrganizationTableData data = new DataPatientConfigService.OrganizationTableData();
            string html = "";
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {
                data = service.OrganizationSearch(param, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                if (data.Valid)
                {
                 
                    facilitysetup.ModelOrganizaton = data.dt.ToFacilityOranganization();
                
                }
            
            
            }
            html = ViewHelper.RenderRazorViewToString("_OrganizationTable",facilitysetup.ModelOrganizaton,this );
            return Json(html);
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetPracticeData(Int64 PracticeId)
        {
            string html = "";
            FacilitySetupModel facilitymodel = new FacilitySetupModel();
            DataPatientConfigService.PracticeParms param = new DataPatientConfigService.PracticeParms();
            DataPatientConfigService.PracticeTableData practicedata = new DataPatientConfigService.PracticeTableData();
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
               practicedata = service.GetPracticesForOrganization(PracticeId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
            
                    if (practicedata.Valid)
                    {
                        facilitymodel.ModelPractice = practicedata.dt.ToFacilityPractice();
                    }
                }
            }

            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_PracticeTable", facilitymodel.ModelPractice, this);
            return Json(html);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetFacilityData(Int64 PracticeId)
        {
            string html = "";
            FacilitySetupModel FaciliyModel = new FacilitySetupModel();
            DataPatientConfigService.FacilityTableData facillitydata = new DataPatientConfigService.FacilityTableData();
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                    facillitydata = service.GetFacilitiesForPractice(PracticeId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                    if (facillitydata.Valid)
                    {
                        FaciliyModel.ModelFacility = facillitydata.dt.ToFacilitySetupPractice();
                    }
                }
            }

            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_FacilityTable", FaciliyModel.ModelFacility, this);
            return Json(html);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult OrganizationSaveData(OrangizationModel Model)
        {
            DataPatientConfigService.OrganizationData data = new DataPatientConfigService.OrganizationData();
            DataPatientConfigService.OrganizationParms param = new DataPatientConfigService.OrganizationParms();
            data.OrganizationId = Model.OrganizationId;
            data.OrganizationName = Model.OrganizationName;
            data.Address1 = Model.Address1;
            data.Address2 = Model.Address2;
            data.Address3 = Model.Address3;
            data.City = Model.City;
            data.CountryCode = Model.CountryCode;
            data.PostalCode = Model.PostalCode;
            data.State = Model.State;
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                   data=service.SaveOrganizationData(data, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                  
                }
            }

            catch (Exception ex)
            {

            }
            return Json("");
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult PracticeSaveData(FacilityPracticeModel Model)
        {
            FacilitySetupModel FaciliyModel = new FacilitySetupModel();
            DataPatientConfigService.PracticeData data = new DataPatientConfigService.PracticeData();
            DataPatientConfigService.PracticeTableData practicedata = new DataPatientConfigService.PracticeTableData();
            data.PracticeId = Model.PracticeId;
            data.OrganizationId = Model.OrganizationId;
            data.PracticeName = Model.PracticeName;
            data.Address1 = Model.Address1;
            data.Address2 = Model.Address2;
            data.Address3 = Model.Address3;
            data.City = Model.City;
            data.State = Model.State;
            data.PostalCode = Model.PostalCode;
            data.CountryCode = Model.CountryCode;

            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                    data = service.SavePracticeData(data, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (data.Valid)
                    {

                       // param.PracticeId = Model.PracticeId;
                        practicedata = service.GetPracticesForOrganization(Model.OrganizationId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                        if (practicedata.Valid)
                        {
                            FaciliyModel.ModelPractice = practicedata.dt.ToFacilityPractice();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_PracticeTable", FaciliyModel.ModelPractice, this);
            return Json(html);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult FacilitySaveData( FacilitySetupPracticeModel Model)
        {
            FacilitySetupModel model = new FacilitySetupModel();
            DataPatientConfigService.FacilityTableData facillitydata = new DataPatientConfigService.FacilityTableData();

            DataPatientConfigService.FacilityData data = new DataPatientConfigService.FacilityData();
            data.FacilityId = Model.FacilityId;
            data.PracticeId = Model.PracticeId;
            data.FacilityName = Model.PracticeName;
            data.Address1 = Model.Address1;
            data.Address2 = Model.Address2;
            data.BillAddress3 = Model.Address3;
            data.City = Model.City;
            data.State = Model.State;
            data.PostalCode = Model.PostalCode;
            data.CountryCode = Model.CountryCode;
            data.Comment = Model.Comment;
            
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                    data = service.SaveFacilityData(data, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    if (data.Valid)
                    {
                        facillitydata = service.GetFacilitiesForPractice(Model.PracticeId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                        if (facillitydata.Valid)
                        {
                            model.ModelFacility = facillitydata.dt.ToFacilitySetupPractice();
                        }
                    }
                }
            }

            catch (Exception ex)
            {

            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_FacilityTable", model.ModelFacility, this);
            return Json(html);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        public ActionResult FaciltySetupIndex()
        {
           
            return View();
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetDocumentData(DocumentModelForAdmin Model)
        {
            string html = "";
            List<DocumentModelForAdmin> PatientDocument = new List<DocumentModelForAdmin>();
            DataPatientDocumentService.PatientDocTableData PatientDocumentSummary = new DataPatientDocumentService.PatientDocTableData();
            DataPatientDocumentService.PatientDocumentParms parms = new DataPatientDocumentService.PatientDocumentParms();
            parms.PatientId = Model.PatientId;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    
                    PatientDocumentSummary = service.GetCombinedPatientDocumentData(parms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                 if (PatientDocumentSummary.Valid)
                   {
                       PatientDocument = PatientDocumentSummary.dt.ToDocumentModelListAdmin();
                   }
                }
            }

            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_DocumentList", PatientDocument, this);
            return Json(html);
        
        
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult DeleteDocument(DocumentModelForAdmin Model)
        
        {
            string html = "";
            DataPatientDocumentService.PatientDocumentData PatientDocumentData = new DataPatientDocumentService.PatientDocumentData();
            DataPatientDocumentService.PatientCareDocumentData PatientCareData = new DataPatientDocumentService.PatientCareDocumentData();
            DataPatientDocumentService.PatientClinicalDocumentData PatientClinicalData=new DataPatientDocumentService.PatientClinicalDocumentData();
            DataPatientDocumentService.PatientMedicalDocumentData PatientMedicalData = new DataPatientDocumentService.PatientMedicalDocumentData();
            List<DocumentModelForAdmin> PatientDocument = new List<DocumentModelForAdmin>();
            DataPatientDocumentService.PatientDocTableData PatientDocumentSummary = new DataPatientDocumentService.PatientDocTableData();
            DataPatientDocumentService.PatientDocumentParms parms = new DataPatientDocumentService.PatientDocumentParms();
            parms.PatientId = Model.PatientId;
            //Patient Document
            PatientDocumentData.PatientId = Model.PatientId;
            PatientDocumentData.DocumentCntr = Model.DocumentCntr;
            
            //Patient Care Documents
            PatientCareData.PatientId = Model.PatientId;
            PatientCareData.DocumentCntr = Model.DocumentCntr;
          
            //Patient clinical Documents
            PatientClinicalData.PatientId = Model.PatientId;
            PatientClinicalData.DocumentCntr = Model.DocumentCntr;
            PatientClinicalData.FacilityId = Model.FacilityId;
            PatientClinicalData.VisitId = Model.VisitId;

            //Patient Medical Documents
            PatientMedicalData.PatientId = Model.PatientId;
            PatientMedicalData.DocumentCntr = Model.DocumentCntr;


            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    if (Model.DocType == "Dr. Uploaded")
                    {
                        PatientCareData = service.DeletePatientCareDocumentData(PatientCareData, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    }
                    else if (Model.DocType == "Clinical")
                    {
                        PatientClinicalData = service.DeletePatientClinicalDocumentData(PatientClinicalData, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
 
                    }
                    else if (Model.DocType == "Patient Uploaded")
                    {
                        PatientMedicalData = service.DeletePatientMedicalDocumentData(PatientMedicalData, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);
                    }
                    else {
                        PatientDocumentData = service.DeletePatientDocumentData(PatientDocumentData, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                    
                    }
                        
                    
                        PatientDocumentSummary = service.GetCombinedPatientDocumentData(parms, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                        if (PatientDocumentSummary.Valid)
                        {
                            PatientDocument = PatientDocumentSummary.dt.ToDocumentModelListAdmin();
                        }

                    
                }
            }
            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_DocumentList", PatientDocument, this);
            return Json(html);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]        
        public JsonResult DeletePatient(Int64 PatientId)
        {
            string msg = "";
            DataPatientService.PatientResp Resp = new DataPatientService.PatientResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                // usrData = service.GetUserTypeLink(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0, 5, PatientId);
                Resp = service.DeletePatientData(PatientId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                //auResp = service.AuthenticateInterface(0, PatientId, Password);
                if (Resp.Valid)
                {

                    msg = "Patient was deleted successfully";


                }
                else
                {
                    msg = "Patient deletion failed";
                }
            }
            return Json(msg);
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [HttpPost]
        [Authorize]
        public JsonResult GetPatientSecurityInfo(Int64 PatientId)
        {
            string question1 = "";
            string question2 = "";
            string answer1 = "";
            string answer2 = "";
            //DataAuthenticationService
          //  SecurityQuestionModel model = new SecurityQuestionModel();
            using (var service=new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                DataAuthenticationService.PatientSecurityData PatSecurityData = new DataAuthenticationService.PatientSecurityData();
                PatSecurityData = service.GetPatientSecurityInfo(PatientId, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId);
                if (PatSecurityData.Valid)
                {
                    question1 = PatSecurityData.SecurityQuestionId.ToString();
                    question2 = PatSecurityData.SecurityQuestionId2.ToString();
                    answer1 = PatSecurityData.SecurityAnswer;
                    answer2 = PatSecurityData.SecurityAnswer2;

               
                }
            }
            return Json(new { question1 = question1, question2 = question2, answer1 = answer1, answer2 = answer2 });
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [HttpPost]
        [Authorize]
        public JsonResult ChangeSecurityQuestion(string PatientId, SecurityQuestionModel Model)
        {

            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                DataAuthenticationService.AuthenticationResponse Response = new DataAuthenticationService.AuthenticationResponse();
             
               Response = service.ChangeSecurityQuestion(RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, RequestHelper.MyAdminGlobalVar.FacilityId, 1, Convert.ToInt32(Model.SecurityQuestionId), Model.SecurityAnswer, Convert.ToInt32(Model.SecurityQuestionId2), Model.SecurityAnswer2, PatientId);
                if (Response.Valid)
                {
                   

                }

            }
            return Json("");
        
        
        
        
        
        }

        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetVisitData(VisitModel Model )
        {
            string html = "";
            List<VisitModel> VisitModel = new List<VisitModel>();
            DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
            DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
            PatVisitParms.PatientId = Model.PatientId;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (VisitData.Valid)
                    {
                        VisitModel = VisitData.dt.ToVisitModelList();

                    }
                }
            }
            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_VisitList", VisitModel, this);
            return Json(html);
        

        
        
        }
       [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult GetFaclityData(int Id)
        {
            List<FacilityModel> model = new List<FacilityModel>();
            DataPatientConfigService.FacilityTableData facillitydata = new DataPatientConfigService.FacilityTableData();

            
       
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                       facillitydata = service.GetFacilitiesForPractice(Id, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                        if (facillitydata.Valid)
                        {
                            model= facillitydata.dt.ToFacilityModelList();//.ToFacilitySetupPractice();
                        }
                    
                }
            }

            catch (Exception ex)
            {

            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_FacilityLookUp", model, this);
            return Json(html);
        
        }


        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult SearchFacility(string FacilityName, int PageNumber, int PageSize)
        {
            List<FacilityModel> model = new List<FacilityModel>();
            DataPatientConfigService.FacilitySearchParms facilitySearchParm = new DataPatientConfigService.FacilitySearchParms();
            DataPatientConfigService.FacilityTableData data = new DataPatientConfigService.FacilityTableData();
            facilitySearchParm.FacilityName = FacilityName;
            facilitySearchParm.PageNumber = PageNumber;
            facilitySearchParm.PageSize = PageSize;
            try
            {
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {
                    data = service.FacilitySearch(facilitySearchParm, RequestHelper.MyAdminGlobalVar.Token, RequestHelper.MyAdminGlobalVar.UserId, 0);

                    if (data.Valid)
                    {
                        model = data.dt.ToFacilityModelList();//.ToFacilitySetupPractice();
                    }

                }
            }

            catch (Exception ex)
            {

            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_FacilityLookUp", model, this);
            return Json(new { html = html, count =data.count });
        
        }
        [AdminAnotherSessionRedirectionFilter]
        [Authorize]
        [HttpPost]
        public JsonResult DeleteVisit(VisitModel Model)
        {
            string html = "";
            List<VisitModel> VisitModel = new List<VisitModel>();
            DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
            DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientResp PatResponse = new DataPatientService.PatientResp();
            PatVisitParms.PatientId = Model.PatientId;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatResponse = service.DeletePatientVisitData(Model.PatientId, Model.VisitID, Model.FacilityId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatResponse.Valid)
                    {
                        VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (VisitData.Valid)
                        {
                            VisitModel = VisitData.dt.ToVisitModelList();

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            html = ViewHelper.RenderRazorViewToString("_VisitList", VisitModel, this);
            return Json(html);
        
        }

        private void DeleteAuthenticationCookie()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {

                authCookie.Expires = DateTime.Now.AddDays(-100);
                Response.Cookies.Set(authCookie);
            }

        }
        public ActionResult LogOffAdminPortal()
        {
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    ResponseData = service.LogoutUser(RequestHelper.MyGlobalVar.FacilityId, RequestHelper.MyGlobalVar.UserId);

                    if (ResponseData.Valid)
                    {
                        FormsAuthentication.SignOut();
                       //DeleteAuthenticationCookie();

                    }
                }
            }

            catch (Exception ex)
            {

            }
            return RedirectToAction("Login", "Admin");
        }
    }
}
