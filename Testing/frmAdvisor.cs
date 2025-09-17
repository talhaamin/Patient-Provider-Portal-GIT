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
    public partial class frmAdvisor : Form
    {
        public frmAdvisor()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientDocParms DocParms = new PatientDocumentService.PatientDocParms();
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();

            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            DocTableData = PatinetDocumentWS.GetPatientAdvisorData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            BindingSource bs = new BindingSource();
            bs.DataSource = DocTableData.dt;
            dgCodes.DataSource = bs;

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();


            PatientDocumentService.PatientAdvisorData AdvisorData = new PatientDocumentService.PatientAdvisorData();
            try
            {
                AdvisorData.PatientId = Convert.ToInt64(nudPatientId.Value);
                AdvisorData.PatientAdvisorId = Convert.ToInt64(nudAdvisorId.Text);

                PatinetDocumentWS.DeletePatientAdvisorData(AdvisorData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            PatientDocumentService.PatientAdvisorData AdvisorData = new PatientDocumentService.PatientAdvisorData();
            try
            {
                AdvisorData.PatientId = Convert.ToInt64(nudPatientId.Value);
                AdvisorData.PatientAdvisorId = Convert.ToInt64(nudAdvisorId.Value);
                AdvisorData.Name = txtName.Text;
                AdvisorData.ContactAgent = txtAdvisor.Text;
                AdvisorData.Address1 = txtAddress1.Text;
                AdvisorData.City = txtCity.Text;
                AdvisorData.State = txtState.Text;
                AdvisorData.PostalCode = txtPostal.Text;
                AdvisorData.WorkPhone = txtHomePhone.Text;
                AdvisorData.AdvisorId = 1;

                AdvisorData = PatinetDocumentWS.SavePatientAdvisorData(AdvisorData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (AdvisorData.Valid == false)
                    MessageBox.Show(AdvisorData.ErrorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void dgCodes_Click(object sender, EventArgs e)
        {
            if (dgCodes.Rows.Count > 0)
            {
                nudAdvisorId.Value = Convert.ToInt64(dgCodes.CurrentRow.Cells["PatientAdvisorId"].Value);
                txtName.Text = dgCodes.CurrentRow.Cells["Name"].Value.ToString();
                txtAdvisor.Text = dgCodes.CurrentRow.Cells["ContactAgent"].Value.ToString();
                txtAddress1.Text = dgCodes.CurrentRow.Cells["Address1"].Value.ToString();
                txtCity.Text = dgCodes.CurrentRow.Cells["City"].Value.ToString();
                txtState.Text = dgCodes.CurrentRow.Cells["State"].Value.ToString();
                txtPostal.Text = dgCodes.CurrentRow.Cells["PostalCode"].Value.ToString();
                txtHomePhone.Text = dgCodes.CurrentRow.Cells["WorkPhone"].Value.ToString();


            }
        }
    }
}
