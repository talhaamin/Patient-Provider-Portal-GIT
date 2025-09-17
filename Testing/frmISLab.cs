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
    public partial class frmISLab : Form
    {
        public frmISLab()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();

            DataTable dt = new DataTable();
            dt.Columns.Add("Component", typeof(string));
            dt.Columns.Add("Result", typeof(string));
            dt.Columns.Add("RefRange", typeof(string));
            dt.Columns.Add("Units", typeof(string));
            dt.Columns.Add("Abnormal", typeof(string));
            dt.Columns.Add("ResultStatus", typeof(string));

            dt.TableName = "Lab Test";

            dt.Rows.Add("Fasting Blood Glucode", "178 mg/dl", "100-120", "See results", "0", "Rcvd");
            //dt.Rows.Add("Component 2", "Result 2", "100-120", "ppm", "0", "Good");

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("LabData");
            DataTable dataTable2 = new DataTable("table1");
            //dataTable.Columns.Add("col1", typeof(string));
            dataSet.Tables.Add(dataTable);

            //string labs = "<Labs>" + 
            //            "<LabData>" +
            //            "    <Component>Fasting Blood Glucode</Component>" +
            //            "    <Result>178 mg/dl</Result>" +
            //            "    <RefRange>100-120</RefRange>" +
            //            "    <Units>See results</Units>" +
            //            "    <Abnormal>0</Abnormal>" +
            //            "    <ResultStatus>Rcvd</ResultStatus>" +
            //            "</LabData>" +
            //            "<LabData>" +
            //            "    <Component>Component 2</Component>" +
            //            "    <Result>200 mg/dl</Result>" +
            //            "    <RefRange>100-120</RefRange>" +
            //            "    <Units>ppm</Units>" +
            //            "    <Abnormal>0</Abnormal>" +
            //            "    <ResultStatus>Good</ResultStatus>" +
            //            "</LabData>" +
            //            "</Labs>";

            string labs = "<Labs><LabData>" +
                    "<Component>Glucose, Serum</Component>" +
                    "<Result>84 mg/dL</Result>" +
                    "<RefRange>65-99</RefRange>" +
                    "<Units>See results</Units>" +
                    "<Abnormal>0</Abnormal>" +
                    "<ResultStatus>Rcvd</ResultStatus>" +
                    "</LabData>" +
                    "<LabData>" +
                    "<Component>Creatinine</Component>" +
                    "<Result>1.45 mg/dl</Result>" +
                    "<RefRange>Unknown</RefRange>" +
                    "<Units>See results</Units>" +
                    "<Abnormal>1</Abnormal>" +
                    "<ResultStatus>Rcvd</ResultStatus>" +
                    "</LabData>" +
                    "</Labs>";


            //System.IO.StringReader theReader = new System.IO.StringReader(labs);
            //DataSet theDataSet = new DataSet();
            //theDataSet.ReadXml(theReader);

            ISPatientService.LabDataXML LabDataXML = new ISPatientService.LabDataXML();
            try
            {
                //LabDataXML.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                //LabDataXML.VisitId = Convert.ToInt64(nudVisitId.Value);
                //LabDataXML.ProviderId = 1;
                //LabDataXML.LabName = txtDescription.Text;
                //LabDataXML.OrderDate = dtpDate.Value.AddDays(-2);
                //LabDataXML.CollectionDate = dtpDate.Value;
                //LabDataXML.Requisition = "R2314";
                //LabDataXML.Specimen = "Spec123";
                //LabDataXML.SpecimenSource = "Mouth";
                //LabDataXML.ReportDate = dtpDate.Value.AddDays(2);
                //LabDataXML.ReviewDate = dtpDate.Value.AddDays(3);
                //LabDataXML.Reviewer = "Unknown";
                //LabDataXML.Test = labs;

                LabDataXML.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                LabDataXML.VisitId = Convert.ToInt64(nudVisitId.Value);
                LabDataXML.ProviderId = 107;
                LabDataXML.LabName = "LabCorp EDI Testing";
                LabDataXML.OrderDate = Convert.ToDateTime("2014-08-11");
                LabDataXML.CollectionDate = Convert.ToDateTime("2014-08-11");
                LabDataXML.Requisition = "N/A";
                LabDataXML.Specimen = "Unknown";
                LabDataXML.SpecimenSource = "Unknown";
                LabDataXML.ReportDate = Convert.ToDateTime("2014-08-11");
                LabDataXML.ReviewDate = Convert.ToDateTime("2014-08-11");
                LabDataXML.Reviewer = "Unknown";
                LabDataXML.Test = labs;

                PatinetWS.LabPostXML(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, LabDataXML);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }

            //ISPatientService.LabData LabData = new ISPatientService.LabData();
            //try
            //{
            //    LabData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
            //    LabData.VisitId = Convert.ToInt64(nudVisitId.Value);
            //    LabData.ProviderId = 1;
            //    LabData.LabName = txtDescription.Text;
            //    LabData.OrderDate = dtpDate.Value.AddDays(-2);
            //    LabData.CollectionDate = dtpDate.Value;
            //    LabData.Requisition = "R2314";
            //    LabData.Specimen = "Spec123";
            //    LabData.SpecimenSource = "Mouth";
            //    LabData.ReportDate = dtpDate.Value.AddDays(2);
            //    LabData.ReviewDate = dtpDate.Value.AddDays(3);
            //    LabData.Reviewer = "Unknown";
            //    LabData.Test = dataTable;

            //    PatinetWS.LabPost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, LabData);


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error Saving");
            //}
        }
    }
}
