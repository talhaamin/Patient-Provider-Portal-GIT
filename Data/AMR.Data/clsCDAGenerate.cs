// Service Name  : clsCDAGenerate
// Date Created  : 09/29/2014
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Create a CDA Formatted Document
//                  
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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace AMR.Data
{
    public class clsCDAGenerate
    {
        public bool createPatientCDA(Int64 PatientId, Int64 FacilityId, Int64 ProviderId, string Attachment, ref string CCA)
        {
           //byte[] imageBytes

            try
            {

                using (var db = new AMREntities())
                {
                    // Get Visit Information
                    Patient pResults = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);

                    Facility fResults = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityId);

                    Provider prResults = db.Providers.FirstOrDefault(p => p.ProviderId == ProviderId);

                    // Get Organization Name
                    string orgName = fResults.FacilityName;


                    //<ClinicalDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" moodCode="EVN" xmlns="urn:hl7-org:v3">
                    POCD_MT000040ClinicalDocument ccd2 = new POCD_MT000040ClinicalDocument();
                    ccd2.realmCode = new CS[1];
                    ccd2.realmCode[0] = new CS();
                    ccd2.realmCode[0].code = "US";

                    ccd2.typeId = new POCD_MT000040InfrastructureRoottypeId();
                    ccd2.typeId.root = "2.16.840.1.113883.1.3";
                    ccd2.typeId.extension = "POCD_HD000040";

                    ccd2.templateId = new II[1];
                    ccd2.templateId[0] = new II();
                    ccd2.templateId[0].root = "2.16.840.1.113883.10.20.19.1"; // <!-- Unstructure CDA -->
                    ccd2.templateId[0].extension = null;

                    ccd2.id = new II();
                    ccd2.id.root = Guid.NewGuid().ToString();

                    ccd2.code = new CE();
                    ccd2.code.code = "34133-9";
                    ccd2.code.codeSystem = "2.16.840.1.113883.6.1";
                    ccd2.code.codeSystemName = "LOINC";
                    ccd2.code.displayName = "Unstructured CDA With Attachment";

                    //<title>orgname Continuity of Care Document</title>
                    ccd2.title = new ST();
                    ccd2.title.Text = new string[] { orgName + " Unstructured CDA With Attachment" };

                    //<effectiveTime value="20111109552200-0700" />
                    int hours = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
                    int minutes = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Minutes;
                    string tzOffset = String.Format("{0,2:D2}{1,2:D2}", hours, minutes);
                    ccd2.effectiveTime = new TS();
                    ccd2.effectiveTime.value = DateTime.Now.ToString("yyyyMMddmmss") + "00" + tzOffset;

                    //<confidentialityCode code="N" codeSystem="2.16.840.1.113883.5.25" />
                    ccd2.confidentialityCode = new CE();
                    ccd2.confidentialityCode.code = "N";
                    ccd2.confidentialityCode.codeSystem = "2.16.840.1.113883.5.25";

                    //<languageCode code="en-US" />
                    ccd2.languageCode = new CS();
                    ccd2.languageCode.code = "en-US";

                    #region DocumentationOf

                    ccd2.documentationOf = new POCD_MT000040DocumentationOf[1];
                    ccd2.documentationOf[0] = new POCD_MT000040DocumentationOf();
                    ccd2.documentationOf[0].serviceEvent = new POCD_MT000040ServiceEvent();
                    ccd2.documentationOf[0].serviceEvent.classCode = "PCPR";

                    ccd2.documentationOf[0].serviceEvent.effectiveTime = new IVL_TS();
                    ccd2.documentationOf[0].serviceEvent.effectiveTime.ItemsElementName = new ItemsChoiceType2[2];
                    ccd2.documentationOf[0].serviceEvent.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                    ccd2.documentationOf[0].serviceEvent.effectiveTime.ItemsElementName[1] = ItemsChoiceType2.high;

                    ccd2.documentationOf[0].serviceEvent.effectiveTime.Items = new QTY[2];
                    ccd2.documentationOf[0].serviceEvent.effectiveTime.Items[0] = new IVXB_TS();
                    ((IVXB_TS)ccd2.documentationOf[0].serviceEvent.effectiveTime.Items[0]).value = pResults.DOB != null ? ((DateTime)pResults.DOB).ToString("yyyyMMddmmss") : ccd2.effectiveTime.value;
                    ccd2.documentationOf[0].serviceEvent.effectiveTime.Items[1] = new IVXB_TS();
                    ((IVXB_TS)ccd2.documentationOf[0].serviceEvent.effectiveTime.Items[1]).value = ccd2.effectiveTime.value;

                    ccd2.documentationOf[0].serviceEvent.performer = new POCD_MT000040Performer1[1];
                    ccd2.documentationOf[0].serviceEvent.performer[0] = new POCD_MT000040Performer1();
                    ccd2.documentationOf[0].serviceEvent.performer[0].typeCode = x_ServiceEventPerformer.PRF;
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity = new POCD_MT000040AssignedEntity();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.classCode = "ASSIGNED";
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.id = new II[1];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.id[0] = new II();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.id[0].nullFlavor = "NI";
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr = new AD[1];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0] = new AD();

                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items = new ADXP[4];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[0] = new adxpstreetAddressLine();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[0].Text = new string[] { (fResults.Address1 + " " + fResults.Address2).Trim() };
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[1] = new adxpcity();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[1].Text = new string[] { fResults.City };
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[2] = new adxpstate();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[2].Text = new string[] { fResults.State };
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[3] = new adxppostalCode();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[3].Text = new string[] { fResults.PostalCode };

                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom = new TEL[1];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0] = new TEL();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0].value = (fResults.Phone != null ? fResults.Phone : "").Trim();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson = new POCD_MT000040Person();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name = new PN[1];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0] = new PN();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items = new ENXP[2];
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[0] = new enfamily();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[0].Text = new string[] { prResults.LastName };
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[1] = new engiven();
                    ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[1].Text = new string[] { prResults.FirstName };

                    #endregion

                    #region Demographics - ok

                    ccd2.recordTarget = new POCD_MT000040RecordTarget[1];
                    ccd2.recordTarget[0] = new POCD_MT000040RecordTarget();
                    ccd2.recordTarget[0].patientRole = new POCD_MT000040PatientRole();
                    ccd2.recordTarget[0].patientRole.id = new II[1];
                    ccd2.recordTarget[0].patientRole.id[0] = new II();
                    ccd2.recordTarget[0].patientRole.id[0].root = "2.16.840.1.113883.19.5";
                    if (pResults.SSN != "")
                        ccd2.recordTarget[0].patientRole.id[0].extension = pResults.SSN;
                    ccd2.recordTarget[0].patientRole.addr = new AD[1];
                    ccd2.recordTarget[0].patientRole.addr[0] = new AD();
                    ccd2.recordTarget[0].patientRole.addr[0].use = new string[] { "H" };
                    ccd2.recordTarget[0].patientRole.addr[0].Items = new ADXP[5];
                    ccd2.recordTarget[0].patientRole.addr[0].Items[0] = new adxpstreetAddressLine();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[0].Text = new string[] { (pResults.Address1 + " " + pResults.Address2).Trim() };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[1] = new adxpcity();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[1].Text = new string[] { pResults.City };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[2] = new adxpstate();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[2].Text = new string[] { pResults.State };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[3] = new adxppostalCode();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[3].Text = new string[] { pResults.Zip };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[4] = new adxpcountry();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[4].Text = new string[] { pResults.CountryCode };
                    ccd2.recordTarget[0].patientRole.telecom = new TEL[1];
                    ccd2.recordTarget[0].patientRole.telecom[0] = new TEL();
                    if (pResults.HomePhone == "" && pResults.MobilePhone == "")
                        ccd2.recordTarget[0].patientRole.telecom[0].nullFlavor = "NI";
                    else
                        ccd2.recordTarget[0].patientRole.telecom[0].value = (pResults.HomePhone != null ? pResults.HomePhone : (pResults.MobilePhone != null ? pResults.MobilePhone : "")).Trim();
                    ccd2.recordTarget[0].patientRole.patient = new POCD_MT000040Patient();
                    ccd2.recordTarget[0].patientRole.patient.name = new PN[1];
                    ccd2.recordTarget[0].patientRole.patient.name[0] = new PN();
                    ccd2.recordTarget[0].patientRole.patient.name[0].Items = new ENXP[2];
                    ccd2.recordTarget[0].patientRole.patient.name[0].Items[0] = new enfamily();
                    ccd2.recordTarget[0].patientRole.patient.name[0].Items[0].Text = new string[] { pResults.LastName };
                    ccd2.recordTarget[0].patientRole.patient.name[0].Items[1] = new engiven();
                    ccd2.recordTarget[0].patientRole.patient.name[0].Items[1].Text = new string[] { pResults.FirstName };
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode = new CE();
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.nullFlavor = "NA";
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.code = pResults.GenderId != null ? (pResults.GenderId == 1 ? "M" : (pResults.GenderId == 2 ? "F" : "U")) : "U";
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.codeSystem = "2.16.840.1.113883.5.1";
                    ccd2.recordTarget[0].patientRole.patient.birthTime = new TS();
                    ccd2.recordTarget[0].patientRole.patient.birthTime.value = UnFormatDateTime(Convert.ToDateTime(pResults.DOB)).Substring(0,8);
                    if (pResults.RaceId_Native == true)
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.code = "1002-5";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.displayName = "American Indian or Alaska Native";
                    }
                    else if (pResults.RaceId_Asian == true)
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.code = "2028-9";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.displayName = "Asian";
                    }
                    else if (pResults.RaceId_Black == true)
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.code = "2054-5";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.displayName = "Black or African American";
                    }
                    else if (pResults.RaceId_Hawaiian == true)
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.code = "2076-8";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.displayName = "Native Hawaiian or Other Pacific Islander";
                    }
                    else if (pResults.RaceId_White == true)
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.code = "2106-3";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.raceCode.displayName = "White";
                    }
                    if (pResults.EthnicityId == 1)
                    {
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.code = "2135-2";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.displayName = "Hispanic or Latino";
                    }
                    else if (pResults.EthnicityId == 2)
                    {
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.code = "2186-5";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.codeSystem = "2.16.840.1.113883.6.238";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.codeSystemName = "Race and Ethnicity - CDC";
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.displayName = "Not Hispanic or Latino";
                    }


                    C_PreferredLanguage lResults = db.C_PreferredLanguage.FirstOrDefault(l => l.PreferredLanguageId == pResults.PreferredLanguageId);
                    if (lResults.ISO639_2 != null)
                    {
                        ccd2.recordTarget[0].patientRole.patient.languageCommunication = new POCD_MT000040LanguageCommunication[1];
                        ccd2.recordTarget[0].patientRole.patient.languageCommunication[0] = new POCD_MT000040LanguageCommunication();
                        ccd2.recordTarget[0].patientRole.patient.languageCommunication[0].languageCode = new CS();
                        ccd2.recordTarget[0].patientRole.patient.languageCommunication[0].languageCode.code = lResults.ISO639_2;
                    }
                 

                    #endregion

                    #region Author
                    // add in the author information
                    /*<author typeCode="AUT" contextControlCode="OP">
                        <time value="20111109552200-0700" />
                        <assignedAuthor nullFlavor="NA" classCode="ASSIGNED">
                          <id root="21cc0d32-4697-47c2-81bb-53797da3d89f" />
                          <representedOrganization classCode="ORG" determinerCode="INSTANCE">
                            <id nullFlavor="NI" />
                            <name>orgName</name>
                          </representedOrganization>
                        </assignedAuthor>
                      </author>*/
                    ccd2.author = new POCD_MT000040Author[1];
                    ccd2.author[0] = new POCD_MT000040Author();
                    ccd2.author[0].time = new TS();
                    ccd2.author[0].time.value = ccd2.effectiveTime.value;
                    ccd2.author[0].assignedAuthor = new POCD_MT000040AssignedAuthor();
                    ccd2.author[0].assignedAuthor.id = new II[1];
                    ccd2.author[0].assignedAuthor.id[0] = new II();
                    //ccd2.author[0].assignedAuthor.id[0].root = Guid.NewGuid().ToString();
                    ccd2.author[0].assignedAuthor.id[0].root = "2.16.840.1.113883.4.6";
                    ccd2.author[0].assignedAuthor.classCode = "ASSIGNED";


                    // add in the author organization
                    ccd2.author[0].assignedAuthor.representedOrganization = new POCD_MT000040Organization();
                    ccd2.author[0].assignedAuthor.representedOrganization.classCode = "ORG";
                    ccd2.author[0].assignedAuthor.representedOrganization.determinerCode = "INSTANCE";
                    ccd2.author[0].assignedAuthor.representedOrganization.id = new II[1];
                    ccd2.author[0].assignedAuthor.representedOrganization.id[0] = new II();
                    ccd2.author[0].assignedAuthor.representedOrganization.id[0].nullFlavor = "NI";
                    ccd2.author[0].assignedAuthor.representedOrganization.name = new ON[1];
                    ccd2.author[0].assignedAuthor.representedOrganization.name[0] = new ON();

                    ccd2.author[0].assignedAuthor.representedOrganization.name[0].Text = new string[] { orgName };

                    ccd2.author[0].assignedAuthor.addr = new AD[1];
                    ccd2.author[0].assignedAuthor.addr[0] = new AD();
                    ccd2.author[0].assignedAuthor.addr[0].use = new string[] { "WP" };
                    ccd2.author[0].assignedAuthor.addr[0].Items = new ADXP[5];
                    ccd2.author[0].assignedAuthor.addr[0].Items[0] = new adxpstreetAddressLine();
                    ccd2.author[0].assignedAuthor.addr[0].Items[0].Text = new string[] { (fResults.Address1 + " " + fResults.Address2).Trim() };
                    ccd2.author[0].assignedAuthor.addr[0].Items[1] = new adxpcity();
                    ccd2.author[0].assignedAuthor.addr[0].Items[1].Text = new string[] { fResults.City };
                    ccd2.author[0].assignedAuthor.addr[0].Items[2] = new adxpstate();
                    ccd2.author[0].assignedAuthor.addr[0].Items[2].Text = new string[] { fResults.State };
                    ccd2.author[0].assignedAuthor.addr[0].Items[3] = new adxppostalCode();
                    ccd2.author[0].assignedAuthor.addr[0].Items[3].Text = new string[] { fResults.PostalCode };
                    ccd2.author[0].assignedAuthor.addr[0].Items[4] = new adxpcountry();
                    ccd2.author[0].assignedAuthor.addr[0].Items[4].Text = new string[] { fResults.CountryCode };

                    ccd2.author[0].assignedAuthor.telecom = new TEL[1];
                    ccd2.author[0].assignedAuthor.telecom[0] = new TEL();
                    ccd2.author[0].assignedAuthor.telecom[0].use = new string[] { "WP" };
                    ccd2.author[0].assignedAuthor.telecom[0].value = (fResults.Phone != null ? fResults.Phone : "").Trim();
                    ccd2.author[0].assignedAuthor.assignedPerson = new POCD_MT000040Person[1];
                    ccd2.author[0].assignedAuthor.assignedPerson[0] = new POCD_MT000040Person();
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name = new PN[1];
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0] = new PN();
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items = new ENXP[2];
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[0] = new enfamily();
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[0].Text = new string[] { prResults.LastName };
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[1] = new engiven();
                    ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[1].Text = new string[] { prResults.FirstName };
                    
                  

                    // add in the custodian information
                    ccd2.custodian = new POCD_MT000040Custodian();
                    ccd2.custodian.assignedCustodian = new POCD_MT000040AssignedCustodian();

                    ccd2.custodian.assignedCustodian.representedCustodianOrganization = new POCD_MT000040CustodianOrganization();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.classCode = "ORG";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.determinerCode = "INSTANCE";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id = new II[1];
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id[0] = new II();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id[0].nullFlavor = "NI";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.name = new ON();

                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.name.Text = new string[] { orgName };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr = new AD();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.use = new string[] { "WP" };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items = new ADXP[5];
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[0] = new adxpstreetAddressLine();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[0].Text = new string[] { (fResults.Address1 + " " + fResults.Address2).Trim() };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[1] = new adxpcity();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[1].Text = new string[] { fResults.City };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[2] = new adxpstate();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[2].Text = new string[] { fResults.State };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[3] = new adxppostalCode();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[3].Text = new string[] { fResults.PostalCode };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[4] = new adxpcountry();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[4].Text = new string[] { fResults.CountryCode };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom = new TEL();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.use = new string[] { "WP" };
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.value = (fResults.Phone != null ? fResults.Phone : "").Trim();
                    
                    #endregion

                    // add in the outer component
                    //  <component>
                    ccd2.component = new POCD_MT000040Component2();

                    // add in the unstructured body

                    ccd2.component.Item = new POCD_MT000040NonXMLBody();
                    ((POCD_MT000040NonXMLBody)ccd2.component.Item).text = new ED();
                    ((POCD_MT000040NonXMLBody)ccd2.component.Item).text.mediaType = "application/pdf";
                    ((POCD_MT000040NonXMLBody)ccd2.component.Item).text.representation = BinaryDataEncoding.B64;
                    ((POCD_MT000040NonXMLBody)ccd2.component.Item).text.Text = new string[] { Attachment }; 
                    //Convert.ToBase64String(imageBytes)



                    try
                    {
                        // write out the ccd document
                       //Serialize<POCD_MT000040ClinicalDocument>(ccd2, @"c:\temp\testcda.xml");
                        CCA = Serialize<POCD_MT000040ClinicalDocument>(ccd2);
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }



        public static void Serialize<T>(T value, string pathName)
        {
            using (TextWriter writer = new StreamWriter(pathName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, value);
            }
        }

        public static string Serialize<T>(T value)
        {


            XmlSerializer serializer = new XmlSerializer(typeof(T));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = false;
            settings.OmitXmlDeclaration = false;

            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(xmlWriter, value);
                }
                return writer.ToString();
            }
        }


        public static T Deserialize<T>(string pathName)
        {
            using (TextReader reader = new StreamReader(pathName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(reader);
            }
        }
        private string FormatDate(string strIn)
        {
            if (strIn == null)
                strIn = "";
            string strDate = "";
            strIn = strIn.Trim();
            
            if (strIn.Length == 4)
                strDate = strIn;
            else if (strIn.Length == 6)
                strDate = strIn.Substring(4, 2) + "/" + strIn.Substring(0, 4);
            else if (strIn.Length >= 8)
                strDate = strIn.Substring(4, 2) + "/" + strIn.Substring(6, 2) + "/" + strIn.Substring(0, 4);

            return strDate.Trim();
        }
        private string UnFormatDateTime(DateTime dteIn)
        {
            string strDate = "";

            if (dteIn == null)
                strDate = "";
            else if (dteIn == Convert.ToDateTime("1/1/1900"))
                strDate = "";
            else
            {
                strDate = dteIn.ToString("yyyyMMddhhmm");
            }
            return strDate;
        }


    }
}
