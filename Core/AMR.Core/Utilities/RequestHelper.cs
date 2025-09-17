using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AMR.Core.Utilities
{
    public static class RequestHelper
    {
        public static GlobalVar MyGlobalVar { 
            
            get
            {
                return TicketHelper.GetUserData();
            } 
        
        }

        public static GlobalVar MyCareProviderGlobalVar
        {

            get
            {
                return TicketHelper.GetCareProviderUserData();
            }

        }

        public static GlobalVar MyMedicalSummaryGlobalVar
        {

            get
            {
                return TicketHelper.GetMedicalSummaryUserData();
            }

        }
        public static GlobalVar MyAdminGlobalVar
        {
            get {
                return TicketHelper.GetAdminUserData();
            }
        }
    }
}
