// Service Name  : PatientDocumentService
// Date Created  : 10/21/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Patient Documents
//                 Get Options: 0  Get a list of documents that the patient entered
//                              1  Get a list of documents for the selected visit
//                      Active: 1-Active, 0-Inactive, 2-Both
// MM/DD/YYYY XXX Description
// 10/31/2013 SJF Added Immunization
// 11/14/2013 SJF Added Emergency Contacts
// 11/15/2013 SJF Added Clinical Documents & Insurance
// 11/19/2013 SJF Added Patient Plan Of Care
// 12/05/2013 SJF Added Summary
// 01/03/2014 SJF Added Get Patient CCD
// 01/28/2014 SJF Added PatientDoctor & PatientAdvisor
// 02/06/2014 SJF Added Medical Portfolio
// 02/07/2014 SJF Added PatientCareDocumentShare, PatientMedicalDocumentShare
// 02/07/2014 SJF Added PatientPolicy
// 04/21/2014 SJF Added vwVisitCCD
// 04/29/2014 SJF Added Get Patient Visit CCD Data
// 06/17/2014 SJF Added PatientNote
// 10/29/2014 SJF Added Get Combined Patient Document List
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Xml;
using System.Data.Entity;
using System.Text;
using AMR.Data;

namespace AMR.DataService
{
    /// <summary>
    /// Summary description for PatientDocumentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "PatientDocumentWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PatientDocumentService : System.Web.Services.WebService
    {
        #region Static Variables
        //------------------------------------------------------------------------
        // Define Static Variables
        //------------------------------------------------------------------------

        //static Int16 ActionInsert = 1;
        //static Int16 ActionUpdate = 2;
        static Int16 ActionRead = 3;
        //static Int16 ActionEmail = 4;
        //static Int16 ActionDownload = 5;

        //static Int16 DocTypePatients = 0;
        static Int16 DocTypePatientAllergy = 1;
        static Int16 DocTypePatientFamilyHist = 2;
        static Int16 DocTypePatientLabResults = 3;
        static Int16 DocTypePatientMedicalHist = 4;
        static Int16 DocTypePatientMedication = 5;
        static Int16 DocTypePatientProblem = 6;
        static Int16 DocTypePatientProcedure = 7;
        static Int16 DocTypePatientSocialHist = 8;
        static Int16 DocTypePatientSurgicalHist = 9;
        static Int16 DocTypePatientVitalSign = 10;
        static Int16 DocTypePatientImmunization = 11;
        //static Int16 DocTypePatientOrgan = 12;
        static Int16 DocTypePatientClinicalDoc = 13;
        static Int16 DocTypePatientDocument = 14;
        static Int16 DocTypePatientInsurance = 15;
        static Int16 DocTypePatientEmergency = 16;
        static Int16 DocTypePatientPlanOfCare = 17;
        static Int16 DocTypePatientMedicalDoc = 18;
        static Int16 DocTypePatientPharmacy = 19;
        static Int16 DocTypeCCD = 20;
        static Int16 DocTypePatientCareDoc = 21;
        static Int16 DocTypePatientDoctor = 22;
        static Int16 DocTypePatientAdvisor = 23;
        static Int16 DocTypePatientPolicy = 24;

        #endregion

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------
        public struct PatientDocParms
        {
            public int Option;
            public int Active;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
        }
        public struct PatientDocTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct PatientCCDResult
        {
            public bool Valid;
            public string ErrorMessage;
            public string CCD;
        }

        public struct PatientSummaryParms
        {
            public int Option;
            public int Active;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 LabResultCntr;
            public bool Visit;
            public bool Allergy;
            public bool Medication;
            public bool SocialHist;
            public bool FamilyHist;
            public bool MedicalHist;
            public bool Problem;
            public bool Procedure;
            public bool VitalSign;
            public bool Immunization;
            public bool PlanOfCare;
            public bool Lab;
            public bool Insurance;
            public bool ClinicalDocs;
            public bool Provider;
        }

        public struct PatientSummaryTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dtVisit;
            public DataTable dtAllergy;
            public DataTable dtMedication;
            public DataTable dtSocialHist;
            public DataTable dtFamilyHist;
            public DataTable dtMedicalHist;
            public DataTable dtProblem;
            public DataTable dtProcedure;
            public DataTable dtVitalSign;
            public DataTable dtImmunization;
            public DataTable dtPlanOfCare;
            public DataTable dtLab;
            public DataTable dtLabDetail;
            public DataTable dtLabResult;  //add by Talha Amin
            public DataTable dtInsurance;
            public DataTable dtClinicalDocs;
            public DataTable dtProvider;
        }

        public struct PatientMedicalParms
        {
            public Int64 PatientId;
            public bool Emergency;
            public bool Visit;
            public bool DoctorUploaded;
            public bool PatientUploaded;
            public bool Allergy;
            public bool Medication;
            public bool Problem;
            public bool Policy;
            public bool Document;
        }

        public struct PatientMedicalTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dtEmergency;
            public DataTable dtVisit;
            public DataTable dtDoctorUploaded;
            public DataTable dtPatientUploaded;
            public DataTable dtAllergy;
            public DataTable dtMedication;
            public DataTable dtProblem;
            public DataTable dtPolicy;
            public DataTable dtDocument;
        }

        public struct PatientAllergyData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientAllergyCntr;
            public string CodeValue_Allergen;
            public Int16 CodeSystem_Allergen;
            public string Allergen;
            public string AllergenType;
            public string CodeValue_Reaction;
            public Int16 CodeSystem_Reaction;
            public string Reaction;
            public string EffectiveDate;
            public Int16 ConditionStatusId;
            public string Note;
            public bool OnCard;
            public bool OnKeys;
        }

        public struct PatientFamilyHistData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatFamilyHistCntr;
            public Int16 RelationshipId;
            public string FamilyMember;
            public string Description;
            public string Qualifier;
            public string CodeValue;
            public int CodeSystemId;
            public Int16 ConditionStatusId;
            public string SNOMEDCode;
            public int AgeAtOnset;
            public bool Diseased;
            public int DiseasedAge;
            public string Note;
        }

        public struct PatientMedicalHistData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatMedicalHistCntr;
            public string Description;
            public string DateOfOccurance;
            public string Note;
            public bool OnCard;
            public bool OnKeys;
        }

        public struct PatientMedicationData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientMedicationCntr;
            public string CodeValue;
            public int CodeSystemId;
            public string MedicationName;
            public bool Active;
            public int Quantity;
            public string RouteId;
            public int Dose;
            public string DoseUnit;
            public int Refills;
            public string Frequency;
            public string Sig;
            public string Diagnosis;
            public DateTime StartDate;
            public DateTime ExpireDate;
            public string Pharmacy;
            public string Note;
            public bool DuringVisit;
        }

        public struct PatientProblemData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientProblemCntr;
            public string CodeValue;
            public int CodeSystemId;
            public string Condition;
            public string EffectiveDate;
            public string LastChangeDate;
            public Int16 ConditionStatusId;
            public string Note;
        }

        public struct PatientProcedureData
	    {
		    public bool Valid;
		    public string ErrorMessage;
		    public Int64 PatientId;
            public Int64 FacilityId;
		    public Int64 VisitId;
		    public Int64 PatProcedureCntr;
		    public string Description;
		    public string CodeValue;
            public int CodeSystemId;
		    public string Diagnosis;
		    public string PerformedBy;
		    public string ServiceLocation;
		    public string ServiceDate;
		    public string Note;
	    }

        public struct PatientSocialHistData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatSocialHistCntr;
            public string Description;
            public string Qualifier;
            public string CodeValue;
            public int CodeSystemId;
            public string BeginDate;
            public string EndDate;
            public string Note;
        }

        public struct PatientSurgicalHistData
	    {
		    public bool Valid;
		    public string ErrorMessage;
		    public Int64 PatientId;
            public Int64 FacilityId;
		    public Int64 VisitId;
		    public Int64 PatSurgicalHistCntr;
		    public string Description;
		    public string CodeValue;
            public int CodeSystemId;
		    public string Diagnosis;
		    public string PerformedBy;
		    public string ServiceLocation;
		    public string ServiceDate;
		    public string Note;
	    }

        public struct PatientVitalSignData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientVitalCntr;
            public DateTime VitalDate;
            public int HeightIn;
            public int WeightLb;
            public string BloodPressurePosn;
            public int Systolic;
            public int Diastolic;
            public int Pulse;
            public int Respiration;
            public decimal Temperature;
        }

        public struct PatientLabResultData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 LabResultCntr;
            public Int64 ProviderId_Requested;
            public string CodeValue;
            public int CodeSystemId;
            public string LabName;
            public DateTime OrderDate;
            public DateTime CollectionDate;
            public string Requisiton;
            public string Specimen;
            public string SpecimenSource;
            public DateTime ReportDate;
            public DateTime ReviewDate;
            public string Reviewer;
            public DataTable dtTests;
        }
        public struct PatientLabResultParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 LabResultCntr;
        }

        public struct PatientImmunizationData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientImmunizationCntr;
            public string ImmunizationDate;
            public string ImmunizationTime;
            public string CodeValue;
            public int CodeSystemId;
            public string Vaccine;
            public string Amount;
            public string Route;
            public string Site;
            public string Sequence;
            public DateTime ExpirationDate;
            public string LotNumber;
            public string Manufacturer;
            public string Note;
        }

        public struct PatientEmergencyData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PatientEmergencyId;
            public string Name;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;
            public string HomePhone;
            public string MobilePhone;
            public string WorkPhone;
            public string Email;
            public bool IsPrimary;
            public int? RelationshipId; //Edit By Talha Amin...
            public bool OnCard;
        }

        public struct PatientInsuranceData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PatientInsuranceId;
            public string PlanName;
            public string MemberNumber;
            public string GroupNumber;
            public string Subscriber;
            public string Relationship;
            public DateTime EffectiveDate;
        }

        public struct PatientPharmacyData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PharmacyCntr;
            public string PharmacyName;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;
            public string Phone;
            public bool Preferred;
            public string Note;
        }

        public struct PatientClinicalDocumentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 DocumentCntr;
            public string DocumentDescription;
            public string DocumentId;
            public string DocumentFormat;
            public string StorageLocation;
            public string Notes;
            public bool Viewable;
            public byte[] DocumentImage;
        }
        public struct PatientClinicalDocumentDataApp
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 DocumentCntr;
            public string DocumentDescription;
            public string DocumentId;
            public string DocumentFormat;
            public string StorageLocation;
            public string Notes;
            public bool Viewable;
         //   public string DocumentImage;
            public byte[] DocumentImage;   //Added by Talha
        }

        public struct PatientClinicalDocumentParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 DocumentCntr;
        }

        public struct PatientPlanOfCareData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PlanCntr;
            public Int16 InstructionTypeId;
            public string CodeValue;
            public int CodeSystemId;
            public string Instruction;
            public string Goal;
            public string Note;
            public DateTime AppointmentDateTime;
            public Int64 ProviderId;
        }
        //Added By Ahmed 
        public struct PatientClinicalInstructionsData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 PlanCntr;
            public Int16 InstructionTypeId;
            public string CodeValue;
            public int CodeSystemId;
            public string Instruction;
            public string Goal;
            public string Note;
            public DateTime AppointmentDateTime;
            public Int64 ProviderId;
        }

        public struct PatientDocumentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 DocumentCntr;
            public string DocumentDescription;
            public string DocumentId;
            public string DocumentFormat;
            public string StorageLocation;
            public string Notes;
            public bool Viewable;
            public byte[] DocumentImage;
        }

        public struct PatientDocumentParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 DocumentCntr;
            public bool Share;
        }

        public struct PatientDocumentResp
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct PatientMedicalDocumentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 DocumentCntr;
            public string DocumentDescription;
            public string FacilityName;
            public string DoctorName;
            public string DocumentId;
            public string DocumentFormat;
            public string StorageLocation;
            public string Notes;
            public bool Viewable;
            public byte[] DocumentImage;
        }

        public struct PatientCareDocumentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 DocumentCntr;
            public string DocumentDescription;
            public string DoctorName;
            public string DocumentId;
            public string DocumentFormat;
            public string StorageLocation;
            public string Notes;
            public bool Viewable;
            public byte[] DocumentImage;
        }

        public struct PatientDoctorData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PatientDoctorId;
            public int DoctorTypeId;
            public string DoctorTypeDesc;
            public string Name;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;
            public string WorkPhone;
            public string MobilePhone;
            public string Fax;
            public string Email;
            public bool IsPrimary;
            public bool OnCard;
        }

        public struct PatientAdvisorData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PatientAdvisorId;
            public int AdvisorId;
            public string AdvisorDesc;
            public string Name;
            public string ContactAgent;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;
            public string WorkPhone;
            public string MobilePhone;
            public string Fax;
            public string EMail;
            public string Notes;
        }

        public struct CCDParms
        {
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public bool Custom;
            public bool Demographics;
            public bool Provider;
            public bool Problems;
            public bool Allergies;
            public bool Immunizations;
            public bool Medications;
            public bool MedsAdministered;
            public bool Social;
            public bool Procedures;
            public bool VitalSigns;
            public bool Labs;
            //public bool ClinicalInstructions;
            //public bool FutureAppointments;
            //public bool Referrals;
            //public bool ScheduledTests;
            //public bool DecisionAids;
            public bool PlanOfCare;
            public bool VisitReason;
            public DataSet Selection;
        }

        public struct CCDPostData
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public DateTime VisitDate;
            public Int64 ProviderId;
            public string VisitReason;
            public byte[] Document;
        }
        public struct CCDPostResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct MedicalPortfolioData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dtVisit;
            public DataTable dtOutsideDoctor;
            public DataTable dtPatiendDocs;
        }

        public struct PatientPolicyData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 PatientPolicyId;
            public int PolicyTypeId;
            public string PolicyTypeName;
            public string Company;
            public string PolicyNo;
            public string GroupNumber;
            public string PlanNumber;
            public string Agent;
            public string AgentPhone;
            public string AgentFax;
            public string Notes;
        }

        public struct PatientVisitCCD
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public Int64 ProviderId;
            public string Title;
            public string FirstName;
            public string LastName;
            public string License;
            public string Phone;
            public string Email;
            public int FacilityTypeId;
            public string FacilityType;
            public string FacilityName;
            public string Address1;
            public string Address2;
            public string City;
            public string State;
            public string CountryCode;
            public string CountryName;
            public string PostalCode;
            public string FacilityPhone;
            public string FacilityFax;
            public string VisitReason;
            public DateTime VisitDate;
        }
        public struct PatientCCDParms
        {
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;
            public bool ClinicalSummary;
        }
        public struct PatientCCDTableData
        {
            public bool Valid;
            public bool ClinicalSummary;
            public string ErrorMessage;
            public DataTable dtMedication;
            public DataTable dtMedsAdministered;
            public DataTable dtProblem;
            public DataTable dtAllergy;
            public DataTable dtImmunization;
            public DataTable dtSocialHist;
            public DataTable dtProcedure;
            public DataTable dtVitalSign;
            public DataTable dtLab;
            public DataTable dtPlanOfCare;
            public DataTable dtClinicalInstructions;
            public DataTable dtFamilyHist;
            public DataTable dtMedicalHist;
        }

        public struct PatientNoteData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public string Note;
        }

        public struct PatientSocialSelfData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public string Birthplace;
            public Int16 EducationLevelId;
            public string Occupation;
            public bool Retired;
            public Int16 ChildrenSon;
            public Int16 ChildrenDaughter;
            public bool CaffeineUser;
            public string CaffieneType;
            public string CaffeineAmount;
            public bool ExerciseMember;
            public Int16 ExerciseFrequencyId;
            public string ExerciseAmount;
            public Int16 ActivityLevelId;
            public string Activity1;
            public string Activity2;
            public string Activity3;
            public bool AlcoholUser;
            public Int16 AlcoholFrequencyId;
            public DateTime AlcoholLastUse;
            public string AlcoholType;
            public Int16 AlcoholStartAge;
            public bool AlcoholFamilyHist;
            public Int32 SmokingStatusId;
            public string SmokingStatus;
            public Int16 SmokingDailyAmount;
            public string SmokingType;
            public Int16 SmokingYears;
            public Int16 SmokingQuitAttempts;
            public DateTime SmokingQuitDate;
            public bool SmokingSecondHand;
      }
        #endregion


        #region Get PatientAllergy Data
        //------------------------------------------------------------------------
        // Get PatientAllergy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientAllergy Data")]
        public PatientDocTableData GetPatientAllergyData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientAllergy = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientAllergies
                                      join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientAllergyCntr,
                                          p.CodeValue_Allergen,
                                          p.CodeSystemId_Allergen,
                                          p.Allergen,
                                          p.AllergenType,
                                          p.CodeValue_Reaction,
                                          p.CodeSystemId_Reaction,
                                          p.Reaction,
                                          p.EffectiveDate,
                                          p.ConditionStatusId,
                                          ConditionStatus = c.Value,
                                          p.Note,
                                          p.OnCard,
                                          p.OnKeys,
                                          p.DateModified
                                      };

                        PatientAllergy.dt = clsTableConverter.ToDataTable(results);
                        PatientAllergy.dt.TableName = "PatientAllergy";

                        PatientAllergy.Valid = true;
                        PatientAllergy.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientAllergy, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientAllergy.Valid = false;
                    PatientAllergy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAllergy.Valid = false;
                PatientAllergy.ErrorMessage = "Invalid Token";
            }
            return PatientAllergy;
        }
        #endregion

        #region Save PatientAllergy Data
        //------------------------------------------------------------------------
        // Save PatientAllergy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientAllergy Data")]
        public PatientAllergyData SavePatientAllergyData(PatientAllergyData PatientAllergy, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientAllergy.Valid = true;
            PatientAllergy.ErrorMessage = "";

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

                        if (PatientAllergy.PatientAllergyCntr == 0)
                        {
                            //// Add Items
                            var Allergy = new PatientAllergy()
                            {
                                PatientId = PatientAllergy.PatientId,
                                VisitId = PatientAllergy.VisitId,
                                PatientAllergyCntr = 0,
                                FacilityId = PatientAllergy.FacilityId,
                                CodeValue_Allergen = PatientAllergy.CodeValue_Allergen,
                                CodeSystemId_Allergen = PatientAllergy.CodeSystem_Allergen,
                                Allergen = PatientAllergy.Allergen,
                                AllergenType = PatientAllergy.AllergenType,
                                CodeValue_Reaction = PatientAllergy.CodeValue_Reaction,
                                CodeSystemId_Reaction = PatientAllergy.CodeSystem_Reaction,
                                Reaction = PatientAllergy.Reaction,
                                EffectiveDate = PatientAllergy.EffectiveDate,
                                ConditionStatusId = PatientAllergy.ConditionStatusId,
                                Note = PatientAllergy.Note,
                                OnCard = PatientAllergy.OnCard,
                                OnKeys = PatientAllergy.OnKeys,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientAllergies.Add(Allergy);

                        }
                        else
                        {
                            PatientAllergy Allergy = db.PatientAllergies.FirstOrDefault(p => p.PatientId == PatientAllergy.PatientId
                                                  && p.FacilityId == PatientAllergy.FacilityId 
                                                  && p.VisitId == PatientAllergy.VisitId
                                                  && p.PatientAllergyCntr == PatientAllergy.PatientAllergyCntr);

                            if (Allergy != null)
                            {
                                Allergy.Allergen = PatientAllergy.Allergen;
                                Allergy.CodeValue_Allergen = PatientAllergy.CodeValue_Allergen;
                                Allergy.CodeSystemId_Allergen = PatientAllergy.CodeSystem_Allergen;
                                Allergy.AllergenType = PatientAllergy.AllergenType;
                                Allergy.CodeValue_Reaction = PatientAllergy.CodeValue_Reaction;
                                Allergy.CodeSystemId_Reaction = PatientAllergy.CodeSystem_Reaction;
                                Allergy.Reaction = PatientAllergy.Reaction;
                                Allergy.EffectiveDate = PatientAllergy.EffectiveDate;
                                Allergy.ConditionStatusId = PatientAllergy.ConditionStatusId;
                                Allergy.Note = PatientAllergy.Note;
                                Allergy.OnCard = PatientAllergy.OnCard;
                                Allergy.OnKeys = PatientAllergy.OnKeys;
                                Allergy.UserId_Modified = UserId;
                                Allergy.DateModified = System.DateTime.Now;

                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientAllergy.Valid = false;
                    PatientAllergy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAllergy.Valid = false;
                PatientAllergy.ErrorMessage = "Invalid Token";
            }
            return PatientAllergy;
        }
        #endregion

        #region Delete PatientAllergy Data
        //------------------------------------------------------------------------
        // Delete PatientAllergy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientAllergy Data")]
        public PatientAllergyData DeletePatientAllergyData(PatientAllergyData PatientAllergy, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientAllergy Allergy = db.PatientAllergies.FirstOrDefault(p => p.PatientId == PatientAllergy.PatientId
                                                && p.FacilityId == PatientAllergy.FacilityId 
                                                && p.VisitId == PatientAllergy.VisitId
                                                && p.PatientAllergyCntr == PatientAllergy.PatientAllergyCntr);

                        if (Allergy != null)
                        {
                            Allergy.Deleted = true;
                            Allergy.UserId_Deleted = UserId;
                            Allergy.DateDeleted = System.DateTime.Now;
                            PatientAllergy.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientAllergy.Valid = false;
                    PatientAllergy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAllergy.Valid = false;
                PatientAllergy.ErrorMessage = "Invalid Token";
            }
            return PatientAllergy;
        }
        #endregion

 
        #region Get PatientFamilyHist Data
        //------------------------------------------------------------------------
        // Get PatientFamilyHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientFamilyHist Data")]
        public PatientDocTableData GetPatientFamilyHistData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return

            PatientDocTableData PatientFamilyHist = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            // If Option = 0 - Patient entered, then VisitId needs to be 0.
            if (Parms.Option == 0)
                Parms.VisitId = 0;
            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientFamilyHists
                                      join r in db.C_Relationship on p.RelationshipId equals r.RelationshipId
                                      join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatFamilyHistCntr,
                                          p.RelationshipId,
                                          FamilyMember = r.Value,
                                          SNOMEDCode = r.SNOMED,  //Added by Talha Amin
                                          p.Description,
                                          p.Qualifier,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          p.ConditionStatusId,
                                          ConditionStatus = c.Value,
                                          p.AgeAtOnset,
                                          p.Diseased,
                                          p.DiseasedAge,
                                          p.Note,
                                          p.DateModified
                                      };

                        PatientFamilyHist.dt = clsTableConverter.ToDataTable(results);
                        PatientFamilyHist.dt.TableName = "PatientFamilyHist";

                        PatientFamilyHist.Valid = true;
                        PatientFamilyHist.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientFamilyHist, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientFamilyHist.Valid = false;
                    PatientFamilyHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientFamilyHist.Valid = false;
                PatientFamilyHist.ErrorMessage = "Invalid Token";
            }
            return PatientFamilyHist;
        }
        #endregion

        #region Save PatientFamilyHist Data
        //------------------------------------------------------------------------
        // Save PatientFamilyHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientFamilyHist Data")]
        public PatientFamilyHistData SavePatientFamilyHistData(PatientFamilyHistData PatientFamilyHist, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientFamilyHist.Valid = true;
            PatientFamilyHist.ErrorMessage = "";

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
                        if (PatientFamilyHist.PatFamilyHistCntr == 0)
                        {
                            //// Add Items
                            var FamilyHist = new PatientFamilyHist()
                            {
                                PatientId = PatientFamilyHist.PatientId,
                                VisitId = PatientFamilyHist.VisitId,
                                PatFamilyHistCntr = 0,
                                FacilityId = PatientFamilyHist.FacilityId,
                                RelationshipId = PatientFamilyHist.RelationshipId,
                                Description = PatientFamilyHist.Description,
                                Qualifier = PatientFamilyHist.Qualifier,
                                CodeValue = PatientFamilyHist.CodeValue,
                                CodeSystemId = PatientFamilyHist.CodeSystemId,
                                ConditionStatusId = PatientFamilyHist.ConditionStatusId,
                                AgeAtOnset = PatientFamilyHist.AgeAtOnset,
                                Diseased = PatientFamilyHist.Diseased,
                                DiseasedAge = PatientFamilyHist.DiseasedAge,
                                Note = PatientFamilyHist.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientFamilyHists.Add(FamilyHist);
                        }
                        else
                        {
                            PatientFamilyHist FamilyHist = db.PatientFamilyHists.FirstOrDefault(p => p.PatientId == PatientFamilyHist.PatientId
                                                  && p.FacilityId == PatientFamilyHist.FacilityId 
                                                  && p.VisitId == PatientFamilyHist.VisitId
                                                  && p.PatFamilyHistCntr == PatientFamilyHist.PatFamilyHistCntr);

                            if (FamilyHist != null)
                            {
                                FamilyHist.RelationshipId = PatientFamilyHist.RelationshipId;
                                FamilyHist.Description = PatientFamilyHist.Description;
                                FamilyHist.Qualifier = PatientFamilyHist.Qualifier;
                                FamilyHist.CodeValue = PatientFamilyHist.CodeValue;
                                FamilyHist.CodeSystemId = PatientFamilyHist.CodeSystemId;
                                FamilyHist.ConditionStatusId = PatientFamilyHist.ConditionStatusId;
                                FamilyHist.AgeAtOnset = PatientFamilyHist.AgeAtOnset;
                                FamilyHist.Diseased = PatientFamilyHist.Diseased;
                                FamilyHist.DiseasedAge = PatientFamilyHist.DiseasedAge;
                                FamilyHist.Note = PatientFamilyHist.Note;
                                FamilyHist.UserId_Modified = UserId;
                                FamilyHist.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientFamilyHist.Valid = false;
                    PatientFamilyHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientFamilyHist.Valid = false;
                PatientFamilyHist.ErrorMessage = "Invalid Token";
            }
            return PatientFamilyHist;
        }
        #endregion

        #region Delete PatientFamilyHist Data
        //------------------------------------------------------------------------
        // Delete PatientFamilyHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientFamilyHist Data")]
        public PatientFamilyHistData DeletePatientFamilyHistData(PatientFamilyHistData PatientFamilyHist, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        
                        PatientFamilyHist FamilyHist = db.PatientFamilyHists.FirstOrDefault(p => p.PatientId == PatientFamilyHist.PatientId
                                                && p.FacilityId == PatientFamilyHist.FacilityId 
                                                && p.VisitId == PatientFamilyHist.VisitId
                                                && p.PatFamilyHistCntr == PatientFamilyHist.PatFamilyHistCntr);

                        if (FamilyHist != null)
                        {
                            FamilyHist.Deleted = true;
                            FamilyHist.UserId_Deleted = UserId;
                            FamilyHist.DateDeleted = System.DateTime.Now;
                            PatientFamilyHist.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientFamilyHist.Valid = false;
                    PatientFamilyHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientFamilyHist.Valid = false;
                PatientFamilyHist.ErrorMessage = "Invalid Token";
            }
            return PatientFamilyHist;
        }
        #endregion


        #region Get PatientMedicalHist Data
        //------------------------------------------------------------------------
        // Get PatientMedicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientMedicalHist Data")]
        public PatientDocTableData GetPatientMedicalHistData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientMedicalHist = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientMedicalHists
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatMedicalHistCntr,
                                          p.Description,
                                          p.DateOfOccurance,
                                          p.Note,
                                          p.OnCard,
                                          p.OnKeys,
                                          p.DateModified
                                      };

                        PatientMedicalHist.dt = clsTableConverter.ToDataTable(results);
                        PatientMedicalHist.dt.TableName = "PatientMedicalHist";

                        PatientMedicalHist.Valid = true;
                        PatientMedicalHist.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientMedicalHist, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientMedicalHist.Valid = false;
                    PatientMedicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedicalHist.Valid = false;
                PatientMedicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientMedicalHist;
        }
        #endregion

        #region Save PatientMedicalHist Data
        //------------------------------------------------------------------------
        // Save PatientMedicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientMedicalHist Data")]
        public PatientMedicalHistData SavePatientMedicalHistData(PatientMedicalHistData PatientMedicalHist, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientMedicalHist.Valid = true;
            PatientMedicalHist.ErrorMessage = "";

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

                        if (PatientMedicalHist.PatMedicalHistCntr == 0)
                        {
                            //// Add Items
                            var MedicalHist = new PatientMedicalHist()
                            {
                                PatientId = PatientMedicalHist.PatientId,
                                VisitId = PatientMedicalHist.VisitId,
                                PatMedicalHistCntr = 0,
                                FacilityId = PatientMedicalHist.FacilityId,
                                Description = PatientMedicalHist.Description,
                                DateOfOccurance = PatientMedicalHist.DateOfOccurance,
                                Note = PatientMedicalHist.Note,
                                OnCard = PatientMedicalHist.OnCard,
                                OnKeys = PatientMedicalHist.OnKeys,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientMedicalHists.Add(MedicalHist);
                        }
                        else
                        {
                            PatientMedicalHist MedicalHist = db.PatientMedicalHists.FirstOrDefault(p => p.PatientId == PatientMedicalHist.PatientId
                                                  && p.FacilityId == PatientMedicalHist.FacilityId 
                                                  && p.VisitId == PatientMedicalHist.VisitId
                                                  && p.PatMedicalHistCntr == PatientMedicalHist.PatMedicalHistCntr);

                            if (MedicalHist != null)
                            {
                                MedicalHist.Description = PatientMedicalHist.Description;
                                MedicalHist.DateOfOccurance = PatientMedicalHist.DateOfOccurance;
                                MedicalHist.Note = PatientMedicalHist.Note;
                                MedicalHist.OnCard = PatientMedicalHist.OnCard;
                                MedicalHist.OnKeys = PatientMedicalHist.OnKeys;
                                MedicalHist.UserId_Modified = UserId;
                                MedicalHist.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientMedicalHist.Valid = false;
                    PatientMedicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedicalHist.Valid = false;
                PatientMedicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientMedicalHist;
        }
        #endregion

        #region Delete PatientMedicalHist Data
        //------------------------------------------------------------------------
        // Delete PatientMedicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientMedicalHist Data")]
        public PatientMedicalHistData DeletePatientMedicalHistData(PatientMedicalHistData PatientMedicalHist, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        PatientMedicalHist MedicalHist = db.PatientMedicalHists.FirstOrDefault(p => p.PatientId == PatientMedicalHist.PatientId
                                                && p.FacilityId == PatientMedicalHist.FacilityId 
                                                && p.VisitId == PatientMedicalHist.VisitId
                                                && p.PatMedicalHistCntr == PatientMedicalHist.PatMedicalHistCntr);

                        if (MedicalHist != null)
                        {
                            MedicalHist.Deleted = true;
                            MedicalHist.UserId_Deleted = UserId;
                            MedicalHist.DateDeleted = System.DateTime.Now;
                            PatientMedicalHist.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientMedicalHist.Valid = false;
                    PatientMedicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedicalHist.Valid = false;
                PatientMedicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientMedicalHist;
        }
        #endregion


        #region Get PatientMedication Data
        //------------------------------------------------------------------------
        // Get PatientMedication Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientMedication Data")]
        public PatientDocTableData GetPatientMedicationData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientMedication = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        if (Parms.Active == 1 && Parms.Option == 2)  // Active
                        {
                            var results = from p in db.PatientMedications
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId  && p.Deleted == false && p.Active == true
                                          select new
                                          {
                                              p.PatientMedicationCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.MedicationName,
                                              p.Active,
                                              p.Quantity,
                                              p.RouteId,
                                              p.Dose,
                                              p.DoseUnit,
                                              p.Refills,
                                              p.Frequency,
                                              p.Sig,
                                              p.Diagnosis,
                                              p.StartDate,
                                              p.ExpireDate,
                                              p.Pharmacy,
                                              p.Status,
                                              p.Note,
                                              p.DateModified,
                                              p.DuringVisit
                                          };

                            PatientMedication.dt = clsTableConverter.ToDataTable(results);
                        }
                        else if (Parms.Active == 1 && Parms.Option != 2)  // Active
                        {
                            var results = from p in db.PatientMedications
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.Active == true
                                          select new
                                          {
                                              p.PatientMedicationCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.MedicationName,
                                              p.Active,
                                              p.Quantity,
                                              p.RouteId,
                                              p.Dose,
                                              p.DoseUnit,
                                              p.Refills,
                                              p.Frequency,
                                              p.Sig,
                                              p.Diagnosis,
                                              p.StartDate,
                                              p.ExpireDate,
                                              p.Pharmacy,
                                              p.Status,
                                              p.Note,
                                              p.DateModified,
                                              p.DuringVisit
                                          };

                            PatientMedication.dt = clsTableConverter.ToDataTable(results);
                        }
                        else if (Parms.Active == 0)  // Invactive
                        {
                            var results = from p in db.PatientMedications
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.Active == false
                                          select new
                                          {
                                              p.PatientMedicationCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.MedicationName,
                                              p.Active,
                                              p.Quantity,
                                              p.RouteId,
                                              p.Dose,
                                              p.DoseUnit,
                                              p.Refills,
                                              p.Frequency,
                                              p.Sig,
                                              p.Diagnosis,
                                              p.StartDate,
                                              p.ExpireDate,
                                              p.Pharmacy,
                                              p.Status,
                                              p.Note,
                                              p.DateModified,
                                              p.DuringVisit
                                          };

                            PatientMedication.dt = clsTableConverter.ToDataTable(results);
                        }
                        else  // All
                        {
                            var results = from p in db.PatientMedications
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientMedicationCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.MedicationName,
                                              p.Active,
                                              p.Quantity,
                                              p.RouteId,
                                              p.Dose,
                                              p.DoseUnit,
                                              p.Refills,
                                              p.Frequency,
                                              p.Sig,
                                              p.Diagnosis,
                                              p.StartDate,
                                              p.ExpireDate,
                                              p.Pharmacy,
                                              p.Status,
                                              p.Note,
                                              p.DateModified,
                                              p.DuringVisit
                                          };

                            PatientMedication.dt = clsTableConverter.ToDataTable(results);
                        }


                        PatientMedication.dt.TableName = "PatientMedication";

                        PatientMedication.Valid = true;
                        PatientMedication.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientMedication, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientMedication.Valid = false;
                    PatientMedication.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedication.Valid = false;
                PatientMedication.ErrorMessage = "Invalid Token";
            }
            return PatientMedication;
        }
        #endregion

        #region Save PatientMedication Data
        //------------------------------------------------------------------------
        // Save PatientMedication Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientMedication Data")]
        public PatientMedicationData SavePatientMedicationData(PatientMedicationData PatientMedication, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientMedication.Valid = true;
            PatientMedication.ErrorMessage = "";

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

                        if (PatientMedication.PatientMedicationCntr == 0)
                        {
                            //// Add Items
                            var Medication = new PatientMedication()
                            {
                                PatientId = PatientMedication.PatientId,
                                VisitId = PatientMedication.VisitId,
                                PatientMedicationCntr = 0,
                                FacilityId = PatientMedication.FacilityId,
                                CodeValue = PatientMedication.CodeValue,
                                CodeSystemId = PatientMedication.CodeSystemId,
                                MedicationName = PatientMedication.MedicationName,
                                Active = PatientMedication.Active,
                                Quantity = PatientMedication.Quantity,
                                RouteId = PatientMedication.RouteId,
                                Dose = PatientMedication.Dose,
                                DoseUnit = PatientMedication.DoseUnit,
                                Refills = PatientMedication.Refills,
                                Frequency = PatientMedication.Frequency,
                                Sig = PatientMedication.Sig,
                                Diagnosis = PatientMedication.Diagnosis,
                                StartDate = PatientMedication.StartDate,
                                ExpireDate = PatientMedication.ExpireDate,
                                Pharmacy = PatientMedication.Pharmacy,
                                Note = PatientMedication.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                                DuringVisit = PatientMedication.DuringVisit,
                            };
                            db.PatientMedications.Add(Medication);
                        }
                        else
                        {
                            PatientMedication Medication = db.PatientMedications.FirstOrDefault(p => p.PatientId == PatientMedication.PatientId
                                                  && p.FacilityId == PatientMedication.FacilityId 
                                                  && p.VisitId == PatientMedication.VisitId
                                                  && p.PatientMedicationCntr == PatientMedication.PatientMedicationCntr);

                            if (Medication != null)
                            {
                                Medication.MedicationName = PatientMedication.MedicationName;
                                Medication.CodeValue = PatientMedication.CodeValue;
                                Medication.CodeSystemId = PatientMedication.CodeSystemId;
                                Medication.Active = PatientMedication.Active;
                                Medication.Quantity = PatientMedication.Quantity;
                                Medication.RouteId = PatientMedication.RouteId;
                                Medication.Dose = PatientMedication.Dose;
                                Medication.DoseUnit = PatientMedication.DoseUnit;
                                Medication.Refills = PatientMedication.Refills;
                                Medication.Frequency = PatientMedication.Frequency;
                                Medication.Sig = PatientMedication.Sig;
                                Medication.Diagnosis = PatientMedication.Diagnosis;
                                Medication.StartDate = PatientMedication.StartDate;
                                Medication.ExpireDate = PatientMedication.ExpireDate;
                                Medication.Pharmacy = PatientMedication.Pharmacy;
                                Medication.Note = PatientMedication.Note;
                                Medication.UserId_Modified = UserId;
                                Medication.DateModified = System.DateTime.Now;
                                Medication.DuringVisit = PatientMedication.DuringVisit;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientMedication.Valid = false;
                    PatientMedication.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedication.Valid = false;
                PatientMedication.ErrorMessage = "Invalid Token";
            }
            return PatientMedication;
        }
        #endregion

        #region Delete PatientMedication Data
        //------------------------------------------------------------------------
        // Delete PatientMedication Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientMedication Data")]
        public PatientMedicationData DeletePatientMedicationData(PatientMedicationData PatientMedication, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        PatientMedication Medication = db.PatientMedications.FirstOrDefault(p => p.PatientId == PatientMedication.PatientId
                                                && p.FacilityId == PatientMedication.FacilityId 
                                                && p.VisitId == PatientMedication.VisitId
                                                && p.PatientMedicationCntr == PatientMedication.PatientMedicationCntr);

                        if (Medication != null)
                        {
                            Medication.Deleted = true;
                            Medication.UserId_Deleted = UserId;
                            Medication.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientMedication.Valid = false;
                    PatientMedication.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientMedication.Valid = false;
                PatientMedication.ErrorMessage = "Invalid Token";
            }
            return PatientMedication;
        }
        #endregion


        #region Get PatientProblem Data
        //------------------------------------------------------------------------
        // Get PatientProblem Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientProblem Data")]
        public PatientDocTableData GetPatientProblemData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientProblem = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientProblems
                                      join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientProblemCntr,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          p.Condition,
                                          p.EffectiveDate,
                                          p.LastChangeDate,
                                          p.ConditionStatusId,
                                          ConditionStatus = c.Value,
                                          p.Note,
                                          p.DateModified
                                      };

                        PatientProblem.dt = clsTableConverter.ToDataTable(results);
                        PatientProblem.dt.TableName = "PatientProblem";

                        PatientProblem.Valid = true;
                        PatientProblem.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientProblem, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientProblem.Valid = false;
                    PatientProblem.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProblem.Valid = false;
                PatientProblem.ErrorMessage = "Invalid Token";
            }
            return PatientProblem;
        }
        #endregion

        #region Save PatientProblem Data
        //------------------------------------------------------------------------
        // Save PatientProblem Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientProblem Data")]
        public PatientProblemData SavePatientProblemData(PatientProblemData PatientProblem, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientProblem.Valid = true;
            PatientProblem.ErrorMessage = "";

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

                        if (PatientProblem.PatientProblemCntr == 0)
                        {
                            //// Add Items
                            var Problem = new PatientProblem()
                            {
                                PatientId = PatientProblem.PatientId,
                                VisitId = PatientProblem.VisitId,
                                PatientProblemCntr = 0,
                                FacilityId = PatientProblem.FacilityId,
                                CodeValue = PatientProblem.CodeValue,
                                CodeSystemId = PatientProblem.CodeSystemId,
                                Condition = PatientProblem.Condition,
                                EffectiveDate = PatientProblem.EffectiveDate,
                                LastChangeDate = PatientProblem.LastChangeDate,
                                ConditionStatusId = PatientProblem.ConditionStatusId,
                                Note = PatientProblem.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientProblems.Add(Problem);
                        }
                        else
                        {
                            PatientProblem Problem = db.PatientProblems.FirstOrDefault(p => p.PatientId == PatientProblem.PatientId
                                                  && p.FacilityId == PatientProblem.FacilityId 
                                                  && p.VisitId == PatientProblem.VisitId
                                                  && p.PatientProblemCntr == PatientProblem.PatientProblemCntr);

                            if (Problem != null)
                            {
                                Problem.CodeValue = PatientProblem.CodeValue;
                                Problem.CodeSystemId = PatientProblem.CodeSystemId;
                                Problem.Condition = PatientProblem.Condition;
                                Problem.EffectiveDate = PatientProblem.EffectiveDate;
                                Problem.LastChangeDate = PatientProblem.LastChangeDate;
                                Problem.ConditionStatusId = PatientProblem.ConditionStatusId;
                                Problem.Note = PatientProblem.Note;
                                Problem.UserId_Modified = UserId;
                                Problem.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientProblem.Valid = false;
                    PatientProblem.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProblem.Valid = false;
                PatientProblem.ErrorMessage = "Invalid Token";
            }
            return PatientProblem;
        }
        #endregion

        #region Delete PatientProblem Data
        //------------------------------------------------------------------------
        // Delete PatientProblem Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientProblem Data")]
        public PatientProblemData DeletePatientProblemData(PatientProblemData PatientProblem, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientProblem Problem = db.PatientProblems.FirstOrDefault(p => p.PatientId == PatientProblem.PatientId
                                                && p.FacilityId == PatientProblem.FacilityId 
                                                && p.VisitId == PatientProblem.VisitId
                                                && p.PatientProblemCntr == PatientProblem.PatientProblemCntr);

                        if (Problem != null)
                        {
                            Problem.Deleted = true;
                            Problem.UserId_Deleted = UserId;
                            Problem.DateDeleted = System.DateTime.Now;
                            PatientProblem.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientProblem.Valid = false;
                    PatientProblem.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProblem.Valid = false;
                PatientProblem.ErrorMessage = "Invalid Token";
            }
            return PatientProblem;
        }
        #endregion


        #region Get PatientProcedure Data
        //------------------------------------------------------------------------
        // Get PatientProcedure Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientProcedure Data")]
        public PatientDocTableData GetPatientProcedureData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientProcedure = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientProcedures
                                      join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatProcedureCntr,
                                          p.Description,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          CodeSystem = s.Value,
                                          p.Diagnosis,
                                          p.PerformedBy,
                                          p.ServiceLocation,
                                          p.ServiceDate,
                                          p.Note,
                                          p.DateModified
                                      };

                        PatientProcedure.dt = clsTableConverter.ToDataTable(results);
                        PatientProcedure.dt.TableName = "PatientProcedure";

                        PatientProcedure.Valid = true;
                        PatientProcedure.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientProcedure, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientProcedure.Valid = false;
                    PatientProcedure.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProcedure.Valid = false;
                PatientProcedure.ErrorMessage = "Invalid Token";
            }
            return PatientProcedure;
        }
        #endregion

        #region Save PatientProcedure Data
        //------------------------------------------------------------------------
        // Save PatientProcedure Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientProcedure Data")]
        public PatientProcedureData SavePatientProcedureData(PatientProcedureData PatientProcedure, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientProcedure.Valid = true;
            PatientProcedure.ErrorMessage = "";

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

                        if (PatientProcedure.PatProcedureCntr == 0)
                        {
                            //// Add Items
                            var Procedure = new PatientProcedure()
                            {
                                PatientId = PatientProcedure.PatientId,
                                VisitId = PatientProcedure.VisitId,
                                PatProcedureCntr = 0,
                                FacilityId = PatientProcedure.FacilityId,
                                Description = PatientProcedure.Description,
                                CodeValue = PatientProcedure.CodeValue,
                                CodeSystemId = PatientProcedure.CodeSystemId,
                                Diagnosis = PatientProcedure.Diagnosis,
                                PerformedBy = PatientProcedure.PerformedBy,
                                ServiceLocation = PatientProcedure.ServiceLocation,
                                ServiceDate = PatientProcedure.ServiceDate,
                                Note = PatientProcedure.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientProcedures.Add(Procedure);
                        }
                        else
                        {
                            PatientProcedure Procedure = db.PatientProcedures.FirstOrDefault(p => p.PatientId == PatientProcedure.PatientId
                                                  && p.FacilityId == PatientProcedure.FacilityId 
                                                  && p.VisitId == PatientProcedure.VisitId
                                                  && p.PatProcedureCntr == PatientProcedure.PatProcedureCntr);

                            if (Procedure != null)
                            {
                                Procedure.Description = PatientProcedure.Description;
                                Procedure.CodeValue = PatientProcedure.CodeValue;
                                Procedure.CodeSystemId = PatientProcedure.CodeSystemId;
                                Procedure.Diagnosis = PatientProcedure.Diagnosis;
                                Procedure.PerformedBy = PatientProcedure.PerformedBy;
                                Procedure.ServiceLocation = PatientProcedure.ServiceLocation;
                                Procedure.ServiceDate = PatientProcedure.ServiceDate;
                                Procedure.Note = PatientProcedure.Note;
                                Procedure.UserId_Modified = UserId;
                                Procedure.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientProcedure.Valid = false;
                    PatientProcedure.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProcedure.Valid = false;
                PatientProcedure.ErrorMessage = "Invalid Token";
            }
            return PatientProcedure;
        }
        #endregion

        #region Delete PatientProcedure Data
        //------------------------------------------------------------------------
        // Delete PatientProcedure Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientProcedure Data")]
        public PatientProcedureData DeletePatientProcedureData(PatientProcedureData PatientProcedure, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        PatientProcedure Procedure = db.PatientProcedures.FirstOrDefault(p => p.PatientId == PatientProcedure.PatientId
                                                && p.FacilityId == PatientProcedure.FacilityId 
                                                && p.VisitId == PatientProcedure.VisitId
                                                && p.PatProcedureCntr == PatientProcedure.PatProcedureCntr);

                        if (Procedure != null)
                        {
                            Procedure.Deleted = true;
                            Procedure.UserId_Deleted = UserId;
                            Procedure.DateDeleted = System.DateTime.Now;
                            PatientProcedure.Valid = true;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientProcedure.Valid = false;
                    PatientProcedure.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientProcedure.Valid = false;
                PatientProcedure.ErrorMessage = "Invalid Token";
            }
            return PatientProcedure;
        }
        #endregion


        #region Get PatientSocialHist Data
        //------------------------------------------------------------------------
        // Get PatientSocialHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientSocialHist Data")]
        public PatientDocTableData GetPatientSocialHistData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientSocialHist = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientSocialHists
                                      join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatSocialHistCntr,
                                          p.Description,
                                          p.Qualifier,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          CodeSystem = s.Value,
                                          p.BeginDate,
                                          p.EndDate,
                                          p.Note,
                                          p.DateModified

                                      };

                        PatientSocialHist.dt = clsTableConverter.ToDataTable(results);
                        PatientSocialHist.dt.TableName = "PatientSocialHist";

                        PatientSocialHist.Valid = true;
                        PatientSocialHist.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientSocialHist, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientSocialHist.Valid = false;
                    PatientSocialHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSocialHist.Valid = false;
                PatientSocialHist.ErrorMessage = "Invalid Token";
            }
            return PatientSocialHist;
        }
        #endregion

        #region Save PatientSocialHist Data
        //------------------------------------------------------------------------
        // Save PatientSocialHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientSocialHist Data")]
        public PatientSocialHistData SavePatientSocialHistData(PatientSocialHistData PatientSocialHist, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSocialHist.Valid = true;
            PatientSocialHist.ErrorMessage = "";

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

                        if (PatientSocialHist.PatSocialHistCntr == 0)
                        {
                            //// Add Items
                            var SocialHist = new PatientSocialHist()
                            {
                                PatientId = PatientSocialHist.PatientId,
                                VisitId = PatientSocialHist.VisitId,
                                PatSocialHistCntr = 0,
                                FacilityId = PatientSocialHist.FacilityId,
                                Description = PatientSocialHist.Description,
                                Qualifier = PatientSocialHist.Qualifier,
                                CodeValue = PatientSocialHist.CodeValue,
                                CodeSystemId = PatientSocialHist.CodeSystemId,
                                BeginDate = PatientSocialHist.BeginDate,
                                EndDate = PatientSocialHist.EndDate,
                                Note = PatientSocialHist.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientSocialHists.Add(SocialHist);
                        }
                        else
                        {
                            PatientSocialHist SocialHist = db.PatientSocialHists.FirstOrDefault(p => p.PatientId == PatientSocialHist.PatientId
                                                  && p.FacilityId == PatientSocialHist.FacilityId 
                                                  && p.VisitId == PatientSocialHist.VisitId
                                                  && p.PatSocialHistCntr == PatientSocialHist.PatSocialHistCntr);

                            if (SocialHist != null)
                            {
                                SocialHist.Description = PatientSocialHist.Description;
                                SocialHist.Qualifier = PatientSocialHist.Qualifier;
                                SocialHist.CodeValue = PatientSocialHist.CodeValue;
                                SocialHist.CodeValue = PatientSocialHist.CodeValue;
                                SocialHist.BeginDate = PatientSocialHist.BeginDate;
                                SocialHist.EndDate = PatientSocialHist.EndDate;
                                SocialHist.Note = PatientSocialHist.Note;
                                SocialHist.UserId_Modified = UserId;
                                SocialHist.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientSocialHist.Valid = false;
                    PatientSocialHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSocialHist.Valid = false;
                PatientSocialHist.ErrorMessage = "Invalid Token";
            }
            return PatientSocialHist;
        }
        #endregion

        #region Delete PatientSocialHist Data
        //------------------------------------------------------------------------
        // Delete PatientSocialHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientSocialHist Data")]
        public PatientSocialHistData DeletePatientSocialHistData(PatientSocialHistData PatientSocialHist, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        PatientSocialHist SocialHist = db.PatientSocialHists.FirstOrDefault(p => p.PatientId == PatientSocialHist.PatientId
                                                && p.FacilityId == PatientSocialHist.FacilityId 
                                                && p.VisitId == PatientSocialHist.VisitId
                                                && p.PatSocialHistCntr == PatientSocialHist.PatSocialHistCntr);

                        if (SocialHist != null)
                        {
                            SocialHist.Deleted = true;
                            SocialHist.UserId_Deleted = UserId;
                            SocialHist.DateDeleted = System.DateTime.Now;
                            PatientSocialHist.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientSocialHist.Valid = false;
                    PatientSocialHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSocialHist.Valid = false;
                PatientSocialHist.ErrorMessage = "Invalid Token";
            }
            return PatientSocialHist;
        }
        #endregion


        #region Get PatientSurgicalHist Data
        //------------------------------------------------------------------------
        // Get PatientSurgicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientSurgicalHist Data")]
        public PatientDocTableData GetPatientSurgicalHistData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientSurgicalHist = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientSurgicalHists
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatSurgicalHistCntr,
                                          p.Description,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          p.Diagnosis,
                                          p.PerformedBy,
                                          p.ServiceLocation,
                                          p.ServiceDate,
                                          p.Note,
                                          p.DateModified
                                      };

                        PatientSurgicalHist.dt = clsTableConverter.ToDataTable(results);
                        PatientSurgicalHist.dt.TableName = "PatientSurgicalHist";

                        PatientSurgicalHist.Valid = true;
                        PatientSurgicalHist.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientSurgicalHist, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientSurgicalHist.Valid = false;
                    PatientSurgicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSurgicalHist.Valid = false;
                PatientSurgicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientSurgicalHist;
        }
        #endregion

        #region Save PatientSurgicalHist Data
        //------------------------------------------------------------------------
        // Save PatientSurgicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientSurgicalHist Data")]
        public PatientSurgicalHistData SavePatientSurgicalHistData(PatientSurgicalHistData PatientSurgicalHist, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSurgicalHist.Valid = true;
            PatientSurgicalHist.ErrorMessage = "";

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

                        if (PatientSurgicalHist.PatSurgicalHistCntr == 0)
                        {
                            //// Add Items
                            var SurgicalHist = new PatientSurgicalHist()
                            {
                                PatientId = PatientSurgicalHist.PatientId,
                                VisitId = PatientSurgicalHist.VisitId,
                                PatSurgicalHistCntr = 0,
                                FacilityId = PatientSurgicalHist.FacilityId,
                                Description = PatientSurgicalHist.Description,
                                CodeValue = PatientSurgicalHist.CodeValue,
                                CodeSystemId = PatientSurgicalHist.CodeSystemId,
                                Diagnosis = PatientSurgicalHist.Diagnosis,
                                PerformedBy = PatientSurgicalHist.PerformedBy,
                                ServiceLocation = PatientSurgicalHist.ServiceLocation,
                                ServiceDate = PatientSurgicalHist.ServiceDate,
                                Note = PatientSurgicalHist.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientSurgicalHists.Add(SurgicalHist);
                        }
                        else
                        {
                            PatientSurgicalHist SurgicalHist = db.PatientSurgicalHists.FirstOrDefault(p => p.PatientId == PatientSurgicalHist.PatientId
                                                  && p.FacilityId == PatientSurgicalHist.FacilityId 
                                                  && p.VisitId == PatientSurgicalHist.VisitId
                                                  && p.PatSurgicalHistCntr == PatientSurgicalHist.PatSurgicalHistCntr);

                            if (SurgicalHist != null)
                            {
                                SurgicalHist.Description = PatientSurgicalHist.Description;
                                SurgicalHist.CodeValue = PatientSurgicalHist.CodeValue;
                                SurgicalHist.CodeSystemId = PatientSurgicalHist.CodeSystemId;
                                SurgicalHist.Diagnosis = PatientSurgicalHist.Diagnosis;
                                SurgicalHist.PerformedBy = PatientSurgicalHist.PerformedBy;
                                SurgicalHist.ServiceLocation = PatientSurgicalHist.ServiceLocation;
                                SurgicalHist.ServiceDate = PatientSurgicalHist.ServiceDate;
                                SurgicalHist.Note = PatientSurgicalHist.Note;
                                SurgicalHist.UserId_Modified = UserId;
                                SurgicalHist.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientSurgicalHist.Valid = false;
                    PatientSurgicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSurgicalHist.Valid = false;
                PatientSurgicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientSurgicalHist;
        }
        #endregion

        #region Delete PatientSurgicalHist Data
        //------------------------------------------------------------------------
        // Delete PatientSurgicalHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientSurgicalHist Data")]
        public PatientSurgicalHistData DeletePatientSurgicalHistData(PatientSurgicalHistData PatientSurgicalHist, string Token, Int64 UserId, Int64 FacilityId)
        {
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
                        PatientSurgicalHist SurgicalHist = db.PatientSurgicalHists.FirstOrDefault(p => p.PatientId == PatientSurgicalHist.PatientId
                                                && p.FacilityId == PatientSurgicalHist.FacilityId 
                                                && p.VisitId == PatientSurgicalHist.VisitId
                                                && p.PatSurgicalHistCntr == PatientSurgicalHist.PatSurgicalHistCntr);

                        if (SurgicalHist != null)
                        {
                            SurgicalHist.Deleted = true;
                            SurgicalHist.UserId_Deleted = UserId;
                            SurgicalHist.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientSurgicalHist.Valid = false;
                    PatientSurgicalHist.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSurgicalHist.Valid = false;
                PatientSurgicalHist.ErrorMessage = "Invalid Token";
            }
            return PatientSurgicalHist;
        }
        #endregion


        #region Get PatientVitalSign Data
        //------------------------------------------------------------------------
        // Get PatientVitalSign Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientVitalSign Data")]
        public PatientDocTableData GetPatientVitalSignData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientVitalSign = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientVitalSigns
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientVitalCntr,
                                          p.VitalDate,
                                          p.HeightIn,
                                          p.WeightLb,
                                          p.BloodPressurePosn,
                                          p.Systolic,
                                          p.Diastolic,
                                          p.Pulse,
                                          p.Respiration,
                                          p.Temperature,
                                          p.DateModified
                                      };

                        PatientVitalSign.dt = clsTableConverter.ToDataTable(results);
                        PatientVitalSign.dt.TableName = "PatientVitalSign";

                        PatientVitalSign.Valid = true;
                        PatientVitalSign.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientVitalSign, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientVitalSign.Valid = false;
                    PatientVitalSign.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVitalSign.Valid = false;
                PatientVitalSign.ErrorMessage = "Invalid Token";
            }
            return PatientVitalSign;
        }
        #endregion

        #region Save PatientVitalSign Data
        //------------------------------------------------------------------------
        // Save PatientVitalSign Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientVitalSign Data")]
        public PatientVitalSignData SavePatientVitalSignData(PatientVitalSignData PatientVitalSign, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientVitalSign.Valid = true;
            PatientVitalSign.ErrorMessage = "";

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

                        if (PatientVitalSign.PatientVitalCntr == 0)
                        {
                            //// Add Items
                            var VitalSign = new PatientVitalSign()
                            {
                                PatientId = PatientVitalSign.PatientId,
                                VisitId = PatientVitalSign.VisitId,
                                PatientVitalCntr = 0,
                                FacilityId = PatientVitalSign.FacilityId,
                                VitalDate = PatientVitalSign.VitalDate,
                                HeightIn = PatientVitalSign.HeightIn,
                                WeightLb = PatientVitalSign.WeightLb,
                                BloodPressurePosn = PatientVitalSign.BloodPressurePosn,
                                Systolic = PatientVitalSign.Systolic,
                                Diastolic = PatientVitalSign.Diastolic,
                                Pulse = PatientVitalSign.Pulse,
                                Respiration = PatientVitalSign.Respiration,
                                Temperature = PatientVitalSign.Temperature,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientVitalSigns.Add(VitalSign);
                        }
                        else
                        {
                            PatientVitalSign VitalSign = db.PatientVitalSigns.First(p => p.PatientId == PatientVitalSign.PatientId
                                                  && p.FacilityId == PatientVitalSign.FacilityId 
                                                  && p.VisitId == PatientVitalSign.VisitId
                                                  && p.PatientVitalCntr == PatientVitalSign.PatientVitalCntr);

                            if (VitalSign != null)
                            {
                                VitalSign.VitalDate = PatientVitalSign.VitalDate;
                                VitalSign.HeightIn = PatientVitalSign.HeightIn;
                                VitalSign.WeightLb = PatientVitalSign.WeightLb;
                                VitalSign.BloodPressurePosn = PatientVitalSign.BloodPressurePosn;
                                VitalSign.Systolic = PatientVitalSign.Systolic;
                                VitalSign.Diastolic = PatientVitalSign.Diastolic;
                                VitalSign.Pulse = PatientVitalSign.Pulse;
                                VitalSign.Respiration = PatientVitalSign.Respiration;
                                VitalSign.Temperature = PatientVitalSign.Temperature;
                                VitalSign.UserId_Modified = UserId;
                                VitalSign.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientVitalSign.Valid = false;
                    PatientVitalSign.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVitalSign.Valid = false;
                PatientVitalSign.ErrorMessage = "Invalid Token";
            }
            return PatientVitalSign;
        }
        #endregion

        #region Delete PatientVitalSign Data
        //------------------------------------------------------------------------
        // Delete PatientVitalSign Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientVitalSign Data")]
        public PatientVitalSignData DeletePatientVitalSignData(PatientVitalSignData PatientVitalSign, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientVitalSign VitalSign = db.PatientVitalSigns.First(p => p.PatientId == PatientVitalSign.PatientId
                                                && p.FacilityId == PatientVitalSign.FacilityId 
                                                && p.VisitId == PatientVitalSign.VisitId
                                                && p.PatientVitalCntr == PatientVitalSign.PatientVitalCntr);

                        if (VitalSign != null)
                        {
                            VitalSign.Deleted = true;
                            VitalSign.UserId_Deleted = UserId;
                            VitalSign.DateDeleted = System.DateTime.Now;
                            PatientVitalSign.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientVitalSign.Valid = false;
                    PatientVitalSign.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientVitalSign.Valid = false;
                PatientVitalSign.ErrorMessage = "Invalid Token";
            }
            return PatientVitalSign;
        }
        #endregion


        #region Get PatientImmunization Data
        //------------------------------------------------------------------------
        // Get PatientImmunization Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientImmunization Data")]
        public PatientDocTableData GetPatientImmunizationData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientImmunization = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientImmunizations
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientImmunizationCntr,
                                          p.ImmunizationDate,
                                          p.ImmunizationTime,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          p.Vaccine,
                                          p.Amount,
                                          p.Route,
                                          p.Site,
                                          p.Sequence,
                                          p.ExpirationDate,
                                          p.LotNumber,
                                          p.Manufacturer,
                                          p.Note,
                                          p.DateModified
                                      };

                        PatientImmunization.dt = clsTableConverter.ToDataTable(results);
                        PatientImmunization.dt.TableName = "PatientImmunization";

                        PatientImmunization.Valid = true;
                        PatientImmunization.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientImmunization, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientImmunization.Valid = false;
                    PatientImmunization.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientImmunization.Valid = false;
                PatientImmunization.ErrorMessage = "Invalid Token";
            }
            return PatientImmunization;
        }
        #endregion

        #region Save PatientImmunization Data
        //------------------------------------------------------------------------
        // Save PatientImmunization Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientImmunization Data")]
        public PatientImmunizationData SavePatientImmunizationData(PatientImmunizationData PatientImmunization, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientImmunization.Valid = true;
            PatientImmunization.ErrorMessage = "";

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

                        if (PatientImmunization.PatientImmunizationCntr == 0)
                        {
                            //// Add Items
                            var Immunization = new PatientImmunization()
                            {
                                PatientId = PatientImmunization.PatientId,
                                VisitId = PatientImmunization.VisitId,
                                PatientImmunizationCntr = 0,
                                FacilityId = PatientImmunization.FacilityId,
                                ImmunizationDate = PatientImmunization.ImmunizationDate,
                                ImmunizationTime = PatientImmunization.ImmunizationTime,
                                CodeValue = PatientImmunization.CodeValue,
                                CodeSystemId = PatientImmunization.CodeSystemId,
                                Vaccine = PatientImmunization.Vaccine,
                                Amount = PatientImmunization.Amount,
                                Route = PatientImmunization.Route,
                                Site = PatientImmunization.Site,
                                Sequence = PatientImmunization.Sequence,
                                ExpirationDate = PatientImmunization.ExpirationDate,
                                LotNumber = PatientImmunization.LotNumber,
                                Manufacturer = PatientImmunization.Manufacturer,
                                Note = PatientImmunization.Note,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientImmunizations.Add(Immunization);
                        }
                        else
                        {
                            PatientImmunization Immunization = db.PatientImmunizations.First(p => p.PatientId == PatientImmunization.PatientId
                                                  && p.FacilityId == PatientImmunization.FacilityId 
                                                  && p.VisitId == PatientImmunization.VisitId
                                                  && p.PatientImmunizationCntr == PatientImmunization.PatientImmunizationCntr);

                            if (Immunization != null)
                            {
                                Immunization.ImmunizationDate = PatientImmunization.ImmunizationDate;
                                Immunization.ImmunizationTime = PatientImmunization.ImmunizationTime;
                                Immunization.CodeValue = PatientImmunization.CodeValue;
                                Immunization.CodeSystemId = PatientImmunization.CodeSystemId;
                                Immunization.Vaccine = PatientImmunization.Vaccine;
                                Immunization.Amount = PatientImmunization.Amount;
                                Immunization.Route = PatientImmunization.Route;
                                Immunization.Site = PatientImmunization.Site;
                                Immunization.Sequence = PatientImmunization.Sequence;
                                Immunization.ExpirationDate = PatientImmunization.ExpirationDate;
                                Immunization.LotNumber = PatientImmunization.LotNumber;
                                Immunization.Manufacturer = PatientImmunization.Manufacturer;
                                Immunization.Note = PatientImmunization.Note;
                                Immunization.UserId_Modified = UserId;
                                Immunization.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientImmunization.Valid = false;
                    PatientImmunization.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientImmunization.Valid = false;
                PatientImmunization.ErrorMessage = "Invalid Token";
            }
            return PatientImmunization;
        }
        #endregion

        #region Delete PatientImmunization Data
        //------------------------------------------------------------------------
        // Delete PatientImmunization Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientImmunization Data")]
        public PatientImmunizationData DeletePatientImmunizationData(PatientImmunizationData PatientImmunization, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientImmunization Immunization = db.PatientImmunizations.First(p => p.PatientId == PatientImmunization.PatientId
                                                  && p.FacilityId == PatientImmunization.FacilityId 
                                                  && p.VisitId == PatientImmunization.VisitId
                                                  && p.PatientImmunizationCntr == PatientImmunization.PatientImmunizationCntr);

                        if (Immunization != null)
                        {
                            Immunization.Deleted = true;
                            Immunization.UserId_Deleted = UserId;
                            Immunization.DateDeleted = System.DateTime.Now;
                            PatientImmunization.Valid = true; // Added valid=true by Talha Amin
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientImmunization.Valid = false;
                    PatientImmunization.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientImmunization.Valid = false;
                PatientImmunization.ErrorMessage = "Invalid Token";
            }
            return PatientImmunization;
        }
        #endregion


        #region Get PatientLabResult Data
        //------------------------------------------------------------------------
        // Get PatientLabResult Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientLabResult Data")]
        public PatientLabResultData GetPatientLabResultData(PatientLabResultParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientLabResultData PatientLabResult = new PatientLabResultData();

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
                        PatientLabResult results = db.PatientLabResults.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.LabResultCntr == Parms.LabResultCntr);

                        if (results != null)
                        {
                            PatientLabResult.PatientId = results.PatientId;
                            PatientLabResult.VisitId = results.VisitId;
                            PatientLabResult.LabResultCntr = results.LabResultCntr;
                            PatientLabResult.FacilityId = Convert.ToInt64(results.FacilityId);
                            PatientLabResult.ProviderId_Requested = Convert.ToInt64(results.ProviderId_Requested);
                            PatientLabResult.CodeValue = results.CodeValue;
                            PatientLabResult.CodeSystemId = Convert.ToInt32(results.CodeSystemId);
                            PatientLabResult.LabName = results.LabName;
                            PatientLabResult.OrderDate = Convert.ToDateTime(results.OrderDate);
                            PatientLabResult.CollectionDate = Convert.ToDateTime(results.CollectionDate);
                            PatientLabResult.Requisiton = results.Requisiton;
                            PatientLabResult.Specimen = results.Specimen;
                            PatientLabResult.SpecimenSource = results.SpecimenSource;
                            PatientLabResult.ReportDate = Convert.ToDateTime(results.ReportDate);
                            PatientLabResult.ReviewDate = Convert.ToDateTime(results.ReviewDate);
                            PatientLabResult.Reviewer = results.Reviewer;

                            //var tests = db.PatientLabResultTests.Where(p => p.PatientId == Parms.PatientId && p.VisitId == Parms.VisitId && p.LabResultCntr == Parms.LabResultCntr);

                            var tests = from p in db.PatientLabResultTests
                                        where p.PatientId == Parms.PatientId && p.VisitId == Parms.VisitId && p.LabResultCntr == Parms.LabResultCntr
                                          select new
                                          {
                                              p.TestCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.Component,
                                              p.Result,
                                              p.RefRange,
                                              p.Units,
                                              p.Abnormal,
                                              p.ResultStatus
                                          };

                            PatientLabResult.dtTests = clsTableConverter.ToDataTable(tests);
                            PatientLabResult.dtTests.TableName = "Tests";

                            PatientLabResult.Valid = true;
                            PatientLabResult.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, Parms.LabResultCntr, FacilityId, DocTypePatientLabResults, UserId, ActionRead);


                        }
                        else
                        {
                            PatientLabResult.Valid = false;
                            PatientLabResult.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientLabResult.Valid = false;
                    PatientLabResult.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientLabResult.Valid = false;
                PatientLabResult.ErrorMessage = "Invalid Token";
            }
            return PatientLabResult;
        }
        #endregion

        #region Save PatientLabResult Data
        //------------------------------------------------------------------------
        // Save PatientLabResult Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientLabResult Data")]
        public PatientLabResultData SavePatientLabResultData(PatientLabResultData PatientLabResult, string Token, Int64 UserId, Int64 FacilityId)
        {
            //  Note:  Patient Lab Results Can Only Be Saved, Not Modified

            PatientLabResult.Valid = true;
            PatientLabResult.ErrorMessage = "";
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

                        // Add Items
                        var LabResult = new PatientLabResult()
                        {
                            PatientId = PatientLabResult.PatientId,
                            VisitId = PatientLabResult.VisitId,
                            LabResultCntr = 0,
                            FacilityId = PatientLabResult.FacilityId,
                            ProviderId_Requested = PatientLabResult.ProviderId_Requested,
                            CodeValue = PatientLabResult.CodeValue,
                            CodeSystemId = PatientLabResult.CodeSystemId,
                            LabName = PatientLabResult.LabName,
                            OrderDate = PatientLabResult.OrderDate,
                            CollectionDate = PatientLabResult.CollectionDate,
                            Requisiton = PatientLabResult.Requisiton,
                            Specimen = PatientLabResult.Specimen,
                            SpecimenSource = PatientLabResult.SpecimenSource,
                            ReportDate = PatientLabResult.ReportDate,
                            ReviewDate = PatientLabResult.ReviewDate,
                            Reviewer = PatientLabResult.Reviewer,
                            UserId_Created = UserId,
                            DateCreated = System.DateTime.Now,
                        };

                        if (PatientLabResult.dtTests != null)
                        {
                            int cntr = 0;
                            foreach (DataRow dr in PatientLabResult.dtTests.Rows)
                            {
                                cntr++;
                                var LabTest = new PatientLabResultTest()
                                {
                                    TestCntr = cntr,
                                    CodeValue = dr["CodeValue"].ToString(),
                                    CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                    Component = dr["Component"].ToString(),
                                    Result = dr["Result"].ToString(),
                                    RefRange = dr["RefRange"].ToString(),
                                    Units = dr["Units"].ToString(),
                                    Abnormal = dr["Abnormal"].ToString(),
                                    ResultStatus = dr["ResultStatus"].ToString(),
                                };
                                LabResult.PatientLabResultTests.Add(LabTest);

                            }
                        }
                        db.PatientLabResults.Add(LabResult);

                        db.SaveChanges();
                    }

                    
                    
                }
                catch (Exception ex)
                {
                    PatientLabResult.Valid = false;
                    PatientLabResult.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientLabResult.Valid = false;
                PatientLabResult.ErrorMessage = "Invalid Token";
            }
            return PatientLabResult;
        }
        #endregion

        #region Get PatientLabResult List
        //------------------------------------------------------------------------
        // Get PatientLabResult List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientLabResult List")]
        public PatientDocTableData GetPatientLabResultList(PatientLabResultParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientLabResult = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientLabResults
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId
                                      select new
                                      {
                                          p.LabResultCntr,
                                          p.ProviderId_Requested,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          p.LabName,
                                          p.OrderDate,
                                          p.CollectionDate,
                                          p.Requisiton,
                                          p.Specimen,
                                          p.SpecimenSource,
                                          p.ReportDate,
                                          p.ReviewDate,
                                          p.Reviewer
                                      };

                        PatientLabResult.dt = clsTableConverter.ToDataTable(results);
                        PatientLabResult.dt.TableName = "PatientLabResult";

                        PatientLabResult.Valid = true;
                        PatientLabResult.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientLabResults, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientLabResult.Valid = false;
                    PatientLabResult.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientLabResult.Valid = false;
                PatientLabResult.ErrorMessage = "Invalid Token";
            }
            return PatientLabResult;
        }
        #endregion


        #region Get PatientEmergency Data
        //------------------------------------------------------------------------
        // Get PatientEmergency Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientEmergency Data")]
        public PatientDocTableData GetPatientEmergencyData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientEmergency = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientEmergencies
                            where p.PatientId == Parms.PatientId && p.Deleted == false
                            select new
                            {
                                p.PatientEmergencyId,
                                p.Name,
                                p.Address1,
                                p.Address2,
                                p.Address3,
                                p.City,
                                p.State,
                                p.PostalCode,
                                p.CountryCode,
                                p.HomePhone,
                                p.MobilePhone,
                                p.WorkPhone,
                                p.Email,
                                p.IsPrimary,
                                p.RelationshipId,
                                p.OnCard
                            };

                        PatientEmergency.dt = clsTableConverter.ToDataTable(results);
                        PatientEmergency.dt.TableName = "PatientEmergency";

                        PatientEmergency.Valid = true;
                        PatientEmergency.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientEmergency, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientEmergency.Valid = false;
                    PatientEmergency.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientEmergency.Valid = false;
                PatientEmergency.ErrorMessage = "Invalid Token";
            }
            return PatientEmergency;
        }
        #endregion

        #region Save PatientEmergency Data
        //------------------------------------------------------------------------
        // Save PatientEmergency Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientEmergency Data")]
        public PatientEmergencyData SavePatientEmergencyData(PatientEmergencyData PatientEmergency, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientEmergency.Valid = true;
            PatientEmergency.ErrorMessage = "";
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

                        if (PatientEmergency.PatientEmergencyId == 0)
                        {
                            //// Add Items
                            var Emergency = new PatientEmergency()
                            {
                                PatientId = PatientEmergency.PatientId,
                                PatientEmergencyId = 0,
                                Name = PatientEmergency.Name,
                                Address1 = PatientEmergency.Address1,
                                Address2 = PatientEmergency.Address2,
                                Address3 = PatientEmergency.Address3,
                                City = PatientEmergency.City,
                                State = PatientEmergency.State,
                                PostalCode = PatientEmergency.PostalCode,
                                CountryCode = PatientEmergency.CountryCode,
                                HomePhone = PatientEmergency.HomePhone,
                                MobilePhone = PatientEmergency.MobilePhone,
                                WorkPhone = PatientEmergency.WorkPhone,
                                Email = PatientEmergency.Email,
                                IsPrimary = PatientEmergency.IsPrimary,
                                RelationshipId = PatientEmergency.RelationshipId,
                                OnCard = PatientEmergency.OnCard,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientEmergencies.Add(Emergency);
                            db.SaveChanges();
                            PatientEmergency.PatientEmergencyId = Emergency.PatientEmergencyId;
                        }
                        else
                        {
                            PatientEmergency Emergency = db.PatientEmergencies.First(p => p.PatientId == PatientEmergency.PatientId
                                                  && p.PatientEmergencyId == PatientEmergency.PatientEmergencyId);

                            if (Emergency != null)
                            {
                                Emergency.Name = PatientEmergency.Name;
                                Emergency.Address1 = PatientEmergency.Address1;
                                Emergency.Address2 = PatientEmergency.Address2;
                                Emergency.Address3 = PatientEmergency.Address3;
                                Emergency.City = PatientEmergency.City;
                                Emergency.State = PatientEmergency.State;
                                Emergency.PostalCode = PatientEmergency.PostalCode;
                                Emergency.CountryCode = PatientEmergency.CountryCode;
                                Emergency.HomePhone = PatientEmergency.HomePhone;
                                Emergency.MobilePhone = PatientEmergency.MobilePhone;
                                Emergency.WorkPhone = PatientEmergency.WorkPhone;
                                Emergency.Email = PatientEmergency.Email;
                                Emergency.IsPrimary = PatientEmergency.IsPrimary;
                                Emergency.RelationshipId = PatientEmergency.RelationshipId;
                                Emergency.OnCard = PatientEmergency.OnCard;
                                Emergency.UserId_Modified = UserId;
                                Emergency.DateModified = System.DateTime.Now;
                                db.SaveChanges();
                            }
                        }
                        

                        if (PatientEmergency.IsPrimary)
                        {
                            bool finished = false;
                            try
                            {
                                do
                                {
                                    PatientEmergency Emergency = db.PatientEmergencies.First(p => p.PatientId == PatientEmergency.PatientId
                                                          && p.PatientEmergencyId != PatientEmergency.PatientEmergencyId && p.IsPrimary == true);

                                    if (Emergency != null)
                                    {
                                        Emergency.IsPrimary = false;
                                        Emergency.UserId_Modified = UserId;
                                        Emergency.DateModified = System.DateTime.Now;
                                        db.SaveChanges();
                                    }
                                    else
                                        finished = true;
                                }
                                while (finished == false);
                            }
                            catch
                            {
                                // If not items are available to select, it will throw an error
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientEmergency.Valid = false;
                    PatientEmergency.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientEmergency.Valid = false;
                PatientEmergency.ErrorMessage = "Invalid Token";
            }
            return PatientEmergency;
        }
        #endregion

        #region Delete PatientEmergency Data
        //------------------------------------------------------------------------
        // Delete PatientEmergency Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientEmergency Data")]
        public PatientEmergencyData DeletePatientEmergencyData(PatientEmergencyData PatientEmergency, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientEmergency Emergency = db.PatientEmergencies.First(p => p.PatientId == PatientEmergency.PatientId
                                                  && p.PatientEmergencyId == PatientEmergency.PatientEmergencyId);

                        if (Emergency != null)
                        {
                            Emergency.Deleted = true;
                            Emergency.UserId_Deleted = UserId;
                            Emergency.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientEmergency.Valid = false;
                    PatientEmergency.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientEmergency.Valid = false;
                PatientEmergency.ErrorMessage = "Invalid Token";
            }
            return PatientEmergency;
        }
        #endregion


        #region Get PatientInsurance Data
        //------------------------------------------------------------------------
        // Get PatientInsurance Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientInsurance Data")]
        public PatientDocTableData GetPatientInsuranceData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientInsurance = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientInsurances
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientInsuranceId,
                                          p.PlanName,
                                          p.MemberNumber,
                                          p.GroupNumber,
                                          p.Subscriber,
                                          p.Relationship,
                                          p.EffectiveDate
                                      };


                        PatientInsurance.dt = clsTableConverter.ToDataTable(results);
                        PatientInsurance.dt.TableName = "PatientInsurance";

                        PatientInsurance.Valid = true;
                        PatientInsurance.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientInsurance, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientInsurance.Valid = false;
                    PatientInsurance.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientInsurance.Valid = false;
                PatientInsurance.ErrorMessage = "Invalid Token";
            }
            return PatientInsurance;
        }
        #endregion

        #region Save PatientInsurance Data
        //------------------------------------------------------------------------
        // Save PatientInsurance Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientInsurance Data")]
        public PatientInsuranceData SavePatientInsuranceData(PatientInsuranceData PatientInsurance, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientInsurance.Valid = true;
            PatientInsurance.ErrorMessage = "";

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

                        if (PatientInsurance.PatientInsuranceId == 0)
                        {
                            //// Add Items
                            var Insurance = new PatientInsurance()
                            {
                                PatientId = PatientInsurance.PatientId,
                                VisitId = PatientInsurance.VisitId,
                                PatientInsuranceId = 0,
                                FacilityId = PatientInsurance.FacilityId,
                                PlanName = PatientInsurance.PlanName,
                                MemberNumber = PatientInsurance.MemberNumber,
                                GroupNumber = PatientInsurance.GroupNumber,
                                Subscriber = PatientInsurance.Subscriber,
                                Relationship = PatientInsurance.Relationship,
                                EffectiveDate = PatientInsurance.EffectiveDate,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientInsurances.Add(Insurance);
                        }
                        else
                        {
                            PatientInsurance Insurance = db.PatientInsurances.FirstOrDefault(p => p.PatientId == PatientInsurance.PatientId
                                                  && p.FacilityId == PatientInsurance.FacilityId 
                                                  && p.VisitId == PatientInsurance.VisitId
                                                  && p.PatientInsuranceId == PatientInsurance.PatientInsuranceId);

                            if (Insurance != null)
                            {
                                Insurance.PlanName = PatientInsurance.PlanName;
                                Insurance.MemberNumber = PatientInsurance.MemberNumber;
                                Insurance.GroupNumber = PatientInsurance.GroupNumber;
                                Insurance.Subscriber = PatientInsurance.Subscriber;
                                Insurance.Relationship = PatientInsurance.Relationship;
                                Insurance.EffectiveDate = PatientInsurance.EffectiveDate;
                                Insurance.UserId_Modified = UserId;
                                Insurance.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientInsurance.Valid = false;
                    PatientInsurance.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientInsurance.Valid = false;
                PatientInsurance.ErrorMessage = "Invalid Token";
            }
            return PatientInsurance;
        }
        #endregion

        #region Delete PatientInsurance Data
        //------------------------------------------------------------------------
        // Delete PatientInsurance Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientInsurance Data")]
        public PatientInsuranceData DeletePatientInsuranceData(PatientInsuranceData PatientInsurance, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientInsurance Insurance = db.PatientInsurances.FirstOrDefault(p => p.PatientId == PatientInsurance.PatientId
                                                && p.FacilityId == PatientInsurance.FacilityId 
                                                && p.VisitId == PatientInsurance.VisitId
                                                && p.PatientInsuranceId == PatientInsurance.PatientInsuranceId);

                        if (Insurance != null)
                        {
                            Insurance.Deleted = true;
                            Insurance.UserId_Deleted = UserId;
                            Insurance.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientInsurance.Valid = false;
                    PatientInsurance.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientInsurance.Valid = false;
                PatientInsurance.ErrorMessage = "Invalid Token";
            }
            return PatientInsurance;
        }
        #endregion


        #region Get PatientPharmacy Data
        //------------------------------------------------------------------------
        // Get PatientPharmacy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientPharmacy Data")]
        public PatientDocTableData GetPatientPharmacyData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientPharmacy = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientPharmacies
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      orderby p.Preferred descending, p.PharmacyCntr descending
                                      select new
                                      {
                                          p.PharmacyCntr,
                                          p.PharmacyName,
                                          p.Address1,
                                          p.Address2,
                                          p.Address3,
                                          p.City,
                                          p.State,
                                          p.PostalCode,
                                          p.CountryCode,
                                          p.Phone,
                                          p.Preferred,
                                          p.Note
                                      };

                        PatientPharmacy.dt = clsTableConverter.ToDataTable(results);
                        PatientPharmacy.dt.TableName = "PatientPharmacy";

                        PatientPharmacy.Valid = true;
                        PatientPharmacy.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientPharmacy, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientPharmacy.Valid = false;
                    PatientPharmacy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPharmacy.Valid = false;
                PatientPharmacy.ErrorMessage = "Invalid Token";
            }
            return PatientPharmacy;
        }
        #endregion

        #region Save PatientPharmacy Data
        //------------------------------------------------------------------------
        // Save PatientPharmacy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientPharmacy Data")]
        public PatientPharmacyData SavePatientPharmacyData(PatientPharmacyData PatientPharmacy, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientPharmacy.Valid = true;
            PatientPharmacy.ErrorMessage = "";

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

                        if (PatientPharmacy.PharmacyCntr == 0)
                        {
                            //// Add Items
                            var Pharmacy = new PatientPharmacy()
                            {
                                PatientId = PatientPharmacy.PatientId,
                                PharmacyCntr = 0,
                                PharmacyName = PatientPharmacy.PharmacyName,
                                Address1 = PatientPharmacy.Address1,
                                Address2 = PatientPharmacy.Address2,
                                Address3 = PatientPharmacy.Address3,
                                City = PatientPharmacy.City,
                                State = PatientPharmacy.State,
                                PostalCode = PatientPharmacy.PostalCode,
                                CountryCode = PatientPharmacy.CountryCode,
                                Phone = PatientPharmacy.Phone,
                                Preferred = PatientPharmacy.Preferred,
                                Note = PatientPharmacy.Note,                                
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientPharmacies.Add(Pharmacy);
                            PatientPharmacy.Preferred = Convert.ToBoolean(Pharmacy.Preferred);
                        }

                        else
                        {
                            PatientPharmacy Pharmacy = db.PatientPharmacies.FirstOrDefault(p => p.PatientId == PatientPharmacy.PatientId
                                                  && p.PharmacyCntr == PatientPharmacy.PharmacyCntr);

                            if (Pharmacy != null)
                            {
                                Pharmacy.PharmacyName = PatientPharmacy.PharmacyName;
                                Pharmacy.Address1 = PatientPharmacy.Address1;
                                Pharmacy.Address2 = PatientPharmacy.Address2;
                                Pharmacy.Address3 = PatientPharmacy.Address3;
                                Pharmacy.City = PatientPharmacy.City;
                                Pharmacy.State = PatientPharmacy.State;
                                Pharmacy.PostalCode = PatientPharmacy.PostalCode;
                                Pharmacy.CountryCode = PatientPharmacy.CountryCode;
                                Pharmacy.Phone = PatientPharmacy.Phone;
                                Pharmacy.Preferred = PatientPharmacy.Preferred;
                                Pharmacy.Note = PatientPharmacy.Note;
                                Pharmacy.UserId_Modified = UserId;
                                Pharmacy.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();

                        if (PatientPharmacy.Preferred)
                        {
                            bool finished = false;
                            try
                            {
                                do
                                {
                                    PatientPharmacy Pharmacy = db.PatientPharmacies.First(p => p.PatientId == PatientPharmacy.PatientId
                                                          && p.PharmacyCntr != PatientPharmacy.PharmacyCntr && p.Preferred == true);

                                    if (Pharmacy != null)
                                    {
                                        Pharmacy.Preferred = false;
                                        Pharmacy.UserId_Modified = UserId;
                                        Pharmacy.DateModified = System.DateTime.Now;
                                        db.SaveChanges();
                                    }
                                    else
                                        finished = true;
                                }
                                while (finished == false);
                            }
                            catch
                            {
                                // If no items are available to select, it will throw an error
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientPharmacy.Valid = false;
                    PatientPharmacy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPharmacy.Valid = false;
                PatientPharmacy.ErrorMessage = "Invalid Token";
            }
            return PatientPharmacy;
        }
        #endregion

        #region Delete PatientPharmacy Data
        //------------------------------------------------------------------------
        // Delete PatientPharmacy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientPharmacy Data")]
        public PatientPharmacyData DeletePatientPharmacyData(PatientPharmacyData PatientPharmacy, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientPharmacy Pharmacy = db.PatientPharmacies.FirstOrDefault(p => p.PatientId == PatientPharmacy.PatientId
                                                  && p.PharmacyCntr == PatientPharmacy.PharmacyCntr);

                        if (Pharmacy != null)
                        {
                            Pharmacy.Deleted = true;
                            Pharmacy.UserId_Deleted = UserId;
                            Pharmacy.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientPharmacy.Valid = false;
                    PatientPharmacy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPharmacy.Valid = false;
                PatientPharmacy.ErrorMessage = "Invalid Token";
            }
            return PatientPharmacy;
        }
        #endregion


        #region Get PatientClinicalDocument Data
        //------------------------------------------------------------------------
        // Get PatientClinicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientClinicalDocument Data")]
        public PatientClinicalDocumentData GetPatientClinicalDocumentData(PatientClinicalDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientClinicalDocumentData PatientDocument = new PatientClinicalDocumentData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    
                    using (var db = new AMREntities())
                    {
                        PatientClinicalDocument results = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.DocumentCntr == Parms.DocumentCntr);

                        if (results != null)
                        {
                            PatientDocument.PatientId = results.PatientId;
                            PatientDocument.VisitId = results.VisitId;
                            PatientDocument.DocumentCntr = results.DocumentCntr;
                            PatientDocument.FacilityId = Convert.ToInt64(results.FacilityId);
                            PatientDocument.DocumentDescription = results.DocumentDescription;
                            PatientDocument.DocumentId = results.DocumentId;
                            PatientDocument.DocumentFormat = results.DocumentFormat;
                            PatientDocument.StorageLocation = results.StorageLocation;
                            PatientDocument.Notes = results.Notes;
                            PatientDocument.Viewable = Convert.ToBoolean(results.Viewable);
                            PatientDocument.Valid = true;
                            PatientDocument.ErrorMessage = "";

                            string FileName = PatientDocument.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;

                            PatientDocument.DocumentImage = FileHelper.DiskToBytes(PatientDocument.StorageLocation + "\\" + FileName);

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, Parms.DocumentCntr, FacilityId, DocTypePatientClinicalDoc, UserId, ActionRead);


                        }
                        else
                        {
                            PatientDocument.Valid = false;
                            PatientDocument.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Get PatientClinicalDocument Data for App
        //------------------------------------------------------------------------
        // Get PatientClinicalDocument Data for App
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientClinicalDocument Data for App")]
        public PatientClinicalDocumentDataApp GetPatientClinicalDocumentDataApp(PatientClinicalDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientClinicalDocumentDataApp PatientDocument = new PatientClinicalDocumentDataApp();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {

                    using (var db = new AMREntities())
                    {
                        PatientClinicalDocument results = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.DocumentCntr == Parms.DocumentCntr);

                        if (results != null)
                        {
                            PatientDocument.PatientId = results.PatientId;
                            PatientDocument.VisitId = results.VisitId;
                            PatientDocument.DocumentCntr = results.DocumentCntr;
                            PatientDocument.FacilityId = Convert.ToInt64(results.FacilityId);
                            PatientDocument.DocumentDescription = results.DocumentDescription;
                            PatientDocument.DocumentId = results.DocumentId;
                            PatientDocument.DocumentFormat = results.DocumentFormat;
                            PatientDocument.StorageLocation = results.StorageLocation;
                            PatientDocument.Notes = results.Notes;
                            PatientDocument.Viewable = Convert.ToBoolean(results.Viewable);
                            PatientDocument.Valid = true;
                            PatientDocument.ErrorMessage = "";

                            string FileName = PatientDocument.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;

                            PatientDocument.DocumentImage = FileHelper.DiskToBytes(PatientDocument.StorageLocation + "\\" + FileName);  //Edit by Talha DiskToString

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, Parms.DocumentCntr, FacilityId, DocTypePatientClinicalDoc, UserId, ActionRead);


                        }
                        else
                        {
                            PatientDocument.Valid = false;
                            PatientDocument.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Save PatientClinicalDocument Data
        //------------------------------------------------------------------------
        // Save PatientClinicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientClinicalDocument Data")]
        public PatientClinicalDocumentData SavePatientClinicalDocumentData(PatientClinicalDocumentData PatientClinicalDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientClinicalDocument.Valid = true;
            PatientClinicalDocument.ErrorMessage = "";

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

                        if (PatientClinicalDocument.DocumentCntr == 0)
                        {
                            //// Add Items

                            // Get Attachment Folder Info & Update Counters
                            ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 1); // Type = 1 - Clinical Documents

                            // Update Attachment Counter
                            if (Config.CurrentFolderDocCntr > 1000)
                            {
                                Config.AttachmentFolderCntr++;
                                Config.CurrentFolderDocCntr = 0;
                            }
                            Config.CurrentFolderDocCntr++;
                            db.SaveChanges();

                            // Save Document Info To Database
                            var ClinicalDocument = new PatientClinicalDocument()
                            {
                                PatientId = PatientClinicalDocument.PatientId,
                                VisitId = PatientClinicalDocument.VisitId,
                                DocumentCntr = 0,
                                FacilityId = PatientClinicalDocument.FacilityId,
                                DocumentDescription = PatientClinicalDocument.DocumentDescription,
                                DocumentId = PatientClinicalDocument.DocumentId,
                                DocumentFormat = PatientClinicalDocument.DocumentFormat,
                                StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                Notes = PatientClinicalDocument.Notes,
                                Viewable = PatientClinicalDocument.Viewable,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientClinicalDocuments.Add(ClinicalDocument);
                            db.SaveChanges();

                            // Write the document to the hard disk
                            string FileName = ClinicalDocument.DocumentCntr.ToString() + "." + PatientClinicalDocument.DocumentFormat;
                            FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                            FileHelper.BytesToDisk(PatientClinicalDocument.DocumentImage, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                            
                        }
                        else
                        {
                            PatientClinicalDocument ClinicalDocument = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == PatientClinicalDocument.PatientId
                                                  && p.FacilityId == PatientClinicalDocument.FacilityId 
                                                  && p.VisitId == PatientClinicalDocument.VisitId
                                                  && p.DocumentCntr == PatientClinicalDocument.DocumentCntr);

                            if (ClinicalDocument != null)
                            {
                                ClinicalDocument.DocumentDescription = PatientClinicalDocument.DocumentDescription;
                                ClinicalDocument.Notes = PatientClinicalDocument.Notes;
                                ClinicalDocument.Viewable = PatientClinicalDocument.Viewable;
                                ClinicalDocument.UserId_Modified = UserId;
                                ClinicalDocument.DateModified = System.DateTime.Now;

                                db.SaveChanges();
                            }
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    PatientClinicalDocument.Valid = false;
                    PatientClinicalDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientClinicalDocument.Valid = false;
                PatientClinicalDocument.ErrorMessage = "Invalid Token";
            }
            return PatientClinicalDocument;
        }
        #endregion


        #region Delete PatientClinicalDocument Data
        //------------------------------------------------------------------------
        // Delete PatientClinicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientClinicalDocument Data")]
        public PatientClinicalDocumentData DeletePatientClinicalDocumentData(PatientClinicalDocumentData PatientClinicalDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientClinicalDocument ClinicalDocument = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == PatientClinicalDocument.PatientId
                                                && p.FacilityId == PatientClinicalDocument.FacilityId 
                                                && p.VisitId == PatientClinicalDocument.VisitId
                                                && p.DocumentCntr == PatientClinicalDocument.DocumentCntr);

                        if (ClinicalDocument != null)
                        {
                            ClinicalDocument.Deleted = true;
                            ClinicalDocument.UserId_Deleted = UserId;
                            ClinicalDocument.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientClinicalDocument.Valid = false;
                    PatientClinicalDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientClinicalDocument.Valid = false;
                PatientClinicalDocument.ErrorMessage = "Invalid Token";
            }
            return PatientClinicalDocument;
        }
        #endregion

        #region Get PatientClinicalDocument List
        //------------------------------------------------------------------------
        // Get PatientClinicalDocument List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientClinicalDocument List")]
        public PatientDocTableData GetPatientClinicalDocumentList(PatientClinicalDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientClinicalDocument = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientClinicalDocuments
                                      join f in db.Facilities on p.FacilityId equals f.FacilityId
                                      join v in db.PatientVisits on new { p.VisitId, p.PatientId, f.FacilityId } equals new { v.VisitId, v.PatientId, v.FacilityId }

                                      where p.PatientId == Parms.PatientId
                                      //&& p.FacilityId == 4 
                                      //&& p.VisitId == Parms.VisitId
                                      select new
                                      {
                                          p.DocumentCntr,
                                          p.DocumentDescription,
                                          p.DateCreated,
                                          f.FacilityName,
                                          f.FacilityId,
                                          v.VisitDate,
                                          p.VisitId,
                                          p.Viewable,
                                          p.DocumentFormat
                                      };

                        PatientClinicalDocument.dt = clsTableConverter.ToDataTable(results);
                        PatientClinicalDocument.dt.TableName = "PatientClinicalDocument";

                        PatientClinicalDocument.Valid = true;
                        PatientClinicalDocument.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientClinicalDoc, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientClinicalDocument.Valid = false;
                    PatientClinicalDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientClinicalDocument.Valid = false;
                PatientClinicalDocument.ErrorMessage = "Invalid Token";
            }
            return PatientClinicalDocument;
        }
        #endregion

        #region Share PatientDocument
        //------------------------------------------------------------------------
        // PatientClinicalDocument Share 
        //------------------------------------------------------------------------

        [WebMethod(Description = "PatientClinicalDocument Share")]
        public PatientDocumentResp PatientClinicalDocumentShare(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocumentResp Response = new PatientDocumentResp();
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
                        PatientClinicalDocument Document = db.PatientClinicalDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId
                                                  && p.DocumentCntr == Parms.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Viewable = Parms.Share;
                            Document.UserId_Modified = UserId;
                            Document.DateModified = System.DateTime.Now;

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


        #region Get PatientDocument Data
        //------------------------------------------------------------------------
        // Get PatientDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientDocument Data")]
        public PatientDocumentData GetPatientDocumentData(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocumentData PatientDocument = new PatientDocumentData();

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
                        PatientDocument results = db.PatientDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.DocumentCntr == Parms.DocumentCntr);

                        if (results != null)
                        {
                            PatientDocument.PatientId = results.PatientId;
                            PatientDocument.DocumentCntr = results.DocumentCntr;
                            PatientDocument.DocumentDescription = results.DocumentDescription;
                            PatientDocument.DocumentId = results.DocumentId;
                            PatientDocument.DocumentFormat = results.DocumentFormat;
                            PatientDocument.StorageLocation = results.StorageLocation;
                            PatientDocument.Notes = results.Notes;
                            PatientDocument.Viewable = Convert.ToBoolean(results.Viewable);
                            PatientDocument.Valid = true;
                            PatientDocument.ErrorMessage = "";

                            string FileName = PatientDocument.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;

                            PatientDocument.DocumentImage = FileHelper.DiskToBytes(PatientDocument.StorageLocation + "\\" + FileName);

                            WriteAuditRecord(Parms.PatientId, 0, Parms.DocumentCntr, FacilityId, DocTypePatientDocument, UserId, ActionRead);


                        }
                        else
                        {
                            PatientDocument.Valid = false;
                            PatientDocument.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Save PatientDocument Data
        //------------------------------------------------------------------------
        // Save PatientDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientDocument Data")]
        public PatientDocumentData SavePatientDocumentData(PatientDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocument.Valid = true;
            PatientDocument.ErrorMessage = "";

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

                        if (PatientDocument.DocumentCntr == 0)
                        {
                            //// Add Items

                            // Get Attachment Folder Info & Update Counters
                            ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 3);  // Type = 3 - Patient Documents

                            // Update Attachment Counter
                            if (Config.CurrentFolderDocCntr > 1000)
                            {
                                Config.AttachmentFolderCntr++;
                                Config.CurrentFolderDocCntr = 0;
                            }
                            Config.CurrentFolderDocCntr++;
                            db.SaveChanges();

                            // Save Document Info To Database
                            var Document = new PatientDocument()
                            {
                                PatientId = PatientDocument.PatientId,
                                DocumentCntr = 0,
                                DocumentDescription = PatientDocument.DocumentDescription,
                                DocumentId = PatientDocument.DocumentId,
                                DocumentFormat = PatientDocument.DocumentFormat,
                                StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                Notes = PatientDocument.Notes,
                                Viewable = PatientDocument.Viewable,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientDocuments.Add(Document);
                            db.SaveChanges();

                            // Write the document to the hard disk
                            string FileName = Document.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;
                            FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                            FileHelper.BytesToDisk(PatientDocument.DocumentImage, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                        }
                        else
                        {
                            PatientDocument Document = db.PatientDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                  && p.DocumentCntr == PatientDocument.DocumentCntr);

                            if (Document != null)
                            {
                                Document.DocumentDescription = PatientDocument.DocumentDescription;
                                Document.Notes = PatientDocument.Notes;
                                Document.Viewable = PatientDocument.Viewable;
                                Document.UserId_Modified = UserId;
                                Document.DateModified = System.DateTime.Now;

                                db.SaveChanges();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Delete PatientDocument Data
        //------------------------------------------------------------------------
        // Delete PatientDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientDocument Data")]
        public PatientDocumentData DeletePatientDocumentData(PatientDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientDocument Document = db.PatientDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                && p.DocumentCntr == PatientDocument.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Deleted = true;
                            Document.UserId_Deleted = UserId;
                            Document.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Get PatientDocument List
        //------------------------------------------------------------------------
        // Get PatientDocument List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientDocument List")]
        public PatientDocTableData GetPatientDocumentList(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientDocument = new PatientDocTableData();

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
                        //var results = db.PatientDocuments.Where(p => p.PatientId == Parms.PatientId);

                        var results = from p in db.PatientDocuments
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.DocumentCntr,
                                          p.DocumentDescription,
                                          p.DateCreated, //Edit by Talha Amin
                                          p.Notes,
                                          p.Viewable,
                                          p.DocumentFormat
                                      };


                        PatientDocument.dt = clsTableConverter.ToDataTable(results);
                        PatientDocument.dt.TableName = "PatientDocument";

                        PatientDocument.Valid = true;
                        PatientDocument.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientDocument, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Share PatientDocument
        //------------------------------------------------------------------------
        // PatientDocument Share 
        //------------------------------------------------------------------------

        [WebMethod(Description = "PatientDocument Share")]
        public PatientDocumentResp PatientDocumentShare(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocumentResp Response = new PatientDocumentResp();
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
                        PatientDocument Document = db.PatientDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId
                                                  && p.DocumentCntr == Parms.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Viewable = Parms.Share;
                            Document.UserId_Modified = UserId;
                            Document.DateModified = System.DateTime.Now;

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


        #region Get PatientMedicalDocument Data
        //------------------------------------------------------------------------
        // Get PatientMedicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientMedicalDocument Data")]
        public PatientMedicalDocumentData GetPatientMedicalDocumentData(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientMedicalDocumentData PatientDocument = new PatientMedicalDocumentData();

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
                        PatientMedicalDocument results = db.PatientMedicalDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.DocumentCntr == Parms.DocumentCntr);

                        if (results != null)
                        {
                            PatientDocument.PatientId = results.PatientId;
                            PatientDocument.DocumentCntr = results.DocumentCntr;
                            PatientDocument.DocumentDescription = results.DocumentDescription;
                            PatientDocument.FacilityName = results.FacilityName;
                            PatientDocument.DoctorName = results.DoctorName;
                            PatientDocument.DocumentId = results.DocumentId;
                            PatientDocument.DocumentFormat = results.DocumentFormat;
                            PatientDocument.StorageLocation = results.StorageLocation;
                            PatientDocument.Notes = results.Notes;
                            PatientDocument.Viewable = Convert.ToBoolean(results.Viewable);
                            PatientDocument.Valid = true;
                            PatientDocument.ErrorMessage = "";

                            string FileName = PatientDocument.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;

                            PatientDocument.DocumentImage = FileHelper.DiskToBytes(PatientDocument.StorageLocation + "\\" + FileName);

                            WriteAuditRecord(Parms.PatientId, 0, Parms.DocumentCntr, FacilityId, DocTypePatientMedicalDoc, UserId, ActionRead);


                        }
                        else
                        {
                            PatientDocument.Valid = false;
                            PatientDocument.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Save PatientMedicalDocument Data
        //------------------------------------------------------------------------
        // Save PatientMedicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientMedicalDocument Data")]
        public PatientMedicalDocumentData SavePatientMedicalDocumentData(PatientMedicalDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocument.Valid = true;
            PatientDocument.ErrorMessage = "";

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

                        if (PatientDocument.DocumentCntr == 0)
                        {
                            //// Add Items

                            // Get Attachment Folder Info & Update Counters
                            ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 5);  // Type = 5 - Patient Medical Documents

                            // Update Attachment Counter
                            if (Config.CurrentFolderDocCntr > 1000)
                            {
                                Config.AttachmentFolderCntr++;
                                Config.CurrentFolderDocCntr = 0;
                            }
                            Config.CurrentFolderDocCntr++;
                            db.SaveChanges();

                            // Save Document Info To Database
                            var Document = new PatientMedicalDocument()
                            {
                                PatientId = PatientDocument.PatientId,
                                DocumentCntr = 0,
                                DocumentDescription = PatientDocument.DocumentDescription,
                                FacilityName = PatientDocument.FacilityName,
                                DoctorName = PatientDocument.DoctorName,
                                DocumentId = PatientDocument.DocumentId,
                                DocumentFormat = PatientDocument.DocumentFormat,
                                StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                Notes = PatientDocument.Notes,
                                Viewable = PatientDocument.Viewable,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientMedicalDocuments.Add(Document);
                            db.SaveChanges();

                            // Write the document to the hard disk
                            string FileName = Document.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;
                            FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                            FileHelper.BytesToDisk(PatientDocument.DocumentImage, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                        }
                        else
                        {
                            PatientMedicalDocument Document = db.PatientMedicalDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                  && p.DocumentCntr == PatientDocument.DocumentCntr);

                            if (Document != null)
                            {
                                Document.DocumentDescription = PatientDocument.DocumentDescription;
                                Document.FacilityName = PatientDocument.FacilityName;
                                Document.DoctorName = PatientDocument.DoctorName;
                                Document.Notes = PatientDocument.Notes;
                                Document.Viewable = PatientDocument.Viewable;
                                Document.UserId_Modified = UserId;
                                Document.DateModified = System.DateTime.Now;

                                db.SaveChanges();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Delete PatientMedicalDocument Data
        //------------------------------------------------------------------------
        // Delete PatientMedicalDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientMedicalDocument Data")]
        public PatientMedicalDocumentData DeletePatientMedicalDocumentData(PatientMedicalDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientMedicalDocument Document = db.PatientMedicalDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                && p.DocumentCntr == PatientDocument.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Deleted = true;
                            Document.UserId_Deleted = UserId;
                            Document.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Get PatientMedicalDocument List
        //------------------------------------------------------------------------
        // Get PatientMedicalDocument List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientMedicalDocument List")]
        public PatientDocTableData GetPatientMedicalDocumentList(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientDocument = new PatientDocTableData();

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

                        var results = from p in db.PatientMedicalDocuments
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.DocumentCntr,
                                          p.DocumentDescription,
                                          p.FacilityName,
                                          p.DoctorName,
                                          p.DateCreated,
                                          //Edit by Talha Amin...
                                          p.Notes,
                                          p.Viewable,
                                          p.DocumentFormat //Edit by Talha Amin
                                      };


                        PatientDocument.dt = clsTableConverter.ToDataTable(results);
                        PatientDocument.dt.TableName = "PatientDocument";

                        PatientDocument.Valid = true;
                        PatientDocument.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientMedicalDoc, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Share PatientMedicalDocument
        //------------------------------------------------------------------------
        // PatientMedicalDocument Share 
        //------------------------------------------------------------------------

        [WebMethod(Description = "PatientMedicalDocument Share")]
        public PatientDocumentResp PatientMedicalDocumentShare(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocumentResp Response = new PatientDocumentResp();
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
                        PatientMedicalDocument Document = db.PatientMedicalDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId
                                                  && p.DocumentCntr == Parms.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Viewable = Parms.Share;
                            Document.UserId_Modified = UserId;
                            Document.DateModified = System.DateTime.Now;

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


        #region Get PatientCareDocument Data
        //------------------------------------------------------------------------
        // Get PatientCareDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientCareDocument Data")]
        public PatientCareDocumentData GetPatientCareDocumentData(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientCareDocumentData PatientDocument = new PatientCareDocumentData();

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
                        PatientCareDocument results = db.PatientCareDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.DocumentCntr == Parms.DocumentCntr);

                        if (results != null)
                        {
                            PatientDocument.PatientId = results.PatientId;
                            PatientDocument.DocumentCntr = results.DocumentCntr;
                            PatientDocument.DocumentDescription = results.DocumentDescription;
                            PatientDocument.DoctorName = results.DoctorName;
                            PatientDocument.DocumentId = results.DocumentId;
                            PatientDocument.DocumentFormat = results.DocumentFormat;
                            PatientDocument.StorageLocation = results.StorageLocation;
                            PatientDocument.Notes = results.Notes;
                            PatientDocument.Viewable = Convert.ToBoolean(results.Viewable);
                            PatientDocument.Valid = true;
                            PatientDocument.ErrorMessage = "";

                            string FileName = PatientDocument.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;

                            PatientDocument.DocumentImage = FileHelper.DiskToBytes(PatientDocument.StorageLocation + "\\" + FileName);

                            WriteAuditRecord(Parms.PatientId, 0, Parms.DocumentCntr, FacilityId, DocTypePatientCareDoc, UserId, ActionRead);


                        }
                        else
                        {
                            PatientDocument.Valid = false;
                            PatientDocument.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Save PatientCareDocument Data
        //------------------------------------------------------------------------
        // Save PatientCareDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientCareDocument Data")]
        public PatientCareDocumentData SavePatientCareDocumentData(PatientCareDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocument.Valid = true;
            PatientDocument.ErrorMessage = "";

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

                        if (PatientDocument.DocumentCntr == 0)
                        {
                            //// Add Items

                            // Get Attachment Folder Info & Update Counters
                            ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 6);  // Type = 6 - Patient Care Documents

                            // Update Attachment Counter
                            if (Config.CurrentFolderDocCntr > 1000)
                            {
                                Config.AttachmentFolderCntr++;
                                Config.CurrentFolderDocCntr = 0;
                            }
                            Config.CurrentFolderDocCntr++;
                            db.SaveChanges();

                            // Save Document Info To Database
                            var Document = new PatientCareDocument()
                            {
                                PatientId = PatientDocument.PatientId,
                                DocumentCntr = 0,
                                DocumentDescription = PatientDocument.DocumentDescription,
                                DoctorName = PatientDocument.DoctorName,
                                DocumentId = PatientDocument.DocumentId,
                                DocumentFormat = PatientDocument.DocumentFormat,
                                StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                Notes = PatientDocument.Notes,
                                Viewable = PatientDocument.Viewable,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientCareDocuments.Add(Document);
                            db.SaveChanges();

                            // Write the document to the hard disk
                            string FileName = Document.DocumentCntr.ToString() + "." + PatientDocument.DocumentFormat;
                            FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                            FileHelper.BytesToDisk(PatientDocument.DocumentImage, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);

                            SendMessageNotification(PatientDocument.PatientId);
                        }
                        else
                        {
                            PatientCareDocument Document = db.PatientCareDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                  && p.DocumentCntr == PatientDocument.DocumentCntr);

                            if (Document != null)
                            {
                                Document.DocumentDescription = PatientDocument.DocumentDescription;
                                Document.DoctorName = PatientDocument.DoctorName;
                                Document.Notes = PatientDocument.Notes;
                                Document.Viewable = PatientDocument.Viewable;
                                Document.UserId_Modified = UserId;
                                Document.DateModified = System.DateTime.Now;

                                db.SaveChanges();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region SendMessageNotification

        private void SendMessageNotification(Int64 PatientId)
        {
            // Send Email to patient notifying of new message
            try
            {
                // Get the patient's setting for notifications

                using (var db = new AMREntities())
                {
                    var results = from p in db.Patients
                                  join w in db.PatientWebSettings on p.PatientId equals w.PatientId
                                  join u in db.Users on PatientId equals u.UserRoleLink
                                  join c in db.Carriers on w.CellCarrier equals c.CarrierId into CarrierGroup
                                  from cg in CarrierGroup.DefaultIfEmpty()
                                  where p.PatientId == PatientId
                                  && u.UserRoleId == 5
                                  select new
                                  {
                                      p.PatientId,
                                      p.Title,
                                      p.FirstName,
                                      p.LastName,
                                      p.MobilePhone,
                                      u.UserEmail,
                                      w.EmailNotifyNewMessage,
                                      w.TextNotifyNewMesssage,
                                      w.CellCarrier,
                                      cg.CarrierURL,
                                  };

                    DataTable dt = clsTableConverter.ToDataTable(results);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToBoolean(dr["EmailNotifyNewMessage"]))
                        {
                            clsEmail objEmail = new clsEmail();
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;

                            string Msg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                            "   <tr style=\"background-color:#00a0e0;\"><td></td></tr>" +
                                            "	<tr style=\"background-color:#00a0e0;\">" +
                                            "       <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                            "			<tr id=\"backgroundTable\" width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                            "                <td width=\"571\" valign=\"top\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                            "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                                            "              </td>" +
                                            "              </tr>" +
                                            "          </table>" +
                                            "       </td>" +
                                            "    </tr>" +
                                            "	<tr>" +
                                            "       <td valign=\"top\">" +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "           <tr style=height:1px;><td></td></tr>" +
                                            "			<tr >" +
                                            "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"] + "</strong>,</h1>  <br />" +
                                            "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                            "               This is confirmation that your medical care provider has just uploaded records " +
                                            "               to your portal account. To view and manage your records, login to your" +
                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">account</a> and enter your AccessID Code and password." +
                                            "               </span>" +
                                            "               <br /><br />" +
                                            "               Click on the <a href=\"#\" style=\"color:#37b74a; font-size:18px;font-weight:700;text-decoration:none;\">Medical Portfolio</a> tab to view and manage the new records that have just been uploaded to your account. AMR recommends that you review each new file and set permission controls related to your view-only Emergency Summary Report. In emergency situations, this Emergency Summary Report is available to Doctors and Emergency Responders should an emergency situation arise. Emergency Responders know to look for a service like ours and emergency access is readily available to them and clearly identified on our login page. " +
                                            "               <br /><br />" +
                                            "               Your Member Services Team!<br /><br />" +
                                            "               Please note that your online account is subject to our <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                            "             </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "      </td>" +
                                            "   </tr>" +
                                            "	<tr style=\"background-color:#37b74a;\">" +
                                            "      <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "            <tr>" +
                                            "            <td></td>" +
                                            "            </tr>" +
                                            "			<tr id=\"backgroundTable\" width=\"571\" style=\"width:571px;display:block;background-color:#fff;margin-top:25px; font-size:14px;background-image: url(https://www.AMRPortal.com/LetterImages/footer-bkg.jpg);background-repeat: no-repeat;background-position: center top;height: 78px;width: 571px;\">" +
                                            "              <td width=\"532\" valign=\"top\" style=\"float:left;line-height:1em; background-color:#00a0e0; \">" +
                                            "              <div style=\"text-align:center; color:#f4f4f5;margin:20px auto 0;font-size:11px;\">" +
                                            "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"font-size:13px;text-decoration:none;font-weight:700;color:#f4f4f5;\">" + host + "</a><br />" +
                                            "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                            "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                            "              </div>" +
                                            "              </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "          <br /><br />" +
                                            "       </td>  " +
                                            "     </tr>" +
                                            "	<tr style=\"background-color:#37b74a;\"><td></td></tr>" +
                                            "</table>";

                            objEmail.SendEmailHTML(dr["UserEMail"].ToString(), "AMR Patient Portal - New Patient Information Available", Msg);

                        }
                        if (Convert.ToBoolean(dr["TextNotifyNewMesssage"]))
                        {
                            if (dr["MobilePhone"].ToString().Trim() != "" && dr["CarrierURL"].ToString() != null)
                            {
                                clsEmail objEmail = new clsEmail();

                                string Msg = "You have a message waiting ";
                                string EmailAddress = dr["MobilePhone"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "@" + dr["CarrierURL"].ToString();

                                objEmail.SendEmail(EmailAddress, "AMRPatientPortal", Msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Delete PatientCareDocument Data
        //------------------------------------------------------------------------
        // Delete PatientCareDocument Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientCareDocument Data")]
        public PatientCareDocumentData DeletePatientCareDocumentData(PatientCareDocumentData PatientDocument, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientCareDocument Document = db.PatientCareDocuments.FirstOrDefault(p => p.PatientId == PatientDocument.PatientId
                                                && p.DocumentCntr == PatientDocument.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Deleted = true;
                            Document.UserId_Deleted = UserId;
                            Document.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Get PatientCareDocument List
        //------------------------------------------------------------------------
        // Get PatientCareDocument List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientCareDocument List")]
        public PatientDocTableData GetPatientCareDocumentList(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientDocument = new PatientDocTableData();

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

                        var results = from p in db.PatientCareDocuments
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.DocumentCntr,
                                          p.DocumentDescription,
                                          p.DoctorName,
                                          p.DateCreated,
                                          p.Notes,     //Edit by Talha Amin...
                                          p.Viewable,
                                          p.DocumentFormat //Edit by Talha Amin
                                      };


                        PatientDocument.dt = clsTableConverter.ToDataTable(results);
                        PatientDocument.dt.TableName = "PatientDocument";

                        PatientDocument.Valid = true;
                        PatientDocument.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientCareDoc, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion

        #region Share PatientCareDocument
        //------------------------------------------------------------------------
        // PatientCareDocument Share 
        //------------------------------------------------------------------------

        [WebMethod(Description = "PatientCareDocument Share")]
        public PatientDocumentResp PatientCareDocumentShare(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDocumentResp Response = new PatientDocumentResp();
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
                        PatientCareDocument Document = db.PatientCareDocuments.FirstOrDefault(p => p.PatientId == Parms.PatientId
                                                  && p.DocumentCntr == Parms.DocumentCntr);

                        if (Document != null)
                        {
                            Document.Viewable = Parms.Share;
                            Document.UserId_Modified = UserId;
                            Document.DateModified = System.DateTime.Now;

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


        #region Get PatientPlanOfCare Data
        //------------------------------------------------------------------------
        // Get PatientPlanOfCare Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientPlanOfCare Data")]
        public PatientDocTableData GetPatientPlanOfCareData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientPlanOfCare = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientPlanOfCares
                                      join c in db.C_InstructionType on p.InstructionTypeId equals c.InstructionTypeId
                                      join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.InstructionTypeId != 4 && p.Deleted ==false
                                      select new
                                      {
                                          p.PlanCntr,
                                          p.InstructionTypeId,
                                          InstructionType = c.Value,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          CodeSystem = s.Value,
                                          p.Instruction,
                                          p.Goal,
                                          p.Note,
                                          p.AppointmentDateTime,
                                          p.ProviderId
                                      };



                        PatientPlanOfCare.dt = clsTableConverter.ToDataTable(results);
                        PatientPlanOfCare.dt.TableName = "PatientPlanOfCare";

                        PatientPlanOfCare.Valid = true;
                        PatientPlanOfCare.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientPlanOfCare, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientPlanOfCare.Valid = false;
                    PatientPlanOfCare.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPlanOfCare.Valid = false;
                PatientPlanOfCare.ErrorMessage = "Invalid Token";
            }
            return PatientPlanOfCare;
        }
        #endregion

        #region Save PatientPlanOfCare Data
        //------------------------------------------------------------------------
        // Save PatientPlanOfCare Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientPlanOfCare Data")]
        public PatientPlanOfCareData SavePatientPlanOfCareData(PatientPlanOfCareData PatientPlanOfCare, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientPlanOfCare.Valid = true;
            PatientPlanOfCare.ErrorMessage = "";

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

                        if (PatientPlanOfCare.PlanCntr == 0)
                        {
                            //// Add Items
                            var PlanOfCare = new PatientPlanOfCare()
                            {
                                PatientId = PatientPlanOfCare.PatientId,
                                VisitId = PatientPlanOfCare.VisitId,
                                PlanCntr = 0,
                                FacilityId = PatientPlanOfCare.FacilityId,
                                InstructionTypeId = PatientPlanOfCare.InstructionTypeId,
                                CodeValue = PatientPlanOfCare.CodeValue,
                                CodeSystemId = PatientPlanOfCare.CodeSystemId,
                                Instruction = PatientPlanOfCare.Instruction,
                                Goal = PatientPlanOfCare.Goal,
                                Note = PatientPlanOfCare.Note,
                                AppointmentDateTime = PatientPlanOfCare.AppointmentDateTime,
                                ProviderId = PatientPlanOfCare.ProviderId,
                                UserId_Created = UserId,
                                Deleted=false,
                                DateCreated = System.DateTime.Now,
                            };

                            db.PatientPlanOfCares.Add(PlanOfCare);
                        }
                        else
                        {
                            PatientPlanOfCare PlanOfCare = db.PatientPlanOfCares.FirstOrDefault(p => p.PatientId == PatientPlanOfCare.PatientId
                                                  && p.FacilityId == PatientPlanOfCare.FacilityId
                                                  && p.VisitId == PatientPlanOfCare.VisitId
                                                  && p.PlanCntr == PatientPlanOfCare.PlanCntr);

                            if (PlanOfCare != null)
                            {
                                PlanOfCare.InstructionTypeId = PatientPlanOfCare.InstructionTypeId;
                                PlanOfCare.CodeSystemId = PatientPlanOfCare.CodeSystemId;
                                PlanOfCare.Instruction = PatientPlanOfCare.Instruction;
                                PlanOfCare.Goal = PatientPlanOfCare.Goal;
                                PlanOfCare.Note = PatientPlanOfCare.Note;
                                PlanOfCare.AppointmentDateTime = PatientPlanOfCare.AppointmentDateTime;
                                PlanOfCare.ProviderId = PatientPlanOfCare.ProviderId;
                            }
                           
                            db.SaveChanges();
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientPlanOfCare.Valid = false;
                    PatientPlanOfCare.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPlanOfCare.Valid = false;
                PatientPlanOfCare.ErrorMessage = "Invalid Token";
            }
            return PatientPlanOfCare;
        }
        #endregion

        #region Delete PatientPlanOfCare Data
        //------------------------------------------------------------------------
        // Delete PatientPlanOfCare Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientPlanOfCare Data")]
        public PatientPlanOfCareData DeletePatientPlanOfCareData(PatientPlanOfCareData PatientPlanOfCare, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientPlanOfCare PlanOfCare = db.PatientPlanOfCares.First(p => p.PatientId == PatientPlanOfCare.PatientId
                                                && p.FacilityId == PatientPlanOfCare.FacilityId
                                                && p.VisitId == PatientPlanOfCare.VisitId
                                                && p.PlanCntr == PatientPlanOfCare.PlanCntr);

                        if (PlanOfCare != null)
                        {
                            PlanOfCare.Deleted = true;
                            PlanOfCare.UserId_Deleted = UserId;
                            PlanOfCare.DateDeleted = System.DateTime.Now;
                            PatientPlanOfCare.Valid = true;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientPlanOfCare.Valid = false;
                    PatientPlanOfCare.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPlanOfCare.Valid = false;
                PatientPlanOfCare.ErrorMessage = "Invalid Token";
            }
            return PatientPlanOfCare;
        }
        #endregion

        //Added By Ahmed
        #region Get PatientClinicalInstructions Data
        //------------------------------------------------------------------------
        // Get PatientClinicalInstructions Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientClinicalInstructions Data")]
        public PatientDocTableData GetPatientClinicalInstructionsData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientClinicalInstructions = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientPlanOfCares
                                      join c in db.C_InstructionType on p.InstructionTypeId equals c.InstructionTypeId
                                      join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                      where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.InstructionTypeId == 4 && p.Deleted ==false
                                      select new
                                      {
                                          p.PlanCntr,
                                          p.InstructionTypeId,
                                          InstructionType = c.Value,
                                          p.CodeValue,
                                          p.CodeSystemId,
                                          CodeSystem = s.Value,
                                          p.Instruction,
                                          p.Goal,
                                          p.Note,
                                          p.AppointmentDateTime,
                                          p.ProviderId
                                      };



                        PatientClinicalInstructions.dt = clsTableConverter.ToDataTable(results);
                        PatientClinicalInstructions.dt.TableName = "PatientPlanOfCare";

                        PatientClinicalInstructions.Valid = true;
                        PatientClinicalInstructions.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientPlanOfCare, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientClinicalInstructions.Valid = false;
                    PatientClinicalInstructions.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientClinicalInstructions.Valid = false;
                PatientClinicalInstructions.ErrorMessage = "Invalid Token";
            }
            return PatientClinicalInstructions;
        }
        #endregion


        #region Get PatientDoctor Data
        //------------------------------------------------------------------------
        // Get PatientDoctor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientDoctor Data")]
        public PatientDocTableData GetPatientDoctorData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientDoctor = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientDoctors
                                      join c in db.C_DoctorType on p.DoctorTypeId equals c.DoctorTypeId
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientDoctorId,
                                          p.DoctorTypeId,
                                          DoctorTypeDesc = c.Value,
                                          p.Name,
                                          p.Address1,
                                          p.Address2,
                                          p.Address3,
                                          p.City,
                                          p.State,
                                          p.PostalCode,
                                          p.CountryCode,
                                          p.WorkPhone,
                                          p.MobilePhone,
                                          p.Fax,
                                          p.Email,
                                          p.IsPrimary,
                                          p.OnCard
                                      };

                        PatientDoctor.dt = clsTableConverter.ToDataTable(results);
                        PatientDoctor.dt.TableName = "PatientDoctor";

                        PatientDoctor.Valid = true;
                        PatientDoctor.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientDoctor, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientDoctor.Valid = false;
                    PatientDoctor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDoctor.Valid = false;
                PatientDoctor.ErrorMessage = "Invalid Token";
            }
            return PatientDoctor;
        }
        #endregion

        #region Save PatientDoctor Data
        //------------------------------------------------------------------------
        // Save PatientDoctor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientDoctor Data")]
        public PatientDoctorData SavePatientDoctorData(PatientDoctorData PatientDoctor, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientDoctor.Valid = true;
            PatientDoctor.ErrorMessage = "";
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

                        if (PatientDoctor.PatientDoctorId == 0)
                        {
                            //// Add Items
                            var Doctor = new PatientDoctor()
                            {
                                PatientId = PatientDoctor.PatientId,
                                PatientDoctorId = 0,
                                DoctorTypeId = PatientDoctor.DoctorTypeId,
                                Name = PatientDoctor.Name,
                                Address1 = PatientDoctor.Address1,
                                Address2 = PatientDoctor.Address2,
                                Address3 = PatientDoctor.Address3,
                                City = PatientDoctor.City,
                                State = PatientDoctor.State,
                                PostalCode = PatientDoctor.PostalCode,
                                CountryCode = PatientDoctor.CountryCode,
                                WorkPhone = PatientDoctor.WorkPhone,
                                MobilePhone = PatientDoctor.MobilePhone,
                                Fax = PatientDoctor.Fax,
                                Email = PatientDoctor.Email,
                                IsPrimary = PatientDoctor.IsPrimary,
                                OnCard = PatientDoctor.OnCard,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientDoctors.Add(Doctor);
                        }
                        else
                        {
                            PatientDoctor Doctor = db.PatientDoctors.First(p => p.PatientId == PatientDoctor.PatientId
                                                  && p.PatientDoctorId == PatientDoctor.PatientDoctorId);

                            if (Doctor != null)
                            {
                                Doctor.DoctorTypeId = PatientDoctor.DoctorTypeId;
                                Doctor.Name = PatientDoctor.Name;
                                Doctor.Address1 = PatientDoctor.Address1;
                                Doctor.Address2 = PatientDoctor.Address2;
                                Doctor.Address3 = PatientDoctor.Address3;
                                Doctor.City = PatientDoctor.City;
                                Doctor.State = PatientDoctor.State;
                                Doctor.PostalCode = PatientDoctor.PostalCode;
                                Doctor.CountryCode = PatientDoctor.CountryCode;
                                Doctor.WorkPhone = PatientDoctor.WorkPhone;
                                Doctor.MobilePhone = PatientDoctor.MobilePhone;
                                Doctor.Fax = PatientDoctor.Fax;
                                Doctor.Email = PatientDoctor.Email;
                                Doctor.IsPrimary = PatientDoctor.IsPrimary;
                                Doctor.OnCard = PatientDoctor.OnCard;
                                Doctor.UserId_Modified = UserId;
                                Doctor.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientDoctor.Valid = false;
                    PatientDoctor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDoctor.Valid = false;
                PatientDoctor.ErrorMessage = "Invalid Token";
            }
            return PatientDoctor;
        }
        #endregion

        #region Delete PatientDoctor Data
        //------------------------------------------------------------------------
        // Delete PatientDoctor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientDoctor Data")]
        public PatientDoctorData DeletePatientDoctorData(PatientDoctorData PatientDoctor, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientDoctor Doctor = db.PatientDoctors.First(p => p.PatientId == PatientDoctor.PatientId
                                                  && p.PatientDoctorId == PatientDoctor.PatientDoctorId);

                        if (Doctor != null)
                        {
                            Doctor.Deleted = true;
                            Doctor.UserId_Deleted = UserId;
                            Doctor.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientDoctor.Valid = false;
                    PatientDoctor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDoctor.Valid = false;
                PatientDoctor.ErrorMessage = "Invalid Token";
            }
            return PatientDoctor;
        }
        #endregion


        #region Get PatientAdvisor Data
        //------------------------------------------------------------------------
        // Get PatientAdvisor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientAdvisor Data")]
        public PatientDocTableData GetPatientAdvisorData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientAdvisor = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        //Edit By Talha Amin...
                        var results = from p in db.PatientAdvisors
                                      join Atype in db.C_Advisor on p.AdvisorId equals Atype.AdvisorId into tempJoin
                                      from Atypesub in tempJoin.DefaultIfEmpty()
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientAdvisorId,
                                          p.AdvisorId,
                                          Value = (Atypesub == null)?string.Empty : Atypesub.Value,
                                          p.AdvisorDesc,
                                          p.Name,
                                          p.ContactAgent,
                                          p.Address1,
                                          p.Address2,
                                          p.Address3,
                                          p.City,
                                          p.State,
                                          p.PostalCode,
                                          p.CountryCode,
                                          p.WorkPhone,
                                          p.MobilePhone,
                                          p.Fax,
                                          p.EMail,
                                          p.Notes
                                      };

                        PatientAdvisor.dt = clsTableConverter.ToDataTable(results);
                        PatientAdvisor.dt.TableName = "PatientAdvisor";

                        PatientAdvisor.Valid = true;
                        PatientAdvisor.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientAdvisor, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientAdvisor.Valid = false;
                    PatientAdvisor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAdvisor.Valid = false;
                PatientAdvisor.ErrorMessage = "Invalid Token";
            }
            return PatientAdvisor;
        }
        #endregion

        #region Save PatientAdvisor Data
        //------------------------------------------------------------------------
        // Save PatientAdvisor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientAdvisor Data")]
        public PatientAdvisorData SavePatientAdvisorData(PatientAdvisorData PatientAdvisor, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientAdvisor.Valid = true;
            PatientAdvisor.ErrorMessage = "";
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

                        if (PatientAdvisor.PatientAdvisorId == 0)
                        {
                            //// Add Items
                            var Advisor = new PatientAdvisor()
                            {
                                PatientId = PatientAdvisor.PatientId,
                                PatientAdvisorId = 0,
                                AdvisorId = PatientAdvisor.AdvisorId,
                                AdvisorDesc = PatientAdvisor.AdvisorDesc,
                                Name = PatientAdvisor.Name,
                                ContactAgent = PatientAdvisor.ContactAgent,
                                Address1 = PatientAdvisor.Address1,
                                Address2 = PatientAdvisor.Address2,
                                Address3 = PatientAdvisor.Address3,
                                City = PatientAdvisor.City,
                                State = PatientAdvisor.State,
                                PostalCode = PatientAdvisor.PostalCode,
                                CountryCode = PatientAdvisor.CountryCode,
                                WorkPhone = PatientAdvisor.WorkPhone,
                                MobilePhone = PatientAdvisor.MobilePhone,
                                Fax = PatientAdvisor.Fax,
                                EMail = PatientAdvisor.EMail,
                                Notes = PatientAdvisor.Notes, //Edit by Talha Amin...
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientAdvisors.Add(Advisor);
                        }
                        else
                        {
                            PatientAdvisor Advisor = db.PatientAdvisors.First(p => p.PatientId == PatientAdvisor.PatientId
                                                  && p.PatientAdvisorId == PatientAdvisor.PatientAdvisorId);

                            if (Advisor != null)
                            {
                                Advisor.AdvisorId = PatientAdvisor.AdvisorId;
                                Advisor.AdvisorDesc = PatientAdvisor.AdvisorDesc;
                                Advisor.Name = PatientAdvisor.Name;
                                Advisor.ContactAgent = PatientAdvisor.ContactAgent;
                                Advisor.Address1 = PatientAdvisor.Address1;
                                Advisor.Address2 = PatientAdvisor.Address2;
                                Advisor.Address3 = PatientAdvisor.Address3;
                                Advisor.City = PatientAdvisor.City;
                                Advisor.State = PatientAdvisor.State;
                                Advisor.PostalCode = PatientAdvisor.PostalCode;
                                Advisor.CountryCode = PatientAdvisor.CountryCode;
                                Advisor.WorkPhone = PatientAdvisor.WorkPhone;
                                Advisor.MobilePhone = PatientAdvisor.MobilePhone;
                                Advisor.Fax = PatientAdvisor.Fax;
                                Advisor.EMail = PatientAdvisor.EMail;
                                Advisor.UserId_Modified = UserId;
                                Advisor.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientAdvisor.Valid = false;
                    PatientAdvisor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAdvisor.Valid = false;
                PatientAdvisor.ErrorMessage = "Invalid Token";
            }
            return PatientAdvisor;
        }
        #endregion

        #region Delete PatientAdvisor Data
        //------------------------------------------------------------------------
        // Delete PatientAdvisor Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientAdvisor Data")]
        public PatientAdvisorData DeletePatientAdvisorData(PatientAdvisorData PatientAdvisor, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientAdvisor Advisor = db.PatientAdvisors.First(p => p.PatientId == PatientAdvisor.PatientId
                                                  && p.PatientAdvisorId == PatientAdvisor.PatientAdvisorId);

                        if (Advisor != null)
                        {
                            Advisor.Deleted = true;
                            Advisor.UserId_Deleted = UserId;
                            Advisor.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientAdvisor.Valid = false;
                    PatientAdvisor.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientAdvisor.Valid = false;
                PatientAdvisor.ErrorMessage = "Invalid Token";
            }
            return PatientAdvisor;
        }
        #endregion

        #region Get Lab Result Test Data
        //Added By Talha Amin
        [WebMethod(Description = "Get Lab Result Test Data")]
        public PatientSummaryTableData GetLabResultTestData(PatientSummaryParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSummaryTableData PatientSummary = new PatientSummaryTableData();
             // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
            var results2 = from p in db.PatientLabResultTests
                           where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.LabResultCntr == Parms.LabResultCntr
                           select new
                           {
                               p.LabResultCntr,
                               p.TestCntr,
                               p.CodeValue,
                               p.CodeSystemId,
                               p.Component,
                               p.Result,
                               p.RefRange,
                               p.Units,
                               p.Abnormal,
                               p.ResultStatus
                           };

            PatientSummary.dtLabResult = clsTableConverter.ToDataTable(results2);  // Added by Talha Amin
            PatientSummary.dtLabResult.TableName = "PatientLabResults";

            PatientSummary.Valid = true;
            PatientSummary.ErrorMessage = "";

            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientLabResults, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientSummary.Valid = false;
                    PatientSummary.ErrorMessage = ex.Message;
                }
            }
            return PatientSummary;
        }

        #endregion

        #region Get Patient Summary Data
        //------------------------------------------------------------------------
        // Get Patient Summary Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Summary Data")]
        public PatientSummaryTableData GetPatientSummaryData(PatientSummaryParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientSummaryTableData PatientSummary = new PatientSummaryTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        #region Visits
                        if (Parms.Visit)
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
                                          };
                            PatientSummary.dtVisit = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtVisit.TableName = "Visits";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";
                        }

                        #endregion

                        #region  Allergies
                        if (Parms.Allergy)
                        {
                            var results = from p in db.PatientAllergies
                                          join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientAllergyCntr,
                                              p.CodeValue_Allergen,
                                              p.CodeSystemId_Allergen,
                                              p.Allergen,
                                              p.AllergenType,
                                              p.CodeValue_Reaction,
                                              p.CodeSystemId_Reaction,
                                              p.Reaction,
                                              p.EffectiveDate,
                                              p.ConditionStatusId,
                                              ConditionStatus = c.Value,
                                              p.Note,
                                              p.OnCard,
                                              p.OnKeys,
                                              p.DateModified
                                          };

                            PatientSummary.dtAllergy = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtAllergy.TableName = "PatientAllergy";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientAllergy, UserId, ActionRead);
                        }
                        #endregion

                        #region Medications
                        if (Parms.Medication)
                        {
                            if (Parms.Active == 1)  // Active
                            {
                                var results = from p in db.PatientMedications
                                              where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.Active == true
                                              select new
                                              {
                                                  p.PatientMedicationCntr,
                                                  p.CodeValue,
                                                  p.CodeSystemId,
                                                  p.MedicationName,
                                                  p.Active,
                                                  p.Quantity,
                                                  p.RouteId,
                                                  p.Dose,
                                                  p.DoseUnit,
                                                  p.Refills,
                                                  p.Frequency,
                                                  p.Sig,
                                                  p.Diagnosis,
                                                  p.StartDate,
                                                  p.ExpireDate,
                                                  p.Pharmacy,
                                                  p.Note,
                                                  p.Status,
                                                  p.DateModified,
                                                  p.DuringVisit
                                              };

                                PatientSummary.dtMedication = clsTableConverter.ToDataTable(results);
                            }
                            else if (Parms.Active == 0)  // Invactive
                            {
                                var results = from p in db.PatientMedications
                                              where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.Active == false
                                              select new
                                              {
                                                  p.PatientMedicationCntr,
                                                  p.CodeValue,
                                                  p.CodeSystemId,
                                                  p.MedicationName,
                                                  p.Active,
                                                  p.Quantity,
                                                  p.RouteId,
                                                  p.Dose,
                                                  p.DoseUnit,
                                                  p.Refills,
                                                  p.Frequency,
                                                  p.Sig,
                                                  p.Diagnosis,
                                                  p.StartDate,
                                                  p.ExpireDate,
                                                  p.Pharmacy,
                                                  p.Status,
                                                  p.Note,
                                                  p.DateModified,
                                                  p.DuringVisit
                                              };

                                PatientSummary.dtMedication = clsTableConverter.ToDataTable(results);
                            }
                            else  // All
                            {
                                var results = from p in db.PatientMedications
                                              where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                              select new
                                              {
                                                  p.PatientMedicationCntr,
                                                  p.CodeValue,
                                                  p.CodeSystemId,
                                                  p.MedicationName,
                                                  p.Active,
                                                  p.Quantity,
                                                  p.RouteId,
                                                  p.Dose,
                                                  p.DoseUnit,
                                                  p.Refills,
                                                  p.Frequency,
                                                  p.Sig,
                                                  p.Diagnosis,
                                                  p.StartDate,
                                                  p.ExpireDate,
                                                  p.Pharmacy,
                                                  p.Status,
                                                  p.Note,
                                                  p.DateModified,
                                                  p.DuringVisit
                                              };

                                PatientSummary.dtMedication = clsTableConverter.ToDataTable(results);
                            }


                            PatientSummary.dtMedication.TableName = "PatientMedication";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientMedication, UserId, ActionRead);
                        }
                        #endregion

                        #region Social History
                        if (Parms.SocialHist)
                        {
                            var results = from p in db.PatientSocialHists
                                          join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatSocialHistCntr,
                                              p.Description,
                                              p.Qualifier,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              CodeSystem = s.Value,
                                              p.BeginDate,
                                              p.EndDate,
                                              p.Note,
                                              p.DateModified

                                          };

                            PatientSummary.dtSocialHist = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtSocialHist.TableName = "PatientSocialHist";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientSocialHist, UserId, ActionRead);
                        }
                        #endregion

                        #region Family History
                        if (Parms.FamilyHist)
                        {
                            var results = from p in db.PatientFamilyHists
                                          join r in db.C_Relationship on p.RelationshipId equals r.RelationshipId
                                          join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatFamilyHistCntr,
                                              p.RelationshipId,
                                              SNOMEDCode = r.SNOMED,  //Added by Talha Amin
                                              FamilyMember = r.Value,
                                              p.Description,
                                              p.Qualifier,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.ConditionStatusId,
                                              ConditionStatus = c.Value,
                                              p.AgeAtOnset,
                                              p.Diseased,
                                              p.DiseasedAge,
                                              p.Note,
                                              p.DateModified
                                          };

                            PatientSummary.dtFamilyHist = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtFamilyHist.TableName = "PatientFamilyHist";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientFamilyHist, UserId, ActionRead);
                        }
                        #endregion

                        #region Medical History
                        if (Parms.MedicalHist)
                        {
                            var results = from p in db.PatientMedicalHists
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatMedicalHistCntr,
                                              p.Description,
                                              p.DateOfOccurance,
                                              p.Note,
                                              p.OnCard,
                                              p.OnKeys,
                                              p.DateModified
                                          };

                            PatientSummary.dtMedicalHist = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtMedicalHist.TableName = "PatientMedicalHist";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientMedicalHist, UserId, ActionRead);
                        }
                        #endregion

                        #region Problems
                        if (Parms.Problem)
                        {
                            var results = from p in db.PatientProblems
                                          join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientProblemCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.Condition,
                                              p.EffectiveDate,
                                              p.LastChangeDate,
                                              p.ConditionStatusId,
                                              ConditionStatus = c.Value,
                                              p.Note,
                                              p.DateModified
                                          };

                            PatientSummary.dtProblem = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtProblem.TableName = "PatientProblem";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientProblem, UserId, ActionRead);
                        }
                        #endregion

                        #region Procedures
                        if (Parms.Procedure)
                        {
                            var results = from p in db.PatientProcedures
                                          join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatProcedureCntr,
                                              p.Description,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              CodeSystem = s.Value,
                                              p.Diagnosis,
                                              p.PerformedBy,
                                              p.ServiceLocation,
                                              p.ServiceDate,
                                              p.Note,
                                              p.DateModified
                                          };

                            PatientSummary.dtProcedure = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtProcedure.TableName = "PatientProcedure";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientProcedure, UserId, ActionRead);
                        }
                        #endregion

                        #region Vital Signs
                        if (Parms.VitalSign)
                        {
                            var results = from p in db.PatientVitalSigns
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientVitalCntr,
                                              p.VitalDate,
                                              p.HeightIn,
                                              p.WeightLb,
                                              p.BloodPressurePosn,
                                              p.Systolic,
                                              p.Diastolic,
                                              p.Pulse,
                                              p.Respiration,
                                              p.Temperature,
                                              p.DateModified
                                          };

                            PatientSummary.dtVitalSign = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtVitalSign.TableName = "PatientVitalSign";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientVitalSign, UserId, ActionRead);
                        }
                        #endregion

                        #region Immunizations
                        if (Parms.Immunization)
                        {
                            var results = from p in db.PatientImmunizations
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientImmunizationCntr,
                                              p.ImmunizationDate,
                                              p.ImmunizationTime,
                                              p.Vaccine,
                                              p.Amount,
                                              p.Route,
                                              p.Site,
                                              p.Sequence,
                                              p.ExpirationDate,
                                              p.LotNumber,
                                              p.Manufacturer,
                                              p.Note,
                                              p.DateModified
                                          };

                            PatientSummary.dtImmunization = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtImmunization.TableName = "PatientImmunization";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientImmunization, UserId, ActionRead);
                        }
                        #endregion

                        #region Plan Of Care
                        if (Parms.PlanOfCare)
                        {
                            var results = from p in db.PatientPlanOfCares
                                          join c in db.C_InstructionType on p.InstructionTypeId equals c.InstructionTypeId
                                          join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.InstructionTypeId != 2 && p.InstructionTypeId != 4
                                          select new
                                          {
                                              p.PlanCntr,
                                              p.InstructionTypeId,
                                              InstructionType = c.Value,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              CodeSystem = s.Value,
                                              p.Instruction,
                                              p.Goal,
                                              p.Note,
                                              p.AppointmentDateTime,
                                              p.ProviderId
                                          };



                            PatientSummary.dtPlanOfCare = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtPlanOfCare.TableName = "PatientPlanOfCare";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientPlanOfCare, UserId, ActionRead);
                        }
                        #endregion

                        #region Lab Test & Results
                        if (Parms.Lab)
                        {
                            var results = from p in db.PatientLabResults
                                          join pv in db.Providers on p.ProviderId_Requested equals pv.ProviderId into tempJoin
                                          from ProviderSub in tempJoin.DefaultIfEmpty()
                                          join f in db.Facilities on p.FacilityId equals f.FacilityId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId
                                          select new
                                          {
                                              p.LabResultCntr,
                                              ProviderName = (ProviderSub == null) ? string.Empty : ProviderSub.FirstName + " " + ProviderSub.LastName,
                                              p.ProviderId_Requested,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.LabName,
                                              p.FacilityId,
                                              f.FacilityName,
                                              p.OrderDate,
                                              p.CollectionDate,
                                              p.Requisiton,
                                              p.Specimen,
                                              p.SpecimenSource,
                                              p.ReportDate,
                                              p.ReviewDate,
                                              p.Reviewer
                                          };

                            PatientSummary.dtLab = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtLab.TableName = "PatientLabResult";

                            var results2 = from p in db.PatientLabResultTests
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId
                                          select new
                                          {
                                              p.LabResultCntr,
                                              p.TestCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.Component,
                                              p.Result,
                                              p.RefRange,
                                              p.Units,
                                              p.Abnormal,
                                              p.ResultStatus
                                          };

                            PatientSummary.dtLabDetail = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtLabDetail.TableName = "PatientLabResultTest";

                            PatientSummary.dtLabResult = clsTableConverter.ToDataTable(results2);  // Added by Talha Amin
                            PatientSummary.dtLabResult.TableName = "PatientLabResults";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientLabResults, UserId, ActionRead);
                        }
                        #endregion

                        #region Insurance
                        if (Parms.Insurance)
                        {
                            var results = from p in db.PatientInsurances
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientInsuranceId,
                                              p.PlanName,
                                              p.MemberNumber,
                                              p.GroupNumber,
                                              p.Subscriber,
                                              p.Relationship,
                                              p.EffectiveDate
                                          };

                            PatientSummary.dtInsurance = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtInsurance.TableName = "PatientInsurance";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientInsurance, UserId, ActionRead);
                        }
                        #endregion

                        #region Clinical Docs
                        if (Parms.ClinicalDocs)
                        {
                            var results = from p in db.PatientClinicalDocuments
                                          where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                          select new
                                          {
                                              p.DocumentCntr,
                                              p.DocumentDescription,
                                              p.Notes,//Added by talha
                                              p.DocumentFormat,//Added by talha
                                              p.DateCreated
                                          };

                            PatientSummary.dtClinicalDocs = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtClinicalDocs.TableName = "PatientClinicalDocs";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientClinicalDoc, UserId, ActionRead);
                        }
                        #endregion

                        #region Providers
                        if (Parms.Provider)
                        {
                            var results = from l in db.PatientProviderLinks
                                          join f in db.ProviderFacilityLinks on l.ProviderId equals f.ProviderId
                                          join p in db.Providers on l.ProviderId equals p.ProviderId
                                          where l.PatientId == Parms.PatientId && f.FacilityId == Parms.FacilityId
                                          select new
                                          {
                                              p.ProviderId,
                                              Name = p.FirstName + " " + p.LastName

                                          };

                            PatientSummary.dtProvider = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtProvider.TableName = "Providers";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";
                        }
                        #endregion

                    }
                }
                catch (Exception ex)
                {
                    PatientSummary.Valid = false;
                    PatientSummary.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSummary.Valid = false;
                PatientSummary.ErrorMessage = "Invalid Token";
            }
            return PatientSummary;
        }
        #endregion

        #region Get Patient CCD
        //------------------------------------------------------------------------
        // Get Patient CCD
        // Action 3 - Read, 5 -Emai, 6 - Download
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient CCD")]
        public PatientCCDResult GetPatientCCD(CCDParms Parms, string Token, Int64 UserId, Int64 FacilityId, short Action)
        {
            PatientCCDResult Response = new PatientCCDResult();

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

                    clsCCDGenerate objCCDGenerate = new clsCCDGenerate();
                    clsCCDGenerate.CCDParms Parms2 = new clsCCDGenerate.CCDParms();
                    Parms2.Custom = Parms.Custom;
                    Parms2.Demographics = Parms.Demographics;
                    Parms2.Provider = Parms.Provider;
                    Parms2.Problems = Parms.Problems;
                    Parms2.Allergies = Parms.Allergies;
                    Parms2.Immunizations = Parms.Immunizations;
                    Parms2.Medications = Parms.Medications;
                    Parms2.MedsAdministered = Parms.MedsAdministered;
                    Parms2.Labs = Parms.Labs;
                    Parms2.PlanOfCare = Parms.PlanOfCare;
                    //Parms2.ClinicalInstructions = Parms.ClinicalInstructions;
                    //Parms2.FutureAppointments = Parms.FutureAppointments;
                    //Parms2.Referrals = Parms.Referrals;
                    //Parms2.ScheduledTests = Parms.ScheduledTests;
                    //Parms2.DecisionAids = Parms.DecisionAids;
                    Parms2.Social = Parms.Social;
                    Parms2.VitalSigns = Parms.VitalSigns;
                    Parms2.VisitReason = Parms.VisitReason;
                    Parms2.Procedures = Parms.Procedures;
                    Parms2.Selection = Parms.Selection;
                    string CCD = "";

                    Response.Valid = objCCDGenerate.createPatientCCD(Parms.PatientId, Parms.FacilityId, Parms.VisitId, Parms2, ref CCD);
                    if (Response.Valid)
                    {
                        Response.CCD = CCD;
                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypeCCD, UserId, Action);
                    }
                    else
                        Response.ErrorMessage = "Error generating CCD";
                }
                catch
                {
                    Response.Valid = false;
                    Response.ErrorMessage = "Error generating CCD";
                }
            }
            return Response;
        }
        #endregion

        #region Save Patient CCD
        //------------------------------------------------------------------------
        // Save Patient CCD
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Patient CCD")]
        public CCDPostResponse SavePatientCCD(CCDPostData CCDData, string Token, Int64 UserId, Int64 FacilityId)
        {
            CCDPostResponse CCDResponse = new CCDPostResponse();
            CCDResponse.Valid = true;
            Int64 VisitId = CCDData.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    Int64 AMRPatientId = CCDData.AMRPatientId;

                    XmlDocument xd = new XmlDocument();

                    string xml = Encoding.UTF8.GetString(CCDData.Document);
                    xd.LoadXml(xml);

                    string ErrorMsg = "";
                    clsCCDImport objCCDImport = new clsCCDImport();

                    bool Valid = objCCDImport.Parse(FacilityId, UserId, AMRPatientId, xd, ref ErrorMsg);


                }
                catch (Exception ex)
                {
                    CCDResponse.Valid = false;
                    CCDResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CCDResponse.Valid = false;
                CCDResponse.ErrorMessage = "Invalid Token";
            }
            return CCDResponse;
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

        #region Get CCD Audit Log
        //------------------------------------------------------------------------
        // Get CCD Audit Log
        //------------------------------------------------------------------------
        [WebMethod(Description = "Get CCD Audit Log")]
        public PatientDocTableData GetCCDAuditLog(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData CCDAudit = new PatientDocTableData();

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
                        var results = from v in db.vwCCDAuditLogs
                                      where v.PatientId == Parms.PatientId
                                      orderby v.TDStamp descending
                                      select new
                                      {
                                          v.AuditId,
                                          v.PatientId,
                                          v.Method,
                                          v.UserId,
                                          v.Name,
                                          v.TDStamp
                                      };

                        CCDAudit.dt = clsTableConverter.ToDataTable(results);
                        CCDAudit.dt.TableName = "CCDAuditLogs";

                        CCDAudit.Valid = true;
                        CCDAudit.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    CCDAudit.Valid = false;
                    CCDAudit.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CCDAudit.Valid = false;
                CCDAudit.ErrorMessage = "Invalid Token";
            }
            return CCDAudit;
        }
        #endregion


        #region Get Medical Portfolio
        //------------------------------------------------------------------------
        // Get Medical Portfolio 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Medical Portfolio")]
        public MedicalPortfolioData GetMedicalPortfolio(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            MedicalPortfolioData Response = new MedicalPortfolioData();

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
                        // Get Visit Information

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
                            Response.dtVisit = clsTableConverter.ToDataTable(results);
                        }
                        else  //  Only Visit Set To Viewable
                        {
                            var results = from p in db.vwPatientVisitSummaries
                                          where p.PatientId == Parms.PatientId && p.Viewable == true
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
                            Response.dtVisit = clsTableConverter.ToDataTable(results);
                        }
                        Response.dtVisit.TableName = "PatientVisits";

                        // Get Outside Provider Documents

                        if (Parms.Option == 0)   // All Documents
                        {
                            var results = from p in db.PatientCareDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientId,
                                              p.DocumentCntr,
                                              p.DateCreated,
                                              p.DoctorName,
                                              p.DocumentDescription,
                                              p.Notes,
                                              p.Viewable,
                                              p.DocumentFormat //Edit by Talha Amin
                                          };
                            Response.dtOutsideDoctor = clsTableConverter.ToDataTable(results);
                        }
                        else  //  Only Documents Set To Viewable
                        {
                            var results = from p in db.PatientCareDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false && p.Viewable == true
                                          select new
                                          {
                                              p.PatientId,
                                              p.DocumentCntr,
                                              p.DateCreated,
                                              p.DoctorName,
                                              p.DocumentDescription,
                                              p.Notes,
                                              p.Viewable,
                                              p.DocumentFormat //Edit by Talha Amin
                                          };
                            Response.dtOutsideDoctor = clsTableConverter.ToDataTable(results);
                        }
                        Response.dtOutsideDoctor.TableName = "OutsideDoctor";

                        // Get Patient Uploaded Medical Documents

                        if (Parms.Option == 0)   // All Documents
                        {
                            var results = from p in db.PatientMedicalDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientId,
                                              p.DocumentCntr,
                                              p.DateCreated,
                                              p.DoctorName,
                                              p.FacilityName,
                                              p.DocumentDescription,
                                              p.Notes,
                                              p.Viewable,
                                              p.DocumentFormat //Edit by Talha Amin
                                          };
                            Response.dtPatiendDocs = clsTableConverter.ToDataTable(results);
                        }
                        else  //  Only Documents Set To Viewable
                        {
                            var results = from p in db.PatientMedicalDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false && p.Viewable == true
                                          select new
                                          {
                                              p.PatientId,
                                              p.DocumentCntr,
                                              p.DateCreated,
                                              p.DoctorName,
                                              p.FacilityName,
                                              p.DocumentDescription,
                                              p.Notes,
                                              p.Viewable,
                                              p.DocumentFormat //Edit by Talha Amin
                                          };
                            Response.dtPatiendDocs = clsTableConverter.ToDataTable(results);
                        }
                        Response.dtPatiendDocs.TableName = "PatientDoc";


                        Response.Valid = true;
                        Response.ErrorMessage = "";
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

        #region Get PatientPolicy Data
        //------------------------------------------------------------------------
        // Get PatientPolicy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientPolicy Data")]
        public PatientDocTableData GetPatientPolicyData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientPolicy = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        //Edit by Talha Amin...(Left join for PolicyType Value...)
                        var results = from p in db.PatientPolicies 
                                      join Ptype in db.C_PolicyType on p.PolicyTypeId equals Ptype.PolicyTypeId into tempJoin
                                      from Ptypesub in tempJoin.DefaultIfEmpty()
                                      where p.PatientId == Parms.PatientId && p.Deleted == false
                                      select new
                                      {
                                          p.PatientPolicyId,
                                          p.PolicyTypeId,
                                          Value = (Ptypesub == null) ? string.Empty : Ptypesub.Value,
                                          p.PolicyTypeName,
                                          p.Company,
                                          p.PolicyNo,
                                          p.GroupNumber,
                                          p.PlanNumber,
                                          p.Agent,
                                          p.AgentPhone,
                                          p.AgentFax,
                                          p.Notes
                                      };

                        PatientPolicy.dt = clsTableConverter.ToDataTable(results);
                        PatientPolicy.dt.TableName = "PatientPolicy";

                        PatientPolicy.Valid = true;
                        PatientPolicy.ErrorMessage = "";

                        WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypePatientPolicy, UserId, ActionRead);
                    }
                }
                catch (Exception ex)
                {
                    PatientPolicy.Valid = false;
                    PatientPolicy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPolicy.Valid = false;
                PatientPolicy.ErrorMessage = "Invalid Token";
            }
            return PatientPolicy;
        }
        #endregion

        #region Save PatientPolicy Data
        //------------------------------------------------------------------------
        // Save PatientPolicy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientPolicy Data")]
        public PatientPolicyData SavePatientPolicyData(PatientPolicyData PatientPolicy, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientPolicy.Valid = true;
            PatientPolicy.ErrorMessage = "";
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

                        if (PatientPolicy.PatientPolicyId == 0)
                        {
                            //// Add Items
                            var Doctor = new PatientPolicy()
                            {
                                PatientId = PatientPolicy.PatientId,
                                PatientPolicyId = 0,
                                PolicyTypeId = PatientPolicy.PolicyTypeId,
                                PolicyTypeName = PatientPolicy.PolicyTypeName,
                                Company = PatientPolicy.Company,
                                PolicyNo = PatientPolicy.PolicyNo,
                                GroupNumber = PatientPolicy.GroupNumber,
                                PlanNumber = PatientPolicy.PlanNumber,
                                Agent = PatientPolicy.Agent,
                                AgentPhone = PatientPolicy.AgentPhone,
                                AgentFax = PatientPolicy.AgentFax,//Edit by Talha Amin...
                                Notes    = PatientPolicy.Notes,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                                Deleted = false,
                            };
                            db.PatientPolicies.Add(Doctor);
                        }
                        else
                        {
                            PatientPolicy Policy = db.PatientPolicies.First(p => p.PatientId == PatientPolicy.PatientId
                                                  && p.PatientPolicyId == PatientPolicy.PatientPolicyId);

                            if (Policy != null)
                            {
                                Policy.PolicyTypeId = PatientPolicy.PolicyTypeId;
                                Policy.PolicyTypeName = PatientPolicy.PolicyTypeName;
                                Policy.Company = PatientPolicy.Company;
                                Policy.PolicyNo = PatientPolicy.PolicyNo;
                                Policy.GroupNumber = PatientPolicy.GroupNumber;
                                Policy.PlanNumber = PatientPolicy.PlanNumber;
                                Policy.Agent = PatientPolicy.Agent;
                                Policy.AgentPhone = PatientPolicy.AgentPhone;
                                Policy.AgentFax = PatientPolicy.AgentFax; 
                                Policy.Notes = PatientPolicy.Notes;//Edit By Talha Amin...
                                Policy.UserId_Modified = UserId;
                                Policy.DateModified = System.DateTime.Now;
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientPolicy.Valid = false;
                    PatientPolicy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPolicy.Valid = false;
                PatientPolicy.ErrorMessage = "Invalid Token";
            }
            return PatientPolicy;
        }
        #endregion

        #region Delete PatientPolicy Data
        //------------------------------------------------------------------------
        // Delete PatientPolicy Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete PatientPolicy Data")]
        public PatientPolicyData DeletePatientPolicyData(PatientPolicyData PatientPolicy, string Token, Int64 UserId, Int64 FacilityId)
        {
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

                        PatientPolicy Doctor = db.PatientPolicies.First(p => p.PatientId == PatientPolicy.PatientId
                                                  && p.PatientPolicyId == PatientPolicy.PatientPolicyId);

                        if (Doctor != null)
                        {
                            Doctor.Deleted = true;
                            Doctor.UserId_Deleted = UserId;
                            Doctor.DateDeleted = System.DateTime.Now;
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientPolicy.Valid = false;
                    PatientPolicy.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientPolicy.Valid = false;
                PatientPolicy.ErrorMessage = "Invalid Token";
            }
            return PatientPolicy;
        }
        #endregion

        #region Get Medical Summary Data
        //------------------------------------------------------------------------
        // Get Medical Summary Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Medical Summary Data")]
        public PatientMedicalTableData GetMedicalSummaryData(PatientMedicalParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientMedicalTableData PatientSummary = new PatientMedicalTableData();

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
                        #region Emergency
                        if (Parms.Emergency)
                        {

                            var results = from p in db.PatientEmergencies
                                          join r in db.C_Relationship on p.RelationshipId equals r.RelationshipId
                                          join c in db.C_Country on p.CountryCode equals c.CountryId
                                          where p.PatientId == Parms.PatientId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientEmergencyId,
                                              p.Name,
                                              p.Address1,
                                              p.Address2,
                                              p.Address3,
                                              p.City,
                                              p.State,
                                              p.PostalCode,
                                              p.CountryCode,
                                              Country= c.Name,
                                              p.HomePhone,
                                              p.MobilePhone,
                                              p.WorkPhone,
                                              p.Email,
                                              p.IsPrimary,
                                              p.RelationshipId,
                                              Relationship = r.Value,
                                              p.OnCard
                                          };

                            PatientSummary.dtEmergency = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtEmergency.TableName = "PatientEmergency";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientEmergency, UserId, ActionRead);
                        }
                        #endregion

                        #region Visits
                        if (Parms.Visit)
                        {
                            var results = from p in db.vwPatientVisitSummaries
                                          where p.PatientId == Parms.PatientId && p.Viewable == true
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
                                          };
                            PatientSummary.dtVisit = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtVisit.TableName = "Visits";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";
                        }

                        #endregion

                        #region DoctorUploaded
                        if (Parms.DoctorUploaded)
                        {
                            var results = from p in db.PatientCareDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false && p.Viewable == true
                                          select new
                                          {
                                              p.DocumentCntr,
                                              p.DocumentDescription,
                                              p.DoctorName,
                                              p.Notes,
                                              p.DateCreated,
                                              p.DocumentFormat //Added By Talha
                                          };


                            PatientSummary.dtDoctorUploaded = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtDoctorUploaded.TableName = "DoctorDocument";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientCareDoc, UserId, ActionRead);
                        }
                        #endregion

                        #region PatientUploaded
                        if (Parms.PatientUploaded)
                        {
                            var results = from p in db.PatientMedicalDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false && p.Viewable == true
                                          select new
                                          {
                                              p.DocumentCntr,
                                              p.DocumentDescription,
                                              p.FacilityName,
                                              p.DoctorName,
                                              p.Notes,
                                              p.DateCreated,
                                              p.DocumentFormat //Added By Talha

                                          };


                            PatientSummary.dtPatientUploaded = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtPatientUploaded.TableName = "PatientDocument";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientMedicalDoc, UserId, ActionRead);
                        }
                        #endregion

                        #region  Allergies
                        if (Parms.Allergy)
                        {
                            var results = from p in db.PatientAllergies
                                          join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == 0 && p.Deleted == false
                                          select new
                                          {
                                              p.PatientAllergyCntr,
                                              p.CodeValue_Allergen,
                                              p.CodeSystemId_Allergen,
                                              p.Allergen,
                                              p.AllergenType,
                                              p.CodeValue_Reaction,
                                              p.CodeSystemId_Reaction,
                                              p.Reaction,
                                              p.EffectiveDate,
                                              p.ConditionStatusId,
                                              ConditionStatus = c.Value,
                                              p.Note,
                                              p.OnCard,
                                              p.OnKeys,
                                              p.DateModified
                                          };

                            PatientSummary.dtAllergy = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtAllergy.TableName = "PatientAllergy";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientAllergy, UserId, ActionRead);
                        }
                        #endregion

                        #region Medications
                        if (Parms.Medication)
                        {
                            var results = from p in db.PatientMedications
                                            where p.PatientId == Parms.PatientId && p.FacilityId == 0 && p.Deleted == false
                                            select new
                                            {
                                                p.PatientMedicationCntr,
                                                p.CodeValue,
                                                p.CodeSystemId,
                                                p.MedicationName,
                                                p.Active,
                                                p.Quantity,
                                                p.RouteId,
                                                p.Dose,
                                                p.DoseUnit,
                                                p.Refills,
                                                p.Frequency,
                                                p.Sig,
                                                p.Diagnosis,
                                                p.StartDate,
                                                p.ExpireDate,
                                                p.Pharmacy,
                                                p.Status,
                                                p.Note,
                                                p.DateModified,
                                                p.DuringVisit
                                            };

                            PatientSummary.dtMedication = clsTableConverter.ToDataTable(results);

                            PatientSummary.dtMedication.TableName = "PatientMedication";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientMedication, UserId, ActionRead);
                        }
                        #endregion

                        #region Problems
                        if (Parms.Problem)
                        {
                            var results = from p in db.PatientProblems
                                          join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                          where p.PatientId == Parms.PatientId && p.FacilityId == 0 && p.Deleted == false
                                          select new
                                          {
                                              p.PatientProblemCntr,
                                              p.CodeValue,
                                              p.CodeSystemId,
                                              p.Condition,
                                              p.EffectiveDate,
                                              p.LastChangeDate,
                                              p.ConditionStatusId,
                                              ConditionStatus = c.Value,
                                              p.Note,
                                              p.DateModified
                                          };

                            PatientSummary.dtProblem = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtProblem.TableName = "PatientProblem";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientProblem, UserId, ActionRead);
                        }
                        #endregion

                        #region Policy
                        if (Parms.Policy)
                        {
                            var results = from p in db.PatientPolicies
                                          where p.PatientId == Parms.PatientId && p.Deleted == false
                                          select new
                                          {
                                              p.PatientPolicyId,
                                              p.PolicyTypeId,
                                              p.PolicyTypeName,
                                              p.Company,
                                              p.PolicyNo,
                                              p.GroupNumber,
                                              p.PlanNumber,
                                              p.Agent,
                                              p.AgentPhone,
                                              p.AgentFax,
                                              p.Notes
                                          };

                            PatientSummary.dtPolicy = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtPolicy.TableName = "PatientPolicy";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientPolicy, UserId, ActionRead);
                        }
                        #endregion

                        #region Document
                        if (Parms.Document)
                        {
                            var results = from p in db.PatientDocuments
                                          where p.PatientId == Parms.PatientId && p.Deleted == false && p.Viewable == true
                                          select new
                                          {
                                              p.DocumentCntr,
                                              p.DocumentDescription,
                                              p.DateCreated, //Edit by Talha Amin
                                              p.Notes,
                                              p.Viewable,
                                              p.DocumentFormat //Added By Talha
                                          };


                            PatientSummary.dtDocument = clsTableConverter.ToDataTable(results);
                            PatientSummary.dtDocument.TableName = "PatientDocument";

                            PatientSummary.Valid = true;
                            PatientSummary.ErrorMessage = "";

                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientDocument, UserId, ActionRead);
                        }
                        #endregion

                    }
                }
                catch (Exception ex)
                {
                    PatientSummary.Valid = false;
                    PatientSummary.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSummary.Valid = false;
                PatientSummary.ErrorMessage = "Invalid Token";
            }
            return PatientSummary;
        }
        #endregion
    
        #region Get Patient Visit CCD Data
        //------------------------------------------------------------------------
        // Get Patient  Visit CCD Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient  Visit CCD Data")]
        public PatientVisitCCD GetPatientVisitCCDData(PatientSummaryParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientVisitCCD PatientCCD = new PatientVisitCCD();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                using (var db = new AMREntities())
                {
                    vwVisitCCD results = db.vwVisitCCDs.FirstOrDefault(p => p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId);

                    if (results != null)
                    {
                        PatientCCD.PatientId = Convert.ToInt64(results.PatientId);
                        PatientCCD.Valid = true;
                        PatientCCD.ErrorMessage = "";
                        PatientCCD.PatientId = Convert.ToInt64(results.PatientId);
                        PatientCCD.FacilityId = Convert.ToInt64(results.FacilityId);
                        PatientCCD.VisitId = Convert.ToInt64(results.VisitId);
                        PatientCCD.ProviderId = Convert.ToInt64(results.ProviderId);
                        PatientCCD.Title = results.Title;
                        PatientCCD.FirstName = results.FirstName;
                        PatientCCD.LastName = results.LastName;
                        PatientCCD.License = results.License;
                        PatientCCD.Phone = results.Phone;
                        PatientCCD.Email = results.Email;
                        PatientCCD.FacilityTypeId = Convert.ToInt32(results.FacilityTypeId);
                        PatientCCD.FacilityType = results.FacilityType;
                        PatientCCD.FacilityName = results.FacilityName;
                        PatientCCD.Address1 = results.Address1;
                        PatientCCD.Address2 = results.Address2;
                        PatientCCD.City = results.City;
                        PatientCCD.State = results.State;
                        PatientCCD.CountryCode = results.CountryCode;
                        PatientCCD.CountryName = results.CountryName;
                        PatientCCD.PostalCode = results.PostalCode;
                        PatientCCD.FacilityPhone = results.FacilityPhone;
                        PatientCCD.FacilityFax = results.FacilityFax;
                        PatientCCD.VisitReason = results.VisitReason;
                        PatientCCD.VisitDate = Convert.ToDateTime(results.VisitDate);
                    }
                }
            }
            else
            {
                // Invalid Token
                PatientCCD.Valid = false;
                PatientCCD.ErrorMessage = "Invalid Token";
            }
            return PatientCCD;
        }
        #endregion


        #region Get Patient CCD Customize Data
        //------------------------------------------------------------------------
        // Get Patient CCD Customize Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient CCD Customize Data")]
        public PatientCCDTableData GetPatientCCDCustomizeData(PatientCCDParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientCCDTableData PatientSummary = new PatientCCDTableData();
            PatientSummary.Valid = true;
            PatientSummary.ErrorMessage = "";

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
                        #region Medications Administered

                        var results0 = from p in db.PatientMedications
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.DuringVisit == true
                                       select new
                                       {
                                           p.PatientMedicationCntr,
                                           p.CodeValue,
                                           p.CodeSystemId,
                                           p.MedicationName,
                                           p.Active,
                                           p.Quantity,
                                           p.RouteId,
                                           p.Dose,
                                           p.DoseUnit,
                                           p.Refills,
                                           p.Frequency,
                                           p.Sig,
                                           p.Diagnosis,
                                           p.StartDate,
                                           p.ExpireDate,
                                           p.Pharmacy,
                                           p.Status,
                                           p.Note,
                                           p.DateModified,
                                           p.DuringVisit
                                       };

                        PatientSummary.dtMedsAdministered = clsTableConverter.ToDataTable(results0);
                        PatientSummary.dtMedsAdministered.TableName = "PatientMedsAdministered";

                        #endregion

                        #region Medications

                        var results1 = from p in db.PatientMedications
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false && p.DuringVisit == false
                                        select new
                                        {
                                            p.PatientMedicationCntr,
                                            p.CodeValue,
                                            p.CodeSystemId,
                                            p.MedicationName,
                                            p.Active,
                                            p.Quantity,
                                            p.RouteId,
                                            p.Dose,
                                            p.DoseUnit,
                                            p.Refills,
                                            p.Frequency,
                                            p.Sig,
                                            p.Diagnosis,
                                            p.StartDate,
                                            p.ExpireDate,
                                            p.Pharmacy,
                                            p.Status,
                                            p.Note,
                                            p.DateModified,
                                            p.DuringVisit
                                        };

                        PatientSummary.dtMedication = clsTableConverter.ToDataTable(results1);
                        PatientSummary.dtMedication.TableName = "PatientMedication";

                        #endregion

                        #region Problems

                        var results2 = from p in db.PatientProblems
                                        join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.CodeSystemId == 5 && p.Deleted == false  // Only Pull SNOMED
                                        select new
                                        {
                                            p.PatientProblemCntr,
                                            p.CodeValue,
                                            p.CodeSystemId,
                                            p.Condition,
                                            p.EffectiveDate,
                                            p.LastChangeDate,
                                            p.ConditionStatusId,
                                            ConditionStatus = c.Value,
                                            p.Note,
                                            p.DateModified
                                        };

                        PatientSummary.dtProblem = clsTableConverter.ToDataTable(results2);
                        PatientSummary.dtProblem.TableName = "PatientProblem";

                        #endregion

                        #region  Allergies

                        if (!Parms.ClinicalSummary)
                        {
                        var results3 = from p in db.PatientAllergies
                                        join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.CodeSystemId_Allergen == 6 && p.Deleted == false // Only Medication Allergies
                                        select new
                                        {
                                            p.PatientAllergyCntr,
                                            p.CodeValue_Allergen,
                                            p.CodeSystemId_Allergen,
                                            p.Allergen,
                                            p.AllergenType,
                                            p.CodeValue_Reaction,
                                            p.CodeSystemId_Reaction,
                                            p.Reaction,
                                            p.EffectiveDate,
                                            p.ConditionStatusId,
                                            ConditionStatus = c.Value,
                                            p.Note,
                                            p.DateModified
                                        };

                        PatientSummary.dtAllergy = clsTableConverter.ToDataTable(results3);
                        PatientSummary.dtAllergy.TableName = "PatientAllergy";
                        }
                        else
                        {
                            var results3 = from p in db.PatientAllergies
                                           join c in db.C_ConditionStatus on p.ConditionStatusId equals c.ConditionStatusId
                                           where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId &&  p.Deleted == false
                                           select new
                                           {
                                               p.PatientAllergyCntr,
                                               p.CodeValue_Allergen,
                                               p.CodeSystemId_Allergen,
                                               p.Allergen,
                                               p.AllergenType,
                                               p.CodeValue_Reaction,
                                               p.CodeSystemId_Reaction,
                                               p.Reaction,
                                               p.EffectiveDate,
                                               p.ConditionStatusId,
                                               ConditionStatus = c.Value,
                                               p.Note,
                                               p.DateModified
                                           };

                            PatientSummary.dtAllergy = clsTableConverter.ToDataTable(results3);
                            PatientSummary.dtAllergy.TableName = "PatientAllergy";
                        }
                        #endregion

                        #region Immunizations

                        var results4 = from p in db.PatientImmunizations
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                        select new
                                        {
                                            p.PatientImmunizationCntr,
                                            p.ImmunizationDate,
                                            p.ImmunizationTime,
                                            p.Vaccine,
                                            p.Amount,
                                            p.Route,
                                            p.Site,
                                            p.Sequence,
                                            p.ExpirationDate,
                                            p.LotNumber,
                                            p.Manufacturer,
                                            p.Note,
                                            p.DateModified
                                        };

                        PatientSummary.dtImmunization = clsTableConverter.ToDataTable(results4);
                        PatientSummary.dtImmunization.TableName = "PatientImmunization";

                        #endregion

                        #region Social History

                        var results5 = from p in db.PatientSocialHists
                                       join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                        select new
                                        {
                                            p.PatSocialHistCntr,
                                            p.Description,
                                            p.Qualifier,
                                            p.CodeValue,
                                            p.CodeSystemId,
                                            CodeSystem = s.Value,
                                            p.BeginDate,
                                            p.EndDate,
                                            p.Note,
                                            p.DateModified

                                        };

                        // Add smoking status from Demographics   -- SJF 5/5/2014
                        var results10 = from p in db.Patients
                                       join s in db.C_SmokingStatus on p.SmokingStatusId equals s.SmokingStatusId
                                       where p.PatientId == Parms.PatientId
                                       select new
                                       {
                                           p.SmokingStatusId,
                                           s.Value,
                                           s.SNOMED
                                       };
                        DataTable dt = clsTableConverter.ToDataTable(results5);
                        

                        if (results0 != null)
                        {
                            DataTable dt2 = clsTableConverter.ToDataTable(results10);
                            foreach (DataRow drsmoking in dt2.Rows)
                            {
                                DataRow dr = dt.NewRow();
                                dr["PatSocialHistCntr"] = 0;
                                dr["Description"] = drsmoking["value"];
                                dr["Qualifier"] = "";
                                dr["CodeValue"] = drsmoking["SNOMED"];
                                dr["CodeSystemId"] = 5;
                                dr["BeginDate"] = "";
                                dr["EndDate"] = "";
                                dr["Note"] = "";
                                dr["DateModified"] = Convert.ToDateTime("1/1/1900");
                                dt.Rows.Add(dr);
                            }
                        }


                        PatientSummary.dtSocialHist = dt;
                        PatientSummary.dtSocialHist.TableName = "PatientSocialHist";

                        #endregion
                    
                        #region Family History

                        var results13 = from p in db.PatientFamilyHists
                                       join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                       join r in db.C_Relationship on p.RelationshipId equals r.RelationshipId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                       select new
                                       {
                                           p.PatFamilyHistCntr,
                                           p.RelationshipId,
                                           Relationship = r.Value,
                                           p.Description,
                                           p.CodeValue,
                                           p.CodeSystemId,
                                           CodeSystem = s.Value,
                                           p.Note

                                       };

                        PatientSummary.dtFamilyHist = clsTableConverter.ToDataTable(results13);
                        PatientSummary.dtFamilyHist.TableName = "PatientFamilyHist";

                        #endregion
                    
                        #region Procedures

                        if (!Parms.ClinicalSummary)
                        {
                        var results6 = from p in db.PatientProcedures
                                       join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.CodeSystemId == 5 && p.Deleted == false
                                        select new
                                        {
                                            p.PatProcedureCntr,
                                            p.Description,
                                            p.CodeValue,
                                            p.CodeSystemId,
                                            CodeSystem = s.Value,
                                            p.Diagnosis,
                                            p.PerformedBy,
                                            p.ServiceLocation,
                                            p.ServiceDate,
                                            p.Note,
                                            p.DateModified
                                        };

                        PatientSummary.dtProcedure = clsTableConverter.ToDataTable(results6);
                        PatientSummary.dtProcedure.TableName = "PatientProcedure";
                        }
                        else
                        {
                            var results6 = from p in db.PatientProcedures
                                           join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                           where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                           select new
                                           {
                                               p.PatProcedureCntr,
                                               p.Description,
                                               p.CodeValue,
                                               p.CodeSystemId,
                                               CodeSystem = s.Value,
                                               p.Diagnosis,
                                               p.PerformedBy,
                                               p.ServiceLocation,
                                               p.ServiceDate,
                                               p.Note,
                                               p.DateModified
                                           };

                            PatientSummary.dtProcedure = clsTableConverter.ToDataTable(results6);
                            PatientSummary.dtProcedure.TableName = "PatientProcedure";
                        }
                        #endregion

                        #region Medicaal History

                        var results14 = from p in db.PatientMedicalHists
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                        select new
                                        {
                                            p.PatMedicalHistCntr,                                            
                                            p.Description,
                                            p.DateOfOccurance,
                                            p.Note,
                                            
                                        };

                        PatientSummary.dtMedicalHist = clsTableConverter.ToDataTable(results14);
                        PatientSummary.dtMedicalHist.TableName = "PatientMedicalHist";

                        #endregion

                        #region Vital Signs

                        var results7 = from p in db.PatientVitalSigns
                                        where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId && p.Deleted == false
                                        select new
                                        {
                                            p.PatientVitalCntr,
                                            p.VitalDate,
                                            p.HeightIn,
                                            p.WeightLb,
                                            p.BloodPressurePosn,
                                            p.Systolic,
                                            p.Diastolic,
                                            p.Pulse,
                                            p.Respiration,
                                            p.Temperature,
                                            p.DateModified
                                        };

                        PatientSummary.dtVitalSign = clsTableConverter.ToDataTable(results7);
                        PatientSummary.dtVitalSign.TableName = "PatientVitalSign";

                        #endregion

                        #region Lab Test & Results

                        var results8 = from p in db.PatientLabResults
                                       join t in db.PatientLabResultTests
                                        on new { p.PatientId, p.FacilityId, p.VisitId, p.LabResultCntr } equals new { t.PatientId, t.FacilityId, t.VisitId, t.LabResultCntr }
                                       join f in db.Facilities on p.FacilityId equals f.FacilityId
                                       join pv in db.Providers on p.ProviderId_Requested equals pv.ProviderId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId
                                       select new
                                       {
                                           p.LabResultCntr,
                                           p.ProviderId_Requested,
                                           ProviderName = pv.FirstName + " " + pv.LastName,
                                           p.CodeValue,
                                           p.CodeSystemId,
                                           p.LabName,
                                           p.FacilityId,
                                           f.FacilityName,
                                           p.OrderDate,
                                           p.CollectionDate,
                                           p.Requisiton,
                                           p.Specimen,
                                           p.SpecimenSource,
                                           p.ReportDate,
                                           p.ReviewDate,
                                           p.Reviewer,
                                           TestCodeValue = t.CodeValue,
                                           TestCodeSystemId = t.CodeSystemId,
                                           t.Component,
                                           t.Result,
                                           t.RefRange,
                                           t.Units,
                                           t.Abnormal,
                                           t.ResultStatus
                                       };

                        PatientSummary.dtLab = clsTableConverter.ToDataTable(results8);
                        PatientSummary.dtLab.TableName = "PatientLabResult";

                        #endregion

                        #region Plan Of Care

                        var results9 = from p in db.PatientPlanOfCares
                                        join c in db.C_InstructionType on p.InstructionTypeId equals c.InstructionTypeId
                                        join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId  && p.InstructionTypeId != 4
                                        select new
                                        {
                                            p.PlanCntr,
                                            p.InstructionTypeId,
                                            InstructionType = c.Value,
                                            p.CodeValue,
                                            p.CodeSystemId,
                                            CodeSystem = s.Value,
                                            p.Instruction,
                                            p.Goal,
                                            p.Note,
                                            p.AppointmentDateTime,
                                            p.ProviderId
                                        };



                        PatientSummary.dtPlanOfCare = clsTableConverter.ToDataTable(results9);
                        PatientSummary.dtPlanOfCare.TableName = "PatientPlanOfCare";

                        #endregion

                        #region Clinical Instructions

                        var results12 = from p in db.PatientPlanOfCares
                                       join c in db.C_InstructionType on p.InstructionTypeId equals c.InstructionTypeId
                                       join s in db.C_CodeSystem on p.CodeSystemId equals s.CodeSystemId
                                       where p.PatientId == Parms.PatientId && p.FacilityId == Parms.FacilityId && p.VisitId == Parms.VisitId &&  p.InstructionTypeId == 4
                                       select new
                                       {
                                           p.PlanCntr,
                                           p.InstructionTypeId,
                                           InstructionType = c.Value,
                                           p.CodeValue,
                                           p.CodeSystemId,
                                           CodeSystem = s.Value,
                                           p.Instruction,
                                           p.Goal,
                                           p.Note,
                                           p.AppointmentDateTime,
                                           p.ProviderId
                                       };


                        
                        PatientSummary.dtClinicalInstructions = clsTableConverter.ToDataTable(results12);
                        PatientSummary.dtClinicalInstructions.TableName = "PatientClinicalInstructions";

                        #endregion


                    }
                }
                catch (Exception ex)
                {
                    PatientSummary.Valid = false;
                    PatientSummary.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSummary.Valid = false;
                PatientSummary.ErrorMessage = "Invalid Token";
            }
            return PatientSummary;
        }
        #endregion

        #region Get PatientNote Data
        //------------------------------------------------------------------------
        // Get PatientNote Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientNote Data")]
        public PatientDocTableData GetPatientNoteData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientNote = new PatientDocTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                // If Option = 0 - Patient entered, then VisitId needs to be 0.
                if (Parms.Option == 0)
                    Parms.VisitId = 0;

                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from p in db.PatientNotes
                                      where p.PatientId == Parms.PatientId
                                      orderby p.DateCreated descending
                                      select new
                                      {
                                          p.PatientId,
                                          p.DateCreated,
                                          p.Note,
                                      };

                        PatientNote.dt = clsTableConverter.ToDataTable(results);
                        PatientNote.dt.TableName = "PatientNote";

                        PatientNote.Valid = true;
                        PatientNote.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientNote.Valid = false;
                    PatientNote.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientNote.Valid = false;
                PatientNote.ErrorMessage = "Invalid Token";
            }
            return PatientNote;
        }
        #endregion

        #region Save PatientNote Data
        //------------------------------------------------------------------------
        // Save PatientNote Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientNote Data")]
        public PatientNoteData SavePatientNoteData(PatientNoteData PatientNote, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientNote.Valid = true;
            PatientNote.ErrorMessage = "";

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

                        //// Add Items
                        var Note = new PatientNote()
                        {
                            PatientId = PatientNote.PatientId,
                            DateCreated = System.DateTime.Now,
                            Note = PatientNote.Note,
                        };
                        db.PatientNotes.Add(Note);

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    PatientNote.Valid = false;
                    PatientNote.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientNote.Valid = false;
                PatientNote.ErrorMessage = "Invalid Token";
            }
            return PatientNote;
        }
        #endregion

        #region Get PatientSocialSelf Data
        //------------------------------------------------------------------------
        // Get PatientSocialSelf Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PatientSocialSelf Data")]
        public PatientSocialSelfData GetPatientSocialSelfData(PatientDocParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientSocialSelfData PatientSocial = new PatientSocialSelfData();

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
                   
                        
                       

                        PatientSocialSelf results = db.PatientSocialSelves.FirstOrDefault(p => p.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            PatientSocial.Valid = true;
                            PatientSocial.ErrorMessage = "";
                            PatientSocial.PatientId = results.PatientId;
                            PatientSocial.Birthplace = results.Birthplace;
                            PatientSocial.EducationLevelId = Convert.ToInt16(results.EducationLevelId);
                            PatientSocial.Occupation = results.Occupation;
                            PatientSocial.Retired = Convert.ToBoolean(results.Retired);
                            PatientSocial.ChildrenSon = Convert.ToInt16(results.ChildrenSon);
                            PatientSocial.ChildrenDaughter = Convert.ToInt16(results.ChildrenDaughter);
                            PatientSocial.CaffeineUser = Convert.ToBoolean(results.CaffeineUser);
                            PatientSocial.CaffieneType = results.CaffieneType;
                            PatientSocial.CaffeineAmount = results.CaffeineAmount;
                            PatientSocial.ExerciseMember = Convert.ToBoolean(results.ExerciseMember);
                            PatientSocial.ExerciseFrequencyId = Convert.ToInt16(results.ExerciseFrequencyId);
                            PatientSocial.ExerciseAmount = results.ExerciseAmount;
                            PatientSocial.ActivityLevelId = Convert.ToInt16(results.ActivityLevelId);
                            PatientSocial.Activity1 = results.Activity1;
                            PatientSocial.Activity2 = results.Activity2;
                            PatientSocial.Activity3 = results.Activity3;
                            PatientSocial.AlcoholUser = Convert.ToBoolean(results.AlcoholUser);
                            PatientSocial.AlcoholFrequencyId = Convert.ToInt16(results.AlcoholFrequencyId);
                            PatientSocial.AlcoholLastUse = Convert.ToDateTime(results.AlcoholLastUse);
                            PatientSocial.AlcoholType = results.AlcoholType;
                            PatientSocial.AlcoholStartAge = Convert.ToInt16(results.AlcoholStartAge);
                            PatientSocial.AlcoholFamilyHist = Convert.ToBoolean(results.AlcoholFamilyHist);
                            PatientSocial.SmokingStatusId = Convert.ToInt32(results.SmokingStatusId);
                            PatientSocial.SmokingDailyAmount = Convert.ToInt16(results.SmokingDailyAmount);
                            PatientSocial.SmokingType = results.SmokingType;
                            PatientSocial.SmokingYears = Convert.ToInt16(results.SmokingYears);
                            PatientSocial.SmokingQuitAttempts = Convert.ToInt16(results.SmokingQuitAttempts);
                            PatientSocial.SmokingQuitDate = Convert.ToDateTime(results.SmokingQuitDate);
                            PatientSocial.SmokingSecondHand = Convert.ToBoolean(results.SmokingSecondHand);
                            PatientSocial.Valid = true;
                            PatientSocial.ErrorMessage = "";

                            //added by talha
                            var res = (from d in db.PatientSocialSelves
                                       join s in db.C_SmokingStatus on d.SmokingStatusId equals s.SmokingStatusId
                                       where d.PatientId == Parms.PatientId
                                       select new
                                       {
                                           s.SNOMED,
                                           s.Value,
                                           s.SmokingStatusId,

                                       }).ToList();
                            PatientSocial.SmokingStatus = res[0].Value;
                            WriteAuditRecord(Parms.PatientId, 0, 0, FacilityId, DocTypePatientSocialHist, UserId, ActionRead);
                        }

                        else
                        {
                            PatientSocial.Valid = false;
                            PatientSocial.ErrorMessage = "Could not read record.";
                        }

                    }
                }
                catch (Exception ex)
                {
                    PatientSocial.Valid = false;
                    PatientSocial.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSocial.Valid = false;
                PatientSocial.ErrorMessage = "Invalid Token";
            }
            return PatientSocial;
        }
        #endregion

        #region Save PatientSocialSelfPatientSocialSelf Data
        //------------------------------------------------------------------------
        // Save PatientSocialHist Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save PatientSocialSelf Data")]
        public PatientSocialSelfData SavePatientSocialSelfData(PatientSocialSelfData PatientSocialData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSocialData.Valid = true;
            PatientSocialData.ErrorMessage = "";

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
                        PatientSocialSelf PatientResp = db.PatientSocialSelves.FirstOrDefault(p => p.PatientId == PatientSocialData.PatientId);

                        if (PatientResp != null)
                        {

                            PatientResp.Birthplace = PatientSocialData.Birthplace;
                            PatientResp.EducationLevelId = PatientSocialData.EducationLevelId;
                            PatientResp.Occupation = PatientSocialData.Occupation;
                            PatientResp.Retired = PatientSocialData.Retired;
                            PatientResp.ChildrenSon = PatientSocialData.ChildrenSon;
                            PatientResp.ChildrenDaughter = PatientSocialData.ChildrenDaughter;
                            PatientResp.CaffeineUser = PatientSocialData.CaffeineUser;
                            PatientResp.CaffieneType = PatientSocialData.CaffieneType;
                            PatientResp.CaffeineAmount = PatientSocialData.CaffeineAmount;
                            PatientResp.ExerciseMember = PatientSocialData.ExerciseMember;
                            PatientResp.ExerciseFrequencyId =PatientSocialData.ExerciseFrequencyId;
                            PatientResp.ExerciseFrequencyId = PatientSocialData.ExerciseFrequencyId;
                            PatientResp.ExerciseAmount = PatientSocialData.ExerciseAmount;
                            PatientResp.ActivityLevelId = PatientSocialData.ActivityLevelId;
                            PatientResp.Activity1 = PatientSocialData.Activity1;
                            PatientResp.Activity2 = PatientSocialData.Activity2;
                            PatientResp.Activity3 = PatientSocialData.Activity3;
                            PatientResp.AlcoholUser = PatientSocialData.AlcoholUser;
                            PatientResp.AlcoholFrequencyId = PatientSocialData.AlcoholFrequencyId;
                            PatientResp.AlcoholLastUse = PatientSocialData.AlcoholLastUse;
                            PatientResp.AlcoholType = PatientSocialData.AlcoholType;
                            PatientResp.AlcoholStartAge = PatientSocialData.AlcoholStartAge;
                            PatientResp.AlcoholFamilyHist = PatientSocialData.AlcoholFamilyHist;
                            PatientResp.SmokingStatusId = PatientSocialData.SmokingStatusId;
                            PatientResp.SmokingDailyAmount = PatientSocialData.SmokingDailyAmount;
                            PatientResp.SmokingType = PatientSocialData.SmokingType;
                            PatientResp.SmokingYears = PatientSocialData.SmokingYears;
                            PatientResp.SmokingQuitAttempts = PatientSocialData.SmokingQuitAttempts;
                            PatientResp.SmokingQuitDate = PatientSocialData.SmokingQuitDate;
                            PatientResp.SmokingSecondHand = PatientSocialData.SmokingSecondHand;
                            PatientResp.UserId_Modified = UserId;
                            PatientResp.DateModified = System.DateTime.Now;
                        }
                        else
                        {
                            // Add Patient
                            var PatientSocial = new PatientSocialSelf()
                            {

                                PatientId = PatientSocialData.PatientId,
                                Birthplace = PatientSocialData.Birthplace,
                                EducationLevelId = PatientSocialData.EducationLevelId,
                                Occupation = PatientSocialData.Occupation,
                                Retired = PatientSocialData.Retired,
                                ChildrenSon = PatientSocialData.ChildrenSon,
                                ChildrenDaughter = PatientSocialData.ChildrenDaughter,
                                CaffeineUser = PatientSocialData.CaffeineUser,
                                CaffieneType = PatientSocialData.CaffieneType,
                                CaffeineAmount = PatientSocialData.CaffeineAmount,
                                ExerciseMember = PatientSocialData.ExerciseMember,
                                ExerciseFrequencyId = PatientSocialData.ExerciseFrequencyId,
                                ExerciseAmount = PatientSocialData.ExerciseAmount,
                                ActivityLevelId = PatientSocialData.ActivityLevelId,
                                Activity1 = PatientSocialData.Activity1,
                                Activity2 = PatientSocialData.Activity2,
                                Activity3 = PatientSocialData.Activity3,
                                AlcoholUser = PatientSocialData.AlcoholUser,
                                AlcoholFrequencyId = PatientSocialData.AlcoholFrequencyId,
                                AlcoholLastUse = PatientSocialData.AlcoholLastUse,
                                AlcoholType = PatientSocialData.AlcoholType,
                                AlcoholStartAge = PatientSocialData.AlcoholStartAge,
                                AlcoholFamilyHist = PatientSocialData.AlcoholFamilyHist,
                                SmokingStatusId = PatientSocialData.SmokingStatusId,
                                SmokingDailyAmount = PatientSocialData.SmokingDailyAmount,
                                SmokingType = PatientSocialData.SmokingType,
                                SmokingYears = PatientSocialData.SmokingYears,
                                SmokingQuitAttempts = PatientSocialData.SmokingQuitAttempts,
                                SmokingQuitDate = PatientSocialData.SmokingQuitDate,
                                SmokingSecondHand = PatientSocialData.SmokingSecondHand,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                            };
                            db.PatientSocialSelves.Add(PatientSocial);
                        }

                        db.SaveChanges();
                        
                    }
                }
                catch (Exception ex)
                {
                    PatientSocialData.Valid = false;
                    PatientSocialData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientSocialData.Valid = false;
                PatientSocialData.ErrorMessage = "Invalid Token";
            }
            return PatientSocialData;
        }
        #endregion


        #region Get Combined Patient Document Data
        //------------------------------------------------------------------------
        // Get Combined Patient Document Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Combined Patient Document Data")]
        public PatientDocTableData GetCombinedPatientDocumentData(PatientDocumentParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            PatientDocTableData PatientDocument = new PatientDocTableData();

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
                        var results = from d in db.vwPatientDocumentsLists
                                      where d.PatientId == Parms.PatientId
                                      select new
                                      {
                                          d.PatientId, 
                                          d.FacilityId, 
                                          d.VisitId, 
                                          d.DocumentCntr, 
                                          d.DocType, 
                                          d.DocumentDescription,
                                      };



                        PatientDocument.dt = clsTableConverter.ToDataTable(results);
                        PatientDocument.dt.TableName = "Documents";

                        PatientDocument.Valid = true;
                        PatientDocument.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    PatientDocument.Valid = false;
                    PatientDocument.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PatientDocument.Valid = false;
                PatientDocument.ErrorMessage = "Invalid Token";
            }
            return PatientDocument;
        }
        #endregion
    }
}
