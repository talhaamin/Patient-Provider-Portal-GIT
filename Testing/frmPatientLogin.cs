using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class frmPatientLogin : Form
    {

        public frmPatientLogin()
        {
            InitializeComponent();
        }

        private void frmPatientLogin_Load(object sender, EventArgs e)
        {


        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            PatientService.PatientWS PatinetWS = new PatientService.PatientWS();

            GlobalVar.FacilityId = 0;
            
            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();
            PatientService.PatientRepData RepData = new PatientService.PatientRepData();
            PatientService.PatientParms PatientParms = new PatientService.PatientParms();

            try
            {
                ResponseData = AuthenticationWS.AuthenticateInterface(GlobalVar.FacilityId, txtLogin.Text, txtPassword.Text);

                if (ResponseData.Valid)
                {
                    GlobalVar.UserId = ResponseData.UserId;
                    GlobalVar.Token = ResponseData.Token;
                    int LoginType = ResponseData.LoginType;

                    txtLogin.Enabled = false;
                    txtPassword.Enabled = false;
                    cmdSave.Enabled = false;

                    txtOld.Enabled = true;
                    txtNew.Enabled = true;
                    cmdChangePassword.Enabled = true;

                    cboQuestions.Enabled = true;
                    txtAnswer.Enabled = true;
                    cmdSecurity.Enabled = true;

                    gbRepresentative.Enabled = true;

                    // Load Dropdown

                    ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
                    ConfigService.CodeTableData CodeData = new ConfigService.CodeTableData();
                    CodeData = ConfigWS.GetSecurityQuestionCodes(GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);


                    if (CodeData.Valid)
                    {
                        BindingSource bs = new BindingSource();
                        bs.DataSource = CodeData.dt;
                        cboQuestions.DataSource = bs;
                        cboQuestions.DisplayMember = "Value";
                        cboQuestions.ValueMember = "SecurityQuestionId";
                    }


                    if (LoginType == 5)
                    {
                        // Get Patient Rep Data
                        PatientParms.PatientId = Convert.ToInt64(txtLogin.Text);
                        RepData = PatinetWS.GetPatientRepData(PatientParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                        if (RepData.Valid)
                        {
                            txtFirstName.Text = RepData.FirstName;
                            txtLastName.Text = RepData.LastName;
                            txtEmail.Text = RepData.EMail;
                            chkDemographics.Checked = RepData.Demographics;
                            chkAllergy.Checked = RepData.Allergy;
                            chkFamily.Checked = RepData.FamilyHistory;
                            chkLab.Checked = RepData.LabResults;
                            chkMedical.Checked = RepData.MedicalHistory;
                            chkMedication.Checked = RepData.Medication;
                            chkProblem.Checked = RepData.Problem;
                            chkProcedure.Checked = RepData.Procedure;
                            chkSocial.Checked = RepData.SocialHistory;
                            chkSurgical.Checked = RepData.SurgicalHistory;
                            chkVital.Checked = RepData.VitalSigns;
                            chkImmunization.Checked = RepData.Immunization;
                            chkOrgan.Checked = RepData.Organ;
                            chkClincal.Checked = RepData.ClinicalDoc;
                            chkInsurance.Checked = RepData.Insurance;
                            chkEmergency.Checked = RepData.EmergencyContact;
                            chkAppointment.Checked = RepData.Appointment;
                            chkVisit.Checked = RepData.Visit;
                            chkUpload.Checked = RepData.UploadDocs;
                            chkPlanOfCare.Checked = RepData.PlanOfCare;
                            chkMessaging.Checked = RepData.Messaging;
                            chkDownload.Checked = RepData.DownloadTransmit;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }


        }

        private void cmdChangePassword_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            int LoginType = 5;

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();

            // Need to have code to check that password meets minimum requirements.

            try
            {
                ResponseData = AuthenticationWS.ChangePassword(GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId, LoginType, txtOld.Text, txtNew.Text);

                if (ResponseData.Valid)
                {
                    MessageBox.Show("Password Changed");
                }
                else
                {
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdSecurity_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            int LoginType = 5;

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();

            // Need to have code to check that password meets minimum requirements.

            //try
            //{
            //    ResponseData = AuthenticationWS.ChangeSecurityQuestion(GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId, LoginType, Convert.ToInt32(cboQuestions.SelectedValue), txtAnswer.Text,0,"",);

            //    if (ResponseData.Valid)
            //    {
            //        MessageBox.Show("Security Question Changed");
            //    }
            //    else
            //    {
            //        MessageBox.Show(ResponseData.ErrorMessage);
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Error Saving");
            //}
        }

        private void cmdSecurityGet_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            GlobalVar.FacilityId = 0;
            int LoginType = 5;

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();


            try
            {
                ResponseData = AuthenticationWS.GetSecurityQuestion(GlobalVar.FacilityId, txtLogin.Text, LoginType, txtEmail.Text);

                if (ResponseData.Valid)
                {
                    txtSecurityQuestion.Text = ResponseData.SecurityQuestion;
                    txtSecurityAnswer.Enabled = true;
                    cmdResetPassword.Enabled = true;
                }
                else
                {
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch
            {
                MessageBox.Show("Error Reading record");
            }

        }

        private void cmdResetPassword_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            GlobalVar.FacilityId = 0;
            int LoginType = 5;

            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();


            try
            {
                ResponseData = AuthenticationWS.ResetPassword(GlobalVar.FacilityId, txtLogin.Text, LoginType, txtSecurityAnswer.Text,"");

                if (ResponseData.Valid)
                {
                    MessageBox.Show("Your password has been reset.  Your new password has been sent to your email.");
                }
                else
                {
                    MessageBox.Show(ResponseData.ErrorMessage);
                }
            }
            catch
            {
                MessageBox.Show("Error Reading record");
            }
        }

        private void cmdCareProvider_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatinetWS = new PatientService.PatientWS();

            GlobalVar.FacilityId = 0;
            PatientService.PatientRepData RepData = new PatientService.PatientRepData();

            try
            {
                RepData.PatientId = Convert.ToInt64(txtLogin.Text);
                RepData.FirstName = txtFirstName.Text;
                RepData.LastName = txtLastName.Text;
                RepData.EMail = txtEmail.Text;
                RepData.Demographics = chkDemographics.Checked;
                RepData.Allergy = chkAllergy.Checked;
                RepData.FamilyHistory = chkFamily.Checked;
                RepData.LabResults = chkLab.Checked;
                RepData.MedicalHistory = chkMedical.Checked;
                RepData.Medication = chkMedication.Checked;
                RepData.Problem = chkProblem.Checked;
                RepData.Procedure = chkProcedure.Checked;
                RepData.SocialHistory = chkSocial.Checked;
                RepData.SurgicalHistory = chkSurgical.Checked;
                RepData.VitalSigns = chkVital.Checked;
                RepData.Immunization = chkImmunization.Checked;
                RepData.Organ = chkOrgan.Checked;
                RepData.ClinicalDoc = chkClincal.Checked;
                RepData.Insurance = chkInsurance.Checked;
                RepData.EmergencyContact = chkEmergency.Checked;
                RepData.Appointment = chkAppointment.Checked;
                RepData.Visit = chkVisit.Checked;
                RepData.UploadDocs = chkUpload.Checked;
                RepData.PlanOfCare = chkPlanOfCare.Checked;
                RepData.Messaging = chkMessaging.Checked;
                RepData.DownloadTransmit = chkDownload.Checked;

                RepData = PatinetWS.SavePatientRepData(RepData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (RepData.Valid == false)
                    MessageBox.Show(RepData.ErrorMessage);

            }
            catch
            {
                MessageBox.Show("Error Saving record");
            }
        }


    }
}
