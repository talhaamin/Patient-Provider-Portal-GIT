using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AMR.Data;

namespace AMR.Models
{
   
    public class ClinicalSummaryViewModel
    {
        public List<PatientMessageModel> Messages;
        public List<ProblemSNOMEDModel> ProblemsSNOMED;   
        public List<FacilityModel> Facilities;
        public List<RelationshipModel> Relationships;
        public List<VisitModel> Visits;
        public PatientModel Patient;
        public PatientSummaryModel PatientSummary;
        public List<SocialHistoryModel> SocialHistories;
        public PatientVisitDataModel PatientVisit;
        public List<FamilyHistoryModel> FamilyHistories;
        public List<PatientMedicationModel> Medications;
        public List<MedicalHistoryModel> MedicalHistories;
        public List<LabResultModel> LabResults;
        public List<ProblemModel> Problems;
        public List<ImmunizationModel> Immunizations;
        public List<PatientVitalSignModel> VitalSigns;
        public List<GenderModel> Gender;
        public List<PreferredLanguageModel> PreferredLanguage;
        public List<EthnicityModel> Ethnicity;
        public List<CountryModel> Country;
        public List<StatesModel> States;
        public List<RaceModel> Race;
        public List<SmokingStatusModel> SmokingStatus; 
        public List<ProviderModel> Providers;
        public List<POCModel> Pocs;
        public List<AllergyModel> Allergies;
        public List<ProcedureModel> Procedures;
        public List<ConditionStatusModel> ConditionStatus;
        public List<BloodTypeModel> BloodType;
        public List<ReligionModel> Religion;
        public PatientEmergencyModel PatientEmergency;
        public PatientOrganModel PatientOrgan;
        public List<VaccineModel> Vaccines;
        public List<PatientPharmacyModel> Pharmacies;
        public List<LabResultTestModel> LabTests;
        public List<SocialModel> Social;
        public PatientRepModel PatientRepresentatives;
        public QueryStringValues Querystring;
        public List<LabResultTestModel> LabResultTest;
        public List<PatientPolicyModel> Policies;
        public List<PatientInsuranceModel> Insurance;
        public List<PatientMedicalDocumentModel> Documents;
        public PatientVisitCCDModel PatientVisitCCD;
        public FacilityVisitSelectModel FacilityVisitSelect;
        public PatientShareModel PatientShare;
        public List<POCModel> ClinicalInstructions;
        public List<ProviderInfoModel> ProivderInfo;
        public List<InstructionType> InstructionType;
        public List<ExerciseFrequencyModel> frequency;
        public List<ActivityLevelModel> ActivityLevel;
        public List<AlcoholFrequencyModel> AlcoholFrequency;
        public List<SmokingStatusModel> smokingstatus;
        public List<EducationLevelModel> Educationlevel;
        public List<MartialStatusModel> MartialStatus;
        public PatientSocialSelfDataModel PatientSocialSelf;
        public List<MedicalPortfolioBaseModel> MedicalPortfolioModel;
        public List<FacilityModel> MessageFacilities;
        public List<MessageTypeModel> MessageType;
        public List<MessageUrgency> MessageUrgencyType;
       

        public ClinicalSummaryViewModel()
        {

            LabResultTest = new List<LabResultTestModel>();
            Pharmacies = new List<PatientPharmacyModel>();
            Messages = new List<PatientMessageModel>();
            Relationships = new List<RelationshipModel>();
            ProblemsSNOMED = new List<ProblemSNOMEDModel>();
            Providers = new List<ProviderModel>();
            Facilities=new List<FacilityModel>();
            Visits = new List<VisitModel>();
            Patient = new PatientModel();
            PatientSummary = new PatientSummaryModel();
            SocialHistories = new List<SocialHistoryModel>();
            FamilyHistories = new List<FamilyHistoryModel>();
            PatientVisit = new PatientVisitDataModel();
            Medications = new List<PatientMedicationModel>();
            MedicalHistories = new List<MedicalHistoryModel>();
            LabResults = new List<LabResultModel>();
            Problems = new List<ProblemModel>();
            Immunizations = new List<ImmunizationModel>();
            VitalSigns = new List<PatientVitalSignModel>();
            Gender = new List<GenderModel>();
            PreferredLanguage = new List<PreferredLanguageModel>();
            Ethnicity = new List<EthnicityModel>();
            Country = new List<CountryModel>();
            States = new List<StatesModel>();
            Race = new List<RaceModel>();
            SmokingStatus = new List<SmokingStatusModel>();
            Pocs = new List<POCModel>();
            Allergies = new List<AllergyModel>();
            Procedures = new List<ProcedureModel>();
            PatientEmergency = new PatientEmergencyModel();
            BloodType = new List<BloodTypeModel>();
            Religion = new List<ReligionModel>();
            ConditionStatus = new List<ConditionStatusModel>();
            PatientOrgan = new PatientOrganModel();
            Vaccines = new List<VaccineModel>();
            LabTests = new List<LabResultTestModel>();
            Social = new List<SocialModel>();
            PatientRepresentatives = new PatientRepModel();
            Querystring = new QueryStringValues();
            Policies = new List<PatientPolicyModel>();
            Documents = new List<PatientMedicalDocumentModel>();
            Insurance = new List<PatientInsuranceModel>();
            PatientVisitCCD = new PatientVisitCCDModel();
            FacilityVisitSelect = new FacilityVisitSelectModel();
            PatientShare = new PatientShareModel();
            ClinicalInstructions = new List<POCModel>();
            ProivderInfo = new List<ProviderInfoModel>();
            InstructionType = new List<InstructionType>();
            PatientSocialSelf = new PatientSocialSelfDataModel();
            frequency = new List<ExerciseFrequencyModel>();
            ActivityLevel = new List<ActivityLevelModel>();
            AlcoholFrequency = new List<AlcoholFrequencyModel>();
            smokingstatus = new List<SmokingStatusModel>();
            Educationlevel = new List<EducationLevelModel>();
            MartialStatus = new List<MartialStatusModel>();
            MedicalPortfolioModel = new List<MedicalPortfolioBaseModel>();
            MessageFacilities = new List<FacilityModel>();
            MessageType = new List<MessageTypeModel>();
            MessageUrgencyType = new List<MessageUrgency>();
            
        }
    }
   
    
    
}
