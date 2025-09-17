// Service Name  : clsCreateDocument
// Date Created  : 11/30/2013
// Written By    : Stephen Farkas
// Version       : 2013.01
// Description   : Create a CCDA Formatted Document
//                  Required Items - Medication Allergies, Medications, Problems, Immunizations, Lab Tests/Results??,  Plan of Care or Assessment and Plan
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
    class clsCreateDocument
    {

        private bool createPatientCCD(Int64 PatientId, Int64 FacilityId, Int64 VisitId)
        {
            try
            {
                // example of how to read in a CCD and output it back to another file
                //POCD_MT000040ClinicalDocument ccd = Deserialize<POCD_MT000040ClinicalDocument>(@"C:\Temp\CCDstuff\testccd.xml");
                //Serialize<POCD_MT000040ClinicalDocument>(ccd, @"C:\Temp\CCDstuff\testccd-output.xml");

                using (var db = new AMREntities())
                {
                    Patient pResults = db.Patients.FirstOrDefault(p => p.PatientId == PatientId);
                    PatientVisit vResults = db.PatientVisits.FirstOrDefault(v => v.PatientId == PatientId && v.FacilityId == FacilityId && v.VisitId == VisitId);
                    Facility fResults = db.Facilities.FirstOrDefault(f => f.FacilityId == FacilityId);
                    string orgName = fResults.FacilityName;


                    //Demographic d = db.SelectFirst<Demographic>("id=" + PatientId);
                    // new document
                    //<ClinicalDocument xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" moodCode="EVN" xmlns="urn:hl7-org:v3">
                    POCD_MT000040ClinicalDocument ccd2 = new POCD_MT000040ClinicalDocument();
                    ccd2.typeId = new POCD_MT000040InfrastructureRoottypeId();
                    ccd2.typeId.root = "2.16.840.1.113883.1.3";
                    ccd2.typeId.extension = "POCD_HD000040";

                    // <templateId root="2.16.840.1.113883.10.20.1" />
                    ccd2.templateId = new II[1];
                    ccd2.templateId[0] = new II();
                    ccd2.templateId[0].root = "2.16.840.1.113883.10.20.1"; // <!-- CCD v1.0 Templates Root -->
                    ccd2.templateId[0].extension = null;

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
                    ccd2.recordTarget[0].patientRole.id[0].extension = pResults.SSN;
                    ccd2.recordTarget[0].patientRole.addr = new AD[1];
                    ccd2.recordTarget[0].patientRole.addr[0] = new AD();
                    ccd2.recordTarget[0].patientRole.addr[0].use = new string[] { "H" };
                    ccd2.recordTarget[0].patientRole.addr[0].Items = new ADXP[4];
                    ccd2.recordTarget[0].patientRole.addr[0].Items[0] = new adxpstreetAddressLine();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[0].Text = new string[] { (pResults.Address1 + " " + pResults.Address2).Trim() };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[1] = new adxpcity();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[1].Text = new string[] { pResults.City };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[2] = new adxpstate();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[2].Text = new string[] { pResults.State };
                    ccd2.recordTarget[0].patientRole.addr[0].Items[3] = new adxppostalCode();
                    ccd2.recordTarget[0].patientRole.addr[0].Items[3].Text = new string[] { pResults.Zip };
                    ccd2.recordTarget[0].patientRole.telecom = new TEL[1];
                    ccd2.recordTarget[0].patientRole.telecom[0] = new TEL();
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
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.code = pResults.GenderId != null ? (pResults.GenderId == 1 ? "M" :(pResults.GenderId == 2 ? "F" : "U")) : "U";
                    ccd2.recordTarget[0].patientRole.patient.administrativeGenderCode.codeSystem = "2.16.840.1.113883.5.1";
                    ccd2.recordTarget[0].patientRole.patient.birthTime = new TS();
                    ccd2.recordTarget[0].patientRole.patient.birthTime.value = DateTime.Now.ToString("yyyyMMdd");
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
                    ccd2.author[0].assignedAuthor.id[0].root = Guid.NewGuid().ToString();
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
                    ccd2.author[0].assignedAuthor.nullFlavor = "NA";

                    // add in the informant information
                    /* <informant typeCode="INF" contextControlCode="OP">
                        <assignedEntity classCode="ASSIGNED">
                          <id nullFlavor="NI" />
                          <representedOrganization classCode="ORG" determinerCode="INSTANCE">
                            <id nullFlavor="NI" />
                            <name>orgName</name>
                          </representedOrganization>
                        </assignedEntity>
                      </informant>*/
                    ccd2.informant = new POCD_MT000040Informant12[1];
                    ccd2.informant[0] = new POCD_MT000040Informant12();
                    ccd2.informant[0].Item = new POCD_MT000040AssignedEntity();
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id = new II[1];
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0] = new II();
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0].nullFlavor = "NI"; // no organization id
                    //((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).id[0].root = "id would go here";

                    // add in the informant organization information
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization = new POCD_MT000040Organization();
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.classCode = "ORG";
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.determinerCode = "INSTANCE";
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id = new II[1];
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id[0] = new II();
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.id[0].nullFlavor = "NI";
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name = new ON[1];
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name[0] = new ON();
                    ((POCD_MT000040AssignedEntity)ccd2.informant[0].Item).representedOrganization.name[0].Text = new string[] { orgName };

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
                    ccd2.custodian.assignedCustodian.representedCustodianOrganization.name.Text = new string[] { orgName };

                    // add in the legal authenticator
                    /*<legalAuthenticator typeCode="LA" contextControlCode="OP">
                        <time value="20111109552200-0700" />
                        <signatureCode code="S" />
                        <assignedEntity classCode="ASSIGNED">
                          <id nullFlavor="NI" />
                          <representedOrganization classCode="ORG" determinerCode="INSTANCE">
                            <id nullFlavor="NI" />
                            <name>orgName</name>
                          </representedOrganization>
                        </assignedEntity>
                      </legalAuthenticator>*/
                    ccd2.legalAuthenticator = new POCD_MT000040LegalAuthenticator();
                    ccd2.legalAuthenticator.time = new TS();
                    ccd2.legalAuthenticator.time.value = ccd2.effectiveTime.value;
                    ccd2.legalAuthenticator.signatureCode = new CS();
                    ccd2.legalAuthenticator.signatureCode.code = "S";
                    ccd2.legalAuthenticator.assignedEntity = new POCD_MT000040AssignedEntity();
                    ccd2.legalAuthenticator.assignedEntity.id = new II[1];
                    ccd2.legalAuthenticator.assignedEntity.id[0] = new II();
                    ccd2.legalAuthenticator.assignedEntity.id[0].nullFlavor = "NI";
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization = new POCD_MT000040Organization();
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.id = new II[1];
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0] = new II();
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0].nullFlavor = "NI"; // no organization id
                    //ccd2.legalAuthenticator.assignedEntity.representedOrganization.id[0].root = "id would go here";
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.name = new ON[1];
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.name[0] = new ON();
                    ccd2.legalAuthenticator.assignedEntity.representedOrganization.name[0].Text = new string[] { orgName };

                    // add in the outter component
                    //  <component>
                    ccd2.component = new POCD_MT000040Component2();

                    // add in the structured body
                    //    <structuredBody classCode="DOCBODY" moodCode="EVN">
                    ccd2.component.Item = new POCD_MT000040StructuredBody();
                    #endregion

                    int componentIndex = 0;

                    #region Medications - Need RxNorm
                    //*****************************************************
                    // add in the medications section
                    //*****************************************************
                    //List<CodedMedication> meds = db.SelectNoLock<CodedMedication>("demographic_id=" + d.Id);

                    List<PatientMedication> meds = db.PatientMedications.Where(p => p.PatientId == PatientId && p.VisitId == VisitId && p.Deleted == false && p.Active == true).ToList();

                    if (meds.Count > 0)
                    {
                        // copy over any existing components and expand the component array by one
                        POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        //<section classCode="DOCSECT" moodCode="EVN">
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                        POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        //<templateId root="2.16.840.1.113883.10.20.1.8" />
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.1.8";
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
                        /*  <table width="100%" border="1">
                              <thead>
                                <tr>
                                  <th>RxNorm Code</th>
                                  <th>Product</th>
                                  <th>Generic Name</th>
                                  <th>Brand Name</th>
                                  <th>Strength</th>
                                  <th>Dose</th>
                                  <th>Route</th>
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
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Generic Name" };
                        table.thead.tr[0].Items[3] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Brand Name" };
                        table.thead.tr[0].Items[4] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Strength" };
                        table.thead.tr[0].Items[5] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Dose" };
                        table.thead.tr[0].Items[6] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Route" };
                        table.thead.tr[0].Items[7] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[7]).Text = new string[] { "Frequency" };
                        table.thead.tr[0].Items[8] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[8]).Text = new string[] { "Date Started" };
                        table.thead.tr[0].Items[9] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[9]).Text = new string[] { "Status" };
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
                            //PatientMedication med = meds.ElementAt<PatientMedication>(mi);
                            ////CodedMedication med = meds.ElementAt<CodedMedication>(mi);
                            //table.tbody[0].tr[mi] = new StrucDocTr();
                            //table.tbody[0].tr[mi].Items = new object[10];
                            //table.tbody[0].tr[mi].Items[0] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[0]).Text = new string[] { med.RxNormId };
                            //table.tbody[0].tr[mi].Items[1] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[1]).Text = new string[] { "Medication" };
                            //table.tbody[0].tr[mi].Items[2] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[2]).Text = new string[] { med.GenericName };
                            //table.tbody[0].tr[mi].Items[3] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[3]).Text = new string[] { med.BrandName };
                            //table.tbody[0].tr[mi].Items[4] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[4]).Text = new string[] { med.Strength };
                            //table.tbody[0].tr[mi].Items[5] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[5]).Text = new string[] { med.Dose };
                            //table.tbody[0].tr[mi].Items[6] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[6]).Text = new string[] { med.Route };
                            //table.tbody[0].tr[mi].Items[7] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[7]).Text = new string[] { med.Frequency };
                            //table.tbody[0].tr[mi].Items[8] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[8]).Text = new string[] { med.StartDate != null ? ((DateTime)med.StartDate).ToShortDateString() : "" };
                            //table.tbody[0].tr[mi].Items[9] = new StrucDocTd();
                            //((StrucDocTd)table.tbody[0].tr[mi].Items[9]).Text = new string[] { med.Deleted != null && med.Deleted.Equals("Y") ? "Inactive" : "Active" };
                        }

                        // add in the stuctured entries for the medications
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
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[meds.Count];
                        for (int mi = 0; mi < meds.Count; mi++)
                        {
                            PatientMedication med = meds.ElementAt<PatientMedication>(mi);
                            ////CodedMedication med = meds.ElementAt<CodedMedication>(mi);
                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi] = new POCD_MT000040Entry();
                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].typeCode = x_ActRelationshipEntry.DRIV;
                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item = new POCD_MT000040SubstanceAdministration();
                            //POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[mi].Item;
                            //sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                            //sadmin.templateId = new II[1];
                            //sadmin.templateId[0] = new II();
                            //sadmin.templateId[0].root = "2.16.840.1.113883.10.20.1.24";
                            //sadmin.id = new II[1];
                            //sadmin.id[0] = new II();
                            //sadmin.id[0].root = Guid.NewGuid().ToString();
                            //sadmin.statusCode = new CS();
                            //sadmin.statusCode.code = med.Deleted != null && med.Deleted.Equals("Y") ? "Inactive" : "Active";
                            //sadmin.effectiveTime = new PIVL_TS[1];
                            //sadmin.effectiveTime[0] = new PIVL_TS();
                            //sadmin.effectiveTime[0].value = med.StartDate != null ? ((DateTime)med.StartDate).ToString("yyyyMMdd") : "";
                            //sadmin.routeCode = new CE();
                            //sadmin.routeCode.code = med.Route;
                            //sadmin.routeCode.codeSystem = "2.16.840.1.113883.5.112";
                            //sadmin.routeCode.displayName = med.Route;
                            //sadmin.doseQuantity = new IVL_PQ();
                            //sadmin.doseQuantity.unit = med.DoseUnit;
                            //sadmin.doseQuantity.value = med.Dose;
                            ////administrationUnitCode ???
                            //sadmin.consumable = new POCD_MT000040Consumable();
                            //sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                            //sadmin.consumable.manufacturedProduct.templateId = new II[1];
                            //sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                            //sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.1.53";
                            //sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                            //POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                            //mat.name = new EN();
                            //mat.name.Text = new string[] { med.BrandName != null ? med.BrandName : med.GenericName };
                            //mat.code = new CE();
                            //mat.code.code = med.RxNormId;
                            //mat.code.codeSystem = "2.16.840.1.113883.6.88";
                            //mat.code.displayName = med.BrandName != null ? med.BrandName : med.GenericName;
                            //mat.code.originalText = new ED();
                            //mat.code.originalText.Text = new string[] { med.BrandName != null ? med.BrandName : med.GenericName };
                        }

                        componentIndex++;
                    }
                    #endregion

                    #region Problems - ok
                    //*****************************************************
                    // add in the problems section
                    //*****************************************************
                    //List<CodedProblem> probs = db.SelectNoLock<CodedProblem>("demographic_id=" + d.Id);

                    List<PatientProblem> probs = db.PatientProblems.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.CodeSystemId == 5).ToList(); // Only Pull SNOMED
                    if (probs.Count > 0)
                    {
                        // copy over any existing components and expand the component array by one
                        POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        //<component>
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        //<section classCode="DOCSECT" moodCode="EVN">
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                        POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        //<templateId root="2.16.840.1.113883.10.20.1.11" />
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.1.11";
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
                            C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
                            table.tbody[0].tr[pi] = new StrucDocTr();
                            table.tbody[0].tr[pi].Items = new object[4];
                            table.tbody[0].tr[pi].Items[0] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[pi].Items[0]).Text = new string[] { prob.CodeValue };
                            table.tbody[0].tr[pi].Items[1] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[pi].Items[1]).Text = new string[] { prob.Condition };
                            table.tbody[0].tr[pi].Items[2] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[pi].Items[2]).Text = new string[] { cResults.Value };
                            table.tbody[0].tr[pi].Items[3] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[pi].Items[3]).Text = new string[] { Convert.ToDateTime(prob.EffectiveDate).ToShortDateString() };
                        }

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
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[probs.Count];
                        for (int pi = 0; pi < probs.Count; pi++)
                        {
                            PatientProblem prob = probs.ElementAt<PatientProblem>(pi);
                            C_ConditionStatus cResults = db.C_ConditionStatus.FirstOrDefault(C => C.ConditionStatusId == prob.ConditionStatusId);
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
                            act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                            act.code = new CD();
                            act.code.nullFlavor = "NA";
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
                            ob.templateId[0].root = "2.16.840.1.113883.10.20.1.28";
                            ob.code = new CD();
                            ob.code.code = "ASSERTION";
                            ob.code.codeSystem = "2.16.840.1.113883.5.4";
                            ob.effectiveTime = new IVL_TS();
                            ob.effectiveTime.value = Convert.ToDateTime(prob.EffectiveDate).ToString("yyyyMMdd");
                            ob.value = new ANY[1];
                            ob.value[0] = new CD();
                            ((CD)ob.value[0]).displayName = prob.Condition;
                            ((CD)ob.value[0]).code = prob.CodeValue;
                            ((CD)ob.value[0]).codeSystem = "2.16.840.1.113883.6.96";
                        }

                        componentIndex++;
                    }
                    #endregion

                    #region Medication Allergies - ok
                    //*****************************************************
                    // add in the Allergies section 
                    //*****************************************************
                    List<PatientAllergy> alls = db.PatientAllergies.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId && p.CodeSystemId_Allergen == 6).ToList();  // Only pull medication Allergies
                    if (alls.Count > 0)
                    {
                        // copy over any existing components and expand the component array by one
                        POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        // <component>
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        //<section classCode="DOCSECT" moodCode="EVN">
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                        POCD_MT000040Section section = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        //<templateId root="2.16.840.1.113883.10.20.1.2" />
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.1.2";
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
                        /*  <table width="100%" border="1">
                              <thead>
                                <tr>
                                  <th>SNOMED Allergy Type Code</th>
                                  <th>RxNorm Code</th>
                                  <th>Medication/Agent Allergy</th>
                                  <th>Reaction</th>
                                  <th>Adverse Event Date</th>
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
                        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Adverse Event Date" };
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
                            ((StrucDocTd)table.tbody[0].tr[ai].Items[4]).Text = new string[] { all.EffectiveDate != null ? ( all.EffectiveDate ) : "" };
                        }

                        // add in the stuctured entries for the allergies
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
                        section.entry = new POCD_MT000040Entry[alls.Count];
                        for (int ai = 0; ai < alls.Count; ai++)
                        {
                            PatientAllergy all = alls.ElementAt<PatientAllergy>(ai);
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
                            act.templateId[0].root = "2.16.840.1.113883.10.20.1.27";
                            act.code = new CD();
                            act.code.nullFlavor = "NA";
                            act.entryRelationship = new POCD_MT000040EntryRelationship[1];
                            act.entryRelationship[0] = new POCD_MT000040EntryRelationship();
                            act.entryRelationship[0].typeCode = x_ActRelationshipEntryRelationship.SUBJ;
                            act.entryRelationship[0].Item = new POCD_MT000040Observation();
                            POCD_MT000040Observation ob = (POCD_MT000040Observation)act.entryRelationship[0].Item;
                            ob.statusCode = new CS();
                            ob.statusCode.code = "completed";
                            ob.classCode = "OBS";
                            ob.moodCode = x_ActMoodDocumentObservation.EVN;
                            ob.templateId = new II[1];
                            ob.templateId[0] = new II();
                            ob.templateId[0].root = "2.16.840.1.113883.10.20.1.18";
                            ob.code = new CD();
                            ob.code.code = "ASSERTION";
                            ob.code.codeSystem = "2.16.840.1.113883.5.4";
                            ob.id = new II[1];
                            ob.id[0] = new II();
                            ob.id[0].root = Guid.NewGuid().ToString();
                            ob.value = new ANY[1];
                            ob.value[0] = new CD();
                            ((CD)ob.value[0]).displayName = "Adverse reaction to substance";
                            ((CD)ob.value[0]).code = "282100009";// Adverse reaction to substance
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
                        }

                        componentIndex++;
                    }
                    #endregion

                    #region Immunizations
                    //*****************************************************
                    // add in the Immunizations section
                    //*****************************************************
                    // 998 - no vaccine administered
                    //List<Immunization> imms = db.SelectNoLock<Immunization>("demographic_id=" + d.Id);

                    List<PatientImmunization> imms = db.PatientImmunizations.Where(p => p.PatientId == PatientId && p.FacilityId == FacilityId && p.VisitId == VisitId).ToList();
                    if (imms.Count > 0)
                    {
                        // copy over any existing components and expand the component array by one
                        POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                        for (int ci = 0; ci < componentIndex; ci++)
                            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                        //<component>
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                        //<section classCode="DOCSECT" moodCode="EVN">
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                        POCD_MT000040Section section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                        //<templateId root="2.16.840.1.113883.10.20.1.6" />
                        section.templateId = new II[1];
                        section.templateId[0] = new II();
                        section.templateId[0].root = "2.16.840.1.113883.10.20.1.6";
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
                        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Date" };
                        table.thead.tr[0].Items[2] = new StrucDocTh();
                        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Status" };
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
                            table.tbody[0].tr[ii] = new StrucDocTr();
                            table.tbody[0].tr[ii].Items = new object[3];
                            table.tbody[0].tr[ii].Items[0] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[ii].Items[0]).Text = new string[] { imm.Vaccine };
                            table.tbody[0].tr[ii].Items[1] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[ii].Items[1]).Text = new string[] { imm.ImmunizationDate };
                            table.tbody[0].tr[ii].Items[2] = new StrucDocTd();
                            ((StrucDocTd)table.tbody[0].tr[ii].Items[2]).Text = new string[] { "Completed" };
                        }

                        // add in the stuctured entries for the immunizations
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
                        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[imms.Count];
                        for (int ii = 0; ii < imms.Count; ii++)
                        {
                            PatientImmunization imm = imms.ElementAt<PatientImmunization>(ii);
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii] = new POCD_MT000040Entry();
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].typeCode = x_ActRelationshipEntry.DRIV;
                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item = new POCD_MT000040SubstanceAdministration();
                            POCD_MT000040SubstanceAdministration sadmin = (POCD_MT000040SubstanceAdministration)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[ii].Item;
                            sadmin.moodCode = x_DocumentSubstanceMood.EVN;
                            sadmin.templateId = new II[1];
                            sadmin.templateId[0] = new II();
                            sadmin.templateId[0].root = "2.16.840.1.113883.10.20.1.24";
                            sadmin.id = new II[1];
                            sadmin.id[0] = new II();
                            sadmin.id[0].root = Guid.NewGuid().ToString();
                            sadmin.statusCode = new CS();
                            sadmin.statusCode.code = "completed";
                            sadmin.effectiveTime = new SXCM_TS[1];
                            sadmin.effectiveTime[0] = new SXCM_TS();
                            sadmin.effectiveTime[0].value = imm.ImmunizationDate;
                            sadmin.consumable = new POCD_MT000040Consumable();
                            sadmin.consumable.manufacturedProduct = new POCD_MT000040ManufacturedProduct();
                            sadmin.consumable.manufacturedProduct.templateId = new II[1];
                            sadmin.consumable.manufacturedProduct.templateId[0] = new II();
                            sadmin.consumable.manufacturedProduct.templateId[0].root = "2.16.840.1.113883.10.20.1.53";
                            sadmin.consumable.manufacturedProduct.Item = new POCD_MT000040Material();
                            POCD_MT000040Material mat = (POCD_MT000040Material)sadmin.consumable.manufacturedProduct.Item;
                            mat.code = new CE();
                            //mat.code.code = imm.NormalizedValue;                // The Code Value for the vaccine
                            mat.code.codeSystem = "2.16.840.1.113883.6.59";  // CVX - CDC Vaccine Codes
                            mat.code.displayName = imm.Vaccine;
                            mat.code.originalText = new ED();
                            mat.code.originalText.Text = new string[] { imm.Note };
                        }

                        componentIndex++;
                    }
                    #endregion

                    #region Observations
                    ////*****************************************************
                    //// add in the Results section
                    ////*****************************************************
                    //using (Database hl7db = new Database(hl7DataSource))
                    //{
                    //    // get the list of coding systems so we can correctly display the code types
                    //    List<CodingSystemType> codingSystems = db.SelectNoLock<CodingSystemType>("id is not null");

                    //    List<int> docIds = new List<int>();
                    //    using (SqlCommand command = new SqlCommand())
                    //    {
                    //        // Get the document ids for a patient from the exchange documents table for labcorp and quest
                    //        string sql = "select document_id from exchange_documents with (nolock) where exchange_system_id in (2,3) and demographic_id={0}";
                    //        command.CommandText = string.Format(sql, job.DemographicId);

                    //        using (DbDataReader reader = db.ExecuteQuery(command))
                    //        {
                    //            while (reader.Read())
                    //                docIds.Add(reader.GetInt32(0));
                    //        }
                    //    }

                    //    List<int> obrIds = new List<int>();
                    //    if (docIds.Count > 0)
                    //    {
                    //        using (SqlCommand command = new SqlCommand())
                    //        {
                    //            // For each document id get the PatientObservationRequestId
                    //            string sql = "select obr.PatientObservationRequestId " +
                    //                "from HL7.dbo.tblPatient_Observation_Request As obr with (nolock) " +
                    //                "    INNER JOIN HL7.dbo.tblPatient_Order As o  with (nolock) ON o.PatientOrderId = obr.PatientOrderId " +
                    //                "    INNER JOIN HL7.dbo.tblPatient_Visit As pv with (nolock) ON pv.PatientVisitId = o.PatientVisitId " +
                    //                "    INNER JOIN HL7.dbo.tblPatient As p with (nolock) ON p.PatientId = obr.PatientId " +
                    //                "where obr.PatientOrderId in ({0})";
                    //            string idList = "";
                    //            foreach (int id in docIds)
                    //                idList += id + ",";
                    //            idList = idList.Substring(0, idList.Length - 1);
                    //            // remove the trailing comma
                    //            command.CommandText = string.Format(sql, idList);

                    //            using (DbDataReader reader = hl7db.ExecuteQuery(command))
                    //            {
                    //                while (reader.Read())
                    //                    obrIds.Add(reader.GetInt32(0));
                    //            }
                    //        }
                    //    }

                    //    List<PatientObservation> obs = new List<PatientObservation>();
                    //    if (obrIds.Count > 0)
                    //    {
                    //        using (SqlCommand command = new SqlCommand())
                    //        {
                    //            // For each PatientObservationRequestId get the observation
                    //            string sql = "select IdentifierID,IdentifierText,IdentifierAltText,ObservationValue,UnitsID,AbnormalFlags,ObservationDate,PatientOrderId,SetID,ReferencesRange,IdentifierNameOfCodingSystem " +
                    //                "from HL7.dbo.tblPatient_Observation with (nolock) where PatientObservationRequestId in ({0}) order by PatientOrderId,SetID,ObservationDate desc";
                    //            string idList = "";
                    //            foreach (int id in obrIds)
                    //                idList += id + ",";
                    //            idList = idList.Substring(0, idList.Length - 1); // remove the trailing comma
                    //            command.CommandText = string.Format(sql, idList);

                    //            using (DbDataReader reader = hl7db.ExecuteQuery(command))
                    //            {
                    //                while (reader.Read())
                    //                {
                    //                    PatientObservation po = new PatientObservation();
                    //                    if (!reader.IsDBNull(0))
                    //                        po.IdentifierID = reader.GetString(0);
                    //                    if (!reader.IsDBNull(1))
                    //                        po.IdentifierText = reader.GetString(1);
                    //                    if (!reader.IsDBNull(2))
                    //                        po.IdentifierAltText = reader.GetString(2);
                    //                    if (!reader.IsDBNull(3))
                    //                        po.ObservationValue = reader.GetString(3);
                    //                    if (!reader.IsDBNull(4))
                    //                        po.UnitsID = reader.GetString(4);
                    //                    if (!reader.IsDBNull(5))
                    //                        po.AbnormalFlags = reader.GetString(5);
                    //                    if (!reader.IsDBNull(6))
                    //                        po.ObservationDate = reader.GetDateTime(6);
                    //                    if (!reader.IsDBNull(7))
                    //                        po.PatientOrderId = reader.GetInt32(7);
                    //                    if (!reader.IsDBNull(8))
                    //                        po.SetId = reader.GetInt32(8);
                    //                    if (!reader.IsDBNull(9))
                    //                        po.ReferencesRange = reader.GetString(9);
                    //                    if (!reader.IsDBNull(10))
                    //                        po.IdentifierNameOfCodingSystem = reader.GetString(10);
                    //                    obs.Add(po);
                    //                }
                    //            }
                    //        }
                    //    }

                    //    if (obs.Count > 0)
                    //    {
                    //        // copy over any existing components and expand the component array by one
                    //        POCD_MT000040Component3[] newComponentArray = new POCD_MT000040Component3[componentIndex + 1];
                    //        for (int ci = 0; ci < componentIndex; ci++)
                    //            newComponentArray[ci] = ((POCD_MT000040StructuredBody)ccd2.component.Item).component[ci];
                    //        ((POCD_MT000040StructuredBody)ccd2.component.Item).component = newComponentArray;

                    //        //<component>
                    //        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex] = new POCD_MT000040Component3();
                    //        //<section classCode="DOCSECT" moodCode="EVN">
                    //        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section = new POCD_MT000040Section();
                    //        POCD_MT000040Section section = (POCD_MT000040Section)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section;
                    //        //<templateId root="2.16.840.1.113883.10.20.1.14" />
                    //        section.templateId = new II[1];
                    //        section.templateId[0] = new II();
                    //        section.templateId[0].root = "2.16.840.1.113883.10.20.1.14";
                    //        //<code nullFlavor="NA" code="30954-2" codeSystem="2.16.840.1.113883.6.1" codeSystemName="LOINC" />
                    //        section.code = new CE();
                    //        section.code.nullFlavor = "NA";
                    //        section.code.code = "30954-2";
                    //        section.code.codeSystem = "2.16.840.1.113883.6.1";
                    //        section.code.codeSystemName = "LOINC";
                    //        //<title>Diagnostic Test Results</title>
                    //        section.title = new ST();
                    //        section.title.Text = new string[] { "Diagnostic Test Results" };
                    //        //<text mediaType="text/x-hl7-text+xml">
                    //        section.text = new StrucDocText();

                    //        // add in the HTML for the results
                    //        /*
                    //            <table width="100%" border="1">
                    //              <thead>
                    //                <tr>
                    //                  <th>Test</th>
                    //                  <th>Result</th>
                    //                  <th>Ref. Range</th>
                    //                  <th>Abnormal Flag</th>
                    //                  <th>Date Performed</th>
                    //                  <th>Set</th>
                    //                  <th>Order</th>
                    //                  <th>Code System</th>
                    //                  <th>Code</th>
                    //                </tr>
                    //              </thead>
                    //              <tbody>
                    //         */
                    //        section.text.Items = new object[1];
                    //        section.text.Items[0] = new StrucDocTable();
                    //        StrucDocTable table = (StrucDocTable)section.text.Items[0];
                    //        table.border = "1";
                    //        table.width = "100%";
                    //        table.thead = new StrucDocThead();
                    //        table.thead.tr = new StrucDocTr[1];
                    //        table.thead.tr[0] = new StrucDocTr();
                    //        table.thead.tr[0].Items = new object[9];
                    //        table.thead.tr[0].Items[0] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[0]).Text = new string[] { "Test" };
                    //        table.thead.tr[0].Items[1] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[1]).Text = new string[] { "Result" };
                    //        table.thead.tr[0].Items[2] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[2]).Text = new string[] { "Ref. Range" };
                    //        table.thead.tr[0].Items[3] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[3]).Text = new string[] { "Abnormal Flag" };
                    //        table.thead.tr[0].Items[4] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[4]).Text = new string[] { "Date Performed" };
                    //        table.thead.tr[0].Items[5] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[5]).Text = new string[] { "Set" };
                    //        table.thead.tr[0].Items[6] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[6]).Text = new string[] { "Order" };
                    //        table.thead.tr[0].Items[7] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[7]).Text = new string[] { "Code System" };
                    //        table.thead.tr[0].Items[8] = new StrucDocTh();
                    //        ((StrucDocTh)table.thead.tr[0].Items[8]).Text = new string[] { "Code" };
                    //        table.tbody = new StrucDocTbody[1];
                    //        table.tbody[0] = new StrucDocTbody();
                    //        table.tbody[0].tr = new StrucDocTr[obs.Count];
                    //        /*
                    //            <tr>
                    //              <td>WBC (Leukocytes)</td>
                    //              <td>&gt;30 /hpf</td>
                    //              <td>0 -  5</td>
                    //              <td>A</td>
                    //              <td>7/15/2011</td>
                    //              <td>1</td>
                    //              <td>492</td>
                    //              <td>Local</td>
                    //              <td>013128</td>
                    //            </tr>
                    //         */
                    //        for (int oi = 0; oi < obs.Count; oi++)
                    //        {
                    //            PatientObservation ob = obs.ElementAt<PatientObservation>(oi);
                    //            table.tbody[0].tr[oi] = new StrucDocTr();
                    //            table.tbody[0].tr[oi].Items = new object[9];
                    //            table.tbody[0].tr[oi].Items[0] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[0]).Text = new string[] { ob.IdentifierText + " (" + ob.IdentifierAltText + ")" };
                    //            table.tbody[0].tr[oi].Items[1] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[1]).Text = new string[] { ob.ObservationValue + " " + ob.UnitsID };
                    //            table.tbody[0].tr[oi].Items[2] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[2]).Text = new string[] { ob.ReferencesRange };
                    //            table.tbody[0].tr[oi].Items[3] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[3]).Text = new string[] { ob.AbnormalFlags + "" };
                    //            table.tbody[0].tr[oi].Items[4] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[4]).Text = new string[] { ob.ObservationDate != null ? ((DateTime)ob.ObservationDate).ToShortDateString() : "" };
                    //            table.tbody[0].tr[oi].Items[5] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[5]).Text = new string[] { ob.SetId + "" };
                    //            table.tbody[0].tr[oi].Items[6] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[6]).Text = new string[] { ob.PatientOrderId + "" };
                    //            table.tbody[0].tr[oi].Items[7] = new StrucDocTd();
                    //            string csystem = null;
                    //            foreach (CodingSystemType cst in codingSystems)
                    //            {
                    //                if (cst.Code.Equals(ob.IdentifierNameOfCodingSystem))
                    //                    csystem = cst.Description;
                    //            }
                    //            if (csystem == null)
                    //            {
                    //                // check for special cases
                    //                if (ob.IdentifierNameOfCodingSystem.StartsWith("99")) //99zzz
                    //                    csystem = "Local";
                    //                else if (ob.IdentifierNameOfCodingSystem.StartsWith("HL7")) //HL7nnnn
                    //                    csystem = "HL7 Defined Codes";
                    //                else if (ob.IdentifierNameOfCodingSystem.StartsWith("ISO")) //ISOnnnn
                    //                    csystem = "ISO Defined Codes";
                    //                else if (ob.IdentifierNameOfCodingSystem.StartsWith("NCPDP")) //NCPDPnnnnsss
                    //                    csystem = "NCPDP code list";
                    //                else if (ob.IdentifierNameOfCodingSystem.StartsWith("NCPDP")) //X12DEnnnn
                    //                    csystem = "ASC X12 Code List";
                    //                else
                    //                    csystem = "UNKNOWN";
                    //            }
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[7]).Text = new string[] { csystem };
                    //            table.tbody[0].tr[oi].Items[8] = new StrucDocTd();
                    //            ((StrucDocTd)table.tbody[0].tr[oi].Items[8]).Text = new string[] { ob.IdentifierID + "" };
                    //        }

                    //        /**
                    //         * COMMENTING OUT THIS BLOCK FOR THE STRUCTURED DATA OF LAB RESULTS FOR THE TIME BEING. IF WE END UP NEEDING TO PUT IT BACK IN
                    //         * REMEMBER THAT THIS CODE ISN'T COMPLETE.  THE OUTPUT OF THE STRUCTURED ELEMENTS IS NOT YET COMPLETELY CORRECT!
                    //         * 
                    //        // find out how many test batteries we have in the results assume a battery is indicated by the PatientOrderId/SetId pair
                    //        // so a change in SetId for a given PatientOrderId means we need a new battery entry and likewise if the PatientOrderId changes
                    //        int batteryCount = 0;
                    //        int lastPatientOrderId = -1;
                    //        int lastSetId = -1;
                    //        for (int oi = 0; oi < obs.Count; oi++)
                    //        {
                    //            PatientObservation po = obs.ElementAt<PatientObservation>(oi);
                    //            if (lastPatientOrderId != po.PatientOrderId || lastSetId != po.SetId)
                    //            {
                    //                batteryCount++;
                    //                lastPatientOrderId = po.PatientOrderId;
                    //                lastSetId = po.SetId;
                    //            }
                    //        }

                    //        // add in the stuctured entries for the results
                    //        ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry = new POCD_MT000040Entry[batteryCount];
                    //        int currentBatteryIndex = -1;
                    //        int currentResultIndex = -1;
                    //        lastPatientOrderId = -1;
                    //        lastSetId = -1;
                    //        int resultCount = 0;
                    //        // loop through the results, produce an entry for each battery of tests, assume a battery is indicated by the PatientOrderId/SetId pair
                    //        // so a change in SetId for a given PatientOrderId means we need a new battery entry and likewise if the PatientOrderId changes
                    //        for (int oi = 0; oi < obs.Count; oi++)
                    //        {
                    //            PatientObservation po = obs.ElementAt<PatientObservation>(oi);
                    //            if (lastPatientOrderId != po.PatientOrderId || lastSetId != po.SetId) // time for a new battery entry
                    //            {
                    //                currentBatteryIndex++;
                    //                currentResultIndex = -1;
                    //                lastPatientOrderId = po.PatientOrderId;
                    //                lastSetId = po.SetId;

                    //                // how many results for this battery
                    //                resultCount = 0;
                    //                for (int oi2 = 0; oi2 < obs.Count; oi2++)
                    //                {
                    //                    PatientObservation po2 = obs.ElementAt<PatientObservation>(oi2);
                    //                    if (lastPatientOrderId == po2.PatientOrderId && lastSetId == po2.SetId)
                    //                        resultCount++;
                    //                }

                    //                // go get the order for the results in this set
                    //                using (SqlCommand command = new SqlCommand())
                    //                {
                    //                    string sql = "select UniversalServiceIDText,UniversalServiceIDIdentifier,PatientOrderId,SetID,MessageDatetime " +
                    //                        "from HL7.dbo.tblPatient_Observation_Request with (nolock) " +
                    //                        "where PatientOrderId={0} and SetID={1} order by PatientOrderId,SetID";
                    //                    command.CommandText = string.Format(sql, po.PatientOrderId, po.SetId);

                    //                    using (DbDataReader reader = hl7db.ExecuteQuery(command))
                    //                    {
                    //                        if (reader.Read())
                    //                        {
                    //                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex] = new POCD_MT000040Entry();
                    //                            POCD_MT000040Entry entry = (POCD_MT000040Entry)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex];
                    //                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].typeCode = x_ActRelationshipEntry.DRIV;
                    //                            ((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].Item = new POCD_MT000040Organizer();
                    //                            ((POCD_MT000040Organizer)entry.Item).classCode = x_ActClassDocumentEntryOrganizer.BATTERY;
                    //                            ((POCD_MT000040Organizer)entry.Item).moodCode = "EVN";
                    //                            ((POCD_MT000040Organizer)entry.Item).templateId = new II[1];
                    //                            ((POCD_MT000040Organizer)entry.Item).templateId[0] = new II();
                    //                            ((POCD_MT000040Organizer)entry.Item).templateId[0].root = "2.16.840.1.113883.10.20.1.32";
                    //                            ((POCD_MT000040Organizer)entry.Item).id = new II[1];
                    //                            ((POCD_MT000040Organizer)entry.Item).id[0] = new II();
                    //                            ((POCD_MT000040Organizer)entry.Item).id[0].root = Guid.NewGuid().ToString();
                    //                            ((POCD_MT000040Organizer)entry.Item).code = new CD();
                    //                            ((POCD_MT000040Organizer)entry.Item).code.code = reader.GetString(1);
                    //                            ((POCD_MT000040Organizer)entry.Item).code.codeSystem = "2.16.840.1.113883.6.96";
                    //                            ((POCD_MT000040Organizer)entry.Item).code.displayName = reader.GetString(0);
                    //                            ((POCD_MT000040Organizer)entry.Item).statusCode = new CS();
                    //                            ((POCD_MT000040Organizer)entry.Item).statusCode.code = "completed";
                    //                            ((POCD_MT000040Organizer)entry.Item).effectiveTime = new IVL_TS();
                    //                            ((POCD_MT000040Organizer)entry.Item).effectiveTime.value = reader.GetDateTime(4).ToString("yyyyMMdd");
                    //                            ((POCD_MT000040Organizer)entry.Item).component = new POCD_MT000040Component4[resultCount];
                    //                        }
                    //                        else
                    //                        {
                                                
                    //                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex] = new POCD_MT000040Entry();
                    //                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].typeCode = x_ActRelationshipEntry.DRIV;
                    //                            //((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].Item = new POCD_MT000040Organizer();
                    //                            //((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].Item).component = new POCD_MT000040Component4[resultCount];
                    //                            // in the code above I was playing around with including the parts even if we couldn't find the order for them.
                    //                            // if you put this back in the the code below (the rest of this else statement) needs to be removed.

                    //                            // couldn't find the order for the results skip this set
                    //                            for (; oi < obs.Count; oi++)
                    //                            {
                    //                                PatientObservation poSkip = obs.ElementAt<PatientObservation>(oi);
                    //                                if (lastPatientOrderId != poSkip.PatientOrderId || lastSetId != poSkip.SetId)
                    //                                {
                    //                                    oi--;
                    //                                    break;
                    //                                }
                    //                            }
                    //                            currentBatteryIndex--;
                    //                            continue;
                    //                        }
                    //                    }
                    //                }
                    //            }

                    //            currentResultIndex++; // moving to the next result for this battery

                    //            ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].Item).component[currentResultIndex] = new POCD_MT000040Component4();
                    //            POCD_MT000040Component4 currentComponent = ((POCD_MT000040Organizer)((POCD_MT000040StructuredBody)ccd2.component.Item).component[componentIndex].section.entry[currentBatteryIndex].Item).component[currentResultIndex];
                    //            currentComponent.Item = new POCD_MT000040Observation();
                    //            POCD_MT000040Observation ob = (POCD_MT000040Observation)currentComponent.Item;
                    //            ob.classCode = "OBS";
                    //            ob.moodCode = x_ActMoodDocumentObservation.EVN;
                    //            ob.templateId = new II[1];
                    //            ob.templateId[0] = new II();
                    //            ob.templateId[0].root = "2.16.840.1.113883.10.20.1.31";
                    //            ob.id = new II[1];
                    //            ob.id[0] = new II();
                    //            ob.id[0].root = Guid.NewGuid().ToString();
                    //            ob.code = new CD();
                    //            ob.code.code = po.IdentifierID;
                    //            ob.code.codeSystem = "2.16.840.1.113883.6.1";
                    //            ob.code.codeSystemName = "LOINC";
                    //            ob.code.displayName = po.IdentifierText;
                    //            ob.statusCode = new CS();
                    //            ob.statusCode.code = "completed";
                    //            ob.effectiveTime = new IVL_TS();
                    //            ob.effectiveTime.value = ((DateTime)po.ObservationDate).ToString("yyyyMMdd");
                    //            ob.value = new ANY[1];
                    //            ob.value[0] = new PQ();
                    //            ((PQ)ob.value[0]).value = po.ObservationValue;
                    //            ((PQ)ob.value[0]).unit = po.UnitsID;
                    //            // this is the place where we would say the test result is Normal, Low, Susceptible, etc
                    //            //ob.interpretationCode = new CE[1];
                    //            //ob.interpretationCode[0] = new CE();
                    //            //ob.interpretationCode[0].code = "N";
                    //            //ob.interpretationCode[0].codeSystem = "2.16.840.1.113883.5.83";
                    //            if (po.ReferencesRange != null)
                    //            {
                    //                ob.referenceRange = new POCD_MT000040ReferenceRange[1];
                    //                ob.referenceRange[0] = new POCD_MT000040ReferenceRange();
                    //                ob.referenceRange[0].observationRange = new POCD_MT000040ObservationRange();
                    //                ob.referenceRange[0].observationRange.text = new ED();
                    //                ob.referenceRange[0].observationRange.text.Text = new string[] { po.ReferencesRange };
                    //                // using the plain text for the reference range above instead of the next two lines to avoid parsing the values
                    //                //ob.referenceRange[0].observationRange.value = new IVL_PQ();
                    //                //((IVL_PQ)ob.referenceRange[0].observationRange.value).value = po.ReferencesRange;
                    //            }
                    //        }*/
                    //    }
                    //}

                    //try
                    //{
                    //    // write out the ccd document
                    //    Serialize<POCD_MT000040ClinicalDocument>(ccd2, "testccd.xml");
                    //}
                    //catch (Exception ex)
                    //{
                    //    return false;
                    //}
                    #endregion
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }




    }
}
