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

using System.Net.Mail;
using System.Net.Mime;


using System.Xml;
using AMR.Data;

namespace Testing
{
    public partial class frmDirectImport : Form
    {
        public frmDirectImport()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int64 PatientId = 0;
            Int64 FacilityId = 0;
            string PatientName = "";
            XmlDocument xd = new XmlDocument();
            bool valid = true;
            string error = "";
            string PatientEmail = "";
             // load the message from a local disk file 


            // Read email record
            valid = true;
            error = "";
            FacilityId = 0;
            PatientId = 0;
            PatientEmail = "";

            Int64 mbintFacilityId = 0;
            string mstrLogin = "admin";
            string mstrPassword = "pass";
            Int64 mbintUserId = 0;
            string mstrToken = "";
            string mstrBadMailFolder = "C:\\inetpub\\mailroot\\gateway\\badmail";
            string mstrProcessedBadMailFolder = "C:\\inetpub\\mailroot\\gateway\\badprocessed";
            string mstrNewMailFolder = "C:\\inetpub\\mailroot\\gateway\\incoming";
            string mstrProcessedMailFolder = "C:\\inetpub\\mailroot\\gateway\\incomingprocessed";

            int fileposn = mstrBadMailFolder.Length + 1;
            int fileposn2 = mstrNewMailFolder.Length + 1;

            // Login to system and get token

            AuthenticationService.AuthenticationWS PatinetWS = new AuthenticationService.AuthenticationWS();
            AuthenticationService.AuthenticationResponse ResponseData = new AuthenticationService.AuthenticationResponse();

            ResponseData = PatinetWS.AuthenticateInterface(mbintFacilityId, mstrLogin, mstrPassword);

            if (ResponseData.Valid)
            {
                mstrToken = ResponseData.Token;
                mbintUserId = ResponseData.UserId;
            }


            MessageService.MessageWS MessageWS = new MessageService.MessageWS();
            MessageService.MessageData MessageData = new MessageService.MessageData();

            ISMessageService.MessageWS ISMessageWS = new ISMessageService.MessageWS();
            ISMessageService.DirectReceivedData DirectReceivedData = new ISMessageService.DirectReceivedData();
            ISMessageService.DirectReceivedResp DirectReceivedResp = new ISMessageService.DirectReceivedResp();

            ISDirectService.DirectWS DirectWS = new ISDirectService.DirectWS();
            ISDirectService.DirectData DirectData = new ISDirectService.DirectData();
            ISDirectService.DirectResp DirectResp = new ISDirectService.DirectResp();

            #region Process Bad Mail
            try
            {
                // Grab a list of emails that are in the badmail folder
                string[] filearray = Directory.GetFiles(mstrBadMailFolder);

                // Loop through bad email files

                foreach (string filename in filearray)
                {
                    // Read email record

                    try
                    {
                        FileStream fs = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
                        EmailReader reader = new EmailReader(fs);
                        fs.Close();

                        int posn1 = reader.Subject.IndexOf('(');
                        int posn2 = reader.Subject.IndexOf(')');
                        PatientId = Convert.ToInt64(reader.Subject.Substring(posn1 + 1, posn2 - posn1 - 1));
                        PatientName = reader.Subject.Substring(posn2 + 2, reader.Subject.Length - posn2 - 2);

                        // Send message to patient

                        MessageData.MessageId = 0;
                        MessageData.PatientId = PatientId;
                        MessageData.MessageTypeId = 4; // General
                        MessageData.FacilityId = 0;
                        MessageData.MessageStatusId = 1;
                        MessageData.MessageDetailId = 0;
                        MessageData.ProviderId_To = 0;
                        MessageData.ProviderId_From = 1;
                        MessageData.User_Id_Created = mbintUserId;
                        MessageData.CreatedByName = PatientName;
                        MessageData.MessageRequest = "";
                        MessageData.MessageResponse = "Could not deliver Direct Email to " + reader.To;
                        MessageData.MessageResponseTypeId = 1;
                        MessageData.PreferredPeriod = "";
                        MessageData.PreferredTime = "";
                        MessageData.PreferredWeekDay = "";
                        MessageData.VisitReason = "";
                        MessageData.AppointmentStart = Convert.ToDateTime("1/1/1900");
                        MessageData.AppointmentEnd = Convert.ToDateTime("1/1/1900");
                        MessageData.ProviderId_Appointment = 0;
                        MessageData.MedicationNDC = "";
                        MessageData.MedicationName = "";
                        MessageData.NoOfRefills = 0;
                        MessageData.MedicationStatus = 0;
                        MessageData.PharmacyName = "";
                        MessageData.PharmacyAddress = "";
                        MessageData.AttachmentName = "";

                        MessageWS.SaveMessageData(MessageData, mstrToken, mbintUserId, mbintFacilityId);

                    }
                    catch (Exception ex)
                    {
                        // Not a email error record originating from portal
                    }
                    // Move bad email to processed folder
                    string newname = mstrProcessedBadMailFolder + "\\" + filename.Substring(fileposn, filename.Length - fileposn);
                    File.Move(filename, newname);
                }

            }
            catch
            { }
            #endregion

            #region Process Direct Messages
            
            // Grab a list of emails that are in the incoming folder
            string[] filearray2 = Directory.GetFiles(mstrNewMailFolder);


            // Loop through direct email files

            foreach (string filename in filearray2)
            {
                // Read email record
                valid = true;
                error = "";
                FacilityId = 0;
                PatientId = 0;
                PatientEmail = "";

                string From = "";
                string To = "";
                string Body = "";
                int AttachmentCnt = 0;

                try
                {

                    FileStream fs = File.Open(filename, FileMode.Open, FileAccess.ReadWrite);
                    EmailReader reader = new EmailReader(fs);
                    fs.Close();

                    From = reader.From;
                    To = reader.To;
                    Body = reader.Body;
                    AttachmentCnt = reader.AttachmentCntr;

                    if (AttachmentCnt == 0)
                    {
                        valid = false;
                        error = "No attachment";
                        // Write transaction to database

                        DirectReceivedData.DirectReceivedId = 0;
                        DirectReceivedData.Processed = valid;
                        DirectReceivedData.DateProcessed = System.DateTime.Now.ToString();
                        DirectReceivedData.PatientId = PatientId;
                        DirectReceivedData.FacilityId = FacilityId;
                        DirectReceivedData.EmailId = filename.Substring(fileposn2, filename.Length - fileposn2);
                        DirectReceivedData.FromEmail = From;
                        DirectReceivedData.ToEmail = To;
                        DirectReceivedData.Body = Body;
                        DirectReceivedData.Attachment = "";
                        DirectReceivedData.Message = error;

                        DirectReceivedResp = ISMessageWS.DirectReceivedPost(mbintFacilityId, mbintUserId, mstrToken, DirectReceivedData);
                    }
                    else
                    {

                        for (int AttachCntr = 0; AttachCntr < AttachmentCnt; AttachCntr++)
                        {

                            try
                            {
                                string xml = "";
                                // Process the inbound message
                                if (reader.AttachmentEncoding[AttachCntr] == null)
                                {
                                    xml = reader.Attachment[AttachCntr];
                                }
                                else if (reader.AttachmentEncoding[AttachCntr].ToUpper() == "BASE64")
                                {
                                    byte[] decodedData = Convert.FromBase64String(reader.Attachment[AttachCntr]);
                                    xml = Encoding.UTF8.GetString(decodedData);
                                }
                                else
                                {
                                    xml = reader.Attachment[AttachmentCnt];
                                }

                                //The following line is needed to fix issue causing error: Data at the root level is invalid. Line 1, position 1
                                xml = xml.Replace("﻿<?xml version=\"1.0\" encoding=\"UTF-8\"?>", "");
                                xml = xml.Replace("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>", "");
                                xml = xml.Replace("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

                                xd.LoadXml(xml);

                            }
                            catch (Exception ex)
                            {
                                valid = false;
                                error = "Attachment is not a valid XML document";
                            }
                            if (valid)
                            {
                                // Parse the Patient's Email Address out of the body text
                                int posn = reader.Body.IndexOf("Patient Email:");
                                if (posn > 0)
                                {
                                    posn = reader.Body.IndexOf("(", posn);
                                    if (posn > 0)
                                    {
                                        int posn2 = reader.Body.IndexOf(")", posn);
                                        if (posn2 > 0)
                                        {
                                            string temp = reader.Body.Substring(posn + 1, posn2 - posn - 1);
                                            if (temp.Length < 100 && temp.Contains("@"))
                                                PatientEmail = temp;
                                        }
                                    }
                                }


                                DirectData.PatientId = PatientId;
                                DirectData.FacilityId = FacilityId;
                                DirectData.PatientEmail = PatientEmail;
                                DirectData.xd = xd;

                                DirectResp = DirectWS.ProcessDirectMessage(DirectData, mstrToken, mbintUserId, mbintFacilityId);

                                PatientId = DirectResp.PatientId;
                                FacilityId = DirectResp.FacilityId;
                                valid = DirectResp.Valid;
                                error = DirectResp.ErrorMessage;
                            }
                            // Write transaction to database

                            DirectReceivedData.DirectReceivedId = 0;
                            DirectReceivedData.Processed = valid;
                            DirectReceivedData.DateProcessed = System.DateTime.Now.ToString();
                            DirectReceivedData.PatientId = PatientId;
                            DirectReceivedData.FacilityId = FacilityId;
                            DirectReceivedData.EmailId = filename.Substring(fileposn2, filename.Length - fileposn2);
                            DirectReceivedData.FromEmail = From;
                            DirectReceivedData.ToEmail = To;
                            DirectReceivedData.Body = Body;
                            DirectReceivedData.Attachment = reader.Attachment[AttachCntr];
                            DirectReceivedData.Message = error;

                            DirectReceivedResp = ISMessageWS.DirectReceivedPost(mbintFacilityId, mbintUserId, mstrToken, DirectReceivedData);
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    valid = false;
                    error = "Bad email file";
                    // Write transaction to database

                    DirectReceivedData.DirectReceivedId = 0;
                    DirectReceivedData.Processed = valid;
                    DirectReceivedData.DateProcessed = System.DateTime.Now.ToString();
                    DirectReceivedData.PatientId = PatientId;
                    DirectReceivedData.FacilityId = FacilityId;
                    DirectReceivedData.EmailId = filename.Substring(fileposn2, filename.Length - fileposn2);
                    DirectReceivedData.FromEmail = From;
                    DirectReceivedData.ToEmail = To;
                    DirectReceivedData.Body = Body;
                    DirectReceivedData.Attachment = "";
                    DirectReceivedData.Message = error;

                    DirectReceivedResp = ISMessageWS.DirectReceivedPost(mbintFacilityId, mbintUserId, mstrToken, DirectReceivedData);
                }

                

                // Move direct email to processed folder
                string newname = mstrProcessedMailFolder + "\\" + filename.Substring(fileposn2, filename.Length - fileposn2);
                File.Move(filename, newname);
            }
            
            #endregion


            

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string ccdPath = @"C:\transfer\CCDA_Ambulatory.xml";
            string ccdPath = @"C:\transfer\inofile2.xml";

            XmlDocument xd = new XmlDocument();
            FileStream fs2 = new FileStream(ccdPath, FileMode.Open, FileAccess.Read);
            byte[] filebytes = new byte[fs2.Length];
            fs2.Read(filebytes, 0, Convert.ToInt32(fs2.Length));
            //string type = Encoding.GetEncoding(ccdPath).ToString();

            string xml = Encoding.UTF8.GetString(filebytes);
            try
            {
                xd.LoadXml(xml);

                Int64 FacilityId = 0;
                Int64 PatientId = 0;
                string PatientEmail = "";
                string ErrorMsg = "";



                clsDirectImport objDirectImport = new clsDirectImport();
                //if (!objDirectImport.Parse(xd, ref ErrorMsg, Convert.ToInt64(nudFacilityId.Value), Convert.ToInt64(nudPatientId.Value)))

                if (!objDirectImport.Parse(xd, ref ErrorMsg, ref FacilityId, ref PatientId, PatientEmail))
                    MessageBox.Show(ErrorMsg);

             }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
        }

    }
}
