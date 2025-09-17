using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public  class AdminViewModel
    {
        public List<ThirdPartyModel> ThirdParty;
        public List<PatientNotesModel> PatientNotes;
        public List<PracticeDataModel> Practice;
        public List<SecurityQuestionModel> SecurityQuestion;
        public List<PartnerModelForAdmin> Partner;

        public AdminViewModel()
        {
            ThirdParty = new List<ThirdPartyModel>();
            PatientNotes = new List<PatientNotesModel>();
            Practice = new List<PracticeDataModel>();
            SecurityQuestion = new List<SecurityQuestionModel>();
            Partner = new List<PartnerModelForAdmin>();
        }
    }
}
