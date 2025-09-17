// Class Name    : clsEmail.cs
// Date Created  : 11/20/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Used to send email
// MM/DD/YYYY XXX Description
//
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;

namespace AMR.Data
{
    public class clsEmail
    {
        #region EMail Sender
        //------------------------------------------------------------------------
        // EMail Sender
        //------------------------------------------------------------------------

        public string SendEmail(string To, string Subject, string Message)
        {
            string ErrorMessage = "";

            // Get the Email Account Information
            using (var db = new AMREntities())
            {
                Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);

                if (Email != null)
                {

                    if (Convert.ToBoolean(Email.EmailActive))
                    {
                        // Within a try catch, format and send the message to
                        // the recipient.  Catch and handle any errors.
                        try
                        {

                            MailMessage mail = new MailMessage();
                            mail.To.Add(To);
                            mail.From = new MailAddress(Email.EmailUser);
                            mail.Bcc.Add(Email.EmailUser);
                            mail.Subject = Subject;
                            mail.Body = Message;

                            SmtpClient client = new SmtpClient();
                            client.Credentials = new System.Net.NetworkCredential(Email.EmailUser, Email.EmailPassword);
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;

                            client.Host = Email.EmailHost;
                            client.EnableSsl = Convert.ToBoolean(Email.EmailEnableSSL);
                            client.Send(mail);



                            ErrorMessage = "";
                        }
                        catch (FormatException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (SmtpException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                }
                else
                    ErrorMessage = "Could not read email configuration file.";
            }
            return ErrorMessage;
        }

        #endregion

        #region EMail Sender
        //------------------------------------------------------------------------
        // EMail Sender
        //------------------------------------------------------------------------

        public string SendEmailHTML(string To, string Subject, string Message)
        {
            string ErrorMessage = "";

            // Get the Email Account Information
            using (var db = new AMREntities())
            {
                Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);

                if (Email != null)
                {

                    if (Convert.ToBoolean(Email.EmailActive))
                    {
                        // Within a try catch, format and send the message to
                        // the recipient.  Catch and handle any errors.
                        try
                        {

                            MailMessage mail = new MailMessage();
                            mail.IsBodyHtml = true;
                            mail.To.Add(To);
                            mail.From = new MailAddress(Email.EmailUser);
                            mail.Bcc.Add(Email.EmailUser);
                            mail.Subject = Subject;
                            mail.Body = Message;

                            SmtpClient client = new SmtpClient();
                            client.Credentials = new System.Net.NetworkCredential(Email.EmailUser, Email.EmailPassword);
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;

                            client.Host = Email.EmailHost;
                            client.EnableSsl = Convert.ToBoolean(Email.EmailEnableSSL);
                            client.Send(mail);



                            ErrorMessage = "";
                        }
                        catch (FormatException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (SmtpException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                }
                else
                    ErrorMessage = "Could not read email configuration file.";
            }
            return ErrorMessage;
        }

        #endregion

        #region Direct EMail Sender
        //------------------------------------------------------------------------
        // Direct EMail Sender
        //------------------------------------------------------------------------

        public string SendSecureEmail(string To, string Subject, string Message, string filename)
        {
            string ErrorMessage = "";
            

            // Get the Email Account Information
            using (var db = new AMREntities())
            {
                Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 2);

                if (Email != null)
                {

                    if (Convert.ToBoolean(Email.EmailActive))
                    {
                        // Within a try catch, format and send the message to
                        // the recipient.  Catch and handle any errors.
                        try
                        {

                            MailMessage mail = new MailMessage();
                            mail.To.Add(To);
                            mail.From = new MailAddress(Email.EmailUser);
                            mail.Subject = Subject;
                            mail.Body = Message;
                            Attachment attachment = new Attachment(filename);
                            mail.Attachments.Add(attachment);


                            SmtpClient client = new SmtpClient();
                            client.Credentials = new System.Net.NetworkCredential(Email.EmailUser, Email.EmailPassword);
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //client.Port = 25;


                            client.Host = Email.EmailHost;
                            client.EnableSsl = Convert.ToBoolean(Email.EmailEnableSSL);
                            client.Send(mail);



                            ErrorMessage = "";
                        }
                        catch (FormatException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (SmtpException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }
                }
                else
                    ErrorMessage = "Could not read email configuration file.";
            }
            return ErrorMessage;
        }

        #endregion

    }
}
