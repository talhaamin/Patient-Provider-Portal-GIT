// Service Name  : ConfigService
// Date Created  : 01/02/2014
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Direct Project
// MM/DD/YYYY XXX Description
// 
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using AMR.Data;
//using System.Net.Mail;
//using System.Net.Mime;

namespace AMR.DataService
{
    /// <summary>
    /// Summary description for DirectService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "DirectWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    public class DirectService : System.Web.Services.WebService
    {

        static Int16 ActionEmail = 4;
        static Int16 ActionDownload = 5;
        static Int16 DocTypeCCD = 20;

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct SendDirectParms
        {
            public string EmailAddress;
            public Int64 PatientId;
            public Int64 FacilityId;
            public Int64 VisitId;

            //public bool Problems;
            //public bool Allergies;
            //public bool Immunizations;
            //public bool Medications;
            //public bool Social;
            //public bool Procedures;
            //public bool VitalSigns;
            //public bool Labs;
            //public bool ClinicalInstructions;
            //public bool FutureAppointments;
            //public bool Referrals;
            //public bool ScheduledTests;
            //public bool DecisionAids;
        }

        public struct DirectResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }
        #endregion

        #region Validate Email Address
        //------------------------------------------------------------------------
        // Validate Email Address
        //------------------------------------------------------------------------

        [WebMethod(Description = "Validate Email Address")]
        public DirectResponse ValidateEmailAddress(string EmailAddress, string Token, Int64 UserId, Int64 FacilityId)
        {
            DirectResponse Response = new DirectResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new DirectConfigEntities())
                    {
                        Address results = db.Addresses.FirstOrDefault(a => a.EmailAddress == EmailAddress);

                        if (results != null)
                        {
                            Response.Valid = true;

                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Email Address Not Configured in Direct.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Send Direct Email With CCD
        //------------------------------------------------------------------------
        // Send Direct Email With CCD
        //------------------------------------------------------------------------

        [WebMethod(Description = "Send Direct Email With CCD")]
        public DirectResponse SendDirectEmail(SendDirectParms Parms, string Token, Int64 UserId, Int64 FacilityId, string CCD_HTML, string CCD_XML)
        {
            DirectResponse Response = new DirectResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    
                    //clsCCDGenerate objCCDGenerate = new clsCCDGenerate();
                    //clsCCDGenerate.CCDParms Parms2 = new clsCCDGenerate.CCDParms();
                    //Parms2.Problems = Parms.Problems;
                    //Parms2.Allergies = Parms.Allergies;
                    //Parms2.Immunizations = Parms.Immunizations;
                    //Parms2.Medications = Parms.Medications;
                    //Parms2.Social = Parms.Social;
                    //Parms2.Labs = Parms.Labs;
                    //Parms2.Procedures = Parms.Procedures;
                    //Parms2.VitalSigns = Parms.VitalSigns;
                    //Parms2.ClinicalInstructions = Parms.ClinicalInstructions;
                    //Parms2.FutureAppointments = Parms.FutureAppointments;
                    //Parms2.Referrals = Parms.Referrals;
                    //Parms2.ScheduledTests = Parms.ScheduledTests;
                    //Parms2.DecisionAids = Parms.DecisionAids;

                    //string CCD = "";

                    //bool Valid = objCCDGenerate.createPatientCCD(Parms.PatientId, Parms.FacilityId, Parms.VisitId, Parms2, ref CCD);

                    //if (Valid)
                    //{
                        // Get email info from server
                        string From = "";
                        string EmailFolder = "";
                        string PatientName = "";
                        using (var db = new AMREntities())
                        {
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 2);

                            if (Email != null)
                            {
                                From = Email.EmailUser;
                                EmailFolder = Email.EmailFolder;
                            }

                            Patient PatientInfo = db.Patients.FirstOrDefault(p => p.PatientId == Parms.PatientId);
                            if (PatientInfo != null)
                            {
                                PatientName = PatientInfo.FirstName + " " + PatientInfo.LastName;
                            }
                        }
                        // Build RFC 822 compliant message 

                        FileHelper.CheckOrCreateDirectory(EmailFolder);


                        //MailMessage mailMessage = new MailMessage();
                        //mailMessage.From = new MailAddress(From);
                        //mailMessage.To.Add(Parms.EmailAddress);
                        //mailMessage.Subject = "(" + Parms.PatientId + ") " + PatientName;
                        //mailMessage.Body = "";

                        //var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(CCD_XML));

                        //System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Xml);
                        //System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(ms, ct);
                        //attach.ContentDisposition.FileName = PatientName + "-CCD.xml";

                        //mailMessage.Attachments.Add(attach);

                        //SmtpClient smtpClient = new SmtpClient();
                        //smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        //smtpClient.PickupDirectoryLocation = EmailFolder;
                        //smtpClient.Send(mailMessage);


                        //mailMessage = new MailMessage();
                        //mailMessage.From = new MailAddress(From);
                        //mailMessage.To.Add(Parms.EmailAddress);
                        //mailMessage.Subject = "(" + Parms.PatientId + ") " + PatientName;
                        //mailMessage.Body = "";

                        //var ms2 = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(CCD_HTML));

                        //System.Net.Mime.ContentType ct2 = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Html);
                        //System.Net.Mail.Attachment attach2 = new System.Net.Mail.Attachment(ms2, ct);
                        //attach2.ContentDisposition.FileName = PatientName + "-CCD.html";

                        //mailMessage.Attachments.Add(attach2);

                        //smtpClient = new SmtpClient();
                        //smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        //smtpClient.PickupDirectoryLocation = EmailFolder;
                        //smtpClient.Send(mailMessage);

                        string dtstring = System.DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss ") + TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours.ToString("00") + "00";

                        if (CCD_XML != "")
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(EmailFolder + @"\" + Parms.PatientId + "-" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + "-1.eml"))
                            {

                                // From
                                file.WriteLine("From: <" + From + ">");
                                // To
                                file.WriteLine("To: <" + Parms.EmailAddress + ">");
                                // Subject
                                file.WriteLine("Subject: (" + Parms.PatientId + ") " + PatientName);
                                // Date
                                file.WriteLine("Date: " + dtstring); //System.DateTime.Now.ToString("R"));
                                // Attachment
                                file.WriteLine("MIME-Version: 1.0");
                                file.WriteLine("Content-Type: text/xml;");
                                file.WriteLine("    name=\"" + PatientName + "-CCD.xml\"");
                                file.WriteLine("Content-Disposition: attachment;");
                                file.WriteLine("    filename=\"" + PatientName + "-CCD.xml\"");
                                file.WriteLine("");
                                file.WriteLine(CCD_XML);
                                file.WriteLine("");
                            }
                        }

                        if (CCD_HTML != "")
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(EmailFolder + @"\" + Parms.PatientId + "-" + System.DateTime.Now.ToString("yyyyMMddhhmmss") + "-2.eml"))
                            {

                                // From
                                file.WriteLine("From: <" + From + ">");
                                // To
                                file.WriteLine("To: <" + Parms.EmailAddress + ">");
                                // Subject
                                file.WriteLine("Subject: (" + Parms.PatientId + ") " + PatientName);
                                // Date
                                file.WriteLine("Date: " + dtstring); //System.DateTime.Now.ToString("R"));
                                // Attachment
                                file.WriteLine("MIME-Version: 1.0");
                                file.WriteLine("Content-Type: text/html;");
                                file.WriteLine("    name=\"" + PatientName + "-CCD.html\"");
                                file.WriteLine("Content-Disposition: attachment;");
                                file.WriteLine("    filename=\"" + PatientName + "-CCD.html\"");
                                file.WriteLine("");
                                file.WriteLine(CCD_HTML);
                                file.WriteLine("");
                            }
                        }

                        //WriteAuditRecord(Parms.PatientId, Parms.VisitId, 0, FacilityId, DocTypeCCD, UserId, ActionEmail);
                    //}
                    //else
                    //{
                    //    Response.Valid = false;
                    //    Response.ErrorMessage = "Error Generating CCD";
                    //}
                    
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    Response.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Response.Valid = false;
                Response.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion

        #region Write Audit Record
        //------------------------------------------------------------------------
        // Write Audit Record
        //------------------------------------------------------------------------
        private void WriteAuditRecord(Int64 PatientId, Int64 VisitId, Int64 Cntr, Int64 FacilityId, Int16 DocumentType, Int64 UserId, Int16 Action)
        {
            using (var db = new AMREntities())
            {
                //// Add Items
                var Audit = new PatientDocumentAudit()
                {
                    PatientId = PatientId,
                    VisitId = VisitId,
                    DocCntr = Cntr,
                    FacilityId = FacilityId,
                    DocumentTypeId = DocumentType,
                    UserId = UserId,
                    TDStamp = System.DateTime.Now,
                    AuditActionId = Action,
                };
                db.PatientDocumentAudits.Add(Audit);

                db.SaveChanges();
            }
        }
        #endregion
    }
}
