// Class Name    : clsToken.cs
// Date Created  : 10/22/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : 2nd Tier Access To Token Table
// MM/DD/YYYY XXX Description
//
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMR.Data
{
    public class clsToken
    {
        #region Local Variables
        //------------------------------------------------------------------------
        // Initialize Class Level Variables
        //------------------------------------------------------------------------

        bool mbolValid = false;
        string mstrErrorMessage = "";
        string mstrConnectionString = "";

        string mstrToken = "";
        Int64 mbintUserId = 0;
        Int64 mbintFacilityId = 0;
        string mstrLastUpdated = "";
        #endregion

        #region Public Variables
        //------------------------------------------------------------------------
        // Allows external routines to read and update values
        //------------------------------------------------------------------------

        public string ErrorMsg
        {
            get { return mstrErrorMessage; }
            set { mstrErrorMessage = value; }
        }
        public string ConnectionString
        {
            get { return mstrConnectionString; }
            set { mstrConnectionString = value; }
        }
        public string Token
        {
            get { return mstrToken; }
            set { mstrToken = value.Replace("'", "''"); }
        }
        public Int64 UserId
        {
            get { return mbintUserId; }
            set { mbintUserId = value; }
        }
        public Int64 FacilityId
        {
            get { return mbintFacilityId; }
            set { mbintFacilityId = value; }
        }
        public string LastUpdated
        {
            get { return mstrLastUpdated; }
            set
            {
                mstrLastUpdated = value;
                if (mstrLastUpdated == "")
                    mstrLastUpdated = "1/1/1900";
            }
        }
        #endregion


        #region Generate Token
        //------------------------------------------------------------------------
        // Generate Token
        //------------------------------------------------------------------------

        public bool Generate()
        {
            mbolValid = true;

            try
            {
                // Build Token String

                mstrToken = System.DateTime.Now.Minute.ToString().PadLeft(2, '0')
                             + mbintUserId.ToString().PadLeft(7, '0')
                             + System.DateTime.Now.Day.ToString().PadLeft(2, '0')
                             + mbintFacilityId.ToString().PadLeft(5, '0')
                             + System.DateTime.Now.Month.ToString().PadLeft(2, '0')
                             + System.DateTime.Now.Hour.ToString().PadLeft(2, '0');

                using (var db = new AMREntities())
                {
                    var NewToken = new Token()
                    {
                        TokenId = mstrToken,
                        UserId = mbintUserId,
                        FacilityId = mbintFacilityId,
                        LastUpdated = System.DateTime.Now,
                    };
                    db.Tokens.Add(NewToken);
                    // Added try catch to cach error if token already exists.  This hapens when generated within same minute.
                    try
                    {
                        db.SaveChanges();
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                mstrErrorMessage = ex.Message.ToString();
                mbolValid = false;
            }
            return mbolValid;
        }
        #endregion

        #region Validate
        //------------------------------------------------------------------------
        // Validate Token
        //------------------------------------------------------------------------

        public bool Validate()
        {
            mbolValid = true;
            try
            {
                using (var db = new AMREntities())
                {
                    Token results = db.Tokens.FirstOrDefault
                        (t => t.TokenId == mstrToken
                        && t.UserId == mbintUserId
                        && t.FacilityId == mbintFacilityId);
                    if (results != null)
                    {
                        // Update Time Stamp On Token
                        results.LastUpdated = System.DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        mstrErrorMessage = "Invalid token";
                        mbolValid = false;
                    }
                }

            }
            catch (Exception ex)
            {
                mstrErrorMessage = ex.Message.ToString();
                mbolValid = false;
            }
            return mbolValid;
        }

        #endregion
    }
}
