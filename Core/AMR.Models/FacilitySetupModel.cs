using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
   public  class FacilitySetupModel
    {

        public List<OrangizationModel> ModelOrganizaton;
        public List<FacilityPracticeModel> ModelPractice;
        public List<FacilitySetupPracticeModel> ModelFacility;
        public FacilitySetupModel()
        {
            ModelOrganizaton = new List<OrangizationModel>();
            ModelPractice = new List<FacilityPracticeModel>();
            ModelFacility = new List<FacilitySetupPracticeModel>();
        
        }
    }
}
