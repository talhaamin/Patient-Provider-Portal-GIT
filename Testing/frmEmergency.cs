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
    public partial class frmEmergency : Form
    {
        public frmEmergency()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientDocParms DocParms = new PatientDocumentService.PatientDocParms();
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();

            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            DocTableData = PatinetDocumentWS.GetPatientEmergencyData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            BindingSource bs = new BindingSource();
            bs.DataSource = DocTableData.dt;
            dgCodes.DataSource = bs;

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();


            PatientDocumentService.PatientEmergencyData EmergencyData = new PatientDocumentService.PatientEmergencyData();
            try
            {
                EmergencyData.PatientId = Convert.ToInt64(nudPatientId.Value);
                EmergencyData.PatientEmergencyId = Convert.ToInt64(nudEmergencyId.Text);
                        
                PatinetDocumentWS.DeletePatientEmergencyData(EmergencyData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            PatientDocumentService.PatientEmergencyData EmergencyData = new PatientDocumentService.PatientEmergencyData();
            try
            {
                EmergencyData.PatientId = Convert.ToInt64(nudPatientId.Value);
                EmergencyData.PatientEmergencyId = Convert.ToInt64(nudEmergencyId.Value);
                EmergencyData.Name = txtName.Text;
                EmergencyData.Address1 = txtAddress1.Text;
                EmergencyData.City = txtCity.Text;
                EmergencyData.State = txtState.Text;
                EmergencyData.PostalCode = txtPostal.Text;
                EmergencyData.HomePhone = txtHomePhone.Text;
                EmergencyData.RelationshipId = 1;
                EmergencyData.IsPrimary = chkPrimary.Checked;

                EmergencyData = PatinetDocumentWS.SavePatientEmergencyData(EmergencyData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (EmergencyData.Valid == false)
                    MessageBox.Show(EmergencyData.ErrorMessage);
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
                nudEmergencyId.Value = Convert.ToInt64(dgCodes.CurrentRow.Cells["PatientEmergencyId"].Value);
                txtName.Text = dgCodes.CurrentRow.Cells["Name"].Value.ToString();
                txtAddress1.Text = dgCodes.CurrentRow.Cells["Address1"].Value.ToString();
                txtCity.Text = dgCodes.CurrentRow.Cells["City"].Value.ToString();
                txtState.Text = dgCodes.CurrentRow.Cells["State"].Value.ToString();
                txtPostal.Text = dgCodes.CurrentRow.Cells["PostalCode"].Value.ToString();
                txtHomePhone.Text = dgCodes.CurrentRow.Cells["HomePhone"].Value.ToString();
                chkPrimary.Checked = Convert.ToBoolean(dgCodes.CurrentRow.Cells["IsPrimary"].Value);

            }
        }
    }
}
