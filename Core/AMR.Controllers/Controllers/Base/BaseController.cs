using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AMR.Core.Utilities;
using AMR.Models;
using AMR.Core.Extensions;

namespace AMR.Controllers.Controllers.Base
{
    public partial class BaseController : Controller
    {


        public BaseController()
        {

        }

        public void PopulateProvider()
        {
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderData Provider = new DataProviderService.ProviderData();
                DataProviderService.ProviderParms PrvParms = new DataProviderService.ProviderParms();

                PrvParms.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);

                Provider = service.GetProviderData(PrvParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Provider.Valid)
                {
                    GlobalVar gb = TicketHelper.SetProviderInformationAndGet(Provider.ProviderId.ToString() + "," + Provider.FirstName + " " + Provider.LastName);
                }
            }

        }

        public void PopulatePatient()
        {
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();


                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);


                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    GlobalVar gb =  TicketHelper.SetPatientInformationAndGet(PatientSummary.PatientId.ToString() + "," + PatientSummary.FirstName + " " + PatientSummary.LastName);
                 
                }
            }
            
        }

        public void PopulatePremiumPatient(bool Upgrade)
        {
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();


                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);


                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    GlobalVar gb = TicketHelper.SetPatientInformationAndGet(PatientSummary.PatientId.ToString() + "," + PatientSummary.FirstName + " " + PatientSummary.LastName + "," + Upgrade.ToString());

                }
            }

        }
        public PatientRepModel GetRepData()
        {
           
            PatientRepModel PatientRepresentatives=new PatientRepModel();
            DataPatientService.PatientRepData PatRepData = new DataPatientService.PatientRepData();
            if (RequestHelper.MyGlobalVar.UserLoginEx.StartsWith("R"))
            {
                using (var service = new DataPatientService.PatientWSSoapClient())
                {
                    DataPatientService.PatientParms PRParms = new DataPatientService.PatientParms();

                    PRParms.FacilityId = RequestHelper.MyGlobalVar.FacilityId;
                    PRParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);
                    PRParms.Option = 1;
                    PatRepData = service.GetPatientRepData(PRParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                    if (PatRepData.Valid)
                    {
                        PatientRepresentatives = this.ConvertToPatientRepDataModel(PatRepData);
                    }
                }
            }
            else {
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
                PatientRepresentatives.Provider = true;     // SJF Added 1/22/2015
            
            
            
            }
            return PatientRepresentatives;
        }
        public string[] GetPatientname()
        {
            String[] Patient = new string[2];
            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
            using (var Pservice = new DataPatientService.PatientWSSoapClient())
            {
                PatTable = Pservice.GetPatientData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);
                Patient[0] = PatTable.FirstName + " " + PatTable.LastName;
                Patient[1] = PatTable.PatientId.ToString();
            }
            return Patient;
        }
        public bool PopulateAccountLinkPatient(String UserLogin)
        {
            bool valid = false;
            GlobalVar gv = null;
            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            PatParams.PatientId = Convert.ToInt64(UserLogin);
            DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
            using (var Pservice = new DataPatientService.PatientWSSoapClient())
            {
                PatTable = Pservice.GetPatientData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);

                gv = TicketHelper.SetPatientInformationAndGet(RequestHelper.MyGlobalVar.PatientId.ToString() + "," + PatTable.FirstName + " " + PatTable.LastName + "," + PatTable.PremiumFlag.ToString() + "," + UserLogin.ToString());    
            }
            if (gv != null) valid = true;
            return valid;
        }
        public bool PopulateSecurityPatient()
        {
            bool valid = false;
            GlobalVar gv = null;
            DataPatientService.PatientParms PatParams = new DataPatientService.PatientParms();
            PatParams.PatientId = RequestHelper.MyGlobalVar.PatientId;
            DataPatientService.PatientData PatTable = new DataPatientService.PatientData();
            using (var Pservice = new DataPatientService.PatientWSSoapClient())
            {
                PatTable = Pservice.GetPatientData(PatParams, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, 0);

                gv = TicketHelper.SetPatientInformationAndGet(RequestHelper.MyGlobalVar.PatientId.ToString() + "," + PatTable.FirstName + " " + PatTable.LastName + "," + PatTable.PremiumFlag.ToString() + "," + RequestHelper.MyGlobalVar.UserLogin + "," + false+","+false+","+  RequestHelper.MyGlobalVar.UserLoginEx);
            }
            if (gv != null) valid = true;
            return valid;
        }
//add for provider security Question
        public bool PopulateSecuirtyProvider()
        {
            bool valid = false;
            GlobalVar gv = null;
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderData Provider = new DataProviderService.ProviderData();
                DataProviderService.ProviderParms PrvParms = new DataProviderService.ProviderParms();

                PrvParms.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);

                Provider = service.GetProviderData(PrvParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Provider.Valid)
                {
                    gv = TicketHelper.SetProviderInformationAndGet(Provider.ProviderId.ToString() + "," + Provider.FirstName + " " + Provider.LastName+ "," + RequestHelper.MyGlobalVar.UserLogin + "," + false+","+false);
                }
            }
            if (gv != null) valid = true;
            return valid;
        
        }
        public GlobalVar PopulatePatientOnLayoutMaster()
        {
            GlobalVar gv=null;
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                DataPatientService.PatientSummaryData PatientSummary = new DataPatientService.PatientSummaryData();
                DataPatientService.PatientParms PatParms = new DataPatientService.PatientParms();


                PatParms.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);


                PatientSummary = service.GetPatientSummaryData(PatParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);

                if (PatientSummary.Valid)
                {
                    gv= TicketHelper.SetPatientInformationAndGet(PatientSummary.PatientId.ToString() + "," + PatientSummary.FirstName + " " + PatientSummary.LastName);

                }
            }
            return gv;
        }

        public GlobalVar PopulateProviderOnLayoutMaster()
        {
            GlobalVar gv = null;
            using (var service = new DataProviderService.ProviderWSSoapClient())
            {
                DataProviderService.ProviderData Provider = new DataProviderService.ProviderData();
                DataProviderService.ProviderParms PrvParms = new DataProviderService.ProviderParms();

                PrvParms.ProviderId = Convert.ToInt64(RequestHelper.MyGlobalVar.UserLogin);

                Provider = service.GetProviderData(PrvParms, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
                if (Provider.Valid)
                {
                     gv = TicketHelper.SetProviderInformationAndGet(Provider.ProviderId.ToString() + "," + Provider.FirstName + " " + Provider.LastName);
                }
            }
            
            return gv;
        }

       
        public DataPatientDocumentService.PatientFamilyHistData ConvertToPatientFamilyHistData(FamilyHistoryModel PatientFamilyHistory)
        {
            DataPatientDocumentService.PatientFamilyHistData objFamilyHist = new DataPatientDocumentService.PatientFamilyHistData();


            objFamilyHist.PatientId = PatientFamilyHistory.PatientId;
            objFamilyHist.VisitId = PatientFamilyHistory.VisitId;
            objFamilyHist.PatFamilyHistCntr = PatientFamilyHistory.PatFamilyHistCntr;
            objFamilyHist.FacilityId = PatientFamilyHistory.FacilityId;
            objFamilyHist.RelationshipId = Convert.ToInt16(PatientFamilyHistory.RelationshipId);
            objFamilyHist.FamilyMember = PatientFamilyHistory.FamilyMember;
            objFamilyHist.Description = PatientFamilyHistory.Description;
            objFamilyHist.Qualifier = PatientFamilyHistory.Qualifier;
            objFamilyHist.CodeValue = PatientFamilyHistory.CodeValue;
            objFamilyHist.CodeSystemId = PatientFamilyHistory.CodeSystemId;
            objFamilyHist.AgeAtOnset = PatientFamilyHistory.AgeAtOnset;
            objFamilyHist.DiseasedAge = PatientFamilyHistory.DiseasedAge;
            objFamilyHist.ConditionStatusId = PatientFamilyHistory.ConditionStatusId;
          //  objFamilyHist.DateReported = PatientFamilyHistory.DateReported;
            objFamilyHist.Diseased = PatientFamilyHistory.Diseased;
            objFamilyHist.Note = PatientFamilyHistory.Note;



            return objFamilyHist;
        }


        public MessageModel ConvertToMessageModel(System.Data.DataTable table)
        {
            MessageModel objMessage = new MessageModel();
            foreach (System.Data.DataRow row in table.Rows)
            {

                objMessage.MessageDetailId = Convert.ToInt64(row["MessageDetailId"]);
                objMessage.ProviderId_To = Convert.ToInt64(row["ProviderId_To"]);
                objMessage.ProviderId_From = Convert.ToInt64(row["ProviderId_From"]);
                objMessage.MessageRequest = row["MessageRequest"].ToString();
                objMessage.MessageResponse = row["MessageResponse"].ToString();
                objMessage.MessageResponseTypeId = Convert.ToInt32(row["MessageResponseTypeId"]);
                objMessage.PreferredPeriod = row["PreferredPeriod"].ToString();
                objMessage.PreferredTime = row["PreferredTime"].ToString();
                objMessage.PreferredWeekDay = row["PreferredWeekDay"].ToString();
                objMessage.VisitReason = row["VisitReason"].ToString();
                objMessage.MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]);
                objMessage.AppointmentStart = Convert.ToDateTime(row["AppointmentStart"].ToString());
                objMessage.AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"].ToString());
                objMessage.ProviderId_Appointment = Convert.ToInt64(row["ProviderId_Appointment"]);
                objMessage.MedicationNDC = row["MedicationNDC"].ToString();
                objMessage.MedicationName = row["MedicationName"].ToString();
                objMessage.NoOfRefills = Convert.ToInt32(row["NoOfRefills"].ToString());
                objMessage.MedicationStatus = Convert.ToInt32(row["MedicationStatus"].ToString());
                objMessage.PharmacyName = row["PharmacyName"].ToString();
                objMessage.PharmacyAddress = row["PharmacyAddress"].ToString();
                objMessage.AttachmentId = Convert.ToString(row["AttachmentId"]);

            }
            return objMessage;
        }
        public PatientMessageModel ConvertToPatientMessageModel(System.Data.DataTable table)
        {
            PatientMessageModel objMessage = new PatientMessageModel();
            foreach (System.Data.DataRow row in table.Rows)
            {

                objMessage.MessageDetailId = Convert.ToInt64(row["MessageDetailId"]);
                objMessage.ProviderId_To = Convert.ToString(row["ProviderId_To"]);
                objMessage.ProviderId_From = Convert.ToString(row["ProviderId_From"]);
                objMessage.MessageRequest = row["MessageRequest"].ToString();
                objMessage.MessageResponse = row["MessageResponse"].ToString();
                objMessage.MessageResponseTypeId = Convert.ToString(row["MessageResponseTypeId"]);
                objMessage.PreferredPeriod = row["PreferredPeriod"].ToString();
                objMessage.PreferredTime = row["PreferredTime"].ToString();
                objMessage.PreferredWeekDay = row["PreferredWeekDay"].ToString();
                objMessage.VisitReason = row["VisitReason"].ToString();
                objMessage.MessageUrgencyId = Convert.ToString(row["MessageUrgencyId"]);
                objMessage.AppointmentStart = Convert.ToDateTime(row["AppointmentStart"].ToString());
                objMessage.AppointmentEnd = Convert.ToDateTime(row["AppointmentEnd"].ToString());
                objMessage.ProviderId_Appointment = Convert.ToString(row["ProviderId_Appointment"]);
                objMessage.MedicationNDC = row["MedicationNDC"].ToString();
                objMessage.MedicationName = row["MedicationName"].ToString();
                objMessage.NoOfRefills = Convert.ToString(row["NoOfRefills"].ToString());
                objMessage.MedicationStatus = Convert.ToString(row["MedicationStatus"].ToString());
                objMessage.PharmacyName = row["PharmacyName"].ToString();
                objMessage.PharmacyAddress = row["PharmacyAddress"].ToString();
                objMessage.AttachmentId = Convert.ToString(row["AttachmentId"]);

                objMessage.ProviderName_To = Convert.ToString(row["ProviderName_To"].ToString());
                objMessage.ProviderName_From = Convert.ToString(row["ProviderName_From"].ToString());
                objMessage.CreatedByName = row["CreatedByName"].ToString();
                objMessage.MessageUrgency = Convert.ToBoolean(row["MessageUrgency"]);
                objMessage.MessageType = Convert.ToString(row["MessageType"]);


            }
            return objMessage;
        }

        public DataMessageService.MessageData ConvertToPatientMessageData(PatientMessageModel PatientMessageData)
        {
            DataMessageService.MessageData objPatientMessage = new DataMessageService.MessageData();

            objPatientMessage.AppointmentEnd =Convert.ToDateTime(PatientMessageData.AppointmentEnd);
            objPatientMessage.AppointmentStart = Convert.ToDateTime(PatientMessageData.AppointmentStart);
            objPatientMessage.AttachmentId = PatientMessageData.AttachmentId;
            objPatientMessage.CreatedByName = PatientMessageData.CreatedByName;
            objPatientMessage.FacilityId = PatientMessageData.FacilityId;
            objPatientMessage.MedicationName = PatientMessageData.MedicationName;
            objPatientMessage.MedicationNDC = Convert.ToString(PatientMessageData.MedicationNDC);

            objPatientMessage.MedicationStatus =Convert.ToInt32(PatientMessageData.MedicationStatus);
            objPatientMessage.MessageDetailId = PatientMessageData.MessageDetailId;
            objPatientMessage.MessageId = PatientMessageData.MessageId;
            objPatientMessage.MessageRequest = PatientMessageData.MessageRequest;
            objPatientMessage.MessageResponse = PatientMessageData.MessageResponse;
            objPatientMessage.MessageResponseTypeId = Convert.ToInt32(PatientMessageData.MessageResponseTypeId);
            objPatientMessage.MessageStatusId = Convert.ToInt32(PatientMessageData.MessageStatusId);
            objPatientMessage.MessageTypeId = Convert.ToInt32(PatientMessageData.MessageTypeId);


         //   objPatientMessage.MessageUrgencyId = Convert.ToInt32(PatientMessageData.MessageUrgencyId);
            objPatientMessage.NoOfRefills = Convert.ToInt32(PatientMessageData.NoOfRefills);
            objPatientMessage.PatientId = PatientMessageData.PatientId;
            objPatientMessage.PharmacyAddress = PatientMessageData.PharmacyAddress;
            objPatientMessage.PharmacyName = PatientMessageData.PharmacyName;
            objPatientMessage.PreferredPeriod = PatientMessageData.PreferredPeriod;
            objPatientMessage.PreferredTime = PatientMessageData.PreferredTime;
            objPatientMessage.PreferredWeekDay = Convert.ToString(PatientMessageData.PreferredWeekDay);

            objPatientMessage.ProviderId_Appointment =Convert.ToInt64( PatientMessageData.ProviderId_Appointment);
            objPatientMessage.ProviderId_From = Convert.ToInt64(PatientMessageData.ProviderId_From);
            objPatientMessage.ProviderId_To = Convert.ToInt64(PatientMessageData.ProviderId_To);
            objPatientMessage.VisitReason =Convert.ToString( PatientMessageData.VisitReason);
            
            return objPatientMessage;
        }


        public DataMessageService.MessageData ConvertToMessageData(MessageModel PatientMessage)
        {
            DataMessageService.MessageData objMessage = new DataMessageService.MessageData();

            objMessage.MessageId = PatientMessage.MessageId;
            objMessage.PatientId = PatientMessage.PatientId;
            objMessage.MessageTypeId = PatientMessage.MessageTypeId;
            objMessage.FacilityId = PatientMessage.FacilityId;
            objMessage.MessageStatusId = PatientMessage.MessageStatusId;
            objMessage.MessageDetailId = PatientMessage.MessageDetailId;
            objMessage.ProviderId_To = PatientMessage.ProviderId_To;
            objMessage.ProviderId_From = PatientMessage.ProviderId_From;
            objMessage.MessageRequest = PatientMessage.MessageRequest;
            objMessage.MessageResponse = PatientMessage.MessageResponse;
            objMessage.MessageResponseTypeId = PatientMessage.MessageResponseTypeId;
            objMessage.PreferredPeriod = PatientMessage.PreferredPeriod;
            objMessage.PreferredTime = PatientMessage.PreferredTime;
            objMessage.PreferredWeekDay = PatientMessage.PreferredWeekDay;
            objMessage.VisitReason = PatientMessage.VisitReason;
            objMessage.MessageUrgency = PatientMessage.MessageUrgency;
            objMessage.AppointmentStart = PatientMessage.AppointmentStart;
            objMessage.AppointmentEnd = PatientMessage.AppointmentEnd;
            objMessage.ProviderId_Appointment = PatientMessage.ProviderId_Appointment;
            objMessage.MedicationNDC = PatientMessage.MedicationNDC;
            objMessage.MedicationName = PatientMessage.MedicationName;
            objMessage.NoOfRefills = PatientMessage.NoOfRefills;
            objMessage.MedicationStatus = PatientMessage.MedicationStatus;
            objMessage.PharmacyName = PatientMessage.PharmacyName;
            objMessage.PharmacyAddress = PatientMessage.PharmacyAddress;
            objMessage.AttachmentId = PatientMessage.AttachmentId;
            objMessage.AttachmentName = PatientMessage.AttachmentName;
            objMessage.Attachment = PatientMessage.Attachment;
            objMessage.CreatedByName = PatientMessage.CreatedByName;
            objMessage.PharmacyPhone = PatientMessage.PharmacyPhone;
            return objMessage;
        }
        public DataPatientDocumentService.PatientProcedureData ConvertToProcedureData(ProcedureModel PatientProcedure)
        {
            DataPatientDocumentService.PatientProcedureData proceduredata = new DataPatientDocumentService.PatientProcedureData();
            proceduredata.PatientId = PatientProcedure.PatientId;
            proceduredata.PatProcedureCntr = PatientProcedure.PatProcedureCntr;
            proceduredata.FacilityId = PatientProcedure.FacilityId;
            proceduredata.VisitId = PatientProcedure.VisitId;
            proceduredata.Description = PatientProcedure.Description;
            proceduredata.CodeValue = PatientProcedure.CodeValue;
            proceduredata.CodeSystemId = PatientProcedure.CodeSystemId;
            proceduredata.Diagnosis = PatientProcedure.Diagnosis;
            proceduredata.PerformedBy = PatientProcedure.PerformedBy;
            proceduredata.ServiceLocation = PatientProcedure.ServiceLocation;
            proceduredata.ServiceDate= PatientProcedure.ServiceDate;
           
            proceduredata.Note = PatientProcedure.Note;
            return proceduredata;
        
        }

        public DataPatientDocumentService.PatientSocialHistData ConvertToPatientSocialHistory(SocialHistoryModel PatientSocialHist)
        {
            DataPatientDocumentService.PatientSocialHistData objPatientSocial = new DataPatientDocumentService.PatientSocialHistData();
            objPatientSocial.PatientId = PatientSocialHist.PatientId;
            objPatientSocial.VisitId = PatientSocialHist.VisitId;
            objPatientSocial.PatSocialHistCntr = PatientSocialHist.PatSocialHistCntr;
            objPatientSocial.FacilityId = PatientSocialHist.FacilityId;
            objPatientSocial.Description = PatientSocialHist.Description;
            objPatientSocial.Qualifier = PatientSocialHist.Qualifier;
            objPatientSocial.CodeValue = PatientSocialHist.CodeValue;
            objPatientSocial.CodeSystemId = Convert.ToInt32(PatientSocialHist.CodeSystemId);
            objPatientSocial.BeginDate = PatientSocialHist.BeginDate;
            objPatientSocial.EndDate = PatientSocialHist.EndDate;
            objPatientSocial.Note = PatientSocialHist.Note;

            return objPatientSocial;
        }

        public DataPatientDocumentService.PatientLabResultData ConvertToPatietntLabResultData(LabResultModel PatientLabResultData)
        {
            DataPatientDocumentService.PatientLabResultData pPatientLabResultData = new DataPatientDocumentService.PatientLabResultData();

          //  pPatientLabResultData.CollectionDate = PatientLabResultData.CollectionDate;
            pPatientLabResultData.LabName = PatientLabResultData.LabName;
            pPatientLabResultData.LabResultCntr = PatientLabResultData.LabResultCntr;
           // pPatientLabResultData.OrderDate = PatientLabResultData.OrderDate;
            pPatientLabResultData.ProviderId_Requested = PatientLabResultData.ProviderId_Requested;
            pPatientLabResultData.ReportDate = PatientLabResultData.ReportDate;
            pPatientLabResultData.Requisiton = PatientLabResultData.Requisiton;
          //  pPatientLabResultData.ReviewDate = PatientLabResultData.ReviewDate;
            pPatientLabResultData.Reviewer = PatientLabResultData.Reviewer;
            pPatientLabResultData.Specimen = PatientLabResultData.Specimen;
            pPatientLabResultData.SpecimenSource = PatientLabResultData.SpecimenSource;
            pPatientLabResultData.PatientId = PatientLabResultData.PatientId;
            pPatientLabResultData.VisitId = PatientLabResultData.VisitId;
            pPatientLabResultData.FacilityId = PatientLabResultData.FacilityId;
            return pPatientLabResultData;
        }

        public DataPatientDocumentService.PatientProblemData ConvertToPatientProblemDataData(ProblemModel PatientProblemData)
        {
            DataPatientDocumentService.PatientProblemData pPatientProblemData = new DataPatientDocumentService.PatientProblemData();

            pPatientProblemData.CodeSystemId = PatientProblemData.CodeSystemId;
            pPatientProblemData.CodeValue = PatientProblemData.CodeValue;
           // pPatientProblemData.Condition = PatientProblemData.Condition;
            pPatientProblemData.Condition = PatientProblemData.Description;
            pPatientProblemData.Note = PatientProblemData.Note;
            pPatientProblemData.ConditionStatusId = PatientProblemData.ConditionStatusId;
            pPatientProblemData.EffectiveDate = PatientProblemData.EffectiveDate;
            pPatientProblemData.LastChangeDate = PatientProblemData.LastChangeDate;
            pPatientProblemData.PatientProblemCntr = PatientProblemData.PatientProblemCntr;
            pPatientProblemData.PatientId = PatientProblemData.PatientId;
            pPatientProblemData.VisitId = PatientProblemData.VisitId;
            pPatientProblemData.FacilityId = PatientProblemData.FacilityId;
            return pPatientProblemData;
        }
      //  for allergies
        public DataPatientDocumentService.PatientAllergyData ConvertToPatientAllergyDataData(AllergyModel PatientAllergyData)
        {
            DataPatientDocumentService.PatientAllergyData pPatientAllergyData = new DataPatientDocumentService.PatientAllergyData();

            
           
            pPatientAllergyData.PatientId = PatientAllergyData.PatientId;
            pPatientAllergyData.FacilityId = PatientAllergyData.FacilityId;
            pPatientAllergyData.VisitId = PatientAllergyData.VisitId;
            pPatientAllergyData.CodeValue_Reaction = PatientAllergyData.CodeValue_Reaction;
           // pPatientAllergyData.CodeSystem_Reaction = PatientAllergyData.CodeSystem_Reaction;
            PatientAllergyData.OnCard = PatientAllergyData.OnCard;
            pPatientAllergyData.OnKeys = PatientAllergyData.OnKeys;
            pPatientAllergyData.CodeValue_Allergen = PatientAllergyData.CodeValue_Allergen;
            pPatientAllergyData.Allergen = PatientAllergyData.Allergen;
            pPatientAllergyData.Note = PatientAllergyData.Note;
            pPatientAllergyData.ConditionStatusId = PatientAllergyData.ConditionStatusId;
            pPatientAllergyData.EffectiveDate = PatientAllergyData.EffectiveDate;
            pPatientAllergyData.AllergenType = PatientAllergyData.AllergenType;
          //  pPatientAllergyData.CodeSystem_Allergen = PatientAllergyData.CodeSystem_Allergen;
            pPatientAllergyData.Reaction = PatientAllergyData.Reaction;
            pPatientAllergyData.OnCard = PatientAllergyData.OnCard;
            pPatientAllergyData.PatientAllergyCntr = PatientAllergyData.PatientAllergyCntr;
            return pPatientAllergyData;
        }



        public DataPatientDocumentService.PatientImmunizationData ConvertToPatietntImmunizationData(ImmunizationModel PatientImmunizationData)
        {
            DataPatientDocumentService.PatientImmunizationData pPatientImmunizationData = new DataPatientDocumentService.PatientImmunizationData();

            pPatientImmunizationData.PatientImmunizationCntr = PatientImmunizationData.PatientImmunizationCntr;
            pPatientImmunizationData.Amount = PatientImmunizationData.Amount;
            pPatientImmunizationData.ExpirationDate = PatientImmunizationData.ExpirationDate;
            pPatientImmunizationData.Note = PatientImmunizationData.Note;
            pPatientImmunizationData.ImmunizationDate = PatientImmunizationData.ImmunizationDate;
            pPatientImmunizationData.LotNumber = PatientImmunizationData.LotNumber;
            pPatientImmunizationData.Manufacturer = PatientImmunizationData.Manufacturer;
            pPatientImmunizationData.Route = PatientImmunizationData.Route;
            pPatientImmunizationData.Sequence = PatientImmunizationData.Sequence;
            pPatientImmunizationData.Site = PatientImmunizationData.Site;
            pPatientImmunizationData.Vaccine = PatientImmunizationData.Vaccine;
            pPatientImmunizationData.PatientId = PatientImmunizationData.PatientId;
            pPatientImmunizationData.VisitId = PatientImmunizationData.VisitId;
            pPatientImmunizationData.ImmunizationTime = PatientImmunizationData.Time;
            pPatientImmunizationData.FacilityId = PatientImmunizationData.FacilityId;
            pPatientImmunizationData.CodeValue = PatientImmunizationData.CodeValue;
            return pPatientImmunizationData;
        }
        public DataPatientDocumentService.PatientMedicalHistData ConvertToPatientMedicalHistoryData(MedicalHistoryModel PatientMedicalHistData)
        {
            DataPatientDocumentService.PatientMedicalHistData pMedicalHistory = new DataPatientDocumentService.PatientMedicalHistData();

            pMedicalHistory.PatMedicalHistCntr = PatientMedicalHistData.PatMedicalHistCntr;
            pMedicalHistory.DateOfOccurance = PatientMedicalHistData.DateOfOccurance;
            pMedicalHistory.Description = PatientMedicalHistData.Description;
            pMedicalHistory.Note = PatientMedicalHistData.Note;
            pMedicalHistory.OnCard = PatientMedicalHistData.OnCard;
            pMedicalHistory.OnKeys = PatientMedicalHistData.OnKeys;
            pMedicalHistory.PatientId = PatientMedicalHistData.PatientId;
            pMedicalHistory.VisitId = PatientMedicalHistData.VisitId;
            pMedicalHistory.FacilityId = PatientMedicalHistData.FacilityId;
            return pMedicalHistory;
        }
        public DataPatientDocumentService.PatientVitalSignData ConvertToPatientVitalSignData(PatientVitalSignModel PatientVitalData)
        {
            DataPatientDocumentService.PatientVitalSignData objPatientVital = new DataPatientDocumentService.PatientVitalSignData();

            objPatientVital.BloodPressurePosn = PatientVitalData.BloodPressurePosn;
            objPatientVital.VitalDate = PatientVitalData.VitalDate;
            objPatientVital.WeightLb = PatientVitalData.WeightLb;
            objPatientVital.HeightIn = PatientVitalData.HeightIn;
            objPatientVital.Systolic = PatientVitalData.Systolic;
            objPatientVital.Diastolic = PatientVitalData.Diastolic;
            objPatientVital.Pulse = PatientVitalData.Pulse;
            objPatientVital.Respiration = PatientVitalData.Respiration;
            objPatientVital.PatientId = PatientVitalData.PatientId;
            objPatientVital.VisitId = PatientVitalData.VisitId;
            objPatientVital.FacilityId = PatientVitalData.FacilityId;
            objPatientVital.PatientVitalCntr = PatientVitalData.PatientVitalCntr;
            objPatientVital.Temperature = PatientVitalData.Temperature;
            return objPatientVital;
        }
        public DataPatientDocumentService.PatientMedicationData ConvertToPatientMedicationData(PatientMedicationModel PatientMedData)
        {
            DataPatientDocumentService.PatientMedicationData objPatientMed = new DataPatientDocumentService.PatientMedicationData();

            objPatientMed.Active = PatientMedData.Active;
            //objPatientMed.Days = PatientMedData.Days;
            objPatientMed.Diagnosis = PatientMedData.Diagnosis;
            objPatientMed.ExpireDate = (PatientMedData.ExpireDate.Year == 1) ? Convert.ToDateTime("1900/1/1") : PatientMedData.ExpireDate;
            objPatientMed.Frequency = PatientMedData.Frequency;
            objPatientMed.MedicationName = PatientMedData.MedicationName;
            objPatientMed.RouteId = PatientMedData.Route;
            objPatientMed.StartDate = PatientMedData.StartDate;
            objPatientMed.Frequency  = PatientMedData.Frequency;
           // objPatientMed.NDC = PatientMedData.NDC;
            objPatientMed.Note = PatientMedData.Note;
            objPatientMed.PatientMedicationCntr = Convert.ToInt64(PatientMedData.PatientMedicationCntr);
            objPatientMed.Pharmacy = PatientMedData.Pharmacy;
            objPatientMed.Quantity = PatientMedData.Quantity;
            objPatientMed.Refills = PatientMedData.Refills;
           // objPatientMed.Route = PatientMedData.Route;
            objPatientMed.Sig = PatientMedData.Sig;
            //objPatientMed.StartDate = Convert.ToDateTime("1900/1/1");
            // objPatientMed.Status = PatientMedData.Status;
            objPatientMed.Dose =  PatientMedData.Dose;
            objPatientMed.DoseUnit = PatientMedData.DoseUnit;
            return objPatientMed;
        }
   
        public PatientMedicationModel ConvertToPatientMedicationModel(DataPatientDocumentService.PatientMedicationData PatientMedData)
        {
            PatientMedicationModel objPatientMed = new PatientMedicationModel();

            objPatientMed.Active = PatientMedData.Active;
            objPatientMed.DateModified = DateTime.Now;
            //objPatientMed.Days = PatientMedData.Days;
            objPatientMed.Diagnosis = PatientMedData.Diagnosis;
            objPatientMed.ExpireDate = PatientMedData.ExpireDate;
            objPatientMed.Frequency = PatientMedData.Frequency;
            objPatientMed.MedicationName = PatientMedData.MedicationName;
           // objPatientMed.NDC = PatientMedData.NDC;
            objPatientMed.Note = PatientMedData.Note;
            objPatientMed.PatientMedicationCntr = PatientMedData.PatientMedicationCntr.ToString();
            objPatientMed.Pharmacy = PatientMedData.Pharmacy;
            objPatientMed.Quantity = PatientMedData.Quantity;
            objPatientMed.Refills = PatientMedData.Refills;
           // objPatientMed.Route = PatientMedData.Route;
            objPatientMed.Sig = PatientMedData.Sig;
            // objPatientMed.Status = PatientMedData.Status;

            return objPatientMed;
        }


        public PatientSocialSelfDataModel ConvertToPatientSocialSelfDataModel(DataPatientDocumentService.PatientSocialSelfData PatientselfData)
        {
            PatientSocialSelfDataModel objPatientMed = new PatientSocialSelfDataModel();
            objPatientMed.PatientId = PatientselfData.PatientId;
            objPatientMed.Birthplace = PatientselfData.Birthplace;
            objPatientMed.Occupation = PatientselfData.Occupation;
            objPatientMed.EducationLevelId = PatientselfData.EducationLevelId;
            objPatientMed.Retired = PatientselfData.Retired;
            objPatientMed.ChildrenSon = PatientselfData.ChildrenSon;
            objPatientMed.ChildrenDaughter = PatientselfData.ChildrenDaughter;
            objPatientMed.CaffeineUser = PatientselfData.CaffeineUser;

            objPatientMed.CaffieneType = PatientselfData.CaffieneType;
            objPatientMed.CaffeineAmount = PatientselfData.CaffeineAmount;
            objPatientMed.ExerciseMember = PatientselfData.ExerciseMember;
            objPatientMed.ExerciseFrequencyId = PatientselfData.ExerciseFrequencyId;
            objPatientMed.ExerciseAmount = PatientselfData.ExerciseAmount;
            objPatientMed.ActivityLevelId = PatientselfData.ActivityLevelId;
            objPatientMed.Activity1 = PatientselfData.Activity1;
            objPatientMed.Activity2 = PatientselfData.Activity2;
            objPatientMed.Activity3 = PatientselfData.Activity3;
            objPatientMed.AlcoholUser = PatientselfData.AlcoholUser;

            objPatientMed.AlcoholFrequencyId = PatientselfData.AlcoholFrequencyId;
            objPatientMed.AlcoholLastUse = PatientselfData.AlcoholLastUse;
            objPatientMed.AlcoholType = PatientselfData.AlcoholType;
            objPatientMed.AlcoholStartAge = PatientselfData.AlcoholStartAge;
            objPatientMed.AlcoholFamilyHist = PatientselfData.AlcoholFamilyHist;
            objPatientMed.SmokingStatusId = PatientselfData.SmokingStatusId;
            objPatientMed.SmokingDailyAmount = PatientselfData.SmokingDailyAmount;
            objPatientMed.SmokingType = PatientselfData.SmokingType;
            objPatientMed.SmokingYears = PatientselfData.SmokingYears;
            objPatientMed.SmokingQuitAttempts = PatientselfData.SmokingQuitAttempts;

            objPatientMed.SmokingQuitDate = PatientselfData.SmokingQuitDate;
            objPatientMed.SmokingSecondHand = PatientselfData.SmokingSecondHand;
            objPatientMed.SmokingStatus = PatientselfData.SmokingStatus;
            // objPatientMed.Status = PatientMedData.Status;

            return objPatientMed;
        }

        public DataPatientDocumentService.PatientPharmacyData ConvertToPatientPharmacyData(PatientPharmacyModel PatientPharmData)
        {
            DataPatientDocumentService.PatientPharmacyData objPatientPharm = new DataPatientDocumentService.PatientPharmacyData();

            objPatientPharm.PharmacyName = PatientPharmData.PharmacyName;
            objPatientPharm.Address1 = PatientPharmData.Address1;
            objPatientPharm.Address2 = PatientPharmData.Address2;
            objPatientPharm.Address3 = PatientPharmData.Address3;
            objPatientPharm.City = PatientPharmData.City;
            objPatientPharm.CountryCode = PatientPharmData.CountryCode;
            objPatientPharm.State = PatientPharmData.State;
            objPatientPharm.PostalCode = PatientPharmData.PostalCode;
            objPatientPharm.Preferred = PatientPharmData.Preferred;
            objPatientPharm.PharmacyCntr = Convert.ToInt64(PatientPharmData.PharmacyCntr);
            objPatientPharm.Phone = PatientPharmData.Phone;

            return objPatientPharm;
        }
        public PatientPharmacyModel ConvertToPatientPharmacyModel(DataPatientDocumentService.PatientPharmacyData PatientPharmData)
        {
            PatientPharmacyModel objPatientPharm = new PatientPharmacyModel();

            objPatientPharm.PharmacyName = PatientPharmData.PharmacyName;
            objPatientPharm.Address1 = PatientPharmData.Address1;
            objPatientPharm.Address2 = PatientPharmData.Address2;
            objPatientPharm.Address3 = PatientPharmData.Address3;
            objPatientPharm.City = PatientPharmData.City;
            objPatientPharm.CountryCode = PatientPharmData.CountryCode;
            objPatientPharm.State = PatientPharmData.State;
            objPatientPharm.PostalCode = PatientPharmData.PostalCode;
            objPatientPharm.Preferred = PatientPharmData.Preferred;
            objPatientPharm.PharmacyCntr = Convert.ToInt64(PatientPharmData.PharmacyCntr);

            return objPatientPharm;
        }

        public DataPatientDocumentService.PatientEmergencyData ConvertToPatientEmergencyData(PatientEmergencyModel PatientEmergencyData)
        {
            DataPatientDocumentService.PatientEmergencyData objPatientEmergency = new DataPatientDocumentService.PatientEmergencyData();

            objPatientEmergency.PatientId = PatientEmergencyData.PatientId;
            objPatientEmergency.PatientEmergencyId = PatientEmergencyData.PatientEmergencyId; //pk 

            objPatientEmergency.Name = PatientEmergencyData.FirstName;

            objPatientEmergency.Address1 = PatientEmergencyData.Address1;
            objPatientEmergency.Address2 = PatientEmergencyData.Address2;
            objPatientEmergency.Address3 = PatientEmergencyData.Address3;

            objPatientEmergency.City = PatientEmergencyData.City;
            objPatientEmergency.State = PatientEmergencyData.State;
            objPatientEmergency.CountryCode = PatientEmergencyData.CountryCode;
            objPatientEmergency.PostalCode = PatientEmergencyData.PostalCode;

            objPatientEmergency.HomePhone = PatientEmergencyData.HomePhone;
            objPatientEmergency.MobilePhone = PatientEmergencyData.MobilePhone;
            PatientEmergencyData.WorkPhone = PatientEmergencyData.WorkPhone;

            objPatientEmergency.Email = Convert.ToString(PatientEmergencyData.Email);
         //   objPatientEmergency.RelationshipId = Convert.ToInt32(PatientEmergencyData.RelationshipId); 
            if (PatientEmergencyData.RelationshipId == -1)
            { objPatientEmergency.RelationshipId = null; }
            else
            {
                objPatientEmergency.RelationshipId = Convert.ToInt32(PatientEmergencyData.RelationshipId);
            }
            objPatientEmergency.IsPrimary = PatientEmergencyData.IsPrimary;
            objPatientEmergency.OnCard = PatientEmergencyData.OnCard;
            

            
            return objPatientEmergency;
        }

        public DataPatientDocumentService.PatientDoctorData ConvertToPatientDoctorData(PatientDoctorModel PatientDoctorData)
        {
            return new DataPatientDocumentService.PatientDoctorData { 
                
                PatientDoctorId = PatientDoctorData.PatientDoctorId,
                DoctorTypeId = PatientDoctorData.DoctorTypeId, //Speciality

                Name = PatientDoctorData.Name,
                
                Address1 = PatientDoctorData.Address1,
                Address2 = PatientDoctorData.Address2,

                City = PatientDoctorData.City,
                State = PatientDoctorData.State,
                PostalCode = PatientDoctorData.PostalCode,
                CountryCode = PatientDoctorData.CountryCode,

                WorkPhone = PatientDoctorData.WorkPhone,
                MobilePhone = PatientDoctorData.MobilePhone,

                Fax = PatientDoctorData.Fax,
                Email = PatientDoctorData.Email,

                IsPrimary = PatientDoctorData.IsPrimary,
                OnCard    = PatientDoctorData.OnCard

            };
        }


        public DataPatientService.PatientOrganData ConvertToPatientOrganData(PatientAndOrganModel PatientOrgan)
        {
            DataPatientService.PatientOrganData objPatientOrgan = new DataPatientService.PatientOrganData();
            
            objPatientOrgan.BoneMarrow = PatientOrgan.BoneMarrow;
            objPatientOrgan.ConnectiveTissue = PatientOrgan.ConnectiveTissue;
            objPatientOrgan.Cornea = PatientOrgan.Cornea;
            objPatientOrgan.Heart = PatientOrgan.Heart;
            objPatientOrgan.HeartValves = PatientOrgan.HeartValves;
            objPatientOrgan.Intestines = PatientOrgan.Intestines;
            objPatientOrgan.Kidneys = PatientOrgan.Kidneys;
            objPatientOrgan.Liver = PatientOrgan.Liver;
            objPatientOrgan.Lungs = PatientOrgan.Lungs;
            objPatientOrgan.Pancreas = PatientOrgan.Pancreas;
            objPatientOrgan.PatientId = PatientOrgan.PatientId;
            objPatientOrgan.Skin = PatientOrgan.Skin;

            return objPatientOrgan;
        }

        public DataPatientService.PatientData ConvertToPatientData(PatientAndOrganModel PatientData)
        {
            DataPatientService.PatientData objPatientSumm = new DataPatientService.PatientData();
            objPatientSumm.HeightFt = PatientData.HeightFt;
            objPatientSumm.HeightIn = PatientData.HeightIn;
            objPatientSumm.Weight = PatientData.Weight;
            objPatientSumm.Comments = PatientData.Comments;
            objPatientSumm.ReligionId = PatientData.ReligionId;
            objPatientSumm.BloodTypeId = PatientData.BloodTypeId;
            objPatientSumm.Eyes = PatientData.EyeColor;
            objPatientSumm.Hair = PatientData.HairColor;
            objPatientSumm.OrganDoner = PatientData.OrganDoner;
            return objPatientSumm;
        }
        public DataPatientService.PatientData ConvertToPatientData(PatientModel PatientSummData)
        {
            DataPatientService.PatientData objPatientSumm = new DataPatientService.PatientData();
            objPatientSumm.FirstName = PatientSummData.FirstName;
            objPatientSumm.LastName = PatientSummData.LastName;
            objPatientSumm.MiddleName = PatientSummData.MiddleName;
            objPatientSumm.Title = PatientSummData.Title;
            objPatientSumm.Suffix = PatientSummData.Suffix;
            objPatientSumm.GenderId = PatientSummData.GenderId;
            objPatientSumm.PreferredLanguageId = PatientSummData.PreferredLanguageId;
            objPatientSumm.EthnicityId = PatientSummData.EthnicityId;
            objPatientSumm.Address1 = PatientSummData.Address1;
            objPatientSumm.Address2 = PatientSummData.Address2;
            objPatientSumm.County = PatientSummData.County;
            objPatientSumm.CountryCode = PatientSummData.CountryCode;
            objPatientSumm.City = PatientSummData.City;
            objPatientSumm.State = PatientSummData.State;
            objPatientSumm.Zip = PatientSummData.Zip;
            objPatientSumm.HomePhone = PatientSummData.HomePhone;
            objPatientSumm.MobilePhone = PatientSummData.MobilePhone;
            objPatientSumm.WorkPhone = PatientSummData.WorkPhone;
            objPatientSumm.EMail = PatientSummData.EMail;
            objPatientSumm.DOB = PatientSummData.DOB;
           // objPatientSumm.RaceId = PatientSummData.RaceId;
            objPatientSumm.Active = PatientSummData.Active;
            objPatientSumm.SmokingStatusId = PatientSummData.SmokingStatusId;
            objPatientSumm.MailAddress1 = PatientSummData.MailAddress1;
            objPatientSumm.MailAddress2 = PatientSummData.MailAddress2;
            objPatientSumm.MailCity = PatientSummData.MailCity;
            objPatientSumm.MailCountryCode = PatientSummData.MailCountryCode;
            objPatientSumm.MailState = PatientSummData.MailState;
            objPatientSumm.MailPostalCode = PatientSummData.MailPostalCode;
            objPatientSumm.RaceId_Asian = PatientSummData.RaceId_Asian;
            objPatientSumm.RaceId_Black = PatientSummData.RaceId_Black;
            objPatientSumm.RaceId_Hawaiian = PatientSummData.RaceId_Hawaiian;
            objPatientSumm.RaceId_Native = PatientSummData.RaceId_Native;
            objPatientSumm.RaceId_NotAnswered = PatientSummData.RaceId_NotAnswered;
            objPatientSumm.RaceId_White = PatientSummData.RaceId_White;
            return objPatientSumm;
        }

        public DataProviderService.ProviderData ConvertToProviderData(ProviderModel ProviderSummData)
        {
            DataProviderService.ProviderData objProviderSumm = new DataProviderService.ProviderData();
            objProviderSumm.FirstName = ProviderSummData.FirstName;
            objProviderSumm.LastName = ProviderSummData.LastName;
            objProviderSumm.MiddleName = ProviderSummData.MiddleName;
            objProviderSumm.DEA = ProviderSummData.DEA;
            objProviderSumm.Email = ProviderSummData.Email;
            objProviderSumm.License = ProviderSummData.License;
            objProviderSumm.Phone = ProviderSummData.Phone;
            objProviderSumm.PracticeId = ProviderSummData.PracticeId;
            objProviderSumm.ProviderId = ProviderSummData.ProviderId;
            objProviderSumm.Title = ProviderSummData.Title;
            objProviderSumm.UserId = ProviderSummData.UserId;
            
            return objProviderSumm;
        }

        public PatientEmergencyModel ConvertToPatientEmergencyModel(DataPatientDocumentService.PatientDocTableData PatientEmergency)
        {
            PatientEmergencyModel objPatientEmergency = new PatientEmergencyModel();
            return objPatientEmergency;
        }

        public PatientVisitCCDModel ConvertPatientVisitCCDModel(DataPatientDocumentService.PatientVisitCCD Visit)
        {
            PatientVisitCCDModel objVisit = new PatientVisitCCDModel();
            objVisit.Address1 = Visit.Address1;
            objVisit.Address2 = Visit.Address2;
            objVisit.City = Visit.City;
            objVisit.CountryCode = Visit.CountryCode;
            objVisit.CountryName = Visit.CountryName;
            objVisit.Email = Visit.Email;
            objVisit.FacilityFax = Visit.FacilityFax;
            objVisit.FacilityId = Visit.FacilityId;
            objVisit.FacilityName = Visit.FacilityName;
            objVisit.FacilityPhone = Visit.FacilityPhone;
            objVisit.FacilityType = Visit.FacilityType;
            objVisit.FacilityTypeId = Visit.FacilityTypeId;
            objVisit.FirstName = Visit.FirstName;
            objVisit.LastName = Visit.LastName;
            objVisit.License = Visit.License;
            objVisit.PatientId = Visit.PatientId;
            objVisit.Phone = Visit.Phone;
            objVisit.PostalCode = Visit.PostalCode;
            objVisit.ProviderId = Visit.ProviderId;
            objVisit.State = Visit.State;
            objVisit.Title = Visit.Title;
            objVisit.VisitId = Visit.VisitId;
            objVisit.VisitReason = Visit.VisitReason;
            objVisit.VisitDate = Visit.VisitDate;
            return objVisit;
        }

        public PatientSummaryModel ConvertToPatientSummaryModel(DataPatientService.PatientSummaryData Patient)
        {
            PatientSummaryModel objPatient = new PatientSummaryModel();

            objPatient.Valid = Patient.Valid;
            objPatient.ErrorMessage = Patient.ErrorMessage;
            objPatient.PatientId = Patient.PatientId;
            objPatient.FirstName = Patient.FirstName;
            objPatient.MiddleName = Patient.MiddleName;
            objPatient.LastName = Patient.LastName;
            objPatient.Title = Patient.Title;
            objPatient.Suffix = Patient.Suffix;
            objPatient.PAddress = Patient.PAddress;
           // objPatient.CityStateZip = Patient.CityStateZip;
            objPatient.DOB = Patient.DOB.ToShortDateString();
            objPatient.HomePhone = Patient.HomePhone;
            objPatient.MobilePhone = Patient.MobilePhone;
            objPatient.WorkPhone = Patient.WorkPhone;
            objPatient.EMail = Patient.EMail;
            objPatient.PreferredLanguage = Patient.PreferredLanguage;
            objPatient.Gender = Patient.Gender;
            objPatient.Race = Patient.Race;
            objPatient.Ethnicity = Patient.Ethnicity;
            objPatient.MaritalStatus = Patient.MaritalStatus;
            objPatient.Religion = Patient.Religion;
            objPatient.BloodType = Patient.BloodType;
            objPatient.HairColor = Patient.HairColor;
            objPatient.EyeColor = Patient.EyeColor;
            objPatient.OrganDoner = Patient.OrganDoner;
            objPatient.SmokingStatus = Patient.SmokingStatus;
            objPatient.EmergencyId = Patient.EmergencyId;
            objPatient.EmergencyName = Patient.EmergencyName;
            objPatient.EmergencyAddress = Patient.EmergencyAddress;
            objPatient.EmergencyCity = Patient.EmergencyCity;
            objPatient.EmergencyCountryCode = Patient.EmergencyCountryCode;
            objPatient.EmergencyCountryName = Patient.EmergencyCountryName;
            objPatient.EmergencyMobilePhone = Patient.EmergencyMobilePhone;
            objPatient.EmergencyHomePhone = Patient.EmergencyHomePhone;
            objPatient.EmergencyRelationshipId = Patient.EmergencyRelationshipId;
            objPatient.EmergencyState = Patient.EmergencyState;
            objPatient.EmergencyWorkPhone = Patient.WorkPhone;
            objPatient.EmergencyZip = Patient.EmergencyZip;
            objPatient.IsPrimary = Patient.IsPrimary;
            objPatient.EmergencyRelationship = Patient.EmergencyRelationship;
            objPatient.PreferredLanguageId = Patient.PreferredLanguageId;
            objPatient.GenderId = Patient.GenderId;
           // objPatient.RaceId = Patient.RaceId;
            objPatient.EthnicityId = Patient.EthnicityId;
            objPatient.SmokingStatusId = Patient.SmokingStatusId;
            objPatient.City = Patient.City;
            objPatient.State = Patient.State;
            objPatient.Zip = Patient.Zip;
            objPatient.CountryCode = Patient.CountryCode;
            objPatient.CountryName = Patient.CountryName;
            objPatient.HeightFt = Convert.ToInt16(Patient.HeightFt);
            objPatient.HeightIn = Convert.ToInt16(Patient.HeightIn);
            objPatient.BloodTypeId = Convert.ToInt16(Patient.BloodTypeId);
            objPatient.ReligionId = Convert.ToInt16(Patient.ReligionId);
            objPatient.Comments = Patient.Comments;
            objPatient.Weight = Convert.ToInt16(Patient.Weight);
            //objPatient.OrganDoner = Patient.OrganDoner;
            objPatient.MailAddress1 = Patient.MailAddress;
            objPatient.MailCity = Patient.MailCity;
            objPatient.MailCountryCode = Patient.MailCountryCode;
            objPatient.MailPostalCode = Patient.MailZip;
            objPatient.MailState = Patient.MailState;
            objPatient.RaceId_Asian = Patient.RaceId_Asian;
            objPatient.RaceId_Black = Patient.RaceId_Black;
            objPatient.RaceId_Hawaiian = Patient.RaceId_Hawaiian;
            objPatient.RaceId_Native = Patient.RaceId_Native;
            objPatient.RaceId_NotAnswered = Patient.RaceId_NotAnswered;
            objPatient.RaceId_White = Patient.RaceId_White;
            objPatient.Race = Patient.Race;
            objPatient.Heart = Patient.Pancreas;
            objPatient.Lungs = Patient.Lungs;
            objPatient.Kidneys = Patient.Kidneys;
            objPatient.Intestines = Patient.Intestines;
            objPatient.Cornea = Patient.Cornea;
            objPatient.Skin = Patient.Skin;
            objPatient.BoneMarrow = Patient.BoneMarrow;
            objPatient.HeartValves = Patient.HeartValves;
            objPatient.ConnectiveTissue = Patient.ConnectiveTissue;
            objPatient.Liver = Patient.Liver;
            objPatient.Pancreas = Patient.Pancreas;
            objPatient.PCP = Patient.PCP;
            return objPatient;
        }



        public PatientVisitDataModel ConvertToPatientVisitDataModel(DataPatientService.PatientVisitData PatientVisit)
        {
            PatientVisitDataModel objPatientVisit = new PatientVisitDataModel();

            objPatientVisit.ErrorMessage = PatientVisit.ErrorMessage;
            objPatientVisit.FacilityId = PatientVisit.FacilityId; ;
            objPatientVisit.PatientId = PatientVisit.PatientId;
            objPatientVisit.ProviderId = PatientVisit.ProviderId;
            objPatientVisit.Valid = PatientVisit.Valid;
            objPatientVisit.VisitDate = PatientVisit.VisitDate;
            objPatientVisit.VisitId = PatientVisit.VisitId;
            objPatientVisit.VistReason = PatientVisit.VistReason;

            return objPatientVisit;

        }


        public PatientRepModel ConvertToPatientRepDataModel(DataPatientService.PatientRepData PatientRep)
        {
            PatientRepModel objPatientRep = new PatientRepModel();

            objPatientRep.ErrorMessage = PatientRep.ErrorMessage;
           
            objPatientRep.PatientId = PatientRep.PatientId;
           
            objPatientRep.Valid = PatientRep.Valid;
            objPatientRep.FirstName = PatientRep.FirstName;
            objPatientRep.LastName = PatientRep.LastName;
            objPatientRep.EMail = PatientRep.EMail;
            objPatientRep.Demographics = PatientRep.Demographics;
            objPatientRep.Allergy = PatientRep.Allergy;
            objPatientRep.FamilyHistory = PatientRep.FamilyHistory;
            objPatientRep.LabResults = PatientRep.LabResults;
            objPatientRep.MedicalHistory = PatientRep.MedicalHistory;
            objPatientRep.Medication = PatientRep.Medication;
            objPatientRep.Problem = PatientRep.Problem;
            objPatientRep.EmergencyContact = PatientRep.EmergencyContact;
            objPatientRep.SocialHistory = PatientRep.SocialHistory;
            objPatientRep.SurgicalHistory = PatientRep.SurgicalHistory;
            objPatientRep.VitalSigns = PatientRep.VitalSigns;
            objPatientRep.Immunization = PatientRep.Immunization;
            objPatientRep.Organ = PatientRep.Organ;
            objPatientRep.ClinicalDoc = PatientRep.ClinicalDoc;
            objPatientRep.Insurance = PatientRep.Insurance;
            objPatientRep.ClinicalSummary = PatientRep.ClinicalSummary ;
            objPatientRep.Appointment = PatientRep.Appointment;
            objPatientRep.Visit = PatientRep.Visit;
            objPatientRep.UploadDocs = PatientRep.UploadDocs;
            objPatientRep.PlanOfCare = PatientRep.PlanOfCare;
            objPatientRep.Messaging = PatientRep.Messaging;
            objPatientRep.DownloadTransmit = PatientRep.DownloadTransmit;
            objPatientRep.Procedure = PatientRep.Procedure;
            objPatientRep.Enabled = PatientRep.Enabled;
            objPatientRep.Provider = PatientRep.Provider;   // SJF Added 1/22/2015
            

            return objPatientRep;

        }

        public PatientOrganModel ConvertToPatientOrganModel(DataPatientService.PatientOrganData PatientOrgan)
        {
            PatientOrganModel objPatientOrgan = new PatientOrganModel();
            objPatientOrgan.BoneMarrow = PatientOrgan.BoneMarrow;
            objPatientOrgan.ConnectiveTissue = PatientOrgan.ConnectiveTissue;
            objPatientOrgan.Cornea = PatientOrgan.Cornea;
            objPatientOrgan.Heart = PatientOrgan.Heart;
            objPatientOrgan.HeartValves = PatientOrgan.HeartValves;
            objPatientOrgan.Intestines = PatientOrgan.Intestines;
            objPatientOrgan.Kidneys = PatientOrgan.Kidneys;
            objPatientOrgan.Liver = PatientOrgan.Liver;
            objPatientOrgan.Lungs = PatientOrgan.Lungs;
            objPatientOrgan.Pancreas = PatientOrgan.Pancreas;
            objPatientOrgan.PatientId = PatientOrgan.PatientId;
            objPatientOrgan.Skin = PatientOrgan.Skin;
            return objPatientOrgan;
        }
        public PatientModel ConvertToPatientModel(DataPatientService.PatientData Patient)
        {
            PatientModel objPatient = new PatientModel();


            objPatient.Valid = Patient.Valid;
            objPatient.ErrorMessage = Patient.ErrorMessage;
            objPatient.PatientId = Patient.PatientId;
            objPatient.FirstName = Patient.FirstName;
            objPatient.MiddleName = Patient.MiddleName;
            objPatient.LastName = Patient.LastName;
            objPatient.Title = Patient.Title;
            objPatient.Address1 = Patient.Address1;
            objPatient.Address2 = Patient.Address2;
            objPatient.Address3 = Patient.Address3;
            objPatient.City = Patient.City;
            objPatient.State = Patient.State;
            objPatient.CommunityName = Patient.CommunityName;
            objPatient.BuildingName = Patient.BuildingName;
            objPatient.County = Patient.County;
            objPatient.Zip = Patient.Zip;
            objPatient.CountryCode = Patient.CountryCode;
            objPatient.MailAddress1 = Patient.MailAddress1;
            objPatient.MailAddress2 = Patient.MailAddress2;
            objPatient.MailAddress3 = Patient.MailAddress3;
            objPatient.MailCity = Patient.MailCity;
            objPatient.MailState = Patient.MailState;
            objPatient.MailPostalCode = Patient.MailPostalCode;
            objPatient.MailCountryCode = Patient.MailCountryCode;
            objPatient.DOB = Patient.DOB;
            objPatient.SSN = Patient.SSN;
            objPatient.HomePhone = Patient.HomePhone;
            objPatient.MobilePhone = Patient.MobilePhone;
            objPatient.WorkPhone = Patient.WorkPhone;
            objPatient.Fax = Patient.Fax;
            objPatient.EMail = Patient.EMail;
            objPatient.PreferredLanguageId = Patient.PreferredLanguageId;
            objPatient.GenderId = Patient.GenderId;
           // objPatient.RaceId = Patient.RaceId;
            objPatient.EthnicityId = Patient.EthnicityId;
            objPatient.MaritalStatusId = Patient.MaritalStatusId;
            objPatient.ReligionId = Patient.ReligionId;
            objPatient.BloodTypeId = Patient.BloodTypeId;
            objPatient.HeightFt = Patient.HeightFt;
            objPatient.HeightIn = Patient.HeightIn;
            objPatient.Eyes = Patient.Eyes;
            objPatient.Hair = Patient.Hair;
            objPatient.Weight = Patient.Weight;
            objPatient.OrganDoner = Patient.OrganDoner;
            objPatient.SmokingStatusId = Patient.SmokingStatusId;
            objPatient.Comments = Patient.Comments;

            return objPatient;
        }
      
        
        #region Medical Portfolio Conversion...
        public DataPatientDocumentService.PatientMedicalDocumentData ConvertToPatientMedicalDocumentData(PatientMedicalDocumentModel model)
        {
            var PatientMedicalDocumentData = new DataPatientDocumentService.PatientMedicalDocumentData()
            {
                DocumentCntr = model.DocumentCntr,
                DocumentDescription = model.DocumentDescription,
                FacilityName = model.FacilityName,
                DoctorName = model.DoctorName,
                Notes = model.Notes,
                Viewable = model.Viewable,

                DocumentImage = model.DocumentImage,
                DocumentId = model.DocumentId,
                DocumentFormat = model.DocumentFormat,
            };

            return PatientMedicalDocumentData;
        }
        public PatientMedicalDocumentModel ConvertToPatientMedicalDocumentModel(DataPatientDocumentService.PatientMedicalDocumentData patientDoc)
        {
            var patientMedicalDocumentModel = new PatientMedicalDocumentModel
            {

                DocumentCntr = patientDoc.DocumentCntr,
                DocumentDescription = patientDoc.DocumentDescription,
                FacilityName = patientDoc.FacilityName,
                DoctorName = patientDoc.DoctorName,
                Notes = patientDoc.Notes,
                Viewable = patientDoc.Viewable,

                DocumentImage = patientDoc.DocumentImage,
                DocumentId = patientDoc.DocumentId,
                DocumentFormat = patientDoc.DocumentFormat,
                FileDirectory = patientDoc.StorageLocation
            };

            return patientMedicalDocumentModel;
        }
        public PatientOutsideDoctorModel ConvertToOutsideDoctorModel(DataPatientDocumentService.PatientCareDocumentData PatientCare)
        {
            var patientCareDocumentModel = new PatientOutsideDoctorModel
            {


                  DocumentCntr = PatientCare.DocumentCntr,
                  DocumentDescription = PatientCare.DocumentDescription,
                  DoctorName = PatientCare.DoctorName,
                  Notes = PatientCare.Notes,
                  Viewable = Convert.ToBoolean(PatientCare.Viewable),

                  DocumentImage = PatientCare.DocumentImage,
                  DocumentId = PatientCare.DocumentId,
                  DocumentFormat = PatientCare.DocumentFormat,
                  FileDirectory  = PatientCare.StorageLocation
            };

              return patientCareDocumentModel;
        }
        #endregion

        #region General Document Conversion...
        public DataPatientDocumentService.PatientDocumentData ConvertToPatientGeneralDocumentData(GeneralDocumentModel model)
        {
            var PatientGeneralDocumentData = new DataPatientDocumentService.PatientDocumentData()
            {
                DocumentCntr = model.DocumentCntr,
                DocumentDescription = model.DocumentDescription,
                Notes = model.Notes,
                Viewable = model.Viewable,
                DocumentImage = model.DocumentImage,
                DocumentId = model.DocumentId,
                DocumentFormat = model.DocumentFormat
            };

            return PatientGeneralDocumentData;
        }
        public GeneralDocumentModel ConvertToPatientGeneralDocumentModel(DataPatientDocumentService.PatientDocumentData model)
        {
            var PatientGeneralDocumentModel = new GeneralDocumentModel {
                DocumentCntr        = model.DocumentCntr,
                DocumentDescription = model.DocumentDescription,
                Notes = model.Notes,
                Viewable = model.Viewable,
                DocumentImage = model.DocumentImage,
                DocumentId = model.DocumentId,
                DocumentFormat = model.DocumentFormat,
                FileDirectory = model.StorageLocation
            };

            return PatientGeneralDocumentModel;
        }
        public GeneralDocumentModel ConvertToPatientClinicalDocumentModel(DataPatientDocumentService.PatientClinicalDocumentData model)
        {
            var PatientGeneralDocumentModel = new GeneralDocumentModel
            {
                DocumentCntr = model.DocumentCntr,
                DocumentDescription = model.DocumentDescription,
                Notes = model.Notes,
                Viewable = model.Viewable,
                DocumentImage = model.DocumentImage,
                DocumentId = model.DocumentId,
                DocumentFormat = model.DocumentFormat,
                FileDirectory = model.StorageLocation
            };

            return PatientGeneralDocumentModel;
        }
        public DataPatientDocumentService.PatientPolicyData ConvertToInsurancePolicyData(InsurancePolicyModel model)
        {
            var InsurancePolicyData = new DataPatientDocumentService.PatientPolicyData()
            {
                PatientPolicyId = model.PatientPolicyId,
                PolicyTypeId   = model.PolicyTypeId,
                PolicyTypeName = model.PolicyTypeName,
                Company        = model.Company,
                PolicyNo       = model.PolicyNo,
                GroupNumber    = model.GroupNumber,
                PlanNumber     = model.PlanNumber,
                Agent          = model.Agent,
                AgentPhone     = model.AgentPhone,
                AgentFax       = model.AgentFax,
                Notes          = model.Notes
            };

            return InsurancePolicyData;
        }
        public DataPatientDocumentService.PatientAdvisorData ConvertToProfessionalAdvisorData(ProfessionalAdvisorModel model)
        {
            var ProfessionalAdvisorData = new DataPatientDocumentService.PatientAdvisorData()
            {
                PatientAdvisorId = model.PatientAdvisorId,
                AdvisorId   = model.AdvisorId,
                AdvisorDesc = model.AdvisorDesc,
                Name        = model.Name,
                ContactAgent = model.ContactAgent,
                Address1    = model.Address1,
                Address2    = model.Address2,
                Address3    = model.Address3,
                City        = model.City,
                State       = model.State,
                PostalCode  = model.PostalCode,
                CountryCode = model.CountryCode,
                WorkPhone   = model.WorkPhone,
                MobilePhone = model.MobilePhone,
                Fax         = model.Fax,
                EMail       = model.EMail,
                Notes       = model.Notes
            };

            return ProfessionalAdvisorData;
        }
        #endregion
        [ChildActionOnly]
        [OutputCache(Duration=3600, VaryByParam = "none")]
        public HomeViewModel MasterLayout()
        {
            HomeViewModel HomeList = new HomeViewModel();
            using (var service = new DataPatientService.PatientWSSoapClient())
            {
                
            
            DataPatientService.PatientTableData ParentData = new DataPatientService.PatientTableData();
            DataPatientService.PatientParms patparm = new DataPatientService.PatientParms();
            patparm.PatientId = Convert.ToInt64(RequestHelper.MyGlobalVar.PatientId);
            ParentData = service.GetPatientAccountLinkParentList(patparm, RequestHelper.MyGlobalVar.Token, RequestHelper.MyGlobalVar.UserId, RequestHelper.MyGlobalVar.FacilityId);
            if (ParentData.Valid)
            {
                HomeList.patientaccount = ParentData.dt.ToParentAccountLinkModel();

            }

            }
            return HomeList;
        }

        public PatientShareModel ConvertToPatientShareModel(DataPatientService.PatientShareData PatShare)
        {
            PatientShareModel Share = new PatientShareModel();
            Share.Allergy = PatShare.Allergy;
            Share.Appointment = PatShare.Appointment;
            Share.ClinicalDoc = PatShare.ClinicalDoc;
            Share.Demographics = PatShare.Demographics;
            Share.EmergencyContact = PatShare.EmergencyContact;
            Share.FamilyHistory = PatShare.FamilyHistory;
            Share.Immunization = PatShare.Immunization;
            Share.Insurance = PatShare.Insurance;
            Share.LabResults = PatShare.LabResults;
            Share.MedicalHistory = PatShare.MedicalHistory;
            Share.Medication = PatShare.Medication;
            Share.Organ = PatShare.Organ;
            Share.PlanOfCare = PatShare.PlanOfCare;
            Share.Problem = PatShare.Problem;
            Share.Procedure = PatShare.Procedure;
            Share.SocialHistory = PatShare.SocialHistory;
            Share.SurgicalHistory = PatShare.SurgicalHistory;
            Share.UploadDocs = PatShare.UploadDocs;
            Share.Visit = PatShare.Visit;
            Share.VitalSigns = PatShare.VitalSigns;

            return Share;

        }

    }

}
