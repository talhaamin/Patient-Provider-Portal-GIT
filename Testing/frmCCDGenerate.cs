using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AMR.Data;
using System.IO;

namespace Testing
{
    public partial class frmCCDGenerate : Form
    {
        public frmCCDGenerate()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

            Int64 PatientId = Convert.ToInt64(nudPatientId.Value);
            Int64 VisitId = Convert.ToInt64(nudVisitId.Value);
            Int64 FacilityId = Convert.ToInt64(nudFacilityId.Value);

            clsCCDGenerate objCCDGenerate = new clsCCDGenerate();
            clsCCDGenerate.CCDParms Parms2 = new clsCCDGenerate.CCDParms();
            Parms2.Problems = true;
            Parms2.Allergies = true;
            Parms2.Immunizations = false;
            Parms2.Medications = false;
            Parms2.Labs = true;
            Parms2.ClinicalInstructions = true;
            //Parms2.FutureAppointments = true;
            //Parms2.Referrals = true;
            //Parms2.ScheduledTests = true;
            //Parms2.DecisionAids = true;
            string CCD = "";
            bool Valid = objCCDGenerate.createPatientCCD(PatientId, FacilityId, VisitId, Parms2, ref CCD);


            using (System.IO.StreamWriter file = new System.IO.StreamWriter( @"c:\temp\ccd_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"))
            {

                file.WriteLine(CCD);
            }

        }
    }
}
