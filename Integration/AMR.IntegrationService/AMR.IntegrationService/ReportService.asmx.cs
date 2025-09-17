// Service Name  : PatientService
// Date Created  : 04/18/2014
// Written By    : Stephen Farkas
// Version       : 2014.01
// Description   : Integration Web Service For Reporting
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
using System.Xml;
using System.Data.Entity;
using System.Text;

namespace AMR.IntegrationService
{
    #region Data Structure
    //------------------------------------------------------------------------
    // Define Data Structure
    //------------------------------------------------------------------------
    public struct SecureMessageParms
    {
        public Int64 FacilityId;
        public DateTime LastCheckTD;
    }
    public struct SecureMessageResponse
    {
        public bool Valid;
        public string ErrorMessage;
        public DateTime LastCheckTD;
        public DataTable dtMessages;
    }

    public struct LoginLogParms
    {
        public Int64 FacilityId;
        public DateTime LastCheckTD;
    }
    public struct LoginLogResponse
    {
        public bool Valid;
        public string ErrorMessage;
        public DateTime LastCheckTD;
        public DataTable dtLogins;
    }
    #endregion


    [WebService(Namespace = "http://tempuri.org/", Name="ReportWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ReportService : System.Web.Services.WebService
    {

        #region Secure Message
        //------------------------------------------------------------------------
        // Secure Message 
        //------------------------------------------------------------------------
        [WebMethod(Description = "Secure Message")]
        public SecureMessageResponse SecureMessage(Int64 FacilityId, Int64 UserId, string Token, SecureMessageParms Parms)
        {
            SecureMessageResponse SMResponse = new SecureMessageResponse();

            SMResponse.Valid = true;

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
                        SMResponse.LastCheckTD = System.DateTime.Now;
                        var results = from v in db.vwRptSecureMessages
                                      where v.DateCreated > Parms.LastCheckTD
                                      && v.DateCreated <= SMResponse.LastCheckTD
                                      && v.FacilityId == Parms.FacilityId
                                      select new
                                      {
                                          v.PatientId,
                                          v.DateCreated,
                                          v.FirstName,
                                          v.LastName,
                                          v.License
                                      };

                        SMResponse.dtMessages = clsTableConverter.ToDataTable(results);
                        SMResponse.dtMessages.TableName = "SecureMessages";

                        SMResponse.Valid = true;
                        SMResponse.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    SMResponse.Valid = false;
                    SMResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                SMResponse.Valid = false;
                SMResponse.ErrorMessage = "Invalid Token";
            }
            return SMResponse;
        }
        #endregion

        #region Patient Login
        //------------------------------------------------------------------------
        // Patient Login
        //------------------------------------------------------------------------
        [WebMethod(Description = "Patient Login")]
        public LoginLogResponse PatientLogin(Int64 FacilityId, Int64 UserId, string Token, LoginLogParms Parms)
        {
            LoginLogResponse LogResponse = new LoginLogResponse();

            LogResponse.Valid = true;

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

                        LogResponse.LastCheckTD = System.DateTime.Now;
                        //vwRptPatientLogin

                        var results = from v in db.vwRptPatientLogins
                                      where v.StartDateTime > Parms.LastCheckTD
                                      && v.StartDateTime <= LogResponse.LastCheckTD
                                      && v.FacilityId == Parms.FacilityId
                                      select new
                                      {
                                          v.PatientId,
                                          v.StartDateTime
                                      };

                        LogResponse.dtLogins = clsTableConverter.ToDataTable(results);
                        LogResponse.dtLogins.TableName = "LoginLog";

                        LogResponse.Valid = true;
                        LogResponse.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    LogResponse.Valid = false;
                    LogResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                LogResponse.Valid = false;
                LogResponse.ErrorMessage = "Invalid Token";
            }
            return LogResponse;
        }
        #endregion
    }
}
