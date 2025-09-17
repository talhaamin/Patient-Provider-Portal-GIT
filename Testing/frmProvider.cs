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
    public partial class frmProvider : Form
    {
        public frmProvider()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            ProviderService.ProviderWS ProviderWS = new ProviderService.ProviderWS();
            ProviderService.ProviderParms ProvParms = new ProviderService.ProviderParms();
            ProviderService.ProviderData ProvData = new ProviderService.ProviderData();


            ProvParms.ProviderId = Convert.ToInt64(nudProviderId.Value);

            ProvData = ProviderWS.GetProviderData(ProvParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (ProvData.Valid)
            {
                txtFirstName.Text = ProvData.FirstName;
                txtLastName.Text = ProvData.LastName;
                txtLicense.Text = ProvData.License;
                txtPhone.Text = ProvData.Phone;
                txtEmail.Text = ProvData.Email;


            }
            else
            {
                MessageBox.Show(ProvData.ErrorMessage);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ProviderService.ProviderWS PatinetWS = new ProviderService.ProviderWS();

            ProviderService.ProviderData ProvData = new ProviderService.ProviderData();
            ProviderService.ProviderResponse ResponseData = new ProviderService.ProviderResponse();
            try
            {
                ProvData.ProviderId = Convert.ToInt64(nudProviderId.Value);
                if (ProvData.ProviderId == 0)
                    ProvData.PracticeId = 1;
                ProvData.FirstName = txtFirstName.Text;
                ProvData.LastName = txtLastName.Text;
                ProvData.License = txtLicense.Text;
                ProvData.Phone = txtPhone.Text;
                ProvData.Email = txtEmail.Text;

                ResponseData = PatinetWS.SaveProviderData(ProvData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (ResponseData.Valid)
                    MessageBox.Show("Provider Saved");
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
            ProviderService.ProviderWS ProviderWS = new ProviderService.ProviderWS();

            ProviderService.ProviderResponse ResponseData = new ProviderService.ProviderResponse();
            try
            {
                ResponseData = ProviderWS.ChangeProviderEmail(Convert.ToInt64(nudProviderId.Value), txtEmail.Text, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (ResponseData.Valid)
                    MessageBox.Show("Email Saved");
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
