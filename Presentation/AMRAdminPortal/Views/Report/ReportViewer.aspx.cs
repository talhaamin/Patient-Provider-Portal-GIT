using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.Reporting.WebForms;
using System.Web.Security;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Security;
using System.Security.Principal;
using System.Data;
using AMR.Core.Utilities;


namespace AMRAdminPortal.Views.Report
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Specify the report server
           ReportViewer1.ServerReport.ReportServerUrl = new Uri(WebConfigurationManager.AppSettings["ReportServerURL"]);

            //Specify the report name
            ReportViewer1.ServerReport.ReportPath = Session["reportPath"].ToString();

            //Specify the server credentials
           // IReportServerCredentials irsc = new CustomReportCredentials(WebConfigurationManager.AppSettings["ReportServerUser"], WebConfigurationManager.AppSettings["ReportServerPassword"], WebConfigurationManager.AppSettings["ReportServerDomain"]);
           // ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            /*
             * With the report specified, hydrate the report
             * parameters based on the values in the
             * reportParameters hash.
             */
            var reportParameters = (Dictionary<string, string>)Session["reportParameters"];

            //foreach (var item in reportParameters)
            //{
            //    ReportViewer1.ServerReport.SetParameters(new List<ReportParameter>()
            //  {
            //    new ReportParameter(item.Key, item.Value)
            //  });
            //}
        }
    }
}