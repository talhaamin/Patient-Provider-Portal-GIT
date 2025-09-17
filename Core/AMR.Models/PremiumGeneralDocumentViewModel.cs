using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class PremiumGeneralDocumentViewModel : PremiumBaseViewModel
    {
        public List<GeneralDocumentModel> GeneralDocuments;
        public List<InsurancePolicyModel> InsurancePolicies;
        public List<ProfessionalAdvisorModel> ProfessionalAdvisors;
        public List<PolicyTypeModel> PolicyTypes;
        public List<AdvisorTypeModel> AdvisorTypes;

        public PremiumGeneralDocumentViewModel ()
	    {
            GeneralDocuments = new List<GeneralDocumentModel>();
            InsurancePolicies = new List<InsurancePolicyModel>();
            ProfessionalAdvisors = new List<ProfessionalAdvisorModel>();
            PolicyTypes = new List<PolicyTypeModel>();
            AdvisorTypes = new List<AdvisorTypeModel>();
	    }
    }
}
