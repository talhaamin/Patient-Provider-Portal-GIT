using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Security.Principal;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Utilities;
    using Microsoft.Reporting.WebForms;


    /// <summary>
    /// Summary description for CustomReportCredentials
    /// </summary>
    /// [Serializable]
    public class CustomReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {


        private string _UserName;
        private string _PassWord;
        private string _DomainName;

        #region IReportServerCredentials Members
        public CustomReportCredentials(String UserName, String Password, String Domain)
        {
            _UserName = UserName;
            _PassWord = Password;
            _DomainName = Domain;
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCookie, out string userName, out string password, out string authority)
        {
            authCookie = null;
            userName = null;
            password = null;
            authority = null;

            return false;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {

                System.Security.Principal.WindowsImpersonationContext impersonationContext = null;
                System.Security.Principal.WindowsIdentity currentWindowsIdentity = null;
                currentWindowsIdentity = (System.Security.Principal.WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                impersonationContext = currentWindowsIdentity.Impersonate();

                //Insert your code that runs under the security context of the authenticating user here.

                impersonationContext.Undo();

                return currentWindowsIdentity; // not use ImpersonationUser
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                string userName = "";
                string domainName = "";
                string password = "";

                return new System.Net.NetworkCredential(userName, password, domainName);
            }
        }

        #endregion

    }
}
