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
    public partial class frmISIntegration : Form
    {
        public frmISIntegration()
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
                txtOName.Text = OrganizationData.OrganizationName;
                txtOAddress1.Text = OrganizationData.Address1;
                txtOCity.Text = OrganizationData.City;
                txtOState.Text = OrganizationData.State;
                txtOPostal.Text = OrganizationData.PostalCode;
                txtOCountry.Text = OrganizationData.CountryCode;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.PracticeParms Practice = new ConfigService.PracticeParms();
            ConfigService.PracticeData PracticeData = new ConfigService.PracticeData();


            Practice.PracticeId = Convert.ToInt64(nudPracticeId.Value);

            PracticeData = ConfigWS.GetPracticeData(Practice, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (PracticeData.Valid)
            {
                nudOrganizationId.Value = PracticeData.OrganizationId;
                txtPName.Text = PracticeData.PracticeName;
                txtPAddress1.Text = PracticeData.Address1;
                txtPCity.Text = PracticeData.City;
                txtPState.Text = PracticeData.State;
                txtPPostal.Text = PracticeData.PostalCode;
                txtPCountry.Text = PracticeData.CountryCode;
                cmdRead_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigService.ConfigWS ConfigWS = new ConfigService.ConfigWS();
            ConfigService.FacilityParms FacilityParms = new ConfigService.FacilityParms();
            ConfigService.FacilityData FacilityData = new ConfigService.FacilityData();

            AuthenticationService.AuthenticationWS AuthenticationWS = new AuthenticationService.AuthenticationWS();
            AuthenticationService.UserData UserData = new AuthenticationService.UserData();

            FacilityParms.FacilityId = Convert.ToInt64(nudFacilityId.Value);

            FacilityData = ConfigWS.GetFacilityData(FacilityParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (FacilityData.Valid)
            {
                nudPracticeId.Value = FacilityData.PracticeId;
                txtFName.Text = FacilityData.FacilityName;
                txtFAddress1.Text = FacilityData.Address1;
                txtFCity.Text = FacilityData.City;
                txtFState.Text = FacilityData.State;
                txtFPostal.Text = FacilityData.PostalCode;
                txtFCountry.Text = FacilityData.CountryCode;
                txtFPhone.Text = FacilityData.Phone;
                txtFFax.Text = FacilityData.Fax;
                txtBAddress1.Text = FacilityData.BillAddress1;
                txtBCity.Text = FacilityData.BillCity;
                txtBState.Text = FacilityData.BillState;
                txtBPostal.Text = FacilityData.BillPostalCode;
                txtBCountry.Text = FacilityData.BillCountryCode;
                txtBPhone.Text = FacilityData.BillPhone;
                txtBFax.Text =  FacilityData.BillFax;

                chkGMAvailable.Checked = FacilityData.GeneralMessageAvailable;
                cboGMNotify.SelectedIndex = FacilityData.GeneralMessageNotify;
                txtGMEmail.Text = FacilityData.GeneralMessageEmail;
                chkAMAvailable.Checked = FacilityData.AppointmentMessageAvailable;
                cboAMNotify.SelectedIndex = FacilityData.AppointmentMessageNotify;
                txtAMEmail.Text = FacilityData.AppointmentMessageEmail;
                chkMMAvailable.Checked = FacilityData.MedicationMessageAvailable;
                cboMMNotify.SelectedIndex = FacilityData.MedicationMessageNotify;
                txtMMEmail.Text = FacilityData.MedicationMessageEmail;
                chkRMAvailable.Checked = FacilityData.ReferalMessageAvailable;
                cboRMNotify.SelectedIndex = FacilityData.ReferralMessageNotify;
                txtRMEmail.Text = FacilityData.ReferralMessageEmail;
                chkPremium.Checked = FacilityData.PremiumAvailable;
                cboBill.SelectedIndex = FacilityData.OnlineBillPayment;

                UserData = AuthenticationWS.GetUserTypeLink(GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId, 2, Convert.ToInt64(nudFacilityId.Value));
                if (UserData.Valid)
                    txtContactEmail.Text = UserData.Email;

                // Load Practice Data
                button2_Click(sender, e);
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISConfigService.ConfigWS ConfigWS = new ISConfigService.ConfigWS();
            ISConfigService.FacilitySetupData FacilityData = new ISConfigService.FacilitySetupData();
            ISConfigService.FacilitySetupResp Response = new ISConfigService.FacilitySetupResp();

            FacilityData.OrganizationId = Convert.ToInt64(nudOrganizationId.Value);
            FacilityData.OrganizationName = txtOName.Text;
            FacilityData.OrganizationAddress1 = txtOAddress1.Text;
            FacilityData.OrganizationAddress2 = "";
            FacilityData.OrganizationAddress3 = "";
            FacilityData.OrganizationCity = txtOCity.Text;
            FacilityData.OrganizationState = txtOState.Text;
            FacilityData.OrganizationPostalCode = txtOPostal.Text;
            FacilityData.OrganizationCountryCode = txtOCountry.Text;

            FacilityData.PracticeId = Convert.ToInt64(nudPracticeId.Value);
            FacilityData.PracticeName = txtPName.Text;
            FacilityData.PracticeAddress1 = txtPAddress1.Text;
            FacilityData.PracticeAddress2 = "";
            FacilityData.PracticeAddress3 = "";
            FacilityData.PracticeCity = txtPCity.Text;
            FacilityData.PracticeState = txtPState.Text;
            FacilityData.PracticePostalCode = txtPPostal.Text;
            FacilityData.PracticeCountryCode = txtPCountry.Text;

            FacilityData.FacilityId = Convert.ToInt64(nudFacilityId.Value);
            FacilityData.EMRSystemId = 0;
            FacilityData.FacilityTypeId = 1;
            FacilityData.FacilityName = txtFName.Text;
            FacilityData.FacilityAddress1 = txtFAddress1.Text;
            FacilityData.FacilityAddress2 = "";
            FacilityData.FacilityAddress3 = "";
            FacilityData.FacilityCity = txtFCity.Text;
            FacilityData.FacilityState = txtFState.Text;
            FacilityData.FacilityCountryCode = txtFCountry.Text;
            FacilityData.FacilityPostalCode = txtFPostal.Text;
            FacilityData.FacilityPhone = txtFPhone.Text;
            FacilityData.FacilityFax = txtFFax.Text;
            FacilityData.FacilityBillAddress1 = txtBAddress1.Text;
            FacilityData.FacilityBillAddress2 = "";
            FacilityData.FacilityBillAddress3 = "";
            FacilityData.FacilityBillCity = txtBCity.Text;
            FacilityData.FacilityBillState = txtBState.Text;
            FacilityData.FacilityBillPostalCode = txtBPostal.Text;
            FacilityData.FacilityBillCountryCode = txtBCountry.Text;
            FacilityData.FacilityBillPhone = txtBPhone.Text;
            FacilityData.FacilityBillFax = txtBFax.Text;

            FacilityData.GeneralMessageAvailable = chkGMAvailable.Checked;
            FacilityData.GeneralMessageNotify = Convert.ToInt16(cboGMNotify.SelectedIndex);
            FacilityData.GeneralMessageEmail = txtGMEmail.Text;
            FacilityData.AppointmentMessageAvailable = chkAMAvailable.Checked;
            FacilityData.AppointmentMessageNotify = Convert.ToInt16(cboAMNotify.SelectedIndex);
            FacilityData.AppointmentMessageEmail = txtAMEmail.Text;
            FacilityData.MedicationMessageAvailable = chkMMAvailable.Checked;
            FacilityData.MedicationMessageNotify = Convert.ToInt16(cboMMNotify.SelectedIndex);
            FacilityData.MedicationMessageEmail = txtMMEmail.Text;
            FacilityData.ReferalMessageAvailable = chkRMAvailable.Checked;
            FacilityData.ReferralMessageNotify = Convert.ToInt16(cboRMNotify.SelectedIndex);
            FacilityData.ReferralMessageEmail = txtRMEmail.Text;
            FacilityData.PremiumAvailable = chkPremium.Checked;
            FacilityData.OnlineBillPayment = Convert.ToInt16(cboBill.SelectedIndex);
            FacilityData.ContactEmail = txtContactEmail.Text;

            Response = ConfigWS.SavePracticeData(FacilityData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            if (!Response.Valid)
                MessageBox.Show(Response.ErrorMessage);
            else
                MessageBox.Show("FacilityId: " + Response.FacilityId + ", UserId: " + Response.UserId + ", UserLogin: " + Response.Logon + ", Password: " + Response.Password);

        }
    }
}
