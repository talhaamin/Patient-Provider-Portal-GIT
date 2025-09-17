// Service Name  : AuthentiationService
// Date Created  : 10/30/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Integration Web Service For Authentication
//                 
// 12/12/2013 SJF Added Change password, charge security question, get security questin and reset password.
// 06/18/2014 SJF Added reset locked account
// 12/01/2014 SJF Added GetPatientSecurityInfo
// 12/11/2014 SJF Added ChangeProviderPasswordAdmin
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
    /// Summary description for AuthenticationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "AuthenticationWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class AuthenticationService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct AuthenticationResponse
        {
            public bool Valid;
            public Int64 UserId;
            public Int16 LoginType;
            public string Token;
            public string ErrorMessage;
            public bool FirstLogin;
            public string SecurityQuestion;
            public string SecurityQuestion2;
            public Int64 UserRoleLink;
            public bool ResetPassword;
        }
        public struct PatientLoginData
        {
            public bool Valid;
            public string ErrorMessage;
            public DateTime DateCreated;
            public DateTime LastLoginDate;
        }
        public struct UserData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 UserId;
            public string Email;
        }
        public struct PatientSecurityData
        {
            public bool Valid;
            public string ErrorMessage;
            public int SecurityQuestionId;
            public string SecurityAnswer;
            public int SecurityQuestionId2;
            public string SecurityAnswer2;
        }
        #endregion

        #region AuthenticateInterface
        //------------------------------------------------------------------------   
        // Authenticate
        //------------------------------------------------------------------------

        [WebMethod(Description = "Authenticate Interface")]

        public AuthenticationResponse AuthenticateInterface(Int64 FacilityId, String UserLogin, string Password)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    // Check If Login Is Valid
                    string PassEnc = clsEncryption.Encrypt(Password, "AMRP@ss");
                    Response.Valid = true;

                    User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin);

                    if (UserResults != null)
                    {
                        if (UserResults.UserRoleId == 5)
                        {
                            // Patient
                            Patient PatientResults = db.Patients.FirstOrDefault(p => p.PatientId == UserResults.UserRoleLink);

                            if (PatientResults == null)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Invalid Access ID";
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = 0,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 6,     // Invalid  Account
                                };
                            }
                            else if (PatientResults.Deleted == true)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Invalid Access ID";
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = 0,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 6,     // Invalid  Account
                                };
                            }
                            else if (PatientResults.Active == false)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Account has been deactivated";
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = 0,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 6,     // Invalid  Account
                                };
                            }
                        }

                        if (Response.Valid == true)
                        {
                            if (UserResults.Enabled == false)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Login is disabled";

                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 2,     // Login Is Disabled
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();
                            }
                            else if (UserResults.Locked == true)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Your account is locked";
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 3,     // Login Is Locked
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();
                            }
                            //else if (UserResults.UserRoleId != LoginType)
                            //{
                            //    Response.Valid = false;
                            //    Response.ErrorMessage = "Invalid login type for this user id.";
                            //    // Write Audit Record
                            //    var LoginAudit = new UserLoginAudit()
                            //    {
                            //        UserId = UserResults.UserId,
                            //        StartDateTime = System.DateTime.Now,
                            //        UserLogin = UserLogin,
                            //        LoginType = Convert.ToInt16(LoginType),
                            //        EndDateTime = System.DateTime.Now,
                            //        AccessStatusId = 5,     // Invalid Account Type
                            //    };
                            //    db.UserLoginAudits.Add(LoginAudit);
                            //    db.SaveChanges();
                            //}
                            else if (UserResults.Password != PassEnc)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Invalid password.";
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                    EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 4,     // Invalid Password
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();
                                // Add to invalid login count
                                UserResults.FailedLoginCount++;
                                if (UserResults.FailedLoginCount > 4)
                                    UserResults.Locked = true;
                                db.SaveChanges();
                            }
                            else
                            {
                                // Generate Token
                                clsToken objToken = new clsToken();
                                objToken.UserId = UserResults.UserId;
                                objToken.FacilityId = FacilityId;

                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserLogin,
                                    LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                    //EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 1,     // Valid Login
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();

                                // Update User Record
                                UserResults.FailedLoginCount = 0;
                                UserResults.LastLoginDate = System.DateTime.Now;
                                db.SaveChanges();

                                if (objToken.Generate())
                                {
                                    Response.UserId = UserResults.UserId;
                                    Response.Token = objToken.Token;
                                    Response.LoginType = Convert.ToInt16(UserResults.UserRoleId);
                                    Response.UserRoleLink = Convert.ToInt64(UserResults.UserRoleLink);
                                    if (UserResults.SecurityAnswer == null)
                                        Response.FirstLogin = true;
                                    else
                                        Response.FirstLogin = false;
                                    if (UserResults.ResetPassword == false)
                                        Response.ResetPassword = Convert.ToBoolean(UserResults.ResetPassword);
                                    else
                                        Response.ResetPassword = true;
                                }
                                else
                                {
                                    Response.Valid = false;
                                    Response.ErrorMessage = "Error Generating Token";
                                }
                            }
                        }

                    }
                    else
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = "Invalid Access ID";
                        // Write Audit Record
                        var LoginAudit = new UserLoginAudit()
                        {
                            UserId = 0,
                            StartDateTime = System.DateTime.Now,
                            UserLogin = UserLogin,
                            LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                            EndDateTime = System.DateTime.Now,
                            AccessStatusId = 6,     // Invalid  Account
                        };
                        db.UserLoginAudits.Add(LoginAudit);
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion

        #region AuthenticatePatientInterface
        //------------------------------------------------------------------------
        // Authenticate
        //------------------------------------------------------------------------

        [WebMethod(Description = "Patient Authenticate Interface")]

        public AuthenticationResponse AuthenticatePatientInterface(Int64 FacilityId, String UserLogin, string Password)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    // Check If Login Is Valid
                    string PassEnc = clsEncryption.Encrypt(Password, "AMRP@ss");

                    User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin);

                    if (UserResults != null)
                    {
                        Patient PatientResults = db.Patients.FirstOrDefault(p => p.PatientId == UserResults.UserRoleLink);

                        if (PatientResults == null)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = 0,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 6,     // Invalid  Account
                            };
                        }
                        else if (PatientResults.Deleted == true)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = 0,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 6,     // Invalid  Account
                            };
                        }
                        else if (PatientResults.Active == false)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Account has been deactivated";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = 0,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 6,     // Invalid  Account
                            };
                        }
                        //else if (UserResults.Enabled == false)
                        //{
                        //    Response.Valid = false;
                        //    Response.ErrorMessage = "Login Is Disabled";

                        //    // Write Audit Record
                        //    var LoginAudit = new UserLoginAudit()
                        //    {
                        //        UserId = UserResults.UserId,
                        //        StartDateTime = System.DateTime.Now,
                        //        UserLogin = UserLogin,
                        //        LoginType = Convert.ToInt16(UserResults.UserRoleId),
                        //        EndDateTime = System.DateTime.Now,
                        //        AccessStatusId = 2,     // Login Is Disabled
                        //    };
                        //    db.UserLoginAudits.Add(LoginAudit);
                        //    db.SaveChanges();
                        //}
                        //else if (UserResults.Locked == true)
                        //{
                        //    Response.Valid = false;
                        //    Response.ErrorMessage = "Your account is locked";
                        //    // Write Audit Record
                        //    var LoginAudit = new UserLoginAudit()
                        //    {
                        //        UserId = UserResults.UserId,
                        //        StartDateTime = System.DateTime.Now,
                        //        UserLogin = UserLogin,
                        //        LoginType = Convert.ToInt16(UserResults.UserRoleId),
                        //        EndDateTime = System.DateTime.Now,
                        //        AccessStatusId = 3,     // Login Is Locked
                        //    };
                        //    db.UserLoginAudits.Add(LoginAudit);
                        //    db.SaveChanges();
                        //}
                        else
                        {
                            // Generate Token
                            clsToken objToken = new clsToken();
                            objToken.UserId = UserResults.UserId;
                            objToken.FacilityId = FacilityId;

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 1,     // Valid Login
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.FailedLoginCount = 0;
                            UserResults.LastLoginDate = System.DateTime.Now;
                            db.SaveChanges();

                            if (objToken.Generate())
                            {
                                Response.UserId = UserResults.UserId;
                                Response.Token = objToken.Token;
                                Response.LoginType = Convert.ToInt16(UserResults.UserRoleId);
                                Response.UserRoleLink = Convert.ToInt64(UserResults.UserRoleLink);
                                if (UserResults.SecurityAnswer == null)
                                    Response.FirstLogin = true;
                                else
                                    Response.FirstLogin = false;
                                if (!UserResults.ResetPassword == null)
                                    Response.ResetPassword = Convert.ToBoolean(UserResults.ResetPassword);
                                else
                                    Response.ResetPassword = true;
                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Error Generating Token";
                            }
                        }
                    }
                    else
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = "Invalid Access ID";
                        // Write Audit Record
                        var LoginAudit = new UserLoginAudit()
                        {
                            UserId = 0,
                            StartDateTime = System.DateTime.Now,
                            UserLogin = UserLogin,
                            LoginType = 99,//Convert.ToInt16(UserResults.UserRoleId),
                            EndDateTime = System.DateTime.Now,
                            AccessStatusId = 6,     // Invalid  Account
                        };
                        db.UserLoginAudits.Add(LoginAudit);
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion


        #region Change Password
        //------------------------------------------------------------------------
        // Change Password
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Password")]

        public AuthenticationResponse ChangePassword(string Token, Int64 UserId, Int64 FacilityId, int LoginType, string Password, string NewPassword)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

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
                        // Check If Login Is Valid
                        string PassEnc = clsEncryption.Encrypt(Password, "AMRP@ss");

                        User UserResults = db.Users.FirstOrDefault(u => u.UserId == UserId);

                        if (UserResults != null)
                        {
                            if (UserResults.Password != PassEnc)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Invalid password.";
                            }
                            else
                            {
                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserResults.UserLogin,
                                    LoginType = Convert.ToInt16(LoginType),
                                    //EndDateTime = System.DateTime.Now,
                                    AccessStatusId = 7,     // Password Changed
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();

                                // Update User Record
                                UserResults.Password = clsEncryption.Encrypt(NewPassword, "AMRP@ss");
                                UserResults.LastPasswordChange = System.DateTime.Now;
                                UserResults.ResetPassword = false;
                                db.SaveChanges();

                                SendMessagePassChange(Convert.ToInt16(UserResults.UserRoleId), Convert.ToInt64(UserResults.UserRoleLink), UserResults.UserEmail);

                            }
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
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

        private void SendMessagePassChange(Int16 UserRoleId, Int64 PatientId, string EmailAddress)
        {
            // Send Email to patient notifying of new message
            try
            {
                // Get the patient's setting for notifications

                using (var db = new AMREntities())
                {
                    string FullName = "";
                    if (UserRoleId == 5)
                    {
                        // Patient
                        var results = from p in db.Patients
                                      where p.PatientId == PatientId
                                      select new
                                      {
                                          p.PatientId,
                                          p.Title,
                                          p.FirstName,
                                          p.LastName,
                                      };

                        DataTable dt = clsTableConverter.ToDataTable(results);
                        foreach (DataRow dr in dt.Rows)
                        {
                            FullName = dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"];
                            break;
                        }
                    }
                    else if (UserRoleId == 6)
                    {
                        // Representative
                        var results = from p in db.PatientRepresentatives
                                      where p.PatientId == PatientId
                                      select new
                                      {
                                          p.PatientId,
                                          p.FirstName,
                                          p.LastName,
                                      };

                        DataTable dt = clsTableConverter.ToDataTable(results);
                        foreach (DataRow dr in dt.Rows)
                        {
                            FullName = dr["FirstName"] + " " + dr["LastName"];
                            break;
                        }
                    }

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
                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + FullName + "</strong>,</h1>  <br />" +
                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                "               Your password has been changed. <br /><br />" +
                                "               You can now <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">log in</a> to your portal account, view and update information, and securely communicate with your doctor(s). " +
                                "               <br /><br />" +
                                "               If you did not change your password, please contact us immediately at support@accessmyrecords.com." +
                                "               <br /><br />" +
                                "               Thank you,<br/>" +
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

                    objEmail.SendEmailHTML(EmailAddress, "AMR Patient Portal - Password Change Notification", Msg);
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Change Patient Password Admin
        //------------------------------------------------------------------------
        // Change Patient Password Admin
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Patient Password Admin")]

        public AuthenticationResponse ChangePatientPasswordAdmin(string Token, Int64 UserId, Int64 FacilityId, Int64 PatientId)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

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
                        // Find The Patient
                        string UserLogin = PatientId.ToString();
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin && u.UserRoleId == 5);

                        if (UserResults != null)
                        {
                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            string passclear = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserResults.UserLogin,
                                LoginType = 5,
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 7,     // Password Changed
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.Password = passencr;
                            UserResults.LastPasswordChange = System.DateTime.Now;
                            UserResults.ResetPassword = true;
                            db.SaveChanges();

                            SendMessagePassReset(5, PatientId, passclear, UserResults.UserEmail);
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
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

        #region Change Provider Password Admin
        //------------------------------------------------------------------------
        // Change Provider Password Admin
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Provider Password Admin")]

        public AuthenticationResponse ChangeProviderPasswordAdmin(string Token, Int64 UserId, Int64 FacilityId, Int64 ProviderId)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

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
                        // Find The Patient
                        User UserResults = db.Users.FirstOrDefault(u => u.UserRoleLink == ProviderId && u.UserRoleId == 4);

                        if (UserResults != null)
                        {
                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            string passclear = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserResults.UserLogin,
                                LoginType = 5,
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 7,     // Password Changed
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.Password = passencr;
                            UserResults.LastPasswordChange = System.DateTime.Now;
                            UserResults.ResetPassword = true;
                            db.SaveChanges();

                            SendMessagePassReset(4, ProviderId, passclear, UserResults.UserEmail);
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
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

        #region Get Patient Security Info
        //------------------------------------------------------------------------
        // Get Patient Security Info
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Security Info")]

        public PatientSecurityData GetPatientSecurityInfo(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientSecurityData Response = new PatientSecurityData();

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
                        string mPatientId = PatientId.ToString();
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == mPatientId && u.UserRoleId ==5);

                        if (UserResults != null)
                        {
                            Response.SecurityQuestionId = Convert.ToInt32(UserResults.SecurityQuestionId);
                            Response.SecurityAnswer = UserResults.SecurityAnswer;
                            Response.SecurityQuestionId2 = Convert.ToInt32(UserResults.SecurityQuestionId2);
                            Response.SecurityAnswer2 = UserResults.SecurityAnswer2;
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Patient Id";
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

        #region Change Security Question
        //------------------------------------------------------------------------
        // Change Security Question
        //------------------------------------------------------------------------

        [WebMethod(Description = "Change Security Question")]

        public AuthenticationResponse ChangeSecurityQuestion(string Token, Int64 UserId, Int64 FacilityId, int LoginType, int QuestionId, string Answer, int QuestionId2, string Answer2,string UserLogin)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

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
                        User UserResults = db.Users.FirstOrDefault(u => u.UserId == UserId);
                        //we need to search patient in user table with the of UserLogin
                       // User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin); //Added by Talha.


                        if (UserResults != null)
                        {
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserResults.UserLogin,
                                LoginType = Convert.ToInt16(LoginType),
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 8,     // Security Question Changed
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.SecurityQuestionId = QuestionId;
                            UserResults.SecurityAnswer = Answer;
                            UserResults.SecurityQuestionId2 = QuestionId2;
                            UserResults.SecurityAnswer2 = Answer2;
                            db.SaveChanges();
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
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

        #region Get Security Question
        //------------------------------------------------------------------------
        // Get Security Question
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Security Question")]

        public AuthenticationResponse GetSecurityQuestion(Int64 FacilityId, String UserLogin, int LoginType, String UserEmail)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";
            Response.SecurityQuestion = "";
            Response.SecurityQuestion2 = "";

            try
            {
                using (var db = new AMREntities())
                {
                    User UserResults = new User();
                    if ( UserEmail !=null)
                    {
                        UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin && u.UserEmail == UserEmail);
                    }
                    else {
                        UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin);
                    }
                    

                    if (UserResults != null)
                    {
                      //  if (UserResults.SecurityQuestionId != null && UserResults.SecurityQuestionId > 0)
                      //  {
                            // Return the Security Question
                            C_SecurityQuestion QuestionResults = db.C_SecurityQuestion.FirstOrDefault(s => s.SecurityQuestionId == UserResults.SecurityQuestionId);
                            if (QuestionResults != null)
                            {
                                Response.SecurityQuestion = QuestionResults.Value;
                            }

                            C_SecurityQuestion QuestionResults2 = db.C_SecurityQuestion.FirstOrDefault(s => s.SecurityQuestionId == UserResults.SecurityQuestionId2);
                            if (QuestionResults2 != null)
                            {
                                Response.SecurityQuestion2 = QuestionResults2.Value;
                            }
                      //  }
                    }
                    else
                    {
                        Response.Valid = false;
                        if (UserEmail != null)
                        {

                            Response.ErrorMessage = "Invalid Access ID and Email";

                        }
                        else
                        {
                            Response.ErrorMessage = "Invalid Access ID";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion

        #region Get Patient Last Login
        //------------------------------------------------------------------------
        // Get Patient Last Login
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Patient Last Login")]

        public PatientLoginData GetPatientLastLogin(Int64 PatientId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PatientLoginData Response = new PatientLoginData();

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
                        string mPatientId = PatientId.ToString();
                        User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == mPatientId);

                        if (UserResults != null)
                        {
                            Patient PatientResult = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);
                           
                            if (PatientResult != null)
                            {
                                Response.LastLoginDate = Convert.ToDateTime(UserResults.LastLoginDate);
                                Response.DateCreated = Convert.ToDateTime(PatientResult.DateCreated);
                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Invalid Patient Id";
                            }
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Patient Id";
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

        #region Reset Password
        //------------------------------------------------------------------------
        // Reset Password
        //------------------------------------------------------------------------

        [WebMethod(Description = "Reset Password")]

        public AuthenticationResponse ResetPassword(Int64 FacilityId, String UserLogin, int LoginType, string Answer, string Answer2)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin);
                    

                    if (UserResults != null)
                    {
                        // Validate the security answer
                        if (Answer2 == null && Answer == null)
                        {
                            if (UserResults.SecurityAnswer == Answer && UserResults.SecurityAnswer2 == Answer2)
                            {
                                // Generate a random password and Encrypt it
                                Random randomNumber = new Random();
                                string passclear = string.Empty;
                                for (int i = 0; i < 8; i++)
                                {
                                    passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                }
                                string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                                // Update User Record
                                UserResults.Password = passencr;
                                UserResults.LastPasswordChange = System.DateTime.Now;
                                UserResults.ResetPassword = true;
                                db.SaveChanges();

                                // Send Email to new patient with account login information.

                                // Send out email to user that the password has changed

                                SendMessagePassReset(Convert.ToInt16(UserResults.UserRoleId), Convert.ToInt64(UserResults.UserRoleLink), passclear, UserResults.UserEmail);

                                clsEmail objEmail = new clsEmail();

                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserResults.UserLogin,
                                    LoginType = Convert.ToInt16(LoginType),
                                    AccessStatusId = 9,     // Password Reset
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();

                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Security Answer does not match.";
                            }
                        }

                            //add by Talha in case of Direct Reset Password from url i create this condition
                      /*  else if (Answer == "Url" && Answer2 == "Url")
                        {
                            // Generate a random password and Encrypt it
                            Random randomNumber = new Random();
                            string passclear = string.Empty;
                            for (int i = 0; i < 8; i++)
                            {
                                passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                            }
                            string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                            // Update User Record
                            UserResults.Password = passencr;
                            UserResults.LastPasswordChange = System.DateTime.Now;
                            UserResults.ResetPassword = true;
                            db.SaveChanges();

                            // Send Email to new patient with account login information.

                            // Send out email to user that the password has changed

                            SendMessagePassReset(Convert.ToInt16(UserResults.UserRoleId), Convert.ToInt64(UserResults.UserRoleLink), passclear, UserResults.UserEmail);

                            clsEmail objEmail = new clsEmail();

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserResults.UserLogin,
                                LoginType = Convert.ToInt16(LoginType),
                                AccessStatusId = 9,     // Password Reset
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();


                        }*/

///end of Direct Url Reset Password

                        else
                        {
                            if (UserResults.SecurityAnswer.ToLower() != Answer.ToLower() && UserResults.SecurityAnswer2.ToLower() != Answer2.ToLower())
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Answer 1 and answer 2 are invalid answers";
                            }
                            else if (UserResults.SecurityAnswer.ToLower() != Answer.ToLower())
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Answer 1 is an invalid answer";
                            }
                            else if (UserResults.SecurityAnswer2.ToLower() != Answer2.ToLower())
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Answer 2 is an invalid answer";
                            }

                            else if (UserResults.SecurityAnswer.ToLower() == Answer.ToLower() && UserResults.SecurityAnswer2.ToLower() == Answer2.ToLower())
                            {
                                // Generate a random password and Encrypt it
                                Random randomNumber = new Random();
                                string passclear = string.Empty;
                                for (int i = 0; i < 8; i++)
                                {
                                    passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                }
                                string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");

                                // Update User Record
                                UserResults.Password = passencr;
                                UserResults.LastPasswordChange = System.DateTime.Now;
                                UserResults.ResetPassword = true;
                                db.SaveChanges();

                                // Send Email to new patient with account login information.

                                // Send out email to user that the password has changed

                                SendMessagePassReset(Convert.ToInt16(UserResults.UserRoleId), Convert.ToInt64(UserResults.UserRoleLink), passclear, UserResults.UserEmail);

                                clsEmail objEmail = new clsEmail();

                                // Write Audit Record
                                var LoginAudit = new UserLoginAudit()
                                {
                                    UserId = UserResults.UserId,
                                    StartDateTime = System.DateTime.Now,
                                    UserLogin = UserResults.UserLogin,
                                    LoginType = Convert.ToInt16(LoginType),
                                    AccessStatusId = 9,     // Password Reset
                                };
                                db.UserLoginAudits.Add(LoginAudit);
                                db.SaveChanges();

                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Security Answer does not match.";
                            }
                        }


                    }
                    else
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = "Invalid Access ID";
                    }

                }
            }


            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }

        private void SendMessagePassReset(Int16 UserRoleId, Int64 UserId, string passclear, string EmailAddress)
        {
            if (UserRoleId == 4)
            {
                // Send Email to provider notifying of new message
                try
                {
                    // Get the provider's info

                    using (var db = new AMREntities())
                    {
                        string FullName = "";

                        var results = from p in db.Providers
                                      where p.ProviderId == UserId
                                        select new
                                        {
                                            p.ProviderId,
                                            p.Title,
                                            p.FirstName,
                                            p.LastName,
                                        };

                        DataTable dt = clsTableConverter.ToDataTable(results);
                        foreach (DataRow dr in dt.Rows)
                        {
                            FullName = dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"];
                            break;
                        }


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
                                        "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + FullName + "</strong>,</h1>  <br />" +
                                        "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                        "               Your password has been reset to: " + passclear + "<br /><br />" +
                                        "               You can now <a href=\"http://" + host + "/providerportal\" style=\"color: rgb(0, 84, 148);\">log in</a> to your portal account, view and update information, and securely communicate with your patients. " +
                                        "               <br /><br />" +
                                        "               If you did not reset your password, please contact us immediately at support@accessmyrecords.com." +
                                        "               <br /><br />" +
                                        "               Thank you,<br/>" +
                                        "               Your Member Services Team!<br /><br />" +
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

                        objEmail.SendEmailHTML(EmailAddress, "AMR Provider Portal - Password Reset Notification", Msg);

                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                // Send Email to patient notifying of new message
                try
                {
                    // Get the patient's setting for notifications

                    using (var db = new AMREntities())
                    {
                        string FullName = "";
                        if (UserRoleId == 5)
                        {
                            // Patient
                            var results = from p in db.Patients
                                          where p.PatientId == UserId
                                          select new
                                          {
                                              p.PatientId,
                                              p.Title,
                                              p.FirstName,
                                              p.LastName,
                                          };

                            DataTable dt = clsTableConverter.ToDataTable(results);
                            foreach (DataRow dr in dt.Rows)
                            {
                                FullName = dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"];
                                break;
                            }
                        }
                        else if (UserRoleId == 6)
                        {
                            // Representative
                            var results = from p in db.PatientRepresentatives
                                          where p.PatientId == UserId
                                          select new
                                          {
                                              p.PatientId,
                                              p.FirstName,
                                              p.LastName,
                                          };

                            DataTable dt = clsTableConverter.ToDataTable(results);
                            foreach (DataRow dr in dt.Rows)
                            {
                                FullName = dr["FirstName"] + " " + dr["LastName"];
                                break;
                            }
                        }

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
                                        "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + FullName + "</strong>,</h1>  <br />" +
                                        "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                        "               Your password has been reset to: " + passclear + "<br /><br />" +
                                        "               You can now <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">log in</a> to your portal account, view and update information, and securely communicate with your doctor(s). " +
                                        "               <br /><br />" +
                                        "               If you did not reset your password, please contact us immediately at support@accessmyrecords.com." +
                                        "               <br /><br />" +
                                        "               Thank you,<br/>" +
                                        "               Your Member Services Team!<br /><br />" +
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

                        objEmail.SendEmailHTML(EmailAddress, "AMR Patient Portal - Password Reset Notification", Msg);

                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        #region Reset Locked Account
        //------------------------------------------------------------------------
        // Reset Locked Account
        //------------------------------------------------------------------------

        [WebMethod(Description = "Reset Locked Account")]

        public AuthenticationResponse ResetLockedAccount(string Token, Int64 UserId, Int64 FacilityId)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

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
                        // Check If Login Is Valid

                        User UserResults = db.Users.FirstOrDefault(u => u.UserId == UserId);

                        if (UserResults != null)
                        {
                            // Update User Record
                            UserResults.Locked = false;
                            UserResults.FailedLoginCount = 0;
                            db.SaveChanges();
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Access ID";
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

        #region Logout
        //------------------------------------------------------------------------
        // Logout
        //------------------------------------------------------------------------

        [WebMethod(Description = "Logout User")]

        public AuthenticationResponse LogoutUser(Int64 FacilityId, Int64 UserId)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    UserLoginAudit UserResults = db.UserLoginAudits.OrderByDescending(u => u.StartDateTime).FirstOrDefault(u => u.UserId == UserId && u.EndDateTime == null && u.AccessStatusId == 1);

                    if (UserResults != null)
                    {
                        // Make Logout Time
                        UserResults.EndDateTime = System.DateTime.Now;
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion



        #region Authenticate Care Provider
        //------------------------------------------------------------------------
        // Authenticate Care Provider
        //------------------------------------------------------------------------

        [WebMethod(Description = "Authenticate Care Provider")]

        public AuthenticationResponse AuthenticateCareProvider(String UserLogin, string FullName, string Password)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    // Check If Login Is Valid
                    string PassEnc = clsEncryption.Encrypt(Password, "AMRP@ss");

                    User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == "C"+ UserLogin);

                    if (UserResults != null)
                    {
                        if (UserResults.Enabled == false)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Login Is Disabled";

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 2,     // Login Is Disabled
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();
                        }
                        else if (UserResults.Locked == true)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Login Is Locked";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 3,     // Login Is Locked
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();
                        }
                        
                        else if (UserResults.Password != PassEnc || UserResults.UserEmail.ToUpper() != FullName.ToUpper())
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid password.";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 4,     // Invalid Password
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();
                            // Add to invalid login count
                            UserResults.FailedLoginCount++;
                            if (UserResults.FailedLoginCount > 10)
                                UserResults.Locked = true;
                            db.SaveChanges();
                        }
                        else
                        {
                            // Generate Token
                            clsToken objToken = new clsToken();
                            objToken.UserId = UserResults.UserId;
                            objToken.FacilityId = 0;

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 1,     // Valid Login
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.FailedLoginCount = 0;
                            UserResults.LastLoginDate = System.DateTime.Now;
                            db.SaveChanges();

                            if (objToken.Generate())
                            {
                                Response.UserId = UserResults.UserId;
                                Response.Token = objToken.Token;
                                Response.LoginType = Convert.ToInt16(UserResults.UserRoleId);
                                if (UserResults.SecurityAnswer == null)
                                    Response.FirstLogin = true;
                                else
                                    Response.FirstLogin = false;
                                if (!UserResults.ResetPassword == null)
                                    Response.ResetPassword = Convert.ToBoolean(UserResults.ResetPassword);
                                else
                                    Response.ResetPassword = true;
                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Error Generating Token";
                            }
                        }


                    }
                    else
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = "Invalid Access ID";
                        // Write Audit Record
                        var LoginAudit = new UserLoginAudit()
                        {
                            UserId = 0,
                            StartDateTime = System.DateTime.Now,
                            UserLogin = UserLogin,
                            LoginType = 7,
                            EndDateTime = System.DateTime.Now,
                            AccessStatusId = 6,     // Invalid  Account
                        };
                        db.UserLoginAudits.Add(LoginAudit);
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion

        #region Authenticate Medical Summary
        //------------------------------------------------------------------------
        // Authenticate Medical Summary
        //------------------------------------------------------------------------

        [WebMethod(Description = "Authenticate Medical Summary")]

        public AuthenticationResponse AuthenticateMedicalSummary(String UserLogin, string FullName)
        {
            AuthenticationResponse Response = new AuthenticationResponse();

            Response.Valid = true;
            Response.ErrorMessage = "";
            Response.Token = "";

            try
            {
                using (var db = new AMREntities())
                {
                    // Check If Login Is Valid
                    User UserResults = db.Users.FirstOrDefault(u => u.UserLogin == "S" + UserLogin);

                    if (UserResults != null)
                    {
                        if (UserResults.Enabled == false)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Only Available With Premium";

                        }
                        else if (UserResults.Locked == true)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Login Is Locked";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 3,     // Login Is Locked
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();
                        }

                        else if (UserResults.UserEmail.ToUpper() != FullName.ToUpper())
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Login.";
                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                EndDateTime = System.DateTime.Now,
                                AccessStatusId = 4,     // Invalid Password
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();
                            // Add to invalid login count
                            UserResults.FailedLoginCount++;
                            if (UserResults.FailedLoginCount > 10)
                                UserResults.Locked = true;
                            db.SaveChanges();
                        }
                        else
                        {
                            // Generate Token
                            clsToken objToken = new clsToken();
                            objToken.UserId = UserResults.UserId;
                            objToken.FacilityId = 0;

                            // Write Audit Record
                            var LoginAudit = new UserLoginAudit()
                            {
                                UserId = UserResults.UserId,
                                StartDateTime = System.DateTime.Now,
                                UserLogin = UserLogin,
                                LoginType = Convert.ToInt16(UserResults.UserRoleId),
                                //EndDateTime = System.DateTime.Now,
                                AccessStatusId = 1,     // Valid Login
                            };
                            db.UserLoginAudits.Add(LoginAudit);
                            db.SaveChanges();

                            // Update User Record
                            UserResults.FailedLoginCount = 0;
                            UserResults.LastLoginDate = System.DateTime.Now;
                            db.SaveChanges();

                            if (objToken.Generate())
                            {
                                Response.UserId = UserResults.UserId;
                                Response.Token = objToken.Token;
                                Response.LoginType = Convert.ToInt16(UserResults.UserRoleId);
                                if (UserResults.SecurityAnswer == null)
                                    Response.FirstLogin = true;
                                else
                                    Response.FirstLogin = false;
                            }
                            else
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "Error Generating Token";
                            }
                        }


                    }
                    else
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = "Invalid Access ID";
                        // Write Audit Record
                        var LoginAudit = new UserLoginAudit()
                        {
                            UserId = 0,
                            StartDateTime = System.DateTime.Now,
                            UserLogin = UserLogin,
                            LoginType = 8,
                            EndDateTime = System.DateTime.Now,
                            AccessStatusId = 6,     // Invalid  Account
                        };
                        db.UserLoginAudits.Add(LoginAudit);
                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                Response.Valid = false;
                Response.ErrorMessage = ex.Message;
            }


            return Response;
        }
        #endregion

        #region Get User Info From Login Type & Role Link
        //------------------------------------------------------------------------
        // Get User Info From Login Type & Role Link
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get User Info From Login Type & Role Link")]

        public UserData GetUserTypeLink(string Token, Int64 UserId, Int64 FacilityId, int LoginType, Int64 RoleLink)
        {
            UserData Response = new UserData();

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
                        User UserResults = db.Users.FirstOrDefault(u => u.UserRoleId == LoginType && u.UserRoleLink == RoleLink);

                        if (UserResults != null)
                        {
                            Response.UserId = UserResults.UserId;
                            Response.Email = UserResults.UserEmail;
                        }
                        else
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Invalid Id";
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

    }
}
