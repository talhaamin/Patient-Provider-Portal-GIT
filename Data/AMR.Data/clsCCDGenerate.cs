// Service Name  : clsCreateDocument
// Date Created  : 11/30/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Create a CCDA Formatted Document
//                  Required Items - Medication Allergies, Medications, Problems, Immunizations, Lab Tests/Results??,  Plan of Care or Assessment and Plan
// MM/DD/YYYY XXX Description               
// 08/25/2014 SJF Added Family History
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
    public class clsCCDGenerate
    {
        public struct CCDParms
        {
            public bool Custom;
            public bool VisitReason;
            public bool Demographics;
            public bool Provider;
            public bool Problems;
            public bool Allergies;
            public bool Immunizations;
            public bool MedsAdministered;
            public bool Medications;
            public bool Social;
            public bool Procedures;
            public bool VitalSigns;
            public bool Labs;
            public bool PlanOfCare;
            //Added By Talha
            public bool ClinicalInstructions;
            public bool FamilyHist;
            //public bool FutureAppointments;
            //public bool Referrals;
            //public bool ScheduledTests;
            //public bool DecisionAids;
            public DataSet Selection;
        }
        public bool createPatientCCD(Int64 PatientId, Int64 FacilityId, Int64 VisitId, CCDParms Parms, ref string CCD)
        {
            DataTable dtProblems = new DataTable();
            DataTable dtAllergies = new DataTable();
            DataTable dtImmunizations = new DataTable();
            DataTable dtMedsAdministered = new DataTable();
            DataTable dtMedications = new DataTable();
            DataTable dtSocial = new DataTable();
            DataTable dtProcedures = new DataTable();
            DataTable dtVitalSigns = new DataTable();
            DataTable dtLabs = new DataTable();
            DataTable dtClinical = new DataTable();
            DataTable dtPlanOfCare = new DataTable();
            DataTable dtVisitReason = new DataTable();
            DataTable dtFamilyHist = new DataTable();

            // Check if customized & Set up datatables
            if (Parms.Custom)
            {
                dtProblems = Parms.Selection.Tables["Problems"];
                if (dtProblems.Rows.Count == 0) Parms.Problems = false;
                dtAllergies = Parms.Selection.Tables["Allergies"];
                if (dtAllergies.Rows.Count == 0) Parms.Allergies = false;
                dtImmunizations = Parms.Selection.Tables["Immunizations"];
                if (dtImmunizations.Rows.Count == 0) Parms.Immunizations = false;
                dtMedications = Parms.Selection.Tables["Medications"];
                if (dtMedications.Rows.Count == 0) Parms.Medications = false;
                dtMedsAdministered = Parms.Selection.Tables["MedsAdministered"];
                if (dtMedsAdministered.Rows.Count == 0) Parms.MedsAdministered = false;
                dtSocial = Parms.Selection.Tables["Social"];
                if (dtSocial.Rows.Count == 0) Parms.Social = false;
                dtProcedures = Parms.Selection.Tables["Procedures"];
                if (dtProcedures.Rows.Count == 0) Parms.Procedures = false;
                dtVitalSigns = Parms.Selection.Tables["VitalSigns"];
                if (dtVitalSigns.Rows.Count == 0) Parms.VitalSigns = false;
                dtLabs = Parms.Selection.Tables["LabResults"];
                if (dtLabs.Rows.Count == 0) Parms.Labs = false;
                dtVisitReason = Parms.Selection.Tables["VisitReason"];
                if (dtVisitReason.Rows.Count == 0) Parms.VisitReason = false;
                //Added By Talha
                dtClinical = Parms.Selection.Tables["Clinical"];
                if (dtClinical.Rows.Count == 0) Parms.ClinicalInstructions = false;
                dtPlanOfCare = Parms.Selection.Tables["POC"];
                if (dtPlanOfCare.Rows.Count == 0)
                {
                    Parms.PlanOfCare = false;
                    //Added By Talha
                    Parms.ClinicalInstructions = false;
                    //Parms.FutureAppointments = false;
                    //Parms.Referrals = false;
                    //Parms.ScheduledTests = false;
                    //Parms.DecisionAids = false;
                }
                if (dtFamilyHist.Rows.Count == 0) Parms.FamilyHist = false;
                dtFamilyHist = Parms.Selection.Tables["FamilyHist"];
            }
            else
            {
                Parms.VisitReason = true;
                Parms.Demographics = true;
                Parms.Provider = true;
                Parms.Problems = true;
                Parms.Allergies = true;
                Parms.Immunizations = true;
                Parms.MedsAdministered = true;
                Parms.Medications = true;
                Parms.Social = true;
                Parms.Procedures = true;
                Parms.VitalSigns = true;
                Parms.Labs = true;
                Parms.PlanOfCare = true;
                Parms.ClinicalInstructions = true;
                Parms.FamilyHist = true;
                Parms.FamilyHist = true;
            }


            try
            {
                // example of how to read in a CCD and output it back to another file
                //POCD_MT000040ClinicalDocument ccd = Deserialize<POCD_MT000040ClinicalDocument>(@"C:\Temp\CCDstuff\testccd.xml");
                //Serialize<POCD_MT000040ClinicalDocument>(ccd, @"C:\Temp\CCDstuff\testccd-output.xml");

                using (var db = new AMREntities())
                {
                    Patient pResults = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);
                    PatientVisit vResults = db.PatientVisits.FirstOrDefault(v => v.PatientId == PatientId && v.FacilityId == FacilityId && v.VisitId == VisitId);

                    //Facility fResults = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityId);

                    vwVisitCCD vwResults = db.vwVisitCCDs.FirstOrDefault(v => v.PatientId == PatientId && v.FacilityId == FacilityId && v.VisitId == VisitId);
                    string orgName = vwResults.FacilityName;


                    //Demographic d = db.SelectFirst<Demographic>("id=" + PatientId);
                    // new document
                    //<ClinicalDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" moodCode="EVN" xmlns="urn:hl7-org:v3">
                    POCD_MT000040ClinicalDocument ccd2 = new POCD_MT000040ClinicalDocument();
                    ccd2.realmCode = new CS[1];
                    ccd2.realmCode[0] = new CS();
                    ccd2.realmCode[0].code = "US";

                    ccd2.typeId = new POCD_MT000040InfrastructureRoottypeId();
                    ccd2.typeId.root = "2.16.840.1.113883.1.3";
                    ccd2.typeId.extension = "POCD_HD000040";

                    ccd2.templateId = new II[2];
                    ccd2.templateId[0] = new II();
                    ccd2.templateId[0].root = "2.16.840.1.113883.10.20.22.1.1"; // <!-- CCD v1.0 Templates Root -->
                    ccd2.templateId[0].extension = null;
                    ccd2.templateId[1] = new II();
                    ccd2.templateId[1].root = "2.16.840.1.113883.10.20.22.1.2"; // <!-- CCD v1.0 Templates Root -->
                    ccd2.templateId[1].extension = null;

                    //<id root="b668c628-7e2f-4393-8615-d5f5e1c41d04" />
                    ccd2.id = new II();
                    ccd2.id.root = Guid.NewGuid().ToString();

                    //<code code="34133-9" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" displayName="Summarization of episode note" />
                    ccd2.code = new CE();
                    ccd2.code.code = "34133-9";
                    ccd2.code.codeSystem = "2.16.840.1.113883.6.1";
                    ccd2.code.codeSystemName = "LOINC";
                    ccd2.code.displayName = "Summarization of episode note";

                    //<title>orgname Continuity of Care Document</title>
                    ccd2.title = new ST();
                    ccd2.title.Text = new string[] { orgName + " Continuity of Care Document" };

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
                    /*<documentationOf typeCode="DOC">
                        <serviceEvent classCode="PCPR" moodCode="EVN">
                          <effectiveTime>
                            <low value="200510200000" />
                            <high value="20111109552200-0700" />
                          </effectiveTime>
                        </serviceEvent>
                      </documentationOf>*/
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

                    // SJF 4/28/14 Added Block
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

                    if (!Parms.Custom || Parms.Provider)    // SJF 5/5/14
                    {
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items = new ADXP[4];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[0] = new adxpstreetAddressLine();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[1] = new adxpcity();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[1].Text = new string[] { vwResults.City };
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[2] = new adxpstate();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[2].Text = new string[] { vwResults.State };
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[3] = new adxppostalCode();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };

                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom = new TEL[1];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0] = new TEL();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson = new POCD_MT000040Person();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name = new PN[1];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0] = new PN();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items = new ENXP[2];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[0] = new enfamily();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[0].Text = new string[] { vwResults.LastName };
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[1] = new engiven();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].Items[1].Text = new string[] { vwResults.FirstName };
                    }
                    else
                    {
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.addr[0].nullFlavor = "NA";

                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom = new TEL[1];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0] = new TEL();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.telecom[0].nullFlavor = "NA";
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson = new POCD_MT000040Person();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name = new PN[1];
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0] = new PN();
                        ccd2.documentationOf[0].serviceEvent.performer[0].assignedEntity.assignedPerson.name[0].nullFlavor = "NA";
                    }
                    // End Block
                    #endregion

                    #region Demographics - ok
                    // add in the record target
                    /*<recordTarget typeCode="RCT" contextControlCode="OP">
                        <patientRole classCode="PAT">
                          <id root="2.16.840.1.113883.19.5" extension="555111666" />
                          <addr use="H">
                            <streetAddressLine>1 Main Street</streetAddressLine>
                            <city>Boulder</city>
                            <state>CO</state>
                            <postalCode>80301</postalCode>
                          </addr>
                          <telecom value="(303)555-1212" />
                          <patient classCode="PSN" determinerCode="INSTANCE">
                            <name>
                              <family>Smith</family>
                              <given>Jane</given>
                            </name>
                            <administrativeGenderCode code="F" codeSystem="2.16.840.1.113883.5.1" />
                            <birthTime value="20111109" />
                          </patient>
                        </patientRole>
                      </recordTarget>*/
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
                    if (Parms.Custom && !Parms.Demographics)    // SJF 5/2/14 - Added block
                    {
                        ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.nullFlavor = "NA";
                    }
                    else
                    {
                        ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.code = pResults.GenderId != null ? (pResults.GenderId == 1 ? "M" : (pResults.GenderId == 2 ? "F" : "U")) : "U";
                        ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.codeSystem = "2.16.840.1.113883.5.1";
                    }
                    if (Parms.Custom && !Parms.Demographics)    // SJF 5/2/14 - Added block
                    {
                        ccd2.recordTarget[0].patientRole.patient.birthTime = new TS();
                        ccd2.recordTarget[0].patientRole.patient.birthTime.nullFlavor = "NA";
                    }
                    else
                    {
                        ccd2.recordTarget[0].patientRole.patient.birthTime = new TS();
                        ccd2.recordTarget[0].patientRole.patient.birthTime.value = UnFormatDateTime(Convert.ToDateTime(pResults.DOB)).Substring(0,8);
                    }
                    if (Parms.Custom && !Parms.Demographics)    // SJF 5/2/14 - Added block
                    {
                        ccd2.recordTarget[0].patientRole.patient.raceCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.raceCode.nullFlavor = "NA";
                    }
                    else if (pResults.RaceId_Native == true)
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
                    if (Parms.Custom && !Parms.Demographics)    // SJF 5/2/14 - Added block
                    {
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode = new CE();
                        ccd2.recordTarget[0].patientRole.patient.ethnicGroupCode.nullFlavor = "NA";
                    }
                    else if (pResults.EthnicityId == 1)
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

                    if (Parms.Custom && !Parms.Demographics)    // SJF 5/2/14 - Added block
                    {
                    }
                    else
                    {
                        C_PreferredLanguage lResults = db.C_PreferredLanguage.FirstOrDefault(l => l.PreferredLanguageId == pResults.PreferredLanguageId);
                        if (lResults.ISO639_2 != null)
                        {
                            ccd2.recordTarget[0].patientRole.patient.languageCommunication = new POCD_MT000040LanguageCommunication[1];
                            ccd2.recordTarget[0].patientRole.patient.languageCommunication[0] = new POCD_MT000040LanguageCommunication();
                            ccd2.recordTarget[0].patientRole.patient.languageCommunication[0].languageCode = new CS();
                            ccd2.recordTarget[0].patientRole.patient.languageCommunication[0].languageCode.code = lResults.ISO639_2;
                        }
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

                    if (!Parms.Custom || Parms.Provider)    // SJF 5/2/14
                    {
                        ccd2.author[0].assignedAuthor.representedOrganization.name[0].Text = new string[] { orgName };

                        ccd2.author[0].assignedAuthor.addr = new AD[1];
                        ccd2.author[0].assignedAuthor.addr[0] = new AD();
                        ccd2.author[0].assignedAuthor.addr[0].use = new string[] { "WP" };
                        ccd2.author[0].assignedAuthor.addr[0].Items = new ADXP[5];
                        ccd2.author[0].assignedAuthor.addr[0].Items[0] = new adxpstreetAddressLine();
                        ccd2.author[0].assignedAuthor.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                        ccd2.author[0].assignedAuthor.addr[0].Items[1] = new adxpcity();
                        ccd2.author[0].assignedAuthor.addr[0].Items[1].Text = new string[] { vwResults.City };
                        ccd2.author[0].assignedAuthor.addr[0].Items[2] = new adxpstate();
                        ccd2.author[0].assignedAuthor.addr[0].Items[2].Text = new string[] { vwResults.State };
                        ccd2.author[0].assignedAuthor.addr[0].Items[3] = new adxppostalCode();
                        ccd2.author[0].assignedAuthor.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };
                        ccd2.author[0].assignedAuthor.addr[0].Items[4] = new adxpcountry();
                        ccd2.author[0].assignedAuthor.addr[0].Items[4].Text = new string[] { vwResults.CountryCode };

                        ccd2.author[0].assignedAuthor.telecom = new TEL[1];
                        ccd2.author[0].assignedAuthor.telecom[0] = new TEL();
                        ccd2.author[0].assignedAuthor.telecom[0].use = new string[] { "WP" };
                        ccd2.author[0].assignedAuthor.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();
                        ccd2.author[0].assignedAuthor.assignedPerson = new POCD_MT000040Person[1];
                        ccd2.author[0].assignedAuthor.assignedPerson[0] = new POCD_MT000040Person();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name = new PN[1];
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0] = new PN();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items = new ENXP[2];
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[0] = new enfamily();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[0].Text = new string[] { vwResults.LastName };
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[1] = new engiven();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].Items[1].Text = new string[] { vwResults.FirstName };
                    }
                    else
                    {
                        ccd2.author[0].assignedAuthor.representedOrganization.name[0].Text = new string[] { "" };

                        ccd2.author[0].assignedAuthor.addr = new AD[1];
                        ccd2.author[0].assignedAuthor.addr[0] = new AD();
                        ccd2.author[0].assignedAuthor.addr[0].use = new string[] { "WP" };
                        ccd2.author[0].assignedAuthor.addr[0].nullFlavor = "NA";

                        ccd2.author[0].assignedAuthor.telecom = new TEL[1];
                        ccd2.author[0].assignedAuthor.telecom[0] = new TEL();
                        ccd2.author[0].assignedAuthor.telecom[0].use = new string[] { "WP" };
                        ccd2.author[0].assignedAuthor.telecom[0].nullFlavor = "NA";

                        ccd2.author[0].assignedAuthor.assignedPerson = new POCD_MT000040Person[1];
                        ccd2.author[0].assignedAuthor.assignedPerson[0] = new POCD_MT000040Person();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name = new PN[1];
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0] = new PN();
                        ccd2.author[0].assignedAuthor.assignedPerson[0].name[0].nullFlavor = "NA";
                    }

                    //ccd2.author[0].assignedAuthor.representedOrganization.addr = new AD[1];
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0] = new AD();
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].use = new string[] { "WP" };
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items = new ADXP[4];
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[0] = new adxpstreetAddressLine();
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[1] = new adxpcity();
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[1].Text = new string[] { vwResults.City };
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[2] = new adxpstate();
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[2].Text = new string[] { vwResults.State };
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[3] = new adxppostalCode();
                    //ccd2.author[0].assignedAuthor.representedOrganization.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };
                    //ccd2.author[0].assignedAuthor.representedOrganization.telecom = new TEL[1];
                    //ccd2.author[0].assignedAuthor.representedOrganization.telecom[0] = new TEL();
                    //ccd2.author[0].assignedAuthor.representedOrganization.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();



                    //ccd2.author[0].assignedAuthor.nullFlavor = "NA";

                    //// add in the informant information
                    ///* <informant typeCode="INF" contextControlCode="OP">
                    //    <assignedEntity classCode="ASSIGNED">
                    //      <id nullFlavor="NI" />
                    //      <representedOrganization classCode="ORG" determinerCode="INSTANCE">
                    //        <id nullFlavor="NI" />
                    //        <name>orgName</name>
                    //      </representedOrganization>
                    //    </assignedEntity>
                    //  </informant>*/
                    //ccd2.informant = new POCD_MT000040Informant12[1];
                    //ccd2.informant[0] = new POCD_MT000040Informant12();
                    //ccd2.informant[0].Item = new POCD_MT000040AssignedEntity();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id = new II[1];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0] = new II();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0].nullFlavor = "NI"; // no organization id
                    ////((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0].root = "id would go here";

                    //// add in the informant organization information
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization = new POCD_MT000040Organization();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.classCode = "ORG";
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.determinerCode = "INSTANCE";
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id = new II[1];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id[0] = new II();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id[0].nullFlavor = "NI";
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name = new ON[1];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name[0] = new ON();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name[0].Text = new string[] { orgName };

                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr = new AD[1];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0] = new AD();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].use = new string[] { "WP" };
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items = new ADXP[4];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[0] = new adxpstreetAddressLine();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[1] = new adxpcity();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[1].Text = new string[] { vwResults.City };
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[2] = new adxpstate();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[2].Text = new string[] { vwResults.State };
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[3] = new adxppostalCode();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.telecom = new TEL[1];
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.telecom[0] = new TEL();
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();




                    // add in the custodian information
                    ccd2.custodian = new POCD_MT000040Custodian();
                    ccd2.custodian.assignedCustodian = new POCD_MT000040AssignedCustodian();

                    // add in the custodian organization information
                    /*<custodian typeCode="CST">
                        <assignedCustodian classCode="ASSIGNED">
                          <representedCustodianOrganization classCode="ORG" determinerCode="INSTANCE">
                            <id nullFlavor="NI" />
                            <name>orgName</name>
                          </representedCustodianOrganization>
                        </assignedCustodian>
                      </custodian>*/
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization = new POCD_MT000040CustodianOrganization();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.classCode = "ORG";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.determinerCode = "INSTANCE";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id = new II[1];
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id[0] = new II();
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.id[0].nullFlavor = "NI";
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.name = new ON();
                    if (!Parms.Custom || Parms.Provider)    // SJF 5/5/14
                    {
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.name.Text = new string[] { orgName };
                        //ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr = new AD;
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr = new AD();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.use = new string[] { "WP" };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items = new ADXP[5];
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[0] = new adxpstreetAddressLine();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[1] = new adxpcity();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[1].Text = new string[] { vwResults.City };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[2] = new adxpstate();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[2].Text = new string[] { vwResults.State };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[3] = new adxppostalCode();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[3].Text = new string[] { vwResults.PostalCode };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[4] = new adxpcountry();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.Items[4].Text = new string[] { vwResults.CountryCode };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom = new TEL();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.use = new string[] { "WP" };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();
                    }
                    else
                    {
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.name.Text = new string[] { "" };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr = new AD();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.use = new string[] { "WP" };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.addr.nullFlavor = "NA";

                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom = new TEL();
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.use = new string[] { "WP" };
                        ccd2.custodian.assignedCustodian.representedCustodianOrganization.telecom.nullFlavor = "NA";
                    }
                    //// add in the legal authenticator
                    ///*<legalAuthenticator typeCode="LA" contextControlCode="OP">
                    //    <time value="20111109552200-0700" />
                    //    <signatureCode code="S" />
                    //    <assignedEntity classCode="ASSIGNED">
                    //      <id nullFlavor="NI" />
                    //      <representedOrganization classCode="ORG" determinerCode="INSTANCE">
                    //        <id nullFlavor="NI" />
                    //        <name>orgName</name>
                    //      </representedOrganization>
                    //    </assignedEntity>
                    //  </legalAuthenticator>*/
                    //ccd2.legalAuthenticator = new POCD_MT000040LegalAuthenticator();
                    //ccd2.legalAuthenticator.time = new TS();
                    //ccd2.legalAuthenticator.time.value = ccd2.effectiveTime.value;
                    //ccd2.legalAuthenticator.signatureCode = new CS();
                    //ccd2.legalAuthenticator.signatureCode.code = "S";
                    //ccd2.legalAuthenticator.assignedEntity = new POCD_MT000040AssignedEntity();
                    //ccd2.legalAuthenticator.assignedEntity.id = new II[1];
                    //ccd2.legalAuthenticator.assignedEntity.id[0] = new II();
                    //ccd2.legalAuthenticator.assignedEntity.id[0].nullFlavor = "NI";
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization = new POCD_MT000040Organization();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.id = new II[1];
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0] = new II();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0].nullFlavor = "NI"; // no organization id
                    ////ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0].root = "id would go here";
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.name = new ON[1];
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.name[0] = new ON();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.name[0].Text = new string[] { orgName };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr = new AD[1];
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0] = new AD();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].use = new string[] { "WP" };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items = new ADXP[4];
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[0] = new adxpstreetAddressLine();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[1] = new adxpcity();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[1].Text = new string[] { vwResults.City };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[2] = new adxpstate();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[2].Text = new string[] { vwResults.State };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[3] = new adxppostalCode();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.telecom = new TEL[1];
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.telecom[0] = new TEL();
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();

                    #endregion

                    #region Encompassing Encounter
                    // add in the encompassing encounter information
                    ccd2.componentOf = new POCD_MT000040Component1();
                    ccd2.componentOf.encompassingEncounter = new POCD_MT000040EncompassingEncounter();
                    ccd2.componentOf.encompassingEncounter.id = new II[1];
                    ccd2.componentOf.encompassingEncounter.id[0] = new II();
                    ccd2.componentOf.encompassingEncounter.id[0].extension = VisitId.ToString();
                    ccd2.componentOf.encompassingEncounter.id[0].root = Guid.NewGuid().ToString();
                    ccd2.componentOf.encompassingEncounter.effectiveTime = new IVL_TS();
                    ccd2.componentOf.encompassingEncounter.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                    ccd2.componentOf.encompassingEncounter.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                    ccd2.componentOf.encompassingEncounter.effectiveTime.Items = new QTY[1];
                    ccd2.componentOf.encompassingEncounter.effectiveTime.Items[0] = new IVXB_TS();
                    ((IVXB_TS)ccd2.componentOf.encompassingEncounter.effectiveTime.Items[0]).value = Convert.ToDateTime(vResults.VisitDate).ToString("yyyyMMdd");


                    // SJF 5/24/2014 SJF Added location

                    ccd2.componentOf.encompassingEncounter.location = new POCD_MT000040Location();
                    ccd2.componentOf.encompassingEncounter.location.healthCareFacility = new POCD_MT000040HealthCareFacility();
                    ccd2.componentOf.encompassingEncounter.location.healthCareFacility.id = new II[1];
                    ccd2.componentOf.encompassingEncounter.location.healthCareFacility.id[0] = new II();
                    ccd2.componentOf.encompassingEncounter.location.healthCareFacility.id[0].root = "2.16.840.1.113883.10.20.22.2.22";

                    if (!Parms.Custom || Parms.Provider)
                    {
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location = new POCD_MT000040Place();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.name = new EN();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.name.Text = new string[] { vwResults.FacilityName };
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr = new AD();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items = new ADXP[5];
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[0] = new adxpstreetAddressLine();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[1] = new adxpcity();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[1].Text = new string[] { vwResults.City };
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[2] = new adxpstate();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[2].Text = new string[] { vwResults.State };
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[3] = new adxppostalCode();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[3].Text = new string[] { vwResults.PostalCode };
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[4] = new adxpcountry();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.Items[4].Text = new string[] { vwResults.CountryCode };
                    }
                    else
                    {
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location = new POCD_MT000040Place();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.name = new EN();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.name.nullFlavor = "NA";
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr = new AD();
                        ccd2.componentOf.encompassingEncounter.location.healthCareFacility.location.addr.nullFlavor = "NA";
                    }

                    ccd2.componentOf.encompassingEncounter.responsibleParty = new POCD_MT000040ResponsibleParty();
                    ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity = new POCD_MT000040AssignedEntity();
                    ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.id = new II[1];
                    ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.id[0] = new II();
                    ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.id[0].root = Guid.NewGuid().ToString();

                    if (!Parms.Custom || Parms.Provider)    // SJF 5/5/14
                    {
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr = new AD[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0] = new AD();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items = new ADXP[5];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[0] = new adxpstreetAddressLine();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[0].Text = new string[] { (vwResults.Address1 + " " + vwResults.Address2).Trim() };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[1] = new adxpcity();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[1].Text = new string[] { vwResults.City };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[2] = new adxpstate();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[2].Text = new string[] { vwResults.State };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[3] = new adxppostalCode();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[3].Text = new string[] { vwResults.PostalCode };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[4] = new adxpcountry();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].Items[4].Text = new string[] { vwResults.CountryCode };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom = new TEL[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0] = new TEL();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0].use = new string[] { "WP" };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0].value = (vwResults.FacilityPhone != null ? vwResults.FacilityPhone : "").Trim();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson = new POCD_MT000040Person();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name = new PN[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0] = new PN();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0].Items = new ENXP[2];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0].Items[0] = new enfamily();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0].Items[0].Text = new string[] { vwResults.LastName };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0].Items[1] = new engiven();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.assignedPerson.name[0].Items[1].Text = new string[] { vwResults.FirstName };
                    }
                    else
                    {
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr = new AD[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0] = new AD();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr = new AD[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.addr[0].nullFlavor = "NA";

                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom = new TEL[1];
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0] = new TEL();
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0].use = new string[] { "WP" };
                        ccd2.componentOf.encompassingEncounter.responsibleParty.assignedEntity.telecom[0].nullFlavor = "NA";
                    }

                    #endregion

                    // add in the outer component
                    //  <component>
                    ccd2.component = new POCD_MT000040Component2();

                    POCD_MT000040Component3[] newComponentArray; //  SJF 4/29/14 Moved out of sections to top
                    POCD_MT000040Section section; //  SJF 4/29/14 Moved out of sections to top

                    // add in the structured body
                    //    <structuredBody classCode="DOCBODY" moodCode="EVN">
                    ccd2.component.Item = new POCD_MT000040StructuredBody();
                    

                    int componentIndex = 0;

                    #region Reason For Visit
                    //*****************************************************
                    // add in the Reason For Visit section
                    // SJF Added 5/7/14
                    //*****************************************************
                    if (Parms.VisitReason)
                    {
                        // copy over any existing components and expand the component array by one
                        newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();

                        section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.12";
                        //<code code="10160-0" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                        section.code = new CE();
                        section.code.code = "29299-5";
                        section.code.codeSystem = "2.16.840.1.113883.6.1";
                        section.code.codeSystemName = "LOINC";
                        //<title>Medications</title>
                        section.title = new ST();
                        section.title.Text = new string[] { "Reason For Visit/Chief Complaint" };

                        section.text = new StrucDocText();

                        if (Parms.VisitReason == true && dtVisitReason.Rows.Count > 0)
                        {
                            DataRow dr = dtVisitReason.Rows[0];
                            section.text.Text = new string[] { dr["VisitReason"].ToString() };
                        }
                        else
                        {
                            section.text.Text = new string[] { vwResults.VisitReason };
                        }



                        componentIndex++;
                    }
                    else
                    {
                        // copy over any existing components and expand the component array by one
                        newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();

                        section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.12";
                        //<code code="10160-0" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                        section.code = new CE();
                        section.code.code = "29299-5";
                        section.code.codeSystem = "2.16.840.1.113883.6.1";
                        section.code.codeSystemName = "LOINC";
                        //<title>Medications</title>
                        section.title = new ST();
                        section.title.Text = new string[] { "Reason For Visit/Chief Complaint" };

                        section.text = new StrucDocText();
                        section.text.Text = new string[] { "" };

                        componentIndex++;
                    }
                    #endregion

                    #region Medications Administered
                    //*****************************************************
                    // add in the medications administered section
                    // SJF 5/7/14
                    //*****************************************************

                    List<PatientMedication> medsa = db.PatientMedications.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.Deleted == false && p.Active == true && p.DuringVisit == true).ToList();
                    // Check if there are any procedures selected, if not set the flag to false
                    if (medsa.Count == 0)
                        Parms.MedsAdministered = false;

                    // copy over any existing components and expand the component array by one
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.38";  
                    section.code = new CE();
                    section.code.code = "29549-3";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Medications</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Medications Administered" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    if (Parms.MedsAdministered)
                    {
                        // add in the HTML for the medications

                        /*  <table width="100%" border="1">
                            <thead>
                            <tr>
                                <th>RxNorm Code</th>
                                <th>Product</th>
                                <th>Generic Name</th>
                                <th>Brand Name</th>
                                <th>Strength</th>
                                <th>Dose</th>
                                //<th>Route</th>
                                <th>Frequency</th>
                                <th>Date Started</th>
                                <th>Status</th>
                            </tr>
                            </thead>
                            <tbody>*/
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[10];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "RxNorm Code" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Product" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Medication Name" };
                        //table.thead.tr[0].Items[3] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Brand Name" };
                        //table.thead.tr[0].Items[4] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Strength" };
                        //table.thead.tr[0].Items[5] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Dose" };
                        //table.thead.tr[0].Items[3] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Route" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Frequency" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Date Started" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Status" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Notes" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[medsa.Count];
                        /*       <tr>
                                    <td>205875</td>
                                    <td>Medication</td>
                                    <td>glyburide</td>
                                    <td>Diabeta</td>
                                    <td>2.5 mg</td>
                                    <td>2.5</td>
                                    <td>PO</td>
                                    <td>Q AM</td>
                                    <td>11/4/2011</td>
                                    <td>Active</td>
                                </tr>*/
                        for (int mi = 0; mi < medsa.Count; mi++)
                        {
                            PatientMedication med = medsa.ElementAt<PatientMedication>(mi);
                            if (Parms.Custom == false || dtMedsAdministered.Rows.Contains(med.PatientMedicationCntr))
                            {
                                //CodedMedication med = meds.ElementAt<CodedMedication>(mi);
                                table.tbody[0].tr[mi] = new StrucDocTr();
                                table.tbody[0].tr[mi].Items = new object[10];
                                table.tbody[0].tr[mi].Items[0] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[0]).Text = new string[] { med.CodeValue };
                                table.tbody[0].tr[mi].Items[1] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[1]).Text = new string[] { "Medication" };
                                table.tbody[0].tr[mi].Items[2] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[2]).Text = new string[] { med.MedicationName };
                                //table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.BrandName };
                                //table.tbody[0].tr[mi].Items[4] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[4]).Text = new string[] { med.Strength };
                                //table.tbody[0].tr[mi].Items[5] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[5]).Text = new string[] { med.Dose };
                                //table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.RouteId };
                                table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.Frequency };
                                table.tbody[0].tr[mi].Items[4] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[4]).Text = new string[] { med.StartDate != null ? ((DateTime)med.StartDate).ToShortDateString() : "" };
                                table.tbody[0].tr[mi].Items[5] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[5]).Text = new string[] { med.Status == null ? "" : med.Status };
                                table.tbody[0].tr[mi].Items[6] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[6]).Text = new string[] { med.Note };
                            }
                        }
                    }
                    // add in the stuctured entries for the medications

                    if (Parms.MedsAdministered)
                    {
                        /*     <entry typeCode="DRIV">
                                <substanceAdministration classCode="SBADM" moodCode="EVN">
                                    <templateId root="2.16.840.1.113883.10.20.1.24" />
                                    <id root="4df4c348-dfbc-4428-8ccd-0aaf84862850" />
                                    <statusCode code="Active" />
                                    <effectiveTime xsi:type="PIVL_TS" value="20111104" />
                                    <routeCode code="PO" codeSystem="2.16.840.1.113883.5.112" displayName="PO" />
                                    <doseQuantity value="2.5" unit="mg" />
                                    <consumable typeCode="CSM">
                                    <manufacturedProduct>
                                        <templateId root="2.16.840.1.113883.10.20.1.53" />
                                        <manufacturedMaterial classCode="MMAT">
                                        <code code="205875" codeSystem="2.16.840.1.113883.6.88" displayName="Diabeta">
                                            <originalText>Diabeta</originalText>
                                        </code>
                                        <name>Diabeta</name>
                                        </manufacturedMaterial>
                                    </manufacturedProduct>
                                    </consumable>
                                </substanceAdministration>
                                </entry> */

                        int icount = medsa.Count;
                        if (Parms.Custom) icount = dtMedsAdministered.Rows.Count;
                        int mia = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int mi2 = 0; mi2 < medsa.Count; mi2++)
                        {
                            PatientMedication med = medsa.ElementAt<PatientMedication>(mi2);
                            if (Parms.Custom == false || dtMedsAdministered.Rows.Contains(med.PatientMedicationCntr))
                            // End Block
                            {
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mia] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mia].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mia].Item = new POCD_MT000040SubstanceAdministration();
                                POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mia].Item;
                                sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                                sadmin.templateId = new II[1];
                                sadmin.templateId[0] = new II();
                                sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.16";
                                sadmin.id = new II[1];
                                sadmin.id[0] = new II();
                                sadmin.id[0].root = Guid.NewGuid().ToString();
                                sadmin.text = new ED();

                                sadmin.text.Text = new string[] { med.MedicationName };
                                sadmin.statusCode = new CS();
                                sadmin.statusCode.code = med.Status == null ? "" : med.Status;

                                sadmin.effectiveTime = new IVL_TS[1];
                                sadmin.effectiveTime[0] = new IVL_TS();
                                IVL_TS tmstamp = new IVL_TS();
                                tmstamp.ItemsElementName = new ItemsChoiceType2[2];
                                tmstamp.ItemsElementName[0] = ItemsChoiceType2.low;
                                tmstamp.ItemsElementName[1] = ItemsChoiceType2.high;
                                tmstamp.Items = new QTY[2];
                                tmstamp.Items[0] = new IVXB_TS();
                                ((IVXB_TS)tmstamp.Items[0]).value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                                tmstamp.Items[1] = new IVXB_TS();
                                ((IVXB_TS)tmstamp.Items[1]).value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                                sadmin.effectiveTime[0] = tmstamp;

                                if (med.RouteId.Trim() != "")
                                {
                                    sadmin.routeCode = new CE();
                                    sadmin.routeCode.code = med.RouteId;
                                    sadmin.routeCode.codeSystem = "2.16.840.1.113883.5.112";
                                    sadmin.routeCode.displayName = med.RouteId;
                                }
                                sadmin.doseQuantity = new IVL_PQ();
                                sadmin.doseQuantity.nullFlavor = "UNK";

                                sadmin.consumable = new POCD_MT000040Consumable();
                                sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                                sadmin.consumable.manufacturedProduct.classCodeSpecified = true;
                                sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;
                                sadmin.consumable.manufacturedProduct.templateId = new II[1];
                                sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                                sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.23";
                                sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                                POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                                mat.name = new EN();
                                mat.name.Text = new string[] { med.MedicationName };
                                mat.code = new CE();
                                mat.code.code = med.CodeValue;
                                mat.code.codeSystem = "2.16.840.1.113883.6.88";
                                mat.code.displayName = med.MedicationName;
                                mat.code.originalText = new ED();
                                mat.code.originalText.Text = new string[] { med.MedicationName };
                                mia++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int mi2 = 0; 
                        
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi2] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi2].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi2].Item = new POCD_MT000040SubstanceAdministration();
                        POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi2].Item;
                        sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                        sadmin.templateId = new II[1];
                        sadmin.templateId[0] = new II();
                        sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.16";
                        sadmin.id = new II[1];
                        sadmin.id[0] = new II();
                        sadmin.id[0].root = Guid.NewGuid().ToString();
                        sadmin.text = new ED();

                        sadmin.text.Text = new string[] { "" };
                        sadmin.statusCode = new CS();
                        sadmin.statusCode.code = "completed";

                        sadmin.effectiveTime = new IVL_TS[1];
                        sadmin.effectiveTime[0] = new IVL_TS();
                        IVL_TS tmstamp = new IVL_TS();
                        tmstamp.ItemsElementName = new ItemsChoiceType2[2];
                        tmstamp.ItemsElementName[0] = ItemsChoiceType2.low;
                        tmstamp.ItemsElementName[1] = ItemsChoiceType2.high;
                        tmstamp.Items = new QTY[2];
                        tmstamp.Items[0] = new IVXB_TS();
                        ((IVXB_TS)tmstamp.Items[0]).nullFlavor = "NA";
                        tmstamp.Items[1] = new IVXB_TS();
                        ((IVXB_TS)tmstamp.Items[1]).nullFlavor = "NA";
                        sadmin.effectiveTime[0] = tmstamp;

                        sadmin.consumable = new POCD_MT000040Consumable();
                        sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                        sadmin.consumable.manufacturedProduct.classCodeSpecified = true;
                        sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;
                        sadmin.consumable.manufacturedProduct.templateId = new II[1];
                        sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                        sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.23";
                        sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                        POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                        mat.name = new EN();
                        mat.name.Text = new string[] { "" };
                        mat.code = new CE();
                        mat.code.nullFlavor = "NI";
                       
                        componentIndex++;
                    }
                    #endregion
               
                    #region Medications
                    //*****************************************************
                    // add in the medications section
                    //*****************************************************

                    List<PatientMedication> meds = db.PatientMedications.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.Deleted == false && p.Active == true && p.DuringVisit == false).ToList();
                    // Check if there are any procedures selected, if not set the flag to false
                    if (meds.Count == 0)
                        Parms.Medications = false;

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;  

                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.8" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.8";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.1.1";  // SJF 04/15/2014
                    //<code code="10160-0" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.code = "10160-0";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Medications</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Medications" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the medications
                    if (Parms.Medications)  // Only build table if there are medications selected.
                    {
                        /*  <table width="100%" border="1">
                            <thead>
                            <tr>
                                <th>RxNorm Code</th>
                                <th>Product</th>
                                <th>Generic Name</th>
                                <th>Brand Name</th>
                                <th>Strength</th>
                                <th>Dose</th>
                                //<th>Route</th>
                                <th>Frequency</th>
                                <th>Date Started</th>
                                <th>Status</th>
                            </tr>
                            </thead>
                            <tbody>*/
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[10];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "RxNorm Code" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Product" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Medication Name" };
                        //table.thead.tr[0].Items[3] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Brand Name" };
                        //table.thead.tr[0].Items[4] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Strength" };
                        //table.thead.tr[0].Items[5] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Dose" };
                        //table.thead.tr[0].Items[3] = new StrucDocTh();
                        //((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Route" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Frequency" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Date Started" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Status" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Notes" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[meds.Count];
                        /*       <tr>
                                    <td>205875</td>
                                    <td>Medication</td>
                                    <td>glyburide</td>
                                    <td>Diabeta</td>
                                    <td>2.5 mg</td>
                                    <td>2.5</td>
                                    <td>PO</td>
                                    <td>Q AM</td>
                                    <td>11/4/2011</td>
                                    <td>Active</td>
                                </tr>*/
                        for (int mi = 0; mi < meds.Count; mi++)
                        {
                            PatientMedication med = meds.ElementAt<PatientMedication>(mi);
                            if (Parms.Custom == false || dtMedications.Rows.Contains(med.PatientMedicationCntr)) // SJF 5/1/2014
                            {
                                //CodedMedication med = meds.ElementAt<CodedMedication>(mi);
                                table.tbody[0].tr[mi] = new StrucDocTr();
                                table.tbody[0].tr[mi].Items = new object[10];
                                table.tbody[0].tr[mi].Items[0] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[0]).Text = new string[] { med.CodeValue };
                                table.tbody[0].tr[mi].Items[1] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[1]).Text = new string[] { "Medication" };
                                table.tbody[0].tr[mi].Items[2] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[2]).Text = new string[] { med.MedicationName };
                                //table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.BrandName };
                                //table.tbody[0].tr[mi].Items[4] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[4]).Text = new string[] { med.Strength };
                                //table.tbody[0].tr[mi].Items[5] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[5]).Text = new string[] { med.Dose };
                                //table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                //((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.RouteId };
                                table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.Frequency };
                                table.tbody[0].tr[mi].Items[4] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[4]).Text = new string[] { med.StartDate != null ? ((DateTime)med.StartDate).ToShortDateString() : "" };
                                table.tbody[0].tr[mi].Items[5] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[5]).Text = new string[] { med.Status == null ? "" : med.Status };
                                table.tbody[0].tr[mi].Items[6] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[mi].Items[6]).Text = new string[] { med.Note };
                            }
                        }
                    }
                    // add in the stuctured entries for the medications
                    if (Parms.Medications) // Only add items if the are medications selected.
                    {
                        /*     <entry typeCode="DRIV">
                                <substanceAdministration classCode="SBADM" moodCode="EVN">
                                    <templateId root="2.16.840.1.113883.10.20.1.24" />
                                    <id root="4df4c348-dfbc-4428-8ccd-0aaf84862850" />
                                    <statusCode code="Active" />
                                    <effectiveTime xsi:type="PIVL_TS" value="20111104" />
                                    <routeCode code="PO" codeSystem="2.16.840.1.113883.5.112" displayName="PO" />
                                    <doseQuantity value="2.5" unit="mg" />
                                    <consumable typeCode="CSM">
                                    <manufacturedProduct>
                                        <templateId root="2.16.840.1.113883.10.20.1.53" />
                                        <manufacturedMaterial classCode="MMAT">
                                        <code code="205875" codeSystem="2.16.840.1.113883.6.88" displayName="Diabeta">
                                            <originalText>Diabeta</originalText>
                                        </code>
                                        <name>Diabeta</name>
                                        </manufacturedMaterial>
                                    </manufacturedProduct>
                                    </consumable>
                                </substanceAdministration>
                                </entry> */
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[meds.Count];
                        //for (int mi = 0; mi < meds.Count; mi++)
                        //{
                        //    PatientMedication med = meds.ElementAt<PatientMedication>(mi);
                        // SJF 5/2/2014 Block Change
                        int icount = meds.Count;
                        if (Parms.Custom) icount = dtMedications.Rows.Count;
                        int mi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int mi2 = 0; mi2 < meds.Count; mi2++)
                        {
                            PatientMedication med = meds.ElementAt<PatientMedication>(mi2);
                            if (Parms.Custom == false || dtMedications.Rows.Contains(med.PatientMedicationCntr))
                            // End Block
                            {
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item = new POCD_MT000040SubstanceAdministration();
                                POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item;
                                sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                                sadmin.templateId = new II[1];
                                sadmin.templateId[0] = new II();
                                //sadmin.templateId[0].root = "2.16.840.1.113883.10.20.1.24";
                                sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.16"; //SJF 04/16/2014
                                sadmin.id = new II[1];
                                sadmin.id[0] = new II();
                                sadmin.id[0].root = Guid.NewGuid().ToString();
                                sadmin.text = new ED();

                                sadmin.text.Text = new string[] { med.MedicationName };
                                sadmin.statusCode = new CS();
                                sadmin.statusCode.code = med.Status == null ? "" : med.Status;

                                //sadmin.effectiveTime = new PIVL_TS[1];
                                //sadmin.effectiveTime[0] = new PIVL_TS();
                                //sadmin.effectiveTime[0].value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";

                                //SJF 4/15/2014
                                sadmin.effectiveTime = new IVL_TS[1];
                                sadmin.effectiveTime[0] = new IVL_TS();
                                //sadmin.effectiveTime[0].value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                                IVL_TS tmstamp = new IVL_TS();
                                tmstamp.ItemsElementName = new ItemsChoiceType2[2];
                                tmstamp.ItemsElementName[0] = ItemsChoiceType2.low;
                                tmstamp.ItemsElementName[1] = ItemsChoiceType2.high;
                                tmstamp.Items = new QTY[2];
                                tmstamp.Items[0] = new IVXB_TS();
                                ((IVXB_TS)tmstamp.Items[0]).value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                                tmstamp.Items[1] = new IVXB_TS();
                                ((IVXB_TS)tmstamp.Items[1]).value = med.ExpireDate != null ? ((DateTime)med.ExpireDate).ToString("yyyyMMdd") : "";
                                sadmin.effectiveTime[0] = tmstamp;

                                //SJF 4/15/2014 END

                                if (med.RouteId.Trim() != "")
                                {
                                    sadmin.routeCode = new CE();
                                    sadmin.routeCode.code = med.RouteId;
                                    sadmin.routeCode.codeSystem = "2.16.840.1.113883.5.112";
                                    sadmin.routeCode.displayName = med.RouteId;
                                }
                                if (med.Dose > 0)  // SJF 8/19/2014 - Added medication dose.
                                {
                                    sadmin.doseQuantity = new IVL_PQ();
                                    sadmin.doseQuantity.unit = med.DoseUnit;
                                    sadmin.doseQuantity.value = med.Dose.ToString();
                                }

                                sadmin.consumable = new POCD_MT000040Consumable();
                                sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                                sadmin.consumable.manufacturedProduct.classCodeSpecified = true; // SJF 4/28/14
                                //sadmin.consumable.manufacturedProduct.classCode = "MANU";
                                sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;  //SJF 4/23/14
                                sadmin.consumable.manufacturedProduct.templateId = new II[1];
                                sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                                //sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.1.53";
                                sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.23"; // SJF 4/15/2014
                                sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                                POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                                mat.name = new EN();
                                mat.name.Text = new string[] { med.MedicationName };
                                mat.code = new CE();
                                mat.code.code = med.CodeValue;
                                mat.code.codeSystem = "2.16.840.1.113883.6.88";
                                mat.code.displayName = med.MedicationName;
                                mat.code.originalText = new ED();
                                mat.code.originalText.Text = new string[] { med.MedicationName };
                                mi++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int mi = 0;
                        
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item = new POCD_MT000040SubstanceAdministration();
                        POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item;
                        sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                        sadmin.templateId = new II[1];
                        sadmin.templateId[0] = new II();
                        //sadmin.templateId[0].root = "2.16.840.1.113883.10.20.1.24";
                        sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.16"; //SJF 04/16/2014
                        sadmin.id = new II[1];
                        sadmin.id[0] = new II();
                        sadmin.id[0].root = Guid.NewGuid().ToString();
                        sadmin.text = new ED();

                        sadmin.text.Text = new string[] { "" };
                        sadmin.statusCode = new CS();
                        sadmin.statusCode.code = "completed";

                        //SJF 4/15/2014
                        sadmin.effectiveTime = new IVL_TS[1];
                        sadmin.effectiveTime[0] = new IVL_TS();
                        //sadmin.effectiveTime[0].value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                        IVL_TS tmstamp = new IVL_TS();
                        tmstamp.ItemsElementName = new ItemsChoiceType2[2];
                        tmstamp.ItemsElementName[0] = ItemsChoiceType2.low;
                        tmstamp.ItemsElementName[1] = ItemsChoiceType2.high;
                        tmstamp.Items = new QTY[2];
                        tmstamp.Items[0] = new IVXB_TS();
                        ((IVXB_TS)tmstamp.Items[0]).nullFlavor = "NA";
                        tmstamp.Items[1] = new IVXB_TS();
                        ((IVXB_TS)tmstamp.Items[1]).nullFlavor = "NA";
                        sadmin.effectiveTime[0] = tmstamp;

                        //SJF 4/15/2014 END

                        //sadmin.doseQuantity = new IVL_PQ();
                        //sadmin.doseQuantity.nullFlavor = "UNK";

                        //sadmin.doseQuantity.unit =  "";//med.DoseUnit;
                        //sadmin.doseQuantity.value = "";// med.Dose;
                        ////administrationUnitCode ???
                        sadmin.consumable = new POCD_MT000040Consumable();
                        sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                        sadmin.consumable.manufacturedProduct.classCodeSpecified = true; // SJF 4/28/14
                        sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;  //SJF 4/23/14
                        sadmin.consumable.manufacturedProduct.templateId = new II[1];
                        sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                        //sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.1.53";
                        sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.23"; // SJF 4/15/2014
                        sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                        POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                        mat.name = new EN();
                        mat.name.Text = new string[] { "" };
                        mat.code = new CE();
                        mat.code.nullFlavor = "NI";
                        
                        componentIndex++;
                    }
                    #endregion

                    #region Problems - ok
                    //*****************************************************
                    // add in the problems section
                    //*****************************************************
                    List<PatientProblem> probs = db.PatientProblems.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.CodeSystemId == 5 && p.Deleted == false).ToList(); // Only Pull SNOMED
                    if (probs.Count == 0)
                        Parms.Problems = false;
                                            
                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.11" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.11";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.5.1"; // SJF 4/15/2014
                    //<code nullFlavor="NA" code="11450-4" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "11450-4";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Problems" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the problems
                    if (Parms.Problems)  // Only build table if there are problems selected.
                    {
                        // add in the HTML for the problems
                        /*  <table width="100%" border="1">
                                <thead>
                                <tr>
                                    <th>SNOMED Code</th>
                                    <th>Patient Problem</th>
                                    <th>Status</th>
                                    <th>Date Diagnosed</th>
                                </tr>
                                </thead>
                                <tbody>
                        */
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[4];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "SNOMED Code" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Patient Problem" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Status" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Date Diagnosed" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[probs.Count];
                        /*  <tr>
                                <td>195967001</td>
                                <td>Asthma</td>
                                <td>Active</td>
                                <td>11/4/2011</td>
                            </tr> */
                        for (int pi = 0; pi < probs.Count; pi++)
                        {
                            PatientProblem prob = probs.ElementAt<PatientProblem>(pi);
                            if (Parms.Custom == false || dtProblems.Rows.Contains(prob.PatientProblemCntr)) // SJF 5/2/2014
                            {
                                C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                table.tbody[0].tr[pi] = new StrucDocTr();
                                table.tbody[0].tr[pi].Items = new object[4];
                                table.tbody[0].tr[pi].Items[0] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { prob.CodeValue };
                                table.tbody[0].tr[pi].Items[1] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { prob.Condition };
                                table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                if (cResults == null)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { "" };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { cResults.Value };
                                table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { FormatDate(prob.EffectiveDate) };
                            }
                        }
                    }
                    // add in the stuctured entries for the problems
                    if (Parms.Problems) // Only add items if the are problems selected.
                    {
                        // add in the stuctured entries for the problems
                        /* <entry typeCode="DRIV">
                            <act classCode="ACT" moodCode="EVN">
                              <templateId root="2.16.840.1.113883.10.20.1.27" />
                              <id root="55537616-afb4-42bd-bbed-1bbb2a6ece11" />
                              <code nullFlavor="NA" />
                              <entryRelationship typeCode="SUBJ">
                                <observation classCode="OBS" moodCode="EVN">
                                  <templateId root="2.16.840.1.113883.10.20.1.28" />
                                  <id root="ed39c95a-a2fd-4c83-9010-58ff4a809628" />
                                  <code code="ASSERTION" codeSystem="2.16.840.1.113883.5.4" />
                                  <statusCode code="completed" />
                                  <effectiveTime value="20111104" />
                                  <value xsi:type="CD" code="195967001" codeSystem="2.16.840.1.113883.6.96" displayName="Asthma" />
                                </observation>
                              </entryRelationship>
                            </act>
                          </entry>*/
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[probs.Count];
                        //for (int pi = 0; pi < probs.Count; pi++)
                        //{
                        //    PatientProblem prob = probs.ElementAt<PatientProblem>(pi);
                        // SJF 5/2/2014 Block Change
                        int icount = probs.Count;
                        if (Parms.Custom) icount = dtProblems.Rows.Count;
                        int pi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int pi2 = 0; pi2 < probs.Count; pi2++)
                        {
                            PatientProblem prob = probs.ElementAt<PatientProblem>(pi2);
                            if (Parms.Custom == false || dtProblems.Rows.Contains(prob.PatientProblemCntr))
                            // End Block
                            {
                                //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Act();
                                POCD_MT000040Act act = (POCD_MT000040Act)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                                act.id = new II[1];
                                act.id[0] = new II();
                                act.id[0].root = Guid.NewGuid().ToString();
                                act.moodCode = x_DocumentActMood.EVN;
                                act.templateId = new II[1];
                                act.templateId[0] = new II();
                                //act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                                act.templateId[0].root = "2.16.840.1.113883.10.20.22.4.3"; //SJF 4/16/2014
                                act.code = new CD();
                                act.code.nullFlavor = "NA";
                                // SJF 4/22/14 Added block below
                                act.statusCode = new CS();
                                act.statusCode.code = "active";
                                act.effectiveTime = new IVL_TS();
                                act.effectiveTime.ItemsElementName = new ItemsChoiceType2[2];
                                act.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                act.effectiveTime.ItemsElementName[1] = ItemsChoiceType2.high;
                                act.effectiveTime.Items = new QTY[2];
                                act.effectiveTime.Items[0] = new IVXB_TS();
                                ((IVXB_TS)act.effectiveTime.Items[0]).value = prob.EffectiveDate.Trim();
                                act.effectiveTime.Items[1] = new IVXB_TS();
                                ((IVXB_TS)act.effectiveTime.Items[1]).nullFlavor = "NA";
                                // SJF End new block

                                act.entryRelationship = new POCD_MT000040EntryRelationship[1];
                                act.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                                act.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                                act.entryRelationship[0].Item = new POCD_MT000040Observation();
                                POCD_MT000040Observation ob = (POCD_MT000040Observation)act.entryRelationship[0].Item;
                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";
                                ob.classCode = "OBS";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                ob.moodCode = x_ActMoodDocumentObservation.EVN;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();
                                //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.28";
                                ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.4"; // SJF 4/22/14
                                ob.code = new CD();
                                ob.code.code = "ASSERTION";
                                ob.code.codeSystem = "2.16.840.1.113883.5.4";
                                ob.effectiveTime = new IVL_TS();
                                //ob.effectiveTime.value = prob.EffectiveDate.Trim();
                                ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                                ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                ob.effectiveTime.Items = new QTY[1];
                                ob.effectiveTime.Items[0] = new IVXB_TS();
                                ((IVXB_TS)ob.effectiveTime.Items[0]).value = prob.EffectiveDate.Trim();

                                ob.value = new ANY[1];
                                ob.value[0] = new CD();
                                ((CD)ob.value[0]).displayName = prob.Condition;
                                ((CD)ob.value[0]).code = prob.CodeValue;
                                ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                pi++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int pi = 0;

                        //PatientProblem prob = probs.ElementAt<PatientProblem>(pi);
                        //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Act();
                        POCD_MT000040Act act = (POCD_MT000040Act)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                        act.id = new II[1];
                        act.id[0] = new II();
                        act.id[0].root = Guid.NewGuid().ToString();
                        act.moodCode = x_DocumentActMood.EVN;
                        act.templateId = new II[1];
                        act.templateId[0] = new II();
                        //act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                        act.templateId[0].root = "2.16.840.1.113883.10.20.22.4.3"; //SJF 4/16/2014
                        act.code = new CD();
                        act.code.nullFlavor = "NA";
                        // SJF 4/22/14 Added block below
                        act.statusCode = new CS();
                        act.statusCode.code = "active";
                        act.effectiveTime = new IVL_TS();
                        act.effectiveTime.ItemsElementName = new ItemsChoiceType2[2];
                        act.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                        act.effectiveTime.ItemsElementName[1] = ItemsChoiceType2.high;
                        act.effectiveTime.Items = new QTY[2];
                        act.effectiveTime.Items[0] = new IVXB_TS();
                        ((IVXB_TS)act.effectiveTime.Items[0]).nullFlavor = "NA";
                        act.effectiveTime.Items[1] = new IVXB_TS();
                        ((IVXB_TS)act.effectiveTime.Items[1]).nullFlavor = "NA";
                        // SJF End new block

                        act.entryRelationship = new POCD_MT000040EntryRelationship[1];
                        act.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                        act.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                        act.entryRelationship[0].Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob = (POCD_MT000040Observation)act.entryRelationship[0].Item;
                        ob.statusCode = new CS();
                        ob.statusCode.code = "completed";
                        ob.classCode = "OBS";
                        ob.id = new II[1];
                        ob.id[0] = new II();
                        ob.id[0].root = Guid.NewGuid().ToString();
                        ob.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob.templateId = new II[1];
                        ob.templateId[0] = new II();
                        //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.28";
                        ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.4"; // SJF 4/22/14
                        ob.code = new CD();
                        ob.code.code = "ASSERTION";
                        ob.code.codeSystem = "2.16.840.1.113883.5.4";
                        ob.effectiveTime = new IVL_TS();
                        //ob.effectiveTime.value = prob.EffectiveDate.Trim();
                        ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                        ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                        ob.effectiveTime.Items = new QTY[1];
                        ob.effectiveTime.Items[0] = new IVXB_TS();
                        ((IVXB_TS)ob.effectiveTime.Items[0]).nullFlavor = "NA";

                        ob.value = new ANY[1];
                        ob.value[0] = new CD();
                        ((CD)ob.value[0]).nullFlavor = "NI";


                        componentIndex++;
                    }
                    #endregion

                    #region Medication Allergies - ok
                    //*****************************************************
                    // add in the Allergies section 
                    //*****************************************************
                    List<PatientAllergy> alls = db.PatientAllergies.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.CodeSystemId_Allergen == 6 && p.Deleted == false).ToList();  // Only pull medication Allergies
                    if (alls.Count == 0)
                        Parms.Allergies = false;

                            // copy over any existing components and expand the component array by one
                            //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];

                            newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                            for (int ci = 0; ci < componentIndex; ci++)
                                newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                            // <component>
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                            //<section classCode="DOCSECT" moodCode="EVN">
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                            //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                            section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                            //<templateId root="2.16.840.1.113883.10.20.1.2" />
                            section.templateId = new II[1];
                            section.templateId[0] = new II();
                            //section.templateId[0].root = "2.16.840.1.113883.10.20.1.2";
                            section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.6.1";  // SJF 04/15/2014
                            //<code nullFlavor="NA" code="48765-2" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                            section.code = new CE();
                            section.code.nullFlavor = "NA";
                            section.code.code = "48765-2";
                            section.code.codeSystem = "2.16.840.1.113883.6.1";
                            section.code.codeSystemName = "LOINC";
                            //<title>Allergies, Adverse Reactions, Alerts</title>
                            section.title = new ST();
                            section.title.Text = new string[] { "Allergies, Adverse Reactions, Alerts" };
                            //<text mediaType="text/x-hl7-text+xml">
                            section.text = new StrucDocText();

                            // add in the HTML for the allergies
                    if (Parms.Allergies)  // Only build table if there are allergies selected.
                    {
                            /*  <table width="100%" border="1">
                                  <thead>
                                    <tr>
                                      <th>SNOMED Allergy Type Code</th>
                                      <th>RxNorm Code</th>
                                      <th>Medication/Agent Allergy</th>
                                      <th>Reaction</th>
                                      <th>Status</th>
                                    </tr>
                                  </thead>
                                  <tbody>*/
                            section.text.Items = new object[1];
                            section.text.Items[0] = new StrucDocTable();
                            StrucDocTable table = (StrucDocTable)section.text.Items[0];
                            table.border = "1";
                            table.width = "100%";
                            table.thead = new StrucDocThead();
                            table.thead.tr = new StrucDocTr[1];
                            table.thead.tr[0] = new StrucDocTr();
                            table.thead.tr[0].Items = new object[5];
                            table.thead.tr[0].Items[0] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "SNOMED Allergy Type Code" };
                            table.thead.tr[0].Items[1] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "RxNorm Code" };
                            table.thead.tr[0].Items[2] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Medication/Agent Allergy" };
                            table.thead.tr[0].Items[3] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Reaction" };
                            table.thead.tr[0].Items[4] = new StrucDocTh();
                            //((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Adverse Event Date" };
                            ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Status" };
                            table.tbody = new StrucDocTbody[1];
                            table.tbody[0] = new StrucDocTbody();
                            table.tbody[0].tr = new StrucDocTr[alls.Count];
                            /*
                                <tr>
                                  <td>416098002</td>
                                  <td>205875</td>
                                  <td>Diabeta</td>
                                  <td>Hives</td>
                                  <td>11/4/2011</td>
                                </tr>
                             */
                            for (int ai = 0; ai < alls.Count; ai++)
                            {
                                PatientAllergy all = alls.ElementAt<PatientAllergy>(ai);
                                if (Parms.Custom == false || dtAllergies.Rows.Contains(all.PatientAllergyCntr)) // SJF 5/2/2014
                                {
                                    table.tbody[0].tr[ai] = new StrucDocTr();
                                    table.tbody[0].tr[ai].Items = new object[5];
                                    table.tbody[0].tr[ai].Items[0] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ai].Items[0]).Text = new string[] { "416098002" };   //Drug Allergy 
                                    table.tbody[0].tr[ai].Items[1] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ai].Items[1]).Text = new string[] { all.CodeValue_Allergen };
                                    table.tbody[0].tr[ai].Items[2] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ai].Items[2]).Text = new string[] { all.Allergen };
                                    table.tbody[0].tr[ai].Items[3] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ai].Items[3]).Text = new string[] { all.Reaction };
                                    table.tbody[0].tr[ai].Items[4] = new StrucDocTd();
                                    //((StrucDocTd)table.tbody[0].tr[ai].Items[4]).Text = new string[] { all.EffectiveDate != null ? (FormatDate(all.EffectiveDate)) : "" };
                                    C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == all.ConditionStatusId);
                                    if (cResults != null)
                                        ((StrucDocTd)table.tbody[0].tr[ai].Items[4]).Text = new string[] { cResults.Value };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[ai].Items[4]).Text = new string[] { "" };
                                }
                            }
                    }

                    // add in the stuctured entries for the allergies
                    if (Parms.Allergies) // Only add items if the are allergies selected.
                    {
                        /*
                          <entry typeCode="DRIV">
                            <act classCode="ACT" moodCode="EVN">
                              <templateId root="2.16.840.1.113883.10.20.1.27" />
                              <id root="c88120ae-c30f-46f4-8167-9c7374b0a870" />
                              <code nullFlavor="NA" />
                              <entryRelationship typeCode="SUBJ">
                                <observation classCode="OBS" moodCode="EVN">
                                  <templateId root="2.16.840.1.113883.10.20.1.18" />
                                  <id root="00ac9ab9-b09f-4d1d-ae1e-b0530deba1c6" />
                                  <code code="ASSERTION" codeSystem="2.16.840.1.113883.5.4" />
                                  <statusCode code="completed" />
                                  <value xsi:type="CD" code="282100009" codeSystem="2.16.840.1.113883.6.96" displayName="Adverse reaction to substance" />
                                  <participant typeCode="CSM" contextControlCode="OP">
                                    <participantRole classCode="MANU">
                                      <playingEntity classCode="MMAT" determinerCode="INSTANCE">
                                        <code code="205875" codeSystem="2.16.840.1.113883.6.88" displayName="Diabeta" />
                                      </playingEntity>
                                    </participantRole>
                                  </participant>
                                </observation>
                              </entryRelationship>
                            </act>
                          </entry>
                         */
                        //section.entry = new POCD_MT000040Entry[alls.Count];
                        //for (int ai = 0; ai < alls.Count; ai++)
                        //{
                        //    PatientAllergy all = alls.ElementAt<PatientAllergy>(ai);
                        // SJF 5/2/2014 Block Change
                        int icount = alls.Count;
                        if (Parms.Custom) icount = dtAllergies.Rows.Count; 
                        int ai = 0;
                        section.entry = new POCD_MT000040Entry[icount];
                        for (int ai2 = 0; ai2 < alls.Count; ai2++)
                        {
                            PatientAllergy all = alls.ElementAt<PatientAllergy>(ai2);
                            if (Parms.Custom == false || dtAllergies.Rows.Contains(all.PatientAllergyCntr)) 
                            // End Block
                            {
                                section.entry[ai] = new POCD_MT000040Entry();
                                section.entry[ai].typeCode = x_ActRelationshipEntry.DRIV;
                                section.entry[ai].Item = new POCD_MT000040Act();
                                POCD_MT000040Act act = (POCD_MT000040Act)section.entry[ai].Item;
                                act.moodCode = x_DocumentActMood.EVN;
                                act.id = new II[1];
                                act.id[0] = new II();
                                act.id[0].root = Guid.NewGuid().ToString();
                                act.templateId = new II[1];
                                act.templateId[0] = new II();
                                //act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                                act.templateId[0].root = "2.16.840.1.113883.10.20.22.4.30"; // SJF 4/16/2014
                                act.code = new CD();
                                act.code.nullFlavor = "NA";
                                // SJF 4/23/14 Added block
                                act.statusCode = new CS();
                                C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == all.ConditionStatusId);
                                if (cResults != null)
                                    act.statusCode.code = cResults.Value.ToLower();
                                else
                                    act.statusCode.nullFlavor = "NA";
                                act.effectiveTime = new IVL_TS();
                                act.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                                act.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                act.effectiveTime.Items = new QTY[1];
                                act.effectiveTime.Items[0] = new IVXB_TS();
                                ((IVXB_TS)act.effectiveTime.Items[0]).value = all.EffectiveDate.Trim();
                                // SJF End Block
                                if (all.CodeSystemId_Reaction > 0) //SJF 9/16/14
                                    act.entryRelationship = new POCD_MT000040EntryRelationship[2];
                                else
                                    act.entryRelationship = new POCD_MT000040EntryRelationship[1];
                                act.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                                act.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                                //act.entryRelationship[0].templateId = new II[1];
                                //act.entryRelationship[0].templateId[0] = new II();
                                act.entryRelationship[0].Item = new POCD_MT000040Observation();
                                POCD_MT000040Observation ob = (POCD_MT000040Observation)act.entryRelationship[0].Item;
                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";

                                ob.effectiveTime = new IVL_TS();    // SJF 4/23/14
                                ob.effectiveTime.value = all.EffectiveDate; // SJF 4/23/14

                                ob.classCode = "OBS";
                                ob.moodCode = x_ActMoodDocumentObservation.EVN;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();
                                //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.18"
                                ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.7"; //  SJF 4/23/14

                                ob.code = new CD();
                                ob.code.code = "ASSERTION";
                                ob.code.codeSystem = "2.16.840.1.113883.5.4";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                ob.value = new ANY[1];
                                ob.value[0] = new CD();
                                //((CD)ob.value[0]).displayName = "Adverse reaction to substance";
                                //((CD)ob.value[0]).code = "282100009";// Adverse reaction to substance
                                ((CD)ob.value[0]).displayName = "Drug Allergy"; // SJF 4/23/14
                                ((CD)ob.value[0]).code = "416098002";// SJF 4/23/14
                                ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                ob.participant = new POCD_MT000040Participant2[1];
                                ob.participant[0] = new POCD_MT000040Participant2();
                                ob.participant[0].typeCode = "CSM";
                                ob.participant[0].participantRole = new POCD_MT000040ParticipantRole();
                                ob.participant[0].participantRole.classCode = "MANU";
                                ob.participant[0].participantRole.Item = new POCD_MT000040PlayingEntity();
                                ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).classCode = "MMAT";
                                ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code = new CE();
                                ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code.code = all.CodeValue_Allergen;
                                ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code.codeSystem = "2.16.840.1.113883.6.88";  // Need to break these out by type.
                                ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code.displayName = all.Allergen;

                                //  SJF 9/16/14 Added block
                                if (all.CodeSystemId_Reaction > 0)
                                {
                                    act.entryRelationship[1] = new POCD_MT000040EntryRelationship();
                                    act.entryRelationship[1].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                                    act.entryRelationship[1].Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)act.entryRelationship[1].Item;

                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";

                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.9";

                                    ob2.code = new CD();
                                    ob2.code.code = "ASSERTION";
                                    ob2.code.codeSystem = "2.16.840.1.113883.5.4";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new CD();
                                    ((CD)ob2.value[0]).displayName = all.Reaction;

                                    if (all.CodeSystemId_Reaction == 1)
                                    {
                                        ((CD)ob2.value[0]).code = all.CodeValue_Reaction;
                                        ((CD)ob2.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob2.value[0]).codeSystemName = "CPT 4";
                                    }
                                    else if (all.CodeSystemId_Reaction == 2)
                                    {
                                        ((CD)ob2.value[0]).code = all.CodeValue_Reaction;
                                        ((CD)ob2.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob2.value[0]).codeSystemName = "ICD-9";
                                    }
                                    else if (all.CodeSystemId_Reaction == 3)
                                    {
                                        ((CD)ob2.value[0]).code = all.CodeValue_Reaction;
                                        ((CD)ob2.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                        ((CD)ob2.value[0]).codeSystemName = "ICD-10";
                                    }
                                    else if (all.CodeSystemId_Reaction == 5)
                                    {
                                        ((CD)ob2.value[0]).code = all.CodeValue_Reaction;
                                        ((CD)ob2.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                        ((CD)ob2.value[0]).codeSystemName = "SNOMED-CT";
                                    }
                                    else
                                    {
                                        ob2.code.nullFlavor = "NA";
                                    }
                                }
                                // SJF End block 9/16/14
                                

                                // removed this bit as it doesn't seem to be needed to get the CCD to validate via schematron http://xreg2.nist.gov:8080/hitspValidation/validation.jsp
                                /*ob.entryRelationship = new POCD_MT000040EntryRelationship[1];
                                ob.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                                ob.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.MFST;
                                ob.entryRelationship[0].inversionInd = true;
                                ob.entryRelationship[0].Item = new POCD_MT000040Observation();
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).classCode = "OBS";
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).moodCode = x_ActMoodDocumentObservation.EVN;
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).templateId = new II[1];
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).templateId[0] = new II();
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).templateId[0].root = "2.16.889889840.1.113883.10.20.1.54";
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).code = new CD();
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).code.code = "ASSERTION";
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).code.codeSystem = "2.16.840.1.113883.5.4";
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).statusCode = new CS();
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).statusCode.code = "completed";
                                ((POCD_MT000040Observation)ob.entryRelationship[0].Item).value = new CD[1];
                                ((CD[])((POCD_MT000040Observation)ob.entryRelationship[0].Item).value)[0] = new CD();
                                ((CD[])((POCD_MT000040Observation)ob.entryRelationship[0].Item).value)[0].code = all.SNOMEDCTId; // this is snomed ct code for the reaction
                                ((CD[])((POCD_MT000040Observation)ob.entryRelationship[0].Item).value)[0].displayName = all.Reaction;*/
                                ai++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        section.entry = new POCD_MT000040Entry[1];
                        int ai = 0;

                        //PatientAllergy all = alls.ElementAt<PatientAllergy>(ai);
                        section.entry[ai] = new POCD_MT000040Entry();
                        section.entry[ai].typeCode = x_ActRelationshipEntry.DRIV;
                        section.entry[ai].Item = new POCD_MT000040Act();
                        POCD_MT000040Act act = (POCD_MT000040Act)section.entry[ai].Item;
                        act.moodCode = x_DocumentActMood.EVN;
                        act.id = new II[1];
                        act.id[0] = new II();
                        act.id[0].root = Guid.NewGuid().ToString();
                        act.templateId = new II[1];
                        act.templateId[0] = new II();
                        //act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                        act.templateId[0].root = "2.16.840.1.113883.10.20.22.4.30"; // SJF 4/16/2014
                        act.code = new CD();
                        act.code.nullFlavor = "NA";
                        // SJF 4/23/14 Added block
                        act.statusCode = new CS();
                        act.statusCode.nullFlavor = "NA";
                        act.effectiveTime = new IVL_TS();
                        act.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                        act.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                        act.effectiveTime.Items = new QTY[1];
                        act.effectiveTime.Items[0] = new IVXB_TS();
                        ((IVXB_TS)act.effectiveTime.Items[0]).nullFlavor = "NA";
                        // SJF End Block
                        act.entryRelationship = new POCD_MT000040EntryRelationship[1];
                        act.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                        act.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                        act.entryRelationship[0].Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob = (POCD_MT000040Observation)act.entryRelationship[0].Item;
                        ob.statusCode = new CS();
                        ob.statusCode.code = "completed";

                        ob.effectiveTime = new IVL_TS();    // SJF 4/23/14
                        ob.effectiveTime.nullFlavor = "NA";

                        ob.classCode = "OBS";
                        ob.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob.templateId = new II[1];
                        ob.templateId[0] = new II();
                        //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.18"
                        ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.7"; //  SJF 4/23/14

                        ob.code = new CD();
                        ob.code.code = "ASSERTION";
                        ob.code.codeSystem = "2.16.840.1.113883.5.4";
                        ob.id = new II[1];
                        ob.id[0] = new II();
                        ob.id[0].root = Guid.NewGuid().ToString();
                        ob.value = new ANY[1];
                        ob.value[0] = new CD();
                        //((CD)ob.value[0]).displayName = "Adverse reaction to substance";
                        //((CD)ob.value[0]).code = "282100009";// Adverse reaction to substance
                        ((CD)ob.value[0]).displayName = "Allergy to substance (disorder)"; // SJF 4/23/14
                        ((CD)ob.value[0]).code = "419199007";// SJF 4/23/14
                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                        ob.participant = new POCD_MT000040Participant2[1];
                        ob.participant[0] = new POCD_MT000040Participant2();
                        ob.participant[0].typeCode = "CSM";
                        ob.participant[0].participantRole = new POCD_MT000040ParticipantRole();
                        ob.participant[0].participantRole.classCode = "MANU";
                        ob.participant[0].participantRole.Item = new POCD_MT000040PlayingEntity();
                        ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).classCode = "MMAT";
                        ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code = new CE();
                        ((POCD_MT000040PlayingEntity)ob.participant[0].participantRole.Item).code.nullFlavor = "NI";

                        componentIndex++;
                    }
                    #endregion

                    #region Immunizations - ok
                    //*****************************************************
                    // add in the Immunizations section
                    //*****************************************************
                    // 998 - no vaccine administered
                    List<PatientImmunization> imms = db.PatientImmunizations.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.Deleted == false).ToList();
                    if (imms.Count == 0)
                        Parms.Immunizations = false;

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.6" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.2.1";     //"2.16.840.1.113883.10.20.1.6";
                    //<code code="11369-6" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.code = "11369-6";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Immunizations</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Immunizations" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the immunizations
                    if (Parms.Immunizations)    // Only build table if there are immunizations selected.
                    {
                            /*
                                <table width="100%" border="1">
                                  <thead>
                                    <tr>
                                      <th>Vaccine</th>
                                      <th>Date</th>
                                      <th>Status</th>
                                    </tr>
                                  </thead>
                                  <tbody>
                            */
                            section.text.Items = new object[1];
                            section.text.Items[0] = new StrucDocTable();
                            StrucDocTable table = (StrucDocTable)section.text.Items[0];
                            table.border = "1";
                            table.width = "100%";
                            table.thead = new StrucDocThead();
                            table.thead.tr = new StrucDocTr[1];
                            table.thead.tr[0] = new StrucDocTr();
                            table.thead.tr[0].Items = new object[4];
                            table.thead.tr[0].Items[0] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Vaccine" };
                            table.thead.tr[0].Items[1] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "CVX Code" };
                            table.thead.tr[0].Items[2] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Date" };
                            table.thead.tr[0].Items[3] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Status" };
                            table.tbody = new StrucDocTbody[1];
                            table.tbody[0] = new StrucDocTbody();
                            table.tbody[0].tr = new StrucDocTr[imms.Count];
                            /*
                                <tr>
                                  <td>Hep B, adolescent or pediatric</td>
                                  <td>10/28/2011</td>
                                  <td>Completed</td>
                                </tr>
                            */
                            for (int ii = 0; ii < imms.Count; ii++)
                            {
                                PatientImmunization imm = imms.ElementAt<PatientImmunization>(ii);
                                if (Parms.Custom == false || dtImmunizations.Rows.Contains(imm.PatientImmunizationCntr)) // SJF 5/2/2014
                                {
                                    table.tbody[0].tr[ii] = new StrucDocTr();
                                    table.tbody[0].tr[ii].Items = new object[4];
                                    table.tbody[0].tr[ii].Items[0] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ii].Items[0]).Text = new string[] { imm.Vaccine };
                                    table.tbody[0].tr[ii].Items[1] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ii].Items[1]).Text = new string[] { imm.CodeValue };
                                    table.tbody[0].tr[ii].Items[2] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ii].Items[2]).Text = new string[] { FormatDate(imm.ImmunizationDate) };
                                    table.tbody[0].tr[ii].Items[3] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[ii].Items[3]).Text = new string[] { "Completed" };
                                }
                            }
                    }
                            // add in the stuctured entries for the immunizations
                    if (Parms.Immunizations) // Only add items if the are immunizations selected.
                    {
                        /*
                          <entry typeCode="DRIV">
                            <substanceAdministration classCode="SBADM" moodCode="EVN">
                              <templateId root="2.16.840.1.113883.10.20.1.24" />
                              <id root="2386ad6d-2864-4715-a093-ac6cba85635f" />
                              <statusCode code="completed" />
                              <effectiveTime value="20111028" />
                              <consumable typeCode="CSM">
                                <manufacturedProduct>
                                  <templateId root="2.16.840.1.113883.10.20.1.53" />
                                  <manufacturedMaterial classCode="MMAT">
                                    <code code="33" codeSystem="2.16.840.1.113883.6.59" displayName="Hep B, adolescent or pediatric">
                                      <originalText>Hep B, adolescent or pediatric</originalText>
                                    </code>
                                  </manufacturedMaterial>
                                </manufacturedProduct>
                              </consumable>
                            </substanceAdministration>
                          </entry>
                        */
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[imms.Count];
                        //for (int ii = 0; ii < imms.Count; ii++)
                        //{
                        //    PatientImmunization imm = imms.ElementAt<PatientImmunization>(ii);

                        // SJF 5/2/2014 Block Change
                        int icount = imms.Count;
                        if (Parms.Custom) icount = dtImmunizations.Rows.Count; 
                        int ii = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int ii2 = 0; ii2 < imms.Count; ii2++)
                        {
                            PatientImmunization imm = imms.ElementAt<PatientImmunization>(ii2);
                            if (Parms.Custom == false || dtImmunizations.Rows.Contains(imm.PatientImmunizationCntr)) 
                            // End Block
                            {
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item = new POCD_MT000040SubstanceAdministration();
                                POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item;
                                sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                                sadmin.negationIndSpecified = true; // SJF Added 5/14/14
                                sadmin.negationInd = false;   // SJF Added 5/14/14
                                sadmin.templateId = new II[1];
                                sadmin.templateId[0] = new II();
                                sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.52"; // "2.16.840.1.113883.10.20.1.24";
                                sadmin.id = new II[1];
                                sadmin.id[0] = new II();
                                sadmin.id[0].root = Guid.NewGuid().ToString();
                                sadmin.statusCode = new CS();
                                sadmin.statusCode.code = "completed";
                                //sadmin.effectiveTime = new SXCM_TS[1];
                                //sadmin.effectiveTime[0] = new SXCM_TS();
                                //sadmin.effectiveTime[0].value = imm.ImmunizationDate.Trim();


                                //SJF 4/15/2014
                                sadmin.effectiveTime = new IVL_TS[1];
                                sadmin.effectiveTime[0] = new IVL_TS();
                                sadmin.effectiveTime[0].value = imm.ImmunizationDate;

                                if (imm.Route != null && imm.Route.Trim() != "")
                                {
                                    sadmin.routeCode = new CE();
                                    sadmin.routeCode.code = imm.Route;
                                    sadmin.routeCode.codeSystem = "2.16.840.1.113883.5.112";
                                    sadmin.routeCode.displayName = imm.Route;
                                }
                                //sadmin.doseQuantity = new IVL_PQ();
                                ////if (imm.Amount != null && imm.Amount != "")
                                ////    sadmin.doseQuantity.value = imm.Amount;
                                ////else
                                //sadmin.doseQuantity.nullFlavor = "UNK";

                                sadmin.consumable = new POCD_MT000040Consumable();
                                sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                                sadmin.consumable.manufacturedProduct.classCodeSpecified = true; // SJF Added 5/14/14
                                sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;  // SJF Added 5/14/14
                                sadmin.consumable.manufacturedProduct.templateId = new II[1];
                                sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                                sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.54"; // "2.16.840.1.113883.10.20.1.53";
                                sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                                POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                                mat.code = new CE();
                                mat.code.code = imm.CodeValue;// NormalizedValue;                // The Code Value for the vaccine
                                mat.code.codeSystem = "2.16.840.1.113883.12.292"; // "2.16.840.1.113883.6.59";  // CVX - CDC Vaccine Codes
                                mat.code.displayName = imm.Vaccine;
                                mat.code.originalText = new ED();
                                mat.code.originalText.Text = new string[] { imm.Note };
                                ii++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int ii = 0;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item = new POCD_MT000040SubstanceAdministration();
                        POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item;
                        sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                        sadmin.negationIndSpecified = true; // SJF Added 5/14/14
                        sadmin.negationInd = false;   // SJF Added 5/14/14
                        sadmin.templateId = new II[1];
                        sadmin.templateId[0] = new II();
                        sadmin.templateId[0].root = "2.16.840.1.113883.10.20.22.4.52"; //"2.16.840.1.113883.10.20.1.24";
                        sadmin.id = new II[1];
                        sadmin.id[0] = new II();
                        sadmin.id[0].root = Guid.NewGuid().ToString();
                        sadmin.statusCode = new CS();
                        sadmin.statusCode.code = "completed";

                        //SJF 4/15/2014
                        sadmin.effectiveTime = new IVL_TS[1];
                        sadmin.effectiveTime[0] = new IVL_TS();

                        //sadmin.doseQuantity = new IVL_PQ();

                        //sadmin.doseQuantity.nullFlavor = "UNK";

                        sadmin.consumable = new POCD_MT000040Consumable();
                        sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                        sadmin.consumable.manufacturedProduct.classCodeSpecified = true; // SJF Added 5/14/14
                        sadmin.consumable.manufacturedProduct.classCode = RoleClassManufacturedProduct.MANU;  // SJF Added 5/14/14
                        sadmin.consumable.manufacturedProduct.templateId = new II[1];
                        sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                        sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.22.4.54"; // "2.16.840.1.113883.10.20.1.53";
                        sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                        POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                        mat.code = new CE();
                        mat.code.nullFlavor = "NI";

                        componentIndex++;
                    }
                    #endregion

                    #region Social History - Smoking Status
                    //*****************************************************
                    // add in the Social History section
                    //*****************************************************
                    
                    List<PatientSocialHist> socials = db.PatientSocialHists.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId).ToList();

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;

                    //<templateId root="2.16.840.1.113883.10.20.1.15" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.15";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.17"; // SJF 04/15/2014
                    //<code nullFlavor="NA" code="18776-5" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "29762-2";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Social History" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the social history
                    if (Parms.Social)
                    {
                        /*  <table width="100%" border="1">
                              <thead>
                                <tr>
                                  <th>Code System</th>
                                  <th>Code</th>
                                  <th>Description</th>
                                  <th>Begin Date</th>
                                  <th>End Date</th>
                                </tr>
                              </thead>
                              <tbody>
                        */
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[5];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Code System" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Description" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Begin Date" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "End Date" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[socials.Count + 1];

                        //// Add smoking Status
                        //C_SmokingStatus sResults = db.C_SmokingStatus.FirstOrDefault(c => c.SmokingStatusId == pResults.SmokingStatusId);
                        //if (Parms.Custom == false || dtSocial.Rows.Contains(0)) // SJF 5/5/2014
                        //{
                        //    table.tbody[0].tr[0] = new StrucDocTr();
                        //    table.tbody[0].tr[0].Items = new object[5];
                        //    table.tbody[0].tr[0].Items[0] = new StrucDocTd();
                        //    ((StrucDocTd)table.tbody[0].tr[0].Items[0]).Text = new string[] { "SNOMED-CT" };
                        //    table.tbody[0].tr[0].Items[1] = new StrucDocTd();
                        //    ((StrucDocTd)table.tbody[0].tr[0].Items[1]).Text = new string[] { sResults.SNOMED };
                        //    table.tbody[0].tr[0].Items[2] = new StrucDocTd();
                        //    ((StrucDocTd)table.tbody[0].tr[0].Items[2]).Text = new string[] { sResults.Value };
                        //    table.tbody[0].tr[0].Items[3] = new StrucDocTd();
                        //    ((StrucDocTd)table.tbody[0].tr[0].Items[3]).Text = new string[] {  FormatDate(pResults.SmokingDate) };
                        //    table.tbody[0].tr[0].Items[4] = new StrucDocTd();
                        //    ((StrucDocTd)table.tbody[0].tr[0].Items[4]).Text = new string[] { "" };
                        //}
                        // Add Social History

                        for (int pi = 1; pi <= socials.Count; pi++)
                        {
                            PatientSocialHist social = socials.ElementAt<PatientSocialHist>(pi - 1);
                            if (Parms.Custom == false || dtSocial.Rows.Contains(social.PatSocialHistCntr)) // SJF 5/2/2014
                            {
                                table.tbody[0].tr[pi] = new StrucDocTr();
                                table.tbody[0].tr[pi].Items = new object[5];
                                table.tbody[0].tr[pi].Items[0] = new StrucDocTd();
                                if (social.CodeSystemId == 1)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "CPT 4" };
                                else if (social.CodeSystemId == 2)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-9" };
                                else if (social.CodeSystemId == 3)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-10" };
                                else if (social.CodeSystemId == 5)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "SNOMED-CT" };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "" };
                                table.tbody[0].tr[pi].Items[1] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { social.CodeValue };
                                table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { social.Description };
                                table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { FormatDate(social.BeginDate) };
                                table.tbody[0].tr[pi].Items[4] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[4]).Text = new string[] { FormatDate(social.EndDate) };
                            }
                        }

                        // add in the stuctured entries for Social 
                        int icount = socials.Count;   // SJF 5/2/2014 Added Block
                        if (Parms.Custom) icount = dtSocial.Rows.Count;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount + 1];
                        // End Block
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[socials.Count + 1];

                        // Add smoking Status
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0] = new POCD_MT000040Entry();
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].typeCode = x_ActRelationshipEntry.DRIV;
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].Item = new POCD_MT000040Observation();
                        //POCD_MT000040Observation ob2 = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].Item);
                        //ob2.statusCode = new CS();
                        //ob2.statusCode.code = "completed";
                        //ob2.classCode = "OBS";
                        //ob2.id = new II[1];
                        //ob2.id[0] = new II();
                        //ob2.id[0].root = Guid.NewGuid().ToString();
                        //ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                        //ob2.templateId = new II[1];
                        //ob2.templateId[0] = new II();
                        ////ob2.templateId[0].root = "2.16.840.1.113883.10.20.1.33";
                        //ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.78"; // SJF 4/16/2014


                        //// Smoking Status
                        //if (Parms.Custom == false || dtSocial.Rows.Contains(0)) // SJF 5/5/2014
                        //{
                        //    ob2.code = new CD();
                        //    ob2.code.code = "ASSERTION";
                        //    ob2.code.codeSystem = "2.16.840.1.113883.5.4";
                        //    ob2.text = new ED();
                        //    ob2.text.Text = new string[] { sResults.Value };
                        //    ob2.statusCode = new CS();
                        //    ob2.statusCode.code = "completed";
                        //    ob2.effectiveTime = new IVL_TS();
                        //    ob2.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                        //    ob2.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                        //    ob2.effectiveTime.Items = new QTY[1];
                        //    ob2.effectiveTime.Items[0] = new IVXB_TS();

                        //    if (pResults.SmokingDate == null || pResults.SmokingDate == "")
                        //        ob2.effectiveTime.Items[0].nullFlavor = "NA";
                        //    else
                        //        ((IVXB_TS)ob2.effectiveTime.Items[0]).value = pResults.SmokingDate.Trim();

                        //    ob2.value = new ANY[1];
                        //    ob2.value[0] = new CD();
                        //    ((CD)ob2.value[0]).displayName = sResults.Value;
                        //    ((CD)ob2.value[0]).code = sResults.SNOMED;
                        //    ((CD)ob2.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                        //    ((CD)ob2.value[0]).codeSystemName = "SNOMED-CT";
                        //}

                        //for (int pi = 1; pi <= socials.Count; pi++)
                        //{
                        //    PatientSocialHist social = socials.ElementAt<PatientSocialHist>(pi - 1);
                        // SJF 5/2/2014 Block Change
                        int si = 1;
                        for (int pi2 = 0; pi2 < socials.Count; pi2++)
                        {
                            PatientSocialHist social = socials.ElementAt<PatientSocialHist>(pi2);
                            if (Parms.Custom == false || dtSocial.Rows.Contains(social.PatSocialHistCntr))
                            // End Block
                            {
                                //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].Item = new POCD_MT000040Observation();
                                POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].Item);

                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";

                                ob.classCode = "OBS";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                ob.moodCode = x_ActMoodDocumentObservation.EVN;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();

                                C_SmokingStatus sResults = db.C_SmokingStatus.FirstOrDefault(c => c.SNOMED == social.CodeValue);
                                if (sResults != null)
                                    ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.78";
                                else
                                    ob.templateId[0].root = "2.16.840.1.113883.10.20.1.33";

                                
                                ob.code = new CD();
                                ob.code.code = "ASSERTION";
                                ob.code.codeSystem = "2.16.840.1.113883.5.4";
                                ob.text = new ED();
                                ob.text.Text = new string[] { social.Description };

                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";

                                ob.value = new ANY[1];
                                ob.value[0] = new CD();
                                ((CD)ob.value[0]).displayName = social.Description;

                                if (social.CodeSystemId == 1)
                                {
                                    ((CD)ob.value[0]).code = social.CodeValue;
                                    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                    ((CD)ob.value[0]).codeSystemName = "CPT 4";
                                }
                                else if (social.CodeSystemId == 2)
                                {
                                    ((CD)ob.value[0]).code = social.CodeValue;
                                    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                    ((CD)ob.value[0]).codeSystemName = "ICD-9";
                                }
                                else if (social.CodeSystemId == 3)
                                {
                                    ((CD)ob.value[0]).code = social.CodeValue;
                                    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                    ((CD)ob.value[0]).codeSystemName = "ICD-10";
                                }
                                else if (social.CodeSystemId == 5)
                                {
                                    ((CD)ob.value[0]).code = social.CodeValue;
                                    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                    ((CD)ob.value[0]).codeSystemName = "SNOMED-CT";
                                }
                                else
                                {
                                    ob.code.nullFlavor = "NA";
                                }
                                if (social.BeginDate.Trim() != "" || social.EndDate.Trim() != "")
                                {
                                    ob.effectiveTime = new IVL_TS();
                                    if (social.BeginDate.Trim() != "" && social.EndDate.Trim() != "")
                                    {
                                        ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[2];
                                        ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                        ob.effectiveTime.ItemsElementName[1] = ItemsChoiceType2.high;
                                        ob.effectiveTime.Items = new QTY[2];
                                        ob.effectiveTime.Items[0] = new IVXB_TS();
                                        ((IVXB_TS)ob.effectiveTime.Items[0]).value = social.BeginDate.Trim();
                                        ob.effectiveTime.Items[1] = new IVXB_TS();
                                        ((IVXB_TS)ob.effectiveTime.Items[1]).value = social.EndDate.Trim();
                                    }
                                    else if (social.BeginDate.Trim() != "")
                                    {
                                        ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                                        ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                        ob.effectiveTime.Items = new QTY[1];
                                        ob.effectiveTime.Items[0] = new IVXB_TS();
                                        ((IVXB_TS)ob.effectiveTime.Items[0]).value = social.BeginDate.Trim(); ;
                                    }
                                    else if (social.EndDate.Trim() != "")
                                    {
                                        ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                                        ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.high;
                                        ob.effectiveTime.Items = new QTY[1];
                                        ob.effectiveTime.Items[0] = new IVXB_TS();
                                        ((IVXB_TS)ob.effectiveTime.Items[0]).value = social.EndDate.Trim(); ;
                                    }
                                }
                                else
                                {
                                    ob.effectiveTime = new IVL_TS();
                                    ob.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                                    ob.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                                    ob.effectiveTime.Items = new QTY[1];
                                    ob.effectiveTime.Items[0] = new IVXB_TS();
                                    ob.effectiveTime.Items[0].nullFlavor = "NA";

                                }
                                //ob.value = new ANY[1];
                                //ob.value[0] = new CD();
                                //((CD)ob.value[0]).displayName = social.Description;
                                //if (social.CodeSystemId == 1)
                                //{
                                //    ((CD)ob.value[0]).code = social.CodeValue;
                                //    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                //    ((CD)ob.value[0]).codeSystemName = "CPT 4";
                                //}
                                //else if (social.CodeSystemId == 2)
                                //{
                                //    ((CD)ob.value[0]).code = social.CodeValue;
                                //    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                //    ((CD)ob.value[0]).codeSystemName = "ICD-9";
                                //}
                                //else if (social.CodeSystemId == 3)
                                //{
                                //    ((CD)ob.value[0]).code = social.CodeValue;
                                //    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                //    ((CD)ob.value[0]).codeSystemName = "ICD-10";
                                //}
                                //else if (social.CodeSystemId == 5)
                                //{
                                //    ((CD)ob.value[0]).code = social.CodeValue;
                                //    ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                //    ((CD)ob.value[0]).codeSystemName = "SNOMED-CT";
                                //}
                                //else
                                //{
                                //    ((CD)ob.value[0]).nullFlavor = "NA";
                                //}  
                                si++;
                            }
                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob2 = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[0].Item);
                        ob2.statusCode = new CS();
                        ob2.statusCode.code = "completed";
                        ob2.classCode = "OBS";
                        ob2.id = new II[1];
                        ob2.id[0] = new II();
                        ob2.id[0].root = Guid.NewGuid().ToString();
                        ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob2.templateId = new II[1];
                        ob2.templateId[0] = new II();
                        //ob2.templateId[0].root = "2.16.840.1.113883.10.20.1.33";
                        ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.78"; // SJF 4/16/2014

                        ob2.code = new CD();
                        ob2.code.code = "ASSERTION";
                        ob2.code.codeSystem = "2.16.840.1.113883.5.4";
                        ob2.text = new ED();
                        ob2.text.Text = new string[] { "" };
                        ob2.statusCode = new CS();
                        ob2.statusCode.code = "completed";
                        ob2.effectiveTime = new IVL_TS();
                        ob2.effectiveTime.ItemsElementName = new ItemsChoiceType2[1];
                        ob2.effectiveTime.ItemsElementName[0] = ItemsChoiceType2.low;
                        ob2.effectiveTime.Items = new QTY[1];
                        ob2.effectiveTime.Items[0] = new IVXB_TS();
                        ((IVXB_TS)ob2.effectiveTime.Items[0]).nullFlavor = "NA";

                        ob2.value = new ANY[1];
                        ob2.value[0] = new CD();
                        ((CD)ob2.value[0]).nullFlavor = "NI";

                        componentIndex++;
                    }
                    #endregion

                    #region Family History
                    //*****************************************************
                    // add in the Family History section
                    //*****************************************************

                    List<PatientFamilyHist> FamilyHists = db.PatientFamilyHists.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId).ToList();

                    if (FamilyHists.Count > 0)
                    {
                        // copy over any existing components and expand the component array by one
                        //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        //<section classCode="DOCSECT" moodCode="EVN">
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                        //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;

                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.1.4";
                        section.code = new CE();
                        section.code.nullFlavor = "NA";
                        section.code.code = "10157-6";
                        section.code.codeSystem = "2.16.840.1.113883.6.1";
                        section.code.codeSystemName = "LOINC";
                        //<title>Problems</title>
                        section.title = new ST();
                        section.title.Text = new string[] { "Family History" };
                        //<text mediaType="text/x-hl7-text+xml">
                        section.text = new StrucDocText();

                        // add in the HTML for the social history
                        if (Parms.FamilyHist)
                        {
                            section.text.Items = new object[1];
                            section.text.Items[0] = new StrucDocTable();
                            StrucDocTable table = (StrucDocTable)section.text.Items[0];
                            table.border = "1";
                            table.width = "100%";
                            table.thead = new StrucDocThead();
                            table.thead.tr = new StrucDocTr[1];
                            table.thead.tr[0] = new StrucDocTr();
                            table.thead.tr[0].Items = new object[5];
                            table.thead.tr[0].Items[0] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Code System" };
                            table.thead.tr[0].Items[1] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code" };
                            table.thead.tr[0].Items[2] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Relationship" };
                            table.thead.tr[0].Items[3] = new StrucDocTh();
                            ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Description" };
                            table.tbody = new StrucDocTbody[1];
                            table.tbody[0] = new StrucDocTbody();
                            table.tbody[0].tr = new StrucDocTr[FamilyHists.Count + 1];

                            // Add Family History

                            for (int pi = 1; pi <= FamilyHists.Count; pi++)
                            {
                                PatientFamilyHist family = FamilyHists.ElementAt<PatientFamilyHist>(pi - 1);
                                if (Parms.Custom == false || dtFamilyHist.Rows.Contains(family.PatFamilyHistCntr))
                                {
                                    C_Relationship cResults = db.C_Relationship.FirstOrDefault(C => C.RelationshipId == family.RelationshipId);

                                    table.tbody[0].tr[pi] = new StrucDocTr();
                                    table.tbody[0].tr[pi].Items = new object[5];
                                    table.tbody[0].tr[pi].Items[0] = new StrucDocTd();
                                    if (family.CodeSystemId == 1)
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "CPT 4" };
                                    else if (family.CodeSystemId == 2)
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-9" };
                                    else if (family.CodeSystemId == 3)
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-10" };
                                    else if (family.CodeSystemId == 5)
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "SNOMED-CT" };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "" };
                                    table.tbody[0].tr[pi].Items[1] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { family.CodeValue };
                                    table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { cResults.Value };
                                    table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { family.Description };

                                }
                            }

                            // add in the stuctured entries for Family History 
                            int icount = FamilyHists.Count;
                            if (Parms.Custom) icount = dtFamilyHist.Rows.Count;
                            int si = 0;
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                            for (int si2 = 0; si2 < FamilyHists.Count; si2++)
                            {
                                PatientFamilyHist family = FamilyHists.ElementAt<PatientFamilyHist>(si2);
                                if (Parms.Custom == false || dtFamilyHist.Rows.Contains(family.PatFamilyHistCntr))
                                {
                                    C_Relationship cResults = db.C_Relationship.FirstOrDefault(C => C.RelationshipId == family.RelationshipId);

                                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si] = new POCD_MT000040Entry();
                                    POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si];
                                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].typeCode = x_ActRelationshipEntry.DRIV;
                                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].Item = new POCD_MT000040Organizer();
                                    ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.BATTERY;
                                    ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                                    ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                                    ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                                    ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.1.23";
                                    ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                                    ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                                    // subject
                                    ((POCD_MT000040Organizer)entry.Item).subject = new POCD_MT000040Subject();
                                    ((POCD_MT000040Organizer)entry.Item).subject.contextControlCode = "OP";
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject = new POCD_MT000040RelatedSubject();
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.code = new CE();
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.code.code = cResults.SNOMED;
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.code.codeSystem = "2.16.840.1.113883.6.96"; //"SNOMED"
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.code.displayName = cResults.Value;
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.subject = new POCD_MT000040SubjectPerson();
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.subject.classCode = "PSN";
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.subject.determinerCode = "INSTANCE";
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.subject.birthTime = new TS();
                                    ((POCD_MT000040Organizer)entry.Item).subject.relatedSubject.subject.birthTime.nullFlavor = "NI";

                                    // component
                                    ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[1];
                                    ((POCD_MT000040Organizer)entry.Item).component[0] = new POCD_MT000040Component4();
                                    // observation
                                    ((POCD_MT000040Organizer)entry.Item).component[0].Item = new POCD_MT000040Observation();

                                    //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[si].Item = new POCD_MT000040Observation();

                                    POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040Organizer)entry.Item).component[0].Item);

                                    ob.statusCode = new CS();
                                    ob.statusCode.code = "completed";

                                    ob.classCode = "OBS";
                                    ob.id = new II[1];
                                    ob.id[0] = new II();
                                    ob.id[0].root = Guid.NewGuid().ToString();
                                    ob.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob.templateId = new II[1];
                                    ob.templateId[0] = new II();
                                    ob.templateId[0].root = "2.16.840.1.113883.10.20.1.22";
                                    ob.code = new CD();
                                    ob.code.code = "ASSERTION";
                                    ob.code.codeSystem = "2.16.840.1.113883.5.4";
                                    ob.text = new ED();
                                    ob.text.Text = new string[] { family.Description };

                                    ob.statusCode = new CS();
                                    ob.statusCode.code = "completed";

                                    ob.value = new ANY[1];
                                    ob.value[0] = new CD();
                                    ((CD)ob.value[0]).displayName = family.Description;


                                    if (family.CodeSystemId == 1)
                                    {
                                        ((CD)ob.value[0]).code = family.CodeValue;
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "CPT 4";
                                    }
                                    else if (family.CodeSystemId == 2)
                                    {
                                        ((CD)ob.value[0]).code = family.CodeValue;
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-9";
                                    }
                                    else if (family.CodeSystemId == 3)
                                    {
                                        ((CD)ob.value[0]).code = family.CodeValue;
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-10";
                                    }
                                    else if (family.CodeSystemId == 5)
                                    {
                                        ((CD)ob.value[0]).code = family.CodeValue;
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                        ((CD)ob.value[0]).codeSystemName = "SNOMED-CT";
                                    }
                                    else
                                    {
                                        ob.code.nullFlavor = "NA";
                                    }

                                    si++;
                                }
                            }

                            componentIndex++;
                        }
                    }
                    
                    #endregion

                    #region Proceedures
                    //*****************************************************
                    // add in the Proceedures section
                    //*****************************************************
                    // Get a list of the Procedures
                    // Only SNOMED coded items can be sent in CCCD.
                    List<PatientProcedure> procs = db.PatientProcedures.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.CodeSystemId == 5).ToList();

                    // Check if there are any procedures selected, if not set the flag to false
                    if (procs.Count == 0)
                        Parms.Procedures = false;

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.10" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.12";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.7.1"; // SJF 04/15/2014
                    //<code nullFlavor="NA" code="18776-5" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "47519-4";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Procedures" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the procedures
                    if (Parms.Procedures)    // Only build table if the are procedures selected.
                    {
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[4];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Code System" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Description" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Date" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[procs.Count];

                        for (int pi = 0; pi < procs.Count; pi++)
                        {
                            PatientProcedure proc = procs.ElementAt<PatientProcedure>(pi);
                            if (Parms.Custom == false || dtProcedures.Rows.Contains(proc.PatProcedureCntr)) // SJF 5/2/2014
                            {
                                table.tbody[0].tr[pi] = new StrucDocTr();
                                table.tbody[0].tr[pi].Items = new object[4];
                                table.tbody[0].tr[pi].Items[0] = new StrucDocTd();
                                if (proc.CodeSystemId == 1)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "CPT 4" };
                                else if (proc.CodeSystemId == 2)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-9" };
                                else if (proc.CodeSystemId == 3)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "ICD-10" };
                                else if (proc.CodeSystemId == 5)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "SNOMED-CT" };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "" };

                                table.tbody[0].tr[pi].Items[1] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { proc.CodeValue };
                                table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { proc.Description };
                                table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { FormatDate(proc.ServiceDate) };
                            }
                        }
                    }

                    // add in the stuctured entries for procedure
                    if (Parms.Procedures)    // Only add items if the are procedures selected.
                    {
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[procs.Count];
                        //for (int pi = 0; pi < procs.Count; pi++)
                        //{
                        //    PatientProcedure proc = procs.ElementAt<PatientProcedure>(pi);
                        // SJF 5/2/2014 Block Change
                        int icount = procs.Count;
                        if (Parms.Custom) icount = dtProcedures.Rows.Count;
                        int pi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int pi2 = 0; pi2 < procs.Count; pi2++)
                        {
                            PatientProcedure proc = procs.ElementAt<PatientProcedure>(pi2);
                            if (Parms.Custom == false || dtProcedures.Rows.Contains(proc.PatProcedureCntr))
                            // End Block
                            {
                                //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                                //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Observation();
                                //POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);

                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Procedure();// SJF 54/27/14
                                POCD_MT000040Procedure ob = (POCD_MT000040Procedure)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);

                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";
                                ob.classCode = "PROC";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                //ob.moodCode = x_ActMoodDocumentObservation.EVN;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();
                                //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.29";
                                ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.14"; //SJF 4/27/14
                                ob.code = new CD();
                                if (proc.CodeSystemId == 1)
                                {
                                    ob.code.code = proc.CodeValue;
                                    ob.code.codeSystem = "2.16.840.1.113883.6.2";
                                    ob.code.codeSystemName = "CPT 4";
                                }
                                else if (proc.CodeSystemId == 2)
                                {
                                    ob.code.code = proc.CodeValue;
                                    ob.code.codeSystem = "2.16.840.1.113883.6.2";
                                    ob.code.codeSystemName = "ICD-9";
                                }
                                else if (proc.CodeSystemId == 3)
                                {
                                    ob.code.code = proc.CodeValue;
                                    ob.code.codeSystem = "2.16.840.1.113883.6.90";
                                    ob.code.codeSystemName = "ICD-10";
                                }
                                else if (proc.CodeSystemId == 5)
                                {
                                    ob.code.code = proc.CodeValue;
                                    ob.code.codeSystem = "2.16.840.1.113883.6.96";
                                    ob.code.codeSystemName = "SNOMED-CT";
                                }
                                else
                                {
                                    ob.code.nullFlavor = "NI";
                                }

                                ob.effectiveTime = new IVL_TS();
                                ob.effectiveTime.value = proc.ServiceDate;
                                ob.text = new ED();
                                ob.text.Text = new string[] { proc.Description };
                                pi++;
                            }
                        }

                        componentIndex++;
                    }

                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int pi = 0;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Procedure();// SJF 54/27/14
                        POCD_MT000040Procedure ob = (POCD_MT000040Procedure)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);

                        ob.statusCode = new CS();
                        ob.statusCode.code = "completed";
                        ob.classCode = "PROC";
                        ob.id = new II[1];
                        ob.id[0] = new II();
                        ob.id[0].root = Guid.NewGuid().ToString();
                        //ob.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob.templateId = new II[1];
                        ob.templateId[0] = new II();
                        //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.29";
                        ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.14"; //SJF 4/27/14
                        ob.code = new CD();
                        ob.code.nullFlavor = "NI";
                        ob.effectiveTime = new IVL_TS();
                        ob.effectiveTime.nullFlavor = "NA";
                        ob.text = new ED();
                        ob.text.Text = new string[] { "" };

                        componentIndex++;
                    }
                    #endregion

                    #region Vital Signs
                    //*****************************************************
                    // add in the Vital Signs section
                    //*****************************************************
                    List<PatientVitalSign> vitals = db.PatientVitalSigns.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.Deleted == false).ToList();
                    if (vitals.Count == 0)
                        Parms.VitalSigns = false;

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.10" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.4.1"; // "2.16.840.1.113883.10.20.1.16";
                    //<code nullFlavor="NA" code="18776-5" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "8716-3";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Vital Signs" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the vital
                    if (Parms.VitalSigns) // Only build table if there are vital signs selected
                    {
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[9];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Date / Time" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "BP-Sys(mm[Hg]" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "BP-Dia(mm[Hg]" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "HR(bpm)" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "RR(rpm)" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Temp" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "WT" };
                        table.thead.tr[0].Items[7] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[7]).Text = new string[] { "HT" };
                        table.thead.tr[0].Items[8] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[8]).Text = new string[] { "BMI" };
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[vitals.Count];

                        for (int vi = 0; vi < vitals.Count; vi++)
                        {
                            PatientVitalSign vital = vitals.ElementAt<PatientVitalSign>(vi);
                            if (Parms.Custom == false || dtVitalSigns.Rows.Contains(vital.PatientVitalCntr)) // SJF 5/2/2014
                            {
                                table.tbody[0].tr[vi] = new StrucDocTr();
                                table.tbody[0].tr[vi].Items = new object[9];
                                table.tbody[0].tr[vi].Items[0] = new StrucDocTd();

                                if (Convert.ToDateTime(vital.VitalDate).ToShortTimeString() == "12:00 AM") 
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[0]).Text = new string[] { Convert.ToDateTime(vital.VitalDate).ToShortDateString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[0]).Text = new string[] { vital.VitalDate.ToString() };
                                table.tbody[0].tr[vi].Items[1] = new StrucDocTd();
                                if (vital.Systolic > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[1]).Text = new string[] { vital.Systolic.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[1]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[2] = new StrucDocTd();
                                if (vital.Diastolic > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[2]).Text = new string[] { vital.Diastolic.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[2]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[3] = new StrucDocTd();
                                if (vital.Pulse > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[3]).Text = new string[] { vital.Pulse.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[3]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[4] = new StrucDocTd();
                                if (vital.Respiration > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[4]).Text = new string[] { vital.Respiration.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[4]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[5] = new StrucDocTd();
                                if (vital.Temperature > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[5]).Text = new string[] { vital.Temperature.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[5]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[6] = new StrucDocTd();
                                if (vital.WeightLb > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[6]).Text = new string[] { vital.WeightLb.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[6]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[7] = new StrucDocTd();
                                if (vital.HeightIn > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[7]).Text = new string[] { vital.HeightIn.ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[7]).Text = new string[] { "" };
                                table.tbody[0].tr[vi].Items[8] = new StrucDocTd();
                                if (vital.WeightLb > 0 && vital.HeightIn > 0)
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[8]).Text = new string[] { (Math.Round(Convert.ToDecimal(vital.WeightLb) / (Convert.ToDecimal(vital.HeightIn) * Convert.ToDecimal(vital.HeightIn)) * 703, 1)).ToString() };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[vi].Items[8]).Text = new string[] { "" };
                            }
                        }
                    }
                    // add in the stuctured entries for the vitals
                    if (Parms.VitalSigns) // Only add items if the are vital signs selected
                    {
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[vitals.Count];

                        // Loop through the vitals
                        ///for (int vi = 0; vi < vitals.Count; vi++)
                        //{
                        //  PatientVitalSign vital = vitals.ElementAt<PatientVitalSign>(vi);
                        ///// SJF 5/2/2014 Block Change
                        int icount = vitals.Count;
                        if (Parms.Custom) icount = dtVitalSigns.Rows.Count;
                        int vi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int vi2 = 0; vi2 < vitals.Count; vi2++)
                        {
                            PatientVitalSign vital = vitals.ElementAt<PatientVitalSign>(vi2);
                            if (Parms.Custom == false || dtVitalSigns.Rows.Contains(vital.PatientVitalCntr))
                            // End Block
                            {
                                // Add the organizer
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi] = new POCD_MT000040Entry();
                                POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi];
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item = new POCD_MT000040Organizer();
                                ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.CLUSTER;
                                ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                                ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                                ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                                ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.22.4.26";
                                ((POCD_MT000040Organizer)entry.Item).id = new II[1];
                                ((POCD_MT000040Organizer)entry.Item).id[0] = new II();
                                ((POCD_MT000040Organizer)entry.Item).id[0].root = Guid.NewGuid().ToString();
                                ((POCD_MT000040Organizer)entry.Item).code = new CD();
                                ((POCD_MT000040Organizer)entry.Item).code.codeSystem = "2.16.840.1.113883.6.96"; //"SNOMED"
                                ((POCD_MT000040Organizer)entry.Item).code.code = "46680005";
                                ((POCD_MT000040Organizer)entry.Item).code.displayName = "Vital Signs";
                                ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                                ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                                ((POCD_MT000040Organizer)entry.Item).effectiveTime = new IVL_TS();
                                ((POCD_MT000040Organizer)entry.Item).effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");

                                // Add observation for each item in the vital that has data

                                int obsitems = 0;
                                if (vital.Systolic > 0)
                                    obsitems++;
                                if (vital.Diastolic > 0)
                                    obsitems++;
                                if (vital.Pulse > 0)
                                    obsitems++;
                                if (vital.Respiration > 0)
                                    obsitems++;
                                if (vital.Temperature > 0)
                                    obsitems++;
                                if (vital.WeightLb > 0)
                                    obsitems++;
                                if (vital.HeightIn > 0)
                                    obsitems++;

                                // Create the component array
                                ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[obsitems];

                                int vit = -1;
                                // Systolic
                                if (vital.Systolic > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "8480-6";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Systolic Blood Pressure";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.Systolic.ToString();
                                    ((PQ)ob2.value[0]).unit = "mm[Hg]";
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    //ob2.interpretationCode[0].nullFlavor = "NA";
                                }

                                // Diastolic
                                if (vital.Diastolic > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "8462-4";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Diastolic Blood Pressure";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.Diastolic.ToString();
                                    ((PQ)ob2.value[0]).unit = "mm[Hg]";
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }

                                // Heart rate
                                if (vital.Pulse > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "8867-4";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Heart Rate";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.Pulse.ToString();
                                    ((PQ)ob2.value[0]).unit = "mm[Hg]";
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }

                                // Respiration
                                if (vital.Respiration > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "9279-1";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Respiration";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.Respiration.ToString();
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }

                                // Temperature
                                if (vital.Temperature > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "8310-5";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Systolic Blood Pressure";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.Temperature.ToString();
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }

                                // Weight
                                if (vital.WeightLb > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "3141-9";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Weight Measured";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.WeightLb.ToString();
                                    ((PQ)ob2.value[0]).unit = "[lb_av]";
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }

                                // Height
                                if (vital.HeightIn > 0)
                                {
                                    vit++;
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();
                                    ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                    ob2.code.code = "8302-2";
                                    ob2.code.codeSystemName = "LOINC";
                                    ob2.code.displayName = "Height Measured";
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = Convert.ToDateTime(vital.VitalDate).ToString("yyyyMMddhhmm");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    ((PQ)ob2.value[0]).value = vital.HeightIn.ToString();
                                    ((PQ)ob2.value[0]).unit = "[in_us]";
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].nullFlavor = "NI";
                                }
                                vi++;
                            }
                        }

                        componentIndex++;

                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int vi = 0; 
                        
                        // Add the organizer
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi] = new POCD_MT000040Entry();
                        POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item = new POCD_MT000040Organizer();
                        ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.CLUSTER;
                        ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                        ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                        ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                        ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.22.4.26";
                        ((POCD_MT000040Organizer)entry.Item).id = new II[1];
                        ((POCD_MT000040Organizer)entry.Item).id[0] = new II();
                        ((POCD_MT000040Organizer)entry.Item).id[0].root = Guid.NewGuid().ToString();
                        ((POCD_MT000040Organizer)entry.Item).code = new CD();
                        ((POCD_MT000040Organizer)entry.Item).code.codeSystem = "2.16.840.1.113883.6.96"; //"SNOMED"
                        ((POCD_MT000040Organizer)entry.Item).code.code = "46680005";
                        ((POCD_MT000040Organizer)entry.Item).code.displayName = "Vital Signs";
                        ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                        ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                        ((POCD_MT000040Organizer)entry.Item).effectiveTime = new IVL_TS();
                        ((POCD_MT000040Organizer)entry.Item).effectiveTime.nullFlavor = "NA";

                        // Create the component array
                        ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[1];

                        int vit = 0;
                        ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit] = new POCD_MT000040Component4();
                        POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[vi].Item).component[vit];

                        currentComponent.Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                        ob2.classCode = "OBS";
                        ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob2.templateId = new II[1];
                        ob2.templateId[0] = new II();
                        ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.27";
                        ob2.id = new II[1];
                        ob2.id[0] = new II();
                        ob2.id[0].root = Guid.NewGuid().ToString();
                        ob2.code = new CD();
                        ob2.code.nullFlavor = "NI";
                        ob2.statusCode = new CS();
                        ob2.statusCode.code = "completed";
                        ob2.effectiveTime = new IVL_TS();
                        ob2.effectiveTime.nullFlavor = "NA";
                        ob2.value = new ANY[1];
                        ob2.value[0] = new PQ();
                        ((PQ)ob2.value[0]).nullFlavor = "NA";
                        ob2.interpretationCode = new CE[1];
                        ob2.interpretationCode[0] = new CE();
                        ob2.interpretationCode[0].nullFlavor = "NI";

                        componentIndex++;
                    }
                    #endregion

                    #region Observations / Labs
                    //*****************************************************
                    // add in the Results section
                    //*****************************************************
                    List<PatientLabResult> obs = db.PatientLabResults.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId).ToList();

                    if (obs.Count == 0)
                        Parms.Labs = false;

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.14" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.14";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.3.1"; // SJF 4/24/14
                    //<code nullFlavor="NA" code="30954-2" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "30954-2";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Diagnostic Test Results</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Lab Tests" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the results
                    if (Parms.Labs)  // Only build table if there are labs selected.
                    {
                        /*
                            <table width="100%" border="1">
                                <thead>
                                <tr>
                                    <th>Test</th>
                                    <th>Result</th>
                                    <th>Ref. Range</th>
                                    <th>Abnormal Flag</th>
                                    <th>Date Performed</th>
                                    <th>Set</th>
                                    <th>Order</th>
                                    <th>Code System</th>
                                    <th>Code</th>
                                </tr>
                                </thead>
                                <tbody>
                            */

                        // Build table title
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[7];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Code System" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Test" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Result" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Ref. Range" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Abnormal Flag" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Order Date" };
                       
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[obs.Count];
                        /*
                            <tr>
                                <td>WBC (Leukocytes)</td>
                                <td>&gt;30 /hpf</td>
                                <td>0 -  5</td>
                                <td>A</td>
                                <td>7/15/2011</td>
                                <td>1</td>
                                <td>492</td>
                                <td>Local</td>
                                <td>013128</td>
                            </tr>
                            */
                        // Build table data
                        int oic = 0;
                        for (int oi = 0; oi < obs.Count; oi++)
                        {
                            PatientLabResult ob = obs.ElementAt<PatientLabResult>(oi);
                            if (Parms.Custom == false || dtLabs.Rows.Contains(ob.LabResultCntr)) // SJF 5/2/2014
                            {
                                List<PatientLabResultTest> obst = db.PatientLabResultTests.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.LabResultCntr == ob.LabResultCntr).ToList();
                                for (int oit = 0; oit < obst.Count; oit++)
                                {
                                    PatientLabResultTest obt = obst.ElementAt<PatientLabResultTest>(oit);
                                    table.tbody[0].tr[oic] = new StrucDocTr();
                                    table.tbody[0].tr[oic].Items = new object[7];

                                    string csystem = null;

                                    if (obt.CodeSystemId == 0)
                                        csystem = "Not Coded";
                                    else if (obt.CodeSystemId == 9)
                                        csystem = "LOINC";
                                    else if (obt.CodeSystemId == 5)
                                        csystem = "SNOMED";

                                    table.tbody[0].tr[oic].Items[0] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[0]).Text = new string[] { csystem };
                                    table.tbody[0].tr[oic].Items[1] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[1]).Text = new string[] { obt.CodeValue + "" };
                                    table.tbody[0].tr[oic].Items[2] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[2]).Text = new string[] { ob.LabName + " (" + obt.Component + ")" };
                                    table.tbody[0].tr[oic].Items[3] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[3]).Text = new string[] { obt.Result + " " + obt.Units };
                                    table.tbody[0].tr[oic].Items[4] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[4]).Text = new string[] { obt.RefRange };
                                    table.tbody[0].tr[oic].Items[5] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[5]).Text = new string[] { obt.Abnormal + "" };
                                    table.tbody[0].tr[oic].Items[6] = new StrucDocTd();
                                    ((StrucDocTd)table.tbody[0].tr[oic].Items[6]).Text = new string[] { ob.OrderDate != null ? ((DateTime)ob.OrderDate).ToShortDateString() : "" };
                                                                       
                                }
                                oic++;
                            }
                        }
                    }

 
                    // add in the stuctured entries for the results
                    if (Parms.Labs)  // Only add items if the are labs selected.
                    {
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[obs.Count];

                        // loop through the results, produce an entry for each 
                        //for (int oi = 0; oi < obs.Count; oi++)
                        //{
                        //    PatientLabResult ob = obs.ElementAt<PatientLabResult>(oi);
                        // SJF 5/2/2014 Block Change
                        int icount = obs.Count;
                        if (Parms.Custom) icount = dtLabs.Rows.Count; 
                        int oi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int oi2 = 0; oi2 < obs.Count; oi2++)
                        {
                            PatientLabResult ob = obs.ElementAt<PatientLabResult>(oi2);
                            if (Parms.Custom == false || dtLabs.Rows.Contains(ob.LabResultCntr))
                            // End Block
                            {
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi] = new POCD_MT000040Entry();
                                POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi];
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item = new POCD_MT000040Organizer();
                                ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.BATTERY;
                                ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                                ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                                ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                                //((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.1.32";
                                ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.22.4.1"; // SJF 4/24/14
                                ((POCD_MT000040Organizer)entry.Item).id = new II[1];
                                ((POCD_MT000040Organizer)entry.Item).id[0] = new II();
                                ((POCD_MT000040Organizer)entry.Item).id[0].root = Guid.NewGuid().ToString();
                                ((POCD_MT000040Organizer)entry.Item).code = new CD();
                                if (ob.CodeSystemId == 9)
                                {
                                    ((POCD_MT000040Organizer)entry.Item).code.codeSystem = "2.16.840.1.113883.6.1"; //"LOINC"
                                    ((POCD_MT000040Organizer)entry.Item).code.code = ob.CodeValue.ToString();
                                }
                                else if (ob.CodeSystemId == 5)
                                {
                                    ((POCD_MT000040Organizer)entry.Item).code.codeSystem = "2.16.840.1.113883.6.96"; //"SNOMED"
                                    ((POCD_MT000040Organizer)entry.Item).code.code = ob.CodeValue.ToString();
                                }
                                else
                                {
                                    ((POCD_MT000040Organizer)entry.Item).code.nullFlavor = "NI";
                                }
                                ((POCD_MT000040Organizer)entry.Item).code.displayName = ob.LabName;
                                ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                                ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                                ((POCD_MT000040Organizer)entry.Item).effectiveTime = new IVL_TS();
                                ((POCD_MT000040Organizer)entry.Item).effectiveTime.value = Convert.ToDateTime(ob.OrderDate).ToString("yyyyMMdd");

                                List<PatientLabResultTest> obst = db.PatientLabResultTests.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.LabResultCntr == ob.LabResultCntr).ToList();

                                ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[obst.Count];
                                // Added line below to initialize
                                //((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item).component = new POCD_MT000040Component4[obst.Count];

                                for (int oit = 0; oit < obst.Count; oit++)
                                {
                                    PatientLabResultTest obt = obst.ElementAt<PatientLabResultTest>(oit);
                                    ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item).component[oit] = new POCD_MT000040Component4();
                                    POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item).component[oit];

                                    currentComponent.Item = new POCD_MT000040Observation();
                                    POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                                    ob2.classCode = "OBS";
                                    ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                                    ob2.templateId = new II[1];
                                    ob2.templateId[0] = new II();
                                    //ob2.templateId[0].root = "2.16.840.1.113883.10.20.1.31";
                                    ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.2";  //SJF 4/24/14
                                    ob2.id = new II[1];
                                    ob2.id[0] = new II();
                                    ob2.id[0].root = Guid.NewGuid().ToString();
                                    ob2.code = new CD();


                                    if (obt.CodeSystemId == 9)
                                    {
                                        ob2.code.codeSystem = "2.16.840.1.113883.6.1";
                                        ob2.code.code = obt.CodeValue;
                                        ob2.code.codeSystemName = "LOINC";
                                    }
                                    else if (obt.CodeSystemId == 5)
                                    {
                                        ob2.code.codeSystem = "2.16.840.1.113883.6.96";
                                        ob2.code.code = obt.CodeValue;
                                        ob2.code.codeSystemName = "SNOMED";
                                    }
                                    else
                                    {
                                        ob2.code.nullFlavor = "NI";
                                    }

                                    ob2.code.displayName = obt.Component;
                                    ob2.statusCode = new CS();
                                    ob2.statusCode.code = "completed";
                                    ob2.effectiveTime = new IVL_TS();
                                    ob2.effectiveTime.value = ((DateTime)ob.ReportDate).ToString("yyyyMMdd");
                                    ob2.value = new ANY[1];
                                    ob2.value[0] = new PQ();
                                    try
                                    {
                                        decimal decCk = Convert.ToDecimal(obt.Result);
                                        ((PQ)ob2.value[0]).value = obt.Result;
                                    }
                                    catch
                                    {
                                        ((PQ)ob2.value[0]).value = "0";
                                    }
                                    if (obt.Units == "")
                                        ((PQ)ob2.value[0]).unit = "NA";
                                    else
                                        ((PQ)ob2.value[0]).unit = obt.Units;


                                    // this is the place where we would say the test result is Normal, Low, Susceptible, etc
                                    ob2.interpretationCode = new CE[1];
                                    ob2.interpretationCode[0] = new CE();
                                    ob2.interpretationCode[0].code = obt.Abnormal;
                                    ob2.interpretationCode[0].codeSystem = "2.16.840.1.113883.5.83";
                                    if (obt.RefRange != null && obt.RefRange != "")
                                    {
                                        ob2.referenceRange = new POCD_MT000040ReferenceRange[1];
                                        ob2.referenceRange[0] = new POCD_MT000040ReferenceRange();
                                        ob2.referenceRange[0].observationRange = new POCD_MT000040ObservationRange();
                                        ob2.referenceRange[0].observationRange.text = new ED();
                                        ob2.referenceRange[0].observationRange.text.Text = new string[] { obt.RefRange };
                                    }
                                }
                                oi++;   //SJF 5/1/2014
                            }

                        }
                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int oi = 0;
                        
                        //PatientLabResult ob = obs.ElementAt<PatientLabResult>(oi);

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi] = new POCD_MT000040Entry();
                        POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item = new POCD_MT000040Organizer();
                        ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.BATTERY;
                        ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                        ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                        ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                        //((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.1.32";
                        ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.22.4.1"; // SJF 4/24/14
                        ((POCD_MT000040Organizer)entry.Item).id = new II[1];
                        ((POCD_MT000040Organizer)entry.Item).id[0] = new II();
                        ((POCD_MT000040Organizer)entry.Item).id[0].root = Guid.NewGuid().ToString();
                        ((POCD_MT000040Organizer)entry.Item).code = new CD();
                        ((POCD_MT000040Organizer)entry.Item).code.nullFlavor = "NI";
                        
                        ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                        ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                        ((POCD_MT000040Organizer)entry.Item).effectiveTime = new IVL_TS();
                        ((POCD_MT000040Organizer)entry.Item).effectiveTime.nullFlavor = "NA";

                        ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[1];
                        int oit = 0;
                        
                        ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item).component[oit] = new POCD_MT000040Component4();
                        POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[oi].Item).component[oit];

                        currentComponent.Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob2 = (POCD_MT000040Observation)currentComponent.Item;
                        ob2.classCode = "OBS";
                        ob2.moodCode = x_ActMoodDocumentObservation.EVN;
                        ob2.templateId = new II[1];
                        ob2.templateId[0] = new II();
                        //ob2.templateId[0].root = "2.16.840.1.113883.10.20.1.31";
                        ob2.templateId[0].root = "2.16.840.1.113883.10.20.22.4.2";  //SJF 4/24/14
                        ob2.id = new II[1];
                        ob2.id[0] = new II();
                        ob2.id[0].root = Guid.NewGuid().ToString();
                        ob2.code = new CD();

                        ob2.code.nullFlavor = "NI";
                        ob2.statusCode = new CS();
                        ob2.statusCode.code = "completed";
                        ob2.effectiveTime = new IVL_TS();
                        ob2.effectiveTime.nullFlavor = "NA";
                        ob2.value = new ANY[1];
                        ob2.value[0] = new PQ();
                        ((PQ)ob2.value[0]).value = "0";
                        ((PQ)ob2.value[0]).unit = "NA";
                        ob2.interpretationCode = new CE[1];
                        ob2.interpretationCode[0] = new CE();
                        ob2.interpretationCode[0].nullFlavor = "NI";
                        componentIndex++;
                    }
                    #endregion

                    #region Plan Of Care
                    //*****************************************************
                    // add in the Plan Of Care section
                    //*****************************************************
                    //List<PatientPlanOfCare> plans = db.PatientPlanOfCares.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId).ToList();

                    List<PatientPlanOfCare> plans = db.PatientPlanOfCares.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId
                            && p.InstructionTypeId != 4).ToList();   // SJF 05/27/2014

                    if (plans.Count == 0)
                    {
                        Parms.PlanOfCare = false;
                        //Parms.ClinicalInstructions = false;
                        //Parms.FutureAppointments = false;
                        //Parms.Referrals = false;
                        //Parms.ScheduledTests = false;
                        //Parms.DecisionAids = false;
                    }

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //<section classCode="DOCSECT" moodCode="EVN">
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //<templateId root="2.16.840.1.113883.10.20.1.10" />
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    //section.templateId[0].root = "2.16.840.1.113883.10.20.1.10";
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.10"; // SJF 4/27/14
                    //<code nullFlavor="NA" code="18776-5" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "18776-5";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Care Plan" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the problems
                    if (Parms.PlanOfCare == true)
                    //if (Parms.ClinicalInstructions == true || Parms.FutureAppointments == true || Parms.Referrals == true || Parms.ScheduledTests == true || Parms.DecisionAids == true)
                    {
                        /*  <table width="100%" border="1">
                                <thead>
                                <tr>
                                    <th>SNOMED Code</th>
                                    <th>Patient Problem</th>
                                    <th>Status</th>
                                    <th>Date Diagnosed</th>
                                </tr>
                                </thead>
                                <tbody>
                        */
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[7];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Type" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code System" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Code" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Instructions" }; //  Plan Activity
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Comments" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Planned Date/Time" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Goals" }; // Plan/Goal
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[plans.Count];

                        for (int pi = 0; pi < plans.Count; pi++)
                        {
                            PatientPlanOfCare plan = plans.ElementAt<PatientPlanOfCare>(pi);
                            if (Parms.Custom == false || dtPlanOfCare.Rows.Contains(plan.PlanCntr)) // SJF 5/2/2014
                            {
                                table.tbody[0].tr[pi] = new StrucDocTr();
                                table.tbody[0].tr[pi].Items = new object[7];

                                table.tbody[0].tr[pi].Items[0] = new StrucDocTd();

                                if (plan.InstructionTypeId == 1)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Future Appointment" };
                                else if (plan.InstructionTypeId == 3)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Care Plan" };
                                else if (plan.InstructionTypeId == 5)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Referral" };
                                else if (plan.InstructionTypeId == 6)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Scheduled Test" };
                                else if (plan.InstructionTypeId == 7)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Decision Aid" };
                                else if (plan.InstructionTypeId == 8)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Pending Test" };

                                table.tbody[0].tr[pi].Items[1] = new StrucDocTd();

                                if (plan.CodeSystemId == 1)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "CPT 4" };
                                else if (plan.CodeSystemId == 2)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "ICD-9" };
                                else if (plan.CodeSystemId == 3)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "ICD-10" };
                                else if (plan.CodeSystemId == 5)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "SNOMED-CT" };
                                else if (plan.CodeSystemId == 9)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "LOINC" };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "" };

                                table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { dr["CodeValue"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { plan.CodeValue };
                                }

                                table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { dr["Plan Activity"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { plan.Instruction };
                                }
                                table.tbody[0].tr[pi].Items[4] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[4]).Text = new string[] { dr["Comments"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[4]).Text = new string[] { plan.Note };
                                }

                                table.tbody[0].tr[pi].Items[5] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    if (dr["Planned Date/Time"] == null ||dr["Planned Date/Time"].ToString() == "")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (dr["Planned Date/Time"].ToString() == "1/1/1900 12:00:00 AM" || dr["Planned Date/Time"].ToString() == "1/1/1900 00:00:00")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (Convert.ToDateTime(dr["Planned Date/Time"]).ToShortTimeString() == "12:00 AM")   //(dr["Planned Date/Time"].ToString().Substring(dr["Planned Date/Time"].ToString().Length - 10, 11) == "12:00:00 AM") // 
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { Convert.ToDateTime(dr["Planned Date/Time"]).ToShortDateString() };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { dr["Planned Date/Time"].ToString() };
                                }
                                else
                                {
                                    if (plan.AppointmentDateTime.ToString() == "1/1/1900 12:00:00 AM" || plan.AppointmentDateTime.ToString() == "1/1/1900 00:00:00")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (Convert.ToDateTime(plan.AppointmentDateTime).ToShortTimeString() == "12:00 AM")   //(dr["Planned Date/Time"].ToString().Substring(dr["Planned Date/Time"].ToString().Length - 10, 11) == "12:00:00 AM") // 
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { Convert.ToDateTime(plan.AppointmentDateTime).ToShortDateString() };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { plan.AppointmentDateTime.ToString() };
                                }

                                table.tbody[0].tr[pi].Items[6] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[6]).Text = new string[] { dr["Goal"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[6]).Text = new string[] { plan.Goal };
                                }

                            }
                        }
                    }
                    // add in the stuctured entries for the plan of care
                    if (Parms.PlanOfCare == true)
                    //if (Parms.ClinicalInstructions == true || Parms.FutureAppointments == true || Parms.Referrals == true || Parms.ScheduledTests == true || Parms.DecisionAids == true)
                    {
                        /* <entry typeCode="DRIV">

                            <observation classCode="OBS" moodCode="RQO">
                                <templateId root="2.16.840.1.113883.10.20.1.10" />
                                <id root="ed39c95a-a2fd-4c83-9010-58ff4a809628" />
                                <code code="18776-5" codeSystem="2.16.840.1.113883.5.4" />
                                <statusCode code="completed" />
                                <effectiveTime value="20111104" />
                                <value xsi:type="CD" code="195967001" codeSystem="2.16.840.1.113883.6.96" displayName="Asthma" />
                            </observation>
                            </entry>*/
                        //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[plans.Count];
                        //for (int pi = 0; pi < plans.Count; pi++)
                        //{
                        //    PatientPlanOfCare plan = plans.ElementAt<PatientPlanOfCare>(pi);
                        // SJF 5/2/2014 Block Change
                        int icount = plans.Count; 
                        if (Parms.Custom) 
                            icount = dtPlanOfCare.Rows.Count; 
                        int pi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int pi2 = 0; pi2 < plans.Count; pi2++)
                        {
                            PatientPlanOfCare plan = plans.ElementAt<PatientPlanOfCare>(pi2);
                            if (Parms.Custom == false || dtPlanOfCare.Rows.Contains(plan.PlanCntr))
                            // End Block
                            {
                                //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Observation();
                                POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";
                                ob.classCode = "OBS";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                ob.moodCode = x_ActMoodDocumentObservation.RQO;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();
                                //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.25";
                                ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.40"; // SJF 4/27/14
                                ob.code = new CD();
                                ob.code.nullFlavor = "NI";
                                if (Convert.ToDateTime(plan.AppointmentDateTime) != Convert.ToDateTime("1/1/1900"))
                                {
                                    ob.effectiveTime = new IVL_TS();
                                    ob.effectiveTime.value = UnFormatDateTime(Convert.ToDateTime(plan.AppointmentDateTime));
                                }
                                ob.text = new ED();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    DataRow dr = dtPlanOfCare.Rows.Find(plan.PlanCntr);
                                    ob.text.Text = new string[] { dr["Comments"].ToString() };
                                }
                                else
                                {
                                    ob.text.Text = new string[] { plan.Instruction };
                                }
                                if (plan.CodeValue != "")
                                {
                                    ob.value = new ANY[1];
                                    ob.value[0] = new CD();
                                    ((CD)ob.value[0]).displayName = plan.Instruction;
                                    ((CD)ob.value[0]).code = plan.CodeValue;

                                    if (plan.CodeSystemId == 1)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "CPT 4";
                                    }
                                    else if (plan.CodeSystemId == 2)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-9";
                                    }
                                    else if (plan.CodeSystemId == 3)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-10";
                                    }
                                    else if (plan.CodeSystemId == 5)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                        ((CD)ob.value[0]).codeSystemName = "SNOMED-CT";
                                    }
                                    else if (plan.CodeSystemId == 9)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.1";
                                        ((CD)ob.value[0]).codeSystemName = "LOINC";
                                    }
                                }
                                pi++; // SJF 5/2/2014
                            }

                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int pi = 0;

                        //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                        ob.statusCode = new CS();
                        ob.statusCode.code = "completed";
                        ob.classCode = "OBS";
                        ob.id = new II[1];
                        ob.id[0] = new II();
                        ob.id[0].root = Guid.NewGuid().ToString();
                        ob.moodCode = x_ActMoodDocumentObservation.RQO;
                        ob.templateId = new II[1];
                        ob.templateId[0] = new II();
                        //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.25";
                        ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.40"; // SJF 4/27/14
                        ob.code = new CD();
                        ob.code.nullFlavor = "NI";
                        ob.text = new ED();
                        ob.text.Text = new string[] { "" };

                        componentIndex++;
                    }
                    #endregion

                    #region Clinical Instructions
                    //*****************************************************
                    // add in the Clinical Instructions section  - Broke out from Care Plan SJF 05/27/2014
                    //*****************************************************
                    List<PatientPlanOfCare> insts = db.PatientPlanOfCares.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId
                            && (p.InstructionTypeId == 4)).ToList();   // SJF 05/27/2014
                    //Added By Talha
                    Parms.ClinicalInstructions = true;
                    //Parms.PlanOfCare = true;
                    if (Parms.Custom && dtClinical.Rows.Count == 0)
                    {
                        //Added By Talha
                        Parms.ClinicalInstructions = false;
                        //Parms.PlanOfCare = false;
                    }
                    if (insts.Count == 0)
                    {
                        //Added By Talha
                        Parms.ClinicalInstructions = false;
                        //Parms.PlanOfCare = false;
                    }

                    // copy over any existing components and expand the component array by one
                    //POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    for (int ci = 0; ci < componentIndex; ci++)
                        newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //<component>
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    section.templateId = new II[1];
                    section.templateId[0] = new II();
                    section.templateId[0].root = "2.16.840.1.113883.10.20.22.2.45"; 
                    section.code = new CE();
                    section.code.nullFlavor = "NA";
                    section.code.code = "18776-5";
                    section.code.codeSystem = "2.16.840.1.113883.6.1";
                    section.code.codeSystemName = "LOINC";
                    //<title>Problems</title>
                    section.title = new ST();
                    section.title.Text = new string[] { "Clinical Instructions" };
                    //<text mediaType="text/x-hl7-text+xml">
                    section.text = new StrucDocText();

                    // add in the HTML for the instructions
                    //Added By Talha
                    if (Parms.ClinicalInstructions == true)
                    {
   
                        section.text.Items = new object[1];
                        section.text.Items[0] = new StrucDocTable();
                        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                        table.border = "1";
                        table.width = "100%";
                        table.thead = new StrucDocThead();
                        table.thead.tr = new StrucDocTr[1];
                        table.thead.tr[0] = new StrucDocTr();
                        table.thead.tr[0].Items = new object[7];
                        table.thead.tr[0].Items[0] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Type" };
                        table.thead.tr[0].Items[1] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Code System" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Code" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Instructions" }; //  Plan Activity
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Comments" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Planned Date/Time" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Goals" }; // Plan/Goal
                        table.tbody = new StrucDocTbody[1];
                        table.tbody[0] = new StrucDocTbody();
                        table.tbody[0].tr = new StrucDocTr[insts.Count];

                        for (int pi = 0; pi < insts.Count; pi++)
                        {
                            PatientPlanOfCare inst = insts.ElementAt<PatientPlanOfCare>(pi);
                            //dtPlanOfCare
                            //Added By Talha
                            if (Parms.Custom == false || dtClinical.Rows.Contains(inst.PlanCntr)) // SJF 5/2/2014
                            {
                                table.tbody[0].tr[pi] = new StrucDocTr();
                                table.tbody[0].tr[pi].Items = new object[7];

                                table.tbody[0].tr[pi].Items[0] = new StrucDocTd();

                                if (inst.InstructionTypeId == 4)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { "Clinical Instruction" };

                                table.tbody[0].tr[pi].Items[1] = new StrucDocTd();

                                if (inst.CodeSystemId == 1)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "CPT 4" };
                                else if (inst.CodeSystemId == 2)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "ICD-9" };
                                else if (inst.CodeSystemId == 3)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "ICD-10" };
                                else if (inst.CodeSystemId == 5)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "SNOMED-CT" };
                                else if (inst.CodeSystemId == 9)
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "LOINC" };
                                else
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { "" };

                                table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                   // DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { dr["CodeValue"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { inst.CodeValue };
                                }

                                table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                    //DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { dr["Plan Activity"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { inst.Instruction };
                                }
                                table.tbody[0].tr[pi].Items[4] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                    //DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[4]).Text = new string[] { dr["Comments"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[4]).Text = new string[] { inst.Note };
                                }

                                table.tbody[0].tr[pi].Items[5] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                    //DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    if (dr["Planned Date/Time"] == null || dr["Planned Date/Time"].ToString() == "")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (dr["Planned Date/Time"].ToString() == "1/1/1900 12:00:00 AM" || dr["Planned Date/Time"].ToString() == "1/1/1900 00:00:00")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (Convert.ToDateTime(dr["Planned Date/Time"]).ToShortTimeString() == "12:00 AM")   //(dr["Planned Date/Time"].ToString().Substring(dr["Planned Date/Time"].ToString().Length - 10, 11) == "12:00:00 AM") // 
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { Convert.ToDateTime(dr["Planned Date/Time"]).ToShortDateString() };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { dr["Planned Date/Time"].ToString() };
                                }
                                else
                                {
                                    if (inst.AppointmentDateTime.ToString() == "1/1/1900 12:00:00 AM" || inst.AppointmentDateTime.ToString() == "1/1/1900 00:00:00")
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { "" };
                                    else if (Convert.ToDateTime(inst.AppointmentDateTime).ToShortTimeString() == "12:00 AM")   //(dr["Planned Date/Time"].ToString().Substring(dr["Planned Date/Time"].ToString().Length - 10, 11) == "12:00:00 AM") // 
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { Convert.ToDateTime(inst.AppointmentDateTime).ToShortDateString() };
                                    else
                                        ((StrucDocTd)table.tbody[0].tr[pi].Items[5]).Text = new string[] { inst.AppointmentDateTime.ToString() };
                                }

                                table.tbody[0].tr[pi].Items[6] = new StrucDocTd();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                    //DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[6]).Text = new string[] { dr["Goal"].ToString() };
                                }
                                else
                                {
                                    ((StrucDocTd)table.tbody[0].tr[pi].Items[6]).Text = new string[] { inst.Goal };
                                }

                            }
                        }
                    }
                    //Added By Talha
                    // add in the stuctured entries for the plan of care
                    //PlanOfCare
                    if (Parms.ClinicalInstructions == true)
                    {
                        int icount = insts.Count;
                        if (Parms.Custom)
                            //Added By Talha
                            //dtPlanOfCare
                            icount = dtClinical.Rows.Count;
                        int pi = 0;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[icount];
                        for (int pi2 = 0; pi2 < insts.Count; pi2++)
                        {
                            PatientPlanOfCare inst = insts.ElementAt<PatientPlanOfCare>(pi2);
                            //Added By Talha
                            //dtPlanOfCare
                            if (Parms.Custom == false || dtClinical.Rows.Contains(inst.PlanCntr))
                            // End Block
                            {
                                //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                                ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Observation();
                                POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                                ob.statusCode = new CS();
                                ob.statusCode.code = "completed";
                                ob.classCode = "OBS";
                                ob.id = new II[1];
                                ob.id[0] = new II();
                                ob.id[0].root = Guid.NewGuid().ToString();
                                ob.moodCode = x_ActMoodDocumentObservation.RQO;
                                ob.templateId = new II[1];
                                ob.templateId[0] = new II();
                                //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.25";
                                ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.40"; // SJF 4/27/14
                                ob.code = new CD();
                                ob.code.nullFlavor = "NI";
                                if (Convert.ToDateTime(inst.AppointmentDateTime) != Convert.ToDateTime("1/1/1900"))
                                {
                                    ob.effectiveTime = new IVL_TS();
                                    ob.effectiveTime.value = UnFormatDateTime(Convert.ToDateTime(inst.AppointmentDateTime));
                                }
                                ob.text = new ED();
                                if (Parms.Custom)   // SJF 5/2/2014 Added if block
                                {
                                    //Added By Talha
                                    //DataRow dr = dtPlanOfCare.Rows.Find(inst.PlanCntr);
                                    DataRow dr = dtClinical.Rows.Find(inst.PlanCntr);
                                    ob.text.Text = new string[] { dr["Comments"].ToString() };
                                }
                                else
                                {
                                    ob.text.Text = new string[] { inst.Instruction };
                                }
                                if (inst.CodeValue != "")
                                {
                                    ob.value = new ANY[1];
                                    ob.value[0] = new CD();
                                    ((CD)ob.value[0]).displayName = inst.Instruction;
                                    ((CD)ob.value[0]).code = inst.CodeValue;

                                    if (inst.CodeSystemId == 1)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "CPT 4";
                                    }
                                    else if (inst.CodeSystemId == 2)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.2";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-9";
                                    }
                                    else if (inst.CodeSystemId == 3)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.90";
                                        ((CD)ob.value[0]).codeSystemName = "ICD-10";
                                    }
                                    else if (inst.CodeSystemId == 5)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                                        ((CD)ob.value[0]).codeSystemName = "SNOMED-CT";
                                    }
                                    else if (inst.CodeSystemId == 9)
                                    {
                                        ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.1";
                                        ((CD)ob.value[0]).codeSystemName = "LOINC";
                                    }
                                }
                                pi++; // SJF 5/2/2014
                            }

                        }

                        componentIndex++;
                    }
                    else
                    {
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[1];
                        int pi = 0;

                        //C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi] = new POCD_MT000040Entry();
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].typeCode = x_ActRelationshipEntry.DRIV;
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item = new POCD_MT000040Observation();
                        POCD_MT000040Observation ob = (POCD_MT000040Observation)(((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[pi].Item);
                        ob.statusCode = new CS();
                        ob.statusCode.code = "completed";
                        ob.classCode = "OBS";
                        ob.id = new II[1];
                        ob.id[0] = new II();
                        ob.id[0].root = Guid.NewGuid().ToString();
                        ob.moodCode = x_ActMoodDocumentObservation.RQO;
                        ob.templateId = new II[1];
                        ob.templateId[0] = new II();
                        //ob.templateId[0].root = "2.16.840.1.113883.10.20.1.25";
                        ob.templateId[0].root = "2.16.840.1.113883.10.20.22.4.40"; // SJF 4/27/14
                        ob.code = new CD();
                        ob.code.nullFlavor = "NI";
                        ob.text = new ED();
                        ob.text.Text = new string[] { "" };

                        componentIndex++;
                    }
                    #endregion

                    try
                    {
                        // write out the ccd document
                       //Serialize<POCD_MT000040ClinicalDocument>(ccd2, @"c:\temp\testccd.xml");
                        CCD = Serialize<POCD_MT000040ClinicalDocument>(ccd2);
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
