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
    public partial class frmPractice : Form
    {
        public frmPractice()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.PracticeParms Practice = new ConfigService.PracticeParms();
            ConfigService.PracticeData PracticeData = new ConfigService.PracticeData();


            Practice.PracticeId = Convert.ToInt64(nudPracticeId.Value);

            PracticeData = ConfigWS.GetPracticeData(Practice, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (PracticeData.Valid)
            {
                nudOrganizationId.Value = PracticeData.OrganizationId;
                txtName.Text = PracticeData.PracticeName;
                txtAddress1.Text = PracticeData.Address1;
                txtCity.Text = PracticeData.City;
                txtState.Text = PracticeData.State;
                txtPostal.Text = PracticeData.PostalCode;
                txtCountry.Text = PracticeData.CountryCode;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.PracticeData PracticeData = new ConfigService.PracticeData();

            try
            {
                PracticeData.PracticeId = Convert.ToInt64(nudPracticeId.Value);
                PracticeData.OrganizationId = Convert.ToInt64(nudOrganizationId.Value);
                PracticeData.PracticeName = txtName.Text;
                PracticeData.Address1 = txtAddress1.Text;
                PracticeData.City = txtCity.Text;
                PracticeData.State = txtState.Text;
                PracticeData.PostalCode = txtPostal.Text;
                PracticeData.CountryCode = txtCountry.Text;


                PracticeData = ConfigWS.SavePracticeData(PracticeData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (PracticeData.Valid == false)
                    MessageBox.Show(PracticeData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
