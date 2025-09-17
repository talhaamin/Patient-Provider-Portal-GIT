// Service Name  : MessageService
// Date Created  : 11/06/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Patient Messaging
// MM/DD/YYYY XXX Description               
// 11/11/2013 SJF Added Code Tables For Messaging
// 12/11/2013 SJF Added Message Attachments
// 12/31/2013 SJF Added DateCreated to MessageDetail
// 01/08/2014 SJF Added Get MessageType General Codes, GetMessageListProvider
// 08/22/2014 SJF Added DirectReceived
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

namespace AMR.DataService
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
            public bool Valid;
            public string ErrorMessage;
            public Int64 MessageId;
            public Int64 PatientId;
            public int MessageTypeId;
            public Int64 FacilityId;
            public int MessageStatusId;
            public Int64 User_Id_Created;
            public string CreatedByName;
            public Int64 MessageDetailId;
            public Int64? ProviderId_To;
            public Int64? ProviderId_From;
            public string MessageRequest;
            public string MessageResponse;
            public Int32 MessageResponseTypeId;
            public String PreferredPeriod;
            public string PreferredTime;
            public string PreferredWeekDay;
            public string VisitReason;
            public bool MessageUrgency;
            public DateTime AppointmentStart;
            public DateTime AppointmentEnd;
            public Int64 ProviderId_Appointment;
            public string MedicationNDC;
            public string MedicationName;
            public int NoOfRefills;
            public int MedicationStatus;
            public string PharmacyName;
            public string PharmacyAddress;
            public string PharmacyPhone;
            public bool MessageRead;
            public string AttachmentId;
            public string AttachmentName;
            public byte[] Attachment;
            
            public DataTable dtDetails;


        }
        public struct MessageParms
        {
            public int Option;
            public Int64 MessageId;
            public Int64 PatientId;
            public Int64 ProviderId;
        }

        public struct MessageTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct CodeTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct MessageAttachmentData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 MessageAttachmentId;
            public Int64 MessageId;
            public Int64 PatientId;
            public string FileDirectory;
            public string FileName;
            public string DocumentFormat;
            public byte[] DocumentImage;
        }
        public struct MessageUnread
        {
            public bool Valid;
            public string ErrorMessage;
            public int TotalMessages;
            public int AppointmentMessages;
            public int MedicationMessages;
            public int ReferralMessages;
            public int GeneralMessages;
        }
        public struct EmailMessageData
        {
            public bool Valid;
            public string ErrorMessage;
            public string To;
            public string Subject;
            public string Body;
        }

        #endregion


        #region Get Message Data
        //------------------------------------------------------------------------
        // Get Message Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message Data")]
        public MessageData GetMessageData(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageData Message = new MessageData();

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
                        Message results = db.Messages.FirstOrDefault(m => m.MessageId == Parms.MessageId);

                        if (results != null)
                        {
                            Message.Valid = true;
                            Message.ErrorMessage = "";
                            Message.MessageId = results.MessageId;
                            Message.PatientId = Convert.ToInt64(results.PatientId);
                            Message.MessageTypeId = Convert.ToInt32(results.MessageTypeId);
                            Message.FacilityId = Convert.ToInt64(results.FacilityId);
                            Message.MessageStatusId = Convert.ToInt32(results.MessageStatusId);
                            Message.User_Id_Created = Convert.ToInt64(results.UserId_Created);
                            Message.CreatedByName = results.CreatedByName;

                            if (Message.MessageTypeId == 1) // Appointment Request
                            {
                                var Details = from d in db.MessageDetails
                                              join p1 in db.Providers on d.ProviderId_To equals p1.ProviderId into outerTo
                                              from y in outerTo.DefaultIfEmpty()
                                              join p2 in db.Providers on d.ProviderId_From equals p2.ProviderId into outerFrom
                                              from x in outerFrom.DefaultIfEmpty()
                                              join p3 in db.Providers on d.ProviderId_Appointment equals p3.ProviderId into outerApp
                                              from z in outerApp.DefaultIfEmpty()
                                              join c1 in db.C_MessageResponseType on d.MessageResponseTypeId equals c1.MessageResponseTypeId
                                              join m in db.Messages on d.MessageId equals m.MessageId
                                              join c3 in db.C_MessageType on m.MessageTypeId equals c3.MessageTypeId
                                              join c4 in db.C_MessageStatus on m.MessageStatusId equals c4.MessageStatusId
                                              join f in db.Facilities on m.FacilityId equals f.FacilityId // Added by Talha
                                              join At in db.AttachmentMessages on d.MessageId equals At.MessageId into outerAtt
                                              from ax in outerAtt.DefaultIfEmpty() //Added By Talha
                                              where d.MessageId == Parms.MessageId
                                              select new
                                              {
                                                  d.MessageDetailId,
                                                  d.ProviderId_To,
                                                  ProviderName_To = (d.ProviderId_To==0? m.CreatedByName:y.FullName),
                                                  d.ProviderId_From,
                                                  ProviderName_From = (d.ProviderId_From == 0 ? m.CreatedByName : x.FullName),
                                                  d.MessageRequest,
                                                  d.MessageResponse,
                                                  d.MessageResponseTypeId,
                                                  MessageResponseType = c1.Value,
                                                  d.PreferredPeriod,
                                                  d.PreferredTime,
                                                  d.PreferredWeekDay,
                                                  d.VisitReason,
                                                  d.MessageUrgency,
                                                  d.AppointmentStart,
                                                  d.AppointmentEnd,
                                                  d.ProviderId_Appointment,
                                                  ProviderName_Appointment = z.FullName,
                                                  d.MedicationNDC,
                                                  d.MedicationName,
                                                  d.NoOfRefills,
                                                  d.MedicationStatus,
                                                  d.PharmacyName,
                                                  d.PharmacyAddress,
                                                  d.PharmacyPhone, // SJF Added 8/25/14
                                                  d.MessageRead, // SJF Added 3/7/14
                                                  d.AttachmentId,
                                                  m.CreatedByName,// Added by Talha
                                                  MessageType = c3.Value,  // Added by Talha
                                                  c3.MessageTypeId,//Added this field by Talha
                                                  m.FacilityId,//Added this field by Talha
                                                  f.FacilityName,//Added this field by Talha
                                                  MessageStatus=c4.Value, //Added by Talha
                                                  ax.DocumentFormat, //Added by Talha
                                                  d.DateCreated,
                                                  m.MessageId //Added By Talha
                                              };
                                Message.dtDetails = clsTableConverter.ToDataTable(Details);
                                Message.dtDetails.TableName = "Details";
                            }
                            else if (Message.MessageTypeId == 2) // Medication Refill
                            {
                                var Details = from d in db.MessageDetails
                                              join p1 in db.Providers on d.ProviderId_To equals p1.ProviderId into outerTo
                                              from y in outerTo.DefaultIfEmpty()
                                              join p2 in db.Providers on d.ProviderId_From equals p2.ProviderId into outerFrom
                                              from x in outerFrom.DefaultIfEmpty()
                                              join c1 in db.C_MessageResponseType on d.MessageResponseTypeId equals c1.MessageResponseTypeId
                                              join m in db.Messages on d.MessageId equals m.MessageId
                                              join c3 in db.C_MessageType on m.MessageTypeId equals c3.MessageTypeId
                                              join c4 in db.C_MessageStatus on m.MessageStatusId equals c4.MessageStatusId
                                              join f in db.Facilities on m.FacilityId equals f.FacilityId // Added by Talha
                                              join At in db.AttachmentMessages on d.MessageId equals At.MessageId into outerAtt
                                              from ax in outerAtt.DefaultIfEmpty() //Added By Talha
                                              where d.MessageId == Parms.MessageId
                                              select new
                                              {
                                                  d.MessageDetailId,
                                                  d.ProviderId_To,
                                                  ProviderName_To = (d.ProviderId_To == 0 ? m.CreatedByName : y.FullName),
                                                  d.ProviderId_From,
                                                  ProviderName_From = (d.ProviderId_From == 0 ? m.CreatedByName : x.FullName),
                                                  d.MessageRequest,
                                                  d.MessageResponse,
                                                  d.MessageResponseTypeId,
                                                  MessageResponseType = c1.Value,
                                                  d.PreferredPeriod,
                                                  d.PreferredTime,
                                                  d.PreferredWeekDay,
                                                  d.VisitReason,
                                                  d.MessageUrgency,
                                                  d.AppointmentStart,
                                                  d.AppointmentEnd,
                                                  d.ProviderId_Appointment,
                                                  ProviderName_Appointment = "",
                                                  d.MedicationNDC,
                                                  d.MedicationName,
                                                  d.NoOfRefills,
                                                  d.MedicationStatus,
                                                  d.PharmacyName,
                                                  d.PharmacyAddress,
                                                  d.PharmacyPhone,  // SJF Added 8/25/14
                                                  d.MessageRead, // SJF Added 3/7/14
                                                  d.AttachmentId,
                                                  m.CreatedByName,// Added by Talha
                                                  MessageType = c3.Value, // Added by Talha
                                                  c3.MessageTypeId,//Added this field by Talha
                                                  MessageStatus=c4.Value, //Added by Talha
                                                  m.FacilityId,//Added this field by Talha
                                                  f.FacilityName,//Added this field by Talha
                                                  ax.DocumentFormat, //Added by Talha
                                                  d.DateCreated,
                                                  m.MessageId //Added By Talha
                                              };
                                Message.dtDetails = clsTableConverter.ToDataTable(Details);
                                Message.dtDetails.TableName = "Details";
                            }
                            else if (Message.MessageTypeId == 5) // Referral
                            {
                                var Details = from d in db.MessageDetails
                                              join p1 in db.Providers on d.ProviderId_To equals p1.ProviderId into outerTo
                                              from y in outerTo.DefaultIfEmpty()
                                              join p2 in db.Providers on d.ProviderId_From equals p2.ProviderId into outerFrom
                                              from x in outerFrom.DefaultIfEmpty()
                                              join c1 in db.C_MessageResponseType on d.MessageResponseTypeId equals c1.MessageResponseTypeId
                                              join m in db.Messages on d.MessageId equals m.MessageId
                                              join c3 in db.C_MessageType on m.MessageTypeId equals c3.MessageTypeId
                                              join c4 in db.C_MessageStatus on m.MessageStatusId equals c4.MessageStatusId
                                              join f in db.Facilities on m.FacilityId equals f.FacilityId // Added by Talha
                                              join At in db.AttachmentMessages on d.MessageId equals At.MessageId into outerAtt
                                              from ax in outerAtt.DefaultIfEmpty() //Added By Talha
                                              where d.MessageId == Parms.MessageId
                                              select new
                                              {
                                                  d.MessageDetailId,
                                                  d.ProviderId_To,
                                                  ProviderName_To = (d.ProviderId_To == 0 ? m.CreatedByName : y.FullName),
                                                  d.ProviderId_From,
                                                  ProviderName_From = (d.ProviderId_From == 0 ? m.CreatedByName : x.FullName),
                                                  d.MessageRequest,
                                                  d.MessageResponse,
                                                  d.MessageResponseTypeId,
                                                  MessageResponseType = c1.Value,
                                                  d.PreferredPeriod,
                                                  d.PreferredTime,
                                                  d.PreferredWeekDay,
                                                  d.VisitReason,
                                                  d.MessageUrgency,
                                                  d.AppointmentStart,
                                                  d.AppointmentEnd,
                                                  d.ProviderId_Appointment,
                                                  ProviderName_Appointment = "",
                                                  d.MedicationNDC,
                                                  d.MedicationName,
                                                  d.NoOfRefills,
                                                  d.MedicationStatus,
                                                  d.PharmacyName,
                                                  d.PharmacyAddress,
                                                  d.PharmacyPhone, // SJF Added 8/25/14
                                                  d.MessageRead, // SJF Added 3/7/14
                                                  d.AttachmentId,
                                                  m.CreatedByName,// Added by Talha
                                                  MessageType = c3.Value, // Added by Talha
                                                  MessageStatus=c4.Value, //Added by Talha
                                                  c3.MessageTypeId,//Added this field by Talha
                                                  m.FacilityId,//Added this field by Talha
                                                  f.FacilityName,//Added this field by Talha
                                                  ax.DocumentFormat, //Added by Talha
                                                  d.DateCreated,
                                                  m.MessageId //Added By Talha
                                              };
                                Message.dtDetails = clsTableConverter.ToDataTable(Details);
                                Message.dtDetails.TableName = "Details";
                            }
                            else // Billing & General
                            {
                                //Ahmed Saidi: fixed Joins added another outer join to get the messages request and response
                                //Talha Amin: Added left outer join in the below query
                                //Talha Amin: Added two more joins with C_MessageType and Message table for getting MessageType field in the select list
                                var Details = from d in db.MessageDetails
                                              join p1 in db.Providers on d.ProviderId_To equals p1.ProviderId into outerTo
                                              from y in outerTo.DefaultIfEmpty()
                                              join p2 in db.Providers on d.ProviderId_From equals p2.ProviderId into outerFrom
                                              from x in outerFrom.DefaultIfEmpty()
                                              join c1 in db.C_MessageResponseType on d.MessageResponseTypeId equals c1.MessageResponseTypeId
                                              join m in db.Messages on d.MessageId equals m.MessageId
                                              join c3 in db.C_MessageType on m.MessageTypeId equals c3.MessageTypeId
                                               join c4 in db.C_MessageStatus on m.MessageStatusId equals c4.MessageStatusId
                                              join f in db.Facilities on m.FacilityId equals f.FacilityId // Added by Talha
                                              join At in db.AttachmentMessages on d.MessageId equals At.MessageId into outerAtt
                                              from ax in outerAtt.DefaultIfEmpty() //Added By Talha
                                              where d.MessageId == Parms.MessageId
                                              select new
                                              {
                                                  m.FacilityId,//Added this field by Talha
                                                  c3.MessageTypeId,//Added this field by Talha
                                                  MessageType = c3.Value, //Added this field by Talha
                                                  f.FacilityName,//Added this field by Talha
                                                  d.MessageDetailId,
                                                  d.ProviderId_To,
                                                  ProviderName_To = (d.ProviderId_To == 0 ? m.CreatedByName : y.FullName),
                                                  d.ProviderId_From,
                                                  ProviderName_From = (d.ProviderId_From == 0 ? m.CreatedByName : x.FullName),
                                                  d.MessageRequest,
                                                  d.MessageResponse,
                                                  d.MessageResponseTypeId,
                                                  MessageResponseType = c1.Value,
                                                  d.PreferredPeriod,
                                                  d.PreferredTime,
                                                  d.PreferredWeekDay,
                                                  d.VisitReason,
                                                  d.MessageUrgency,
                                                  d.AppointmentStart,
                                                  d.AppointmentEnd,
                                                  d.ProviderId_Appointment,
                                                  ProviderName_Appointment = "",
                                                  d.MedicationNDC,
                                                  d.MedicationName,
                                                  d.NoOfRefills,
                                                  d.MedicationStatus,
                                                  d.PharmacyName,
                                                  d.PharmacyAddress,
                                                  d.PharmacyPhone, // SJF Added 8/25/14
                                                  d.MessageRead, // SJF Added 3/7/14
                                                  d.AttachmentId,
                                                  m.CreatedByName,// Added by Talha
                                                  MessageStatus=c4.Value, //Added by Talha
                                                  ax.DocumentFormat, //Added by Talha
                                                  d.DateCreated,
                                                  m.MessageId //Added By Talha
                                              };
                                Message.dtDetails = clsTableConverter.ToDataTable(Details);
                                Message.dtDetails.TableName = "Details";
                            }

                            

                            // Create Audit Record
                            var Audit = new MessageAudit()
                            {
                                MessageId = Parms.MessageId,
                                MessageStatusId = 6,  // Message Was Read
                                FacilityId = FacilityId,
                                UserId = UserId,
                                TDStamp = System.DateTime.Now,
                            };
                            db.MessageAudits.Add(Audit);

                            db.SaveChanges();
                        }
                        else
                        {
                            Message.Valid = false;
                            Message.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Valid = false;
                    Message.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Message.Valid = false;
                Message.ErrorMessage = "Invalid Token";
            }
            return Message;
        }
        #endregion

        //Added By Talha
        #region Update MessageRead Flag Data
        //------------------------------------------------------------------------
        // Update Message Read Flag Data
        //------------------------------------------------------------------------
        [WebMethod(Description = "Update MessageRead Flag Data")]
        public MessageData UpdateMessageReadFlagData(MessageData MessageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageData.Valid = true;
            MessageData.ErrorMessage = "";

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
                    // Update Message Info 
                        MessageDetail MsgDetail = db.MessageDetails.FirstOrDefault(p => p.MessageDetailId == MessageData.MessageDetailId);
                        MsgDetail.MessageRead = true;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageData.Valid = false;
                    MessageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageData.Valid = false;
                MessageData.ErrorMessage = "Invalid Token";
            }
            return MessageData;
        }
        #endregion

        #region Save Message Data
        //------------------------------------------------------------------------
        // Save Message Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Message Data")]
        public MessageData SaveMessageData(MessageData MessageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageData.Valid = true;
            MessageData.ErrorMessage = "";

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
                            // Add Message
                            var Message = new Message()
                            {

                                MessageId = MessageData.MessageId,
                                PatientId = MessageData.PatientId,
                                MessageTypeId = MessageData.MessageTypeId,
                                FacilityId = MessageData.FacilityId,
                                MessageStatusId = MessageData.MessageStatusId,
                                UserId_Created = MessageData.User_Id_Created,
                                CreatedByName = MessageData.CreatedByName,
                            };
                            db.Messages.Add(Message);

                            db.SaveChanges();

                            // Assign the message id that was just created back to variable.
                            MessageData.MessageId = Message.MessageId;
                            Int64 AttachmentId = 0;

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
                                    MessageId = MessageData.MessageId,
                                    PatientId = Convert.ToInt64(MessageData.PatientId),
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
                                FileHelper.BytesToDisk(MessageData.Attachment, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);

                            }

                            var Detail = new MessageDetail()
                            {

                                MessageId = MessageData.MessageId,
                                MessageDetailId = MessageData.MessageDetailId,
                                ProviderId_To = MessageData.ProviderId_To,
                                ProviderId_From = MessageData.ProviderId_From,
                                MessageRequest = MessageData.MessageRequest,
                                MessageResponse = MessageData.MessageResponse,
                                MessageResponseTypeId = MessageData.MessageResponseTypeId,
                                PreferredPeriod = MessageData.PreferredPeriod,
                                PreferredTime = MessageData.PreferredTime,
                                PreferredWeekDay = MessageData.PreferredWeekDay,
                                VisitReason = MessageData.VisitReason,
                                MessageUrgency = MessageData.MessageUrgency,
                                AppointmentStart = MessageData.AppointmentStart,
                                AppointmentEnd = MessageData.AppointmentEnd,
                                ProviderId_Appointment = MessageData.ProviderId_Appointment,
                                MedicationNDC = MessageData.MedicationNDC,
                                MedicationName = MessageData.MedicationName,
                                NoOfRefills = MessageData.NoOfRefills,
                                MedicationStatus = MessageData.MedicationStatus,
                                PharmacyName = MessageData.PharmacyName,
                                PharmacyAddress = MessageData.PharmacyAddress,
                                PharmacyPhone = MessageData.PharmacyPhone,   // SJF Added 8/25/14
                                AttachmentId = AttachmentId.ToString(),
                                DateCreated = System.DateTime.Now,
                                MessageRead = MessageData.MessageRead,
                            };
                            db.MessageDetails.Add(Detail);

                            db.SaveChanges();    
    
                            // Create Audit Record
                            var Audit = new MessageAudit()
                            {
                                MessageId = MessageData.MessageId,
                                MessageStatusId = MessageData.MessageStatusId,
                                FacilityId = FacilityId,
                                UserId = UserId,
                                TDStamp = System.DateTime.Now,
                            };
                            db.MessageAudits.Add(Audit);

                            db.SaveChanges();

                            // Send notification of new message in inbox
                            if (MessageData.ProviderId_To == 0 && MessageData.ProviderId_From > 0)
                            {
                                SendMessageNotification(MessageData.PatientId);
                            }

                        }
                        else
                        {
                            // Update Message Info 

                            Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageData.MessageId);

                            if (MessageResp != null)
                            {

                                MessageResp.MessageStatusId = MessageData.MessageStatusId;
                            }
                            db.SaveChanges();

                            if (MessageData.MessageDetailId == -1)
                            {
                                // Just a status change do nothing
                            }
                            else
                            {
                                // Add new detail record
                                var Detail = new MessageDetail()
                                {

                                    MessageId = MessageData.MessageId,
                                    MessageDetailId = MessageData.MessageDetailId,
                                    ProviderId_To = MessageData.ProviderId_To,
                                    ProviderId_From = MessageData.ProviderId_From,
                                    MessageRequest = MessageData.MessageRequest,
                                    MessageResponse = MessageData.MessageResponse,
                                    MessageResponseTypeId = MessageData.MessageResponseTypeId,
                                    PreferredPeriod = MessageData.PreferredPeriod,
                                    PreferredTime = MessageData.PreferredTime,
                                    PreferredWeekDay = MessageData.PreferredWeekDay,
                                    VisitReason = MessageData.VisitReason,
                                    MessageUrgency = MessageData.MessageUrgency,
                                    AppointmentStart = MessageData.AppointmentStart,
                                    AppointmentEnd = MessageData.AppointmentEnd,
                                    ProviderId_Appointment = MessageData.ProviderId_Appointment,
                                    MedicationNDC = MessageData.MedicationNDC,
                                    MedicationName = MessageData.MedicationName,
                                    NoOfRefills = MessageData.NoOfRefills,
                                    MedicationStatus = MessageData.MedicationStatus,
                                    PharmacyName = MessageData.PharmacyName,
                                    PharmacyAddress = MessageData.PharmacyAddress,
                                    PharmacyPhone = MessageData.PharmacyPhone,  // SJF 8/25/14
                                    AttachmentId = MessageData.AttachmentId,
                                    MessageRead = MessageData.MessageRead,
                                    DateCreated = System.DateTime.Now,
                                };
                                db.MessageDetails.Add(Detail);

                                db.SaveChanges();        
                            }
                            // Create Audit Record
                            var Audit = new MessageAudit()
                            {
                                MessageId = MessageData.MessageId,
                                MessageStatusId = MessageData.MessageStatusId,
                                FacilityId = FacilityId,
                                UserId = UserId,
                                TDStamp = System.DateTime.Now,
                            };
                            db.MessageAudits.Add(Audit);

                            db.SaveChanges();

                            // Send notification of new message in inbox
                            if (MessageData.ProviderId_To == 0 && MessageData.ProviderId_From > 0)
                            {                              
                                var MsgResults = db.Messages.Find(MessageData.MessageId);
                                if (MsgResults != null)
                                    SendMessageNotification(Convert.ToInt64(MsgResults.PatientId));

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageData.Valid = false;
                    MessageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageData.Valid = false;
                MessageData.ErrorMessage = "Invalid Token";
            }
            return MessageData;
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
                            clsEmail objEmail = new clsEmail();
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;

                            string Msg =    "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                            "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                            "	<tr style=\"background-color:#00a0e0;\">" +
                                            "       <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                            "			<tr style=\"width:571px;display:block;background-color:#fff;\">" +
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
                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + dr["Title"] + " " + dr["FirstName"]  + " " + dr["LastName"] + "</strong>,</h1>  <br />" +
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
                                            "     </tr>" +
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

        #region Cancel Message Data
        //------------------------------------------------------------------------
        // Cancel Message  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Cancel Message")]
        public MessageData CancelMessage(MessageData MessageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageData.Valid = true;
            MessageData.ErrorMessage = "";

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

                        // Update Message Info 

                        Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageData.MessageId);

                        if (MessageResp != null)
                        {
                            MessageResp.MessageStatusId = 5;  //  Cancelled
                            db.SaveChanges();

                            // Create Audit Record
                            var Audit = new MessageAudit()
                            {
                                MessageId = MessageData.MessageId,
                                MessageStatusId = 5,  //  Cancelled
                                FacilityId = FacilityId,
                                UserId = UserId,
                                TDStamp = System.DateTime.Now,
                            };
                            db.MessageAudits.Add(Audit);

                            db.SaveChanges();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    MessageData.Valid = false;
                    MessageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageData.Valid = false;
                MessageData.ErrorMessage = "Invalid Token";
            }
            return MessageData;
        }
        #endregion

        #region Delete Message
        //------------------------------------------------------------------------
        // Delete Message  
        //------------------------------------------------------------------------

        [WebMethod(Description = "Delete Message")]
        public MessageData DeleteMessage(MessageData MessageData, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageData.Valid = true;
            MessageData.ErrorMessage = "";

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

                        // Update Message Info 

                        Message MessageResp = db.Messages.FirstOrDefault(p => p.MessageId == MessageData.MessageId);

                        if (MessageResp != null)
                        {
                            MessageResp.MessageStatusId = 7;  //  Deleted
                            db.SaveChanges();

                            // Create Audit Record
                            var Audit = new MessageAudit()
                            {
                                MessageId = MessageData.MessageId,
                                MessageStatusId = 7,  //  Deleted
                                FacilityId = FacilityId,
                                UserId = UserId,
                                TDStamp = System.DateTime.Now,
                            };
                            db.MessageAudits.Add(Audit);

                            db.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageData.Valid = false;
                    MessageData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageData.Valid = false;
                MessageData.ErrorMessage = "Invalid Token";
            }
            return MessageData;
        }
        #endregion

        #region Get Message List
        //------------------------------------------------------------------------
        // Get Message List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message List")]
        public MessageTableData GetMessageList(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                        var results = from m in db.vwMessageSummaries
                                      where m.PatientId == Parms.PatientId
                                      select new
                                      {
                                          m.MessageId,
                                          m.FacilityId,
                                          m.FacilityName,
                                          m.MessageTypeId,
                                          m.MessageType,
                                          m.MessageStatusId,
                                          m.MessageStatus,
                                          m.MedicationName,
                                          m.MessageRequest,
                                          m.PatientId,
                                          m.PharmacyName,
                                          m.PreferredPeriod,
                                          m.PreferredTime,
                                          m.PreferredWeekDay,
                                          m.MessageUrgency,
                                          m.ProviderId_From,
                                          m.ProviderName_From,
                                          m.ProviderId_To,
                                          m.ProviderName_To,
                                          m.VisitReason,
                                          m.UserId_Created,
                                          m.CreatedByName,
                                          m.MessageResponse,
                                          m.MessageResponseTypeId,
                                          m.AppointmentStart,
                                          m.AppointmentEnd,
                                          m.ProviderId_Appointment,
                                          m.ProviderName_Appointment,
                                          m.MedicationNDC,
                                          m.NoOfRefills,
                                          m.MedicationStatus,
                                          m.PharmacyAddress,
                                          m.PharmacyPhone,  // SJF 8/25/14
                                          m.AttachmentId,
                                          //Added By Talha
                                          MessageUnRead = (
                                          from ms in db.MessageDetails
                                          where ms.MessageId == m.MessageId && ms.MessageRead == false 
                                          //&& ms.ProviderId_From != 0
                                          select ms.MessageRead
                                          ).Count(),
                                          ProviderApprDate = (
                                          from md in db.MessageDetails
                                          where md.MessageId == m.MessageId
                                          orderby md.AppointmentStart descending
                                          select md.AppointmentStart
                                          ).FirstOrDefault(),
                                          ProviderApprTime = (
                                          from md in db.MessageDetails
                                          where md.MessageId == m.MessageId
                                          orderby md.MessageDetailId descending
                                          select md.PreferredTime
                                          ).FirstOrDefault(),
                                          DateCreated = (from ms in db.MessageDetails
                                          where ms.MessageId == m.MessageId
										  orderby ms.DateCreated descending
                                          select ms.DateCreated).FirstOrDefault()
                                      };
                        results = results.OrderByDescending(m => m.DateCreated);
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

        #region Get Message List Provider
        //------------------------------------------------------------------------
        // Get Message List Provider
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message List Provider")]
        public MessageTableData GetMessageListProvider(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                        var results = from m in db.vwMessageSummaries
                                      where m.ProviderId_To == Parms.ProviderId || m.ProviderId_From == Parms.ProviderId
                                      select new
                                      {
                                          m.MessageId,
                                          m.FacilityId,
                                          m.FacilityName,
                                          m.MessageTypeId,
                                          m.MessageType,
                                          m.MessageStatusId,
                                          m.MessageStatus,
                                          m.MedicationName,
                                          m.MessageRequest,
                                          m.PatientId,
                                          m.PharmacyName,
                                          m.PreferredPeriod,
                                          m.PreferredTime,
                                          m.PreferredWeekDay,
                                          m.MessageUrgency,
                                          m.ProviderId_From,
                                          m.ProviderName_From,
                                          m.ProviderId_To,
                                          m.ProviderName_To,
                                          m.VisitReason,
                                          m.UserId_Created,
                                          m.CreatedByName,
                                          m.MessageResponse,
                                          m.MessageResponseTypeId,
                                          m.AppointmentStart,
                                          m.AppointmentEnd,
                                          m.ProviderId_Appointment,
                                          m.ProviderName_Appointment,
                                          m.MedicationNDC,
                                          m.NoOfRefills,
                                          m.MedicationStatus,
                                          m.PharmacyAddress,
                                          m.PharmacyPhone,  // SJF 08/25/14
                                          m.AttachmentId,
                                          //Added By Talha
                                          MessageUnRead = (
                                          from ms in db.MessageDetails
                                          where ms.MessageId == m.MessageId && ms.MessageRead == false 
                                          //&& ms.ProviderId_To != 0
                                          select ms.MessageRead
                                          ).Count(),
                                          DateCreated = (from ms in db.MessageDetails
                                                         where ms.MessageId == m.MessageId
                                                         orderby ms.DateCreated descending
                                                         select ms.DateCreated).FirstOrDefault()

                                      };
                        results = results.OrderByDescending(m => m.DateCreated);
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

        #region Get Message Detail Sent List
        //------------------------------------------------------------------------
        // Get Message Detail List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message Detail Sent List")]
        public MessageTableData GetMessageDetailSentList(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                        var results = from m in db.vwMessageDetailSents
                                      where m.PatientId == Parms.PatientId
                                      select new
                                      {
                                          m.MessageId,                                          
                                          m.MessageDetailId,
                                          m.PatientId,
                                          m.PatientName,
                                          m.MessageTypeId,
                                          m.MessageType,
                                          m.FacilityId,
                                          m.FacilityName,
                                          m.MessageStatusId,
                                          m.MessageStatus,
                                          m.Expr1,
                                          m.ProviderId_To,
                                          m.ProviderTo,
                                          m.ProviderId_From,
                                          m.ProviderFrom,
                                          m.MessageRequest,
                                          m.MessageResponse,
                                          m.MessageResponseTypeId,
                                          m.MessageResponseType,
                                          m.PreferredPeriod,
                                          m.PreferredTime,
                                          m.PreferredWeekDay,
                                          m.VisitReason,
                                          m.MessageUrgency,
                                          m.AppointmentStart,
                                          m.AppointmentEnd,
                                          m.ProviderId_Appointment,
                                          m.ProviderAppointment,
                                          m.MedicationNDC,
                                          m.MedicationName,
                                          m.NoOfRefills,
                                          m.MedicationStatus,
                                          m.PharmacyName,
                                          m.PharmacyAddress,
                                          m.PharmacyPhone,
                                          m.DateCreated,
                                          m.MessageRead,
                                          m.MessageAttachmentId,
                                          m.DocumentFormat
                                      };
                        results = results.OrderByDescending(m => m.DateCreated);
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

        #region Get Message Detail Received List
        //------------------------------------------------------------------------
        // Get Message Received Detail List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message Detail Received List")]
        public MessageTableData GetMessageDetailReceivedList(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                    if (Parms.Option == 0)
                    {
                        using (var db = new AMREntities())
                        {
                            var results = from m in db.vwMessageDetailReceiveds
                                          where m.PatientId == Parms.PatientId
                                          select new
                                          {
                                              m.MessageId,
                                              m.MessageDetailId,
                                              m.PatientId,
                                              m.PatientName,
                                              m.MessageTypeId,
                                              m.MessageType,
                                              m.FacilityId,
                                              m.FacilityName,
                                              m.MessageStatusId,
                                              m.MessageStatus,
                                              m.Expr1,
                                              m.ProviderId_To,
                                              m.ProviderTo,
                                              m.ProviderId_From,
                                              m.ProviderFrom,
                                              m.MessageRequest,
                                              m.MessageResponse,
                                              m.MessageResponseTypeId,
                                              m.MessageResponseType,
                                              m.PreferredPeriod,
                                              m.PreferredTime,
                                              m.PreferredWeekDay,
                                              m.VisitReason,
                                              m.MessageUrgency,
                                              m.AppointmentStart,
                                              m.AppointmentEnd,
                                              m.ProviderId_Appointment,
                                              m.ProviderAppointment,
                                              m.MedicationNDC,
                                              m.MedicationName,
                                              m.NoOfRefills,
                                              m.MedicationStatus,
                                              m.PharmacyName,
                                              m.PharmacyAddress,
                                              m.PharmacyPhone,
                                              m.DateCreated,
                                              m.MessageRead,
                                              m.MessageAttachmentId,
                                              m.DocumentFormat
                                          };
                            results = results.OrderByDescending(m => m.DateCreated);
                            Messages.dt = clsTableConverter.ToDataTable(results);

                            Messages.dt.TableName = "Messages";

                            Messages.Valid = true;
                            Messages.ErrorMessage = "";
                        }
                    }
                    else
                    {
                        using (var db = new AMREntities())
                        {
                            var results = from m in db.vwMessageDetailReceiveds
                                          where m.PatientId == Parms.PatientId
                                          && m.MessageTypeId == Parms.Option
                                          select new
                                          {
                                              m.MessageId,
                                              m.MessageDetailId,
                                              m.PatientId,
                                              m.PatientName,
                                              m.MessageTypeId,
                                              m.MessageType,
                                              m.FacilityId,
                                              m.FacilityName,
                                              m.MessageStatusId,
                                              m.MessageStatus,
                                              m.Expr1,
                                              m.ProviderId_To,
                                              m.ProviderTo,
                                              m.ProviderId_From,
                                              m.ProviderFrom,
                                              m.MessageRequest,
                                              m.MessageResponse,
                                              m.MessageResponseTypeId,
                                              m.MessageResponseType,
                                              m.PreferredPeriod,
                                              m.PreferredTime,
                                              m.PreferredWeekDay,
                                              m.VisitReason,
                                              m.MessageUrgency,
                                              m.AppointmentStart,
                                              m.AppointmentEnd,
                                              m.ProviderId_Appointment,
                                              m.ProviderAppointment,
                                              m.MedicationNDC,
                                              m.MedicationName,
                                              m.NoOfRefills,
                                              m.MedicationStatus,
                                              m.PharmacyName,
                                              m.PharmacyAddress,
                                              m.PharmacyPhone,
                                              m.DateCreated,
                                              m.MessageRead,
                                              m.MessageAttachmentId,
                                              m.DocumentFormat
                                          };
                            results = results.OrderByDescending(m => m.DateCreated);
                            Messages.dt = clsTableConverter.ToDataTable(results);

                            Messages.dt.TableName = "Messages";

                            Messages.Valid = true;
                            Messages.ErrorMessage = "";
                        }
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

        #region Get Message Detail Provider Sent List
        //------------------------------------------------------------------------
        // Get Message Detail Provider Sent List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message Detail Provider Sent List")]
        public MessageTableData GetMessageDetailProvSentList(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                        var results = from m in db.vwMessageDetailReceiveds
                                      where m.ProviderId_From == Parms.ProviderId
                                      select new
                                      {
                                          m.MessageId,
                                          m.MessageDetailId,
                                          m.PatientId,
                                          m.PatientName,
                                          m.MessageTypeId,
                                          m.MessageType,
                                          m.FacilityId,
                                          m.FacilityName,
                                          m.MessageStatusId,
                                          m.MessageStatus,
                                          m.Expr1,
                                          m.ProviderId_To,
                                          m.ProviderTo,
                                          m.ProviderId_From,
                                          m.ProviderFrom,
                                          m.MessageRequest,
                                          m.MessageResponse,
                                          m.MessageResponseTypeId,
                                          m.MessageResponseType,
                                          m.PreferredPeriod,
                                          m.PreferredTime,
                                          m.PreferredWeekDay,
                                          m.VisitReason,
                                          m.MessageUrgency,
                                          m.AppointmentStart,
                                          m.AppointmentEnd,
                                          m.ProviderId_Appointment,
                                          m.ProviderAppointment,
                                          m.MedicationNDC,
                                          m.MedicationName,
                                          m.NoOfRefills,
                                          m.MedicationStatus,
                                          m.PharmacyName,
                                          m.PharmacyAddress,
                                          m.PharmacyPhone,
                                          m.DateCreated,
                                          m.MessageRead,
                                          m.MessageAttachmentId,
                                          m.DocumentFormat
                                      };
                        results = results.OrderByDescending(m => m.DateCreated);
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

        #region Get Message Detail Provider Received List
        //------------------------------------------------------------------------
        // Get Message Received Provider Detail List 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Message Detail Provider Received List")]
        public MessageTableData GetMessageDetailProvReceivedList(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
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
                    if (Parms.Option == 0)
                    {
                        using (var db = new AMREntities())
                        {
                            var results = from m in db.vwMessageDetailSents
                                          where m.ProviderId_To == Parms.ProviderId
                                          select new
                                          {
                                              m.MessageId,
                                              m.MessageDetailId,
                                              m.PatientId,
                                              m.PatientName,
                                              m.MessageTypeId,
                                              m.MessageType,
                                              m.FacilityId,
                                              m.FacilityName,
                                              m.MessageStatusId,
                                              m.MessageStatus,
                                              m.Expr1,
                                              m.ProviderId_To,
                                              m.ProviderTo,
                                              m.ProviderId_From,
                                              m.ProviderFrom,
                                              m.MessageRequest,
                                              m.MessageResponse,
                                              m.MessageResponseTypeId,
                                              m.MessageResponseType,
                                              m.PreferredPeriod,
                                              m.PreferredTime,
                                              m.PreferredWeekDay,
                                              m.VisitReason,
                                              m.MessageUrgency,
                                              m.AppointmentStart,
                                              m.AppointmentEnd,
                                              m.ProviderId_Appointment,
                                              m.ProviderAppointment,
                                              m.MedicationNDC,
                                              m.MedicationName,
                                              m.NoOfRefills,
                                              m.MedicationStatus,
                                              m.PharmacyName,
                                              m.PharmacyAddress,
                                              m.PharmacyPhone,
                                              m.DateCreated,
                                              m.MessageRead,
                                              m.MessageAttachmentId,
                                              m.DocumentFormat
                                          };
                            results = results.OrderByDescending(m => m.DateCreated);
                            Messages.dt = clsTableConverter.ToDataTable(results);

                            Messages.dt.TableName = "Messages";

                            Messages.Valid = true;
                            Messages.ErrorMessage = "";
                        }
                    }
                    else
                    {
                        using (var db = new AMREntities())
                        {
                            var results = from m in db.vwMessageDetailSents
                                          where m.ProviderId_To == Parms.ProviderId
                                          && m.MessageTypeId == Parms.Option
                                          select new
                                          {
                                              m.MessageId,
                                              m.MessageDetailId,
                                              m.PatientId,
                                              m.PatientName,
                                              m.MessageTypeId,
                                              m.MessageType,
                                              m.FacilityId,
                                              m.FacilityName,
                                              m.MessageStatusId,
                                              m.MessageStatus,
                                              m.Expr1,
                                              m.ProviderId_To,
                                              m.ProviderTo,
                                              m.ProviderId_From,
                                              m.ProviderFrom,
                                              m.MessageRequest,
                                              m.MessageResponse,
                                              m.MessageResponseTypeId,
                                              m.MessageResponseType,
                                              m.PreferredPeriod,
                                              m.PreferredTime,
                                              m.PreferredWeekDay,
                                              m.VisitReason,
                                              m.MessageUrgency,
                                              m.AppointmentStart,
                                              m.AppointmentEnd,
                                              m.ProviderId_Appointment,
                                              m.ProviderAppointment,
                                              m.MedicationNDC,
                                              m.MedicationName,
                                              m.NoOfRefills,
                                              m.MedicationStatus,
                                              m.PharmacyName,
                                              m.PharmacyAddress,
                                              m.PharmacyPhone,
                                              m.DateCreated,
                                              m.MessageRead,
                                              m.MessageAttachmentId,
                                              m.DocumentFormat
                                          };
                            results = results.OrderByDescending(m => m.DateCreated);
                            Messages.dt = clsTableConverter.ToDataTable(results);

                            Messages.dt.TableName = "Messages";

                            Messages.Valid = true;
                            Messages.ErrorMessage = "";
                        }
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

        #region Get MessageType Codes
        //------------------------------------------------------------------------
        // Get MessageType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MessageType Codes")]
        public CodeTableData GetMessageTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from m in db.C_MessageType
                                      select new
                                      {
                                          m.MessageTypeId,
                                          m.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get MessageType General Codes
        //------------------------------------------------------------------------
        // Get MessageType General Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MessageType General Codes")]
        public CodeTableData GetMessageTypeGeneralCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from m in db.C_MessageType
                                      where m.General == true
                                      select new
                                      {
                                          m.MessageTypeId,
                                          m.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get MessageStatus Codes
        //------------------------------------------------------------------------
        // Get MessageStatus Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MessageStatus Codes")]
        public CodeTableData GetMessageStatusCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_MessageStatus
                                      select new
                                      {
                                          p.MessageStatusId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get MessageResponseType Codes
        //------------------------------------------------------------------------
        // Get MessageResponseType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MessageResponseType Codes")]
        public CodeTableData GetMessageResponseTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_MessageResponseType
                                      select new
                                      {
                                          p.MessageResponseTypeId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get MessagePeriod Codes
        //------------------------------------------------------------------------
        // Get MessagePeriod Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MessagePeriod Codes")]
        public CodeTableData GetMessagePeriodCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_MessagePeriod
                                      select new
                                      {
                                          p.MessagePeriodId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Message Attachment Attachment
        //------------------------------------------------------------------------
        // Get Message Attachment
        //------------------------------------------------------------------------
        [WebMethod(Description = "Get Message Attachment Data")]
        public MessageAttachmentData GetMessageAttachmentData(MessageAttachmentData Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            MessageAttachmentData MessageAttachment = new MessageAttachmentData();

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
                        AttachmentMessage results = db.AttachmentMessages.FirstOrDefault(m => m.MessageAttachmentId == Parms.MessageAttachmentId);

                        if (results != null)
                        {
                            MessageAttachment.MessageAttachmentId = results.MessageAttachmentId;
                            MessageAttachment.MessageId = results.MessageId;
                            MessageAttachment.PatientId = results.PatientId;
                            MessageAttachment.FileDirectory = results.FileDirectory;
                            MessageAttachment.FileName = results.FileName;
                            MessageAttachment.DocumentFormat = results.DocumentFormat;

                            string FileName = MessageAttachment.MessageAttachmentId.ToString() + "." + MessageAttachment.DocumentFormat;

                            MessageAttachment.DocumentImage = FileHelper.DiskToBytes(MessageAttachment.FileDirectory + "\\" + FileName);
                        }
                        else
                        {
                            MessageAttachment.Valid = false;
                            MessageAttachment.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageAttachment.Valid = false;
                    MessageAttachment.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                MessageAttachment.Valid = false;
                MessageAttachment.ErrorMessage = "Invalid Token";
            }
            return MessageAttachment;
        }


        #endregion

        #region Get Unread Message Data
        //------------------------------------------------------------------------
        // Get Unread Message Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Unread Message Data")]
        public MessageUnread GetUnreadMessageData(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageUnread Message = new MessageUnread();

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
                        vwMessageUnread results = db.vwMessageUnreads.FirstOrDefault(m => m.PatientId == Parms.PatientId);

                        if (results != null)
                        {
                            Message.Valid = true;
                            Message.ErrorMessage = "";
                            Message.AppointmentMessages = Convert.ToInt32(results.Appointment);
                            Message.MedicationMessages = Convert.ToInt32(results.Medication);
                            Message.ReferralMessages = Convert.ToInt32(results.Referral);
                            Message.GeneralMessages = Convert.ToInt32(results.General);
                            Message.TotalMessages = Convert.ToInt32(results.Appointment + results.Medication + results.Referral + results.General);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Valid = false;
                    Message.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Message.Valid = false;
                Message.ErrorMessage = "Invalid Token";
            }
            return Message;
        }
        #endregion

        #region Get Unread Provider Message Data
        //------------------------------------------------------------------------
        // Get Unread Provider Message Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Unread Provider Message Data")]
        public MessageUnread GetUnreadProvMessageData(MessageParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            MessageUnread Message = new MessageUnread();

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
                        vwMessageUnreadProv results = db.vwMessageUnreadProvs.FirstOrDefault(m => m.ProviderId == Parms.ProviderId);

                        if (results != null)
                        {
                            Message.Valid = true;
                            Message.ErrorMessage = "";
                            Message.AppointmentMessages = Convert.ToInt32(results.Appointment);
                            Message.MedicationMessages = Convert.ToInt32(results.Medication);
                            Message.ReferralMessages = Convert.ToInt32(results.Referral);
                            Message.GeneralMessages = Convert.ToInt32(results.General);
                            Message.TotalMessages = Convert.ToInt32(results.Appointment + results.Medication + results.Referral + results.General);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message.Valid = false;
                    Message.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Message.Valid = false;
                Message.ErrorMessage = "Invalid Token";
            }
            return Message;
        }
        #endregion

        #region Send Email Message
        //------------------------------------------------------------------------
        // Send Email Message
        //------------------------------------------------------------------------

        [WebMethod(Description = "Send Email Message")]
        public EmailMessageData SendEmailMessage(EmailMessageData Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            Parms.Valid = true;
            // Send Email 
            try
            {
                // Get the patient's setting for notifications
            
                using (var db = new AMREntities())
                {
                    if (Parms.To == "")
                    {
                        // No email passed - internal email get from database.
                        Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 3);
                        if (Email != null)
                            Parms.To = Email.EmailFolder;
                    }

                    clsEmail objEmail = new clsEmail();
                    Parms.ErrorMessage = objEmail.SendEmail(Parms.To, Parms.Subject, Parms.Body);
                    if (Parms.ErrorMessage != "")
                    {
                        Parms.Valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Parms.ErrorMessage = "Error send message";
                Parms.Valid = false;
            }
            return Parms;
        }
        #endregion

    }
}
