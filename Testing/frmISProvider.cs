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
    public partial class frmISProvider : Form
    {
        public frmISProvider()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISProviderService.ProviderWS PatinetWS = new ISProviderService.ProviderWS();

            ISProviderService.ProviderData PatData = new ISProviderService.ProviderData();
            ISProviderService.ProviderResponse ResponseData = new ISProviderService.ProviderResponse();
            try
            {
                PatData.ProviderId = txtProviderId.Text;
                PatData.FirstName = txtFirstName.Text;
                PatData.LastName = txtLastName.Text;
                PatData.License = txtLicense.Text;
                PatData.Phone = txtPhone.Text;
                PatData.Email = txtEmail.Text;

                ResponseData = PatinetWS.CreateAMRProvider(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, PatData);

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
            ISProviderService.ProviderWS PatinetWS = new ISProviderService.ProviderWS();

            ISProviderService.ProviderData providerData = new ISProviderService.ProviderData();
            ISProviderService.ProviderResponse ResponseData = new ISProviderService.ProviderResponse();
            try
            {
                providerData.ProviderId = "210";
                providerData.Title = "Dr.";
                providerData.FirstName = "Daniel";
                providerData.MiddleName = "";
                providerData.LastName = "Gordon";
                providerData.DEA = "AB4321";
                providerData.License = "license";
                providerData.Phone = "2125551111";
                providerData.Email = "dan.gordo+amr-dr@gmail.com";

                ResponseData = PatinetWS.CreateAMRProvider(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, providerData);

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
    }
}
