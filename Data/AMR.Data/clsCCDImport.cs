using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMR.Data;

namespace AMR.Data
{
    public class clsCCDImport
    {
        public bool Parse(Int64 FacilityId, Int64 UserId, Int64 AMRPatientId, XmlDocument xd, ref string ErrorMsg)
        {
            bool Valid = true;

            Int64 VisitId = 0;
            string VisitReason = "";
            string VisitDate = "";

            using (var db = new AMREntities())
            {

                //Parsing XML using XmlDocument

                // Author
                XmlNode nodeAuthor = xd.DocumentElement["author"];
                // The ProviderId will be pulled from this info
                Int64 ProviderId = 0;

                if (FacilityId == 0 && AMRPatientId > 0)
                {
                    // This is a CCD import from the patient

                }
                else
                {
                    // This is a CCD from an Integrated System
                    #region Get Visit Id and Provider Info
                    // Provider Info
                    XmlNode nodeComponentOf = xd.DocumentElement["componentOf"];
                    if (nodeComponentOf != null)
                    {
                        // Try to get the visit id
                        XmlNode nodeEncompassingEncounter = nodeComponentOf["encompassingEncounter"];
                        try
                        {
                            XmlNode nodeId = nodeEncompassingEncounter["id"];
                            VisitId = Convert.ToInt64(nodeId.Attributes.GetNamedItem("extension").Value);
                            XmlNode nodeEffectiveTime = nodeEncompassingEncounter["effectiveTime"];
                            XmlNode nodeLow = nodeEffectiveTime["low"];
                            VisitDate = nodeLow.Attributes.GetNamedItem("value").Value;
                        }
                        catch { }

                        // Make sure that this visit has not already been imported


                        PatientVisit VisitResults = db.PatientVisits.FirstOrDefault
                                            (p => p.PatientId == AMRPatientId
                                            && p.VisitId == VisitId);

                        if (VisitResults != null)
                        {
                            // Visit Already Exists - This is an error
                            Valid = false;
                            ErrorMsg = "This visit id has already been processed.";
                        }

                        if (Valid)
                        {
                            // Get the provider information
                            //XmlNode nodeResponsibleParty = nodeEncompassingEncounter["responsibleParty"];
                            //if (nodeResponsibleParty != null)
                            XmlNode nodeDocumentationOf = xd.DocumentElement["documentationOf"];
                            if (nodeDocumentationOf != null)
                            {
                                XmlNode nodeServiceEvent = nodeDocumentationOf["serviceEvent"];
                                if (nodeServiceEvent != null)
                                {
                                    // There may be multiple providers, so we need to loop through performers
                                    foreach (XmlNode node2 in nodeServiceEvent)
                                    {
                                        if (node2.Name == "performer")
                                        {
                                            int idCntr = 0;
                                            int telecomCntr = 0;
                                            string FacilityProviderId = "";
                                            string LicenseId = "";
                                            string DEA = "";
                                            string Email = "";
                                            string Phone = "";

                                            XmlNode nodeAssignedEntity = node2["assignedEntity"];
                                            foreach (XmlNode node in nodeAssignedEntity)
                                            {
                                                if (node.Name == "id")
                                                {
                                                    idCntr++;
                                                    try
                                                    {
                                                        if (idCntr == 1)
                                                            FacilityProviderId = node.Attributes.GetNamedItem("extension").Value;
                                                        else if (idCntr == 2)
                                                            LicenseId = node.Attributes.GetNamedItem("extension").Value.ToString();
                                                        else if (idCntr == 3)
                                                            DEA = node.Attributes.GetNamedItem("extension").Value.ToString();
                                                    }
                                                    catch { }
                                                }
                                                if (node.Name == "telecom")
                                                {
                                                    telecomCntr++;
                                                    try
                                                    {
                                                        string temp = node.Attributes.GetNamedItem("value").Value.ToString();
                                                        if (temp.Substring(0, 3) == "tel")
                                                        {
                                                            Phone = temp.Substring(4, temp.Length - 4);
                                                            if (Phone.Length > 16)
                                                                Phone = Phone.Substring(0, 16);
                                                        }
                                                        else if (temp.Substring(0, 6) == "mailto")
                                                        {
                                                            Email = temp.Substring(7, temp.Length - 7);
                                                            if (Email.Length > 60)
                                                                Email = Email.Substring(0, 60);
                                                        }
                                                    }
                                                    catch { }
                                                }
                                                if (node.Name == "assignedPerson")
                                                {
                                                    XmlNode nodeName2 = node["name"];
                                                    XmlNode nodeGiven2 = nodeName2["given"];
                                                    string First2 = nodeGiven2.InnerText;
                                                    XmlNode nodeFamily2 = nodeName2["family"];
                                                    string Last2 = nodeFamily2.InnerText;
                                                    string Suffix2 = "";
                                                    XmlNode nodeSuffix2 = nodeName2["suffix"];
                                                    if (nodeSuffix2 != null)
                                                        Suffix2 = nodeSuffix2.InnerText;
                                                    ProviderId = 0;
                                                    Int64 PracticeId2 = 0;

                                                    // Check if provider is alreay in the database
                                                    if (FacilityProviderId != "")
                                                    {
                                                        // Check If EMR ProviderId Passed In Already Exists For Facility
                                                        ProviderFacilityLink LinkResults = db.ProviderFacilityLinks.FirstOrDefault
                                                                (p => p.FacilityProviderId == FacilityProviderId
                                                                && p.FacilityId == FacilityId);

                                                        if (LinkResults != null)
                                                        {
                                                            // Provider Already Exists
                                                            ProviderId = LinkResults.ProviderId;
                                                        }
                                                        else
                                                        {
                                                            //Check If ProviderId Already Exists - If So, just create link to facility

                                                            // First, Get The PracticeId for the facility
                                                            Facility FacilityResults = db.Facilities.FirstOrDefault
                                                                (f => f.FacilityId == FacilityId);

                                                            if (FacilityResults == null)
                                                            {
                                                                Valid = false;
                                                                ErrorMsg = "Error reading facility information.";
                                                            }
                                                            else
                                                            {
                                                                PracticeId2 = Convert.ToInt64(FacilityResults.PracticeId);

                                                                // Check if the provider already exists for the practice

                                                                Provider ProviderResults = db.Providers.FirstOrDefault
                                                                    (p => p.PracticeId == PracticeId2
                                                                    && p.License == LicenseId);

                                                                if (ProviderResults != null)
                                                                {
                                                                    // Provider Exists - Create Link
                                                                    var NewProviderLink = new ProviderFacilityLink()
                                                                    {

                                                                        ProviderId = ProviderResults.ProviderId,
                                                                        FacilityId = FacilityId,
                                                                        FacilityProviderId = FacilityProviderId,
                                                                        DateCreated = System.DateTime.Now,
                                                                    };
                                                                    db.ProviderFacilityLinks.Add(NewProviderLink);

                                                                    db.SaveChanges();
                                                                }

                                                                else
                                                                {
                                                                    Provider ProviderResults2 = db.Providers.FirstOrDefault
                                                                    (p => p.PracticeId == PracticeId2
                                                                    && p.DEA == DEA);

                                                                    if (ProviderResults != null)
                                                                    {
                                                                        try
                                                                        {
                                                                            // Provider Exists - Create Link
                                                                            var NewProviderLink = new ProviderFacilityLink()
                                                                            {

                                                                                ProviderId = ProviderResults.ProviderId,
                                                                                FacilityId = FacilityId,
                                                                                FacilityProviderId = FacilityProviderId,
                                                                                DateCreated = System.DateTime.Now,
                                                                            };
                                                                            db.ProviderFacilityLinks.Add(NewProviderLink);

                                                                            db.SaveChanges();
                                                                        }
                                                                        catch
                                                                        {
                                                                        }
                                                                    }

                                                                    else
                                                                    {
                                                                        // New Provider that needs to be added

                                                                        // Add Provider
                                                                        var NewProvider = new Provider()
                                                                        {
                                                                            PracticeId = PracticeId2,
                                                                            UserId = 0,         // Add This Back After Creating User
                                                                            Title = Suffix2,
                                                                            FirstName = First2,
                                                                            MiddleName = "",
                                                                            LastName = Last2,
                                                                            DEA = DEA,
                                                                            License = LicenseId,
                                                                            Phone = Phone,
                                                                            Email = Email,
                                                                            UserId_Created = UserId,
                                                                            DateCreated = System.DateTime.Now,
                                                                            UserId_Modified = UserId,
                                                                            DateModified = System.DateTime.Now,
                                                                        };

                                                                        db.Providers.Add(NewProvider);

                                                                        db.SaveChanges();

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

                                                                            UserLogin = Email,
                                                                            UserEmail = Email,
                                                                            Password = passencr,
                                                                            UserRoleId = 4,
                                                                            UserRoleLink = NewProvider.ProviderId,
                                                                            Enabled = true,
                                                                            Locked = false,
                                                                            ResetPassword = true,
                                                                        };
                                                                        db.Users.Add(NewUser);

                                                                        db.SaveChanges();

                                                                        // Update UserId In Provider Record
                                                                        Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == NewProvider.ProviderId);

                                                                        if (ProviderResp != null)
                                                                        {
                                                                            ProviderResp.UserId = NewUser.UserId;
                                                                            db.SaveChanges();
                                                                        }

                                                                        // Create Link
                                                                        var NewProviderLink = new ProviderFacilityLink()
                                                                        {

                                                                            ProviderId = NewProvider.ProviderId,
                                                                            FacilityId = FacilityId,
                                                                            FacilityProviderId = FacilityProviderId,
                                                                            DateCreated = System.DateTime.Now,
                                                                        };
                                                                        db.ProviderFacilityLinks.Add(NewProviderLink);

                                                                        db.SaveChanges();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ProviderId = 0;
                                                        //Valid = false;
                                                        //ErrorMsg = "No provider id";

                                                    }
                                                }
                                            }

                                            XmlNode nodeAssignedPerson = nodeAssignedEntity["assignedPerson"];
                                            XmlNode nodeName = nodeAssignedPerson["name"];
                                            XmlNode nodeGiven = nodeName["given"];
                                            string First = nodeGiven.InnerText;
                                            XmlNode nodeFamily = nodeName["family"];
                                            string Last = nodeFamily.InnerText;
                                            string Suffix = "";
                                            XmlNode nodeSuffix = nodeName["suffix"];
                                            if (nodeSuffix != null)
                                                Suffix = nodeSuffix.InnerText;
                                            ProviderId = 0;
                                            Int64 PracticeId = 0;

                                            // Check if provider is alreay in the database
                                            if (FacilityProviderId != "")
                                            {
                                                // Check If EMR ProviderId Passed In Already Exists For Facility
                                                ProviderFacilityLink LinkResults = db.ProviderFacilityLinks.FirstOrDefault
                                                        (p => p.FacilityProviderId == FacilityProviderId
                                                        && p.FacilityId == FacilityId);

                                                if (LinkResults != null)
                                                {
                                                    // Provider Already Exists
                                                    ProviderId = LinkResults.ProviderId;
                                                }
                                                else
                                                {
                                                    //Check If ProviderId Already Exists - If So, just create link to facility

                                                    // First, Get The PracticeId for the facility
                                                    Facility FacilityResults = db.Facilities.FirstOrDefault
                                                        (f => f.FacilityId == FacilityId);

                                                    if (FacilityResults == null)
                                                    {
                                                        Valid = false;
                                                        ErrorMsg = "Error reading facility information.";
                                                    }
                                                    else
                                                    {
                                                        PracticeId = Convert.ToInt64(FacilityResults.PracticeId);

                                                        // Check if the provider already exists for the practice

                                                        Provider ProviderResults = db.Providers.FirstOrDefault
                                                            (p => p.PracticeId == PracticeId
                                                            && p.License == LicenseId);

                                                        if (ProviderResults != null)
                                                        {
                                                            // Provider Exists - Create Link
                                                            var NewProviderLink = new ProviderFacilityLink()
                                                            {

                                                                ProviderId = ProviderResults.ProviderId,
                                                                FacilityId = FacilityId,
                                                                FacilityProviderId = FacilityProviderId,
                                                                DateCreated = System.DateTime.Now,
                                                            };
                                                            db.ProviderFacilityLinks.Add(NewProviderLink);

                                                            db.SaveChanges();
                                                        }

                                                        else
                                                        {
                                                            Provider ProviderResults2 = db.Providers.FirstOrDefault
                                                            (p => p.PracticeId == PracticeId
                                                            && p.DEA == DEA);

                                                            if (ProviderResults != null)
                                                            {
                                                                try
                                                                {
                                                                    // Provider Exists - Create Link
                                                                    var NewProviderLink = new ProviderFacilityLink()
                                                                    {

                                                                        ProviderId = ProviderResults.ProviderId,
                                                                        FacilityId = FacilityId,
                                                                        FacilityProviderId = FacilityProviderId,
                                                                        DateCreated = System.DateTime.Now,
                                                                    };
                                                                    db.ProviderFacilityLinks.Add(NewProviderLink);

                                                                    db.SaveChanges();
                                                                }
                                                                catch
                                                                {
                                                                }
                                                            }

                                                            else
                                                            {
                                                                // New Provider that needs to be added

                                                                // Add Provider
                                                                var NewProvider = new Provider()
                                                                {
                                                                    PracticeId = PracticeId,
                                                                    UserId = 0,         // Add This Back After Creating User
                                                                    Title = Suffix,
                                                                    FirstName = First,
                                                                    MiddleName = "",
                                                                    LastName = Last,
                                                                    DEA = DEA,
                                                                    License = LicenseId,
                                                                    Phone = Phone,
                                                                    Email = Email,
                                                                    UserId_Created = UserId,
                                                                    DateCreated = System.DateTime.Now,
                                                                    UserId_Modified = UserId,
                                                                    DateModified = System.DateTime.Now,
                                                                };

                                                                db.Providers.Add(NewProvider);

                                                                db.SaveChanges();

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

                                                                    UserLogin = Email,
                                                                    UserEmail = Email,
                                                                    Password = passencr,
                                                                    UserRoleId = 4,
                                                                    UserRoleLink = NewProvider.ProviderId,
                                                                    Enabled = true,
                                                                    Locked = false,
                                                                    ResetPassword = true,
                                                                };
                                                                db.Users.Add(NewUser);

                                                                db.SaveChanges();

                                                                // Update UserId In Provider Record
                                                                Provider ProviderResp = db.Providers.FirstOrDefault(p => p.ProviderId == NewProvider.ProviderId);

                                                                if (ProviderResp != null)
                                                                {
                                                                    ProviderResp.UserId = NewUser.UserId;
                                                                    db.SaveChanges();
                                                                }

                                                                // Create Link
                                                                var NewProviderLink = new ProviderFacilityLink()
                                                                {

                                                                    ProviderId = NewProvider.ProviderId,
                                                                    FacilityId = FacilityId,
                                                                    FacilityProviderId = FacilityProviderId,
                                                                    DateCreated = System.DateTime.Now,
                                                                };
                                                                db.ProviderFacilityLinks.Add(NewProviderLink);

                                                                db.SaveChanges();
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ProviderId = 0;
                                                //Valid = false;
                                                //ErrorMsg = "No provider id";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

                if (Valid)
                {
                    // CDA Body
                    XmlNode nodeComponent = xd.DocumentElement["component"];
                    XmlNode nodeStructure = nodeComponent["structuredBody"];

                    #region Visit Record
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
                                            VisitReason = nodeParagraph.InnerText;
                                        else
                                            VisitReason = nodeText.InnerText;

                                        XmlNode nodeComponentOf2 = xd.DocumentElement["componentOf"];
                                        XmlNode nodeEncompassingEncounter = nodeComponentOf2["encompassingEncounter"];
                                        XmlNode nodeEffectiveTime = nodeEncompassingEncounter["effectiveTime"];
                                        XmlNode nodeLow = nodeEffectiveTime["low"];
                                        VisitDate = nodeLow.Attributes.GetNamedItem("value").Value;
                                        break;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        if (VisitDate == "")
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
                                            if (VisitReason == "")
                                                VisitReason = nodeCode["originalText"].InnerText;
                                            XmlNode nodeEffectiveTime = nodeEncounter["effectiveTime"];
                                            try
                                            {
                                                VisitDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                            }
                                            catch
                                            {
                                                XmlNode nodeLow = nodeEffectiveTime["low"];
                                                if (nodeLow != null)
                                                    VisitDate = nodeLow.Attributes.GetNamedItem("value").Value;
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
                        if (VisitDate == "")
                        {
                            XmlNode nodeEffectiveTime = xd.DocumentElement["effectiveTime"];
                            VisitDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                            VisitReason = "Summarization of episode";
                        }

                        if (VisitId == 0)
                        {
                            // Get the next available visit id for the patient at the facility
                            //PatientVisit results = db.PatientVisits.FirstOrDefault(p => p.PatientId == AMRPatientId && p.FacilityId == FacilityId);

                            PatientVisit results = db.PatientVisits.OrderByDescending(i => i.VisitId).FirstOrDefault(p => p.PatientId == AMRPatientId && p.FacilityId == FacilityId);

                            if (results != null)
                            {
                                VisitId = results.VisitId;
                            }
                            VisitId++;  // Increment to next visit id.
                        }
                        if (VisitReason.Length > 256) VisitReason = VisitReason.Substring(0, 256);
                        
                        var Visit = new PatientVisit()
                        {
                            PatientId = AMRPatientId,
                            FacilityId = FacilityId,
                            VisitId = VisitId,
                            VisitDate = Convert.ToDateTime(VisitDate.Substring(4, 2) + "/" + VisitDate.Substring(6, 2) + "/" + VisitDate.Substring(0, 4)),
                            ProviderId = ProviderId,
                            VisitReason = VisitReason,
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
                    #endregion

                    // Loop through the CCD Body To Popluate Clinical Data
                    foreach (XmlNode node in nodeStructure)
                    {
                        if (node.Name == "component")
                        {
                            //XmlNode nodeComponent2 = node;
                            XmlNode nodeSections = node["section"];
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
                                                PatientId = AMRPatientId,
                                                FacilityId = FacilityId,
                                                VisitId = VisitId,
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
                                                        PatientId = AMRPatientId,
                                                        FacilityId = FacilityId,
                                                        VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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

                                                // Get The Date                                                
                                                string BeginDate = "";
                                                string EndDate = "";
                                                XmlNode nodeEffectiveTime = nodeAct["effectiveTime"];
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
                                                    if (BeginDate == "")
                                                    {
                                                        try
                                                        {
                                                            BeginDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                            if (BeginDate.Length > 8) BeginDate = BeginDate.Substring(0, 8);
                                                        }
                                                        catch
                                                        { }
                                                    }
                                                }
                                                if (BeginDate == "")
                                                {
                                                    nodeEffectiveTime = nodeObservation["effectiveTime"];
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
                                                        if (BeginDate == "")
                                                        {
                                                            try
                                                            {
                                                                BeginDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
                                                                if (BeginDate.Length > 8) BeginDate = BeginDate.Substring(0, 8);
                                                            }
                                                            catch
                                                            { }
                                                        }
                                                    }
                                                }
                                                if (BeginDate == "" && EndDate != "")
                                                    BeginDate = EndDate;

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
                                                        //if (nodeEntryRelationship2.Attributes.GetNamedItem("typeCode").Value != null && nodeEntryRelationship2.Attributes.GetNamedItem("typeCode").Value == "MFST")
                                                        //{
                                                        //    try
                                                        //    {
                                                        //        XmlNode nodeObservation2 = nodeEntryRelationship2["observation"];
                                                        //        XmlNode nodeValue2 = nodeObservation2["value"];
                                                        //        CodeValue2 = nodeValue2.Attributes.GetNamedItem("code").Value;
                                                        //        if (CodeValue2.Length > 20) CodeValue2 = CodeValue2.Substring(0, 20);
                                                        //        CodeSystemId2 = CodeSystemValue(nodeValue2.Attributes.GetNamedItem("codeSystem").Value);
                                                        //        Description2 = nodeValue2.Attributes.GetNamedItem("displayName").Value;
                                                        //        if (Description2.Length > 50) Description2 = Description2.Substring(0, 50);
                                                        //    }
                                                        //    catch { }
                                                        //}
                                                        //else if (nodeEntryRelationship2.Attributes.GetNamedItem("typeCode").Value == "REFR")
                                                        //{
                                                        //    if (StatusCode == 0)
                                                        //    {
                                                        //        XmlNode nodeObservation2 = nodeEntryRelationship2["observation"];
                                                        //        XmlNode nodeValue2 = nodeObservation2["value"];
                                                        //        StatusCode = CodeStatus(nodeValue2.Attributes.GetNamedItem("code").Value);
                                                        //    }
                                                        //}
                                                    }
                                                }

                                                // Write Allergy  Record
                                                var Allergy = new PatientAllergy()
                                                {
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
                                                    PatientAllergyCntr = 0,
                                                    CodeValue_Allergen = CodeValue,
                                                    CodeSystemId_Allergen = CodeSystemId,
                                                    Allergen = Description,
                                                    AllergenType = AllergenType,
                                                    CodeValue_Reaction = CodeValue2,
                                                    CodeSystemId_Reaction = CodeSystemId2,
                                                    Reaction = Description2,
                                                    EffectiveDate = BeginDate,
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

                                    #region Medications Administered During Visit  // SJF Added Section 5/8/14
                                    if (template == "2.16.840.1.113883.10.20.22.2.38")     // Medications Administered
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
                                                    PatientMedicationCntr = 0,
                                                    CodeValue = CodeValue,
                                                    CodeSystemId = CodeSystemId,
                                                    MedicationName = Description,
                                                    Active = true,
                                                    Quantity = 0,
                                                    RouteId = Route,
                                                    Dose = Dose,
                                                    DoseUnit = DoseUnit,
                                                    Refills = 0,
                                                    Frequency = Frequency,
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
                                    if (template == "2.16.840.1.113883.10.20.1.8" || template == "2.16.840.1.113883.10.20.22.2.1" || template == "2.16.840.1.113883.10.20.22.2.1.1")     // Medications   
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
                                                    PatientMedicationCntr = 0,
                                                    CodeValue = CodeValue,
                                                    CodeSystemId = CodeSystemId,
                                                    MedicationName = Description,
                                                    Active = true,
                                                    Quantity = 0,
                                                    RouteId = Route,
                                                    Dose = Dose,
                                                    DoseUnit = DoseUnit,
                                                    Refills = 0,
                                                    Frequency = Frequency,
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
                                            catch
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                OrderDate = nodeEffectiveTime.Attributes.GetNamedItem("value").Value;
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
                                                            if (nodeText != null)
                                                            {
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
                                                if (nodeOriginalText != null && nodeOriginalText.InnerText != "")
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                                PatientId = AMRPatientId,
                                                                FacilityId = FacilityId,
                                                                VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    {}
                                                }

                                                if (ProcDate.Length >= 8)
                                                    ProcDate = ProcDate.Substring(4, 2) + "/" + ProcDate.Substring(6, 2) + "/" + ProcDate.Substring(0, 4);
                                                else
                                                    ProcDate = "1/1/1900";



                                                var PlanOfCare = new PatientPlanOfCare()
                                                {
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    // No Codified Data - Use Text
                                                    if (Instruction == "")
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
                                                            Description = nodeText.InnerText;
                                                            if (Description.Length > 256) Instruction = Description.Substring(0, 256);
                                                        }
                                                        catch { }

                                                    }
                                                }
                                                var PlanOfCare = new PatientPlanOfCare()
                                                {
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                    PatientId = AMRPatientId,
                                                    FacilityId = FacilityId,
                                                    VisitId = VisitId,
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
                                                        PatientId = AMRPatientId,
                                                        FacilityId = FacilityId,
                                                        VisitId = VisitId,
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
                                                PatientId = AMRPatientId,
                                                FacilityId = FacilityId,
                                                VisitId = VisitId,
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
