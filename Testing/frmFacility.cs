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
    public partial class frmFacility : Form
    {
        public frmFacility()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.FacilityParms FacilityParms = new ConfigService.FacilityParms();
            ConfigService.FacilityData FacilityData = new ConfigService.FacilityData();


            FacilityParms.FacilityId = Convert.ToInt64(nudFacilityId.Value);

            FacilityData = ConfigWS.GetFacilityData(FacilityParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (FacilityData.Valid)
            {
                nudPracticeId.Value = FacilityData.PracticeId;
                txtName.Text = FacilityData.FacilityName;
                txtAddress1.Text = FacilityData.Address1;
                txtCity.Text = FacilityData.City;
                txtState.Text = FacilityData.State;
                txtPostal.Text = FacilityData.PostalCode;
                txtCountry.Text = FacilityData.CountryCode;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.FacilityData FacilityData = new ConfigService.FacilityData();

            try
            {
                FacilityData.FacilityId = Convert.ToInt64(nudFacilityId.Value);
                FacilityData.PracticeId = Convert.ToInt64(nudPracticeId.Value);
                FacilityData.FacilityName = txtName.Text;
                FacilityData.Address1 = txtAddress1.Text;
                FacilityData.City = txtCity.Text;
                FacilityData.State = txtState.Text;
                FacilityData.PostalCode = txtPostal.Text;
                FacilityData.CountryCode = txtCountry.Text;


                FacilityData = ConfigWS.SaveFacilityData(FacilityData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (FacilityData.Valid == false)
                    MessageBox.Show(FacilityData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
