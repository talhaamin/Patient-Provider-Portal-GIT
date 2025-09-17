using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Core.Utilities
{
    public class GlobalVar
    {
        private Int64 mbintUserId = 0;
        private Int64 mbintFacilityId = 0;
        private Int64 mVisitId = 0;
        private Int64 mFacilitySelectId = 0;
        private string mstrToken = "";
        private string mstrUserLogin = "";
        private Int64 mPatientId = 0;
        private string mPatientName = "";
        private string mPortalType = "";
        private bool mPremiumFlag = false;
        private bool mActiveflag = false;
        private bool mResetPassword = false;
        private bool mGeneralMessageAvailable=false;
        private bool mAppointmentMessageAvailable=false;
        private bool mMedicationMessageAvailable=false;
        private bool mReferralMessageAvailable = false;
        private bool mFirstLogin = false;
        private string mPatientRep = "";
        private string mBuildNo = "7.0.19";

        public string GetData()
        {
            return mbintUserId.ToString() + "|" + mbintFacilityId.ToString() + "|" + mstrToken + "|" + mstrUserLogin + "|" + mPatientId.ToString() + "|" + mPatientName + "|" + mPortalType + "|" + mPremiumFlag + "|" + mActiveflag + "|" + mGeneralMessageAvailable + "|" + mAppointmentMessageAvailable + "|" + mMedicationMessageAvailable + "|" + mReferralMessageAvailable + "|" + mVisitId + "|" + mFacilitySelectId + "|" + mFirstLogin + "|" + mPatientRep + "|" + mBuildNo + "|" + mResetPassword;
        }
        public void SetData(string data)
        {
            string[] strArr=data.Split('|');

            mbintUserId = Convert.ToInt64(strArr[0]);
            mbintFacilityId = Convert.ToInt64(strArr[1]);
            mstrToken =strArr[2];
            mstrUserLogin = strArr[3];
            if (strArr.Length > 4)
            {
                mPatientId  = Convert.ToInt64(strArr[4]);
                mPatientName  = strArr[5];
            }
            mPortalType = strArr[6];
            mPremiumFlag = Convert.ToBoolean(strArr[7]);
            mActiveflag = Convert.ToBoolean(strArr[8]);
            mGeneralMessageAvailable = Convert.ToBoolean(strArr[9]);
            mAppointmentMessageAvailable = Convert.ToBoolean(strArr[10]);
            mMedicationMessageAvailable = Convert.ToBoolean(strArr[11]);
            mReferralMessageAvailable = Convert.ToBoolean(strArr[12]);
            mVisitId = Convert.ToInt64(strArr[13]);
            mFacilitySelectId = Convert.ToInt64(strArr[14]);
            mFirstLogin = Convert.ToBoolean(strArr[15]);
            mPatientRep = Convert.ToString(strArr[16]);
            mBuildNo = Convert.ToString(strArr[17]);
            mResetPassword = Convert.ToBoolean(strArr[18]);
        }
        public Int64 UserId
        {
            get { return mbintUserId; }
            set { mbintUserId = value; }
        }
        public string UserLoginEx
        {
            get
            {
           

                return mstrUserLogin;


            }
            set { mstrUserLogin = value; }
        }
        public string UserLogin
        {
            get {

                if (mstrUserLogin.StartsWith("R"))
                {
                    return mstrUserLogin.Substring(1, mstrUserLogin.Length - 1);
                }
                else
                {
                    return mstrUserLogin;
                }
            
            
            }
            set { mstrUserLogin = value; }
        }
       
         public string PatientName
        {
            get { return mPatientName; }
            set { mPatientName = value; }
        }

         public Int64 PatientId
         {
             get {

                return mPatientId; 
             
             
             }
             set { mPatientId = value; }
         }


        public Int64 FacilityId
        {
            get { return mbintFacilityId; }
            set { mbintFacilityId = value; }
        }

        public  string Token
        {
            get { return mstrToken; }
            set { mstrToken = value; }
        }

        public bool ResetPasswordFlag
        {
            get { return mResetPassword; }
            set { mResetPassword = value; }
        }
        public string PortalType
        {
            get { return mPortalType; }
            set { mPortalType = value; }
        }
        public bool PremiumFlag
        {
            get { return mPremiumFlag; }
            set { mPremiumFlag = value; }
        }
        public bool ActiveFlag
        {
            get { return mActiveflag; }
            set { mActiveflag = value; }
        }

        public bool GeneralMessageAvailable
        {
            get { return mGeneralMessageAvailable; }
            set { mGeneralMessageAvailable = value; }
        }

        public bool AppointmentMessageAvailable
        {
            get { return mAppointmentMessageAvailable; }
            set { mAppointmentMessageAvailable = value; }
        }

        public bool MedicationMessageAvailable
        {
            get { return mMedicationMessageAvailable; }
            set { mMedicationMessageAvailable = value; }
        }

        public bool ReferralMessageAvailable
        {
            get { return mReferralMessageAvailable; }
            set { mReferralMessageAvailable = value; }
        }
        public Int64 VisitId
        {
            get { return mVisitId; }
            set { mVisitId = value; }
        }
        public Int64 FacilitySelectId
        {
            get { return mFacilitySelectId; }
            set { mFacilitySelectId = value; }
        }
        public bool FirstLogin
        {
            get { return mFirstLogin; }
            set { mFirstLogin = value; }
        }
        public string PatientRep
        {
            get { return mPatientRep; }
            set { mPatientRep = value; }
        }
        public string BuildNo
        {
            get { return mBuildNo; }
            set { mBuildNo = value; }
        }
    }
}
