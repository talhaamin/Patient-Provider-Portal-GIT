 // Service Name  : ConfigService
// Date Created  : 11/01/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Web Service For Configuration Tables
// MM/DD/YYYY XXX Description
// 11/19/2013 SJF Added InstructionType Codes & SecurityQuestion Codes
// 12/06/2013 SJF Added Medications & Vaccine
// 12/11/2013 SJF Added CountryCode, State, Frequency, RouteOfAdmin, Social
// 02/06/2014 SJF Added DocUploadType, PolicyType
// 06/18/2014 SJF Added EducationLevel, AlcoholFrequency, ExerciseFrequency & ActivityLevel
// 06/16/2014 SJF Added ThirdParty
// 08/13/2014 SJF Added OrganizationSearch, GetPracticesForOrganization, & GetFacilitiesForPractice
// 12/26/2014 SJF Added Comment to Facility
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


namespace AMR.DataService
{
    /// <summary>
    /// Summary description for ConfigService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/", Name = "ConfigWS")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ConfigService : System.Web.Services.WebService
    {

        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct PracticeData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 PracticeId;
            public Int64 OrganizationId;
            public string PracticeName;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;

        }
        public struct PracticeParms
        {
            public int Option;
            public Int64 PracticeId;
        }
        public struct PracticeTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }

        public struct OrganizationData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 OrganizationId;
            public string OrganizationName;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string PostalCode;
            public string CountryCode;

        }
        public struct OrganizationParms
        {
            public int Option;
            public Int64 OrganizationId;
        }

        public struct OrganizationSearchParms
        {
            public string OrganizationName;
        }
        public struct OrganizationTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }
        public struct FacilityData
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 FacilityId;
            public Int64 PracticeId;
            public int EMRSystemId;
            public int FacilityTypeId;
            public string FacilityName;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string CountryCode;
            public string PostalCode;
            public string Phone;
            public string Fax;
            public string BillAddress1;
            public string BillAddress2;
            public string BillAddress3;
            public string BillCity;
            public string BillState;
            public string BillPostalCode;
            public string BillCountryCode;
            public string BillPhone;
            public string BillFax;
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
            public string Comment;

        }
        public struct FacilityParms
        {
            public int Option;
            public Int64 FacilityId;
        }
        public struct FacilitySearchParms
        {
            public int Option;
            public Int64 FacilityId;
            public string FacilityName;
            public Nullable<System.Int32> PageNumber;
            public Nullable<System.Int32> PageSize;
            public Nullable<System.Int64> TotalRecord;
        }
        public struct FacilityTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
            public Int64 count;
        }
        public struct CodeTableData
        {
            public bool Valid;
            public string ErrorMessage;
            public DataTable dt;
        }
        public struct CodeTableValue
        {
            public bool Valid;
            public string ErrorMessage;
            public int CodeValueId;
            public string Value;
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

        #region Get Practice Data
        //------------------------------------------------------------------------
        // Get Practice Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Practice Data")]
        public PracticeData GetPracticeData(PracticeParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            PracticeData Practice = new PracticeData();

            Practice.Valid = true;
            Practice.ErrorMessage = "";

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
                        Practice results = db.Practices.FirstOrDefault(p => p.PracticeId == Parms.PracticeId);

                        if (results != null)
                        {
                            Practice.Valid = true;
                            Practice.ErrorMessage = "";
                            Practice.PracticeId = results.PracticeId;
                            Practice.OrganizationId = results.OrganizationId;
                            Practice.PracticeName = results.PracticeName;
                            Practice.Address1 = results.Address1;
                            Practice.Address2 = results.Address2;
                            Practice.Address3 = results.Address3;
                            Practice.City = results.City;
                            Practice.State = results.State;
                            Practice.PostalCode = results.PostalCode;
                            Practice.CountryCode = results.CountryCode;

                        }
                        else
                        {
                            Practice.Valid = false;
                            Practice.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Practice.Valid = false;
                    Practice.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Practice.Valid = false;
                Practice.ErrorMessage = "Invalid Token";
            }
            return Practice;
        }
        #endregion

        #region Save Practice Data
        //------------------------------------------------------------------------
        // Save Practice Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Practice Data")]
        public PracticeData SavePracticeData(PracticeData PracticeData, string Token, Int64 UserId, Int64 FacilityId)
        {
            PracticeData.Valid = true;
            PracticeData.ErrorMessage = "";
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
                        if (PracticeData.PracticeId == 0)
                        {
                            // Add Practice
                            var Practice = new Practice()
                            {

                                PracticeId = PracticeData.PracticeId,
                                OrganizationId = PracticeData.OrganizationId,
                                PracticeName = PracticeData.PracticeName,
                                Address1 = PracticeData.Address1,
                                Address2 = PracticeData.Address2,
                                Address3 = PracticeData.Address3,
                                City = PracticeData.City,
                                State = PracticeData.State,
                                PostalCode = PracticeData.PostalCode,
                                CountryCode = PracticeData.CountryCode,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                            };
                            db.Practices.Add(Practice);

                            db.SaveChanges();
                        }
                        else
                        {
                            // Update Practice Info 

                            Practice PracticeResp = db.Practices.FirstOrDefault(p => p.PracticeId == PracticeData.PracticeId);

                            if (PracticeResp != null)
                            {

                                PracticeResp.OrganizationId = PracticeData.OrganizationId;
                                PracticeResp.PracticeName = PracticeData.PracticeName;
                                PracticeResp.Address1 = PracticeData.Address1;
                                PracticeResp.Address2 = PracticeData.Address2;
                                PracticeResp.Address3 = PracticeData.Address3;
                                PracticeResp.City = PracticeData.City;
                                PracticeResp.State = PracticeData.State;
                                PracticeResp.PostalCode = PracticeData.PostalCode;
                                PracticeResp.CountryCode = PracticeData.CountryCode;
                                PracticeResp.UserId_Modified = UserId;
                                PracticeResp.DateModified = System.DateTime.Now;
                            }
                            db.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    PracticeData.Valid = false;
                    PracticeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                PracticeData.Valid = false;
                PracticeData.ErrorMessage = "Invalid Token";
            }
            return PracticeData;
        }
        #endregion

        #region Get Practices For Organization
        //------------------------------------------------------------------------
        // Get Practices For Organization 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Practices For Organization")]
        public PracticeTableData GetPracticesForOrganization(Int64 OrganizationId, string Token, Int64 UserId, Int64 FacilityId)
        {
            PracticeTableData Practice = new PracticeTableData();
            Practice.Valid = true;
            Practice.ErrorMessage = "";

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
                        var results = (from p in db.Practices
                                       join o in db.Organizations on p.OrganizationId equals o.OrganizationId  //add by Talha
                                       where p.OrganizationId == (OrganizationId)
                                       orderby p.PracticeName
                                       select new
                                       {
                                           p.PracticeId,
                                           p.PracticeName,
                                           p.Address1,  //add by Talha
                                           p.Address2,  //add by Talha
                                           p.Address3,  //add by Talha
                                           p.State,    //add by Talha
                                           p.City,  //add by Talha
                                           p.CountryCode,  //add by Talha
                                           p.PostalCode,  //add by Talha
                                           o.OrganizationId, //add by Talha
                                           o.OrganizationName  //add by Talha
                                       });
                        Practice.dt = clsTableConverter.ToDataTable(results);
                        Practice.dt.TableName = "Practice";

                        Practice.Valid = true;
                        Practice.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    Practice.Valid = false;
                    Practice.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Practice.Valid = false;
                Practice.ErrorMessage = "Invalid Token";
            }
            return Practice;
        }
        #endregion

        #region Get Organization Data
        //------------------------------------------------------------------------
        // Get Organization Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Organization Data")]
        public OrganizationData GetOrganizationData(OrganizationParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            OrganizationData Organization = new OrganizationData();
            Organization.Valid = true;
            Organization.ErrorMessage = "";

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
                        Organization results = db.Organizations.FirstOrDefault(p => p.OrganizationId == Parms.OrganizationId);

                        if (results != null)
                        {
                            Organization.Valid = true;
                            Organization.ErrorMessage = "";
                            Organization.OrganizationId = results.OrganizationId;
                            Organization.OrganizationId = results.OrganizationId;
                            Organization.OrganizationName = results.OrganizationName;
                            Organization.Address1 = results.Address1;
                            Organization.Address2 = results.Address2;
                            Organization.Address3 = results.Address3;
                            Organization.City = results.City;
                            Organization.State = results.State;
                            Organization.PostalCode = results.PostalCode;
                            Organization.CountryCode = results.CountryCode;

                        }
                        else
                        {
                            Organization.Valid = false;
                            Organization.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Organization.Valid = false;
                    Organization.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Organization.Valid = false;
                Organization.ErrorMessage = "Invalid Token";
            }
            return Organization;
        }
        #endregion

        #region Save Organization Data
        //------------------------------------------------------------------------
        // Save Organization Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Organization Data")]
        public OrganizationData SaveOrganizationData(OrganizationData OrganizationData, string Token, Int64 UserId, Int64 FacilityId)
        {
            OrganizationData.Valid = true;
            OrganizationData.ErrorMessage = "";
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
                        if (OrganizationData.OrganizationId == 0)
                        {
                            // Add Organization
                            var Organization = new Organization()
                            {

                                OrganizationId = OrganizationData.OrganizationId,
                                OrganizationName = OrganizationData.OrganizationName,
                                Address1 = OrganizationData.Address1,
                                Address2 = OrganizationData.Address2,
                                Address3 = OrganizationData.Address3,
                                City = OrganizationData.City,
                                State = OrganizationData.State,
                                PostalCode = OrganizationData.PostalCode,
                                CountryCode = OrganizationData.CountryCode,
                                UserId_Created = UserId,
                                DateCreated = System.DateTime.Now,
                                UserId_Modified = UserId,
                                DateModified = System.DateTime.Now,
                            };
                            db.Organizations.Add(Organization);

                            db.SaveChanges();
                        }
                        else
                        {
                            // Update Organization Info 

                            Organization OrganizationResp = db.Organizations.FirstOrDefault(p => p.OrganizationId == OrganizationData.OrganizationId);

                            if (OrganizationResp != null)
                            {
                                OrganizationResp.OrganizationName = OrganizationData.OrganizationName;
                                OrganizationResp.Address1 = OrganizationData.Address1;
                                OrganizationResp.Address2 = OrganizationData.Address2;
                                OrganizationResp.Address3 = OrganizationData.Address3;
                                OrganizationResp.City = OrganizationData.City;
                                OrganizationResp.State = OrganizationData.State;
                                OrganizationResp.PostalCode = OrganizationData.PostalCode;
                                OrganizationResp.CountryCode = OrganizationData.CountryCode;
                                OrganizationResp.UserId_Modified = UserId;
                                OrganizationResp.DateModified = System.DateTime.Now;
                            }
                            db.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    OrganizationData.Valid = false;
                    OrganizationData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                OrganizationData.Valid = false;
                OrganizationData.ErrorMessage = "Invalid Token";
            }
            return OrganizationData;
        }
        #endregion

        #region Organization Search
        //------------------------------------------------------------------------
        // Organization Search 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Organization Search")]
        public OrganizationTableData OrganizationSearch(OrganizationSearchParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            OrganizationTableData Organization = new OrganizationTableData();
            Organization.Valid = true;
            Organization.ErrorMessage = "";

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
                        var results = (from o in db.Organizations
                                       where o.OrganizationName.Contains(Parms.OrganizationName)
                                       orderby o.OrganizationName
                                       select new
                                       {
                                           o.OrganizationId,
                                           o.OrganizationName,
                                           o.Address1,  //add by Talha
                                           o.Address2,  //add by Talha
                                           o.Address3,  //add by Talha
                                           o.City,     //add by Talha
                                           o.State,  //add by Talha
                                           o.PostalCode, //add by Talha
                                           o.CountryCode  //add by Talha
                                       }).Take(20);
                        Organization.dt = clsTableConverter.ToDataTable(results);
                        Organization.dt.TableName = "Organizations";

                        Organization.Valid = true;
                        Organization.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    Organization.Valid = false;
                    Organization.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Organization.Valid = false;
                Organization.ErrorMessage = "Invalid Token";
            }
            return Organization;
        }
        #endregion

        #region Get Facility Data
        //------------------------------------------------------------------------
        // Get Facility Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Facility Data")]
        public FacilityData GetFacilityData(FacilityParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilityData Facility = new FacilityData();
            Facility.Valid = true;
            Facility.ErrorMessage = "";

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
                        Facility results = db.Facilities.FirstOrDefault(p => p.FacilityId == Parms.FacilityId);

                        if (results != null)
                        {
                            Facility.Valid = true;
                            Facility.ErrorMessage = "";
                            Facility.FacilityId = results.FacilityId;
                            Facility.PracticeId = Convert.ToInt64(results.PracticeId);
                            Facility.EMRSystemId = Convert.ToInt32(results.EMRSystemId);
                            Facility.FacilityName = results.FacilityName;
                            Facility.Address1 = results.Address1;
                            Facility.Address2 = results.Address2;
                            Facility.Address3 = results.Address3;
                            Facility.City = results.City;
                            Facility.State = results.State;
                            Facility.PostalCode = results.PostalCode;
                            Facility.CountryCode = results.CountryCode;
                            Facility.Phone = results.Phone;
                            Facility.Fax = results.Fax;
                            Facility.BillAddress1 = results.BillAddress1;
                            Facility.BillAddress2 = results.BillAddress2;
                            Facility.BillAddress3 = results.BillAddress3;
                            Facility.BillCity = results.BillCity;
                            Facility.BillState = results.BillState;
                            Facility.BillPostalCode = results.BillPostalCode;
                            Facility.BillCountryCode = results.BillCountryCode;
                            Facility.BillPhone = results.BillPhone;
                            Facility.BillFax = results.BillFax;
                            Facility.GeneralMessageAvailable = Convert.ToBoolean(results.GeneralMessageAvailable);
                            Facility.GeneralMessageNotify = Convert.ToInt32(results.GeneralMessageNotify);
                            Facility.GeneralMessageEmail = results.GeneralMessageEmail;
                            Facility.AppointmentMessageAvailable = Convert.ToBoolean(results.AppointmentMessageAvailable);
                            Facility.AppointmentMessageNotify = Convert.ToInt32(results.AppointmentMessageNotify);
                            Facility.AppointmentMessageEmail = results.AppointmentMessageEmail;
                            Facility.MedicationMessageAvailable = Convert.ToBoolean(results.MedicationMessageAvailable);
                            Facility.MedicationMessageNotify = Convert.ToInt32(results.MedicationMessageNotify);
                            Facility.MedicationMessageEmail = results.MedicationMessageEmail;
                            Facility.ReferalMessageAvailable = Convert.ToBoolean(results.ReferralMessageAvailable);
                            Facility.ReferralMessageNotify = Convert.ToInt32(results.ReferralMessageNotify);
                            Facility.ReferralMessageEmail = results.ReferralMessageEmail;
                            Facility.PremiumAvailable = Convert.ToBoolean(results.PremiumAvailable);
                            Facility.OnlineBillPayment = Convert.ToInt16(results.OnlineBillPayment);
                            Facility.Comment = results.Comment;
                        }
                        else
                        {
                            Facility.Valid = false;
                            Facility.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Facility.Valid = false;
                    Facility.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Facility.Valid = false;
                Facility.ErrorMessage = "Invalid Token";
            }
            return Facility;
        }
        #endregion

        #region Save Facility Data
        //------------------------------------------------------------------------
        // Save Facility Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Save Facility Data")]
        public FacilityData SaveFacilityData(FacilityData FacilityData, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilityData.Valid = true;
            FacilityData.ErrorMessage = "";
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
                        if (FacilityData.FacilityId == 0)
                        {
                            // Add Facility
                            var Facility = new Facility()
                            {

                                PracticeId = FacilityData.PracticeId,
                                EMRSystemId = FacilityData.EMRSystemId,
                                FacilityTypeId = FacilityData.FacilityTypeId,
                                FacilityName = FacilityData.FacilityName,
                                Address1 = FacilityData.Address1,
                                Address2 = FacilityData.Address2,
                                Address3 = FacilityData.Address3,
                                City = FacilityData.City,
                                State = FacilityData.State,
                                PostalCode = FacilityData.PostalCode,
                                CountryCode = FacilityData.CountryCode,
                                Phone = FacilityData.Phone,
                                Fax = FacilityData.Fax,
                                BillAddress1 = FacilityData.BillAddress1,
                                BillAddress2 = FacilityData.BillAddress2,
                                BillAddress3 = FacilityData.BillAddress3,
                                BillCity = FacilityData.BillCity,
                                BillState = FacilityData.BillState,
                                BillPostalCode = FacilityData.BillPostalCode,
                                BillCountryCode = FacilityData.BillCountryCode,
                                BillPhone = FacilityData.BillPhone,
                                BillFax = FacilityData.BillFax,
                                GeneralMessageAvailable = FacilityData.GeneralMessageAvailable,
                                GeneralMessageNotify = FacilityData.GeneralMessageNotify,
                                GeneralMessageEmail = FacilityData.GeneralMessageEmail,
                                AppointmentMessageAvailable = FacilityData.AppointmentMessageAvailable,
                                AppointmentMessageNotify = FacilityData. AppointmentMessageNotify,
                                AppointmentMessageEmail = FacilityData.AppointmentMessageEmail,
                                MedicationMessageAvailable = FacilityData.MedicationMessageAvailable,
                                MedicationMessageNotify = FacilityData. MedicationMessageNotify,
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
                                Comment = FacilityData.Comment,
                            };
                            db.Facilities.Add(Facility);

                            db.SaveChanges();
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
                                FacilityResp.Address1 = FacilityData.Address1;
                                FacilityResp.Address2 = FacilityData.Address2;
                                FacilityResp.Address3 = FacilityData.Address3;
                                FacilityResp.City = FacilityData.City;
                                FacilityResp.State = FacilityData.State;
                                FacilityResp.PostalCode = FacilityData.PostalCode;
                                FacilityResp.CountryCode = FacilityData.CountryCode;
                                FacilityResp.Phone = FacilityData.Phone;
                                FacilityResp.Fax = FacilityData.Fax;
                                FacilityResp.BillAddress1 = FacilityData.BillAddress1;
                                FacilityResp.BillAddress2 = FacilityData.BillAddress2;
                                FacilityResp.BillAddress3 = FacilityData.BillAddress3;
                                FacilityResp.BillCity = FacilityData.BillCity;
                                FacilityResp.BillState = FacilityData.BillState;
                                FacilityResp.BillPostalCode = FacilityData.BillPostalCode;
                                FacilityResp.BillCountryCode = FacilityData.BillCountryCode;
                                FacilityResp.BillPhone = FacilityData.BillPhone;
                                FacilityResp.BillFax = FacilityData.BillFax;
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
                                FacilityResp.Comment = FacilityData.Comment;
                            }
                            db.SaveChanges();
                        }

                    }
                }
                catch (Exception ex)
                {
                    FacilityData.Valid = false;
                    FacilityData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                FacilityData.Valid = false;
                FacilityData.ErrorMessage = "Invalid Token";
            }
            return FacilityData;
        }
        #endregion

        #region Get Facilities For Practice
        //------------------------------------------------------------------------
        // Get Facilities For Practice
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Facilities For Practice")]
        public FacilityTableData GetFacilitiesForPractice(Int64 PracticeId, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilityTableData Facility = new FacilityTableData();
            Facility.Valid = true;
            Facility.ErrorMessage = "";

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
                        var results = (from f in db.Facilities
                                       join p in db.Practices on f.PracticeId equals p.PracticeId  //add by Talha
                                       where f.PracticeId == PracticeId
                                       orderby f.FacilityName
                                       select new
                                       {
                                           f.FacilityId,
                                           f.FacilityName,
                                           f.Address1,  //add by Talha
                                           f.Address2,  //add by Talha
                                           f.Address3,  //add by Talha
                                           f.City,   //add by Talha
                                           f.State,  //add by Talha
                                           f.PostalCode, //add by Talha
                                           f.CountryCode,//add by Talha
                                           p.PracticeId, //add by Talha
                                           p.PracticeName, //add by Talha
                                           f.Comment  //added by Talha
                                       });

                        Facility.dt = clsTableConverter.ToDataTable(results);
                        Facility.dt.TableName = "Facility";

                        Facility.Valid = true;
                        Facility.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    Facility.Valid = false;
                    Facility.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Facility.Valid = false;
                Facility.ErrorMessage = "Invalid Token";
            }
            return Facility;
        }
        #endregion

        #region Facility Search
        //------------------------------------------------------------------------
        // Facility Search
        //------------------------------------------------------------------------

        [WebMethod(Description = "Facility Search")]
        public FacilityTableData FacilitySearch(FacilitySearchParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilityTableData Facility = new FacilityTableData();
            Facility.Valid = true;
            Facility.ErrorMessage = "";

            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    int skip = Int32.Parse((Parms.PageNumber * Parms.PageSize).ToString());
                    int PageSize = Int32.Parse((Parms.PageSize).ToString());

                    using (var db = new AMREntities())
                    {
                        var results = (from f in db.Facilities                                      
                                       where f.FacilityName.StartsWith(Parms.FacilityName)
                                       orderby f.FacilityName
                                       select new
                                       {
                                           f.FacilityId,
                                           f.FacilityName,
                                           f.Address1,  
                                           f.Address2,  
                                           f.Address3,  
                                           f.City,  
                                           f.State, 
                                           f.PostalCode, 
                                           f.CountryCode,
                                       });

                        Facility.count = results.Count();
                        Facility.dt = clsTableConverter.ToDataTable(results.Skip(skip).Take(PageSize));
                        Facility.dt.TableName = "Facility";

                        Facility.Valid = true;
                        Facility.ErrorMessage = "";

                    }
                }
                catch (Exception ex)
                {
                    Facility.Valid = false;
                    Facility.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Facility.Valid = false;
                Facility.ErrorMessage = "Invalid Token";
            }
            return Facility;
        }
        #endregion

        #region Get PreferredLanguage Codes
        //------------------------------------------------------------------------
        // Get PreferredLanguage Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PreferredLanguage Codes")]
        public CodeTableData GetPreferredLanguageCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();
            
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
                        var results = from p in db.C_PreferredLanguage
                                      where p.Inactive == false
                                      select new
                                      {
                                          p.PreferredLanguageId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Gender Codes
        //------------------------------------------------------------------------
        // Get Gender Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Gender Codes")]
        public CodeTableData GetGenderCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Gender
                                      where p.GenderId > 0
                                      select new
                                      {
                                          p.GenderId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Race Codes
        //------------------------------------------------------------------------
        // Get Race Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Race Codes")]
        public CodeTableData GetRaceCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Race
                                      select new
                                      {
                                          p.RaceId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Ethnicity Codes
        //------------------------------------------------------------------------
        // Get Ethnicity Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Ethnicity Codes")]
        public CodeTableData GetEthnicityCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Ethnicity
                                      select new
                                      {
                                          p.EthnicityId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get MaritalStatus Codes
        //------------------------------------------------------------------------
        // Get MaritalStatus Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get MaritalStatus Codes")]
        public CodeTableData GetMaritalStatusCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_MaritalStatus
                                      select new
                                      {
                                          p.MaritalStatusId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get ConditionStatus Codes
        //------------------------------------------------------------------------
        // Get ConditionStatus Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get ConditionStatus Codes")]
        public CodeTableData GetConditionStatusCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_ConditionStatus
                                      select new
                                      {
                                          p.ConditionStatusId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Religion Codes
        //------------------------------------------------------------------------
        // Get Religion Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Religion Codes")]
        public CodeTableData GetReligionCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Religion
                                      select new
                                      {
                                          p.ReligionId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get BloodType Codes
        //------------------------------------------------------------------------
        // Get BloodType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get BloodType Codes")]
        public CodeTableData GetBloodTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_BloodType
                                      select new
                                      {
                                          p.BloodTypeId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get SmokingStatus Codes
        //------------------------------------------------------------------------
        // Get SmokingStatus Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get SmokingStatus Codes")]
        public CodeTableData GetSmokingStatusCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_SmokingStatus
                                      select new
                                      {
                                          p.SmokingStatusId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get FacilityTypes Codes
        //------------------------------------------------------------------------
        // Get FacilityTypes Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get FacilityTypes Codes")]
        public CodeTableData GetFacilityTypesCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_FacilityTypes
                                      select new
                                      {
                                          p.FacilityTypeId,
                                          p.FacilityType,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get UserRole Codes
        //------------------------------------------------------------------------
        // Get UserRole Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get UserRole Codes")]
        public CodeTableData GetUserRoleCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_UserRole
                                      select new
                                      {
                                          p.UserRoleId,
                                          p.RoleName,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get DocumentType Codes
        //------------------------------------------------------------------------
        // Get DocumentType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get DocumentType Codes")]
        public CodeTableData GetDocumentTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_DocumentType
                                      select new
                                      {
                                          p.DocumentTypeId,
                                          p.Description,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get AuditAction Codes
        //------------------------------------------------------------------------
        // Get AuditAction Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get AuditAction Codes")]
        public CodeTableData GetAuditActionCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_AuditAction
                                      select new
                                      {
                                          p.AuditActionId,
                                          p.Description,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get EMRSystem Codes
        //------------------------------------------------------------------------
        // Get EMRSystem Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get EMRSystem Codes")]
        public CodeTableData GetEMRSystemCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_EMRSystem
                                      select new
                                      {
                                          p.EMRSystemId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Relationship Codes
        //------------------------------------------------------------------------
        // Get Relationship Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Relationship Codes")]
        public CodeTableData GetRelationshipCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Relationship
                                      select new
                                      {
                                          p.RelationshipId,
                                          p.Value,
                                          p.SNOMED
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get RelationshipId From SNOMED
        //------------------------------------------------------------------------
        // Get RelationshipId From SNOMED
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get RelationshipId From SNOMED")]
        public CodeTableValue GetRelationshipSNOMED(string Token, Int64 UserId, Int64 FacilityId, string SNOMED)
        {
            // Initialize Dataset To Return
            CodeTableValue CodeData = new CodeTableValue();

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
                        C_Relationship results = db.C_Relationship.FirstOrDefault(r => r.SNOMED == SNOMED);

                        if (results != null)
                        {
                            CodeData.Valid = true;
                            CodeData.ErrorMessage = "";
                            CodeData.CodeValueId = results.RelationshipId;
                            CodeData.Value = results.Value;
                        }
                        else
                        {
                            CodeData.Valid = false;
                            CodeData.ErrorMessage = "Could not read record.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get SecurityQuestion Codes
        //------------------------------------------------------------------------
        // Get SecurityQuestion Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get SecurityQuestion Codes")]
        public CodeTableData GetSecurityQuestionCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_SecurityQuestion
                                      select new
                                      {
                                          p.SecurityQuestionId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get InstructionType Codes
        //------------------------------------------------------------------------
        // Get InstructionType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get InstructionType Codes")]
        public CodeTableData GetInstructionTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_InstructionType
                                      select new
                                      {
                                          p.InstructionTypeId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Problem Codes
        //------------------------------------------------------------------------
        // Get Problem Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Problem Codes")]
        public CodeTableData GetProblemCodes(string Token, Int64 UserId, Int64 FacilityId, string SearchValue)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = (from p in db.C_Problem
                                      where p.Value.StartsWith(SearchValue)
                                      select new
                                      {
                                          p.ProblemId,
                                          p.Value,
                                      }).OrderBy(p=>p.Value).Take(100);

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Vaccine Codes
        //------------------------------------------------------------------------
        // Get Vaccine Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Vaccine Codes")]
        public CodeTableData GetVaccineCodes(string Token, Int64 UserId, Int64 FacilityId, string SearchValue)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = (from p in db.C_Vaccine
                                      where (p.Status == "Active" && p.Description.Contains(SearchValue))
                                      select new
                                      {
                                          p.CVX_Code,
                                          p.Description,
                                      }).OrderBy(p => p.Description).Take(100); 

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Medications
        //------------------------------------------------------------------------
        // Get Medications Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Medications Codes")]
        public CodeTableData GetMedicationsCodes(string Token, Int64 UserId, Int64 FacilityId, string SearchValue)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = (from p in db.C_Medication
                                       where p.Description.Contains(SearchValue)
                                       select new
                                       {
                                           p.RxNormId,
                                           p.Description,
                                       }).OrderBy(p => p.Description).Take(100);

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Frequency Codes
        //------------------------------------------------------------------------
        // Get Frequency Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Frequency Codes")]
        public CodeTableData GetFrequencyCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Frequency
                                      select new
                                      {
                                          p.FrequencyId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Country Codes
        //------------------------------------------------------------------------
        // Get Country Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Country Codes")]
        public CodeTableData GetCountryCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Country
                                      select new
                                      {
                                          p.CountryId,
                                          p.Name,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get State Codes
        //------------------------------------------------------------------------
        // Get State Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get State Codes")]
        public CodeTableData GetStateCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_State
                                      select new
                                      {
                                          p.StateId,
                                          p.Name,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Social Codes
        //------------------------------------------------------------------------
        // Get Social Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Social Codes")]
        public CodeTableData GetSocialCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Social
                                      select new
                                      {
                                          p.SNOMED_Social,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get RouteOfAdministration Codes
        //------------------------------------------------------------------------
        // Get RouteOfAdministration Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get RouteOfAdministration Codes")]
        public CodeTableData GetRouteOfAdministrationCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_RouteOfAdministration
                                      select new
                                      {
                                          p.RouteId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get DocUploadType Codes
        //------------------------------------------------------------------------
        // Get DocUploadType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get DocUploadType Codes")]
        public CodeTableData GetDocUploadTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_DocUploadType
                                      select new
                                      {
                                          p.DocUploadTypeId,
                                          p.Description,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get DoctorType Codes
        //------------------------------------------------------------------------
        // Get DoctorType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get DoctorType Codes")]
        public CodeTableData GetDoctorTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_DoctorType
                                      select new
                                      {
                                          p.DoctorTypeId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get PolicyType Codes
        //------------------------------------------------------------------------
        // Get PolicyType Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get PolicyType Codes")]
        public CodeTableData GetPolicyTypeCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_PolicyType
                                      select new
                                      {
                                          p.PolicyTypeId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Advisor Codes
        //------------------------------------------------------------------------
        // Get Advisor Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Advisor Codes")]
        public CodeTableData GetAdvisorCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_Advisor
                                      select new
                                      {
                                          p.AdvisorId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get ExerciseFrequency Codes
        //------------------------------------------------------------------------
        // Get ExerciseFrequency Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get ExerciseFrequency Codes")]
        public CodeTableData GetExerciseFrequencyCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_ExerciseFrequency
                                      select new
                                      {
                                          p.ExerciseFrequencyId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get ActivityLevel Codes
        //------------------------------------------------------------------------
        // Get ActivityLevel Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get ActivityLevel Codes")]
        public CodeTableData GetActivityLevelCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_ActivityLevel
                                      select new
                                      {
                                          p.ActivityLevelId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get AlcoholFrequency Codes
        //------------------------------------------------------------------------
        // Get AlcoholFrequency Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get AlcoholFrequency Codes")]
        public CodeTableData GetAlcoholFrequencyCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_AlcoholFrequency
                                      select new
                                      {
                                          p.AlcoholFrequencyId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get EducationLevel Codes
        //------------------------------------------------------------------------
        // Get EducationLevel Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get EducationLevel Codes")]
        public CodeTableData GetEducationLevelCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_EducationLevel
                                      select new
                                      {
                                          p.EducationLevelId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get ThirdParty Codes
        //------------------------------------------------------------------------
        // Get ThirdParty Codes
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get ThirdParty Codes")]
        public CodeTableData GetThirdPartyCodes(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from p in db.C_ThirdParty
                                      select new
                                      {
                                          p.ThirdPartyId,
                                          p.Value,
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion

        #region Get Carriers
        //------------------------------------------------------------------------
        // Get Carriers
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Carriers")]
        public CodeTableData GetCarriers(string Token, Int64 UserId, Int64 FacilityId)
        {
            // Initialize Dataset To Return
            CodeTableData CodeData = new CodeTableData();

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
                        var results = from c in db.Carriers
                                      select new
                                      {
                                          c.CarrierId,
                                          c.CarrierURL,
                                          c.CarrierName
                                      };

                        CodeData.dt = clsTableConverter.ToDataTable(results);
                        CodeData.dt.TableName = "Codes";

                        CodeData.Valid = true;
                        CodeData.ErrorMessage = "";
                    }
                }
                catch (Exception ex)
                {
                    CodeData.Valid = false;
                    CodeData.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CodeData.Valid = false;
                CodeData.ErrorMessage = "Invalid Token";
            }
            return CodeData;
        }
        #endregion


        #region Get Facility Setup Data
        //------------------------------------------------------------------------
        // Get Facility Setup Data 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Get Facility Setup Data")]
        public FacilitySetupResp GetFacilitySetupData(FacilityParms Parms, string Token, Int64 UserId, Int64 FacilityId)
        {
            FacilitySetupResp Response = new FacilitySetupResp();
            FacilityData Facility = new FacilityData();
            Facility.Valid = true;
            Facility.ErrorMessage = "";

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
                        Response.FacilityId = Parms.FacilityId;
                        User UserResp = db.Users.FirstOrDefault(u => u.UserRoleId == 2 && u.UserRoleLink == Parms.FacilityId);

                        if (UserResp != null)
                            Response.UserId = UserResp.UserId;
                        Response.Logon = UserResp.UserLogin;
                        string pass = clsEncryption.Decrypt(UserResp.Password, "AMRP@ss");
                        Response.Password = pass;
                    }
                }
                catch (Exception ex)
                {
                    Facility.Valid = false;
                    Facility.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                Facility.Valid = false;
                Facility.ErrorMessage = "Invalid Token";
            }
            return Response;
        }
        #endregion
    }
}
