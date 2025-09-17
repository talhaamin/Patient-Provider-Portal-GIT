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
    public partial class frmSocialSelf : Form
    {
        public frmSocialSelf()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatientDocWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientSocialSelfData PatientSocialData = new PatientDocumentService.PatientSocialSelfData();
            PatientDocumentService.PatientDocParms Parms = new PatientDocumentService.PatientDocParms();


            Parms.PatientId = Convert.ToInt64(nudPatientId.Value);

            PatientSocialData = PatientDocWS.GetPatientSocialSelfData(Parms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            if (PatientSocialData.Valid)
            {
                txtBirthplace.Text = PatientSocialData.Birthplace;
                txtOccupation.Text = PatientSocialData.Occupation;
                chkRetired.Checked = PatientSocialData.Retired;
                nudSons.Value = PatientSocialData.ChildrenSon;
                nudDaughters.Value = PatientSocialData.ChildrenDaughter;
                txtCaffieneType.Text = PatientSocialData.CaffieneType;

            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatientDocWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientSocialSelfData PatientSocialData = new PatientDocumentService.PatientSocialSelfData();

            PatientSocialData.PatientId = Convert.ToInt64(nudPatientId.Value);
            PatientSocialData.Birthplace = txtBirthplace.Text;
            PatientSocialData.Occupation = txtOccupation.Text;
            PatientSocialData.Retired = chkRetired.Checked;
            PatientSocialData.ChildrenSon = Convert.ToInt16(nudSons.Value);
            PatientSocialData.ChildrenDaughter = Convert.ToInt16(nudDaughters.Value);
            PatientSocialData.CaffieneType = txtCaffieneType.Text;

            PatientDocWS.SavePatientSocialSelfData(PatientSocialData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }
    }
}
