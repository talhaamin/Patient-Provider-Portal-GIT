// Service Name  : MessageService
// Date Created  : 11/11/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Patient Messaging
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

namespace AMR.IntegrationService
{
    /// <summary>
    /// Summary description for MessageService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "MessageWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MessageService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct MessageData
        {
            public Int64 MessageId;
            public Int64 ProviderId;
            public string MessageResponse;
            public string AppointmentStart;
            public string AppointmentEnd;
            public Int64 AppointmentProviderId;
            public string MedicationNDC;
            public string MedicationName;
            public int NoOfRefills;
            public int MedicationStatus;
            public string PharmacyName;
            public string PharmacyAddress;
            public string AttachmentName;
            //public byte[] Attachment;
            public string Attachment;
        }
        public struct MessagePostResult
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct MessageTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct MessageReceived
        {
            public DataTable dt;
        }

        public struct NewMessageData
        {
            public string AMRPatientIds;
            public Int64 AMRProviderId;
            public string MessageRequest;
            public string AttachmentName;
            //public byte[] Attachment;
            public string Attachment;

            //public DataTable dtDetails;


        }

        public struct DirectReceivedData
        {
            public Int64 DirectReceivedId;
            public bool Processed;
            public string DateProcessed;
            public Int64 PatientId;
            public Int64 FacilityId;
            public string EmailId;
            public string FromEmail;
            public string ToEmail;
            public string Body;
            public string Attachment;
            public string Message;
        }
        public struct DirectReceivedResp
        {
            public bool Valid;
            public string ErrorMessage;
        }
        #endregion

        #region Save Message Data
        //------------------------------------------------------------------------
        // Save Message Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Message Data")]
        public MessagePostResult MessagePost(Int64 FacilityId, Int64 UserId, string Token, MessageData MessageData)
        {
            MessagePostResult MessageResponse = new MessagePostResult();
            MessageResponse.Valid = true;
            MessageResponse.ErrorMessage = "";
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        if (MessageData.MessageId == 0)
                        {
                            // Should always have a message id returned
                            MessageResponse.Valid = false;
                            MessageResponse.ErrorMessage = "Message Id is invalid";
                           
                        }
                        else
                        {
                            // Update Message Info - First Read Message Header to make sure message exists.

                            Message MessageResp = db.Messages.FirstOrDefault(m => m.MessageId == MessageData.MessageId);

                            if (MessageResp != null)
                            {
                                // We have a good message

                                // Validate the the send data is good

                                string ErrorMsg = "";

                                if (MessageData.ProviderId ==  0)
                                    ErrorMsg = ErrorMsg + "ProviderId is required;   ";

                                //Make sure that the provider returned is valid for the facility
                                ProviderFacilityLink links = db.ProviderFacilityLinks.FirstOrDefault(l => l.ProviderId == MessageData.ProviderId && l.FacilityId == FacilityId);
                                if (links == null)
                                    ErrorMsg = ErrorMsg + "Provider Id supplied is not valid for this facility";

                                if (MessageData.MessageResponse ==  null || MessageData.MessageResponse == "" || MessageData.MessageResponse.Length > 255)
                                    ErrorMsg = ErrorMsg + "Messsage Response is required and must be <= 255 characters;   ";

                                if (MessageData.AppointmentStart == null)
                                    MessageData.AppointmentStart = "1/1/1900";
                                if (MessageData.AppointmentEnd == null)
                                    MessageData.AppointmentEnd = "1/1/1900";

                                try { Convert.ToDateTime(MessageData.AppointmentStart); }
                                catch { MessageData.AppointmentStart = "1/1/1900"; }
                                try { Convert.ToDateTime(MessageData.AppointmentEnd); }
                                catch { MessageData.AppointmentEnd = "1/1/1900"; }

                                // Check required fields for appointment - MessageType = 1
                                if (MessageResp.MessageTypeId == 1)
                                {
                                    try { DateTime Test = Convert.ToDateTime(MessageData.AppointmentStart); }
                                    catch { ErrorMsg = ErrorMsg + "Invalid AppointmentStart;   "; }
                                    try { DateTime Test = Convert.ToDateTime(MessageData.AppointmentEnd); }
                                    catch { ErrorMsg = ErrorMsg + "Invalid AppointmentEnd;   "; }
                                    if (MessageData.AppointmentProviderId == 0)
                                        ErrorMsg = ErrorMsg + "AppointmentProviderId is required;   ";
                                }
                                // Check required fields for Medications - MessageType = 2
                                if (MessageResp.MessageTypeId == 2)
                                {
                                    try
                                    {
                                        if (MessageData.MedicationNDC.Length > 10)
                                            ErrorMsg = ErrorMsg + "MesssageNDC must be <= 10 characters;   ";
                                        if (MessageData.MedicationName == null || MessageData.MedicationName == "" || MessageData.MedicationName.Length > 50)
                                            ErrorMsg = ErrorMsg + "Medication Name is required and must be <= 50 characters;   ";
                                        if (MessageData.MedicationStatus > 1)
                                            ErrorMsg = ErrorMsg + "Medication Status must be 0 or 1;   ";
                                        if (MessageData.PharmacyName == null || MessageData.PharmacyName == "" || MessageData.PharmacyName.Length > 50)
                                            ErrorMsg = ErrorMsg + "Pharmacy Name is required and must be <= 50 characters;   ";
                                        if (MessageData.PharmacyAddress == null || MessageData.PharmacyAddress == "" || MessageData.PharmacyAddress.Length > 150)
                                            ErrorMsg = ErrorMsg + "Pharmacy Address is required and must be <= 50 characters;   ";
                                    }
                                    catch
                                    {
                                        ErrorMsg = ErrorMsg + "Missing medication data;   ";
                                    }
                                }

                                if (ErrorMsg != "")
                                {
                                    MessageResponse.Valid = false;
                                    MessageResponse.ErrorMessage = ErrorMsg;
                                }
                                else
                                {

                                    // Get The Original Request Detail Record For The Message
                                    MessageDetail details = db.MessageDetails.FirstOrDefault(d => d.MessageId == MessageData.MessageId);
                                    Int64 AttachmentId = 0;
                                    if (details != null)
                                    {
                                        // Upload Attachment
                                        if (MessageData.AttachmentName !=  null && MessageData.AttachmentName != "")
                                        {
                                            // Get Attachment Folder Info & Update Counters
                                            ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 2);
                                            // Update Attachment Counter
                                            if (Config.CurrentFolderDocCntr > 1000)
                                            {
                                                Config.AttachmentFolderCntr++;
                                                Config.CurrentFolderDocCntr = 0;
                                            }
                                            Config.CurrentFolderDocCntr++;
                                            db.SaveChanges();

                                            // Parse File Extension From File Name
                                            string FileType = MessageData.AttachmentName;
                                            int posn = FileType.IndexOf(".");
                                            if (posn > 0)
                                                FileType = FileType.Substring(posn + 1, FileType.Length - posn - 1);
                                            else
                                                FileType = "";

                                            // Save Message Attachment
                                            var Attachment = new AttachmentMessage()
                                            {
                                                MessageId = MessageData.MessageId,
                                                PatientId = Convert.ToInt64(MessageResp.PatientId),
                                                FileDirectory = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                                FileName = MessageData.AttachmentName,
                                                DocumentFormat = FileType,
                                                UserId_Created = UserId,
                                                DateCreated = System.DateTime.Now,
                                            };
                                            db.AttachmentMessages.Add(Attachment);

                                            db.SaveChanges();
                                            AttachmentId = Attachment.MessageAttachmentId;

                                            // Write the document to the hard disk
                                            string FileName = AttachmentId + "." + FileType;
                                            FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                                            byte[] imageBytes = Convert.FromBase64String(MessageData.Attachment);
                                            FileHelper.BytesToDisk(imageBytes, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                                            //FileHelper.BytesToDisk(MessageData.Attachment, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);

                                        }

                                        // Add new detail record
                                        var Detail = new MessageDetail()
                                        {

                                            MessageId = MessageData.MessageId,
                                            MessageDetailId = 0,
                                            ProviderId_To = 0,
                                            ProviderId_From = MessageData.ProviderId,
                                            MessageRequest = "",
                                            MessageResponse = MessageData.MessageResponse,
                                            MessageResponseTypeId = details.MessageResponseTypeId,
                                            PreferredPeriod = details.PreferredPeriod,
                                            PreferredTime = details.PreferredTime,
                                            PreferredWeekDay = details.PreferredWeekDay,
                                            VisitReason = details.VisitReason,
                                            MessageUrgency = details.MessageUrgency,
                                            AppointmentStart = Convert.ToDateTime(MessageData.AppointmentStart),
                                            AppointmentEnd = Convert.ToDateTime(MessageData.AppointmentEnd),
                                            ProviderId_Appointment = MessageData.AppointmentProviderId,
                                            MedicationNDC = MessageData.MedicationNDC,
                                            MedicationName = MessageData.MedicationName,
                                            NoOfRefills = MessageData.NoOfRefills,
                                            MedicationStatus = MessageData.MedicationStatus,
                                            PharmacyName = MessageData.PharmacyName,
                                            PharmacyAddress = MessageData.PharmacyAddress,
                                            PharmacyPhone = "",
                                            AttachmentId = AttachmentId.ToString(),
                                            DateCreated = System.DateTime.Now,
                                            MessageRead = false,
                                        };
                                        db.MessageDetails.Add(Detail);

                                        db.SaveChanges();

                                        

                                        // Update the message status to Confirmed
                                        MessageResp.MessageStatusId = 3;
                                        db.SaveChanges();

                                        // Create Audit Record
                                        var Audit = new MessageAudit()
                                        {
                                            MessageId = MessageData.MessageId,
                                            MessageStatusId = 3,
                                            FacilityId = FacilityId,
                                            UserId = UserId,
                                            TDStamp = System.DateTime.Now,
                                        };
                                        db.MessageAudits.Add(Audit);

                                        db.SaveChanges();

                                        SendMessageNotification(Convert.ToInt64(MessageResp.PatientId));
                                    }
                                    else
                                    {
                                        MessageResponse.Valid = false;
                                        MessageResponse.ErrorMessage = "Error reading message detail";
                                    }
                                }
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageResponse.Valid = false;
                    if (ex.Message =="An error occurred while updating the entries. See the inner exception for details.")
                        MessageResponse.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
                        MessageResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageResponse.Valid = false;
                MessageResponse.ErrorMessage = "Invalid Token";
            }
            return MessageResponse;
        }

        private void SendMessageNotification(Int64 PatientId)
        {
            // Send Email to patient notifying of new message
            try
            {
                // Get the patient's setting for notifications

                using (var db = new AMREntities())
                {
                    var results = from p in db.Patients
                                  join w in db.PatientWebSettings on p.PatientId equals w.PatientId
                                  join u in db.Users on PatientId equals u.UserRoleLink
                                  join c in db.Carriers on w.CellCarrier equals c.CarrierId into CarrierGroup
                                  from cg in CarrierGroup.DefaultIfEmpty()
                                  where p.PatientId == PatientId
                                  && u.UserRoleId == 5
                                  select new
                                  {
                                      p.PatientId,
                                      p.Title,
                                      p.FirstName,
                                      p.LastName,
                                      p.MobilePhone,
                                      u.UserEmail,
                                      w.EmailNotifyNewMessage,
                                      w.TextNotifyNewMesssage,
                                      w.CellCarrier,
                                      cg.CarrierURL,
                                  };

                    DataTable dt = clsTableConverter.ToDataTable(results);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToBoolean(dr["EmailNotifyNewMessage"]))
                        {
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;

                            clsEmail objEmail = new clsEmail();

                            string Msg =    "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                            "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                            "	<tr style=\"background-color:#00a0e0;\">" +
                                            "       <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                            "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                            "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                            "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                                            "              </td>" +
                                            "              </tr>" +
                                            "          </table>" +
                                            "       </td>" +
                                            "   </tr>" +
                                            "	<tr>" +
                                            "       <td valign=\"top\">" +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "           <tr style=height:1px;><td></td></tr>" +
                                            "			<tr >" +
                                            "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"] + "</strong>,</h1>  <br />" +
                                            "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                            "               This is a notification that your medical care provider has just sent you a secure message  to your patient portal account. To view and manage secure messages, login to your" +
                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">account</a> and enter your AccessID Code and password. " +
                                            "               <br /><br />" +
                                            "               Once logged in, click on the <a href=\"#\" style=\"color:#37b74a; font-size:18px;font-weight:700;text-decoration:none;\">Messages </a>tab to view and manage the new messages within your portal account. AMR recommends that you promptly review these messages as they contain important information directly from your medical care provider. " +
                                            "               <br /><br />" +
                                            "               Your Member Services Team!<br /><br />" +
                                            "               Please note that your online account is subject to our <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                            "               </span>" +
                                            "             </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "      </td>" +
                                            "   </tr>" +
                                            "	<tr style=\"background-color:#37b74a;\">" +
                                            "      <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "            <tr style=height:10px;>" +
                                            "            <td></td>" +
                                            "            </tr>" +
                                            "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                            "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                            "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                            "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                            "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                            "              </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "          <br /><br />" +
                                            "       </td>  " +
                                            "   </tr>" +
                                            "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                            "</table>";

                            objEmail.SendEmailHTML(dr["UserEMail"].ToString(), "AMR Patient Portal - New Message Waiting", Msg);

                        }
                        if (Convert.ToBoolean(dr["TextNotifyNewMesssage"]))
                        {
                            if (dr["MobilePhone"].ToString().Trim() != "" && dr["CarrierURL"].ToString() != null)
                            {
                                clsEmail objEmail = new clsEmail();

                                string Msg = "You have a message waiting ";
                                string EmailAddress = dr["MobilePhone"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "@" + dr["CarrierURL"].ToString();

                                objEmail.SendEmail(EmailAddress, "AMRPatientPortal", Msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Save New Message Data
        //------------------------------------------------------------------------
        // Save New Message Data 
        //------------------------------------------------------------------------
        [WebMethod(Description = "Post New Message Data")]
        public MessagePostResult NewMessagePost(Int64 FacilityId, Int64 UserId, string Token, NewMessageData MessageData)
        {
            MessagePostResult MessageResponse = new MessagePostResult();
            MessageResponse.Valid = true;
            MessageResponse.ErrorMessage = "";
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Check that provider data is valid
                        Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == MessageData.AMRProviderId);
                        if (ProviderResp != null)
                        {

                            string[] AMRPatientIds = MessageData.AMRPatientIds.Split(',');
                            Int64 MessageId;

                            for (int i = 0; i < AMRPatientIds.Length; i++)
                            {
                                try
                                {
                                    Int64 AMRPatientId = Convert.ToInt64(AMRPatientIds[i]);
                                    // Check that patient and provider data are valid

                                    Patient PatientResp = db.Patients.FirstOrDefault(p => p.PatientId == AMRPatientId);


                                    if (PatientResp != null)
                                    {
                                        // Add Message
                                        var Message = new Message()
                                        {

                                            MessageId = 0,
                                            PatientId = AMRPatientId,
                                            MessageTypeId = 4,
                                            FacilityId = FacilityId,
                                            MessageStatusId = 3,  // Set to confirmed so it does not get picked up by get new message.
                                            UserId_Created = MessageData.AMRProviderId,
                                            CreatedByName = ProviderResp.FirstName + ' ' + ProviderResp.LastName,
                                        };
                                        db.Messages.Add(Message);

                                        db.SaveChanges();

                                        // Assign the message id that was just created back to variable.
                                        MessageId = Message.MessageId;
                                        Int64 AttachmentId = 0;

                                        try
                                        {
                                            if (MessageData.AttachmentName == null)
                                                MessageData.AttachmentName = "";
                                            // Upload Attachment
                                            if (MessageData.AttachmentName != "")
                                            {
                                                // Get Attachment Folder Info & Update Counters
                                                ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 2);
                                                // Update Attachment Counter
                                                if (Config.CurrentFolderDocCntr > 1000)
                                                {
                                                    Config.AttachmentFolderCntr++;
                                                    Config.CurrentFolderDocCntr = 0;
                                                }
                                                Config.CurrentFolderDocCntr++;
                                                db.SaveChanges();

                                                // Parse File Extension From File Name
                                                string FileType = MessageData.AttachmentName;
                                                int posn = FileType.IndexOf(".");
                                                if (posn > 0)
                                                    FileType = FileType.Substring(posn + 1, FileType.Length - posn - 1);
                                                else
                                                    FileType = "";

                                                // Save Message Attachment
                                                var Attachment = new AttachmentMessage()
                                                {
                                                    MessageId = MessageId,
                                                    PatientId = Convert.ToInt64(AMRPatientId),
                                                    FileDirectory = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                                    FileName = MessageData.AttachmentName,
                                                    DocumentFormat = FileType,
                                                    UserId_Created = UserId,
                                                    DateCreated = System.DateTime.Now,
                                                };
                                                db.AttachmentMessages.Add(Attachment);

                                                db.SaveChanges();
                                                AttachmentId = Attachment.MessageAttachmentId;

                                                // Write the document to the hard disk
                                                string FileName = AttachmentId + "." + FileType;
                                                FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                                                byte[] imageBytes = Convert.FromBase64String(MessageData.Attachment);
                                                FileHelper.BytesToDisk(imageBytes, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                                            }
                                        }
                                        catch
                                        { }

                                        var Detail = new MessageDetail()
                                        {

                                            MessageId = MessageId,
                                            MessageDetailId = 0,
                                            ProviderId_To = 0,
                                            ProviderId_From = MessageData.AMRProviderId,
                                            MessageRequest = MessageData.MessageRequest,
                                            MessageResponse = "",
                                            MessageResponseTypeId = 3,
                                            PreferredPeriod = null,
                                            PreferredTime = null,
                                            PreferredWeekDay = null,
                                            VisitReason = null,
                                            MessageUrgency = false,
                                            AppointmentStart = Convert.ToDateTime("1/1/1900"),
                                            AppointmentEnd = Convert.ToDateTime("1/1/1900"),
                                            ProviderId_Appointment = 0,
                                            MedicationNDC = null,
                                            MedicationName = null,
                                            NoOfRefills = 0,
                                            MedicationStatus = 0,
                                            PharmacyName = null,
                                            PharmacyAddress = null,
                                            PharmacyPhone = null,
                                            AttachmentId = AttachmentId.ToString(),
                                            DateCreated = System.DateTime.Now,
                                            MessageRead = false,
                                        };
                                        db.MessageDetails.Add(Detail);

                                        db.SaveChanges();

                                        // Create Audit Record
                                        var Audit = new MessageAudit()
                                        {
                                            MessageId = MessageId,
                                            MessageStatusId = 3,
                                            FacilityId = FacilityId,
                                            UserId = UserId,
                                            TDStamp = System.DateTime.Now,
                                        };
                                        db.MessageAudits.Add(Audit);

                                        db.SaveChanges();

                                        // Send notification of new message in inbox
                                        SendMessageNotification(AMRPatientId);


                                    }

                                    else
                                    {
                                        MessageResponse.Valid = false;
                                        MessageResponse.ErrorMessage += "Patient Id " + AMRPatientId + " invalid, ";

                                    }
                                }
                                catch
                                {
                                    MessageResponse.Valid = false;
                                    MessageResponse.ErrorMessage += "Patient Id " + AMRPatientIds[i] + " invalid, ";
                                }
                            }

                        }
                        else
                        {
                            MessageResponse.Valid = false;
                            MessageResponse.ErrorMessage = "Message ProviderId is invalid";
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageResponse.Valid = false;
                    if (ex.Message =="An error occurred while updating the entries. See the inner exception for details.")
                        MessageResponse.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
                        MessageResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageResponse.Valid = false;
                MessageResponse.ErrorMessage = "Invalid Token";
            }
            return MessageResponse;
        }



        #endregion

        #region Get Message List
        //------------------------------------------------------------------------
        // Get Message List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Waiting Messages")]
        public MessageTableData MessageGetWaiting(Int64 FacilityId, Int64 UserId, string Token)
        {
            MessageTableData Messages = new MessageTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from m in db.vwMessageGetWaitings
                                      where m.FacilityId == FacilityId
                                      select new
                                      {
                                          m.MessageId,
                                          m.AMRPatientId,
                                          m.MessageTypeId,
                                          m.ToProvider,
                                          m.MessageRequest,
                                          m.PreferredResponseBy,
                                          m.PreferredPeriod,
                                          m.PreferredTime,
                                          m.PreferredDay,
                                          m.VisitReason,
                                          m.VisitUrgency,
                                          m.Medication,
                                          m.Pharmacy,
                                          m.AppointmentStart,
                                          m.AppointmentEnd
                                      };
                        Messages.dt = clsTableConverter.ToDataTable(results);

                        Messages.dt.TableName = "Messages";

                        // Mark Each Message As Picked Up

                        //foreach (DataRow dr in Messages.dt.Rows)
                        //{
                        //    Int64 MessageId = Convert.ToInt64(dr["MessageId"]);
                        //    Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageId);

                        //    if (MessageResp != null)
                        //    {
                        //        MessageResp.MessageStatusId = 2;  //  Pending
                        //        db.SaveChanges();

                        //        // Create Audit Record
                        //        var Audit = new MessageAudit()
                        //        {
                        //            MessageId = MessageId,
                        //            MessageStatusId = 2,  //  Pending
                        //            FacilityId = FacilityId,
                        //            UserId = UserId,
                        //            TDStamp = System.DateTime.Now,
                        //        };
                        //        db.MessageAudits.Add(Audit);

                        //        db.SaveChanges();
                        //    }
                        //}
                        Messages.Valid = true;
                        Messages.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    Messages.Valid = false;
                    Messages.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Messages.Valid = false;
                Messages.ErrorMessage = "Invalid Token";
            }
            return Messages;
        }
        #endregion

        #region Get Message Cancelled List
        //------------------------------------------------------------------------
        // Get Message Cancelled List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Cancelled Messages")]
        public MessageTableData MessageGetCancelled(Int64 FacilityId, Int64 UserId, string Token)
        {
            MessageTableData Messages = new MessageTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from m in db.vwMessageGetCancelleds
                                      where m.FacilityId == FacilityId
                                      select new
                                      {
                                          m.MessageId,
                                          m.AMRPatientId,
                                          m.MessageTypeId
                                      };
                        Messages.dt = clsTableConverter.ToDataTable(results);

                        Messages.dt.TableName = "Messages";

                        Messages.Valid = true;
                        Messages.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    Messages.Valid = false;
                    Messages.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Messages.Valid = false;
                Messages.ErrorMessage = "Invalid Token";
            }
            return Messages;
        }
        #endregion

        #region Message Received List
        //------------------------------------------------------------------------
        //  Message Received List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Update List Of Messages Received")]
        public MessagePostResult MessagePostReceived(Int64 FacilityId, Int64 UserId, string Token, MessageReceived MessageReceived)
        {
            MessagePostResult MessageResponse = new MessagePostResult();
            MessageResponse.Valid = true;
            MessageResponse.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Mark Each Message As Picked Up

                        foreach (DataRow dr in MessageReceived.dt.Rows)
                        {
                            Int64 MessageId = Convert.ToInt64(dr["MessageId"]);
                            Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageId);

                            if (MessageResp != null)
                            {
                                int NewStatus = 0;
                                if (MessageResp.MessageStatusId == 1)
                                    NewStatus = 2;  // Pending
                                else
                                    NewStatus = 8;  // Cancelled Completed


                                MessageResp.MessageStatusId = NewStatus;
                                db.SaveChanges();

                                // Create Audit Record
                                var Audit = new MessageAudit()
                                {
                                    MessageId = MessageId,
                                    MessageStatusId = NewStatus, 
                                    FacilityId = FacilityId,
                                    UserId = UserId,
                                    TDStamp = System.DateTime.Now,
                                };
                                db.MessageAudits.Add(Audit);

                                db.SaveChanges();
                            }
                        }
                        MessageResponse.Valid = true;
                        MessageResponse.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageResponse.Valid = false;
                    MessageResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageResponse.Valid = false;
                MessageResponse.ErrorMessage = "Invalid Token";
            }
            return MessageResponse;
        }
        #endregion

        #region Message Received List CSV List
        //------------------------------------------------------------------------
        //  Message Received List CSV List
        //------------------------------------------------------------------------

        [WebMethod(Description = "Update List Of Messages Received -CSV List")]
        public MessagePostResult MessagePostReceivedList(Int64 FacilityId, Int64 UserId, string Token, string MessageReceivedList)
        {
            MessagePostResult MessageResponse = new MessagePostResult();
            MessageResponse.Valid = true;
            MessageResponse.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        // Mark Each Message As Picked Up

                        string[] MessageIds = MessageReceivedList.Split(',');

                        for (int i = 0; i < MessageIds.Length; i++)
                        {
                            try
                            {
                                Int64 MessageId = Convert.ToInt64(MessageIds[i]);

                                Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageId);

                                if (MessageResp != null)
                                {
                                    int NewStatus = 0;
                                    if (MessageResp.MessageStatusId == 1)
                                        NewStatus = 2;  // Pending
                                    else
                                        NewStatus = 8;  // Cancelled Completed

                                    MessageResp.MessageStatusId = NewStatus;
                                    db.SaveChanges();

                                    // Create Audit Record
                                    var Audit = new MessageAudit()
                                    {
                                        MessageId = MessageId,
                                        MessageStatusId = NewStatus,  
                                        FacilityId = FacilityId,
                                        UserId = UserId,
                                        TDStamp = System.DateTime.Now,
                                    };
                                    db.MessageAudits.Add(Audit);

                                    db.SaveChanges();
                                }
                            }
                            catch
                            {
                            }
                        }
                        MessageResponse.Valid = true;
                        MessageResponse.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageResponse.Valid = false;
                    MessageResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageResponse.Valid = false;
                MessageResponse.ErrorMessage = "Invalid Token";
            }
            return MessageResponse;
        }
        #endregion

        #region Save DirectReceived Data
        //------------------------------------------------------------------------
        // Save DirectReceived Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post DirectReceived Data")]
        public DirectReceivedResp DirectReceivedPost(Int64 FacilityId, Int64 UserId, string Token, DirectReceivedData DirectData)
        {
            DirectReceivedResp Response = new DirectReceivedResp();
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
                    using (var db = new AMREntities())
                    {
                        // Add DirectReceived Message To Database
                        var DirectReceived = new DirectReceived()
                        {
                            DirectReceivedId = DirectData.DirectReceivedId,
                            Processed = DirectData.Processed,
                            DateProcessed = Convert.ToDateTime(DirectData.DateProcessed),
                            PatientId = DirectData.PatientId,
                            FacilityId = DirectData.FacilityId,
                            EmailId = DirectData.EmailId,
                            FromEmail = DirectData.FromEmail,
                            ToEmail = DirectData.ToEmail,
                            Body = DirectData.Body,
                            Attachment = DirectData.Attachment,
                            Message = DirectData.Message,
                        };

                        db.DirectReceiveds.Add(DirectReceived);

                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Response.Valid = false;
                    if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                        Response.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
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

        #region Get Direct Received List
        //------------------------------------------------------------------------
        // Get Direct Received List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get List Of Direct Received Messages")]
        public MessageTableData DirectReceivedGetList(Int64 FacilityId, Int64 UserId, string Token, Int64 FacilityIdSearch, string StartDate, string EndDate)
        {
            MessageTableData Messages = new MessageTableData();

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    using (var db = new AMREntities())
                    {
                        var results = from d in db.DirectReceiveds
                                      join p in db.Patients on d.PatientId equals p.PatientId
                                      join f in db.Facilities on d.FacilityId equals f.FacilityId
                                      where d.FacilityId == FacilityIdSearch
                                      && d.DateProcessed >= Convert.ToDateTime(StartDate)
                                      && d.DateProcessed <= Convert.ToDateTime(EndDate)
                                      select new
                                      {
                                          d.DirectReceivedId,
                                          d.Processed,
                                          d.DateProcessed,
                                          d.PatientId,
                                          p.FirstName,
                                          p.LastName,
                                          d.FacilityId,
                                          f.FacilityName,
                                          d.EmailId,
                                          d.FromEmail,
                                          d.ToEmail,
                                          d.Body,
                                          d.Attachment,
                                          d.Message
                                      };
                        Messages.dt = clsTableConverter.ToDataTable(results);

                        Messages.dt.TableName = "DirectReceived";
                        Messages.Valid = true;
                        Messages.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    Messages.Valid = false;
                    Messages.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Messages.Valid = false;
                Messages.ErrorMessage = "Invalid Token";
            }
            return Messages;
        }
        #endregion
        
    }
}
