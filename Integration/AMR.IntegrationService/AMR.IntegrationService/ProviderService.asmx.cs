// Service Name  : ProviderService
// Date Created  : 10/28/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Integration Web Service For Provider Information
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
    /// Summary description for ProviderService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "ProviderWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProviderService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct ProviderData
        {
            public string ProviderId;
            public string Title;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string DEA;
            public string License;
            public string Phone;
            public string Email;
        }

        public struct ProviderResponse
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 ProviderId;
        }

        #endregion
        
        #region Create Provider
        //------------------------------------------------------------------------
        // Create Provider 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Create AMR Provider")]
        public ProviderResponse CreateAMRProvider(Int64 FacilityId, Int64 UserId, string Token, ProviderData ProviderData)
        {
            ProviderResponse ProvResponse = new ProviderResponse();
            ProvResponse.Valid = true;
            Int64 PracticeId = 0;
            bool bNewProvider = false;

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    // Check The ProviderId 
                    if (ProviderData.ProviderId == null || ProviderData.ProviderId == "" || ProviderData.ProviderId.Length > 20)
                    {
                        ProvResponse.Valid = false;
                        ProvResponse.ErrorMessage = "ProviderId is required and must be <= 20 characters";
                    }
                    else
                    {

                        using (var db = new AMREntities())
                        {
                            // Check If EMR ProviderId Passed In Already Exists For Facility
                            ProviderFacilityLink LinkResults = db.ProviderFacilityLinks.FirstOrDefault
                                    (p => p.FacilityProviderId == ProviderData.ProviderId
                                    && p.FacilityId == FacilityId);

                            if (LinkResults != null)
                            {
                                // Provider Already Exists
                                ProvResponse.Valid = true;
                                ProvResponse.ErrorMessage = "";
                                ProvResponse.ProviderId = LinkResults.ProviderId;
                            }
                            else
                            {
                                //Check If ProviderId Already Exists - If So, just create link to facility

                                // First, Get The PracticeId for the facility
                                Facility FacilityResults = db.Facilities.FirstOrDefault
                                    (f => f.FacilityId == FacilityId);

                                if (FacilityResults == null)
                                {
                                    ProvResponse.Valid = false;
                                    ProvResponse.ErrorMessage = "Error reading facility information.";
                                }
                                else
                                {
                                    PracticeId = Convert.ToInt64(FacilityResults.PracticeId);

                                    // Check if the provider already exists for the practice

                                    Provider ProviderResults = db.Providers.FirstOrDefault
                                        (p => p.PracticeId == PracticeId
                                        && p.License == ProviderData.License);

                                    if (ProviderResults != null)
                                    {
                                        ProvResponse.ProviderId = ProviderResults.ProviderId;

                                        // Provider already exist for this license for the practice
                                        ProviderFacilityLink PLinkResults = db.ProviderFacilityLinks.FirstOrDefault
                                            (p => p.ProviderId == ProviderResults.ProviderId
                                            && p.FacilityId == FacilityId);

                                        if (FacilityResults == null)
                                        {
                                            // Provider Exists - Create Link
                                            var NewProviderLink = new ProviderFacilityLink()
                                            {

                                                ProviderId = ProviderResults.ProviderId,
                                                FacilityId = FacilityId,
                                                FacilityProviderId = ProviderData.ProviderId,
                                                DateCreated = System.DateTime.Now,
                                            };
                                            db.ProviderFacilityLinks.Add(NewProviderLink);

                                            db.SaveChanges();
                                        }
                                        else
                                        {
                                            ProvResponse.Valid = false;
                                            ProvResponse.ErrorMessage = "A provider with this license number has already been created for this facility";
                                        }
                                    }
                                    else
                                    {
                                        // New Provider that needs to be added
                                        bNewProvider = true;
                                    }
                                }
                            }
                        }
                        if (bNewProvider)
                        {
                            // New Provider, add to system
                            // Validate Information Sent From The EMR
                            String ErrorMsg = "";

                            if (ProviderData.FirstName == null || ProviderData.FirstName == "" || ProviderData.FirstName.Length > 20)
                                ErrorMsg = ErrorMsg + "First Name is required and must be <= 20 characters   ";

                            if (ProviderData.LastName == null || ProviderData.LastName == "" || ProviderData.LastName.Length > 20)
                                ErrorMsg = ErrorMsg + "Last Name is required and must be <= 20 characters   ";

                            if (ProviderData.MiddleName != null && ProviderData.MiddleName.Length > 20)
                                ErrorMsg = ErrorMsg + "Middle Name must be <= 20 characters   ";

                            if (ProviderData.Title != null && ProviderData.Title.Length > 10)
                                ErrorMsg = ErrorMsg + "Salutation must be <= 10 characters   ";

                            if (ProviderData.DEA != null && ProviderData.DEA.Length > 50)
                                ErrorMsg = ErrorMsg + "DEA must be <= 50 characters   ";

                            if (ProviderData.License == null || ProviderData.License == "" || ProviderData.License.Length > 50)
                                ErrorMsg = ErrorMsg + "License is required and must be <= 50 characters   ";

                            if (ProviderData.Phone != null && ProviderData.Phone.Length > 16)
                                ErrorMsg = ErrorMsg + "Phone must be <= 16 characters   ";

                            if (ProviderData.Email == null || ProviderData.Email == "" || ProviderData.Email.Length > 60)
                                ErrorMsg = ErrorMsg + "EMail is required and must be <= 60 characters   ";

                            if (ErrorMsg != "")
                            {
                                ProvResponse.Valid = false;
                                ProvResponse.ErrorMessage = ErrorMsg;
                            }
                            else
                            {
                                using (var db = new AMREntities())
                                {
                                    // Add Provider
                                    var NewProvider = new Provider()
                                    {
                                        PracticeId = PracticeId,
                                        UserId = 0,         // Add This Back After Creating User
                                        Title = ProviderData.Title,
                                        FirstName = ProviderData.FirstName,
                                        MiddleName = ProviderData.MiddleName,
                                        LastName = ProviderData.LastName,
                                        DEA = ProviderData.DEA,
                                        License = ProviderData.License,
                                        Phone = ProviderData.Phone,
                                        Email = ProviderData.Email,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                    };

                                    db.Providers.Add(NewProvider);

                                    db.SaveChanges();

                                    ProvResponse.ProviderId = NewProvider.ProviderId;

                                    // Generate a random password and Encrypt it
                                    Random randomNumber = new Random();
                                    string passclear = string.Empty;
                                    for (int i = 0; i < 8; i++)
                                    {
                                        passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                    }
                                    string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                                    // Add User
                                    var NewUser = new User()
                                    {

                                        UserLogin = ProviderData.Email,
                                        UserEmail = ProviderData.Email,
                                        Password = passencr,
                                        UserRoleId = 4,
                                        UserRoleLink = NewProvider.ProviderId,
                                        Enabled = true,
                                        Locked = false,
                                        ResetPassword = true,
                                    };
                                    db.Users.Add(NewUser);

                                    db.SaveChanges();

                                    // Update UserId In Provider Record
                                    Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == NewProvider.ProviderId);

                                    if (ProviderResp != null)
                                    {
                                        ProviderResp.UserId = NewUser.UserId;
                                        db.SaveChanges();
                                    }

                                    // Add Provider Web Settings - First Use
                                    var NewWebSetting = new ProviderWebSetting()
                                    {

                                        ProviderId = NewProvider.ProviderId,
                                        EmailNotifyNewMessage = false,
                                        PictureLocation = "",
                                    };
                                    db.ProviderWebSettings.Add(NewWebSetting);
                                    db.SaveChanges();

                                    // Create Link
                                    var NewProviderLink = new ProviderFacilityLink()
                                    {

                                        ProviderId = NewProvider.ProviderId,
                                        FacilityId = FacilityId,
                                        FacilityProviderId = ProviderData.ProviderId,
                                        DateCreated = System.DateTime.Now,
                                    };
                                    db.ProviderFacilityLinks.Add(NewProviderLink);

                                    db.SaveChanges();

                                    try
                                    {

                                        // Send Email to new provider with account login information.

                                        string FacilityName = "";
                                        Facility FacResponse = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityId);

                                        bool SendMail = false;
                                        if (FacResponse != null)
                                        {
                                            FacilityName = FacResponse.FacilityName;
                                            int EMR = Convert.ToInt32(FacResponse.EMRSystemId);
                                            C_EMRSystem EMRResponse = db.C_EMRSystem.FirstOrDefault(c => c.EMRSystemId == EMR);
                                            if (EMRResponse.UseProviderPortal == true)
                                            {
                                                SendMail = true;
                                            }
                                        }

                                        if (SendMail)
                                        {
                                            clsEmail objEmail = new clsEmail();
                                            string host = "";
                                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                                            if (Email != null)
                                                host = Email.SiteURL;

                                            string WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                                    "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                                    "	<tr style=\"background-color:#00a0e0;\">" +
                                                    "       <td valign=\"top\"> " +
                                                    "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                                    "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                                    "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                                    "                <img src=\"https://www.amrportal.com/LetterImages/amr-provider-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
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
                                                    "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + ProviderData.Title + " " + ProviderData.FirstName + " " + ProviderData.LastName + "</strong>,</h1>  <br />" +
                                                    "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                    "               Welcome to your provider portal services provided by AMR Patient Portal. " + FacilityName + " has activated your AMR Provider Portal account. Below you will find your AccessID Code to log in to your portal account. <br /><br />" +
                                                // "               Your AccessID Code:   <strong>" + ProvResponse.ProviderId + "</strong> <br /><br />" +
                                                   "               Your AccessID Code:   <strong>" + ProviderData.Email + "</strong> <br /><br />" +
                                                    "               You will also be receiving an email with your temporary password, which you will need to change once you log in. " +
                                                    "               <a href=\"http://" + host + "/ProviderPortal" + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Provider Portal account.<br /><br /> " +
                                                    "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                    "               Thank you, <br />" +
                                                    "               Your Member Services Team<br /><br />" +
                                                    "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
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

                                                     objEmail.SendEmailHTML(ProviderData.Email, "AMR Provider Portal - AccessID Code", WelcomeMsg);

                                            WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                                               "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                                                "	<tr style=\"background-color:#00a0e0;\">" +
                                                                "       <td valign=\"top\"> " +
                                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                                                "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                                                "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                                                "                <img src=\"https://www.amrportal.com/LetterImages/amr-provider-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
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
                                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + ProviderData.Title + " " + ProviderData.FirstName + " " + ProviderData.LastName + "</strong>,</h1>  <br />" +
                                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                                "               Welcome again to AMR Provider Portal. Below you will find your temporary password.<br /><br />" +
                                                                "               Your Temporary Password:   <strong>" + passclear + "</strong> <br /><br />" +
                                                                "               Using the AccessID Code provided in the previous email, " +
                                                                "               <a href=\"http://" + host + "/ProviderPortal" + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Provider Portal account and reset your temporary password.<br /><br /> " +
                                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                                "               Thank you, <br />" +
                                                                "               Your Member Services Team<br /><br />" +
                                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
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

                                            objEmail.SendEmailHTML(ProviderData.Email, "AMR Provider Portal - Temporary Password", WelcomeMsg);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ProvResponse.Valid = true;
                                        ProvResponse.ErrorMessage = "Patient was created, but email could not be sent";
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ProvResponse.Valid = false;
                    if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                        ProvResponse.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
                        ProvResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                ProvResponse.Valid = false;
                ProvResponse.ErrorMessage = "Invalid Token";
            }
            return ProvResponse;
        }
        #endregion
    }
}
