// Service Name  : PatientService
// Date Created  : 10/26/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Patient Information
// MM/DD/YYYY XXX Description              
// 11/01/2013 SJF Added Patient Organ & Visit
// 11/15/2013 SJF Added Patient Summary View
// 11/16/2013 SJF Added Patient Share
// 11/18/2013 SJF Added Account Link
// 11/19/2013 SJF Added Patinet Web Settings
// 11/20/2013 SJF Added Patient Picture
// 12/13/2013 SJF Added Patient Representative
// 12/31/2013 SJF Added Organs to Patient Summary View
// 01/08/2014 SJF Added GetPatientFacilityList
// 02/06/2014 SJF Added PatientCareProvider
// 02/06/2014 SJF Added Visit Viewable/Share
// 03/12/2014 SJF Added BillRate & BillPayment
// 03/20/2014 SJF Added Patient Activate/Deactivate
// 06/17/2014 SJF Added PatientSearch & PatientEmailSearch
// 06/17/2014 SJF Added ChangePatientRenewDate
// 06/26/2014 SJF Added Third Party, EMRSystem to Patient
// 11/04/2014 SJF Added Delete Patient
// 12/03/2014 SJF Added EMRSystemId to Patient Search
// 12/03/2014 SJF Added Delete Patient Visit ,GetPartnerCodes
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using AMR.Data;

namespace AMR.DataService
{
    /// <summary>
    /// Summary description for PatientService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "PatientWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PatientService : System.Web.Services.WebService
    {

        #region Static Variables
        //------------------------------------------------------------------------
        // Define Static Variables
        //------------------------------------------------------------------------

        //static Int16 ActionInsert = 1;
        //static Int16 ActionUpdate = 2;
        static Int16 ActionRead = 3;
        static Int16 DocTypePatients = 0;
        static Int16 DocTypePatientOrgan = 12;
        #endregion

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct PatientData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string Title;
            public string Suffix;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string CommunityName;
            public string BuildingName;
            public string County;
            public string Zip;
            public string CountryCode;
            public string MailAddress1;
            public string MailAddress2;
            public string MailAddress3;
            public string MailCity;
            public string MailState;
            public string MailPostalCode;
            public string MailCountryCode;
            public DateTime DOB;
            public string SSN;
            public string HomePhone;
            public string MobilePhone;
            public string WorkPhone;
            public string Fax;
            public string EMail;
            public int PreferredLanguageId;
            public int GenderId;
            public bool RaceId_NotAnswered;
            public bool RaceId_Native;
            public bool RaceId_Asian;
            public bool RaceId_Black;
            public bool RaceId_Hawaiian;
            public bool RaceId_White;
            public int EthnicityId;
            public int MaritalStatusId;
            public int ReligionId;
            public int BloodTypeId;
            public int HeightFt;
            public int HeightIn;
            public string Eyes;
            public string Hair;
            public int Weight;
            public bool OrganDoner;
            public int SmokingStatusId;
            public string Comments;
            public bool PremiumFlag;
            public string PremiumStartDate;
            public string PremiumExpireDate;
            public int Option;
            public bool Active;
            public string SmokingDate;
            public int ThirdPartyId;
            public int EMRSystemId;
        }
        public struct PatientParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 FacilityId;
        }

        public struct PatientResp
        {
            public bool Valid;
            public string ErrorMessage;
        }
        public struct PatientSearchParms
        {
            public Int64 PatientId;
            public string FirstName;
            public string LastName;
            public string Address1;
            public string City;
            public string State;
            public string County;
            public string Zip;
            public string CountryCode;
            public string HomePhone;
            public string EMail;
            public Nullable<System.DateTime> DOB;
            public Nullable<System.Int32> EMRSystemId;
            public Nullable<System.Int32> PageNumber; //added by Talha
            public Nullable<System.Int32> PageSize;//added by Talha
            public Nullable<System.Int64> TotalRecord;//added by Talha
            public Nullable<System.Int64> FacilityId;
        }
        public struct PatientTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
            public Int64 count;//added by Talha
        }

        public struct PatientOrganData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public bool Heart;
            public bool Liver;
            public bool Pancreas;
            public bool Lungs;
            public bool Kidneys;
            public bool Intestines;
            public bool Cornea;
            public bool Skin;
            public bool BoneMarrow;
            public bool HeartValves;
            public bool ConnectiveTissue;
        }

        public struct PatientVisitData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public DateTime VisitDate;
            public Int64 ProviderId;
            public string VistReason;
            public bool Viewable;
        }

        public struct PatientVisitParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public bool Share;
        }
        public struct PatientVisitTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct PatientFacilityTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct PatientSummaryData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public string Title;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string Suffix;
            public string PAddress;
            public string City;
            public string State;
            public string Zip;
            public string CountryCode;
            public string CountryName;
            public int HeightFt;
            public int HeightIn;
            public int Weight;
            public string Comments;
            public DateTime DOB;
            public string HomePhone;
            public string MobilePhone;
            public string WorkPhone;
            public string EMail;
            public int PreferredLanguageId;
            public string PreferredLanguage;
            public int GenderId;
            public string Gender;
            public bool RaceId_NotAnswered;
            public bool RaceId_Native;
            public bool RaceId_Asian;
            public bool RaceId_Black;
            public bool RaceId_Hawaiian;
            public bool RaceId_White;
            public string Race;
            public int EthnicityId;
            public string Ethnicity;
            public int MaritalStatusId;
            public string MaritalStatus;
            public int ReligionId;
            public string Religion;
            public int BloodTypeId;
            public string BloodType;
            public string EyeColor;
            public string HairColor;
            public string OrganDoner; 
            public int SmokingStatusId;
            public string SmokingStatus;

            public int RelationshipId;

            public Int64 EmergencyId;

            public string EmergencyName;
            public string EmergencyAddress;
            public string EmergencyCity;
            public string EmergencyState;
            public string EmergencyZip;
            public string EmergencyCountryCode;
            public string EmergencyCountryName;
            public string EmergencyHomePhone;
            public string EmergencyMobilePhone;
            public string EmergencyWorkPhone;
            public int EmergencyRelationshipId;
            public string EmergencyRelationship;
            public bool IsPrimary;
            public string PCP;
            public string MailAddress;
            public string MailCity;
            public string MailState;
            public string MailZip;
            public string MailCountryCode;
            public string MailCountryName;

            public bool Heart;
            public bool Liver;
            public bool Pancreas;
            public bool Lungs;
            public bool Kidneys;
            public bool Intestines;
            public bool Cornea;
            public bool Skin;
            public bool BoneMarrow;
            public bool HeartValves;
            public bool ConnectiveTissue;
        }

        public struct PatientShareData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public bool Demographics;
            public bool Allergy;
            public bool FamilyHistory;
            public bool LabResults;
            public bool MedicalHistory;
            public bool Medication;
            public bool Problem;
            public bool Procedure;
            public bool SocialHistory;
            public bool SurgicalHistory;
            public bool VitalSigns;
            public bool Immunization;
            public bool Organ;
            public bool ClinicalDoc;
            public bool Insurance;
            public bool EmergencyContact;
            public bool Appointment;
            public bool Visit;
            public bool UploadDocs;
            public bool PlanOfCare;
            public bool ClinicalInstructions;
        }
        public struct PatientShareResp
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct PatientAccountLinkData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PatientId_Linked;
            DateTime DateApproved;
        }

        public struct PatientWebSettingData
        {
            public bool Valid;
            public Int64 Option;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int16 VisitWidgetLeft;
            public Int16 VisitWidgetRight;
            public Int16 AllergyWidgetLeft;
            public Int16 AllergyWidgetRight;
            public Int16 AppointmentsWidgetLeft;
            public Int16 AppointmentsWidgetRight;
            public Int16 ClinicalDocWidgetLeft;
            public Int16 ClinicalDocWidgetRight;
            public Int16 FamilyWidgetLeft;
            public Int16 FamilyWidgetRight;
            public Int16 ImmunizationWidgetLeft;
            public Int16 ImmunizationWidgetRight;
            public Int16 InsuranceWidgetLeft;
            public Int16 InsuranceWidgetRight;
            public Int16 LabTestWidgetLeft;
            public Int16 LabTestWidgetRight;
            public Int16 MedicationWidgetLeft;
            public Int16 MedicationWidgetRight;
            public Int16 PastMedicalWidgetLeft;
            public Int16 PastMedicalWidgetRight;
            public Int16 ProblemWidgetLeft;
            public Int16 ProblemWidgetRight;
            public Int16 ProcedureWidgetLeft;
            public Int16 ProcedureWidgetRight;
            public Int16 SocialWidgetLeft;
            public Int16 SocialWidgetRight;
            public Int16 SurgicalWidgetLeft;
            public Int16 SurgicalWidgetRight;
            public Int16 VitalSignsWidgetLeft;
            public Int16 VitalSignsWidgetRight;
            public Int16 PremiumWidgetLeft;
            public Int16 PremiumWidgetRight;
            public Int16 StatementWidgetLeft;
            public Int16 StatementWidgetRight;
            public Int16 DocumentWidgetLeft;
            public Int16 DocumentWidgetRight;
            public Int16 PlanOfCareWidgetLeft;
            public Int16 PlanOfCareWidgetRight;
            public Int16 ClinicalInstructionsWidgetLeft;
            public Int16 ClinicalInstructionsWidgetRight;
            public Int16 ProviderWidgetLeft;
            public Int16 ProviderWidgetRight;
            public bool EmailNotifyNewMessage;
            public bool TextNotifyNewMesssage;
            public int CellCarrier;
        }

        public struct PatientImageData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public byte[] Image;
            public string ImageFormat;
        }

        public struct PatientRepData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 UserId;
            public Int64 PatientId;
            public string FirstName;
            public string LastName;
            public string EMail;
            public bool Enabled;
            public bool Demographics;
            public bool Allergy;
            public bool FamilyHistory;
            public bool LabResults;
            public bool MedicalHistory;
            public bool Medication;
            public bool Problem;
            public bool Procedure;
            public bool SocialHistory;
            public bool SurgicalHistory;
            public bool VitalSigns;
            public bool Immunization;
            public bool Organ;
            public bool ClinicalDoc;
            public bool Insurance;
            public bool EmergencyContact;
            public bool Appointment;
            public bool Visit;
            public bool UploadDocs;
            public bool PlanOfCare;
            public bool Messaging;
            public bool DownloadTransmit;
            public bool ClinicalSummary;
            public bool Provider;
        }

        public struct CareProviderData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 UserId;
            public Int64 PatientId;
            public string Password;
            public string PatientFullName;
        }
        public struct CareProviderEmail
        {
            public Int64 UserId;
            public Int64 PatientId;
            public string PatientName;
            public string Password;
            public string EmailAddress;
        }
        public struct BillRateData
        {
            public bool Valid;
            public string ErrorMessage;
            public int BillRateId;
            public string PromoCode;
            public bool Active;
            public bool IsFree;
            public decimal Rate;
            public decimal Renewal;
            public decimal Residual;
            public int FreeMonths;
            public Int64 UserId_Created;
            public string DateCreated;
            public Int64 UserId_Modified;
            public string DateModified;
        }
        public struct BillRateParms
        {
            public int Option;
            public int BillRateId;
            public string PromoCode;
        }
        public struct BillRateTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }
        public struct BillPaymentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 BillPaymentId;
            public Int64 PatientId;
            public int BillRateId;
            public string TransactionDate;
            public string Response;
            public string CustId;
            public string RecurrId;
            public string PaymentType;
            public string PaymentFrequency;
            public string PaymentId;
            public int NoOfPayments;
            public string StartDate;
            public string AccountHolderName;
            public decimal Amount;
            public Int64 UserId_Created;
            public string DateCreated;
            public Int64 UserId_Modified;
            public string DateModified;
        }
        public struct BillPaymentParms
        {
            public int Option;
            public Int64 BillPaymentId;
            public Int64 PatientId;
        }
        public struct BillPaymentTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }
        public struct PatientFacilityOptionsData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public bool GeneralMessageAvailable;
            public bool AppointmentMessageAvailable;
            public bool MedicationMessageAvailable;
            public bool ReferralMessageAvailable;
            public bool PremiumAvailable;
            public bool OnlineBillPayment;
        }
        public struct PatientAdmin
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public string Name;
            public string Password;
            public string Address;
            public string Address2;
            public string CityStateZip;
            public string EMail;
            public bool PremiumFlag;
            public string PremiumExpireDate;
            public bool Active;
            public int ThirdPartyId;
            public string ThirdPartyName;
            public int EMRSystemId;
            public string EMRSystemName;
            public string RepresentativePwd;
        }

        public struct PatientPartnerTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }
        #endregion

        #region Get Patient Data
        //------------------------------------------------------------------------
        // Get Patient Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Data")]
        public PatientData GetPatientData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientData Patient = new PatientData();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient results = db.Patients.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            Patient.PatientId = results.PatientId;
                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                            Patient.PatientId = results.PatientId;
                            Patient.FirstName = results.FirstName;
                            Patient.MiddleName = results.MiddleName;
                            Patient.LastName = results.LastName;
                            Patient.Title = results.Title;
                            Patient.Suffix = results.Suffix;
                            Patient.Address1 = results.Address1;
                            Patient.Address2 = results.Address2;
                            Patient.Address3 = results.Address3;
                            Patient.City = results.City;
                            Patient.State = results.State;
                            Patient.CommunityName = results.CommunityName;
                            Patient.BuildingName = results.BuildingName;
                            Patient.County = results.County;
                            Patient.Zip = results.Zip;
                            Patient.CountryCode = results.CountryCode;
                            Patient.MailAddress1 = results.MailAddress1;
                            Patient.MailAddress2 = results.MailAddress2;
                            Patient.MailAddress3 = results.MailAddress3;
                            Patient.MailCity = results.MailCity;
                            Patient.MailState = results.MailState;
                            Patient.MailPostalCode = results.MailPostalCode;
                            Patient.MailCountryCode = results.MailCountryCode;
                            Patient.DOB = Convert.ToDateTime(results.DOB);
                            Patient.SSN = results.SSN;
                            Patient.HomePhone = results.HomePhone;
                            Patient.MobilePhone = results.MobilePhone;
                            Patient.WorkPhone = results.WorkPhone;
                            Patient.Fax = results.Fax;
                            Patient.EMail = results.EMail;
                            Patient.PreferredLanguageId = Convert.ToInt16(results.PreferredLanguageId);
                            Patient.GenderId = Convert.ToInt16(results.GenderId);
                            Patient.RaceId_NotAnswered = Convert.ToBoolean(results.RaceId_NotAnswer);
                            Patient.RaceId_Native = Convert.ToBoolean(results.RaceId_Native);
                            Patient.RaceId_Asian = Convert.ToBoolean(results.RaceId_Asian);
                            Patient.RaceId_Black = Convert.ToBoolean(results.RaceId_Black);
                            Patient.RaceId_Hawaiian = Convert.ToBoolean(results.RaceId_Hawaiian);
                            Patient.RaceId_White = Convert.ToBoolean(results.RaceId_White);
                            Patient.EthnicityId = Convert.ToInt16(results.EthnicityId);
                            Patient.MaritalStatusId = Convert.ToInt16(results.MaritalStatusId);
                            Patient.ReligionId = Convert.ToInt16(results.ReligionId);
                            Patient.BloodTypeId = Convert.ToInt16(results.BloodTypeId);
                            Patient.HeightFt = Convert.ToInt16(results.HeightFt);
                            Patient.HeightIn = Convert.ToInt16(results.HeightIn);
                            Patient.Eyes = results.Eyes;
                            Patient.Hair = results.Hair;
                            Patient.Weight = Convert.ToInt16(results.Weight);
                            Patient.OrganDoner = Convert.ToBoolean(results.OrganDoner);
                            Patient.Comments = results.Comments;
                            Patient.PremiumFlag = Convert.ToBoolean(results.PremiumFlag);
                            Patient.PremiumStartDate = (results.PremiumStartDate!=null?results.PremiumStartDate.ToString():"");
                            Patient.PremiumExpireDate = (results.PremiumExpireDate!=null?results.PremiumExpireDate.ToString():"");
                            Patient.SmokingDate = (results.SmokingDate != null ? results.SmokingDate.ToString() : "");
                            Patient.Active = Convert.ToBoolean(results.Active);
                            Patient.ThirdPartyId = Convert.ToInt32(results.ThirdPartyId);
                            Patient.EMRSystemId = Convert.ToInt32(results.EMRSystemId);
                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatients, UserId, ActionRead);


                        }
                        else
                        {
                            Patient.Valid = false;
                            Patient.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion

        #region Get Patient Exists
        //------------------------------------------------------------------------
        // Get Patient Exists 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Exists")]
        public PatientData GetPatientExists(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientData Patient = new PatientData();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient results = db.Patients.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            Patient.PatientId = results.PatientId;
                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                        }
                        else
                        {
                            Patient.Valid = false;
                            Patient.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion

        #region Get Patient Summary Data
        //------------------------------------------------------------------------
        // Get Patient Summary Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Summary Data")]
        public PatientSummaryData GetPatientSummaryData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSummaryData Patient = new PatientSummaryData();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        vwPatientSummary results = db.vwPatientSummaries.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                            Patient.PatientId = results.PatientId;
                            Patient.FirstName = results.FirstName;
                            Patient.MiddleName = results.MiddleName;
                            Patient.LastName = results.LastName;
                            Patient.Title = results.Title;
                            Patient.Suffix = results.Suffix;
                            Patient.PAddress = results.PAddress;
                            Patient.City = results.City;
                            Patient.State = results.State;
                            Patient.Zip = results.Zip;
                            Patient.CountryCode = results.CountryCode;
                            Patient.CountryName = results.CountryName;
                            Patient.HeightFt = Convert.ToInt16(results.HeightFt);
                            Patient.HeightIn = Convert.ToInt16(results.HeightIn);
                            Patient.Weight = Convert.ToInt16(results.Weight);
                            Patient.Comments = results.Comments;
                            Patient.DOB = Convert.ToDateTime(results.DOB);
                            Patient.HomePhone = results.HomePhone;
                            Patient.MobilePhone = results.MobilePhone;
                            Patient.WorkPhone = results.WorkPhone;
                            Patient.EMail = results.EMail;
                            Patient.PreferredLanguageId = Convert.ToInt16(results.PreferredLanguageId);
                            Patient.PreferredLanguage = results.PreferredLanguage;
                            Patient.GenderId = Convert.ToInt16(results.GenderId);
                            Patient.Gender = results.Gender;
                            Patient.RaceId_NotAnswered = Convert.ToBoolean(results.RaceId_NotAnswer);
                            Patient.RaceId_Native = Convert.ToBoolean(results.RaceId_Native);
                            Patient.RaceId_Asian = Convert.ToBoolean(results.RaceId_Asian);
                            Patient.RaceId_Black = Convert.ToBoolean(results.RaceId_Black);
                            Patient.RaceId_Hawaiian = Convert.ToBoolean(results.RaceId_Hawaiian);
                            Patient.RaceId_White = Convert.ToBoolean(results.RaceId_White);
                            Patient.Race = results.Race;
                            Patient.EthnicityId = Convert.ToInt16(results.EthnicityId);
                            Patient.Ethnicity = results.Ethnicity;
                            Patient.MaritalStatusId = Convert.ToInt16(results.MaritalStatusId);
                            Patient.MaritalStatus = results.MaritalStatus;
                            Patient.ReligionId = Convert.ToInt16(results.ReligionId);
                            Patient.Religion = results.Religion;
                            Patient.BloodTypeId = Convert.ToInt16(results.BloodTypeId);
                            Patient.BloodType = results.BloodType;
                            Patient.EyeColor = results.EyeColor;
                            Patient.HairColor = results.HairColor;
                            Patient.OrganDoner = results.OrganDoner;
                            Patient.SmokingStatusId = Convert.ToInt16(results.SmokingStatusId);
                            Patient.SmokingStatus = results.SmokingStatus;
                            Patient.RelationshipId = Convert.ToInt16(results.RelationshipId);
                            Patient.EmergencyId = Convert.ToInt64(results.PatientEmergencyId);
                            Patient.EmergencyName = results.EmergencyName;
                            Patient.EmergencyAddress = results.EmergencyAddress;
                            Patient.EmergencyCity = results.EmergencyCity;
                            Patient.EmergencyState = results.EmergencyState;
                            Patient.EmergencyZip = results.EmergencyZip;
                            Patient.EmergencyCountryCode = results.EmergencyCountryCode;
                            Patient.EmergencyCountryName = results.EmergencyCountryName;
                            Patient.EmergencyHomePhone = results.EmergencyHomePhone;
                            Patient.EmergencyMobilePhone = results.EmergencyMobilePhone;
                            Patient.EmergencyWorkPhone = results.EmergencyWorkPhone;
                            Patient.EmergencyName = results.EmergencyName;
                            Patient.EmergencyName = results.EmergencyName;
                            Patient.EmergencyRelationship = results.EmergencyRelationship;
                            Patient.EmergencyRelationshipId = Convert.ToInt32(results.EmergencyRelationshipId);
                            Patient.IsPrimary = Convert.ToBoolean(results.IsPrimary);
                            Patient.PCP = results.PCP;
                            Patient.MailAddress = results.MailAddress;
                            Patient.MailCity = results.MailCity;
                            Patient.MailState = results.MailState;
                            Patient.MailZip = results.MailZip;
                            Patient.MailCountryCode = results.MailCountryCode;
                            Patient.MailCountryName = results.MailCountryName;

                            Patient.Heart = Convert.ToBoolean(results.Pancreas);
                            Patient.Lungs = Convert.ToBoolean(results.Lungs);
                            Patient.Kidneys = Convert.ToBoolean(results.Kidneys);
                            Patient.Intestines = Convert.ToBoolean(results.Intestines);
                            Patient.Cornea = Convert.ToBoolean(results.Cornea);
                            Patient.Skin = Convert.ToBoolean(results.Skin);
                            Patient.BoneMarrow = Convert.ToBoolean(results.BoneMarrow);
                            Patient.HeartValves = Convert.ToBoolean(results.HeartValves);
                            Patient.ConnectiveTissue = Convert.ToBoolean(results.ConnectiveTissue);
                            Patient.PCP = Convert.ToString(results.PCP);
                            //Added By Talha
                            Patient.Liver = Convert.ToBoolean(results.Liver);
                            Patient.Pancreas = Convert.ToBoolean(results.Pancreas);


                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatients, UserId, ActionRead);

                        }
                        else
                        {
                            Patient.Valid = false;
                            Patient.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion

        #region Get Patient Admin Data
        //------------------------------------------------------------------------
        // Get Patient Admin Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Admin Data")]
        public PatientAdmin GetPatientAdminData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientAdmin Patient = new PatientAdmin();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient results = db.Patients.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            Patient.PatientId = results.PatientId;
                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                            Patient.PatientId = results.PatientId;
                            Patient.Name = results.FirstName + " " + results.LastName;
                            Patient.Address = results.Address1;
                            Patient.Address2 = results.Address2;
                            Patient.CityStateZip = results.City + ", " + results.State + " " + results.Zip;
                            Patient.EMail = results.EMail;
                            Patient.PremiumFlag = Convert.ToBoolean(results.PremiumFlag);
                            Patient.PremiumExpireDate = results.PremiumExpireDate.ToString();
                            Patient.Active = Convert.ToBoolean(results.Active);
                            Patient.ThirdPartyId = Convert.ToInt32(results.ThirdPartyId);
                            Patient.EMRSystemId = Convert.ToInt32(results.EMRSystemId);
                            
                            
                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatients, UserId, ActionRead);

                            User uResults = db.Users.FirstOrDefault(u => u.UserRoleLink == Parms.PatientId && u.UserRoleId == 5);

                            if (uResults != null)
                            {
                                Patient.Password = clsEncryption.Decrypt(uResults.Password, "AMRP@ss");


                                string RepId = "R" + uResults.UserLogin;
                                User uResults2 = db.Users.FirstOrDefault(u => u.UserLogin == RepId && u.UserRoleId == 6);

                                if (uResults2 != null)
                                {
                                    Patient.RepresentativePwd = clsEncryption.Decrypt(uResults2.Password, "AMRP@ss");
                                }
                                else
                                {
                                    Patient.RepresentativePwd = "";
                                }
                            }

                            C_EMRSystem cResult = db.C_EMRSystem.FirstOrDefault(c => c.EMRSystemId == Patient.EMRSystemId);
                            if (cResult != null)
                                Patient.EMRSystemName = cResult.Value;
                            else
                                Patient.EMRSystemName = "";

                            C_ThirdParty cResults = db.C_ThirdParty.FirstOrDefault(c => c.ThirdPartyId == Patient.ThirdPartyId);
                            if (cResults != null)
                                Patient.ThirdPartyName = cResults.Value;
                            else
                                Patient.ThirdPartyName = "";

                        }
                        else
                        {
                            Patient.Valid = false;
                            Patient.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion

        #region Save Patient Data
        //------------------------------------------------------------------------
        // Save Patient Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Patient Data")]
        public PatientData SavePatientData(PatientData PatientData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientData.Valid = true;
            PatientData.ErrorMessage = "";

            // Remove token validation - This needs to be called to create a new patient from website - no token will be created yet.

            //// Validate the Token
            //clsToken objToken = new clsToken();
            //objToken.Token = Token;
            //objToken.UserId = UserId;
            //objToken.FacilityId = FacilityId;

            //if (objToken.Validate())
            //{
                try
                {
                    using (var db = new AMREntities())
                    {
                        // If language not set, set to English
                        //The below lines are commented by Ahmed As per bug reported by Scott
                        //if (PatientData.PreferredLanguageId == 0)
                        //    PatientData.PreferredLanguageId= 1;
                        if (PatientData.Address1 == null)
                            PatientData.Address1 = "";
                        if (PatientData.Address2 == null)
                            PatientData.Address2 = "";
                        if (PatientData.HomePhone == null)
                            PatientData.HomePhone = "";
                        if (PatientData.MobilePhone == null)
                            PatientData.MobilePhone = "";
                        if (PatientData.Zip == null)
                            PatientData.Zip = "";

                        if (PatientData.PatientId == 0)
                        {
                            // Add Patient
                            var Patient = new Patient()
                            {

                                PatientId = PatientData.PatientId,
                                FirstName = PatientData.FirstName,
                                MiddleName = PatientData.MiddleName,
                                LastName = PatientData.LastName,
                                Title = PatientData.Title,
                                Suffix = PatientData.Suffix,
                                Address1 = PatientData.Address1,
                                Address2 = PatientData.Address2,
                                Address3 = PatientData.Address3,
                                City = PatientData.City,
                                State = PatientData.State,
                                CommunityName = PatientData.CommunityName,
                                BuildingName = PatientData.BuildingName,
                                County = PatientData.County,
                                Zip = PatientData.Zip,
                                CountryCode = PatientData.CountryCode,
                                MailAddress1 = PatientData.MailAddress1,
                                MailAddress2 = PatientData.MailAddress2,
                                MailAddress3 = PatientData.MailAddress3,
                                MailCity = PatientData.MailCity,
                                MailState = PatientData.MailState,
                                MailPostalCode = PatientData.MailPostalCode,
                                MailCountryCode = PatientData.MailCountryCode,
                                DOB = PatientData.DOB,
                                SSN = PatientData.SSN,
                                HomePhone = PatientData.HomePhone,
                                MobilePhone = PatientData.MobilePhone,
                                WorkPhone = PatientData.WorkPhone,
                                Fax = PatientData.Fax,
                                EMail = PatientData.EMail,
                                PreferredLanguageId = PatientData.PreferredLanguageId,
                                GenderId = PatientData.GenderId,
                                RaceId_NotAnswer = PatientData.RaceId_NotAnswered,
                                RaceId_Native = PatientData.RaceId_Native,
                                RaceId_Asian = PatientData.RaceId_Asian,
                                RaceId_Black = PatientData.RaceId_Black,
                                RaceId_Hawaiian = PatientData.RaceId_Hawaiian,
                                RaceId_White = PatientData.RaceId_White,
                                EthnicityId = PatientData.EthnicityId,
                                MaritalStatusId = PatientData.MaritalStatusId,
                                ReligionId = PatientData.ReligionId,
                                BloodTypeId = PatientData.BloodTypeId,
                                HeightFt = PatientData.HeightFt,
                                HeightIn = PatientData.HeightIn,
                                Eyes = PatientData.Eyes,
                                Hair = PatientData.Hair,
                                Weight = PatientData.Weight,
                                OrganDoner = PatientData.OrganDoner,
                                SmokingStatusId = PatientData.SmokingStatusId,
                                Comments = PatientData.Comments,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                FacilityId_Created = FacilityId,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Active = PatientData.Active,
                                SmokingDate = PatientData.SmokingDate,
                                ThirdPartyId = 1,
                                EMRSystemId = 0,

                            };
                            db.Patients.Add(Patient);

                            db.SaveChanges();

                            //  Create PatientMatch Record for Matching
                            //string CleanAddress = Cleaner(PatientData.Address1);
                            //if (PatientData.Address2 != "" && (CleanAddress.Contains("appt") || CleanAddress.Contains("suite")))
                            //    CleanAddress = Cleaner(PatientData.Address2);

                            //string CleanPhone = PatientData.HomePhone.Replace(" ", "").Replace("-", "");
                            //if (CleanPhone == "")
                            //    CleanPhone = PatientData.MobilePhone.Replace(" ", "").Replace("-", "");

                            //var PatientMatch = new PatientMatch()
                            //{

                            //    PatientId = Patient.PatientId,
                            //    FirstClean = Cleaner(PatientData.FirstName),
                            //    LastClean = Cleaner(PatientData.LastName),
                            //    GenderId = PatientData.GenderId,
                            //    DOB = PatientData.DOB,
                            //    AddressClean = CleanAddress,
                            //    CityClean = Cleaner(PatientData.City),
                            //    StateClean = Cleaner(PatientData.State),
                            //    PostalClean = PatientData.Zip.Replace(" ","").Replace("-",""),
                            //    PhoneClean = CleanPhone,
                            //};
                            //db.PatientMatches.Add(PatientMatch);

                            //db.SaveChanges();

                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            string passclear = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Add User
                            var NewUser = new User()
                            {

                                UserLogin = Patient.PatientId.ToString(),
                                UserEmail = PatientData.EMail,
                                Password = passencr,
                                UserRoleId = 5,
                                UserRoleLink = Patient.PatientId,
                                Enabled = true,
                                Locked = false,
                                ResetPassword = true,
                            };
                            db.Users.Add(NewUser);

                            db.SaveChanges();

                            PatientData.PatientId = Patient.PatientId;
                            // Add Visit 0
                            var NewVisit = new PatientVisit()
                            {

                                PatientId = Patient.PatientId,
                                FacilityId = 0,
                                VisitId = 0,
                                VisitDate = System.DateTime.Now,
                                ProviderId = 0,
                                VisitReason = "Patient Entered",
                                Viewable = false,
                                DateCreated = System.DateTime.Now,
                                ClinicalSummary = "Print",
                            };
                            db.PatientVisits.Add(NewVisit);

                            db.SaveChanges();

                            // Create Patient Share Record

                            var PatientShare = new PatientShare()
                            {
                                PatientId = Patient.PatientId,
                                Demographics = true,
                                Allergy = true,
                                FamilyHistory = true,
                                LabResults = true,
                                MedicalHistory = true,
                                Medication = true,
                                Problem = true,
                                Procedure = true,
                                SocialHistory = true,
                                SurgicalHistory = true,
                                VitalSigns = true,
                                Immunization = true,
                                Organ = true,
                                ClinicalDoc = true,
                                Insurance = true,
                                EmergencyContact = true,
                                Appointment = true,
                                Visit = true,
                                UploadDocs = true,
                                PlanOfCare = true,
                                ClinicalInstructions = true,
                            };
                            db.PatientShares.Add(PatientShare);
                            db.SaveChanges();

                            // Create an entry in the PatientWebSettings table

                            // Add Patient Web Settings - First Use
                           var NewWebSetting = new PatientWebSetting()
                            {

                                PatientId = Patient.PatientId,
                                VisitWidgetLeft = 2,
                                VisitWidgetRight = 0,
                                AllergyWidgetLeft = 0,
                                AllergyWidgetRight = 3,
                                AppointmentsWidgetLeft = 7,
                                AppointmentsWidgetRight = 0,
                                FamilyWidgetLeft = 4,
                                FamilyWidgetRight = 0,
                                ImmunizationWidgetLeft = 0,
                                ImmunizationWidgetRight = 4,
                                InsuranceWidgetLeft = 8,
                                InsuranceWidgetRight = 0,
                                LabTestWidgetLeft = 0,
                                LabTestWidgetRight = 5,
                                MedicationWidgetLeft = 0,
                                MedicationWidgetRight = 2,
                                PastMedicalWidgetLeft = 5,
                                PastMedicalWidgetRight = 0,
                                ProblemWidgetLeft = 0,
                                ProblemWidgetRight = 1,
                                ProcedureWidgetLeft = 6,
                                ProcedureWidgetRight = 0,
                                SocialWidgetLeft = 3,
                                SocialWidgetRight = 0,
                                VitalSignsWidgetLeft = 0,
                                VitalSignsWidgetRight = 6,
                                PremiumWidgetLeft = 0,
                                PremiumWidgetRight = 0,
                                StatementWidgetLeft = 0,
                                StatementWidgetRight = 0,
                                DocumentWidgetLeft = 9,
                                DocumentWidgetRight = 0,
                                PlanOfCareWidgetLeft = 0,
                                PlanOfCareWidgetRight = 7,
                                ClinicalInstructionsWidgetLeft = 0,
                                ClinicalInstructionsWidgetRight = 8,
                                ProviderWidgetLeft = 1,
                                ProviderWidgetRight = 0,
                                EmailNotifyNewMessage = true,
                                PictureLocation = "",
                                TextNotifyNewMesssage = false,
                                CellCarrier = 0
                            };
                            db.PatientWebSettings.Add(NewWebSetting);
                            db.SaveChanges();

                            // Send Email to new patient with account login information.

                            clsEmail objEmail = new clsEmail();
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;


                            string WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
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
                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientData.Title + " " + PatientData.FirstName + " " + PatientData.LastName + "</strong>,</h1>  <br />" +
                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                "               Thank you for creating an AMR Patient Portal account. Below you will find your AccessID Code to log in to your portal account.<br /><br />" +
                                                "               Your AccessID Code:   <strong>" + Patient.PatientId.ToString() + "</strong> <br /><br />" +
                                                "               You will also be receiving an email with your temporary password, which you will need to change once you log in. " +
                                                "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account.<br /><br /> " +
                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                "               Thank you, <br />" +
                                                "               Your Member Services Team<br />" +
                                                "               <br /><br />" +
                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                "               </span>" +
                                                "             </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "      </td>" +
                                                "   </tr>" +
                                                "	<tr style=\"background-color:#37b74a;\">" +
                                                "      <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "            <tr style=height:10px;>" +
                                                "            <td></td>" +
                                                "            </tr>" +
                                                "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                "              </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "          <br /><br />" +
                                                "       </td>  " +
                                                "     </tr>" +
                                                "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                "</table>";

                            objEmail.SendEmailHTML(PatientData.EMail, "AMR Patient Portal - AccessID Code", WelcomeMsg);

                            WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
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
                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientData.Title + " " + PatientData.FirstName + " " + PatientData.LastName + "</strong>,</h1>  <br />" +
                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                "               Welcome again to AMR Patient Portal. Below you will find your temporary password.<br /><br />" +
                                                "               Your Temporary Password:   <strong>" + passclear + "</strong> <br /><br />" +
                                                "               Using the AccessID Code provided in the previous email, " +
                                                "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account and reset your temporary password.<br /><br /> " +
                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                "               Thank you, <br />" +
                                                "               Your Member Services Team<br /><br />" +
                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                "               </span>" +
                                                "             </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "      </td>" +
                                                "   </tr>" +
                                                "	<tr style=\"background-color:#37b74a;\">" +
                                                "      <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "            <tr style=height:10px;>" +
                                                "            <td></td>" +
                                                "            </tr>" +
                                                "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                "              </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "          <br /><br />" +
                                                "       </td>  " +
                                                "     </tr>" +
                                                "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                "</table>";

                            objEmail.SendEmailHTML(PatientData.EMail, "AMR Patient Portal - Temporary Password", WelcomeMsg);
                        }
                        else
                        {
                            // Update Patient Info 
                            string PatientId = PatientData.PatientId.ToString();
                            User UserResults = db.Users.FirstOrDefault(p => p.UserLogin == PatientId);
                            Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientData.PatientId);

                            if (PatientResp != null)
                            {
                                if (PatientData.Option == 1) // only Updating Personal Information Data [Edit by Talha]
                                {
                                    PatientResp.ReligionId = PatientData.ReligionId;
                                    PatientResp.BloodTypeId = PatientData.BloodTypeId;
                                    PatientResp.HeightFt = PatientData.HeightFt;
                                    PatientResp.HeightIn = PatientData.HeightIn;
                                    PatientResp.Eyes = PatientData.Eyes;
                                    PatientResp.Hair = PatientData.Hair;
                                    PatientResp.Weight = PatientData.Weight;
                                    PatientResp.OrganDoner = PatientData.OrganDoner;
                                    PatientResp.Comments = PatientData.Comments;
                                }
                                    //Added by talha
                                else if (PatientData.Option == 3)
                                {
                                    PatientResp.MobilePhone = PatientData.MobilePhone;
                                    PatientResp.EMail = PatientData.EMail;
                                    if (UserResults != null)
                                    {
                                        UserResults.UserEmail = PatientData.EMail;
                                    }
                                }
                                else
                                    if (PatientData.Option == 2)
                                    {
                                        if (UserResults != null)
                                        {
                                            UserResults.UserEmail = PatientData.EMail;
                                        }
                                        PatientResp.FirstName = PatientData.FirstName;
                                        PatientResp.MiddleName = PatientData.MiddleName;
                                        PatientResp.LastName = PatientData.LastName;
                                        PatientResp.Title = PatientData.Title;
                                        PatientResp.Suffix = PatientData.Suffix;
                                        PatientResp.Address1 = PatientData.Address1;
                                        PatientResp.Address2 = PatientData.Address2;
                                        PatientResp.Address3 = PatientData.Address3;
                                        PatientResp.City = PatientData.City;
                                        PatientResp.State = PatientData.State;
                                        PatientResp.CommunityName = PatientData.CommunityName;
                                        PatientResp.BuildingName = PatientData.BuildingName;
                                        PatientResp.County = PatientData.County;
                                        PatientResp.Zip = PatientData.Zip;
                                        PatientResp.CountryCode = PatientData.CountryCode;
                                        PatientResp.MailAddress1 = PatientData.MailAddress1;
                                        PatientResp.MailAddress2 = PatientData.MailAddress2;
                                        PatientResp.MailAddress3 = PatientData.MailAddress3;
                                        PatientResp.MailCity = PatientData.MailCity;
                                        PatientResp.MailState = PatientData.MailState;
                                        PatientResp.MailPostalCode = PatientData.MailPostalCode;
                                        PatientResp.MailCountryCode = PatientData.MailCountryCode;
                                        PatientResp.DOB = PatientData.DOB;
                                        PatientResp.SSN = PatientData.SSN;
                                        PatientResp.HomePhone = PatientData.HomePhone;
                                        PatientResp.MobilePhone = PatientData.MobilePhone;
                                        PatientResp.WorkPhone = PatientData.WorkPhone;
                                        PatientResp.Fax = PatientData.Fax;
                                        PatientResp.EMail = PatientData.EMail;
                                        PatientResp.PreferredLanguageId = PatientData.PreferredLanguageId;
                                        PatientResp.GenderId = PatientData.GenderId;
                                        PatientResp.RaceId_NotAnswer = PatientData.RaceId_NotAnswered;
                                        PatientResp.RaceId_Native = PatientData.RaceId_Native;
                                        PatientResp.RaceId_Asian = PatientData.RaceId_Asian;
                                        PatientResp.RaceId_Black = PatientData.RaceId_Black;
                                        PatientResp.RaceId_Hawaiian = PatientData.RaceId_Hawaiian;
                                        PatientResp.RaceId_White = PatientData.RaceId_White;
                                        PatientResp.EthnicityId = PatientData.EthnicityId;
                                        PatientResp.MaritalStatusId = PatientData.MaritalStatusId;
                                        PatientResp.SmokingStatusId = PatientData.SmokingStatusId;
                                        PatientResp.UserId_Modified = UserId;
                                        PatientResp.DateModified = System.DateTime.Now;
                                    }
                                    else //For All Fields Updation
                                    {
                                        PatientResp.FirstName = PatientData.FirstName;
                                        PatientResp.MiddleName = PatientData.MiddleName;
                                        PatientResp.LastName = PatientData.LastName;
                                        PatientResp.Title = PatientData.Title;
                                        PatientResp.Suffix = PatientData.Suffix;
                                        PatientResp.Address1 = PatientData.Address1;
                                        PatientResp.Address2 = PatientData.Address2;
                                        PatientResp.Address3 = PatientData.Address3;
                                        PatientResp.City = PatientData.City;
                                        PatientResp.State = PatientData.State;
                                        PatientResp.CommunityName = PatientData.CommunityName;
                                        PatientResp.BuildingName = PatientData.BuildingName;
                                        PatientResp.County = PatientData.County;
                                        PatientResp.Zip = PatientData.Zip;
                                        PatientResp.CountryCode = PatientData.CountryCode;
                                        PatientResp.MailAddress1 = PatientData.MailAddress1;
                                        PatientResp.MailAddress2 = PatientData.MailAddress2;
                                        PatientResp.MailAddress3 = PatientData.MailAddress3;
                                        PatientResp.MailCity = PatientData.MailCity;
                                        PatientResp.MailState = PatientData.MailState;
                                        PatientResp.MailPostalCode = PatientData.MailPostalCode;
                                        PatientResp.MailCountryCode = PatientData.MailCountryCode;
                                        PatientResp.DOB = PatientData.DOB;
                                        PatientResp.SSN = PatientData.SSN;
                                        PatientResp.HomePhone = PatientData.HomePhone;
                                        PatientResp.MobilePhone = PatientData.MobilePhone;
                                        PatientResp.WorkPhone = PatientData.WorkPhone;
                                        PatientResp.Fax = PatientData.Fax;
                                        PatientResp.EMail = PatientData.EMail;
                                        PatientResp.PreferredLanguageId = PatientData.PreferredLanguageId;
                                        PatientResp.GenderId = PatientData.GenderId;
                                        PatientResp.RaceId_NotAnswer = PatientData.RaceId_NotAnswered;
                                        PatientResp.RaceId_Native = PatientData.RaceId_Native;
                                        PatientResp.RaceId_Asian = PatientData.RaceId_Asian;
                                        PatientResp.RaceId_Black = PatientData.RaceId_Black;
                                        PatientResp.RaceId_Hawaiian = PatientData.RaceId_Hawaiian;
                                        PatientResp.RaceId_White = PatientData.RaceId_White;
                                        PatientResp.EthnicityId = PatientData.EthnicityId;
                                        PatientResp.MaritalStatusId = PatientData.MaritalStatusId;
                                        PatientResp.ReligionId = PatientData.ReligionId;
                                        PatientResp.BloodTypeId = PatientData.BloodTypeId;
                                        PatientResp.HeightFt = PatientData.HeightFt;
                                        PatientResp.HeightIn = PatientData.HeightIn;
                                        PatientResp.Eyes = PatientData.Eyes;
                                        PatientResp.Hair = PatientData.Hair;
                                        PatientResp.Weight = PatientData.Weight;
                                        PatientResp.OrganDoner = PatientData.OrganDoner;
                                        PatientResp.SmokingStatusId = PatientData.SmokingStatusId;
                                        PatientResp.Comments = PatientData.Comments;
                                        PatientResp.UserId_Modified = UserId;
                                        PatientResp.DateModified = System.DateTime.Now;
                                    }
                            }
                            db.SaveChanges();

                            if (PatientData.Option != 3)
                            {
                                string CleanAddress = Cleaner(PatientData.Address1);
                                if (PatientData.Address2 != null && PatientData.Address2 != "" && (CleanAddress.Contains("appt") || CleanAddress.Contains("suite")))
                                    CleanAddress = Cleaner(PatientData.Address2);
                                string CleanPhone = PatientData.HomePhone.Replace(" ", "").Replace("-", "");
                                if (CleanPhone == "")
                                    CleanPhone = PatientData.MobilePhone.Replace(" ", "").Replace("-", "");

                                PatientMatch PatientMatchResp = db.PatientMatches.FirstOrDefault(p => p.PatientId == PatientData.PatientId);

                                if (PatientMatchResp != null)
                                {
                                    PatientMatchResp.FirstClean = Cleaner(PatientData.FirstName);
                                    PatientMatchResp.LastClean = Cleaner(PatientData.LastName);
                                    PatientMatchResp.GenderId = PatientData.GenderId;
                                    PatientMatchResp.DOB = PatientData.DOB;
                                    PatientMatchResp.AddressClean = CleanAddress;
                                    PatientMatchResp.CityClean = Cleaner(PatientData.City);
                                    PatientMatchResp.StateClean = Cleaner(PatientData.State);
                                    PatientMatchResp.PostalClean = PatientData.Zip.Replace(" ", "").Replace("-", "");
                                    PatientMatchResp.PhoneClean = CleanPhone;
                                }
                                else
                                {
                                    //  Create PatientMatch Record for Matching
                                    var PatientMatch = new PatientMatch()
                                    {

                                        PatientId = PatientData.PatientId,
                                        FirstClean = Cleaner(PatientData.FirstName),
                                        LastClean = Cleaner(PatientData.LastName),
                                        GenderId = PatientData.GenderId,
                                        DOB = PatientData.DOB,
                                        AddressClean = CleanAddress,
                                        CityClean = Cleaner(PatientData.City),
                                        StateClean = Cleaner(PatientData.State),
                                        PostalClean = PatientData.Zip.Replace(" ", "").Replace("-", ""),
                                        PhoneClean = CleanPhone,
                                    };
                                    db.PatientMatches.Add(PatientMatch);
                                }
                            }
                            db.SaveChanges();

                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    PatientData.Valid = false;
                    PatientData.ErrorMessage = ex.Message;
                }
            //}
            //else
            //{
            //    // Invalid Token
            //    PatientData.Valid = false;
            //    PatientData.ErrorMessage = "Invalid Token";
            //}
            return PatientData;
        }
        #endregion

        #region Patient Search
        //------------------------------------------------------------------------
        // Patient Search 
        // SJF 12/17/2014  Changed to only pull active patients.
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Search")]
        public PatientTableData PatientSearch(PatientSearchParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientTableData Patient = new PatientTableData();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    int skip = Int32.Parse((Parms.PageNumber * Parms.PageSize).ToString());
                    int PageSize = Int32.Parse((Parms.PageSize).ToString());

                    using (var db = new AMREntities())
                    {
                        string userlogin=Parms.PatientId.ToString();

                        if (Parms.PatientId > 0)
                        {
                            var results = from p in db.Patients
                                          join c in db.C_Country on p.CountryCode equals c.CountryId
                                          where p.PatientId == Parms.PatientId
                                          && (p.Deleted == false || p.Deleted == null)
                                          select new
                                          {
                                              p.PatientId,
                                              p.FirstName,
                                              p.LastName,
                                              p.Address1,
                                              p.Address2,
                                              p.City,
                                              p.State,
                                              p.Zip,
                                              p.CountryCode,
                                              CountryName = c.Name,
                                              p.HomePhone,
                                              p.EMail,
                                              p.DOB,
                                          };
                            Patient.dt = clsTableConverter.ToDataTable(results);
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                        }
                        else if (Parms.DOB != null && Parms.EMRSystemId != null)
                        {

                            var results = (from p in db.Patients
                                           join c in db.C_Country on p.CountryCode equals c.CountryId
                                           where p.DOB == Parms.DOB 
                                           && p.EMRSystemId == Parms.EMRSystemId
                                           && (p.Deleted == false || p.Deleted == null)
                                           orderby p.LastName
                                           select new
                                           {
                                               p.PatientId,
                                               p.FirstName,
                                               p.LastName,
                                               p.Address1,
                                               p.Address2,
                                               p.City,
                                               p.State,
                                               p.Zip,
                                               p.CountryCode,
                                               CountryName = c.Name,
                                               p.HomePhone,
                                               p.EMail,
                                               p.DOB,
                                           });
                            Patient.count = results.Count();  //added by Talha for count the record

                            Patient.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize));//changed by Talha
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";

                        }
                        else if (Parms.DOB != null)
                        {

                            var results = (from p in db.Patients
                                           join c in db.C_Country on p.CountryCode equals c.CountryId
                                           where p.DOB == Parms.DOB // Added by Talha)
                                           && (p.Deleted == false || p.Deleted == null)
                                           orderby p.LastName
                                           select new
                                           {
                                               p.PatientId,
                                               p.FirstName,
                                               p.LastName,
                                               p.Address1,
                                               p.Address2,
                                               p.City,
                                               p.State,
                                               p.Zip,
                                               p.CountryCode,
                                               CountryName = c.Name,
                                               p.HomePhone,
                                               p.EMail,
                                               p.DOB,
                                           });
                            Patient.count = results.Count();  //added by Talha for count the record

                            Patient.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize)); //changed by Talha
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";

                        }
                        else if (Parms.FacilityId != null)
                        {
                            var results = (from p in db.Patients
                                           join l in db.PatientFacilityLinks on p.PatientId equals l.PatientId
                                           join c in db.C_Country on p.CountryCode equals c.CountryId
                                           where l.FacilityId == Parms.FacilityId
                                               && p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Address1.StartsWith(Parms.Address1)
                                               && p.City.StartsWith(Parms.City)
                                               && p.State.StartsWith(Parms.State)
                                               && p.Zip.StartsWith(Parms.Zip)
                                               && p.CountryCode.StartsWith(Parms.CountryCode)
                                               && p.HomePhone.StartsWith(Parms.HomePhone)
                                               && p.EMail.StartsWith(Parms.EMail)
                                               && (p.Deleted == false || p.Deleted == null)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.PatientId,
                                               p.FirstName,
                                               p.LastName,
                                               p.Address1,
                                               p.Address2,
                                               p.City,
                                               p.State,
                                               p.Zip,
                                               p.CountryCode,
                                               CountryName = c.Name,
                                               p.HomePhone,
                                               p.EMail,
                                               p.DOB,
                                           });
                            Patient.count = results.Count();

                            Patient.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize)); 
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                        }
                        else if (Parms.EMRSystemId != null)
                        {
                            var results = (from p in db.Patients
                                           join c in db.C_Country on p.CountryCode equals c.CountryId
                                           where p.EMRSystemId == Parms.EMRSystemId
                                               && p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Address1.StartsWith(Parms.Address1)
                                               && p.City.StartsWith(Parms.City)
                                               && p.State.StartsWith(Parms.State)
                                               && p.Zip.StartsWith(Parms.Zip)
                                               && p.CountryCode.StartsWith(Parms.CountryCode)
                                               && p.HomePhone.StartsWith(Parms.HomePhone)
                                               && p.EMail.StartsWith(Parms.EMail)
                                               && (p.Deleted == false || p.Deleted == null)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.PatientId,
                                               p.FirstName,
                                               p.LastName,
                                               p.Address1,
                                               p.Address2,
                                               p.City,
                                               p.State,
                                               p.Zip,
                                               p.CountryCode,
                                               CountryName = c.Name,
                                               p.HomePhone,
                                               p.EMail,
                                               p.DOB,
                                           });
                            Patient.count = results.Count(); //added by Talha for count the record

                            Patient.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize)); //changed by Talha
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                        }
                        else
                        {
                            var results = (from p in db.Patients
                                           join c in db.C_Country on p.CountryCode equals c.CountryId
                                           where p.FirstName.StartsWith(Parms.FirstName)
                                               && p.LastName.StartsWith(Parms.LastName)
                                               && p.Address1.StartsWith(Parms.Address1)
                                               && p.City.StartsWith(Parms.City)
                                               && p.State.StartsWith(Parms.State)
                                               && p.Zip.StartsWith(Parms.Zip)
                                               && p.CountryCode.StartsWith(Parms.CountryCode)
                                               && p.HomePhone.StartsWith(Parms.HomePhone)
                                               && p.EMail.StartsWith(Parms.EMail)
                                               && (p.Deleted == false || p.Deleted == null)

                                           orderby p.LastName
                                           select new
                                           {
                                               p.PatientId,
                                               p.FirstName,
                                               p.LastName,
                                               p.Address1,
                                               p.Address2,
                                               p.City,
                                               p.State,
                                               p.Zip,
                                               p.CountryCode,
                                               CountryName = c.Name,
                                               p.HomePhone,
                                               p.EMail,
                                               p.DOB,
                                           });
                            Patient.count = results.Count(); //added by Talha for count the record
                            Patient.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize)); //changed by Talha
                            Patient.dt.TableName = "Patients";

                            Patient.Valid = true;
                            Patient.ErrorMessage = "";
                        }


                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion

        #region Upgrade Patient
        //------------------------------------------------------------------------
        // Upgrade Patient  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Upgrade Patient")]
        public PatientResp UpgradePatient(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.PremiumFlag = true;
                            PatientResp.PremiumStartDate = System.DateTime.Now;
                            PatientResp.PremiumExpireDate = System.DateTime.Now.AddYears(1);
                        }
                        db.SaveChanges();

                        string LoginId = "S" + PatientId.ToString();
                        User UserResp = db.Users.FirstOrDefault(u => u.UserLogin == LoginId);

                        if (UserResp != null)
                        {
                            UserResp.UserEmail = PatientResp.FirstName + " " + PatientResp.LastName;
                            UserResp.Enabled = true;
                        }
                        else
                        {
                            // Add User For Medical Summary Login
                            var NewMSUser = new User()
                            {

                                UserLogin = LoginId,
                                UserEmail = PatientResp.FirstName + " " + PatientResp.LastName,
                                Password = null,
                                UserRoleId = 8,
                                UserRoleLink = PatientId,
                                Enabled = true,
                                Locked = false,
                            };
                            db.Users.Add(NewMSUser);
                        }
                        db.SaveChanges();

                        // Turn on Care Provider Upload
                        string LoginId2 = "C" + PatientId.ToString();
                        User UserResp2 = db.Users.FirstOrDefault(u => u.UserLogin == LoginId2);

                        if (UserResp2 != null)
                        {
                            UserResp2.Enabled = true;
                            db.SaveChanges();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Downgrade Patient
        //------------------------------------------------------------------------
        // Downgrade Patient  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Downgrade Patient")]
        public PatientResp DowngradePatient(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.PremiumFlag = false;
                        }
                        db.SaveChanges();

                        // Turn off Medical Summary Login
                        string LoginId = "S" + PatientId.ToString();
                        User UserResp = db.Users.FirstOrDefault(u => u.UserLogin == LoginId);

                        if (UserResp != null)
                        {
                            UserResp.Enabled = false;
                        }
                        db.SaveChanges();

                        // Turn off Care Provider Upload
                        string LoginId2 = "C" + PatientId.ToString();
                        User UserResp2 = db.Users.FirstOrDefault(u => u.UserLogin == LoginId2);

                        if (UserResp2 != null)
                        {
                            UserResp2.Enabled = false;
                            db.SaveChanges();
                        }
                        

                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Renew Patient
        //------------------------------------------------------------------------
        // Renew Patient  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Renew Patient")]
        public PatientResp RenewPatient(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.PremiumFlag = true;
                            PatientResp.PremiumExpireDate = PatientResp.PremiumExpireDate.Value.AddYears(1);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Change Patient Renew Date
        //------------------------------------------------------------------------
        // Change Patient Renew Date  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Patient Renew Date")]
        public PatientResp ChangePatientRenewDate(Int64 PatientId, DateTime RenewDate, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.PremiumFlag = true;
                            PatientResp.PremiumExpireDate = RenewDate;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Get Patient Activate / Inactive
        //------------------------------------------------------------------------
        // Get Patient Activate / Inactive  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Activate / Inactive")]
        public bool GetPatientActivateInacive(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            bool Active = false;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            Active = Convert.ToBoolean(PatientResp.Active);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Active = false;
                }
            }
            else
            {
                // Invalid Token
                Active = false;
            }
            return Active;
        }
        #endregion

        #region Activate Patient
        //------------------------------------------------------------------------
        // Activate Patient  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Activate Patient")]
        public PatientResp ActivatePatient(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Active = true;
                            PatientResp.UserId_Modified = UserId;
                            PatientResp.DateModified = System.DateTime.Now;
                            db.SaveChanges();
                        }


                        //User UserResp = db.Users.FirstOrDefault(u => u.UserRoleLink == PatientId && u.UserRoleId == 5);
                        //if (UserResp != null)
                        //{
                        //    UserResp.Enabled = true;
                        //}
                        //db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Deactivate Patient
        //------------------------------------------------------------------------
        // Deactivate Patient  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Deactivate Patient")]
        public PatientResp DeactivatePatient(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Active = false;
                            PatientResp.UserId_Modified = UserId;
                            PatientResp.DateModified = System.DateTime.Now;
                            db.SaveChanges();
                        }

                        //User UserResp = db.Users.FirstOrDefault(u => u.UserRoleLink == PatientId && u.UserRoleId == 5);
                        //if (UserResp != null)
                        //{
                        //    UserResp.Enabled = false;
                        //}
                        //db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Update Patient Third Party
        //------------------------------------------------------------------------
        // Update Patient Third Party  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Update Patient Third Party")]
        public PatientResp UpdatePatientThirdParty(Int64 PatientId, int ThirdPartyId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.ThirdPartyId = ThirdPartyId;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Update Patient EMR System
        //------------------------------------------------------------------------
        // Update Patient EMR System  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Update Patient EMR System")]
        public PatientResp UpdatePatientEMRSystem(Int64 PatientId, int EMRSystemId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.EMRSystemId = EMRSystemId;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Change Patient Email
        //------------------------------------------------------------------------
        // Change Patient Email  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Patient Email")]
        public PatientResp ChangePatientEmail(Int64 PatientId, string Email, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp RespData = new PatientResp();
            RespData.Valid = true;
            RespData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.EMail = Email;
                            db.SaveChanges();
                        }
                        

                        User UserResp = db.Users.FirstOrDefault(u => u.UserRoleId == 5 && u.UserRoleLink == PatientId);

                        if (UserResp != null)
                        {
                            UserResp.UserEmail = Email;
                            db.SaveChanges();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    RespData.Valid = false;
                    RespData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                RespData.Valid = false;
                RespData.ErrorMessage = "Invalid Token";
            }
            return RespData;
        }
        #endregion

        #region Get PatientOrgan Data
        //------------------------------------------------------------------------
        // Get PatientOrgan Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientOrgan Data")]
        public PatientOrganData GetPatientOrganData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientOrganData PatientOrgan = new PatientOrganData();
            PatientOrgan.Valid = true;
            PatientOrgan.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientOrgan results = db.PatientOrgans.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            PatientOrgan.Valid = true;
                            PatientOrgan.ErrorMessage = "";
                            PatientOrgan.PatientId = results.PatientId;
                            PatientOrgan.Heart = Convert.ToBoolean(results.Heart);
                            PatientOrgan.Liver = Convert.ToBoolean(results.Liver);
                            PatientOrgan.Pancreas = Convert.ToBoolean(results.Pancreas);
                            PatientOrgan.Lungs = Convert.ToBoolean(results.Lungs);
                            PatientOrgan.Kidneys = Convert.ToBoolean(results.Kidneys);
                            PatientOrgan.Intestines = Convert.ToBoolean(results.Intestines);
                            PatientOrgan.Cornea = Convert.ToBoolean(results.Cornea);
                            PatientOrgan.Skin = Convert.ToBoolean(results.Skin);
                            PatientOrgan.BoneMarrow = Convert.ToBoolean(results.BoneMarrow);
                            PatientOrgan.HeartValves = Convert.ToBoolean(results.HeartValves);
                            PatientOrgan.ConnectiveTissue = Convert.ToBoolean(results.ConnectiveTissue);

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientOrgan, UserId, ActionRead);
                        }
                        else
                        {
                            PatientOrgan.Valid = false;
                            PatientOrgan.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientOrgan.Valid = false;
                    PatientOrgan.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientOrgan.Valid = false;
                PatientOrgan.ErrorMessage = "Invalid Token";
            }
            return PatientOrgan;
        }
        #endregion

        #region Save PatientOrgan Data
        //------------------------------------------------------------------------
        // Save PatientOrgan Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientOrgan Data")]
        public PatientOrganData SavePatientOrganData(PatientOrganData PatientOrganData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientOrganData.Valid = true;
            PatientOrganData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Update PatientOrgan Info 

                        PatientOrgan PatientResp = db.PatientOrgans.FirstOrDefault(p => p.PatientId == PatientOrganData.PatientId);

                        if (PatientResp != null)
                        {

                            PatientResp.Heart = PatientOrganData.Heart;
                            PatientResp.Liver = PatientOrganData.Liver;
                            PatientResp.Pancreas = PatientOrganData.Pancreas;
                            PatientResp.Lungs = PatientOrganData.Lungs;
                            PatientResp.Kidneys = PatientOrganData.Kidneys;
                            PatientResp.Intestines = PatientOrganData.Intestines;
                            PatientResp.Cornea = PatientOrganData.Cornea;
                            PatientResp.Skin = PatientOrganData.Skin;
                            PatientResp.BoneMarrow = PatientOrganData.BoneMarrow;
                            PatientResp.HeartValves = PatientOrganData.HeartValves;
                            PatientResp.ConnectiveTissue = PatientOrganData.ConnectiveTissue;
                            PatientResp.UserId_Modified = UserId;
                            PatientResp.DateModified = System.DateTime.Now;
                        }
                        else
                        {
                            // Add Patient
                            var PatientOrgan = new PatientOrgan()
                            {

                                PatientId = PatientOrganData.PatientId,
                                Heart = PatientOrganData.Heart,
                                Liver = PatientOrganData.Liver,
                                Pancreas = PatientOrganData.Pancreas,
                                Lungs = PatientOrganData.Lungs,
                                Kidneys = PatientOrganData.Kidneys,
                                Intestines = PatientOrganData.Intestines,
                                Cornea = PatientOrganData.Cornea,
                                Skin = PatientOrganData.Skin,
                                BoneMarrow = PatientOrganData.BoneMarrow,
                                HeartValves = PatientOrganData.HeartValves,
                                ConnectiveTissue = PatientOrganData.ConnectiveTissue,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                            };
                            db.PatientOrgans.Add(PatientOrgan);
                        }

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientOrganData.Valid = false;
                    PatientOrganData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientOrganData.Valid = false;
                PatientOrganData.ErrorMessage = "Invalid Token";
            }
            return PatientOrganData;
        }
        #endregion


        #region Get Visit Data
        //------------------------------------------------------------------------
        // Get Visit Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Visit Data")]
        public PatientVisitData GetPatientVisitData(PatientVisitParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientVisitData PatientVisit = new PatientVisitData();
            PatientVisit.Valid = true;
            PatientVisit.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == Parms.PatientId && FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId);

                        if (results != null)
                        {
                            PatientVisit.PatientId = results.PatientId;
                            PatientVisit.Valid = true;
                            PatientVisit.ErrorMessage = "";
                            PatientVisit.PatientId = results.PatientId;
                            PatientVisit.FacilityId = results.FacilityId;
                            PatientVisit.VisitId = results.VisitId;
                            PatientVisit.VisitDate = Convert.ToDateTime(results.VisitDate);
                            PatientVisit.ProviderId = Convert.ToInt64(results.ProviderId);
                            PatientVisit.VistReason = results.VisitReason;
                            PatientVisit.Viewable = Convert.ToBoolean(results.Viewable);
                        }
                        else
                        {
                            PatientVisit.Valid = false;
                            PatientVisit.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientVisit.Valid = false;
                    PatientVisit.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVisit.Valid = false;
                PatientVisit.ErrorMessage = "Invalid Token";
            }
            return PatientVisit;
        }
        #endregion

        #region Get Visit List
        //------------------------------------------------------------------------
        // Get Visit List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Visit List")]
        public PatientVisitTableData GetPatientVisitList(PatientVisitParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientVisitTableData PatientVisit = new PatientVisitTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())

                    {
                        if (Parms.Option == 0)   // All Visits
                        {
                            var results = from p in db.vwPatientVisitSummaries
                                        where p.PatientId == Parms.PatientId
                                        select new
                                        {
                                            p.PatientId,
                                            p.FacilityId,
                                            p.FacilityName,
                                            p.FacilityAddress,
                                            p.FacilityCityStatePostal,
                                            p.VisitId,
                                            p.VisitDate,
                                            p.ProviderId,
                                            p.ProviderName,
                                            p.VisitReason,
                                            p.Viewable,
                                        };
                            PatientVisit.dt = clsTableConverter.ToDataTable(results);
                        }
                        else if (Parms.Option == 1) //  Visits For Specific Facility and Visit ID
                        {
                            var results = from p in db.vwPatientVisitSummaries
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId
                                          select new
                                          {
                                              p.PatientId,
                                              p.FacilityId,
                                              p.FacilityName,
                                              p.FacilityAddress,
                                              p.FacilityCityStatePostal,
                                              p.VisitId,
                                              p.VisitDate,
                                              p.ProviderId,
                                              p.ProviderName,
                                              p.VisitReason,
                                              p.Viewable,
                                          };
                            PatientVisit.dt = clsTableConverter.ToDataTable(results);
                        }
                        else  //  Visits For Specific Facility
                        {
                            var results = from p in db.vwPatientVisitSummaries
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId
                                          select new
                                          {
                                              p.PatientId,
                                              p.FacilityId,
                                              p.FacilityName,
                                              p.FacilityAddress,
                                              p.FacilityCityStatePostal,
                                              p.VisitId,
                                              p.VisitDate,
                                              p.ProviderId,
                                              p.ProviderName,
                                              p.VisitReason,
                                              p.Viewable,
                                          };
                            PatientVisit.dt = clsTableConverter.ToDataTable(results);
                        }
                        PatientVisit.dt.TableName = "PatientVisits";

                        PatientVisit.Valid = true;
                        PatientVisit.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientVisit.Valid = false;
                    PatientVisit.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVisit.Valid = false;
                PatientVisit.ErrorMessage = "Invalid Token";
            }
            return PatientVisit;
        }
        #endregion

        #region Get Latest Visit 
        //------------------------------------------------------------------------
        // Get Latest Visit 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Latest Visit")]
        public PatientVisitData GetLatestPatientVisit(PatientVisitParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientVisitData PatientVisit = new PatientVisitData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientVisit results = db.PatientVisits.OrderByDescending(p => p.VisitDate).FirstOrDefault(p => p.PatientId == Parms.PatientId && p.VisitId > 0 && p.Deleted == false);

                        if (results != null)
                        {
                            PatientVisit.PatientId = results.PatientId;
                            PatientVisit.Valid = true;
                            PatientVisit.ErrorMessage = "";
                            PatientVisit.PatientId = results.PatientId;
                            PatientVisit.FacilityId = results.FacilityId;
                            PatientVisit.VisitId = results.VisitId;
                            PatientVisit.VisitDate = Convert.ToDateTime(results.VisitDate);
                            PatientVisit.ProviderId = Convert.ToInt64(results.ProviderId);
                            PatientVisit.VistReason = results.VisitReason;
                            PatientVisit.Viewable = Convert.ToBoolean(results.Viewable);
                        }
                        else
                        {
                            results = db.PatientVisits.OrderByDescending(p => p.VisitDate).FirstOrDefault(p => p.PatientId == Parms.PatientId);

                            if (results != null)
                            {
                                PatientVisit.PatientId = results.PatientId;
                                PatientVisit.Valid = true;
                                PatientVisit.ErrorMessage = "";
                                PatientVisit.PatientId = results.PatientId;
                                PatientVisit.FacilityId = results.FacilityId;
                                PatientVisit.VisitId = results.VisitId;
                                PatientVisit.VisitDate = Convert.ToDateTime(results.VisitDate);
                                PatientVisit.ProviderId = Convert.ToInt64(results.ProviderId);
                                PatientVisit.VistReason = results.VisitReason;
                                PatientVisit.Viewable = Convert.ToBoolean(results.Viewable);
                            }
                            else
                            {
                                PatientVisit.Valid = false;
                                PatientVisit.ErrorMessage = "Could not read record.";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientVisit.Valid = false;
                    PatientVisit.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVisit.Valid = false;
                PatientVisit.ErrorMessage = "Invalid Token";
            }
            return PatientVisit;
        }
        #endregion

        #region Share Visit
        //------------------------------------------------------------------------
        // Visit Share 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Visit Share")]
        public PatientResp VisitShare(PatientVisitParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp Response = new PatientResp();
            Response.Valid = true;
            Response.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientVisit PatientVisit = db.PatientVisits.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId); //Edit by Talha Amin...(Replace FacilityId with p.FacilityId)

                        if (PatientVisit != null)
                        {

                            PatientVisit.Viewable = Parms.Share;
                            db.SaveChanges();
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Get Facility List
        //------------------------------------------------------------------------
        // Get Facility List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Facility List")]
        public PatientFacilityTableData GetPatientFacilityList(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientFacilityTableData PatientFacility = new PatientFacilityTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from f in db.vwFacilitiesForPatients
                                        where f.PatientId == Parms.PatientId
                                        select new
                                        {
                                            f.FacilityId,
                                            f.FacilityName
                                        };
                        PatientFacility.dt = clsTableConverter.ToDataTable(results);

                        PatientFacility.dt.TableName = "Facillities";

                        PatientFacility.Valid = true;
                        PatientFacility.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientFacility.Valid = false;
                    PatientFacility.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientFacility.Valid = false;
                PatientFacility.ErrorMessage = "Invalid Token";
            }
            return PatientFacility;
        }
        #endregion

        #region Write Audit Record
        //------------------------------------------------------------------------
        // Write Audit Record
        //------------------------------------------------------------------------
        private void WriteAuditRecord(Int64 PatientId, Int64 VisitId, Int64 Cntr, Int64 FacilityId, Int16 DocumentType, Int64 UserId, Int16 Action)
        {
            using (var db = new AMREntities())
            {
                //// Add Items
                var Audit = new PatientDocumentAudit()
                {
                    PatientId = PatientId,
                    VisitId = VisitId,
                    DocCntr = Cntr,
                    FacilityId = FacilityId,
                    DocumentTypeId = DocumentType,
                    UserId = UserId,
                    TDStamp = System.DateTime.Now,
                    AuditActionId = Action,
                };
                db.PatientDocumentAudits.Add(Audit);

                db.SaveChanges();
            }
        }
        #endregion


        #region Get PatientShare Data
        //------------------------------------------------------------------------
        // Get PatientShare Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientShare Data")]
        public PatientShareData GetPatientShareData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareData PatientShareData = new PatientShareData();
            PatientShareData.Valid = true;
            PatientShareData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare results = db.PatientShares.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            PatientShareData.Valid = true;
                            PatientShareData.ErrorMessage = "";
                            PatientShareData.PatientId = results.PatientId;
                            PatientShareData.Demographics = Convert.ToBoolean(results.Demographics);
                            PatientShareData.Allergy = Convert.ToBoolean(results.Allergy);
                            PatientShareData.FamilyHistory = Convert.ToBoolean(results.FamilyHistory);
                            PatientShareData.LabResults = Convert.ToBoolean(results.LabResults);
                            PatientShareData.MedicalHistory = Convert.ToBoolean(results.MedicalHistory);
                            PatientShareData.Medication = Convert.ToBoolean(results.Medication);
                            PatientShareData.Problem = Convert.ToBoolean(results.Problem);
                            PatientShareData.SocialHistory = Convert.ToBoolean(results.SocialHistory);
                            PatientShareData.SurgicalHistory = Convert.ToBoolean(results.SurgicalHistory);
                            PatientShareData.VitalSigns = Convert.ToBoolean(results.VitalSigns);
                            PatientShareData.Immunization = Convert.ToBoolean(results.Immunization);
                            PatientShareData.Organ = Convert.ToBoolean(results.Organ);
                            PatientShareData.ClinicalDoc = Convert.ToBoolean(results.ClinicalDoc);
                            PatientShareData.Insurance = Convert.ToBoolean(results.Insurance);
                            PatientShareData.EmergencyContact = Convert.ToBoolean(results.EmergencyContact);
                            PatientShareData.Appointment = Convert.ToBoolean(results.Appointment);
                            PatientShareData.Visit = Convert.ToBoolean(results.Visit);
                            PatientShareData.UploadDocs = Convert.ToBoolean(results.UploadDocs);
                            PatientShareData.PlanOfCare = Convert.ToBoolean(results.PlanOfCare);
                            PatientShareData.Procedure = Convert.ToBoolean(results.Procedure);
                            PatientShareData.ClinicalInstructions = Convert.ToBoolean(results.ClinicalInstructions);
                        }
                        else
                        {
                            // Create Patient Share Record

                            var PatientShare = new PatientShare()
                            {
                                PatientId = Parms.PatientId,
                                Demographics = false,
                                Allergy = false,
                                FamilyHistory = false,
                                LabResults = false,
                                MedicalHistory = false,
                                Medication = false,
                                Problem = false,
                                SocialHistory = false,
                                SurgicalHistory = false,
                                VitalSigns = false,
                                Immunization = false,
                                Organ = false,
                                ClinicalDoc = false,
                                Insurance = false,
                                EmergencyContact = false,
                                Appointment = false,
                                Visit = false,
                                UploadDocs = false,
                                PlanOfCare = false,
                                Procedure=false,
                                ClinicalInstructions = false,
                            };
                            db.PatientShares.Add(PatientShare);
                            db.SaveChanges();

                            PatientShareData.Valid = true;
                            PatientShareData.ErrorMessage = "";
                            PatientShareData.PatientId = results.PatientId;
                            PatientShareData.Demographics = false;
                            PatientShareData.Allergy = false;
                            PatientShareData.FamilyHistory = false;
                            PatientShareData.LabResults = false;
                            PatientShareData.MedicalHistory = false;
                            PatientShareData.Medication = false;
                            PatientShareData.Problem = false;
                            PatientShareData.SocialHistory = false;
                            PatientShareData.SurgicalHistory = false;
                            PatientShareData.VitalSigns = false;
                            PatientShareData.Immunization = false;
                            PatientShareData.Organ = false;
                            PatientShareData.ClinicalDoc = false;
                            PatientShareData.Insurance = false;
                            PatientShareData.EmergencyContact = false;
                            PatientShareData.Appointment = false;
                            PatientShareData.Visit = false;
                            PatientShareData.UploadDocs = false;
                            PatientShareData.PlanOfCare = false;
                            PatientShareData.Procedure = false;
                            PatientShareData.ClinicalInstructions = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientShareData.Valid = false;
                    PatientShareData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientShareData.Valid = false;
                PatientShareData.ErrorMessage = "Invalid Token";
            }
            return PatientShareData;
        }
        #endregion

        #region Patient Share Demographics
        //------------------------------------------------------------------------
        // Patient Share Demographics 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Demographics")]
        public PatientShareResp PatientShareDemographics(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Demographics = Share;
                        }         
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Allergy
        //------------------------------------------------------------------------
        // Patient Share Allergy 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Allergy")]
        public PatientShareResp PatientShareAllergy(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Allergy = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share FamilyHistory
        //------------------------------------------------------------------------
        // Patient Share FamilyHistory 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share FamilyHistory")]
        public PatientShareResp PatientShareFamilyHistory(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.FamilyHistory = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share LabResults
        //------------------------------------------------------------------------
        // Patient Share LabResults 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share LabResults")]
        public PatientShareResp PatientShareLabResults(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.LabResults = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share MedicalHistory
        //------------------------------------------------------------------------
        // Patient Share MedicalHistory 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share MedicalHistory")]
        public PatientShareResp PatientShareMedicalHistory(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.MedicalHistory = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Medication
        //------------------------------------------------------------------------
        // Patient Share Medication 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Medication")]
        public PatientShareResp PatientShareMedication(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Medication = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Problem
        //------------------------------------------------------------------------
        // Patient Share Problem 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Problem")]
        public PatientShareResp PatientShareProblem(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Problem = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Procedure
        //------------------------------------------------------------------------
        // Patient Share Procedure 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Problem")]
        public PatientShareResp PatientShareProcedure(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Procedure = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share SocialHistory
        //------------------------------------------------------------------------
        // Patient Share SocialHistory 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share SocialHistory")]
        public PatientShareResp PatientShareSocialHistory(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.SocialHistory = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share SurgicalHistory
        //------------------------------------------------------------------------
        // Patient Share SurgicalHistory 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share SurgicalHistory")]
        public PatientShareResp PatientShareSurgicalHistory(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.SurgicalHistory = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share VitalSigns
        //------------------------------------------------------------------------
        // Patient Share VitalSigns 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share VitalSigns")]
        public PatientShareResp PatientShareVitalSigns(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.VitalSigns = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Immunization
        //------------------------------------------------------------------------
        // Patient Share Immunization 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Immunization")]
        public PatientShareResp PatientShareImmunization(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Immunization = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Organ
        //------------------------------------------------------------------------
        // Patient Share Organ 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Organ")]
        public PatientShareResp PatientShareOrgan(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Organ = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share ClinicalDoc
        //------------------------------------------------------------------------
        // Patient Share ClinicalDoc 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share ClinicalDoc")]
        public PatientShareResp PatientShareClinicalDoc(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.ClinicalDoc = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Insurance
        //------------------------------------------------------------------------
        // Patient Share Insurance 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Insurance")]
        public PatientShareResp PatientShareInsurance(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Insurance = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share EmergencyContact
        //------------------------------------------------------------------------
        // Patient Share EmergencyContact 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share EmergencyContact")]
        public PatientShareResp PatientShareEmergencyContact(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.EmergencyContact = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Appointment
        //------------------------------------------------------------------------
        // Patient Share Appointment 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Appointment")]
        public PatientShareResp PatientShareAppointment(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Appointment = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share Visit
        //------------------------------------------------------------------------
        // Patient Share Visit 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share Visit")]
        public PatientShareResp PatientShareVisit(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Visit = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share UploadDocs
        //------------------------------------------------------------------------
        // Patient Share UploadDocs 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share UploadDocs")]
        public PatientShareResp PatientShareUploadDocs(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.UploadDocs = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share PlanOfCare
        //------------------------------------------------------------------------
        // Patient Share PlanOfCare 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share PlanOfCare")]
        public PatientShareResp PatientSharePlanOfCare(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.PlanOfCare = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Patient Share ClinicalInstructions
        //------------------------------------------------------------------------
        // Patient Share ClinicalInstructions 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Share ClinicalInstructions")]
        public PatientShareResp PatientShareClinicalInstructions(Int64 PatientId, bool Share, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientShareResp Response = new PatientShareResp();
            Response.Valid = true;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientShare PatientResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.ClinicalInstructions = Share;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion


        #region Save Patient Account Link
        //------------------------------------------------------------------------
        // Save Patient Account Link Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Patient Account Link Data")]
        public PatientAccountLinkData SavePatientAccountLinkData(PatientAccountLinkData PatientData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientData.Valid = true;
            PatientData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientAccountLink PatientResp = db.PatientAccountLinks.FirstOrDefault(p => p.PatientId == PatientData.PatientId && p.PatientId_Linked == PatientData.PatientId_Linked);

                        if (PatientResp != null)
                        {
                            PatientData.Valid = false;
                            PatientData.ErrorMessage = "Link already exists.";
                        }
                        else
                        {
                            // Add Patient Account Link Record
                            var Link = new PatientAccountLink()
                            {
                                PatientId = PatientData.PatientId,
                                PatientId_Linked = PatientData.PatientId_Linked,
                                DateApproved = System.DateTime.Now,
                            };
                            db.PatientAccountLinks.Add(Link);

                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientData.Valid = false;
                    PatientData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientData.Valid = false;
                PatientData.ErrorMessage = "Invalid Token";
            }
            return PatientData;
        }
        #endregion

        #region Delete Patient Account Link
        //------------------------------------------------------------------------
        // Delete Patient Account Link Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete Patient Account Link Data")]
        public PatientAccountLinkData DeletePatientAccountLinkData(PatientAccountLinkData PatientData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientData.Valid = true;
            PatientData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {

                    using (var db = new AMREntities())
                    {
                        var Link = new PatientAccountLink()
                        {
                            PatientId = PatientData.PatientId,
                            PatientId_Linked = PatientData.PatientId_Linked,
                        };

                        db.PatientAccountLinks.Attach(Link);
                        db.PatientAccountLinks.Remove(Link);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientData.Valid = false;
                    PatientData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientData.Valid = false;
                PatientData.ErrorMessage = "Invalid Token";
            }
            return PatientData;
        }
        #endregion

        #region Get Account Link List
        //------------------------------------------------------------------------
        // Get Account Link List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Account Link List")]
        public PatientTableData GetPatientAccountLinkList(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientTableData PatientTableData = new PatientTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientAccountLinks
                                      join m in db.Patients on p.PatientId_Linked equals m.PatientId 
                                      where p.PatientId == Parms.PatientId
                                      select new
                                      {
                                          p.PatientId,
                                          p.PatientId_Linked,
                                          PatientName = m.FirstName + " " + m.LastName,
                                          p.DateApproved
                                      };
                        PatientTableData.dt = clsTableConverter.ToDataTable(results);

                        PatientTableData.dt.TableName = "Links";

                        PatientTableData.Valid = true;
                        PatientTableData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientTableData.Valid = false;
                    PatientTableData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientTableData.Valid = false;
                PatientTableData.ErrorMessage = "Invalid Token";
            }
            return PatientTableData;
        }
        #endregion

        #region Get Account Link Parent List
        //------------------------------------------------------------------------
        // Get Account Link List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Account Link Parent List")]
        public PatientTableData GetPatientAccountLinkParentList(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientTableData PatientTableData = new PatientTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientAccountLinks
                                      join m in db.Patients on p.PatientId equals m.PatientId 
                                      where p.PatientId_Linked == Parms.PatientId
                                      select new
                                      {
                                          p.PatientId,
                                          p.PatientId_Linked,
                                          PatientName = m.FirstName + " " + m.LastName,
                                          p.DateApproved
                                      };
                        PatientTableData.dt = clsTableConverter.ToDataTable(results);

                        PatientTableData.dt.TableName = "Links";

                        PatientTableData.Valid = true;
                        PatientTableData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientTableData.Valid = false;
                    PatientTableData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientTableData.Valid = false;
                PatientTableData.ErrorMessage = "Invalid Token";
            }
            return PatientTableData;
        }
        #endregion

        #region Get PatientWebSetting Data
        //------------------------------------------------------------------------
        // Get PatientWebSetting Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientWebSetting Data")]
        public PatientWebSettingData GetPatientWebSettingData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientWebSettingData PatientWebSetting = new PatientWebSettingData();
            PatientWebSetting.Valid = true;
            PatientWebSetting.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientWebSetting results = db.PatientWebSettings.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            PatientWebSetting.Valid = true;
                            PatientWebSetting.ErrorMessage = "";
                            PatientWebSetting.PatientId = results.PatientId;
                            PatientWebSetting.VisitWidgetLeft = Convert.ToInt16(results.VisitWidgetLeft);
                            PatientWebSetting.VisitWidgetRight = Convert.ToInt16(results.VisitWidgetRight);
                            PatientWebSetting.AllergyWidgetLeft = Convert.ToInt16(results.AllergyWidgetLeft);
                            PatientWebSetting.AllergyWidgetRight = Convert.ToInt16(results.AllergyWidgetRight);
                            PatientWebSetting.AppointmentsWidgetLeft = Convert.ToInt16(results.AppointmentsWidgetLeft);
                            PatientWebSetting.AppointmentsWidgetRight = Convert.ToInt16(results.AppointmentsWidgetRight);
                            PatientWebSetting.ImmunizationWidgetLeft = Convert.ToInt16(results.ImmunizationWidgetLeft);
                            PatientWebSetting.ImmunizationWidgetRight = Convert.ToInt16(results.ImmunizationWidgetRight);
                            PatientWebSetting.InsuranceWidgetLeft = Convert.ToInt16(results.InsuranceWidgetLeft);
                            PatientWebSetting.InsuranceWidgetRight = Convert.ToInt16(results.InsuranceWidgetRight);
                            PatientWebSetting.LabTestWidgetLeft = Convert.ToInt16(results.LabTestWidgetLeft);
                            PatientWebSetting.LabTestWidgetRight = Convert.ToInt16(results.LabTestWidgetRight);
                            PatientWebSetting.MedicationWidgetLeft = Convert.ToInt16(results.MedicationWidgetLeft);
                            PatientWebSetting.MedicationWidgetRight = Convert.ToInt16(results.MedicationWidgetRight);
                            PatientWebSetting.PastMedicalWidgetLeft = Convert.ToInt16(results.PastMedicalWidgetLeft);
                            PatientWebSetting.PastMedicalWidgetRight = Convert.ToInt16(results.PastMedicalWidgetRight);
                            PatientWebSetting.ProblemWidgetLeft = Convert.ToInt16(results.ProblemWidgetLeft);
                            PatientWebSetting.ProblemWidgetRight = Convert.ToInt16(results.ProblemWidgetRight);
                            PatientWebSetting.ProcedureWidgetLeft = Convert.ToInt16(results.ProcedureWidgetLeft);
                            PatientWebSetting.ProcedureWidgetRight = Convert.ToInt16(results.ProcedureWidgetRight);
                            PatientWebSetting.SocialWidgetLeft = Convert.ToInt16(results.SocialWidgetLeft);
                            PatientWebSetting.SocialWidgetRight = Convert.ToInt16(results.SocialWidgetRight);
                            PatientWebSetting.VitalSignsWidgetLeft = Convert.ToInt16(results.VitalSignsWidgetLeft);
                            PatientWebSetting.VitalSignsWidgetRight = Convert.ToInt16(results.VitalSignsWidgetRight);
                            PatientWebSetting.PremiumWidgetLeft = Convert.ToInt16(results.PremiumWidgetLeft);
                            PatientWebSetting.PremiumWidgetRight = Convert.ToInt16(results.PremiumWidgetRight);
                            PatientWebSetting.StatementWidgetLeft = Convert.ToInt16(results.StatementWidgetLeft);
                            PatientWebSetting.StatementWidgetRight = Convert.ToInt16(results.StatementWidgetRight);
                            PatientWebSetting.DocumentWidgetLeft = Convert.ToInt16(results.DocumentWidgetLeft);
                            PatientWebSetting.DocumentWidgetRight = Convert.ToInt16(results.DocumentWidgetRight);
                            PatientWebSetting.FamilyWidgetLeft = Convert.ToInt16(results.FamilyWidgetLeft);
                            PatientWebSetting.FamilyWidgetRight = Convert.ToInt16(results.FamilyWidgetRight);
                            PatientWebSetting.PlanOfCareWidgetLeft = Convert.ToInt16(results.PlanOfCareWidgetLeft);
                            PatientWebSetting.PlanOfCareWidgetRight = Convert.ToInt16(results.PlanOfCareWidgetRight);
                            PatientWebSetting.ClinicalInstructionsWidgetLeft = Convert.ToInt16(results.ClinicalInstructionsWidgetLeft);
                            PatientWebSetting.ClinicalInstructionsWidgetRight = Convert.ToInt16(results.ClinicalInstructionsWidgetRight);
                            PatientWebSetting.ProviderWidgetLeft = Convert.ToInt16(results.ProviderWidgetLeft);
                            PatientWebSetting.ProviderWidgetRight = Convert.ToInt16(results.ProviderWidgetRight);

                            PatientWebSetting.EmailNotifyNewMessage = Convert.ToBoolean(results.EmailNotifyNewMessage);
                            PatientWebSetting.TextNotifyNewMesssage = Convert.ToBoolean(results.TextNotifyNewMesssage);
                            PatientWebSetting.CellCarrier = Convert.ToInt32(results.CellCarrier);

                        }
                        else
                        {
                            // Add Patient Web Settings - First Use
                            var NewWebSetting = new PatientWebSetting()
                            {

                                PatientId = Parms.PatientId,
                                VisitWidgetLeft = 2,
                                VisitWidgetRight = 0,
                                AllergyWidgetLeft = 0,
                                AllergyWidgetRight = 3,
                                AppointmentsWidgetLeft = 7,
                                AppointmentsWidgetRight = 0,
                                FamilyWidgetLeft = 4,
                                FamilyWidgetRight = 0,
                                ImmunizationWidgetLeft = 4,
                                ImmunizationWidgetRight = 0,
                                InsuranceWidgetLeft = 8,
                                InsuranceWidgetRight = 0,
                                LabTestWidgetLeft = 0,
                                LabTestWidgetRight = 5,
                                MedicationWidgetLeft = 0,
                                MedicationWidgetRight = 2,
                                PastMedicalWidgetLeft = 5,
                                PastMedicalWidgetRight = 0,
                                ProblemWidgetLeft = 0,
                                ProblemWidgetRight = 1,
                                ProcedureWidgetLeft = 6,
                                ProcedureWidgetRight = 0,
                                SocialWidgetLeft = 3,
                                SocialWidgetRight = 0,
                                VitalSignsWidgetLeft = 0,
                                VitalSignsWidgetRight = 6,
                                PremiumWidgetLeft = 8,
                                PremiumWidgetRight = 0,
                                StatementWidgetLeft = 0,
                                StatementWidgetRight = 0,
                                DocumentWidgetLeft  = 9,
                                DocumentWidgetRight = 0,
                                PlanOfCareWidgetLeft = 0,
                                PlanOfCareWidgetRight = 7,
                                ClinicalInstructionsWidgetLeft = 0,
                                ClinicalInstructionsWidgetRight = 8,
                                ProviderWidgetLeft = 1,
                                ProviderWidgetRight = 0,
                                EmailNotifyNewMessage = true,
                                PictureLocation = "",
                                TextNotifyNewMesssage = false,
                                CellCarrier = 0,
                            };
                            db.PatientWebSettings.Add(NewWebSetting);
                            db.SaveChanges();
                            

                            // Set Value To Defaults
                            PatientWebSetting.Valid = true;
                            PatientWebSetting.ErrorMessage = "";
                            PatientWebSetting.PatientId = results.PatientId;
                            PatientWebSetting.VisitWidgetLeft = 0;
                            PatientWebSetting.VisitWidgetRight = 7;
                            PatientWebSetting.AllergyWidgetLeft = 2;
                            PatientWebSetting.AllergyWidgetRight = 0;
                            PatientWebSetting.AppointmentsWidgetLeft = 1;
                            PatientWebSetting.AppointmentsWidgetRight = 0;
                            PatientWebSetting.ClinicalDocWidgetLeft = 3;
                            PatientWebSetting.ClinicalDocWidgetRight = 0;
                            PatientWebSetting.FamilyWidgetLeft = 4;
                            PatientWebSetting.FamilyWidgetRight = 0;
                            PatientWebSetting.ImmunizationWidgetLeft = 5;
                            PatientWebSetting.ImmunizationWidgetRight = 0;
                            PatientWebSetting.InsuranceWidgetLeft = 0;
                            PatientWebSetting.InsuranceWidgetRight = 0;
                            PatientWebSetting.LabTestWidgetLeft = 0;
                            PatientWebSetting.LabTestWidgetRight = 1;
                            PatientWebSetting.MedicationWidgetLeft = 0;
                            PatientWebSetting.MedicationWidgetRight = 2;
                            PatientWebSetting.PastMedicalWidgetLeft = 0;
                            PatientWebSetting.PastMedicalWidgetRight = 3;
                            PatientWebSetting.ProblemWidgetLeft = 0;
                            PatientWebSetting.ProblemWidgetRight = 4;
                            PatientWebSetting.ProcedureWidgetLeft = 0;
                            PatientWebSetting.ProcedureWidgetRight = 5;
                            PatientWebSetting.SocialWidgetLeft = 6;
                            PatientWebSetting.SocialWidgetRight = 0;
                            PatientWebSetting.SurgicalWidgetLeft = 7;
                            PatientWebSetting.SurgicalWidgetRight = 0;
                            PatientWebSetting.VitalSignsWidgetLeft = 0;
                            PatientWebSetting.VitalSignsWidgetRight = 6;
                            PatientWebSetting.PremiumWidgetLeft = 8;
                            PatientWebSetting.PremiumWidgetRight = 0;
                            PatientWebSetting.StatementWidgetLeft = 0;
                            PatientWebSetting.StatementWidgetRight = 0;
                            PatientWebSetting.DocumentWidgetLeft = 0;
                            PatientWebSetting.DocumentWidgetRight = 8;
                            PatientWebSetting.PlanOfCareWidgetLeft = 9;
                            PatientWebSetting.PlanOfCareWidgetRight = 0;
                            PatientWebSetting.ClinicalInstructionsWidgetLeft = 0;
                            PatientWebSetting.ClinicalInstructionsWidgetRight = 9;
                            PatientWebSetting.ProviderWidgetLeft = 1;
                            PatientWebSetting.ProviderWidgetRight = 0;

                            PatientWebSetting.EmailNotifyNewMessage = true;
                            PatientWebSetting.TextNotifyNewMesssage = false;
                            PatientWebSetting.CellCarrier = 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientWebSetting.Valid = false;
                    PatientWebSetting.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientWebSetting.Valid = false;
                PatientWebSetting.ErrorMessage = "Invalid Token";
            }
            return PatientWebSetting;
        }
        #endregion

        #region Save PatientWebSetting Data
        //------------------------------------------------------------------------
        // Save PatientWebSetting Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientWebSetting Data")]
        public PatientWebSettingData SavePatientWebSettingData(PatientWebSettingData PatientWebSettingData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientWebSettingData.Valid = true;
            PatientWebSettingData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Update PatientWebSetting Info 

                        PatientWebSetting PatientResp = db.PatientWebSettings.FirstOrDefault(p => p.PatientId == PatientWebSettingData.PatientId);

                        if (PatientResp != null)
                        {
                            //Added By Talha
                            if (PatientWebSettingData.Option == 1)
                            {
                                PatientResp.TextNotifyNewMesssage = PatientWebSettingData.TextNotifyNewMesssage;
                                PatientResp.CellCarrier = PatientWebSettingData.CellCarrier;
                                PatientResp.EmailNotifyNewMessage = PatientWebSettingData.EmailNotifyNewMessage;
                            }
                            else
                            {
                                PatientResp.VisitWidgetLeft = PatientWebSettingData.VisitWidgetLeft;
                                PatientResp.VisitWidgetRight = PatientWebSettingData.VisitWidgetRight;
                                PatientResp.AllergyWidgetLeft = PatientWebSettingData.AllergyWidgetLeft;
                                PatientResp.AllergyWidgetRight = PatientWebSettingData.AllergyWidgetRight;
                                PatientResp.AppointmentsWidgetLeft = PatientWebSettingData.AppointmentsWidgetLeft;
                                PatientResp.AppointmentsWidgetRight = PatientWebSettingData.AppointmentsWidgetRight;
                                PatientResp.FamilyWidgetLeft = PatientWebSettingData.FamilyWidgetLeft;
                                PatientResp.FamilyWidgetRight = PatientWebSettingData.FamilyWidgetRight;
                                PatientResp.ImmunizationWidgetLeft = PatientWebSettingData.ImmunizationWidgetLeft;
                                PatientResp.ImmunizationWidgetRight = PatientWebSettingData.ImmunizationWidgetRight;
                                PatientResp.InsuranceWidgetLeft = PatientWebSettingData.InsuranceWidgetLeft;
                                PatientResp.InsuranceWidgetRight = PatientWebSettingData.InsuranceWidgetRight;
                                PatientResp.LabTestWidgetLeft = PatientWebSettingData.LabTestWidgetLeft;
                                PatientResp.LabTestWidgetRight = PatientWebSettingData.LabTestWidgetRight;
                                PatientResp.MedicationWidgetLeft = PatientWebSettingData.MedicationWidgetLeft;
                                PatientResp.MedicationWidgetRight = PatientWebSettingData.MedicationWidgetRight;
                                PatientResp.PastMedicalWidgetLeft = PatientWebSettingData.PastMedicalWidgetLeft;
                                PatientResp.PastMedicalWidgetRight = PatientWebSettingData.PastMedicalWidgetRight;
                                PatientResp.ProblemWidgetLeft = PatientWebSettingData.ProblemWidgetLeft;
                                PatientResp.ProblemWidgetRight = PatientWebSettingData.ProblemWidgetRight;
                                PatientResp.ProcedureWidgetLeft = PatientWebSettingData.ProcedureWidgetLeft;
                                PatientResp.ProcedureWidgetRight = PatientWebSettingData.ProcedureWidgetRight;
                                PatientResp.SocialWidgetLeft = PatientWebSettingData.SocialWidgetLeft;
                                PatientResp.SocialWidgetRight = PatientWebSettingData.SocialWidgetRight;
                                PatientResp.VitalSignsWidgetLeft = PatientWebSettingData.VitalSignsWidgetLeft;
                                PatientResp.VitalSignsWidgetRight = PatientWebSettingData.VitalSignsWidgetRight;
                                PatientResp.StatementWidgetLeft = PatientWebSettingData.StatementWidgetLeft;
                                PatientResp.StatementWidgetRight = PatientWebSettingData.StatementWidgetRight;
                                PatientResp.DocumentWidgetLeft = PatientWebSettingData.DocumentWidgetLeft;
                                PatientResp.DocumentWidgetRight = PatientWebSettingData.DocumentWidgetRight;
                                PatientResp.PlanOfCareWidgetLeft = PatientWebSettingData.PlanOfCareWidgetLeft;
                                PatientResp.PlanOfCareWidgetRight = PatientWebSettingData.PlanOfCareWidgetRight;
                                PatientResp.ClinicalInstructionsWidgetLeft = PatientWebSettingData.ClinicalInstructionsWidgetLeft;
                                PatientResp.ClinicalInstructionsWidgetRight = PatientWebSettingData.ClinicalInstructionsWidgetRight;
                                PatientResp.ProviderWidgetLeft = PatientWebSettingData.ProviderWidgetLeft;
                                PatientResp.ProviderWidgetRight = PatientWebSettingData.ProviderWidgetRight;
                                PatientResp.TextNotifyNewMesssage = PatientWebSettingData.TextNotifyNewMesssage;
                                PatientResp.CellCarrier = PatientWebSettingData.CellCarrier;

                                PatientResp.EmailNotifyNewMessage = PatientWebSettingData.EmailNotifyNewMessage;
                            }
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientWebSettingData.Valid = false;
                    PatientWebSettingData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientWebSettingData.Valid = false;
                PatientWebSettingData.ErrorMessage = "Invalid Token";
            }
            return PatientWebSettingData;
        }
        #endregion


        #region Get Patient Image 
        //------------------------------------------------------------------------
        // Get Patient Image
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Image")]
        public PatientImageData GetPatientImageData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientImageData ImageData = new PatientImageData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {

                    using (var db = new AMREntities())
                    {
                        PatientWebSetting results = db.PatientWebSettings.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            string Filename = results.PictureLocation;

                            if (Filename != "" && Filename != null)
                            {
                                ImageData.Image = FileHelper.DiskToBytes(Filename);
                                ImageData.Valid = true;
                            }
                            else
                            {
                                ImageData.Valid = false;
                                ImageData.ErrorMessage = "No Image Available.";
                            }
                        }
                        else
                        {
                            ImageData.Valid = false;
                            ImageData.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImageData.Valid = false;
                    ImageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ImageData.Valid = false;
                ImageData.ErrorMessage = "Invalid Token";
            }
            return ImageData;
        }
        #endregion

        #region Save Patient Image
        //------------------------------------------------------------------------
        // Save Patient Image
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Patient Image")]
        public PatientImageData SavePatientmageData(PatientImageData ImageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            ImageData.Valid = true;
            ImageData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Check if image already exists 
                        PatientWebSetting results = db.PatientWebSettings.FirstOrDefault(p => p.PatientId == ImageData.PatientId);

                        if (results != null)
                        {
                            string Filename = results.PictureLocation;

                            if (Filename != "" && Filename != null)
                            {
                                // Already has an image, replace at the current location.

                                // Delete Old Image
                                FileHelper.DeleteFile(Filename);

                                // Check that extension is correct

                                int posn = Filename.IndexOf(".");
                                if (posn > 0)
                                    Filename = Filename.Substring(0, posn + 1) + ImageData.ImageFormat;
                                results.PictureLocation = Filename;
                                db.SaveChanges();
                            }
                            else
                            {
                                // New Image, get file location

                                // Get Attachment Folder Info & Update Counters
                                ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 4);

                                // Update Attachment Counter
                                if (Config.CurrentFolderDocCntr > 1000)
                                {
                                    Config.AttachmentFolderCntr++;
                                    Config.CurrentFolderDocCntr = 0;
                                }
                                Config.CurrentFolderDocCntr++;
                                db.SaveChanges();
                                Filename = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + ImageData.PatientId + "." + ImageData.ImageFormat;
                                // Make sure folder exists
                                FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);

                                results.PictureLocation = Filename;
                                db.SaveChanges();
                            }

                            // Write the document to the hard disk
                            
                            FileHelper.BytesToDisk(ImageData.Image, Filename);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImageData.Valid = false;
                    ImageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ImageData.Valid = false;
                ImageData.ErrorMessage = "Invalid Token";
            }
            // Clear picture before returning response
            ImageData.Image = null;
            return ImageData;
        }
        #endregion


        #region Get Patient Representative
        //------------------------------------------------------------------------
        // Get Patient Representative
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Representative Data")]
        public PatientRepData GetPatientRepData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientRepData PatientRepData = new PatientRepData();
            PatientRepData.Valid = true;
            PatientRepData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        PatientRepresentative results = db.PatientRepresentatives.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            PatientRepData.Valid = true;
                            PatientRepData.ErrorMessage = "";
                            PatientRepData.PatientId = results.PatientId;
                            PatientRepData.FirstName = results.FirstName;
                            PatientRepData.LastName = results.LastName;
                            PatientRepData.Enabled = Convert.ToBoolean(results.Enabled);
                            PatientRepData.Demographics = Convert.ToBoolean(results.Demographics);
                            PatientRepData.Allergy = Convert.ToBoolean(results.Allergy);
                            PatientRepData.FamilyHistory = Convert.ToBoolean(results.FamilyHistory);
                            PatientRepData.LabResults = Convert.ToBoolean(results.LabResults);
                            PatientRepData.MedicalHistory = Convert.ToBoolean(results.MedicalHistory);
                            PatientRepData.Medication = Convert.ToBoolean(results.Medication);
                            PatientRepData.Problem = Convert.ToBoolean(results.Problem);
                            PatientRepData.SocialHistory = Convert.ToBoolean(results.SocialHistory);
                            PatientRepData.SurgicalHistory = Convert.ToBoolean(results.SurgicalHistory);
                            PatientRepData.VitalSigns = Convert.ToBoolean(results.VitalSigns);
                            PatientRepData.Immunization = Convert.ToBoolean(results.Immunization);
                            PatientRepData.Organ = Convert.ToBoolean(results.Organ);
                            PatientRepData.ClinicalDoc = Convert.ToBoolean(results.ClinicalDoc);
                            PatientRepData.Insurance = Convert.ToBoolean(results.Insurance);
                            PatientRepData.EmergencyContact = Convert.ToBoolean(results.EmergencyContact);
                            PatientRepData.Appointment = Convert.ToBoolean(results.Appointment);
                            PatientRepData.Visit = Convert.ToBoolean(results.Visit);
                            PatientRepData.UploadDocs = Convert.ToBoolean(results.UploadDocs);
                            PatientRepData.PlanOfCare = Convert.ToBoolean(results.PlanOfCare);
                            PatientRepData.Messaging = Convert.ToBoolean(results.Messaging);
                            PatientRepData.DownloadTransmit = Convert.ToBoolean(results.DownloadTransmit);
                            PatientRepData.ClinicalSummary = Convert.ToBoolean(results.ClinicalSummary);
                            PatientRepData.Procedure = Convert.ToBoolean(results.Procedure);   //Added by Talha Amin
                            PatientRepData.Provider = Convert.ToBoolean(results.Provider);  // SJF Added 1/20/15

                            // Get Email Address
                            string RepId = "R" + PatientRepData.PatientId.ToString();

                            User UserResp = db.Users.FirstOrDefault(u => u.UserLogin == RepId);

                            if (UserResp != null)
                            {
                                PatientRepData.EMail = UserResp.UserEmail;
                            }
                        }
                        else
                        {
                            PatientRepData.Valid = true;
                            PatientRepData.ErrorMessage = "";
                            PatientRepData.PatientId = results.PatientId;
                            PatientRepData.FirstName = "";
                            PatientRepData.LastName = "";
                            PatientRepData.Enabled = true;
                            PatientRepData.Demographics = false;
                            PatientRepData.Allergy = false;
                            PatientRepData.FamilyHistory = false;
                            PatientRepData.LabResults = false;
                            PatientRepData.MedicalHistory = false;
                            PatientRepData.Medication = false;
                            PatientRepData.Problem = false;
                            PatientRepData.SocialHistory = false;
                            PatientRepData.SurgicalHistory = false;
                            PatientRepData.VitalSigns = false;
                            PatientRepData.Immunization = false;
                            PatientRepData.Organ = false;
                            PatientRepData.ClinicalDoc = false;
                            PatientRepData.Insurance = false;
                            PatientRepData.EmergencyContact = false;
                            PatientRepData.Appointment = false;
                            PatientRepData.Visit = false;
                            PatientRepData.UploadDocs = false;
                            PatientRepData.PlanOfCare = false;
                            PatientRepData.Messaging = false;
                            PatientRepData.DownloadTransmit = false;
                            PatientRepData.ClinicalSummary = false;
                            PatientRepData.Procedure = false;  //Added by Talha Amin
                            PatientRepData.Provider = false;   // SJF Added 1/20/15
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientRepData.Valid = false;
                    PatientRepData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientRepData.Valid = false;
                PatientRepData.ErrorMessage = "Invalid Token";
            }
            return PatientRepData;
        }
        #endregion

        #region Save Patient Representative
        //------------------------------------------------------------------------
        // Save Patient Representative
        //------------------------------------------------------------------------
        [WebMethod(Description = "Save Patient Representative Data")]
        public PatientRepData SavePatientRepData(PatientRepData PatientRepData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientRepData.Valid = true;
            PatientRepData.ErrorMessage = "";
            bool SendEmail = false;
            string passclear = string.Empty;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Get The Patient Id


                        string RepId = "R" + PatientRepData.PatientId.ToString();

                        // Make sure that there is not already a representative created for this account.
                        User UserResp = db.Users.FirstOrDefault(u => u.UserLogin == RepId);

                        if (UserResp != null)
                        {
                            if (UserResp.UserEmail != PatientRepData.EMail)
                            {
                                // Email has changed, most likely assigned to a new person.
                                SendEmail = true;
                                UserResp.UserEmail = PatientRepData.EMail;
                                // Generate a random password and Encrypt it
                                Random randomNumber = new Random();
                                for (int i = 0; i < 8; i++)
                                {
                                    passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                }
                                string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");
                                UserResp.Password = passencr;
                                UserResp.Enabled = true;
                                UserResp.Locked = false;

                            }
                            // Get the Patient Rep Record
                            PatientRepresentative PatientRepResp = db.PatientRepresentatives.FirstOrDefault(p => p.UserId == UserResp.UserId);
                            if (PatientRepResp != null)
                            {
                                // Update Patient Rep
                                PatientRepResp.FirstName = PatientRepData.FirstName;
                                PatientRepResp.LastName = PatientRepData.LastName;
                                PatientRepResp.Enabled = PatientRepData.Enabled;
                                PatientRepResp.Demographics = PatientRepData.Demographics;
                                PatientRepResp.Allergy = PatientRepData.Allergy;
                                PatientRepResp.FamilyHistory = PatientRepData.FamilyHistory;
                                PatientRepResp.LabResults = PatientRepData.LabResults;
                                PatientRepResp.MedicalHistory = PatientRepData.MedicalHistory;
                                PatientRepResp.Medication = PatientRepData.Medication;
                                PatientRepResp.Problem = PatientRepData.Problem;
                                PatientRepResp.SocialHistory = PatientRepData.SocialHistory;
                                PatientRepResp.SurgicalHistory = PatientRepData.SurgicalHistory;
                                PatientRepResp.VitalSigns = PatientRepData.VitalSigns;
                                PatientRepResp.Immunization = PatientRepData.Immunization;
                                PatientRepResp.Organ = PatientRepData.Organ;
                                PatientRepResp.ClinicalDoc = PatientRepData.ClinicalDoc;
                                PatientRepResp.Insurance = PatientRepData.Insurance;
                                PatientRepResp.EmergencyContact = PatientRepData.EmergencyContact;
                                PatientRepResp.Appointment = PatientRepData.Appointment;
                                PatientRepResp.Visit = PatientRepData.Visit;
                                PatientRepResp.UploadDocs = PatientRepData.UploadDocs;
                                PatientRepResp.PlanOfCare = PatientRepData.PlanOfCare;
                                PatientRepResp.Messaging = PatientRepData.Messaging;
                                PatientRepResp.DownloadTransmit = PatientRepData.DownloadTransmit;
                                PatientRepResp.ClinicalSummary = PatientRepData.ClinicalSummary;
                                PatientRepResp.Procedure = PatientRepData.Procedure;  //Added by Talha Amin
                                PatientRepResp.Provider = PatientRepData.Provider; // SJF Added 1/20/15
                            }
                            db.SaveChanges();
                        }
                        else
                        {
                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Create User
                            var NewUser = new User()
                            {
                                UserLogin = RepId,
                                UserEmail = PatientRepData.EMail,
                                Password = passencr,
                                UserRoleId = 6,
                                UserRoleLink = PatientRepData.PatientId,
                                Enabled = true,
                                Locked = false,
                                ResetPassword = true,
                            };
                            db.Users.Add(NewUser);

                            db.SaveChanges();

                            // Add Patient Rep
                            var PatientRep = new PatientRepresentative()
                            {
                                UserId = NewUser.UserId,
                                PatientId = PatientRepData.PatientId,
                                FirstName = PatientRepData.FirstName,
                                LastName = PatientRepData.LastName,
                                Enabled = PatientRepData.Enabled,
                                Demographics = PatientRepData.Demographics,
                                Allergy = PatientRepData.Allergy,
                                FamilyHistory = PatientRepData.FamilyHistory,
                                LabResults = PatientRepData.LabResults,
                                MedicalHistory = PatientRepData.MedicalHistory,
                                Medication = PatientRepData.Medication,
                                Problem = PatientRepData.Problem,
                                SocialHistory = PatientRepData.SocialHistory,
                                SurgicalHistory = PatientRepData.SurgicalHistory,
                                VitalSigns = PatientRepData.VitalSigns,
                                Immunization = PatientRepData.Immunization,
                                Organ = PatientRepData.Organ,
                                ClinicalDoc = PatientRepData.ClinicalDoc,
                                Insurance = PatientRepData.Insurance,
                                EmergencyContact = PatientRepData.EmergencyContact,
                                Appointment = PatientRepData.Appointment,
                                Visit = PatientRepData.Visit,
                                UploadDocs = PatientRepData.UploadDocs,
                                PlanOfCare = PatientRepData.PlanOfCare,
                                Messaging = PatientRepData.Messaging,
                                DownloadTransmit = PatientRepData.DownloadTransmit,
                                ClinicalSummary = PatientRepData.ClinicalSummary,
                                Procedure = PatientRepData.Procedure,   //Added by Talha Amin
                                Provider = PatientRepData.Provider  // SJF Added 1/20/15
                            };
                            db.PatientRepresentatives.Add(PatientRep);

                            db.SaveChanges();
                            SendEmail = true;
                        }
                        if (SendEmail)
                        {
                            // Get Patient's Name
                            string PatientName = "";
                            Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientRepData.PatientId);

                            if (PatientResp != null)
                            {
                                PatientName = PatientResp.FirstName + " " + PatientResp.LastName;
                            }


                            // Send Email to new patient rep with account login information.

                            clsEmail objEmail = new clsEmail();
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;


                            string WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
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
                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientRepData.FirstName + " " + PatientRepData.LastName + "</strong>,</h1>  <br />" +
                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                "               You have been added as a Patient Representative to  " + PatientName + "'s AMR Patient Portal. Below you will find your AccessID Code to log in to your portal account. <br /><br />" +
                                                "               Your AccessID Code:   <strong>" + RepId + "</strong> <br /><br />" +
                                                "               You will also be receiving an email with your temporary password, which you will need to change once you log in. " +
                                                "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account.<br /><br /> " +
                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                "               Thank you, <br />" +
                                                "               Your Member Services Team<br />" +
                                                "               <br /><br />" +
                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                "               </span>" +
                                                "             </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "      </td>" +
                                                "   </tr>" +
                                                "	<tr style=\"background-color:#37b74a;\">" +
                                                "      <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "            <tr style=height:10px;>" +
                                                "            <td></td>" +
                                                "            </tr>" +
                                                "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                "              </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "          <br /><br />" +
                                                "       </td>  " +
                                                "     </tr>" +
                                                "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                "</table>";

                            objEmail.SendEmailHTML(PatientRepData.EMail, "AMR Patient Portal - AccessID Code", WelcomeMsg);

                            WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
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
                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientRepData.FirstName + " " + PatientRepData.LastName + "</strong>,</h1>  <br />" +
                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                "               Below is your temporary password.<br /><br />" +
                                                "               Your Temporary Password:   <strong>" + passclear + "</strong> <br /><br />" +
                                                "               Using the credentials provided in the previous email, " +
                                                "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to reset your temporary password and complete your secure enrollment. <br /><br /> " +
                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                "               Thank you, <br />" +
                                                "               Your Member Services Team<br /><br />" +
                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                "               </span>" +
                                                "             </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "      </td>" +
                                                "   </tr>" +
                                                "	<tr style=\"background-color:#37b74a;\">" +
                                                "      <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "            <tr style=height:10px;>" +
                                                "            <td></td>" +
                                                "            </tr>" +
                                                "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                "              </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "          <br /><br />" +
                                                "       </td>  " +
                                                "     </tr>" +
                                                "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                "</table>";

                            objEmail.SendEmailHTML(PatientRepData.EMail, "AMR Patient Portal - Temporary Password", WelcomeMsg);
                        }
                    }
                }

                catch (Exception ex)
                {
                    PatientRepData.Valid = false;
                    PatientRepData.ErrorMessage = ex.Message;
                }
            }

            return PatientRepData;
        }
        #endregion

        #region Get Care Provider 
        //------------------------------------------------------------------------
        // Get Care Provider
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Care Provider")]

        public CareProviderData GetCareProviderData(PatientParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            CareProviderData CareProviderData = new CareProviderData();
            CareProviderData.Valid = true;
            CareProviderData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    string CareProvLogin = "C" + Parms.PatientId.ToString();

                    using (var db = new AMREntities())
                    {
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == CareProvLogin);

                        if (UserResults != null)
                        {
                            CareProviderData.Password = clsEncryption.Decrypt(UserResults.Password.Trim(), "AMRP@ss");
                        }
                        else
                        {
                            CareProviderData.Valid = false;
                            CareProviderData.ErrorMessage = "Invalid Login Id";
                        }
                    }
                }

                catch (Exception ex)
                {
                    CareProviderData.Valid = false;
                    CareProviderData.ErrorMessage = ex.Message;
                }

            }
            else
            {
                // Invalid Token
                CareProviderData.Valid = false;
                CareProviderData.ErrorMessage = "Invalid Token";
            }
            return CareProviderData;
        }
        #endregion

        #region Save Care Provider
        //------------------------------------------------------------------------
        // Save Care Provider
        //------------------------------------------------------------------------
        [WebMethod(Description = "Save Care Provider Data")]
        public CareProviderData SaveCareProviderData(CareProviderData CareProviderData, string Token, Int64 UserId, Int64 FacilityId)
        {
            CareProviderData.Valid = true;
            CareProviderData.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Get The UserLogin Id

                        string CareProvLogin = "C" + CareProviderData.PatientId.ToString();

                        string passencr = clsEncryption.Encrypt(CareProviderData.Password, "AMRP@ss");

                        // Check if there is not already a care provider created for this account.
                        
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == CareProvLogin);

                        if (UserResults != null)
                        {
                            // Care Provider User Login Already Exists,  Update User Record
                            UserResults.Password = passencr;
                            UserResults.LastPasswordChange = System.DateTime.Now;
                            UserResults.Locked = false;
                            UserResults.Enabled = true;
                            db.SaveChanges();

                           
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserResults.UserLogin,
                                LoginType = 7,          // Care Provider
                                AccessStatusId = 9,     // Password Reset
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                        }
                        else
                        {

                            // Create User
                            var NewUser = new User()
                            {
                                UserLogin = CareProvLogin,
                                UserEmail = CareProviderData.PatientFullName,
                                Password = passencr,
                                UserRoleId = 7,          // Care Provider
                                UserRoleLink = CareProviderData.PatientId,
                                Enabled = true,
                                Locked = false,
                                ResetPassword = true,
                            };
                            db.Users.Add(NewUser);

                            db.SaveChanges();
                        }
                    }
                }

                catch (Exception ex)
                {
                    CareProviderData.Valid = false;
                    CareProviderData.ErrorMessage = ex.Message;
                }
            }

            return CareProviderData;
        }
        #endregion


        #region Get List Of Patients For Facility
        //------------------------------------------------------------------------
        // Get List Of Patients For Facility
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Patients For Facility")]
        public PatientTableData GetPatientForFacilityList(Int64 CkFacilityId, string FirstName, string LastName, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientTableData PatientList = new PatientTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.vwPatientsForFacilities
                                      where p.FacilityId == CkFacilityId
                                      && (p.FirstName.Contains(FirstName)
                                      || p.LastName.Contains(LastName))
                                      select new
                                      {
                                          p.PatientId,
                                          p.FirstName,
                                          p.LastName,
                                          p.Address1,
                                          p.DOB
                                      };
                        PatientList.dt = clsTableConverter.ToDataTable(results);

                        PatientList.dt.TableName = "Patients";

                        PatientList.Valid = true;
                        PatientList.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientList.Valid = false;
                    PatientList.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientList.Valid = false;
                PatientList.ErrorMessage = "Invalid Token";
            }
            return PatientList;
        }
        #endregion

        #region Get List Of Patients Facility Options
        //------------------------------------------------------------------------
        // Get List Of Patients Facility Options
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Patients Facility Options")]
        public PatientFacilityOptionsData GetPatientFacilityOptions(Int64 CkPatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientFacilityOptionsData OptionsData = new PatientFacilityOptionsData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {

                        vwPatientFacilityOption Results = db.vwPatientFacilityOptions.FirstOrDefault(p => p.PatientId == CkPatientId);

                        if (Results != null)
                        {
                            OptionsData.PatientId = Results.PatientId;
                            OptionsData.GeneralMessageAvailable = Convert.ToBoolean(Results.GeneralMessageAvailable);
                            OptionsData.AppointmentMessageAvailable = Convert.ToBoolean(Results.AppointmentMessageAvailable);
                            OptionsData.MedicationMessageAvailable = Convert.ToBoolean(Results.MedicationMessageAvailable);
                            OptionsData.ReferralMessageAvailable = Convert.ToBoolean(Results.ReferralMessageAvailable);
                            OptionsData.PremiumAvailable = Convert.ToBoolean(Results.PremiumAvailable);
                            if (Results.ReferralMessageAvailable > 0)
                                OptionsData.ReferralMessageAvailable = true;
                            else
                                OptionsData.ReferralMessageAvailable = false;
                        }
                        else
                        {
                            OptionsData.Valid = false;
                            OptionsData.ErrorMessage = "Invalid Login Id";
                        }
                        
                        OptionsData.Valid = true;
                        OptionsData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    OptionsData.Valid = false;
                    OptionsData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                OptionsData.Valid = false;
                OptionsData.ErrorMessage = "Invalid Token";
            }
            return OptionsData;
        }
        #endregion

        #region Cleaner
        //------------------------------------------------------------------------
        // Clean String Removing Extra Characters.
        //------------------------------------------------------------------------
        private string Cleaner(string InString)
        {
            if (InString == null)
                InString = "";
            InString = InString.ToLower();
            string OutString = "";
            foreach(char c in InString)
            {
              if (Char.IsLetter(c))
                  OutString = OutString + c.ToString();
            }
            return OutString;
        }
        #endregion


        #region Send Outside Provider Email
        //------------------------------------------------------------------------
        // Send Outside Provider Email
        //------------------------------------------------------------------------

        [WebMethod(Description = "Send Outside Provider Email")]
        public PatientResp SendOutsideProviderEmail(CareProviderEmail CPData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp resp = new PatientResp();
            resp.Valid = true;
            resp.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    // Send Email to new patient with account login information.

                    clsEmail objEmail = new clsEmail();

                    string Msg ="<center><font size=5><b>Care Provider Label Info</b></font></center>"
                        + "Below is a label template that you can copy & paste to a word document which you can print using your label option. We recommend using Avery Label 5163 (2\"x 4\") size label."
                        + "You can print this information and give it to your Doctor or Care Provider to have it affixed to your chart at their office for their reference."
                        + "<br><br>" 
                        + "<table>"
                        + "<tr>"
                        + "<td style=\"width:150px;\"></td>"
                        + "<td style=\"width:400px;\">"
                        + "<center>"
                        + "<div>"
                        + "<br>"
                        + "<strong>Care Provider AMR Label</strong>"
                        + "<br><br>"
                        + "<strong>Name: </strong>" +  CPData.PatientName
                        + "<br>"
                        + "<strong>AccessID Code: </strong>" + CPData.PatientId
                        + "<br>"
                        + "<strong>Care Provider Pass Code: </strong>" + CPData.Password
                        + "<br><br>"
                        + "Visit:<strong> www.amrpatientportal.com</strong><br>"
                        + "Click on Medical Care Provider Login<br>"
                        + "Enter information above<br><br>"
                        + "</div>"
                        + "</center>"
                        + "</td>"
                        + "</tr>"
                        + "</table>";


                    objEmail.SendEmailHTML(CPData.EmailAddress, CPData.PatientName + " upload information", Msg);

                }

                catch (Exception ex)
                {
                    resp.Valid = false;
                    resp.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                resp.Valid = false;
                resp.ErrorMessage = "Invalid Token";
            }
            return resp;
        }
        #endregion



        #region Get BillRate Data
        //------------------------------------------------------------------------
        // Get BillRate Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BillRate Data")]
        public BillRateData GetBillRateData(BillRateParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            BillRateData BillRate = new BillRateData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        BillRate results = db.BillRates.FirstOrDefault(b => b.BillRateId == Parms.BillRateId);

                        if (results != null)
                        {
                            BillRate.Valid = true;
                            BillRate.ErrorMessage = "";
                            BillRate.BillRateId = results.BillRateId;
                            BillRate.PromoCode = results.PromoCode;
                            BillRate.Active = Convert.ToBoolean(results.Active);
                            BillRate.IsFree = Convert.ToBoolean(results.IsFree);
                            BillRate.Rate = Convert.ToDecimal(results.Rate);
                            BillRate.Renewal = Convert.ToDecimal(results.Renewal);
                            BillRate.Residual = Convert.ToDecimal(results.Residual);
                            BillRate.FreeMonths = Convert.ToInt32(results.FreeMonths);
                            BillRate.UserId_Created = Convert.ToInt64(results.UserId_Created);
                            BillRate.DateCreated = results.DateCreated.ToString();
                            BillRate.UserId_Modified = Convert.ToInt64(results.UserId_Modified);
                            BillRate.DateModified = results.DateModified.ToString();
                        }
                        else
                        {
                            BillRate.Valid = false;
                            BillRate.ErrorMessage = "Record does not exist";
                        }
                    }
                }
                catch (Exception ex)
                {
                    BillRate.Valid = false;
                    BillRate.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                BillRate.Valid = false;
                BillRate.ErrorMessage = "Invalid Token";
            }
            return BillRate;
        }
        #endregion

        #region Get BillRate Data By Promo Code
        //------------------------------------------------------------------------
        // Get BillRate Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BillRate Data By Promo Code")]
        public BillRateData GetBillRateDataByPromo(BillRateParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            BillRateData BillRate = new BillRateData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        BillRate results = db.BillRates.FirstOrDefault(b => b.PromoCode == Parms.PromoCode);

                        if (results != null)
                        {
                            BillRate.Valid = true;
                            BillRate.ErrorMessage = "";
                            BillRate.BillRateId = results.BillRateId;
                            BillRate.PromoCode = results.PromoCode;
                            BillRate.Active = Convert.ToBoolean(results.Active);
                            BillRate.IsFree = Convert.ToBoolean(results.IsFree);
                            BillRate.Rate = Convert.ToDecimal(results.Rate);
                            BillRate.Renewal = Convert.ToDecimal(results.Renewal);
                            BillRate.Residual = Convert.ToDecimal(results.Residual);
                            BillRate.FreeMonths = Convert.ToInt32(results.FreeMonths);
                            BillRate.UserId_Created = Convert.ToInt64(results.UserId_Created);
                            BillRate.DateCreated = results.DateCreated.ToString();
                            BillRate.UserId_Modified = Convert.ToInt64(results.UserId_Modified);
                            BillRate.DateModified = results.DateModified.ToString();
                        }
                        else
                        {
                            BillRate.Valid = false;
                            BillRate.ErrorMessage = "Record does not exist";
                        }
                    }
                }
                catch (Exception ex)
                {
                    BillRate.Valid = false;
                    BillRate.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                BillRate.Valid = false;
                BillRate.ErrorMessage = "Invalid Token";
            }
            return BillRate;
        }
        #endregion

        #region Save BillRate Data
        //------------------------------------------------------------------------
        // Save BillRate Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save BillRate Data")]
        public BillRateData SaveBillRateData(BillRateData BillRate, string Token, Int64 UserId, Int64 FacilityId)
	    {
            BillRate.Valid = true;
            BillRate.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        if (BillRate.BillRateId == 0)
                        {
                            // Add Item
                            var Bill = new BillRate()
                            {
                                PromoCode = BillRate.PromoCode,
                                Active = BillRate.Active,
                                IsFree = BillRate.IsFree,
                                Rate = BillRate.Rate,
                                Renewal = BillRate.Renewal,
                                Residual = BillRate.Residual,
                                FreeMonths = BillRate.FreeMonths,
                                UserId_Created = BillRate.UserId_Created,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = BillRate.UserId_Modified,
                                DateModified = System.DateTime.Now,
                            };
                            db.BillRates.Add(Bill);
                        }
                        else
                        {
                            BillRate BillRateResp = db.BillRates.FirstOrDefault(b => b.BillRateId == BillRate.BillRateId);

                            if (BillRateResp != null)
                            {
                                BillRateResp.BillRateId = BillRate.BillRateId;
                                BillRateResp.PromoCode = BillRate.PromoCode;
                                BillRateResp.Active = BillRate.Active;
                                BillRateResp.IsFree = BillRate.IsFree;
                                BillRateResp.Rate = BillRate.Rate;
                                BillRateResp.Renewal = BillRate.Renewal;
                                BillRateResp.Residual = BillRate.Residual;
                                BillRateResp.FreeMonths = BillRate.FreeMonths;
                                BillRateResp.UserId_Modified = BillRate.UserId_Modified;
                                BillRateResp.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    BillRate.Valid = false;
                    BillRate.ErrorMessage = ex.Message;
                }
            }
          else
          {
              // Invalid Token
              BillRate.Valid = false;
              BillRate.ErrorMessage = "Invalid Token";
          }
	        return BillRate;
	    }
        #endregion

        #region Get BillRate DataTable
        //------------------------------------------------------------------------
        // Get BillRate DataTable 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BillRate Data Table")]
        public BillRateTableData GetBillRateDataTable(BillRateParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            BillRateTableData BillRate = new BillRateTableData();

           // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from b in db.BillRates
                                      select new
                                      {
                                          b.BillRateId,
                                          b.PromoCode,
                                          b.Active,
                                          b.IsFree,
                                          b.Rate,
                                          b.Renewal,
                                          b.Residual,
                                          b.FreeMonths
                                      };

                        BillRate.dt = clsTableConverter.ToDataTable(results);
                        BillRate.dt.TableName = "PatientProcedure";

                        BillRate.Valid = true;
                        BillRate.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    BillRate.Valid = false;
                    BillRate.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                BillRate.Valid = false;
                BillRate.ErrorMessage = "Invalid Token";
            }
            return BillRate;

        }
        #endregion

        #region Get BillPayment Data
        //------------------------------------------------------------------------
        // Get BillPayment Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BillPayment Data")]
        public BillPaymentData GetBillPaymentData(BillPaymentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            BillPaymentData BillPayment = new BillPaymentData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        BillPayment results = db.BillPayments.FirstOrDefault(b => b.BillPaymentId == Parms.BillPaymentId);

                        if (results != null)
                        {
                            BillPayment.Valid = true;
                            BillPayment.ErrorMessage = "";
                            BillPayment.BillPaymentId = results.BillPaymentId;
                            BillPayment.PatientId = results.PatientId;
                            BillPayment.BillRateId = results.BillRateId;
                            BillPayment.TransactionDate = results.TransactionDate.ToString();
                            BillPayment.Response = results.Response;
                            BillPayment.CustId = results.CustId;
                            BillPayment.RecurrId = results.RecurrId;
                            BillPayment.PaymentType = results.PaymentType;
                            BillPayment.PaymentFrequency = results.PaymentFrequency;
                            BillPayment.PaymentId = results.PaymentId;
                            BillPayment.NoOfPayments = Convert.ToInt32(results.NoOfPayments);
                            BillPayment.StartDate = results.StartDate.ToString();
                            BillPayment.AccountHolderName = results.AccountHolderName;
                            BillPayment.Amount = Convert.ToDecimal(results.Amount);
                            BillPayment.UserId_Created = Convert.ToInt64(results.UserId_Created);
                            BillPayment.DateCreated = results.DateCreated.ToString();
                            BillPayment.UserId_Modified = Convert.ToInt64(results.UserId_Modified);
                            BillPayment.DateModified = results.DateModified.ToString();
                        }
                        else
                        {
                            BillPayment.Valid = false;
                            BillPayment.ErrorMessage = "Can not read record";
                        }
                    }
                }
                catch (Exception ex)
                {
                    BillPayment.Valid = false;
                    BillPayment.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                BillPayment.Valid = false;
                BillPayment.ErrorMessage = "Invalid Token";
            }
            return BillPayment;
        }
        #endregion

        #region Save BillPayment Data
        //------------------------------------------------------------------------
        // Save BillPayment Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save BillPayment Data")]
        public BillPaymentData SaveBillPaymentData(BillPaymentData BillPayment, string Token, Int64 UserId, Int64 FacilityId)
	    {
            BillPayment.Valid = true;
            BillPayment.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        if (BillPayment.BillPaymentId == 0)
                        {
                            // Add Item
                            var Bill = new BillPayment()
                            {
                                PatientId = BillPayment.PatientId,
                                BillRateId = BillPayment.BillRateId,
                                TransactionDate = Convert.ToDateTime(BillPayment.TransactionDate),
                                Response = BillPayment.Response,
                                CustId = BillPayment.CustId,
                                RecurrId = BillPayment.RecurrId,
                                PaymentType = BillPayment.PaymentType,
                                PaymentFrequency = BillPayment.PaymentFrequency,
                                PaymentId = BillPayment.PaymentId,
                                NoOfPayments = BillPayment.NoOfPayments,
                                StartDate = Convert.ToDateTime(BillPayment.StartDate),
                                AccountHolderName = BillPayment.AccountHolderName,
                                Amount = BillPayment.Amount,
                                UserId_Created = BillPayment.UserId_Created,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = BillPayment.UserId_Modified,
                                DateModified = System.DateTime.Now,
                            };
                            db.BillPayments.Add(Bill);
                        }
                        else
                        {
                            BillPayment BillPaymentResp = db.BillPayments.FirstOrDefault(b => b.BillPaymentId == BillPayment.BillPaymentId);

                            if (BillPaymentResp != null)
                            {
                                BillPaymentResp.BillPaymentId = BillPayment.BillPaymentId;
                                BillPaymentResp.PatientId = BillPayment.PatientId;
                                BillPaymentResp.BillRateId = BillPayment.BillRateId;
                                BillPaymentResp.TransactionDate = Convert.ToDateTime(BillPayment.TransactionDate);
                                BillPaymentResp.Response = BillPayment.Response;
                                BillPaymentResp.CustId = BillPayment.CustId;
                                BillPaymentResp.RecurrId = BillPayment.RecurrId;
                                BillPaymentResp.PaymentType = BillPayment.PaymentType;
                                BillPaymentResp.PaymentFrequency = BillPayment.PaymentFrequency;
                                BillPaymentResp.PaymentId = BillPayment.PaymentId;
                                BillPaymentResp.NoOfPayments = BillPayment.NoOfPayments;
                                BillPaymentResp.StartDate = Convert.ToDateTime(BillPayment.StartDate);
                                BillPaymentResp.AccountHolderName = BillPayment.AccountHolderName;
                                BillPaymentResp.Amount = BillPayment.Amount;
                                BillPaymentResp.UserId_Modified = BillPayment.UserId_Modified;
                                BillPaymentResp.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    BillPayment.Valid = false;
                    BillPayment.ErrorMessage = ex.Message;
                }
            }
          else
          {
              // Invalid Token
              BillPayment.Valid = false;
              BillPayment.ErrorMessage = "Invalid Token";
          }
	        return BillPayment;
	    }
        #endregion

        #region Get BillPayment DataTable
        //------------------------------------------------------------------------
        // Get BillPayment DataTable 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BillPayment Data Table")]
        public BillPaymentTableData GetBillPaymentDataTable(BillPaymentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            BillPaymentTableData BillPayment = new BillPaymentTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from b in db.BillPayments
                                      join r in db.BillRates on b.BillRateId equals r.BillRateId
                                      where b.PatientId == Parms.PatientId
                                      select new
                                      {
                                          b.BillPaymentId,
                                          b.PatientId,
                                          b.BillRateId,
                                          b.TransactionDate,
                                          b.Response,
                                          b.CustId,
                                          b.RecurrId,
                                          b.PaymentType,
                                          b.PaymentFrequency,
                                          b.PaymentId,
                                          b.NoOfPayments,
                                          b.StartDate,
                                          b.AccountHolderName,
                                          b.Amount
                                      };

                        BillPayment.dt = clsTableConverter.ToDataTable(results);
                        BillPayment.dt.TableName = "PatientProcedure";

                        BillPayment.Valid = true;
                        BillPayment.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    BillPayment.Valid = false;
                    BillPayment.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                BillPayment.Valid = false;
                BillPayment.ErrorMessage = "Invalid Token";
            }
            return BillPayment;

        }
        #endregion

        #region Patient Email Search
        //------------------------------------------------------------------------
        // Patient Email Search 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Email Search")]
        public PatientTableData PatientEmailSearch(string EMail, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientTableData Patient = new PatientTableData();
            Patient.Valid = true;
            Patient.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.Patients
                                        where p.EMail == EMail
                                        select new
                                        {
                                            p.PatientId,
                                            p.FirstName,
                                            p.LastName,
                                            p.EMail,
                                        };
                        Patient.dt = clsTableConverter.ToDataTable(results);
                        Patient.dt.TableName = "Patients";

                        Patient.Valid = true;
                        Patient.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    Patient.Valid = false;
                    Patient.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Patient.Valid = false;
                Patient.ErrorMessage = "Invalid Token";
            }
            return Patient;
        }
        #endregion


        #region Delete Patient Data
        //------------------------------------------------------------------------
        // Delete Patient Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete Patient Data")]
        public PatientResp DeletePatientData(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp Response = new PatientResp();
            Response.Valid = true;
            Response.ErrorMessage = "";
            //Do to convert long into string because linq does not support it in query
            string UserLogin = Convert.ToString(PatientId);
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        bool finished = false;

                        #region // Update Patient Info
                        User UserResults = db.Users.FirstOrDefault(p => p.UserLogin == UserLogin);
                        Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);
                        PatientShare PatientShareResp = db.PatientShares.FirstOrDefault(p => p.PatientId == PatientId);

                        if (PatientResp != null)
                        {
                            PatientResp.Deleted = true;
                            PatientResp.DateDeleted = System.DateTime.Now;
                            PatientResp.Active = false;
                            PatientResp.UserId_Modified = UserId;
                            PatientResp.DateModified = System.DateTime.Now;
                            db.SaveChanges();
                        }
                        if (UserResults != null)
                        {
                            UserResults.Enabled = false;

                            db.SaveChanges();
                        }
                        if (PatientShareResp != null)
                        {
                            PatientShareResp.Demographics = false;
                            PatientShareResp.Allergy = false;
                            PatientShareResp.FamilyHistory = false;
                            PatientShareResp.LabResults = false;
                            PatientShareResp.MedicalHistory = false;
                            PatientShareResp.Medication = false;
                            PatientShareResp.Problem = false;
                            PatientShareResp.Procedure = false;
                            PatientShareResp.SocialHistory = false;
                            PatientShareResp.SurgicalHistory = false;
                            PatientShareResp.VitalSigns = false;
                            PatientShareResp.Immunization = false;
                            PatientShareResp.Organ = false;
                            PatientShareResp.ClinicalDoc = false;
                            PatientShareResp.Insurance = false;
                            PatientShareResp.EmergencyContact = false;
                            PatientShareResp.Appointment = false;
                            PatientShareResp.Visit = false;
                            PatientShareResp.UploadDocs = false;
                            PatientShareResp.PlanOfCare = false;
                            PatientShareResp.ClinicalInstructions = false;

                            db.SaveChanges();
                        }
                        #endregion

                        #region // Patient Visits
                        finished = false;
                        try
                        {
                            do
                            {
                                PatientVisit Record = db.PatientVisits.First(p => p.PatientId == PatientId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    Record.Viewable = false;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Patient Rep Info
                        User UserResults2 = db.Users.FirstOrDefault(p => p.UserLogin == "R" + UserLogin);
                        PatientRepresentative PatientRepResp = db.PatientRepresentatives.FirstOrDefault(p => p.PatientId == PatientId);


                        if (PatientRepResp != null)
                        {
                            PatientRepResp.Enabled = false;
                            db.SaveChanges();
                        }

                        if (UserResults2 != null)
                        {
                            UserResults2.Enabled = false;
                            db.SaveChanges();
                        }
                        #endregion

                        #region // Mark PatientAdvisor as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientAdvisor Record = db.PatientAdvisors.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientAllergy as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientAllergy Record = db.PatientAllergies.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientCareDocument as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientCareDocument Record = db.PatientCareDocuments.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientClinicalDocument as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientClinicalDocument Record = db.PatientClinicalDocuments.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientDoctor as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientDoctor Record = db.PatientDoctors.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientDocument as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientDocument Record = db.PatientDocuments.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientEmergency as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientEmergency Record = db.PatientEmergencies.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientFamilyHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientFamilyHist Record = db.PatientFamilyHists.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientImmunization as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientImmunization Record = db.PatientImmunizations.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientInsurance as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientInsurance Record = db.PatientInsurances.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientInsurancePlan as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientInsurancePlan Record = db.PatientInsurancePlans.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientLabResult as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientLabResult Record = db.PatientLabResults.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientMedicalDocument as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientMedicalDocument Record = db.PatientMedicalDocuments.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientMedicalHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientMedicalHist Record = db.PatientMedicalHists.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientMedication as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientMedication Record = db.PatientMedications.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientPharmacy as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientPharmacy Record = db.PatientPharmacies.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientPlanOfCare as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientPlanOfCare Record = db.PatientPlanOfCares.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientPolicy as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientPolicy Record = db.PatientPolicies.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientProblem as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientProblem Record = db.PatientProblems.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientProcedure as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientProcedure Record = db.PatientProcedures.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientSocialHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientSocialHist Record = db.PatientSocialHists.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientSurgicalHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientSurgicalHist Record = db.PatientSurgicalHists.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientVitalSign as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientVitalSign Record = db.PatientVitalSigns.First(p => p.PatientId == PatientId
                                    && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion


                        #region // Mark Messages as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                Message Record = db.Messages.First(p => p.PatientId == PatientId
                                    && p.MessageStatusId != 7);

                                if (Record != null)
                                {
                                    Record.MessageStatusId = 7;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion
                    }

                }

                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Delete Patient Visit Data
        //------------------------------------------------------------------------
        // Delete Patient Visit Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete Patient Visit Data")]
        public PatientResp DeletePatientVisitData(Int64 PatientId, Int64 VisitId, Int64 VisitFacilityId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp Response = new PatientResp();
            Response.Valid = true;
            Response.ErrorMessage = "";
            //Do to convert long into string because linq does not support it in query
            string UserLogin = Convert.ToString(PatientId);
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        bool finished = false;

                        #region // Patient Visits
                        try
                        {
                            PatientVisit Record = db.PatientVisits.First(p => p.PatientId == PatientId
                                && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                            if (Record != null)
                            {
                                Record.Deleted = true;
                                Record.UserId_Deleted = UserId;
                                Record.DateDeleted = System.DateTime.Now;
                                Record.Viewable = false;
                                db.SaveChanges();
                            }

                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientAllergy as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientAllergy Record = db.PatientAllergies.First(p => p.PatientId == PatientId
                                     && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientClinicalDocument as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientClinicalDocument Record = db.PatientClinicalDocuments.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientFamilyHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientFamilyHist Record = db.PatientFamilyHists.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientImmunization as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientImmunization Record = db.PatientImmunizations.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientInsurance as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientInsurance Record = db.PatientInsurances.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientLabResult as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientLabResult Record = db.PatientLabResults.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientMedicalHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientMedicalHist Record = db.PatientMedicalHists.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientMedication as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientMedication Record = db.PatientMedications.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientPlanOfCare as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientPlanOfCare Record = db.PatientPlanOfCares.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientProblem as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientProblem Record = db.PatientProblems.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientProcedure as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientProcedure Record = db.PatientProcedures.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientSocialHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientSocialHist Record = db.PatientSocialHists.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientSurgicalHist as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientSurgicalHist Record = db.PatientSurgicalHists.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                        #region // Mark PatientVitalSign as Deleted

                        finished = false;
                        try
                        {
                            do
                            {
                                PatientVitalSign Record = db.PatientVitalSigns.First(p => p.PatientId == PatientId
                                    && p.FacilityId == VisitFacilityId && p.VisitId == VisitId && p.Deleted != true);

                                if (Record != null)
                                {
                                    Record.Deleted = true;
                                    Record.UserId_Deleted = UserId;
                                    Record.DateDeleted = System.DateTime.Now;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion

                    }

                }

                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Get EducationLevel Codes
        //------------------------------------------------------------------------
        // Get Partner Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Partner Data")]
        public PatientPartnerTableData GetPartnerCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientPartnerTableData CodeData = new PatientPartnerTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.C_EMRSystem
                                      select new
                                      {
                                          p.EMRSystemId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Partner Table";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Merge Patient Data
        //------------------------------------------------------------------------
        // Merge Patient Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Merge Patient Data")]
        public PatientResp MergePatientData(Int64 PatientId, Int64 PatientIdToMerge, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientResp Response = new PatientResp();
            Response.Valid = true;
            Response.ErrorMessage = "";
            //Do to convert long into string because linq does not support it in query
            string UserLogin = Convert.ToString(PatientId);
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        bool finished = false;
                        Int64 VisitFacilityId = 0;
                        Int64 VisitId = 0;
                        Int64 NewVisitId = 0;

                        #region // Patient Entered Data
                        try
                        {
                            //  Get a list of visits to merge
                            PatientVisit VRecord = db.PatientVisits.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                            if (VRecord != null)
                            {                                                               
                                #region // PatientAllergy

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientAllergy Record = db.PatientAllergies.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var Allergy = new PatientAllergy()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientAllergyCntr = 0,
                                                FacilityId = 0,
                                                CodeValue_Allergen = Record.CodeValue_Allergen,
                                                CodeSystemId_Allergen = Record.CodeSystemId_Allergen,
                                                Allergen = Record.Allergen,
                                                AllergenType = Record.AllergenType,
                                                CodeValue_Reaction = Record.CodeValue_Reaction,
                                                CodeSystemId_Reaction = Record.CodeSystemId_Reaction,
                                                Reaction = Record.Reaction,
                                                EffectiveDate = Record.EffectiveDate,
                                                ConditionStatusId = Record.ConditionStatusId,
                                                Note = Record.Note,
                                                OnCard = Record.OnCard,
                                                OnKeys = Record.OnKeys,
                                                UserId_Created = Record.UserId_Created,  
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientAllergies.Add(Allergy);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientAllergies.Remove(Record);
                                            db.SaveChanges();

                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch (Exception ex)
                                {
                                }
                                #endregion

                                #region // PatientClinicalDocument

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientClinicalDocument Record = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            /// Add Items
                                            var ClinicalDocument = new PatientClinicalDocument()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                DocumentCntr = 0,
                                                FacilityId = 0,
                                                DocumentDescription = Record.DocumentDescription,
                                                DocumentId = Record.DocumentId,
                                                DocumentFormat = Record.DocumentFormat,
                                                StorageLocation = Record.StorageLocation,
                                                Notes = Record.Notes,
                                                Viewable = Record.Viewable,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientClinicalDocuments.Add(ClinicalDocument);

                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientClinicalDocuments.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientFamilyHist

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientFamilyHist Record = db.PatientFamilyHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var FamilyHist = new PatientFamilyHist()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatFamilyHistCntr = 0,
                                                FacilityId = 0,
                                                RelationshipId = Record.RelationshipId,
                                                Description = Record.Description,
                                                Qualifier = Record.Qualifier,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                ConditionStatusId = Record.ConditionStatusId,
                                                AgeAtOnset = Record.AgeAtOnset,
                                                Diseased = Record.Diseased,
                                                DiseasedAge = Record.DiseasedAge,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientFamilyHists.Add(FamilyHist);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientFamilyHists.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientImmunizations

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientImmunization Record = db.PatientImmunizations.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var Immunization = new PatientImmunization()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientImmunizationCntr = 0,
                                                FacilityId = 0,
                                                ImmunizationDate = Record.ImmunizationDate,
                                                ImmunizationTime = Record.ImmunizationTime,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                Vaccine = Record.Vaccine,
                                                Amount = Record.Amount,
                                                Route = Record.Route,
                                                Site = Record.Site,
                                                Sequence = Record.Sequence,
                                                ExpirationDate = Record.ExpirationDate,
                                                LotNumber = Record.LotNumber,
                                                Manufacturer = Record.Manufacturer,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,   
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientImmunizations.Add(Immunization);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientImmunizations.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientInsurances

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientInsurance Record = db.PatientInsurances.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var Insurance = new PatientInsurance()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientInsuranceId = 0,
                                                FacilityId = 0,
                                                PlanName = Record.PlanName,
                                                MemberNumber = Record.MemberNumber,
                                                GroupNumber = Record.GroupNumber,
                                                Subscriber = Record.Subscriber,
                                                Relationship = Record.Relationship,
                                                EffectiveDate = Record.EffectiveDate,
                                                UserId_Created = Record.UserId_Created,   
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientInsurances.Add(Insurance);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientInsurances.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientLabResults

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientLabResult Record = db.PatientLabResults.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var LabResult = new PatientLabResult()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                LabResultCntr = 0,
                                                FacilityId = 0,
                                                ProviderId_Requested = Record.ProviderId_Requested,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                LabName = Record.LabName,
                                                OrderDate = Record.OrderDate,
                                                CollectionDate = Record.CollectionDate,
                                                Requisiton = Record.Requisiton,
                                                Specimen = Record.Specimen,
                                                SpecimenSource = Record.SpecimenSource,
                                                ReportDate = Record.ReportDate,
                                                ReviewDate = Record.ReviewDate,
                                                Reviewer = Record.Reviewer,
                                                UserId_Created = UserId,
                                                DateCreated = System.DateTime.Now,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };

                                            db.SaveChanges();

                                            bool finished2 = false;
                                            do
                                            {
                                                PatientLabResultTest Record2 = db.PatientLabResultTests.FirstOrDefault(p => p.LabResultCntr == Record.LabResultCntr);

                                                if (Record2 != null)
                                                {
                                                    // Move Lab Test Details
                                                    var LabTest = new PatientLabResultTest()
                                                    {                                                        
                                                        LabResultCntr = Record.LabResultCntr,
                                                        TestCntr = Record2.TestCntr,
                                                        CodeValue = Record2.CodeValue,
                                                        CodeSystemId = Record2.CodeSystemId,
                                                        Component = Record2.Component,
                                                        Result = Record2.Result,
                                                        RefRange = Record2.RefRange,
                                                        Units = Record2.Units,
                                                        Abnormal = Record2.Abnormal,
                                                        ResultStatus = Record2.ResultStatus,
                                                    };
                                                    LabResult.PatientLabResultTests.Add(LabTest);
                                                    db.SaveChanges();

                                                    // Remove Old Record
                                                    db.PatientLabResultTests.Remove(Record2);
                                                    db.SaveChanges();
                                                }
                                                else
                                                    finished2 = true;
                                            }
                                            while (finished2 == true);

                                            // Remove Old Record
                                            db.PatientLabResults.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientMedicalHists

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientMedicalHist Record = db.PatientMedicalHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            // Add Items
                                            var Medical = new PatientMedicalHist()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatMedicalHistCntr = 0,
                                                FacilityId = 0,
                                                Description = Record.Description,
                                                DateOfOccurance = Record.DateOfOccurance,
                                                Note = Record.Note,
                                                OnCard = Record.OnCard,
                                                OnKeys = Record.OnKeys,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientMedicalHists.Add(Medical);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientMedicalHists.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientMedications

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientMedication Record = db.PatientMedications.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var Medication = new PatientMedication()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientMedicationCntr = 0,
                                                FacilityId = 0,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                MedicationName = Record.MedicationName,
                                                Active = Record.Active,
                                                Quantity = Record.Quantity,
                                                RouteId = Record.RouteId,
                                                Dose = Record.Dose,
                                                DoseUnit = Record.DoseUnit,
                                                Refills = Record.Refills,
                                                Frequency = Record.Frequency,
                                                Sig = Record.Sig,
                                                Diagnosis = Record.Diagnosis,
                                                StartDate = Record.StartDate,
                                                ExpireDate = Record.ExpireDate,
                                                Pharmacy = Record.Pharmacy,
                                                Status = Record.Status,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                DuringVisit = Record.DuringVisit,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientMedications.Add(Medication);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientMedications.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientPlanOfCares

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientPlanOfCare Record = db.PatientPlanOfCares.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var PlanOfCare = new PatientPlanOfCare()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PlanCntr = 0,
                                                FacilityId = 0,
                                                InstructionTypeId = Record.InstructionTypeId,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                Instruction = Record.Instruction,
                                                Goal = Record.Goal,
                                                Note = Record.Note,
                                                AppointmentDateTime = Record.AppointmentDateTime,
                                                ProviderId = Record.ProviderId,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientPlanOfCares.Add(PlanOfCare);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientPlanOfCares.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientProblems

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientProblem Record = db.PatientProblems.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var Problem = new PatientProblem()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientProblemCntr = 0,
                                                FacilityId = 0,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                Condition = Record.Condition,
                                                EffectiveDate = Record.EffectiveDate,
                                                ConditionStatusId = Record.ConditionStatusId,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientProblems.Add(Problem);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientProblems.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientProcedures

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientProcedure Record = db.PatientProcedures.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var Procedure = new PatientProcedure()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatProcedureCntr = 0,
                                                FacilityId = 0,
                                                Description = Record.Description,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                Diagnosis = Record.Diagnosis,
                                                PerformedBy = Record.PerformedBy,
                                                ServiceLocation = Record.ServiceLocation,
                                                ServiceDate = Record.ServiceDate,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientProcedures.Add(Procedure);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientProcedures.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientSocialHists

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientSocialHist Record = db.PatientSocialHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var Social = new PatientSocialHist()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatSocialHistCntr = 0,
                                                FacilityId = 0,
                                                Description = Record.Description,
                                                Qualifier = Record.Qualifier,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                BeginDate = Record.BeginDate,
                                                EndDate = Record.EndDate,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientSocialHists.Add(Social);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientSocialHists.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientSurgicalHists

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientSurgicalHist Record = db.PatientSurgicalHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            //// Add Items
                                            var SurgicalHist = new PatientSurgicalHist()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatSurgicalHistCntr = 0,
                                                FacilityId = 0,
                                                Description = Record.Description,
                                                CodeValue = Record.CodeValue,
                                                CodeSystemId = Record.CodeSystemId,
                                                Diagnosis = Record.Diagnosis,
                                                PerformedBy = Record.PerformedBy,
                                                ServiceLocation = Record.ServiceLocation,
                                                ServiceDate = Record.ServiceDate,
                                                Note = Record.Note,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientSurgicalHists.Add(SurgicalHist);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientSurgicalHists.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                #region // PatientVitalSigns

                                finished = false;
                                try
                                {
                                    do
                                    {
                                        PatientVitalSign Record = db.PatientVitalSigns.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                             && p.FacilityId == 0 && p.VisitId == 0);

                                        if (Record != null)
                                        {
                                            var Vital = new PatientVitalSign()
                                            {
                                                PatientId = PatientId,
                                                VisitId = 0,
                                                PatientVitalCntr = 0,
                                                FacilityId = 0,
                                                VitalDate = Record.VitalDate,
                                                HeightIn = Record.HeightIn,
                                                WeightLb = Record.WeightLb,
                                                BloodPressurePosn = Record.BloodPressurePosn,
                                                Systolic = Record.Systolic,
                                                Diastolic = Record.Diastolic,
                                                Pulse = Record.Pulse,
                                                Respiration = Record.Respiration,
                                                Temperature = Record.Temperature,
                                                UserId_Created = Record.UserId_Created,
                                                DateCreated = Record.DateCreated,
                                                UserId_Modified = Record.UserId_Modified,
                                                DateModified = Record.DateModified,
                                                Deleted = Record.Deleted,
                                                UserId_Deleted = Record.UserId_Deleted,
                                                DateDeleted = Record.DateDeleted,
                                            };
                                            db.PatientVitalSigns.Add(Vital);
                                            db.SaveChanges();

                                            // Remove Old Record
                                            db.PatientVitalSigns.Remove(Record);
                                            db.SaveChanges();
                                        }
                                        else
                                            finished = true;
                                    }
                                    while (finished == false);
                                }
                                catch
                                {
                                }
                                #endregion

                                // Delete Visit Information
                                db.PatientVisits.Remove(VRecord);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        #region // Patient Visits
                        try
                        {
                            bool vfinished = false;
                            do
                            {
                                //  Get a list of visits to merge
                                PatientVisit VRecord = db.PatientVisits.FirstOrDefault(p => p.PatientId == PatientIdToMerge);

                                if (VRecord != null)
                                {

                                    // Move Visit Info
                                    VisitFacilityId = VRecord.FacilityId;
                                    VisitId = VRecord.VisitId;
                                    NewVisitId = VisitId;

                                    // Make sure that the key has not already been used
                                    PatientVisit VRecord2 = db.PatientVisits.FirstOrDefault(p => p.PatientId == PatientId
                                        && p.FacilityId == VisitFacilityId && p.VisitId == NewVisitId);
                                    if (VRecord2 != null)
                                    {
                                        // This visit key has already been used, get the next valid id.

                                        PatientVisit results = db.PatientVisits.OrderByDescending(i => i.VisitId).FirstOrDefault(p => p.PatientId == PatientId && p.FacilityId == FacilityId);

                                        if (results != null)
                                        {
                                            NewVisitId = results.VisitId;
                                        }
                                        NewVisitId++;  // Increment to next visit id.

                                    }
                                    #region // Create New Visit Record
                                    try
                                    {
                                        var NewVisit = new PatientVisit()
                                        {

                                            PatientId = PatientId,
                                            FacilityId = VisitFacilityId,
                                            VisitId = NewVisitId,
                                            VisitDate = VRecord.VisitDate,
                                            ProviderId = VRecord.ProviderId,
                                            VisitReason = VRecord.VisitReason,
                                            Viewable = VRecord.Viewable,
                                            DateCreated = VRecord.DateCreated,
                                            ClinicalSummary = VRecord.ClinicalSummary,
                                        };
                                        db.PatientVisits.Add(NewVisit);

                                        db.SaveChanges();
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientAllergy

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientAllergy Record = db.PatientAllergies.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var Allergy = new PatientAllergy()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientAllergyCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    CodeValue_Allergen = Record.CodeValue_Allergen,
                                                    CodeSystemId_Allergen = Record.CodeSystemId_Allergen,
                                                    Allergen = Record.Allergen,
                                                    AllergenType = Record.AllergenType,
                                                    CodeValue_Reaction = Record.CodeValue_Reaction,
                                                    CodeSystemId_Reaction = Record.CodeSystemId_Reaction,
                                                    Reaction = Record.Reaction,
                                                    EffectiveDate = Record.EffectiveDate,
                                                    ConditionStatusId = Record.ConditionStatusId,
                                                    Note = Record.Note,
                                                    OnCard = Record.OnCard,
                                                    OnKeys = Record.OnKeys,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientAllergies.Add(Allergy);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientAllergies.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientClinicalDocument

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientClinicalDocument Record = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                /// Add Items
                                                var ClinicalDocument = new PatientClinicalDocument()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    DocumentCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    DocumentDescription = Record.DocumentDescription,
                                                    DocumentId = Record.DocumentId,
                                                    DocumentFormat = Record.DocumentFormat,
                                                    StorageLocation = Record.StorageLocation,
                                                    Notes = Record.Notes,
                                                    Viewable = Record.Viewable,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientClinicalDocuments.Add(ClinicalDocument);

                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientClinicalDocuments.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientFamilyHist

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientFamilyHist Record = db.PatientFamilyHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var FamilyHist = new PatientFamilyHist()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatFamilyHistCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    RelationshipId = Record.RelationshipId,
                                                    Description = Record.Description,
                                                    Qualifier = Record.Qualifier,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    ConditionStatusId = Record.ConditionStatusId,
                                                    AgeAtOnset = Record.AgeAtOnset,
                                                    Diseased = Record.Diseased,
                                                    DiseasedAge = Record.DiseasedAge,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                };
                                                db.PatientFamilyHists.Add(FamilyHist);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientFamilyHists.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                    #endregion

                                    #region // PatientImmunizations

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientImmunization Record = db.PatientImmunizations.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var Immunization = new PatientImmunization()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientImmunizationCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    ImmunizationDate = Record.ImmunizationDate,
                                                    ImmunizationTime = Record.ImmunizationTime,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    Vaccine = Record.Vaccine,
                                                    Amount = Record.Amount,
                                                    Route = Record.Route,
                                                    Site = Record.Site,
                                                    Sequence = Record.Sequence,
                                                    ExpirationDate = Record.ExpirationDate,
                                                    LotNumber = Record.LotNumber,
                                                    Manufacturer = Record.Manufacturer,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientImmunizations.Add(Immunization);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientImmunizations.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientInsurances

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientInsurance Record = db.PatientInsurances.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var Insurance = new PatientInsurance()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientInsuranceId = 0,
                                                    FacilityId = VisitFacilityId,
                                                    PlanName = Record.PlanName,
                                                    MemberNumber = Record.MemberNumber,
                                                    GroupNumber = Record.GroupNumber,
                                                    Subscriber = Record.Subscriber,
                                                    Relationship = Record.Relationship,
                                                    EffectiveDate = Record.EffectiveDate,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientInsurances.Add(Insurance);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientInsurances.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientLabResults

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientLabResult Record = db.PatientLabResults.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var LabResult = new PatientLabResult()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    LabResultCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    ProviderId_Requested = Record.ProviderId_Requested,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    LabName = Record.LabName,
                                                    OrderDate = Record.OrderDate,
                                                    CollectionDate = Record.CollectionDate,
                                                    Requisiton = Record.Requisiton,
                                                    Specimen = Record.Specimen,
                                                    SpecimenSource = Record.SpecimenSource,
                                                    ReportDate = Record.ReportDate,
                                                    ReviewDate = Record.ReviewDate,
                                                    Reviewer = Record.Reviewer,
                                                    UserId_Created = UserId,
                                                    DateCreated = System.DateTime.Now,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };

                                                db.PatientLabResults.Add(LabResult);
                                                db.SaveChanges();

                                                bool finished2 = false;
                                                do
                                                {
                                                    PatientLabResultTest Record2 = db.PatientLabResultTests.FirstOrDefault(p => p.LabResultCntr == Record.LabResultCntr);

                                                    if (Record2 != null)
                                                    {
                                                        // Move Lab Test Details
                                                        var LabTest = new PatientLabResultTest()
                                                        {
                                                            LabResultCntr = Record.LabResultCntr,
                                                            TestCntr = Record2.TestCntr,
                                                            CodeValue = Record2.CodeValue,
                                                            CodeSystemId = Record2.CodeSystemId,
                                                            Component = Record2.Component,
                                                            Result = Record2.Result,
                                                            RefRange = Record2.RefRange,
                                                            Units = Record2.Units,
                                                            Abnormal = Record2.Abnormal,
                                                            ResultStatus = Record2.ResultStatus,
                                                        };
                                                        LabResult.PatientLabResultTests.Add(LabTest);
                                                        db.SaveChanges();

                                                        // Remove Old Record
                                                        db.PatientLabResultTests.Remove(Record2);
                                                        db.SaveChanges();
                                                    }
                                                    else
                                                        finished2 = true;
                                                }
                                                while (finished2 == true);

                                                // Remove Old Record
                                                db.PatientLabResults.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientMedicalHists

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientMedicalHist Record = db.PatientMedicalHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                // Add Items
                                                var Medical = new PatientMedicalHist()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatMedicalHistCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    Description = Record.Description,
                                                    DateOfOccurance = Record.DateOfOccurance,
                                                    Note = Record.Note,
                                                    OnCard = Record.OnCard,
                                                    OnKeys = Record.OnKeys,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientMedicalHists.Add(Medical);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientMedicalHists.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientMedications

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientMedication Record = db.PatientMedications.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var Medication = new PatientMedication()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientMedicationCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    MedicationName = Record.MedicationName,
                                                    Active = Record.Active,
                                                    Quantity = Record.Quantity,
                                                    RouteId = Record.RouteId,
                                                    Dose = Record.Dose,
                                                    DoseUnit = Record.DoseUnit,
                                                    Refills = Record.Refills,
                                                    Frequency = Record.Frequency,
                                                    Sig = Record.Sig,
                                                    Diagnosis = Record.Diagnosis,
                                                    StartDate = Record.StartDate,
                                                    ExpireDate = Record.ExpireDate,
                                                    Pharmacy = Record.Pharmacy,
                                                    Status = Record.Status,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    DuringVisit = Record.DuringVisit,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientMedications.Add(Medication);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientMedications.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientPlanOfCares

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientPlanOfCare Record = db.PatientPlanOfCares.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var PlanOfCare = new PatientPlanOfCare()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PlanCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    InstructionTypeId = Record.InstructionTypeId,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    Instruction = Record.Instruction,
                                                    Goal = Record.Goal,
                                                    Note = Record.Note,
                                                    AppointmentDateTime = Record.AppointmentDateTime,
                                                    ProviderId = Record.ProviderId,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientPlanOfCares.Add(PlanOfCare);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientPlanOfCares.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientProblems

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientProblem Record = db.PatientProblems.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var Problem = new PatientProblem()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientProblemCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    Condition = Record.Condition,
                                                    EffectiveDate = Record.EffectiveDate,
                                                    ConditionStatusId = Record.ConditionStatusId,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientProblems.Add(Problem);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientProblems.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientProcedures

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientProcedure Record = db.PatientProcedures.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var Procedure = new PatientProcedure()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatProcedureCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    Description = Record.Description,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    Diagnosis = Record.Diagnosis,
                                                    PerformedBy = Record.PerformedBy,
                                                    ServiceLocation = Record.ServiceLocation,
                                                    ServiceDate = Record.ServiceDate,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientProcedures.Add(Procedure);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientProcedures.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientSocialHists

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientSocialHist Record = db.PatientSocialHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var Social = new PatientSocialHist()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatSocialHistCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    Description = Record.Description,
                                                    Qualifier = Record.Qualifier,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    BeginDate = Record.BeginDate,
                                                    EndDate = Record.EndDate,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientSocialHists.Add(Social);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientSocialHists.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientSurgicalHists

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientSurgicalHist Record = db.PatientSurgicalHists.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                //// Add Items
                                                var SurgicalHist = new PatientSurgicalHist()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatSurgicalHistCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    Description = Record.Description,
                                                    CodeValue = Record.CodeValue,
                                                    CodeSystemId = Record.CodeSystemId,
                                                    Diagnosis = Record.Diagnosis,
                                                    PerformedBy = Record.PerformedBy,
                                                    ServiceLocation = Record.ServiceLocation,
                                                    ServiceDate = Record.ServiceDate,
                                                    Note = Record.Note,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientSurgicalHists.Add(SurgicalHist);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientSurgicalHists.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    #region // PatientVitalSigns

                                    finished = false;
                                    try
                                    {
                                        do
                                        {
                                            PatientVitalSign Record = db.PatientVitalSigns.FirstOrDefault(p => p.PatientId == PatientIdToMerge
                                                 && p.FacilityId == VisitFacilityId && p.VisitId == VisitId);

                                            if (Record != null)
                                            {
                                                var Vital = new PatientVitalSign()
                                                {
                                                    PatientId = PatientId,
                                                    VisitId = NewVisitId,
                                                    PatientVitalCntr = 0,
                                                    FacilityId = VisitFacilityId,
                                                    VitalDate = Record.VitalDate,
                                                    HeightIn = Record.HeightIn,
                                                    WeightLb = Record.WeightLb,
                                                    BloodPressurePosn = Record.BloodPressurePosn,
                                                    Systolic = Record.Systolic,
                                                    Diastolic = Record.Diastolic,
                                                    Pulse = Record.Pulse,
                                                    Respiration = Record.Respiration,
                                                    Temperature = Record.Temperature,
                                                    UserId_Created = Record.UserId_Created,
                                                    DateCreated = Record.DateCreated,
                                                    UserId_Modified = Record.UserId_Modified,
                                                    DateModified = Record.DateModified,
                                                    Deleted = Record.Deleted,
                                                    UserId_Deleted = Record.UserId_Deleted,
                                                    DateDeleted = Record.DateDeleted,
                                                };
                                                db.PatientVitalSigns.Add(Vital);
                                                db.SaveChanges();

                                                // Remove Old Record
                                                db.PatientVitalSigns.Remove(Record);
                                                db.SaveChanges();
                                            }
                                            else
                                                finished = true;
                                        }
                                        while (finished == false);
                                    }
                                    catch
                                    {
                                    }
                                    #endregion

                                    // Delete Visit Information
                                    db.PatientVisits.Remove(VRecord);
                                    db.SaveChanges();
                                }
                                else
                                    vfinished = true;
                            }
                            while (vfinished == false);


                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion

                        #region // Messages

                        finished = false;
                        try
                        {
                            do
                            {
                                Message Record = db.Messages.FirstOrDefault(p => p.PatientId == PatientIdToMerge);

                                if (Record != null)
                                {
                                    Record.PatientId = PatientId;
                                    db.SaveChanges();
                                }
                                else
                                    finished = true;
                            }
                            while (finished == false);
                        }
                        catch
                        {
                        }
                        #endregion


                        // Mark Old Patient Record as Deleted & Add MergeId

                        Patient PRecord = db.Patients.FirstOrDefault(p => p.PatientId == PatientIdToMerge);
                        if (PRecord != null)
                        {
                            PRecord.Active = false;
                            PRecord.Deleted = true;
                            PRecord.DateDeleted = System.DateTime.Now;
                            PRecord.MergePatientId = PatientId;

                            db.SaveChanges();

                        }

                        // Mark PatientRep as Disabled
                        String RepId = "R" + PatientIdToMerge;
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == RepId);

                        if (UserResults != null)
                        {
                            // Update User Record
                            UserResults.Enabled = false;
                            db.SaveChanges();
                        }

                    }

                }

                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion
    }
}
