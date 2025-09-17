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
    public class HomeViewModel
    {
        public List<PatientMessageModel> Messages;
        public UserModel CurrentUser;
        public PatientImageData ImageData;
        public PatientModel Patient;
        public List<ProblemSNOMEDModel> ProblemsSNOMED;
        public List<MedicationListModel> MedicationList;
        public List<RelationshipModel> Relationships;
        public List<VaccineModel> Vaccines;
        public List<FacilityModel> Facilities;
        public List<FacilityModel> MessageFacilities;
        public List<VisitModel> Visits;
        public List<PatientMedicationModel> Medications;
        public List<PatientMedicationModel> MedicationsAdministered;
        public List<LabResultModel> LabResults;
        public List<ProblemModel> Problems;
        public List<ImmunizationModel> Immunizations;
        public List<PatientVitalSignModel> VitalSigns;
        public List<MedicalHistoryModel> MedicalHistories;
        public List<ConditionStatusModel> ConditionStatus;
        public List<SocialHistoryModel> SocialHistories;
        public List<FamilyHistoryModel> FamilyHistories;
        public List<ProviderModel> Providers;
        public List<PatientPharmacyModel> Pharmacies;
        public List<POCModel> Pocs;
        public List<POCModel> ClinicalInstructions;
        public List<AllergyModel> Allergies;
        public List<ProcedureModel> Procedures;
        public List<MessageTypeModel> MessageType;
        public List<MessageUrgency> MessageUrgencyType;
        public PatientMessageModel PatientMessage;
        public PatientSummaryModel PatientSummary;
        public List<CountryModel> Country;
        public List<StatesModel> States;
        public string FirstLogin;
        public List<SecurityQuestionModel> SecurityQuestion;
        public List<SocialModel> Social;
        public PatientRepModel PatientRepresentatives;
        public List<PatientPharmacyModel> PharmciesForRefill;
        public PatientCCD PatientCCDownload;
        public List<LabResultTestModel> LabResultTest;
        public List<PatientPolicyModel> Policies;
        public List<PatientMedicalDocumentModel> Documents;
        public List<PatientInsuranceModel> Insurance;
        public PatientVisitCCDModel PatientVisitCCD;
        public FacilityVisitSelectModel FacilityVisitSelect;
        public List<HomeWidgetModel> widgets;
        public List<PatientAccountLinkModel> patientaccount;
        public BillRateDataModel Billrate;
        public PatientAccountLinkModel patacclinked;
        public PatientShareModel PatientShare;
        public List<PatientWebSettingModelString> PatientWebSetting;
        public List<InstructionType> InstructionType;
        public List<PatientModel> PatientModel;
        public List<CarrierModel> CarrierModel;
        public List<ProviderModel> ProviderModel;
        public PatientWebSettingDataModel PatientWebSettingData;
        public List<ExerciseFrequencyModel> frequency;
        public List<ActivityLevelModel> ActivityLevel;
        public List<AlcoholFrequencyModel> AlcoholFrequency;
        public List<SmokingStatusModel> smokingstatus;
        public List<EducationLevelModel> Educationlevel;
        public List<MartialStatusModel> MartialStatus;
        public PatientSocialSelfDataModel PatientSocialSelf;
        public List<ProviderInfoModel> ProivderInfo;
        public List<ThirdPartyModel> ThirdParty;
        public List<RoutesOfAdministrationModel> RoutesAdministration;
        public List<PatientMedicationModel> MedicationsClincial;

        public HomeViewModel()
        {
            ImageData = new PatientImageData();
            Patient=new PatientModel();
            PatientSummary = new PatientSummaryModel();
            Messages = new List<PatientMessageModel>();
            CurrentUser = new UserModel();
            Facilities=new List<FacilityModel>();
            MessageFacilities = new List<FacilityModel>();
            Visits = new List<VisitModel>();
            Medications =new List<PatientMedicationModel>();
            LabResults = new List<LabResultModel>();
            SocialHistories = new List<SocialHistoryModel>();
            FamilyHistories = new List<FamilyHistoryModel>();
            MedicalHistories = new List<MedicalHistoryModel>();
            MedicationList = new List<MedicationListModel>();
            Problems = new List<ProblemModel>();
            ProblemsSNOMED = new List<ProblemSNOMEDModel>();
            Immunizations = new List<ImmunizationModel>();
            VitalSigns = new List<PatientVitalSignModel>();
            Providers = new List<ProviderModel>();
            Pharmacies = new List<PatientPharmacyModel>();
            Relationships = new List<RelationshipModel>();
            Vaccines = new List<VaccineModel>();
            Pocs = new List<POCModel>();
            ClinicalInstructions = new List<POCModel>();
            Allergies = new List<AllergyModel>();
            Procedures = new List<ProcedureModel>();
            MessageType = new List<MessageTypeModel>();
            MessageUrgencyType = new List<MessageUrgency>();
            PatientMessage = new PatientMessageModel();
            ConditionStatus = new List<ConditionStatusModel>();
            Country = new List<CountryModel>();
            States = new List<StatesModel>();
            SecurityQuestion = new List<SecurityQuestionModel>();
            Social = new List<SocialModel>();
            PatientRepresentatives = new PatientRepModel();
            PharmciesForRefill = new List<PatientPharmacyModel>();
            LabResultTest = new List<LabResultTestModel>();
            PatientCCDownload = new PatientCCD();
            Policies = new List<PatientPolicyModel>();
            Documents = new List<PatientMedicalDocumentModel>();
            Insurance = new List<PatientInsuranceModel>();
            PatientVisitCCD = new PatientVisitCCDModel();
            FacilityVisitSelect = new FacilityVisitSelectModel();
            widgets = new List<HomeWidgetModel>();
            patientaccount = new List<PatientAccountLinkModel>();
            Billrate = new BillRateDataModel();
            patacclinked = new PatientAccountLinkModel();
            PatientShare = new PatientShareModel();
            PatientWebSetting = new List<PatientWebSettingModelString>();
            InstructionType = new List<InstructionType>();
            PatientModel = new List<PatientModel>();
            CarrierModel = new List<CarrierModel>();
            PatientWebSettingData = new PatientWebSettingDataModel();
            frequency = new List<ExerciseFrequencyModel>();
            ActivityLevel = new List<ActivityLevelModel>();
            AlcoholFrequency = new List<AlcoholFrequencyModel>();
            smokingstatus = new List<SmokingStatusModel>();
            Educationlevel = new List<EducationLevelModel>();
            MartialStatus = new List<MartialStatusModel>();
            PatientSocialSelf = new PatientSocialSelfDataModel();
            ProivderInfo = new List<ProviderInfoModel>();
            RoutesAdministration = new List<RoutesOfAdministrationModel>();
            MedicationsClincial = new List<PatientMedicationModel>();
            ProviderModel = new List<ProviderModel>();
            
        }
    }
   
   
    
}
