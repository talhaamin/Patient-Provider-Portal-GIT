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

namespace Testing
{
    public partial class frmCCDDirect : Form
    {
        bool EmailValid = false;
        public frmCCDDirect()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //clsEmail objEmail = new clsEmail();

            //string emailTo = txtEmail.Text;
            //string subject = "CCD Test Message";
            //string filename = @"C:\temp\testccd.xml";
            //string message = "CCD Record for John Doe";

            //objEmail.SendSecureEmail(emailTo, subject, message, filename);

            DirectService.DirectWS DirectWS = new DirectService.DirectWS();
            DirectService.DirectResponse ResponseData = new DirectService.DirectResponse();
            DirectService.SendDirectParms Parms = new DirectService.SendDirectParms();

            Parms.PatientId = Convert.ToInt64(nudPatientId.Value);
            Parms.FacilityId = Convert.ToInt64(nudFacilityId.Value);
            Parms.VisitId = Convert.ToInt64(nudVisitId.Value);
            Parms.EmailAddress = txtEmail.Text;
            Parms.Allergies = true;
            Parms.ClinicalInstructions = true;
            Parms.DecisionAids = true;
            Parms.FutureAppointments = true;
            Parms.Immunizations = true;
            Parms.Procedures = true;
            Parms.VitalSigns = true;
            Parms.Labs = true;
            Parms.Medications = true;
            Parms.Social = true;
            Parms.Problems = true;
            Parms.Referrals = true;
            Parms.ScheduledTests = true;


            try
            {
                ResponseData = DirectWS.SendDirectEmail(Parms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (ResponseData.Valid == false)
                    MessageBox.Show(ResponseData.ErrorMessage);
                else

                    MessageBox.Show("Message sent");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Sending");
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            DirectService.DirectWS DirectWS = new DirectService.DirectWS();
            DirectService.DirectResponse ResponseData = new DirectService.DirectResponse();

            try
            {
                ResponseData = DirectWS.ValidateEmailAddress(txtEmail.Text, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (ResponseData.Valid == false)
                {
                    MessageBox.Show("Invalid Email Address");
                    EmailValid = false;
                }
                else
                    EmailValid = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }

        }
    }
}
