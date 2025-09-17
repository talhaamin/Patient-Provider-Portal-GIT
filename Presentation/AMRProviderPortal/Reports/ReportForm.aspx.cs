using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices;
using System.Net;
using System.Security.Principal;


namespace AMRPatientPortal.Views.Report
{
    public partial class ReportForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  var requestID = Request.QueryString["r"];

              //  ReportRequest request;

              //  using (var context = new ReportContext())
              //  {
               //     request = context.ReportRequests.Include("Parameters").Where(one => one.UniqueId == id).FirstOrDefault();
               // }

                string filename = Session["reportPath"].ToString();
                mainReportViewer.ServerReport.ReportServerUrl =
                    new Uri(ConfigurationManager.AppSettings["ReportServerUrl"]);
                mainReportViewer.ServerReport.ReportPath =filename;
                mainReportViewer.ServerReport.ReportServerCredentials = new MyReportServerCredentials();
                mainReportViewer.ProcessingMode = ProcessingMode.Remote;
                mainReportViewer.ShowParameterPrompts = false;
                mainReportViewer.ShowRefreshButton = false;
                mainReportViewer.ShowWaitControlCancelLink = false;
                mainReportViewer.ShowBackButton = false;
                mainReportViewer.ShowCredentialPrompts = false;

                mainReportViewer.ServerReport.Refresh();
                var reportParameters = (Dictionary<string,string>)Session["reportParameters"];
                foreach (var parameter in reportParameters)
                {
                    mainReportViewer.ServerReport.SetParameters(new List<ReportParameter>()
              {
                new ReportParameter
                  (parameter.Key, parameter.Value)
              });
        }
                    //var parameterName = parameter.ParameterName;
                    //if (parameterName.StartsWith("@"))
                    //{
                    //    parameterName = parameterName.Substring(1);
                    //}
                    //parametersCollection.Add(new ReportParameter(parameterName, parameter.ParameterValue, false));
                //}
              //  mainReportViewer.ServerReport.SetParameters(parametersCollection);
               
            }



        }
    }

    [Serializable]
    public sealed class MyReportServerCredentials :
        IReportServerCredentials
    {
        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use the default Windows user.  Credentials will be
                // provided by the NetworkCredentials property.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Read the user information from the Web.config file.  
                // By reading the information on demand instead of 
                // storing it, the credentials will not be stored in 
                // session, reducing the vulnerable surface area to the
                // Web.config file, which can be secured with an ACL.

                // User name
                string userName =
                    ConfigurationManager.AppSettings
                        ["ReportServerUser"];

                if (string.IsNullOrEmpty(userName))
                    throw new Exception(
                        "Missing user name from web.config file");

                // Password
                string password =
                    ConfigurationManager.AppSettings
                        ["ReportServerPassword"];

                if (string.IsNullOrEmpty(password))
                    throw new Exception(
                        "Missing password from web.config file");

                // Domain
                string domain =
                    ConfigurationManager.AppSettings
                        ["ReportServerDomain"];

                if (string.IsNullOrEmpty(domain))
                    throw new Exception(
                        "Missing domain from web.config file");

                return new NetworkCredential(userName, password, domain);
            }
        }

        public bool GetFormsCredentials(out Cookie authCookie,
                    out string userName, out string password,
                    out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            // Not using form credentials
            return false;
        }
    }
}