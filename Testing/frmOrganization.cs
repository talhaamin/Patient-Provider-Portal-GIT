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
    public partial class frmOrganization : Form
    {
        public frmOrganization()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.OrganizationParms OrgParms = new ConfigService.OrganizationParms();
            ConfigService.OrganizationData OrganizationData = new ConfigService.OrganizationData();


            OrgParms.OrganizationId = Convert.ToInt64(nudOrganizationId.Value);

            OrganizationData = ConfigWS.GetOrganizationData(OrgParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (OrganizationData.Valid)
            {
                txtName.Text = OrganizationData.OrganizationName;
                txtAddress1.Text = OrganizationData.Address1;
                txtCity.Text = OrganizationData.City;
                txtState.Text = OrganizationData.State;
                txtPostal.Text = OrganizationData.PostalCode;
                txtCountry.Text = OrganizationData.CountryCode;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.OrganizationData OrganizationData = new ConfigService.OrganizationData();

            try
            {
                OrganizationData.OrganizationId = Convert.ToInt64(nudOrganizationId.Value);
                OrganizationData.OrganizationName = txtName.Text;
                OrganizationData.Address1 = txtAddress1.Text;
                OrganizationData.City = txtCity.Text;
                OrganizationData.State = txtState.Text;
                OrganizationData.PostalCode = txtPostal.Text;
                OrganizationData.CountryCode = txtCountry.Text;


                OrganizationData = ConfigWS.SaveOrganizationData(OrganizationData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                if (OrganizationData.Valid == false)
                    MessageBox.Show(OrganizationData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

    }
}
