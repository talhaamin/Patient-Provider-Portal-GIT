using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public static class GlobalVar
    {
        static Int64 mbintUserId = 0;
        static Int64 mbintFacilityId = 0;
        static string mstrToken = "";


        public static Int64 UserId
        {
            get { return mbintUserId; }
            set { mbintUserId = value; }
        }

        public static Int64 FacilityId
        {
            get { return mbintFacilityId; }
            set { mbintFacilityId = value; }
        }

        public static string Token
        {
            get { return mstrToken; }
            set { mstrToken = value; }
        }


    }
}
