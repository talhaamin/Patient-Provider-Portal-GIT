// Service Name  : AuthentiationService
// Date Created  : 10/28/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Integration Web Service For Authentication
//                 
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
            public string Token;
            public string ErrorMessage;
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

                    User results = db.Users.FirstOrDefault(u => u.UserLogin == UserLogin && u.Password == PassEnc && u.UserRoleId == 2 && u.UserRoleLink == FacilityId);

                    if (results != null)
                    {
                        if (results.Enabled == false)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Login Is Disabled";
                        }
                        else if (results.Locked == true)
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = "Login Is Locked";
                        }
                        else
                        {
                            // Generate Token
                            clsToken objToken = new clsToken();
                            objToken.UserId = results.UserId;
                            objToken.FacilityId = FacilityId;

                            if (objToken.Generate())
                                Response.Token = objToken.Token;
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
                        Response.ErrorMessage = "Invalid Login / Password";
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

    }
}
