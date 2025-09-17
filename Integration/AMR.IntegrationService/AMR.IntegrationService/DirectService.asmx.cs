// Service Name  : ConfigService
// Date Created  : 06/18/2014
// Written By    : Stephen Farkas
// Version       : 2014.01
// Description   : Web Service For Configuration Tables
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
using System.Xml;
using AMR.Data;


namespace AMR.IntegrationService
{

    [WebService(Namespace = "http://tempuri.org/", Name = "Direct+WS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class DirectService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct DirectData
        {
            public Int64 PatientId;
            public Int64 FacilityId;
            public string PatientEmail;
            public XmlDocument xd;           
        }

        public struct DirectResp
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PatientId;
            public Int64 FacilityId;
        }
        #endregion

        #region Process Direct Message
        //------------------------------------------------------------------------
        // Process Direct Message
        //------------------------------------------------------------------------

        [WebMethod(Description = "Process Direct Message")]
        public DirectResp ProcessDirectMessage(DirectData Direct, string Token, Int64 UserId, Int64 FacilityId)
        {
            DirectResp Response = new DirectResp();
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
                    string error = "";
                    clsDirectImport objDirectImport = new clsDirectImport();

                    Response.Valid = objDirectImport.Parse(Direct.xd, ref error, ref Direct.FacilityId, ref Direct.PatientId, Direct.PatientEmail);
                    Response.ErrorMessage = error;
                    Response.PatientId = Direct.PatientId;
                    Response.FacilityId = Direct.FacilityId;

                    if (Response.Valid)
                        SendMessageNotification(Direct.PatientId);

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


        #region SendMessageNotification

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

                            string Msg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
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
                                            "               This is to inform you that your portal has received an update from your physician. We encourage you to look at the most current documentation associated with your last appointment. To view this information," +
                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">click here</a> to log in to your portal using your AccessID Code and password.<br /><br />" +
                                            "               Thank you, <br />" +
                                            "               Your Member Services Team<br />" +
                                            "               <br /><br />" +
                                            "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
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

                            objEmail.SendEmailHTML(dr["UserEMail"].ToString(), "AMR Patient Portal - New Patient Information Available", Msg);

                        }
                        if (Convert.ToBoolean(dr["TextNotifyNewMesssage"]))
                        {
                            if (dr["MobilePhone"].ToString().Trim() != "" && dr["CarrierURL"].ToString() != null)
                            {
                                clsEmail objEmail = new clsEmail();

                                string Msg = "You have a message waiting ";
                                string EmailAddress = dr["MobilePhone"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "@" + dr["CarrierURL"].ToString();

                                objEmail.SendEmail(EmailAddress, "AMR Patient Portal - New Patient Information Available", Msg);
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
    }
}

