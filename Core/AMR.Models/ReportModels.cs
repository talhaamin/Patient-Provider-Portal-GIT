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
    public class ReportViewModels
    {
        public List<FacilityModel> Facilities;
        public ReportViewModels()
        {
            Facilities = new List<FacilityModel>();
        }
    }
}
