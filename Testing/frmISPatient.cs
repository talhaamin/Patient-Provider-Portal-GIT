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
    public partial class frmISPatient : Form
    {
        public frmISPatient()
        {
            InitializeComponent();
        }


        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();

            ISPatientService.PatientData PatData = new ISPatientService.PatientData();
            ISPatientService.PatientResponse ResponseData = new ISPatientService.PatientResponse();
            try
            {
                PatData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                PatData.FirstName = txtFirstName.Text;
                PatData.LastName = txtLastName.Text;
                PatData.Address1 = txtAddress1.Text;
                PatData.City = txtCity.Text;
                PatData.State = txtState.Text;
                PatData.Postal = txtPostal.Text;
                PatData.EMail = txtEmail.Text;
                PatData.DOB = Convert.ToDateTime(dtpDate.Value.ToShortDateString());
                PatData.SSN = txtSSN.Text;
                PatData.MedicareId = txtMedicareId.Text;
                PatData.MedicaidState = txtMedicaidState.Text;
                PatData.MedicaidId = txtMedicaidId.Text;
                PatData.AltId1_Type = txtAltId1Type.Text;
                PatData.AltId1_Key = txtAltId1Key.Text;
                PatData.AltId2_Type = txtAltId2Type.Text;
                PatData.AltId2_Key = txtAltId2Key.Text;
                PatData.AltId3_Type = txtAltId3Type.Text;
                PatData.AltId3_Key = txtAltId3Key.Text;

                ResponseData = PatinetWS.CreateAMRPatient(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, PatData);

                if (ResponseData.Valid)
                {
                    if (ResponseData.AMRPatientId > 0)
                    {
                        nudPatientId.Value = ResponseData.AMRPatientId;
                        //txtPass.Text = ResponseData.Password;
                    }
                }
                else
                    MessageBox.Show(ResponseData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();

            ISPatientService.PatientData PatData = new ISPatientService.PatientData();
            ISPatientService.PatientResponse ResponseData = new ISPatientService.PatientResponse();
            try
            {
                PatData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                PatData.FirstName = "Bob";
                PatData.LastName = "Abbot";
                PatData.Address1 = "200 Hillcrest Ave";
                PatData.City = "Wood Ridge";
                PatData.State = "KY";
                PatData.Postal = "07075";
                PatData.EMail = "sfarkas@electroniccharge.com";
                PatData.DOB = Convert.ToDateTime("02/14/1995");
                PatData.SSN = "209-38-9876";
                PatData.HomePhone = "201-964-9997";
                PatData.WorkPhone = "201-280-9999";
                PatData.MobilePhone = "201-493-9998";
                PatData.PreferredLanguageId = 0;
                PatData.GenderId = 1;
                PatData.RaceId = 0;
                PatData.EthnicityId = 0;
                PatData.MaritalStatusId = 2;
                PatData.SmokingStatusId = 2;


                ResponseData = PatinetWS.CreateAMRPatient(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, PatData);

                if (ResponseData.Valid)
                {
                    if (ResponseData.AMRPatientId > 0)
                    {
                        nudPatientId.Value = ResponseData.AMRPatientId;
                        //txtPass.Text = ResponseData.Password;
                    }
                }
                else
                    MessageBox.Show(ResponseData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
