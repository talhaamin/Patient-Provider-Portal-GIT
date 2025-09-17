using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AMR.Models;
using AMR.Core;
using AMR.Core.Extensions;
using AMR.Core.Utilities;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    public class MedicationController : Base.BaseController
    {

        [HttpPost]
        public JsonResult FilterCurrentMedicationData(PatientMedicationModel  Model)
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
            smodel.facilityOptions = Model.facilityOptions;
            smodel.facilitySelected = Model.facilitySelected;
            smodel.visitOptions = Model.visitOptions;
            smodel.visitSelected = Model.visitSelected;
            Session["OptionSelected"] = smodel;

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            PatDocParms.Active = 1;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        MedicationLists.Medications = PatientDocument.dt.ToMedicationHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Current.cshtml", MedicationLists.Medications, this);
           // html = MedicationLists.Medications.GetPatientMedicationModelListHTMLForDashboard();
            return Json(html);
        }


        [HttpPost]
        public JsonResult FilterPastMedicationData(PatientMedicationModel Model)
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();


            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            PatDocParms.VisitId = Model.VisitId;
            PatDocParms.FacilityId = Model.FacilityId;
            PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            PatDocParms.Active = 0;
            PatDocParms.Option = 1;
            try
            {
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {

                        MedicationLists.Medications = PatientDocument.dt.ToMedicationHistoryModelList();

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            //html = MedicationLists.Medications.GetPatientMedicationModelListHTMLForDashboard();
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Past.cshtml", MedicationLists.Medications, this);
            return Json(html);
        }


       


        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult MedicationPrint(string Status)
        {
            Status = Request.QueryString["status"];
            MedicationViewModel MedicationLists = new MedicationViewModel();
            string visitId = Request.QueryString["visitId"];
            string facilityId = Request.QueryString["facilityId"];
            DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
            DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    MedicationLists.PatientSummary = base.ConvertToPatientSummaryModel(PatientSummary);
                }
            }

            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatDocParms.VisitId = Convert.ToInt64(visitId);
                PatDocParms.FacilityId = Convert.ToInt64(facilityId);
                PatDocParms.Option = 1;
                if (Status != null)
                {
                    if (Status.ToString() == "1")
                    {
                        PatDocParms.Active = 1;
                    }
                    else if (Status.ToString() == "0")
                    {
                        PatDocParms.Active = 0;
                    }
                }
                else
                {
                    PatDocParms.Active = 1;
                }

                PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {
                   
                        MedicationLists.Medications = PatientDocument.dt.ToMedicationHistoryModelList();
                                
                }
                                               
            }

            return View(MedicationLists);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult MedicationIndex()
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();
            FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
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

                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    MedicationLists.Facilities = FacilityData.dt.ToFacilityModelList();
                    MedicationLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    MedicationLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }


                PatVisitParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                VisitData = service.GetPatientVisitList(PatVisitParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (VisitData.Valid)
                {
                    MedicationLists.Visits = VisitData.dt.ToVisitModelList();
                }


            }
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    MedicationLists.Providers = ProviderData.dt.ToProviderModelList();
                    MedicationLists.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

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
                    MedicationLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                    MedicationLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
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
                    MedicationLists.FacilityVisitSelect = smodel;
                }
                else
                {
                    parms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                    parms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                }
                parms.Option = 1;
                parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
             
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
                    MedicationLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                    MedicationLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
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
            // Patients Document Related data which includes labs, allergies and all histories
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
               
                PatDocParms.VisitId = RequestHelper.MyGlobalVar.VisitId;
                PatDocParms.Option = 1;
                PatDocParms.Active = 1;
                PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilitySelectId;
                
                if (smodel != null)
                {
                    PatDocParms.VisitId = Convert.ToInt64(smodel.visitSelected);
                    PatDocParms.FacilityId = Convert.ToInt64(smodel.facilitySelected);
                    MedicationLists.FacilityVisitSelect = smodel;
                }
                PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {
                    MedicationLists.ActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                }

               
                PatDocParms.Active = 0;

                PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {
                    MedicationLists.InActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();

                }

                PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {
                    MedicationLists.Pharmacies  = PatientDocument.dt.ToPatientPhramcyModelList();
                    MedicationLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                }

                //PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                //if (PatientDocument.Valid)
                //{
                //    MedicationLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();
                //    MedicationLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });
                //}


            }

            using (var service = new DataPatientConfigService.ConfigWSSoapClient())
            {
                DataPatientConfigService.CodeTableData PatientConfig = new DataPatientConfigService.CodeTableData();
                PatientConfig = service.GetRouteOfAdministrationCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientConfig.Valid)
                {
                    MedicationLists.RoutesAdministration = PatientConfig.dt.ToRoutesOfAdministrationModelList();
                }
            }

           // base.PopulatePatient();
            return View(MedicationLists);
        }

        [HttpPost]
        public JsonResult MedicationDelete(PatientMedicationModel Model)
        {
             MedicationViewModel MedicationLists = new MedicationViewModel();
             try
             {
                 DataPatientDocumentService.PatientMedicationData pMed = base.ConvertToPatientMedicationData(Model);

                 DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                 DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();


                 if (Model.Status == "1")
                 {
                    Model.Active = true;
                    PatDocParms.Active = 1;
                 }
                 else
                 {
                     Model.Active = false;
                     PatDocParms.Active = 0;
                 }


                 pMed.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                 pMed.VisitId = 0;

                 using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                 {
                     DataPatientDocumentService.PatientMedicationData PatientMedData = service.DeletePatientMedicationData(pMed, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);

                     PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                     PatDocParms.VisitId = 0;
                     PatDocParms.Option = 1;

                     PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                     if (PatientDocument.Valid)
                     {

                         if (Model.Active)
                         {
                             MedicationLists.ActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                         }
                         else
                         {
                             MedicationLists.InActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
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
            if (Model.Active)
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Current.cshtml", MedicationLists.ActiveMedications, this);
                html1 = ViewHelper.RenderRazorViewToString("_Index_Medication", MedicationLists.ActiveMedications, this);
                
                // html = MedicationLists.ActiveMedications.GetPatientCurrentMedicationModelListHTML();
            }
            else
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Past.cshtml", MedicationLists.InActiveMedications, this);
                html1 = ViewHelper.RenderRazorViewToString("_Index_Medication", MedicationLists.Medications, this);
               // html = MedicationLists.InActiveMedications.GetPatientPastMedicationModelListHTML();
            }
           // return Json(html);
            return Json(new { html = html, html1 = html1 });
        }


        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MedicationRefillSave(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            try
            {

                Model.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                Model.MessageTypeId=2; //Refill request
              //  Model.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                Model.MessageStatusId=1; // New Message
                Model.MessageResponseTypeId = 2;// Email
                Model.MessageUrgency = true; //Any Day
                Model.AppointmentStart = Convert.ToDateTime("1900-01-01 00:00:00");
                Model.AppointmentEnd = Convert.ToDateTime("1900-01-01 00:00:00");
                
                                
                DataMessageService.MessageData mData = base.ConvertToMessageData(Model);
                mData.User_Id_Created = 1;
                mData.CreatedByName = RequestHelper.MyGlobalVar.PatientName;
                mData.AttachmentName = "";
                mData.PharmacyAddress = Model.PharmacyAddress;
                using (var service = new DataMessageService.MessageWSSoapClient())
                {

                    mData = service.SaveMessageData(mData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);


                    if (mData.Valid)
                    {

                        if (Model.status == "Medicaton Refill")
                        {
                        DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                        DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                        MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);


                        MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (MessageData.Valid)
                        {
                            MessageLists.Messages = MessageData.dt.ToMessageModelList();
                        }
                       
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.Messages, this);

                        }
                        else
                        {
                            html = "Message sent successfully";
                        }
                    }

                    else
                    {
                        html = mData.ErrorMessage;
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
        public JsonResult MedicationShow(PatientMedicationModel Model)
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();
            string html = string.Empty;

            DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
            DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatDocParms.VisitId = Model.VisitId; 
                PatDocParms.Option = 1;
                PatDocParms.FacilityId = Model.FacilityId; 

                if (Model.Current == false)
                {
                    PatDocParms.Active = 1;
                    PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {
                        MedicationLists.ActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                        //html = MedicationLists.ActiveMedications.GetPatientCurrentMedicationModelListHTML();
                        html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Current.cshtml", MedicationLists.ActiveMedications, this);
                    }
                }
                else
                    if (Model.Current == true)
                    {
                        PatDocParms.Active = 0;
                        PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {
                            MedicationLists.InActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                            //html = MedicationLists.InActiveMedications.GetPatientPastMedicationModelListHTML();
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Past.cshtml", MedicationLists.InActiveMedications, this);

                        }
                    }

            }
            return Json(html);

        }

        [HttpPost] 
        public JsonResult  MedicationSave(PatientMedicationModel Model)
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();
            string html = string.Empty;
            try
            {
               

                DataPatientDocumentService.PatientMedicationData pMed = base.ConvertToPatientMedicationData(Model);
                pMed.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                pMed.VisitId = 0;
                pMed.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                pMed.DuringVisit = Model.duringvisit;

                               
                pMed.DuringVisit = Model.duringvisit;


                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();


                if (Model.Status=="1")
                {
                    Model.Status = "Active";
                    Model.Active = true;
                    pMed.Active = true;
                    PatDocParms.Active = 1;
                }
                else
                {
                    Model.Status = "InActive";
                    pMed.Active = false;
                    Model.Active = false;
                    PatDocParms.Active = 0;
                }
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientMedicationData PatientMedData = service.SavePatientMedicationData(pMed, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);



                    PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    PatDocParms.VisitId = 0;
                    PatDocParms.Option = 1;
                    PatDocParms.FacilityId = 0;

                    if (Convert.ToInt32(Model.PatientMedicationCntr) > 0)
                    {
                        if (Model.Current == true)
                        {
                            PatDocParms.Active = 1;
                            PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (PatientDocument.Valid)
                            {
                                MedicationLists.ActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                               // html = MedicationLists.ActiveMedications.GetPatientCurrentMedicationModelListHTML();
                                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Current.cshtml", MedicationLists.ActiveMedications, this);
                            }
                        }
                        else
                        if (Model.Current == false)
                        {
                            PatDocParms.Active = 0;
                            PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (PatientDocument.Valid)
                            {
                                MedicationLists.InActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                            //    html = MedicationLists.InActiveMedications.GetPatientPastMedicationModelListHTML();
                                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Past.cshtml", MedicationLists.InActiveMedications, this);

                            }
                        }

                    }
                    else
                    {
                        PatientDocument = service.GetPatientMedicationData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (PatientDocument.Valid)
                        {

                            if (Model.Active)
                            {
                                MedicationLists.ActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Current.cshtml", MedicationLists.ActiveMedications, this);
                               // html = MedicationLists.ActiveMedications.GetPatientCurrentMedicationModelListHTML();
                            }
                            else
                            {
                                MedicationLists.InActiveMedications = PatientDocument.dt.ToMedicationHistoryModelList();
                               // html = MedicationLists.InActiveMedications.GetPatientPastMedicationModelListHTML();
                                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Past.cshtml", MedicationLists.InActiveMedications, this);
                            }


                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new {Medhtml = html,Status = Model.Status} );
        }

        [HttpPost]
        public JsonResult MedicationPharmaciesData()
        {
            MedicationViewModel MedicationLists = new MedicationViewModel();
            string html = string.Empty;
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();
                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                PatDocParms.VisitId = 0;
                PatDocParms.Option = 1;
                PatDocParms.Active = 1;
                // PatDocParms.FacilityId = 0;

                PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (PatientDocument.Valid)
                {
                    MedicationLists.Pharmacies = PatientDocument.dt.ToPatientPhramcyModelList();
                    MedicationLists.Pharmacies.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });
                }

            }

            html = MedicationLists.Pharmacies.GetPharmacyDropDownListHTML();

            return Json(html);
        }

        [HttpPost]
        public JsonResult PharmacySave(PatientPharmacyModel Model)
        {
            MedicationViewModel PharmacyLists = new MedicationViewModel();
            HomeViewModel HomeLists = new HomeViewModel();
            try
            {

                DataPatientDocumentService.PatientPharmacyData pPharm = base.ConvertToPatientPharmacyData(Model);
                pPharm.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                //pPharm.VisitId = 0;
                //pPharm.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                if (Model.PharmacyCntr == 0)
                {
                    pPharm.PharmacyCntr = 0;
                }
                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientPharmacyData PatientPharmData = service.SavePatientPharmacyData(pPharm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);



                    PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    PatDocParms.Option = 1;

                    PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {


                        PharmacyLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();

                     HomeLists.PharmciesForRefill = PatientDocument.dt.ToPatientPharmacyModellList();
                    // HomeLists.PharmciesForRefill.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });


                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;
            if (Model.Flag == "1")
            {
                html = ViewHelper.RenderRazorViewToString("_RefillPharmacyDropDown", HomeLists, this);
            }
            else
            {
                html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Pharmacy.cshtml", PharmacyLists.Pharmacies, this);
            }
            //html = PharmacyLists.Pharmacies.GetPatientPharmacyModelListHTML();
           
            return Json(html);
        }

        [HttpPost]
        public JsonResult PharmacyDelete(PatientPharmacyModel Model)
        {
            MedicationViewModel PharmacyLists = new MedicationViewModel();
            try
            {
                DataPatientDocumentService.PatientPharmacyData pPharm = base.ConvertToPatientPharmacyData(Model);

                DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();
                DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();





                pPharm.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
               

                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                    DataPatientDocumentService.PatientPharmacyData PatientPharmData = service.DeletePatientPharmacyData(pPharm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);

                    PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    PatDocParms.VisitId = 0;
                    PatDocParms.Option = 1;

                    PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId ,RequestHelper.MyGlobalVar.FacilityId);
                    if (PatientDocument.Valid)
                    {
                        PharmacyLists.Pharmacies = PatientDocument.dt.ToPatientPharmacyModellList();
                       
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            string html = string.Empty;

           // html = PharmacyLists.Pharmacies.GetPatientPharmacyModelListHTML();

            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Medication/_Medication_Pharmacy.cshtml", PharmacyLists.Pharmacies, this);
           
            return Json(html);

        }
    }
}
