using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AMR.Models
{
    public class ProviderInfoModel
    {
        public string Name { get; set; }
        public long FacilityID { get; set; }
        public Int64 VisitId { get; set; }
        public string FacilityName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityStateZip { get; set; }
        public string CountryCode { get; set; }
        public string OfficePhone { get; set; }
        public string OfficeFax { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
    }

    public class PatientSearchModel
    {
        public Int64 PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
        public string EMail { get; set; }
        public Nullable<System.Int32> EMRSystemId { get; set; }
        public Nullable<System.DateTime> dob { get; set; }
        public Nullable<System.Int32> FacilityId { get; set; }
        public Nullable<System.Int32> PageNumber { get; set; }
        public Nullable<System.Int32> PageSize { get; set; }
        public Nullable<System.Int64> TotalRecords { get; set; }
       // public Nullable<System.Int64> TotalCount { get; set; }
    }

    public class ProviderSearchModel {
        public Int64 ProviderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string EMail { get; set; }
        public string License { get; set; }
        public Nullable<System.Int32> EMRSystemId { get; set; }
        public Nullable<System.Int32> FacilityId { get; set;  }
        public Nullable<System.Int32> PageNumber { get; set; }
        public Nullable<System.Int32> PageSize { get; set; }
        public Nullable<System.Int64> TotalRecords { get; set; }

     
    }
    // SJF Added 7/2/14
    // AHS Added Address2 12/29/2014
    public class PatientAdminModel
    {
        public Int64 PatientId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CityStateZip { get; set; }
        public string EMail { get; set; }
        public bool PremiumFlag { get; set; }
        public string PremiumExpireDate { get; set; }
        public bool Active { get; set; }
        public int ThirdPartyId { get; set; }
        public string ThirdPartyName { get; set; }
        public int EMRSystemId { get; set; }
        public string EMRSystemName { get; set; }
        public string RepresentativePwd { get; set; }
        public bool ClientNote { get; set; }
        public string Practices { get; set; }
    }

    public class ProviderAdminModel {
        public Int64 ProviderId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string CityStateZip { get; set; }
        public string EMail { get; set; }
        public bool PremiumFlag { get; set; }
        public string PremiumExpireDate { get; set; }
        public bool Active { get; set; }
        public int ThirdPartyId { get; set; }
        public string ThirdPartyName { get; set; }
        public int EMRSystemId { get; set; }
        public string EMRSystemName { get; set; }
        public string RepresentativePwd { get; set; }
        public bool ClientNote { get; set; }
        public string Practices { get; set; }
    }

    public class PatientWebSettingModelString
    {
        public List<string> widgetname = new List<string>();
        public Int16 PatientWebSettingId;
    }

    public class PatientWebSettingDataModel
    {
    public bool EmailNotifyNewMessage{get;set;}
    public bool TextNotifyNewMesssage { get; set; }
    public Int32 CellCarrier {get;set;}
    public string Email { get; set; }
    public string CellPhoneNumber { get; set; }
    

    }
    public class PatientShareModel {
        public bool Demographics { get; set; }
        public bool Allergy { get; set; }
        public bool FamilyHistory { get; set; }
        public bool LabResults { get; set; }
        public bool MedicalHistory { get; set; }
        public bool Medication { get; set; }
        public bool Problem { get; set; }
        public bool Procedure { get; set; }
        public bool SocialHistory { get; set; }
        public bool SurgicalHistory { get; set; }
        public bool VitalSigns { get; set; }
        public bool Immunization { get; set; }
        public bool Organ { get; set; }
        public bool ClinicalDoc { get; set; }
        public bool Insurance { get; set; }
        public bool EmergencyContact { get; set; }
        public bool Appointment { get; set; }
        public bool Visit { get; set; }
        public bool UploadDocs { get; set; }
        public bool PlanOfCare { get; set; }
        public bool ClinicalInstructions { get; set; }
    }
    public class FacilityModel
    {
        public long FacilityID { get; set; }
        public string FacilityName { get; set; }
    }
    public class FacilityVisitSelectModel
    {
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
    }
    public class ConsolidateCallModel
    {
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        public Int64 facilityId { get; set; }
        public Int64 visitId { get; set; }
    }
    public class CareProviderModel
    {
        public string Password { get; set; }
        public Int64 UserId { get; set; }
        public Int64 PatientId { get; set; }
        public string PatientFullName { get; set; }
        public DateTime DOB { get; set; }
        public string ErrorMessage { get; set; }
        public string EmailAddress { get; set; }
        
    }

    public class CarrierModel
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
    }

    public class MessageUnreadModel
    {
        public Int32 AppointmentMessages { get; set; }
        public Int32 MedicationMessages { get; set; }
        public Int32 ReferralMessages { get; set; }
        public Int32 GeneralMessages { get; set; }
        public Int32 TotalMessages { get; set; }
        public Boolean MessageRead { get; set; }
    }

    public class PatientCareDocumentDataModel
    {
        public Int64 PatientId { get; set; }
        public Int64 DocumentCntr { get; set; }
        public string DocumentDescription { get; set; }
        public string DoctorName { get; set; }
        public string DocumentId { get; set; }
        public string DocumentFormat { get; set; }
        public string StorageLocation { get; set; }
        public string Notes { get; set; }
        public bool Viewable { get; set; }
        public byte[] DocumentImage { get; set; }
    }
    public class PatientRepModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 UserId { get; set; }
        public Int64 PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public bool Demographics { get; set; }
        public bool Allergy { get; set; }
        public bool FamilyHistory { get; set; }
        public bool LabResults { get; set; }
        public bool MedicalHistory { get; set; }
        public bool Medication { get; set; }
        public bool Problem { get; set; }
        public bool Procedure { get; set; }
        public bool SocialHistory { get; set; }
        public bool SurgicalHistory { get; set; }
        public bool VitalSigns { get; set; }
        public bool Immunization { get; set; }
        public bool Organ { get; set; }
        public bool ClinicalDoc { get; set; }
        public bool Insurance { get; set; }
        public bool EmergencyContact { get; set; }
        public bool Appointment { get; set; }
        public bool Visit { get; set; }
        public bool UploadDocs { get; set; }
        public bool PlanOfCare { get; set; }
        public bool Messaging { get; set; }
        public bool DownloadTransmit { get; set; }
        public bool ClinicalSummary { get; set; }
        public bool Enabled { get; set; }
        public bool edit { get; set; }
        public bool loginFlag { get; set; }
        public bool ClinicalInstructions { get; set; }
        public bool Provider { get; set; }
        
      
    }

    public class QueryStringValues
    {
        public Int32 Flag;
       
    }

    public class PatientCCD
    { 
     public bool  Problems { get; set; }
     public bool Allergies{ get; set; }
     public bool Immunizations{ get; set; }
     public bool Medications{ get; set; }
     public bool Labs{ get; set; }
     public bool ClinicalInstructions{ get; set; }
     public bool FutureAppointments{ get; set; }
     public bool Referrals{ get; set; }
     public bool ScheduledTests{ get; set; }
     public bool DecisionAids { get; set; }
     public Int32 VisitID { get; set; }
     public Int32 FacilityID { get; set; }
     public string EmailID { get; set; }
     public bool SocialHistory { get; set; }
     public bool VitalSigns { get; set; }
     public bool Procedures { get; set; }
     public bool View { get; set; }
     public bool Download { get; set; }
     public bool Demographics { get; set; }
     public bool Provider { get; set; }
     public bool Visit { get; set; }
     public bool Customize { get; set; }
     public bool CCDHTML { get; set; }
     public bool CCDXML { get; set; }
    }

    public class PatientImageData
    {
        public bool Valid;
        public string ErrorMessage;
        public Int64 PatientId;
        public byte[] Image;
        public string ImageFormat;
    }
    public class SecurityQuestionModel
        {
        public Int64 SecurityQuestionId{ get; set; }
        public string SecurityAnswer { get; set; }
        public Int64 SecurityQuestionId2 { get; set; }
        public string SecurityAnswer2 { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        
    }
    public class UserModel 
    {
        public long UserId { get; set; }

        public string UserLogin { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public Int32 UserRoleId {get; set;}

        public string RoleName { get; set; }

        public string ErrorMessage { get; set; }
        public string SecurityAnswer { get; set; }
        public string SecurityAnswer2 { get; set; }
    }

    public class POCModel
    {
         public long PlanCntr { get; set; }
         public string CodeValue { get; set; }
         public string CodeSystem { get; set; }
         public short InstructionTypeId { get; set; }
         public string InstructionType { get; set; }
         public string Instruction { get; set; }
         public string Goal { get; set; }
         public string Note { get; set; }
         public DateTime? AppointmentDateTime { get; set; }
         public long ProviderId { get; set; }
         public Int64 FacilityId { get; set; }
         public Int64 PatientId { get; set; }
         public Int64 VisitId { get; set; }
         public string facilityOptions { get; set; }
         public string visitOptions { get; set; }
         public string facilitySelected { get; set; }
         public string visitSelected { get; set; }
         public string flag { get; set; }
         public string ExtensionToggleFunct { get; set; }
         public string ExtensionFilterFunct { get; set; }
         public string ExtensionIdName { get; set; }
         
    }

    public class ProcedureModel
    {
        public long PatProcedureCntr { get; set; }
        public int PatientId { get; set; }
        public string Description { get; set; }

        public string CodeValue { get; set; }
        public string CodeSystem { get; set; }

        public int CodeSystemId { get; set; }

        public string Diagnosis { get; set; }

        public string PerformedBy { get; set; }

        public string ServiceLocation { get; set; }

        public string ServiceDate { get; set; }

        public string Note { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public Int64 FacilityId { get; set; }
   
        public Int64 VisitId { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        public string flag { get; set; }
        public string ExtensionToggleFunct { get; set; }
        public string ExtensionFilterFunct { get; set; }
        public string ExtensionIdName { get; set; }
 
   }


    public class AllergyModel
    {                                 
        public long PatientAllergyCntr { get; set; }                                     
        public string CodeValue_Allergen { get; set; }
        public int CodeSystemId_Allergen { get; set; }
        public string Allergen { get; set; }
        public string AllergenType { get; set; }
         public string CodeValue_Reaction { get; set; }
        public int CodeSystemId_Reaction { get; set; }
        public string Reaction { get; set; }
        public string EffectiveDate { get; set; }
        public short ConditionStatusId { get; set; }
        public string ConditionStatus { get; set; }
        public string Note { get; set; }
        public bool OnCard { get; set; }
        public bool OnKeys { get; set; }
         public DateTime DateModified { get; set; }
         public string Severity { get; set; }
         public Int64 FacilityId { get; set; }
         public Int64 PatientId { get; set; }
         public Int64 VisitId { get; set; }
         public string Flag { get; set; }
         public string facilityOptions { get; set; }
         public string visitOptions { get; set; }
         public string facilitySelected { get; set; }
         public string visitSelected { get; set; }
    }

    public class GenderModel
    {
        public Int64 GenderID { get; set; }
        public string Value { get; set; }
    }

    public class MedicationListModel
    {
        public Int64 RxNormId { get; set; }
        public string Description { get; set; }


    }

    public class ProblemSNOMEDModel
    {
        public Int64 ProblemID { get; set; }
        public string Value { get; set; }
    }
    public class RelationshipModel
    {
        public Int64 RelationShipID { get; set; }
        
        public string Value { get; set; }
        public string SNOMED { get; set; }
    }

    public class PreferredLanguageModel
    {
        public Int64 PreferredLanguageId { get; set; }
        public string Value { get; set; }
    }
    public class EthnicityModel
    {
        public Int64 EthnicityId { get; set; }
        public string Value { get; set; }

    }


    public class CountryModel
    {

        public string CountryId { get; set; }
        public string Name { get; set; }
    }

    public class ConditionStatusModel
    {
        public Int32 ConditionStatusId { get; set; }
        public string Value { get; set; }
    }
    public class StatesModel
    {
        public string StateId { get; set; }
        public string Name { get; set; }
    }

    public class RaceModel
    {
        public Int64 RaceId { get; set; }
        public string Value { get; set; }
    }

    public class SmokingStatusModel
    {
        public Int64 SmokingStatusId { get; set; }
        public string Value { get; set; }
    }
    public class ProviderModel
    {
        public Int64 ProviderId { get; set; }
        public Int64 PracticeId { get; set; }
        public Int64 UserId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DEA { get; set; }
        public string License { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
    public class VisitModel
    {
        public long VisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }
        public string FacilityCityStatePostal { get; set; }
        public string ProviderName { get; set; }
        public string VisitReason { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 ProviderId { get; set; }
        public string Flag { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }

    }
    public class PatientVitalSignModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 PatientVitalCntr { get; set; }
        public Int64 FacilityId { get; set; }
        public DateTime VitalDate { get; set; }
        public int HeightIn { get; set; }
        public int WeightLb { get; set; }
        public string BloodPressurePosn { get; set; }
        public int Systolic { get; set; }
        public int Diastolic { get; set; }
        public int Pulse { get; set; }
        public int Respiration { get; set; }
        public string Flag { get; set; }
        public decimal Temperature { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
           }
    public class FamilyHistoryModel
    {
        public long PatFamilyHistCntr { get; set; }
        public string FamilyMember { get; set; }
        public string Description { get; set; }
        public string Qualifier { get; set; }
        public string CodeValue { get; set; }
        public string SNOMEDCode { get; set; }
        public int CodeSystemId { get; set; }
        public short ConditionStatusId { get; set; }
        public string DateReported { get; set; }
        public bool Diseased { get; set; }
        public string Note { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 RelationshipId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public Int32 AgeAtOnset { get; set; }//added by maaz
        public Int32 DiseasedAge { get; set; }//added by maaz
        public string Relationship { get; set; }
        public string CodeSystem { get; set; }
        public string Flag { get; set; }

        
    }

    public class VaccineModel
    {
        public Int64 CVX_Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class PatientMessageModel
    {
        public String DeletedMessageIds { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 MessageId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 MessageDetailId {get; set;}
        public String FacilityName { get; set; }
        public String MessageTypeId { get; set; }
        public String MessageType { get; set; }
        public String MessageStatusId { get; set; }
        public String MessageStatus { get; set; }
        public String MedicationName { get; set; }
        public String MessageRequest { get; set; }
       
        public String PharmacyName { get; set; }
        public String PreferredPeriod { get; set; }
        public String PreferredTime { get; set; }
        public String PreferredWeekDay { get; set; }
        public String MessageUrgencyId { get; set; }
        public Boolean MessageUrgency { get; set; }
        public String ProviderId_From { get; set; }
        public String ProviderFrom { get; set; }
        public String MessageResponseType { get; set; }
        public String ProviderAppointment { get; set; }
        public String ProviderName_From { get; set; }
        public String ProviderId_To { get; set; }
        public String ProviderTo { get; set; }
        public String ProviderName_To { get; set; }
        public String VisitReason { get; set; }
        public String UserId_Created { get; set; }
        public String CreatedByName { get; set; }
        public String MessageResponse { get; set; }
        public String MessageResponseTypeId { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public String ProviderId_Appointment { get; set; }
        public String MedicationNDC { get; set; }
        public String NoOfRefills { get; set; }
        public String MedicationStatus { get; set; }
        public String PharmacyAddress { get; set; }
        public String AttachmentId { get; set; }
        public String DocumentFormat { get; set; }
        public Int64 VisitId { get; set; }
        public Boolean MessageRead { get; set; }
        public Int64 MessageUnRead { get; set; }
        public DateTime DateCreated { get; set; }
        public string PharmacyPhone { get; set; }
        public DateTime ProviderApprDate { get; set; }
        public String ProviderApprTime { get; set; }
        public String PatientName { get; set; }
        public Nullable<long> MessageAttachmentId { get; set; }
    }

    public class ProviderMessageModel
    {
        public String PatientName { get; set; }
        public long MessageId { get; set; }
        public Nullable<long> MessageDetailId { get; set; }
        public Nullable<long> PatientId { get; set; }
        public Nullable<int> MessageTypeId { get; set; }
        public string MessageType { get; set; }
        public Nullable<long> FacilityId { get; set; }
        public string FacilityName { get; set; }
        public Nullable<int> MessageStatusId { get; set; }
        public string MessageStatus { get; set; }
        public Nullable<long> Expr1 { get; set; }
        public Nullable<long> ProviderId_To { get; set; }
        public string ProviderTo { get; set; }
        public Nullable<long> ProviderId_From { get; set; }
        public string ProviderFrom { get; set; }
        public string MessageRequest { get; set; }
        public string MessageResponse { get; set; }
        public Nullable<int> MessageResponseTypeId { get; set; }
        public string MessageResponseType { get; set; }
        public string PreferredPeriod { get; set; }
        public string PreferredTime { get; set; }
        public string PreferredWeekDay { get; set; }
        public string VisitReason { get; set; }
        public Nullable<bool> MessageUrgency { get; set; }
        public Nullable<System.DateTime> AppointmentStart { get; set; }
        public Nullable<System.DateTime> AppointmentEnd { get; set; }
        public Nullable<long> ProviderId_Appointment { get; set; }
        public string ProviderAppointment { get; set; }
        public string MedicationNDC { get; set; }
        public string MedicationName { get; set; }
        public Nullable<int> NoOfRefills { get; set; }
        public Nullable<int> MedicationStatus { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyAddress { get; set; }
        public string PharmacyPhone { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> MessageRead { get; set; }
        public Nullable<long> MessageAttachmentId { get; set; }
        public string DocumentFormat { get; set; }
    }
    public class MessageTypeModel
    {
        public Int32 MessageTypeId { get; set; }
        public String Value { get; set; }
    }

    public class MessageAttachmentModel
    {
      public long MessageAttachmentId {get; set;}
      public long MessageId {get; set;}
      public long PatientId {get; set;}
      public string FileDirectory {get; set;}
      public string FileName {get; set;}
      public string DocumentFormat {get; set;}
      public byte[] DocumentImage {get; set;}   
    }

    public class MessageStatusModel
    {
        public Int64 MessageStatusId { get; set; }
        public string Value { get; set; }
    }
    public class MessageModel
    {
        public HttpPostedFileBase file { get; set; }
        public string AttachmentTest { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; } 
        public Int64 MessageId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public int MessageTypeId { get; set; }
        public string MessageType { get; set; }
        public int MessageStatusId { get; set; }
        public Int64 MessageDetailId { get; set; }
        public Int64 ProviderId_To { get; set; }
        public Int64 ProviderId_From { get; set; }
        public string MessageRequest { get; set; }
        public string MessageResponse { get; set; }
        public Int32 MessageResponseTypeId { get; set; }
        public String PreferredPeriod { get; set; }
        public string PreferredTime { get; set; }
        public string PreferredWeekDay { get; set; }
        public string VisitReason { get; set; }
        public Boolean MessageUrgency { get; set; }
        public DateTime AppointmentStart { get; set; }
        public DateTime AppointmentEnd { get; set; }
        public Int64 ProviderId_Appointment { get; set; }
        public string MedicationNDC { get; set; }
        public string MedicationName { get; set; }
        public int NoOfRefills { get; set; }
        public int MedicationStatus { get; set; }
        public string PharmacyName { get; set; }
        public string PharmacyAddress { get; set; }
        public string AttachmentId { get; set; }
        public string ProviderName { get; set; }
        public string Flag { get; set; }
        public string CreatedByName { get; set; }
        public string status { get; set; }
        public string PharmacyPhone { get; set; }

    }

      public class MessageUrgency 
        {
         public int MessageUrgencyId { get;set; }
         public string Value { get;set; }   
        }


    public class PatientMedicationModel
    {
        public string PatientMedicationCntr { get; set; }
        public string CodeValue { get; set; }
        public string NDC { get; set; }
        public string MedicationName { get; set; }
        public bool Active { get; set; }
        public int Quantity { get; set; }
        public int Days { get; set; }
        public string Route { get; set; }
        public int Refills { get; set; }
        public string Frequency { get; set; }
        public string Sig { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool duringvisit { get; set; }
        
        public string Pharmacy { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public bool Current { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        public Int32 Dose { get; set; }
        public string DoseUnit { get; set; }
    }

    public class PatientPharmacyModel
    {
        public Int64 PatientId { get; set; }
        public Int64 PharmacyCntr { get; set; }
        public string PharmacyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public bool Preferred { get; set; }
        public string Note { get; set; }
        public string Flag { get; set; }
        public string Phone { get; set; }
    }

    public class SocialHistoryModel
    {
        
        public long PatSocialHistCntr { get; set; }
        public string Description { get; set; }
        public string Qualifier { get; set; }
        public string CodeValue { get; set; }
        public long CodeSystemId { get; set; }
        public string CodeSystem { get; set; }
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public string Note { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public string Flag { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        
    }

    public class SocialModel {
        public Int64 SNOMED_Social { get; set; }
        public string Value { get; set; }

    }

    public class MedicalHistoryModel
    {
        public long PatMedicalHistCntr { get; set; }
        public string Description { get; set; }
        public string DateOfOccurance { get; set; }
        public string Note { get; set; }
        public bool OnCard { get; set; }
        public bool OnKeys { get; set; }
        public DateTime DateModified { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public string Flag { get; set; }
        
    }

    public class LabResultModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 LabResultCntr { get; set; }
        public Int64 FacilityId { get; set; }
        public string FacilityName { get; set; }
        public Int64 ProviderId_Requested { get; set; }
        public string ProviderName { get; set; }
        public string LabName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? CollectionDate { get; set; }
        public string Requisiton { get; set; }
        public string Specimen { get; set; }
        public string SpecimenSource { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Reviewer { get; set; }
        public string Flag { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        public string CodeValue { get; set; }
        public string CodeSystemId { get; set; }
        public string Result { get; set; }
        public string RefRange { get; set; }
        public string Abnormal { get; set; }
          }

    public class LabResultTestModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 ProviderId { get; set; }
        public Int64 LabResultCntr { get; set; } 
        public string Component { get; set; }
        public string Result { get; set; }
        public string RefRange { get; set; }
        public string Units { get; set; }
        public string AbNormal { get; set; }
        public string ResultStatus { get; set; }
        public Int64 TestCntr { get; set; }
        public string  CodeValue { get; set; }
        public Int64 CodeSystemId { get; set; }
    }




    public class PatientVisitDataModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public Int64 ProviderId { get; set; }
        public string VistReason { get; set; }
        public string ProviderName { get; set; }
        public string Note { get; set; }
        public string ExtensionToggleFunct { get; set; }
        public string ExtensionFilterFunct { get; set; }
        public string ExtensionIdName { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
    }

    public class ProblemModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 PatientProblemCntr { get; set; }
        public Int64 FacilityId { get; set; }
        public string CodeValue { get; set; }
        public int CodeSystemId { get; set; }
        public string Condition { get; set; }
        public string EffectiveDate { get; set; }
        public string LastChangeDate { get; set; }
        public Int16 ConditionStatusId { get; set; }
        public string ConditionStatus { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public string Flag { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
    }

    public class ImmunizationModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 PatientImmunizationCntr { get; set; }
        public Int64 FacilityId { get; set; }
        public string Time { get; set; }
        public string ImmunizationDate { get; set; }
        public string Vaccine { get; set; }
        public string CodeValue { get; set; }
        public Int64 CodeSystemId { get; set; }
        public string Amount { get; set; }
        public string Route { get; set; }
        public string Site { get; set; }
        public string Sequence { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string LotNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Note { get; set; }
        public string Flag { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
    }

    //public class AllergyModel
    //{
    //    public Int64 PatientId { get; set; }
    //    public Int64 VisitId { get; set; }
    //    public Int64 PatientAllergyCntr { get; set; }
    //    public Int64 FacilityId { get; set; }
    //    public string CodeValue_Allergen { get; set; }
    //    public Int16 CodeSystemId_Allergen { get; set; }
    //    public string Allergen { get; set; }
    //    public string AllergenType { get; set; }
    //    public string CodeValue_Reaction { get; set; }
    //    public Int16 CodeSystemId_Reaction { get; set; }
    //    public string Reaction { get; set; }
    //    public string EffectiveDate { get; set; }
    //    public short ConditionStatusId { get; set; }
    //    public string Note { get; set; }
    //    public bool OnCard { get; set; }
    //    public bool OnKeys { get; set; }
    //    public DateTime DateModified { get; set; }
    //}

    public class PatientListModel
    {
        public Int64 PatientId { get; set; }
        public string Name { get; set; }
    }

    public class PatientModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Suffix { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CommunityName { get; set; }
        public string BuildingName { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailAddress3 { get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailPostalCode { get; set; }
        public string MailCountryCode { get; set; }
        public DateTime DOB { get; set; }
        public string SSN { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public bool Active { get; set; }
        public int PreferredLanguageId { get; set; }
        public int GenderId { get; set; }
        public int RaceId { get; set; }
        public int EthnicityId { get; set; }
        public int MaritalStatusId { get; set; }
        public int ReligionId { get; set; }
        public int BloodTypeId { get; set; }
        public int HeightFt { get; set; }
        public int HeightIn { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public int Weight { get; set; }
        public bool OrganDoner { get; set; }
        public int SmokingStatusId { get; set; }
        public string Comments { get; set; }
        public bool RaceId_NotAnswered {get; set;}
        public bool RaceId_Native {get; set;}
        public bool RaceId_Asian {get; set;}
        public bool RaceId_Black {get; set;}
        public bool RaceId_Hawaiian {get; set;}
        public bool RaceId_White { get; set; }
        public string PromoCode { get; set; }
    }

    public class BloodTypeModel
    {
        public Int32 BloodTypeId { get; set; }
        public string Value { get; set; }
    }

    public class ReligionModel
    {
        public Int32 ReligionId { get; set; }
        public string Value { get; set; }
    }

    public class PatientVisitCCDModel
    {
        public Int64 PatientId { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 VisitId { get; set; }
        public Int64 ProviderId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int FacilityTypeId { get; set; }
        public string FacilityType { get; set; }
        public string FacilityName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string FacilityPhone { get; set; }
        public string FacilityFax { get; set; }
        public string VisitReason { get; set; }
        public DateTime VisitDate { get; set; }
        }

    public class PatientSummaryModel
    {
        public bool Valid { get; set; }
        public string ErrorMessage { get; set; }
        public Int64 PatientId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string PAddress { get; set; }
        public string CityStateZip { get; set; }
        public string DOB { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string EMail { get; set; }
        public string PreferredLanguage { get; set; }
        public string Gender { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string BloodType { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string OrganDoner { get; set; }
        public string SmokingStatus { get; set; }

        public Int64 EmergencyId { get; set; }
        public string EmergencyName { get; set; }
        public string EmergencyAddress { get; set; }
        public string EmergencyCity { get; set; }
        public string EmergencyState { get; set; }
        public string EmergencyZip { get; set; }
        public string EmergencyCountryCode { get; set; }
        public string EmergencyCountryName { get; set; }
        public string EmergencyHomePhone { get; set; }
        public string EmergencyMobilePhone { get; set; }
        public string EmergencyWorkPhone { get; set; }
        public string EmergencyRelationship { get; set; }
        public Int64 EmergencyRelationshipId { get; set; }
        public bool IsPrimary { get; set; }
        public string PCP { get; set; }

        public string MailAddress1 { get; set; }
        public string MailAddress2 { get; set; }
        public string MailAddress3 { get; set; }
        public string MailCity { get; set; }
        public string MailState { get; set; }
        public string MailPostalCode { get; set; }
        public string MailCountryCode { get; set; }

        public Int16 HeightFt { get; set; }
        public Int16 HeightIn { get; set; }
        public Int16 Weight { get; set; }
        public string Comments { get; set; }
        public Int16 ReligionId { get; set; }
        public Int16 BloodTypeId { get; set; }


        public Int64 PreferredLanguageId { get; set; }
        public Int64 GenderId { get; set; }
        public Int64 RaceId { get; set; }
        public Int64 EthnicityId { get; set; }
        public Int64 SmokingStatusId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public bool RaceId_NotAnswered { get; set; }
        public bool RaceId_Native { get; set; }
        public bool RaceId_Asian { get; set; }
        public bool RaceId_Black { get; set; }
        public bool RaceId_Hawaiian { get; set; }
        public bool RaceId_White { get; set; }
        public bool Heart { get; set; }
        public bool Lungs { get; set; }
        public bool Kidneys { get; set; }
        public bool Intestines { get; set; } 
        public bool Cornea { get; set; }
        public bool Skin { get; set; }
        public bool BoneMarrow { get; set; }
        public bool HeartValves { get; set; }
        public bool ConnectiveTissue { get; set; }  
        public bool Liver { get; set; }
        public bool Pancreas { get; set; }
    }

    public class PatientEmergency
    {
        public Int64 PatientId { get; set; }
        public Int64 PatientEmergencyId {get; set;}
        public string Name {get; set;}
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
        public int RelationshipId { get; set; }
        public bool OnCard { get; set; }
        public string Relationship { get; set; }
    }

    public class DoctorUploadModel
     {
        public Int64 DocumentCntr  { get; set; }
        public string DocumentDescription  { get; set; }
        public string  DoctorName  { get; set; }
        public DateTime DateCreated  { get; set; }
        public string Notes { get; set; }
        public string DocumentFormat { get; set; }
    }

    public class PatientUploadModel
    {
        public Int64   DocumentCntr  { get; set; }
        public string   DocumentDescription  { get; set; }
        public string   FacilityName  { get; set; }
        public string   DoctorName  { get; set; }
        public DateTime DateCreated   { get; set; }
        public string DocumentFormat { get; set; }
        public string Notes { get; set; }
    }

     public class PatientPolicyModel
     {
         public Int64 PatientPolicyId {get; set;}
         public Int32 PolicyTypeId {get; set;}
         public string PolicyTypeName  {get; set;}
         public string Company {get; set;}
         public string PolicyNo {get; set;}
         public string PlanNumber {get; set;}
         public string GroupNumber { get; set; }
         public string Agent {get; set;}
         public string AgentPhone {get; set;}
         public string AgentFax {get; set;}
         public string Notes {get; set;}
     }

     public class PatientInsuranceModel
     {
         public Int64 PatientInsuranceId{ get ; set;}
         public String PlanName{ get ; set;}
         public String MemberNumber{ get ; set;}
         public String GroupNumber{ get ; set;}
         public String Subscriber{ get ; set;}
         public String Relationship{ get ; set;}
         public DateTime EffectiveDate{ get ; set;}
         public Int64 VisitId { get; set; }
         public Int64 FacilityId { get; set; }
     }
    /*
    public class PatientMedicalDocumentModel
      {
         public Int64 DocumentCntr {get; set;}
         public string DocumentDescription{get; set;}
         public DateTime DateCreated {get; set;}
         public string Notes {get; set;}
         public bool Viewable {get; set;}
    }
    */


    public class PatientOrganModel
    {
        public Int64 PatientId { get; set; }
        public bool Heart { get; set; }
        public bool Liver { get; set; }
        public bool Pancreas { get; set; }
        public bool Lungs { get; set; }
        public bool Kidneys { get; set; }
        public bool Intestines { get; set; }
        public bool Cornea { get; set; }
        public bool Skin { get; set; }
        public bool BoneMarrow { get; set; }
        public bool HeartValves { get; set; }
        public bool ConnectiveTissue { get; set; }

    }

    public class PatientAndOrganModel
    {
        public Int16 HeightFt { get; set; }
        public Int16 HeightIn { get; set; }
        public Int16 Weight { get; set; }
        public string Comments { get; set; }
        public Int16 ReligionId { get; set; }
        public Int16 BloodTypeId { get; set; }

        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public bool OrganDoner { get; set; }

        public Int64 PatientId { get; set; }
        public bool Heart { get; set; }
        public bool Liver { get; set; }
        public bool Pancreas { get; set; }
        public bool Lungs { get; set; }
        public bool Kidneys { get; set; }
        public bool Intestines { get; set; }
        public bool Cornea { get; set; }
        public bool Skin { get; set; }
        public bool BoneMarrow { get; set; }
        public bool HeartValves { get; set; }
        public bool ConnectiveTissue { get; set; }
    }
    public class ReportModel
    {
        public string Id { get; set; }
        public Int64 FacilityId { get; set;}
        public string Year { get; set; }
        public string QYear { get; set;}
        public string Quarter { get; set;}
        public string  FromDate { get; set;}
        public string ToDate { get; set; }
        public string MU { get; set; }
        public string ReportPath { get; set; }
            
    }
    public class AdminReportModel
    {
        public string ThirdParty { get; set; }
        public string AccountType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string MU { get; set; }
        public string ReportPath { get; set; }

    }
    #region Patient Emergency Model...
    public class PatientEmergencyModel
    {
        public Int64 PatientId { get; set; }
        public Int64 PatientEmergencyId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public bool IsPrimary { get; set; }
        public bool OnCard { get; set; }
        public Int64 RelationshipId { get; set; }

    }
    #endregion

    #region Patient Doctor Models...
    public class PatientDoctorModel
    {
        public long PatientDoctorId { get; set; }
        public int DoctorTypeId { get; set; }
        public string DoctorTypeDesc { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool IsPrimary { get; set; }
        public bool OnCard { get; set; }
    }
    public class DoctorSpecialityModel
    {
        public Int64 DoctorSpecialityId { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region Medical Portfolio Models...

    public class MedicalPortfolioBaseModel
    {
        public long PatientId { get; set; }
        public long DocumentCntr { get; set; }
        
        public DateTime DateCreated { get; set; }
        public string DoctorName { get; set; }

        public string DocumentDescription { get; set; }
        public string Notes { get; set; }
        public bool Viewable { get; set; }

        public string DocumentId { get; set; } //fileName
        public string DocumentFormat { get; set; }
        public byte[] DocumentImage { get; set; }
        public DateTime VisitDate { get; set; }
        public long VisitId { get; set; }
        public string FileDirectory { get; set; }
        public string FacilityName { get; set; }
        public long FacilityId { get; set; }
        public bool Share { get; set; }
    }

    public class PatientVisitModel 
    {
        public long PatientId { get; set; }
        public long FacilityId { get; set; }
        public string FacilityName { get; set; }
        public long VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public long ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string VisitReason { get; set; }
        public bool Viewable { get; set; }

    }
    public class PatientOutsideDoctorModel : MedicalPortfolioBaseModel
    {
       
    }
    public class PatientMedicalDocumentModel : MedicalPortfolioBaseModel
    {
        public string FacilityName { get; set; } //Only Difference...
    }
    #endregion 

    #region General Document Model...
    public class GeneralDocumentModel
    {
        public long DocumentCntr { get; set; }
        public DateTime DateCreated { get; set; }
        public string DocumentDescription { get; set; }
        public string Notes { get; set; }
        public bool Viewable { get; set; }

        public string DocumentId { get; set; } //fileName
        public string DocumentFormat { get; set; }
        public byte[] DocumentImage { get; set; }
        public string FileDirectory { get; set; }
    }
    #endregion 

    #region Insurance Policy Model...
    public class InsurancePolicyModel
    {
        
        public long PatientPolicyId { get; set; }
        public int PolicyTypeId { get; set; }
        public string Value { get; set; }
        public string PolicyTypeName { get; set; }
        public string Company { get; set; }
        public string PolicyNo { get; set; }
        public string GroupNumber { get; set; }
        public string PlanNumber { get; set; }
        public string Agent { get; set; }
        public string AgentPhone { get; set; }
        public string AgentFax { get; set; }
        public string Notes { get; set; }
        public string facilityOptions { get; set; }
        public string visitOptions { get; set; }
        public string facilitySelected { get; set; }
        public string visitSelected { get; set; }
        public string flag { get; set; }
        public string ExtensionToggleFunct { get; set; }
        public string ExtensionFilterFunct { get; set; }
        public string ExtensionIdName { get; set; }
        public Int64 FacilityId { get; set; }
        public Int64 PatientId { get; set; }
        public Int64 VisitId { get; set; }
       
    }
    public class PolicyTypeModel
    {
        public short PolicyTypeId { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region Professional Advisor Model...
    public class ProfessionalAdvisorModel
    {
        public long   PatientAdvisorId { get; set; }
        public int    AdvisorId { get; set; }
        public string Value { get; set; }
        public string AdvisorDesc { get; set; }
        public string Name { get; set; }
        public string ContactAgent { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string EMail { get; set; }
        public string Notes { get; set; }
    }
    public class AdvisorTypeModel
    {
        public short AdvisorTypeId { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region Home Widgets Model
    public class HomeWidgetModel 
    {
        public string Id { get; set; }
        public string WidgetHtml { get; set; }
    }
    #endregion


    public class CCDActivityLogModel
    {
        public long AuditId;
        public long PatientId;
        public string Method;
        public long? UserId;
        public string Name;
        public DateTime? TDStamp;

    
    }
    public  class PatientAccountLinkModel
    {
    public long PatientId { get;set;}
     public long PatientId_Linked { get;set;}
     public DateTime DateApproved { get; set; }
     public bool linkflag { get; set; }
     public string PatientName { get; set; }
     }

    public class BillPaymentModel
    {
        public long PatientId { get; set; }
        public long BillRateId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Response { get; set; }
        public string CustId { get; set; }
        public string RecurrId { get; set; }
        public Char PaymentType { get; set; }
        public string PaymentFrequency { get; set; }
        public string PaymentId { get; set; }
        public long NoOfPayments { get; set; }
        public DateTime StartDate { get; set; }
        public string AccountHolderName { get; set; }
        public string  Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public long UserId_Created { get; set; }
        public DateTime UserId_Modified { get; set; }

    }
    public class InstructionType
        {
        public Int64 InstructionTypeId { get; set; }
        public string Value { get; set; }

        }
    public class ExerciseFrequencyModel
    {
        public Int64 ExerciseFrequencyId { get; set; }
        public string Value { get; set; }

    }
    public class ActivityLevelModel
    {
        public Int64 ActivityLevelId { get; set; }
        public string Value { get; set; }
    }
    public class AlcoholFrequencyModel
    {

        public Int64 AlcoholFrequencyId { get; set; }
        public string Value { get; set; }
    }
    public class EducationLevelModel
    {
        public Int64 EducationLevelId { get; set;}
        public string Value { get; set; }
    }
    public class MartialStatusModel
    {
        public Int64 MaritalStatusId { get; set; }
        public string Value { get; set; }
    }
    public class PatientSocialSelfDataModel
    {

        public Int64 PatientId { get; set; }
        public string Birthplace { get; set; }
        public Int16 EducationLevelId { get; set; }
        public string Occupation { get; set; }
        public bool Retired { get; set; }
        public Int16 ChildrenSon { get; set; }
        public Int16 ChildrenDaughter { get; set; }
        public bool CaffeineUser { get; set; }
        public string CaffieneType { get; set; }
        public string CaffeineAmount { get; set; }
        public bool ExerciseMember { get; set; }
        public Int16 ExerciseFrequencyId { get; set; }
        public string ExerciseAmount { get; set; }
        public Int16 ActivityLevelId { get; set; }
        public string Activity1 { get; set; }
        public string Activity2 { get; set; }
        public string Activity3 { get; set; }
        public bool AlcoholUser { get; set; }
        public Int16 AlcoholFrequencyId { get; set; }
        public DateTime AlcoholLastUse { get; set; }
        public string AlcoholType { get; set; }
        public Int16 AlcoholStartAge { get; set; }
        public bool AlcoholFamilyHist { get; set; }
        public Int32 SmokingStatusId { get; set; }
        public Int16 SmokingDailyAmount { get; set; }
        public string SmokingType { get; set; }
        public Int16 SmokingYears { get; set; }
        public Int16 SmokingQuitAttempts { get; set; }
        public DateTime SmokingQuitDate { get; set; }
        public bool SmokingSecondHand { get; set; }
        public string SmokingStatus { get; set; }
    
    }
    public class ThirdPartyModel  // SJF 07/07/14
    {
        public Int64 ThirdPartyId { get; set; }
        public string Value { get; set; }
    }


    public class PatientNotesModel
    {

        public Int64 PatientId {get;set;}
        public DateTime DateCreated {get;set;}
        public string Note {get;set;}
        
    }

    public class RoutesOfAdministrationModel
    {
        public string RouteId { get; set; }
        public string Value { get; set; }

    }
    public class PracticeDataModel
    {

        public Int64 PracticeId { get; set; }
        public Int64 OrganizationId { get; set; }
        public string PracticeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    
    }

    public class OrangizationModel
    {

        public Int64 OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Address1{ get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
    }

    public class FacilityPracticeModel
    {

        public Int64 PracticeId { get; set; }
        public Int64 OrganizationId { get; set; }
        public string PracticeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string OrganizationName { get; set; }
    }
    public class FacilitySetupPracticeModel
    {
        public Int64 FacilityId { get; set; }
        public Int64 PracticeId { get; set; }
        public int EMRSystemId { get; set; }
        public int FacilityTypeId { get; set; }
        public string FacilityName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string PracticeName { get; set; }
        public string Comment { get; set; }
    }
    public class DocumentModelForAdmin
    {
        public Int64 FacilityId { get; set; }
        public Int64 PracticeId { get; set; }
        public Int64 DocumentCntr { get;set;}
        public Int64 PatientId { get; set; }
        public string DocType { get; set; }
        public Int64 VisitId { get; set; }
        public string DocumentDescription { get; set; }
    }
    public class PartnerModelForAdmin
    { 
    
    public Int64 EMRSystemId {get;set;}
    public string Value { get; set; }
    
    
    
    }


}
