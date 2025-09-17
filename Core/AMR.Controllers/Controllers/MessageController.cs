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
using System.IO;
using AMR.Controllers.CustomActionFilter;

namespace AMR.Controllers.Controllers
{
    public class MessageController : Base.BaseController
    {
        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult AppointmentIndex()
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
          
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();
                DataMessageService.CodeTableData MessageType = new DataMessageService.CodeTableData();

                MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

              //  MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                MessageData = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                   // MessageLists.Messages = MessageData.dt.ToAppointmentMessageModelList();
                    MessageLists.Messages = MessageData.dt.ToMessageModelSendList();
                }
                //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (MessageUrgencyType.Valid)
                //{
                //    MessageLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();

                //}   
                MessageType = service.GetMessageTypeGeneralCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                    MessageLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                    MessageLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                }

            }
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    MessageLists.Facilities = FacilityData.dt.ToFacilityModelList();
                    MessageLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                    MessageLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                }
            }
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    MessageLists.Providers = ProviderData.dt.ToProviderModelList();
                    MessageLists.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });

                }
            }

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
               
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
               
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    MessageLists.Facilities = FacilityData.dt.ToFacilityModelList();
                }
            }
            using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
            {
                
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
                    MessageLists.Medications = PatientDocument.dtMedication.ToMedicationHistoryModelList();
                    MessageLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
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
           // base.PopulatePatient();
            return View(MessageLists);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult ReferralIndex()
        {
            MessageCenterModel MessageLists = new MessageCenterModel();

            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();
                MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);


                MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MessageData.Valid)
                {
                    MessageLists.Messages = MessageData.dt.ToMessageModelList();
                }
                //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //if (MessageUrgencyType.Valid)
                //{
                //    MessageLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                //}
               
            }
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId),RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (ProviderData.Valid)
                {
                    MessageLists.Providers = ProviderData.dt.ToProviderModelList();
                    
                }
            }

            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();

                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();

                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                FacilityData = service.GetPatientFacilityList(PatParms,RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    MessageLists.Facilities = FacilityData.dt.ToFacilityModelList();
                }
            }
           // base.PopulatePatient();
            return View(MessageLists);
        }

        [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult MessageAttachment()
        {
            string MsgId = Request.QueryString["id"];
            MessageCenterModel MessageLists = new MessageCenterModel();
            using (var service = new DataMessageService.MessageWSSoapClient()) 
            {
                DataMessageService.MessageAttachmentData MsgParams = new DataMessageService.MessageAttachmentData();
                MsgParams.MessageAttachmentId = Convert.ToInt64(MsgId);
                DataMessageService.MessageAttachmentData Attach = new DataMessageService.MessageAttachmentData();
                Attach = service.GetMessageAttachmentData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                // if (Attach.Valid)
                // {
                MessageLists.MessageAttachment.DocumentFormat = Attach.DocumentFormat;
                MessageLists.MessageAttachment.DocumentImage = Attach.DocumentImage;
                MessageLists.MessageAttachment.FileDirectory = Attach.FileDirectory;
                if (Attach.DocumentFormat == "pdf")
                {
                    MessageLists.MessageAttachment.FileName = Attach.FileDirectory + "\\" + Attach.MessageAttachmentId + ".pdf";
                }
                if (Attach.DocumentFormat == "jpg")
                {
                    MessageLists.MessageAttachment.FileName = Attach.FileDirectory + "\\" + Attach.MessageAttachmentId + ".jpg";
                }
                MessageLists.MessageAttachment.MessageAttachmentId = Attach.MessageAttachmentId;
                MessageLists.MessageAttachment.MessageId = Attach.MessageId;
                MessageLists.MessageAttachment.PatientId = Attach.PatientId;
                //MessageLists.MessageAttachment.
            }
            if (!System.IO.File.Exists(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + "." + MessageLists.MessageAttachment.DocumentFormat))
            {
                return PartialView("_FileNotFound");
            }
               // image/jpg
            //if (MessageLists.MessageAttachment.DocumentFormat == "pdf")
            //    {
            //        return File(MessageLists.MessageAttachment.FileName, "application/pdf");
            //    }
            //if (MessageLists.MessageAttachment.DocumentFormat == "jpg")
            //    {
            //        return File(MessageLists.MessageAttachment.FileName, "image/jpeg");
            //    }
                    



            switch (MessageLists.MessageAttachment.DocumentFormat.ToLower())
            {
                case "jpg":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".jpg", "image/jpeg");
                    break;

                case "jpeg":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".jpeg", "image/jpeg");
                    break;

                case "png":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".png", "image/png");
                    break;

                case "bmp":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".bmp", "image/bmp");
                    break;

                case "pdf":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".pdf", "application/pdf");
                    break;

                case "doc":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".doc", "application/msword");
                    break;

                case "docx":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".docx", "application/msword");
                    break;

                case "xls":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".xls", "application/vnd.ms-excel");
                    break;

                case "xlsx":
                    return File(MessageLists.MessageAttachment.FileDirectory + "\\" + MessageLists.MessageAttachment.MessageAttachmentId + ".xlsx", "application/vnd.ms-excel");
                    break;


                default:
                    return PartialView("_AttachmentSupport");
                    break;
            }
            
            return View(MessageLists);
        }

        [HttpGet]
        public JsonResult GetMessageCounter()
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
            DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
            MsgParams.PatientId = RequestHelper.MyGlobalVar.PatientId; //Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                Unread = service.GetUnreadMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Unread.Valid)
                {
                    MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                    MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                    MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                    MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                    MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                }
            }
            return Json(MessageLists.UnreadMessages,JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMessageCounterProvider()
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
            DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
            MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                Unread = service.GetUnreadProvMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Unread.Valid)
                {
                    MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                    MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                    MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                    MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                    MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                }
            }
            return Json(MessageLists.UnreadMessages, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult UpdateMessageReadFlag(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
            DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
            
            DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
            MsgData.MessageDetailId = Model.MessageDetailId;
            MsgData.MessageRead = Model.MessageRead;
            using (var service = new DataMessageService.MessageWSSoapClient())
            {
                MsgData = service.UpdateMessageReadFlagData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (MsgData.Valid)
                {
                    MessageLists.UnreadMessages.MessageRead = true;
                }
                else
                {
                    MessageLists.UnreadMessages.MessageRead = false;
                }
                if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                {
                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    Unread = service.GetUnreadMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (Unread.Valid)
                    {
                        MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                        MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                        MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                        MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                        MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                    }
                }
                else
                {
                    MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    Unread = service.GetUnreadProvMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (Unread.Valid)
                    {
                        MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                        MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                        MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                        MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                        MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                    }
                }
            }
            return Json(MessageLists.UnreadMessages);
        }

       [AnotherSessionRedirectionFilter] [Authorize]
        public ActionResult MessageIndex()
        {
            MessageCenterModel MessageLists = new MessageCenterModel();

                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                    DataMessageService.CodeTableData MessageType = new DataMessageService.CodeTableData();
                    DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();

                MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);


                MessageData = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.Messages = MessageData.dt.ToMessageModelSendList();
                    }

                //MessageType = service.GetMessageTypeCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //    if (MessageData.Valid)
                //    {
                //        MessageLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                //    }
                    MessageType = service.GetMessageTypeGeneralCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                        MessageLists.MessageType.Insert(0, new MessageTypeModel { MessageTypeId = -1, Value = "--Select--" });
                    }
                    

                //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                //    if (MessageUrgencyType.Valid)
                //    {
                //        MessageLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                //        MessageLists.MessageUrgencyType.Insert(0, new MessageUrgency { MessageUrgencyId = -1, Value = "--Select--" });
                //    }

                }

                using (var service = new DataProviderService.ProviderWSSoapClient())
                {
                    DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

                ProviderData = service.GetProvidersForPatientList(Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (ProviderData.Valid)
                    {
                        MessageLists.Providers = ProviderData.dt.ToProviderModelList();
                        MessageLists.Providers.Insert(0, new ProviderModel { ProviderId = -1, FullName = "--Select--" });
                    }
                }

                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
                    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (FacilityData.Valid)
                    {
                        MessageLists.Facilities = FacilityData.dt.ToFacilityModelList();
                        MessageLists.MessageFacilities = FacilityData.dt.ToFacilityModelList();
                        MessageLists.MessageFacilities.Insert(0, new FacilityModel { FacilityID = -1, FacilityName = "--Select--" });
                    }
                }

              
                using (var service = new DataPatientDocumentService.PatientDocumentWSSoapClient())
                {
                  //  FacilityVisitSelectModel smodel = new FacilityVisitSelectModel();
                    DataPatientDocumentService.PatientDocTableData PatientDocument = new DataPatientDocumentService.PatientDocTableData();

                    

                   
                    DataPatientDocumentService.PatientSummaryTableData PatientDocumentSummary = new DataPatientDocumentService.PatientSummaryTableData();

                    DataPatientDocumentService.PatientSummaryParms parms = new DataPatientDocumentService.PatientSummaryParms();

                    parms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    parms.VisitId = 0;
                    parms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    parms.Option = 1;
                    parms.Active = 1;
                    parms.Medication = true;
                    PatientDocumentSummary = service.GetPatientSummaryData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientDocumentSummary.Valid)
                    {
                        MessageLists.Medications = PatientDocumentSummary.dtMedication.ToMedicationHistoryModelList();
                        MessageLists.Medications.Insert(0, new PatientMedicationModel { PatientMedicationCntr = "-1", MedicationName = "--Select--" });
                        
                      

                    }

                    DataPatientDocumentService.PatientDocParms PatDocParms = new DataPatientDocumentService.PatientDocParms();

                    PatDocParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    PatDocParms.VisitId = 0;
                    PatDocParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PatDocParms.Option = 1;
                    PatDocParms.Active = 1;

                    PatientDocument = service.GetPatientPharmacyData(PatDocParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (PatientDocument.Valid)
                    {

                        MessageLists.PharmciesForRefill = PatientDocument.dt.ToPatientPharmacyModellList();
                        MessageLists.PharmciesForRefill.Insert(0, new PatientPharmacyModel { PharmacyCntr = -1, PharmacyName = "--Select--" });

                    }



                }
                
         //   base.PopulatePatient();


            return View(MessageLists);
        }


       [AnotherSessionRedirectionFilter] [Authorize]
       public ActionResult MsgIndex()
       {
           MessageCenterModel MessageLists = new MessageCenterModel();
           base.PopulateProvider();
           string usertyp = RequestHelper.MyGlobalVar.PortalType;
          
           using (var service = new DataMessageService.MessageWSSoapClient())
           {
               DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

               DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
               DataMessageService.CodeTableData MessageType = new DataMessageService.CodeTableData();
               DataMessageService.CodeTableData MessageUrgencyType = new DataMessageService.CodeTableData();
               DataMessageService.CodeTableData MessageStatus = new DataMessageService.CodeTableData();
               MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);

               MessageStatus = service.GetMessageStatusCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (MessageStatus.Valid)
               {
                   MessageLists.MessageStatus = MessageStatus.dt.ToMessageStatusModelList();
               }

               MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);
               MessageData = service.GetMessageDetailProvReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (MessageData.Valid)
               {
                   MessageLists.ProviderMessages = MessageData.dt.ToProviderMessageModelList();
               }
               MessageData = service.GetMessageDetailProvSentList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (MessageData.Valid)
               {
                   MessageLists.ProviderSentMessages = MessageData.dt.ToProviderMessageModelList();
               }
               //MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               //if (MessageData.Valid)
               //{
               //    MessageLists.Messages = MessageData.dt.ToMessageModelList();
               //}

               MessageType = service.GetMessageTypeGeneralCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (MessageData.Valid)
               {
                   MessageLists.MessageType = MessageType.dt.ToMessageTypeModelList();
                   
               }
                    
               //MessageUrgencyType = service.GetMessageUrgencyCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               //if (MessageUrgencyType.Valid)
               //{
               //    MessageLists.MessageUrgencyType = MessageUrgencyType.dt.ToMessageUrgencyList();
                  
               //}

           }

           using (var service = new DataProviderService.ProviderWSSoapClient())
           {
               DataProviderService.ProviderTableData ProviderData = new DataProviderService.ProviderTableData();

               ProviderData = service.GetPatientForProviderList(Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ProviderData.Valid)
               {
                   MessageLists.PatientList = ProviderData.dt.ToPatientModelList();
               }
               ProviderData = service.GetFacilityListForProviders(Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin), RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
               if (ProviderData.Valid)
               {
                   MessageLists.Facilities = ProviderData.dt.ToFacilityModelList();
               }
           }
           using (var ConService = new DataPatientConfigService.ConfigWSSoapClient())
           {
               try
               {
                   DataPatientConfigService.CodeTableData CarData = new DataPatientConfigService.CodeTableData();

                  /* CarData = ConService.GetCarriers(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                   if (CarData.Valid)
                   {
                       HomeList.CarrierModel = CarData.dt.ToCarrierModelList();
                   }*/
                   CarData = ConService.GetSecurityQuestionCodes(RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                   if (CarData.Valid)
                   {
                       MessageLists.SecurityQuestion = CarData.dt.ToSecurityQuestionModelList();
                   }
               }
               catch (Exception ex)
               {
                   return Json(ex.Message, JsonRequestBehavior.AllowGet);
               }

           }

           //using (var service = new DataPatientService.PatientWSSoapClient())
           //{
           //    DataPatientService.PatientFacilityTableData FacilityData = new DataPatientService.PatientFacilityTableData();
           //    DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();
           //    PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
           //    FacilityData = service.GetPatientFacilityList(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

           //    if (FacilityData.Valid)
           //    {
           //        MessageLists.Facilities = FacilityData.dt.ToFacilityModelList();
           //    }
           //}


         //  base.PopulatePatient();
          


           return View(MessageLists);
       }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
       public JsonResult MessageCompose(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
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



                Model.Attachment = AttachmentData;
                Model.AttachmentName = myFile.FileName;
            }
            else 
            {
                Model.AttachmentName = "";
            }
            //if (Model.AttachmentTest != null)
            //{
            //    byte[] array = Encoding.Unicode.GetBytes(Model.AttachmentTest);
            //    Model.Attachment = array;
            //}
            //if (Model.AttachmentName == null)
            //{
            //    Model.AttachmentName = "";
            //}
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);

                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        //  MsgData.ProviderId_From = Convert.ToInt64(RequestHelper.MyGlobalVar.UserId); //UserID_Created
                        MsgData.User_Id_Created = Convert.ToInt64(RequestHelper.MyGlobalVar.UserId); //UserID_Created
                        MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientName; //Logged in user name
                    }
                    else if (RequestHelper.MyGlobalVar.PortalType == "Provider")
                    {
                        // MsgData.PatientId = RequestHelper.MyGlobalVar.PatientId;
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;
                        //MsgData.ProviderId_To;
                        MsgData.ProviderId_To = 0;
                        MsgData.User_Id_Created = Convert.ToInt64(RequestHelper.MyGlobalVar.UserId); //UserID_Created
                        // MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientName; //Logged in user name

                    }

                    //Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgData.MessageStatusId = 1; // New Message



                    MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");
                    // MsgData.MessageUrgencyId = 1;//Next Available
                    MsgData.MessageResponseTypeId = 3;//Text
                    //  MsgData.ProviderId_Appointment = 1;// This passing just to prevent the join in GetMessageData web method in message service.
                    MsgData.AttachmentId = "0";
                    MsgData.MessageUrgency = Model.MessageUrgency;
                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                        {
                            MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                            if (Model.Flag == "message-compose")
                            {
                                MessageData = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                                if (MessageData.Valid)
                                {
                                    MessageLists.Messages = MessageData.dt.ToMessageModelSendList();
                                    //html = MessageLists.Messages.GetMessageMasterGridHtml();
                                    html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.Messages, this);
                                }


                            }
                        }
                        else if (RequestHelper.MyGlobalVar.PortalType == "Provider")
                        {
                            //MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                            //if (Model.Flag == "message-compose")
                            //{
                            //    MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            //    if (MessageData.Valid)
                            //    {
                            //        MessageLists.Messages = MessageData.dt.ToMessageModelList();
                            //        html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.Messages, this);
                  
                            //       // html = MessageLists.Messages.GetMessageMasterGridHtml();
                            //    }
                            //}
                            html = "Message sent successfully";
                        }
                        else
                        {
                            html = "Message not sent";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            if (myFile != null)
            {
                return Json(new { Msghtml = html, message = message }, "text/html");
            }
            else 
            {
                return Json(html);
            }
            return Json(html);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageReply(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string MainGridHtml = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);

                    //MsgData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgData.MessageStatusId = 1; // New Message
                   // MsgData.ProviderId_From = RequestHelper; //UserID_Created
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.ProviderId_From = 0;
                    }
                    else
                    {
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;//Patient Id is holding logged in Provider ID
                    }
                    //MsgData.ProviderId_To = RequestHelper.MyGlobalVar.UserId;
                    MsgData.User_Id_Created = RequestHelper.MyGlobalVar.UserId; //UserID_Created
                    MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientId.ToString(); //Logged in user name
                    MsgData.AttachmentId = "0";
                    MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");
                    // MsgData.MessageUrgencyId = 1;//Next Available
                    MsgData.MessageResponseTypeId = 3;//Text
                    //  MsgData.ProviderId_Appointment = 1;// This passing just to prevent the join in GetMessageData web method in message service.

                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        MsgParams.MessageId = Model.MessageId;
                       if (Model.Flag == "message-reply")
                        {
                            MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (MsgData.Valid)
                            {
                                if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                                {
                                    html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMessageDetailGridHtml();
                                }
                                else
                                {
                                    html = "No responses found in this conversation.";
                                }

                            }

                        }
                     
                    }
                    DataMessageService.MessageParms MainMsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MainMessageData = new DataMessageService.MessageTableData();
                    MainMsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MainMessageData = service.GetMessageList(MainMsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MainMessageData.Valid)
                    {
                        List<PatientMessageModel> MainMessages = MainMessageData.dt.ToMessageModelList();
                        MainGridHtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MainMessages, this);

                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new { html = html, MainGridHtml = MainGridHtml });
        }

        [HttpPost]
        public JsonResult MessageReplyProvider(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string MainGridHtml = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);

                    //MsgData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgData.MessageStatusId = 1; // New Message
                    // MsgData.ProviderId_From = RequestHelper; //UserID_Created
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.ProviderId_From = 0;
                    }
                    else
                    {
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;//Patient Id is holding logged in Provider ID
                    }
                    //MsgData.ProviderId_To = RequestHelper.MyGlobalVar.UserId;
                    MsgData.User_Id_Created = RequestHelper.MyGlobalVar.UserId; //UserID_Created
                    MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientId.ToString(); //Logged in user name
                    MsgData.AttachmentId = "0";
                    MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");
                    // MsgData.MessageUrgencyId = 1;//Next Available
                    MsgData.MessageResponseTypeId = 3;//Text
                    //  MsgData.ProviderId_Appointment = 1;// This passing just to prevent the join in GetMessageData web method in message service.

                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        //MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        //MsgParams.MessageId = Model.MessageId;
                        //if (Model.Flag == "message-reply")
                        //{
                        //    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //    if (MsgData.Valid)
                        //    {
                        //        if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        //        {
                        //            html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMessageDetailGridHtml();
                        //        }
                        //        else
                        //        {
                        //            html = "No responses found in this conversation.";
                        //        }

                        //    }

                        //}
                        html = "Message Sent Succuessfully";
                    }
                    else
                    {
                        html = "Message Not Sent";
                    }
                    //DataMessageService.MessageParms MainMsgParams = new DataMessageService.MessageParms();

                    //DataMessageService.MessageTableData MainMessageData = new DataMessageService.MessageTableData();
                    //MainMsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    //MainMessageData = service.GetMessageListProvider(MainMsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (MainMessageData.Valid)
                    //{
                    //    List<PatientMessageModel> MainMessages = MainMessageData.dt.ToMessageModelList();
                    //    MainGridHtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MainMessages, this);
                    
                    //}
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new { html = html, MainGridHtml = MainGridHtml });
        }
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageAttachmentReply(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string html1 = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);

                    //MsgData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    
                    //MsgData.MessageStatusId = 3; // New Message
                   
                    // MsgData.ProviderId_From = RequestHelper; //UserID_Created
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.ProviderId_From = 0;
                    }
                    else
                    {
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;//Patient Id is holding logged in Provider ID
                    }
                    MsgData.AppointmentStart = MsgData.AppointmentStart.Add(TimeSpan.Parse(MsgData.PreferredTime)); //DateTime.ParseExact(MsgData.AppointmentStart.ToShortDateString() + " " + MsgData.PreferredTime, "MM/dd/yyyy HH:mm", System.Globalization.CultureInfo.InstalledUICulture);
                    MsgData.PreferredTime = null;
                    //MsgData.ProviderId_To = RequestHelper.MyGlobalVar.UserId;
                    MsgData.User_Id_Created = RequestHelper.MyGlobalVar.UserId; //UserID_Created
                    MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientId.ToString(); //Logged in user name
                    MsgData.AttachmentId = "0";
                   // MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");
                   
                    MsgData.MessageResponseTypeId = 3;//Text
                    MsgData.MessageUrgency = true;
                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        //  MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        //  MsgParams.MessageId = Model.MessageId;
                        // // if (Model.Flag == "message-reply")
                        ////  {
                        //      MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //      if (MsgData.Valid)
                        //      {
                        //          if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        //          {
                        //              html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetAppointmentDetailGridHtml();
                        //          }
                        //          else
                        //          {
                        //              html = "No responses found in this conversation.";
                        //          }

                        //      }

                        //  }
                        html = "Message Sent Succussfully";
                    }
                    else
                    {
                        html = "Message Not Sent";
                    }
                //    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                 //   DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    //MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    //string MessageType = Model.MessageType;

                    //MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (MessageData.Valid)
                    //{
                    //    MessageLists.Messages = MessageData.dt.ToMessageModelList();

                    //}

                    //if (MessageType == "Appointment Request")
                    //{
                    //    html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", MessageLists.Messages, this);
                    //    // html = HTMLExtensions.GetAppointmentTabsDataHTML(MessageLists.Messages, MessageType);
                    //}
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new { html=html, html1=html1});
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageRequestReferralReply(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string htmlMainGrid = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);

                    //MsgData.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    MsgData.MessageStatusId = 3; // New Message

                    // MsgData.ProviderId_From = RequestHelper; //UserID_Created
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.ProviderId_From = 0;
                    }
                    else
                    {
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;//Patient Id is holding logged in Provider ID
                    }
                    //MsgData.ProviderId_To = RequestHelper.MyGlobalVar.UserId;
                    MsgData.User_Id_Created = RequestHelper.MyGlobalVar.UserId; //UserID_Created
                    MsgData.CreatedByName = RequestHelper.MyGlobalVar.PatientId.ToString(); //Logged in user name
                    MsgData.AttachmentId = "0";
                    MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");

                    MsgData.MessageResponseTypeId = 3;//Text
                    MsgData.MessageUrgency = true;
                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        //MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        //MsgParams.MessageId = Model.MessageId;
                        //// if (Model.Flag == "message-reply")
                        ////  {
                        //MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //if (MsgData.Valid)
                        //{
                        //    if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        //    {
                        //        html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetReferralRequestDetailGridHtml();
                        //    }
                        //    else
                        //    {
                        //        html = "No responses found in this conversation.";
                        //    }

                        //}

                        //  }
                        html = "Message Sent Successfully";
                    }
                    else {
                        html = "Message Not Successfully";
                    }
                    //MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    //string MessageType = Model.MessageType;

                    //MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (MessageData.Valid)
                    //{
                    //    MessageLists.Messages = MessageData.dt.ToMessageModelList();
                    //    htmlMainGrid = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", MessageLists.Messages, this);

                    //}
                    //else
                    //{
                    //    htmlMainGrid = "No responses found in this conversation.";
                    //}
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new { html = html, htmlMainGrid = htmlMainGrid });
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageRequestRefillReply(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string htmlMainGrid = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();
                    DataMessageService.MessageData MsgReturnData = new DataMessageService.MessageData();
                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgData = base.ConvertToMessageData(Model);
                    if (RequestHelper.MyGlobalVar.PortalType == "Patient")
                    {
                        MsgData.ProviderId_From = 0;
                    }
                    else
                    {
                        MsgData.ProviderId_From = RequestHelper.MyGlobalVar.PatientId;//Patient Id is holding logged in Provider ID
                    }
                    
                    
                    MsgData.AttachmentId = "0";
                    MsgData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                    MsgData.AppointmentEnd = Convert.ToDateTime("1/1/1900");

                    MsgData.MessageResponseTypeId = 3;//Text
                    MsgData.MessageUrgency = true;
                    MsgReturnData = service.SaveMessageData(MsgData, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgReturnData.Valid)
                    {
                        MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                        MsgParams.MessageId = Model.MessageId;
                        // if (Model.Flag == "message-reply")
                        // {
                        //MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        //if (MsgData.Valid)
                        //{
                        //    if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        //    {
                        //        html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMedicationRefillDetailGridHtml();
                        //    }
                        //    else
                        //    {
                        //        html = "No responses found in this conversation.";
                        //    }

                        //}

                        // }
                        html = "Message Sent Successfully";
                    }
                    else
                    {
                        html = "Message Not Sent";
                    }
                    //MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    //string MessageType = Model.MessageType;

                    //MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //if (MessageData.Valid)
                    //{
                    //    MessageLists.Messages = MessageData.dt.ToMessageModelList();
                    //    htmlMainGrid = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.Messages, this);

                    //}
                    //else
                    //{
                    //    htmlMainGrid = "No responses found in this conversation.";
                    //}
                   
                   
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(new { html=html, htmlMainGrid=htmlMainGrid});
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageSentDetails(PatientMessageModel Model)
        {
            MessageCenterModel MessageList = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgParams.MessageId = Model.MessageId;

                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgData.Valid)
                    {
                        html = base.ConvertToPatientMessageModel(MsgData.dtDetails).GetSentDetailHTML();
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
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageRefillDetails(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
             string html = string.Empty;
            try
            {
             using (var service = new DataMessageService.MessageWSSoapClient())
             {
                 DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                 DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                 MsgParams.MessageId = Model.MessageId;
                     
                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                 if (MsgData.Valid)
                 {
                     html = base.ConvertToMessageModel(MsgData.dtDetails).GetMedicationRefillDetailHTML();
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
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageRequestReferralDetail(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgParams.MessageId = Model.MessageId;

                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgData.Valid)
                    {
                        
                        html = base.ConvertToPatientMessageModel(MsgData.dtDetails).GetReferralRequestDetailHTML();
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
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageAppointmentRequestDetail(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgParams.MessageId = Model.MessageId;

                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    
                    if (MsgData.Valid)
                    {
                        html = base.ConvertToPatientMessageModel(MsgData.dtDetails).GetAppointmentRequestDetailHTML();
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
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageTabsData(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
                    string MessageType = Model.MessageType;

            string Msghtml = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {

                    //if (MessageType == "Sent Message")
                    //{
                    //    DataMessageService.MessageParms parms = new DataMessageService.MessageParms();

                    //    DataMessageService.MessageTableData MessageData1 = new DataMessageService.MessageTableData();
                    //    parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
                    //    MessageData1 = service.GetMessageDetailSentList(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //    if (MessageData1.Valid)
                    //    {
                    //        //   html = base.ConvertToPatientMessageModel(messagedate.dt).GetSentDetailHTML();
                    //        MessageLists.Messages = MessageData1.dt.ToMessageModelSendList();

                    //    }
                    //    Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.Messages, this);

                    //    DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
                    //    Unread = service.GetUnreadMessageData(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    //    if (Unread.Valid)
                    //    {
                    //        MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                    //        MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                    //        MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                    //        MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                    //        MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                    //    }
             
                    //}
                    //else
                    //{
                        DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                        DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                        MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);


                        string[] MessageType1 = Model.MessageType.Select(c => c.ToString()).ToArray();
                        if (MessageType == "Sent Message")
                        {
                            MessageData = service.GetMessageDetailSentList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        }
                        else
                        {
                            MessageData = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        }
                            if (MessageData.Valid)
                        {
                            MessageLists.Messages = MessageData.dt.ToMessageModelSendList();

                        }

                        if (MessageType == "Appointment Request")
                        {
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", MessageLists.Messages, this);
                            // html = HTMLExtensions.GetAppointmentTabsDataHTML(MessageLists.Messages, MessageType);
                        }
                        else if (MessageType == "Referral Message")
                        {
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", MessageLists.Messages, this);
                            //html = HTMLExtensions.GetReferalRequestTabsDataHTML(MessageLists.Messages, MessageType);
                        }
                        else if (MessageType == "Medication Refill")
                        {
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.Messages, this);
                            //html = HTMLExtensions.GetReferalRequestTabsDataHTML(MessageLists.Messages, MessageType);
                        }
                        else if (MessageType == "34I")
                        {
                            // html = HTMLExtensions.GetSentTabsDataHTML(MessageLists.Messages, MessageType1);
                            //  html = HTMLExtensions.GetInboxTabsDataHTML(MessageLists.Messages, MessageType1);
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.Messages, this);
                        }
                        else if (MessageType == "Sent Message")
                        {
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.Messages, this);
                        }
                        else if (MessageType == "34D")
                        {
                            Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Delete.cshtml", MessageLists.Messages, this);

                        }


                        DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
                        Unread = service.GetUnreadMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                        if (Unread.Valid)
                        {
                            MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                            MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                            MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                            MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                            MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                        }
                    }
                }
            //}

                 

               

            catch (Exception ex)
            {
                return Json(ex.Message);
            }
         
         

            //return Json(new { Msghtml = Msghtml, AppointmentMessages = MessageLists.UnreadMessages.AppointmentMessages, GeneralMessages = MessageLists.UnreadMessages.GeneralMessages, MedicationMessages = MessageLists.UnreadMessages.MedicationMessages, ReferralMessages = MessageLists.UnreadMessages.ReferralMessages, TotalMessages = MessageLists.UnreadMessages.TotalMessages }, "text/html");
            return new JsonResult()
            {
                Data = new { Msghtml = Msghtml, AppointmentMessages = MessageLists.UnreadMessages.AppointmentMessages, GeneralMessages = MessageLists.UnreadMessages.GeneralMessages, MedicationMessages = MessageLists.UnreadMessages.MedicationMessages, ReferralMessages = MessageLists.UnreadMessages.ReferralMessages, TotalMessages = MessageLists.UnreadMessages.TotalMessages },
                ContentType = "text/html",
                //ContentEncoding = contentEncoding,
                //JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
            //return Json(Msghtml);

            
            //return Json(html);
        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageTabsDataProvider(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string Msghtml = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    string MessageType = Model.MessageType;

                    string[] MessageType1 = Model.MessageType.Select(c => c.ToString()).ToArray();

                    MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);
                    MessageData = service.GetMessageDetailProvReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.ProviderMessages = MessageData.dt.ToProviderMessageModelList();
                    }
                    MessageData = service.GetMessageDetailProvSentList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.ProviderSentMessages = MessageData.dt.ToProviderMessageModelList();
                    }
                    MessageData = service.GetMessageListProvider(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.Messages = MessageData.dt.ToMessageModelList();

                    }

                    if (MessageType == "Appointment Request")
                    {
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", MessageLists.ProviderMessages, this);
                        // html = HTMLExtensions.GetAppointmentTabsDataHTML(MessageLists.Messages, MessageType);
                    }
                    else if (MessageType == "Referral Message")
                    {
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", MessageLists.ProviderMessages, this);
                        //html = HTMLExtensions.GetReferalRequestTabsDataHTML(MessageLists.Messages, MessageType);
                    }
                    else if (MessageType == "Medication Refill")
                    {
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.ProviderMessages, this);
                        //html = HTMLExtensions.GetReferalRequestTabsDataHTML(MessageLists.Messages, MessageType);
                    }
                    else if (MessageType == "34I")
                    {
                        // html = HTMLExtensions.GetSentTabsDataHTML(MessageLists.Messages, MessageType1);
                        //  html = HTMLExtensions.GetInboxTabsDataHTML(MessageLists.Messages, MessageType1);
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.ProviderMessages, this);
                    }
                    else if (MessageType == "34S")
                    {
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.ProviderSentMessages, this);

                    }
                    else if (MessageType == "34D")
                    {
                        Msghtml = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Delete.cshtml", MessageLists.ProviderMessages, this);

                    }
                    DataMessageService.MessageUnread Unread = new DataMessageService.MessageUnread();
                    Unread = service.GetUnreadProvMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (Unread.Valid)
                    {
                        MessageLists.UnreadMessages.AppointmentMessages = Unread.AppointmentMessages;
                        MessageLists.UnreadMessages.GeneralMessages = Unread.GeneralMessages;
                        MessageLists.UnreadMessages.MedicationMessages = Unread.MedicationMessages;
                        MessageLists.UnreadMessages.ReferralMessages = Unread.ReferralMessages;
                        MessageLists.UnreadMessages.TotalMessages = Unread.TotalMessages;
                    }

                }
            }





            catch (Exception ex)
            {
                return Json(ex.Message);
            }

            //return Json(new { Msghtml = Msghtml, AppointmentMessages = MessageLists.UnreadMessages.AppointmentMessages, GeneralMessages = MessageLists.UnreadMessages.GeneralMessages, MedicationMessages = MessageLists.UnreadMessages.MedicationMessages, ReferralMessages = MessageLists.UnreadMessages.ReferralMessages, TotalMessages = MessageLists.UnreadMessages.TotalMessages }, "text/html");
            return new JsonResult()
            {
                Data = new { Msghtml = Msghtml, AppointmentMessages = MessageLists.UnreadMessages.AppointmentMessages, GeneralMessages = MessageLists.UnreadMessages.GeneralMessages, MedicationMessages = MessageLists.UnreadMessages.MedicationMessages, ReferralMessages = MessageLists.UnreadMessages.ReferralMessages, TotalMessages = MessageLists.UnreadMessages.TotalMessages },
                ContentType = "text/html",
                MaxJsonLength = Int32.MaxValue
            };
        }

        //for inbox delete 
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult InboxDeleteProvider(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string Message = string.Empty;
            try
            {
                string[] ids = Model.DeletedMessageIds.Split(',');
                DataMessageService.MessageData Msg = base.ConvertToPatientMessageData(Model);

                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    foreach (var item in ids)
                    {
                        if (item != "")
                        {
                            Msg.MessageId = Convert.ToInt64(item);
                            DataMessageService.MessageData MessageData = service.DeleteMessage(Msg, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if(MessageData.Valid)
                            {
                            Message = "Message Deleted";
                            }
                            else{
                            Message = "Message Not Deleted";
                            }
                        }
                    }

                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
                    DataMessageService.MessageTableData MessageData1 = new DataMessageService.MessageTableData();
                    MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                                        
                    if (Model.MessageType == "SentDeleteMessage")
                    {
                        MessageData1 = service.GetMessageDetailProvSentList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    }
                    else {
                        MessageData1 = service.GetMessageDetailProvReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    
                    }
                    if (MessageData1.Valid)
                    {
                        MessageLists.ProviderMessages = MessageData1.dt.ToProviderMessageModelList();
                        if (Model.MessageType == "SentDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.ProviderMessages, this);
                        }
                        if (Model.MessageType == "RefillDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.ProviderMessages, this);
                      
                        }
                        
                        if (Model.MessageType == "ReferrallDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", MessageLists.ProviderMessages, this);
                            //html = HTMLExtensions.GetReferalRequestTabsDataHTML(MessageLists.Messages, MessageType);
                           
                        }
                        if (Model.MessageType == "InboxMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.ProviderMessages, this);
                        }
                        
                    }
                }

            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }



            return Json(new {html = html,Message = Message});

        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageInboxGridDetails(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgParams.MessageId = Model.MessageId;
                   
                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgData.Valid)
                    {
                        if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        {
                            if (Model.MessageType == "Medication Refill")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMedicationRefillDetailGridHtml();
                            }
                            else if (Model.MessageType == "Appointment Request")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetAppointmentDetailGridHtml();
                            }
                            else if (Model.MessageType == "Referral Request")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetReferralRequestDetailGridHtml();
                            }
                            else
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMessageDetailGridHtml();
                            }
                        }
                        else
                        {
                            html = "No further responses found in this conversation.";
                        }

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
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageSendGridDetails(MessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageData MsgData = new DataMessageService.MessageData();

                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MsgParams.MessageId = Model.MessageId;

                    MsgData = service.GetMessageData(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MsgData.Valid)
                    {
                        if (MsgData.dtDetails != null && MsgData.dtDetails.Rows.Count > 0)
                        {
                            if (Model.MessageType == "Medication Refill")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMedicationRefillDetailGridHtml();
                            }
                            else if (Model.MessageType == "Appointment Request")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetAppointmentDetailGridHtml();
                            }
                            else if (Model.MessageType == "Referral Request")
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetReferralRequestDetailGridHtml();
                            }
                            else
                            {
                                html = MsgData.dtDetails.ToMessageModelDetailInboxList().GetMessageDetailGridHtml();
                            }
                        }
                        else
                        {
                            html = "No further responses found in this conversation.";
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }


            return Json(html);
        }

        //for inbox delete 
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult InboxDelete(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string htmldelete = string.Empty;
            try
            {
                string[] ids = Model.DeletedMessageIds.Split(',');
                DataMessageService.MessageData Msg = base.ConvertToPatientMessageData(Model);

                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    foreach (var item in ids)
                    {
                        if (item != "")
                        {
                            Msg.MessageId = Convert.ToInt64(item);
                            DataMessageService.MessageData MessageData = service.DeleteMessage(Msg, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                        }
                    }

                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();
                    DataMessageService.MessageTableData MessageData1 = new DataMessageService.MessageTableData();
                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                    MessageData1 = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData1.Valid)
                    {
                        MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
                     //   MessageLists.Messages = MessageData1.dt.ToMessageModelList();
                        MessageLists.Messages = MessageData1.dt.ToMessageModelSendList();

                        //html = MessageLists.Messages.GetMessageMasterGridHtml();
                        if (Model.MessageType == "SentDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.Messages, this);
                         //   html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageLists.Messages, this);

                        }
                        else  if (Model.MessageType == "RefillDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refill.cshtml", MessageLists.Messages, this);
                            htmldelete = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Delete.cshtml", MessageLists.Messages, this);

                        }
                        else if (Model.MessageType == "ReferrallDeleteMessage")
                        {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Refferal.cshtml", MessageLists.Messages, this);
                            htmldelete = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Delete.cshtml", MessageLists.Messages, this);

                        }
                            
                        else {
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Inbox.cshtml", MessageLists.Messages, this);
                            htmldelete = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Delete.cshtml", MessageLists.Messages, this);

                        }
                        
                    }
                }
                    
            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }



            return Json(new { html = html, htmldelete = htmldelete });

        }



        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult MessageSendData(PatientMessageModel Model)
        {
            DataMessageService.MessageTableData messagedate = new DataMessageService.MessageTableData();
            DataMessageService.MessageParms parms = new DataMessageService.MessageParms();
            parms.PatientId = RequestHelper.MyGlobalVar.PatientId;
             MessageCenterModel MessageList = new MessageCenterModel();
            string html = string.Empty;
            try
            {
                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    messagedate = service.GetMessageDetailSentList(parms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (messagedate.Valid)
                    {
                     //   html = base.ConvertToPatientMessageModel(messagedate.dt).GetSentDetailHTML();
                        MessageList.Messages = messagedate.dt.ToMessageModelSendList();

                    }
                      

                
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Sent.cshtml", MessageList.Messages, this);

            return Json(html);
        
        }
        //Appointment Cancel
        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult AppointmentCancel(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string html1 = string.Empty;
            try
            {
                string[] ids = new string[] { };

                if (Model.DeletedMessageIds.Contains(","))
                {
                   
                    ids = Model.DeletedMessageIds.Split(',');
                }
                else
                {
                    ids = new string[] {Model.DeletedMessageIds};
                }

                int totRows = ids.Count();
                DataMessageService.MessageData Msg = base.ConvertToPatientMessageData(Model);

                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    foreach (var item in ids)
                    {
                        if (item != "")
                        {
                            Msg.MessageId = Convert.ToInt64(item);
                            DataMessageService.MessageData AppCancel = service.CancelMessage(Msg,RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                   

                        }
                    }

                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();
                    
                    MsgParams.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);

                    string MessageType = "Appointment Request";
                  //  MessageData = service.GetMessageList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    MessageData = service.GetMessageDetailReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                    if (MessageData.Valid)
                    {
                        MessageLists.Messages = MessageData.dt.ToMessageModelSendList();
                   
                            html1 = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", MessageLists.Messages, this);
                           // html = HTMLExtensions.GetAppointmentTabsDataHTML(MessageLists.Messages, MessageType);
                       
                     
                          //  html = ViewHelper.RenderRazorViewToString("_Appointment_Index", MessageLists.Messages, this);
                            html = ViewHelper.RenderRazorViewToString("AppointmentTab_index", MessageLists.Messages, this);

                      
                    }

                    
                   
                }

            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }



            return Json(new { html = html, html1 = html1 });

        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult AppointmentCancelProvider(PatientMessageModel Model)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();
            string html = string.Empty;
            string Message = string.Empty;
            try
            {
                string[] ids = new string[] { };

                if (Model.DeletedMessageIds.Contains(","))
                {

                    ids = Model.DeletedMessageIds.Split(',');
                }
                else
                {
                    ids = new string[] { Model.DeletedMessageIds };
                }

                int totRows = ids.Count();
                DataMessageService.MessageData Msg = base.ConvertToPatientMessageData(Model);

                using (var service = new DataMessageService.MessageWSSoapClient())
                {
                    foreach (var item in ids)
                    {
                        if (item != "")
                        {
                            Msg.MessageId = Convert.ToInt64(item);
                            DataMessageService.MessageData AppCancel = service.CancelMessage(Msg, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                            if (AppCancel.Valid)
                            {
                                Message = "Appointment Cancelled";
                            }
                            else {
                                Message = "Appointment Not Cancelled";
                            }

                        }
                    }

                    DataMessageService.MessageParms MsgParams = new DataMessageService.MessageParms();

                    DataMessageService.MessageTableData MessageData = new DataMessageService.MessageTableData();

                    MsgParams.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);

                    //string MessageType = "Appointment Request";
                    MessageData = service.GetMessageDetailProvReceivedList(MsgParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (MessageData.Valid)
                    {
                        MessageLists.ProviderMessages = MessageData.dt.ToProviderMessageModelList();
                        //if (totRows > 1)
                        //{
                            html = ViewHelper.RenderRazorViewToString("~/Views/Templates/Message/_Message_Appointment.cshtml", MessageLists.ProviderMessages, this);
                            // html = HTMLExtensions.GetAppointmentTabsDataHTML(MessageLists.Messages, MessageType);
                        //}
                        //else
                        //{
                        //    html = ViewHelper.RenderRazorViewToString("_Appointment_Index", MessageLists.Messages, this);
                        //    // html = MessageLists.Messages.GetAppointmentIndexDataHTML();
                        //}
                    }



                }

            }

            catch (Exception ex)
            {
                return Json(ex.Message);
            }



            return Json(new { html = html, Message = Message });

        }

        [HttpPost]
        [AnotherSessionRedirectionFilter] [Authorize]
        public JsonResult GetPatientList(string term, long FacilityId)
        {
            MessageCenterModel MessageLists = new MessageCenterModel();

            try
            {
                //Configure Dropdowns Data
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientTableData PatData = new DataPatientService.PatientTableData();
                    RequestHelper.MyGlobalVar.FacilityId = 4;
                    PatData = service.GetPatientForFacilityList(FacilityId, term, term, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                   if(PatData.Valid)
                   {
                       MessageLists.PatientList = PatData.dt.ToPatientModel1List();
                   }
                    

                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
          
           return Json(MessageLists.PatientList, JsonRequestBehavior.AllowGet);
        }
    }

    }
