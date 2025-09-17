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
    public partial class frmMedicalPortfolio : Form
    {
        public frmMedicalPortfolio()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            
            LoadGrids();
        }
        private void LoadGrids()
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            PatientDocumentService.MedicalPortfolioData PortfolioData = new PatientDocumentService.MedicalPortfolioData();
            PatientDocumentService.PatientDocParms DocParms = new PatientDocumentService.PatientDocParms();
            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            DocParms.Option = 0;
            PortfolioData = PatinetDocumentWS.GetMedicalPortfolio(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            

            BindingSource bs1 = new BindingSource();
            bs1.DataSource = PortfolioData.dtVisit;
            dgVisits.DataSource = bs1;

            BindingSource bs2 = new BindingSource();
            bs2.DataSource = PortfolioData.dtOutsideDoctor;
            dgDocs.DataSource = bs2;

            BindingSource bs3 = new BindingSource();
            bs3.DataSource = PortfolioData.dtPatiendDocs;
            dgPatients.DataSource = bs3;
        }

        private void cmdVisitShare_Click(object sender, EventArgs e)
        {
            if (dgVisits.Rows.Count > 0)
            {
                PatientService.PatientWS PatientWS = new PatientService.PatientWS();
                PatientService.PatientVisitParms VisitParms = new PatientService.PatientVisitParms();
                PatientService.PatientResp Resp = new PatientService.PatientResp();

                VisitParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                VisitParms.FacilityId = Convert.ToInt64(dgVisits.CurrentRow.Cells["FacilityId"].Value);
                VisitParms.VisitId = Convert.ToInt64(dgVisits.CurrentRow.Cells["VisitId"].Value);
                if (Convert.ToBoolean(dgVisits.CurrentRow.Cells["Viewable"].Value) == false)
                    VisitParms.Share = true;
                else
                    VisitParms.Share = false;

                Resp = PatientWS.VisitShare(VisitParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                LoadGrids();
            }
        }

        private void cmdDocShare_Click(object sender, EventArgs e)
        {
            if (dgDocs.Rows.Count > 0)
            {
                PatientDocumentService.PatientDocumentWS PatientDocWS = new PatientDocumentService.PatientDocumentWS();
                PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                PatientDocumentService.PatientDocumentResp Resp = new PatientDocumentService.PatientDocumentResp();

                DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                DocParms.DocumentCntr = Convert.ToInt64(dgDocs.CurrentRow.Cells["DocumentCntr"].Value);
                if (Convert.ToBoolean(dgDocs.CurrentRow.Cells["Viewable"].Value) == false)
                    DocParms.Share = true;
                else
                    DocParms.Share = false;

                Resp = PatientDocWS.PatientCareDocumentShare(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                LoadGrids();
            }
        }

        private void cmdPatientShare_Click(object sender, EventArgs e)
        {
            if (dgPatients.Rows.Count > 0)
            {
                PatientDocumentService.PatientDocumentWS PatientDocWS = new PatientDocumentService.PatientDocumentWS();
                PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                PatientDocumentService.PatientDocumentResp Resp = new PatientDocumentService.PatientDocumentResp();

                DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                DocParms.DocumentCntr = Convert.ToInt64(dgPatients.CurrentRow.Cells["DocumentCntr"].Value);
                if (Convert.ToBoolean(dgPatients.CurrentRow.Cells["Viewable"].Value) == false)
                    DocParms.Share = true;
                else
                    DocParms.Share = false;

                Resp = PatientDocWS.PatientMedicalDocumentShare(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                LoadGrids();
            }
        }
    }
}
