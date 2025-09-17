// Service Name  : ConfigService
// Date Created  : 06/18/2014
// Written By    : Stephen Farkas
// Version       : 2014.01
// Description   : Web Service For Configuration Tables
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


namespace AMR.IntegrationService
{

    [WebService(Namespace = "http://tempuri.org/", Name = "ConfigWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ConfigService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct FacilitySetupData
        {
            public Int64 OrganizationId;
            public string OrganizationName;
            public string OrganizationAddress1;
            public string OrganizationAddress2;
            public string OrganizationAddress3;
            public string OrganizationCity;
            public string OrganizationState;
            public string OrganizationPostalCode;
            public string OrganizationCountryCode;
            public Int64 PracticeId;
            public string PracticeName;
            public string PracticeAddress1;
            public string PracticeAddress2;
            public string PracticeAddress3;
            public string PracticeCity;
            public string PracticeState;
            public string PracticePostalCode;
            public string PracticeCountryCode;
            public Int64 FacilityId;
            public int EMRSystemId;
            public int FacilityTypeId;
            public string FacilityName;
            public string FacilityAddress1;
            public string FacilityAddress2;
            public string FacilityAddress3;
            public string FacilityCity;
            public string FacilityState;
            public string FacilityCountryCode;
            public string FacilityPostalCode;
            public string FacilityPhone;
            public string FacilityFax;
            public string FacilityBillAddress1;
            public string FacilityBillAddress2;
            public string FacilityBillAddress3;
            public string FacilityBillCity;
            public string FacilityBillState;
            public string FacilityBillPostalCode;
            public string FacilityBillCountryCode;
            public string FacilityBillPhone;
            public string FacilityBillFax;
            public bool GeneralMessageAvailable;
            public int GeneralMessageNotify;
            public string GeneralMessageEmail;
            public bool AppointmentMessageAvailable;
            public int AppointmentMessageNotify;
            public string AppointmentMessageEmail;
            public bool MedicationMessageAvailable;
            public int MedicationMessageNotify;
            public string MedicationMessageEmail;
            public bool ReferalMessageAvailable;
            public int ReferralMessageNotify;
            public string ReferralMessageEmail;
            public bool PremiumAvailable;
            public Int16 OnlineBillPayment;
            public string ContactEmail;
        }

        public struct FacilitySetupResp
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 FacilityId;
            public Int64 UserId;
            public string Logon;
            public string Password;
        }
        #endregion

        #region Save Practice Data
        //------------------------------------------------------------------------
        // Save Practice Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Practice Data")]
        public FacilitySetupResp SavePracticeData(FacilitySetupData FacilityData, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilitySetupResp Response = new FacilitySetupResp();
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
                        string error = "";
                        // Check passed data to verify that it is correct
                        if (FacilityData.OrganizationId > 0)
                        {
                            // Make sure it exits
                            Organization results = db.Organizations.FirstOrDefault(o => o.OrganizationId == FacilityData.OrganizationId);
                            if (results == null)
                                error = error + "Organization id is invalid; ";

                        }
                        if (FacilityData.OrganizationName == "")
                            error = error + "Organization name is required; ";
                        if (FacilityData.OrganizationAddress1 == "")
                            error = error + "Organization address is required; ";
                        if (FacilityData.OrganizationCity == "")
                            error = error + "Organization city is required; ";
                        if (FacilityData.OrganizationState == "")
                            error = error + "Organization state is required; ";
                        if (FacilityData.OrganizationPostalCode == "")
                            error = error + "Organization postal code is required; ";
                        if (FacilityData.OrganizationCountryCode == "")
                            error = error + "Organization country code is required; ";
                        else
                        {
                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == FacilityData.OrganizationCountryCode);
                            if (results == null)
                                error = error + "Organization Country Code is invalid; ";
                        }

                        if (FacilityData.PracticeId > 0)
                        {
                            // Make sure it exits
                            Practice results = db.Practices.FirstOrDefault(p => p.PracticeId == FacilityData.PracticeId);
                            if (results == null)
                                error = error + "Practice id is invalid; ";

                        }

                        if (FacilityData.PracticeName == "")
                            error = error + "Practice name is required; ";
                        if (FacilityData.PracticeAddress1 == "")
                            error = error + "Practice address is required; ";
                        if (FacilityData.PracticeCity == "")
                            error = error + "Practice city is required; ";
                        if (FacilityData.PracticeState == "")
                            error = error + "Practice state is required; ";
                        if (FacilityData.PracticePostalCode == "")
                            error = error + "Practice postal code is required; ";
                        if (FacilityData.PracticeCountryCode == "")
                            error = error + "Practice country code is required; ";
                        else
                        {
                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == FacilityData.PracticeCountryCode);
                            if (results == null)
                                error = error + "Practice Country Code is invalid; ";
                        }

                        if (FacilityData.FacilityId > 0)
                        {
                            // Make sure it exits
                            Facility results = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityData.FacilityId);
                            if (results == null)
                                error = error + "Facility id is invalid; ";

                        }

                        // Make sure valid interface system type passed
                        C_EMRSystem Eresults = db.C_EMRSystem.FirstOrDefault(e => e.EMRSystemId == FacilityData.EMRSystemId);
                        if (Eresults == null)
                            error = error + "EMR System id is invalid; ";

                        if (FacilityData.FacilityName == "")
                            error = error + "Facility name is required; ";
                        if (FacilityData.FacilityAddress1 == "")
                            error = error + "Facility address is required; ";
                        if (FacilityData.FacilityCity == "")
                            error = error + "Facility city is required; ";
                        if (FacilityData.FacilityState == "")
                            error = error + "Facility state is required; ";
                        if (FacilityData.FacilityPostalCode == "")
                            error = error + "Facility postal code is required; ";
                        if (FacilityData.FacilityCountryCode == "")
                            error = error + "Facility country code is required; ";
                        else
                        {
                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == FacilityData.FacilityCountryCode);
                            if (results == null)
                                error = error + "Facility Country Code is invalid; ";
                        }
                        if (FacilityData.FacilityBillCountryCode != "")
                        {
                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == FacilityData.FacilityBillCountryCode);
                            if (results == null)
                                error = error + "Facility Bill Country Code is invalid; ";
                        }


                        if (FacilityData.ContactEmail == "")
                            error = error + "Contact Email is required; ";


                        if (error != "")
                        {
                            Response.Valid = false;
                            Response.ErrorMessage = error;
                        }
                        else
                        {
                            // Make sure none of the text fields are too long
                            if (FacilityData.OrganizationName.Length > 128)
                                FacilityData.OrganizationName = FacilityData.OrganizationName.Substring(0, 128);
                            if (FacilityData.OrganizationAddress1.Length > 50)
                                FacilityData.OrganizationAddress1 = FacilityData.OrganizationAddress1.Substring(0, 50);
                            if (FacilityData.OrganizationAddress2.Length > 50)
                                FacilityData.OrganizationAddress2 = FacilityData.OrganizationAddress2.Substring(0, 50);
                            if (FacilityData.OrganizationAddress3.Length > 50)
                                FacilityData.OrganizationAddress3 = FacilityData.OrganizationAddress3.Substring(0, 50);
                            if (FacilityData.OrganizationCity.Length > 30)
                                FacilityData.OrganizationCity = FacilityData.OrganizationCity.Substring(0, 30);
                            if (FacilityData.OrganizationState.Length > 10)
                                FacilityData.OrganizationState = FacilityData.OrganizationState.Substring(0, 10);
                            if (FacilityData.OrganizationPostalCode.Length > 10)
                                FacilityData.OrganizationPostalCode = FacilityData.OrganizationPostalCode.Substring(0, 10);

                            if (FacilityData.PracticeName.Length > 128)
                                FacilityData.PracticeName = FacilityData.PracticeName.Substring(0, 128);
                            if (FacilityData.PracticeAddress1.Length > 50)
                                FacilityData.PracticeAddress1 = FacilityData.PracticeAddress1.Substring(0, 50);
                            if (FacilityData.PracticeAddress2.Length > 50)
                                FacilityData.PracticeAddress2 = FacilityData.PracticeAddress2.Substring(0, 50);
                            if (FacilityData.PracticeAddress3.Length > 50)
                                FacilityData.PracticeAddress3 = FacilityData.PracticeAddress3.Substring(0, 50);
                            if (FacilityData.PracticeCity.Length > 30)
                                FacilityData.PracticeCity = FacilityData.PracticeCity.Substring(0, 30);
                            if (FacilityData.PracticeState.Length > 10)
                                FacilityData.PracticeState = FacilityData.PracticeState.Substring(0, 10);
                            if (FacilityData.PracticePostalCode.Length > 10)
                                FacilityData.PracticePostalCode = FacilityData.PracticePostalCode.Substring(0, 10);

                            if (FacilityData.FacilityName.Length > 128)
                                FacilityData.FacilityName = FacilityData.FacilityName.Substring(0, 128);
                            if (FacilityData.FacilityAddress1.Length > 50)
                                FacilityData.FacilityAddress1 = FacilityData.FacilityAddress1.Substring(0, 50);
                            if (FacilityData.FacilityAddress2.Length > 50)
                                FacilityData.FacilityAddress2 = FacilityData.FacilityAddress2.Substring(0, 50);
                            if (FacilityData.FacilityAddress3.Length > 50)
                                FacilityData.FacilityAddress3 = FacilityData.FacilityAddress3.Substring(0, 50);
                            if (FacilityData.FacilityCity.Length > 30)
                                FacilityData.FacilityCity = FacilityData.FacilityCity.Substring(0, 30);
                            if (FacilityData.FacilityState.Length > 10)
                                FacilityData.FacilityState = FacilityData.FacilityState.Substring(0, 10);
                            if (FacilityData.FacilityPostalCode.Length > 10)
                                FacilityData.FacilityPostalCode = FacilityData.FacilityPostalCode.Substring(0, 10);

                            if (FacilityData.FacilityBillAddress1.Length > 50)
                                FacilityData.FacilityBillAddress1 = FacilityData.FacilityBillAddress1.Substring(0, 50);
                            if (FacilityData.FacilityBillAddress2.Length > 50)
                                FacilityData.FacilityBillAddress2 = FacilityData.FacilityBillAddress2.Substring(0, 50);
                            if (FacilityData.FacilityBillAddress3.Length > 50)
                                FacilityData.FacilityBillAddress3 = FacilityData.FacilityBillAddress3.Substring(0, 50);
                            if (FacilityData.FacilityBillCity.Length > 30)
                                FacilityData.FacilityBillCity = FacilityData.FacilityBillCity.Substring(0, 30);
                            if (FacilityData.FacilityBillState.Length > 10)
                                FacilityData.FacilityBillState = FacilityData.FacilityBillState.Substring(0, 10);
                            if (FacilityData.FacilityBillPostalCode.Length > 10)
                                FacilityData.FacilityBillPostalCode = FacilityData.FacilityBillPostalCode.Substring(0, 10);


                            if (FacilityData.GeneralMessageEmail.Length > 256)
                                FacilityData.GeneralMessageEmail = FacilityData.GeneralMessageEmail.Substring(0, 256);
                            if (FacilityData.AppointmentMessageEmail.Length > 256)
                                FacilityData.AppointmentMessageEmail = FacilityData.AppointmentMessageEmail.Substring(0, 256);
                            if (FacilityData.MedicationMessageEmail.Length > 256)
                                FacilityData.MedicationMessageEmail = FacilityData.MedicationMessageEmail.Substring(0, 256);
                            if (FacilityData.ReferralMessageEmail.Length > 256)
                                FacilityData.ReferralMessageEmail = FacilityData.ReferralMessageEmail.Substring(0, 256);

                            if (FacilityData.ContactEmail.Length > 60)
                                FacilityData.ContactEmail = FacilityData.ContactEmail.Substring(0, 60);


                            // Save Data
                            if (FacilityData.OrganizationId == 0)
                            {

                                Organization OrganizationResp = db.Organizations.FirstOrDefault(p => p.OrganizationName == FacilityData.OrganizationName && p.Address1 == FacilityData.OrganizationAddress1 && p.PostalCode == FacilityData.OrganizationPostalCode);

                                if (OrganizationResp != null)
                                {
                                    FacilityData.OrganizationId = OrganizationResp.OrganizationId;
                                }
                            }
                            if (FacilityData.OrganizationId == 0)
                            {

                                // Add Organization
                                var Organization = new Organization()
                                {

                                    OrganizationId = FacilityData.OrganizationId,
                                    OrganizationName = FacilityData.OrganizationName,
                                    Address1 = FacilityData.OrganizationAddress1,
                                    Address2 = FacilityData.OrganizationAddress2,
                                    Address3 = FacilityData.OrganizationAddress3,
                                    City = FacilityData.OrganizationCity,
                                    State = FacilityData.OrganizationState,
                                    PostalCode = FacilityData.OrganizationPostalCode,
                                    CountryCode = FacilityData.OrganizationCountryCode,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Organizations.Add(Organization);

                                db.SaveChanges();

                                FacilityData.OrganizationId = Organization.OrganizationId;
                            }
                            else
                            {
                                // Update Organization Info 

                                Organization OrganizationResp = db.Organizations.FirstOrDefault(p => p.OrganizationId == FacilityData.OrganizationId);

                                if (OrganizationResp != null)
                                {
                                    OrganizationResp.OrganizationName = FacilityData.OrganizationName;
                                    OrganizationResp.Address1 = FacilityData.OrganizationAddress1;
                                    OrganizationResp.Address2 = FacilityData.OrganizationAddress2;
                                    OrganizationResp.Address3 = FacilityData.OrganizationAddress3;
                                    OrganizationResp.City = FacilityData.OrganizationCity;
                                    OrganizationResp.State = FacilityData.OrganizationState;
                                    OrganizationResp.PostalCode = FacilityData.OrganizationPostalCode;
                                    OrganizationResp.CountryCode = FacilityData.OrganizationCountryCode;
                                    OrganizationResp.UserId_Modified = UserId;
                                    OrganizationResp.DateModified = System.DateTime.Now;
                                }
                                db.SaveChanges();
                            }

                            if (FacilityData.PracticeId == 0)
                            {
                                Practice PracticeResp = db.Practices.FirstOrDefault(p => p.PracticeName == FacilityData.PracticeName && p.Address1 == FacilityData.PracticeAddress1 && p.PostalCode == FacilityData.PracticePostalCode);

                                if (PracticeResp != null)
                                {
                                    FacilityData.PracticeId = PracticeResp.PracticeId;
                                }
                            }
                            if (FacilityData.PracticeId == 0)
                            {
                                // Add Practice
                                var Practice = new Practice()
                                {

                                    PracticeId = FacilityData.PracticeId,
                                    OrganizationId = FacilityData.OrganizationId,
                                    PracticeName = FacilityData.PracticeName,
                                    Address1 = FacilityData.PracticeAddress1,
                                    Address2 = FacilityData.PracticeAddress2,
                                    Address3 = FacilityData.PracticeAddress3,
                                    City = FacilityData.PracticeCity,
                                    State = FacilityData.PracticeState,
                                    PostalCode = FacilityData.PracticePostalCode,
                                    CountryCode = FacilityData.PracticeCountryCode,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Practices.Add(Practice);

                                db.SaveChanges();

                                FacilityData.PracticeId = Practice.PracticeId;

                            }
                            else
                            {
                                // Update Practice Info 

                                Practice PracticeResp = db.Practices.FirstOrDefault(p => p.PracticeId == FacilityData.PracticeId);

                                if (PracticeResp != null)
                                {

                                    PracticeResp.OrganizationId = FacilityData.OrganizationId;
                                    PracticeResp.PracticeName = FacilityData.PracticeName;
                                    PracticeResp.Address1 = FacilityData.PracticeAddress1;
                                    PracticeResp.Address2 = FacilityData.PracticeAddress2;
                                    PracticeResp.Address3 = FacilityData.PracticeAddress3;
                                    PracticeResp.City = FacilityData.PracticeCity;
                                    PracticeResp.State = FacilityData.PracticeState;
                                    PracticeResp.PostalCode = FacilityData.PracticePostalCode;
                                    PracticeResp.CountryCode = FacilityData.PracticeCountryCode;
                                    PracticeResp.UserId_Modified = UserId;
                                    PracticeResp.DateModified = System.DateTime.Now;
                                }
                                db.SaveChanges();
                            }

                            if (FacilityData.FacilityId == 0)
                            {
                                Facility FacilityResp = db.Facilities.FirstOrDefault(p => p.FacilityName == FacilityData.FacilityName && p.Address1 == FacilityData.FacilityAddress1 && p.PostalCode == FacilityData.FacilityPostalCode);

                                if (FacilityResp != null)
                                {
                                    FacilityData.FacilityId = FacilityResp.FacilityId;
                                }
                            }
                            if (FacilityData.FacilityId == 0)
                            {
                                // Add Facility
                                var Facility = new Facility()
                                {

                                    PracticeId = FacilityData.PracticeId,
                                    EMRSystemId = FacilityData.EMRSystemId,
                                    FacilityTypeId = FacilityData.FacilityTypeId,
                                    FacilityName = FacilityData.FacilityName,
                                    Address1 = FacilityData.FacilityAddress1,
                                    Address2 = FacilityData.FacilityAddress2,
                                    Address3 = FacilityData.FacilityAddress3,
                                    City = FacilityData.FacilityCity,
                                    State = FacilityData.FacilityState,
                                    PostalCode = FacilityData.FacilityPostalCode,
                                    CountryCode = FacilityData.FacilityCountryCode,
                                    Phone = FacilityData.FacilityPhone,
                                    Fax = FacilityData.FacilityFax,
                                    BillAddress1 = FacilityData.FacilityBillAddress1,
                                    BillAddress2 = FacilityData.FacilityBillAddress2,
                                    BillAddress3 = FacilityData.FacilityBillAddress3,
                                    BillCity = FacilityData.FacilityBillCity,
                                    BillState = FacilityData.FacilityBillState,
                                    BillPostalCode = FacilityData.FacilityBillPostalCode,
                                    BillCountryCode = FacilityData.FacilityBillCountryCode,
                                    BillPhone = FacilityData.FacilityBillPhone,
                                    BillFax = FacilityData.FacilityBillFax,
                                    GeneralMessageAvailable = FacilityData.GeneralMessageAvailable,
                                    GeneralMessageNotify = FacilityData.GeneralMessageNotify,
                                    GeneralMessageEmail = FacilityData.GeneralMessageEmail,
                                    AppointmentMessageAvailable = FacilityData.AppointmentMessageAvailable,
                                    AppointmentMessageNotify = FacilityData.AppointmentMessageNotify,
                                    AppointmentMessageEmail = FacilityData.AppointmentMessageEmail,
                                    MedicationMessageAvailable = FacilityData.MedicationMessageAvailable,
                                    MedicationMessageNotify = FacilityData.MedicationMessageNotify,
                                    MedicationMessageEmail = FacilityData.MedicationMessageEmail,
                                    ReferralMessageAvailable = FacilityData.ReferalMessageAvailable,
                                    ReferralMessageNotify = FacilityData.ReferralMessageNotify,
                                    ReferralMessageEmail = FacilityData.ReferralMessageEmail,
                                    PremiumAvailable = FacilityData.PremiumAvailable,
                                    OnlineBillPayment = FacilityData.OnlineBillPayment,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Facilities.Add(Facility);

                                db.SaveChanges();

                                FacilityData.FacilityId = Facility.FacilityId;

                                // Create UserLogin ForeignKeyConstraint Faciltiy

                                Random randomNumber = new Random();
                                string passclear = string.Empty;
                                for (int i = 0; i < 8; i++)
                                {
                                    passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                }
                                string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");
                                // Add User

                                string Login = FacilityData.FacilityName.Replace(" ", "").Replace(",", "").Replace(".", "").Replace("-", "").Replace("'", "");
                                if (Login.Length > 15)
                                    Login = Login.Substring(0, 15);

                                // Check if login already used
                                User results = db.Users.FirstOrDefault(u => u.UserLogin == Login && u.UserRoleId == 2);
                                int cntr = 1;
                                string Login2 = Login;
                                // This item already exists, increment id
                                while (results != null)
                                {
                                    Login2 = Login + cntr.ToString();
                                    results = db.Users.FirstOrDefault(u => u.UserLogin == Login2 && u.UserRoleId == 2);
                                }

                                var NewUser = new User()
                                {

                                    UserLogin = Login2,
                                    UserEmail = FacilityData.ContactEmail,
                                    Password = passencr,
                                    UserRoleId = 2,
                                    UserRoleLink = FacilityData.FacilityId,
                                    Enabled = true,
                                    Locked = false,
                                    ResetPassword = false,
                                };
                                db.Users.Add(NewUser);

                                db.SaveChanges();

                                Response.FacilityId = FacilityData.FacilityId;
                                Response.UserId = NewUser.UserId;
                                Response.Logon = Login2;
                                Response.Password = passclear;

                            }
                            else
                            {
                                // Update Facility Info 

                                Facility FacilityResp = db.Facilities.FirstOrDefault(p => p.FacilityId == FacilityData.FacilityId);

                                if (FacilityResp != null)
                                {
                                    FacilityResp.PracticeId = FacilityData.PracticeId;
                                    FacilityResp.EMRSystemId = FacilityData.EMRSystemId;
                                    FacilityResp.FacilityTypeId = FacilityData.FacilityTypeId;
                                    FacilityResp.FacilityName = FacilityData.FacilityName;
                                    FacilityResp.Address1 = FacilityData.FacilityAddress1;
                                    FacilityResp.Address2 = FacilityData.FacilityAddress2;
                                    FacilityResp.Address3 = FacilityData.FacilityAddress3;
                                    FacilityResp.City = FacilityData.FacilityCity;
                                    FacilityResp.State = FacilityData.FacilityState;
                                    FacilityResp.PostalCode = FacilityData.FacilityPostalCode;
                                    FacilityResp.CountryCode = FacilityData.FacilityCountryCode;
                                    FacilityResp.Phone = FacilityData.FacilityPhone;
                                    FacilityResp.Fax = FacilityData.FacilityFax;
                                    FacilityResp.BillAddress1 = FacilityData.FacilityBillAddress1;
                                    FacilityResp.BillAddress2 = FacilityData.FacilityBillAddress2;
                                    FacilityResp.BillAddress3 = FacilityData.FacilityBillAddress3;
                                    FacilityResp.BillCity = FacilityData.FacilityBillCity;
                                    FacilityResp.BillState = FacilityData.FacilityBillState;
                                    FacilityResp.BillPostalCode = FacilityData.FacilityBillPostalCode;
                                    FacilityResp.BillCountryCode = FacilityData.FacilityBillCountryCode;
                                    FacilityResp.BillPhone = FacilityData.FacilityBillPhone;
                                    FacilityResp.BillFax = FacilityData.FacilityBillFax;
                                    FacilityResp.GeneralMessageAvailable = FacilityData.GeneralMessageAvailable;
                                    FacilityResp.GeneralMessageNotify = FacilityData.GeneralMessageNotify;
                                    FacilityResp.GeneralMessageEmail = FacilityData.GeneralMessageEmail;
                                    FacilityResp.AppointmentMessageAvailable = FacilityData.AppointmentMessageAvailable;
                                    FacilityResp.AppointmentMessageNotify = FacilityData.AppointmentMessageNotify;
                                    FacilityResp.AppointmentMessageEmail = FacilityData.AppointmentMessageEmail;
                                    FacilityResp.MedicationMessageAvailable = FacilityData.MedicationMessageAvailable;
                                    FacilityResp.MedicationMessageNotify = FacilityData.MedicationMessageNotify;
                                    FacilityResp.MedicationMessageEmail = FacilityData.MedicationMessageEmail;
                                    FacilityResp.ReferralMessageAvailable = FacilityData.ReferalMessageAvailable;
                                    FacilityResp.ReferralMessageNotify = FacilityData.ReferralMessageNotify;
                                    FacilityResp.ReferralMessageEmail = FacilityData.ReferralMessageEmail;
                                    FacilityResp.PremiumAvailable = FacilityData.PremiumAvailable;
                                    FacilityResp.OnlineBillPayment = FacilityData.OnlineBillPayment;
                                    FacilityResp.UserId_Modified = UserId;
                                    FacilityResp.DateModified = System.DateTime.Now;
                                }
                                db.SaveChanges();

                                Response.FacilityId = FacilityData.FacilityId;

                                User UserResp = db.Users.FirstOrDefault(u => u.UserRoleId == 2 && u.UserRoleLink == FacilityData.FacilityId);

                                if (UserResp != null)
                                    Response.UserId = UserResp.UserId;
                                Response.Logon = UserResp.UserLogin;
                                string pass = clsEncryption.Decrypt(UserResp.Password, "AMRP@ss");
                                Response.Password = pass;
                            }
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
