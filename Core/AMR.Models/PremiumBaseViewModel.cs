using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class PremiumBaseViewModel
    {
        public List<CountryModel> Country;
        public List<StatesModel> States;

        public PremiumBaseViewModel()
        {
            Country = new List<CountryModel>();
            States = new List<StatesModel>();
        }
    }
}
