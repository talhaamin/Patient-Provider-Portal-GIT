using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using AMR.Data;


namespace Testing
{
    public partial class frmISSendCDA : Form
    {
        public frmISSendCDA()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            string Attachment = DiskToBase64(txtFullName.Text);

            string PatientName = "";
            string PatientEmail = "";
            //byte[] decodedData = Convert.FromBase64String(reader.Attachment);

            Int64 PatientId = Convert.ToInt64(nudPatientId.Value);
            Int64 FacilityId = Convert.ToInt64(nudFacilityId.Value);
            Int64 ProviderId = Convert.ToInt64(nudProviderId.Value);

            clsCDAGenerate objCDAGenerate = new clsCDAGenerate();
            string CDA = "";
            bool Valid = objCDAGenerate.createPatientCDA(PatientId, FacilityId, ProviderId, Attachment, ref CDA);


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\temp\cda_" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + ".xml"))
            {

                file.WriteLine(CDA);
            }

            //using (var db = new AMREntities())
            //{
            //    Patient PatientInfo = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);
            //    if (PatientInfo != null)
            //    {
            //        PatientName = PatientInfo.FirstName + " " + PatientInfo.LastName;
            //        PatientEmail = PatientInfo.EMail;
            //    }
            //}
            //string subject = "(" + nudPatientId.Value.ToString() + ") " + PatientName;
            //SendMail(CDA, subject, PatientEmail, txtEmailFrom.Text, txtEmailTo.Text, PatientName);


        }


        private void cmdProcess2_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(txtFullName2.Text, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs.Length];
            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            string CDA = Encoding.UTF8.GetString(filebytes);

            //string CDA = DiskToBase64(txtFullName2.Text);
            SendMail(CDA, "Testing CDA", txtEmailPatient2.Text, txtEmailFrom2.Text, txtEmailTo2.Text, txtPatientName2.Text);
        }

        private void SendMail(string CDA, string subject, string PatientEmail, string From, string To, string PatientName)
        {

                try
                {
                    byte[] decodedData = System.Text.ASCIIEncoding.ASCII.GetBytes(CDA);

                    string Attachment = Convert.ToBase64String(decodedData);


                    // Get email info from server
                    string EmailFolder = "";
                    
                    using (var db = new AMREntities())
                    {
                        Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 2);

                        if (Email != null)
                        {
                            
                            EmailFolder = Email.EmailFolder;
                        }
                        EmailFolder = @"C:\inetpub\mailroot\Gateway\incoming\";
                        
                    }
                    // Build RFC 822 compliant message 

                    FileHelper.CheckOrCreateDirectory(EmailFolder);

                    string dtstring = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss ") + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours.ToString("00") + "00";

                    //EmailFolder = @"C:\temp";

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(EmailFolder + @"\" + nudPatientId.Value + "-" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + "-1.eml"))
                    {

                        // From
                        file.WriteLine("From: <" + From + ">");
                        // To
                        file.WriteLine("To: <" + To + ">");
                        // Subject
                        file.WriteLine("Subject: " + subject);
                        // Date
                        file.WriteLine("Date: " + dtstring); //System.DateTime.Now.ToString("R"));
                        // Multipart Mimi message
                        file.WriteLine("Content-Type: multipart/mixed;");
                        file.WriteLine("    boundary=\"----=_NextPart_000_9E7F_179C6A2B.8D56669F\"");
                        file.WriteLine("");
                        file.WriteLine("This is a multi-part message in MIME format.");
                        file.WriteLine("");
                        file.WriteLine("------=_NextPart_000_9E7F_179C6A2B.8D56669F");
                        file.WriteLine("Content-Type: multipart/alternative;");
                        file.WriteLine("    boundary=\"----=_NextPart_000_D209_0FD98ECB.61EE5811\"");
                        file.WriteLine("");
                        // The text in plain text
                        file.WriteLine("------=_NextPart_000_D209_0FD98ECB.61EE5811");
                        file.WriteLine("Content-Type: text/plain;");
                        file.WriteLine("    charset=\"iso-8859-1\"");
                        file.WriteLine("Content-Transfer-Encoding: quoted-printable");
                        file.WriteLine("");
                        file.WriteLine("Patient Name: " + PatientName + "=0A" + "Patient Email:(" + PatientEmail + ")");
                        file.WriteLine("");
                        // The text in HTML
                        file.WriteLine("------=_NextPart_000_D209_0FD98ECB.61EE5811");
                        file.WriteLine("Content-Type: text/html;");
                        file.WriteLine("    charset=\"iso-8859-1\"");
                        file.WriteLine("Content-Transfer-Encoding: quoted-printable");
                        file.WriteLine("");
                        file.WriteLine("Patient Name: " + PatientName);
                        file.WriteLine("<br>");
                        file.WriteLine("Patient Email:(" + PatientEmail + ")");
                        file.WriteLine("<br>");
                        file.WriteLine("");
                        // Attachment
                        file.WriteLine("------=_NextPart_000_D209_0FD98ECB.61EE5811--");
                        file.WriteLine("");
                        file.WriteLine("------=_NextPart_000_9E7F_179C6A2B.8D56669F");
                        file.WriteLine("Content-Type: text/xml;");
	                    file.WriteLine("    name=\"" + PatientName + ".xml\"");
                        file.WriteLine("Content-Disposition: attachment;");
	                    file.WriteLine("    filename=\"" + PatientName + ".xml\"");
                        file.WriteLine("Content-Transfer-Encoding: base64");
                        file.WriteLine("");
                        file.WriteLine(Attachment);
                        //file.WriteLine("");
                        file.WriteLine("------=_NextPart_000_9E7F_179C6A2B.8D56669F--");
                        file.WriteLine("");
                    }
                   
                }
                catch (Exception ex)
                {

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

        private void cmdOpenFile2_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                txtFileName2.Text = openFileDialog1.SafeFileName;
                txtFullName2.Text = openFileDialog1.FileName;

            }
        }




    }
}
