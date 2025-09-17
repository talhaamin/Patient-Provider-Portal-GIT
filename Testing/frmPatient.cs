using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Testing
{
    public partial class frmPatient : Form
    {
        public frmPatient()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientParms PatParms = new PatientService.PatientParms();
            PatientService.PatientData PatData = new PatientService.PatientData();
            PatientService.PatientVisitParms VisitParms = new PatientService.PatientVisitParms();
            PatientService.PatientVisitTableData VisitData = new PatientService.PatientVisitTableData();
            PatientService.PatientFacilityTableData FacilityData = new PatientService.PatientFacilityTableData();
            PatientService.PatientOrganData OrganData = new PatientService.PatientOrganData();

            PatientService.PatientShareData ShareData = new PatientService.PatientShareData();
            PatientService.PatientTableData ParentData = new PatientService.PatientTableData();
            PatientService.PatientTableData ChildData = new PatientService.PatientTableData();
            PatientService.PatientWebSettingData WebData = new PatientService.PatientWebSettingData();
            PatientService.PatientImageData ImageData = new PatientService.PatientImageData();

            PatParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            PatData = PatientWS.GetPatientData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (PatData.Valid)
            {
                txtFirstName.Text = PatData.FirstName;
                txtLastName.Text = PatData.LastName;
                txtAddress1.Text = PatData.Address1;
                txtCity.Text = PatData.City;

                txtState.Text = PatData.State;
                txtPostal.Text = PatData.Zip;
                txtEmail.Text = PatData.EMail;

                dtpDate.Value = PatData.DOB;

                VisitParms.PatientId = PatParms.PatientId;
                VisitParms.Option = 0;
                VisitData = PatientWS.GetPatientVisitList(VisitParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                PatientService.PatientSummaryData PatSummaryData = new PatientService.PatientSummaryData();
                PatSummaryData = PatientWS.GetPatientSummaryData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (VisitData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = VisitData.dt;
                    dgVisits.DataSource = bs;
                }

                FacilityData = PatientWS.GetPatientFacilityList(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (FacilityData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = FacilityData.dt;
                    dgFacilities.DataSource = bs;
                }

                OrganData = PatientWS.GetPatientOrganData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (OrganData.Valid)
                {
                    chkHeart.Checked = OrganData.Heart;
                    chkLiver.Checked = OrganData.Liver;
                    chkPancreas.Checked = OrganData.Pancreas;
                    chkLungs.Checked = OrganData.Lungs;
                }
                else
                {
                    chkHeart.Checked = false;
                    chkLiver.Checked = false;
                    chkPancreas.Checked = false;
                    chkLungs.Checked = false;
                }
                ShareData = PatientWS.GetPatientShareData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (ShareData.Valid)
                {
                    chkDemographics.Checked = ShareData.Demographics;
                    chkAllergy.Checked = ShareData.Allergy;
                    chkFamily.Checked = ShareData.FamilyHistory;
                    chkLab.Checked = ShareData.LabResults;
                }
                WebData = PatientWS.GetPatientWebSettingData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (ShareData.Valid)
                {
                    //nudWAppointment.Value = WebData.AppointmentsWidgetPosn;
                    //nudWAllergy.Value = WebData.AllergyWidgetPosn;
                    //nudWFamily.Value = WebData.FamilyWidgetPosn;
                    //nudWLab.Value = WebData.LabTestWidgetPosn;
                    //nudWImmunization.Value = WebData.ImmunizationWidgetPosn;
                }
                ChildData = PatientWS.GetPatientAccountLinkList(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (ChildData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = ChildData.dt;
                    dgChildren.DataSource = bs;
                }

                ParentData = PatientWS.GetPatientAccountLinkParentList(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (ParentData.Valid)
                {
                    BindingSource bs = new BindingSource();
                    bs.DataSource = ParentData.dt;
                    dgParent.DataSource = bs;
                }

                ImageData = PatientWS.GetPatientImageData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (ImageData.Valid)
                {
                    MemoryStream ms = new MemoryStream(ImageData.Image);
        //            pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(ms);
                }

                PatientService.CareProviderData CPData = new PatientService.CareProviderData();

                CPData = PatientWS.GetCareProviderData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                txtCPPassword.Text = CPData.Password;



            }
            else
            {
                MessageBox.Show(PatData.ErrorMessage);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatinetWS = new PatientService.PatientWS();

            PatientService.PatientData PatData = new PatientService.PatientData();
            PatientService.PatientOrganData OrganData = new PatientService.PatientOrganData();
            PatientService.PatientWebSettingData WebData = new PatientService.PatientWebSettingData();

            try
            {
                PatData.PatientId = Convert.ToInt64(nudPatientId.Value);
                PatData.FirstName = txtFirstName.Text;
                PatData.LastName = txtLastName.Text;
                PatData.Address1 = txtAddress1.Text;
                PatData.City = txtCity.Text;
                PatData.State = txtState.Text;
                PatData.Zip = txtPostal.Text;
                PatData.EMail = txtEmail.Text;
                PatData.DOB = dtpDate.Value;




                PatinetWS.SavePatientData(PatData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                OrganData.PatientId = PatData.PatientId;
                OrganData.Heart = chkHeart.Checked;
                OrganData.Liver = chkLiver.Checked;
                OrganData.Pancreas = chkPancreas.Checked;
                OrganData.Lungs = chkLungs.Checked;

                PatinetWS.SavePatientOrganData(OrganData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);


                //WebData.PatientId = PatData.PatientId;
                //WebData.AppointmentsWidgetPosn = Convert.ToInt16(nudWAppointment.Value);
                //WebData.AllergyWidgetPosn = Convert.ToInt16(nudWAllergy.Value);
                //WebData.FamilyWidgetPosn = Convert.ToInt16(nudWFamily.Value);
                //WebData.LabTestWidgetPosn = Convert.ToInt16(nudWLab.Value);
                //WebData.ImmunizationWidgetPosn = Convert.ToInt16(nudWImmunization.Value);
                //WebData.InsuranceWidgetPosn = 0;
                //WebData.LabTestWidgetPosn = 0;
                //WebData.MedicationWidgetPosn = 0;
                //WebData.PastMedicalWidgetPosn = 0;
                //WebData.ProblemWidgetPosn = 0;
                //WebData.ProcedureWidgetPosn = 0;
                //WebData.SocialWidgetPosn = 0;
                //WebData.StatementWidgetPosn = 0;
                //WebData.SurgicalWidgetPosn = 0;
                //WebData.VisitWidgetPosn = 0;
                //WebData.VitalSignsWidgetPosn = 0;
                //WebData.EmailNotifyNewMessage = true;

                PatinetWS.SavePatientWebSettingData(WebData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void chkDemographics_CheckedChanged(object sender, EventArgs e)
        {
            PatientService.PatientWS PatinetWS = new PatientService.PatientWS();
            PatinetWS.PatientShareDemographics(Convert.ToInt64(nudPatientId.Value), chkDemographics.Checked, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void chkAllergy_CheckedChanged(object sender, EventArgs e)
        {
            PatientService.PatientWS PatinetWS = new PatientService.PatientWS();
            PatinetWS.PatientShareDemographics(Convert.ToInt64(nudPatientId.Value), chkAllergy.Checked, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void chkFamily_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkLab_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmdUploadPicture_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();


            if (result == DialogResult.OK) 
            {
                PatientService.PatientWS PatinetWS = new PatientService.PatientWS();
                PatientService.PatientImageData ImageData = new PatientService.PatientImageData();

                ImageData.PatientId = Convert.ToInt64(nudPatientId.Value);

                byte[] bytes = DiskToBytes(openFileDialog1.FileName);
                ImageData.Image = bytes;
                
                // Parse File Extension From File Name
                string FileType = openFileDialog1.SafeFileName;
                int posn = FileType.IndexOf(".");
                if (posn > 0)
                    ImageData.ImageFormat = FileType.Substring(posn + 1, FileType.Length - posn - 1);
                else
                    ImageData.ImageFormat = "";

                PatinetWS.SavePatientmageData(ImageData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
        }

        private static byte[] DiskToBytes(string filename)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                //EventLogger.WriteEntry("MX2", ex.Message, EventLogEntryType.Error, 742);
                return new byte[0];
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientAccountLinkData LinkData = new PatientService.PatientAccountLinkData();

            LinkData.PatientId = Convert.ToInt64(nudParent.Value);
            LinkData.PatientId_Linked = Convert.ToInt64(nudPatientId.Value);
            

            LinkData = PatientWS.SavePatientAccountLinkData(LinkData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void cmdCareProvider_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.CareProviderData CPData = new PatientService.CareProviderData();
            CPData.PatientId = Convert.ToInt64(nudPatientId.Value);
            CPData.PatientFullName = txtFirstName.Text + " " + txtLastName.Text;
            CPData.Password = txtCPPassword.Text;

            CPData = PatientWS.SaveCareProviderData(CPData,GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

        }

        private void frmPatient_Load(object sender, EventArgs e)
        {

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (dgParent.Rows.Count > 0)
            {
                Int64 ParentId  = Convert.ToInt64(dgParent.CurrentRow.Cells["PatientId"].Value);

                PatientService.PatientWS PatientWS = new PatientService.PatientWS();
                PatientService.PatientAccountLinkData LinkData = new PatientService.PatientAccountLinkData();

                LinkData.PatientId = ParentId;
                LinkData.PatientId_Linked = Convert.ToInt64(nudPatientId.Value);


                LinkData = PatientWS.DeletePatientAccountLinkData(LinkData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientSearchParms PatParms = new PatientService.PatientSearchParms();
            PatientService.PatientTableData PatData = new PatientService.PatientTableData();


            PatParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            PatParms.FirstName = txtFirstName.Text;
            PatParms.LastName = txtLastName.Text;
            PatParms.Address1 = txtAddress1.Text;
            PatParms.City = txtCity.Text;
            PatParms.State = txtState.Text;
            PatParms.CountryCode = "USA";
            PatParms.Zip = txtPostal.Text;
            PatParms.HomePhone = "";
            PatParms.EMail = txtEmail.Text;
            //PatParms.EMRSystemId = 4;

            PatData = PatientWS.PatientSearch(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (PatData.Valid)
            {
                DataTable dt = PatData.dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientParms PatParms = new PatientService.PatientParms();
            PatientService.PatientAdmin PatData = new PatientService.PatientAdmin();

            PatientService.PatientVisitParms VisitParms = new PatientService.PatientVisitParms();
            PatientService.PatientVisitData VisitData = new PatientService.PatientVisitData();

            PatParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            VisitParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            //PatData = PatientWS.GetPatientAdminData(PatParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            VisitData = PatientWS.GetLatestPatientVisit(VisitParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            AuthenticationService.AuthenticationResponse Response = new AuthenticationService.AuthenticationResponse();


            Response = AuthenticationWS.ChangePatientPasswordAdmin(GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId, Convert.ToInt64(nudPatientId.Value));
        }

        private void cmdEnable_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientResp PatResp = new PatientService.PatientResp();

            PatResp = PatientWS.ActivatePatient(Convert.ToInt64(nudPatientId.Value), GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void cmdDisable_Click(object sender, EventArgs e)
        {
            PatientService.PatientWS PatientWS = new PatientService.PatientWS();
            PatientService.PatientResp PatResp = new PatientService.PatientResp();

            PatResp = PatientWS.DeactivatePatient(Convert.ToInt64(nudPatientId.Value), GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
