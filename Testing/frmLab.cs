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
    public partial class frmLab : Form
    {
        public frmLab()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientLabResultParms DocParms = new PatientDocumentService.PatientLabResultParms();
            PatientDocumentService.PatientLabResultData LabResultData = new PatientDocumentService.PatientLabResultData();

            //DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            //DocParms.VisitId = Convert.ToInt64(nudVisitId.Value);
            //DocParms.LabResultCntr = Convert.ToInt64(nudCntrId.Value);

            //LabResultData = PatinetDocumentWS.GetPatientLabResultData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            //if (LabResultData.Valid)
            //{
            //    txtDescription.Text = LabResultData.LabName;
            //}
            //else
            //{
            //    MessageBox.Show(LabResultData.ErrorMessage);
            //}

            DocParms.PatientId = 11;
            DocParms.VisitId = 4;
           // DocParms.LabResultCntr = Convert.ToInt64(nudCntrId.Value);
            DocParms.Option = 1;

            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();
            DocTableData = PatinetDocumentWS.GetPatientLabResultList(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            DataTable dt = new DataTable();
            dt.Columns.Add("Component", typeof(string));
            dt.Columns.Add("Result", typeof(string));
            dt.Columns.Add("RefRange", typeof(string));
            dt.Columns.Add("Units", typeof(string));
            dt.Columns.Add("Abnormal", typeof(string));
            dt.Columns.Add("ResultStatus", typeof(string));

            dt.TableName = "Lab Test";

            dt.Rows.Add("Component 1", "Result 1", "100-120","ppm","false","Good");
            dt.Rows.Add("Component 2", "Result 2", "100-120","ppm","false","Good");


            PatientDocumentService.PatientLabResultData LabResultData = new PatientDocumentService.PatientLabResultData();
            try
            {
                LabResultData.PatientId = Convert.ToInt64(nudPatientId.Value);
                LabResultData.VisitId = Convert.ToInt64(nudVisitId.Value);
                LabResultData.LabResultCntr = 0;
                LabResultData.FacilityId = 0;
                LabResultData.ProviderId_Requested = 1;
                LabResultData.LabName = txtDescription.Text;
                LabResultData.OrderDate =  dtpDate.Value.AddDays(-2);
                LabResultData.CollectionDate = dtpDate.Value;
                LabResultData.Requisiton = "";
                LabResultData.Specimen = "";
                LabResultData.SpecimenSource = "";
                LabResultData.ReportDate =  dtpDate.Value.AddDays(2);
                LabResultData.ReviewDate = dtpDate.Value.AddDays(3);
                LabResultData.SpecimenSource = "";
                LabResultData.dtTests = dt;

                PatinetDocumentWS.SavePatientLabResultData(LabResultData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
