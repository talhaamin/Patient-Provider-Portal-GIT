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
    public partial class frmDoctor : Form
    {
        public frmDoctor()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientDocParms DocParms = new PatientDocumentService.PatientDocParms();
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();

            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);

            DocTableData = PatinetDocumentWS.GetPatientDoctorData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            BindingSource bs = new BindingSource();
            bs.DataSource = DocTableData.dt;
            dgCodes.DataSource = bs;

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();


            PatientDocumentService.PatientDoctorData DoctorData = new PatientDocumentService.PatientDoctorData();
            try
            {
                DoctorData.PatientId = Convert.ToInt64(nudPatientId.Value);
                DoctorData.PatientDoctorId = Convert.ToInt64(nudDoctorId.Text);

                PatinetDocumentWS.DeletePatientDoctorData(DoctorData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            PatientDocumentService.PatientDoctorData DoctorData = new PatientDocumentService.PatientDoctorData();
            try
            {
                DoctorData.PatientId = Convert.ToInt64(nudPatientId.Value);
                DoctorData.PatientDoctorId = Convert.ToInt64(nudDoctorId.Value);
                DoctorData.Name = txtName.Text;
                DoctorData.Address1 = txtAddress1.Text;
                DoctorData.City = txtCity.Text;
                DoctorData.State = txtState.Text;
                DoctorData.PostalCode = txtPostal.Text;
                DoctorData.WorkPhone = txtHomePhone.Text;
                DoctorData.DoctorTypeId = 1;

                DoctorData = PatinetDocumentWS.SavePatientDoctorData(DoctorData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                if (DoctorData.Valid == false)
                    MessageBox.Show(DoctorData.ErrorMessage);
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
                nudDoctorId.Value = Convert.ToInt64(dgCodes.CurrentRow.Cells["PatientDoctorId"].Value);
                txtName.Text = dgCodes.CurrentRow.Cells["Name"].Value.ToString();
                txtAddress1.Text = dgCodes.CurrentRow.Cells["Address1"].Value.ToString();
                txtCity.Text = dgCodes.CurrentRow.Cells["City"].Value.ToString();
                txtState.Text = dgCodes.CurrentRow.Cells["State"].Value.ToString();
                txtPostal.Text = dgCodes.CurrentRow.Cells["PostalCode"].Value.ToString();
                txtHomePhone.Text = dgCodes.CurrentRow.Cells["WorkPhone"].Value.ToString();


            }
        }

        private void frmDoctor_Load(object sender, EventArgs e)
        {
            // Load Dropdown Values
        }
    }
}
