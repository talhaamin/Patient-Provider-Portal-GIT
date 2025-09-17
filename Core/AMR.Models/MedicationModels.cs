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
    public class MedicationViewModel
    {
        public List<PatientMedicationModel> ActiveMedications;
        public List<PatientMedicationModel> InActiveMedications;
        public List<PatientMedicationModel> Medications;
        public PatientSummaryModel PatientSummary;
        public List<FacilityModel> Facilities;
        public List<VisitModel> Visits;
        public FacilityVisitSelectModel FacilityVisitSelect;
        public List<PatientPharmacyModel> Pharmacies;
        public List<RoutesOfAdministrationModel> RoutesAdministration;
        public List<FacilityModel> MessageFacilities;
        public List<MessageTypeModel> MessageType;
        public List<MessageUrgency> MessageUrgencyType;
        public List<ProviderModel> Providers;

        
        public MedicationViewModel()
        {
           
            ActiveMedications = new List<PatientMedicationModel>();
            InActiveMedications = new List<PatientMedicationModel>();
            Medications = new List<PatientMedicationModel>();
            PatientSummary=new PatientSummaryModel();
            Facilities = new List<FacilityModel>();
            Visits = new List<VisitModel>();
            Pharmacies = new List<PatientPharmacyModel>();
            FacilityVisitSelect = new FacilityVisitSelectModel();
            RoutesAdministration = new List<RoutesOfAdministrationModel>();
            MessageFacilities = new List<FacilityModel>();
            MessageType = new List<MessageTypeModel>();
            MessageUrgencyType = new List<MessageUrgency>();
            Providers = new List<ProviderModel>();
        }
    }
   
    
}
