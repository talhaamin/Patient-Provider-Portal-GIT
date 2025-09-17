using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public  class PremiumViewModel
    {
       
        #region  Share My Records
        public CareProviderModel CareProvider;
        public PatientCareDocumentDataModel PatientCareDocumentData;
        public PatientSummaryModel PatientSummary;
        #endregion

        public List<PatientEmergency> Emergencies;
        public List<DoctorUploadModel> DoctorUploads;
        public List<PatientUploadModel> PatientUploads;
        public List<PatientPolicyModel> Policies;
        public List<PatientMedicalDocumentModel> Documents;
        public List<PatientMedicationModel> Medications;
        public List<ProblemModel> Problems;
        public List<VisitModel> Visits;
        public List<AllergyModel> Allergies;
        public List<PatientDoctorModel> PatientDoctor;
        public PremiumViewModel()
        {
            PatientSummary = new PatientSummaryModel();
            PatientCareDocumentData = new PatientCareDocumentDataModel();
            CareProvider = new CareProviderModel();


            Emergencies = new List<PatientEmergency>();
            DoctorUploads = new List<DoctorUploadModel>();
            PatientUploads = new List<PatientUploadModel>();
            Policies = new List<PatientPolicyModel>();
            Documents = new List<PatientMedicalDocumentModel>();
            Visits = new List<VisitModel>();
            Allergies = new List<AllergyModel>();
            Medications = new List<PatientMedicationModel>();
            Problems = new List<ProblemModel>();
            PatientDoctor = new List<PatientDoctorModel>();
        }
    }
}
