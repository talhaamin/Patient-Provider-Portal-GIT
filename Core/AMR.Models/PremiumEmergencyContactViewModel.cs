using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class PremiumEmergencyContactViewModel : PremiumBaseViewModel
    {
        public List<PatientEmergencyModel> PatientEmergency;
        public List<RelationshipModel> Relationships;

        public PremiumEmergencyContactViewModel()
        {
            PatientEmergency = new List<PatientEmergencyModel>();
            Relationships = new List<RelationshipModel>();
        }
    }
}
