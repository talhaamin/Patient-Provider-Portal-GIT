using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMR.Data;

namespace AMR.Data
{
    public class clsDirectImport
    {
        private struct PatientData
        {
            public Int64 AMRPatientId;
            public string FirstName;
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
            public int GenderId;
        }
        private struct FacilityData
        {
            public Int64 FacilityId;
            public Int64 OrganizationId;
            public Int64 PracticeId;
            public int EMRSystemId;
            public string SendingSystemName;
            public string Name;
            public string Address1;
            public string Address2;
            public string Address3;
            public string City;
            public string State;
            public string CountryCode;
            public string PostalCode;
            public string Phone;
            public string Fax;
        }
        private struct VisitData
        {
            public Int64 VisitId;
            public string Reason;
            public string Date;
        }

        public bool Parse(XmlDocument xd, ref string ErrorMsg, ref Int64 FacilityId, ref Int64 PatientId,  string PatientEmail)
        {
            string DocumentType = "";
            PatientData sPatient = new PatientData();
            FacilityData sFacility = new FacilityData();
            VisitData sVisit = new VisitData();
            
            #region Initialize Data Structures
            sPatient.AMRPatientId = PatientId;
            sPatient.FirstName = "";
            sPatient.LastName = "";
            sPatient.Address1 = "";
            sPatient.Address2 = "";
            sPatient.Address3 = "";
            sPatient.City = "";
            sPatient.State = "";
            sPatient.Postal = "";
            sPatient.CountryCode = "";
            sPatient.HomePhone = "";
            sPatient.MobilePhone = "";
            sPatient.WorkPhone = "";
            sPatient.Fax = "";
            sPatient.EMail = PatientEmail;
            sPatient.DOB = Convert.ToDateTime("1/1/1900");
            sPatient.SSN = "";
            sPatient.GenderId = 99;

            sFacility.FacilityId = FacilityId;
            sFacility.EMRSystemId = 0;
            sFacility.SendingSystemName = "";
            sFacility.Name = "";
            sFacility.Address1 = "";
            sFacility.Address2 = "";
            sFacility.Address3 = "";
            sFacility.City = "";
            sFacility.State = "";
            sFacility.PostalCode = "";
            sFacility.CountryCode = "";
            sFacility.Phone = "";
            sFacility.Fax = "";

            sVisit.VisitId = 0;
            sVisit.Reason = "General";
            sVisit.Date = "";// System.DateTime.Now.ToString();
            #endregion

            bool Valid = true;

            string ServiceDate = "";
            Int64 UserId = 0;
            Int64 ProviderId = 0;
            

            #region Get Document Type

            XmlNode nodeClinicalDocument = xd["ClinicalDocument"];
            foreach (XmlNode node in nodeClinicalDocument)
            {
                if (node.Name == "templateId")
                {
                    string type = node.Attributes.GetNamedItem("root").Value;
                    if (type == "2.16.840.1.113883.10.20.22.1.10" || type == "2.16.840.1.113883.10.20.19.1")
                    {
                        DocumentType = "Unstructured";
                        break;
                    }
                    else if (type == "1.3.6.1.4.1.19376.1.2.20") // ITI OIDs for XDS Scanned Documents 
                    {
                        DocumentType = "Unstructured";
                        break;
                    }
                    else if (type == "2.16.840.1.113883.10.20.22.1.2")  // Continuity Of Care Document
                    {
                        DocumentType = "CCD";
                        break;
                    }
                    else if (type == "2.16.840.1.113883.10.20.22.1.8")  // Discharge Summary
                    {
                        DocumentType = "CCD";
                        break;
                    }
                    else if (type == "1.3.6.1.4.1.19376.1.5.3.1.1.2")  // IHE Medical Summary
                    {
                        DocumentType = "CCD";
                        break;
                    }
                }
            }
            #endregion

            // Check if the document type is valid
            if (DocumentType == "")
            {
                Valid = false;
                ErrorMsg = "Invalid document type";
            }
            else
            {

                #region Get the patient and facility information

                // Patient
                if (sPatient.AMRPatientId == 0)
                {
                    XmlNode nodeRecordTarget = xd.DocumentElement["recordTarget"];
                    if (nodeRecordTarget != null)
                    {
                        try
                        {
                            XmlNode NodePatientRole = nodeRecordTarget["patientRole"];

                            XmlNode nodePatient = NodePatientRole["patient"];
                            XmlNode nodeName = nodePatient["name"];
                            XmlNode nodeGiven = nodeName["given"];
                            sPatient.FirstName = nodeGiven.InnerText;
                            XmlNode nodeFamily = nodeName["family"];
                            sPatient.LastName = nodeFamily.InnerText;
                            XmlNode nodeBirthTime = nodePatient["birthTime"];
                            if (nodeBirthTime != null)
                            {
                                string temp = nodeBirthTime.Attributes.GetNamedItem("value").Value;
                                sPatient.DOB = Convert.ToDateTime(temp.Substring(4, 2) + "/" + temp.Substring(6, 2) + "/" + temp.Substring(0, 4));
                            }
                            XmlNode nodeGender = nodePatient["administrativeGenderCode"];
                            if (nodeGender != null)
                            {
                                string Gender = (nodeGender.Attributes.GetNamedItem("code").Value);
                                if (Gender.ToUpper() == "M")
                                    sPatient.GenderId = 1;
                                else if (Gender.ToUpper() == "F")
                                    sPatient.GenderId = 2;
                                else if (Gender.ToUpper() == "U")
                                    sPatient.GenderId = 0;
                            }
                            XmlNode nodeAddr = NodePatientRole["addr"];
                            XmlNode nodeStreet = nodeAddr["streetAddressLine"];
                            sPatient.Address1 = nodeStreet.InnerText;
                            XmlNode nodeCity = nodeAddr["city"];
                            sPatient.City = nodeCity.InnerText;
                            XmlNode nodeState = nodeAddr["state"];
                            sPatient.State = nodeState.InnerText;
                            XmlNode nodePostalCode = nodeAddr["postalCode"];
                            sPatient.Postal = nodePostalCode.InnerText;
                            XmlNode nodeCountry = nodeAddr["country"];
                            sPatient.CountryCode = nodeCountry.InnerText;

                            foreach (XmlNode node in NodePatientRole)
                            {
                                if (node.Name == "id")
                                {
                                    try
                                    {
                                        string type = node.Attributes.GetNamedItem("root").Value;
                                        if (type == "2.16.840.1.113883.4.1")
                                        {
                                            // SSN
                                            sPatient.SSN = node.Attributes.GetNamedItem("extension").Value.Replace("-", "");
                                        }
                                    }
                                    catch { }
                                }
                                if (node.Name == "telecom")
                                {
                                    try
                                    {
                                        string temp = node.Attributes.GetNamedItem("value").Value.ToString();
                                        if (temp.Substring(0, 3) == "tel")
                                        {
                                            sPatient.HomePhone = temp.Substring(4, temp.Length - 4);
                                            if (sPatient.HomePhone.Length > 16)
                                                sPatient.HomePhone = sPatient.HomePhone.Substring(0, 16);
                                        }
                                        else if (temp.Substring(0, 6) == "mailto")
                                        {
                                            sPatient.EMail = temp.Substring(7, temp.Length - 7);
                                            if (sPatient.EMail.Length > 60)
                                                sPatient.EMail = sPatient.EMail.Substring(0, 60);
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                // Facility
                if (sFacility.FacilityId == 0)
                {
                    XmlNode nodeCustodian = xd.DocumentElement["custodian"];
                    if (nodeCustodian != null)
                    {
                        try
                        {
                            XmlNode nodeAssignedCustodian = nodeCustodian["assignedCustodian"];

                            XmlNode nodeOrganization = nodeAssignedCustodian["representedCustodianOrganization"];

                            XmlNode nodeName = nodeOrganization["name"];
                            sFacility.Name = nodeName.InnerText;

                            XmlNode nodeAddr = nodeOrganization["addr"];
                            XmlNode nodeStreet = nodeAddr["streetAddressLine"];
                            sFacility.Address1 = nodeStreet.InnerText;
                            XmlNode nodeCity = nodeAddr["city"];
                            sFacility.City = nodeCity.InnerText;
                            XmlNode nodeState = nodeAddr["state"];
                            sFacility.State = nodeState.InnerText;
                            XmlNode nodePostalCode = nodeAddr["postalCode"];
                            sFacility.PostalCode = nodePostalCode.InnerText;
                            XmlNode nodeCountry = nodeAddr["country"];
                            sFacility.CountryCode = nodeCountry.InnerText;

                            foreach (XmlNode node in nodeOrganization)
                            {
                                if (node.Name == "id")
                                {
                                    try
                                    {
                                        string type = node.Attributes.GetNamedItem("root").Value;
                                        // if (root == "")
                                        string id = node.Attributes.GetNamedItem("extension").Value;
                                    }
                                    catch { }
                                }
                                if (node.Name == "telecom")
                                {
                                    try
                                    {
                                        string temp = node.Attributes.GetNamedItem("value").Value.ToString();
                                        if (temp.Substring(0, 3) == "tel")
                                        {
                                            sFacility.Phone = temp.Substring(4, temp.Length - 4);
                                            if (sFacility.Phone.Length > 16)
                                                sFacility.Phone = sPatient.HomePhone.Substring(0, 16);
                                        }
                                    }
                                    catch { }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                // Get the system that the data is coming from
                XmlNode nodeAuthor = xd.DocumentElement["author"];
                if (nodeAuthor != null)
                {
                    try
                    {
                        XmlNode nodeAssignedAuthor = nodeAuthor["assignedAuthor"];
                        XmlNode nodeRepresentedOrganization = nodeAssignedAuthor["representedOrganization"];
                        XmlNode nodeName = nodeRepresentedOrganization["name"];
                        sFacility.SendingSystemName = nodeName.InnerText;
                    }
                    catch
                    {
                    }
                }
                //  Get The Service Date
                XmlNode nodeDocumentationOf = xd.DocumentElement["documentationOf"];
                if (nodeDocumentationOf != null)
                {
                    try
                    {
                        XmlNode nodeServiceEvent = nodeDocumentationOf["serviceEvent"];
                        XmlNode nodeEffectiveTime = nodeServiceEvent["effectiveTime"];
                        XmlNode nodeLow = nodeEffectiveTime["low"];
                        ServiceDate = nodeLow.Attributes.GetNamedItem("value").Value;
                    }
                    catch
                    {
                    }
                }
                #endregion

                #region Validate the patient and facility information

                if (sPatient.AMRPatientId == 0)
                {
                    if (sPatient.FirstName == "" || sPatient.LastName == "")
                    {
                        ErrorMsg = "Missing patient name";
                        Valid = false;
                    }
                    else if (sPatient.Address1 == "" || sPatient.City == "" || sPatient.State == "" || sPatient.Postal == "" || sPatient.CountryCode == "")
                    {
                        ErrorMsg = "Missing patient address";
                        Valid = false;
                    }
                    else if (sPatient.EMail == "")
                    {
                        ErrorMsg = "Missing patient email";
                        Valid = false;
                    }
                    else if (sPatient.DOB == Convert.ToDateTime("1/1/1900"))
                    {
                        ErrorMsg = "Missing patient DOB";
                        Valid = false;
                    }
                    else if (sPatient.GenderId == 99)
                    {
                        ErrorMsg = "Missing patient gender";
                        Valid = false;
                    }
                    else
                    {
                        // Make sure fields are not too long
                        if (sPatient.FirstName.Length > 20)
                            sPatient.FirstName = sPatient.FirstName.Substring(0, 20);
                        if (sPatient.LastName.Length > 20)
                            sPatient.LastName = sPatient.LastName.Substring(0, 20);
                        if (sPatient.Address1.Length > 50)
                            sPatient.Address1 = sPatient.Address1.Substring(0, 50);
                        if (sPatient.City.Length > 30)
                            sPatient.City = sPatient.City.Substring(0, 30);
                        if (sPatient.State.Length > 10)
                            sPatient.State = sPatient.State.Substring(0, 10);
                        if (sPatient.Postal.Length > 10)
                            sPatient.Postal = sPatient.Postal.Substring(0, 10);
                        if (sPatient.CountryCode.Length > 3)
                            sPatient.CountryCode = "USA";
                        if (sPatient.EMail.Length > 60)
                            sPatient.EMail = sPatient.EMail.Substring(0, 60);
                        sPatient.SSN = sPatient.SSN.Replace("-", "").Replace(" ", "");
                        if (sPatient.SSN.Length > 9)
                            sPatient.SSN = sPatient.SSN.Substring(0, 9);
                        if (sPatient.HomePhone.Length > 16)
                            sPatient.HomePhone = sPatient.HomePhone.Substring(0, 16);
                    }
                }
                if (sFacility.FacilityId == 0)
                {
                    if (sFacility.Name == "")
                    {
                        ErrorMsg = "Missing facility name";
                        Valid = false;
                    }
                    else if (sFacility.Address1 == "" || sFacility.City == "" || sFacility.State == "" || sFacility.PostalCode == "" || sFacility.CountryCode == "")
                    {
                        ErrorMsg = "Missing facility address";
                        Valid = false;
                    }
                    else
                    {
                        if (sFacility.Name.Length > 50)
                            sFacility.Name = sFacility.Name.Substring(0, 50);
                        if (sFacility.Address1.Length > 50)
                            sFacility.Address1 = sFacility.Address1.Substring(0, 50);
                        if (sFacility.City.Length > 30)
                            sFacility.City = sFacility.City.Substring(0, 30);
                        if (sFacility.State.Length > 10)
                            sFacility.State = sFacility.State.Substring(0, 10);
                        if (sFacility.PostalCode.Length > 10)
                            sFacility.PostalCode = sFacility.PostalCode.Substring(0, 10);
                        if (sFacility.CountryCode.Length > 3)
                            sFacility.CountryCode = "USA";
                        if (sFacility.Phone.Length > 16)
                            sFacility.Phone = sFacility.Phone.Substring(0, 16);
                        if (sFacility.Fax.Length > 16)
                            sFacility.Fax = sFacility.Fax.Substring(0, 16);
                    }
                }

                #endregion

                using (var db = new AMREntities())
                {

                    #region Match / Create Facility
                    if (sFacility.FacilityId == 0)
                    {
                        if (Valid)
                        {
                            sFacility.EMRSystemId = 0;
                            if (sFacility.SendingSystemName != "")
                            {
                                C_EMRSystem Eresults = db.C_EMRSystem.FirstOrDefault(s => s.Value == sFacility.SendingSystemName);
                                if (Eresults != null)
                                    sFacility.EMRSystemId = Eresults.EMRSystemId;
                            }

                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == sFacility.CountryCode);
                            if (results == null)
                                sFacility.CountryCode = "USA";

                            // Check if the Facility Already Exists
                            Facility FacilityResp = db.Facilities.FirstOrDefault(p => p.FacilityName == sFacility.Name && p.Address1 == sFacility.Address1 && p.PostalCode == sFacility.PostalCode);

                            if (FacilityResp != null)
                            {
                                sFacility.FacilityId = FacilityResp.FacilityId;
                            }
                            else
                            {
                                // Create New Organization, Practice & Facility
                                // Add Organization
                                var Organization = new Organization()
                                {

                                    OrganizationId = 0,
                                    OrganizationName = sFacility.Name,
                                    Address1 = sFacility.Address1,
                                    Address2 = "",
                                    Address3 = "",
                                    City = sFacility.City,
                                    State = sFacility.State,
                                    PostalCode = sFacility.PostalCode,
                                    CountryCode = sFacility.CountryCode,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Organizations.Add(Organization);

                                db.SaveChanges();

                                sFacility.OrganizationId = Organization.OrganizationId;

                                var Practice = new Practice()
                                {

                                    PracticeId = 0,
                                    OrganizationId = sFacility.OrganizationId,
                                    PracticeName = sFacility.Name,
                                    Address1 = sFacility.Address1,
                                    Address2 = "",
                                    Address3 = "",
                                    City = sFacility.City,
                                    State = sFacility.State,
                                    PostalCode = sFacility.PostalCode,
                                    CountryCode = sFacility.CountryCode,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Practices.Add(Practice);

                                db.SaveChanges();

                                sFacility.PracticeId = Practice.PracticeId;

                                var Facility = new Facility()
                                {

                                    PracticeId = sFacility.PracticeId,
                                    EMRSystemId = sFacility.EMRSystemId,
                                    FacilityTypeId = 0,
                                    FacilityName = sFacility.Name,
                                    Address1 = sFacility.Address1,
                                    Address2 = "",
                                    Address3 = "",
                                    City = sFacility.City,
                                    State = sFacility.State,
                                    PostalCode = sFacility.PostalCode,
                                    CountryCode = sFacility.CountryCode,
                                    Phone = sFacility.Phone,
                                    Fax = sFacility.Fax,
                                    BillAddress1 = "",
                                    BillAddress2 = "",
                                    BillAddress3 = "",
                                    BillCity = "",
                                    BillState = "",
                                    BillPostalCode = "",
                                    BillCountryCode = "",
                                    BillPhone = "",
                                    BillFax = "",
                                    GeneralMessageAvailable = false,
                                    GeneralMessageNotify = 0,
                                    GeneralMessageEmail = "",
                                    AppointmentMessageAvailable = false,
                                    AppointmentMessageNotify = 0,
                                    AppointmentMessageEmail = "",
                                    MedicationMessageAvailable = false,
                                    MedicationMessageNotify = 0,
                                    MedicationMessageEmail = "",
                                    ReferralMessageAvailable = false,
                                    ReferralMessageNotify = 0,
                                    ReferralMessageEmail = "",
                                    PremiumAvailable = true,
                                    OnlineBillPayment = 0,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                };
                                db.Facilities.Add(Facility);

                                db.SaveChanges();

                                sFacility.FacilityId = Facility.FacilityId;
                            }
                        }
                    }
                    // Set the FacilityId to return
                    FacilityId = sFacility.FacilityId;

                    #endregion

                    #region Match / Create Patient
                    if (sPatient.AMRPatientId == 0)
                    {
                        if (Valid)
                        {
                            C_Country results = db.C_Country.FirstOrDefault(c => c.CountryId == sPatient.CountryCode);
                            if (results == null)
                                sFacility.CountryCode = "USA";
                            string SSN_Encrypted = "";
                            if (sPatient.SSN != "")
                                SSN_Encrypted = clsEncryption.Encrypt(sPatient.SSN, "AMRP@ss");

                            System.Data.Objects.ObjectParameter s = new System.Data.Objects.ObjectParameter("PatientId", typeof(Int64));
                            db.FindPatient(s, sPatient.FirstName, sPatient.LastName, sPatient.DOB, SSN_Encrypted, sPatient.GenderId, sPatient.Address1, sPatient.City, sPatient.State, sPatient.Postal, sPatient.HomePhone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", ""), "", "", "", "", "", "", "", "", "", "", "", "", "");
                            sPatient.AMRPatientId = (Int64)s.Value;

                            if (sPatient.AMRPatientId > 0)
                            {
                                // Patient Found

                                // Check If Link Exist
                                var LinkResults = db.PatientFacilityLinks.Find(sPatient.AMRPatientId, sFacility.FacilityId);
                                if (LinkResults == null)
                                {
                                    // Create Link 
                                    var NewFacilityLink = new PatientFacilityLink()
                                    {

                                        PatientId = sPatient.AMRPatientId,
                                        FacilityId = sFacility.FacilityId,
                                        DataAdded = System.DateTime.Now,
                                    };
                                    db.PatientFacilityLinks.Add(NewFacilityLink);

                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                // Create A New Patient & Set Patient Id


                                // Get EMR Type from Facility File

                                int EHRSystem = 0;
                                Facility FacilityResp = db.Facilities.FirstOrDefault(p => p.FacilityId == sFacility.FacilityId);
                                if (FacilityResp != null)
                                {
                                    EHRSystem = Convert.ToInt32(FacilityResp.EMRSystemId);
                                }



                                // Add Patient
                                var NewPatient = new Patient()
                                {
                                    PatientId = sPatient.AMRPatientId,
                                    FirstName = sPatient.FirstName,
                                    MiddleName = "",
                                    LastName = sPatient.LastName,
                                    Title = "",
                                    Suffix = "",
                                    Address1 = sPatient.Address1,
                                    Address2 = "",
                                    Address3 = "",
                                    City = sPatient.City,
                                    State = sPatient.State,
                                    Zip = sPatient.Postal,
                                    CountryCode = sPatient.CountryCode,
                                    DOB = sPatient.DOB,
                                    SSN = SSN_Encrypted,
                                    HomePhone = sPatient.HomePhone,
                                    MobilePhone = "",
                                    WorkPhone = "",
                                    Fax = "",
                                    EMail = sPatient.EMail,
                                    PreferredLanguageId = 0,
                                    GenderId = sPatient.GenderId,
                                    RaceId_NotAnswer = true,
                                    RaceId_Native = false,
                                    RaceId_Asian = false,
                                    RaceId_Black = false,
                                    RaceId_Hawaiian = false,
                                    RaceId_White = false,
                                    EthnicityId = 0,
                                    MaritalStatusId = 0,
                                    SmokingStatusId = 0,
                                    ReligionId = 0,
                                    UserId_Created = UserId,
                                    DateCreated = System.DateTime.Now,
                                    FacilityId_Created = sFacility.FacilityId,
                                    UserId_Modified = UserId,
                                    DateModified = System.DateTime.Now,
                                    SmokingDate = "1/1/1900",
                                    Active = true,
                                    ThirdPartyId = 1,
                                    EMRSystemId = EHRSystem,

                                };
                                db.Patients.Add(NewPatient);

                                db.SaveChanges();

                                sPatient.AMRPatientId = NewPatient.PatientId;

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

                                    UserLogin = sPatient.AMRPatientId.ToString(),
                                    UserEmail = sPatient.EMail,
                                    Password = passencr,
                                    UserRoleId = 5,
                                    UserRoleLink = sPatient.AMRPatientId,
                                    Enabled = true,
                                    Locked = false,
                                    ResetPassword = true,
                                };
                                db.Users.Add(NewUser);

                                db.SaveChanges();

                                // Add Visit 0
                                var NewVisit = new PatientVisit()
                                {

                                    PatientId = sPatient.AMRPatientId,
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

                                    PatientId = sPatient.AMRPatientId,
                                    FacilityId = sFacility.FacilityId,
                                    DataAdded = System.DateTime.Now,
                                };
                                db.PatientFacilityLinks.Add(NewFacilityLink);

                                db.SaveChanges();

                                // Create Patient Share Record

                                var PatientShare = new PatientShare()
                                {
                                    PatientId = sPatient.AMRPatientId,
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

                                    PatientId = sPatient.AMRPatientId,
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
                                    Facility FacResponse = db.Facilities.FirstOrDefault(f => f.FacilityId == sFacility.FacilityId);

                                    if (FacResponse != null)
                                        FacilityName = FacResponse.FacilityName;

                                    string host = "";
                                    Config Email = db.Configs.FirstOrDefault(c => c.ConfigId == 1);
                                    if (Email != null)
                                        host = Email.SiteURL;

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
                                            "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\">" +
                                            "			<tr id=\"backgroundTable\" width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                            "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                            "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + sPatient.FirstName + " " + sPatient.LastName + "</strong>,</h1>  <br />" +
                                            "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                            "               Welcome to your patient portal services provided by AMR Patient Portal. " + FacilityName + " has activated your AMR Patient Portal account for you to access your health information. Below you will find your AccessID Code to log in to your portal account. <br /><br />" +
                                            "               Your AccessID Code:   <strong>" + sPatient.AMRPatientId + "</strong> <br /><br />" +
                                            "               You will also be receiving an email with your temporary password, which you will need to change once you log in. " +
                                            "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account.<br /><br /> " +
                                            "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
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

                                    objEmail.SendEmailHTML(sPatient.EMail, "AMR Patient Portal - AccessID Code", WelcomeMsg);

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
                                                        "          <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" align=\"center\">" +
                                                        "			<tr id=\"backgroundTable\" width=\"571\" style=\"width:571px;display:block;background-color:#fff;\">" +
                                                        "			  <td valign=\"top\" style=\"display:block;color:#000;padding:21px;font-size:14px;line-height:1.5em;float:left;margin:0 auto;\" width=\"532\">" +
                                                        "               <h1 style=\"color:#005494;font-family: 'Lato', Arial, sans-serif; font-size:18px;font-weight:400;\">Dear <strong>" + sPatient.FirstName + " " + sPatient.LastName + "</strong>,</h1>  <br />" +
                                                        "               <span style=\"color: rgb(0, 84, 148); font-family: 'Lato Bold', Arial, sans-serif; font-size: 16px;\">" +
                                                        "               Welcome again to AMR Patient Portal. Below you will find your temporary password.<br /><br />" +
                                                        "               Your Temporary Password:   <strong>" + passclear + "</strong> <br /><br />" +
                                                        "               Using the AccessID Code provided in the previous email, " +
                                                        "               <a href=\"http://" + host + "\" style=\"color: rgb(0, 84, 148);\">Click here</a> to log in to your AMR Patient Portal account and reset your temporary password.<br /><br /> " +
                                                        "               Should you require further assistance, do not hesitate to email us at support@accessmyrecords.com.<br /><br />" +
                                                        "               Thank you, <br />" +
                                                        "               Your Member Services Team<br /><br />" +
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

                                    objEmail.SendEmailHTML(sPatient.EMail, "AMR Patient Portal - Temporary Password", WelcomeMsg);


                                }
                                catch (Exception ex)
                                {

                                }

                            }
                        }
                    }
                    // Set the PatientId to return
                    PatientId = sPatient.AMRPatientId;
                    #endregion

                    #region check for visit
                    if (Valid)
                    {
                        // If this is an unstructured document, make sure that there is a Zero visit.
                        if (DocumentType == "Unstructured")
                        {
                            sVisit.VisitId = 0;

                            PatientVisit VisitResults = db.PatientVisits.FirstOrDefault
                                                    (p => p.PatientId == sPatient.AMRPatientId
                                                    && p.FacilityId == sFacility.FacilityId
                                                    && p.VisitId == sVisit.VisitId);

                            if (VisitResults == null)
                            {
                                sVisit.Date = ServiceDate;
                                var Visit = new PatientVisit()
                                {
                                    PatientId = sPatient.AMRPatientId,
                                    FacilityId = sFacility.FacilityId,
                                    VisitId = sVisit.VisitId,
                                    VisitDate = Convert.ToDateTime(sVisit.Date.Substring(4, 2) + "/" + sVisit.Date.Substring(6, 2) + "/" + sVisit.Date.Substring(0, 4)),
                                    ProviderId = ProviderId,
                                    VisitReason = sVisit.Reason,
                                    Viewable = false,
                                    DateCreated = System.DateTime.Now,
                                    ClinicalSummary = "Portal",
                                };
                                db.PatientVisits.Add(Visit);
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            #region Try to find visit information

                            XmlNode nodeComponentOf = xd.DocumentElement["componentOf"];
                            if (nodeComponentOf != null)
                            {
                                // Try to get the visit Information
                                XmlNode nodeEncompassingEncounter = nodeComponentOf["encompassingEncounter"];
                                if (nodeEncompassingEncounter != null)
                                {
                                    try
                                    {
                                        XmlNode nodeId = nodeEncompassingEncounter["id"];
                                        if (nodeId != null)
                                            sVisit.VisitId = Convert.ToInt64(nodeId.Attributes.GetNamedItem("extension").Value);
                                        XmlNode nodeEffectiveTime = nodeEncompassingEncounter["effectiveTime"];
                                        XmlNode nodeLow = nodeEffectiveTime["low"];
                                        sVisit.Date = nodeLow.Attributes.GetNamedItem("value").Value;
                                    }
                                    catch { }
                                }


                                // Make sure that this visit has not already been imported


                                PatientVisit VisitResults = db.PatientVisits.FirstOrDefault
                                                    (p => p.PatientId == sPatient.AMRPatientId
                                                    && p.FacilityId == sFacility.FacilityId
                                                    && p.VisitId == sVisit.VisitId);

                                if (VisitResults != null)
                                {
                                    // Visit Already Exists - This is an error
                                    Valid = false;
                                    ErrorMsg = "This visit id has already been processed.";
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    if (Valid)
                    {
                        // CDA Body
                        XmlNode nodeComponent = xd.DocumentElement["component"];
                        XmlNode nodeStructure = nodeComponent["structuredBody"];

                        #region Visit Record
                        if (DocumentType != "Unstructured")
                        {
                            // Create Visit Record

                            try
                            {
                                // First Look For Reason For Visit
                                foreach (XmlNode node in nodeStructure)
                                {
                                    if (node.Name == "component")
                                    {
                                        try
                                        {
                                            XmlNode nodeSections = node["section"];
                                            XmlNode nodeTemplateId = nodeSections["templateId"];

                                            string template = nodeTemplateId.Attributes.GetNamedItem("root").Value;
                                            if (template == "2.16.840.1.113883.10.20.22.2.12")  // Reason For Visit Section
                                            {
                                                XmlNode nodeText = nodeSections["text"];
                                                XmlNode nodeParagraph = nodeText["paragraph"];
                                                if (nodeParagraph != null)
                                                    sVisit.Reason = nodeParagraph.InnerText;
                                                else
                                                    sVisit.Reason = nodeText.InnerText;

                                                XmlNode nodeComponentOf2 = xd.DocumentElement["componentOf"];
                                                XmlNode nodeEncompassingEncounter = nodeComponentOf2["encompassingEncounter"];
                                                XmlNode nodeEffectiveTime = nodeEncompassingEncounter["effectiveTime"];
                                                XmlNode nodeLow = nodeEffectiveTime["low"];
                                                sVisit.Date = nodeLow.Attributes.GetNamedItem("value").Value;
                                                break;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                }
                                if (sVisit.Date == "")
                                {
                                    //  Next Try to Find Encounders Setion 
                                    foreach (XmlNode node in nodeStructure)
                                    {
                                        if (node.Name == "component")
                                        {
                                            try
                                            {
                                                XmlNode nodeSections = node["section"];
                                                XmlNode nodeTemplateId = nodeSections["templateId"];

                                                string template = nodeTemplateId.Attributes.GetNamedItem("root").Value;
                                                if (template == "2.16.840.1.113883.10.20.1.3" || template == "2.16.840.1.113883.10.20.22.2.22" || template == "2.16.840.1.113883.10.20.22.2.22.1")  // Encounters Section
                                                {
                                                    XmlNode nodeEntry = nodeSections["entry"];
                                                    XmlNode nodeEncounter = nodeEntry["encounter"];
                                                    XmlNode nodeCode = nodeEncounter["code"];
                                                    if (sVisit.Reason == "")
                                                        sVisit.Reason = nodeCode["originalText"].InnerText;
                                                    XmlNode nodeEffectiveTime = nodeEncounter["effectiveTime"];
                                                    try
                                                    {
                                                        sVisit.Date = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                    }
                                                    catch
                                                    {
                                                        XmlNode nodeLow = nodeEffectiveTime["low"];
                                                        if (nodeLow != null)
                                                            sVisit.Date = nodeLow.Attributes.GetNamedItem("value").Value;
                                                    }
                                                    break;
                                                }
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                                // No Encounter Section - This is a consolidated CCD.
                                if (sVisit.Date == "")
                                {
                                    XmlNode nodeEffectiveTime = xd.DocumentElement["effectiveTime"];
                                    sVisit.Date = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                    sVisit.Reason = "Summarization of episode";
                                }

                                if (sVisit.VisitId == 0)
                                {
                                    // Get the next available visit id for the patient at the facility
                                    //PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == sPatient.AMRPatientId && p.sFacility.FacilityId == sFacility.FacilityId);

                                    PatientVisit results = db.PatientVisits.OrderByDescending(i => i.VisitId).FirstOrDefault(p => p.PatientId == sPatient.AMRPatientId && p.FacilityId == sFacility.FacilityId);

                                    if (results != null)
                                    {
                                        sVisit.VisitId = results.VisitId;
                                    }
                                    sVisit.VisitId++;  // Increment to next visit id.
                                }
                                if (sVisit.Date == "")
                                {
                                    sVisit.Date = System.DateTime.Now.ToString("yyyymmdd");
                                }
                                if (sVisit.Reason.Length > 256) sVisit.Reason = sVisit.Reason.Substring(0, 256);

                                var Visit = new PatientVisit()
                                {
                                    PatientId = sPatient.AMRPatientId,
                                    FacilityId = sFacility.FacilityId,
                                    VisitId = sVisit.VisitId,
                                    VisitDate = Convert.ToDateTime(sVisit.Date.Substring(4, 2) + "/" + sVisit.Date.Substring(6, 2) + "/" + sVisit.Date.Substring(0, 4)),
                                    ProviderId = ProviderId,
                                    VisitReason = sVisit.Reason,
                                    Viewable = false,
                                    DateCreated = System.DateTime.Now,
                                    ClinicalSummary = "Portal",
                                };
                                db.PatientVisits.Add(Visit);
                                db.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Valid = false;
                                ErrorMsg = "Error creating visit";
                            }
                        }
                        #endregion

                        if (Valid)
                        {
                            if (DocumentType != "Unstructured")
                            {
                                // Loop through the CCD Body To Popluate Clinical Data
                                foreach (XmlNode node in nodeStructure)
                                {
                                    if (node.Name == "component")
                                    {
                                        //XmlNode nodeComponent2 = node;
                                        XmlNode nodeSections = node["section"];
                                        if (nodeSections != null)
                                        {
                                            XmlNode nodeTemplateId = nodeSections["templateId"];

                                            string template = nodeTemplateId.Attributes.GetNamedItem("root").Value;

                                            foreach (XmlNode nodeSection in nodeSections)
                                            {
                                                //if (nodeSection.Name == "templateId")
                                                //{
                                                //    //string template = nodes.OuterXml;
                                                //    template = nodeSection.Attributes.GetNamedItem("root").Value;
                                                //}
                                                if (nodeSection.Name == "code")
                                                {
                                                    string x = nodeSection.Attributes.GetNamedItem("code").Value;
                                                    string y = nodeSection.Attributes.GetNamedItem("codeSystem").Value;
                                                }
                                                if (nodeSection.Name == "entry")
                                                {
                                                    #region Problem
                                                    if (template == "2.16.840.1.113883.10.20.1.11" || template == "2.16.840.1.113883.10.20.22.2.5" || template == "2.16.840.1.113883.10.20.22.2.5.1")     // Problems 
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeAct = nodeSection["act"];
                                                            XmlNode nodeEntryRelationship = nodeAct["entryRelationship"];
                                                            XmlNode nodeObservation = nodeEntryRelationship["observation"];

                                                            // Get The Date
                                                            XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                            XmlNode nodeLow = nodeEffectiveTime["low"];
                                                            string ProblemDate = "";
                                                            if (nodeLow != null)
                                                            {
                                                                try
                                                                {
                                                                    ProblemDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                    if (ProblemDate.Length > 8) ProblemDate = ProblemDate.Substring(0, 8);
                                                                }
                                                                catch { }
                                                            }

                                                            // Value Node - Get Problem Code Data
                                                            XmlNode nodeValue = nodeObservation["value"];

                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;

                                                            try
                                                            {
                                                                try
                                                                {
                                                                    CodeValue = nodeValue.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeValue.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeValue.Attributes.GetNamedItem("displayName").Value;
                                                                }
                                                                catch
                                                                {
                                                                    XmlNode nodeTranslation = nodeValue["translation"];
                                                                    CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                // Data not codified - Use Text
                                                                XmlNode nodeText = nodeObservation["text"];
                                                                if (nodeText != null)
                                                                    Description = nodeText.InnerText;
                                                            }
                                                            if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                            // Status
                                                            Int16 ProblemStatusId = 0;
                                                            try
                                                            {
                                                                XmlNode nodeStatusCode = nodeSection["statusCode"];
                                                                if (nodeStatusCode != null)
                                                                    ProblemStatusId = CodeStatus(nodeStatusCode.Attributes.GetNamedItem("code").Value);
                                                                else
                                                                {
                                                                    nodeStatusCode = nodeAct["statusCode"];
                                                                    if (nodeStatusCode != null)
                                                                        ProblemStatusId = CodeStatus(nodeStatusCode.Attributes.GetNamedItem("code").Value);
                                                                }
                                                            }
                                                            catch
                                                            {
                                                                XmlNode nodeSEntryRelationship = nodeObservation["entryRelationship"];

                                                                if (nodeSEntryRelationship != null)
                                                                {
                                                                    XmlNode nodeSObservation = nodeSEntryRelationship["observation"];
                                                                    XmlNode nodeSValue = nodeSObservation["value"];
                                                                    try
                                                                    {
                                                                        ProblemStatusId = CodeStatus(nodeSValue.Attributes.GetNamedItem("displayName").Value);
                                                                    }
                                                                    catch { }
                                                                }
                                                            }

                                                            // Write Problem Record

                                                            var Problem = new PatientProblem()
                                                            {
                                                                PatientId = sPatient.AMRPatientId,
                                                                FacilityId = sFacility.FacilityId,
                                                                VisitId = sVisit.VisitId,
                                                                PatientProblemCntr = 0,
                                                                CodeValue = CodeValue,
                                                                CodeSystemId = CodeSystemId,
                                                                Condition = Description,
                                                                EffectiveDate = ProblemDate,
                                                                ConditionStatusId = ProblemStatusId,
                                                                Note = "",
                                                                UserId_Created = UserId,
                                                                DateCreated = System.DateTime.Now,
                                                                UserId_Modified = UserId,
                                                                DateModified = System.DateTime.Now,
                                                                Deleted = false,
                                                            };
                                                            db.PatientProblems.Add(Problem);
                                                            db.SaveChanges();
                                                        }
                                                        catch
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error processing problem " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Family History
                                                    if (template == "2.16.840.1.113883.10.20.1.4" || template == "2.16.840.1.113883.10.20.22.2.15")     // Family History
                                                    {
                                                        string Relative = "";
                                                        try
                                                        {
                                                            XmlNode nodeOrganizer = nodeSection["organizer"];
                                                            XmlNode nodeSubject = nodeOrganizer["subject"];
                                                            XmlNode nodeRelatedSubject = nodeSubject["relatedSubject"];
                                                            XmlNode nodeCode = nodeRelatedSubject["code"];
                                                            Relative = nodeCode.Attributes.GetNamedItem("code").Value;
                                                            // Translate the SNOMED Code to RelatinshipId
                                                            int RelativeId = GetRelationshipSNOMED(Relative);

                                                            if (RelativeId == 0)
                                                            {
                                                                XmlNode nodeTranslation = nodeRelatedSubject["translation"];
                                                                if (nodeTranslation != null)
                                                                {
                                                                    Relative = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                    RelativeId = GetRelationshipSNOMED(Relative);
                                                                }
                                                            }

                                                            string BirthYear = "1/1/1900";
                                                            try
                                                            {
                                                                XmlNode nodeSubject2 = nodeRelatedSubject["subject"];
                                                                XmlNode nodeBirthTime = nodeSubject2["birthTime"];
                                                                BirthYear = nodeBirthTime.Attributes.GetNamedItem("value").Value;
                                                            }
                                                            catch
                                                            {
                                                            }

                                                            bool Diseased = false;
                                                            int Age = 0;

                                                            // Get a list of the observations
                                                            // Loop through components   - Each item for the family member
                                                            foreach (XmlNode CNode in nodeOrganizer)
                                                            {
                                                                if (CNode.Name == "component")
                                                                {
                                                                    XmlNode nodeObservation = CNode["observation"];
                                                                    // Code
                                                                    XmlNode nodeValue = nodeObservation["value"];
                                                                    string CodeValue = "";
                                                                    try
                                                                    {
                                                                        CodeValue = nodeValue.Attributes.GetNamedItem("code").Value;
                                                                    }
                                                                    catch { }
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    int CodeSystemId = CodeSystemValue(nodeValue.Attributes.GetNamedItem("codeSystem").Value);
                                                                    string Description = nodeValue.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);


                                                                    // Get Age Observation
                                                                    XmlNode nodeOTemplate = nodeObservation["templateId"];
                                                                    string oTemplate = nodeOTemplate.Attributes.GetNamedItem("root").Value;
                                                                    if (oTemplate == "2.16.840.1.113883.10.20.1.42") //Family history cause of death observation template
                                                                    {
                                                                        Diseased = true;
                                                                        foreach (XmlNode RNode in nodeObservation)
                                                                        {
                                                                            if (RNode.Name == "entryRelationship")
                                                                            {
                                                                                if (RNode.Attributes.GetNamedItem("typeCode").Value == "CAUS")
                                                                                {
                                                                                    // Skip Over this - Value Is Dead
                                                                                }
                                                                                else
                                                                                {
                                                                                    // Get the Age At Onset
                                                                                    XmlNode nodeAObservation = RNode["observation"];
                                                                                    XmlNode nodeAValue = nodeAObservation["value"];
                                                                                    try { Age = Convert.ToInt32(nodeAValue.Attributes.GetNamedItem("value").Value); }
                                                                                    catch { Age = 0; }
                                                                                }
                                                                            }
                                                                            else if (RNode.Name == "effectiveTime")
                                                                            {
                                                                                XmlNode nodeLow = RNode["low"];
                                                                                if (BirthYear == "1/1/1900")
                                                                                    Age = 0;
                                                                                else
                                                                                {
                                                                                    try { Age = Convert.ToInt32(nodeLow.Attributes.GetNamedItem("value").Value) - Convert.ToInt32(BirthYear); }
                                                                                    catch { Age = 0; }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        // Get the Age At Onset
                                                                        XmlNode nodeEntryRelationship = nodeObservation["entryRelationship"];
                                                                        if (nodeEntryRelationship != null)
                                                                        {
                                                                            XmlNode nodeAObservation = nodeEntryRelationship["observation"];
                                                                            XmlNode nodeAValue = nodeAObservation["value"];
                                                                            try { Age = Convert.ToInt32(nodeAValue.Attributes.GetNamedItem("value").Value); }
                                                                            catch { Age = 0; }
                                                                        }
                                                                        else
                                                                        {
                                                                            XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                                            if (nodeEffectiveTime != null)
                                                                            {
                                                                                XmlNode nodeLow = nodeEffectiveTime["low"];
                                                                                if (BirthYear == "1/1/1900")
                                                                                    Age = 0;
                                                                                else
                                                                                {
                                                                                    try { Age = Convert.ToInt32(nodeLow.Attributes.GetNamedItem("value").Value) - Convert.ToInt32(BirthYear); }
                                                                                    catch { Age = 0; }
                                                                                }
                                                                            }
                                                                        }
                                                                    }

                                                                    // Write Family History Record
                                                                    var Family = new PatientFamilyHist()
                                                                    {
                                                                        PatientId = sPatient.AMRPatientId,
                                                                        FacilityId = sFacility.FacilityId,
                                                                        VisitId = sVisit.VisitId,
                                                                        PatFamilyHistCntr = 0,
                                                                        RelationshipId = RelativeId,
                                                                        Description = Description,
                                                                        Qualifier = "",
                                                                        CodeValue = CodeValue,
                                                                        CodeSystemId = CodeSystemId,
                                                                        ConditionStatusId = 1,
                                                                        AgeAtOnset = Age,
                                                                        Diseased = Diseased,
                                                                        DiseasedAge = 0,
                                                                        Note = "",
                                                                        UserId_Created = UserId,
                                                                        DateCreated = System.DateTime.Now,
                                                                        UserId_Modified = UserId,
                                                                        DateModified = System.DateTime.Now,
                                                                        Deleted = false,
                                                                    };
                                                                    db.PatientFamilyHists.Add(Family);
                                                                    db.SaveChanges();
                                                                }
                                                            }
                                                        }
                                                        catch
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error family history " + Relative;
                                                            Valid = false;
                                                        }


                                                    }
                                                    #endregion

                                                    #region Social History
                                                    if (template == "2.16.840.1.113883.10.20.1.15" || template == "2.16.840.1.113883.10.20.22.2.17")     // Social History
                                                    {
                                                        string Description = "";
                                                        try
                                                        {

                                                            XmlNode nodeObservation = nodeSection["observation"];

                                                            // Get The Date
                                                            XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                            string BeginDate = "";
                                                            string EndDate = "";
                                                            if (nodeEffectiveTime != null)
                                                            {
                                                                XmlNode nodeLow = nodeEffectiveTime["low"];
                                                                if (nodeLow != null)
                                                                {
                                                                    try
                                                                    {
                                                                        BeginDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                        if (BeginDate.Length > 8) BeginDate = BeginDate.Substring(0, 8);
                                                                    }
                                                                    catch
                                                                    { }
                                                                }

                                                                XmlNode nodeHigh = nodeEffectiveTime["high"];
                                                                if (nodeHigh != null)
                                                                {
                                                                    try
                                                                    {
                                                                        EndDate = nodeHigh.Attributes.GetNamedItem("value").Value;
                                                                        if (EndDate.Length > 8) EndDate = EndDate.Substring(0, 8);
                                                                    }
                                                                    catch
                                                                    { }
                                                                }
                                                            }
                                                            // Code Node - Get Element Code Data
                                                            XmlNode nodeCode = nodeObservation["code"];
                                                            XmlNode nodeTranslation = nodeCode["translation"];
                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;

                                                            try
                                                            {
                                                                CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                            }
                                                            catch
                                                            {
                                                                CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                            }

                                                            try
                                                            {
                                                                // Used to trap a social history section which is required but contains no data
                                                                if (CodeValue != "ASSERTION")
                                                                {
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    try
                                                                    {
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                        Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                        if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                    }
                                                                    catch
                                                                    {
                                                                        CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                        Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                        if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    XmlNode nodeValue = nodeObservation["value"];
                                                                    if (nodeValue != null)
                                                                    {
                                                                        CodeValue = nodeValue.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeValue.Attributes.GetNamedItem("codeSystem").Value);
                                                                        Description = nodeValue.Attributes.GetNamedItem("displayName").Value;
                                                                        if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                    }
                                                                }
                                                                string Qualifier = nodeObservation["value"].InnerText;
                                                                if (Qualifier.Length > 20) Qualifier = Qualifier.Substring(0, 20);

                                                                // Write Social Hist  Record
                                                                var Social = new PatientSocialHist()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatSocialHistCntr = 0,
                                                                    Description = Description,
                                                                    Qualifier = Qualifier,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    BeginDate = BeginDate,
                                                                    EndDate = EndDate,
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientSocialHists.Add(Social);

                                                                db.SaveChanges();
                                                            }
                                                            catch
                                                            {
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error social history " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Allerts - Allergies
                                                    if (template == "2.16.840.1.113883.10.20.1.2" || template == "2.16.840.1.113883.10.20.22.2.6" || template == "2.16.840.1.113883.10.20.22.2.6.1")     // Alerts  
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeAct = nodeSection["act"];
                                                            XmlNode nodeEntryRelationship = nodeAct["entryRelationship"];
                                                            XmlNode nodeObservation = nodeEntryRelationship["observation"];
                                                            XmlNode nodeValue = nodeObservation["value"];

                                                            if (nodeValue.Attributes.GetNamedItem("code").Value == "282100009" // Allergy To substance
                                                                || nodeValue.Attributes.GetNamedItem("code").Value == "416098002" // Adverse Reaction (Allergy), Drug Allergy
                                                                || nodeValue.Attributes.GetNamedItem("code").Value == "419511003" // Adverse Reaction (Allergy), Drug Allergy
                                                                || nodeValue.Attributes.GetNamedItem("code").Value == "414285001" // Food Allergy
                                                                || nodeValue.Attributes.GetNamedItem("code").Value == "419199007" // Allergy to substance
                                                                || nodeValue.Attributes.GetNamedItem("code").Value == "420134006") // Propensity to adverse reactions
                                                            {
                                                                string CodeValue = "";
                                                                int CodeSystemId = 0;

                                                                Int16 StatusCode = 0;

                                                                try
                                                                {
                                                                    XmlNode nodeStatusCode = nodeAct["statusCode"];
                                                                    StatusCode = CodeStatus(nodeStatusCode.Attributes.GetNamedItem("code").Value);
                                                                }
                                                                catch { }


                                                                XmlNode nodeParticipant = nodeObservation["participant"];
                                                                if (nodeParticipant != null)
                                                                {
                                                                    XmlNode nodeParticipantRole = nodeParticipant["participantRole"];
                                                                    XmlNode nodePlayingEntity = nodeParticipantRole["playingEntity"];
                                                                    XmlNode nodeCode = nodePlayingEntity["code"];


                                                                    try
                                                                    {
                                                                        CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);

                                                                    }
                                                                    catch
                                                                    {
                                                                        // Not using RxNorm
                                                                        try
                                                                        {
                                                                            XmlNode nodeTranslation = nodeCode["translation"];
                                                                            CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                            if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                            CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                        }
                                                                        catch { }
                                                                    }
                                                                    try
                                                                    {
                                                                        Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    }
                                                                    catch
                                                                    {
                                                                        XmlNode nodeName = nodePlayingEntity["name"];
                                                                        if (nodeName != null)
                                                                            Description = nodeName.InnerText;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    XmlNode nodeValue2 = nodeObservation["value"];
                                                                    CodeValue = nodeValue2.Attributes.GetNamedItem("code").Value;
                                                                    CodeSystemId = CodeSystemValue(nodeValue2.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeValue2.Attributes.GetNamedItem("displayName").Value;
                                                                }

                                                                if (Description.Length > 50) Description = Description.Substring(0, 50);

                                                                string AllergenType = "";
                                                                if (nodeValue.Attributes.GetNamedItem("code").Value == "282100009")
                                                                    AllergenType = "Substance";
                                                                else if (nodeValue.Attributes.GetNamedItem("code").Value == "416098002")
                                                                    AllergenType = "Drug";
                                                                else if (nodeValue.Attributes.GetNamedItem("code").Value == "419511003")
                                                                    AllergenType = "Drug";
                                                                else if (nodeValue.Attributes.GetNamedItem("code").Value == "414285001")
                                                                    AllergenType = "Food";
                                                                else if (nodeValue.Attributes.GetNamedItem("code").Value == "419199007")
                                                                    AllergenType = "Substance";
                                                                else if (nodeValue.Attributes.GetNamedItem("code").Value == "420134006")
                                                                    AllergenType = "Undefined";

                                                                string CodeValue2 = "";
                                                                int CodeSystemId2 = 0;
                                                                string Description2 = "";


                                                                foreach (XmlNode nodeEntryRelationship2 in nodeObservation)
                                                                {
                                                                    if (nodeEntryRelationship2.Name == "entryRelationship")
                                                                    {
                                                                        XmlNode nodeObservation2 = nodeEntryRelationship2["observation"];
                                                                        XmlNode nodeTemplateId2 = nodeObservation2["templateId"];
                                                                        string TemplateType = nodeTemplateId2.Attributes.GetNamedItem("root").Value.ToString();
                                                                        if (TemplateType == "2.16.840.1.113883.10.20.22.4.28")
                                                                        {
                                                                            // Allergy Status
                                                                            try
                                                                            {
                                                                                XmlNode nodeValue2 = nodeObservation2["value"];
                                                                                StatusCode = CodeStatus(nodeValue2.Attributes.GetNamedItem("code").Value);
                                                                            }
                                                                            catch { }
                                                                        }
                                                                        else if (TemplateType == "2.16.840.1.113883.10.20.22.4.9")
                                                                        {
                                                                            // Reaction
                                                                            try
                                                                            {
                                                                                XmlNode nodeValue2 = nodeObservation2["value"];
                                                                                CodeValue2 = nodeValue2.Attributes.GetNamedItem("code").Value;
                                                                                if (CodeValue2.Length > 20) CodeValue2 = CodeValue2.Substring(0, 20);
                                                                                CodeSystemId2 = CodeSystemValue(nodeValue2.Attributes.GetNamedItem("codeSystem").Value);
                                                                                Description2 = nodeValue2.Attributes.GetNamedItem("displayName").Value;
                                                                                if (Description2.Length > 50) Description2 = Description2.Substring(0, 50);
                                                                            }
                                                                            catch { }
                                                                        }
                                                                        else if (TemplateType == "2.16.840.1.113883.10.20.22.4.8")
                                                                        {
                                                                            // Severity

                                                                        }
                                                                    }
                                                                }

                                                                // Write Allergy  Record
                                                                var Allergy = new PatientAllergy()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatientAllergyCntr = 0,
                                                                    CodeValue_Allergen = CodeValue,
                                                                    CodeSystemId_Allergen = CodeSystemId,
                                                                    Allergen = Description,
                                                                    AllergenType = AllergenType,
                                                                    CodeValue_Reaction = CodeValue2,
                                                                    CodeSystemId_Reaction = CodeSystemId2,
                                                                    Reaction = Description2,
                                                                    EffectiveDate = "",
                                                                    ConditionStatusId = StatusCode,
                                                                    Note = "",
                                                                    OnCard = false,
                                                                    OnKeys = false,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientAllergies.Add(Allergy);


                                                                db.SaveChanges();
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error allergies " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Medications Administered  During Visit // SJF Added Section 5/8/14
                                                    if (template == "2.16.840.1.113883.10.20.22.2.38" ||     // Medications Administered
                                                        template == "2.16.840.1.113883.10.20.22.1.1")  // Medications Section 
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeSubstanceAdministration = nodeSection["substanceAdministration"];
                                                            string Route = "";
                                                            XmlNode nodeRouteCode = nodeSubstanceAdministration["routeCode"];
                                                            if (nodeRouteCode != null)
                                                            {
                                                                try
                                                                {
                                                                    Route = nodeRouteCode.Attributes.GetNamedItem("code").Value;
                                                                }
                                                                catch { }
                                                            }

                                                            XmlNode nodeDoseQuantity = nodeSubstanceAdministration["doseQuantity"];
                                                            int Dose = 0;
                                                            string DoseUnit = "";
                                                            if (nodeDoseQuantity != null)
                                                            {
                                                                try
                                                                {
                                                                    Dose = Convert.ToInt32(nodeDoseQuantity.Attributes.GetNamedItem("value").Value);
                                                                    DoseUnit = nodeDoseQuantity.Attributes.GetNamedItem("unit").Value;
                                                                }
                                                                catch { }
                                                            }
                                                            string MedStatus = "";

                                                            try
                                                            {
                                                                XmlNode nodeStatusCode = nodeSubstanceAdministration["statusCode"];
                                                                MedStatus = nodeStatusCode.Attributes.GetNamedItem("code").Value;
                                                            }
                                                            catch { }


                                                            XmlNode nodeConsumable = nodeSubstanceAdministration["consumable"];
                                                            XmlNode nodeManufacturedProduct = nodeConsumable["manufacturedProduct"];
                                                            XmlNode nodeManufacturedMaterial = nodeManufacturedProduct["manufacturedMaterial"];
                                                            XmlNode nodeCode = nodeManufacturedMaterial["code"];

                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;


                                                            try  // Added to trap for medication section that does not have any data
                                                            {
                                                                XmlNode nodeCk = nodeCode["translation"];
                                                                if (nodeCk == null)
                                                                {
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                }
                                                                else
                                                                {
                                                                    foreach (XmlNode nodeTranslation in nodeCode)
                                                                    {
                                                                        if (nodeTranslation.Name == "translation")
                                                                        {
                                                                            if (nodeTranslation.Attributes.GetNamedItem("codeSystem").Value == "2.16.840.1.113883.6.88")  // Pull RxNorm
                                                                            {
                                                                                CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                                if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                                CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                                Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                                if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                string Frequency = "";
                                                                string StartDate = "";
                                                                string ExpireDate = "1/1/1900";
                                                                foreach (XmlNode nodeEffectiveTime in nodeSubstanceAdministration)
                                                                {
                                                                    if (nodeEffectiveTime.Name == "effectiveTime")
                                                                    {
                                                                        if (nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value != null && nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value == "IVL_TS")
                                                                        {
                                                                            XmlNode nodeHigh = nodeEffectiveTime["high"];
                                                                            if (nodeHigh != null)
                                                                            {
                                                                                try
                                                                                {
                                                                                    ExpireDate = nodeHigh.Attributes.GetNamedItem("value").Value;
                                                                                    ExpireDate = ExpireDate.Substring(4, 2) + "/" + ExpireDate.Substring(6, 2) + "/" + ExpireDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                            XmlNode nodeLow = nodeEffectiveTime["low"];
                                                                            if (nodeLow != null)
                                                                            {
                                                                                try
                                                                                {
                                                                                    StartDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                                    StartDate = StartDate.Substring(4, 2) + "/" + StartDate.Substring(6, 2) + "/" + StartDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                            if (StartDate == "")
                                                                            {
                                                                                try
                                                                                {
                                                                                    StartDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                                    StartDate = StartDate.Substring(4, 2) + "/" + StartDate.Substring(6, 2) + "/" + StartDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                        }
                                                                        else if (nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value != null && nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value == "PIVL_TS")
                                                                        {
                                                                            XmlNode nodePeriod = nodeEffectiveTime["period"];
                                                                            Frequency = nodePeriod.Attributes.GetNamedItem("value").Value + nodePeriod.Attributes.GetNamedItem("unit").Value;
                                                                        }

                                                                    }
                                                                }

                                                                // Write Medication  Record
                                                                var Medication = new PatientMedication()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatientMedicationCntr = 0,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    MedicationName = Description,
                                                                    Active = true,
                                                                    Quantity = 0,
                                                                    RouteId = Route,
                                                                    Refills = 0,
                                                                    Frequency = Frequency,
                                                                    Dose = Dose,
                                                                    DoseUnit = DoseUnit,
                                                                    Sig = "",
                                                                    Diagnosis = "",
                                                                    StartDate = Convert.ToDateTime(StartDate),
                                                                    ExpireDate = Convert.ToDateTime(ExpireDate),
                                                                    Pharmacy = "",
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                    Status = MedStatus,
                                                                    DuringVisit = true,
                                                                };
                                                                db.PatientMedications.Add(Medication);

                                                                db.SaveChanges();
                                                            }
                                                            catch
                                                            {
                                                                // No data in this section.
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error medications administered " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Medications
                                                    if (template == "2.16.840.1.113883.10.20.1.8" || template == "2.16.840.1.113883.10.20.22.2.1" || template == "2.16.840.1.113883.10.20.22.2.1.1" ||    // Medications   
                                                        template == "2.16.840.1.113883.10.20.22.4.16") // Medication Activity

                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeSubstanceAdministration = nodeSection["substanceAdministration"];
                                                            string Route = "";
                                                            XmlNode nodeRouteCode = nodeSubstanceAdministration["routeCode"];
                                                            if (nodeRouteCode != null)
                                                            {
                                                                try
                                                                {
                                                                    Route = nodeRouteCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Route.Length > 20)
                                                                        Route = Route.Substring(0, 20);
                                                                }
                                                                catch { }
                                                            }

                                                            XmlNode nodeDoseQuantity = nodeSubstanceAdministration["doseQuantity"];
                                                            int Dose = 0;
                                                            string DoseUnit = "";
                                                            if (nodeDoseQuantity != null)
                                                            {
                                                                try
                                                                {
                                                                    Dose = Convert.ToInt32(nodeDoseQuantity.Attributes.GetNamedItem("value").Value);
                                                                    DoseUnit = nodeDoseQuantity.Attributes.GetNamedItem("unit").Value;
                                                                }
                                                                catch { }
                                                            }
                                                            string MedStatus = "";

                                                            try
                                                            {
                                                                XmlNode nodeStatusCode = nodeSubstanceAdministration["statusCode"];
                                                                MedStatus = nodeStatusCode.Attributes.GetNamedItem("code").Value;
                                                            }
                                                            catch { }

                                                            XmlNode nodeConsumable = nodeSubstanceAdministration["consumable"];
                                                            XmlNode nodeManufacturedProduct = nodeConsumable["manufacturedProduct"];
                                                            XmlNode nodeManufacturedMaterial = nodeManufacturedProduct["manufacturedMaterial"];
                                                            XmlNode nodeCode = nodeManufacturedMaterial["code"];

                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;


                                                            try  // Added to trap for medication section that does not have any data
                                                            {
                                                                XmlNode nodeCk = nodeCode["translation"];
                                                                if (nodeCk == null)
                                                                {
                                                                    try
                                                                    {
                                                                        CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    }
                                                                    catch { }
                                                                    try
                                                                    {
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch { }
                                                                    Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                }
                                                                else
                                                                {
                                                                    foreach (XmlNode nodeTranslation in nodeCode)
                                                                    {
                                                                        if (nodeTranslation.Name == "translation")
                                                                        {
                                                                            if (nodeTranslation.Attributes.GetNamedItem("codeSystem").Value == "2.16.840.1.113883.6.88")  // Pull RxNorm
                                                                            {
                                                                                try
                                                                                {
                                                                                    CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                                }
                                                                                catch { }
                                                                                try
                                                                                {
                                                                                    CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                                }
                                                                                catch { }
                                                                                Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                                if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                                break;
                                                                            }
                                                                        }
                                                                    }
                                                                }

                                                                string Frequency = "";
                                                                string StartDate = "1/1/1900";
                                                                string ExpireDate = "1/1/1900";
                                                                foreach (XmlNode nodeEffectiveTime in nodeSubstanceAdministration)
                                                                {
                                                                    if (nodeEffectiveTime.Name == "effectiveTime")
                                                                    {
                                                                        if (nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value != null && nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value == "IVL_TS")
                                                                        {
                                                                            XmlNode nodeHigh = nodeEffectiveTime["high"];
                                                                            if (nodeHigh != null)
                                                                            {
                                                                                try
                                                                                {
                                                                                    ExpireDate = nodeHigh.Attributes.GetNamedItem("value").Value;
                                                                                    ExpireDate = ExpireDate.Substring(4, 2) + "/" + ExpireDate.Substring(6, 2) + "/" + ExpireDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                            XmlNode nodeLow = nodeEffectiveTime["low"];
                                                                            if (nodeLow != null)
                                                                            {
                                                                                try
                                                                                {
                                                                                    StartDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                                    StartDate = StartDate.Substring(4, 2) + "/" + StartDate.Substring(6, 2) + "/" + StartDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                            if (StartDate == "")
                                                                            {
                                                                                try
                                                                                {
                                                                                    StartDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                                    StartDate = StartDate.Substring(4, 2) + "/" + StartDate.Substring(6, 2) + "/" + StartDate.Substring(0, 4);
                                                                                }
                                                                                catch { }
                                                                            }
                                                                        }
                                                                        else if (nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value != null && nodeEffectiveTime.Attributes.GetNamedItem("xsi:type").Value == "PIVL_TS")
                                                                        {
                                                                            XmlNode nodePeriod = nodeEffectiveTime["period"];
                                                                            try
                                                                            {
                                                                                Frequency = nodePeriod.Attributes.GetNamedItem("value").Value + nodePeriod.Attributes.GetNamedItem("unit").Value;
                                                                            }
                                                                            catch { }
                                                                        }

                                                                    }
                                                                }

                                                                // Write Medication  Record
                                                                var Medication = new PatientMedication()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatientMedicationCntr = 0,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    MedicationName = Description,
                                                                    Active = true,
                                                                    Quantity = 0,
                                                                    RouteId = Route,
                                                                    Refills = 0,
                                                                    Frequency = Frequency,
                                                                    Dose = Dose,
                                                                    DoseUnit = DoseUnit,
                                                                    Sig = "",
                                                                    Diagnosis = "",
                                                                    StartDate = Convert.ToDateTime(StartDate),
                                                                    ExpireDate = Convert.ToDateTime(ExpireDate),
                                                                    Pharmacy = "",
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                    Status = MedStatus,
                                                                    DuringVisit = false,
                                                                };
                                                                db.PatientMedications.Add(Medication);

                                                                db.SaveChanges();
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                // No data in this section.
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error medications " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Immunization
                                                    if (template == "2.16.840.1.113883.10.20.1.6" || template == "2.16.840.1.113883.10.20.22.2.2" || template == "2.16.840.1.113883.10.20.22.2.2.1")     // Immunization   
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeSubstanceAdministration = nodeSection["substanceAdministration"];
                                                            XmlNode nodeEffectiveTime = nodeSubstanceAdministration["effectiveTime"];
                                                            XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                            string ImmDate = "";
                                                            if (nodeCenter != null)
                                                                ImmDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                            else
                                                            {
                                                                try
                                                                {
                                                                    ImmDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                catch { }
                                                            }

                                                            XmlNode nodeRouteCode = nodeSubstanceAdministration["routeCode"];
                                                            string Route = "";
                                                            if (nodeRouteCode != null)
                                                            {
                                                                try
                                                                {
                                                                    Route = nodeRouteCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Route.Length > 20)
                                                                        Route = Route.Substring(0, 20);
                                                                }
                                                                catch { }
                                                            }
                                                            string Amount = "";
                                                            XmlNode nodeDoseQuantity = nodeSubstanceAdministration["doseQuantity"];
                                                            if (nodeDoseQuantity != null)
                                                            {
                                                                try
                                                                {
                                                                    Amount = nodeDoseQuantity.Attributes.GetNamedItem("value").Value;
                                                                    Amount = Amount + nodeDoseQuantity.Attributes.GetNamedItem("unit").Value;
                                                                    if (Amount.Length > 20)
                                                                        Amount = Amount.Substring(0, 20);
                                                                }
                                                                catch { }
                                                            }

                                                            XmlNode nodeConsumable = nodeSubstanceAdministration["consumable"];
                                                            XmlNode nodeManufacturedProduct = nodeConsumable["manufacturedProduct"];
                                                            XmlNode nodeManufacturedMaterial = nodeManufacturedProduct["manufacturedMaterial"];
                                                            XmlNode nodeCode = nodeManufacturedMaterial["code"];

                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;
                                                            try
                                                            {
                                                                try
                                                                {
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                }
                                                                catch { }
                                                                try
                                                                {
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                }
                                                                catch { }
                                                                Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                if (Description.Length > 100) Description = Description.Substring(0, 100);

                                                                string LotNumber = "";
                                                                try
                                                                {
                                                                    XmlNode nodeLot = nodeManufacturedMaterial["LotNumberText"];
                                                                    if (nodeLot != null)
                                                                        LotNumber = nodeLot.InnerText;
                                                                }
                                                                catch { }

                                                                if (ImmDate.Length > 8) ImmDate = ImmDate.Substring(0, 8);

                                                                var Immunization = new PatientImmunization()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatientImmunizationCntr = 0,
                                                                    ImmunizationDate = ImmDate,
                                                                    ImmunizationTime = "0000",
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Vaccine = Description,
                                                                    Amount = Amount,
                                                                    Route = Route,
                                                                    Site = "",
                                                                    Sequence = "",
                                                                    ExpirationDate = Convert.ToDateTime("1/1/1900"),
                                                                    LotNumber = "",
                                                                    Manufacturer = "",
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientImmunizations.Add(Immunization);

                                                                db.SaveChanges();
                                                            }
                                                            catch
                                                            {
                                                                // Not a valid Immunization Record - Code left blank
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error immunizations " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Vital Signs
                                                    if (template == "2.16.840.1.113883.10.20.1.16" || template == "2.16.840.1.113883.10.20.22.2.4" || template == "2.16.840.1.113883.10.20.22.2.4.1")     // Vital Signs   
                                                    {
                                                        try
                                                        {
                                                            XmlNode nodeOrganizer = nodeSection["organizer"];
                                                            XmlNode nodeEffectiveTime = nodeOrganizer["effectiveTime"];
                                                            string VitalDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                            int Height = 0;
                                                            int Weight = 0;
                                                            int Systolic = 0;
                                                            int Diastolic = 0;

                                                            foreach (XmlNode nodeComponent2 in nodeOrganizer)
                                                            {
                                                                if (nodeComponent2.Name == "component")
                                                                {
                                                                    XmlNode nodeObservation = nodeComponent2["observation"];
                                                                    if (nodeObservation != null)
                                                                    {
                                                                        XmlNode nodeCode = nodeObservation["code"];
                                                                        XmlNode nodeValue = nodeObservation["value"];
                                                                        if (!nodeCode.OuterXml.Contains("code="))
                                                                        {
                                                                            // skip
                                                                        }
                                                                        else if (nodeCode.Attributes.GetNamedItem("code").Value == "50373000" || nodeCode.Attributes.GetNamedItem("code").Value == "8302-2") // Height
                                                                        {
                                                                            try
                                                                            {
                                                                                decimal value = Convert.ToDecimal(nodeValue.Attributes.GetNamedItem("value").Value);
                                                                                if (nodeValue.Attributes.GetNamedItem("unit").Value == "cm")
                                                                                    value = value * (decimal).393701;
                                                                                Height = Decimal.ToInt32(value);
                                                                            }
                                                                            catch { Height = 0; }
                                                                        }
                                                                        else if (nodeCode.Attributes.GetNamedItem("code").Value == "27113001" || nodeCode.Attributes.GetNamedItem("code").Value == "3141-9") // Weight
                                                                        {
                                                                            try
                                                                            {
                                                                                decimal value = Convert.ToDecimal(nodeValue.Attributes.GetNamedItem("value").Value);
                                                                                if (nodeValue.Attributes.GetNamedItem("unit").Value == "kg")
                                                                                    value = value * (decimal)2.20462;
                                                                                Weight = Decimal.ToInt32(value);
                                                                            }
                                                                            catch { Weight = 0; }
                                                                        }
                                                                        else if (nodeCode.Attributes.GetNamedItem("code").Value == "271649006" || nodeCode.Attributes.GetNamedItem("code").Value == "8480-6") // Systolic
                                                                        {
                                                                            try
                                                                            {
                                                                                Systolic = Convert.ToInt32(Convert.ToDecimal(nodeValue.Attributes.GetNamedItem("value").Value));
                                                                            }
                                                                            catch { Systolic = 0; }
                                                                        }
                                                                        else if (nodeCode.Attributes.GetNamedItem("code").Value == "271650006" || nodeCode.Attributes.GetNamedItem("code").Value == "8462-4") // Diastolic
                                                                        {
                                                                            try
                                                                            {
                                                                                Diastolic = Convert.ToInt32(Convert.ToDecimal(nodeValue.Attributes.GetNamedItem("value").Value));
                                                                            }
                                                                            catch { Diastolic = 0; }
                                                                        }
                                                                    }
                                                                }

                                                            }

                                                            if (Height > 0 || Weight > 0 || Systolic > 0 || Diastolic > 0)
                                                            {
                                                                if (VitalDate.Length == 8)
                                                                    VitalDate = VitalDate.Substring(4, 2) + "/" + VitalDate.Substring(6, 2) + "/" + VitalDate.Substring(0, 4);
                                                                else
                                                                    VitalDate = VitalDate.Substring(4, 2) + "/" + VitalDate.Substring(6, 2) + "/" + VitalDate.Substring(0, 4) + " " + VitalDate.Substring(8, 2) + ":" + VitalDate.Substring(10, 2);
                                                                var Vital = new PatientVitalSign()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatientVitalCntr = 0,
                                                                    VitalDate = Convert.ToDateTime(VitalDate),
                                                                    HeightIn = Height,
                                                                    WeightLb = Weight,
                                                                    BloodPressurePosn = "",
                                                                    Systolic = Systolic,
                                                                    Diastolic = Diastolic,
                                                                    Pulse = 0,
                                                                    Respiration = 0,
                                                                    Temperature = 0,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientVitalSigns.Add(Vital);

                                                                db.SaveChanges();
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error vitals ";
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Lab Results
                                                    if (template == "2.16.840.1.113883.10.20.1.14" || template == "2.16.840.1.113883.10.20.22.2.3" || template == "2.16.840.1.113883.10.20.22.2.3.1")     // Lab Results
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeOrganizer = nodeSection["organizer"];
                                                            XmlNode nodeEffectiveTime = nodeOrganizer["effectiveTime"];
                                                            string ReportDate = "19000101";
                                                            string OrderDate = "19000101";
                                                            string CollectionDate = "19000101";
                                                            if (nodeEffectiveTime != null)
                                                                ReportDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                            else
                                                            {
                                                                foreach (XmlNode nodeComponent2 in nodeOrganizer)
                                                                {
                                                                    if (nodeComponent2.Name == "component")
                                                                    {
                                                                        try
                                                                        {
                                                                            XmlNode nodeObservation = nodeComponent2["observation"];
                                                                            XmlNode nodeText = nodeObservation["text"];
                                                                            XmlNode nodeReference = nodeText["reference"];

                                                                            int ncntr = 1;
                                                                            // Multiple Dates Passed as center value - 1st = Order Date, 2nd - Collection Date, 3rd Report Date
                                                                            foreach (XmlNode NodeUseablePeriod in nodeReference)
                                                                            {
                                                                                if (NodeUseablePeriod.Name == "useablePeriod")
                                                                                {
                                                                                    if (ncntr == 1)
                                                                                        OrderDate = NodeUseablePeriod.Attributes.GetNamedItem("value").Value;
                                                                                    else if (ncntr == 2)
                                                                                        CollectionDate = NodeUseablePeriod.Attributes.GetNamedItem("value").Value;
                                                                                    else if (ncntr == 3)
                                                                                        ReportDate = NodeUseablePeriod.Attributes.GetNamedItem("value").Value;

                                                                                    ncntr++;
                                                                                }
                                                                            }
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (OrderDate == "19000101")
                                                            {
                                                                // Check Effective Time under observation
                                                                XmlNode nodeComponent3 = nodeOrganizer["component"];
                                                                if (nodeComponent3 != null)
                                                                {
                                                                    XmlNode nodeObservation = nodeComponent3["observation"];
                                                                    if (nodeObservation != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            XmlNode nodeEffectiveTime2 = nodeObservation["effectiveTime"];
                                                                            OrderDate = nodeEffectiveTime2.Attributes.GetNamedItem("value").Value;
                                                                        }
                                                                        catch
                                                                        { }
                                                                    }
                                                                }
                                                            }

                                                            XmlNode nodeCode = nodeOrganizer["code"];
                                                            string CodeValue = "";
                                                            int CodeSystemId = 0;

                                                            try
                                                            {
                                                                CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                XmlNode nodeOriginalText = nodeCode["originalText"];
                                                                if (nodeOriginalText != null)
                                                                    Description = nodeOriginalText.InnerText;
                                                                else
                                                                    Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    Description = nodeOrganizer.Attributes.GetNamedItem("classCode").Value;
                                                                }
                                                                catch { }
                                                            }

                                                            if (Description.Length > 50) Description = Description.Substring(0, 50);

                                                            if ((Description == "" || Description == "BATTERY") && ReportDate == "19000101")
                                                            {
                                                                // This is a blank entry - Skip
                                                            }
                                                            else
                                                            {

                                                                var LabResult = new PatientLabResult()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    LabResultCntr = 0,
                                                                    ProviderId_Requested = 0,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    LabName = Description,
                                                                    OrderDate = Convert.ToDateTime(OrderDate.Substring(4, 2) + "/" + OrderDate.Substring(6, 2) + "/" + OrderDate.Substring(0, 4)),
                                                                    CollectionDate = Convert.ToDateTime(CollectionDate.Substring(4, 2) + "/" + CollectionDate.Substring(6, 2) + "/" + CollectionDate.Substring(0, 4)),
                                                                    Requisiton = "",
                                                                    Specimen = "",
                                                                    SpecimenSource = "",
                                                                    ReportDate = Convert.ToDateTime(ReportDate.Substring(4, 2) + "/" + ReportDate.Substring(6, 2) + "/" + ReportDate.Substring(0, 4)),
                                                                    ReviewDate = Convert.ToDateTime("1/1/1900"),
                                                                    Reviewer = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                };
                                                                db.PatientLabResults.Add(LabResult);

                                                                db.SaveChanges();

                                                                Int64 LabId = LabResult.LabResultCntr;

                                                                // Get Test Result Details

                                                                int cntr = 0;
                                                                foreach (XmlNode nodeComponent2 in nodeOrganizer)
                                                                {
                                                                    if (nodeComponent2.Name == "component")
                                                                    {
                                                                        XmlNode nodeObservation = nodeComponent2["observation"];
                                                                        XmlNode nodeCode2 = nodeObservation["code"];
                                                                        try
                                                                        {
                                                                            string CodeValue2 = "";
                                                                            int CodeSystemId2 = 0;
                                                                            string Description2 = "";
                                                                            try
                                                                            {
                                                                                CodeValue2 = nodeCode2.Attributes.GetNamedItem("code").Value;
                                                                                if (CodeValue2.Length > 20) CodeValue2 = CodeValue2.Substring(0, 20);
                                                                            }
                                                                            catch { }
                                                                            try
                                                                            {
                                                                                CodeSystemId2 = CodeSystemValue(nodeCode2.Attributes.GetNamedItem("codeSystem").Value);
                                                                            }
                                                                            catch { }
                                                                            try
                                                                            {
                                                                                Description2 = nodeCode2.Attributes.GetNamedItem("displayName").Value;
                                                                                if (Description2.Length > 50) Description2 = Description2.Substring(0, 50);
                                                                            }
                                                                            catch { }

                                                                            XmlNode nodeValue = nodeObservation["value"];
                                                                            string value = "";
                                                                            string unit = "";
                                                                            try
                                                                            {
                                                                                value = nodeValue.Attributes.GetNamedItem("value").Value;
                                                                                unit = nodeValue.Attributes.GetNamedItem("unit").Value;
                                                                            }
                                                                            catch { }


                                                                            XmlNode nodeInterpretationCode = nodeObservation["interpretationCode"];
                                                                            string Abnormal = "N";
                                                                            try
                                                                            {
                                                                                Abnormal = nodeInterpretationCode.Attributes.GetNamedItem("code").Value;
                                                                                if (Abnormal.Length > 1)
                                                                                    Abnormal = Abnormal.Substring(0, 1);
                                                                            }
                                                                            catch { }  // No Data for Abnormal

                                                                            string RefRange = "";
                                                                            try
                                                                            {
                                                                                XmlNode nodeRefRange = nodeObservation["referenceRange"];
                                                                                XmlNode nodeObsRange = nodeRefRange["observationRange"];
                                                                                XmlNode nodeObsText = nodeObsRange["text"];
                                                                                if (nodeObsText != null)
                                                                                    RefRange = nodeObsText.InnerText;
                                                                                else
                                                                                {
                                                                                    XmlNode nodeValue2 = nodeObsRange["value"];
                                                                                    XmlNode nodeLow = nodeValue2["low"];
                                                                                    XmlNode nodeHigh = nodeValue2["high"];
                                                                                    RefRange = nodeLow.Attributes.GetNamedItem("value").Value + " - " + nodeHigh.Attributes.GetNamedItem("value").Value + " " + nodeHigh.Attributes.GetNamedItem("unit").Value;
                                                                                }
                                                                                if (RefRange.Length > 120) RefRange = RefRange.Substring(0, 120);
                                                                            }
                                                                            catch { } // No ref range data

                                                                            cntr++;
                                                                            var LabTest = new PatientLabResultTest()
                                                                            {
                                                                                PatientId = sPatient.AMRPatientId,
                                                                                FacilityId = sFacility.FacilityId,
                                                                                VisitId = sVisit.VisitId,
                                                                                LabResultCntr = LabId,
                                                                                TestCntr = cntr,
                                                                                CodeValue = CodeValue2,
                                                                                CodeSystemId = CodeSystemId2,
                                                                                Component = Description2,
                                                                                Result = value,
                                                                                RefRange = RefRange,
                                                                                Units = unit,
                                                                                Abnormal = Abnormal,
                                                                                ResultStatus = "",
                                                                            };
                                                                            LabResult.PatientLabResultTests.Add(LabTest);

                                                                            db.SaveChanges();
                                                                        }
                                                                        catch { }  // No valid data for the lab detail

                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error labs " + Description;
                                                            Valid = false;
                                                        }

                                                    }
                                                    #endregion

                                                    #region Procedures
                                                    if (template == "2.16.840.1.113883.10.20.1.12" || template == "2.16.840.1.113883.10.20.22.2.7" || template == "2.16.840.1.113883.10.20.22.2.7.1")     // Procedures    
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeProcedure = nodeSection["procedure"];
                                                            XmlNode nodeObservation = nodeSection["observation"];
                                                            if (nodeProcedure != null)
                                                            {
                                                                XmlNode nodeCode = nodeProcedure["code"];

                                                                string CodeValue = "";
                                                                int CodeSystemId = 0;

                                                                try
                                                                {
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                }
                                                                catch
                                                                {
                                                                    XmlNode nodeTranslation = nodeCode["translation"];
                                                                    if (nodeTranslation != null)
                                                                    {
                                                                        CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                        Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                        if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                    }
                                                                }
                                                                if (Description.Trim() == "")
                                                                {
                                                                    XmlNode nodeText = nodeProcedure["text"];
                                                                    if (nodeText != null)
                                                                        Description = nodeText.InnerText;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                }

                                                                string ProcDate = "";
                                                                XmlNode nodeEffectiveTime = nodeProcedure["effectiveTime"];
                                                                if (nodeEffectiveTime != null)
                                                                {
                                                                    XmlNode nodeLow = nodeEffectiveTime["low"];

                                                                    if (nodeLow != null)
                                                                        ProcDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                    else
                                                                        ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                if (ProcDate.Length > 8) ProcDate = ProcDate.Substring(0, 8);

                                                                var Procedure = new PatientProcedure()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatProcedureCntr = 0,
                                                                    Description = Description,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Diagnosis = "",
                                                                    PerformedBy = "",
                                                                    ServiceLocation = "",
                                                                    ServiceDate = ProcDate,
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientProcedures.Add(Procedure);

                                                                db.SaveChanges();
                                                            }


                                                            else if (nodeObservation != null)
                                                            {
                                                                XmlNode nodeCode = nodeObservation["code"];

                                                                string CodeValue = "";
                                                                int CodeSystemId = 0;
                                                                try
                                                                {
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                }
                                                                catch
                                                                {
                                                                    XmlNode nodeTranslation = nodeCode["translation"];
                                                                    if (nodeTranslation != null)
                                                                    {
                                                                        CodeValue = nodeTranslation.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeTranslation.Attributes.GetNamedItem("codeSystem").Value);
                                                                        Description = nodeTranslation.Attributes.GetNamedItem("displayName").Value;
                                                                        if (Description.Length > 50) Description = Description.Substring(0, 50);
                                                                    }
                                                                }

                                                                XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                                XmlNode nodeLow = nodeEffectiveTime["low"];
                                                                string ProcDate = "";
                                                                if (nodeLow != null)
                                                                    ProcDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                                                else
                                                                    ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;

                                                                if (ProcDate.Length > 8) ProcDate = ProcDate.Substring(0, 8);

                                                                var Procedure = new PatientProcedure()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PatProcedureCntr = 0,
                                                                    Description = Description,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Diagnosis = "",
                                                                    PerformedBy = "",
                                                                    ServiceLocation = "",
                                                                    ServiceDate = ProcDate,
                                                                    Note = "",
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientProcedures.Add(Procedure);

                                                                db.SaveChanges();
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error procedures " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Plan Of Care
                                                    if (template == "2.16.840.1.113883.10.20.1.10" || template == "2.16.840.1.113883.10.20.22.2.10")     // Plan Of Care    
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeObservation = nodeSection["observation"];
                                                            XmlNode nodeEncounter = nodeSection["encounter"];
                                                            XmlNode nodeAct = nodeSection["act"];

                                                            if (nodeObservation != null)
                                                            {
                                                                XmlNode nodeCode = nodeObservation["code"];
                                                                string CodeValue = "";
                                                                string Instruction = "";
                                                                int CodeSystemId = 0;

                                                                string ProcDate = "";
                                                                XmlNode nodeValue = nodeObservation["value"];
                                                                if (nodeValue != null)
                                                                {
                                                                    try
                                                                    {
                                                                        Instruction = nodeValue.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeValue.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeValue.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch { }
                                                                }
                                                                if (Instruction == "")
                                                                {
                                                                    try
                                                                    {
                                                                        Instruction = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch
                                                                    {
                                                                        // No Codified Data - Use Text
                                                                        XmlNode nodeText = nodeObservation["text"];
                                                                        Instruction = nodeText.InnerText;
                                                                    }
                                                                }
                                                                if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);

                                                                XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                                try
                                                                {
                                                                    ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                                        if (nodeCenter != null)
                                                                            ProcDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                                    }
                                                                    catch
                                                                    { }
                                                                }

                                                                if (ProcDate.Length >= 8)
                                                                    ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                                else
                                                                    ProcDate = "1/1/1900";



                                                                var PlanOfCare = new PatientPlanOfCare()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PlanCntr = 0,
                                                                    InstructionTypeId = 4,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Instruction = Instruction,
                                                                    Goal = Description,
                                                                    Note = "",
                                                                    AppointmentDateTime = Convert.ToDateTime(ProcDate),
                                                                    ProviderId = 0,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientPlanOfCares.Add(PlanOfCare);

                                                                db.SaveChanges();
                                                            }
                                                            else if (nodeEncounter != null)
                                                            {
                                                                XmlNode nodeCode = nodeEncounter["code"];
                                                                string CodeValue = "";
                                                                string Instruction = "";
                                                                int CodeSystemId = 0;

                                                                string ProcDate = "";
                                                                try
                                                                {
                                                                    Instruction = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        XmlNode nodeEntryRelationship2 = nodeEncounter["entryRelationship"];
                                                                        XmlNode nodeAct2 = nodeEntryRelationship2["act"];
                                                                        XmlNode nodeCode2 = nodeAct2["code"];

                                                                        Instruction = nodeCode2.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeCode2.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeCode2.Attributes.GetNamedItem("codeSystem").Value);
                                                                        if (Instruction == "instruction")
                                                                        {
                                                                            XmlNode nodeText = nodeAct2["text"];
                                                                            Instruction = nodeText.InnerText;
                                                                        }

                                                                    }
                                                                    catch
                                                                    {
                                                                        // No Codified Data - Use Text
                                                                        if (Instruction == "")
                                                                        {
                                                                            XmlNode nodeText = nodeEncounter["text"];
                                                                            Instruction = nodeText.InnerText;
                                                                        }
                                                                    }
                                                                }
                                                                if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);

                                                                XmlNode nodeEffectiveTime = nodeEncounter["effectiveTime"];
                                                                try
                                                                {
                                                                    ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                                        if (nodeCenter != null)
                                                                            ProcDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                                    }
                                                                    catch
                                                                    { }
                                                                }

                                                                if (ProcDate.Length >= 8)
                                                                    ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                                else
                                                                    ProcDate = "1/1/1900";

                                                                XmlNode nodeEntryRelationship = nodeEncounter["entryRelationship"];
                                                                if (nodeEntryRelationship != null)
                                                                {
                                                                    XmlNode NodeAct = nodeEntryRelationship["act"];
                                                                    if (NodeAct != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            XmlNode nodeText = NodeAct["text"];
                                                                            Instruction = nodeText.InnerText;
                                                                            if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);
                                                                        }
                                                                        catch { }

                                                                    }
                                                                }
                                                                var PlanOfCare = new PatientPlanOfCare()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PlanCntr = 0,
                                                                    InstructionTypeId = 4,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Instruction = Instruction,
                                                                    Goal = Description,
                                                                    Note = "",
                                                                    AppointmentDateTime = Convert.ToDateTime(ProcDate),
                                                                    ProviderId = 0,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientPlanOfCares.Add(PlanOfCare);

                                                                db.SaveChanges();
                                                            }

                                                            else if (nodeAct != null)
                                                            {
                                                                XmlNode nodeCode = nodeAct["code"];
                                                                string CodeValue = "";
                                                                string Instruction = "";
                                                                int CodeSystemId = 0;

                                                                string ProcDate = "";
                                                                try
                                                                {
                                                                    Instruction = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                    CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                    if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                    CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);

                                                                    if (Instruction == "instruction")
                                                                    {
                                                                        XmlNode nodeText = nodeAct["text"];
                                                                        Instruction = nodeText.InnerText;
                                                                    }
                                                                }
                                                                catch
                                                                {
                                                                    // No Codified Data - Use Text
                                                                    if (Instruction == "")
                                                                    {
                                                                        XmlNode nodeText = nodeAct["text"];
                                                                        Instruction = nodeText.InnerText;
                                                                    }
                                                                }
                                                                if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);

                                                                XmlNode nodeEffectiveTime = nodeAct["effectiveTime"];
                                                                try
                                                                {
                                                                    ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                                        if (nodeCenter != null)
                                                                            ProcDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                                    }
                                                                    catch
                                                                    { }
                                                                }

                                                                if (ProcDate.Length >= 8)
                                                                    ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                                else
                                                                    ProcDate = "1/1/1900";


                                                                var PlanOfCare = new PatientPlanOfCare()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PlanCntr = 0,
                                                                    InstructionTypeId = 1,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Instruction = Instruction,
                                                                    Goal = Description,
                                                                    Note = "",
                                                                    AppointmentDateTime = Convert.ToDateTime(ProcDate),
                                                                    ProviderId = 0,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientPlanOfCares.Add(PlanOfCare);

                                                                db.SaveChanges();

                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error plan of care " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Clinical Instructions
                                                    if (template == "2.16.840.1.113883.10.20.22.2.45")     // Clinical Instructions    
                                                    {
                                                        string Description = "";
                                                        try
                                                        {
                                                            XmlNode nodeObservation = nodeSection["observation"];
                                                            if (nodeObservation != null)
                                                            {
                                                                XmlNode nodeCode = nodeObservation["code"];
                                                                string CodeValue = "";
                                                                string Instruction = "";
                                                                int CodeSystemId = 0;

                                                                string ProcDate = "";

                                                                XmlNode nodeValue = nodeObservation["value"];
                                                                if (nodeValue != null)
                                                                {
                                                                    try
                                                                    {
                                                                        Instruction = nodeValue.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeValue.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeValue.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch { }
                                                                }
                                                                if (Instruction == "")
                                                                {
                                                                    try
                                                                    {
                                                                        Instruction = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch
                                                                    {
                                                                        // No Codified Data - Use Text
                                                                        XmlNode nodeText = nodeObservation["text"];
                                                                        Instruction = nodeText.InnerText;
                                                                    }
                                                                }
                                                                if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);

                                                                XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                                try
                                                                {
                                                                    ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                }
                                                                catch
                                                                {
                                                                    try
                                                                    {
                                                                        XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                                        if (nodeCenter != null)
                                                                            ProcDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                                    }
                                                                    catch
                                                                    { }
                                                                }

                                                                if (ProcDate.Length >= 8)
                                                                    ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                                else
                                                                    ProcDate = "1/1/1900";



                                                                var PlanOfCare = new PatientPlanOfCare()
                                                                {
                                                                    PatientId = sPatient.AMRPatientId,
                                                                    FacilityId = sFacility.FacilityId,
                                                                    VisitId = sVisit.VisitId,
                                                                    PlanCntr = 0,
                                                                    InstructionTypeId = 4,
                                                                    CodeValue = CodeValue,
                                                                    CodeSystemId = CodeSystemId,
                                                                    Instruction = Instruction,
                                                                    Goal = Description,
                                                                    Note = "",
                                                                    AppointmentDateTime = Convert.ToDateTime(ProcDate),
                                                                    ProviderId = 0,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    Deleted = false,
                                                                };
                                                                db.PatientPlanOfCares.Add(PlanOfCare);

                                                                db.SaveChanges();
                                                            }
                                                            else
                                                            {
                                                                XmlNode nodeEncounter = nodeSection["encounter"];
                                                                if (nodeEncounter != null)
                                                                {
                                                                    XmlNode nodeCode = nodeEncounter["code"];
                                                                    string CodeValue = "";
                                                                    string Instruction = "";
                                                                    int CodeSystemId = 0;

                                                                    string ProcDate = "";
                                                                    try
                                                                    {
                                                                        Description = nodeCode.Attributes.GetNamedItem("displayName").Value;
                                                                        CodeValue = nodeCode.Attributes.GetNamedItem("code").Value;
                                                                        if (CodeValue.Length > 20) CodeValue = CodeValue.Substring(0, 20);
                                                                        CodeSystemId = CodeSystemValue(nodeCode.Attributes.GetNamedItem("codeSystem").Value);
                                                                    }
                                                                    catch
                                                                    {
                                                                        // No Codified Data - Use Text
                                                                        if (Description == "")
                                                                        {
                                                                            XmlNode nodeText = nodeEncounter["text"];
                                                                            Description = nodeText.InnerText;
                                                                        }
                                                                    }
                                                                    if (Description.Length > 256) Description = Description.Substring(0, 256);

                                                                    XmlNode nodeEffectiveTime = nodeEncounter["effectiveTime"];
                                                                    try
                                                                    {
                                                                        ProcDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                    }
                                                                    catch
                                                                    {
                                                                        XmlNode nodeCenter = nodeEffectiveTime["center"];
                                                                        if (nodeCenter != null)
                                                                            ProcDate = nodeCenter.Attributes.GetNamedItem("value").Value;
                                                                    }

                                                                    if (ProcDate.Length >= 8)
                                                                        ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                                    else
                                                                        ProcDate = "1/1/1900";

                                                                    XmlNode nodeEntryRelationship = nodeEncounter["entryRelationship"];
                                                                    if (nodeEntryRelationship != null)
                                                                    {
                                                                        XmlNode NodeAct = nodeEntryRelationship["act"];
                                                                        if (NodeAct != null)
                                                                        {
                                                                            try
                                                                            {
                                                                                XmlNode nodeText = NodeAct["text"];
                                                                                Instruction = nodeText.InnerText;
                                                                                if (Instruction.Length > 256) Instruction = Instruction.Substring(0, 256);
                                                                            }
                                                                            catch { }

                                                                        }
                                                                    }
                                                                    var PlanOfCare = new PatientPlanOfCare()
                                                                    {
                                                                        PatientId = sPatient.AMRPatientId,
                                                                        FacilityId = sFacility.FacilityId,
                                                                        VisitId = sVisit.VisitId,
                                                                        PlanCntr = 0,
                                                                        InstructionTypeId = 4,
                                                                        CodeValue = CodeValue,
                                                                        CodeSystemId = CodeSystemId,
                                                                        Instruction = Instruction,
                                                                        Goal = Description,
                                                                        Note = "",
                                                                        AppointmentDateTime = Convert.ToDateTime(ProcDate),
                                                                        ProviderId = 0,
                                                                        UserId_Created = UserId,
                                                                        DateCreated = System.DateTime.Now,
                                                                        Deleted = false,
                                                                    };
                                                                    db.PatientPlanOfCares.Add(PlanOfCare);

                                                                    db.SaveChanges();
                                                                }
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error plan of care " + Description;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion

                                                    #region Payer
                                                    if (template == "2.16.840.1.113883.10.20.1.9" || template == "2.16.840.1.113883.10.20.22.2.18")     // Payer
                                                    {
                                                        string PlanName = "";
                                                        try
                                                        {
                                                            string MemberNumber = "";
                                                            string GroupNumber = "";
                                                            string Subscriber = "";
                                                            string Relationship = "";
                                                            string EffectiveDate = "";


                                                            XmlNode nodeAct = nodeSection["act"];
                                                            XmlNode nodeEntryRelationship = nodeAct["entryRelationship"];
                                                            XmlNode nodeAct2 = nodeSection["act"];
                                                            XmlNode nodePerformer = nodeSection["performer"];
                                                            XmlNode nodeAssingedEntity = nodePerformer["assignedEntity"];
                                                            XmlNode nodeRepresentedOrganization = nodeSection["representedOrganization"];
                                                            XmlNode nodeName = nodeRepresentedOrganization["name"];

                                                            PlanName = nodeName.InnerText;

                                                            XmlNode nodeParticipant = nodeSection["participant"];
                                                            XmlNode nodeParticipantRole = nodeParticipant["participantRole"];
                                                            XmlNode nodeId = nodeParticipantRole["id"];

                                                            GroupNumber = nodeId.Attributes.GetNamedItem("root").Value;


                                                            //// Get The Date
                                                            //XmlNode nodeEffectiveTime = nodeObservation["effectiveTime"];
                                                            //XmlNode nodeLow = nodeEffectiveTime["low"];
                                                            //string ProblemDate = nodeLow.Attributes.GetNamedItem("value").Value;

                                                            // Value Node - Get Payer Name
                                                            //XmlNode nodePerformer = nodeAct2["performer"];
                                                            //XmlNode nodeAssignedEntity = nodePerformer["assignedEntity"];
                                                            //XmlNode nodeRepresentedOrganization = nodeAssignedEntity["representedOrganization"];
                                                            //XmlNode nodeName = nodeRepresentedOrganization["name"];
                                                            //string PayerName = nodeName.InnerText;

                                                            ////string x = nodeValue.Attributes.GetNamedItem("xsi:type").Value;
                                                            //string ProblemCode = nodeValue.Attributes.GetNamedItem("code").Value;
                                                            //string ProblemCodeSystem = nodeValue.Attributes.GetNamedItem("codeSystem").Value;
                                                            //string ProblemName = nodeValue.Attributes.GetNamedItem("displayName").Value;

                                                            //// Status
                                                            //XmlNode nodeSEntryRelationship = nodeObservation["entryRelationship"];
                                                            //XmlNode nodeSObservation = nodeSEntryRelationship["observation"];
                                                            //XmlNode nodeSValue = nodeSObservation["value"];
                                                            //string ProblemStatus = nodeSValue.Attributes.GetNamedItem("displayName").Value;

                                                            var Insurance = new PatientInsurance()
                                                            {
                                                                PatientId = sPatient.AMRPatientId,
                                                                FacilityId = sFacility.FacilityId,
                                                                VisitId = sVisit.VisitId,
                                                                PatientInsuranceId = 0,
                                                                PlanName = PlanName,
                                                                MemberNumber = MemberNumber,
                                                                GroupNumber = GroupNumber,
                                                                Subscriber = Subscriber,
                                                                Relationship = Relationship,
                                                                EffectiveDate = null,
                                                                UserId_Created = UserId,
                                                                DateCreated = System.DateTime.Now,
                                                                UserId_Modified = UserId,
                                                                DateModified = System.DateTime.Now,
                                                                Deleted = false,
                                                            };
                                                            db.PatientInsurances.Add(Insurance);

                                                            db.SaveChanges();
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            ErrorMsg = ErrorMsg + " Error payor " + PlanName;
                                                            Valid = false;
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                #region Unstructured
                                // Check for unstructured attachement
                                XmlNode nodeNonXMLBody = nodeComponent["nonXMLBody"];
                                if (nodeNonXMLBody != null)
                                {
                                    XmlNode nodeText = nodeNonXMLBody["text"];
                                    string Format = nodeText.Attributes.GetNamedItem("mediaType").Value;
                                    string Attachment = nodeText.InnerText;
                                    string Description = "Document uploaded " + System.DateTime.Now.ToShortDateString();


                                    if (Format == "application/pdf")
                                        Format = "PDF";
                                    else if (Format == "application/tiff")
                                        Format = "TIFF";
                                    else if (Format == "application/gif")
                                        Format = "GIF";
                                    else if (Format == "application/jped")
                                        Format = "JPEG";
                                    else if (Format == "application/png")
                                        Format = "PNG";
                                    else if (Format == "application/bmp")
                                        Format = "BMP";

                                    // Save Document

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
                                        PatientId = Convert.ToInt64(sPatient.AMRPatientId),
                                        VisitId = 0,
                                        DocumentCntr = 0,
                                        FacilityId = sFacility.FacilityId,
                                        DocumentDescription = Description,
                                        DocumentId = ServiceDate,
                                        DocumentFormat = Format,
                                        StorageLocation = Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr,
                                        Notes = "",
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
                                    string FileName = Document.DocumentCntr.ToString() + "." + Format;
                                    FileHelper.CheckOrCreateDirectory(Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr);
                                    byte[] imageBytes = Convert.FromBase64String(Attachment);
                                    FileHelper.BytesToDisk(imageBytes, Config.AttachmentLocation + "\\" + Config.AttachmentFolderCntr + "\\" + FileName);
                                }
                                #endregion
                            }
                        }
                    }

                }
            }
            return Valid;
        }
        int GetRelationshipSNOMED(string SNOMED)
        {
            int value = 0;
            try
            {
                using (var db = new AMREntities())
                {
                    C_Relationship results = db.C_Relationship.FirstOrDefault(r => r.SNOMED == SNOMED);

                    if (results != null)
                    {
                        value = Convert.ToInt32(results.RelationshipId);
                    }
                }
            }
            catch (Exception ex)
            {
                value = 0;
            }
            return value;
        }

        private int CodeSystemValue(string CodeSystemValue)
        {
            int cValue = 0;  // Not Coded
            if (CodeSystemValue == "2.16.840.1.113883.6.12")
                cValue = 1;	//CPT 4
            else if (CodeSystemValue == "2.16.840.1.113883.6.2" || CodeSystemValue == "2.16.840.1.113883.6.103")
                cValue = 2;	//ICD 9
            else if (CodeSystemValue == "2.16.840.1.113883.6.90")
                cValue = 3;	//ICD 10
            //4	MEDCIN
            else if (CodeSystemValue == "2.16.840.1.113883.6.96")
                cValue = 5;	//SNOMED
            else if (CodeSystemValue == "2.16.840.1.113883.6.88")
                cValue = 6; //RxNorm
            else if (CodeSystemValue == "2.16.840.1.113883.6.59" || CodeSystemValue == "2.16.840.1.113883.12.292")
                cValue = 7; //CVX
            else if (CodeSystemValue == "2.16.840.1.113883.6.69")
                cValue = 8; // NDC
            else if (CodeSystemValue == "2.16.840.1.113883.6.1")
                cValue = 9; // LOINC
            else if (CodeSystemValue == "2.16.840.1.113883.3.26.1.5")
                cValue = 9; // LOINC
            return cValue;
            // (CodeSystemValue == "2.16.840.1.113883.5.83")    // PH_ObservationInterpretation_HL7_V3
            // (CodeSystemValue == "2.16.840.1.113883.5.4")  // PH_ActCode_HL7_V3
            // (CodeSystemValue == "2.16.840.1.113883.5.1")   // Gender Code   -M,F,UN
            // (CodeSystemValue == "2.16.840.1.113883.5.112")   //Route of Admin
        }
        private Int16 CodeStatus(string SNOMEDStatusCode)
        {
            Int16 value = 5;
            if (SNOMEDStatusCode == "55561003" || SNOMEDStatusCode == "active") // Active
                value = 1;
            else if (SNOMEDStatusCode == "73425007" || SNOMEDStatusCode == "inactive")  // Inactive
                value = 2;
            else if (SNOMEDStatusCode == "7087005" || SNOMEDStatusCode == "intermittent")  // Intermittent
                value = 3;
            else if (SNOMEDStatusCode == "413322009" || SNOMEDStatusCode == "resolved")  // Resolved
                value = 4;

            return value;
        }
    }
}
