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
using System.Security.Cryptography;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Web.Script.Serialization;
using System.Data;
using System.Net;
using System.Globalization;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    public class HomeController : Base.BaseController
    {

        // starting actions of chart summary tab 
        //[HttpPost]
        //public JsonResult FilterAppointmentWidgData(PatientMessageModel Model)
        //{
        //    ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();

        //    DataMessageService.MessageParms patMessageParms = new DataMessageService.MessageParms();
        //   // DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
        //   // DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
        //    DataMessageService.MessageTableData PatientDocument = new DataMessageService.MessageTableData();


        //    patMessageParms.VisitId = Model.VisitId;
        //    patMessageParms. = Model.FacilityId;
        //    patMessageParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
        //    patMessageParms.Option = 1;
        //    try
        //    {
        //        using (var service = new DataMessageService.MessageWSSoapClient())
        //        {
        //            PatientDocument = service.GetMessageList(patMessageParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
        //            if (PatientDocument.Valid)
        //            {

        //                ClinicalSummaryLists.Messages = PatientDocument.dt.ToMessageModelList();

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message);
        //    }
        //    string html = string.Empty;

        //    html = ClinicalSummaryLists.Messages.GetAppointmentListHTMLForDashboard();
        //    return Json(html);
        //}
        
        public ActionResult UpgradePremium()
        {
            string returnurl = Request.QueryString["returnurl"];
            bool upgrade = Convert.ToBoolean(Request.QueryString["upgrade"]);
            DataPatientService.PatientResp PatParams = new DataPatientService.PatientResp();
            if (upgrade == false)
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatParams = service.UpgradePatient(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatParams.Valid)
                    {
                        base.PopulatePremiumPatient(true);
                       // RequestHelper.MyGlobalVar.PremiumFlag = true;
                    }
                    bool test = RequestHelper.MyGlobalVar.PremiumFlag;
                    

                }
                
            }

            if (upgrade == true)
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatParams = service.DowngradePatient(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatParams.Valid)
                    {
                      //  RequestHelper.MyGlobalVar.PremiumFlag = false;
                        base.PopulatePremiumPatient(false);
                    }
                }
            }
            //Ahmed Ask Khusroo?
            string url = "";
            if (HttpContext.Request.Url.Port != 80 && HttpContext.Request.Url.Port != 443)
            {
                url = "https://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + returnurl;
                if (HttpContext.Request.Url.Host == "localhost") {
                    url = "https://" + HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + returnurl;
                }
            }
            else
            {
                url = "https://" + HttpContext.Request.Url.Host + returnurl;
            }
            return Redirect(url);
           // return View();
        }

        [HttpPost]
        public JsonResult ResetPassword(UserModel Model)
        {
            DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

            int LoginType = 5;
            string msg = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


            try
            {

                ResponseData = AuthenticationWS.ResetPassword(0, Model.UserLogin, LoginType, Model.SecurityAnswer, Model.SecurityAnswer2);

                if (ResponseData.Valid)
                {
                    msg = "Your password has been reset.  Your new password has been sent to your email.";

                }
                else
                {
                    return Json(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = msg;
            return Json(html);


        }

         [HttpPost]
        public JsonResult ResetPasswordProvider(UserModel Model)
        {
          /*  DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();
            
            int LoginType = 6;
            string msg = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


            try
            {

                ResponseData = AuthenticationWS.ResetPassword(0, Model.UserLogin, LoginType, Model.SecurityAnswer, Model.SecurityAnswer2);

                if (ResponseData.Valid)
                {
                    msg = "Your password has been reset.  Your new password has been sent to your email.";

                }
                else
                {
                    return Json(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = msg;
            return Json(html);*/
            DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

            int LoginType = 6;
            string msg = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


            try
            {

                ResponseData = AuthenticationWS.ResetPassword(0, Model.UserLogin, LoginType, Model.SecurityAnswer, Model.SecurityAnswer2);

                if (ResponseData.Valid)
                {
                    msg = "Your password has been reset.  Your new password has been sent to your email.";

                }
                else
                {
                    return Json(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = msg;
            return Json(html);


        }


        [HttpPost]
        public JsonResult SecurityQuestion(UserModel Model)
        {
            HomeViewModel homlist = new HomeViewModel();
            DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();
            DataPatientService.PatientWSSoapClient service = new DataPatientService.PatientWSSoapClient();
            int LoginType = 5;
            bool flag = false;
            string userid = "";
            string htmlretun = string.Empty;
            string useremail = Model.UserEmail;
            string question = String.Empty;
            string questionTwo = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            DataPatientService.PatientTableData patientdt = new DataPatientService.PatientTableData();
            DataPatientService.PatientData patdata=new DataPatientService.PatientData();

             try
             {
                 if (Model.UserLogin != null)
                 {
                     if (Model.UserEmail == "" || Model.UserEmail ==null)
                     {
                         ResponseData = AuthenticationWS.GetSecurityQuestion(0, Model.UserLogin, LoginType,Model.UserEmail);

                     if (ResponseData.Valid)
                     {
                         question = ResponseData.SecurityQuestion;
                         questionTwo = ResponseData.SecurityQuestion2;
                         userid = Model.UserLogin;
                         flag = true;
                     }
                     else
                     {
                         return Json(ResponseData.ErrorMessage);
                     }
                     }
                     else {

                         ResponseData = AuthenticationWS.GetSecurityQuestion(0, Model.UserLogin, LoginType,Model.UserEmail);

                         if (ResponseData.Valid)
                         {
                             question = ResponseData.SecurityQuestion;
                             questionTwo = ResponseData.SecurityQuestion2;
                             userid = Model.UserLogin;
                             flag = true;
                         }
                         else
                         {
                             return Json(ResponseData.ErrorMessage);
                 }
                     
                     }
                 }
                 if (Model.UserEmail != null && Model.UserLogin == null)
                 {


                     patientdt = service.PatientEmailSearch(useremail,"40000000728000000415", 7, 0);
                     if (patientdt.Valid)
                         {
                             homlist.PatientModel = patientdt.dt.ToPatientModel();
                             if (patientdt.dt.Rows.Count > 0 && patientdt.dt.Rows.Count < 2)
                             {
                                 flag = true;
                                 userid=patientdt.dt.Rows[0][0].ToString();
                                 ResponseData = AuthenticationWS.GetSecurityQuestion(0, userid, LoginType,Model.UserEmail);
                                 if (ResponseData.Valid)
                                 {
                                     question = ResponseData.SecurityQuestion;
                                     questionTwo = ResponseData.SecurityQuestion2;
                                     flag = true;
                                 }
                                 else
                                 {
                                     return Json(ResponseData.ErrorMessage);
                                 }

                             }
                             else
                             {
                                 flag = false;
                                 question = "false";
                                 questionTwo = "false";
                                 htmlretun = ViewHelper.RenderRazorViewToString("_ListOfUser", homlist.PatientModel, this);
                             }
                          
                         }
                
                   }
                 
                 
                 
                 }
             
             catch (Exception ex)
             {

                 return Json(ex.Message);
             }
             string html = string.Empty;
             string patientid = string.Empty;
             html = question;
             return Json(new { html = html, flag = flag, userid = userid, htmlretun = htmlretun, questionTwo = questionTwo });

            
        }

        [HttpPost]
        public JsonResult SecurityQuestionProvider(UserModel Model)
        {
          /*  DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

            int LoginType = 6;
            string question = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


            try
            {

                ResponseData = AuthenticationWS.GetSecurityQuestion(0, Model.UserLogin, LoginType,Model.UserEmail);

                if (ResponseData.Valid)
                {
                    question = ResponseData.SecurityQuestion;

                }
                else
                {
                    return Json(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = question;
            return Json(html);*/
            HomeViewModel homlist = new HomeViewModel();
            DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();
            DataProviderService.ProviderWSSoap service = new DataProviderService.ProviderWSSoapClient();
            int LoginType = 6;
            bool flag = false;
            string userid = "";
            string htmlretun = string.Empty;
            string useremail = Model.UserEmail;
            string question = String.Empty;
            string questionTwo = String.Empty;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            DataProviderService.ProviderTableData patientdt = new DataProviderService.ProviderTableData();
            DataProviderService.ProviderData patdata = new DataProviderService.ProviderData();

            try
            {
                if (Model.UserLogin != null)
                {
                    if (Model.UserEmail == "" || Model.UserEmail == null)
                    {
                        ResponseData = AuthenticationWS.GetSecurityQuestion(0, Model.UserLogin, LoginType, Model.UserEmail);

                        if (ResponseData.Valid)
                        {
                            question = ResponseData.SecurityQuestion;
                            questionTwo = ResponseData.SecurityQuestion2;
                            userid = Model.UserLogin;
                            flag = true;
                        }
                        else
                        {
                            return Json(ResponseData.ErrorMessage);
                        }
                    }
                    else
                    {

                        ResponseData = AuthenticationWS.GetSecurityQuestion(0, Model.UserLogin, LoginType, Model.UserEmail);

                        if (ResponseData.Valid)
                        {
                            question = ResponseData.SecurityQuestion;
                            questionTwo = ResponseData.SecurityQuestion2;
                            userid = Model.UserLogin;
                            flag = true;
                        }
                        else
                        {
                            return Json(ResponseData.ErrorMessage);
                        }

                    }
                }
                if (Model.UserEmail != null && Model.UserLogin == null)
                {


                    patientdt = service.ProviderEmailSearch(useremail, "40000000728000000415", 7, 0);
                    if (patientdt.Valid)
                    {
                        homlist.ProviderModel = patientdt.dt.ToProviderModel();
                        if (patientdt.dt.Rows.Count > 0 && patientdt.dt.Rows.Count < 2)
                        {
                            flag = true;
                            userid = patientdt.dt.Rows[0][0].ToString();
                            ResponseData = AuthenticationWS.GetSecurityQuestion(0, userid, LoginType, Model.UserEmail);
                            if (ResponseData.Valid)
                            {
                                question = ResponseData.SecurityQuestion;
                                questionTwo = ResponseData.SecurityQuestion2;
                                flag = true;
                            }
                            else
                            {
                                return Json(ResponseData.ErrorMessage);
                            }

                        }
                        else
                        {
                            flag = false;
                            question = "false";
                            questionTwo = "false";
                            htmlretun = ViewHelper.RenderRazorViewToString("_ListOfUser", homlist.ProviderModel, this);
                        }

                    }

                }



            }

            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;
            string patientid = string.Empty;
            html = question;
            return Json(new { html = html, flag = flag, userid = userid, htmlretun = htmlretun, questionTwo = questionTwo });

            


        }
        public ActionResult ForgotPassword()
        {

            HomeViewModel HomeList = new HomeViewModel();

            return View(HomeList);
        }

        public ActionResult LicenseAgreement()
        {

            HomeViewModel HomeList = new HomeViewModel();

            return View(HomeList);
        }

        public ActionResult DownloadAMRApp()
        {

            HomeViewModel HomeList = new HomeViewModel();

            return View(HomeList);
        }

        [HttpPost]
        public JsonResult SocialHistorySelfSave(PatientSocialSelfDataModel Model)
        {
            HomeViewModel Homelist = new HomeViewModel();
            DataPatientDocumentService.PatientDocParms patdata = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientSocialSelfData patsochisdata = new DataPatientDocumentService.PatientSocialSelfData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            patsochisdata.Birthplace = Model.Birthplace;
            patsochisdata.Occupation = Model.Occupation;
            patsochisdata.Retired = Model.Retired;
            patsochisdata.EducationLevelId = Model.EducationLevelId;
            patsochisdata.ChildrenSon = Model.ChildrenSon;
            patsochisdata.ChildrenDaughter = Model.ChildrenDaughter;
            patsochisdata.CaffeineUser = Model.CaffeineUser;
            patsochisdata.CaffieneType = Model.CaffieneType;
            patsochisdata.CaffeineAmount = Model.CaffeineAmount;
            patsochisdata.ExerciseFrequencyId = Model.ExerciseFrequencyId;
            patsochisdata.ExerciseAmount = Model.ExerciseAmount;
            patsochisdata.ExerciseMember = Model.ExerciseMember;
            patsochisdata.ActivityLevelId = Model.ActivityLevelId;
            patsochisdata.Activity1 = Model.Activity1;
            patsochisdata.Activity2 = Model.Activity2;
            patsochisdata.Activity3 = Model.Activity3;
            patsochisdata.AlcoholUser = Model.AlcoholUser;
            patsochisdata.AlcoholFrequencyId = Model.AlcoholFrequencyId;
            patsochisdata.AlcoholLastUse = Model.AlcoholLastUse;
            patsochisdata.AlcoholType = Model.AlcoholType;
            patsochisdata.AlcoholStartAge = Model.AlcoholStartAge;
            patsochisdata.AlcoholFamilyHist = Model.AlcoholFamilyHist;
            patsochisdata.SmokingStatusId = Model.SmokingStatusId;
            patsochisdata.SmokingDailyAmount = Model.SmokingDailyAmount;
            patsochisdata.SmokingType = Model.SmokingType;
            patsochisdata.SmokingYears = Model.SmokingYears;
            patsochisdata.SmokingQuitAttempts = Model.SmokingQuitAttempts;
            patsochisdata.SmokingQuitDate = Model.SmokingQuitDate;
            patsochisdata.SmokingSecondHand = Model.SmokingSecondHand;
            patsochisdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
            
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    patsochisdata = service.SavePatientSocialSelfData(patsochisdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (patsochisdata.Valid)
                    {
                        patdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        patsochisdata = service.GetPatientSocialSelfData(patdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (patsochisdata.Valid)
                        {
                            Homelist.PatientSocialSelf = base.ConvertToPatientSocialSelfDataModel(patsochisdata);
                        }
                  
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_SocailHistorySelf", Homelist.PatientSocialSelf, this);

            return Json(new { html=html});
        }

        public ActionResult SocialHistorySelf()
        { 
           HomeViewModel HomeList = new HomeViewModel();
           DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
         using(var service = new DataPatientConfigService.ConfigWSSoapClient())
         {
             ConfigDocument = service.GetExerciseFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             if (ConfigDocument.Valid)
             {

                 HomeList.frequency = ConfigDocument.dt.ToExerciseFrequencyModelList();
                 HomeList.frequency.Insert(0, new ExerciseFrequencyModel { ExerciseFrequencyId = -1, Value = "--Select--" });
             }
             ConfigDocument = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             if (ConfigDocument.Valid)
             {
                 HomeList.smokingstatus = ConfigDocument.dt.ToSmokingStatusModelList();
                 HomeList.smokingstatus.Insert(0, new SmokingStatusModel { SmokingStatusId = -1, Value = "--Select--" });

             }

             ConfigDocument = service.GetActivityLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             
             if(ConfigDocument.Valid)
             {
                 HomeList.ActivityLevel = ConfigDocument.dt.ToActivityLevelModelList();
                 HomeList.ActivityLevel.Insert(0, new ActivityLevelModel { ActivityLevelId = -1, Value = "--Select--" });

             }

             ConfigDocument = service.GetAlcoholFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             if (ConfigDocument.Valid)
             {
                 HomeList.AlcoholFrequency = ConfigDocument.dt.ToAlcoholFrequencyModelList();
                 HomeList.AlcoholFrequency.Insert(0, new AlcoholFrequencyModel { AlcoholFrequencyId = -1, Value = "--Select--" });

             }
             ConfigDocument = service.GetEducationLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             if (ConfigDocument.Valid)
             {
                 HomeList.Educationlevel = ConfigDocument.dt.ToEducationLevelModelList();
                 HomeList.Educationlevel.Insert(0, new EducationLevelModel { EducationLevelId = -1, Value = "--Select--" });

             }
             //ConfigDocument = service.GetMaritalStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             //if (ConfigDocument.Valid)
             //{
             //    HomeList.MartialStatus = ConfigDocument.dt.ToMartialStatusModelList();
             //}
             //  HomeList.smokingstatus = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
             // HomeList.AlcoholFrequency = service.GetAlcoholFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

         }  

           return PartialView("SocialHistorySelf", HomeList);
        
        }
        public ActionResult ForgotPasswordProvider()
        {

            HomeViewModel HomeList = new HomeViewModel();

            return View(HomeList);
        }
        public ActionResult SignUp()
        {

            HomeViewModel HomeList = new HomeViewModel();
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                //DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                //ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (ConfigDocument.Valid)
                //{
                //    HomeList.Country = ConfigDocument.dt.ToCountryModelList();
                //}
                //ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (ConfigDocument.Valid)
                //{
                //    HomeList.States = ConfigDocument.dt.ToStatesModelList();
                //}
            }
            return View(HomeList);
        }

     
        public ActionResult Terms()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PatientRegister(PatientModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            DataPatientService.BillRateData billratedata = new DataPatientService.BillRateData();
            DataPatientService.BillPaymentData billpayinfo = new DataPatientService.BillPaymentData();
            DataPatientService.PatientData PatDat = new DataPatientService.PatientData();
            bool status=false;
            var FullName = string.Empty;
            try
            {

                  DataPatientService.PatientData pSumm = base.ConvertToPatientData(Model);
                  DataPatientService.BillRateParms RateRarms = new DataPatientService.BillRateParms();
                  DataPatientService.BillPaymentParms payparms = new DataPatientService.BillPaymentParms();
                  DataPatientService.BillPaymentData paydata = new DataPatientService.BillPaymentData();
                  DataPatientService.BillPaymentParms payparm = new DataPatientService.BillPaymentParms();
                  RateRarms.PromoCode = Model.PromoCode;
                
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    //Add token and userid for testing
                   PatDat = service.SavePatientData(pSumm, "", 0, 0);
                   FullName = PatDat.FirstName + " " + PatDat.LastName;
                    if (PatDat.Valid)
                    {
                        
                      
                        if(RateRarms.PromoCode=="0000")
                        {
                         status=true;
                        }

                        else 
                        {
                            RateRarms.PromoCode = "Test";
                            billratedata = service.GetBillRateDataByPromo(RateRarms, "40000000728000000415", 7, 0);
                            if (billratedata.Valid)
                            {
                                
                                Session.Add("billrateid", billratedata.BillRateId);

                            
                            
                        }
                        }

                      
                       
                    }
                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;
           

            html = "User Is created";

            return Json(new { billrateid = billratedata.BillRateId, html = FullName, billrenewal = billratedata.Renewal, billrenewal1 = billratedata.Rate, RecurringAmount = billratedata.Renewal, status = status, add = PatDat.Address1, add2 = PatDat.Address2, city = PatDat.City, State = PatDat.State, Zip = PatDat.Zip, phone = PatDat.HomePhone, Email = PatDat.EMail, WalletDescription = billratedata.BillRateId, patientid = PatDat.PatientId });
          //  return Json(html);
           
        }
        [HttpPost]
        public JsonResult ProviderRegister(ProviderModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {

                DataProviderService.ProviderData pSumm = base.ConvertToProviderData(Model);


                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    //Add token and userid for testing
                    DataProviderService.ProviderResponse ProDat = service.SaveProviderData(pSumm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                }

            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = "User Is created";
            return Json(html);

        }

        public FileContentResult GetPatientImg(int id)
        {
            DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            if (Request.Cookies["MedicalSummary"] != null)
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                    PatientImage = service.GetPatientImageData(PatParms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                }
            }
            else
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatientImage = service.GetPatientImageData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                }
            }
             //= DbContext.Persons.Find(id).Image;
            if (PatientImage.Image != null)
            {
               // if(PatientImage.ImageFormat == "jpeg")
               // {
                return new FileContentResult(PatientImage.Image, "image/jpeg");
               // }
               // else if (PatientImage.ImageFormat == "png")
               // {
               //     return new FileContentResult(PatientImage.Image, "image/png");
               // }
            }
            else
            {
                string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                return new FileContentResult(bytes, "image/png");
            }
        }

        public FileContentResult GetProviderImg(int id)
        {
            DataProviderService.ProviderImageData ProviderImage = new DataProviderService.ProviderImageData();
            DataProviderService.ProviderParms ProParams = new DataProviderService.ProviderParms();
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                ProParams.ProviderId = RequestHelper.MyGlobalVar.PatientId;
                ProviderImage = service.GetProviderImageData(ProParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            }
            if (ProviderImage.Image != null)
            {
                return new FileContentResult(ProviderImage.Image, "image/jpeg");
            }
            else
            {
                string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                return new FileContentResult(bytes, "image/png");
            }
            
        }

        [HttpPost]
        public ActionResult ProviderImageSave()
        {

          

            HttpPostedFileBase myFile = Request.Files["MyFile"];
            bool isUploaded = false;
            string message = "File uploaded";
            byte[] Image = null;
            using (var binaryReader = new BinaryReader(Request.Files["MyFile"].InputStream))
            {
                Image = binaryReader.ReadBytes(Request.Files["MyFile"].ContentLength);
            }
            string ImageFormat = System.IO.Path.GetExtension(myFile.FileName);
            var newformat = ImageFormat.Substring(1, ImageFormat.Length - 1);


            DataProviderService.ProviderImageData ProviderImage = new DataProviderService.ProviderImageData();
            DataProviderService.ProviderParms ProParams = new DataProviderService.ProviderParms();
        
            ProviderImage.Image = Image;
            ProviderImage.ImageFormat = ImageFormat;
            ProviderImage.ProviderId = RequestHelper.MyGlobalVar.PatientId;
           

            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                ProviderImage = service.SaveProvidermageData(ProviderImage, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                ProParams.ProviderId = RequestHelper.MyGlobalVar.PatientId;
                ProviderImage = service.GetProviderImageData(ProParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            }

            string imageBase64 = Convert.ToBase64String(ProviderImage.Image);
            string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);

            string Imghtml = "<img src=\"" + imageSrc + "\" id=\"PatientImage\" title=\"Upload Pateint Image\" style=\"cursor:pointer;  border: 1px solid rgb(166, 201, 226); -moz-border-radius: 5px; width: 90%; -webkit-border-radius: 5px; border-radius: 5px; margin-bottom: 5px;\"  onclick=\"imageUpload();\"/>";
       
            return Json(new { Imghtml = Imghtml, message = message }, "text/html");

        }

        [HttpPost]
        public ActionResult PatientImageSave()
        {
           
            //object Image
            //var bytes = System.Text.Encoding.UTF8.GetBytes(Image);
            //PatientImageData objImage = new PatientImageData();
            //objImage.Image = bytes;
            //objImage.PatientId = RequestHelper.MyGlobalVar.PatientId;

            HttpPostedFileBase myFile = Request.Files["MyFile"];
            bool isUploaded = false;
            string message = "File uploaded";
            byte[] Image = null;
            using (var binaryReader = new BinaryReader(Request.Files["MyFile"].InputStream))
            {
                Image = binaryReader.ReadBytes(Request.Files["MyFile"].ContentLength);
            }
            string ImageFormat = System.IO.Path.GetExtension(myFile.FileName);
            var newformat = ImageFormat.Substring(1, ImageFormat.Length-1);

            DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();

            PatientImage.Image = Image;
            PatientImage.ImageFormat = ImageFormat;
            PatientImage.PatientId = RequestHelper.MyGlobalVar.PatientId;

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                PatientImage = service.SavePatientmageData(PatientImage, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                PatientImage = service.GetPatientImageData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                
            }
            string imageBase64 = Convert.ToBase64String(PatientImage.Image);
            string imageSrc = string.Format("data:image/jpeg;base64,{0}", imageBase64);

            string Imghtml = "<img src=\"" + imageSrc + "\" id=\"PatientImage\" title=\"Upload Pateint Image\" style=\"cursor:pointer;  border: 1px solid rgb(166, 201, 226); -moz-border-radius: 5px; width: 90%; -webkit-border-radius: 5px; border-radius: 5px; margin-bottom: 5px;\"  onclick=\"imageUpload();\"/>";
            //if (myFile != null && myFile.ContentLength != 0)
            //{
            //    string pathForSaving = Server.MapPath("~/Uploads");
            //    if (this.CreateFolderIfNeeded(pathForSaving))
            //    {
            //        try
            //        {
            //            myFile.SaveAs(Path.Combine(pathForSaving, myFile.FileName));
            //            isUploaded = true;
            //            message = "File uploaded successfully!";
            //        }
            //        catch (Exception ex)
            //        {
            //            message = string.Format("File upload failed: {0}", ex.Message);
            //        }
            //    }
            //}
            Session["PatDisp"] = System.Convert.ToBase64String(PatientImage.Image);
            return Json(new { Imghtml = Imghtml, message = message }, "text/html");

        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        [HttpPost]
        public JsonResult FilterMedicationWidgData(PatientMedicationModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        ClinicalSummaryLists.Medications = PatientDocument.dt.ToMedicationHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Medications.GetPatientMedicationModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Medication", ClinicalSummaryLists.Medications, this);
            return Json(html);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult AllergyWidget(AllergyModel Model)
        {
            List<AllergyModel> allergymodel = new List<AllergyModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Allergy = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    allergymodel = PatientDocumentSummary.dtAllergy.ToAllergyModelList();
                }
               
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Allergies", allergymodel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult PlanOfCareWidgetData(POCModel Model)

        {
            List<POCModel> PocModel = new List<POCModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.PlanOfCare = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    PocModel = PatientDocumentSummary.dtPlanOfCare.ToPOCModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_PlanofCare", PocModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult InsuranceWidgetData(PatientInsuranceModel Model)
        {
            List<PatientInsuranceModel> InsurancePolicyModel = new List<PatientInsuranceModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Insurance = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    InsurancePolicyModel = PatientDocumentSummary.dtInsurance.ToInsuranceModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Insurance", InsurancePolicyModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult VisitsWidgetData(VisitModel Model)
        {
            List<VisitModel> VisitModel = new List<VisitModel>();
            //FacilityVisitSelectModel smodel;
            //smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
            //if (smodel == null)
            //{
            //    //smodel.
            //    smodel = new FacilityVisitSelectModel
            //    {
            //        facilitySelected = RequestHelper.MyGlobalVar.FacilitySelectId.ToString(),
            //        visitSelected = RequestHelper.MyGlobalVar.VisitId.ToString(),
            //    };
            //    Session["OptionSelected"] = smodel;
            //    //smodel.facilitySelected = "0";
            //    //smodel.visitSelected = "0";
            //}
          
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
           parms.FacilityId = Model.FacilityId;
            //if (smodel != null)
            //{
            //    parms.VisitId = Model.VisitID;
            //  //  parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);

            //}
           parms.VisitId = Model.VisitID;
            parms.Visit = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    VisitModel = PatientDocumentSummary.dtVisit.ToVisitModelList();

                }
                string html = string.Empty;
                //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                html = ViewHelper.RenderRazorViewToString("_Index_Visits", VisitModel, this);
                return Json(html);
            }
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult ProblemWidgetData(ProblemModel Model)
        {
            List<ProblemModel> ProblemModel = new List<ProblemModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Problem = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    ProblemModel = PatientDocumentSummary.dtProblem.ToProblemModelList();

                }
                string html = string.Empty;
                //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                html = ViewHelper.RenderRazorViewToString("_Index_Problems", ProblemModel, this);
                return Json(html);
            }
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MedicationWidgetData(PatientMedicationModel Model)
        {
            List<PatientMedicationModel> MedicationModel = new List<PatientMedicationModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Medication = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    MedicationModel = PatientDocumentSummary.dtMedication.ToMedicationHistoryModelList();

                }
                string html = string.Empty;
                //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                html = ViewHelper.RenderRazorViewToString("_Index_Medication", MedicationModel, this);
                return Json(html);
            }
        
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult ImmunizationWidgetData(ImmunizationModel Model)
        {
            string html = string.Empty;
            try
            {
                List<ImmunizationModel> ImmunizationModel = new List<ImmunizationModel>();
                DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
                DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                parms.FacilityId = Model.FacilityId;
                parms.VisitId = Model.VisitId;
                parms.Immunization = true;
                parms.Option = 1;
                parms.Active = 1;
                parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocumentSummary.Valid)
                    {

                        ImmunizationModel = PatientDocumentSummary.dtImmunization.ToImmunizationModelList();
                    }
                    //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                    html = ViewHelper.RenderRazorViewToString("_Index_Immunization", ImmunizationModel, this);

                }
                
            }
            catch (Exception)
            {
               //html = "/Login";
                //throw;
            }

            return Json(html);
        
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult FamilyWidgetData(FamilyHistoryModel Model)
        {

            List<FamilyHistoryModel> FamilyHistoryModel = new List<FamilyHistoryModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.FamilyHist = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    FamilyHistoryModel = PatientDocumentSummary.dtFamilyHist.ToFamilyHistoryModelList();
                }
                string html = string.Empty;
                //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                html = ViewHelper.RenderRazorViewToString("_Index_FamilyHistory", FamilyHistoryModel, this);
                return Json(html);
            }
        
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult ProviderWidgetData(ProviderInfoModel Model)
        {
            List<ProviderInfoModel> ProviderModel = new List<ProviderInfoModel>();
            //DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            //DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            //parms.FacilityId = Model.FacilityID;
            //parms.VisitId = Model.VisitId;
            //parms.FamilyHist = true;
            //parms.Option = 1;
            //parms.Active = 1;
            //parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {

                DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
                //   RequestHelper.MyGlobalVar.FacilityId
                ProviderList = service.GetPatientProviderInfoList(RequestHelper.MyGlobalVar.PatientId, Model.FacilityID, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                ProviderModel = ProviderList.dt.ToProivderInfoList();
            
           
                string html = string.Empty;
                //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
                html = ViewHelper.RenderRazorViewToString("_Index_Provider", ProviderModel, this);
                return Json(html);
            }
        }
        [HttpPost]
        public JsonResult AppointmentWidgetData(PatientMessageModel Model)
        {
            List<PatientMessageModel> AppointmentModel = new List<PatientMessageModel>();
     
          
              using (var service = new DataMessageService.MessageWSSoapClient())
            {
                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                   MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;

                MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                    AppointmentModel = MessageData.dt.ToMessageModelList();
                }
            
            }
              string html = string.Empty;
              //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
              html = ViewHelper.RenderRazorViewToString("_Index_Appointment", AppointmentModel, this);
              return Json(html);
        
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult VitalWidgetData(PatientVitalSignModel Model)
        {

            List<PatientVitalSignModel> VitalSignModel = new List<PatientVitalSignModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.VitalSign = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    VitalSignModel = PatientDocumentSummary.dtVitalSign.ToVitalsignModelList();
                }
             
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_VitalSign", VitalSignModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult PastMedicationWidgetData(MedicalHistoryModel Model)
        {
            List<MedicalHistoryModel> MedicalHistoryModel = new List<MedicalHistoryModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.MedicalHist = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    MedicalHistoryModel = PatientDocumentSummary.dtMedicalHist.ToMedicalHistoryModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_MedicalHistory", MedicalHistoryModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult DocumentWidgetData(PatientMedicalDocumentModel Model)
        {
            FacilityVisitSelectModel smodel;
            smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
            if (smodel == null)
            {
                //smodel.
                smodel = new FacilityVisitSelectModel
                {
                    facilitySelected = RequestHelper.MyGlobalVar.FacilitySelectId.ToString(),
                    visitSelected = RequestHelper.MyGlobalVar.VisitId.ToString(),
                };
                Session["OptionSelected"] = smodel;
                //smodel.facilitySelected = "0";
                //smodel.visitSelected = "0";
            }
            List<PatientMedicalDocumentModel> MedicalDocument = new List<PatientMedicalDocumentModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
         if (smodel != null)
                {
                    parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                    parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                   
                }
             parms.ClinicalDocs = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    MedicalDocument =PatientDocumentSummary.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Documents", MedicalDocument, this);
            return Json(html);
        
        }

        [HttpPost]
        public JsonResult ClinicalInstructionWidgetData(POCModel Model)
        {

            List<POCModel> ClinicalModel = new List<POCModel>();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                PatDocParms.VisitId = Model.VisitId;
                PatDocParms.FacilityId = Model.FacilityId;
                PatDocParms.Option = 1;
                PatDocParms.Active = 1;
              
                PatientDocument = service.GetPatientClinicalInstructionsData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {

                    ClinicalModel = PatientDocument.dt.ToPOCModelList();
                    // ClinicalSummaryLists.ClinicalInstructions = PatientDocument.dt.ToPOCModelList();

                }
            }

            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_ClinicalInstructions", ClinicalModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult LabWidgetData( LabResultModel Model)
        {
            List<LabResultModel> LabResultModel = new List<LabResultModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Lab = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    LabResultModel = PatientDocumentSummary.dtLab.ToLabModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_LabTests", LabResultModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult ProcedureWidgetData(ProcedureModel Model)
        {

            List<ProcedureModel> ProcedureModel = new List<ProcedureModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.Procedure = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    ProcedureModel = PatientDocumentSummary.dtProcedure.ToProcedureModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Procedures", ProcedureModel, this);
            return Json(html);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MySocialHistoryWidgetData(PatientSocialSelfDataModel Model)
        {
            Model = new PatientSocialSelfDataModel();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocParms patdata = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientSocialSelfData socialselfdata = new DataPatientDocumentService.PatientSocialSelfData();
                DataPatientDocumentService.PatientDocTableData patdoctable = new DataPatientDocumentService.PatientDocTableData();
                patdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
                socialselfdata = service.GetPatientSocialSelfData(patdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (socialselfdata.Valid)
                {
                    Model = base.ConvertToPatientSocialSelfDataModel(socialselfdata);
                }


            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_SocailHistorySelf", Model, this);
            return Json(html);
        }
        public JsonResult FilterImmunizationWidgData(ImmunizationModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientImmunizationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.Immunizations = PatientDocument.dt.ToPatientImmunizationModelList();


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Immunizations.GetPatientImmunizationModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Immunization", ClinicalSummaryLists.Immunizations, this);
            return Json(html);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult SocailWidgetData( SocialHistoryModel Model)
        {
            List<SocialHistoryModel> SocialModel = new List<SocialHistoryModel>();
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
            DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            parms.FacilityId = Model.FacilityId;
            parms.VisitId = Model.VisitId;
            parms.SocialHist = true;
            parms.Option = 1;
            parms.Active = 1;
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {

                    SocialModel = PatientDocumentSummary.dtSocialHist.ToSocialHistoryModelList();
                }

            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_SocialHistory", SocialModel, this);
            return Json(html);
        }
        [HttpPost]
        public JsonResult FilterVisitWidgData(VisitModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();


            PatParms.FacilityId = Model.FacilityId;
            PatParms.VisitId = Model.VisitID;
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatParms.Option = 1;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatientDocument = service.GetPatientVisitList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {
                        ClinicalSummaryLists.Visits = PatientDocument.dt.ToVisitModelList();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Visits.GetPatientVisitModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Visits", ClinicalSummaryLists.Visits, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterAllergyWidgData(AllergyModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientAllergyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.Allergies = PatientDocument.dt.ToPatientAllergyModelList();


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Allergies", ClinicalSummaryLists.Allergies, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterLabWidgData(LabResultModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientLabResultParms PatDocParms = new DataPatientDocumentService.PatientLabResultParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientLabResultList(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.LabResults = PatientDocument.dt.ToPatientLabResultModelList();


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.LabResults.GetPatienLabResultModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_LabTests", ClinicalSummaryLists.LabResults, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterProblemWidgData(ProblemModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientProblemData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.Problems = PatientDocument.dt.ToPatientProblemModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Problems.GetPatientProblemModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Problems", ClinicalSummaryLists.Problems, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterVitalWidgData(PatientVitalSignModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientVitalSignData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.VitalSigns = PatientDocument.dt.ToPatientVitalSignModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.VitalSigns.GetPatientVitalSignModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_VitalSign", ClinicalSummaryLists.VitalSigns, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterSocialWidgData(SocialHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientSocialHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.SocialHistories = PatientDocument.dt.ToSocialHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.SocialHistories.GetPatientSocialHistoryModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_SocialHistory", ClinicalSummaryLists.SocialHistories, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterFamilyWidgData(FamilyHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientFamilyHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.FamilyHistories = PatientDocument.dt.ToFamilyHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.FamilyHistories.GetPatientFamilyHistoryModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_FamilyHistory", ClinicalSummaryLists.FamilyHistories, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterPastWidgData(MedicalHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientMedicalHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.MedicalHistories = PatientDocument.dt.ToPatientMedicalHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_MedicalHistory", ClinicalSummaryLists.MedicalHistories, this);
            return Json(html);
        }


        public ActionResult AnotherSession()
        {
            return View();
        }


        //ending action of chart summary tab
        public ActionResult Login()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            string access = Request.QueryString["access"];
            if(authCookie != null)
            {
                if (!String.IsNullOrEmpty(access))
                {
                    return View();

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                    
                }
            }
            return View();
        }
        public ActionResult Allergies()
        {
            HomeViewModel homelist = new HomeViewModel();
            List<ConditionStatusModel> conditionstatus = new List<ConditionStatusModel>();
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;
                ConfigDocument = service.GetConditionStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    conditionstatus = ConfigDocument.dt.ToConditionStatusModelList();
                    conditionstatus.Insert(0, new ConditionStatusModel { ConditionStatusId = -1, Value = "--Select--" });
                }

            }
            return View(conditionstatus);
        }
        public ActionResult ProviderLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProviderLogin(UserModel model)
        {
            //HomeViewModel HomeLists = new HomeViewModel();
            //string usrlogin = clsEncryption.Dec(Request.QueryString["id"].ToString());
            //string usrdata = clsEncryption.Dec(Request.QueryString["value1"].ToString());

            //TicketHelper.CreateAuthCookie(usrlogin, usrdata, true);
            ////base.PopulateProvider();


            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();


            objGlobalVar.FacilityId = 0;
            objGlobalVar.UserLogin = model.UserLogin;
           

           // Session["ProviderFlag"] = false;

            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {

                    ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, objGlobalVar.UserLogin, model.Password);

                    if (ResponseData.Valid)
                    {
                      //  Session["ProviderFlag"] = ResponseData.FirstLogin;
                        objGlobalVar.UserId = ResponseData.UserId;
                        objGlobalVar.Token = ResponseData.Token;
                        objGlobalVar.UserLogin = ResponseData.UserRoleLink.ToString();
                        objGlobalVar.FirstLogin = ResponseData.FirstLogin;
                        objGlobalVar.ResetPasswordFlag = ResponseData.ResetPassword;
                        if (ResponseData.LoginType == 5)
                        {
                            objGlobalVar.PortalType = "Patient";
                        }
                        else if (ResponseData.LoginType == 4)
                        {
                            objGlobalVar.PortalType = "Provider";
                        }

                        //              Session.Add("UserId", ResponseData.UserId);


                        //if (ResponseData.LoginType == 4 && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                        //{// for provider portal
                        //    string usrlogin = clsEncryption.Enc(objGlobalVar.UserLogin);
                        //    string usrdata = clsEncryption.Enc(objGlobalVar.GetData());
                        //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["RedirectToProviderHome"] + usrlogin + "&&value1=" + usrdata);

           
                        //}
                        //else
                        //{ // for patient portal
                        //if (ResponseData.FirstLogin)
                        //{
                        if (ResponseData.LoginType == 4 && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                        {
                            TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
           return RedirectToAction("MsgIndex", "Message");
        }
                        else
                        {
                            objGlobalVar.Token = "";
                            objGlobalVar.UserId = 0;
                            model.ErrorMessage = "Invalid Login for Provider portal";
                        }
                        //return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });

                        //}
                        //else
                        //{
                        //  TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                        //  return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });
                        // }
                        //     }


                    }
                    else
                    {
                        objGlobalVar.Token = "";
                        objGlobalVar.UserId = 0;
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
        [HttpPost]
        public ActionResult Confirmation()
        {
            
           BillPaymentDataModel billRecord = new BillPaymentDataModel();
            DataPatientService.BillPaymentData billpay = new DataPatientService.BillPaymentData(); 
            DataPatientService.BillPaymentParms payparam = new DataPatientService.BillPaymentParms();
            DataPatientService.PatientResp patrespdata = new DataPatientService.PatientResp();
            Int64 patientid = Convert.ToInt64(Request.Params["CustomerReferenceID"]);

            billpay.PatientId = patientid; 
            billpay.BillRateId = Convert.ToInt32(Session["billrateid"]);
            billpay.TransactionDate = System.DateTime.Now.ToString();
            billpay.Response = Request.Params["rspCode"].ToString();
            billpay.CustId=Request.Params["CustID"].ToString();
            billpay.RecurrId=Request.Params["RecurrID"].ToString();
            billpay.PaymentType= Request.Params["PmtType"];
            billpay.NoOfPayments=Convert.ToInt32( Request.Params["NumberOfPayments"]);
            billpay.StartDate= Request.Params["StartDate"];
            billpay.AccountHolderName= Request.Params["AccountHoldername"].ToString();
            billpay.Amount= Convert.ToInt64(Request.Params["Amount"]);
            Session.Remove("billrateid");


            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    billpay = service.SaveBillPaymentData(billpay, "40000000728000000415", 7, 0);
                    if (billpay.Response =="00")
                    {
                       patrespdata = service.ActivatePatient(patientid, "40000000728000000415", 7, 0);
                        patrespdata = service.UpgradePatient(patientid, "40000000728000000415", 7, 0);
                        billRecord.status = true;

                    }
                    else
                    {

                        billRecord.status = false;

                    }
                }
            }
             catch (Exception ex)
                {
                    billRecord.status = false;
                    billRecord.ErrorMessage = ex.Message;

                }

            return View(billRecord);
        
        }

        [HttpPost]
      
        public ActionResult PremiumPay()
        {
            HomeViewModel home = new HomeViewModel();

            BillPaymentDataModel billRecord = new BillPaymentDataModel();
            DataPatientService.BillPaymentData billpay = new DataPatientService.BillPaymentData();
            DataPatientService.BillPaymentParms payparam = new DataPatientService.BillPaymentParms();
            DataPatientService.PatientResp patrespdata = new DataPatientService.PatientResp();
            Int64 patientid = Convert.ToInt64(Request.Params["CustomerReferenceID"]);
            AMR.Controllers.Controllers.Base.BaseController bc2 = new AMR.Controllers.Controllers.Base.BaseController();
            billpay.PatientId = patientid;
            billpay.BillRateId = Convert.ToInt32(Session["billrate"]);
            billpay.TransactionDate = System.DateTime.Now.ToString();
            billpay.Response = Request.Params["rspCode"].ToString();
            billpay.CustId = Request.Params["CustID"].ToString();
            billpay.RecurrId = Request.Params["RecurrID"].ToString();
            billpay.PaymentType = Request.Params["PmtType"];
            billpay.NoOfPayments = Convert.ToInt32(Request.Params["NumberOfPayments"]);
            billpay.StartDate = Request.Params["StartDate"];
            billpay.AccountHolderName = Request.Params["AccountHoldername"].ToString();
            billpay.Amount = Convert.ToInt64(Request.Params["Amount"]);
            Session.Remove("billrate");
            bool abc = RequestHelper.MyGlobalVar.PremiumFlag;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    billpay = service.SaveBillPaymentData(billpay, "40000000728000000415", 7, 0);
                    if (billpay.Response == "00")
                    {
                        patrespdata = service.ActivatePatient(patientid, "40000000728000000415", 7, 0);
                        patrespdata = service.UpgradePatient(patientid, "40000000728000000415", 7, 0);
                        billRecord.status = true;

                        if (patrespdata.Valid)
                        {
                            base.PopulatePremiumPatient(true);
                        }
                        

                    }
                    else
                    {
                        billRecord.status = false;

                       

                    }
                   

                }
            }
            catch (Exception ex)
            {
                billRecord.status = false;
                billRecord.ErrorMessage = ex.Message;


            }

          // return RedirectToAction("Index", "Home");
            return View(billRecord);
        }

        public ActionResult AccountLinkChage(string id)
        {
            bool val = base.PopulateAccountLinkPatient(id);
            if (val)
            {
              return  RedirectToAction("Index", "Home");
            }
            return null;
        }

       //[OutputCache(Duration = 3600)]
        public ActionResult PaymentTransfer()
        {
            HomeViewModel home= new HomeViewModel();
            BillRateDataModel billrate = new BillRateDataModel();
            PatientModel patient = new PatientModel();
           DataPatientService.PatientData patdate=new DataPatientService.PatientData();
           DataPatientService.PatientParms patpara=new DataPatientService.PatientParms();
           DataPatientService.BillRateData billdata = new DataPatientService.BillRateData();
           DataPatientService.BillRateParms billparm = new DataPatientService.BillRateParms();
           patpara.PatientId=RequestHelper.MyGlobalVar.PatientId;
           billparm.PromoCode = "TEST";
           //get patient data according to patiend id
           try
           {
               using (var service = new DataPatientService.PatientWSSoapClient())
               {

                   patdate = service.GetPatientData(patpara, "40000000728000000415", 7, 0);
                   if (patdate.Valid)
                   {
                       patient.FirstName = patdate.FirstName;
                       patient.LastName = patdate.LastName;
                       patient.Address1 = patdate.Address1;
                       patient.Address2 = patdate.Address2;
                       patient.City = patdate.City;
                       patient.State = patdate.State;
                       patient.Zip = patdate.Zip;
                       patient.HomePhone = patdate.HomePhone;
                       patient.EMail = patdate.EMail;
                   }
  
                    home.Patient=patient;

               }
               using (var service = new DataPatientService.PatientWSSoapClient())
               {

                   billdata = service.GetBillRateDataByPromo(billparm, "40000000728000000415", 7, 0);
                   if (billdata.Valid)
                   {
                       billrate.BillRateId = billdata.BillRateId;
                       billrate.Rate = Convert.ToDecimal(billdata.Rate);
                       billrate.Renewal =Convert.ToDecimal(billdata.Renewal);
                       billrate.Valid = true;
                       Session.Add("billrate", billdata.BillRateId);

                   
                   }

                   home.Billrate = billrate;
               }
           }
   
                catch (Exception ex)
                {
              

                    
                }
           return View(home);
           
        }
        
         
        [HttpPost]
        public ActionResult Login(UserModel model, string returnUrl)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            
            HomeViewModel HomeLists = new HomeViewModel();
            PatientRepModel PatientRepresentatives = new PatientRepModel();
            GlobalVar objGlobalVar = new GlobalVar();
            

            objGlobalVar.FacilityId = 0;
            objGlobalVar.UserLogin = model.UserLogin;
            if(model.UserLogin.StartsWith("R"))
            { PatientRepresentatives.loginFlag = true;}
            else { PatientRepresentatives.loginFlag = false; }
            Session["loginRepFlag"] = PatientRepresentatives.loginFlag;
             DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

             try
             {
                 if (authCookie != null)
                 {
                     if (model.UserLogin == RequestHelper.MyGlobalVar.UserLogin)
                     {
                         return RedirectToAction("AnotherSession", "Home");
                     }
                     model.ErrorMessage = "Another user is currently logged in and must be logged out to continue";
                     model.RoleName = "Logout";
                 }
                 else
                 {
                     using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                     {

                         ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, model.UserLogin, model.Password);



                         if (ResponseData.Valid)
                         {

                             objGlobalVar.UserId = ResponseData.UserId;
                             objGlobalVar.Token = ResponseData.Token;
                             objGlobalVar.FirstLogin = ResponseData.FirstLogin;
                             objGlobalVar.ResetPasswordFlag = ResponseData.ResetPassword;
                             if (ResponseData.LoginType == 5)
                             {
                                 objGlobalVar.PortalType = "Patient";
                                 Session["LoginType"] = "PatientPortal";
                             }
                             else if (ResponseData.LoginType == 4)
                             {
                                 objGlobalVar.PortalType = "Provider";
                             }
                             else if (ResponseData.LoginType == 6)
                             {
                                 objGlobalVar.PortalType = "Patient";
                                 Session["LoginType"] = "PatientPortal"; //  SJF Added 7/29/14
                             }
                             Session.Add("UserId", ResponseData.UserId);


                             DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
                             if (model.UserLogin.ToString().StartsWith("R"))
                             {
                                 PatParams.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
                                 objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
                             }
                             else
                             {
                                 PatParams.PatientId = Convert.ToInt64(objGlobalVar.UserLogin);
                                 objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin);
                             }
                             DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
                             using (var Pservice = new DataPatientService.PatientWSSoapClient())
                             {
                                 PatTable = Pservice.GetPatientData(PatParams, ResponseData.Token, ResponseData.UserId, 0);
                                 objGlobalVar.PremiumFlag = PatTable.PremiumFlag;
                                 objGlobalVar.ActiveFlag = PatTable.Active;
                                 objGlobalVar.PatientName = PatTable.FirstName + " " + PatTable.LastName;

                                 DataPatientService.PatientFacilityOptionsData FacTable = new DataPatientService.PatientFacilityOptionsData();

                                 FacTable = Pservice.GetPatientFacilityOptions(objGlobalVar.PatientId, ResponseData.Token, ResponseData.UserId, 0);  // SJF Changed to objGlobalVar.PatientId 7/29/14
                                 objGlobalVar.AppointmentMessageAvailable = FacTable.AppointmentMessageAvailable;
                                 objGlobalVar.GeneralMessageAvailable = FacTable.GeneralMessageAvailable;
                                 objGlobalVar.MedicationMessageAvailable = FacTable.MedicationMessageAvailable;
                                 objGlobalVar.ReferralMessageAvailable = FacTable.ReferralMessageAvailable;

                                 DataPatientService.PatientVisitData PatVisit = new DataPatientService.PatientVisitData();
                                 DataPatientService.PatientVisitParms PatVisitParams = new DataPatientService.PatientVisitParms();
                                 PatVisitParams.PatientId = objGlobalVar.PatientId;

                                 PatVisit = Pservice.GetLatestPatientVisit(PatVisitParams, ResponseData.Token, ResponseData.UserId, 0);
                                 objGlobalVar.FacilitySelectId = PatVisit.FacilityId;
                                 objGlobalVar.VisitId = PatVisit.VisitId;

                                 DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
                                 DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                                 PatParms.PatientId = objGlobalVar.PatientId;  // SJF Changed to objGlobalVar.PatientId 7/29/14
                                 PatientImage = Pservice.GetPatientImageData(PatParms, ResponseData.Token, ResponseData.UserId, 0);
                                 if (PatientImage.Image != null)
                                 {
                                     Session["PatDisp"] = System.Convert.ToBase64String(PatientImage.Image);
                                 }
                                 else
                                 {
                                     string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
                                     byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                                     Session["PatDisp"] = System.Convert.ToBase64String(bytes);
                                 }
                             }
                             //if (ResponseData.LoginType == 4 && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                             //{// for provider portal
                             //    string usrlogin = clsEncryption.Enc(objGlobalVar.UserLogin);
                             //    string usrdata = clsEncryption.Enc(objGlobalVar.GetData());
                             //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["RedirectToProviderHome"] + usrlogin + "&&value1=" + usrdata);


                             //}
                             //else
                             //{ // for patient portal
                             //if (ResponseData.FirstLogin)
                             //{


                             //start
                             // if(model.UserLogin.ToString().StartsWith("R"))
                             // {
                             using (var service1 = new DataPatientService.PatientWSSoapClient())
                             {
                                 DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();
                                 DataPatientService.PatientRepData RepParms = new DataPatientService.PatientRepData();
                                 try
                                 {
                                     PRParms.FacilityId = objGlobalVar.FacilityId;
                                     PRParms.PatientId = Convert.ToInt64(model.UserLogin.Substring(1)); //objGlobalVar.PatientId; 
                                     PRParms.Option = 1;
                                     RepParms = service1.GetPatientRepData(PRParms, objGlobalVar.Token, objGlobalVar.UserId, objGlobalVar.FacilityId);
                                     PatientRepresentatives.edit = RepParms.Valid;
                                     if (RepParms.Valid)
                                     {
                                         PatientRepresentatives = base.ConvertToPatientRepDataModel(RepParms);
                                     }
                                     else
                                     {
                                         PatientRepresentatives.Demographics = true;
                                         PatientRepresentatives.Allergy = true;
                                         PatientRepresentatives.FamilyHistory = true;
                                         PatientRepresentatives.LabResults = true;
                                         PatientRepresentatives.MedicalHistory = true;
                                         PatientRepresentatives.Medication = true;
                                         PatientRepresentatives.Problem = true;
                                         PatientRepresentatives.EmergencyContact = true;
                                         PatientRepresentatives.SocialHistory = true;
                                         PatientRepresentatives.SurgicalHistory = true;
                                         PatientRepresentatives.VitalSigns = true;
                                         PatientRepresentatives.Immunization = true;
                                         PatientRepresentatives.Organ = true;
                                         PatientRepresentatives.ClinicalDoc = true;
                                         PatientRepresentatives.Insurance = true;
                                         PatientRepresentatives.ClinicalSummary = true;
                                         PatientRepresentatives.Appointment = true;
                                         PatientRepresentatives.Visit = true;
                                         PatientRepresentatives.UploadDocs = true;
                                         PatientRepresentatives.PlanOfCare = true;
                                         PatientRepresentatives.Messaging = true;
                                         PatientRepresentatives.DownloadTransmit = true;
                                         PatientRepresentatives.Procedure = true;
                                         PatientRepresentatives.Enabled = true;
                                         PatientRepresentatives.Provider = true;  // SJF Added 1/22/2015
                                     }
                                     Session["PatRep"] = PatientRepresentatives;
                                     var mylist = string.Join(", ", PatientRepresentatives);
                                     string Model = new JavaScriptSerializer().Serialize(PatientRepresentatives);
                                     objGlobalVar.PatientRep = Model;
                                 }


                                 catch (Exception ex)
                                 {
                                     return Json(ex.Message);
                                 }

                             }

                             // }
                             //end

                             if (PatientRepresentatives.Enabled == false && model.UserLogin.ToString().StartsWith("R"))
                             {
                                 LogOff();
                                 objGlobalVar.Token = "";
                                 objGlobalVar.UserId = 0;
                                 model.ErrorMessage = "Patient Rep access is disabled";

                             }
                             else
                             {
                                 if ((ResponseData.LoginType == 5 || ResponseData.LoginType == 6) && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                                 {
                                     TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                                     // Session["FirstLogin"] = ResponseData.FirstLogin;
                                     return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });
                                 }
                                 else
                                 {
                                     objGlobalVar.Token = "";
                                     objGlobalVar.UserId = 0;
                                     model.ErrorMessage = "Invalid Login for Patient portal";
                                 }
                             } //end outer else
                             //}
                             //else
                             //{
                             //  TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                             //  return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });
                             // }
                             //     }


                         }
                         else
                         {
                             objGlobalVar.Token = "";
                             objGlobalVar.UserId = 0;
                             model.ErrorMessage = ResponseData.ErrorMessage;
                         }
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

        
        public ActionResult EditPatientDataLogin(UserModel model)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            HomeViewModel HomeLists = new HomeViewModel();
            PatientRepModel PatientRepresentatives = new PatientRepModel();
            GlobalVar objGlobalVar = new GlobalVar();


            objGlobalVar.FacilityId = 0;
            objGlobalVar.UserLogin = model.UserLogin;
            if (model.UserLogin.StartsWith("R"))
            { PatientRepresentatives.loginFlag = true; }
            else { PatientRepresentatives.loginFlag = false; }
            Session["loginRepFlag"] = PatientRepresentatives.loginFlag;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

            try
            {
                //if (authCookie != null)
                //{
                //    if (model.UserLogin == RequestHelper.MyGlobalVar.UserLogin)
                //    {
                //        return RedirectToAction("Index", "Home");
                //    }
                //    model.ErrorMessage = "Another user is currently logged in and must be logged out to continue";
                //    model.RoleName = "Logout";
                //}
                //else
                //{
                    using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                    {

                        ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, model.UserLogin, model.Password);



                        if (ResponseData.Valid)
                        {

                            objGlobalVar.UserId = ResponseData.UserId;
                            objGlobalVar.Token = ResponseData.Token;
                            objGlobalVar.FirstLogin = ResponseData.FirstLogin;
                            if (ResponseData.LoginType == 5)
                            {
                                objGlobalVar.PortalType = "Patient";
                                Session["LoginType"] = "PatientPortal";
                            }
                            else if (ResponseData.LoginType == 4)
                            {
                                objGlobalVar.PortalType = "Provider";
                            }
                            else if (ResponseData.LoginType == 6)
                            {
                                objGlobalVar.PortalType = "Patient";
                                Session["LoginType"] = "PatientPortal"; //  SJF Added 7/29/14
                            }
                            Session.Add("UserId", ResponseData.UserId);


                            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
                            if (model.UserLogin.ToString().StartsWith("R"))
                            {
                                PatParams.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
                                objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin.Substring(1));
                            }
                            else
                            {
                                PatParams.PatientId = Convert.ToInt64(objGlobalVar.UserLogin);
                                objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin);
                            }
                            DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
                            using (var Pservice = new DataPatientService.PatientWSSoapClient())
                            {
                                PatTable = Pservice.GetPatientData(PatParams, ResponseData.Token, ResponseData.UserId, 0);
                                objGlobalVar.PremiumFlag = PatTable.PremiumFlag;
                                objGlobalVar.ActiveFlag = PatTable.Active;
                                objGlobalVar.PatientName = PatTable.FirstName + " " + PatTable.LastName;

                                DataPatientService.PatientFacilityOptionsData FacTable = new DataPatientService.PatientFacilityOptionsData();

                                FacTable = Pservice.GetPatientFacilityOptions(objGlobalVar.PatientId, ResponseData.Token, ResponseData.UserId, 0);  // SJF Changed to objGlobalVar.PatientId 7/29/14
                                objGlobalVar.AppointmentMessageAvailable = FacTable.AppointmentMessageAvailable;
                                objGlobalVar.GeneralMessageAvailable = FacTable.GeneralMessageAvailable;
                                objGlobalVar.MedicationMessageAvailable = FacTable.MedicationMessageAvailable;
                                objGlobalVar.ReferralMessageAvailable = FacTable.ReferralMessageAvailable;

                                DataPatientService.PatientVisitData PatVisit = new DataPatientService.PatientVisitData();
                                DataPatientService.PatientVisitParms PatVisitParams = new DataPatientService.PatientVisitParms();
                                PatVisitParams.PatientId = objGlobalVar.PatientId;

                                PatVisit = Pservice.GetLatestPatientVisit(PatVisitParams, ResponseData.Token, ResponseData.UserId, 0);
                                objGlobalVar.FacilitySelectId = PatVisit.FacilityId;
                                objGlobalVar.VisitId = PatVisit.VisitId;

                                DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
                                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                                PatParms.PatientId = objGlobalVar.PatientId;  // SJF Changed to objGlobalVar.PatientId 7/29/14
                                PatientImage = Pservice.GetPatientImageData(PatParms, ResponseData.Token, ResponseData.UserId, 0);
                                if (PatientImage.Image != null)
                                {
                                    Session["PatDisp"] = System.Convert.ToBase64String(PatientImage.Image);
                                }
                                else
                                {
                                    string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
                                    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                                    Session["PatDisp"] = System.Convert.ToBase64String(bytes);
                                }
                            }
                            //if (ResponseData.LoginType == 4 && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                            //{// for provider portal
                            //    string usrlogin = clsEncryption.Enc(objGlobalVar.UserLogin);
                            //    string usrdata = clsEncryption.Enc(objGlobalVar.GetData());
                            //    return Redirect(System.Configuration.ConfigurationManager.AppSettings["RedirectToProviderHome"] + usrlogin + "&&value1=" + usrdata);


                            //}
                            //else
                            //{ // for patient portal
                            //if (ResponseData.FirstLogin)
                            //{


                            //start
                            // if(model.UserLogin.ToString().StartsWith("R"))
                            // {
                            using (var service1 = new DataPatientService.PatientWSSoapClient())
                            {
                                DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();
                                DataPatientService.PatientRepData RepParms = new DataPatientService.PatientRepData();
                                try
                                {
                                    PRParms.FacilityId = objGlobalVar.FacilityId;
                                    PRParms.PatientId = Convert.ToInt64(model.UserLogin.Substring(1)); //objGlobalVar.PatientId; 
                                    PRParms.Option = 1;
                                    RepParms = service1.GetPatientRepData(PRParms, objGlobalVar.Token, objGlobalVar.UserId, objGlobalVar.FacilityId);
                                    PatientRepresentatives.edit = RepParms.Valid;
                                    if (RepParms.Valid)
                                    {
                                        PatientRepresentatives = base.ConvertToPatientRepDataModel(RepParms);
                                    }
                                    else
                                    {
                                        PatientRepresentatives.Demographics = true;
                                        PatientRepresentatives.Allergy = true;
                                        PatientRepresentatives.FamilyHistory = true;
                                        PatientRepresentatives.LabResults = true;
                                        PatientRepresentatives.MedicalHistory = true;
                                        PatientRepresentatives.Medication = true;
                                        PatientRepresentatives.Problem = true;
                                        PatientRepresentatives.EmergencyContact = true;
                                        PatientRepresentatives.SocialHistory = true;
                                        PatientRepresentatives.SurgicalHistory = true;
                                        PatientRepresentatives.VitalSigns = true;
                                        PatientRepresentatives.Immunization = true;
                                        PatientRepresentatives.Organ = true;
                                        PatientRepresentatives.ClinicalDoc = true;
                                        PatientRepresentatives.Insurance = true;
                                        PatientRepresentatives.ClinicalSummary = true;
                                        PatientRepresentatives.Appointment = true;
                                        PatientRepresentatives.Visit = true;
                                        PatientRepresentatives.UploadDocs = true;
                                        PatientRepresentatives.PlanOfCare = true;
                                        PatientRepresentatives.Messaging = true;
                                        PatientRepresentatives.DownloadTransmit = true;
                                        PatientRepresentatives.Procedure = true;
                                        PatientRepresentatives.Enabled = true;
                                        PatientRepresentatives.Provider = true;   // SJF Added 1/22/2015
                                    }
                                    Session["PatRep"] = PatientRepresentatives;
                                    var mylist = string.Join(", ", PatientRepresentatives);
                                    string Model = new JavaScriptSerializer().Serialize(PatientRepresentatives);
                                    objGlobalVar.PatientRep = Model;
                                }


                                catch (Exception ex)
                                {
                                    return Json(ex.Message);
                                }

                            }

                            // }
                            //end

                            if (PatientRepresentatives.Enabled == false && model.UserLogin.ToString().StartsWith("R"))
                            {
                                LogOff();
                                objGlobalVar.Token = "";
                                objGlobalVar.UserId = 0;
                                model.ErrorMessage = "Patient Rep access is disabled";

                            }
                            else
                            {
                                if ((ResponseData.LoginType == 5 || ResponseData.LoginType == 6) && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                                {
                                    TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                                    // Session["FirstLogin"] = ResponseData.FirstLogin;
                                    return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });
                                }
                                else
                                {
                                    objGlobalVar.Token = "";
                                    objGlobalVar.UserId = 0;
                                    model.ErrorMessage = "Invalid Login for Patient portal";
                                }
                            } //end outer else
                            //}
                            //else
                            //{
                            //  TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);
                            //  return RedirectToAction("Index", "Home", new { Login = ResponseData.FirstLogin });
                            // }
                            //     }


                        }
                        else
                        {
                            objGlobalVar.Token = "";
                            objGlobalVar.UserId = 0;
                            model.ErrorMessage = ResponseData.ErrorMessage;
                        }
                    }

                }
            //}

            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
            HomeLists.CurrentUser = model;

            return Json(HomeLists.CurrentUser.ErrorMessage, JsonRequestBehavior.AllowGet);
           // return View(HomeLists);
        }
        private void DeleteAuthenticationCookie()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                
                    authCookie.Expires = DateTime.Now.AddDays(-100);
                    Response.Cookies.Set(authCookie);
                    Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            }

        }
        public ActionResult LogOff()
        {
             DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
             string url = Request.QueryString["CCDUrl"];
             try
             {
                 using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                 {
                    ResponseData = service.LogoutUser(RequestHelper.MyGlobalVar.FacilityId, RequestHelper.MyGlobalVar.UserId);

                       if (ResponseData.Valid)
                       {
                           FormsAuthentication.SignOut();
                           //DeleteAuthenticationCookie() ;
                           Session["OptionSelected"] = null;
                           
                       }
                 }
             }

             catch (Exception ex)
             {
                 
             }
             if (!String.IsNullOrEmpty(url))
             {
                 return RedirectToAction("CCDLogin", "Home");
                
             }
             Session["LoginType"] = null;
             return RedirectToAction("Login", "Home");
        }


        public ActionResult LogOffProviderPortal()
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
            return RedirectToAction("ProviderLogin", "Home");
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CCD()
        {
            bool cust = Convert.ToBoolean(Request.QueryString["Customize"]);
            ViewData["Customize"] = cust;
            return View();
        }

        //ending action of chart summary tab
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CCDView()
        {
            
            return View();
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CustomizedCCDView()
        {
            if (Request.Cookies["Cunstomize"] != null)
            {
                string CCDStr = Request.Cookies["Cunstomize"].Value.ToString();
                DataSet CCDds = utility.GetDataSetFromString(CCDStr);
                HttpCookie myCookie = new HttpCookie("Cunstomize");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return View();
        }
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CustomizeCCD()
        {
            PatientVisitCCDModel Model = new PatientVisitCCDModel();
            Int64 loc = Convert.ToInt64(Request.QueryString["Location"]); 
            Int64 VisitId = Convert.ToInt64(Request.QueryString["Visit"]);
            HomeViewModel HomeLists = new HomeViewModel();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = loc;
            VisitParams.VisitId = VisitId;
            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                VisitData = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    HomeLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(VisitData);
                }
            }
            using (var Summservice = new DataPatientService.PatientWSSoapClient())
            {

                PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {

                    HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                }
            }
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            //if (Model.facilityOptions != null && Model.visitOptions != null)
            //{
            //    smodel.facilityOptions = Model.facilityOptions;
            //    smodel.facilitySelected = Model.facilitySelected;
            //    smodel.visitOptions = Model.visitOptions;
            //    smodel.visitSelected = Model.visitSelected;
            //    Session["OptionSelected"] = smodel;
            //}
          //  HomeViewModel HomeLists = new HomeViewModel();
           
                //using (var service = new DataPatientService.PatientWSSoapClient())
                //{
                //    //Visit Data...
                //   // DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                //    DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                //    PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId;

                //    VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //    if (VisitData.Valid)
                //    {
                //        HomeLists.Visits = VisitData.dt.ToVisitModelList();
                //    }
                //}
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    // consolidated call 
                    //DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
                    //DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                    DataPatientDocumentService.PatientCCDTableData PatientCCDCustomize = new DataPatientDocumentService.PatientCCDTableData();
                    DataPatientDocumentService.PatientCCDParms parms = new DataPatientDocumentService.PatientCCDParms();
                    parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    parms.VisitId = Convert.ToInt64(Request.QueryString["Visit"]);
                    parms.FacilityId = Convert.ToInt64(Request.QueryString["Location"]);
                    //PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    PatientCCDCustomize = service.GetPatientCCDCustomizeData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientCCDCustomize.Valid)
                    {
                        HomeLists.SocialHistories = PatientCCDCustomize.dtSocialHist.ToSocialHistoryModelList();
                        HomeLists.VitalSigns = PatientCCDCustomize.dtVitalSign.ToVitalsignModelList();
                        HomeLists.Procedures = PatientCCDCustomize.dtProcedure.ToProcedureModelList();
                        HomeLists.Medications = PatientCCDCustomize.dtMedication.ToMedicationHistoryModelList();
                        HomeLists.Problems = PatientCCDCustomize.dtProblem.ToProblemModelList();
                        HomeLists.LabResults = PatientCCDCustomize.dtLab.ToLabResultModelList();
                        HomeLists.Immunizations = PatientCCDCustomize.dtImmunization.ToImmunizationModelList();
                        HomeLists.Allergies = PatientCCDCustomize.dtAllergy.ToAllergyModelList();
                        HomeLists.Pocs = PatientCCDCustomize.dtPlanOfCare.ToPOCModelList();
                        HomeLists.FamilyHistories = PatientCCDCustomize.dtFamilyHist.ToFamilyHistoryClinicalModelList();
                        HomeLists.MedicalHistories = PatientCCDCustomize.dtMedicalHist.ToMedicalHistoryClinicalModelList();
                        HomeLists.MedicationsAdministered = PatientCCDCustomize.dtMedsAdministered.ToMedicationHistoryModelList();
                        HomeLists.ClinicalInstructions = PatientCCDCustomize.dtClinicalInstructions.ToPOCModelList();
                    }
                }
            
            return View(HomeLists);
        
        }
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CCDActivityLog()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                List<CCDActivityLogModel> Model = new List<CCDActivityLogModel>();
                // CCDActivityLogModel Model = new CCDActivityLogModel();
                DataPatientDocumentService.PatientDocParms patdoc = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData tabledoc = new DataPatientDocumentService.PatientDocTableData();
                patdoc.PatientId = RequestHelper.MyGlobalVar.PatientId;
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    tabledoc = service.GetCCDAuditLog(patdoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (tabledoc.Valid)
                    {
                        Model = tabledoc.dt.ToCddActivityLog();
                    }
                }
                return View(Model);
            }
            else {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpGet]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult GetCCDData()
        {
            PatientVisitCCDModel Model = new PatientVisitCCDModel();
            HomeViewModel HomeLists = new HomeViewModel();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = Convert.ToInt64(TempData["FacilityId"]);
            VisitParams.VisitId = Convert.ToInt64(TempData["VisitId"]);
            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            bool demograhics = Convert.ToBoolean(TempData["Demographics"]);
            bool Provider = Convert.ToBoolean(TempData["Provider"]);
            bool Visit = Convert.ToBoolean(TempData["Visit"]);
           // if(Provider == true){
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                VisitData = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    HomeLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(VisitData);
                    //Model = base.ConvertPatientVisitCCDModel(VisitData);
                }
            }
          //  }
           // if (demograhics == true)
           // {
                using (var Summservice = new DataPatientService.PatientWSSoapClient())
                {

                    PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientSummary.Valid)
                    {

                        HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                }
         //   }
            HomeLists.PatientCCDownload.Provider = Provider;
            HomeLists.PatientCCDownload.Demographics = demograhics;
            HomeLists.PatientCCDownload.Visit = Visit;
            string XML = TempData["Xml"].ToString();
            List<HomeWidgetModel> widgts = new List<HomeWidgetModel>();
            widgts = utility.ConvertToHTML(XML);
           // if (demograhics == true && Provider == true)
           // {
                widgts.Add(new HomeWidgetModel
                {
                    Id = "patientSummary",
                    WidgetHtml = ViewHelper.RenderRazorViewToString("_CCD_Patient", HomeLists, this)
                });
           // }
            //var data = widgts.FirstOrDefault();
            //PatientModel Model = new JavaScriptSerializer().Deserialize<PatientModel>(data.WidgetHtml);
            //string WidgetHtml = ViewHelper.RenderRazorViewToString("_CCD_Patient", Model, this);
            //widgts.Where(d => d.Id == "patientSummary").First().WidgetHtml = WidgetHtml;
            return Json(widgts, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        [AnotherSessionRedirectionFilter]
        [HttpGet]
        public ActionResult Index()
        {
            long fac = RequestHelper.MyGlobalVar.FacilitySelectId;
            bool appoint = RequestHelper.MyGlobalVar.AppointmentMessageAvailable;
            bool msg = RequestHelper.MyGlobalVar.GeneralMessageAvailable;
            bool med = RequestHelper.MyGlobalVar.MedicationMessageAvailable;
            bool referal = RequestHelper.MyGlobalVar.ReferralMessageAvailable;

            FacilityVisitSelectModel smodel;
            smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
            if (smodel == null)
            {
                //smodel.
                smodel = new FacilityVisitSelectModel {
                facilitySelected = RequestHelper.MyGlobalVar.FacilitySelectId.ToString(),
                visitSelected = RequestHelper.MyGlobalVar.VisitId.ToString(),
                };
                Session["OptionSelected"] = smodel;
            }
            if (Session["PatDisp"] == null)
            {
                using (var Pservice = new DataPatientService.PatientWSSoapClient())
                {
                            DataPatientService.PatientImageData PatientImage = new DataPatientService.PatientImageData();
                            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                            PatientImage = Pservice.GetPatientImageData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);
                            if (PatientImage.Image != null)
                            {
                                Session["PatDisp"] = System.Convert.ToBase64String(PatientImage.Image);
                            }
                            else
                            {
                                string filePath = Server.MapPath(Url.Content("~/Content/img/GenericPerson.png"));
                                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                                Session["PatDisp"] = System.Convert.ToBase64String(bytes);
                            }
                        }
            }
            string firstlogin = Request.QueryString["Login"];

            HomeViewModel HomeLists = new HomeViewModel();
            HomeLists.FirstLogin = firstlogin;
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                DataMessageService.CodeTableData MessageType = new DataMessageService.CodeTableData();
                DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();

                MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;

                MessageType = service.GetMessageTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageType.Valid)
                {
                    HomeLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                    HomeLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                }


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
                    HomeLists.Relationships = ConfigDocument.dt.ToRelationshipModelList();
                    HomeLists.Relationships.Insert(0, new RelationshipModel { RelationShipID = -1, Value = "--Select--", SNOMED = "-1" });

                }
                ConfigDocument = service.GetConditionStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    HomeLists.ConditionStatus = ConfigDocument.dt.ToConditionStatusModelList();
                    HomeLists.ConditionStatus.Insert(0, new ConditionStatusModel { ConditionStatusId = -1, Value = "--Select--" });
                }

                ConfigDocument = service.GetSocialCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    //HomeLists.= ConfigDocument.dt.ToSocialHistModelList();
                    HomeLists.Social = ConfigDocument.dt.ToSocialHistModelList();
                    HomeLists.Social.Insert(0, new SocialModel { SNOMED_Social = -1, Value = "--Select--" });
                }
                ConfigDocument = service.GetInstructionTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                {
                    HomeLists.InstructionType = ConfigDocument.dt.ToInstructionTypeModelList();
                }
            }

            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;

                ConfigDocument = service.GetVaccineCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, "");
                if (ConfigDocument.Valid)
                {
                    HomeLists.Vaccines = ConfigDocument.dt.ToVaccineModelList();
                }
                ConfigDocument = service.GetSecurityQuestionCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    HomeLists.SecurityQuestion = ConfigDocument.dt.ToSecurityQuestionModelList();
                }
                ConfigDocument = service.GetExerciseFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {

                    HomeLists.frequency = ConfigDocument.dt.ToExerciseFrequencyModelList();
                    HomeLists.frequency.Insert(0, new ExerciseFrequencyModel { ExerciseFrequencyId = -1, Value = "--Select--" });
                }
                ConfigDocument = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    HomeLists.smokingstatus = ConfigDocument.dt.ToSmokingStatusModelList();
                    HomeLists.smokingstatus.Insert(0, new SmokingStatusModel { SmokingStatusId = -1, Value = "--Select--" });

                }

                ConfigDocument = service.GetActivityLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (ConfigDocument.Valid)
                {
                    HomeLists.ActivityLevel = ConfigDocument.dt.ToActivityLevelModelList();
                    HomeLists.ActivityLevel.Insert(0, new ActivityLevelModel { ActivityLevelId = -1, Value = "--Select--" });

                }

                ConfigDocument = service.GetAlcoholFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    HomeLists.AlcoholFrequency = ConfigDocument.dt.ToAlcoholFrequencyModelList();
                    HomeLists.AlcoholFrequency.Insert(0, new AlcoholFrequencyModel { AlcoholFrequencyId = -1, Value = "--Select--" });
                    
                }
                ConfigDocument = service.GetEducationLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    HomeLists.Educationlevel = ConfigDocument.dt.ToEducationLevelModelList();
                    HomeLists.Educationlevel.Insert(0, new EducationLevelModel { EducationLevelId = -1, Value = "--Select--" });

                }

            }

            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    HomeLists.Providers = ProviderData.dt.ToProviderModelList();
                    HomeLists.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

                }
                DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
              
            }

            // Patients Related data which includes Patient demographics, visits and facilities
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                DataPatientService.PatientRepData PatRepData = new DataPatientService.PatientRepData();
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientSummary.Valid)
                {
                    HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                    // TicketHelper.SetPatientInformationAndGet(HomeLists.PatientSummary.PatientId.ToString() + "," + HomeLists.PatientSummary.FirstName + " " + HomeLists.PatientSummary.LastName);  
                }

                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    HomeLists.Facilities = FacilityData.dt.ToFacilityModelList();
                    HomeLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    HomeLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }


                PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                if (smodel != null)
                {
                    PatVisitParms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                }
                else {
                     PatVisitParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;                
                }
                PatVisitParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                PatVisitParms.Option = 3;
                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    HomeLists.Visits = VisitData.dt.ToVisitModelList();

                }
                if (RequestHelper.MyGlobalVar.UserLoginEx.StartsWith("R"))
                {
                    DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();
                    PRParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PRParms.PatientId = RequestHelper.MyGlobalVar.PatientId;  //RequestHelper.MyGlobalVar.PatientId;
                    PRParms.Option = 1;
                    PatRepData = service.GetPatientRepData(PRParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatRepData.Valid)
                    {
                        HomeLists.PatientRepresentatives = base.ConvertToPatientRepDataModel(PatRepData);

                    }
                }
                else
                {
                    HomeLists.PatientRepresentatives.Demographics = true;
                    HomeLists.PatientRepresentatives.Allergy = true;
                    HomeLists.PatientRepresentatives.FamilyHistory = true;
                    HomeLists.PatientRepresentatives.LabResults = true;
                    HomeLists.PatientRepresentatives.MedicalHistory = true;
                    HomeLists.PatientRepresentatives.Medication = true;
                    HomeLists.PatientRepresentatives.Problem = true;
                    HomeLists.PatientRepresentatives.EmergencyContact = true;
                    HomeLists.PatientRepresentatives.SocialHistory = true;
                    HomeLists.PatientRepresentatives.SurgicalHistory = true;
                    HomeLists.PatientRepresentatives.VitalSigns = true;
                    HomeLists.PatientRepresentatives.Immunization = true;
                    HomeLists.PatientRepresentatives.Organ = true;
                    HomeLists.PatientRepresentatives.ClinicalDoc = true;
                    HomeLists.PatientRepresentatives.Insurance = true;
                    HomeLists.PatientRepresentatives.ClinicalSummary = true;
                    HomeLists.PatientRepresentatives.Appointment = true;
                    HomeLists.PatientRepresentatives.Visit = true;
                    HomeLists.PatientRepresentatives.UploadDocs = true;
                    HomeLists.PatientRepresentatives.PlanOfCare = true;
                    HomeLists.PatientRepresentatives.Messaging = true;
                    HomeLists.PatientRepresentatives.DownloadTransmit = true;
                    HomeLists.PatientRepresentatives.Procedure = true;
                    HomeLists.PatientRepresentatives.Enabled = true;
                    HomeLists.PatientRepresentatives.Provider = true;



                }


                if (Session["PatRep"] == null)
                {
                    Session["PatRep"] = HomeLists.PatientRepresentatives;
                }
            }
            // Patients Document Related data which includes labs, allergies and all histories
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                PatDocParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                PatDocParms.Option = 1;
                PatDocParms.Active = 1;


           DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();

               DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();

                parms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
               parms.VisitId = 0;
               parms.FacilityId = 0;
             parms.Option = 1;
               parms.Active = 1;
                parms.Medication = true;
           
                if (smodel != null)
                {
               
                 HomeLists.FacilityVisitSelect = smodel;
                }
            PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

               if (PatientDocumentSummary.Valid)
               {
                   HomeLists.Medications = PatientDocumentSummary.dtMedication.ToMedicationHistoryModelList();
                  HomeLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
              }

                PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientDocument.Valid)
                {
                    HomeLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();
                    HomeLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                    HomeLists.PharmciesForRefill = PatientDocument.dt.ToPatientPharmacyModellList();
                    HomeLists.PharmciesForRefill.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                }


            }
            DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                PatShare = service.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                HomeLists.PatientShare = base.ConvertToPatientShareModel(PatShare);
            }
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {
                DataPatientConfigService.CodeTableData PatientConfig = new DataPatientConfigService.CodeTableData();
                PatientConfig = service.GetRouteOfAdministrationCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientConfig.Valid)
                {
                    HomeLists.RoutesAdministration = PatientConfig.dt.ToRoutesOfAdministrationModelList();
                }
            }
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                DataPatientService.PatientWebSettingData PatientData = new DataPatientService.PatientWebSettingData();
                PatientData = service.GetPatientWebSettingData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);




                if (PatientData.Valid)
                {
                  
                    if (PatientData.Valid)
                    {
                        List<KeyValuePair<string, int>> listdisable = new List<KeyValuePair<string, int>>();

                        List<PatientWebSettingModelString> objPatientWebSettingModelStringList = new List<PatientWebSettingModelString>();

                        List<KeyValuePair<string, int>> lst1 = new List<KeyValuePair<string, int>>();
                        List<KeyValuePair<string, int>> lstComman = new List<KeyValuePair<string, int>>();
                        for (int i = 1; i < 3; i++)
                        {
                            Dictionary<string, int> dictionary = new Dictionary<string, int>();
                            if (i == 1)
                            {
                                dictionary[WidgetEnums.Widget.appointmentWidget.ToString()] = PatientData.AppointmentsWidgetLeft;
                                dictionary[WidgetEnums.Widget.labWidget.ToString()] = PatientData.LabTestWidgetLeft;
                                dictionary[WidgetEnums.Widget.visitsWidget.ToString()] = PatientData.VisitWidgetLeft;
                                dictionary[WidgetEnums.Widget.medicationWidget.ToString()] = PatientData.MedicationWidgetLeft;
                                dictionary[WidgetEnums.Widget.probsWidget.ToString()] = PatientData.ProblemWidgetLeft;
                                dictionary[WidgetEnums.Widget.vitalWidget.ToString()] = PatientData.VitalSignsWidgetLeft;
                                dictionary[WidgetEnums.Widget.allergyWidget.ToString()] = PatientData.AllergyWidgetLeft;
                                dictionary[WidgetEnums.Widget.ProceduresWidget.ToString()] = PatientData.ProcedureWidgetLeft;
                                //dictionary[WidgetEnums.Widget.PremiumWidget.ToString()] = PatientData.PremiumWidgetLeft;
                                dictionary[WidgetEnums.Widget.socialhistWidget.ToString()] = PatientData.SocialWidgetLeft;
                                dictionary[WidgetEnums.Widget.statementWidget.ToString()] = PatientData.StatementWidgetLeft;
                                dictionary[WidgetEnums.Widget.familyhistWidget.ToString()] = PatientData.FamilyWidgetLeft;
                                dictionary[WidgetEnums.Widget.PmedicalhistWidget.ToString()] = PatientData.PastMedicalWidgetLeft;
                                dictionary[WidgetEnums.Widget.immunizWidget.ToString()] = PatientData.ImmunizationWidgetLeft;
                                dictionary[WidgetEnums.Widget.clinicalWidget.ToString()] = PatientData.ClinicalDocWidgetLeft;
                                dictionary[WidgetEnums.Widget.insuranceWidget.ToString()] = PatientData.InsuranceWidgetLeft;
                                dictionary[WidgetEnums.Widget.documentsWidget.ToString()] = PatientData.DocumentWidgetLeft;
                                dictionary[WidgetEnums.Widget.pocWidget.ToString()] = PatientData.PlanOfCareWidgetLeft;
                                dictionary[WidgetEnums.Widget.ClinicalInstructionsWidget.ToString()] = PatientData.ClinicalInstructionsWidgetLeft;
                                dictionary[WidgetEnums.Widget.ProviderWidget.ToString()] = PatientData.ProviderWidgetLeft;
                            }
                            else if (i == 2)
                            {
                                dictionary[WidgetEnums.Widget.appointmentWidget.ToString()] = PatientData.AppointmentsWidgetRight;
                                dictionary[WidgetEnums.Widget.labWidget.ToString()] = PatientData.LabTestWidgetRight;
                                dictionary[WidgetEnums.Widget.visitsWidget.ToString()] = PatientData.VisitWidgetRight;
                                dictionary[WidgetEnums.Widget.medicationWidget.ToString()] = PatientData.MedicationWidgetRight;
                                dictionary[WidgetEnums.Widget.probsWidget.ToString()] = PatientData.ProblemWidgetRight;
                                dictionary[WidgetEnums.Widget.vitalWidget.ToString()] = PatientData.VitalSignsWidgetRight;
                                dictionary[WidgetEnums.Widget.allergyWidget.ToString()] = PatientData.AllergyWidgetRight;
                                dictionary[WidgetEnums.Widget.ProceduresWidget.ToString()] = PatientData.ProcedureWidgetRight;
                                dictionary[WidgetEnums.Widget.socialhistWidget.ToString()] = PatientData.SocialWidgetRight;
                                dictionary[WidgetEnums.Widget.statementWidget.ToString()] = PatientData.StatementWidgetRight;
                                dictionary[WidgetEnums.Widget.familyhistWidget.ToString()] = PatientData.FamilyWidgetRight;
                                dictionary[WidgetEnums.Widget.PmedicalhistWidget.ToString()] = PatientData.PastMedicalWidgetRight;
                                dictionary[WidgetEnums.Widget.immunizWidget.ToString()] = PatientData.ImmunizationWidgetRight;
                                dictionary[WidgetEnums.Widget.clinicalWidget.ToString()] = PatientData.ClinicalDocWidgetRight;
                                dictionary[WidgetEnums.Widget.insuranceWidget.ToString()] = PatientData.InsuranceWidgetRight;
                                dictionary[WidgetEnums.Widget.documentsWidget.ToString()] = PatientData.DocumentWidgetRight;
                                //dictionary[WidgetEnums.Widget.PremiumWidget.ToString()] = PatientData.PremiumWidgetRight;
                                dictionary[WidgetEnums.Widget.pocWidget.ToString()] = PatientData.PlanOfCareWidgetRight;
                                dictionary[WidgetEnums.Widget.ClinicalInstructionsWidget.ToString()] = PatientData.ClinicalInstructionsWidgetRight;
                                dictionary[WidgetEnums.Widget.ProviderWidget.ToString()] = PatientData.ProviderWidgetRight;
                            }

                            foreach (var item in dictionary)
                            {
                                if (item.Value == 0)
                                    lstComman.Add(item);
                            }

                            // Call ToList.
                            List<KeyValuePair<string, int>> list = dictionary.OrderBy(x => x.Value).ToList();
                            var zeroitem = dictionary.Where(x => x.Value == 0).ToList();
                            lst1.AddRange(zeroitem);
                            PatientWebSettingModelString objPatientWebSettingModelString = new PatientWebSettingModelString();
                            List<string> objList = new List<string>();
                            // Loop over list.
                            foreach (KeyValuePair<string, int> pair in list)
                            {
                                switch (pair.Value)
                                {
                                    case 1:
                                        objList.Add(pair.Key);
                                        break;
                                    case 2:
                                        objList.Add(pair.Key);
                                        break;
                                    case 3:
                                        objList.Add(pair.Key);
                                        break;
                                    case 4:
                                        objList.Add(pair.Key);
                                        break;
                                    case 5:
                                        objList.Add(pair.Key);
                                        break;
                                    case 6:
                                        objList.Add(pair.Key);
                                        break;
                                    case 7:
                                        objList.Add(pair.Key);
                                        break;
                                    case 8:
                                        objList.Add(pair.Key);
                                        break;
                                    case 9:
                                        objList.Add(pair.Key);
                                        break;
                                    case 10:
                                        objList.Add(pair.Key);
                                        break;
                                    case 11:
                                        objList.Add(pair.Key);
                                        break;
                                    case 12:
                                        objList.Add(pair.Key);
                                        break;
                                    case 13:
                                        objList.Add(pair.Key);
                                        break;
                                    case 14:
                                        objList.Add(pair.Key);
                                        break;
                                    case 15:
                                        objList.Add(pair.Key);
                                        break;
                                    case 16:
                                        objList.Add(pair.Key);
                                        break;
                                    case 17:
                                        objList.Add(pair.Key);
                                        break;
                                    case 18:
                                        objList.Add(pair.Key);
                                        break;
                                    case 19:
                                        objList.Add(pair.Key);
                                        break;
                                }
                                objPatientWebSettingModelString.widgetname = objList;
                                objPatientWebSettingModelString.PatientWebSettingId = (short)i;
                            }
                            listdisable.AddRange(list);
                            objPatientWebSettingModelStringList.Add(objPatientWebSettingModelString);
                        }
                        var objlist = lst1.GroupBy(x => x.Key).Where(x => x.Count() > 1).ToList();
                        List<string> commansection = new List<string>();
                        foreach (var item in objlist)
                            commansection.Add(item.Key);

                        ViewBag.DisableLeftList = commansection;
                   
                        var temp = listdisable.OrderByDescending(x => x.Value).ToList();
                        listdisable.RemoveRange(0, listdisable.Count);
                        foreach (var item in temp)
                        {
                            if (listdisable.Count == 0) { listdisable.Add(item); }
                            else if (!listdisable.Any(va => va.Key.Contains(item.Key)))
                                listdisable.Add(item);
                        }
                        ViewBag.DisableList = listdisable;
                        HomeLists.PatientWebSetting = objPatientWebSettingModelStringList;
                    }
                    else
                    {
                        PatientWebSettingModelString objPatientWebSettingModelString = new PatientWebSettingModelString();
                        List<string> objList = new List<string>();
                        objList.Add(WidgetEnums.Widget.allergyWidget.ToString());
                        objList.Add(WidgetEnums.Widget.appointmentWidget.ToString());
                        objList.Add(WidgetEnums.Widget.labWidget.ToString());
                        objList.Add(WidgetEnums.Widget.visitsWidget.ToString());
                        objList.Add(WidgetEnums.Widget.medicationWidget.ToString());
                        objList.Add(WidgetEnums.Widget.probsWidget.ToString());
                        objList.Add(WidgetEnums.Widget.vitalWidget.ToString());
                        objList.Add(WidgetEnums.Widget.pocWidget.ToString());
                        objList.Add(WidgetEnums.Widget.PremiumWidget.ToString());
                        objPatientWebSettingModelString.widgetname = objList;
                        objPatientWebSettingModelString.PatientWebSettingId = 1;
                        PatientWebSettingModelString objPatientWebSettingModelString1 = new PatientWebSettingModelString();
                        List<string> objList1 = new List<string>();
                        objList1.Add(WidgetEnums.Widget.familyhistWidget.ToString());
                        objList1.Add(WidgetEnums.Widget.immunizWidget.ToString());
                        objList1.Add(WidgetEnums.Widget.PmedicalhistWidget.ToString());
                        objList1.Add(WidgetEnums.Widget.socialhistWidget.ToString());
                        objList1.Add(WidgetEnums.Widget.statementWidget.ToString());
                        objPatientWebSettingModelString1.widgetname = objList1;
                        objPatientWebSettingModelString1.PatientWebSettingId = 2;
                        List<PatientWebSettingModelString> objPatientWebSettingModelStringList = new List<PatientWebSettingModelString>();
                        objPatientWebSettingModelStringList.Add(objPatientWebSettingModelString);
                        objPatientWebSettingModelStringList.Add(objPatientWebSettingModelString1);
                        HomeLists.PatientWebSetting = objPatientWebSettingModelStringList;
                    }

                }
                return View(HomeLists);
            }
        }


     
        public PartialViewResult RefillPharmacypartialview()
        {
            HomeViewModel HomeLists = new HomeViewModel();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                PatDocParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                PatDocParms.Option = 1;
                PatDocParms.Active = 1;
                PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientDocument.Valid)
                {
                  //  HomeLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();
                  //  HomeLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                    HomeLists.PharmciesForRefill = PatientDocument.dt.ToPatientPharmacyModellList();
                 //  HomeLists.PharmciesForRefill.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                }
            }
            return PartialView("_RefillPharmacyDropDown", HomeLists);

        }
        [HttpPost]
        public JsonResult WidgetIdSave(string Id, string widgetorder)
        {
            short count = 1;
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                DataPatientService.PatientWebSettingData PatientData = new DataPatientService.PatientWebSettingData();
                PatientData = service.GetPatientWebSettingData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                string[] widgetname = widgetorder.Split(',');

                if (Id == "firstcolumn")
                {

                    foreach (string word in widgetname)
                    {
                        switch (word)
                        {
                            case "widg1":
                                PatientData.AppointmentsWidgetRight = 0;
                                PatientData.AppointmentsWidgetLeft = count;
                                break;
                            case "labWidget":
                                PatientData.LabTestWidgetRight = 0;
                                PatientData.LabTestWidgetLeft = count;
                                break;
                            case "visitswidg":
                                PatientData.VisitWidgetRight = 0;
                                PatientData.VisitWidgetLeft = count;
                                break;
                            case "widg2":
                                PatientData.MedicationWidgetRight = 0;
                                PatientData.MedicationWidgetLeft = count;
                                break;
                            case "probswidg":
                                PatientData.ProblemWidgetRight = 0;
                                PatientData.ProblemWidgetLeft = count;
                                break;
                            case "vitalwidg":
                                PatientData.VitalSignsWidgetRight = 0;
                                PatientData.VitalSignsWidgetLeft = count;
                                break;
                            case "allergywidg":
                                PatientData.AllergyWidgetRight = 0;
                                PatientData.AllergyWidgetLeft = count;
                                break;
                            case "Procedureswidg":
                                PatientData.ProcedureWidgetRight = 0;
                                PatientData.ProcedureWidgetLeft = count;
                                break;
                            case "PremiumWidget":
                                PatientData.PremiumWidgetRight = 0;
                                PatientData.PremiumWidgetLeft = count;
                                break;
                            case "socialhistwidg":
                                PatientData.SocialWidgetRight = 0;
                                PatientData.SocialWidgetLeft = count;
                                break;
                            case "SocailHistorySelf":
                                PatientData.SocialWidgetRight = 0;
                                PatientData.SocialWidgetLeft = count;

                                break;
                            case "SocialHist-Div":
                                PatientData.SocialWidgetRight = 0;
                                PatientData.SocialWidgetLeft = count;
                                break;
                            case "widg4":
                                PatientData.StatementWidgetRight = 0;
                                PatientData.StatementWidgetLeft = count;
                                break;
                            case "familyhistwidg":
                                PatientData.FamilyWidgetRight = 0;
                                PatientData.FamilyWidgetLeft = count;
                                break;
                            case "Pmedicalhistwidg":
                                PatientData.PastMedicalWidgetRight = 0;
                                PatientData.PastMedicalWidgetLeft = count;
                                break;
                            case "immunizwidg":
                                PatientData.ImmunizationWidgetRight = 0;
                                PatientData.ImmunizationWidgetLeft = count;
                                break;
                            case "Documentswidg":
                                PatientData.DocumentWidgetRight = 0;
                                PatientData.DocumentWidgetLeft = count;
                                break;
                            case "Insurancewidg":
                                PatientData.InsuranceWidgetRight = 0;
                                PatientData.InsuranceWidgetLeft = count;
                                break;
                            case "pocwidg":
                                PatientData.PlanOfCareWidgetRight = 0;
                                PatientData.PlanOfCareWidgetLeft = count;
                                break;
                            case "ClinicalInstructionsWidget":
                                PatientData.ClinicalInstructionsWidgetRight = 0;
                                PatientData.ClinicalInstructionsWidgetLeft = count;
                                break;
                            case "ProviderWidget":
                                PatientData.ProviderWidgetRight = 0;
                                PatientData.ProviderWidgetLeft = count;
                                break;     
                        }
                        count++;
                    }
                }
                else
                {
                    short counter = 1;
                    foreach (string word in widgetname)
                    {
                        switch (word)
                        {
                            case "widg1":
                                PatientData.AppointmentsWidgetLeft = 0;
                                PatientData.AppointmentsWidgetRight = counter;
                                break;
                            case "widg3":
                                PatientData.LabTestWidgetLeft = 0;
                                PatientData.LabTestWidgetRight = counter;
                                break;
                            case "visitswidg":
                                PatientData.VisitWidgetLeft = 0;
                                PatientData.VisitWidgetRight = counter;
                                break;
                            case "widg2":
                                PatientData.MedicationWidgetLeft = 0;
                                PatientData.MedicationWidgetRight = counter;
                                break;
                            case "probswidg":
                                PatientData.ProblemWidgetLeft = 0;
                                PatientData.ProblemWidgetRight = counter;
                                break;
                            case "vitalwidg":
                                PatientData.VitalSignsWidgetLeft = 0;
                                PatientData.VitalSignsWidgetRight = counter;
                                break;
                            case "allergywidg":
                                PatientData.AllergyWidgetLeft = 0;
                                PatientData.AllergyWidgetRight = counter;
                                break;
                            case "Procedureswidg":
                                PatientData.ProcedureWidgetLeft = 0;
                                PatientData.ProcedureWidgetRight = counter;
                                break;
                            case "PremiumWidget":
                                PatientData.PremiumWidgetLeft = 0;
                                PatientData.PremiumWidgetRight = counter;
                                break;
                            case "socialhistwidg":
                                PatientData.SocialWidgetLeft = 0;
                                PatientData.SocialWidgetRight = counter;
                                break;
                            case "SocialHist-Div":
                                PatientData.SocialWidgetRight = counter;
                                PatientData.SocialWidgetLeft = 0 ;
                                break;
                         
                            case "widg4":
                                PatientData.StatementWidgetLeft = 0;
                                PatientData.StatementWidgetRight = counter;
                                break;
                            case "familyhistwidg":
                                PatientData.FamilyWidgetLeft = 0;
                                PatientData.FamilyWidgetRight = counter;
                                break;
                            case "Pmedicalhistwidg":
                                PatientData.PastMedicalWidgetLeft = 0;
                                PatientData.PastMedicalWidgetRight = counter;
                                break;
                            case "immunizwidg":
                                PatientData.ImmunizationWidgetLeft = 0;
                                PatientData.ImmunizationWidgetRight = counter;
                                break;
                            case "Documentswidg":
                                PatientData.DocumentWidgetRight = counter;
                                PatientData.DocumentWidgetLeft = 0;
                                break;
                            case "Insurancewidg":
                                PatientData.InsuranceWidgetRight = counter;
                                PatientData.InsuranceWidgetLeft = 0;
                                break;
                            case "pocwidg":
                                PatientData.PlanOfCareWidgetRight = counter;
                                PatientData.PlanOfCareWidgetLeft = 0;
                                break;
                            case "ClinicalInstructionsWidget":
                                PatientData.ClinicalInstructionsWidgetRight = counter;
                                PatientData.ClinicalInstructionsWidgetLeft = 0;
                                break;
                            case "ProviderWidget":
                                PatientData.ProviderWidgetRight = counter;
                                PatientData.ProviderWidgetLeft = 0;
                                break;
                        }
                        counter++;
                    }
                }
                PatientData = service.SavePatientWebSettingData(PatientData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            }
            string success = Convert.ToString(count);
            return Json(success);
        }

        [HttpPost]
        public JsonResult WidgetIdDelete(string WidgetName, string action, string highestvalue)
        {
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                short count = 0;
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                DataPatientService.PatientWebSettingData PatientData = new DataPatientService.PatientWebSettingData();
                PatientData = service.GetPatientWebSettingData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (action == "hide")
                {
                    switch (WidgetName)
                    {
                        case "widg1":
                            PatientData.AppointmentsWidgetLeft = count;
                            PatientData.AppointmentsWidgetRight = count;
                            break;
                        case "labWidget":
                            PatientData.LabTestWidgetLeft = count;
                            PatientData.LabTestWidgetRight = count;
                            break;
                        case "visitswidg":
                            PatientData.VisitWidgetLeft = count;
                            PatientData.VisitWidgetRight = count;
                            break;
                        case "widg2":
                            PatientData.MedicationWidgetLeft = count;
                            PatientData.MedicationWidgetRight = count;
                            break;
                        case "probswidg":
                            PatientData.ProblemWidgetLeft = count;
                            PatientData.ProblemWidgetRight = count;
                            break;
                        case "vitalwidg":
                            PatientData.VitalSignsWidgetLeft = count;
                            PatientData.VitalSignsWidgetRight = count;
                            break;
                        case "allergywidg":
                            PatientData.AllergyWidgetLeft = count;
                            PatientData.AllergyWidgetRight = count;
                            break;
                        case "Procedurewidg":
                            PatientData.ProcedureWidgetLeft = count;
                            PatientData.ProcedureWidgetRight = count;
                            break;
                        case "PremiumWidget":
                            PatientData.PremiumWidgetLeft = count;
                            PatientData.PremiumWidgetRight = count;
                            break;
                        case "socialhistwidg":
                            PatientData.SocialWidgetLeft = count;
                            PatientData.SocialWidgetRight = count;
                            break;
                        case "widg4":
                            PatientData.StatementWidgetLeft = count;
                            PatientData.StatementWidgetRight = count;
                            break;
                        case "familyhistwidg":
                            PatientData.FamilyWidgetLeft = count;
                            PatientData.FamilyWidgetRight = count;
                            break;
                        case "Pmedicalhistwidg":
                            PatientData.PastMedicalWidgetLeft = count;
                            PatientData.PastMedicalWidgetRight = count;
                            break;
                        case "immunizwidg":
                            PatientData.ImmunizationWidgetLeft = count;
                            PatientData.ImmunizationWidgetRight = count;
                            break;
                        case "Documentswidg":
                            PatientData.DocumentWidgetRight = count;
                            PatientData.DocumentWidgetLeft = count;
                            break;
                        case "Insurancewidg":
                            PatientData.InsuranceWidgetRight = count;
                            PatientData.InsuranceWidgetLeft = count;
                            break;
                        case "pocwidg":
                            PatientData.PlanOfCareWidgetRight = count;
                            PatientData.PlanOfCareWidgetLeft = count;
                            break;
                        case "ClinicalInstructionsWidget":
                            PatientData.ClinicalInstructionsWidgetRight = count;
                            PatientData.ClinicalInstructionsWidgetLeft = count;
                            break;

                    }
                }
                else
                {
                    //if (highestvalue == "")
                    //    highestvalue = "0";
                    short value = 15;
                    switch (WidgetName)
                    {
                        case "widg1":
                            PatientData.AppointmentsWidgetLeft = value;
                            break;
                        case "labWidget":
                            PatientData.LabTestWidgetLeft = value;
                            break;
                        case "visitswidg":
                            PatientData.VisitWidgetLeft = value;
                            break;
                        case "widg2":
                            PatientData.MedicationWidgetLeft = value;
                            break;
                        case "probswidg":
                            PatientData.ProblemWidgetLeft = value;
                            break;
                        case "vitalwidg":
                            PatientData.VitalSignsWidgetLeft = value;
                            break;
                        case "allergywidg":
                            PatientData.AllergyWidgetLeft = value;
                            break;
                        case "Procedurewidg":
                            PatientData.ProcedureWidgetLeft = value;
                            break;
                        case "PremiumWidget":
                            PatientData.PremiumWidgetLeft = value;
                            break;
                        case "socialhistwidg":
                            PatientData.SocialWidgetLeft = value;
                            break;
                        case "widg4":
                            PatientData.StatementWidgetLeft = value;
                            break;
                        case "familyhistwidg":
                            PatientData.FamilyWidgetLeft = value;
                            break;
                        case "Pmedicalhistwidg":
                            PatientData.PastMedicalWidgetLeft = value;
                            break;
                        case "immunizwidg":
                            PatientData.ImmunizationWidgetLeft = value;
                            break;
                        case "Documentswidg":
                            PatientData.DocumentWidgetLeft = value;
                            break;
                        case "Insurancewidg":
                            PatientData.InsuranceWidgetLeft = value;
                            break;
                        case "pocwidg":
                            PatientData.PlanOfCareWidgetLeft = value;
                            break;
                        case "ClinicalInstructionsWidget":
                            PatientData.ClinicalInstructionsWidgetLeft = value;
                            break;
                    }
                }
                PatientData = service.SavePatientWebSettingData(PatientData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            }
            string success = "";
            return Json(success);
        
        }


        public ActionResult PatientClinicalDocumentAttachment(string patientDocumentId,string VisitId,string FacilityId,string Menu)
        {
            try
            {

                FacilityVisitSelectModel smodel;
                smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
                //Result...
                var result = new GeneralDocumentModel();

                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientClinicalDocumentParms ParamPatClin = new DataPatientDocumentService.PatientClinicalDocumentParms();
                    ParamPatClin.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    if (Menu == "Menu")
                    {

                        ParamPatClin.FacilityId =long.Parse(FacilityId);
                        ParamPatClin.VisitId = long.Parse(VisitId);
                    }
                    else
                    {
                        ParamPatClin.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                        ParamPatClin.VisitId = Convert.ToInt64(smodel.visitSelected);
                    }
                    ParamPatClin.DocumentCntr = long.Parse(patientDocumentId);
                    ParamPatClin.Option = 1;
                    DataPatientDocumentService.PatientClinicalDocumentData DataPatClin = service.GetPatientClinicalDocumentData(ParamPatClin, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (DataPatClin.Valid)
                    {
                        result = base.ConvertToPatientClinicalDocumentModel(DataPatClin);
                    }
                }

                if (!System.IO.File.Exists(result.FileDirectory + "\\" + result.DocumentCntr + "." + result.DocumentFormat))
                {
                    return PartialView("_FileNotFound");
                }
                switch (result.DocumentFormat.ToLower())
                {
                    case "jpg":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".jpg", "image/jpeg");
                        break;

                    case "jpeg":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".jpeg", "image/jpeg");
                        break;

                    case "png":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".png", "image/png");
                        break;

                    case "bmp":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".bmp", "image/bmp");
                        break;
                    case "tif":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".tif", "image/tiff");
                        break;
                    case "pdf":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".pdf", "application/pdf");
                        break;

                    case "doc":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".doc", "application/msword");
                        break;

                    case "docx":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".docx", "application/msword");
                        break;

                    case "xls":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".xls", "application/vnd.ms-excel");
                        break;

                    case "xlsx":
                        return File(result.FileDirectory + "\\" + result.DocumentCntr + ".xlsx", "application/vnd.ms-excel");
                        break;


                    default:
                        return PartialView("_AttachmentSupport");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult CCDLogin()
        {
            return View();
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult CCDLocation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CCDLogin( UserModel model,string urlreturn)
        {

            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();


            objGlobalVar.FacilityId = 0;
            objGlobalVar.UserLogin = model.UserLogin;
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();

            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                     ResponseData = service.AuthenticateInterface(objGlobalVar.FacilityId, model.UserLogin, model.Password);
                     if (ResponseData.Valid)
                    {
                        objGlobalVar.UserId = ResponseData.UserId;
                        objGlobalVar.Token = ResponseData.Token;
                        objGlobalVar.PatientId = Convert.ToInt64(model.UserLogin);
                        if (ResponseData.LoginType == 5)
                        {
                            objGlobalVar.PortalType = "Patient";
                        }
                        else if (ResponseData.LoginType == 4)
                        {
                            objGlobalVar.PortalType = "Provider";
                        }

                        Session.Add("UserId", ResponseData.UserId);
                        Session["LoginType"] = "Accessibility";
                    }
                    else
                    {
                        objGlobalVar.Token = "";
                        objGlobalVar.UserId = 0;
                        model.ErrorMessage = ResponseData.ErrorMessage;
                    }

                    if ((ResponseData.LoginType == 5 || ResponseData.LoginType == 6) && objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                         {
                                TicketHelper.CreateAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true);

                                return RedirectToAction("CCDLocation", "Home", new { Login = ResponseData.FirstLogin });
                            }
                   else
                            {
                                objGlobalVar.Token = "";
                                objGlobalVar.UserId = 0;
                                model.ErrorMessage = "Invalid Login for Patient portal";
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

        [HttpPost]
        public JsonResult ConsolidateCallHomeWidgetsFilter(ConsolidateCallModel Model) 
        {
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
        {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }
            HomeViewModel HomeLists = new HomeViewModel();
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    //Visit Data...
                    DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                    DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                    PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatVisitParms.FacilityId = Convert.ToInt64(Model.facilitySelected);
                    PatVisitParms.VisitId = Convert.ToInt64(Model.visitSelected);
                    PatVisitParms.Option = 1;
                    VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (VisitData.Valid)
                    {
                        HomeLists.Visits = VisitData.dt.ToVisitModelList();
                    }
                }
                //get Patientsocialhistself
                if (smodel.facilitySelected == "0")
                {
                    
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                        DataPatientDocumentService.PatientDocParms patdata = new DataPatientDocumentService.PatientDocParms();
                        DataPatientDocumentService.PatientSocialSelfData socialselfdata = new DataPatientDocumentService.PatientSocialSelfData();
                        DataPatientDocumentService.PatientDocTableData patdoctable = new DataPatientDocumentService.PatientDocTableData();
                        patdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        socialselfdata = service.GetPatientSocialSelfData(patdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (socialselfdata.Valid)
                        {
                            HomeLists.PatientSocialSelf = base.ConvertToPatientSocialSelfDataModel(socialselfdata);
                        }


                    }
                    DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
                    DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
                    using (var service = new DataPatientService.PatientWSSoapClient())
                    {
                        PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatShare = service.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        HomeLists.PatientShare = base.ConvertToPatientShareModel(PatShare);
                    }
                }
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                    PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                    PatDocParms.VisitId = Model.visitId;
                    PatDocParms.FacilityId = Model.facilityId;
                    PatDocParms.Option = 1;
                    PatDocParms.Active = 1;
                    PatientDocument = service.GetPatientClinicalInstructionsData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        HomeLists.ClinicalInstructions = PatientDocument.dt.ToPOCModelList();
                        // ClinicalSummaryLists.ClinicalInstructions = PatientDocument.dt.ToPOCModelList();

                    }
                //    // consolidated call 
                //    DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
                //    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();

                //    parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                //    parms.VisitId = Model.visitId;
                //    parms.FacilityId = Model.facilityId;
                //    parms.Option = 1;
                //    parms.Active = 99;

                //    parms.FamilyHist = true;
                //    parms.SocialHist = true;
                //    parms.MedicalHist = true;
                //    parms.Problem = true;

                //    parms.VitalSign = true;
                //    parms.Medication = true;
                //    parms.Lab = true;
                //    parms.Immunization = true;
                //    parms.Allergy = true;
                //    parms.PlanOfCare = true;
                //    parms.ClinicalDocs = true;
                //    parms.Insurance = true;
                //    parms.Procedure = true;
                //    PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                //    if (PatientDocumentSummary.Valid)
                //    {
                //        HomeLists.SocialHistories = PatientDocumentSummary.dtSocialHist.ToSocialHistoryModelList();
                //        HomeLists.VitalSigns = PatientDocumentSummary.dtVitalSign.ToVitalsignModelList();
                //        HomeLists.MedicalHistories = PatientDocumentSummary.dtMedicalHist.ToMedicalHistoryModelList();
                //        HomeLists.Medications = PatientDocumentSummary.dtMedication.ToMedicationHistoryModelList();
                //        //HomeLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
                //        HomeLists.Problems = PatientDocumentSummary.dtProblem.ToProblemModelList();
                //        HomeLists.LabResults = PatientDocumentSummary.dtLab.ToLabResultModelList();
                //        HomeLists.Immunizations = PatientDocumentSummary.dtImmunization.ToImmunizationModelList();
                //        HomeLists.FamilyHistories = PatientDocumentSummary.dtFamilyHist.ToFamilyHistoryModelList();
                //        HomeLists.Allergies = PatientDocumentSummary.dtAllergy.ToAllergyModelList();
                //        HomeLists.Documents = PatientDocumentSummary.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                //       // HomeLists.Policies = PatientDocumentSummary.dtInsurance.ToPolicyModelList();
                //        HomeLists.Pocs = PatientDocumentSummary.dtPlanOfCare.ToPOCModelList();
                //        HomeLists.Insurance = PatientDocumentSummary.dtInsurance.ToInsuranceModelList();
                //        HomeLists.Procedures = PatientDocumentSummary.dtProcedure.ToProcedureModelList();
                      
                //    }
                }
                using (var service = new DataProviderService.ProviderWSSoapClient())
                {

                    DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
                    //   RequestHelper.MyGlobalVar.FacilityId
                    ProviderList = service.GetPatientProviderInfoList(RequestHelper.MyGlobalVar.PatientId, Model.facilityId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    HomeLists.ProivderInfo = ProviderList.dt.ToProivderInfoList();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            var HomeWidgetsList = new List<HomeWidgetModel>();

            if (smodel.facilitySelected == "0")
            {
                HomeWidgetsList.Add(new HomeWidgetModel
                {
                    Id = "SocialHist-Div",
                    WidgetHtml = ViewHelper.RenderRazorViewToString("_SocailHistorySelf_New", HomeLists, this)
                });
            }
            else
            {
                HomeWidgetsList.Add(new HomeWidgetModel
                {
                    Id = "SocialHist-Div",
                    //WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_SocialHistory", HomeLists.SocialHistories, this)
                    WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_SocialHistory_New", HomeLists, this)
                });
            }


            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "lab-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_LabTests", HomeLists.LabResults, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "Visits-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Visits", HomeLists.Visits, this)
            });


            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "family-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_FamilyHistory", HomeLists.FamilyHistories, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "medications-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Medication", HomeLists.Medications, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "pasthistory-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_MedicalHistory", HomeLists.MedicalHistories, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "problem-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Problems", HomeLists.Problems, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "vital-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_VitalSign", HomeLists.VitalSigns, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "allergies-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Allergies", HomeLists.Allergies, this)
            });


            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "immunization-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Immunization", HomeLists.Immunizations, this)
            });

            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "poc-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_PlanofCare", HomeLists.Pocs, this)
            });
            //Ahmed Come Back             
            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "ClinicalInstructions-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_ClinicalInstructions", HomeLists.ClinicalInstructions, this)
            });
            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "Insurance-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Insurance", HomeLists.Insurance, this)
            });
            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "Documents-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Documents", HomeLists.Documents, this)
            });
            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "Procedure-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Procedures", HomeLists.Procedures, this)
            });
            HomeWidgetsList.Add(new HomeWidgetModel
            {
                Id = "ProviderWidget-portlet",
                WidgetHtml = ViewHelper.RenderRazorViewToString("_Index_Provider", HomeLists.ProivderInfo, this)
            });
            return Json(HomeWidgetsList);
        }

        [HttpPost]
        public JsonResult VisitFill(ConsolidateCallModel Model)
        {
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }
            return Json("");
        }

        [HttpPost]
        public JsonResult FilterDownloadHealthRecord(PatientVisitDataModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            string html = string.Empty;

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

            PatParms.VisitId = Model.VisitId;
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatParms.Option = 1;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatientDocument = service.GetPatientVisitList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        ClinicalSummaryLists.Visits = PatientDocument.dt.ToVisitModelList();
                        //  html = ViewHelper.RenderRazorViewToString("~/Views/Shared/", ClinicalSummaryLists.Visits, this);


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
            return Json(html);
        }
        
        
        [HttpPost]
        public JsonResult FilterSendHealthRecord(PatientVisitDataModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            string html = string.Empty;

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

            PatParms.VisitId = Model.VisitId;
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            PatParms.Option = 1;
            try
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    PatientDocument = service.GetPatientVisitList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        ClinicalSummaryLists.Visits = PatientDocument.dt.ToVisitModelList();
                      //  html = ViewHelper.RenderRazorViewToString("~/Views/Shared/_VisitDropDown.cshtml", ClinicalSummaryLists.Visits, this);


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
            return Json(html);
        }


        [ChildActionOnly]
        [OutputCache(Duration = 30)]
        public PartialViewResult VisitPartialView()
        {

            GeneralViewModel model =  new GeneralViewModel();
              using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                HomeViewModel Homelists = new HomeViewModel();
                PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
              
                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    model.Visits = VisitData.dt.ToVisitModelList().ToVisitModelSelectList();
                    
                }

            }
            return PartialView("_VisitDropDown",model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 30)]
        public PartialViewResult VisitPartialViewDownloadHealthRecord()
        {

            GeneralViewModel model = new GeneralViewModel();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                HomeViewModel Homelists = new HomeViewModel();
                PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;

                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    model.Visits = VisitData.dt.ToVisitModelList().ToVisitModelSelectList();

                }

            }
            return PartialView("_VisitDropDown_DownloadHealthRecord", model);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        [ChildActionOnly]
        //[OutputCache(Duration = 30)]
        public PartialViewResult VisitPartialViewCCD()
        {
            Int64 facility = Convert.ToInt64(Request.QueryString["Location"]);

            GeneralViewModel model = new GeneralViewModel();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                HomeViewModel Homelists = new HomeViewModel();
                PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                PatVisitParms.Option = 2;
                PatVisitParms.FacilityId = facility;
                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    model.Visits = VisitData.dt.ToVisitModelList().ToVisitModelSelectList();

                }

            }
            return PartialView("_VisitDropDown_DownloadHealthRecord", model);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 30)]
        public PartialViewResult FacilityPartialView()
        {

            GeneralViewModel model = new GeneralViewModel();
              using (var service = new DataPatientService.PatientWSSoapClient())
            {
             
                HomeViewModel Homelists = new HomeViewModel();
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    model.Facilities = FacilityData.dt.ToFacilityModelList().ToFacilityModelSelectList();
                  
                }
            }
              return PartialView("_FacilityDropDown", model);
        }


        [ChildActionOnly]
        public PartialViewResult FacilityPartialViewDownloadHealthRecord()
        {

            GeneralViewModel model = new GeneralViewModel();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {

                HomeViewModel Homelists = new HomeViewModel();
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;  //RequestHelper.MyGlobalVar.PatientId;
                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    model.Facilities = FacilityData.dt.ToFacilityModelList().ToFacilityModelSelectList();

                }
            }
            return PartialView("_FacilityDropDown_DownloadHealthRecord", model);
        }

        [HttpPost]
        public JsonResult GetImmunizationList(string term)
        {
            HomeViewModel HomeLists = new HomeViewModel();

            try
            {
                //Configure Dropdowns Data
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {

                    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                    DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                    ConDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    ConDocParms.Option = 1;


                    ConfigDocument = service.GetVaccineCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId,term);
                    if (ConfigDocument.Valid)
                    {
                        HomeLists.Vaccines = ConfigDocument.dt.ToVaccineModelList();


                    }


                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(HomeLists.Vaccines, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetMedicationList(string term)
        {
            HomeViewModel HomeLists = new HomeViewModel();

            try
            {
                //Configure Dropdowns Data
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {

                    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                    DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                    ConDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    ConDocParms.Option = 1;


                    ConfigDocument = service.GetMedicationsCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, term);
                    if (ConfigDocument.Valid)
                    {
                        HomeLists.MedicationList = ConfigDocument.dt.ToMedicationlList();
                       
                              
                    }

                    
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(HomeLists.MedicationList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProblemList(string term)
        {
            HomeViewModel HomeLists = new HomeViewModel();

            try
            {
                //Configure Dropdowns Data
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {

                    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                    DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                    ConDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    ConDocParms.Option = 1;


                    ConfigDocument = service.GetProblemCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, term);
                    if (ConfigDocument.Valid)
                    {
                        HomeLists.ProblemsSNOMED = ConfigDocument.dt.ToProblemModeSNOMEDlList();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            return Json(HomeLists.ProblemsSNOMED, JsonRequestBehavior.AllowGet);

        }
        

        [HttpPost]
        public JsonResult SecuritySave(SecurityQuestionModel Model)
        {
            HomeViewModel HomeLists = new HomeViewModel();
            
        DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            try
            {
            using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
            {
                ResponseData = service.ChangeSecurityQuestion(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 5, Convert.ToInt32(Model.SecurityQuestionId), Model.SecurityAnswer, Convert.ToInt32(Model.SecurityQuestionId2), Model.SecurityAnswer2,RequestHelper.MyGlobalVar.UserLogin.ToString());
                ResponseData = service.ChangePassword(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 5, Model.Password, Model.NewPassword);
                if (ResponseData.Valid)
                {
                    //Session["FirstLogin"] = false;
                    base.PopulateSecurityPatient();
                   
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
          [HttpPost]
        public JsonResult SecuritySaveProvider(SecurityQuestionModel Model)
        {

            HomeViewModel HomeLists = new HomeViewModel();
            GlobalVar objGlobalVar = new GlobalVar();

            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();
            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {
                    ResponseData = service.ChangeSecurityQuestion(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 4, Convert.ToInt32(Model.SecurityQuestionId), Model.SecurityAnswer, Convert.ToInt32(Model.SecurityQuestionId2), Model.SecurityAnswer2, RequestHelper.MyGlobalVar.UserLogin.ToString());
                    //Session["ProviderFlag"] = ResponseData.FirstLogin;
                    ResponseData = service.ChangePassword(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, 4, Model.Password, Model.NewPassword);
                    if (ResponseData.Valid)
                    {
                        base.PopulateSecuirtyProvider();
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

        [HttpPost]
        public JsonResult LabTestSave(LabResultModel Model)
        {
            
          //  HomeViewModel HomeLists = new HomeViewModel();
            List<LabResultModel> Labresult = new List<LabResultModel>();
            
            Model.VisitId = 0;
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientLabResultData pPatientLabResultData = base.ConvertToPatietntLabResultData(Model);
            DataPatientDocumentService.PatientLabResultData pPatientLabResultDataReturn = new DataPatientDocumentService.PatientLabResultData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    pPatientLabResultDataReturn = service.SavePatientLabResultData(pPatientLabResultData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pPatientLabResultDataReturn.Valid)
                    {
                        DataPatientDocumentService.PatientLabResultParms PatDocParms = new DataPatientDocumentService.PatientLabResultParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientLabResultList(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (pPatientLabResultDataReturn.Valid)
                        {


                            Labresult = PatientDocument.dt.ToPatientLabResultModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            if (Model.Flag == "labWidg")
                html = ViewHelper.RenderRazorViewToString("_Index_LabTests", Labresult, this);
            else if (Model.Flag == "labTab")
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_LabTests.cshtml", Labresult, this);
            return Json(html);

        }

        [HttpPost]
        public JsonResult AllergiesSave(AllergyModel Model)
        {
          //  HomeViewModel HomeLists = new HomeViewModel();
            List<AllergyModel> alergymodel = new List<AllergyModel>();
            
            Model.VisitId = 0;
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientAllergyData pPatientAllergyData = base.ConvertToPatientAllergyDataData(Model);
            DataPatientDocumentService.PatientAllergyData pPatientAllergyDataReturn = new DataPatientDocumentService.PatientAllergyData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    pPatientAllergyDataReturn = service.SavePatientAllergyData(pPatientAllergyData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pPatientAllergyDataReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientAllergyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (pPatientAllergyDataReturn.Valid)
                        {


                            alergymodel = PatientDocument.dt.ToPatientAllergyModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];
          //  if (Model.Flag == "allergyWidg")
                //html[0] = HomeLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_Allergies", alergymodel, this);
          //  else if (Model.Flag == "allergyTab")
                //html[1] = HomeLists.Allergies.GetPatienAllergyModelListHTMLForTab();
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Allergies.cshtml", alergymodel, this);

            return Json(html);

        }

        [HttpPost]
        public JsonResult VisitSave(PatientVisitDataModel Model)
        {
            HomeViewModel HomeList = new HomeViewModel();
            
            Model.VisitId = 0;
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;
            try
            {

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;

            html = "";// HomeList.VitalSigns.GetPatientVitalSignModelListHTMLForDashboard();

            return Json(html);

        }

        [HttpPost]
        public JsonResult PastMedicalSave(MedicalHistoryModel Model)
        {
          //  HomeViewModel HomeList = new HomeViewModel();
            List<MedicalHistoryModel> MedicalHistory = new List<MedicalHistoryModel>();
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.VisitId = 0;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientMedicalHistData pMedicalHistory = base.ConvertToPatientMedicalHistoryData(Model);
            DataPatientDocumentService.PatientMedicalHistData pMedicalHistoryReturn = new DataPatientDocumentService.PatientMedicalHistData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pMedicalHistoryReturn = service.SavePatientMedicalHistData(pMedicalHistory, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pMedicalHistoryReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientMedicalHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            MedicalHistory = PatientDocument.dt.ToPatientMedicalHistoryModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            string[] html = new string[2];


          //  if (Model.Flag == "pastWidg")
                //html[0] = HomeList.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_MedicalHistory", MedicalHistory, this);
          //  else if (Model.Flag == "pastTab")
                //html[1] = HomeList.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Medical.cshtml", MedicalHistory, this);

            return Json(html);

        }

        
        [HttpPost]
    public JsonResult SavePOCData(POCModel Model)
        {
          //  ClinicalSummaryViewModel SummaryList = new ClinicalSummaryViewModel();
            List<POCModel> POCModel = new List<Models.POCModel>();
            DataPatientDocumentService.PatientPlanOfCareData patplandata=new DataPatientDocumentService.PatientPlanOfCareData();
            DataPatientDocumentService.PatientPlanOfCareData patplandatareturn = new DataPatientDocumentService.PatientPlanOfCareData();

            DataPatientDocumentService.PatientDocTableData PatDocTable = new DataPatientDocumentService.PatientDocTableData();
            patplandata.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            patplandata.VisitId = 0; 
            patplandata.Instruction=Model.Instruction;
            patplandata.InstructionTypeId=Model.InstructionTypeId;
            CultureInfo provider = CultureInfo.InvariantCulture;
            //1900-01-01 00:00:00
            patplandata.AppointmentDateTime = Convert.ToDateTime(Model.AppointmentDateTime);
                //DateTime.ParseExact("yyyy-mm-dd hh:mm:ss", Model.AppointmentDateTime.ToString(), provider);   //
            patplandata.Goal=Model.Goal;
            patplandata.Note=Model.Note;
            patplandata.PatientId=RequestHelper.MyGlobalVar.PatientId;
            patplandata.PlanCntr = Model.PlanCntr;
          
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    patplandatareturn = service.SavePatientPlanOfCareData(patplandata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (patplandatareturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatDocTable = service.GetPatientPlanOfCareData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatDocTable.Valid)
                        {
                            POCModel = PatDocTable.dt.ToPOCModelList();
                        }
                      }
                   
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html, html1 = string.Empty;

            html = ViewHelper.RenderRazorViewToString("_Index_PlanofCare", POCModel, this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_PlanOfCare.cshtml", POCModel, this);
            return Json(new { html=html,html1=html1});
        }

         [HttpPost]
        public JsonResult SaveClinicalInstructionData(POCModel Model)
        {
            ClinicalSummaryViewModel SummaryList = new ClinicalSummaryViewModel();
            List<POCModel> POCModel = new List<Models.POCModel>();
            DataPatientDocumentService.PatientPlanOfCareData patplandata=new DataPatientDocumentService.PatientPlanOfCareData();
            DataPatientDocumentService.PatientPlanOfCareData patplandatareturn = new DataPatientDocumentService.PatientPlanOfCareData();

            DataPatientDocumentService.PatientDocTableData PatDocTable = new DataPatientDocumentService.PatientDocTableData();
            patplandata.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            patplandata.VisitId = 0; 
            patplandata.Instruction=Model.Instruction;
            patplandata.InstructionTypeId = 4; //Model.InstructionTypeId;
            patplandata.AppointmentDateTime=Convert.ToDateTime(Model.AppointmentDateTime);
            patplandata.Goal=Model.Goal;
            patplandata.Note=Model.Note;
            patplandata.PlanCntr = Model.PlanCntr;
            patplandata.PatientId=RequestHelper.MyGlobalVar.PatientId;
          
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    patplandatareturn = service.SavePatientPlanOfCareData(patplandata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (patplandatareturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatDocTable = service.GetPatientClinicalInstructionsData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatDocTable.Valid)
                        {
                            POCModel = PatDocTable.dt.ToPOCModelList();
                        }
                      }
                   
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html, html1 = string.Empty;

            html = ViewHelper.RenderRazorViewToString("_Index_ClinicalInstructions", POCModel, this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_ClinicalInstructions.cshtml", POCModel, this);
            return Json(new { html=html,html1=html1});
        }

        

        [HttpPost]
        public JsonResult VitalSave(PatientVitalSignModel Model)
        {
           // HomeViewModel HomeList = new HomeViewModel();
            List<PatientVitalSignModel> ViewModel = new List<PatientVitalSignModel>();
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.VisitId = 0;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientVitalSignData pVitalSign = base.ConvertToPatientVitalSignData(Model);
            DataPatientDocumentService.PatientVitalSignData pVitalSignReturn = new DataPatientDocumentService.PatientVitalSignData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pVitalSignReturn = service.SavePatientVitalSignData(pVitalSign, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pVitalSignReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatientDocument = service.GetPatientVitalSignData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            ViewModel = PatientDocument.dt.ToPatientVitalSignModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];


           // if (Model.Flag == "vitalWidg")
                //html[0] = HomeList.VitalSigns.GetPatientVitalSignModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_VitalSign", ViewModel, this);
           // else if (Model.Flag == "vitalTab")
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Vitals.cshtml", ViewModel, this);




            return Json(html);
        }

        [HttpGet]
        [AnotherSessionRedirectionFilter] [Authorize]
        public FileResult  CCDHtmlDownload(string healthRecordData)
        {
            HomeViewModel HomeLists = new HomeViewModel();
            bool customize = false;
            DataSet CCDds = new DataSet();
            if (Request.Cookies["Cunstomize"] != null)
            {
                string CCDStr = Request.Cookies["Cunstomize"].Value.ToString();
                CCDds = utility.GetDataSetFromString(CCDStr);
                HttpCookie myCookie = new HttpCookie("Cunstomize");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                customize = true;
            }

            PatientCCD Model = new JavaScriptSerializer().Deserialize<PatientCCD>(healthRecordData);
            DataPatientDocumentService.PatientCCDResult PatientDocument = new DataPatientDocumentService.PatientCCDResult();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    DataPatientDocumentService.CCDParms PatCCDParms = new DataPatientDocumentService.CCDParms();
                    PatCCDParms.Custom = customize;
                    PatCCDParms.Demographics = Model.Demographics;
                    PatCCDParms.Provider = Model.Provider;
                    PatCCDParms.Allergies = true; // Model.Allergies;
                    //PatCCDParms.ClinicalInstructions = true; // Model.ClinicalInstructions;
                    //PatCCDParms.DecisionAids = true; // Model.DecisionAids;
                    //PatCCDParms.FutureAppointments = true; //Model.FutureAppointments;
                    PatCCDParms.Immunizations = true; //Model.Immunizations;
                    PatCCDParms.Labs = true;  //Model.Labs;
                    PatCCDParms.Medications = true;  //Model.Medications;
                    PatCCDParms.MedsAdministered = true;
                    PatCCDParms.Problems = true;  //Model.Problems;
                    //PatCCDParms.Referrals = true;  //Model.Referrals;
                    //PatCCDParms.ScheduledTests = true;  //Model.ScheduledTests;
                    PatCCDParms.PlanOfCare = true;
                    PatCCDParms.PatientId = long.Parse(RequestHelper.MyGlobalVar.UserLogin);
                    PatCCDParms.FacilityId = Model.FacilityID;
                    PatCCDParms.VisitId = Model.VisitID;
                    PatCCDParms.VisitReason = true;
                    PatCCDParms.Social = true;  //Model.SocialHistory;
                    PatCCDParms.VitalSigns = true; //Model.VitalSigns;
                    PatCCDParms.VisitReason = true;
                    PatCCDParms.Procedures = true;  //Model.Procedures;
                    PatCCDParms.Selection = CCDds;
                    short Action = 0;
                    if (Model.View == true)
                    {
                        Action = 3;
                    }
                    else
                    {
                        Action = 6;
                    }
                    //Needs to changes
                    PatientDocument = service.GetPatientCCD(PatCCDParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, Action);
                }
            }
            catch (Exception ex)
            {
               // return Json(ex.Message);
            }

            string xml = "";
            if (PatientDocument.Valid)
            {
                if (!string.IsNullOrEmpty(PatientDocument.CCD))
                {
                    xml = PatientDocument.CCD.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                    //TempData["Xml"] = xml;
                }
            }

            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = Model.FacilityID;
            VisitParams.VisitId = Model.VisitID;
            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
          //  if (Model.Provider == true)
           // {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    VisitData = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (VisitData.Valid)
                    {
                        HomeLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(VisitData);
                    }
                }
          //  }
          //  if (Model.Demographics == true)
         //   {
                using (var Summservice = new DataPatientService.PatientWSSoapClient())
                {

                    PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientSummary.Valid)
                    {

                        HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                }
       //     }
            HomeLists.PatientCCDownload = Model;
            HomeLists.PatientCCDownload.Customize = customize;
           // string XML = TempData["Xml"].ToString();
            List<HomeWidgetModel> widgts = new List<HomeWidgetModel>();
            widgts = utility.ConvertToHTML(xml);

            //widgts.Add(new HomeWidgetModel
            //{
            //    Id = "patientSummary",
            //    WidgetHtml = ViewHelper.RenderRazorViewToString("_CCD_Patient", HomeLists, this)
            //});
            HomeLists.widgets = widgts;
            string html = ViewHelper.RenderRazorViewToString("_CCD_Download_HTML", HomeLists, this);
            //return Json(widgts, JsonRequestBehavior.AllowGet);
            byte[] array = Encoding.ASCII.GetBytes(html);
           // 


            //FileInfo info = new FileInfo("HTML Download.htm");
            //if (!info.Exists)
            //{
            //    using (StreamWriter writer = info.CreateText())
            //    {
            //        writer.Write(html);
            //        //WriteLine("Hello, I am a new text file");

            //    }
            //}

            var stream = new MemoryStream(array);
            return File(stream, "text/html", "CCD (Human Readable).htm");
           // return File(info.OpenRead(), "text/html");
            //return File(html, "text/html", "CCD-HTML-Download");
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult GetCCDRun(string healthRecordData)//PatientCCD Model)
        {
            HomeViewModel HomeList = new HomeViewModel();
            
            bool customize = false;
            DataSet CCDds = new DataSet();
            if (Request.Cookies["Cunstomize"] != null)
            {
                string CCDStr = Request.Cookies["Cunstomize"].Value.ToString();
                CCDds = utility.GetDataSetFromString(CCDStr);
                HttpCookie myCookie = new HttpCookie("Cunstomize");
                //myCookie.Domain = "www.amrportal.com";
                myCookie.Expires = DateTime.Now.AddDays(-7d);
                Response.Cookies.Add(myCookie);
                customize = true;
            }
            PatientCCD Model = new JavaScriptSerializer().Deserialize<PatientCCD>(healthRecordData);
            Session["healthRecordData"] = healthRecordData;
            TempData["VisitId"] = Model.VisitID;
            TempData["FacilityId"] = Model.FacilityID;
            TempData["Demographics"] = Model.Demographics;
            TempData["Provider"] = Model.Provider;
            TempData["Visit"] = Model.Visit;
            DataPatientDocumentService.PatientCCDResult PatientDocument = new DataPatientDocumentService.PatientCCDResult();
                try
                {
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                   
                            DataPatientDocumentService.CCDParms PatCCDParms = new DataPatientDocumentService.CCDParms();

                            PatCCDParms.Custom = customize;
                            PatCCDParms.Demographics = Model.Demographics;
                            PatCCDParms.Provider = Model.Provider;
                            PatCCDParms.Allergies = true; //Model.Allergies;
                            //PatCCDParms.ClinicalInstructions = true; //Model.ClinicalInstructions;
                            //PatCCDParms.DecisionAids = true; //Model.DecisionAids;
                            //PatCCDParms.FutureAppointments = true; //Model.FutureAppointments;
                            PatCCDParms.Immunizations = true; //Model.Immunizations;
                            PatCCDParms.Labs = true; //Model.Labs;
                            PatCCDParms.Medications = true; //Model.Medications;
                            PatCCDParms.MedsAdministered = true;
                            PatCCDParms.Problems = true; //Model.Problems;
                            //PatCCDParms.Referrals = true; //Model.Referrals;
                            //PatCCDParms.ScheduledTests = true; //Model.ScheduledTests;
                            PatCCDParms.PlanOfCare = true;
                            PatCCDParms.PatientId = long.Parse(RequestHelper.MyGlobalVar.UserLogin);
                            PatCCDParms.FacilityId = Model.FacilityID;
                            PatCCDParms.VisitId = Model.VisitID;
                            PatCCDParms.Social = true; //Model.SocialHistory;
                            PatCCDParms.VitalSigns = true; //Model.VitalSigns;
                            PatCCDParms.VisitReason = true;
                            PatCCDParms.Procedures = true; //Model.Procedures;
                            PatCCDParms.Selection = CCDds;
                            short Action = 0;
                            if (Model.View == true)
                            {
                                Action = 3;
                            }
                            else
                            {
                                Action = 6;
                            }
                        //Needs to changes
                            PatientDocument = service.GetPatientCCD(PatCCDParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, Action);
                    }
                }
                catch (Exception ex)
                {
                    return Json(ex.Message);
                }

                string xml = "";
                if (PatientDocument.Valid)
                {
                    if (!string.IsNullOrEmpty(PatientDocument.CCD))
                    {
                        xml = PatientDocument.CCD.Replace("encoding=\"utf-16\"","encoding=\"utf-8\"");
                        TempData["Xml"] = xml;
                    }
                }

                return RedirectToAction("CCD", "Home", new { Customize = customize });
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult GetCCDDataXML(string healthRecordData)
        {

            bool customize = false;
            DataSet CCDds = new DataSet();
            if (Request.Cookies["Cunstomize"] != null)
            {
                string CCDStr = Request.Cookies["Cunstomize"].Value.ToString();
                CCDds = utility.GetDataSetFromString(CCDStr);
                HttpCookie myCookie = new HttpCookie("Cunstomize");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                customize = true;
            }
            PatientCCD Model = new JavaScriptSerializer().Deserialize<PatientCCD>(healthRecordData);
            
            DataPatientDocumentService.PatientCCDResult PatientDocument = new DataPatientDocumentService.PatientCCDResult();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    DataPatientDocumentService.CCDParms PatCCDParms = new DataPatientDocumentService.CCDParms();
                    PatCCDParms.Custom = customize;
                    PatCCDParms.Demographics = Model.Demographics;
                    PatCCDParms.Provider = Model.Provider;
                    PatCCDParms.Allergies = true; //Model.Allergies;
                    //PatCCDParms.ClinicalInstructions = true; //Model.ClinicalInstructions;
                    //PatCCDParms.DecisionAids = true; //Model.DecisionAids;
                    //PatCCDParms.FutureAppointments = true; //Model.FutureAppointments;
                    PatCCDParms.Immunizations = true; //Model.Immunizations;
                    PatCCDParms.Labs = true; //Model.Labs;
                    PatCCDParms.Medications = true; //Model.Medications;
                    PatCCDParms.MedsAdministered = true;
                    PatCCDParms.Problems = true; //Model.Problems;
                    //PatCCDParms.Referrals = true; //Model.Referrals;
                    //PatCCDParms.ScheduledTests = true; //Model.ScheduledTests;
                    PatCCDParms.PlanOfCare = true;
                    PatCCDParms.PatientId = long.Parse(RequestHelper.MyGlobalVar.UserLogin);
                    PatCCDParms.FacilityId = Model.FacilityID;
                    PatCCDParms.VisitId = Model.VisitID;
                    PatCCDParms.Social = true; //Model.SocialHistory;
                    PatCCDParms.VitalSigns = true; //Model.VitalSigns;
                    PatCCDParms.VisitReason = true;
                    PatCCDParms.Procedures = true; //Model.Procedures;
                    PatCCDParms.Selection = CCDds;
                    short Action = 0;
                    if (Model.View == true)
                    {
                        Action = 3;
                    }
                    else
                    {
                        Action = 6;
                    }
                   
                    PatientDocument = service.GetPatientCCD(PatCCDParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, Action);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string xml = "";
            if (PatientDocument.Valid)
            {
                if (!string.IsNullOrEmpty(PatientDocument.CCD))
                {
                    xml = PatientDocument.CCD.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                }
            }

            var fileContent = Encoding.UTF8.GetBytes(xml.ToString());
            var mimeType = "text/xml";
            var fileName = "CCD (CCDA XML).xml";//System.IO.Path.GetFileName(filePath);

            return File(fileContent, mimeType, fileName);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult SendCCD(PatientCCD Model)
        {
            //if()
            //string healthRecordData = Session["healthRecordData"].ToString();
          //  PatientCCD Model = new JavaScriptSerializer().Deserialize<PatientCCD>(healthRecordData);
            HomeViewModel HomeLists = new HomeViewModel();
            bool customize = false;
            DataSet CCDds = new DataSet();
            if (Request.Cookies["Cunstomize"] != null)
            {
                string CCDStr = Request.Cookies["Cunstomize"].Value.ToString();
                CCDds = utility.GetDataSetFromString(CCDStr);
                HttpCookie myCookie = new HttpCookie("Cunstomize");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
                customize = true;
            }
            DataPatientDocumentService.PatientCCDResult PatientDocument = new DataPatientDocumentService.PatientCCDResult();
            try
            {

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    DataPatientDocumentService.CCDParms PatCCDParms = new DataPatientDocumentService.CCDParms();
                    PatCCDParms.Custom = customize;
                    PatCCDParms.Demographics = Model.Demographics;
                    PatCCDParms.Provider = Model.Provider;
                    PatCCDParms.VisitReason = true;
                    PatCCDParms.Allergies = true; // Model.Allergies;
                    //PatCCDParms.ClinicalInstructions = true; // Model.ClinicalInstructions;
                    //PatCCDParms.DecisionAids = true; // Model.DecisionAids;
                    //PatCCDParms.FutureAppointments = true; //Model.FutureAppointments;
                    PatCCDParms.Immunizations = true; //Model.Immunizations;
                    PatCCDParms.Labs = true;  //Model.Labs;
                    PatCCDParms.Medications = true;  //Model.Medications;
                    PatCCDParms.MedsAdministered = true;
                    PatCCDParms.Problems = true;  //Model.Problems;
                    //PatCCDParms.Referrals = true;  //Model.Referrals;
                    //PatCCDParms.ScheduledTests = true;  //Model.ScheduledTests;
                    PatCCDParms.PlanOfCare = true;
                    PatCCDParms.PatientId = long.Parse(RequestHelper.MyGlobalVar.UserLogin);
                    PatCCDParms.FacilityId = Model.FacilityID;
                    PatCCDParms.VisitId = Model.VisitID;
                    PatCCDParms.Social = true;  //Model.SocialHistory;
                    PatCCDParms.VitalSigns = true; //Model.VitalSigns;
                    PatCCDParms.VisitReason = true;
                    PatCCDParms.Procedures = true;  //Model.Procedures;
                    PatCCDParms.Selection = CCDds;
                    short Action = 5;
                  //  if (Model.View == true)
                  //  {
                  //      Action = 3;
                  //  }
                   // else
                   // {
                  //      Action = 6;
                   // }
                    //Needs to changes
                    PatientDocument = service.GetPatientCCD(PatCCDParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId, Action);
                
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            string StringXml = "";
            if (PatientDocument.Valid)
            {
                if (!string.IsNullOrEmpty(PatientDocument.CCD))
                {
                    StringXml = PatientDocument.CCD.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                    //TempData["Xml"] = xml;
                }
            }
            string Stringhtml = "";
            if (Model.CCDHTML)
            {
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = Model.FacilityID;
            VisitParams.VisitId = Model.VisitID;
            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
               // if (Model.Provider == true)
               // {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    VisitData = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (VisitData.Valid)
                    {
                        HomeLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(VisitData);
                    }
                }
              //  }
        //    if (Model.Demographics == true)
        //    {
                using (var Summservice = new DataPatientService.PatientWSSoapClient())
                {

                    PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientSummary.Valid)
                    {

                        HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                }
       //     }
            HomeLists.PatientCCDownload = Model;
            HomeLists.PatientCCDownload.Customize = customize;
            List<HomeWidgetModel> widgts = new List<HomeWidgetModel>();
            widgts = utility.ConvertToHTML(StringXml);
            HomeLists.widgets = widgts;
                Stringhtml = ViewHelper.RenderRazorViewToString("_CCD_Download_HTML", HomeLists, this);
                StringXml = "";
            }
                   DataDirectService.DirectResponse directresp = new DataDirectService.DirectResponse();
                   string messsage="";
            try
            {
                using (var service = new  DataDirectService.DirectWSSoapClient())//
                {
                    DataDirectService.SendDirectParms sendCCDParms = new DataDirectService.SendDirectParms();


                    //sendCCDParms.Allergies = true; //Model.Allergies;
                    //sendCCDParms.ClinicalInstructions = true; //Model.ClinicalInstructions;
                    //sendCCDParms.DecisionAids = true; //Model.DecisionAids;
                    //sendCCDParms.FutureAppointments = true; //Model.FutureAppointments;
                    //sendCCDParms.Immunizations = true; //Model.Immunizations;
                    //sendCCDParms.Labs = true; //Model.Labs;
                    //sendCCDParms.Medications = true; //Model.Medications;
                    //sendCCDParms.Problems = true; //Model.Problems;
                    //sendCCDParms.Referrals = true; //Model.Referrals;
                    //sendCCDParms.ScheduledTests = true; //Model.ScheduledTests;
                    sendCCDParms.PatientId = long.Parse(RequestHelper.MyGlobalVar.UserLogin);
                    sendCCDParms.FacilityId = Model.FacilityID;
                    sendCCDParms.VisitId = Model.VisitID;
                    sendCCDParms.EmailAddress = Model.EmailID;
                    //sendCCDParms.Social = true; //Model.SocialHistory;
                    //sendCCDParms.VitalSigns = true; //Model.VitalSigns;
                    //sendCCDParms.Procedures = true; //Model.Procedures;
                    ////Commented by Ahmed Saidi to by pass validation for now
                    //directresp = service.ValidateEmailAddress(Model.EmailID, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (directresp.Valid)
                    //{

                    //Commented by khusroo Hayat
                        directresp = service.SendDirectEmail(sendCCDParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId,Stringhtml,StringXml);

                        if (directresp.Valid)
                        {
                            messsage = "CCD Sent Successfully";
                            
                        }

                        else
                        {
                            messsage = "Error Sending CCD";
                            
                        }


                    }
                    //else {
                        //messsage = "Invalid Email Adress";
                        
                    
                    //}
                
                //}



            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(messsage);
        }









        [HttpPost]
        public JsonResult ImmunizationSave(ImmunizationModel Model)
        {
            string strTime;
            string strDate;
            if (Model.ImmunizationDate == null)
            {
               strDate= Model.ImmunizationDate = "";
            }
            else
            {
                var VaccineDate = DateTime.Parse(Model.ImmunizationDate);
                strDate = VaccineDate.ToString("yyyyMMdd");
            }
            if (Model.Time == null)
            {
                strTime = "";
            }
            else
            {
                var VaccineTime = DateTime.Parse(Model.Time);
                strTime = VaccineTime.ToString("HHmm");
            }
           // HomeViewModel HomeList = new HomeViewModel();
            List<ImmunizationModel> Immunization = new List<ImmunizationModel>();
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.VisitId = 0;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;
            Model.CodeSystemId = 7;



            Model.ImmunizationDate = strDate;
            Model.Time = strTime;

            DataPatientDocumentService.PatientImmunizationData pPatientImmunizationData = base.ConvertToPatietntImmunizationData(Model);
            DataPatientDocumentService.PatientImmunizationData pPatietntImmunizationDataReturn = new DataPatientDocumentService.PatientImmunizationData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pPatietntImmunizationDataReturn = service.SavePatientImmunizationData(pPatientImmunizationData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pPatietntImmunizationDataReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientImmunizationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            Immunization = PatientDocument.dt.ToPatientImmunizationModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];

          //  if (Model.Flag == "immunizationWidg")
                //html[0] = HomeList.Immunizations.GetPatientImmunizationModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_Immunization", Immunization, this);
           // else if (Model.Flag == "immunizationTab")
                //html[1] = HomeList.Immunizations.GetPatientImmunizationModelListHTMLForTab();
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Immunization.cshtml", Immunization, this);
            return Json(html);

        }


        [HttpPost]
        public JsonResult ProblemSave(ProblemModel Model)
        {
           // HomeViewModel HomeList = new HomeViewModel();
            List<ProblemModel> Problem = new List<ProblemModel>();
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.CodeSystemId = 5;
            Model.VisitId = 0;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientProblemData pPatientProblemData = base.ConvertToPatientProblemDataData(Model);
            DataPatientDocumentService.PatientProblemData pPatientProblemDataReturn = new DataPatientDocumentService.PatientProblemData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pPatientProblemDataReturn = service.SavePatientProblemData(pPatientProblemData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pPatientProblemDataReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatientDocument = service.GetPatientProblemData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            Problem = PatientDocument.dt.ToPatientProblemModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];

          //  if (Model.Flag == "problemWidg")
                //html[0] = HomeList.Problems.GetPatientProblemModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_Problems", Problem, this);
           // else if (Model.Flag == "problemTab")
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Problems.cshtml", Problem, this); 

            return Json(html);

        }

        [HttpPost]
        public JsonResult FamilySave(FamilyHistoryModel Model)
        {
         //   HomeViewModel HomeList = new HomeViewModel();
            List<FamilyHistoryModel> FamilyHistory = new List<FamilyHistoryModel>();    
            Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Model.VisitId = 0;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;
            if (Model.CodeValue == "Other Relative")
            {
                Model.CodeSystemId = 0;
            }
            else
            {

                Model.CodeSystemId = 5;

            }
            DataPatientDocumentService.PatientFamilyHistData pFamilyHistory = base.ConvertToPatientFamilyHistData(Model);
            DataPatientDocumentService.PatientFamilyHistData pFamilyHistoryReturn = new DataPatientDocumentService.PatientFamilyHistData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pFamilyHistoryReturn = service.SavePatientFamilyHistData(pFamilyHistory, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pFamilyHistoryReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientFamilyHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            FamilyHistory = PatientDocument.dt.ToPatientFamilyHistoryModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];
          //  if (Model.Flag == "familyWidg")
                //html[0] = HomeList.FamilyHistories.GetPatientFamilyHistoryModelListHTMLForDashboard();
            html[0] = ViewHelper.RenderRazorViewToString("_Index_FamilyHistory", FamilyHistory, this);
          //  else if (Model.Flag == "familyTab")
                //html[1] = HomeList.FamilyHistories.GetPatientFamilyHistoryModelListHTMLForTab();
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Family.cshtml", FamilyHistory, this);
               return Json(html);
        }

        [HttpPost]
        public JsonResult MedicationSave(PatientMedicationModel Model)
        {
           // MedicationViewModel MedicationLists = new MedicationViewModel();
            List<PatientMedicationModel> Medication = new List<PatientMedicationModel>();
            try
            {

                DataPatientDocumentService.PatientMedicationData pMed = base.ConvertToPatientMedicationData(Model);
                pMed.PatientId = RequestHelper.MyGlobalVar.PatientId;
                pMed.VisitId = 0;
                pMed.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                pMed.DuringVisit = Model.duringvisit;
                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                if (Model.Status == "1")
                {
                    Model.Status = "Active";
                    Model.Active = true;
                    pMed.Active = true;

                }

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientMedicationData PatientMedData = service.SavePatientMedicationData(pMed, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);



                    PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatDocParms.VisitId = 0;
                    PatDocParms.Option = 1;
                    PatDocParms.Active = 1;

                    PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        Medication = PatientDocument.dt.ToMedicationHistoryModelList();
                        

                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = MedicationLists.Medications.GetPatientMedicationModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_Medication", Medication, this);

            return Json(html);
        }

        [HttpPost]
        public JsonResult PatientSummarySave(PatientModel Model)
        {
            //HomeViewModel HomeList = new HomeViewModel();
            string patName = String.Empty;
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                
                DataPatientService.PatientData pSumm = base.ConvertToPatientData(Model);
                pSumm.Option = 2;
                pSumm.PatientId = RequestHelper.MyGlobalVar.PatientId;

                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientData PatDat = service.SavePatientData(pSumm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatDat.Valid)
                    {
                        DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                        DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                        //PatParms.Option = 2;
                        PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        //  PatParms.Option = 1;

                        PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientSummary.Valid)
                        {
                            ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                            TicketHelper.SetPatientInformationAndGet(RequestHelper.MyGlobalVar.PatientId.ToString() + "," + ClinicalSummaryLists.PatientSummary.FirstName + " " + ClinicalSummaryLists.PatientSummary.LastName);
                            patName = ClinicalSummaryLists.PatientSummary.FirstName + " " + ClinicalSummaryLists.PatientSummary.LastName;
                            
                        }
                        DataPatientService.PatientOrganData PatientOrgan = new DataPatientService.PatientOrganData();
                        PatientOrgan = service.GetPatientOrganData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientOrgan.Valid)
                        {
                            ClinicalSummaryLists.PatientOrgan = base.ConvertToPatientOrganModel(PatientOrgan);

                        }
                    }
                   
                }
             
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;
            if (RequestHelper.MyGlobalVar.PremiumFlag == true)
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_ChartSummaryPartial.cshtml", ClinicalSummaryLists, this);
                
            }
            else
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_DemographicsPartial.cshtml", ClinicalSummaryLists, this);             
            }
           // html = HTMLExtensions.GetPatientSummaryHTMLForDashBoard(ClinicalSummaryLists.PatientSummary, ClinicalSummaryLists.PatientOrgan);
            return Json(new { html = html, patName = patName });
        }

        [HttpPost]
        public JsonResult PatientEmergencySave(PatientEmergencyModel Model)
        {

            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientEmergencyData pEmergency = base.ConvertToPatientEmergencyData(Model);
                pEmergency.PatientEmergencyId = 3;
                pEmergency.PatientId = RequestHelper.MyGlobalVar.PatientId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientEmergencyData PatEmergency = service.SavePatientEmergencyData(pEmergency, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);



                    DataPatientDocumentService.PatientDocTableData PatientEmergency = new DataPatientDocumentService.PatientDocTableData();
                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();


                    PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;

                    PatientEmergency = service.GetPatientEmergencyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientEmergency.Valid)
                    {
                        ClinicalSummaryLists.PatientEmergency = base.ConvertToPatientEmergencyModel(PatientEmergency);

                    }



                }

                using (var service = new DataPatientService.PatientWSSoapClient())
                {


                    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                    PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    //  PatParms.Option = 1;

                    PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientSummary.Valid)
                    {
                        ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                    DataPatientService.PatientOrganData PatientOrgan = new DataPatientService.PatientOrganData();
                    PatientOrgan = service.GetPatientOrganData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientOrgan.Valid)
                    {
                        ClinicalSummaryLists.PatientOrgan = base.ConvertToPatientOrganModel(PatientOrgan);

                    }
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_ChartSummaryPartial.cshtml", ClinicalSummaryLists, this);
           // html = HTMLExtensions.GetPatientSummaryHTMLForDashBoard(ClinicalSummaryLists.PatientSummary, ClinicalSummaryLists.PatientOrgan);
            return Json(html);
        }

        [HttpPost]
        public JsonResult patientAndOrganDataSave(PatientAndOrganModel Model)
        {

            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientService.PatientData PatData = base.ConvertToPatientData(Model);
                DataPatientService.PatientOrganData PatOrgData = base.ConvertToPatientOrganData(Model);


                PatData.PatientId = RequestHelper.MyGlobalVar.PatientId;
                PatData.Option = 1;
                PatOrgData.PatientId = RequestHelper.MyGlobalVar.PatientId;

                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientData PatDataSave = service.SavePatientData(PatData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    DataPatientService.PatientOrganData PatOrganSave = service.SavePatientOrganData(PatOrgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                    PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;

                    PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientSummary.Valid)
                    {
                        ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                    DataPatientService.PatientOrganData PatientOrgan = new DataPatientService.PatientOrganData();
                    PatientOrgan = service.GetPatientOrganData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientOrgan.Valid)
                    {
                        ClinicalSummaryLists.PatientOrgan = base.ConvertToPatientOrganModel(PatientOrgan);

                    }
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_ChartSummaryPartial.cshtml", ClinicalSummaryLists, this);
           //html = HTMLExtensions.GetPatientSummaryHTMLForDashBoard(ClinicalSummaryLists.PatientSummary, ClinicalSummaryLists.PatientOrgan);
            return Json(html);
        }

        [HttpPost]
        public JsonResult SocialHistorySave(SocialHistoryModel Model)
        {
         //   HomeViewModel HomeList = new HomeViewModel();
            List<SocialHistoryModel> socialhistory = new List<SocialHistoryModel>();
            Model.FacilityId = 0;
            Model.VisitId = 0;
            Model.CodeSystemId = 5;
            Model.PatientId = RequestHelper.MyGlobalVar.PatientId;

            DataPatientDocumentService.PatientSocialHistData pSocialHistory = base.ConvertToPatientSocialHistory(Model);
            DataPatientDocumentService.PatientSocialHistData pSocialHistoryReturn = new DataPatientDocumentService.PatientSocialHistData();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    pSocialHistoryReturn = service.SavePatientSocialHistData(pSocialHistory, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (pSocialHistoryReturn.Valid)
                    {
                        DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = 0;
                        PatDocParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        PatDocParms.Option = 1;
                        PatientDocument = service.GetPatientSocialHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            socialhistory = PatientDocument.dt.ToSocialHistoryModelList();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string[] html = new string[2];
         //   if (Model.Flag == "socialWidg")
         //html[0] = HomeList.SocialHistories.GetPatientSocialHistoryModelListHTMLForDashboard();

            html[0] = ViewHelper.RenderRazorViewToString("_Index_SocialHistory", socialhistory, this);

          //  else if (Model.Flag == "socialTab")
         //html[1] = HomeList.SocialHistories.GetPatientSocialHistoryModelListHTMLForTab();
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Social.cshtml", socialhistory, this);


            return Json(html);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult AppointmentSave(MessageModel Model)
        {
            //HomeViewModel HomeLists = new HomeViewModel();

            List<PatientMessageModel> Message = new List<PatientMessageModel>();

            DataMessageService.MessageData Msgdata = base.ConvertToMessageData(Model);
            Msgdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
            Msgdata.MessageTypeId = 1;
            Msgdata.MessageStatusId = 1;
            //Msgdata.ProviderId_From = 1;
            Msgdata.User_Id_Created = 1;
            Msgdata.CreatedByName = RequestHelper.MyGlobalVar.PatientName;
            //"Admin";
            Msgdata.AttachmentName = "";
            Msgdata.MessageResponseTypeId = 3;
            Msgdata.MessageUrgency = Model.MessageUrgency;
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                Msgdata = service.SaveMessageData(Msgdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();


                MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId;

                MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                    Message = MessageData.dt.ToAppointmentMessageModelList();
                }
            }

            string html = string.Empty;

          
            if (Model.status == "appointmentmenu")
            {
                html = ViewHelper.RenderRazorViewToString("_Appointment_Index", Message, this); 
            
            }
            else { html = ViewHelper.RenderRazorViewToString("_Index_Appointment", Message, this); }
            if (Model.status == "Appointment")
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", Message, this);

            }
            else
            {
                html = ViewHelper.RenderRazorViewToString("_Appointment_Index", Message, this);
            }
            return Json(html);
            
        }

        [HttpPost]
        public JsonResult AppointmentDelete(MessageModel Model)
        {
         //   HomeViewModel HomeLists = new HomeViewModel();
            List<PatientMessageModel> Message = new List<PatientMessageModel>();
            DataMessageService.MessageData Msgdata = new DataMessageService.MessageData();
            using (var service = new DataMessageService.MessageWSSoapClient())
            {

                Msgdata.MessageId = Model.MessageId;
                Msgdata = service.DeleteMessage(Msgdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Msgdata.Valid)
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                    MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        //HomeLists.Messages = MessageData.dt.ToMessageModelList();
                        Message = MessageData.dt.ToAppointmentMessageModelList();
                    }
                }
            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_Appointment_Index", Message, this);            
            return Json(html);

        
        }

        [HttpGet]
        [AnotherSessionRedirectionFilter] [Authorize]
        public ViewResult ProblemsIndex()
        {
              HomeViewModel HomeList = new HomeViewModel();
              FacilityVisitSelectModel smodel;
              smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
              if (smodel == null)
              {
                  //smodel.
                  smodel = new FacilityVisitSelectModel
                  {
                      facilitySelected = RequestHelper.MyGlobalVar.FacilitySelectId.ToString(),
                      visitSelected = RequestHelper.MyGlobalVar.VisitId.ToString(),
                  };
                  Session["OptionSelected"] = smodel;
              }
              using (var service = new DataPatientService.PatientWSSoapClient())
              {
                  DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                  DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();

                  DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                  DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();

                  PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;

                  FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);

                  if (FacilityData.Valid)
                  {
                      HomeList.Facilities = FacilityData.dt.ToFacilityModelList();
                  }

                  PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                  if (smodel != null)
                  {
                      PatVisitParms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                  }
                  else
                  {
                      PatVisitParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                  }
                  PatVisitParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                  PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                  PatVisitParms.Option = 3;
                  VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (VisitData.Valid)
                  {
                      HomeList.Visits = VisitData.dt.ToVisitModelList();
                  }


              }
              using (var service = new DataProviderService.ProviderWSSoapClient())
              {
                  DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                  ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (ProviderData.Valid)
                  {
                      HomeList.Providers = ProviderData.dt.ToProviderModelList();
                      HomeList.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

                  }
                  DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
                  //   RequestHelper.MyGlobalVar.FacilityId
                  //  ProviderList = service.GetPatientProviderInfoList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.FacilitySelectId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  // HomeLists.ProivderInfo = ProviderList.dt.ToProivderInfoList();
              }
              using (var service = new DataMessageService.MessageWSSoapClient())
              {
                  DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                  DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                  DataMessageService.CodeTableData MessageType = new DataMessageService.CodeTableData();
                  DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();

                  MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;

                
                  MessageType = service.GetMessageTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (MessageType.Valid)
                  {
                      HomeList.MessageType = MessageType.dt.ToMessageTypeModelList();
                      HomeList.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                  }
                  //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  //if (MessageUrgencyType.Valid)
                  //{
                  //    HomeLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                  //    HomeLists.MessageUrgencyType.Insert(0, new MessageUrgency { MessageUrgencyId = -1, Value = "--Select--" });
                  //}

              }
              DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
              DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
              DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
              DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();

              parms.Problem = true;
              if (smodel != null)
              {
                  parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                  parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                  HomeList.FacilityVisitSelect = smodel;
              }
              else
              {
                  parms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                  parms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
              }
              parms.Option = 1;
              parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
              using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
              {
                  PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (PatientDocumentSummary.Valid)
                  {
                      HomeList.Problems = PatientDocumentSummary.dtProblem.ToProblemModelList();
                  }
                  DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

                  DataPatientDocumentService.PatientSummaryParms Patientparms = new DataPatientDocumentService.PatientSummaryParms();

                  Patientparms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                  Patientparms.VisitId = 0;
                  Patientparms.FacilityId = 0;
                  Patientparms.Option = 1;
                  Patientparms.Active = 1;
                  //    parms.FamilyHist = true;
                  //    parms.SocialHist = true;
                  //    parms.MedicalHist = true;
                  //    parms.Problem = true;

                  //    parms.VitalSign = true;
                  Patientparms.Medication = true;
                  //    parms.Lab = true;
                  //    parms.Immunization = true;
                  //    parms.Allergy = true;
                  //    parms.PlanOfCare = true;
                  //    parms.Insurance = true;
                  //    parms.ClinicalDocs = true;
                  //    parms.Procedure = true;
                  //if (smodel != null)
                  //{
                  //    //    parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                  //    //    parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                  //    HomeList.FacilityVisitSelect = smodel;
                  //}
                  PatientDocument = service.GetPatientSummaryData(Patientparms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                  if (PatientDocument.Valid)
                  {
                      //        HomeLists.SocialHistories = PatientDocumentSummary.dtSocialHist.ToSocialHistoryModelList();
                      //        HomeLists.VitalSigns = PatientDocumentSummary.dtVitalSign.ToVitalsignModelList();
                      //        HomeLists.MedicalHistories = PatientDocumentSummary.dtMedicalHist.ToMedicalHistoryModelList();
                      HomeList.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                      HomeList.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
                      //        HomeLists.Problems = PatientDocumentSummary.dtProblem.ToProblemModelList();
                      //        HomeLists.LabResults = PatientDocumentSummary.dtLab.ToLabResultModelList();
                      //        HomeLists.Immunizations = PatientDocumentSummary.dtImmunization.ToImmunizationModelList();
                      //        HomeLists.FamilyHistories = PatientDocumentSummary.dtFamilyHist.ToFamilyHistoryModelList();
                      //        HomeLists.Allergies = PatientDocumentSummary.dtAllergy.ToAllergyModelList();
                      //        HomeLists.Documents = PatientDocumentSummary.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                      //        // HomeLists.Policies = PatientDocumentSummary.dtInsurance.ToPolicyModelList();
                      //        HomeLists.Pocs = PatientDocumentSummary.dtPlanOfCare.ToPOCModelList();
                      //        HomeLists.Insurance = PatientDocumentSummary.dtInsurance.ToInsuranceModelList();
                      //        HomeLists.Procedures = PatientDocumentSummary.dtProcedure.ToProcedureModelList();
                      //        //HomeLists.ClinicalInstructions = PatientDocumentSummary.dtc
                  }
              }
              using (var service = new DataPatientConfigService.ConfigWSSoapClient())
              {
                  ConfigDocument = service.GetConditionStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (ConfigDocument.Valid)
                  {
                      HomeList.ConditionStatus = ConfigDocument.dt.ToConditionStatusModelList();
                      //HomeList.ConditionStatus = ConfigDocument.dt.ToConditionStatusModelList().ToList().Select(x => new SelectListItem { Text = x.Value, Value = x.ConditionStatusId.ToString() }).ToList<ConditionStatusModel>();
                      //ViewBag.ConditionStatus = ConfigDocument.dt.ToConditionStatusModelList().ToList().Select(x => new SelectListItem { Text = x.Value, Value = x.ConditionStatusId.ToString() }).ToList();
                      HomeList.ConditionStatus.Insert(0, new ConditionStatusModel { ConditionStatusId = -1, Value = "--Select--" });
                  }
              }
              using (var service = new DataPatientService.PatientWSSoapClient())
              {
                  DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                  DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                  DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                  DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                  DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                  DataPatientService.PatientRepData PatRepData = new DataPatientService.PatientRepData();
                  PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                  PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (PatientSummary.Valid)
                  {
                      HomeList.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                      // TicketHelper.SetPatientInformationAndGet(HomeLists.PatientSummary.PatientId.ToString() + "," + HomeLists.PatientSummary.FirstName + " " + HomeLists.PatientSummary.LastName);  
                  }

                  FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                  if (FacilityData.Valid)
                  {
                      HomeList.Facilities = FacilityData.dt.ToFacilityModelList();
                      HomeList.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                      HomeList.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                  }


                  PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                  if (smodel != null)
                  {
                      PatVisitParms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                  }
                  else
                  {
                      PatVisitParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                  }
                  PatVisitParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                  PatVisitParms.Option = 3;
                  VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                  if (VisitData.Valid)
                  {
                      HomeList.Visits = VisitData.dt.ToVisitModelList();

                  }
                
              }


              return View(HomeList);
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult RequestReferralSave(MessageModel Model)
        {
            List<PatientMessageModel> Message = new List<PatientMessageModel>();


            DataMessageService.MessageData Msgdata = base.ConvertToMessageData(Model);
            Msgdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
            Msgdata.MessageTypeId = 5;
            //Msgdata.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            Msgdata.MessageStatusId = 1;
            //Msgdata.ProviderId_From = 1;
            Msgdata.User_Id_Created = 1;
            Msgdata.CreatedByName = RequestHelper.MyGlobalVar.PatientName;
            Msgdata.AppointmentStart = Convert.ToDateTime("1/1/1900");
            Msgdata.AppointmentEnd = Convert.ToDateTime("1/1/1900");
            Msgdata.AttachmentName = "";
            Msgdata.MessageResponseTypeId = 3;
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                Msgdata = service.SaveMessageData(Msgdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();


                MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId;

                MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                    Message = MessageData.dt.ToMessageModelList();
                }
            }


            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", Message, this);
            //html = HomeLists.Messages.GetAppointmentListHTMLForDashboard();
            return Json(html);
        }

        public JsonResult GetProviderFacilityData(long FacilityID, string Value)
        {
            List<ProviderModel> HomeLists = new List<ProviderModel>();
            
            try
            {
                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();
                    if (FacilityID == -1)
                    {
                        HomeLists.Insert(0, new ProviderModel { FullName = "--Select--", ProviderId = -1 });
                       // ProviderData = service.GetProvidersForPatientList(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                       // if (ProviderData.Valid)
                       // {
                       //     HomeLists.Providers = ProviderData.dt.ToProviderFacilityModelList();
                       // }
                    }
                    else
                    {
                        ProviderData = service.GetFacilityList(FacilityID, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        // ProviderData = service.GetProvidersForPatientList(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (ProviderData.Valid)
                        {
                            HomeLists = ProviderData.dt.ToProviderFacilityModelList();
                        }
                        HomeLists.Insert(0, new ProviderModel { FullName = "--Select--", ProviderId = -1 });
                    }
                    
                }

              
              
            }
            catch (Exception ex)
            {
                //Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
            string Providers = string.Empty;
            if (Value == "SendMessage")
            {
                Providers = ViewHelper.RenderRazorViewToString("_FacilityProviderSendMessage", HomeLists, this);
            }
            else if (Value == "RequestRefferal")
            {
                Providers = ViewHelper.RenderRazorViewToString("_FacilityProviderRequestRefferal", HomeLists, this);
            }
            else if (Value == "RequestRefil")
            {
                Providers = ViewHelper.RenderRazorViewToString("_CmbRefildoctorDropDown", HomeLists, this);

            }
           
            else
            {
                 Providers = ViewHelper.RenderRazorViewToString("_FacilityProvider", HomeLists, this);
            }
           // string ddlEvent = ViewHelper.RenderRazorViewToString("_ddlEvent", HomeLists.Events, this);
           // string ddlVenue = ViewHelper.RenderRazorViewToString("_ddlVenue", HomeLists.Venues, this);
            return Json(Providers);
        }

        public JsonResult GetPatientMedicationList(long FacilityID)
        {
           string html = string.Empty;
            HomeViewModel HomeLists = new HomeViewModel();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                //  DataProviderService.ProviderTableData ProviderDatamedication = new DataProviderService.ProviderTableData();
                DataPatientDocumentService.PatientDocParms patdocpara = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData patdoctable = new DataPatientDocumentService.PatientDocTableData();

                if (FacilityID == -1)
                {
                    HomeLists.Providers.Insert(0, new ProviderModel { FullName = "--Select--", ProviderId = -1 });
                    // ProviderData = service.GetProvidersForPatientList(RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    // if (ProviderData.Valid)
                    // {
                    //     HomeLists.Providers = ProviderData.dt.ToProviderFacilityModelList();
                    // }
                }
                else
                {
                    patdocpara.FacilityId = FacilityID;
                    patdocpara.Active = 1;
                    patdocpara.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    patdocpara.Option = 2;
                   
                    //patdocpara.VisitId=RequestHelper.MyGlobalVar
                    patdoctable = service.GetPatientMedicationData(patdocpara, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //  ProviderDatamedication = service.GetProvidersForPatientList(FacilityID,RequestHelper.MyGlobalVar.PatientId, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (patdoctable.Valid)
                    {
                        HomeLists.Medications = patdoctable.dt.ToMedicationHistoryModelList();
                    }
                    HomeLists.Medications.Insert(0, new PatientMedicationModel { MedicationName = "--Select--", PatientMedicationCntr = "-1" });
                }
                
                html = ViewHelper.RenderRazorViewToString("_Index_RefillMedication", HomeLists.Medications, this);
            }





            return Json(html);
        }
        public JsonResult AppointmentShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareAppointment(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHide\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_widg1(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else {
                    html = "    <div id=\"App-ShareHide\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_widg1(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        }
        public JsonResult SocialHistoryShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp resp = new DataPatientService.PatientShareResp();
            using (var service=new DataPatientService.PatientWSSoapClient())
            {
                resp = service.PatientShareSocialHistory(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if(resp.Valid && Share)
                    {
                        html = "    <div id=\"App-ShareHideSocialHistory\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Socialhistory(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                    
                    }
                else{
                    html = "    <div id=\"App-ShareHideSocialHistory\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Socialhistory(true)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                      "   </div>    ";
                    }
            }
        return Json(html);
        }
        public JsonResult FamilyHistoryShareHide(bool Share)
        {

            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareFamilyHistory(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHidefamilyhist\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Familyhist(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHidefamilyhist\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Familyhist(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        }
        public ActionResult VisitsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareVisit(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideVisits\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Visits(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideVisits\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Visits(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        
        
        
        }

        public ActionResult MedicationShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareMedication(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideMedications\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Medication(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/Share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideMedications\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Medication(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        }

        public ActionResult PastMedicalHistoryShareHide(bool Share)
        {

            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareMedicalHistory (RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHidePastMedicalHistory\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_PastMedicalHistory(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHidePastMedicalHistory\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_PastMedicalHistory(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        }

        public ActionResult ProblemsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareProblem(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideProblems\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Problems(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideProblems\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Problems(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        
        
        }

        public ActionResult InsuranceShareHide(bool Share)
        {

            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareInsurance(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideInsurance\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Insurance(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideInsurance\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Insurance(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        
        
        
        }


        public ActionResult ImmunizationShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareImmunization(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideImmunization\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Immunization(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideImmunization\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Immunization(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        
        }

        public ActionResult VitalsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareVitalSigns(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideVitals\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Vitals(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideVitals\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Vitals(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        }
           
        public ActionResult DocumentsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareClinicalDoc(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideDocuments\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Documents(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideDocuments\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Documents(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);
        }
        public ActionResult AllergiesShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareAllergy(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideAllergies\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Allergies(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideAllergies\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Allergies(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);

        }
        public ActionResult POCShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientSharePlanOfCare(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHidePOC\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_POC(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHidePOC\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_POC(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);

        }


        public JsonResult LabsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareLabResults(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"Lab-ShareHide\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_widg3(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"Lab-ShareHide\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_widg3(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);

        }

        public JsonResult ClinicalInstructionsShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareClinicalInstructions(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideClinicalInstructions\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_ClinicalInstructions(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideClinicalInstructions\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_ClinicalInstructions(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 10px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);

        }
        public JsonResult GoogleMap()
        {
            
            string postData = "";
            string responseData = "";
            string location =Request.QueryString["Location"];
            string Search = Request.QueryString["Search"];
            string URL = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=pharmacy+in+" + Search + "&sensor=true&key=AIzaSyCxZObVHhRiEYZHe4k9TmXYpKEGwaKrrH8";

            try
            {
                System.Net.HttpWebRequest hwrequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(URL);
                hwrequest.AllowAutoRedirect = true;
                hwrequest.UserAgent = "http_requester/0.1";
                hwrequest.Timeout = 60000;
                hwrequest.Method = "POST";
                if (hwrequest.Method == "POST")
                {
                    hwrequest.ContentType = "application/x-www-form-urlencoded";
                    // Use UTF8Encoding instead of ASCIIEncoding for XML requests:
                    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                    byte[] postByteArray = encoding.GetBytes(postData);
                    hwrequest.ContentLength = postByteArray.Length;
                    System.IO.Stream postStream = hwrequest.GetRequestStream();
                    postStream.Write(postByteArray, 0, postByteArray.Length);
                    postStream.Close();


                }
                System.Net.HttpWebResponse hwresponse = (System.Net.HttpWebResponse)hwrequest.GetResponse();
                if (hwresponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    System.IO.Stream responseStream = hwresponse.GetResponseStream();
                    System.IO.StreamReader myStreamReader = new System.IO.StreamReader(responseStream);
                    responseData = myStreamReader.ReadToEnd();
                }
                hwresponse.Close();
            }
            catch (Exception e)
            {
                responseData = "An error occurred: " + e.Message;
            }
            return Json(responseData);

        }


        public JsonResult ProceduresShareHide(bool Share)
        {
            string html = "";
            DataPatientService.PatientShareResp Resp = new DataPatientService.PatientShareResp();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.PatientShareProcedure(RequestHelper.MyGlobalVar.PatientId, Share, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Resp.Valid && Share)
                {
                    html = "    <div id=\"App-ShareHideProcedures\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Procedures(false)\";>" +
                    "<span class=\"anc\" style=\"padding-right: 2px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> UnShare  </span>" +
                      "   </div>    ";
                }
                else
                {
                    html = "    <div id=\"App-ShareHideProcedures\" style=\"cursor: pointer; float: right; width: auto; height: 20px;\" onclick=\"ShareHide_Procedures(true)\";>" +
                       "<span class=\"anc\" style=\"padding-right: 2px; font-weight: normal;\" ><img src=\"Content/img/share.png\"/> Share  </span>" +
                         "   </div>    ";
                }
            }
            return Json(html);

    }


        //[HttpPost]
        //public JsonResult AllergyWidget(AllergyModel Model)
        //{
        //    List<AllergyModel> allergymodel = new List<AllergyModel>();
        //    DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
        //    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
        //    parms.FacilityId = Model.FacilityId;
        //    parms.VisitId = Model.VisitId;
        //    parms.Allergy = true;
        //    parms.Option = 1;
        //    parms.Active = 1;
        //    parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
        //    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
        //    {
        //        PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
        //        if (PatientDocumentSummary.Valid)
        //        {

        //            allergymodel = PatientDocumentSummary.dtAllergy.ToAllergyModelList();
        //        }

        //    }
        //    string html = string.Empty;
        //    //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForDashboard();
        //    html = ViewHelper.RenderRazorViewToString("_Index_Allergies", allergymodel, this);
        //    return Json(html);
        //}

        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
         //  var abc = RouteData.Values["id"];
           HomeViewModel homelist = new HomeViewModel();
            //   string id = Request.QueryString["id"].ToString();
               DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

               int LoginType = 5;
               string msg = String.Empty;
               DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


               try
               {
                   
                   DataAuthenticationService.PatientSecurityData PatSecurityData = new DataAuthenticationService.PatientSecurityData();
                   ResponseData = AuthenticationWS.GetSecurityQuestion(0, id, LoginType, null);
                   if ((ResponseData.SecurityQuestion == null && ResponseData.SecurityQuestion2 == null) || (ResponseData.SecurityQuestion == "" && ResponseData.SecurityQuestion2 == ""))
                   {
                       ResponseData = AuthenticationWS.ResetPassword(0, id, LoginType, null, null);

                       if (ResponseData.Valid)
                       {
                           msg = "Your password has been reset.  Your new password has been sent to your email.";

                       }
                       else
                       {
                           msg = "Your password has been reset failed";

                       }
                   }
                   else {
                       msg = "Password not reset. Account is active.";
                   
                   }
               }
               catch (Exception ex)
               {

                   return Json(ex.Message);
               }
               string html = string.Empty;

               html = msg;
              
            homelist.CurrentUser.ErrorMessage = msg;
           // TempData["Message"] = msg;
            return View(homelist);
          //  return RedirectToAction("Index", "Home");

        }

           [HttpGet]
        public ActionResult ResetPasswordProviderDirectly(string id)
        {
         //  var abc = RouteData.Values["id"];
           HomeViewModel homelist = new HomeViewModel();
            //   string id = Request.QueryString["id"].ToString();
               DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

               int LoginType = 6;
               string msg = String.Empty;
               DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


               try
               {
                     ResponseData = AuthenticationWS.GetSecurityQuestion(0, id, LoginType, null);
                     if ((ResponseData.SecurityQuestion == null && ResponseData.SecurityQuestion2 == null) || (ResponseData.SecurityQuestion == "" && ResponseData.SecurityQuestion2 == ""))
                     {


                         ResponseData = AuthenticationWS.ResetPassword(0, id, LoginType, null, null);

                         if (ResponseData.Valid)
                         {
                             msg = "Your password has been reset.  Your new password has been sent to your email.";

                         }
                         else
                         {
                             msg = "Your password has been reset failed";

                         }
                     }
                     else
                     {
                         msg = "Password not reset. Account is active.";

                     }
               }
               catch (Exception ex)
               {

                   return Json(ex.Message);
               }
               string html = string.Empty;

               html = msg;
              
            homelist.CurrentUser.ErrorMessage = msg;
           // TempData["Message"] = msg;
            return View(homelist);
          //  return RedirectToAction("Index", "Home");

        }
        
        //[HttpPost]
        //public JsonResult preactive_pw_resend()
        //{
        //    string id = RouteData.Values["id"].ToString();
        //    DataAuthenticationService.AuthenticationWSSoapClient AuthenticationWS = new DataAuthenticationService.AuthenticationWSSoapClient();

        //    int LoginType = 5;
        //    string msg = String.Empty;
        //  /*  DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();


        //    try
        //    {

        //        ResponseData = AuthenticationWS.ResetPassword(0, id, LoginType, "Url", "Url");

        //        if (ResponseData.Valid)
        //        {
        //            msg = "Your password has been reset.  Your new password has been sent to your email.";

        //        }
        //        else
        //        {
        //            return Json(ResponseData.ErrorMessage);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(ex.Message);
        //    }
        //    string html = string.Empty;

        //    html = msg;
        //    return Json(html);*/


        //}

}

}
