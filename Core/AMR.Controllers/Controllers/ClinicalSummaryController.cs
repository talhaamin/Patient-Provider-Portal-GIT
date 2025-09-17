using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AMR.Models;
using AMR.Core.Extensions;
using AMR.Core.Utilities;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    public class ClinicalSummaryController : Base.BaseController
    {

        [HttpPost]
        public JsonResult FilterImmunizationData(ImmunizationModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }
            
            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
          
            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.Immunizations.GetPatientImmunizationModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Immunization.cshtml", ClinicalSummaryLists.Immunizations, this);
            return Json(html);
        }

        
        [HttpPost]
        public JsonResult FilterPOCData(POCModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatParms.VisitId = Model.VisitId;
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            PatParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientPlanOfCareData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.Pocs = PatientDocument.dt.ToPOCModelList();


                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Pocs.GetPatientPOCModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("_Index_PlanofCare", ClinicalSummaryLists.Pocs, this);
            return Json(html);
        }


        

         [HttpPost]
        [Authorize]
        public JsonResult FillVisitDropDown(PatientVisitDataModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            smodel.facilityOptions = Model.facilityOptions;
            smodel.facilitySelected = Model.facilitySelected;
            smodel.visitOptions = Model.visitOptions;
            smodel.visitSelected = Model.visitSelected;
            Session["OptionSelected"] = smodel;

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

            PatParms.VisitId = Model.VisitId;
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            PatParms.Option = 2;
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
            html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
            return Json(html);
        }

         [HttpPost]
         public JsonResult FillProcedureDropDown(ProcedureModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            smodel.facilityOptions = Model.facilityOptions;
            smodel.facilitySelected = Model.facilitySelected;
            smodel.visitOptions = Model.visitOptions;
            smodel.visitSelected = Model.visitSelected;
            Session["OptionSelected"] = smodel;

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

            PatParms.VisitId = Model.VisitId;
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
            return Json(html);
        }


         [HttpPost]
         public JsonResult FilterPlanOFCaredropdownData(POCModel Model)
         {
             ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
             FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
             smodel.facilityOptions = Model.facilityOptions;
             smodel.facilitySelected = Model.facilitySelected;
             smodel.visitOptions = Model.visitOptions;
             smodel.visitSelected = Model.visitSelected;
             Session["OptionSelected"] = smodel;

             DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
             DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

             PatParms.VisitId = Model.VisitId;
             PatParms.FacilityId = Model.FacilityId;
             PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
             html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
             return Json(html);
         
         }
         [HttpPost]
         public JsonResult FilterInsurancedropdownData(InsurancePolicyModel Model)
         {
             ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
             FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
             smodel.facilityOptions = Model.facilityOptions;
             smodel.facilitySelected = Model.facilitySelected;
             smodel.visitOptions = Model.visitOptions;
             smodel.visitSelected = Model.visitSelected;
             Session["OptionSelected"] = smodel;

             DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
             DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

             PatParms.VisitId = Model.VisitId;
             PatParms.FacilityId = Model.FacilityId;
             PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
             html = ClinicalSummaryLists.Visits.GetPatientVisitDropDown(Model.ExtensionIdName, Model.ExtensionFilterFunct, Model.ExtensionToggleFunct);
             return Json(html);
         
         
         }
         [HttpPost]
         public JsonResult FilterPlanOfCareData(POCModel Model)
         {
             ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


             DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
             DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
             PatDocParms.VisitId = Model.VisitId;
             PatDocParms.FacilityId = Model.FacilityId;
             PatDocParms.Option = 1;
             PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
             try
             {
                 using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                 {
                     PatientDocument = service.GetPatientPlanOfCareData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                     if (PatientDocument.Valid)
                     {


                         ClinicalSummaryLists.Pocs = PatientDocument.dt.ToPOCModelList();

                     }
                 }
             }
             catch (Exception ex)
             {
                 return Json(ex.Message);
             }
             string html = string.Empty;
             //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
             html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_PlanOfCare.cshtml", ClinicalSummaryLists.Pocs , this);
             return Json(html);

         
         
        }

        
            [HttpPost]
         public JsonResult FilterInsuranceData(PatientInsuranceModel Model)
         {
             ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


             DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
             DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
             PatDocParms.VisitId = Model.VisitId;
             PatDocParms.FacilityId = Model.FacilityId;
             PatDocParms.Option = 1;
             PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
             try
             {
                 using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                 {
                     PatientDocument = service.GetPatientInsuranceData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                     if (PatientDocument.Valid)
                     {


                         ClinicalSummaryLists.Insurance = PatientDocument.dt.ToInsuranceModelList();

                     }
                 }
             }
             catch (Exception ex)
             {
                 return Json(ex.Message);
             }
             string html = string.Empty;
             //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
             html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Insurance.cshtml", ClinicalSummaryLists.Insurance , this);
             return Json(html);

         
         
        }
         [HttpPost]
            public JsonResult FilterClinicalInstructionsData(POCModel Model)
         {
             ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


             DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
             DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
             PatDocParms.VisitId = Model.VisitId;
             PatDocParms.FacilityId = Model.FacilityId;
             PatDocParms.Option = 1;
             PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
             try
             {
                 using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                 {
                     PatientDocument = service.GetPatientClinicalInstructionsData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                     if (PatientDocument.Valid)
                     {


                         ClinicalSummaryLists.ClinicalInstructions = PatientDocument.dt.ToPOCModelList(); 

                     }
                 }
             }
             catch (Exception ex)
             {
                 return Json(ex.Message);
             }
             string html = string.Empty;
             //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
             html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_ClinicalInstructions.cshtml", ClinicalSummaryLists.ClinicalInstructions, this);
             return Json(html);

         
         
        }

        
        [HttpPost]
        public JsonResult FilterLabData(LabResultModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }
                

            DataPatientDocumentService.PatientLabResultParms PatDocParms = new DataPatientDocumentService.PatientLabResultParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId); 
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
            //html = ClinicalSummaryLists.LabResults.GetPatienLabResultModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_LabTests.cshtml", ClinicalSummaryLists.LabResults, this);

            return Json(html);
        }

        [HttpPost]
        public JsonResult FilterVisitData(VisitModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }

            DataPatientService.PatientVisitParms PatParms = new DataPatientService.PatientVisitParms();
            DataPatientService.PatientVisitTableData PatientDocument = new DataPatientService.PatientVisitTableData();

             
            PatParms.FacilityId = Model.FacilityId;
            PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.Visits.GetPatientVisitModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Visits.cshtml", ClinicalSummaryLists.Visits, this);
            return Json(html);
        }

        [HttpPost]
        public JsonResult FilterProvidersData(ProviderInfoModel Model)
        {
           List<ProviderInfoModel> ProviderInfo = new List<ProviderInfoModel>();
            //ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
                //   RequestHelper.MyGlobalVar.FacilityId
                ProviderList = service.GetPatientProviderInfoList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),Model.FacilityID, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderList.Valid)
                {
                    ProviderInfo = ProviderList.dt.ToProivderInfoList();
                }
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.Visits.GetPatientVisitModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Providers.cshtml", ProviderInfo, this);
            return Json(html);
        }
        [HttpPost]
        public JsonResult FilterAllergyData(AllergyModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId); 
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
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Allergies.cshtml", ClinicalSummaryLists.Allergies, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterProblemData(ProblemModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId); 
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
            //html = ClinicalSummaryLists.Problems.GetPatientProblemModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Problems.cshtml", ClinicalSummaryLists.Problems, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterVitalData(PatientVitalSignModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.VitalSigns.GetPatientVitalSignModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Vitals.cshtml", ClinicalSummaryLists.VitalSigns, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterDocumentData(ConsolidateCallModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }

            string html = string.Empty;

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.visitId;
            PatDocParms.FacilityId = Model.facilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientVitalSignData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                      
                        html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Document.cshtml", ClinicalSummaryLists.Documents, this);

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            return Json(html);
        }

        

        [HttpPost]
        public JsonResult FilterSocialData(SocialHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            if (Model.facilityOptions != null && Model.visitOptions != null)
            {
                smodel.facilityOptions = Model.facilityOptions;
                smodel.facilitySelected = Model.facilitySelected;
                smodel.visitOptions = Model.visitOptions;
                smodel.visitSelected = Model.visitSelected;
                Session["OptionSelected"] = smodel;
            }

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.SocialHistories.GetPatientSocialHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Social.cshtml", ClinicalSummaryLists.SocialHistories, this);
            return Json(html);
        }



        [HttpPost]
        public JsonResult FilterFamilyData(FamilyHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.FamilyHistories.GetPatientFamilyHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Family.cshtml", ClinicalSummaryLists.FamilyHistories, this);
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterPastData(MedicalHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
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
            //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Medical.cshtml", ClinicalSummaryLists.MedicalHistories, this);
            return Json(html);
        }

        [HttpPost]
        public JsonResult FilterProcedureData(ProcedureModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId =Model.FacilityId;
            PatDocParms.Option = 1;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientProcedureData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        ClinicalSummaryLists.Procedures = PatientDocument.dt.ToProcedureModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Procedures.cshtml", ClinicalSummaryLists.Procedures, this);
            return Json(html);

        }



        [HttpPost]
        public JsonResult ProcedureDelete(ProcedureModel Model)
        {
            ClinicalSummaryViewModel HomeList = new ClinicalSummaryViewModel();
            DataPatientDocumentService.PatientProcedureData PatDocParms = new DataPatientDocumentService.PatientProcedureData();
            DataPatientDocumentService.PatientProcedureData PatDocParmsreturn = new DataPatientDocumentService.PatientProcedureData();

            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            PatDocParms.PatProcedureCntr = Model.PatProcedureCntr;
            PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            PatDocParms.VisitId = 0;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatDocParmsreturn =service.DeletePatientProcedureData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatDocParmsreturn.Valid)
                    {

                       // DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                        DataPatientDocumentService.PatientDocParms PatientDoc = new DataPatientDocumentService.PatientDocParms();
                
                        PatientDoc.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatientDoc.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatientDoc.VisitId = 0;
                        PatientDoc.Option = 1;
                        using (var service1 = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                        {
                            PatientDocument = service1.GetPatientProcedureData(PatientDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (PatientDocument.Valid)
                            {
                                HomeList.Procedures = PatientDocument.dt.ToProcedureModelList();
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
            string html1 = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Shared/_Index_Procedures.cshtml", HomeList.Procedures, this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Procedures.cshtml", HomeList.Procedures, this);

            return Json(new { html = html, html1 = html1 });
        }

        [HttpPost]
        public JsonResult POCDelete(POCModel Model)
        {
            ClinicalSummaryViewModel HomeList = new ClinicalSummaryViewModel();
            DataPatientDocumentService.PatientPlanOfCareData PatDocParms = new DataPatientDocumentService.PatientPlanOfCareData();
            DataPatientDocumentService.PatientPlanOfCareData PatDocParmsreturn = new DataPatientDocumentService.PatientPlanOfCareData();

            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            PatDocParms.PlanCntr = Model.PlanCntr;
            PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            PatDocParms.VisitId = 0;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatDocParmsreturn = service.DeletePatientPlanOfCareData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatDocParmsreturn.Valid)
                    {

                        // DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                        DataPatientDocumentService.PatientDocParms PatientDoc = new DataPatientDocumentService.PatientDocParms();

                        PatientDoc.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatientDoc.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatientDoc.VisitId = 0;
                        PatientDoc.Option = 1;
                        using (var service1 = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                        {
                            PatientDocument = service1.GetPatientPlanOfCareData(PatientDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (PatientDocument.Valid)
                            {
                                HomeList.Pocs = PatientDocument.dt.ToPOCModelList();
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
            string html1 = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Shared/_Index_PlanofCare.cshtml", HomeList.Pocs, this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_PlanOfCare.cshtml", HomeList.Pocs, this);

            return Json(new { html = html, html1 = html1 });
        
        }

        [HttpPost]
        public JsonResult ClinicalInstructionDelete(POCModel Model)
        {
            ClinicalSummaryViewModel HomeList = new ClinicalSummaryViewModel();
            DataPatientDocumentService.PatientPlanOfCareData PatDocParms = new DataPatientDocumentService.PatientPlanOfCareData();
            DataPatientDocumentService.PatientPlanOfCareData PatDocParmsreturn = new DataPatientDocumentService.PatientPlanOfCareData();

            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
            PatDocParms.PlanCntr = Model.PlanCntr;
            PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            PatDocParms.VisitId = 0;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatDocParmsreturn = service.DeletePatientPlanOfCareData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatDocParmsreturn.Valid)
                    {

                        // DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                        DataPatientDocumentService.PatientDocParms PatientDoc = new DataPatientDocumentService.PatientDocParms();

                        PatientDoc.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatientDoc.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatientDoc.VisitId = 0;
                        PatientDoc.Option = 1;
                        using (var service1 = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                        {
                            PatientDocument = service1.GetPatientClinicalInstructionsData(PatientDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (PatientDocument.Valid)
                            {
                                HomeList.ClinicalInstructions = PatientDocument.dt.ToPOCModelList();
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
            string html1 = string.Empty;
           
           html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_ClinicalInstructions.cshtml", HomeList.ClinicalInstructions, this);
           html1 = ViewHelper.RenderRazorViewToString("_Index_ClinicalInstructions", HomeList.ClinicalInstructions, this);

            //return Json(html);
           return Json(new { html = html, html1 = html1 });
        }
        public ActionResult ClinicalSummaryMedicalPrint()
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();


            string visitId = Request.QueryString["visitId"];
            if (Request.Cookies["MedicalSummary"] != null)
            {

            }
            else
            {
            // Patients Document Related data which includes labs, allergies and all histories
               
            }

            return View(ClinicalSummaryLists);
        }


        public ActionResult ClinicalSummaryPrint()
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();

            HomeViewModel HomeLists = new HomeViewModel();
            string visitId = Request.QueryString["visitId"];
            string facilityId = Request.QueryString["facilityId"];
            if (Request.Cookies["MedicalSummary"] != null)
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

                    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();

                    parms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                    parms.VisitId = (!string.IsNullOrEmpty(visitId)) ? long.Parse(visitId) : 0;
                    parms.FacilityId = (!string.IsNullOrEmpty(facilityId)) ? long.Parse(facilityId) : 0;
                        //RequestHelper.MyMedicalSummaryGlobalVar.FacilityId;
                    parms.Active = 1;
                    parms.Option = 1;
                    parms.Allergy = true;
                    parms.FamilyHist = true;
                    parms.SocialHist = true;
                    parms.MedicalHist = true;
                    parms.Problem = true;
                    parms.Procedure = true;
                    parms.PlanOfCare = true;
                    parms.VitalSign = true;
                    parms.Medication = true;
                    parms.Lab = true;
                    parms.Immunization = true;
                    parms.ClinicalDocs = true;
                    parms.Insurance = true;
                   
                    PatientDocument = service.GetPatientSummaryData(parms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                    if (PatientDocument.Valid)
                    {
                        ClinicalSummaryLists.SocialHistories = PatientDocument.dtSocialHist.ToSocialHistoryModelList();
                        ClinicalSummaryLists.VitalSigns = PatientDocument.dtVitalSign.ToVitalsignModelList();
                        ClinicalSummaryLists.MedicalHistories = PatientDocument.dtMedicalHist.ToMedicalHistoryModelList();
                        ClinicalSummaryLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                        ClinicalSummaryLists.Problems = PatientDocument.dtProblem.ToProblemModelList();
                        ClinicalSummaryLists.LabResults = PatientDocument.dtLab.ToLabResultModelList();
                       // ClinicalSummaryLists.LabResultTest = PatientDocument.dtLabDetail.ToLabResultTestModelList();
                        ClinicalSummaryLists.LabResultTest = PatientDocument.dtLabResult.ToLabResultTestModelList();
                        ClinicalSummaryLists.Immunizations = PatientDocument.dtImmunization.ToImmunizationModelList();
                        ClinicalSummaryLists.Allergies = PatientDocument.dtAllergy.ToAllergyModelList();
                        ClinicalSummaryLists.Procedures = PatientDocument.dtProcedure.ToProcedureModelList();
                        ClinicalSummaryLists.Pocs = PatientDocument.dtPlanOfCare.ToPOCModelList();
                        ClinicalSummaryLists.Documents = PatientDocument.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                        ClinicalSummaryLists.Insurance = PatientDocument.dtInsurance.ToInsuranceModelList();
                    }
                    // Patients Related data which includes Patient demographics, visits and facilities
                    using (var service1 = new DataPatientService.PatientWSSoapClient())
                    {
                        DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                        DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                        PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                        VisitData = service1.GetPatientVisitList(PatVisitParms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                        if (VisitData.Valid)
                        {
                            ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
                        }

                    }
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);
                    using (var Summservice = new DataPatientService.PatientWSSoapClient())
                    {

                        PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);

                        if (PatientSummary.Valid)
                        {
                            ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                        }
                    }
                    DataPatientDocumentService.PatientVisitCCD Visits = new DataPatientDocumentService.PatientVisitCCD();
                    DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
                    VisitParams.FacilityId = (!string.IsNullOrEmpty(facilityId)) ? long.Parse(facilityId) : 0;
                    VisitParams.VisitId = (!string.IsNullOrEmpty(visitId)) ? long.Parse(visitId) : 0;
                    VisitParams.PatientId = Convert.ToInt64(RequestHelper.MyMedicalSummaryGlobalVar.UserLogin);

                    Visits = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyMedicalSummaryGlobalVar.Token, RequestHelper.MyMedicalSummaryGlobalVar.UserId, RequestHelper.MyMedicalSummaryGlobalVar.FacilityId);
                    if (Visits.Valid)
                    {
                        ClinicalSummaryLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(Visits);
                    }
                }
            }
            else
            {

                //// Patients Document Related data which includes labs, allergies and all histories
                //using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                //{

                //    DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

                //    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                //    DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
                //    DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
                //    using (var service1 = new DataPatientService.PatientWSSoapClient())
                //    {
                //        PatParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                //        PatShare = service1.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //        ClinicalSummaryLists.PatientShare = base.ConvertToPatientShareModel(PatShare);

                //      //  HomeLists1.PatientShare = base.ConvertToPatientShareModel(PatShare);
                //    }
                //    parms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                //    parms.VisitId = (!string.IsNullOrEmpty(visitId)) ? long.Parse(visitId) : 0;
                //    parms.FacilityId = (!string.IsNullOrEmpty(facilityId)) ? long.Parse(facilityId) : 0;
                //    //RequestHelper.MyMedicalSummaryGlobalVar.FacilityId;
                //    parms.Option = 1;
                //    parms.Allergy = PatShare.Allergy;
                //    parms.FamilyHist = PatShare.FamilyHistory;
                //    parms.SocialHist = PatShare.SocialHistory;
                //    parms.MedicalHist = PatShare.MedicalHistory;
                //    parms.Problem = PatShare.Problem;
                //    parms.Procedure = PatShare.Procedure;
                //    parms.PlanOfCare = PatShare.PlanOfCare;
                //    parms.VitalSign = PatShare.VitalSigns;
                //    parms.Medication = PatShare.Medication;
                //    parms.Lab = PatShare.LabResults;
                //    parms.Immunization = PatShare.Immunization;
                //    parms.ClinicalDocs = PatShare.ClinicalDoc;
                //    parms.Insurance = PatShare.Insurance;
                //    parms.Visit = PatShare.Visit;

                //    PatientDocument = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                //    if (PatientDocument.Valid)
                //    {
                //        if (parms.SocialHist == true)
                //        {
                //        ClinicalSummaryLists.SocialHistories = PatientDocument.dtSocialHist.ToSocialHistoryModelList();
                //        }
                //        if (parms.VitalSign == true)
                //        {
                //        ClinicalSummaryLists.VitalSigns = PatientDocument.dtVitalSign.ToVitalsignModelList();
                //        }
                //        if (parms.MedicalHist == true)
                //        {
                //        ClinicalSummaryLists.MedicalHistories = PatientDocument.dtMedicalHist.ToMedicalHistoryModelList();
                //        }
                //        if (parms.Medication == true)
                //        {
                //        ClinicalSummaryLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                //        }
                //        if (parms.Problem == true)
                //        {
                //        ClinicalSummaryLists.Problems = PatientDocument.dtProblem.ToProblemModelList();
                //        }
                //        if (parms.Lab == true)
                //        {
                //        ClinicalSummaryLists.LabResults = PatientDocument.dtLab.ToLabResultModelList();
                //        ClinicalSummaryLists.LabResultTest = PatientDocument.dtLabResult.ToLabResultTestModelList();
                //        }
                //        if (parms.Immunization)
                //        {
                //        ClinicalSummaryLists.Immunizations = PatientDocument.dtImmunization.ToImmunizationModelList();
                //        }
                //        if (parms.Allergy == true)
                //        {
                //        ClinicalSummaryLists.Allergies = PatientDocument.dtAllergy.ToAllergyModelList();
                //        }
                //        if (parms.Procedure == true)
                //        {
                //        ClinicalSummaryLists.Procedures = PatientDocument.dtProcedure.ToProcedureModelList();
                //        }
                //        if (parms.PlanOfCare == true)
                //        {
                //        ClinicalSummaryLists.Pocs = PatientDocument.dtPlanOfCare.ToPOCModelList();
                //        }
                //        if (parms.ClinicalDocs == true)
                //        {
                //        ClinicalSummaryLists.Documents = PatientDocument.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                //        }
                //        if (parms.Insurance == true)
                //        {
                //        ClinicalSummaryLists.Insurance = PatientDocument.dtInsurance.ToInsuranceModelList();
                //        }
                //    }
                //    // Patients Related data which includes Patient demographics, visits and facilities
                //    using (var service1 = new DataPatientService.PatientWSSoapClient())
                //    {
                //        DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                //        DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                //        PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                //        VisitData = service1.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                //        if (VisitData.Valid)
                //        {
                //            if (parms.Visit == true)
                //            {
                //            ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
                //        }
                //        }

                //    }
                //    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                //    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                //    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                //    using (var Summservice = new DataPatientService.PatientWSSoapClient())
                //    {

                //        PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                //        if (PatientSummary.Valid)
                //        {
                //            ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                //        }
                //    }
                //    DataPatientDocumentService.PatientVisitCCD Visits = new DataPatientDocumentService.PatientVisitCCD();
                //    DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
                //    VisitParams.FacilityId = (!string.IsNullOrEmpty(facilityId)) ? long.Parse(facilityId) : 0;
                //        //Convert.ToInt64(TempData["FacilityId"]);
                //    VisitParams.VisitId = (!string.IsNullOrEmpty(visitId)) ? long.Parse(visitId) : 0;
                //        //Convert.ToInt64(TempData["VisitId"]);
                //    VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;

                //    Visits = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //    if (Visits.Valid)
                //        {
                //            ClinicalSummaryLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(Visits);
                //            //Model = base.ConvertPatientVisitCCDModel(VisitData);
                //        }

                //}
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
                DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
                VisitParams.FacilityId = Convert.ToInt64(facilityId);
                VisitParams.VisitId = Convert.ToInt64(visitId);
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
                    parms.VisitId = Convert.ToInt64(visitId);
                    parms.FacilityId = Convert.ToInt64(facilityId);
                    parms.ClinicalSummary = true;
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
        }
            return View(HomeLists);
        }

        [Authorize]
        public ActionResult ClinicalSummary()
        {
           // ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            HomeViewModel HomeLists = new HomeViewModel();
            //string facilityId = RequestHelper.MyGlobalVar.FacilityId.ToString();
            //string visitId = "0";
            //Patient visit and facility dropdowns
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
            HomeLists.FacilityVisitSelect = smodel;
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
                    HomeLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                    HomeLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                }
                //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (MessageUrgencyType.Valid)
                //{
                //    HomeLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                //    HomeLists.MessageUrgencyType.Insert(0, new MessageUrgency { MessageUrgencyId = -1, Value = "--Select--" });
                //}

            }
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientVisitTableData VisitDatas = new DataPatientService.PatientVisitTableData();

                DataPatientService.PatientParms PatParmss = new DataPatientService.PatientParms();
                DataPatientService.PatientVisitParms PatVisitParmss = new DataPatientService.PatientVisitParms();

                PatParmss.PatientId = RequestHelper.MyGlobalVar.PatientId;

                FacilityData = service.GetPatientFacilityList(PatParmss, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);

                if (FacilityData.Valid)
                {
                    HomeLists.Facilities = FacilityData.dt.ToFacilityModelList();
                    HomeLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    HomeLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }

                PatVisitParmss.PatientId = RequestHelper.MyGlobalVar.PatientId; //Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                if (smodel != null)
                {
                    PatVisitParmss.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                }
                else
                {
                    PatVisitParmss.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                }
                PatVisitParmss.VisitId = RequestHelper.MyGlobalVar.VisitId;
                PatVisitParmss.PatientId = RequestHelper.MyGlobalVar.PatientId;
                PatVisitParmss.Option = 3;
                VisitDatas = service.GetPatientVisitList(PatVisitParmss, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (VisitDatas.Valid)
                {
                    HomeLists.Visits = VisitDatas.dt.ToVisitModelList();
                }


            }
            //    // Patients Document Related data which includes labs, allergies and all histories
            //    using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            //    {

            //        DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

            //        DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            //        DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
            //        DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            //        using (var service1 = new DataPatientService.PatientWSSoapClient())
            //        {
            //            PatParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            //            PatShare = service1.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //            ClinicalSummaryLists.PatientShare = base.ConvertToPatientShareModel(PatShare);

            //            //  HomeLists1.PatientShare = base.ConvertToPatientShareModel(PatShare);
            //        }
            //        parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //        if (smodel != null)
            //        {
            //            parms.VisitId = Convert.ToInt64(smodel.visitSelected);
            //            parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
            //            ClinicalSummaryLists.FacilityVisitSelect = smodel;
            //        }
            //        else
            //        {
            //            parms.VisitId = RequestHelper.MyGlobalVar.VisitId;
            //            parms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
            //        }
            //            //RequestHelper.MyMedicalSummaryGlobalVar.FacilityId;
            //        parms.Option = 1;
            //        parms.Allergy = PatShare.Allergy;
            //        parms.FamilyHist = PatShare.FamilyHistory;
            //        parms.SocialHist = PatShare.SocialHistory;
            //        parms.MedicalHist = PatShare.MedicalHistory;
            //        parms.Problem = PatShare.Problem;
            //        parms.Procedure = PatShare.Procedure;
            //        parms.PlanOfCare = PatShare.PlanOfCare;
            //        parms.VitalSign = PatShare.VitalSigns;
            //        parms.Medication = PatShare.Medication;
            //        parms.Lab = PatShare.LabResults;
            //        parms.Immunization = PatShare.Immunization;
            //        parms.ClinicalDocs = PatShare.ClinicalDoc;
            //        parms.Insurance = PatShare.Insurance;
            //        parms.Visit = PatShare.Visit;

            //        PatientDocument = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //        if (PatientDocument.Valid)
            //        {
            //            if (parms.SocialHist == true)
            //            {
            //                ClinicalSummaryLists.SocialHistories = PatientDocument.dtSocialHist.ToSocialHistoryModelList();
            //            }
            //            if (parms.VitalSign == true)
            //            {
            //                ClinicalSummaryLists.VitalSigns = PatientDocument.dtVitalSign.ToVitalsignModelList();
            //            }
            //            if (parms.MedicalHist == true)
            //            {
            //                ClinicalSummaryLists.MedicalHistories = PatientDocument.dtMedicalHist.ToMedicalHistoryModelList();
            //            }
            //            if (parms.Medication == true)
            //            {
            //                ClinicalSummaryLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
            //            }
            //            if (parms.Problem == true)
            //            {
            //                ClinicalSummaryLists.Problems = PatientDocument.dtProblem.ToProblemModelList();
            //            }
            //            if (parms.Lab == true)
            //            {
            //                ClinicalSummaryLists.LabResults = PatientDocument.dtLab.ToLabResultModelList();
            //                ClinicalSummaryLists.LabResultTest = PatientDocument.dtLabResult.ToLabResultTestModelList();
            //            }
            //            if (parms.Immunization)
            //            {
            //                ClinicalSummaryLists.Immunizations = PatientDocument.dtImmunization.ToImmunizationModelList();
            //            }
            //            if (parms.Allergy == true)
            //            {
            //                ClinicalSummaryLists.Allergies = PatientDocument.dtAllergy.ToAllergyModelList();
            //            }
            //            if (parms.Procedure == true)
            //            {
            //                ClinicalSummaryLists.Procedures = PatientDocument.dtProcedure.ToProcedureModelList();
            //            }
            //            if (parms.PlanOfCare == true)
            //            {
            //                ClinicalSummaryLists.Pocs = PatientDocument.dtPlanOfCare.ToPOCModelList();
            //            }
            //            if (parms.ClinicalDocs == true)
            //            {
            //                ClinicalSummaryLists.Documents = PatientDocument.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
            //            }
            //            if (parms.Insurance == true)
            //            {
            //                ClinicalSummaryLists.Insurance = PatientDocument.dtInsurance.ToInsuranceModelList();
            //            }
            //        }
            //        // Patients Related data which includes Patient demographics, visits and facilities
            //        using (var service1 = new DataPatientService.PatientWSSoapClient())
            //        {
            //            DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
            //            DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
            //            PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            //            VisitData = service1.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //            if (VisitData.Valid)
            //            {
            //                if (parms.Visit == true)
            //                {
            //                    ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
            //                }
            //            }

            //        }
            //        DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            //        DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            //        PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            //        using (var Summservice = new DataPatientService.PatientWSSoapClient())
            //        {

            //            PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //            if (PatientSummary.Valid)
            //            {
            //                ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

            //            }
            //        }
            //        DataPatientDocumentService.PatientVisitCCD Visits = new DataPatientDocumentService.PatientVisitCCD();
            //        DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            //        if (smodel != null)
            //        {
            //            VisitParams.VisitId = Convert.ToInt64(smodel.visitSelected);
            //            VisitParams.FacilityId = Convert.ToInt64(smodel.facilitySelected);
            //            // ClinicalSummaryLists.FacilityVisitSelect = smodel;
            //        }
            //        else
            //        {
            //            VisitParams.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
            //            VisitParams.VisitId = RequestHelper.MyGlobalVar.VisitId;
            //        }
            //            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;

            //        Visits = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //        if (Visits.Valid)
            //        {
            //            ClinicalSummaryLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(Visits);
            //        }


            //}



            //HomeViewModel HomeLists = new HomeViewModel();
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = Convert.ToInt64(smodel.facilitySelected);
            VisitParams.VisitId = Convert.ToInt64(smodel.visitSelected);
            VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                VisitData = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (VisitData.Valid)
                        {
                    HomeLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(VisitData);
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
                            HomeLists.MedicationsClincial = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                            HomeLists.MedicationsClincial.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
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
                    using (var Summservice = new DataPatientService.PatientWSSoapClient())
                    {

                        PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        if (PatientSummary.Valid)
                        {

                    HomeLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

                        }
                    }
           // FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {

                // consolidated call 
                //DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
                //DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                DataPatientDocumentService.PatientCCDTableData PatientCCDCustomize = new DataPatientDocumentService.PatientCCDTableData();
                DataPatientDocumentService.PatientCCDParms parms = new DataPatientDocumentService.PatientCCDParms();
                parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                parms.ClinicalSummary = true;
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

        [HttpPost]
        public ActionResult ClinicalSummaryConsolidate(ConsolidateCallModel Model) 
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
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            HomeViewModel HomeLists = new HomeViewModel();
            // Patients Document Related data which includes labs, allergies and all histories
            //using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            //{

            //    DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

            //    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
            //    DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
            //    DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            //    using (var service1 = new DataPatientService.PatientWSSoapClient())
            //    {
            //        PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //        PatShare = service1.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //        ClinicalSummaryLists.PatientShare = base.ConvertToPatientShareModel(PatShare);

            //        //  HomeLists1.PatientShare = base.ConvertToPatientShareModel(PatShare);
            //    }
            //    parms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            //    parms.VisitId = Model.visitId;
            //    parms.FacilityId = Model.facilityId;
            //    //RequestHelper.MyMedicalSummaryGlobalVar.FacilityId;
            //    parms.Option = 1;
            //    parms.Allergy = PatShare.Allergy;
            //    parms.FamilyHist = PatShare.FamilyHistory;
            //    parms.SocialHist = PatShare.SocialHistory;
            //    parms.MedicalHist = PatShare.MedicalHistory;
            //    parms.Problem = PatShare.Problem;
            //    parms.Procedure = PatShare.Procedure;
            //    parms.PlanOfCare = PatShare.PlanOfCare;
            //    parms.VitalSign = PatShare.VitalSigns;
            //    parms.Medication = PatShare.Medication;
            //    parms.Lab = PatShare.LabResults;
            //    parms.Immunization = PatShare.Immunization;
            //    parms.ClinicalDocs = PatShare.ClinicalDoc;
            //    parms.Insurance = PatShare.Insurance;
            //    parms.Visit = PatShare.Visit;

            //    PatientDocument = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //    if (PatientDocument.Valid)
            //    {
            //        if (parms.SocialHist == true)
            //        {
            //            ClinicalSummaryLists.SocialHistories = PatientDocument.dtSocialHist.ToSocialHistoryModelList();
            //        }
            //        if (parms.VitalSign == true)
            //        {
            //            ClinicalSummaryLists.VitalSigns = PatientDocument.dtVitalSign.ToVitalsignModelList();
            //        }
            //        if (parms.MedicalHist == true)
            //        {
            //            ClinicalSummaryLists.MedicalHistories = PatientDocument.dtMedicalHist.ToMedicalHistoryModelList();
            //        }
            //        if (parms.Medication == true)
            //        {
            //            ClinicalSummaryLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
            //        }
            //        if (parms.Problem == true)
            //        {
            //            ClinicalSummaryLists.Problems = PatientDocument.dtProblem.ToProblemModelList();
            //        }
            //        if (parms.Lab == true)
            //        {
            //            ClinicalSummaryLists.LabResults = PatientDocument.dtLab.ToLabResultModelList();
            //            ClinicalSummaryLists.LabResultTest = PatientDocument.dtLabResult.ToLabResultTestModelList();
            //        }
            //        if (parms.Immunization)
            //        {
            //            ClinicalSummaryLists.Immunizations = PatientDocument.dtImmunization.ToImmunizationModelList();
            //        }
            //        if (parms.Allergy == true)
            //        {
            //            ClinicalSummaryLists.Allergies = PatientDocument.dtAllergy.ToAllergyModelList();
            //        }
            //        if (parms.Procedure == true)
            //        {
            //            ClinicalSummaryLists.Procedures = PatientDocument.dtProcedure.ToProcedureModelList();
            //        }
            //        if (parms.PlanOfCare == true)
            //        {
            //            ClinicalSummaryLists.Pocs = PatientDocument.dtPlanOfCare.ToPOCModelList();
            //        }
            //        if (parms.ClinicalDocs == true)
            //        {
            //            ClinicalSummaryLists.Documents = PatientDocument.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
            //        }
            //        if (parms.Insurance == true)
            //        {
            //            ClinicalSummaryLists.Insurance = PatientDocument.dtInsurance.ToInsuranceModelList();
            //        }
            //    }
            //    // Patients Related data which includes Patient demographics, visits and facilities
            //    using (var service1 = new DataPatientService.PatientWSSoapClient())
            //    {
            //        DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
            //        DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
            //        PatVisitParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //        VisitData = service1.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //        if (VisitData.Valid)
            //        {
            //            if (parms.Visit == true)
            //            {
            //                ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
            //            }
            //        }

            //    }
            //    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            //    DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            //    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            //    using (var Summservice = new DataPatientService.PatientWSSoapClient())
            //    {

            //        PatientSummary = Summservice.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

            //        if (PatientSummary.Valid)
            //        {
            //            ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);

            //        }
            //    }
            //    DataPatientDocumentService.PatientVisitCCD Visits = new DataPatientDocumentService.PatientVisitCCD();
            //    DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            //    VisitParams.FacilityId = Model.facilityId;
            //    VisitParams.VisitId = Model.visitId;
            //    VisitParams.PatientId = RequestHelper.MyGlobalVar.PatientId;

            //    Visits = service.GetPatientVisitCCDData(VisitParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //    if (Visits.Valid)
            //    {
            //        ClinicalSummaryLists.PatientVisitCCD = base.ConvertPatientVisitCCDModel(Visits);
            //    }


            //}


            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
            PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientDocumentService.PatientVisitCCD VisitData = new DataPatientDocumentService.PatientVisitCCD();
            DataPatientDocumentService.PatientSummaryParms VisitParams = new DataPatientDocumentService.PatientSummaryParms();
            VisitParams.FacilityId = Convert.ToInt64(smodel.facilitySelected);
            VisitParams.VisitId = Convert.ToInt64(smodel.visitSelected);
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
            // FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {

                // consolidated call 
                //DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
                //DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                DataPatientDocumentService.PatientCCDTableData PatientCCDCustomize = new DataPatientDocumentService.PatientCCDTableData();
                DataPatientDocumentService.PatientCCDParms parms = new DataPatientDocumentService.PatientCCDParms();
                parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                parms.ClinicalSummary = true;
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
            string html = string.Empty;
            //html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Clinical_Summary_Partial.cshtml", ClinicalSummaryLists, this);
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Summary_new.cshtml", HomeLists, this);
            

            return Json(html);
        }
        //start of social delete action
        [HttpPost]
        public JsonResult SocialDelete(SocialHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientSocialHistData psocial = base.ConvertToPatientSocialHistory(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                psocial.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                psocial.VisitId = 0;
                psocial.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientSocialHistData PatientSocData = service.DeletePatientSocialHistData(psocial, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientSocData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientSocialHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.SocialHistories = PatientDocument.dt.ToSocialHistoryModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            string html1 = string.Empty;
            //html = ClinicalSummaryLists.SocialHistories.GetPatientSocialHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Social.cshtml", ClinicalSummaryLists.SocialHistories, this);
            html1 = ViewHelper.RenderRazorViewToString("_Index_SocialHistory", ClinicalSummaryLists.SocialHistories, this);
          //  return Json(html);
            return Json(new { html = html, html1 = html1 });

        }



        [HttpPost]
        public JsonResult FamilyDelete(FamilyHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientFamilyHistData pFamily = base.ConvertToPatientFamilyHistData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pFamily.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pFamily.VisitId = 0;
                pFamily.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientFamilyHistData PatientFamData = service.DeletePatientFamilyHistData(pFamily, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientFamData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientFamilyHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.FamilyHistories = PatientDocument.dt.ToFamilyHistoryModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            string html1 = string.Empty;
            //html = ClinicalSummaryLists.FamilyHistories.GetPatientFamilyHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Family.cshtml", ClinicalSummaryLists.FamilyHistories, this);
            html1 = ViewHelper.RenderRazorViewToString("_Index_FamilyHistory", ClinicalSummaryLists.FamilyHistories, this);
            return Json(new { html = html, html1 = html1 });

        }


        [HttpPost]
        public JsonResult PastMedicalDelete(MedicalHistoryModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientMedicalHistData pMedicalHist = base.ConvertToPatientMedicalHistoryData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pMedicalHist.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pMedicalHist.VisitId = 0;
                pMedicalHist.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientMedicalHistData PatientMedData = service.DeletePatientMedicalHistData(pMedicalHist, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientMedData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientMedicalHistData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.MedicalHistories = PatientDocument.dt.ToMedicalHistoryModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            string html1 = string.Empty;
            //html = ClinicalSummaryLists.MedicalHistories.GetPatientMedicalHistoryModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_History_Medical.cshtml", ClinicalSummaryLists.MedicalHistories, this);
            html1 = ViewHelper.RenderRazorViewToString("_Index_MedicalHistory", ClinicalSummaryLists.MedicalHistories, this);
            return Json(new { html = html, html1 = html1 });

        }


        [HttpPost]
        public JsonResult AllergyDelete(AllergyModel Model)
        {
          
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientAllergyData pAllergy = base.ConvertToPatientAllergyDataData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pAllergy.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pAllergy.VisitId = 0;
                pAllergy.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientAllergyData PatientFamData = service.DeletePatientAllergyData(pAllergy, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientFamData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientAllergyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.Allergies = PatientDocument.dt.ToAllergyModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            string html1 = string.Empty;
            //html = ClinicalSummaryLists.Allergies.GetPatienAllergyModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Allergies.cshtml", ClinicalSummaryLists.Allergies, this);
            html1 = ViewHelper.RenderRazorViewToString("_Index_Allergies", ClinicalSummaryLists.Allergies, this);
            return Json(new { html = html, html1 = html1 });
        }



        [HttpPost]
        public JsonResult ImmunizationDelete(ImmunizationModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientImmunizationData pImmunization = base.ConvertToPatietntImmunizationData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pImmunization.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pImmunization.VisitId = 0;
                pImmunization.FacilityId = 0;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientImmunizationData PatientFamData = service.DeletePatientImmunizationData(pImmunization, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientFamData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientImmunizationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.Immunizations = PatientDocument.dt.ToImmunizationModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            string html1 = string.Empty;
            //html = ClinicalSummaryLists.Immunizations.GetPatientImmunizationModelListHTMLForTab();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Immunization.cshtml", ClinicalSummaryLists.Immunizations, this);
            html1 = ViewHelper.RenderRazorViewToString("_Index_Immunization", ClinicalSummaryLists.Immunizations, this);
            return Json(new { html = html, html1 = html1 });

        }


        [HttpPost]
        public JsonResult ProblemDelete(ProblemModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientProblemData pProblem = base.ConvertToPatientProblemDataData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pProblem.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pProblem.VisitId = 0;
                pProblem.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientProblemData PatientFamData = service.DeletePatientProblemData(pProblem, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientFamData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientProblemData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.Problems = PatientDocument.dt.ToProblemModelList();

                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            string[] html = new string[2];
            html[0] = ViewHelper.RenderRazorViewToString("_Index_Problems", ClinicalSummaryLists.Problems, this); 
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Problems.cshtml", ClinicalSummaryLists.Problems, this); 
           // string html = string.Empty;
           // html = ClinicalSummaryLists.Problems.GetPatientProblemModelListHTMLForTab();
            return Json(html);

        }

        [HttpPost]
        public JsonResult LabResultDetails(LabResultModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryList = new ClinicalSummaryViewModel();
            //Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
            //Model.VisitId = 0;
            //Model.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

            DataPatientDocumentService.PatientSummaryParms PatDocParms = new DataPatientDocumentService.PatientSummaryParms();
            DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    PatDocParms.VisitId = Model.VisitId;
                    PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    PatDocParms.Option = 1;
                    PatDocParms.FacilityId = Model.FacilityId;
                    PatDocParms.LabResultCntr = Model.LabResultCntr;
                    PatientDocument = service.GetLabResultTestData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            ClinicalSummaryList.LabResultTest = PatientDocument.dtLabResult.ToLabResultTestModelList();   


                        }
                    }
            
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = "";
                                
            html = ClinicalSummaryList.LabResultTest.GetPatientLabResutltModelListHTMLForDashboard();
             return Json(html);
        }

        [HttpPost]
        public JsonResult VitalDelete(PatientVitalSignModel Model)
        {
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
            try
            {
                DataPatientDocumentService.PatientVitalSignData pVital = base.ConvertToPatientVitalSignData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                pVital.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pVital.VisitId = 0;
                pVital.FacilityId = RequestHelper.MyGlobalVar.FacilityId;

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientVitalSignData PatientFamData = service.DeletePatientVitalSignData(pVital, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientFamData.Valid)
                    {
                        PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        PatDocParms.VisitId = 0;
                        PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                        PatDocParms.Option = 1;

                        PatientDocument = service.GetPatientVitalSignData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            ClinicalSummaryLists.VitalSigns = PatientDocument.dt.ToVitalsignModelList();

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
            html[0] = ViewHelper.RenderRazorViewToString("_Index_VitalSign", ClinicalSummaryLists.VitalSigns, this); ;
            // else if (Model.Flag == "vitalTab")
            html[1] = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Vitals.cshtml", ClinicalSummaryLists.VitalSigns, this);
           // string html = string.Empty;
           // html = ClinicalSummaryLists.VitalSigns.GetPatientVitalSignModelListHTMLForTab();
            return Json(html);

        }



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

               ConfigDocument = service.GetSocialCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ConfigDocument.Valid)
               {
                   
                   ClinicalSummaryLists.Social = ConfigDocument.dt.ToSocialHistModelList();
                   ClinicalSummaryLists.Social.Insert(0, new SocialModel { SNOMED_Social = -1, Value = "--Select--" }); 
               }
               ConfigDocument = service.GetExerciseFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

               if (ConfigDocument.Valid)
               {

                   ClinicalSummaryLists.frequency = ConfigDocument.dt.ToExerciseFrequencyModelList();
                   ClinicalSummaryLists.frequency.Insert(0, new ExerciseFrequencyModel { ExerciseFrequencyId = -1, Value = "--Select--" });
               }
               ConfigDocument = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ConfigDocument.Valid)
               {
                   ClinicalSummaryLists.smokingstatus = ConfigDocument.dt.ToSmokingStatusModelList();
                   ClinicalSummaryLists.smokingstatus.Insert(0, new SmokingStatusModel { SmokingStatusId = -1, Value = "--Select--" });

               }

               ConfigDocument = service.GetActivityLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

               if (ConfigDocument.Valid)
               {
                   ClinicalSummaryLists.ActivityLevel = ConfigDocument.dt.ToActivityLevelModelList();
                   ClinicalSummaryLists.ActivityLevel.Insert(0, new ActivityLevelModel { ActivityLevelId = -1, Value = "--Select--" });

               }

               ConfigDocument = service.GetAlcoholFrequencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ConfigDocument.Valid)
               {
                   ClinicalSummaryLists.AlcoholFrequency = ConfigDocument.dt.ToAlcoholFrequencyModelList();
                   ClinicalSummaryLists.AlcoholFrequency.Insert(0, new AlcoholFrequencyModel { AlcoholFrequencyId = -1, Value = "--Select--" });

               }
               ConfigDocument = service.GetEducationLevelCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ConfigDocument.Valid)
               {
                   ClinicalSummaryLists.Educationlevel = ConfigDocument.dt.ToEducationLevelModelList();
                   ClinicalSummaryLists.Educationlevel.Insert(0, new EducationLevelModel { EducationLevelId = -1, Value = "--Select--" });

               }

           }



           using (var service = new DataProviderService.ProviderWSSoapClient())
           {
               DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ProviderData.Valid)
               {
                   ClinicalSummaryLists.Providers = ProviderData.dt.ToProviderModelList();
               }
               DataProviderService.ProviderTableData ProviderList = new DataProviderService.ProviderTableData();
            //   RequestHelper.MyGlobalVar.FacilityId
               ProviderList = service.GetPatientProviderInfoList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), 4, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               ClinicalSummaryLists.ProivderInfo = ProviderList.dt.ToProivderInfoList();
           }
           // Patients Related data which includes Patient demographics, visits and facilities
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                HomeViewModel HomeLists = new HomeViewModel();
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientData Patient = new DataPatientService.PatientData();
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientVisitData PatientVisit = new DataPatientService.PatientVisitData();
                DataPatientService.PatientOrganData PatientOrgan = new DataPatientService.PatientOrganData();

                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                DataPatientService.PatientRepData PatRepData = new DataPatientService.PatientRepData();

                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                
                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    ClinicalSummaryLists.Facilities = FacilityData.dt.ToFacilityModelList();
                 }
                PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatVisitParms.FacilityId = Convert.ToInt64(RequestHelper.MyGlobalVar.FacilityId);
                PatVisitParms.Option = 1;
                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (VisitData.Valid)
                {
                    ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
                }

                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                  //  TicketHelper.SetPatientInformationAndGet(ClinicalSummaryLists.PatientSummary.PatientId.ToString() + "," + ClinicalSummaryLists.PatientSummary.FirstName + " " + ClinicalSummaryLists.PatientSummary.LastName);  
                }

                PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                PatientVisit = service.GetPatientVisitData(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientVisit.Valid)
                {
                    ClinicalSummaryLists.PatientVisit = base.ConvertToPatientVisitDataModel(PatientVisit);
                }

                PatientOrgan = service.GetPatientOrganData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientOrgan.Valid)
                {
                    ClinicalSummaryLists.PatientOrgan = base.ConvertToPatientOrganModel(PatientOrgan);
                   // ClinicalSummaryLists.PatientOrgan.
                }

                if (RequestHelper.MyGlobalVar.UserLoginEx.StartsWith("R"))
                {
                    PatParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatParms.Option = 1;
                    PatRepData = service.GetPatientRepData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatRepData.Valid)
                    {
                        ClinicalSummaryLists.PatientRepresentatives = base.ConvertToPatientRepDataModel(PatRepData);
                    }
                }
                else
                {
                    ClinicalSummaryLists.PatientRepresentatives.Demographics = true;
                    ClinicalSummaryLists.PatientRepresentatives.Allergy = true;
                    ClinicalSummaryLists.PatientRepresentatives.FamilyHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.LabResults = true;
                    ClinicalSummaryLists.PatientRepresentatives.MedicalHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.Medication = true;
                    ClinicalSummaryLists.PatientRepresentatives.Problem = true;
                    ClinicalSummaryLists.PatientRepresentatives.EmergencyContact = true;
                    ClinicalSummaryLists.PatientRepresentatives.SocialHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.SurgicalHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.VitalSigns = true;
                    ClinicalSummaryLists.PatientRepresentatives.Immunization = true;
                    ClinicalSummaryLists.PatientRepresentatives.Organ = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalDoc = true;
                    ClinicalSummaryLists.PatientRepresentatives.Insurance = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalSummary = true;
                    ClinicalSummaryLists.PatientRepresentatives.Appointment = true;
                    ClinicalSummaryLists.PatientRepresentatives.Visit = true;
                    ClinicalSummaryLists.PatientRepresentatives.UploadDocs = true;
                    ClinicalSummaryLists.PatientRepresentatives.PlanOfCare = true;
                    ClinicalSummaryLists.PatientRepresentatives.Messaging = true;
                    ClinicalSummaryLists.PatientRepresentatives.DownloadTransmit = true;
                    ClinicalSummaryLists.PatientRepresentatives.Procedure = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalInstructions = true;
                    ClinicalSummaryLists.PatientRepresentatives.Enabled = true;
                                }


            }

            //get Patientsocialhistself
            //using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            //{
            //    DataPatientDocumentService.PatientDocParms patdata = new DataPatientDocumentService.PatientDocParms();
            //    DataPatientDocumentService.PatientSocialSelfData socialselfdata = new DataPatientDocumentService.PatientSocialSelfData();
            //    DataPatientDocumentService.PatientDocTableData patdoctable = new DataPatientDocumentService.PatientDocTableData();
            //    patdata.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //    socialselfdata = service.GetPatientSocialSelfData(patdata, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            //    if (socialselfdata.Valid)
            //    {
            //        ClinicalSummaryLists.PatientSocialSelf = base.ConvertToPatientSocialSelfDataModel(socialselfdata);
            //    }


            //}
             // Patients Document Related data which includes labs, allergies and all histories
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();


                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatDocParms.VisitId = 0;
                PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                PatDocParms.Option = 1;


                // consolidated call 
                DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();

                DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();

                parms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                parms.VisitId = 0;
                parms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                parms.Option = 1;
                parms.Active = 1;
                parms.FamilyHist = true;
                parms.SocialHist = true;
                parms.MedicalHist = true;
                parms.Problem = true;
                parms.Allergy = true;
                parms.VitalSign = true;
                parms.Medication = true;
                parms.Lab = true;
                parms.Immunization = true;
                parms.PlanOfCare = true;
                parms.Insurance = true;
                parms.ClinicalDocs = true;
                parms.Procedure = true;
                
                if (smodel != null)
                {
                    parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                    parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                    ClinicalSummaryLists.FacilityVisitSelect = smodel;
                }
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientDocumentSummary.Valid)
                {
                    ClinicalSummaryLists.SocialHistories = PatientDocumentSummary.dtSocialHist.ToSocialHistoryModelList();
                    
                    ClinicalSummaryLists.VitalSigns = PatientDocumentSummary.dtVitalSign.ToVitalsignModelList();
                    ClinicalSummaryLists.MedicalHistories = PatientDocumentSummary.dtMedicalHist.ToMedicalHistoryModelList();
                    ClinicalSummaryLists.Medications = PatientDocumentSummary.dtMedication.ToMedicationHistoryModelList();
                    ClinicalSummaryLists.Problems = PatientDocumentSummary.dtProblem.ToProblemModelList();
                    ClinicalSummaryLists.LabResults = PatientDocumentSummary.dtLab.ToLabResultModelList();
                    ClinicalSummaryLists.LabResultTest = PatientDocumentSummary.dtLabResult.ToLabResultTestModelList();
                     ClinicalSummaryLists.Immunizations = PatientDocumentSummary.dtImmunization.ToImmunizationModelList();
                    ClinicalSummaryLists.FamilyHistories = PatientDocumentSummary.dtFamilyHist.ToFamilyHistoryModelList();
                    ClinicalSummaryLists.Allergies = PatientDocumentSummary.dtAllergy.ToAllergyModelList();
                    ClinicalSummaryLists.Pocs = PatientDocumentSummary.dtPlanOfCare.ToPOCModelList();
                    ClinicalSummaryLists.Documents = PatientDocumentSummary.dtClinicalDocs.ToConsolidatedCallDocumentModelList();
                   // ClinicalSummaryLists.Policies = PatientDocumentSummary.dtInsurance.ToPolicyModelList();
                    ClinicalSummaryLists.Insurance = PatientDocumentSummary.dtInsurance.ToInsuranceModelList();
                    ClinicalSummaryLists.Procedures = PatientDocumentSummary.dtProcedure.ToProcedureModelList();
                 //   ClinicalSummaryLists.ClinicalInstructions = PatientDocumentSummary.dtPlanOfCare.ToPOCModelList();

                }
                
                PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientDocument.Valid)
                {
                    ClinicalSummaryLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();
                    ClinicalSummaryLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });
                }
                //DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
               // DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
               
                PatDocParms.Option = 1;
                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
               
                    using (var service1 = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                        PatientDocument = service1.GetPatientClinicalInstructionsData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {


                            ClinicalSummaryLists.ClinicalInstructions = PatientDocument.dt.ToPOCModelList();

                        }
                    }
                
                
             
            }


            //Configure Dropdowns Data

            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;

                ConfigDocument = service.GetVaccineCodes("01000000212000041111", 2, 4, "");
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Vaccines = ConfigDocument.dt.ToVaccineModelList();
                }


            }

            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                // DataPatientConfigService.FacilityData ConfigDocument1 = new DataPatientConfigService.FacilityData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;

                ConfigDocument = service.GetGenderCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Gender = ConfigDocument.dt.ToGenderModelList();
                }

                ConfigDocument = service.GetPreferredLanguageCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.PreferredLanguage = ConfigDocument.dt.ToPreferredLanguageModelList();

                }

                ConfigDocument = service.GetEthnicityCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Ethnicity = ConfigDocument.dt.ToEthnicityModelList();
                }

                ConfigDocument = service.GetRaceCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Race = ConfigDocument.dt.ToRaceModelList();
                }

                ConfigDocument = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.SmokingStatus = ConfigDocument.dt.ToSmokingStatusModelList();
                }

                ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Country = ConfigDocument.dt.ToCountryModelList();
                }

                ConfigDocument = service.GetConditionStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.ConditionStatus = ConfigDocument.dt.ToConditionStatusModelList();
                    ClinicalSummaryLists.ConditionStatus.Insert(0, new ConditionStatusModel { ConditionStatusId = -1, Value = "--Select--" }); 
                }
                ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                    {
                        ClinicalSummaryLists.States = ConfigDocument.dt.ToStatesModelList();
                    }

                ConfigDocument = service.GetBloodTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        ClinicalSummaryLists.BloodType = ConfigDocument.dt.ToBloodTypeModelList();
                    }

                ConfigDocument = service.GetReligionCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ConfigDocument.Valid)
                    {
                        ClinicalSummaryLists.Religion = ConfigDocument.dt.ToReligionModelList();
                    }
                    ConfigDocument = service.GetInstructionTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                {
                 ClinicalSummaryLists.InstructionType=ConfigDocument.dt.ToInstructionTypeModelList();
                }

            }
            DataPatientService.PatientShareData PatShare = new DataPatientService.PatientShareData();
            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                PatParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatShare = service.GetPatientShareData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                ClinicalSummaryLists.PatientShare = base.ConvertToPatientShareModel(PatShare);
            }
          
            return View(ClinicalSummaryLists);
        }

        [Authorize]
        public ActionResult PatientInfoIndex()
        {
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            smodel = (FacilityVisitSelectModel)Session["OptionSelected"];
            ClinicalSummaryViewModel ClinicalSummaryLists = new ClinicalSummaryViewModel();
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

            // Patients Related data which includes Patient demographics, visits and facilities
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                HomeViewModel HomeLists = new HomeViewModel();
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientVisitTableData VisitData = new DataPatientService.PatientVisitTableData();
                DataPatientService.PatientData Patient = new DataPatientService.PatientData();
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientVisitData PatientVisit = new DataPatientService.PatientVisitData();
                DataPatientService.PatientOrganData PatientOrgan = new DataPatientService.PatientOrganData();

                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                DataPatientService.PatientVisitParms PatVisitParms = new DataPatientService.PatientVisitParms();
                DataPatientService.PatientRepData PatRepData = new DataPatientService.PatientRepData();

                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    ClinicalSummaryLists.Facilities = FacilityData.dt.ToFacilityModelList();
                    ClinicalSummaryLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    ClinicalSummaryLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }
                PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatVisitParms.FacilityId = Convert.ToInt64(RequestHelper.MyGlobalVar.FacilityId);
                PatVisitParms.Option = 1;
                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (VisitData.Valid)
                {
                    ClinicalSummaryLists.Visits = VisitData.dt.ToVisitModelList();
                }

                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    ClinicalSummaryLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                    //  TicketHelper.SetPatientInformationAndGet(ClinicalSummaryLists.PatientSummary.PatientId.ToString() + "," + ClinicalSummaryLists.PatientSummary.FirstName + " " + ClinicalSummaryLists.PatientSummary.LastName);  
                }

                PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                PatientOrgan = service.GetPatientOrganData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientOrgan.Valid)
                {
                    ClinicalSummaryLists.PatientOrgan = base.ConvertToPatientOrganModel(PatientOrgan);
                    // ClinicalSummaryLists.PatientOrgan.
                }

                if (RequestHelper.MyGlobalVar.UserLoginEx.StartsWith("R"))
                {
                    PatParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PatParms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    PatParms.Option = 1;
                    PatRepData = service.GetPatientRepData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatRepData.Valid)
                    {
                        ClinicalSummaryLists.PatientRepresentatives = base.ConvertToPatientRepDataModel(PatRepData);
                    }
                }
                else
                {
                    ClinicalSummaryLists.PatientRepresentatives.Demographics = true;
                    ClinicalSummaryLists.PatientRepresentatives.Allergy = true;
                    ClinicalSummaryLists.PatientRepresentatives.FamilyHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.LabResults = true;
                    ClinicalSummaryLists.PatientRepresentatives.MedicalHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.Medication = true;
                    ClinicalSummaryLists.PatientRepresentatives.Problem = true;
                    ClinicalSummaryLists.PatientRepresentatives.EmergencyContact = true;
                    ClinicalSummaryLists.PatientRepresentatives.SocialHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.SurgicalHistory = true;
                    ClinicalSummaryLists.PatientRepresentatives.VitalSigns = true;
                    ClinicalSummaryLists.PatientRepresentatives.Immunization = true;
                    ClinicalSummaryLists.PatientRepresentatives.Organ = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalDoc = true;
                    ClinicalSummaryLists.PatientRepresentatives.Insurance = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalSummary = true;
                    ClinicalSummaryLists.PatientRepresentatives.Appointment = true;
                    ClinicalSummaryLists.PatientRepresentatives.Visit = true;
                    ClinicalSummaryLists.PatientRepresentatives.UploadDocs = true;
                    ClinicalSummaryLists.PatientRepresentatives.PlanOfCare = true;
                    ClinicalSummaryLists.PatientRepresentatives.Messaging = true;
                    ClinicalSummaryLists.PatientRepresentatives.DownloadTransmit = true;
                    ClinicalSummaryLists.PatientRepresentatives.Procedure = true;
                    ClinicalSummaryLists.PatientRepresentatives.ClinicalInstructions = true;
                    ClinicalSummaryLists.PatientRepresentatives.Enabled = true;
                }


            }


            // Patients Document Related data which includes labs, allergies and all histories

            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    ClinicalSummaryLists.Providers = ProviderData.dt.ToProviderModelList();
                    ClinicalSummaryLists.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

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
                    ClinicalSummaryLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                    ClinicalSummaryLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                }
                //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (MessageUrgencyType.Valid)
                //{
                //    HomeLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                //    HomeLists.MessageUrgencyType.Insert(0, new MessageUrgency { MessageUrgencyId = -1, Value = "--Select--" });
                //}

            }
            DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();
        //    DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
         //   DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();

        
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();
                parms.Problem = true;
                if (smodel != null)
                {
                    parms.VisitId = Convert.ToInt64(smodel.visitSelected);
                    parms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                    ClinicalSummaryLists.FacilityVisitSelect = smodel;
                }
                else
                {
                    parms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                    parms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                }
                parms.Option = 1;
                parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocumentSummary.Valid)
                {
                    ClinicalSummaryLists.Problems = PatientDocumentSummary.dtProblem.ToProblemModelList();
                }
                DataPatientDocumentService.PatientSummaryTableData PatientDocument = new DataPatientDocumentService.PatientSummaryTableData();

                DataPatientDocumentService.PatientSummaryParms Patientparms = new DataPatientDocumentService.PatientSummaryParms();

                Patientparms.PatientId = RequestHelper.MyGlobalVar.PatientId; //RequestHelper.MyGlobalVar.PatientId;
                Patientparms.VisitId = 0;
                Patientparms.FacilityId = 0;
                Patientparms.Option = 1;
                Patientparms.Active = 1;
       
                Patientparms.Medication = true;
              
                PatientDocument = service.GetPatientSummaryData(Patientparms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientDocument.Valid)
                {
                   ClinicalSummaryLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                    ClinicalSummaryLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
                    }
            }
            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {

                DataPatientConfigService.CodeTableData ConfigDocument = new DataPatientConfigService.CodeTableData();
                // DataPatientConfigService.FacilityData ConfigDocument1 = new DataPatientConfigService.FacilityData();
                DataPatientConfigService.FacilityParms ConDocParms = new DataPatientConfigService.FacilityParms();
                ConDocParms.FacilityId = 0;
                ConDocParms.Option = 1;

                ConfigDocument = service.GetGenderCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Gender = ConfigDocument.dt.ToGenderModelList();
                }

                ConfigDocument = service.GetPreferredLanguageCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.PreferredLanguage = ConfigDocument.dt.ToPreferredLanguageModelList();

                }

                ConfigDocument = service.GetEthnicityCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Ethnicity = ConfigDocument.dt.ToEthnicityModelList();
                }

                ConfigDocument = service.GetRaceCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Race = ConfigDocument.dt.ToRaceModelList();
                }

                ConfigDocument = service.GetSmokingStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.SmokingStatus = ConfigDocument.dt.ToSmokingStatusModelList();
                }

                ConfigDocument = service.GetCountryCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.Country = ConfigDocument.dt.ToCountryModelList();
                    ClinicalSummaryLists.Country.Insert(0, new CountryModel { CountryId = "-1", Name = "" });
                }
                ConfigDocument = service.GetStateCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ConfigDocument.Valid)
                {
                    ClinicalSummaryLists.States = ConfigDocument.dt.ToStatesModelList();
                    ClinicalSummaryLists.States.Insert(0, new StatesModel { StateId = "-1", Name = "" });
                }

            ConfigDocument = service.GetBloodTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            if (ConfigDocument.Valid)
            {
                ClinicalSummaryLists.BloodType = ConfigDocument.dt.ToBloodTypeModelList();
            }

            ConfigDocument = service.GetReligionCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            if (ConfigDocument.Valid)
            {
                ClinicalSummaryLists.Religion = ConfigDocument.dt.ToReligionModelList();
            }

            }

            return View(ClinicalSummaryLists);

           }

        [HttpPost]
        public JsonResult SaveProceduresData(ProcedureModel Model)
        {
            ClinicalSummaryViewModel HomeList = new ClinicalSummaryViewModel();
            DataPatientDocumentService.PatientProcedureData proceduredata = base.ConvertToProcedureData(Model);
            DataPatientDocumentService.PatientProcedureData PatDocParms = new DataPatientDocumentService.PatientProcedureData();
            PatDocParms.ServiceDate = proceduredata.ServiceDate;
            PatDocParms.Description = proceduredata.Description;
            PatDocParms.Note = proceduredata.Note;
            PatDocParms.PatProcedureCntr = proceduredata.PatProcedureCntr;
            PatDocParms.VisitId = proceduredata.VisitId;
            PatDocParms.FacilityId = proceduredata.FacilityId;
            //PatDocParms.Option = 1;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                proceduredata = service.SavePatientProcedureData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (proceduredata.Valid)
                {
                    DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                    DataPatientDocumentService.PatientDocParms PatientDoc = new DataPatientDocumentService.PatientDocParms();
                    PatientDoc.FacilityId =proceduredata.FacilityId;
                    PatientDoc.VisitId = proceduredata.VisitId;
                    PatientDoc.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    using (var service1 = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                    {
                        PatientDocument = service1.GetPatientProcedureData(PatientDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {
                            HomeList.Procedures = PatientDocument.dt.ToProcedureModelList();
                        }
                      
                    }
                }
            }
            string html = string.Empty;
            string html1 = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Shared/_Index_Procedures.cshtml", HomeList.Procedures,this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_Procedures.cshtml", HomeList.Procedures, this);

            return Json(new { html=html, html1=html1});

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
            string html1 = string.Empty;
            html = ViewHelper.RenderRazorViewToString("_SocailHistorySelf", Homelist.PatientSocialSelf, this);
            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/ClinicalSummary/_Tab_SocialHistorySelf.cshtml", Homelist.PatientSocialSelf, this);

            return Json(new { html = html,html1=html1 });
        }


        [Authorize]
        public ActionResult Documents()
        {
            //PremiumMedicalPortfolioViewModel PremiumMedicalPortfolioLists = null;
            //PremiumMedicalPortfolioLists = new PremiumMedicalPortfolioViewModel();
            ClinicalSummaryViewModel ClincialSummary = new ClinicalSummaryViewModel();
            DataPatientDocumentService.PatientDocTableData doctable = new DataPatientDocumentService.PatientDocTableData();
            DataPatientDocumentService.PatientClinicalDocumentParms para = new DataPatientDocumentService.PatientClinicalDocumentParms();

            try
            {
                //Patient Document Service Instance...
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (FacilityData.Valid)
                    {
                        ClincialSummary.Facilities = FacilityData.dt.ToFacilityModelList();
                        ClincialSummary.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                        ClincialSummary.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                    }
                }
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {

                    //Patient Document Param Instance...
                    var paramPatDoc = new DataPatientDocumentService.PatientDocParms
                    {
                        PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),
                        Option = 0
                    };

                    para.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    para.Option = 0;
                    para.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    doctable = service.GetPatientClinicalDocumentList(para, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (doctable.Valid)
                    {
                        ClincialSummary.MedicalPortfolioModel = doctable.dt.ToMedicalPortfolioBaseModel();
                    
                    }
                    //Getting patient doctor data...
                    DataPatientDocumentService.MedicalPortfolioData dtPatientDoctor = service.GetMedicalPortfolio(paramPatDoc, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    //Local Collection Conversion Using ModelExtentions Methods from Core.Extensions...
                    //Or from base controller public methods..(Model to Data and Data to Model vise versa)...
                    //if (dtPatientDoctor.Valid)
                    //{
                    //    PremiumMedicalPortfolioLists.PatientVisit = dtPatientDoctor.dtVisit.ToPatientVisitModelList();
                    //    PremiumMedicalPortfolioLists.OutsideDoctor = dtPatientDoctor.dtOutsideDoctor.ToOutsideDoctorModelList();
                    //    PremiumMedicalPortfolioLists.PatientDocument = dtPatientDoctor.dtPatiendDocs.ToPatientDocumentModelList();
                    //}
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
                        ClincialSummary.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                        ClincialSummary.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
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
                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                    ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ProviderData.Valid)
                    {
                        ClincialSummary.Providers = ProviderData.dt.ToProviderModelList();
                        ClincialSummary.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

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
                        ClincialSummary.MessageType = MessageType.dt.ToMessageTypeModelList();
                        ClincialSummary.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                    }
                    //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (MessageUrgencyType.Valid)
                    //{
                    //    HomeLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                    //    HomeLists.MessageUrgencyType.Insert(0, new MessageUrgency { MessageUrgencyId = -1, Value = "--Select--" });
                    //}

                }

            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }

            return View(ClincialSummary);
        }


        [HttpPost]
        public JsonResult ShareHideDocumentData(MedicalPortfolioBaseModel Model)
        {
            string html = string.Empty;
            ClinicalSummaryViewModel ClincialSummary = new ClinicalSummaryViewModel();

            DataPatientDocumentService.PatientClinicalDocumentData docdata = new DataPatientDocumentService.PatientClinicalDocumentData();
            DataPatientDocumentService.PatientClinicalDocumentData docdatareturn = new DataPatientDocumentService.PatientClinicalDocumentData();
            DataPatientDocumentService.PatientClinicalDocumentParms para = new DataPatientDocumentService.PatientClinicalDocumentParms();
            DataPatientDocumentService.PatientDocTableData doctable = new DataPatientDocumentService.PatientDocTableData();
            DataPatientDocumentService.PatientDocumentParms docpara = new DataPatientDocumentService.PatientDocumentParms();
            DataPatientDocumentService.PatientDocumentResp docresp=new DataPatientDocumentService.PatientDocumentResp();
            
            try
            {
                docpara.DocumentCntr = Model.DocumentCntr;
              //  docdata.VisitId = Model.VisitId;
              //  docdata.FacilityId = Model.FacilityId;
                docpara.Share = Model.Viewable;
                docpara.PatientId = RequestHelper.MyGlobalVar.PatientId;
            //    docdata.DocumentDescription = Model.DocumentDescription;
             //   docdata.Notes = Model.Notes;
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    docresp = service.PatientClinicalDocumentShare(docpara, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    para.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    doctable = service.GetPatientClinicalDocumentList(para, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (doctable.Valid)
                    {
                        ClincialSummary.MedicalPortfolioModel = doctable.dt.ToMedicalPortfolioBaseModel();

                    }
                    html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Document/Document_Tab.cshtml", ClincialSummary.MedicalPortfolioModel, this);

                }
            }
            catch //(Exception ex)
            {

                //Redirect to generic error page for saying "Forbidden";
            }
            return Json(html);
           
        }
    }
}
