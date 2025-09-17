using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc; 

namespace AMR.Models
{
    public class GeneralViewModel
    {
        public IEnumerable<SelectListItem> Visits { get; set; }
        public IEnumerable<SelectListItem> Facilities { get; set; }

    }

   
}
