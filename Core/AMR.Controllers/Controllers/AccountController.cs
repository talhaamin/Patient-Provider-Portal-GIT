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
    public class AccountController : Base.BaseController
    {
      
        [AnotherSessionRedirectionFilter] [Authorize]
        //[HttpGet]
        public ActionResult AccountIndex()
        {
            HomeViewModel HomeList = new HomeViewModel();
           HomeList.PatientRepresentatives.loginFlag = Convert.ToBoolean(HttpContext.Session["loginRepFlag"]);
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();
                DataPatientService.PatientRepData RepParms = new DataPatientService.PatientRepData();
                try
                {
                    Session["PatientRepresentative"] = false;
                    PRParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PRParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);
                    PRParms.Option = 1;
                    RepParms = service.GetPatientRepData(PRParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    HomeList.PatientRepresentatives.edit = RepParms.Valid;
                    if (RepParms.Valid)
                    {
                      Session["PatientRepresentative"] = true;
                        HomeList.PatientRepresentatives = base.ConvertToPatientRepDataModel(RepParms);
                    }

                    DataPatientService.PatientTableData ParentData = new DataPatientService.PatientTableData();
                    DataPatientService.PatientParms patparm = new DataPatientService.PatientParms();
                    patparm.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);
                    ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ParentData.Valid)
                    {
                        HomeList.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                      

                    //   string htmls = ViewHelper.RenderRazorViewToString("_Index_Switch_Account", ParentData.dt.ToPatientAccountLinkModel(), this);
                        //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                    }
                    DataAuthenticationService.PatientLoginData PatLog = new DataAuthenticationService.PatientLoginData();
                    using (var AuthService = new DataAuthenticationService.AuthenticationWSSoapClient())
                    {
                        PatLog = AuthService.GetPatientLastLogin(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatLog.Valid)
                        {
                            ViewBag.loginDate = PatLog.LastLoginDate.ToShortDateString();
                            ViewBag.DateCreated = PatLog.DateCreated.ToShortDateString();
                        }
                    }
                }



                catch (Exception ex)
                {
                    return Json(ex.Message,JsonRequestBehavior.AllowGet);
                }


             //   return View(HomeList);


            }

            //using (var service = new DataPatientService.PatientWSSoapClient())
            //{
               
            //        DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            //        DataPatientService.PatientRepData PatientDocument = new DataPatientService.PatientRepData();

            //        // PatParms.VisitId = 0;
            //        PatParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            //        PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //        PatParms.Option = 1;
            //        PatientDocument = service.GetPatientRepData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //        if (PatientDocument.Valid)
            //        {
            //            HomeList.PatientRepresentatives = base.ConvertToPatientRepDataModel(PatientDocument);
            //        }
                   
               
            //}
            using (var patwebstng = new DataPatientService.PatientWSSoapClient())
            {
                try
                {
                    DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
                    PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    DataPatientService.PatientWebSettingData PatData = new DataPatientService.PatientWebSettingData();
                    PatData = patwebstng.GetPatientWebSettingData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatData.Valid)
                    {
                        HomeList.PatientWebSettingData.CellCarrier = PatData.CellCarrier;
                        HomeList.PatientWebSettingData.EmailNotifyNewMessage = PatData.EmailNotifyNewMessage;
                        HomeList.PatientWebSettingData.TextNotifyNewMesssage = PatData.TextNotifyNewMesssage;
                    }
                    DataPatientService.PatientData PatientData = new DataPatientService.PatientData();
                    DataPatientService.PatientParms PatientParms = new DataPatientService.PatientParms();
                    PatientParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatientParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                    PatientData = patwebstng.GetPatientData(PatientParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientData.Valid)
                    {
                        //HomeList.Patient.EMail = PatientData.EMail;
                        //HomeList.Patient.MobilePhone = PatientData.MobilePhone;
                        HomeList.PatientWebSettingData.Email=PatientData.EMail;
                        HomeList.PatientWebSettingData.CellPhoneNumber = PatientData.MobilePhone;

                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }
            }
            using (var ConService = new DataPatientConfigService.ConfigWSSoapClient())
            {
                try
                {
                    DataPatientConfigService.CodeTableData CarData = new DataPatientConfigService.CodeTableData();
                    
                    CarData = ConService.GetCarriers(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (CarData.Valid)
                    {
                        HomeList.CarrierModel = CarData.dt.ToCarrierModelList();
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet);
                }

            }
          //  base.PopulatePatient();
            return View(HomeList);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult PatientRepSave(PatientRepModel Model)
        {
            HomeViewModel HomeList = new HomeViewModel();
            DataPatientService.PatientRepData RepData = new DataPatientService.PatientRepData();
          
            // base.PopulatePatient();
            RepData.PatientId = RequestHelper.MyGlobalVar.PatientId;
            RepData.FirstName = Model.FirstName;
            RepData.EMail = Model.EMail;
            RepData.LastName = Model.LastName;
            RepData.Demographics = Model.Demographics;
            RepData.Allergy = Model.Allergy;
            RepData.FamilyHistory = Model.FamilyHistory;
            RepData.LabResults = Model.LabResults;
            RepData.MedicalHistory = Model.MedicalHistory;
            RepData.Medication = Model.Medication;
            RepData.Problem = Model.Problem;
            RepData.Procedure = Model.Procedure;
            RepData.SocialHistory = Model.SocialHistory;
            RepData.SurgicalHistory = Model.SurgicalHistory;
            RepData.VitalSigns = Model.VitalSigns;
            RepData.Immunization = Model.Immunization;
            RepData.Organ = Model.Organ;
            RepData.ClinicalDoc = Model.ClinicalDoc;
            RepData.Insurance = Model.Insurance;
            RepData.EmergencyContact = Model.EmergencyContact;
            RepData.Appointment = Model.Appointment;
            RepData.Visit = Model.Visit;
            RepData.UploadDocs = Model.UploadDocs;
            RepData.PlanOfCare = Model.PlanOfCare;
            RepData.Messaging = Model.Messaging;
            RepData.DownloadTransmit = Model.DownloadTransmit;
            RepData.ClinicalSummary = Model.ClinicalSummary;
            RepData.Provider = Model.Provider;      // SJF Added 1/22/2015
            RepData.Enabled = Model.Enabled;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    RepData = service.SavePatientRepData(RepData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (RepData.Valid)
                    {
                        DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                        DataPatientService.PatientRepData PatientDocument = new DataPatientService.PatientRepData();

                       // PatParms.VisitId = 0;
                        PatParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatParms.Option = 1;
                        PatientDocument = service.GetPatientRepData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (RepData.Valid)
                        {
                            HomeList.PatientRepresentatives = base.ConvertToPatientRepDataModel(PatientDocument);
                        }
                        else
                        {
                            return Json(RepData.ErrorMessage);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_PatientRepresentative", HomeList.PatientRepresentatives, this);
          //  html = HomeList.PatientRepresentatives.GetPatientRepresentativeModelListHTMLForDashboard();
            return Json(html);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult PasswordCheck(UserModel Model)
        {
            string html = string.Empty;
            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();

            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    Model.UserLogin = RequestHelper.MyGlobalVar.UserLogin;

                    ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, Model.UserLogin, Model.Password);


                    
                    if (ResponseData.Valid)
                    {
                         html = "true";
                    }
                    else 
                    {
                      html = "false";

                    }
                        //end

                        
                  
                }
            }
            catch (Exception ex)
            {
                //model.ErrorMessage = ex.Message;
            }
            
            //html = "true";
            return Json(html);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult PasswordCheckEdit(UserModel Model)
        {
            string html = string.Empty;
            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();

            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    Model.UserLogin = RequestHelper.MyGlobalVar.UserLogin;

                    ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, Model.UserLogin, Model.Password);



                    if (ResponseData.Valid)
                    {
                        html = "true";
                    }
                    else
                    {
                        html = "false";

                    }
                    //end



                }
            }
            catch (Exception ex)
            {
                //model.ErrorMessage = ex.Message;
            }

            //html = "true";
            return Json(html);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult PasswordSave(SecurityQuestionModel Model)
        {
            HomeViewModel HomeLists = new HomeViewModel();
            string usertyp = RequestHelper.MyGlobalVar.PortalType;
            
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        ResponseData = service.ChangePassword(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 5, Model.Password, Model.NewPassword);
                        if (ResponseData.Valid)
                        {
                            base.PopulateSecurityPatient();
                        }

                    }
                    else
                    {
                        ResponseData = service.ChangePassword(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 4, Model.Password, Model.NewPassword);

                        if (ResponseData.Valid)
                        {
                            base.PopulateSecuirtyProvider();
                        }

                    }
                }

               
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = ResponseData.ErrorMessage;

            return Json(html);

        }

        public JsonResult AccountNotificationSave(PatientWebSettingDataModel Model)
        {
            HomeViewModel HomeLists = new HomeViewModel();
            DataPatientService.PatientWebSettingData NotificationSave = new DataPatientService.PatientWebSettingData();
            NotificationSave.EmailNotifyNewMessage = Model.EmailNotifyNewMessage;
            NotificationSave.TextNotifyNewMesssage = Model.TextNotifyNewMesssage;
            NotificationSave.CellCarrier = Model.CellCarrier;
            NotificationSave.Option = 1;
            NotificationSave.PatientId = RequestHelper.MyGlobalVar.PatientId;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    NotificationSave = service.SavePatientWebSettingData(NotificationSave,RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (NotificationSave.Valid)
                    {
                   // HomeLists.PatientWebSettingData=
                    }
                }

                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientData PatData = new DataPatientService.PatientData();
                    PatData.Option = 3;
                    PatData.MobilePhone = Model.CellPhoneNumber;
                    PatData.EMail = Model.Email;
                    PatData.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatData = service.SavePatientData(PatData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatData.Valid)
                    {
 
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;

            return Json(html);
        }
        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult LinkedPatientId(PatientAccountLinkModel model )
        {
            
            HomeViewModel homelist = new HomeViewModel();
            DataPatientService.PatientParms patparm = new DataPatientService.PatientParms();
            DataPatientService.PatientParms patchkpara = new DataPatientService.PatientParms();
            DataPatientService.PatientTableData ParentData = new DataPatientService.PatientTableData();
            DataPatientService.PatientData patientdata = new DataPatientService.PatientData();
            patparm.PatientId = RequestHelper.MyGlobalVar.PatientId;
            patchkpara.PatientId = model.PatientId_Linked;
            string htmls = "";
            bool message = true;
            DataPatientService.PatientAccountLinkData linkdata=new DataPatientService.PatientAccountLinkData();
            linkdata.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            linkdata.PatientId_Linked = Convert.ToInt64(model.PatientId_Linked);
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    patientdata = service.GetPatientData(patchkpara, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (patientdata.Valid)
                    {
                        linkdata = service.SavePatientAccountLinkData(linkdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        if (linkdata.Valid)
                        {
                            if (ParentData.Valid)
                            {
                                htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                                //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                            }


                        }
                        else
                        {
                            htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                        }
                    }
                    else {
                                 message = false;
                                        ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                                        if (linkdata.Valid)
                                        {
                                            if (ParentData.Valid)
                                            {
                                                htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                                                //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                                            }


                                        }
                                        else
                                        {
                                            htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);
                                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = htmls;

            return Json(new { html = html, message = message });

        }
        [AnotherSessionRedirectionFilter] [Authorize]
        [HttpPost]
        public JsonResult AccountLinkdelete( PatientAccountLinkModel model)
        {
            DataPatientService.PatientAccountLinkData linkdata = new DataPatientService.PatientAccountLinkData();
            DataPatientService.PatientTableData ParentData = new DataPatientService.PatientTableData();
            DataPatientService.PatientParms patparm = new DataPatientService.PatientParms();

            linkdata.PatientId = model.PatientId;
            linkdata.PatientId_Linked = model.PatientId_Linked;
            HomeViewModel HomeList = new HomeViewModel();
            string htmls = "";
           
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    linkdata = service.DeletePatientAccountLinkData(linkdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (linkdata.Valid)
                    {
                        patparm.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        ParentData = service.GetPatientAccountLinkList(patparm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (ParentData.Valid)
                        {
                            HomeList.patientaccount = ParentData.dt.ToPatientAccountLinkModel();

                            htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);

                            //   homelist.patientaccount = ParentData.dt.ToPatientAccountLinkModel();
                        }
                        else {
                            htmls = ViewHelper.RenderRazorViewToString("_Index_AccountLink", ParentData.dt.ToPatientAccountLinkModel(), this);

                        
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            
            }
            return Json(htmls);
        
        }

        [HttpPost]
        public JsonResult SendEmailMessage(string EmailAddress, string EmailBody, string EmailContact)
        {
            string html = String.Empty;


//Set To = "" - the system will know this is an internal email and route it to an internal email address. 
//            Set Subject = "Request From (PatientId)  Patient Name 
//            Set Body = Information collected in form. (Patient's email address and request)
               try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {

                    DataMessageService.EmailMessageData MessageData = new DataMessageService.EmailMessageData();

                    MessageData.To = "";
                    MessageData.Subject = "Request From ("+ RequestHelper.MyGlobalVar.PatientId +") " + RequestHelper.MyGlobalVar.PatientName;
                    MessageData.Body = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                             "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                             "	<tr style=\"background-color:#00a0e0;\">" +
                             "       <td valign=\"top\"> " +
                             "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                             "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                             "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                             "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                             "              </td>" +
                             "              </tr>" +
                             "          </table>" +
                             "       </td>" +
                             "   </tr>" +
                             "	<tr>" +
                             "       <td valign=\"top\">" +
                             "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                             "           <tr style=height:1px;><td></td></tr>" +
                             "			<tr >" +
                             "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                             "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Email Address: <strong>" + EmailAddress + "</strong>,</h1>  <br />" +
                             "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                             " Contact: "+ EmailContact +" <br /><br />" +
                             "         " + EmailBody + " <br /><br />" +
                             "               </span>" +
                             "             </td>" +
                             "            </tr>" +
                             "          </table>" +
                             "      </td>" +
                             "   </tr>" +
                             "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                             "</table>";
                    MessageData = service.SendEmailMessage(MessageData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid == false) 
                    {
                        html = "Error Sending";
                    }
                }
                    }
            catch (Exception ex) {
                html = "Error Sending";
            }
               html = "Email has been sent successfully!";
               return Json(html);

        }
    }
}
