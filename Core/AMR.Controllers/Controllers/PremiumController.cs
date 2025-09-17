using AMR.Core.Extensions;
using AMR.Core.Utilities;
using AMR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    public class PremiumController : Base.BaseController
    {

        #region Private Variables...
        PremiumViewModel PremiumLists = null;
        PremiumEmergencyContactViewModel PremiumEmergencyContactLists = null;
        PremiumManageDoctorViewModel PremiumManageDoctorLists = null;
        PremiumMedicalPortfolioViewModel PremiumMedicalPortfolioLists = null;
        PremiumGeneralDocumentViewModel PremiumGeneralDocumentLists = null;

        #endregion

        #region Emergency Contact Actions...
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult EmergencyContactIndex()
        {

            PremiumEmergencyContactLists = new PremiumEmergencyContactViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    { 
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientEmergency = service.GetPatientEmergencyData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientEmergency.Valid)
                    {
                        PremiumEmergencyContactLists.PatientEmergency = dtPatientEmergency.dt.ToPatientEmergencyModelList();
                    }


                }


                //DropDownLists Data...
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {

                    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                    
                    //Getting Country Data...
                    ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumEmergencyContactLists.Country = ConfigDocument.dt.ToCountryModelList();
                    }

                    //Getting State Data...
                    ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumEmergencyContactLists.States = ConfigDocument.dt.ToStatesModelList();
                    }

                    //Getting Relationship Data...
                    ConfigDocument = service.GetRelationshipCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumEmergencyContactLists.Relationships = ConfigDocument.dt.ToRelationshipModelList();
                        PremiumEmergencyContactLists.Relationships.Insert(0, new RelationshipModel { RelationShipID = -1, Value = "--Select--", SNOMED = "-1" });
                    }
                }
          



            }
            catch //(Exception ex)
            {
                
                //Redirect to generic error page for saying "Forbidden";
            }

            return View(PremiumEmergencyContactLists);
        }

        [HttpPost]
        public JsonResult EmergencyContactSave(PatientEmergencyModel Model)
        {

            PremiumEmergencyContactLists = new PremiumEmergencyContactViewModel();
            try
            {

                //Updata : Param Emergency Contact Data...
                DataPatientDocumentService.PatientEmergencyData paramEmergencyContact = base.ConvertToPatientEmergencyData(Model);
                
                //Assign user login...
                paramEmergencyContact.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Make a call for save or update...
                    DataPatientDocumentService.PatientEmergencyData result = service.SavePatientEmergencyData(paramEmergencyContact, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Get : Param Emergency Contact Data... 
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Make a call for getting data...
                    DataPatientDocumentService.PatientDocTableData dtPatientEmergency = service.GetPatientEmergencyData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientEmergency.Valid)
                    {
                        PremiumEmergencyContactLists.PatientEmergency = dtPatientEmergency.dt.ToPatientEmergencyModelList();
                    }

                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = PremiumEmergencyContactLists.PatientEmergency.ToHTMLPatientEmergencyContactForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/EmergencyContact/_EmergencyContact.cshtml", PremiumEmergencyContactLists.PatientEmergency, this);
            return Json(html);
        }

        [HttpPost]
        public JsonResult EmergencyContactDelete(PatientEmergencyModel Model)
        {

            PremiumEmergencyContactLists = new PremiumEmergencyContactViewModel();
            try
            {

                //Updata : Param Emergency Contact Data...
                DataPatientDocumentService.PatientEmergencyData paramEmergencyContact = base.ConvertToPatientEmergencyData(Model);

                //Assign user login...
                paramEmergencyContact.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Make a call for save or update...
                    DataPatientDocumentService.PatientEmergencyData result = service.DeletePatientEmergencyData(paramEmergencyContact, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Get : Param Emergency Contact Data... 
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Make a call for getting data...
                    DataPatientDocumentService.PatientDocTableData dtPatientEmergency = service.GetPatientEmergencyData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientEmergency.Valid)
                    {
                        PremiumEmergencyContactLists.PatientEmergency = dtPatientEmergency.dt.ToPatientEmergencyModelList();
                    }

                }
            }
            catch (Exception ex)
        {

                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = PremiumEmergencyContactLists.PatientEmergency.ToHTMLPatientEmergencyContactForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/EmergencyContact/_EmergencyContact.cshtml", PremiumEmergencyContactLists.PatientEmergency, this);
            return Json(html);
        }
        #endregion

        #region Manage Doctor Actions...
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult ManageDoctorIndex()
        {
            PremiumManageDoctorLists = new PremiumManageDoctorViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientDoctorData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumManageDoctorLists.PatientDoctor = dtPatientDoctor.dt.ToPatientDoctorModelList();
                    }


                }


                //DropDownLists Data...
                using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                {

                    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();

                    //Getting Country Data...
                    ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumManageDoctorLists.Country = ConfigDocument.dt.ToCountryModelList();
                    }

                    //Getting State Data...
                    ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumManageDoctorLists.States = ConfigDocument.dt.ToStatesModelList();
                    }

                    //Getting Doctor Speciality Data...
                    ConfigDocument = service.GetDoctorTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        PremiumManageDoctorLists.DoctorSpeciality = ConfigDocument.dt.ToDoctorSpecialityModelList();

                        //PremiumLists.DoctorSpeciality = new List<DoctorSpecialityModel> {new DoctorSpecialityModel{Value="Dummy Speciality", DoctorSpecialityId=-1} };
                    }
                }




            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            return View(PremiumManageDoctorLists);
        }

        [HttpPost]
        public ActionResult ManageDoctorSave(PatientDoctorModel Model)
        {
            PremiumManageDoctorLists = new PremiumManageDoctorViewModel();
            try
            {

                //Updata : Param Patient Doctor Data...
                DataPatientDocumentService.PatientDoctorData paramPatientDoctor = base.ConvertToPatientDoctorData(Model);


                
                //Assign user login...
                paramPatientDoctor.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Make a call for save or update...
                    DataPatientDocumentService.PatientDoctorData result = service.SavePatientDoctorData(paramPatientDoctor, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Get : Param Doctor Data... 
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientDoctorData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumManageDoctorLists.PatientDoctor = dtPatientDoctor.dt.ToPatientDoctorModelList();
                    }

                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = PremiumManageDoctorLists.PatientDoctor.ToHTMLPatientDoctorForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ManageDoctor/_ManageDoctor.cshtml", PremiumManageDoctorLists.PatientDoctor, this);
            return Json(html);
        }

        [HttpPost]
        public ActionResult ManageDoctorDelete(PatientDoctorModel Model)
        {
            PremiumManageDoctorLists = new PremiumManageDoctorViewModel();
            try
            {

                //Updata : Param Emergency Contact Data...
                DataPatientDocumentService.PatientDoctorData paramPatientDoctor = base.ConvertToPatientDoctorData(Model);

                //Assign user login...
                paramPatientDoctor.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Make a call for save or update...
                    var result = service.DeletePatientDoctorData(paramPatientDoctor, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Get : Param Doctor Data... 
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientDoctorData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumManageDoctorLists.PatientDoctor = dtPatientDoctor.dt.ToPatientDoctorModelList();
                    }

                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = PremiumManageDoctorLists.PatientDoctor.ToHTMLPatientDoctorForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ManageDoctor/_ManageDoctor.cshtml", PremiumManageDoctorLists.PatientDoctor, this);
            return Json(html);
        }

        #endregion


        #region Medical Portfolio Actions...
        [AnotherSessionRedirectionFilter] [Authorize]
        public ViewResult MedicalPortfolioIndex()
        {
            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Document Param Instance...
                    var paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                        Option = 0
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.MedicalPortfolioData dtPatientDoctor = service.GetMedicalPortfolio(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumMedicalPortfolioLists.PatientVisit = dtPatientDoctor.dtVisit.ToPatientVisitModelList();
                        PremiumMedicalPortfolioLists.OutsideDoctor = dtPatientDoctor.dtOutsideDoctor.ToOutsideDoctorModelList();
                        PremiumMedicalPortfolioLists.PatientDocument = dtPatientDoctor.dtPatiendDocs.ToPatientDocumentModelList();
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            return View(PremiumMedicalPortfolioLists);
        }

        [HttpPost]
        public ActionResult MedicalPortfolioVisitShare(PatientVisitModel Model)
        {
            
            //Just Data Transport...
            string shareHideRecordsIds = Model.FacilityName;
            bool doShareHide = Model.Viewable;

            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();
            

            bool IsValid = false;
            string errorMessage = "";

            try
            {
                if (!string.IsNullOrEmpty(shareHideRecordsIds))
                {
                    //var sharedHideIdsArray = shareHideRecordsIds.Split(',');

                    //Patient Service Instance...
                    using (var service = new DataPatientService.PatientWSSoapClient())
                    {
                        //for (int iVisitRecord = 0; iVisitRecord < sharedHideIdsArray.Length - 1; iVisitRecord++)
                        //{
                            var result = new DataPatientService.PatientResp();

                            var paramVisit = new DataPatientService.PatientVisitParms
                            {
                                PatientId = RequestHelper.MyGlobalVar.PatientId,
                                //VisitId = long.Parse(sharedHideIdsArray[iVisitRecord].Split('|')[0]),
                                //FacilityId = long.Parse(sharedHideIdsArray[iVisitRecord].Split('|')[1]),

                                VisitId = long.Parse(shareHideRecordsIds.Split('|')[0]),
                                FacilityId = long.Parse(shareHideRecordsIds.Split('|')[1]),
                                //FacilityId = RequestHelper.MyGlobalVar.FacilityId,

                                //FacilityId = RequestHelper.MyGlobalVar.FacilityId,
                                Share = doShareHide
                            };

                            result = service.VisitShare(paramVisit, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            IsValid = result.Valid;
                            errorMessage = result.ErrorMessage;
                        //}
                        }


                    //Patient Document Service Instance...
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {

                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            Option = 0
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.MedicalPortfolioData dtPatientDoctor = service.GetMedicalPortfolio(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoctor.Valid)
                        {
                            PremiumMedicalPortfolioLists.PatientVisit = dtPatientDoctor.dtVisit.ToPatientVisitModelList();
                            //PremiumMedicalPortfolioLists.OutsideDoctor = dtPatientDoctor.dtOutsideDoctor.ToOutsideDoctorModelList();
                            //PremiumMedicalPortfolioLists.PatientDocument = dtPatientDoctor.dtPatiendDocs.ToPatientDocumentModelList();
                        }
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }
            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/MedicalPortfolio/_HealthCareDataExchange.cshtml", PremiumMedicalPortfolioLists.PatientVisit, this);
            return Json(html);
            
        }

        [HttpPost]
        public ActionResult MedicalPortfolioDoctorShare(PatientOutsideDoctorModel Model)
        {

            //Just Data Transport...
            string shareHideRecordsIds = Model.DoctorName;
            bool doShareHide = Model.Viewable;


            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();

            try
            {
                if (!string.IsNullOrEmpty(shareHideRecordsIds))
                {
                    //var sharedHideIdsArray = shareHideRecordsIds.Split(',');

                    //Patient Service Instance...
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                        //for (int iPDoctorRecord = 0; iPDoctorRecord < sharedHideIdsArray.Length - 1; iPDoctorRecord++)
                        //{
                            var result = new DataPatientDocumentService.PatientDocumentResp();

                            var paramVisit = new DataPatientDocumentService.PatientDocumentParms()
                            {
                                PatientId = RequestHelper.MyGlobalVar.PatientId,
                                //DocumentCntr = long.Parse(sharedHideIdsArray[iPDoctorRecord]),
                                DocumentCntr = long.Parse(shareHideRecordsIds),
                                Share = doShareHide
                            };

                            result = service.PatientCareDocumentShare(paramVisit, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //}
                    }


                    //Patient Document Service Instance...
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {

                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            Option = 0
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientCareDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoctor.Valid)
                        {
                            PremiumMedicalPortfolioLists.OutsideDoctor = dtPatientDoctor.dt.ToOutsideDoctorModelList();
                        }
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/MedicalPortfolio/_PatientOutsideDoctors.cshtml", PremiumMedicalPortfolioLists.OutsideDoctor, this);
            return Json(html);
        }

        public ActionResult MedicalPortfolioDoctorAttachment()
        {
            try
            {
                string outsideDoctorId = Request.QueryString["outsideDoctorId"].ToString();

                //Result...
                var result = new PatientOutsideDoctorModel();

                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    if (Request.Cookies["MedicalSummary"] != null)
                    {
                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin),
                            DocumentCntr = long.Parse(outsideDoctorId),
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientCareDocumentData dtPatientDoctor = service.GetPatientCareDocumentData(paramPatDoc, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoctor.Valid)
                        {
                            result = base.ConvertToOutsideDoctorModel(dtPatientDoctor);
                        }
                    }
                    else
                    {
                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            DocumentCntr = long.Parse(outsideDoctorId),
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientCareDocumentData dtPatientDoctor = service.GetPatientCareDocumentData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoctor.Valid)
                        {
                            result = base.ConvertToOutsideDoctorModel(dtPatientDoctor);
                        }
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

        [HttpPost]
        public ActionResult MedicalPortfolioDocumentShare(PatientMedicalDocumentModel Model)
        {

            //Just Data Transport...
            string shareHideRecordsIds = Model.FacilityName;
            bool doShareHide = Model.Viewable;


            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();

            try
            {
                if (!string.IsNullOrEmpty(shareHideRecordsIds))
                {
                    //var sharedHideIdsArray = shareHideRecordsIds.Split(',');

                    //Patient Service Instance...
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                        //for (int iPDocRecord = 0; iPDocRecord < sharedHideIdsArray.Length - 1; iPDocRecord++)
                        //{
                            var result = new DataPatientDocumentService.PatientDocumentResp();

                            var paramVisit = new DataPatientDocumentService.PatientDocumentParms()
                            {
                                PatientId = RequestHelper.MyGlobalVar.PatientId,
                                //DocumentCntr = long.Parse(sharedHideIdsArray[iPDocRecord]),
                                DocumentCntr = long.Parse(shareHideRecordsIds),
                                Share = doShareHide
                            };

                            result = service.PatientMedicalDocumentShare(paramVisit, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //}
                    }


                    //Patient Document Service Instance...
                    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {

                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            Option = 0
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientMedicalDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoctor.Valid)
                        {
                            PremiumMedicalPortfolioLists.PatientDocument = dtPatientDoctor.dt.ToPatientDocumentModelList();
                        }
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/MedicalPortfolio/_PatientMedicalDocument.cshtml", PremiumMedicalPortfolioLists.PatientDocument, this);
            return Json(html);
        }

        public ActionResult MedicalPortfolioDocumentAttachment()
        {
            try
            {
                string patientDocumentId = Request.QueryString["patientDocumentId"].ToString();

                //Result...
                var result = new PatientMedicalDocumentModel();

                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    if (Request.Cookies["MedicalSummary"] != null)
                    {
                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin),
                            DocumentCntr = long.Parse(patientDocumentId),
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientMedicalDocumentData dtPatientDoc = service.GetPatientMedicalDocumentData(paramPatDoc, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoc.Valid)
                        {
                            result = base.ConvertToPatientMedicalDocumentModel(dtPatientDoc);
                        }
                    }
                    else
                    {
                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            DocumentCntr = long.Parse(patientDocumentId),
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientMedicalDocumentData dtPatientDoc = service.GetPatientMedicalDocumentData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dtPatientDoc.Valid)
                        {
                            result = base.ConvertToPatientMedicalDocumentModel(dtPatientDoc);
                        }
                    }

                }

                if (!System.IO.File.Exists(result.FileDirectory + "\\" + result.DocumentCntr +"." + result.DocumentFormat))
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
            catch //(Exception ex)
            {
                return PartialView("_AttachmentSupport");
                //throw;
            }
        }

        [HttpPost]
        public JsonResult   MedicalPortfolioDocumentFileUpload(string patientData)
        {

            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();

            try
            {
                var json_serializer = new JavaScriptSerializer();
                PatientMedicalDocumentModel model = json_serializer.Deserialize<PatientMedicalDocumentModel>(patientData);

                if (model.DocumentCntr == 0)
                {
                    HttpPostedFileBase myFile = Request.Files["file_upload"];

                    byte[] AttachmentData = null;
                    using (var binaryReader = new BinaryReader(Request.Files["file_upload"].InputStream))
                    {
                        AttachmentData = binaryReader.ReadBytes(Request.Files["file_upload"].ContentLength);
                    }

                    model.DocumentImage = AttachmentData;
                    model.DocumentFormat = Path.GetExtension(myFile.FileName).Replace(".", "");
                    model.DocumentId = myFile.FileName;
                }
               
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Param Patient Medical Document Data Instance...
                    var paramPatientMedicalDocumentData = new DataPatientDocumentService.PatientMedicalDocumentData();
                    paramPatientMedicalDocumentData = base.ConvertToPatientMedicalDocumentData(model);
                    paramPatientMedicalDocumentData.PatientId = RequestHelper.MyGlobalVar.PatientId;


                    //Save patient medical document data...
                    DataPatientDocumentService.PatientMedicalDocumentData result = service.SavePatientMedicalDocumentData(paramPatientMedicalDocumentData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    //Patient Document Param Instance...
                    var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                        Option = 0
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientMedicalDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumMedicalPortfolioLists.PatientDocument = dtPatientDoctor.dt.ToPatientDocumentModelList();
                    }
                 }
                
            }
            catch (Exception ex)
            {
                var response = new { message = "Uploaded File Failed." };
                return Json(response);

                throw;
            }

            /*
            string html = string.Empty;

            html = PremiumMedicalPortfolioLists.PatientDocument.ToHTMLPatientMedicalDocumentForDashBoard();
            return Json(new { Msghtml = html }, "text/html");
             */

            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/MedicalPortfolio/_PatientMedicalDocument.cshtml", PremiumMedicalPortfolioLists.PatientDocument, this);
            return Json(new { Msghtml = html }, "text/html");
        }

        [HttpPost]
        public ActionResult MedicalPortfolioDocumentDelete(string patienDocumentId)
        {
            PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();

            try
            {
             
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    
                    //Patient Medical Document Param Instance...
                    var paramPatMedDoc = new DataPatientDocumentService.PatientMedicalDocumentData
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                        DocumentCntr = long.Parse(patienDocumentId)
                    };

                    //Deleting  patient medical document data...
                    DataPatientDocumentService.PatientMedicalDocumentData dataPatientMedDoc = service.DeletePatientMedicalDocumentData(paramPatMedDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Document Param Instance...
                    var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                        Option = 0
                    };

                    //Getting patient medical document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientMedDoc = service.GetPatientMedicalDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientMedDoc.Valid)
                    {
                        PremiumMedicalPortfolioLists.PatientDocument = dtPatientMedDoc.dt.ToPatientDocumentModelList();
                    }
                }
            }
            catch //(Exception)
            {
                
                //throw;
            }

            string html = string.Empty;

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/MedicalPortfolio/_PatientMedicalDocument.cshtml", PremiumMedicalPortfolioLists.PatientDocument, this);
            return Json(html);
        }
 
        #endregion

        #region General Document Actions...
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult GeneralDocumentIndex()
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    #region General Document Data...

                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocumentParms paramPatDoc1 = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient general document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData1 = service.GetPatientDocumentList(paramPatDoc1, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    if (dtPatientDocData1.Valid)
                    {
                        PremiumGeneralDocumentLists.GeneralDocuments = dtPatientDocData1.dt.ToGeneralDocumentModelList();
                    }

                    #endregion

                    #region Insurance Policy Data...

                    //Patient Doc Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc2 = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient insurance policy data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData2 = service.GetPatientPolicyData(paramPatDoc2, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDocData2.Valid)
                    {
                        PremiumGeneralDocumentLists.InsurancePolicies = dtPatientDocData2.dt.ToInsurancePolicyModelList();
                    }

                    #endregion

                    #region Professional Advisor Data...

                    //Getting patient advisor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData3 = service.GetPatientAdvisorData(paramPatDoc2, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDocData3.Valid)
                    {
                        PremiumGeneralDocumentLists.ProfessionalAdvisors = dtPatientDocData3.dt.ToProfessionalAdvisorModelList();
                    }

                    #endregion
                }

                    #region Drop Down List Data...
                        //DropDownLists Data...
                        using (var service = new DataPatientConfigService.ConfigWSSoapClient())
                        {

                            DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();

                            //Getting Country Data...
                            ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (ConfigDocument.Valid)
                            {
                                PremiumGeneralDocumentLists.Country = ConfigDocument.dt.ToCountryModelList();
                            }

                            //Getting State Data...
                            ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (ConfigDocument.Valid)
                            {
                                PremiumGeneralDocumentLists.States = ConfigDocument.dt.ToStatesModelList();
                            }

                            //Getting Insurance Type Data...
                            ConfigDocument = service.GetPolicyTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (ConfigDocument.Valid)
                            {
                                PremiumGeneralDocumentLists.PolicyTypes = ConfigDocument.dt.ToPolicyTypeModelList();
                                int index = (PremiumGeneralDocumentLists.PolicyTypes != null) ? PremiumGeneralDocumentLists.PolicyTypes.Count : 0;
                                PremiumGeneralDocumentLists.PolicyTypes.Insert(index, new PolicyTypeModel { PolicyTypeId = -1, Value = "Other" });
                            }

                            //Getting Advisor Type Data...
                            ConfigDocument = service.GetAdvisorCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (ConfigDocument.Valid)
                            {
                                PremiumGeneralDocumentLists.AdvisorTypes = ConfigDocument.dt.ToAdvisorTypeModelList();
                                int index = (PremiumGeneralDocumentLists.AdvisorTypes != null) ? PremiumGeneralDocumentLists.AdvisorTypes.Count : 0;
                                PremiumGeneralDocumentLists.AdvisorTypes.Insert(index, new AdvisorTypeModel { AdvisorTypeId = -1, Value = "Other" });
                    }
                }
                  #endregion

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            return View(PremiumGeneralDocumentLists);
        }

        [HttpPost]
        public JsonResult GeneralDocumentFileUploader(string GeneralDocData)
        {

            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                var json_serializer = new JavaScriptSerializer();
                GeneralDocumentModel model = json_serializer.Deserialize<GeneralDocumentModel>(GeneralDocData);

                if (model.DocumentCntr == 0)
                {
                    HttpPostedFileBase myFile = Request.Files["file_upload"];

                    byte[] AttachmentData = null;
                    using (var binaryReader = new BinaryReader(Request.Files["file_upload"].InputStream))
                    {
                        AttachmentData = binaryReader.ReadBytes(Request.Files["file_upload"].ContentLength);
                    }

                    model.DocumentImage = AttachmentData;
                    model.DocumentFormat = Path.GetExtension(myFile.FileName).Replace(".", "");
                    model.DocumentId = myFile.FileName;
                }

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Param Patient Document Data Instance...
                    var paramPatientDocumentData = new DataPatientDocumentService.PatientDocumentData();
                    paramPatientDocumentData = base.ConvertToPatientGeneralDocumentData(model);
                    paramPatientDocumentData.PatientId = RequestHelper.MyGlobalVar.PatientId;


                    //Save patient document data...
                    DataPatientDocumentService.PatientDocumentData result = service.SavePatientDocumentData(paramPatientDocumentData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocumentParms paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting general document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData = service.GetPatientDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDocData.Valid)
                    {
                        PremiumGeneralDocumentLists.GeneralDocuments = dtPatientDocData.dt.ToGeneralDocumentModelList();
                    }
                 }
                
            }
            catch (Exception ex)
            {
                var response = new { message = "Uploaded File Failed." };
                return Json(response);

                throw;
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.GeneralDocuments.ToHTMLGeneralDocumentForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_GeneralDocument.cshtml", PremiumGeneralDocumentLists.GeneralDocuments, this);
            return Json(new { Msghtml = html }, "text/html");
        }

        [HttpPost]
        public JsonResult GeneralDocumentDelete(string generalDocumentId)
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();
            try
            {

                //Delete : Param Patient Document Data...
                var paramPatientDocument = new DataPatientDocumentService.PatientDocumentData { 
                    DocumentCntr = long.Parse(generalDocumentId),
                    PatientId  = RequestHelper.MyGlobalVar.PatientId
                };
                


                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Delete patient document data...
                    DataPatientDocumentService.PatientDocumentData result = service.DeletePatientDocumentData(paramPatientDocument, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocumentParms paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting general document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData = service.GetPatientDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDocData.Valid)
                    {
                        PremiumGeneralDocumentLists.GeneralDocuments = dtPatientDocData.dt.ToGeneralDocumentModelList();
                    }
                }
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.GeneralDocuments.ToHTMLGeneralDocumentForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_GeneralDocument.cshtml", PremiumGeneralDocumentLists.GeneralDocuments, this);
            return Json(html);
        }

        [HttpPost]
        public ActionResult GeneralDocumentShare(string generalDocumentId, bool doShareHide)
        {

            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();
            try
            {

                //Share/Hide : Param Patient Document...
                var paramPatientDocument = new DataPatientDocumentService.PatientDocumentParms
                {
                    DocumentCntr = long.Parse(generalDocumentId),
                    Share        = doShareHide,
                    PatientId    = RequestHelper.MyGlobalVar.PatientId
                };

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    //Share/Hide patient document data...
                    DataPatientDocumentService.PatientDocumentResp result = service.PatientDocumentShare(paramPatientDocument, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocumentParms paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting general document data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData = service.GetPatientDocumentList(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDocData.Valid)
                    {
                        PremiumGeneralDocumentLists.GeneralDocuments = dtPatientDocData.dt.ToGeneralDocumentModelList();
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(string.Format("<tr class='r1'><td colspan='6'>{0}</td></tr>", ex.Message));
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.GeneralDocuments.ToHTMLGeneralDocumentForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_GeneralDocument.cshtml", PremiumGeneralDocumentLists.GeneralDocuments, this);
            return Json(html);
        }

        public ActionResult GeneralDocumentAttachment(string patientDocumentId)
        {
            try
            {
                

                //Result...
                var result = new GeneralDocumentModel();

                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    
                        //Patient Document Param Instance...
                        var paramPatDoc = new DataPatientDocumentService.PatientDocumentParms
                        {
                            PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                            DocumentCntr = long.Parse(patientDocumentId),
                        };

                        //Getting patient doctor data...
                        DataPatientDocumentService.PatientDocumentData dataPatientDoc = service.GetPatientDocumentData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                        //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                        if (dataPatientDoc.Valid)
                        {
                            result = base.ConvertToPatientGeneralDocumentModel(dataPatientDoc);
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
        #endregion

        #region Insurance Policy Actions...

        [HttpPost]
        public ActionResult InsurancePolicySave(InsurancePolicyModel model)
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Policy Data Param Instance...
                    DataPatientDocumentService.PatientPolicyData paramPatPolicyData = base.ConvertToInsurancePolicyData(model);
                    paramPatPolicyData.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    

                    //Saving patient policy data...
                    DataPatientDocumentService.PatientPolicyData dataPatientPolicy = service.SavePatientPolicyData(paramPatPolicyData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Policy Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient policy data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData = service.GetPatientPolicyData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDocData.Valid)
                    {
                        PremiumGeneralDocumentLists.InsurancePolicies = dtPatientDocData.dt.ToInsurancePolicyModelList();
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.InsurancePolicies.ToHTMLInsurancePolicyForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_InsurancePolicy.cshtml", PremiumGeneralDocumentLists.InsurancePolicies, this);
            return Json(html);
        }

        [HttpPost]
        public ActionResult InsurancePolicyDelete(string patientPolicyId)
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Policy Data Param Instance...
                    var paramPatPolicyData = new DataPatientDocumentService.PatientPolicyData{                        
                        PatientId = RequestHelper.MyGlobalVar.PatientId,
                        PatientPolicyId = long.Parse(patientPolicyId)
                    };
                    


                    //Saving patient policy data...
                    DataPatientDocumentService.PatientPolicyData dataPatientPolicy = service.DeletePatientPolicyData(paramPatPolicyData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Policy Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient policy data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDocData = service.GetPatientPolicyData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDocData.Valid)
                    {
                        PremiumGeneralDocumentLists.InsurancePolicies = dtPatientDocData.dt.ToInsurancePolicyModelList();
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.InsurancePolicies.ToHTMLInsurancePolicyForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_InsurancePolicy.cshtml", PremiumGeneralDocumentLists.InsurancePolicies, this);
            return Json(html);
        }


        #endregion

        #region Professional Advisor Actions...

        [HttpPost]
        public ActionResult ProfessionalAdvisorSave(ProfessionalAdvisorModel model)
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Policy Data Param Instance...
                    DataPatientDocumentService.PatientAdvisorData paramPatAdvisorData = base.ConvertToProfessionalAdvisorData(model);
                    paramPatAdvisorData.PatientId = RequestHelper.MyGlobalVar.PatientId;


                    //Saving patient policy data...
                    DataPatientDocumentService.PatientAdvisorData dataPatientAdvisor = service.SavePatientAdvisorData(paramPatAdvisorData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Doc Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient advisor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoc = service.GetPatientAdvisorData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDoc.Valid)
                    {
                        PremiumGeneralDocumentLists.ProfessionalAdvisors = dtPatientDoc.dt.ToProfessionalAdvisorModelList();
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.ProfessionalAdvisors.ToHTMLProfessionalAdvisorForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_ProfessionalAdvisor.cshtml", PremiumGeneralDocumentLists.ProfessionalAdvisors, this);
            return Json(html);
        }

        [HttpPost]
        public ActionResult ProfessionalAdvisorDelete(string patientAdvisorId) 
        {
            PremiumGeneralDocumentLists = new PremiumGeneralDocumentViewModel();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Policy Data Param Instance...
                    var paramPatAdvisorData = new DataPatientDocumentService.PatientAdvisorData
                    {
                        PatientId = RequestHelper.MyGlobalVar.PatientId,
                        PatientAdvisorId = long.Parse(patientAdvisorId)
                    };



                    //Deleting patient advisor data...
                    DataPatientDocumentService.PatientAdvisorData dataPatientAdvisor = service.DeletePatientAdvisorData(paramPatAdvisorData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Patient Doc Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId)
                    };

                    //Getting patient advisor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoc = service.GetPatientAdvisorData(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (dtPatientDoc.Valid)
                    {
                        PremiumGeneralDocumentLists.ProfessionalAdvisors = dtPatientDoc.dt.ToProfessionalAdvisorModelList();
                    }
                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            string html = string.Empty;

            //html = PremiumGeneralDocumentLists.ProfessionalAdvisors.ToHTMLProfessionalAdvisorForDashBoard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/GeneralDocument/_ProfessionalAdvisor.cshtml", PremiumGeneralDocumentLists.ProfessionalAdvisors, this);
            return Json(html);
        }


        #endregion


        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult ShareMyRecordsIndex()
        {
            PremiumLists = new PremiumViewModel();
            //PatientService.PatientWS PatientWS = new PatientService.PatientWS();

            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();

            DataPatientService.CareProviderData CPData = new DataPatientService.CareProviderData();



            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);


            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                CPData = service.GetCareProviderData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (CPData.Valid)
                {
                    PremiumLists.CareProvider.Password = CPData.Password;
                    PremiumLists.CareProvider.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PremiumLists.CareProvider.PatientFullName = RequestHelper.MyGlobalVar.PatientName;
                }
            }
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                
                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                
                if (PatientSummary.Valid)
                {
                    PremiumLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                }
            }

            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientMedicalTableData medicalDocument = new DataPatientDocumentService.PatientMedicalTableData();
                DataPatientDocumentService.PatientMedicalParms medicalParms = new DataPatientDocumentService.PatientMedicalParms();
                medicalParms.Allergy = true;
                medicalParms.DoctorUploaded = true;
                medicalParms.Document = true;
                medicalParms.Emergency = true;
                medicalParms.Medication = true;
                medicalParms.PatientUploaded = true;
                medicalParms.Policy = true;
                medicalParms.Visit = true;
                medicalParms.Problem = true;
                medicalParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                medicalDocument = service.GetMedicalSummaryData(medicalParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                PremiumBaseViewModel PremiumList = new PremiumBaseViewModel();
                if (medicalDocument.Valid)
                {
                    PremiumLists.Visits = medicalDocument.dtVisit.ToVisitModelList();
                    PremiumLists.Allergies = medicalDocument.dtAllergy.ToAllergyModelList();
                    PremiumLists.Medications = medicalDocument.dtMedication.ToMedicationHistoryModelList();
                    PremiumLists.Problems = medicalDocument.dtProblem.ToProblemModelList();
                    PremiumLists.Emergencies = medicalDocument.dtEmergency.ToEmergencyModelList();
                    PremiumLists.DoctorUploads = medicalDocument.dtDoctorUploaded.ToDoctorUploadModelList();
                    PremiumLists.PatientUploads = medicalDocument.dtPatientUploaded.ToPatientUploadModelList();
                    PremiumLists.Policies = medicalDocument.dtPolicy.ToPolicyModelList();
                    PremiumLists.Documents = medicalDocument.dtDocument.ToDocumentModelList();

                }

            }



            //txtCPPassword.Text = CPData.Password;
            return View(PremiumLists);
        }

        [HttpPost]
        public JsonResult PasscodeSave(string Password)
        {
            string html = string.Empty;
            PremiumLists = new PremiumViewModel();

            // DataPatientService.PatientWS PatientWS = new DataPatientService.PatientWS();
            DataPatientService.CareProviderData CPData = new DataPatientService.CareProviderData();



            CPData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

            CPData.PatientFullName = RequestHelper.MyGlobalVar.PatientName;

            CPData.Password = Password;


            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                CPData = service.SaveCareProviderData(CPData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (CPData.Valid)
                {
                    html = CPData.Password;
                }
            }


            return Json(html);

        }

        [HttpPost]
        public JsonResult SendEmail(string EmailAddress, string  Password)
        {
            string html = string.Empty;


            PremiumLists = new PremiumViewModel();

            // DataPatientService.PatientWS PatientWS = new DataPatientService.PatientWS();

            DataPatientService.PatientResp Resp = new DataPatientService.PatientResp();
            DataPatientService.CareProviderEmail CPData = new DataPatientService.CareProviderEmail();
            CPData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            CPData.Password = Password;
            CPData.PatientName = RequestHelper.MyGlobalVar.PatientName;
            CPData.EmailAddress = EmailAddress;


            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                Resp = service.SendOutsideProviderEmail(CPData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (!Resp.Valid)
                {
                    html = Resp.ErrorMessage;
                }
            }


            return Json(html);

        }


        #region Care Provider Actions...

        public ViewResult CareProviderLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CareProviderLogin(CareProviderModel model)
        {
           
            GlobalVar objGlobalVar = new GlobalVar();

            objGlobalVar.UserLogin = model.PatientId.ToString();
            DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();



            try
            {
                using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                {

                    ResponseData = service.AuthenticateCareProvider(model.PatientId.ToString(), model.PatientFullName, model.Password);

                }




                if (ResponseData.Valid)
                {

                    

                    objGlobalVar.UserId = ResponseData.UserId;

                    objGlobalVar.Token = ResponseData.Token;
                    objGlobalVar.FacilityId = 0;

                    if (objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                    {
                        TicketHelper.CreateCareProviderAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true, "CareProvider");
                        return RedirectToAction("CareProviderIndex", "Premium");
                    }

                   // Session["Token"] = ResponseData.Token;
                  //  Session["UserId"] = ResponseData.UserId;
                  

                  //  string test = objGlobalVar.UserId + ",0," + objGlobalVar.Token + ",,0,,,false";

                  //   objGlobalVar.SetData(test);

                    
                }

                else
                {
                    Session["Token"] = "";
                  //  objGlobalVar.Token = "";
                    model.ErrorMessage = ResponseData.ErrorMessage;
                    //  MessageBox.Show(ResponseData.ErrorMessage);

                }

            }

            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }


            return View();
        }

        public ActionResult CareProviderIndex()
        {
            HttpCookie testcookie = Request.Cookies["CareProvider"];
            if (testcookie != null)
            {
                PremiumLists = new PremiumViewModel();
                var value = testcookie.Value;
                string token = RequestHelper.MyCareProviderGlobalVar.Token;
                string userId = RequestHelper.MyCareProviderGlobalVar.UserId.ToString();
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();


                    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyCareProviderGlobalVar.UserLogin);


                    PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyCareProviderGlobalVar.Token, RequestHelper.MyCareProviderGlobalVar.UserId, RequestHelper.MyCareProviderGlobalVar.FacilityId);

                    if (PatientSummary.Valid)
                    {
                        PremiumLists.CareProvider.PatientFullName = PatientSummary.FirstName + " " + PatientSummary.LastName;
                        PremiumLists.CareProvider.DOB = PatientSummary.DOB;

                    }
                }
                //Patient Document Service Instance...
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Document Param Instance...
                    DataPatientDocumentService.PatientDocParms paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyCareProviderGlobalVar.UserLogin)
                    };

                    //Getting patient doctor data...
                    DataPatientDocumentService.PatientDocTableData dtPatientDoctor = service.GetPatientDoctorData(paramPatDoc, RequestHelper.MyCareProviderGlobalVar.Token, RequestHelper.MyCareProviderGlobalVar.UserId, RequestHelper.MyCareProviderGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    if (dtPatientDoctor.Valid)
                    {
                        PremiumLists.PatientDoctor = dtPatientDoctor.dt.ToPatientDoctorModelList();
                    }


                }
            }
            else
            {
                return RedirectToAction("CareProviderLogin", "Premium");
            }
            
          


                //Session["Token"].ToString();
            
           // GlobalVar objGlobalVar = new GlobalVar();

           // string val = objGlobalVar.GetData();
            return View(PremiumLists);
        }

        [HttpPost]
        public JsonResult CareProviderIndex(PatientCareDocumentDataModel Model)
        {
            PremiumLists = new PremiumViewModel();
            
                DataPatientDocumentService.PatientCareDocumentData CareDoc = new DataPatientDocumentService.PatientCareDocumentData();
                CareDoc.DoctorName = Model.DoctorName;
                CareDoc.DocumentDescription = Model.DocumentDescription;
                CareDoc.Notes = Model.Notes;
                CareDoc.DocumentCntr = 0;
                CareDoc.PatientId = Convert.ToInt64(RequestHelper.MyCareProviderGlobalVar.UserLogin);
                CareDoc.Viewable = false;
                HttpPostedFileBase myFile = Request.Files["MyAttachFile"];
                bool isUploaded = false;
                string message = "File uploaded";
                if (myFile != null)
                {
                    byte[] AttachmentData = null;
                    using (var binaryReader = new BinaryReader(Request.Files["MyAttachFile"].InputStream))
                    {
                        AttachmentData = binaryReader.ReadBytes(Request.Files["MyAttachFile"].ContentLength);
                    }
                    string ImageFormat = System.IO.Path.GetExtension(myFile.FileName);
                    var newformat = ImageFormat.Substring(1, ImageFormat.Length - 1);



                    CareDoc.DocumentImage = AttachmentData;
                    CareDoc.DocumentId = myFile.FileName;
                    CareDoc.DocumentFormat = newformat;
                }
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    CareDoc = service.SavePatientCareDocumentData(CareDoc, RequestHelper.MyCareProviderGlobalVar.Token, RequestHelper.MyCareProviderGlobalVar.UserId, RequestHelper.MyCareProviderGlobalVar.FacilityId);
            }
                if (Request.Cookies["CareProvider"] != null)
                {
                   // HttpCookie myCookie = new HttpCookie("CareProvider");
                   // //Request.Cookies["CareProvider"].Expires 
                   // myCookie.Expires = DateTime.Now.AddDays(-1d);

                   // Response.Cookies.Add(myCookie);
                   
                }
            string html = string.Empty;
            return Json(new { Msghtml = html, message = message }, "text/html");
        }

        public ActionResult CareProviderLogOut()
        {
            if (Request.Cookies["CareProvider"] != null)
            {
                HttpCookie myCookie = new HttpCookie("CareProvider");
                //Request.Cookies["CareProvider"].Expires 
                  myCookie.Expires  = DateTime.Now.AddDays(-1d);
                
                Response.Cookies.Add(myCookie);
                return RedirectToAction("CareProviderIndex", "Premium");
            }
            return View();
        }

        
            public ViewResult MedicalSummaryReportLogin()
        {
            return View();
        }

        [HttpPost]
            public ActionResult MedicalSummaryReportLogin(CareProviderModel model)
            {
                PremiumLists = new PremiumViewModel();
                GlobalVar objGlobalVar = new GlobalVar();

                objGlobalVar.UserLogin = model.PatientId.ToString();
                DataAuthenticationService.AuthenticationResponse ResponseData = new DataAuthenticationService.AuthenticationResponse();



                try
                {
                    using (var service = new DataAuthenticationService.AuthenticationWSSoapClient())
                    {
                        ResponseData = service.AuthenticateMedicalSummary(model.PatientId.ToString(), model.PatientFullName);
                        //ResponseData = service.AuthenticateCareProvider(model.PatientId.ToString(), model.PatientFullName, model.Password);

                    }




                    if (ResponseData.Valid)
                    {
                        objGlobalVar.UserId = ResponseData.UserId;

                        objGlobalVar.Token = ResponseData.Token;
                        objGlobalVar.FacilityId = 0;

                        if (objGlobalVar.UserLogin != "" && objGlobalVar.GetData() != "")
                        {
                            TicketHelper.CreateCareProviderAuthCookie(objGlobalVar.UserLogin, objGlobalVar.GetData(), true, "MedicalSummary");
                            return RedirectToAction("MedicalSummaryReportIndex", "Premium");
                        }

                        


                    }

                    else
                    {
                       
                        PremiumLists.CareProvider.ErrorMessage = ResponseData.ErrorMessage;
                    
                        

                    }

                }

                catch (Exception ex)
                {
                    PremiumLists.CareProvider.ErrorMessage = ex.Message;
                }


                return View(PremiumLists);

               
            }


        public ActionResult MedicalSummaryReportIndex()
        {
            if (Request.Cookies["MedicalSummary"] != null)
            {
               // HttpCookie myCookie = new HttpCookie("MedicalSummary");
              
              //  myCookie.Expires = DateTime.Now.AddDays(-1d);

              //  Response.Cookies.Add(myCookie);
                PremiumLists = new PremiumViewModel();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                using (var service = new DataPatientService.PatientWSSoapClient())
                {

                    PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                    if (PatientSummary.Valid)
                    {
                        PremiumLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                    }
                }

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientMedicalTableData medicalDocument = new DataPatientDocumentService.PatientMedicalTableData();
                    DataPatientDocumentService.PatientMedicalParms medicalParms = new DataPatientDocumentService.PatientMedicalParms();
                    medicalParms.Allergy = true;
                    medicalParms.DoctorUploaded = true;
                    medicalParms.Document = true;
                    medicalParms.Emergency = true;
                    medicalParms.Medication = true;
                    medicalParms.PatientUploaded = true;
                    medicalParms.Policy = true;
                    medicalParms.Visit = true;
                    medicalParms.Problem = true;
                    medicalParms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                    medicalDocument = service.GetMedicalSummaryData(medicalParms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);
                    PremiumBaseViewModel PremiumList = new PremiumBaseViewModel();
                    if (medicalDocument.Valid)
                    {
                        PremiumLists.Visits = medicalDocument.dtVisit.ToVisitModelList();
                        PremiumLists.Allergies = medicalDocument.dtAllergy.ToAllergyModelList();
                        PremiumLists.Medications = medicalDocument.dtMedication.ToMedicationHistoryModelList();
                        PremiumLists.Problems = medicalDocument.dtProblem.ToProblemModelList();
                        PremiumLists.Emergencies = medicalDocument.dtEmergency.ToEmergencyModelList();
                        PremiumLists.DoctorUploads = medicalDocument.dtDoctorUploaded.ToDoctorUploadModelList();
                        PremiumLists.PatientUploads = medicalDocument.dtPatientUploaded.ToPatientUploadModelList();
                        PremiumLists.Policies = medicalDocument.dtPolicy.ToPolicyModelList();
                        PremiumLists.Documents = medicalDocument.dtDocument.ToDocumentModelList();

                    }

                }
                
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            return View(PremiumLists);
        }

   


        #endregion
    }

}
