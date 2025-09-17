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
    public partial class frmISDocument : Form
    {
        public frmISDocument()
        {
            InitializeComponent();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatientWS = new ISPatientService.PatientWS();
            ISPatientService.DocumentData DocumentData = new ISPatientService.DocumentData();

            try
            {

                DocumentData.VisitId = Convert.ToInt64(nudVisitId.Value);
                DocumentData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                DocumentData.DocumentId = "0";// txtFileName.Text;
                DocumentData.Description = txtDescription.Text;
                DocumentData.DateCreated = System.DateTime.Now;
                DocumentData.DocumentFormat = txtType.Text;
                DocumentData.Notes = txtNotes.Text;



                if (DocumentData.DocumentId != "")
                {
                    //byte[] bytes = DiskToBytes(txtFullName.Text);
                    //DocumentData.DocumentImage = bytes;

                    DocumentData.DocumentImage = DiskToBase64(txtFullName.Text);
                }


                PatientWS.DocumentPost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, DocumentData);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
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

        private static string DiskToBase64(string filename)
        {
            string base64String = "";
            try
            {
                FileStream stream = new FileStream(filename, FileMode.Open);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                base64String = Convert.ToBase64String(bytes);
                return base64String;
            }
            catch (Exception ex)
            {
                //EventLogger.WriteEntry("MX2", ex.Message, EventLogEntryType.Error, 742);
                return base64String;
            }
        }
    }
}
