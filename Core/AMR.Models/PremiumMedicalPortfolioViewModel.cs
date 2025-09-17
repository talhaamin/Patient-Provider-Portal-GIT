using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class PremiumMedicalPortfolioViewModel
    {
        public List<PatientVisitModel> PatientVisit;
        public List<PatientOutsideDoctorModel> OutsideDoctor;
        public List<PatientMedicalDocumentModel> PatientDocument;

        public PremiumMedicalPortfolioViewModel()
        {
            PatientVisit = new List<PatientVisitModel>();
            OutsideDoctor = new List<PatientOutsideDoctorModel>();
            PatientDocument = new List<PatientMedicalDocumentModel>();
        }

    }
}
