using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Models
{
    public class PremiumManageDoctorViewModel : PremiumBaseViewModel
    {
        public List<PatientDoctorModel> PatientDoctor;
        public List<DoctorSpecialityModel> DoctorSpeciality;

        public PremiumManageDoctorViewModel()
        {
            PatientDoctor = new List<PatientDoctorModel>();
            DoctorSpeciality = new List<DoctorSpecialityModel>();
        }


    }
}
