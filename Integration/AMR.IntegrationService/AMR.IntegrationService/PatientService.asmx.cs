// Service Name  : PatientService
// Date Created  : 10/28/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Integration Web Service For Patient Information
// MM/DD/YYYY XXX Description               
// 11/12/2013 SJF Added DocumentPost
// 11/20/2013 SJF Added PlanOfCare to VisitPost
// 12/17/2013 SJF Added CCDPost
//------------------------------------------------------------------------
//  Set Options & Imports
//------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using AMR.Data;
using System.Xml;
using System.Data.Entity;
using System.Text;

namespace AMR.IntegrationService
{

    [WebService(Namespace = "http://tempuri.org/", Name = "PatientWS",Description="Integration Patient Web Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PatientService : System.Web.Services.WebService
    {
        #region Data Structure
        //------------------------------------------------------------------------
        // Define Data Structure
        //------------------------------------------------------------------------

        public struct PatientData
        {
            public Int64 AMRPatientId;
            public string Salutation;
            public string FirstName;
            public string MiddleName;
            public string LastName;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string Postal;
            public string CountryCode;
            public string HomePhone;
            public string MobilePhone;
            public string WorkPhone;
            public string Fax;
            public string EMail;
            public DateTime DOB;
            public string SSN;
            public int PreferredLanguageId;
            public int GenderId;
            public int RaceId;
            public int EthnicityId;
            public int MaritalStatusId;
            public int SmokingStatusId;
            public string MedicareId;
            public string MedicaidState;
            public string MedicaidId;
            public string AltId1_Type;
            public string AltId1_Key;
            public string AltId2_Type;
            public string AltId2_Key;
            public string AltId3_Type;
            public string AltId3_Key;
            public string AltId4_Type;
            public string AltId4_Key;
            public string AltId5_Type;
            public string AltId5_Key;

        }
        public struct PatientParms
        {
            public int Option;
            public Int64 PatientId;
            public Int64 FacilityId;
        }
        public struct PatientResponse
        {
            public bool Valid;
            public string ErrorMessage;
            public Int64 AMRPatientId;
            public string WelcomeLetter;
            
            //public DataTable PotentialMatch;
        }

        public struct VisitPostData
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public DateTime VisitDate;
            public Int64 ProviderId;
            public string VisitReason;
            public DataTable Insurance;
            public DataTable ProblemList;
            public DataTable Medication;
            public DataTable Allergy;
            public DataTable VitalSign;
            public DataTable FamilyHistory;
            public DataTable SocialHistory;
            public DataTable MedicalHistory;
            public DataTable Procedure;
            public DataTable Immunization;
            public DataTable PlanOfCare;
        }

        public struct VisitPostDataXML
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public DateTime VisitDate;
            public Int64 ProviderId;
            public string VisitReason;
            public string Insurance;
            public string ProblemList;
            public string Medication;
            public string Allergy;
            public string VitalSign;
            public string FamilyHistory;
            public string SocialHistory;
            public string MedicalHistory;
            public string Procedure;
            public string Immunization;
            public string PlanOfCare;
        }
        public struct VisitPostResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct DocumentData
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public string DocumentId;
            public string Description;
            public DateTime DateCreated;
            public string DocumentFormat;
            //public byte[] DocumentImage;
            public string DocumentImage;
            public string Notes;
        }
        public struct DocumentPostResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct LabData
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public Int64 ProviderId;
            public string LabName;
            public DateTime OrderDate;
            public DateTime CollectionDate;
            public string Requisition;
            public string Specimen;
            public string SpecimenSource;
            public DateTime ReportDate;
            public DateTime ReviewDate;
            public string Reviewer;
            public DataTable Test;

        }
        public struct LabDataXML
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public Int64 ProviderId;
            public string LabName;
            public DateTime OrderDate;
            public DateTime CollectionDate;
            public string Requisition;
            public string Specimen;
            public string SpecimenSource;
            public DateTime ReportDate;
            public DateTime ReviewDate;
            public string Reviewer;
            public string Test;

        }
        public struct LabPostResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }

        public struct CCDPostData
        {
            public Int64 VisitId;
            public Int64 AMRPatientId;
            public DateTime VisitDate;
            public Int64 ProviderId;
            public string VisitReason;
            public byte[] Document;
        }
        public struct CCDPostResponse
        {
            public bool Valid;
            public string ErrorMessage;
        }

        #endregion

        #region Create Patient
        //------------------------------------------------------------------------
        // Create Patient 
        //------------------------------------------------------------------------

        [WebMethod(Description = "Create AMR Patient")]
        public PatientResponse CreateAMRPatient(Int64 FacilityId, Int64 UserId, string Token, PatientData PatientData)
        {
            PatientResponse PatResponse = new PatientResponse();

            PatResponse.Valid = true;

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
                            // Check If AMR PatientId Is Passed - If So, just create link to facility
                        if (PatientData.AMRPatientId != 0)
                        {

                            Patient results = db.Patients.FirstOrDefault(p => p.PatientId == PatientData.AMRPatientId);

           
                            //var results2 = db.Patients.Select(p => p.PatientId == PatientData.AMRPatientId);
                            //results2.
                            if (results != null)
                            {
                                // Check If Link Exist
                                var LinkResults = db.PatientFacilityLinks.Find(PatientData.AMRPatientId, FacilityId);
                                //PatientFacilityLink LinkResults = db.PatientFacilityLinks.First(f => f.PatientId == PatientData.AMRPatientId && f.FacilityId == FacilityId);
                                if (LinkResults == null)
                                {
                                    // Create Link 
                                    var NewFacilityLink = new PatientFacilityLink()
                                    {

                                        PatientId = PatientData.AMRPatientId,
                                        FacilityId = FacilityId,
                                        DataAdded = System.DateTime.Now,
                                    };
                                    db.PatientFacilityLinks.Add(NewFacilityLink);

                                    db.SaveChanges();
                                }

                            }
                            else
                            {
                                PatResponse.Valid = false;
                                PatResponse.ErrorMessage = "Invalid AMR Patient Id";
                            }
                        }
                        else
                        {
                            #region // Validate Information Sent From The
                            // If language not set, set to English
                            if (PatientData.PreferredLanguageId == 0)
                                PatientData.PreferredLanguageId = 1;
                            string ErrorMsg = "";

                            if (PatientData.FirstName == null || PatientData.FirstName == "" || PatientData.FirstName.Length > 20)
                                ErrorMsg = ErrorMsg + "First Name is required and must be <= 20 characters   ";

                            if (PatientData.LastName == null || PatientData.LastName == "" || PatientData.LastName.Length > 20)
                                ErrorMsg = ErrorMsg + "Last Name is required and must be <= 20 characters   ";

                            if (PatientData.MiddleName != null && PatientData.MiddleName.Length > 20)
                                ErrorMsg = ErrorMsg + "Middle Name must be <= 20 characters   ";

                            if (PatientData.Salutation != null && PatientData.Salutation.Length > 10)
                                ErrorMsg = ErrorMsg + "Salutation must be <= 10 characters   ";

                            if (PatientData.Address1 == null || PatientData.Address1 == "" || PatientData.Address1.Length > 50)
                                ErrorMsg = ErrorMsg + "Address 1 is required and must be <= 50 characters   ";

                            if (PatientData.Address2 != null && PatientData.Address2.Length > 50)
                                ErrorMsg = ErrorMsg + "Address 2 must be <= 50 characters   ";

                            if (PatientData.Address3 != null && PatientData.Address3.Length > 50)
                                ErrorMsg = ErrorMsg + "Address 3 must be <= 50 characters   ";

                            if (PatientData.City == null || PatientData.City == "" || PatientData.City.Length > 30)
                                ErrorMsg = ErrorMsg + "City is required and must be <= 30 characters   ";

                            if (PatientData.State == null || PatientData.State == "" || PatientData.State.Length > 10)
                                ErrorMsg = ErrorMsg + "State 1 is required and must be <= 10 characters   ";

                            if (PatientData.Postal == null || PatientData.Postal == "" || PatientData.Postal.Length > 10)
                                ErrorMsg = ErrorMsg + "Postal is required and must be <= 10 characters   ";

                            if (PatientData.CountryCode != null && PatientData.CountryCode.Length > 3)
                                ErrorMsg = ErrorMsg + "Country Code must be <= 3 characters   ";

                            if (PatientData.HomePhone != null && PatientData.HomePhone.Length > 16)
                                ErrorMsg = ErrorMsg + "Home Phone must be <= 16 characters   ";

                            if (PatientData.MobilePhone != null && PatientData.MobilePhone.Length > 16)
                                ErrorMsg = ErrorMsg + "Mobile Phone must be <= 16 characters   ";

                            if (PatientData.WorkPhone != null && PatientData.WorkPhone.Length > 16)
                                ErrorMsg = ErrorMsg + "Work Phone must be <= 16 characters   ";

                            if (PatientData.Fax != null && PatientData.Fax.Length > 16)
                                ErrorMsg = ErrorMsg + "Fax must be <= 16 characters   ";

                            if (PatientData.EMail == null || PatientData.EMail == "" || PatientData.EMail.Length > 60)
                                ErrorMsg = ErrorMsg + "EMail is required and must be <= 60 characters   ";

                            //if (PatientData.DOB.Length > 50)
                            //    ErrorMsg = ErrorMsg + "Invalid DOB   ";

                            if (PatientData.PreferredLanguageId < 0 || PatientData.PreferredLanguageId > 3)
                                ErrorMsg = ErrorMsg + "Preferred Language Id must be between 0 and 3   ";

                            if (PatientData.GenderId < 0 || PatientData.GenderId > 2)
                                ErrorMsg = ErrorMsg + "Gender Id must be between 0 and 2   ";

                            if (PatientData.RaceId < 0 || PatientData.RaceId > 5)
                                ErrorMsg = ErrorMsg + "Race Id must be between 0 and 5   ";

                            if (PatientData.EthnicityId < 0 || PatientData.EthnicityId > 2)
                                ErrorMsg = ErrorMsg + "Ethnicity Id must be between 0 and 2   ";

                            if (PatientData.MaritalStatusId < 0 || PatientData.MaritalStatusId > 5)
                                ErrorMsg = ErrorMsg + "MaritalStatus Id must be between 0 and 5   ";

                            if (PatientData.SmokingStatusId < 0 || PatientData.SmokingStatusId > 7)
                                ErrorMsg = ErrorMsg + "SmokingStatus Id must be between 0 and 7   ";
                            #endregion

                            if (ErrorMsg != "")
                            {
                                PatResponse.Valid = false;
                                PatResponse.ErrorMessage = ErrorMsg;
                            }
                            else
                            {
                                // Check to see if the patient already exist in the database

                                #region check field lengths

                                if (PatientData.Address1 == null)
                                    PatientData.Address1 = "";
                                if (PatientData.Address2 == null)
                                    PatientData.Address2 = "";
                                if (PatientData.HomePhone == null)
                                    PatientData.HomePhone = "";
                                if (PatientData.MobilePhone == null)
                                    PatientData.MobilePhone = "";
                                if (PatientData.Postal == null)
                                    PatientData.Postal = "";

                                if (PatientData.MedicareId == null)
                                    PatientData.MedicareId = "";
                                if (PatientData.MedicaidState == null)
                                    PatientData.MedicaidState = "";
                                if (PatientData.MedicaidId == null)
                                    PatientData.MedicaidId = "";
                                if (PatientData.AltId1_Type == null)
                                    PatientData.AltId1_Type = "";
                                if (PatientData.AltId1_Key == null)
                                    PatientData.AltId1_Key = "";
                                if (PatientData.AltId2_Type == null)
                                    PatientData.AltId2_Type = "";
                                if (PatientData.AltId2_Key == null)
                                    PatientData.AltId2_Key = "";
                                if (PatientData.AltId3_Type == null)
                                    PatientData.AltId3_Type = "";
                                if (PatientData.AltId3_Key == null)
                                    PatientData.AltId3_Key = "";
                                if (PatientData.AltId4_Type == null)
                                    PatientData.AltId4_Type = "";
                                if (PatientData.AltId4_Key == null)
                                    PatientData.AltId4_Key = "";
                                if (PatientData.AltId5_Type == null)
                                    PatientData.AltId5_Type = "";
                                if (PatientData.AltId5_Key == null)
                                    PatientData.AltId5_Key = "";

                                if (PatientData.MedicareId.Length > 20)
                                    PatientData.MedicareId = PatientData.MedicareId.Substring(0, 20);
                                if (PatientData.MedicaidState.Length > 2)
                                    PatientData.MedicaidState = PatientData.MedicaidState.Substring(0, 2);
                                if (PatientData.MedicaidId.Length > 20)
                                    PatientData.MedicaidId = PatientData.MedicaidId.Substring(0, 20);
                                if (PatientData.AltId1_Type.Length > 10)
                                    PatientData.AltId1_Type = PatientData.AltId1_Type.Substring(0, 10);
                                if (PatientData.AltId1_Key.Length > 20)
                                    PatientData.AltId1_Key = PatientData.AltId1_Key.Substring(0, 20);
                                if (PatientData.AltId2_Type.Length > 10)
                                    PatientData.AltId2_Type = PatientData.AltId2_Type.Substring(0, 10);
                                if (PatientData.AltId2_Key.Length > 20)
                                    PatientData.AltId2_Key = PatientData.AltId2_Key.Substring(0, 20);
                                if (PatientData.AltId3_Type.Length > 10)
                                    PatientData.AltId3_Type = PatientData.AltId3_Type.Substring(0, 10);
                                if (PatientData.AltId3_Key.Length > 20)
                                    PatientData.AltId3_Key = PatientData.AltId3_Key.Substring(0, 20);
                                if (PatientData.AltId4_Type.Length > 10)
                                    PatientData.AltId4_Type = PatientData.AltId4_Type.Substring(0, 10);
                                if (PatientData.AltId4_Key.Length > 20)
                                    PatientData.AltId4_Key = PatientData.AltId4_Key.Substring(0, 20);
                                if (PatientData.AltId5_Type.Length > 10)
                                    PatientData.AltId5_Type = PatientData.AltId5_Type.Substring(0, 10);
                                if (PatientData.AltId5_Key.Length > 20)
                                    PatientData.AltId5_Key = PatientData.AltId5_Key.Substring(0, 20);
                                #endregion

                                Int64 PatientId = 0;
                                //int cntr = 0;

                                // Call Store Proc To Find Matches

                                string SSN_Encrypted = "";
                                if (PatientData.SSN != "")
                                    SSN_Encrypted = clsEncryption.Encrypt(PatientData.SSN.Replace(" ", "").Replace("-", ""), "AMRP@ss"); 
                                //var results = db.FindPatient(PatientData.FirstName, PatientData.LastName, PatientData.DOB.ToString(), SSN_Encrypted, PatientData.GenderId, PatientData.Address1, PatientData.City, PatientData.State, PatientData.Postal, PatientData.HomePhone).ToList<vwPatientSearch>();

                                System.Data.Objects.ObjectParameter s = new System.Data.Objects.ObjectParameter("PatientId", typeof(Int64));
                                db.FindPatient(s, PatientData.FirstName, PatientData.LastName, PatientData.DOB, SSN_Encrypted, PatientData.GenderId, PatientData.Address1, PatientData.City, PatientData.State, PatientData.Postal, PatientData.HomePhone.Replace(" ", "").Replace("-", "").Replace("(","").Replace(")",""), PatientData.MedicareId, PatientData.MedicaidState, PatientData.MedicaidId, PatientData.AltId1_Type, PatientData.AltId1_Key, PatientData.AltId2_Type, PatientData.AltId2_Key, PatientData.AltId3_Type, PatientData.AltId3_Key, PatientData.AltId4_Type, PatientData.AltId4_Key, PatientData.AltId5_Type, PatientData.AltId5_Key);
                                PatientId = (Int64)s.Value;



                                if (PatientId > 0)
                                {
                                    // 100 Match - Return Patient
                                    PatResponse.AMRPatientId = PatientId;
                                    PatResponse.Valid = true;
                                    PatResponse.ErrorMessage = "";

                                    // Check for Keys to be update
                                    var PatientResult = db.Patients.Find(PatientId);
                                    if (PatientResult != null)
                                    {
                                        if (PatientData.MedicareId != "")
                                            PatientResult.MedicareId = PatientData.MedicareId;
                                        if (PatientData.MedicaidState != "" && PatientData.MedicaidId != "")
                                        {
                                            PatientResult.MedicaidState = PatientData.MedicaidState;
                                            PatientResult.MedicaidId = PatientData.MedicaidId;
                                        }
                                        if (PatientData.AltId1_Type != "" && PatientData.AltId1_Key != "")
                                        {
                                            bool found = false;
                                            if (PatientData.AltId1_Type == PatientResult.AltId1_Type && PatientData.AltId1_Key == PatientResult.AltId1_Key)
                                                found = true;
                                            else if (PatientData.AltId1_Type == PatientResult.AltId2_Type && PatientData.AltId1_Key == PatientResult.AltId2_Key)
                                                found = true;
                                            else if (PatientData.AltId1_Type == PatientResult.AltId3_Type && PatientData.AltId1_Key == PatientResult.AltId3_Key)
                                                found = true;
                                            else if (PatientData.AltId1_Type == PatientResult.AltId4_Type && PatientData.AltId1_Key == PatientResult.AltId4_Key)
                                                found = true;
                                            else if (PatientData.AltId1_Type == PatientResult.AltId5_Type && PatientData.AltId1_Key == PatientResult.AltId5_Key)
                                                found = true;
                                            if (!found)
                                            {
                                                if (PatientResult.AltId1_Type == "")
                                                {
                                                    PatientResult.AltId1_Type = PatientData.AltId1_Type;
                                                    PatientResult.AltId1_Key = PatientData.AltId1_Key;
                                                }
                                                else if (PatientResult.AltId2_Type == "")
                                                {
                                                    PatientResult.AltId2_Type = PatientData.AltId1_Type;
                                                    PatientResult.AltId2_Key = PatientData.AltId1_Key;
                                                }
                                                else if (PatientResult.AltId3_Type == "")
                                                {
                                                    PatientResult.AltId3_Type = PatientData.AltId1_Type;
                                                    PatientResult.AltId3_Key = PatientData.AltId1_Key;
                                                }
                                                else if (PatientResult.AltId4_Type == "")
                                                {
                                                    PatientResult.AltId4_Type = PatientData.AltId1_Type;
                                                    PatientResult.AltId4_Key = PatientData.AltId1_Key;
                                                }
                                                else if (PatientResult.AltId5_Type == "")
                                                {
                                                    PatientResult.AltId5_Type = PatientData.AltId1_Type;
                                                    PatientResult.AltId5_Key = PatientData.AltId1_Key;
                                                }
                                            }
                                        }
                                        if (PatientData.AltId2_Type != "" && PatientData.AltId2_Key != "")
                                        {
                                            bool found = false;
                                            if (PatientData.AltId2_Type == PatientResult.AltId1_Type && PatientData.AltId2_Key == PatientResult.AltId1_Key)
                                                found = true;
                                            else if (PatientData.AltId2_Type == PatientResult.AltId2_Type && PatientData.AltId2_Key == PatientResult.AltId2_Key)
                                                found = true;
                                            else if (PatientData.AltId2_Type == PatientResult.AltId3_Type && PatientData.AltId2_Key == PatientResult.AltId3_Key)
                                                found = true;
                                            else if (PatientData.AltId2_Type == PatientResult.AltId4_Type && PatientData.AltId2_Key == PatientResult.AltId4_Key)
                                                found = true;
                                            else if (PatientData.AltId2_Type == PatientResult.AltId5_Type && PatientData.AltId2_Key == PatientResult.AltId5_Key)
                                                found = true;
                                            if (!found)
                                            {
                                                if (PatientResult.AltId1_Type == "")
                                                {
                                                    PatientResult.AltId1_Type = PatientData.AltId2_Type;
                                                    PatientResult.AltId1_Key = PatientData.AltId2_Key;
                                                }
                                                else if (PatientResult.AltId2_Type == "")
                                                {
                                                    PatientResult.AltId2_Type = PatientData.AltId2_Type;
                                                    PatientResult.AltId2_Key = PatientData.AltId2_Key;
                                                }
                                                else if (PatientResult.AltId3_Type == "")
                                                {
                                                    PatientResult.AltId3_Type = PatientData.AltId2_Type;
                                                    PatientResult.AltId3_Key = PatientData.AltId2_Key;
                                                }
                                                else if (PatientResult.AltId4_Type == "")
                                                {
                                                    PatientResult.AltId4_Type = PatientData.AltId2_Type;
                                                    PatientResult.AltId4_Key = PatientData.AltId2_Key;
                                                }
                                                else if (PatientResult.AltId5_Type == "")
                                                {
                                                    PatientResult.AltId5_Type = PatientData.AltId2_Type;
                                                    PatientResult.AltId5_Key = PatientData.AltId2_Key;
                                                }
                                            }
                                        }
                                        if (PatientData.AltId3_Type != "" && PatientData.AltId3_Key != "")
                                        {
                                            bool found = false;
                                            if (PatientData.AltId3_Type == PatientResult.AltId1_Type && PatientData.AltId3_Key == PatientResult.AltId1_Key)
                                                found = true;
                                            else if (PatientData.AltId3_Type == PatientResult.AltId2_Type && PatientData.AltId3_Key == PatientResult.AltId2_Key)
                                                found = true;
                                            else if (PatientData.AltId3_Type == PatientResult.AltId3_Type && PatientData.AltId3_Key == PatientResult.AltId3_Key)
                                                found = true;
                                            else if (PatientData.AltId3_Type == PatientResult.AltId4_Type && PatientData.AltId3_Key == PatientResult.AltId4_Key)
                                                found = true;
                                            else if (PatientData.AltId3_Type == PatientResult.AltId5_Type && PatientData.AltId3_Key == PatientResult.AltId5_Key)
                                                found = true;
                                            if (!found)
                                            {
                                                if (PatientResult.AltId1_Type == "")
                                                {
                                                    PatientResult.AltId1_Type = PatientData.AltId3_Type;
                                                    PatientResult.AltId1_Key = PatientData.AltId3_Key;
                                                }
                                                else if (PatientResult.AltId2_Type == "")
                                                {
                                                    PatientResult.AltId2_Type = PatientData.AltId3_Type;
                                                    PatientResult.AltId2_Key = PatientData.AltId3_Key;
                                                }
                                                else if (PatientResult.AltId3_Type == "")
                                                {
                                                    PatientResult.AltId3_Type = PatientData.AltId3_Type;
                                                    PatientResult.AltId3_Key = PatientData.AltId3_Key;
                                                }
                                                else if (PatientResult.AltId4_Type == "")
                                                {
                                                    PatientResult.AltId4_Type = PatientData.AltId3_Type;
                                                    PatientResult.AltId4_Key = PatientData.AltId3_Key;
                                                }
                                                else if (PatientResult.AltId5_Type == "")
                                                {
                                                    PatientResult.AltId5_Type = PatientData.AltId3_Type;
                                                    PatientResult.AltId5_Key = PatientData.AltId3_Key;
                                                }
                                            }
                                        }
                                        if (PatientData.AltId4_Type != "" && PatientData.AltId4_Key != "")
                                        {
                                            bool found = false;
                                            if (PatientData.AltId4_Type == PatientResult.AltId1_Type && PatientData.AltId4_Key == PatientResult.AltId1_Key)
                                                found = true;
                                            else if (PatientData.AltId4_Type == PatientResult.AltId2_Type && PatientData.AltId4_Key == PatientResult.AltId2_Key)
                                                found = true;
                                            else if (PatientData.AltId4_Type == PatientResult.AltId3_Type && PatientData.AltId4_Key == PatientResult.AltId3_Key)
                                                found = true;
                                            else if (PatientData.AltId4_Type == PatientResult.AltId4_Type && PatientData.AltId4_Key == PatientResult.AltId4_Key)
                                                found = true;
                                            else if (PatientData.AltId4_Type == PatientResult.AltId5_Type && PatientData.AltId4_Key == PatientResult.AltId5_Key)
                                                found = true;
                                            if (!found)
                                            {
                                                if (PatientResult.AltId1_Type == "")
                                                {
                                                    PatientResult.AltId1_Type = PatientData.AltId4_Type;
                                                    PatientResult.AltId1_Key = PatientData.AltId4_Key;
                                                }
                                                else if (PatientResult.AltId2_Type == "")
                                                {
                                                    PatientResult.AltId2_Type = PatientData.AltId4_Type;
                                                    PatientResult.AltId2_Key = PatientData.AltId4_Key;
                                                }
                                                else if (PatientResult.AltId3_Type == "")
                                                {
                                                    PatientResult.AltId3_Type = PatientData.AltId4_Type;
                                                    PatientResult.AltId3_Key = PatientData.AltId4_Key;
                                                }
                                                else if (PatientResult.AltId4_Type == "")
                                                {
                                                    PatientResult.AltId4_Type = PatientData.AltId4_Type;
                                                    PatientResult.AltId4_Key = PatientData.AltId4_Key;
                                                }
                                                else if (PatientResult.AltId5_Type == "")
                                                {
                                                    PatientResult.AltId5_Type = PatientData.AltId4_Type;
                                                    PatientResult.AltId5_Key = PatientData.AltId4_Key;
                                                }
                                            }
                                        }
                                        if (PatientData.AltId5_Type != "" && PatientData.AltId5_Key != "")
                                        {
                                            bool found = false;
                                            if (PatientData.AltId5_Type == PatientResult.AltId1_Type && PatientData.AltId5_Key == PatientResult.AltId1_Key)
                                                found = true;
                                            else if (PatientData.AltId5_Type == PatientResult.AltId2_Type && PatientData.AltId5_Key == PatientResult.AltId2_Key)
                                                found = true;
                                            else if (PatientData.AltId5_Type == PatientResult.AltId3_Type && PatientData.AltId5_Key == PatientResult.AltId3_Key)
                                                found = true;
                                            else if (PatientData.AltId5_Type == PatientResult.AltId4_Type && PatientData.AltId5_Key == PatientResult.AltId4_Key)
                                                found = true;
                                            else if (PatientData.AltId5_Type == PatientResult.AltId5_Type && PatientData.AltId5_Key == PatientResult.AltId5_Key)
                                                found = true;
                                            if (!found)
                                            {
                                                if (PatientResult.AltId1_Type == "")
                                                {
                                                    PatientResult.AltId1_Type = PatientData.AltId5_Type;
                                                    PatientResult.AltId1_Key = PatientData.AltId5_Key;
                                                }
                                                else if (PatientResult.AltId2_Type == "")
                                                {
                                                    PatientResult.AltId2_Type = PatientData.AltId5_Type;
                                                    PatientResult.AltId2_Key = PatientData.AltId5_Key;
                                                }
                                                else if (PatientResult.AltId3_Type == "")
                                                {
                                                    PatientResult.AltId3_Type = PatientData.AltId5_Type;
                                                    PatientResult.AltId3_Key = PatientData.AltId5_Key;
                                                }
                                                else if (PatientResult.AltId4_Type == "")
                                                {
                                                    PatientResult.AltId4_Type = PatientData.AltId5_Type;
                                                    PatientResult.AltId4_Key = PatientData.AltId5_Key;
                                                }
                                                else if (PatientResult.AltId5_Type == "")
                                                {
                                                    PatientResult.AltId5_Type = PatientData.AltId5_Type;
                                                    PatientResult.AltId5_Key = PatientData.AltId5_Key;
                                                }
                                            }
                                        }
                                        db.SaveChanges();
                                    }
                                    // Check If Link Exist
                                    var LinkResults = db.PatientFacilityLinks.Find(PatientId, FacilityId);
                                    //PatientFacilityLink LinkResults = db.PatientFacilityLinks.First(f => f.PatientId == PatientData.AMRPatientId && f.FacilityId == FacilityId);
                                    if (LinkResults == null)
                                    {
                                        // Create Link 
                                        var NewFacilityLink = new PatientFacilityLink()
                                        {

                                            PatientId = PatientId,
                                            FacilityId = FacilityId,
                                            DataAdded = System.DateTime.Now,
                                        };
                                        db.PatientFacilityLinks.Add(NewFacilityLink);

                                        db.SaveChanges();
                                    }

                                }
                                //if (cntr > 0)
                                //{
                                //    // Potential Matches Found - Return List
                                //    DataTable dt = new DataTable();
                                //    // Create Rows
                                //    dt.Columns.Add("PatientId", typeof(Int64));
                                //    dt.Columns.Add("Rank", typeof(Int16));
                                //    dt.Columns.Add("FirstName", typeof(string));
                                //    dt.Columns.Add("LastName", typeof(string));
                                //    dt.Columns.Add("DOB", typeof(string));
                                //    dt.Columns.Add("Address", typeof(string));
                                //    dt.Columns.Add("State", typeof(string));

                                //    dt.TableName = "MatchList";

                                //    foreach (vwPatientSearch res in results)
                                //    {
                                //        dt.Rows.Add(res.PatientId, res.PRank, res.FirstName, res.LastName, res.DOB.ToString(), res.PAddress, res.State);
                                //    }

                                //    PatResponse.AMRPatientId = 0;
                                //    PatResponse.PotentialMatch = dt;
                                //    PatResponse.Valid = true;
                                //    PatResponse.ErrorMessage = "";

                                //}

                                else
                                {
                                    // Create A New Patient & Return Patient Id
                                    bool RaceId_NotAnswer = false;
                                    bool RaceId_Native = false;
                                    bool RaceId_Asian = false;
                                    bool RaceId_Black = false;
                                    bool RaceId_Hawaiian = false;
                                    bool RaceId_White = false;

                                    if (PatientData.RaceId == 0)
                                        RaceId_NotAnswer = true;
                                    else if (PatientData.RaceId == 1)
                                        RaceId_Native = true;
                                    else if (PatientData.RaceId == 2)
                                        RaceId_Asian = true;
                                    else if (PatientData.RaceId == 3)
                                        RaceId_Black = true;
                                    else if (PatientData.RaceId == 4)
                                        RaceId_Hawaiian = true;
                                    else if (PatientData.RaceId == 5)
                                        RaceId_White = true;
                                    else
                                        RaceId_NotAnswer = true;


                                    if (PatientData.PreferredLanguageId == 1)
                                        PatientData.PreferredLanguageId = 1;
                                    else if (PatientData.PreferredLanguageId == 2)
                                        PatientData.PreferredLanguageId = 427;
                                    else if (PatientData.PreferredLanguageId == 3)
                                        PatientData.PreferredLanguageId = 150;
                                    else if (PatientData.PreferredLanguageId == 0)
                                        PatientData.PreferredLanguageId = 0;

                                    // Get EMR Type from Facility File

                                    int EHRSystem = 0;
                                    Facility FacilityResp = db.Facilities.FirstOrDefault(p => p.FacilityId == FacilityId);
                                    if (FacilityResp != null)
                                    {
                                        EHRSystem = Convert.ToInt32(FacilityResp.EMRSystemId);
                                    }
                                    


                                    // Add Patient
                                    var NewPatient = new Patient()
                                    {
                                        PatientId = PatientData.AMRPatientId,
                                        FirstName = PatientData.FirstName,
                                        MiddleName = PatientData.MiddleName,
                                        LastName = PatientData.LastName,
                                        Title = PatientData.Salutation,
                                        Suffix = "",
                                        Address1 = PatientData.Address1,
                                        Address2 = PatientData.Address2,
                                        Address3 = PatientData.Address3,
                                        City = PatientData.City,
                                        State = PatientData.State,
                                        Zip = PatientData.Postal,
                                        CountryCode = PatientData.CountryCode,
                                        DOB = PatientData.DOB,
                                        SSN = SSN_Encrypted,
                                        HomePhone = PatientData.HomePhone,
                                        MobilePhone = PatientData.MobilePhone,
                                        WorkPhone = PatientData.WorkPhone,
                                        Fax = PatientData.Fax,
                                        EMail = PatientData.EMail,
                                        PreferredLanguageId = PatientData.PreferredLanguageId,
                                        GenderId = PatientData.GenderId,
                                        RaceId_NotAnswer = RaceId_NotAnswer,
                                        RaceId_Native = RaceId_Native,
                                        RaceId_Asian = RaceId_Asian,
                                        RaceId_Black = RaceId_Black,
                                        RaceId_Hawaiian = RaceId_Hawaiian,
                                        RaceId_White = RaceId_White,
                                        EthnicityId = PatientData.EthnicityId,
                                        MaritalStatusId = PatientData.MaritalStatusId,
                                        SmokingStatusId = PatientData.SmokingStatusId,
                                        ReligionId = 0,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        FacilityId_Created = FacilityId,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        SmokingDate = "1/1/1900",
                                        Active = true,
                                        ThirdPartyId = 1,
                                        EMRSystemId = EHRSystem,
                                        MedicareId = PatientData.MedicareId,
                                        MedicaidState = PatientData.MedicaidState,
                                        MedicaidId = PatientData.MedicaidId,
                                        AltId1_Type = PatientData.AltId1_Type,
                                        AltId1_Key = PatientData.AltId1_Key,
                                        AltId2_Type = PatientData.AltId2_Type,
                                        AltId2_Key = PatientData.AltId2_Key,
                                        AltId3_Type = PatientData.AltId3_Type,
                                        AltId3_Key = PatientData.AltId3_Key,
                                        AltId4_Type = PatientData.AltId4_Type,
                                        AltId4_Key = PatientData.AltId4_Key,
                                        AltId5_Type = PatientData.AltId5_Type,
                                        AltId5_Key = PatientData.AltId5_Key,

                                    };
                                    db.Patients.Add(NewPatient);

                                    db.SaveChanges();

                                    PatientData.AMRPatientId = NewPatient.PatientId;

                                    //  Create PatientMatch Record for Matching
                                    //var PatientMatch = new PatientMatch()
                                    //{

                                    //    PatientId = PatientData.AMRPatientId,
                                    //    FirstClean = CleanFirst,
                                    //    LastClean = CleanLast,
                                    //    GenderId = PatientData.GenderId,
                                    //    DOB = PatientData.DOB,
                                    //    AddressClean = CleanAddress,
                                    //    CityClean = CleanCity,
                                    //    StateClean = CleanState,
                                    //    PostalClean = CleanPostal,
                                    //    PhoneClean = CleanPhone,
                                    //};
                                    //db.PatientMatches.Add(PatientMatch);

                                    //db.SaveChanges();

                                    // Generate a random password and Encrypt it
                                    Random randomNumber = new Random();
                                    string passclear = string.Empty;
                                    for (int i = 0; i < 8; i++)
                                    {
                                        passclear += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * randomNumber.NextDouble() + 65))).ToString();

                                    }
                                    string passencr = clsEncryption.Encrypt(passclear, "AMRP@ss");
                                    // Add User
                                    var NewUser = new User()
                                    {

                                        UserLogin = PatientData.AMRPatientId.ToString(),
                                        UserEmail = PatientData.EMail,
                                        Password = passencr,
                                        UserRoleId = 5,
                                        UserRoleLink = PatientData.AMRPatientId,
                                        Enabled = true,
                                        Locked = false,
                                        ResetPassword = true,
                                    };
                                    db.Users.Add(NewUser);

                                    db.SaveChanges();

                                    // Add Visit 0
                                    var NewVisit = new PatientVisit()
                                    {

                                        PatientId = PatientData.AMRPatientId,
                                        FacilityId = 0,
                                        VisitId = 0,
                                        VisitDate = System.DateTime.Now,
                                        ProviderId = 0,
                                        VisitReason = "Patient Entered",
                                        Viewable = false,
                                        DateCreated = System.DateTime.Now,
                                        ClinicalSummary = "Print",
                                    };
                                    db.PatientVisits.Add(NewVisit);

                                    db.SaveChanges();

                                    // Create Link 
                                    var NewFacilityLink = new PatientFacilityLink()
                                    {

                                        PatientId = PatientData.AMRPatientId,
                                        FacilityId = FacilityId,
                                        DataAdded = System.DateTime.Now,
                                    };
                                    db.PatientFacilityLinks.Add(NewFacilityLink);

                                    db.SaveChanges();

                                    PatResponse.AMRPatientId = PatientData.AMRPatientId;
                                    PatResponse.Valid = true;
                                    PatResponse.ErrorMessage = "";

                                    // Create Patient Share Record

                                    var PatientShare = new PatientShare()
                                    {
                                        PatientId = PatientData.AMRPatientId,
                                        Demographics = true,
                                        Allergy = true,
                                        FamilyHistory = true,
                                        LabResults = true,
                                        MedicalHistory = true,
                                        Medication = true,
                                        Problem = true,
                                        Procedure = true,
                                        SocialHistory = true,
                                        SurgicalHistory = true,
                                        VitalSigns = true,
                                        Immunization = true,
                                        Organ = true,
                                        ClinicalDoc = true,
                                        Insurance = true,
                                        EmergencyContact = true,
                                        Appointment = true,
                                        Visit = true,
                                        UploadDocs = true,
                                        PlanOfCare = true,
                                        ClinicalInstructions = true,
                                    };
                                    db.PatientShares.Add(PatientShare);
                                    db.SaveChanges();

                                    // Create an entry in the PatientWebSettings table

                                    // Add Patient Web Settings - First Use
                                    var NewWebSetting = new PatientWebSetting()
                                    {

                                        PatientId = PatientData.AMRPatientId,
                                        VisitWidgetLeft = 2,
                                        VisitWidgetRight = 0,
                                        AllergyWidgetLeft = 0,
                                        AllergyWidgetRight = 3,
                                        AppointmentsWidgetLeft = 7,
                                        AppointmentsWidgetRight = 0,
                                        FamilyWidgetLeft = 4,
                                        FamilyWidgetRight = 0,
                                        ImmunizationWidgetLeft = 0,
                                        ImmunizationWidgetRight = 4,
                                        InsuranceWidgetLeft = 8,
                                        InsuranceWidgetRight = 0,
                                        LabTestWidgetLeft = 0,
                                        LabTestWidgetRight = 5,
                                        MedicationWidgetLeft = 0,
                                        MedicationWidgetRight = 2,
                                        PastMedicalWidgetLeft = 5,
                                        PastMedicalWidgetRight = 0,
                                        ProblemWidgetLeft = 0,
                                        ProblemWidgetRight = 1,
                                        ProcedureWidgetLeft = 6,
                                        ProcedureWidgetRight = 0,
                                        SocialWidgetLeft = 3,
                                        SocialWidgetRight = 0,
                                        VitalSignsWidgetLeft = 0,
                                        VitalSignsWidgetRight = 6,
                                        PremiumWidgetLeft = 0,
                                        PremiumWidgetRight = 0,
                                        StatementWidgetLeft = 0,
                                        StatementWidgetRight = 0,
                                        DocumentWidgetLeft = 9,
                                        DocumentWidgetRight = 0,
                                        PlanOfCareWidgetLeft = 0,
                                        PlanOfCareWidgetRight = 7,
                                        ClinicalInstructionsWidgetLeft = 0,
                                        ClinicalInstructionsWidgetRight = 8,
                                        ProviderWidgetLeft = 1,
                                        ProviderWidgetRight = 0,
                                        EmailNotifyNewMessage = true,
                                        PictureLocation = "",
                                        TextNotifyNewMesssage = false,
                                        CellCarrier = 0

                                    };
                                    db.PatientWebSettings.Add(NewWebSetting);
                                    db.SaveChanges();

                                    try
                                    {

                                        // Send Email to new patient with account login information.

                                        clsEmail objEmail = new clsEmail();

                                        string FacilityName = "";
                                        Facility FacResponse = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityId);

                                        if (FacResponse != null)
                                            FacilityName = FacResponse.FacilityName;

                                        string host = "";
                                        Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                                        if (Email != null)
                                            host = Email.SiteURL;


                                        //string WelcomeMsg = "<table style=\"width: 800px;height: auto;font-family:Arial, Helvetica, sans-serif;font-size:14px;\">" +
                                        //                    "<tr>" +
                                        //                    "    <td width=80px height=80px></td>" +
                                        //                    "    <td width=341px>" +
                                        //                    "        <img src=\"https://www.amrportal.com/LetterImages/AMRLetterLogo.gif\">" +
                                        //                    "    </td>" +
                                        //                    "    <td width=349px></td>" +
                                        //                    "    <td width=22px></td>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=84px></td>" +
                                        //                    "    <td colspan=2>" +
                                        //                    "        Dear<span style=\"font-weight:bold\"> " + PatientData.Salutation + " " + PatientData.FirstName + " " + PatientData.LastName + ",</span> <br /> <br />" +
                                        //                    "        Welcome to your patient portal services provided by <span style=\"font-weight:bold\">" + FacilityName + "</span><br /><br />" +
                                        //                    "        This message is to confirm that your provider has activated your AMR Patient Portal service. With this service, you now have access to the following health record management, features, and more:<br /><br />" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr valign=top;line-height: 24px;>" +
                                        //                    "    <td height=164></td>" +
                                        //                    "    <td line-height: 24px; style=\"line-height: 24px\">" +
                                        //                    "    <ul>" +
                                        //                    "        <li>Ask questions of us through secure messaging</li>" +
                                        //                    "        <li>Receive text and email notifications</li>" +
                                        //                    "        <li>Request prescription refills</li>" +
                                        //                    "        <li>View your Clinical Summary once uploaded</li>" +
                                        //                    "        <li>Manage and update your referral requests</li>" +
                                        //                    "        <li>Update family, social, and past histories</li>" +
                                        //                    "    </ul>" +
                                        //                    "</td>" +
                                        //                    "<td style=\"line-height: 24px\">" +
                                        //                    "    <ul>" +
                                        //                    "        <li>View and manage your allergy information</li>" +
                                        //                    "        <li>Request to schedule an appointment</li>" +
                                        //                    "        <li>View your labwork results and medication lists</li>" +
                                        //                    "        <li>View and keep track of your immunizations</li>" +
                                        //                    "        <li>View Statements and pay invoice</li>" +
                                        //                    "        <li>View your upcoming appointments</li>" +
                                        //                    "    </ul>" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=35></td>" +
                                        //                    "    <td colspan=2 align=\"center\" style=\"font-weight: bold; font-size: 20px; font-style: italic;\">" +
                                        //                    "        Your Quick Instructions & Log In Credentials" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=46></td>" +
                                        //                    "    <td colspan=2 align=\"center\">" +
                                        //                    "        Below, is your AccessID code and your temporary password to login to your portal account.<br />" +
                                        //                    "        You can access the Member Log In section by going to " + host + "." +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=100></td>" +
                                        //                    "    <td>" +
                                        //                    "        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" +
                                        //                    "        Your AccessID Code: <strong><span style=\"font-size: 14px\">" + PatientData.AMRPatientId + "</span></strong><br /><br />" +
                                        //                    "    </td>" +
                                        //                    "    <td>" +
                                        //                    "        <img src=\"https://www.amrportal.com/LetterImages/AMRLetterButtonColor.gif\"/>" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=50></td>" +
                                        //                    "    <td colspan=2 align=\"center\" style=\"font-weight: bold; font-size: 20px; font-style: italic;\">" +
                                        //                    "        Our Recommendations Once You've Logged On" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=220px></td>" +
                                        //                    "    <td colspan=2 align=\"justify\">" +
                                        //                    "        We recommend that you log into your account  and create your personal password today. Once the password has been changed, please verify your personal information, and review the information that has been uploaded by your provider. If any of your information is incorrect please contact your doctor's office for assistance.<br /><br />" +
                                        //                    "        There are advanced and optional sharing controls within your portal that are unique and worth mentioning. <br /><br />" +
                                        //                    "        The<span style=\"font-weight:bold\"> Premium Services Package</span> allows all of your medical providers to upload your health records into your patient portal based on the permissions you set. You, the patient, have the advanced controls on how these records are shared, managed and viewed. Within your portal's Marketplace you have access to: patient education resources, notification tools, prescription discount programs, mobile device access and apps, online blood lab services, medical ID cards and key-tags, and additional Marketplace savings and resources.You can upgrade your AMR Patient Portal Standard with the Premium Services Package at any time." +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "<tr>" +
                                        //                    "    <td height=75></td>" +
                                        //                    "    <td></td>" +
                                        //                    "    <td>" +
                                        //                    "        " + FacilityName + "<br />" +
                                        //                    "        " + FacResponse.Address1 + "<br />" +
                                        //                    "        " + FacResponse.City + ", " + FacResponse.State + "  " + FacResponse.PostalCode + "<br />" +
                                        //                    "        " + FacResponse.Phone + "<br />" +
                                        //                    "    </td>" +
                                        //                    "    <td></td>" +
                                        //                    "</tr>" +
                                        //                    "</table>";

                                        string WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                                "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                                "	<tr style=\"background-color:#00a0e0;\">" +
                                                "       <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                                "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                                "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                                "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                                                "              </td>" +
                                                "              </tr>" +
                                                "          </table>" +
                                                "       </td>" +
                                                "   </tr>" +
                                                "	<tr>" +
                                                "       <td valign=\"top\">" +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "           <tr style=height:1px;><td></td></tr>" +
                                                "			<tr >" +
                                                "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                                "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientData.Salutation + " " + PatientData.FirstName + " " + PatientData.LastName + "</strong>,</h1>  <br />" +
                                                "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                "               Welcome to your patient portal services provided by AMR Patient Portal. <strong>" + FacilityName + "</strong> has activated your AMR Patient Portal account for you to access your health information. Below you will find your AccessID Code to log in to your portal account. <br /><br />" +
                                                "               Your AccessID Code:   <strong>" + PatientData.AMRPatientId + "</strong> <br /><br />" +
                                                "               You will also be receiving an email with your temporary password, which you will need to change once you log in. " +
                                                "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account.<br /><br /> " +
                                                "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                "               Thank you, <br />" +
                                                "               Your Member Services Team<br /><br />" +
                                                "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                "               </span>" + 
                                                "             </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "      </td>" +
                                                "   </tr>" +
                                                "	<tr style=\"background-color:#37b74a;\">" +
                                                "      <td valign=\"top\"> " +
                                                "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                "            <tr style=height:10px;>" +
                                                "            <td></td>" +
                                                "            </tr>" +
                                                "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                "              </td>" +
                                                "            </tr>" +
                                                "          </table>" +
                                                "          <br /><br />" +
                                                "       </td>  " +
                                                "     </tr>" +
                                                "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                "</table>";

                                        objEmail.SendEmailHTML(PatientData.EMail, "AMR Patient Portal - AccessID Code", WelcomeMsg);

                                        WelcomeMsg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                                           "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                                            "	<tr style=\"background-color:#00a0e0;\">" +
                                                            "       <td valign=\"top\"> " +
                                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                                            "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                                            "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                                            "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                                                            "              </td>" +
                                                            "              </tr>" +
                                                            "          </table>" +
                                                            "       </td>" +
                                                            "   </tr>" +
                                                            "	<tr>" +
                                                            "       <td valign=\"top\">" +
                                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                            "           <tr style=height:1px;><td></td></tr>" +
                                                            "			<tr >" +
                                                            "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + PatientData.Salutation + " " + PatientData.FirstName + " " + PatientData.LastName + "</strong>,</h1>  <br />" +
                                                            "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                            "               Welcome again to AMR Patient Portal. Below you will find your temporary password.<br /><br />" +
                                                            "               Your Temporary Password:   <strong>" + passclear + "</strong> <br /><br />" +
                                                            "               Using the AccessID Code provided in the previous email, " +
                                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account and reset your temporary password.<br /><br /> " +
                                                            "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                            "               Thank you, <br />" +
                                                            "               Your Member Services Team<br /><br />" +
                                                            "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                                            "               </span>" + 
                                                            "             </td>" +
                                                            "            </tr>" +
                                                            "          </table>" +
                                                            "      </td>" +
                                                            "   </tr>" +
                                                            "	<tr style=\"background-color:#37b74a;\">" +
                                                            "      <td valign=\"top\"> " +
                                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                                            "            <tr style=height:10px;>" +
                                                            "            <td></td>" +
                                                            "            </tr>" +
                                                            "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                                            "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                                            "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                                            "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                                            "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                                            "              </td>" +
                                                            "            </tr>" +
                                                            "          </table>" +
                                                            "          <br /><br />" +
                                                            "       </td>  " +
                                                            "     </tr>" +
                                                            "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                                            "</table>";

                                        objEmail.SendEmailHTML(PatientData.EMail, "AMR Patient Portal - Temporary Password", WelcomeMsg);

                                        string WelcomeLetter = "<html>" +
                                                                "<div style=\"position: absolute;width: 832px;height: 1000px;margin-left:-416px;left: 50%;z-index: 1;background-image:url(https://www.amrportal.com/LetterImages/AMRLetterTemplate.png);font-family:Arial, Helvetica, sans-serif;font-size:14px;\">" +
                                                                "  <div style=\"position: absolute;width: 697px;height: auto;z-index: 1;left: 74px;top: 79px;padding-top: 0px;\">" +
                                                                "    Dear<span style=\"font-weight:bold\"> " + PatientData.Salutation + " " + PatientData.FirstName + " " + PatientData.LastName + ",</span> <br /> <br />" +
                                                                "    Welcome to your patient portal services provided by <span style=\"font-weight:bold\">" + FacilityName + "</span><br /><br />" +
                                                                "    This message is to confirm that your provider has activated your AMR Patient Portal service. With this service, you now have access to the following health record management, features, and more:<br />" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 341px;height: auto;z-index: 2;left: 73px;top: 180px;line-height: 24px;\">" +
                                                                "    <ul>" +
                                                                "        <li>Ask questions of us through secure messaging</li>" +
                                                                "        <li>Receive text and email notifications</li>" +
                                                                "        <li>Request prescription refills</li>" +
                                                                "        <li>View your Clinical Summary</li>" +
                                                                "        <li>Manage and update your referral requests</li>" +
                                                                "    </ul>" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 341px;height: 162px;z-index: 3;line-height: 24px;left: 430px;top: 180px;\">" +
                                                                "    <ul>" +
                                                                "        <li>Update family, social, and past histories</li>" +
                                                                "        <li>View and manage your allergy information</li>" +
                                                                "        <li>Request to schedule and manage appointments</li>" +
                                                                "        <li>View your labwork results and medication lists</li>" +
                                                                "        <li>View and keep track of your immunizations</li>" +
                                                                "    </ul>" +
                                                                "  </div>" +//
                                                                " <div style=\"position: absolute;width: 701px;height: auto;z-index: 5;left: 71px;top: 388px;text-align: center;font-size: 20px;font-style: italic;font-weight: bold;\">" +
                                                                "    Your Quick Instructions and Login Credentials" +
                                                                "  </div>" +
                                                                //"  <div style=\"position: absolute;width: 159px;height: 29px;z-index: 7;left: 165px;top: 919px;font-weight:bold\">[PracticeLogo]</div>" +
                                                                "  <div style=\"position: absolute;width: 619px;height: 38px;z-index: 8;left: 129px;top: 424px;text-align: center;\">" +
                                                                "    Below, is your AccessID code and your temporary password to log in to your portal account.<br />" +
                                                                "    You can log in by going to " + host + "." +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 136px;height: 19px;z-index: 11;left: 132px;top: 493px;\">" +
                                                                "	Your AccessID Code:" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 142px;height: 19px;z-index: 11;left: 128px;top: 523px;\">" +
                                                                "    Temporary Password:" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 125px;height: 17px;z-index: 12;left: 291px;top: 493px;font-size: 16px;font-weight: bold;\">" +
                                                                "    " + PatientData.AMRPatientId +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 125px;height: 17px;z-index: 12;left: 291px;top: 522px;font-size: 16px;font-weight: bold;\">" +
                                                                "    " + passclear +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 200px;height: 78px;z-index: 9;left: 494px;top: 478px;\">" +
                                                                "    <img src=\"https://www.amrportal.com/LetterImages/AMRLetterButtons.png\"/>" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 701px;height: 21px;z-index: 5;left: 64px;top: 620px;text-align: center;font-size: 20px;font-style: italic;font-weight: bold;\">" +
                                                                "    Our Recommendations Once You've Logged In" +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 699px;height: auto;z-index: 4;left: 65px;top: 667px;text-align: justify;\">" +
                                                                "    We recommend that you log into your account  and create your personal password today. Once the password has been changed, please verify your personal information, and review the information that has been uploaded by your provider. If any of your information is incorrect please contact your doctor's office for assistance.<br /><br />" +
                                                                //"    The<span style=\"font-weight:bold\"> Premium Services Package</span> allows all of your medical providers to upload your health records into your patient portal based on the permissions you set. You, the patient, have the advanced controls on how these records are shared, managed and viewed. Within your portal's Marketplace you have access to: patient education resources, notification tools, prescription discount programs, mobile device access and apps, online blood lab services, medical ID cards and key-tags, and additional Marketplace savings and resources.You can upgrade your AMR Patient Portal Standard with the Premium Services Package at any time." +
                                                                "  </div>" +
                                                                "  <div style=\"position: absolute;width: 315px;height: 76px;z-index: 6;left: 456px;top: 897px;\">" +
                                                                "    " + FacilityName + "<br />" +
                                                                "    " + FacResponse.Address1 + "<br />" +
                                                                "    " + FacResponse.City + ", " + FacResponse.State + "  " + FacResponse.PostalCode + "<br />" +
                                                                "    " + FacResponse.Phone + "<br />" +
                                                                "  </div>" +
                                                                "</div>" +
                                                                "</html>";

                                        PatResponse.WelcomeLetter = WelcomeLetter;
                                    }
                                    catch (Exception ex)
                                    {
                                        PatResponse.Valid = true;
                                        PatResponse.ErrorMessage = "Patient was created, but email could not be sent";
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("See the inner exception for details"))
                        PatResponse.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
                        PatResponse.ErrorMessage = ex.Message;
                    
                    PatResponse.Valid = false;
                }
            }
            else
            {
                // Invalid Token
                PatResponse.Valid = false;
                PatResponse.ErrorMessage = "Invalid Token";
            }
            return PatResponse;
        }
        #endregion

        #region Post Visit Data
        //------------------------------------------------------------------------
        // Post Visit Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Visit Data")]
        public VisitPostResponse VisitPost(Int64 FacilityId, Int64 UserId, string Token, VisitPostData VisitData)
        {
            VisitPostResponse VisitResponse = new VisitPostResponse();
            VisitResponse.Valid = true;
            Int64 VisitId = VisitData.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            int MaxCodeSystemId = 11;
            int MaxConditionStatusId = 4;
            int MaxRelationshipId = 20;
            

            if (objToken.Validate())
            {
                try
                {

                    // Validate the data first
                    string Error = "";

                    #region Insurance

                    if (VisitData.Insurance.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.Insurance.Rows)
                            {
                                if (dr["PlanName"].ToString().Length > 35)
                                    dr["PlanName"] = dr["PlanName"].ToString().Substring(0, 35);
                                if (dr["PlanName"].ToString() == "")
                                    Error = Error + "Insurance - PlanName is required;  ";
                                if (dr["MemberNumber"].ToString().Length > 35)
                                    dr["MemberNumber"] = dr["MemberNumber"].ToString().Substring(0, 35);
                                if (dr["MemberNumber"].ToString() == "")
                                    Error = Error + "Insurance " + dr["PlanName"].ToString() + " MemberNumber is required;  ";
                                if (dr["GroupNumber"].ToString().Length > 35)
                                    dr["GroupNumber"] = dr["GroupNumber"].ToString().Substring(0, 35);
                                if (dr["GroupNumber"].ToString() == "")
                                    Error = Error + "Insurance " + dr["PlanName"].ToString() + " GroupNumber is required;  ";
                                if (dr["Subscriber"].ToString().Length > 35)
                                    dr["Subscriber"] = dr["Subscriber"].ToString().Substring(0, 35);
                                if (dr["Relationship"].ToString().Length > 15)
                                    dr["Relationship"] = dr["Relationship"].ToString().Substring(0, 15);
                                try { DateTime EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]); }
                                catch { Error = Error + "Insurance " + dr["PlanName"].ToString() + " Invalid Date Format;  "; }

                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Insurance " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Problem List

                    if (VisitData.ProblemList.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.ProblemList.Rows)
                            {
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0,20);
                                if (dr["CodeValue"].ToString() == "")
                                    Error = Error + "Problem " + dr["Condition"].ToString() + " CodeValue is required;  ";
                                if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                    Error = Error + "Problem " + dr["Condition"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["Condition"].ToString().Length > 100)
                                    dr["Condition"] = dr["Condition"].ToString().Substring(0,100);
                                if (dr["EffectiveDate"].ToString() == "19000101")
                                    dr["EffectiveDate"] = "";
                                if (!DateStringCheck(dr["EffectiveDate"].ToString()))
                                    Error = Error + "Problem " + dr["Condition"].ToString() + " Invalid date format;  ";
                                if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                    Error = Error + "Problem " + dr["Condition"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);                                                  
                            }
                        }
                        catch (Exception ex)
                        {
                             Error = Error + "Problem List " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Medication

                    if (VisitData.Medication.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.Medication.Rows)
                            {
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                if (Convert.ToInt16(dr["CodeSystemId"]) != 6 && Convert.ToInt16(dr["CodeSystemId"]) != 8)
                                    Error = Error + "Medication " + dr["Description"].ToString() + " CodeSystemId must be 6 or 8;  ";

                                if (dr["MedicationName"].ToString().Length > 50)
                                    dr["MedicationName"] = dr["MedicationName"].ToString().Substring(0, 50);
                                if (dr["MedicationName"].ToString() == "")
                                    Error = Error + "MedicationName is required;  ";
                                if (Convert.ToInt16(dr["Active"]) < 0 || Convert.ToInt16(dr["Active"]) > 1)
                                    Error = Error + "Medication " + dr["MedicationName"].ToString() + " Active must be 0 or 1;  ";
                                try {int Quantity = Convert.ToInt32(dr["Quantity"]);}
                                catch {Error = Error + "Medication " + dr["MedicationName"].ToString() + " Quantity Must Be Numeric;  ";}
                                //if (dr["Days"].ToString().Trim() == "")
                                //    dr["Days"] = 0;
                                //try {int Days = Convert.ToInt32(dr["Days"]);}
                                //catch {Error = Error + "Medication " + dr["MedicationName"].ToString() + " Days Must Be Numeric;  ";}
                                if (dr["Route"].ToString().Length > 20)
                                    dr["Route"] = dr["Route"].ToString().Substring(0, 20);
                                if (dr["Route"].ToString() == "")
                                    Error = Error + "Medication " + dr["MedicationName"].ToString() + " Route is required;  ";
                                try {int Refills = Convert.ToInt32(dr["Refills"]);}
                                catch {Error = Error + "Medication " + dr["MedicationName"].ToString() + " Refills Must Be Numeric;  ";}
                                if (dr["Frequency"].ToString().Length > 10)
                                    dr["Frequency"] = dr["Frequency"].ToString().Substring(0, 10);
                                if (dr["Frequency"].ToString() == "")
                                    Error = Error + "Medication " + dr["MedicationName"].ToString() + " Frequency is required;  ";
                                if (dr["Sig"].ToString().Length > 50)
                                    dr["Sig"] = dr["Sig"].ToString().Substring(0, 50);
                                if (dr["Diagnosis"].ToString().Length > 10)
                                    dr["Diagnosis"] = dr["Diagnosis"].ToString().Substring(0, 10);
                                if (dr["StartDate"].ToString().Trim() == "")
                                    dr["StartDate"] = "1/1/1900";
                                try {DateTime StartDate = Convert.ToDateTime(dr["StartDate"]);}
                                catch {Error = Error + "Medication " + dr["MedicationName"].ToString() + " Invalid Date Format;  ";}
                                if (dr["ExpireDate"].ToString().Trim() == "")
                                    dr["ExpireDate"] = "1/1/1900";
                                try { DateTime ExpireDate = Convert.ToDateTime(dr["ExpireDate"]); }
                                catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Invalid Date Format;  "; }
                                if (dr["Pharmacy"].ToString().Length > 50)
                                    dr["Pharmacy"] = dr["Pharmacy"].ToString().Substring(0, 50);
                                if (dr["Status"].ToString().Length > 10)
                                    dr["Status"] = dr["Status"].ToString().Substring(0, 10);
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                if (dr["DuringVisit"].ToString().Trim() == "")
                                    dr["DuringVisit"] = 0;
                                if (Convert.ToInt16(dr["DuringVisit"]) < 0 || Convert.ToInt16(dr["Active"]) > 1)
                                    Error = Error + "Medication " + dr["MedicationName"].ToString() + " DuringVisit must be 0 or 1;  ";
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Medication " + ex.Message + "; ";
                        }
                    }
                    #endregion

                    #region Allergies

                    if (VisitData.Allergy.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.Allergy.Rows)
                            {
                                if (dr["Allergen"].ToString().Length > 50)
                                    dr["Allergen"] = dr["Allergen"].ToString().Substring(0, 50);
                                if (dr["Allergen"].ToString() == "")
                                    Error = Error + "Allergen is required;  ";
                                if (dr["CodeValue_Allergen"].ToString().Length > 20)
                                    dr["CodeValue_Allergen"] = dr["CodeValue_Allergen"].ToString().Substring(0, 20);
                                if (dr["CodeValue_Allergen"].ToString() == "")
                                    Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeValue_Allergen is required;  ";
                                if (Convert.ToInt16(dr["CodeSystemId_Allergen"]) < 0 || Convert.ToInt16(dr["CodeSystemId_Allergen"]) > MaxCodeSystemId)
                                    Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeSystemId_Allergen must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["AllergenType"].ToString().Length > 20)
                                    dr["AllergenType"] = dr["AllergenType"].ToString().Substring(0, 20);
                                if (dr["AllergenType"].ToString() == "")
                                    Error = Error + "Allergy " + dr["Allergen"].ToString() + " AllergenType is required;  ";
                                if (dr["CodeValue_Reaction"].ToString().Length > 20)
                                    dr["CodeValue_Reaction"] = dr["CodeValue_Reaction"].ToString().Substring(0, 20);
                                if (dr["CodeValue_Reaction"].ToString() == "")
                                    Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeValue_Reaction is required;  ";
                                if (Convert.ToInt16(dr["CodeSystemId_Reaction"]) < 0 || Convert.ToInt16(dr["CodeSystemId_Reaction"]) > MaxCodeSystemId)
                                    Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeSystemId_Reaction must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["Reaction"].ToString().Length > 50)
                                    dr["Reaction"] = dr["Reaction"].ToString().Substring(0, 50);
                                if (dr["Reaction"].ToString() == "")
                                    Error = Error + "Allergy " + dr["Allergen"].ToString() + " Reaction is required;  ";
                                if (dr["EffectiveDate"].ToString() == "19000101")
                                    dr["EffectiveDate"] = "";
                                if (!DateStringCheck(dr["EffectiveDate"].ToString()))
                                    Error = Error + "Allergy " + dr["Allergen"].ToString() + " Invalid date format;  ";
                                if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                    Error = Error + "Allergy " + dr["Allergen"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Allergy " + ex.Message + ";  ";
                        }
                    }
                    #endregion

                    #region VitalSigns

                    if (VisitData.VitalSign.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.VitalSign.Rows)
                            {
                                try { DateTime VitalDate = Convert.ToDateTime(dr["VitalDate"]); }
                                catch { Error = Error + "Vital Invalid Date Format;  "; }
                                if (dr["HeightIn"].ToString() == "") dr["HeightIn"] = 0;
                                try { int HeightIn = Convert.ToInt32(dr["HeightIn"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " HeightIn Must Be Numeric;  "; }
                                if (dr["WeightLb"].ToString() == "") dr["WeightLb"] = 0;
                                try { int WeightLb = Convert.ToInt32(dr["WeightLb"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " WeightLb Must Be Numeric;  "; }
                                if (dr["BloodPressurePosn"].ToString().Length > 15)
                                    dr["BloodPressurePosn"] = dr["BloodPressurePosn"].ToString().Substring(0, 15);
                                if (dr["Systolic"].ToString() == "") dr["Systolic"] = 0;
                                try { int Systolic = Convert.ToInt32(dr["Systolic"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Systolic Must Be Numeric;  "; }
                                if (dr["Diastolic"].ToString() == "") dr["Diastolic"] = 0;
                                try { int Diastolic = Convert.ToInt32(dr["Diastolic"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Diastolic Must Be Numeric;  "; }
                                if (dr["Pulse"].ToString() == "") dr["Pulse"] = 0;
                                try { int Pulse = Convert.ToInt32(dr["Pulse"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Pulse Must Be Numeric;  "; }
                                if (dr["Respiration"].ToString() == "") dr["Respiration"] = 0;
                                try { int Respiration = Convert.ToInt32(dr["Respiration"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Respiration Must Be Numeric;  "; }
                                if (dr["Temperature"].ToString() == "") dr["Temperature"] = 0;
                                try { decimal Temperature = Convert.ToDecimal(dr["Temperature"]); }
                                catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Temperature Must Be Numeric;  "; }
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Vital Sign " + ex.Message + ";  ";
                        }
                    }
                    #endregion

                    #region Family History

                    if (VisitData.FamilyHistory.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.FamilyHistory.Rows)
                            {
                                if (Convert.ToInt16(dr["RelationshipId"]) < 0 || Convert.ToInt16(dr["RelationshipId"]) > MaxRelationshipId)
                                    Error = Error + "Family History - RelationshipId must be between 0 and " + MaxRelationshipId.ToString() + ";  ";
                                if (dr["Description"].ToString().Length > 50)
                                    dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                if (dr["Description"].ToString() == "")
                                    Error = Error + "Family History " + dr["RelationshipId"].ToString() + " Description is required;  ";
                                if (dr["Qualifier"].ToString().Length > 20)
                                    dr["Qualifier"] = dr["Qualifier"].ToString().Substring(0, 20);
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0,20);
                                if (dr["CodeValue"].ToString() == "")
                                    Error = Error + "Family History " + dr["RelationshipId"].ToString() + " CodeValue is required;  ";
                                if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                    Error = Error + "Family History " + dr["RelationshipId"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                    Error = Error + "Family History " + dr["RelationshipId"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                try { int Age = Convert.ToInt32(dr["AgeAtOnset"]); }
                                catch { Error = Error + "Family History " + dr["RelationshipId"].ToString() + " AgeAtOnset Must Be Numeric;  "; }
                                if (Convert.ToInt16(dr["Diseased"]) < 0 || Convert.ToInt16(dr["Diseased"]) > 1)
                                    Error = Error + "Family History " + dr["RelationshipId"].ToString() + " Diseased must be 0 or 1;  ";
                                if (dr["DiseasedAge"] == "")
                                    dr["DiseasedAge"] = 0;
                                try { int DiseasedAge = Convert.ToInt32(dr["DiseasedAge"]); }
                                catch { Error = Error + "Family History " + dr["RelationshipId"].ToString() + " DiseasedAge Must Be Numeric;  "; }
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Family History " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Social History

                    if (VisitData.SocialHistory.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.SocialHistory.Rows)
                            {
                                if (dr["Description"].ToString().Length > 50)
                                    dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                if (dr["Description"].ToString() == "")
                                    Error = Error + "Social History - Description is required;  ";
                                if (dr["Qualifier"].ToString().Length > 20)
                                    dr["Qualifier"] = dr["Qualifier"].ToString().Substring(0, 20);
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                    Error = Error + "Social History " + dr["Description"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["BeginDate"].ToString() == "19000101")
                                    dr["BeginDate"] = "";
                                if (!DateStringCheck(dr["BeginDate"].ToString()))
                                    Error = Error + "Social History " + dr["Description"].ToString() + " Invalid date format;  ";
                                if (dr["BeginDate"].ToString() == "")
                                    Error = Error + "Social History " + dr["Description"].ToString() + " Begin Date is required;  ";
                                if (dr["EndDate"].ToString() == "19000101")
                                    dr["EndDate"] = "";
                                if (!DateStringCheck(dr["EndDate"].ToString()))
                                    Error = Error + "Social History " + dr["Description"].ToString() + " Invalid date format;  ";
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Social History " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Medical History

                    if (VisitData.MedicalHistory.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.MedicalHistory.Rows)
                            {
                                if (dr["Description"].ToString().Length > 50)
                                    dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                if (dr["Description"].ToString() == "")
                                    Error = Error + "Medical History - Description is required;  ";
                                if (dr["DateOfOccurance"].ToString() == "19000101")
                                    dr["DateOfOccurance"] = "";
                                if (!DateStringCheck(dr["DateOfOccurance"].ToString()))
                                    Error = Error + "Medical History " + dr["Description"].ToString() + " Invalid date format;  ";
                                if (dr["DateOfOccurance"].ToString() == "")
                                    Error = Error + "Medical History " + dr["Description"].ToString() + " DateOfOccurance is required;  ";
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Medical History " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Procedure

                    if (VisitData.Procedure.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.Procedure.Rows)
                            {
                                if (dr["Description"].ToString().Length > 50)
                                    dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                if (dr["Description"].ToString() == "")
                                    Error = Error + "Procedure - Description is required;  ";
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                    Error = Error + "Procedure " + dr["Description"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["Diagnosis"].ToString().Length > 100)
                                    dr["Diagnosis"] = dr["Diagnosis"].ToString().Substring(0, 100);
                                if (dr["PerformedBy"].ToString().Length > 50)
                                    dr["PerformedBy"] = dr["PerformedBy"].ToString().Substring(0, 50);
                                if (dr["ServiceLocation"].ToString().Length > 50)
                                    dr["ServiceLocation"] = dr["ServiceLocation"].ToString().Substring(0, 50);
                                if (dr["ServiceDate"].ToString() == "19000101")
                                    dr["ServiceDate"] = "";
                                if (!DateStringCheck(dr["ServiceDate"].ToString()))
                                    Error = Error + "Procedure " + dr["Description"].ToString() + " Invalid date format;  ";
                                if (dr["ServiceDate"].ToString() == "")
                                    Error = Error + "Procedure " + dr["Description"].ToString() + " Service Date is required;  ";
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Procedure " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region Immunization

                    if (VisitData.Immunization.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.Immunization.Rows)
                            {
                                if (dr["Vaccine"].ToString().Length > 100)
                                    dr["Vaccine"] = dr["Vaccine"].ToString().Substring(0, 100);
                                if (dr["Vaccine"].ToString() == "")
                                    Error = Error + "Immunization - Vaccine is required;  ";
                                if (dr["ImmunizationDate"].ToString() == "19000101")
                                    dr["ImmunizationDate"] = "";
                                if (!DateStringCheck(dr["ImmunizationDate"].ToString()))
                                    Error = Error + "Immunization " + dr["Vaccine"].ToString() + " Invalid date format;  ";
                                if (dr["ImmunizationDate"].ToString() == "")
                                    Error = Error + "Immunization " + dr["ImmunizationDate"].ToString() + " Immunization Date is required;  ";
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                if (Convert.ToInt16(dr["CodeSystemId"]) != 4 && Convert.ToInt16(dr["CodeSystemId"]) != 6 && Convert.ToInt16(dr["CodeSystemId"]) != 7 && Convert.ToInt16(dr["CodeSystemId"]) != 11)
                                    Error = Error + "Medication " + dr["Description"].ToString() + " CodeSystemId must be 4, 6, 7 or 11;  ";
                                if (dr["Amount"].ToString().Length > 20)
                                    dr["Amount"] = dr["Amount"].ToString().Substring(0, 20);
                                if (dr["Route"].ToString().Length > 20)
                                    dr["Route"] = dr["Route"].ToString().Substring(0, 20);
                                if (dr["Site"].ToString().Length > 20)
                                    dr["Site"] = dr["Site"].ToString().Substring(0, 20);
                                if (dr["Sequence"].ToString().Length > 20)
                                    dr["Sequence"] = dr["Sequence"].ToString().Substring(0, 20);
                                try { DateTime ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]); }
                                catch { Error = Error + "Immunization " + dr["Vaccine"].ToString() + " Invalid Date Format;  "; }
                                if (dr["LotNumber"].ToString().Length > 20)
                                    dr["LotNumber"] = dr["LotNumber"].ToString().Substring(0, 20);
                                if (dr["Manufacturer"].ToString().Length > 50)
                                    dr["Manufacturer"] = dr["Manufacturer"].ToString().Substring(0, 50);
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Immunization " + ex.Message + "  ";
                        }
                    }
                    #endregion

                    #region PlanOfCare

                    if (VisitData.PlanOfCare.Rows.Count > 0)
                    {
                        try
                        {
                            foreach (DataRow dr in VisitData.PlanOfCare.Rows)
                            {
                                try { DateTime AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]); }
                                catch { Error = Error + "Plan Of Care - Invalid Date Format;  "; }
                                if (Convert.ToInt16(dr["InstructionTypeId"]) < 1 || Convert.ToInt16(dr["InstructionTypeId"]) > 7)
                                    Error = Error + "Plan Of Care - InstructionTypeId must be between 1 and 7;  ";
                                if (dr["Instruction"].ToString().Length > 256)
                                    dr["Instruction"] = dr["Instruction"].ToString().Substring(0, 256);
                                if (dr["CodeValue"].ToString().Length > 20)
                                    dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                    Error = Error + "Plan Of Care " + dr["Instruction"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                if (dr["Goal"].ToString().Length > 256)
                                    dr["Goal"] = dr["Goal"].ToString().Substring(0, 256);
                                if (dr["Note"].ToString().Length > 256)
                                    dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                try { int HeightIn = Convert.ToInt32(dr["ProviderId"]); }
                                catch { Error = Error + "Plan Of Care - ProviderId Must Be Numeric;  "; }

                            }
                        }
                        catch (Exception ex)
                        {
                            Error = Error + "Plan Of Care " + ex.Message + ";  ";
                        }
                    }
                    #endregion

                    if (Error != "")
                    {
                        VisitResponse.Valid = false;
                        VisitResponse.ErrorMessage = Error;
                    }
                    else
                    {
                        // Save data to database

                        using (var db = new AMREntities())
                        {
                            // Create New Visit Record
                            var Visit = new PatientVisit()
                            {
                                PatientId = VisitData.AMRPatientId,
                                FacilityId = FacilityId,
                                VisitId = VisitData.VisitId,
                                VisitDate = VisitData.VisitDate,
                                ProviderId = VisitData.ProviderId,
                                VisitReason = VisitData.VisitReason,
                                Viewable = false,
                                DateCreated = System.DateTime.Now,
                                ClinicalSummary = "Portal",
                            };
                            db.PatientVisits.Add(Visit);
                            db.SaveChanges();

                            #region PatientProviderLink
                            PatientProviderLink LinkResp = db.PatientProviderLinks.FirstOrDefault(p => p.PatientId == VisitData.AMRPatientId && p.ProviderId == VisitData.ProviderId);

                            if (LinkResp == null)
                            {
                                var NewLink = new PatientProviderLink()
                                {
                                    PatientId = VisitData.AMRPatientId,
                                    ProviderId = VisitData.ProviderId,
                                    PCP = false,
                                    DateAdded = System.DateTime.Now,
                                };

                                db.PatientProviderLinks.Add(NewLink);

                                db.SaveChanges();
                            }
                            #endregion

                            #region Insurance

                            if (VisitData.Insurance.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.Insurance.Rows)
                                {
                                    var Insurance = new PatientInsurance()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientInsuranceId = 0,
                                        FacilityId = FacilityId,
                                        PlanName = dr["PlanName"].ToString(),
                                        MemberNumber = dr["MemberNumber"].ToString(),
                                        GroupNumber = dr["GroupNumber"].ToString(),
                                        Subscriber = dr["Subscriber"].ToString(),
                                        Relationship = dr["Relationship"].ToString(),
                                        EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientInsurances.Add(Insurance);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Problem List

                            if (VisitData.ProblemList.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.ProblemList.Rows)
                                {
                                    var Problem = new PatientProblem()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientProblemCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt16(dr["CodeSystemId"]),
                                        Condition = dr["Condition"].ToString(),
                                        EffectiveDate = dr["EffectiveDate"].ToString(),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientProblems.Add(Problem);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Medication

                            if (VisitData.Medication.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.Medication.Rows)
                                {
                                    bool bActive = false;
                                    if (Convert.ToInt16(dr["Active"]) == 1) bActive = true;
                                    bool bDuring = false;
                                    if (Convert.ToInt16(dr["DuringVisit"]) == 1) bDuring = true;

                                    var Medication = new PatientMedication()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientMedicationCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        MedicationName = dr["MedicationName"].ToString(),
                                        Active = bActive,
                                        Quantity = Convert.ToInt32(dr["Quantity"]),
                                        //Days = Convert.ToInt32(dr["Days"]),
                                        RouteId = dr["Route"].ToString(),
                                        Dose = 0,
                                        DoseUnit = "",
                                        Refills = Convert.ToInt32(dr["Refills"]),
                                        Frequency = dr["Frequency"].ToString(),
                                        Sig = dr["Sig"].ToString(),
                                        Diagnosis = dr["Diagnosis"].ToString(),
                                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                                        ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                        Pharmacy = dr["Pharmacy"].ToString(),
                                        Status = dr["Status"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                        DuringVisit = bDuring,
                                    };
                                    db.PatientMedications.Add(Medication);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Allergies

                            if (VisitData.Allergy.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.Allergy.Rows)
                                {
                                    var Allergy = new PatientAllergy()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientAllergyCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue_Allergen = dr["CodeValue_Allergen"].ToString(),
                                        CodeSystemId_Allergen = Convert.ToInt32(dr["CodeSystemId_Allergen"]),
                                        Allergen = dr["Allergen"].ToString(),
                                        AllergenType = dr["AllergenType"].ToString(),
                                        CodeValue_Reaction = dr["CodeValue_Reaction"].ToString(),
                                        CodeSystemId_Reaction = Convert.ToInt32(dr["CodeSystemId_Reaction"]),
                                        Reaction = dr["Reaction"].ToString(),
                                        EffectiveDate = dr["EffectiveDate"].ToString(),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        Note = dr["Note"].ToString(),
                                        OnCard = false,
                                        OnKeys = false,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientAllergies.Add(Allergy);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Vital Signs

                            if (VisitData.VitalSign.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.VitalSign.Rows)
                                {
                                    var Vital = new PatientVitalSign()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientVitalCntr = 0,
                                        FacilityId = FacilityId,
                                        VitalDate = Convert.ToDateTime(dr["VitalDate"]),
                                        HeightIn = Convert.ToInt16(dr["HeightIn"]),
                                        WeightLb = Convert.ToInt16(dr["WeightLb"]),
                                        BloodPressurePosn = dr["BloodPressurePosn"].ToString(),
                                        Systolic = Convert.ToInt16(dr["Systolic"]),
                                        Diastolic = Convert.ToInt16(dr["Diastolic"]),
                                        Pulse = Convert.ToInt16(dr["Pulse"]),
                                        Respiration = Convert.ToInt16(dr["Respiration"]),
                                        Temperature = Convert.ToDecimal(dr["Respiration"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientVitalSigns.Add(Vital);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Family History

                            if (VisitData.FamilyHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.FamilyHistory.Rows)
                                {
                                    var Family = new PatientFamilyHist()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatFamilyHistCntr = 0,
                                        FacilityId = FacilityId,
                                        RelationshipId = Convert.ToInt16(dr["RelationshipId"]),
                                        Description = dr["Description"].ToString(),
                                        Qualifier = dr["Qualifier"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        AgeAtOnset = Convert.ToInt32(dr["AgeAtOnset"]),
                                        Diseased = Convert.ToBoolean(dr["Diseased"]),
                                        DiseasedAge = Convert.ToInt32(dr["DiseasedAge"]),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientFamilyHists.Add(Family);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Social History

                            if (VisitData.SocialHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.SocialHistory.Rows)
                                {
                                    var Social = new PatientSocialHist()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatSocialHistCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        Qualifier = dr["Qualifier"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        BeginDate = dr["BeginDate"].ToString(),
                                        EndDate = dr["EndDate"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientSocialHists.Add(Social);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Medical History

                            if (VisitData.MedicalHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.MedicalHistory.Rows)
                                {
                                    var Medical = new PatientMedicalHist()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatMedicalHistCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        DateOfOccurance = dr["DateOfOccurance"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        OnCard = false,
                                        OnKeys = false,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientMedicalHists.Add(Medical);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Procedure

                            if (VisitData.Procedure.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.Procedure.Rows)
                                {
                                    var Procedure = new PatientProcedure()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatProcedureCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Diagnosis = dr["Diagnosis"].ToString(),
                                        PerformedBy = dr["PerformedBy"].ToString(),
                                        ServiceLocation = dr["ServiceLocation"].ToString(),
                                        ServiceDate = dr["ServiceDate"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientProcedures.Add(Procedure);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Immunization

                            if (VisitData.Immunization.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.Immunization.Rows)
                                {
                                    var Immunization = new PatientImmunization()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientImmunizationCntr = 0,
                                        FacilityId = FacilityId,
                                        ImmunizationDate = dr["ImmunizationDate"].ToString(),
                                        ImmunizationTime = "0000",
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Vaccine = dr["Vaccine"].ToString(),
                                        Amount = dr["Amount"].ToString(),
                                        Route = dr["Route"].ToString(),
                                        Site = dr["Site"].ToString(),
                                        Sequence = dr["Sequence"].ToString(),
                                        ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]),
                                        LotNumber = dr["LotNumber"].ToString(),
                                        Manufacturer = dr["Manufacturer"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientImmunizations.Add(Immunization);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region PlanOfCare

                            if (VisitData.PlanOfCare.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in VisitData.PlanOfCare.Rows)
                                {
                                    var PlanOfCare = new PatientPlanOfCare()
                                    {
                                        PatientId = VisitData.AMRPatientId,
                                        VisitId = VisitId,
                                        PlanCntr = 0,
                                        FacilityId = FacilityId,
                                        InstructionTypeId = Convert.ToInt16(dr["InstructionTypeId"]),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Instruction = dr["Instruction"].ToString(),
                                        Goal = dr["Goal"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]),
                                        ProviderId = Convert.ToInt64(dr["ProviderId"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientPlanOfCares.Add(PlanOfCare);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            SendMessageNotification(VisitData.AMRPatientId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    VisitResponse.Valid = false;
                    VisitResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                VisitResponse.Valid = false;
                VisitResponse.ErrorMessage = "Invalid Token";
            }
            return VisitResponse;
        }
        #endregion

        #region Post Visit Data XML
        //------------------------------------------------------------------------
        // Post Visit Data XML
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Visit Data XML")]
        public VisitPostResponse VisitPostXML(Int64 FacilityId, Int64 UserId, string Token, VisitPostDataXML VisitDataXML)
        {
            VisitPostResponse VisitResponse = new VisitPostResponse();
            VisitResponse.Valid = true;
            Int64 VisitId = VisitDataXML.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            int MaxCodeSystemId = 11;
            int MaxConditionStatusId = 4;
            int MaxRelationshipId = 20;

            string Error = "";


            if (objToken.Validate())
            {
                try
                {
                    // Convert XML To DataTables\

                    DataTable dtInsurance = new DataTable();
                    if (VisitDataXML.Insurance != null && VisitDataXML.Insurance != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.Insurance);
                            DataSet dsInsurance = new DataSet();
                            dsInsurance.ReadXml(sReader);
                            dtInsurance = dsInsurance.Tables["InsuranceData"];
                            if (dtInsurance == null)
                                Error = Error + "Invalid XML format for Insurance ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Insurance ";
                        }
                    }

                    DataTable dtProblemList = new DataTable();
                    if (VisitDataXML.ProblemList != null && VisitDataXML.ProblemList != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.ProblemList);
                            DataSet dsProblemList = new DataSet();
                            dsProblemList.ReadXml(sReader);
                            dtProblemList = dsProblemList.Tables["ProblemData"];
                            if (dtProblemList == null)
                                Error = Error + "Invalid XML format for Problems ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Problems ";
                        }
                    }

                    DataTable dtMedication = new DataTable();
                    if (VisitDataXML.Medication != null && VisitDataXML.Medication != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.Medication);
                            DataSet dsMedication = new DataSet();
                            dsMedication.ReadXml(sReader);
                            dtMedication = dsMedication.Tables["MedicationData"];
                            if (dtMedication == null)
                                Error = Error + "Invalid XML format for Medications ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Medications ";
                        } 
                    }

                    DataTable dtAllergy = new DataTable();
                    if (VisitDataXML.Allergy != null && VisitDataXML.Allergy != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.Allergy);
                            DataSet dsAllergy = new DataSet();
                            dsAllergy.ReadXml(sReader);
                            dtAllergy = dsAllergy.Tables["AllergyData"];
                            if (dtAllergy == null)
                                Error = Error + "Invalid XML format for Allergies ";
                        }
                        catch 
                        {
                            Error = Error + "Invalid XML format for Allergies ";
                        }
                    }

                    DataTable dtVitalSign = new DataTable();
                    if (VisitDataXML.VitalSign != null && VisitDataXML.VitalSign != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.VitalSign);
                            DataSet dsVitalSign = new DataSet();
                            dsVitalSign.ReadXml(sReader);
                            dtVitalSign = dsVitalSign.Tables["VitalSignData"];
                            if (dtVitalSign == null)
                                Error = Error + "Invalid XML format for Vital Signs ";
                        }
                        catch 
                        {
                            Error = Error + "Invalid XML format for Vital Signs ";
                        }
                    }

                    DataTable dtFamilyHistory = new DataTable();
                    if (VisitDataXML.FamilyHistory != null && VisitDataXML.FamilyHistory != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.FamilyHistory);
                            DataSet dsFamilyHistory = new DataSet();
                            dsFamilyHistory.ReadXml(sReader);
                            dtFamilyHistory = dsFamilyHistory.Tables["FamilyHistoryData"];
                            if (dtFamilyHistory == null)
                                Error = Error + "Invalid XML format for Family History ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Family History ";
                        }
                    }

                    DataTable dtSocialHistory = new DataTable();
                    if (VisitDataXML.SocialHistory != null && VisitDataXML.SocialHistory != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.SocialHistory);
                            DataSet dsSocialHistory = new DataSet();
                            dsSocialHistory.ReadXml(sReader);
                            dtSocialHistory = dsSocialHistory.Tables["SocialHistoryData"];
                            if (dtSocialHistory == null)
                                Error = Error + "Invalid XML format for Social History ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Social History ";
                        }
                    }

                    DataTable dtMedicalHistory = new DataTable();
                    if (VisitDataXML.MedicalHistory != null && VisitDataXML.MedicalHistory != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.MedicalHistory);
                            DataSet dsMedicalHistory = new DataSet();
                            dsMedicalHistory.ReadXml(sReader);
                            dtMedicalHistory = dsMedicalHistory.Tables["MedicalHistoryData"];
                            if (dtMedicalHistory == null)
                                Error = Error + "Invalid XML format for Medical History ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Medical History ";
                        }
                    }

                    DataTable dtProcedure = new DataTable();
                    if (VisitDataXML.Procedure != null && VisitDataXML.Procedure != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.Procedure);
                            DataSet dsProcedure = new DataSet();
                            dsProcedure.ReadXml(sReader);
                            dtProcedure = dsProcedure.Tables["ProcedureData"];
                            if (dtProcedure == null)
                                Error = Error + "Invalid XML format for Procedures ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Procedures ";
                        }
                    }

                    DataTable dtImmunization = new DataTable();
                    if (VisitDataXML.Immunization != null && VisitDataXML.Immunization != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.Immunization);
                            DataSet dsImmunization = new DataSet();
                            dsImmunization.ReadXml(sReader);
                            dtImmunization = dsImmunization.Tables["ImmunizationData"];
                            if (dtImmunization == null)
                                Error = Error + "Invalid XML format for Immunizations ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Immunizations ";
                        }
                    }

                    DataTable dtPlanOfCare = new DataTable();
                    if (VisitDataXML.PlanOfCare != null && VisitDataXML.PlanOfCare != "")
                    {
                        try
                        {
                            System.IO.StringReader sReader = new System.IO.StringReader(VisitDataXML.PlanOfCare);
                            DataSet dsPlanOfCare = new DataSet();
                            dsPlanOfCare.ReadXml(sReader);
                            dtPlanOfCare = dsPlanOfCare.Tables["PlanOfCareData"];
                            if (dtPlanOfCare == null)
                                Error = Error + "Invalid XML format for Plan Of Care ";
                        }
                        catch
                        {
                            Error = Error + "Invalid XML format for Plan Of Care ";
                        }
                    }

                    // Validate the data first
                    if (Error == "")
                    {
                        #region Insurance

                        if (dtInsurance.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtInsurance.Rows)
                                {
                                    if (dr["PlanName"].ToString().Length > 35)
                                        dr["PlanName"] = dr["PlanName"].ToString().Substring(0, 35);
                                    if (dr["PlanName"].ToString() == "")
                                        Error = Error + "Insurance - PlanName is required;  ";
                                    if (dr["MemberNumber"].ToString().Length > 35)
                                        dr["MemberNumber"] = dr["MemberNumber"].ToString().Substring(0, 35);
                                    if (dr["MemberNumber"].ToString() == "")
                                        Error = Error + "Insurance " + dr["PlanName"].ToString() + " MemberNumber is required;  ";
                                    if (dr["GroupNumber"].ToString().Length > 35)
                                        dr["GroupNumber"] = dr["GroupNumber"].ToString().Substring(0, 35);
                                    if (dr["GroupNumber"].ToString() == "")
                                        Error = Error + "Insurance " + dr["PlanName"].ToString() + " GroupNumber is required;  ";
                                    if (dr["Subscriber"].ToString().Length > 35)
                                        dr["Subscriber"] = dr["Subscriber"].ToString().Substring(0, 35);
                                    if (dr["Relationship"].ToString().Length > 15)
                                        dr["Relationship"] = dr["Relationship"].ToString().Substring(0, 15);
                                    try { DateTime EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]); }
                                    catch { Error = Error + "Insurance " + dr["PlanName"].ToString() + " Invalid Date Format;  "; }

                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Insurance " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Problem List

                        if (dtProblemList.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtProblemList.Rows)
                                {
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (dr["CodeValue"].ToString() == "")
                                        Error = Error + "Problem " + dr["Condition"].ToString() + " CodeValue is required;  ";
                                    if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                        Error = Error + "Problem " + dr["Condition"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["Condition"].ToString().Length > 100)
                                        dr["Condition"] = dr["Condition"].ToString().Substring(0, 100);
                                    if (dr["EffectiveDate"].ToString() == "19000101")
                                        dr["EffectiveDate"] = "";
                                    if (!DateStringCheck(dr["EffectiveDate"].ToString()))
                                        Error = Error + "Problem " + dr["Condition"].ToString() + " Invalid date format;  ";
                                    if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                        Error = Error + "Problem " + dr["Condition"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Problem List " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Medication

                        if (dtMedication.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtMedication.Rows)
                                {
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (Convert.ToInt16(dr["CodeSystemId"]) != 6 && Convert.ToInt16(dr["CodeSystemId"]) != 8)
                                        Error = Error + "Medication " + dr["Description"].ToString() + " CodeSystemId must be 6 or 8;  ";

                                    if (dr["MedicationName"].ToString().Length > 50)
                                        dr["MedicationName"] = dr["MedicationName"].ToString().Substring(0, 50);
                                    if (dr["MedicationName"].ToString() == "")
                                        Error = Error + "MedicationName is required;  ";
                                    if (Convert.ToInt16(dr["Active"]) < 0 || Convert.ToInt16(dr["Active"]) > 1)
                                        Error = Error + "Medication " + dr["MedicationName"].ToString() + " Active must be 0 or 1;  ";
                                    if (dr["Quantity"].ToString().Trim() == "")
                                        dr["Quantity"] = 0;
                                    try { int Quantity = Convert.ToInt32(dr["Quantity"]); }
                                    catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Quantity Must Be Numeric;  "; }
                                    //if (dr["Days"].ToString().Trim() == "")
                                    //    dr["Days"] = 0;
                                    //try { int Days = Convert.ToInt32(dr["Days"]); }
                                    //catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Days Must Be Numeric;  "; }
                                    if (dr["Route"].ToString().Length > 20)
                                        dr["Route"] = dr["Route"].ToString().Substring(0, 20);
                                    if (dr["Route"].ToString() == "")
                                        Error = Error + "Medication " + dr["MedicationName"].ToString() + " Route is required;  ";
                                    try { int Refills = Convert.ToInt32(dr["Refills"]); }
                                    catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Refills Must Be Numeric;  "; }
                                    if (dr["Frequency"].ToString().Length > 10)
                                        dr["Frequency"] = dr["Frequency"].ToString().Substring(0, 10);
                                    if (dr["Frequency"].ToString() == "")
                                        Error = Error + "Medication " + dr["MedicationName"].ToString() + " Frequency is required;  ";
                                    if (dr["Sig"].ToString().Length > 50)
                                        dr["Sig"] = dr["Sig"].ToString().Substring(0, 50);
                                    if (dr["Diagnosis"].ToString().Length > 10)
                                        dr["Diagnosis"] = dr["Diagnosis"].ToString().Substring(0, 10);
                                    if (dr["StartDate"].ToString().Trim() == "")
                                        dr["StartDate"] = "1/1/1900";
                                    try { DateTime StartDate = Convert.ToDateTime(dr["StartDate"]); }
                                    catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Invalid Date Format;  "; }
                                    if (dr["ExpireDate"].ToString().Trim() == "")
                                        dr["ExpireDate"] = "1/1/1900";
                                    try { DateTime ExpireDate = Convert.ToDateTime(dr["ExpireDate"]); }
                                    catch { Error = Error + "Medication " + dr["MedicationName"].ToString() + " Invalid Date Format;  "; }
                                    if (dr["Pharmacy"].ToString().Length > 50)
                                        dr["Pharmacy"] = dr["Pharmacy"].ToString().Substring(0, 50);
                                    if (dr["Status"].ToString().Length > 10)
                                        dr["Status"] = dr["Status"].ToString().Substring(0, 10);
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                    if (dr["DuringVisit"].ToString().Trim() == "")
                                        dr["DuringVisit"] = 0;
                                    if (Convert.ToInt16(dr["DuringVisit"]) < 0 || Convert.ToInt16(dr["Active"]) > 1)
                                        Error = Error + "Medication " + dr["MedicationName"].ToString() + " DuringVisit must be 0 or 1;  ";
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Medication " + ex.Message + "; ";
                            }
                        }
                        #endregion

                        #region Allergies

                        if (dtAllergy.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtAllergy.Rows)
                                {
                                    if (dr["Allergen"].ToString().Length > 50)
                                        dr["Allergen"] = dr["Allergen"].ToString().Substring(0, 50);
                                    if (dr["Allergen"].ToString() == "")
                                        Error = Error + "Allergen is required;  ";
                                    if (dr["CodeValue_Allergen"].ToString().Length > 20)
                                        dr["CodeValue_Allergen"] = dr["CodeValue_Allergen"].ToString().Substring(0, 20);
                                    if (dr["CodeValue_Allergen"].ToString() == "")
                                        Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeValue_Allergen is required;  ";
                                    if (Convert.ToInt16(dr["CodeSystemId_Allergen"]) < 0 || Convert.ToInt16(dr["CodeSystemId_Allergen"]) > MaxCodeSystemId)
                                        Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeSystemId_Allergen must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["AllergenType"].ToString().Length > 20)
                                        dr["AllergenType"] = dr["AllergenType"].ToString().Substring(0, 20);
                                    if (dr["AllergenType"].ToString() == "")
                                        Error = Error + "Allergy " + dr["Allergen"].ToString() + " AllergenType is required;  ";
                                    if (dr["CodeValue_Reaction"].ToString().Length > 20)
                                        dr["CodeValue_Reaction"] = dr["CodeValue_Reaction"].ToString().Substring(0, 20);
                                    if (dr["CodeValue_Reaction"].ToString() == "")
                                        Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeValue_Reaction is required;  ";
                                    if (Convert.ToInt16(dr["CodeSystemId_Reaction"]) < 0 || Convert.ToInt16(dr["CodeSystemId_Reaction"]) > MaxCodeSystemId)
                                        Error = Error + "Allergy " + dr["AllergenType"].ToString() + " CodeSystemId_Reaction must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["Reaction"].ToString().Length > 50)
                                        dr["Reaction"] = dr["Reaction"].ToString().Substring(0, 50);
                                    if (dr["Reaction"].ToString() == "")
                                        Error = Error + "Allergy " + dr["Allergen"].ToString() + " Reaction is required;  ";
                                    if (dr["EffectiveDate"].ToString() == "19000101")
                                        dr["EffectiveDate"] = "";
                                    if (!DateStringCheck(dr["EffectiveDate"].ToString()))
                                        Error = Error + "Allergy " + dr["Allergen"].ToString() + " Invalid date format;  ";
                                    if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                        Error = Error + "Allergy " + dr["Allergen"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Allergy " + ex.Message + ";  ";
                            }
                        }
                        #endregion

                        #region VitalSigns

                        if (dtVitalSign.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtVitalSign.Rows)
                                {
                                    try { DateTime VitalDate = Convert.ToDateTime(dr["VitalDate"]); }
                                    catch { Error = Error + "Vital Invalid Date Format;  "; }
                                    if (dr["HeightIn"].ToString() == "") dr["HeightIn"] = 0;
                                    try { int HeightIn = Convert.ToInt32(dr["HeightIn"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " HeightIn Must Be Numeric;  "; }
                                    if (dr["WeightLb"].ToString() == "") dr["WeightLb"] = 0;
                                    try { int WeightLb = Convert.ToInt32(dr["WeightLb"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " WeightLb Must Be Numeric;  "; }
                                    if (dr["BloodPressurePosn"].ToString().Length > 15)
                                        dr["BloodPressurePosn"] = dr["BloodPressurePosn"].ToString().Substring(0, 15);
                                    if (dr["Systolic"].ToString() == "") dr["Systolic"] = 0;
                                    try { int Systolic = Convert.ToInt32(dr["Systolic"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Systolic Must Be Numeric;  "; }
                                    if (dr["Diastolic"].ToString() == "") dr["Diastolic"] = 0;
                                    try { int Diastolic = Convert.ToInt32(dr["Diastolic"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Diastolic Must Be Numeric;  "; }
                                    if (dr["Pulse"].ToString() == "") dr["Pulse"] = 0;
                                    try { int Pulse = Convert.ToInt32(dr["Pulse"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Pulse Must Be Numeric;  "; }
                                    if (dr["Respiration"].ToString() == "") dr["Respiration"] = 0;
                                    try { int Respiration = Convert.ToInt32(dr["Respiration"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Respiration Must Be Numeric;  "; }
                                    if (dr["Temperature"].ToString() == "") dr["Temperature"] = 0;
                                    try { decimal Temperature = Convert.ToDecimal(dr["Temperature"]); }
                                    catch { Error = Error + "Vital Sign " + dr["VitalDate"].ToString() + " Temperature Must Be Numeric;  "; }
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Vital Sign " + ex.Message + ";  ";
                            }
                        }
                        #endregion

                        #region Family History

                        if (dtFamilyHistory.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtFamilyHistory.Rows)
                                {
                                    if (Convert.ToInt16(dr["RelationshipId"]) < 0 || Convert.ToInt16(dr["RelationshipId"]) > MaxRelationshipId)
                                        Error = Error + "Family History - RelationshipId must be between 0 and " + MaxRelationshipId.ToString() + ";  ";
                                    if (dr["Description"].ToString().Length > 50)
                                        dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                    if (dr["Description"].ToString() == "")
                                        Error = Error + "Family History " + dr["RelationshipId"].ToString() + " Description is required;  ";
                                    if (dr["Qualifier"].ToString().Length > 20)
                                        dr["Qualifier"] = dr["Qualifier"].ToString().Substring(0, 20);
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (dr["CodeValue"].ToString() == "")
                                        Error = Error + "Family History " + dr["RelationshipId"].ToString() + " CodeValue is required;  ";
                                    if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                        Error = Error + "Family History " + dr["RelationshipId"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (Convert.ToInt16(dr["ConditionStatusId"]) < 0 || Convert.ToInt16(dr["ConditionStatusId"]) > MaxConditionStatusId)
                                        Error = Error + "Family History " + dr["RelationshipId"].ToString() + " ConditionStatusId must be between 0 and " + MaxConditionStatusId.ToString() + ";  ";
                                    try { int Age = Convert.ToInt32(dr["AgeAtOnset"]); }
                                    catch { Error = Error + "Family History " + dr["RelationshipId"].ToString() + " AgeAtOnset Must Be Numeric;  "; }
                                    if (Convert.ToInt16(dr["Diseased"]) < 0 || Convert.ToInt16(dr["Diseased"]) > 1)
                                        Error = Error + "Family History " + dr["RelationshipId"].ToString() + " Diseased must be 0 or 1;  ";
                                    if (dr["DiseasedAge"] == "")
                                        dr["DiseasedAge"] = 0;
                                    try { int DiseasedAge = Convert.ToInt32(dr["DiseasedAge"]); }
                                    catch { Error = Error + "Family History " + dr["RelationshipId"].ToString() + " DiseasedAge Must Be Numeric;  "; }
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Family History " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Social History

                        if (dtSocialHistory.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtSocialHistory.Rows)
                                {
                                    if (dr["Description"].ToString().Length > 50)
                                        dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                    if (dr["Description"].ToString() == "")
                                        Error = Error + "Social History - Description is required;  ";
                                    if (dr["Qualifier"].ToString().Length > 20)
                                        dr["Qualifier"] = dr["Qualifier"].ToString().Substring(0, 20);
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                        Error = Error + "Social History " + dr["Description"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["BeginDate"].ToString() == "19000101")
                                        dr["BeginDate"] = "";
                                    if (!DateStringCheck(dr["BeginDate"].ToString()))
                                        Error = Error + "Social History " + dr["Description"].ToString() + " Invalid date format;  ";
                                    if (dr["BeginDate"].ToString() == "")
                                        Error = Error + "Social History " + dr["Description"].ToString() + " Begin Date is required;  ";
                                    if (dr["EndDate"].ToString() == "19000101")
                                        dr["EndDate"] = "";
                                    if (!DateStringCheck(dr["EndDate"].ToString()))
                                        Error = Error + "Social History " + dr["Description"].ToString() + " Invalid date format;  ";
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Social History " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Medical History

                        if (dtMedicalHistory.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtMedicalHistory.Rows)
                                {
                                    if (dr["Description"].ToString().Length > 50)
                                        dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                    if (dr["Description"].ToString() == "")
                                        Error = Error + "Medical History - Description is required;  ";
                                    if (dr["DateOfOccurance"].ToString() == "19000101")
                                        dr["DateOfOccurance"] = "";
                                    if (!DateStringCheck(dr["DateOfOccurance"].ToString()))
                                        Error = Error + "Medical History " + dr["Description"].ToString() + " Invalid date format;  ";
                                    if (dr["DateOfOccurance"].ToString() == "")
                                        Error = Error + "Medical History " + dr["Description"].ToString() + " DateOfOccurance is required;  ";
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Medical History " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Procedure

                        if (dtProcedure.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtProcedure.Rows)
                                {
                                    if (dr["Description"].ToString().Length > 50)
                                        dr["Description"] = dr["Description"].ToString().Substring(0, 50);
                                    if (dr["Description"].ToString() == "")
                                        Error = Error + "Procedure - Description is required;  ";
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                        Error = Error + "Procedure " + dr["Description"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["Diagnosis"].ToString().Length > 100)
                                        dr["Diagnosis"] = dr["Diagnosis"].ToString().Substring(0, 100);
                                    if (dr["PerformedBy"].ToString().Length > 50)
                                        dr["PerformedBy"] = dr["PerformedBy"].ToString().Substring(0, 50);
                                    if (dr["ServiceLocation"].ToString().Length > 50)
                                        dr["ServiceLocation"] = dr["ServiceLocation"].ToString().Substring(0, 50);
                                    if (dr["ServiceDate"].ToString() == "19000101")
                                        dr["ServiceDate"] = "";
                                    if (!DateStringCheck(dr["ServiceDate"].ToString()))
                                        Error = Error + "Procedure " + dr["Description"].ToString() + " Invalid date format;  ";
                                    if (dr["ServiceDate"].ToString() == "")
                                        Error = Error + "Procedure " + dr["Description"].ToString() + " Service Date is required;  ";
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Procedure " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region Immunization

                        if (dtImmunization.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtImmunization.Rows)
                                {
                                    if (dr["Vaccine"].ToString().Length > 100)
                                        dr["Vaccine"] = dr["Vaccine"].ToString().Substring(0, 100);
                                    if (dr["Vaccine"].ToString() == "")
                                        Error = Error + "Immunization - Vaccine is required;  ";
                                    if (dr["ImmunizationDate"].ToString() == "19000101")
                                        dr["ImmunizationDate"] = "";
                                    if (!DateStringCheck(dr["ImmunizationDate"].ToString()))
                                        Error = Error + "Immunization " + dr["Vaccine"].ToString() + " Invalid date format;  ";
                                    if (dr["ImmunizationDate"].ToString() == "")
                                        Error = Error + "Immunization " + dr["ImmunizationDate"].ToString() + " Immunization Date is required;  ";
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (Convert.ToInt16(dr["CodeSystemId"]) != 4 && Convert.ToInt16(dr["CodeSystemId"]) != 6 && Convert.ToInt16(dr["CodeSystemId"]) != 7 && Convert.ToInt16(dr["CodeSystemId"]) != 11)
                                        Error = Error + "Medication " + dr["Description"].ToString() + " CodeSystemId must be 4, 6, 7 or 11;  ";
                                    if (dr["Amount"].ToString().Length > 20)
                                        dr["Amount"] = dr["Amount"].ToString().Substring(0, 20);
                                    if (dr["Route"].ToString().Length > 20)
                                        dr["Route"] = dr["Route"].ToString().Substring(0, 20);
                                    if (dr["Site"].ToString().Length > 20)
                                        dr["Site"] = dr["Site"].ToString().Substring(0, 20);
                                    if (dr["Sequence"].ToString().Length > 20)
                                        dr["Sequence"] = dr["Sequence"].ToString().Substring(0, 20);
                                    try { DateTime ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]); }
                                    catch { Error = Error + "Immunization " + dr["Vaccine"].ToString() + " Invalid Date Format;  "; }
                                    if (dr["LotNumber"].ToString().Length > 20)
                                        dr["LotNumber"] = dr["LotNumber"].ToString().Substring(0, 20);
                                    if (dr["Manufacturer"].ToString().Length > 50)
                                        dr["Manufacturer"] = dr["Manufacturer"].ToString().Substring(0, 50);
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Immunization " + ex.Message + "  ";
                            }
                        }
                        #endregion

                        #region PlanOfCare

                        if (dtPlanOfCare.Rows.Count > 0)
                        {
                            try
                            {
                                foreach (DataRow dr in dtPlanOfCare.Rows)
                                {
                                    try { DateTime AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]); }
                                    catch { Error = Error + "Plan Of Care - Invalid Date Format;  "; }
                                    if (Convert.ToInt16(dr["InstructionTypeId"]) < 1 || Convert.ToInt16(dr["InstructionTypeId"]) > 7)
                                        Error = Error + "Plan Of Care - InstructionTypeId must be between 1 and 7;  ";
                                    if (dr["Instruction"].ToString().Length > 256)
                                        dr["Instruction"] = dr["Instruction"].ToString().Substring(0, 256);
                                    if (dr["CodeValue"].ToString().Length > 20)
                                        dr["CodeValue"] = dr["CodeValue"].ToString().Substring(0, 20);
                                    if (Convert.ToInt16(dr["CodeSystemId"]) < 0 || Convert.ToInt16(dr["CodeSystemId"]) > MaxCodeSystemId)
                                        Error = Error + "Plan Of Care " + dr["Instruction"].ToString() + " CodeSystemId must be between 0 and " + MaxCodeSystemId.ToString() + ";  ";
                                    if (dr["Goal"].ToString().Length > 256)
                                        dr["Goal"] = dr["Goal"].ToString().Substring(0, 256);
                                    if (dr["Note"].ToString().Length > 256)
                                        dr["Note"] = dr["Note"].ToString().Substring(0, 256);
                                    try { int HeightIn = Convert.ToInt32(dr["ProviderId"]); }
                                    catch { Error = Error + "Plan Of Care - ProviderId Must Be Numeric;  "; }

                                }
                            }
                            catch (Exception ex)
                            {
                                Error = Error + "Plan Of Care " + ex.Message + ";  ";
                            }
                        }
                        #endregion
                    }

                    if (Error != "")
                    {
                        VisitResponse.Valid = false;
                        VisitResponse.ErrorMessage = Error;
                    }
                    else
                    {
                        // Save data to database

                        using (var db = new AMREntities())
                        {
                            // Create New Visit Record
                            var Visit = new PatientVisit()
                            {
                                PatientId = VisitDataXML.AMRPatientId,
                                FacilityId = FacilityId,
                                VisitId = VisitDataXML.VisitId,
                                VisitDate = VisitDataXML.VisitDate,
                                ProviderId = VisitDataXML.ProviderId,
                                VisitReason = VisitDataXML.VisitReason,
                                Viewable = false,
                                DateCreated = System.DateTime.Now,
                                ClinicalSummary = "Portal",
                            };
                            db.PatientVisits.Add(Visit);
                            db.SaveChanges();

                            #region PatientProviderLink
                            PatientProviderLink LinkResp = db.PatientProviderLinks.FirstOrDefault(p => p.PatientId == VisitDataXML.AMRPatientId && p.ProviderId == VisitDataXML.ProviderId);

                            if (LinkResp == null)
                            {
                                var NewLink = new PatientProviderLink()
                                {
                                    PatientId = VisitDataXML.AMRPatientId,
                                    ProviderId = VisitDataXML.ProviderId,
                                    PCP = false,
                                    DateAdded = System.DateTime.Now,
                                };

                                db.PatientProviderLinks.Add(NewLink);

                                db.SaveChanges();
                            }
                            #endregion

                            #region Insurance

                            if (dtInsurance.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtInsurance.Rows)
                                {
                                    var Insurance = new PatientInsurance()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientInsuranceId = 0,
                                        FacilityId = FacilityId,
                                        PlanName = dr["PlanName"].ToString(),
                                        MemberNumber = dr["MemberNumber"].ToString(),
                                        GroupNumber = dr["GroupNumber"].ToString(),
                                        Subscriber = dr["Subscriber"].ToString(),
                                        Relationship = dr["Relationship"].ToString(),
                                        EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientInsurances.Add(Insurance);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Problem List

                            if (dtProblemList.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtProblemList.Rows)
                                {
                                    var Problem = new PatientProblem()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientProblemCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt16(dr["CodeSystemId"]),
                                        Condition = dr["Condition"].ToString(),
                                        EffectiveDate = dr["EffectiveDate"].ToString(),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientProblems.Add(Problem);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Medication

                            if (dtMedication.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtMedication.Rows)
                                {
                                    bool bActive = false;
                                    if (Convert.ToInt16(dr["Active"]) == 1) bActive = true;
                                    bool bDuring = false;
                                    if (Convert.ToInt16(dr["DuringVisit"]) == 1) bDuring = true;

                                    var Medication = new PatientMedication()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientMedicationCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        MedicationName = dr["MedicationName"].ToString(),
                                        Active = bActive,
                                        Quantity = Convert.ToInt32(dr["Quantity"]),
                                        //Days = Convert.ToInt32(dr["Days"]),
                                        RouteId = dr["Route"].ToString(),
                                        Dose = 0,
                                        DoseUnit = "",
                                        Refills = Convert.ToInt32(dr["Refills"]),
                                        Frequency = dr["Frequency"].ToString(),
                                        Sig = dr["Sig"].ToString(),
                                        Diagnosis = dr["Diagnosis"].ToString(),
                                        StartDate = Convert.ToDateTime(dr["StartDate"]),
                                        ExpireDate = Convert.ToDateTime(dr["ExpireDate"]),
                                        Pharmacy = dr["Pharmacy"].ToString(),
                                        Status = dr["Status"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                        DuringVisit = bDuring,
                                    };
                                    db.PatientMedications.Add(Medication);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Allergies

                            if (dtAllergy.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtAllergy.Rows)
                                {
                                    var Allergy = new PatientAllergy()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientAllergyCntr = 0,
                                        FacilityId = FacilityId,
                                        CodeValue_Allergen = dr["CodeValue_Allergen"].ToString(),
                                        CodeSystemId_Allergen = Convert.ToInt32(dr["CodeSystemId_Allergen"]),
                                        Allergen = dr["Allergen"].ToString(),
                                        AllergenType = dr["AllergenType"].ToString(),
                                        CodeValue_Reaction = dr["CodeValue_Reaction"].ToString(),
                                        CodeSystemId_Reaction = Convert.ToInt32(dr["CodeSystemId_Reaction"]),
                                        Reaction = dr["Reaction"].ToString(),
                                        EffectiveDate = dr["EffectiveDate"].ToString(),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        Note = dr["Note"].ToString(),
                                        OnCard = false,
                                        OnKeys = false,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientAllergies.Add(Allergy);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Vital Signs

                            if (dtVitalSign.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtVitalSign.Rows)
                                {
                                    var Vital = new PatientVitalSign()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientVitalCntr = 0,
                                        FacilityId = FacilityId,
                                        VitalDate = Convert.ToDateTime(dr["VitalDate"]),
                                        HeightIn = Convert.ToInt16(dr["HeightIn"]),
                                        WeightLb = Convert.ToInt16(dr["WeightLb"]),
                                        BloodPressurePosn = dr["BloodPressurePosn"].ToString(),
                                        Systolic = Convert.ToInt16(dr["Systolic"]),
                                        Diastolic = Convert.ToInt16(dr["Diastolic"]),
                                        Pulse = Convert.ToInt16(dr["Pulse"]),
                                        Respiration = Convert.ToInt16(dr["Respiration"]),
                                        Temperature = Convert.ToDecimal(dr["Respiration"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientVitalSigns.Add(Vital);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Family History

                            if (dtFamilyHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtFamilyHistory.Rows)
                                {
                                    bool diseased = false;
                                    if (dr["diseased"] == "1")
                                        diseased = true;
                                    var Family = new PatientFamilyHist()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatFamilyHistCntr = 0,
                                        FacilityId = FacilityId,
                                        RelationshipId = Convert.ToInt16(dr["RelationshipId"]),
                                        Description = dr["Description"].ToString(),
                                        Qualifier = dr["Qualifier"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        ConditionStatusId = Convert.ToInt16(dr["ConditionStatusId"]),
                                        AgeAtOnset = Convert.ToInt32(dr["AgeAtOnset"]),
                                        Diseased = diseased,
                                        DiseasedAge = Convert.ToInt32(dr["DiseasedAge"]),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientFamilyHists.Add(Family);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Social History

                            if (dtSocialHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtSocialHistory.Rows)
                                {
                                    var Social = new PatientSocialHist()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatSocialHistCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        Qualifier = dr["Qualifier"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        BeginDate = dr["BeginDate"].ToString(),
                                        EndDate = dr["EndDate"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientSocialHists.Add(Social);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Medical History

                            if (dtMedicalHistory.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtMedicalHistory.Rows)
                                {
                                    var Medical = new PatientMedicalHist()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatMedicalHistCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        DateOfOccurance = dr["DateOfOccurance"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        OnCard = false,
                                        OnKeys = false,
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientMedicalHists.Add(Medical);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                             #region Procedure

                            if (dtProcedure.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtProcedure.Rows)
                                {
                                    var Procedure = new PatientProcedure()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatProcedureCntr = 0,
                                        FacilityId = FacilityId,
                                        Description = dr["Description"].ToString(),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Diagnosis = dr["Diagnosis"].ToString(),
                                        PerformedBy = dr["PerformedBy"].ToString(),
                                        ServiceLocation = dr["ServiceLocation"].ToString(),
                                        ServiceDate = dr["ServiceDate"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientProcedures.Add(Procedure);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region Immunization

                            if (dtImmunization.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtImmunization.Rows)
                                {
                                    var Immunization = new PatientImmunization()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PatientImmunizationCntr = 0,
                                        FacilityId = FacilityId,
                                        ImmunizationDate = dr["ImmunizationDate"].ToString(),
                                        ImmunizationTime = "0000",
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Vaccine = dr["Vaccine"].ToString(),
                                        Amount = dr["Amount"].ToString(),
                                        Route = dr["Route"].ToString(),
                                        Site = dr["Site"].ToString(),
                                        Sequence = dr["Sequence"].ToString(),
                                        ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]),
                                        LotNumber = dr["LotNumber"].ToString(),
                                        Manufacturer = dr["Manufacturer"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        UserId_Modified = UserId,
                                        DateModified = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientImmunizations.Add(Immunization);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            #region PlanOfCare

                            if (dtPlanOfCare.Rows.Count > 0)
                            {
                                // Add Items
                                foreach (DataRow dr in dtPlanOfCare.Rows)
                                {
                                    var PlanOfCare = new PatientPlanOfCare()
                                    {
                                        PatientId = VisitDataXML.AMRPatientId,
                                        VisitId = VisitId,
                                        PlanCntr = 0,
                                        FacilityId = FacilityId,
                                        InstructionTypeId = Convert.ToInt16(dr["InstructionTypeId"]),
                                        CodeValue = dr["CodeValue"].ToString(),
                                        CodeSystemId = Convert.ToInt32(dr["CodeSystemId"]),
                                        Instruction = dr["Instruction"].ToString(),
                                        Goal = dr["Goal"].ToString(),
                                        Note = dr["Note"].ToString(),
                                        AppointmentDateTime = Convert.ToDateTime(dr["AppointmentDateTime"]),
                                        ProviderId = Convert.ToInt64(dr["ProviderId"]),
                                        UserId_Created = UserId,
                                        DateCreated = System.DateTime.Now,
                                        Deleted = false,
                                    };
                                    db.PatientPlanOfCares.Add(PlanOfCare);
                                }
                                db.SaveChanges();
                            }
                            #endregion

                            SendMessageNotification(VisitDataXML.AMRPatientId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "An error occurred while updating the entries. See the inner exception for details.")
                        VisitResponse.ErrorMessage = ex.InnerException.InnerException.ToString();
                    else
                        VisitResponse.ErrorMessage = ex.Message;

                    VisitResponse.Valid = false;
                }
            }
            else
            {
                // Invalid Token
                VisitResponse.Valid = false;
                VisitResponse.ErrorMessage = "Invalid Token";
            }
            return VisitResponse;
        }
        #endregion

        #region Post Document
        //------------------------------------------------------------------------
        // Post Document
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Document")]
        public DocumentPostResponse DocumentPost(Int64 FacilityId, Int64 UserId, string Token, DocumentData DocData)
        {
            DocumentPostResponse Response = new DocumentPostResponse();
            Response.Valid = true;
            Int64 VisitId = DocData.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    // Validate the the send data is good

                    string ErrorMsg = "";


                    if (DocData.VisitId == 0)
                        ErrorMsg = ErrorMsg + "VisitId is required;   ";
                    if (DocData.AMRPatientId == 0)
                        ErrorMsg = ErrorMsg + "AMRPatientId is required;   ";
                    if (DocData.DocumentId == null || DocData.DocumentId == "" || DocData.DocumentId.Length > 20)
                        ErrorMsg = ErrorMsg + "DocumentId is required and must be <= 20 characters;   ";
                    if (DocData.Description == null || DocData.Description == "" || DocData.Description.Length > 50)
                        ErrorMsg = ErrorMsg + "Description is required and must be <= 50 characters;   ";

                    if (DocData.DocumentFormat == null || DocData.DocumentFormat == "" || DocData.DocumentFormat.Length > 10)
                        ErrorMsg = ErrorMsg + "DocumentFormat is required and must be <= 10 characters;   ";
                    if (DocData.Notes.Length > 255)
                        DocData.Notes = DocData.Notes.Substring(0, 256);

                    if (ErrorMsg != "")
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = ErrorMsg;
                    }
                    else
                    {
                        using (var db = new AMREntities())
                        {
                            // Make sure this is a valid visit
                            PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == DocData.AMRPatientId && p.VisitId == DocData.VisitId);

                            if (results == null)
                            {
                                DocData.VisitId = 0;

                                results = db.PatientVisits.FirstOrDefault
                                                        (p => p.PatientId == DocData.AMRPatientId
                                                        && p.FacilityId == FacilityId
                                                        && p.VisitId == DocData.VisitId);

                                if (results == null)
                                {

                                    var Visit = new PatientVisit()
                                    {
                                        PatientId = DocData.AMRPatientId,
                                        FacilityId = FacilityId,
                                        VisitId = DocData.VisitId,
                                        VisitDate = DocData.DateCreated,
                                        ProviderId = 0,
                                        VisitReason = "Documents",
                                        Viewable = false,
                                        DateCreated = System.DateTime.Now,
                                        ClinicalSummary = "Portal",
                                    };
                                    db.PatientVisits.Add(Visit);
                                    db.SaveChanges();
                                    //Response.Valid = false;
                                    //Response.ErrorMessage = "VisitId is invalid for this patient";
                                }
                            }
                            //else
                            //{
                                // Get Attachment Folder Info & Update Counters
                                ConfigAttachment Config = db.ConfigAttachments.FirstOrDefault(c => c.ConfigAttachmentId == 1);
                                // Update Attachment Counter
                                if (Config.CurrentFolderDocCntr > 1000)
                                {
                                    Config.AttachmentFolderCntr++;
                                    Config.CurrentFolderDocCntr = 0;
                                }
                                Config.CurrentFolderDocCntr++;
                                db.SaveChanges();

                                // Save Patient Clinical Document
                                var Document = new PatientClinicalDocument()
                                {
                                    PatientId = Convert.ToInt64(DocData.AMRPatientId),
                                    VisitId = DocData.VisitId,
                                    DocumentCntr = 0,
                                    FacilityId = FacilityId,
                                    DocumentDescription = DocData.Description,
                                    DocumentId = DocData.DocumentId,
                                    DocumentFormat = DocData.DocumentFormat,
                                    StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                    Notes = DocData.Notes,
                                    Viewable = true,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                    Deleted = false,
                                };
                                db.PatientClinicalDocuments.Add(Document);

                                db.SaveChanges();

                                // Write the document to the hard disk
                                string FileName = Document.DocumentCntr.ToString() + "." + DocData.DocumentFormat;
                                FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                                byte[] imageBytes = Convert.FromBase64String(DocData.DocumentImage);
                                FileHelper.BytesToDisk(imageBytes, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName); 
                                //FileHelper.BytesToDisk(DocData.DocumentImage, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);

                                SendMessageNotification(Convert.ToInt64(DocData.AMRPatientId));
                            //}
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

        #region Post Lab
        //------------------------------------------------------------------------
        // Post Lab
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Lab")]
        public LabPostResponse LabPost(Int64 FacilityId, Int64 UserId, string Token, LabData LabData)
        {
            LabPostResponse Response = new LabPostResponse();
            Response.Valid = true;
            Int64 VisitId = LabData.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    // Validate the the send data is good

                    string ErrorMsg = "";


                    if (LabData.VisitId == 0)
                        ErrorMsg = ErrorMsg + "VisitId is required;   ";
                    if (LabData.AMRPatientId == 0)
                        ErrorMsg = ErrorMsg + "AMRPatientId is required;   ";
                    if (LabData.ProviderId == 0)
                        ErrorMsg = ErrorMsg + "ProviderId is required;   ";
                    if (LabData.LabName == null || LabData.LabName == "" || LabData.LabName.Length > 50)
                        ErrorMsg = ErrorMsg + "Lab Name is required and must be <= 50 characters;   ";
                    try { DateTime OrderDate = Convert.ToDateTime(LabData.OrderDate); }
                    catch { ErrorMsg = ErrorMsg + " Order Date - Invalid Date Format;  "; }
                    try { DateTime CollectionDate = Convert.ToDateTime(LabData.CollectionDate); }
                    catch { ErrorMsg = ErrorMsg + " Collection Date - Invalid Date Format;  "; }
                    if (LabData.Requisition == null)
                        LabData.Requisition = "";
                    if (LabData.Requisition.Length > 20)
                        ErrorMsg = ErrorMsg + "Requisition must be <= 20 characters;   ";
                    if (LabData.Specimen == null)
                        LabData.Specimen = "";
                    if (LabData.Specimen.Length > 20)
                        ErrorMsg = ErrorMsg + "Specimen must be <= 20 characters;   ";
                    if (LabData.SpecimenSource == null)
                        LabData.SpecimenSource = "";
                    if (LabData.SpecimenSource.Length > 20)
                        ErrorMsg = ErrorMsg + "specimenSource must be <= 20 characters;   ";
                    try { DateTime ReportDate = Convert.ToDateTime(LabData.ReportDate); }
                    catch { ErrorMsg = ErrorMsg + " Report Date - Invalid Date Format;  "; }
                    try { DateTime ReviewDate = Convert.ToDateTime(LabData.ReviewDate); }
                    catch { ErrorMsg = ErrorMsg + " Review Date - Invalid Date Format;  "; }
                    if (LabData.Reviewer == null)
                        LabData.Reviewer = "";
                    if (LabData.Reviewer.Length > 30)
                        ErrorMsg = ErrorMsg + "Reviewer must be <= 30 characters;   ";

                    // Check Lab Details
                    try
                    {
                        foreach (DataRow dr in LabData.Test.Rows)
                        {
                            if (dr["Component"].ToString().Length > 30)
                                dr["Component"] = dr["Component"].ToString().Substring(0, 30);
                            if (dr["Component"].ToString() == "")
                                ErrorMsg = ErrorMsg + "Component is required;  ";
                            if (dr["Result"].ToString().Length > 20)
                                dr["Result"] = dr["Result"].ToString().Substring(0, 20);
                            if (dr["Result"].ToString() == "")
                                ErrorMsg = ErrorMsg + "Result is required;  ";
                            if (dr["RefRange"].ToString().Length > 20)
                                dr["RefRange"] = dr["RefRange"].ToString().Substring(0, 20);
                            if (dr["Units"].ToString().Length > 20)
                                dr["Units"] = dr["Units"].ToString().Substring(0, 20);
                            if (dr["Abnormal"].ToString() != "1" && dr["Abnormal"].ToString() != "0")
                                ErrorMsg = ErrorMsg + "Abnormal must be 0 or 1;  ";
                            if (dr["ResultStatus"].ToString().Length > 50)
                                dr["ResultStatus"] = dr["ResultStatus"].ToString().Substring(0, 50);
                            if (dr["ResultStatus"].ToString() == "")
                                ErrorMsg = ErrorMsg + "ResultStatus is required;  ";
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ErrorMsg + "Procedure " + ex.Message + ";  ";
                    }


                    if (ErrorMsg != "")
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = ErrorMsg;
                    }
                    else
                    {
                        using (var db = new AMREntities())
                        {
                            // Make sure this is a valid visit
                            PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == LabData.AMRPatientId && p.VisitId == VisitId);

                            if (results == null)
                            {
                                Response.Valid = false;
                                Response.ErrorMessage = "VisitId is invalid for this patient";
                            }
                            else
                            {
                                // Add Items
                                var LabResult = new PatientLabResult()
                                {
                                    PatientId = LabData.AMRPatientId,
                                    VisitId = LabData.VisitId,
                                    LabResultCntr = 0,
                                    FacilityId = FacilityId,
                                    ProviderId_Requested = LabData.ProviderId,
                                    CodeValue = "",
                                    CodeSystemId = 0,
                                    LabName = LabData.LabName,
                                    OrderDate = LabData.OrderDate,
                                    CollectionDate = LabData.CollectionDate,
                                    Requisiton = LabData.Requisition,
                                    Specimen = LabData.Specimen,
                                    SpecimenSource = LabData.SpecimenSource,
                                    ReportDate = LabData.ReportDate,
                                    ReviewDate = LabData.ReviewDate,
                                    Reviewer = LabData.Reviewer,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                };

                                int cntr = 0;
                                foreach (DataRow dr in LabData.Test.Rows)
                                {
                                    cntr++;
                                    string abnormal = "N";
                                    if (dr["Abnormal"].ToString() != "0") abnormal = "Y";
                                    var LabTest = new PatientLabResultTest()
                                    {
                                        TestCntr = cntr,
                                        CodeValue = "",
                                        CodeSystemId = 0,
                                        Component = dr["Component"].ToString(),
                                        Result = dr["Result"].ToString(),
                                        RefRange = dr["RefRange"].ToString(),
                                        Units = dr["Units"].ToString(),
                                        Abnormal = abnormal,
                                        ResultStatus = dr["ResultStatus"].ToString(),
                                    };
                                    LabResult.PatientLabResultTests.Add(LabTest);

                                }
                                db.PatientLabResults.Add(LabResult);

                                db.SaveChanges();

                                SendMessageNotification(LabData.AMRPatientId);

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

        #region Post Lab XML
        //------------------------------------------------------------------------
        // Post Lab XML
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post Lab XML")]
        public LabPostResponse LabPostXML(Int64 FacilityId, Int64 UserId, string Token, LabDataXML LabDataXML)
        {
            LabPostResponse Response = new LabPostResponse();
            Response.Valid = true;
            Int64 VisitId = LabDataXML.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;

            if (objToken.Validate())
            {
                try
                {
                    // Validate the the send data is good

                    string ErrorMsg = "";


                    if (LabDataXML.VisitId == 0)
                        ErrorMsg = ErrorMsg + "VisitId is required;   ";
                    if (LabDataXML.AMRPatientId == 0)
                        ErrorMsg = ErrorMsg + "AMRPatientId is required;   ";
                    if (LabDataXML.ProviderId == 0)
                        ErrorMsg = ErrorMsg + "ProviderId is required;   ";
                    if (LabDataXML.LabName == null || LabDataXML.LabName == "" || LabDataXML.LabName.Length > 50)
                        ErrorMsg = ErrorMsg + "Lab Name is required and must be <= 50 characters;   ";
                    try { DateTime OrderDate = Convert.ToDateTime(LabDataXML.OrderDate); }
                    catch { ErrorMsg = ErrorMsg + " Order Date - Invalid Date Format;  "; }
                    try { DateTime CollectionDate = Convert.ToDateTime(LabDataXML.CollectionDate); }
                    catch { ErrorMsg = ErrorMsg + " Collection Date - Invalid Date Format;  "; }
                    if (LabDataXML.Requisition == null) 
                        LabDataXML.Requisition = "";
                    if (LabDataXML.Requisition.Length > 20)
                        ErrorMsg = ErrorMsg + "Requisition must be <= 20 characters;   ";
                    if (LabDataXML.Specimen == null) 
                        LabDataXML.Specimen = "";
                    if (LabDataXML.Specimen.Length > 20)
                        ErrorMsg = ErrorMsg + "Specimen must be <= 20 characters;   ";
                    if (LabDataXML.SpecimenSource == null)
                        LabDataXML.SpecimenSource = "";
                    if (LabDataXML.SpecimenSource.Length > 20)
                        ErrorMsg = ErrorMsg + "specimenSource must be <= 20 characters;   ";
                    try { DateTime ReportDate = Convert.ToDateTime(LabDataXML.ReportDate); }
                    catch { ErrorMsg = ErrorMsg + " Report Date - Invalid Date Format;  "; }
                    try { DateTime ReviewDate = Convert.ToDateTime(LabDataXML.ReviewDate); }
                    catch { ErrorMsg = ErrorMsg + " Review Date - Invalid Date Format;  "; }
                    if (LabDataXML.Reviewer == null)
                        LabDataXML.Reviewer = "";
                    if (LabDataXML.Reviewer.Length > 30)
                        ErrorMsg = ErrorMsg + "Reviewer and must be <= 30 characters;   ";


                    System.IO.StringReader theReader = new System.IO.StringReader(LabDataXML.Test);
                    DataSet LabDataSet = new DataSet();
                    LabDataSet.ReadXml(theReader);
                    DataTable dtTests = LabDataSet.Tables["LabData"];

                    // Check Lab Details
                    try
                    {
                        foreach (DataRow dr in dtTests.Rows)
                        {
                            if (dr["Component"].ToString().Length > 30)
                                dr["Component"] = dr["Component"].ToString().Substring(0, 30);
                            if (dr["Component"].ToString() == "")
                                ErrorMsg = ErrorMsg + "Component is required;  ";
                            if (dr["Result"].ToString().Length > 20)
                                dr["Result"] = dr["Result"].ToString().Substring(0, 20);
                            if (dr["Result"].ToString() == "")
                                ErrorMsg = ErrorMsg + "Result is required;  ";
                            if (dr["RefRange"].ToString().Length > 20)
                                dr["RefRange"] = dr["RefRange"].ToString().Substring(0, 20);
                            if (dr["Units"].ToString().Length > 20)
                                dr["Units"] = dr["Units"].ToString().Substring(0, 20);
                            if (dr["Abnormal"].ToString() != "1" && dr["Abnormal"].ToString() != "0")
                                ErrorMsg = ErrorMsg + "Abnormal must be 0 or 1;  ";
                            if (dr["ResultStatus"].ToString().Length > 50)
                                dr["ResultStatus"] = dr["ResultStatus"].ToString().Substring(0, 50);
                            if (dr["ResultStatus"].ToString() == "")
                                ErrorMsg = ErrorMsg + "ResultStatus is required;  ";
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorMsg = ErrorMsg + "Procedure " + ex.Message + ";  ";
                    }


                    if (ErrorMsg != "")
                    {
                        Response.Valid = false;
                        Response.ErrorMessage = ErrorMsg;
                    }
                    else
                    {
                        using (var db = new AMREntities())
                        {
                            // Make sure this is a valid visit
                            PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == LabDataXML.AMRPatientId && p.VisitId == VisitId);

                            if (results == null)
                            {
                                var Visit = new PatientVisit()
                                {
                                    PatientId = LabDataXML.AMRPatientId,
                                    FacilityId = FacilityId,
                                    VisitId = LabDataXML.VisitId,
                                    VisitDate = LabDataXML.OrderDate,
                                    ProviderId = LabDataXML.ProviderId,
                                    VisitReason = "Lab results",
                                    Viewable = false,
                                    DateCreated = System.DateTime.Now,
                                    ClinicalSummary = "Portal",
                                };
                                db.PatientVisits.Add(Visit);
                                db.SaveChanges();
                                //Response.Valid = false;
                                //Response.ErrorMessage = "VisitId is invalid for this patient";
                            }
                            //else
                            //{
                                // Add Items
                                var LabResult = new PatientLabResult()
                                {
                                    PatientId = LabDataXML.AMRPatientId,
                                    VisitId = LabDataXML.VisitId,
                                    LabResultCntr = 0,
                                    FacilityId = FacilityId,
                                    ProviderId_Requested = LabDataXML.ProviderId,
                                    CodeValue = "",
                                    CodeSystemId = 0,
                                    LabName = LabDataXML.LabName,
                                    OrderDate = LabDataXML.OrderDate,
                                    CollectionDate = LabDataXML.CollectionDate,
                                    Requisiton = LabDataXML.Requisition,
                                    Specimen = LabDataXML.Specimen,
                                    SpecimenSource = LabDataXML.SpecimenSource,
                                    ReportDate = LabDataXML.ReportDate,
                                    ReviewDate = LabDataXML.ReviewDate,
                                    Reviewer = LabDataXML.Reviewer,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                };

                                int cntr = 0;
                                foreach (DataRow dr in dtTests.Rows)
                                {
                                    cntr++;
                                    string abnormal = "N";
                                    if (dr["Abnormal"].ToString() != "0") abnormal = "Y";
                                    var LabTest = new PatientLabResultTest()
                                    {
                                        TestCntr = cntr,
                                        CodeValue = "",
                                        CodeSystemId = 0,
                                        Component = dr["Component"].ToString(),
                                        Result = dr["Result"].ToString(),
                                        RefRange = dr["RefRange"].ToString(),
                                        Units = dr["Units"].ToString(),
                                        Abnormal = abnormal,
                                        ResultStatus = dr["ResultStatus"].ToString(),
                                    };
                                    LabResult.PatientLabResultTests.Add(LabTest);

                                }
                                db.PatientLabResults.Add(LabResult);

                                db.SaveChanges();

                                SendMessageNotification(LabDataXML.AMRPatientId);
                            //}
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

        #region Post CCD Data
        //------------------------------------------------------------------------
        // Post CCD Data
        //------------------------------------------------------------------------

        [WebMethod(Description = "Post CCD Data")]
        public CCDPostResponse CCDPost(Int64 FacilityId, Int64 UserId, string Token, CCDPostData CCDData)
        {
            CCDPostResponse CCDResponse = new CCDPostResponse();
            CCDResponse.Valid = true;
            Int64 VisitId = CCDData.VisitId;
            // Validate the Token
            clsToken objToken = new clsToken();
            objToken.Token = Token;
            objToken.UserId = UserId;
            objToken.FacilityId = FacilityId;          

            if (objToken.Validate())
            {
                try
                {
                    Int64 AMRPatientId = CCDData.AMRPatientId;
                    
                    XmlDocument xd = new XmlDocument();

                    string xml = Encoding.UTF8.GetString(CCDData.Document);
                    xd.LoadXml(xml);

                    string ErrorMsg = "";
                    clsCCDImport objCCDImport = new clsCCDImport();

                    bool Valid = objCCDImport.Parse(FacilityId, UserId, AMRPatientId, xd, ref ErrorMsg);

                    if (ErrorMsg == "")
                    {
                        SendMessageNotification(CCDData.AMRPatientId);
                    }


                }
                catch (Exception ex)
                {
                    CCDResponse.Valid = false;
                    CCDResponse.ErrorMessage = ex.Message;
                }
            }
            else
            {
                // Invalid Token
                CCDResponse.Valid = false;
                CCDResponse.ErrorMessage = "Invalid Token";
            }
            return CCDResponse;
        }
        #endregion

        #region Date Check
        //------------------------------------------------------------------------
        // Date Check
        //------------------------------------------------------------------------
        private bool DateStringCheck(string ckDate)
        {
            bool Valid = true;
            try
            {
                if (ckDate.IndexOf('-') > 0)
                {
                    Valid = false;
                }
                else if (ckDate.Length == 0)
                {
                        Valid = true;
                }
                else if (ckDate.Length == 4)
                {
                    if (Convert.ToInt32(ckDate) < 1900 || Convert.ToInt32(ckDate) > Convert.ToInt32(System.DateTime.Now.Year))
                        Valid = false;
                }

                else if (ckDate.Length == 6)
                {
                    if (Convert.ToInt32(ckDate.Substring(0, 4)) < 1900 || Convert.ToInt32(ckDate.Substring(0, 4)) > Convert.ToInt32(System.DateTime.Now.Year))
                        Valid = false;
                    if (Convert.ToInt32(ckDate.Substring(4, 2)) < 1 || Convert.ToInt32(ckDate.Substring(4, 2)) > 12)
                        Valid = false;
                }
                else if (ckDate.Length == 8)
                {
                    if (Convert.ToInt32(ckDate.Substring(0, 4)) < 1900 || Convert.ToInt32(ckDate.Substring(0, 4)) > Convert.ToInt32(System.DateTime.Now.Year))
                        Valid = false;
                    if (Convert.ToInt32(ckDate.Substring(4, 2)) < 1 || Convert.ToInt32(ckDate.Substring(4, 2)) > 12)
                        Valid = false;
                    if (Convert.ToInt32(ckDate.Substring(6, 2)) < 1 || Convert.ToInt32(ckDate.Substring(6, 2)) > 31)
                        Valid = false;
                    DateTime Test = Convert.ToDateTime(ckDate.Substring(4, 2) + "/" + ckDate.Substring(6, 2) + "/" + ckDate.Substring(0, 4));
                }
                else
                {
                    Valid = false;
                }
            }

            catch
            {
                Valid = false;
            }
            return Valid;
        }
        #endregion

        #region Cleaner
        //------------------------------------------------------------------------
        // Clean String Removing Extra Characters.
        //------------------------------------------------------------------------
        //private string Cleaner(string InString)
        //{
        //    if (InString == null)
        //        InString = "";
        //    InString = InString.ToLower();
        //    string OutString = "";
        //    foreach (char c in InString)
        //    {
        //        if (Char.IsLetter(c))
        //            OutString = OutString + c.ToString();
        //    }
        //    return OutString;
        //}
        #endregion

        #region SendMessageNotification

        private void SendMessageNotification(Int64 PatientId)
        {
            // Send Email to patient notifying of new message
            try
            {
                // Get the patient's setting for notifications

                using (var db = new AMREntities())
                {
                    var results = from p in db.Patients
                                  join w in db.PatientWebSettings on p.PatientId equals w.PatientId
                                  join u in db.Users on PatientId equals u.UserRoleLink
                                  join c in db.Carriers on w.CellCarrier equals c.CarrierId into CarrierGroup
                                  from cg in CarrierGroup.DefaultIfEmpty()
                                  where p.PatientId == PatientId
                                  && u.UserRoleId == 5
                                  select new
                                  {
                                      p.PatientId,
                                      p.Title,
                                      p.FirstName,
                                      p.LastName,
                                      p.MobilePhone,
                                      u.UserEmail,
                                      w.EmailNotifyNewMessage,
                                      w.TextNotifyNewMesssage,
                                      w.CellCarrier,
                                      cg.CarrierURL,
                                  };

                    DataTable dt = clsTableConverter.ToDataTable(results);

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToBoolean(dr["EmailNotifyNewMessage"]))
                        {
                            clsEmail objEmail = new clsEmail();
                            string host = "";
                            Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                            if (Email != null)
                                host = Email.SiteURL;

                            string Msg = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" style=\"width:100%; line-height: 100%; font-family: 'Lato', Arial, sans-serif; color:#000;\">" +
                                            "   <tr style=\"background-color:#00a0e0; height:10px; \"><td></td></tr>" +
                                            "	<tr style=\"background-color:#00a0e0;\">" +
                                            "       <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; margin-top:15px;\">" +
                                            "			<tr width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                            "                <td width=\"571\" valign=\"center\" align=\"center\" style=\"text-align:center;background-color:#fff;margin:15px 0; \">" +
                                            "                <img src=\"https://www.amrportal.com/LetterImages/amr-patient-portal.png\" alt=\"Logo\" style=\"outline:none; text-decoration:none; -ms-interpolation-mode: bicubic; border:none; display:block;margin:15px auto\" />" +
                                            "              </td>" +
                                            "              </tr>" +
                                            "          </table>" +
                                            "       </td>" +
                                            "   </tr>" +
                                            "	<tr>" +
                                            "       <td valign=\"top\">" +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "           <tr style=height:1px;><td></td></tr>" +
                                            "			<tr >" +
                                            "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + dr["Title"] + " " + dr["FirstName"] + " " + dr["LastName"] + "</strong>,</h1>  <br />" +
                                            "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                            "               This is to inform you that your portal has received an update from your physician. We encourage you to look at the most current documentation associated with your last appointment. To view this information," +
                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">click here</a> to log in to your portal using your AccessID Code and password.<br /><br />" +
                                            "               Thank you, <br />" +
                                            "               Your Member Services Team<br />" +
                                            "               <br /><br />" +
                                            "               Please note that your online account is subject to our <a href=\"http://" + host + "/Content/Legal/Terms.html" + "\" style=\"color: rgb(0, 84, 148);\">Terms and Conditions</a>. Use of your account confirms agreement to these terms. " +
                                            "             </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "      </td>" +
                                            "   </tr>" +
                                            "	<tr style=\"background-color:#37b74a;\">" +
                                            "      <td valign=\"top\"> " +
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\" style=\"border-collapse:collapse; background-color:#fff;\" width=\"571\">" +
                                            "            <tr style=height:10px;>" +
                                            "            <td></td>" +
                                            "            </tr>" +
                                            "			 <tr style=\"display:block;background-color:#00a0e0;height: 78px;width: 571px;\">" +
                                            "              <td valign=\"bottom\" align=\"center\" style=\"color: #FFFFFF; font-size: 12px\">" +
                                            "              <a href=\"http://" + host + "\" target=\"_blank\" style=\"color: #FFFFFF;\">" + host + "</a><br />" +
                                            "              Copyright © 2014 AMR Patient Portal, All rights reserved.<br/>" +
                                            "              2385 NW Executive Center Drive, Suite 100, Boca Raton, FL 33431 <br />" +
                                            "              </td>" +
                                            "            </tr>" +
                                            "          </table>" +
                                            "          <br /><br />" +
                                            "       </td>  " +
                                            "     </tr>" +
                                            "	<tr style=\"background-color:#37b74a; height:10px;\"><td></td></tr>" +
                                            "</table>";

                            objEmail.SendEmailHTML(dr["UserEMail"].ToString(), "AMR Patient Portal - New Patient Information Available", Msg);

                        }
                        if (Convert.ToBoolean(dr["TextNotifyNewMesssage"]))
                        {
                            if (dr["MobilePhone"].ToString().Trim() != "" && dr["CarrierURL"].ToString() != null)
                            {
                                clsEmail objEmail = new clsEmail();

                                string Msg = "You have a message waiting ";
                                string EmailAddress = dr["MobilePhone"].ToString().Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "@" + dr["CarrierURL"].ToString();

                                objEmail.SendEmail(EmailAddress, "AMR Patient Portal - New Patient Information Available", Msg);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion
    }
}
