using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Testing
{
    public partial class frmDocument : Form
    {
        public frmDocument()
        {
            InitializeComponent();
        }

        private void cmdOpenFile_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtFileName.Text = openFileDialog1.SafeFileName;
                txtFullName.Text = openFileDialog1.FileName;

                // Parse File Extension From File Name
                string FileType = openFileDialog1.SafeFileName;
                int posn = FileType.IndexOf(".");
                if (posn > 0)
                    txtType.Text = FileType.Substring(posn + 1, FileType.Length - posn - 1);
                else
                    txtType.Text = "";

            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            
            

            try
            {
                // Clinical Documents

                if (optCareProvider.Checked)
                {
                    PatientDocumentService.PatientCareDocumentData DocumentData = new PatientDocumentService.PatientCareDocumentData();
                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;
                    DocumentData.DocumentDescription = txtDescription.Text;
                    DocumentData.DoctorName = txtDoctor.Text;
                    DocumentData.DocumentFormat = txtType.Text;
                    DocumentData.Notes = txtNotes.Text;


                    if (nudDocId.Value == 0)
                    {
                        if (DocumentData.DocumentId != "")
                        {
                            byte[] bytes = DiskToBytes(txtFullName.Text);
                            DocumentData.DocumentImage = bytes;
                        }
                    }


                    DocumentData = PatinetDocumentWS.SavePatientCareDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
                    
                // Patient General Documents

                else if (optGeneral.Checked)
                {
                    PatientDocumentService.PatientDocumentData DocumentData = new PatientDocumentService.PatientDocumentData();

                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;
                    DocumentData.DocumentDescription = txtDescription.Text;
                    DocumentData.DocumentFormat = txtType.Text;
                    DocumentData.Notes = txtNotes.Text;


                    if (nudDocId.Value == 0)
                    {
                        if (DocumentData.DocumentId != "")
                        {
                            byte[] bytes = DiskToBytes(txtFullName.Text);
                            DocumentData.DocumentImage = bytes;
                        }
                    }

                    DocumentData = PatinetDocumentWS.SavePatientDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
                // Medical Documents

                else 
                {
                    PatientDocumentService.PatientMedicalDocumentData DocumentData = new PatientDocumentService.PatientMedicalDocumentData();

                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;
                    DocumentData.DocumentDescription = txtDescription.Text;
                    DocumentData.FacilityName = txtFacility.Text;
                    DocumentData.DoctorName = txtDoctor.Text;
                    DocumentData.DocumentFormat = txtType.Text;
                    DocumentData.Notes = txtNotes.Text;


                    if (nudDocId.Value == 0)
                    {
                        if (DocumentData.DocumentId != "")
                        {
                            byte[] bytes = DiskToBytes(txtFullName.Text);
                            DocumentData.DocumentImage = bytes;
                        }
                    }

                    DocumentData = PatinetDocumentWS.SavePatientMedicalDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private static byte[] DiskToBytes(string filename)
        {
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                //EventLogger.WriteEntry("MX2", ex.Message, EventLogEntryType.Error, 742);
                return new byte[0];
            }
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();



            if (optCareProvider.Checked)
            {
                PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                DocParms.Option = 1;
                DocTableData = PatinetDocumentWS.GetPatientCareDocumentList(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
            else if (optGeneral.Checked)
            {
                PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                DocParms.Option = 1;
                DocTableData = PatinetDocumentWS.GetPatientDocumentList(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }
            else
            {
                PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                DocParms.Option = 1;
                DocTableData = PatinetDocumentWS.GetPatientMedicalDocumentList(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            }

            
        }

        private void dgCodes_Click(object sender, EventArgs e)
        {
            if (dgCodes.Rows.Count > 0)
            {
                
                nudDocId.Value = Convert.ToInt64(dgCodes.CurrentRow.Cells["DocumentCntr"].Value);
                PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();


                if (optCareProvider.Checked)
                {
                    PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                    PatientDocumentService.PatientCareDocumentData DocData = new PatientDocumentService.PatientCareDocumentData();

                    DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocParms.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocParms.Option = 1;

                    DocData = PatinetDocumentWS.GetPatientCareDocumentData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    if (DocData.Valid)
                    {
                        nudDocId.Value = DocData.DocumentCntr;
                        txtFileName.Text = DocData.DocumentId;
                        txtType.Text = DocData.DocumentFormat;
                        txtDescription.Text = DocData.DocumentDescription;
                        txtNotes.Text = DocData.Notes;

                        MemoryStream ms = new MemoryStream(DocData.DocumentImage);
                        pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(ms);
                    }
                }
                else if (optGeneral.Checked)
                {
                    PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                    PatientDocumentService.PatientDocumentData DocData = new PatientDocumentService.PatientDocumentData();

                    DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocParms.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocParms.Option = 1;

                    DocData = PatinetDocumentWS.GetPatientDocumentData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    if (DocData.Valid)
                    {
                        nudDocId.Value = DocData.DocumentCntr;
                        txtFileName.Text = DocData.DocumentId;
                        txtType.Text = DocData.DocumentFormat;
                        txtDescription.Text = DocData.DocumentDescription;
                        txtNotes.Text = DocData.Notes;

                        MemoryStream ms = new MemoryStream(DocData.DocumentImage);
                        pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(ms);
                    }
                }
                else
                {
                    PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
                    PatientDocumentService.PatientMedicalDocumentData DocData = new PatientDocumentService.PatientMedicalDocumentData();

                    DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocParms.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocParms.Option = 1;

                    DocData = PatinetDocumentWS.GetPatientMedicalDocumentData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    if (DocData.Valid)
                    {
                        nudDocId.Value = DocData.DocumentCntr;
                        txtFileName.Text = DocData.DocumentId;
                        txtType.Text = DocData.DocumentFormat;
                        txtDescription.Text = DocData.DocumentDescription;
                        txtNotes.Text = DocData.Notes;

                        MemoryStream ms = new MemoryStream(DocData.DocumentImage);
                        pictureBox1.BackgroundImage = System.Drawing.Image.FromStream(ms);
                    }
                }

            }
        }
        private static System.Drawing.Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;

        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            nudDocId.Value = 0;
            txtFileName.Text = "";
            txtType.Text = "";
            txtDescription.Text = "";
            txtNotes.Text = "";
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            
            

            try
            {
                //// Clinical Documents

                //if (optCareProvider.Checked)
                //{
                //    PatientDocumentService.PatientClinicalDocumentData DocumentData = new PatientDocumentService.PatientClinicalDocumentData();
                //    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                //    DocumentData.VisitId = Convert.ToInt64(nudVisitId.Value);
                //    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                //    DocumentData.DocumentId = txtFileName.Text;

                //    DocumentData = PatinetDocumentWS.DeletePatientClinicalDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                //    if (DocumentData.Valid)
                //    {

                //        nudDocId.Value = 0;
                //        txtFileName.Text = "";
                //        txtType.Text = "";
                //        txtDescription.Text = "";
                //        txtNotes.Text = "";
                //    }
                //    else
                //    {
                //        MessageBox.Show(DocumentData.ErrorMessage);
                //    }
                //}

                // Care Documents

                if (optCareProvider.Checked)
                {
                    PatientDocumentService.PatientCareDocumentData DocumentData = new PatientDocumentService.PatientCareDocumentData();
                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;

                    DocumentData = PatinetDocumentWS.DeletePatientCareDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
                    
                // Patient Documents

                else if (optGeneral.Checked)
                {
                    PatientDocumentService.PatientDocumentData DocumentData = new PatientDocumentService.PatientDocumentData();

                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;

                    DocumentData = PatinetDocumentWS.DeletePatientDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
                // Legal Documents

                else 
                {
                    PatientDocumentService.PatientMedicalDocumentData DocumentData = new PatientDocumentService.PatientMedicalDocumentData();

                    DocumentData.DocumentCntr = Convert.ToInt64(nudDocId.Value);
                    DocumentData.PatientId = Convert.ToInt64(nudPatientId.Value);
                    DocumentData.DocumentId = txtFileName.Text;


                    DocumentData = PatinetDocumentWS.DeletePatientMedicalDocumentData(DocumentData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    if (DocumentData.Valid)
                    {

                        nudDocId.Value = 0;
                        txtFileName.Text = "";
                        txtType.Text = "";
                        txtDescription.Text = "";
                        txtNotes.Text = "";
                    }
                    else
                    {
                        MessageBox.Show(DocumentData.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void optCareProvider_CheckedChanged(object sender, EventArgs e)
        {
            if (optCareProvider.Checked)
            {
                txtFacility.Enabled = false;
                txtDoctor.Enabled = true;
            }

        }

        private void optGeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (optGeneral.Checked)
            {
                txtFacility.Enabled = false;
                txtDoctor.Enabled = false;
            }
        }

        private void optMedical_CheckedChanged(object sender, EventArgs e)
        {
            if (optMedical.Checked)
            {
                txtFacility.Enabled = true;
                txtDoctor.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();
            PatientDocumentService.PatientDocumentParms DocParms = new PatientDocumentService.PatientDocumentParms();
            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
           
            DocTableData = PatinetDocumentWS.GetCombinedPatientDocumentData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
            BindingSource bs = new BindingSource();
            bs.DataSource = DocTableData.dt;
            dgCodes.DataSource = bs;
        }


    }
}
