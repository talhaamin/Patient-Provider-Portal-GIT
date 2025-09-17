using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Testing
{
    public partial class frmISVisitUpload : Form
    {
        public frmISVisitUpload()
        {
            InitializeComponent();
        }

        private void frmISVisitUpload_Load(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();

            ISPatientService.VisitPostData VisitData = new ISPatientService.VisitPostData();
            ISPatientService.VisitPostResponse ResponseData = new ISPatientService.VisitPostResponse();
            try
            {
                // Load Basic Visit Info
                VisitData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                VisitData.VisitId = Convert.ToInt64(nudVisitId.Value);
                VisitData.ProviderId = Convert.ToInt64(nudProviderId.Value);
                VisitData.VisitReason = txtReason.Text;
                VisitData.VisitDate = Convert.ToDateTime(dtpDate.Value.ToShortDateString());

                // Create Data Table For Each Data Type

                DataTable dtInsurance = new DataTable();

                dtInsurance.Columns.Add("PlanName", typeof(string));
                dtInsurance.Columns.Add("MemberNumber", typeof(string));
                dtInsurance.Columns.Add("GroupNumber", typeof(string));
                dtInsurance.Columns.Add("Subscriber", typeof(string));
                dtInsurance.Columns.Add("Relationship", typeof(string));
                dtInsurance.Columns.Add("EffectiveDate", typeof(string));

                dtInsurance.TableName = "Insurance";

                dtInsurance.Rows.Add("ABC Insurnace Plan", "12323-ABC", "GRP123-212", "John Smith", "Father", "1/1/2013");

                VisitData.Insurance = dtInsurance;

                DataTable dtProblem = new DataTable();

                dtProblem.Columns.Add("CodeValue", typeof(string));
                dtProblem.Columns.Add("CodeSystemId", typeof(int));
                dtProblem.Columns.Add("Condition", typeof(string));
                dtProblem.Columns.Add("EffectiveDate", typeof(string));
                dtProblem.Columns.Add("ConditionStatusId", typeof(Int16));
                dtProblem.Columns.Add("Note", typeof(string));

                dtProblem.TableName = "ProblemList";

                dtProblem.Rows.Add("10380004", 5, "Crushing injury of finger", "201211", 1, "Note on Problem 1");
                dtProblem.Rows.Add("10405006", 5, "Encephalomyelocele", "201308", 1, "Note on Problem 2");

                VisitData.ProblemList = dtProblem;

                StringWriter sw = new StringWriter();
                dtProblem.WriteXml(sw);
                string result = sw.ToString();

                DataTable dtMedication = new DataTable();

                dtMedication.Columns.Add("CodeValue", typeof(string));
                dtMedication.Columns.Add("CodeSystemId", typeof(int));
                dtMedication.Columns.Add("MedicationName", typeof(string));
                dtMedication.Columns.Add("Active", typeof(Boolean));
                dtMedication.Columns.Add("Quantity", typeof(int));
                dtMedication.Columns.Add("Days", typeof(int));
                dtMedication.Columns.Add("Route", typeof(string));
                dtMedication.Columns.Add("Refills", typeof(int));
                dtMedication.Columns.Add("Frequency", typeof(string));
                dtMedication.Columns.Add("Sig", typeof(string));
                dtMedication.Columns.Add("Diagnosis", typeof(string));
                dtMedication.Columns.Add("StartDate", typeof(DateTime));
                dtMedication.Columns.Add("ExpireDate", typeof(DateTime));
                dtMedication.Columns.Add("Pharmacy", typeof(string));
                dtMedication.Columns.Add("Status", typeof(string));
                dtMedication.Columns.Add("Note", typeof(string));
                dtMedication.Columns.Add("DuringVisit", typeof(Boolean));

                dtMedication.TableName = "Medications";

                dtMedication.Rows.Add("123-22-3221-123412412341241243321",6, "atenolol",1, 60, 30, "Sub", 0, "BID", "", "", "10/1/2013", "12/31/2013", "Publix","Active", "Take At Mealtime",0);
                dtMedication.Rows.Add("123-2233-21", 6, "captopril",1, 60, 30, "Sub", 0, "BID", "", "", "10/1/2013", "12/31/2013", "Publix", "Active", "Take At Mealtime",0);

                VisitData.Medication = dtMedication;

                DataTable dtAllergy = new DataTable();

                dtAllergy.Columns.Add("CodeValue_Allergen", typeof(string));
                dtAllergy.Columns.Add("CodeSystemId_Allergen", typeof(int));
                dtAllergy.Columns.Add("Allergen", typeof(string));
                dtAllergy.Columns.Add("AllergenType", typeof(string));
                dtAllergy.Columns.Add("CodeValue_Reaction", typeof(string));
                dtAllergy.Columns.Add("CodeSystemId_Reaction", typeof(int));
                dtAllergy.Columns.Add("Reaction", typeof(string));
                dtAllergy.Columns.Add("EffectiveDate", typeof(string));
                dtAllergy.Columns.Add("ConditionStatusId", typeof(Int16));
                dtAllergy.Columns.Add("Note", typeof(string));

                dtAllergy.TableName = "Allergy";

                dtAllergy.Rows.Add("70618", 6, "Penicillin", "Drug", "247472004", 5, "Hives", "2008", 1, "Patient breaks out in hives.");
                dtAllergy.Rows.Add("1191", 6, "Aspirin", "Drug", "56018004", 5, "Wheezing", "2008", 1, "Avoid these");

                VisitData.Allergy = dtAllergy;

                DataTable dtVital = new DataTable();

                dtVital.Columns.Add("VitalDate", typeof(DateTime));
                dtVital.Columns.Add("HeightIn", typeof(int));
                dtVital.Columns.Add("WeightLb", typeof(int));
                dtVital.Columns.Add("BloodPressurePosn", typeof(string));
                dtVital.Columns.Add("Systolic", typeof(int));
                dtVital.Columns.Add("Diastolic", typeof(int));
                dtVital.Columns.Add("Pulse", typeof(int));
                dtVital.Columns.Add("Respiration", typeof(int));
                dtVital.Columns.Add("Temperature", typeof(decimal));

                dtVital.TableName = "Vitals";

                dtVital.Rows.Add("11/1/2013", 55, 170, "Left Arm", 120, 80, 80, 50, 98.5);
                //
                dtVital.Rows.Add("11/1/23", 55, 170, "Left Arm", 120, 80, 80, 50, 98.5);

                VisitData.VitalSign = dtVital;


                DataTable dtFamily = new DataTable();

                dtFamily.Columns.Add("RelationshipId", typeof(int));
                dtFamily.Columns.Add("Description", typeof(string));
                dtFamily.Columns.Add("Qualifier", typeof(string));
                dtFamily.Columns.Add("CodeValue", typeof(string));
                dtFamily.Columns.Add("CodeSystemId", typeof(int));
                dtFamily.Columns.Add("ConditionStatusId", typeof(Int16));
                dtFamily.Columns.Add("AgeAtOnset", typeof(Int32));
                dtFamily.Columns.Add("Diseased", typeof(Boolean));
                dtFamily.Columns.Add("DiseasedAge", typeof(Int32));
                dtFamily.Columns.Add("Note", typeof(string));

                dtFamily.TableName = "Family";

                dtFamily.Rows.Add(5, "Depression", "", "192080009", 4, 1, 34, 0, 0, "Father is depressed");
                dtFamily.Rows.Add(4, "Hypertension", "", "401.9", 1, 1, 40, 0, 0, "Mother has issues");

                VisitData.FamilyHistory = dtFamily;

                DataTable dtSocial = new DataTable();

                dtSocial.Columns.Add("Description", typeof(string));
                dtSocial.Columns.Add("Qualifier", typeof(string));
                dtSocial.Columns.Add("CodeValue", typeof(string));
                dtSocial.Columns.Add("CodeSystemId", typeof(int));
                dtSocial.Columns.Add("BeginDate", typeof(string));
                dtSocial.Columns.Add("EndDate", typeof(string));
                dtSocial.Columns.Add("Note", typeof(string));

                dtSocial.TableName = "Social";

                dtSocial.Rows.Add("Smoking", "", "", 0, "200812", "", "Has a hard time trying to quit.");

                VisitData.SocialHistory = dtSocial;

                DataTable dtMedical = new DataTable();

                dtMedical.Columns.Add("Description", typeof(string));
                dtMedical.Columns.Add("DateOfOccurance", typeof(string));
                dtMedical.Columns.Add("Note", typeof(string));

                dtMedical.TableName = "Medical";

                dtMedical.Rows.Add("Microscopic Surgery On Right Index Finger", "201211", "Still has occasional soreness in finger.");

                VisitData.MedicalHistory = dtMedical;

                DataTable dtProcedure = new DataTable();

                dtProcedure.Columns.Add("Description", typeof(string));
                dtProcedure.Columns.Add("CodeValue", typeof(string));
                dtProcedure.Columns.Add("CodeSystemId", typeof(int));
                dtProcedure.Columns.Add("Diagnosis", typeof(string));
                dtProcedure.Columns.Add("PerformedBy", typeof(string));
                dtProcedure.Columns.Add("ServiceLocation", typeof(string));
                dtProcedure.Columns.Add("ServiceDate", typeof(string));
                dtProcedure.Columns.Add("Note", typeof(string));

                dtProcedure.TableName = "Procedure";

                dtProcedure.Rows.Add("Preperation", "", 0, "Successful", "Dr Smith", "Eye institute", "20051114", "Good.");

                VisitData.Procedure = dtProcedure;

                DataTable dtImmunization = new DataTable();

                dtImmunization.Columns.Add("ImmunizationDate", typeof(string));
                dtImmunization.Columns.Add("CodeValue", typeof(string));
                dtImmunization.Columns.Add("CodeSystemId", typeof(int));
                dtImmunization.Columns.Add("Vaccine", typeof(string));
                dtImmunization.Columns.Add("Amount", typeof(string));
                dtImmunization.Columns.Add("Route", typeof(string));
                dtImmunization.Columns.Add("Site", typeof(string));
                dtImmunization.Columns.Add("Sequence", typeof(string));
                dtImmunization.Columns.Add("ExpirationDate", typeof(DateTime));
                dtImmunization.Columns.Add("LotNumber", typeof(string));
                dtImmunization.Columns.Add("Manufacturer", typeof(string));
                dtImmunization.Columns.Add("Note", typeof(string));

                dtImmunization.TableName = "Immunization";

                dtImmunization.Rows.Add("20121101", 144, 7, "influenza, seasonal, intradermal, preservative free", "10mg", "Injection", "Left Arm", "1", "1/1/1900", "", "", "Flue Shot");

                VisitData.Immunization = dtImmunization;

                DataTable dtPlanOfCare = new DataTable();

                dtPlanOfCare.Columns.Add("InstructionTypeId", typeof(int));
                dtPlanOfCare.Columns.Add("CodeValue", typeof(string));
                dtPlanOfCare.Columns.Add("CodeSystemId", typeof(int));
                dtPlanOfCare.Columns.Add("Instruction", typeof(string));
                dtPlanOfCare.Columns.Add("Goal", typeof(string));
                dtPlanOfCare.Columns.Add("Note", typeof(string));
                dtPlanOfCare.Columns.Add("AppointmentDateTime", typeof(DateTime));
                dtPlanOfCare.Columns.Add("ProviderId", typeof(Int64));

                dtPlanOfCare.TableName = "PlanOfCare";

                dtPlanOfCare.Rows.Add(1, "", 0, "Come back for follow up visit in 2 weeks", "better health", "Note 1", "12/2/2013 10:00AM", 5);
                dtPlanOfCare.Rows.Add(7, "", 0, "Go to website www.health.com and read article", "better health", "Note 2", "1/1/1900", 5);

                VisitData.PlanOfCare = dtPlanOfCare;


                ResponseData = PatinetWS.VisitPost(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, VisitData);

                if (ResponseData.Valid)
                    MessageBox.Show("Visit Saved");
                else
                    MessageBox.Show(ResponseData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ISPatientService.PatientWS PatinetWS = new ISPatientService.PatientWS();

            ISPatientService.VisitPostDataXML VisitData = new ISPatientService.VisitPostDataXML();
            ISPatientService.VisitPostResponse ResponseData = new ISPatientService.VisitPostResponse();
            try
            {
                // Load Basic Visit Info
                VisitData.AMRPatientId = Convert.ToInt64(nudPatientId.Value);
                VisitData.VisitId = Convert.ToInt64(nudVisitId.Value);
                VisitData.ProviderId = Convert.ToInt64(nudProviderId.Value);
                VisitData.VisitReason = txtReason.Text;
                VisitData.VisitDate = Convert.ToDateTime(dtpDate.Value.ToShortDateString());

                // Create XML Data For Each Data Type

                string insurance =  "<Insurances>" +
                                    " <InsuranceData>" +
                                    "  <PlanName>ABC Insurnace Plan</PlanName>" +
                                    "  <MemberNumber>12323-ABC</MemberNumber>" +
                                    "  <GroupNumber>GRP123-212</GroupNumber>" +
                                    "  <Subscriber>John Smith</Subscriber>" +
                                    "  <Relationship>Father</Relationship>" +
                                    "  <EffectiveDate>1/1/2013</EffectiveDate>" +
                                    " </InsuranceData>" +
                                    "</Insurances>";

                VisitData.Insurance = insurance;

                string problem =    "<Problems>" +
                                    " <ProblemData>" +
                                    "  <CodeValue>10380004</CodeValue>" +
                                    "  <CodeSystemId>5</CodeSystemId>" +
                                    "  <Condition>Crushing injury of finger</Condition>" +
                                    "  <EffectiveDate>201211</EffectiveDate>" +
                                    "  <ConditionStatusId>2</ConditionStatusId>" +
                                    "  <Note>Note on Problem 1</Note>" +
                                    " </ProblemData>" +
                                    " <ProblemData>" +
                                    "  <CodeValue>10405006</CodeValue>" +
                                    "  <CodeSystemId>5</CodeSystemId>" +
                                    "  <Condition>Encephalomyelocele</Condition>" +
                                    "  <EffectiveDate>19000101</EffectiveDate>" +
                                    "  <ConditionStatusId>1</ConditionStatusId>" +
                                    "  <Note>Note on Problem 2</Note>" +
                                    " </ProblemData>" +
                                    "</Problems>";

                VisitData.ProblemList = problem;

                //string medication = "<Medications>" +
                //                    " <MedicationData>" +
                //                    "  <CodeValue>123-22-3221-123</CodeValue>" +
                //                    "  <CodeSystemId>6</CodeSystemId>" +
                //                    "  <MedicationName>atenolol</MedicationName>" +
                //                    "  <Active>1</Active>" +
                //                    "  <Quantity>60</Quantity>" +
                //                    "  <Days>30</Days>" +
                //                    "  <Route>Sub</Route>" +
                //                    "  <Refills>0</Refills>" +
                //                    "  <Frequency>BID</Frequency>" +
                //                    "  <Sig></Sig>" +
                //                    "  <Diagnosis></Diagnosis>" +
                //                    "  <StartDate>10/1/2013</StartDate>" +
                //                    "  <ExpireDate>12/31/2013</ExpireDate>" +
                //                    "  <Pharmacy>Publix</Pharmacy>" +
                //                    "  <Status>Active</Status>" +
                //                    "  <Note>Take At Mealtime</Note>" +
                //                    "  <DuringVisit>0</DuringVisit>" +
                //                    " </MedicationData>" +
                //                    " <MedicationData>" +
                //                    "  <CodeValue>123-2233-21</CodeValue>" +
                //                    "  <CodeSystemId>6</CodeSystemId>" +
                //                    "  <MedicationName>captopril</MedicationName>" +
                //                    "  <Active>1</Active>" +
                //                    "  <Quantity>60</Quantity>" +
                //                    "  <Days>30</Days>" +
                //                    "  <Route>Sub</Route>" +
                //                    "  <Refills>0</Refills>" +
                //                    "  <Frequency>BID</Frequency>" +
                //                    "  <Sig></Sig>" +
                //                    "  <Diagnosis></Diagnosis>" +
                //                    "  <StartDate>10/1/2013</StartDate>" +
                //                    "  <ExpireDate>12/31/2013</ExpireDate>" +
                //                    "  <Pharmacy>Publix</Pharmacy>" +
                //                    "  <Status>Active</Status>" +
                //                    "  <Note>Take At Mealtime</Note>" +
                //                    "  <DuringVisit>0</DuringVisit>" +
                //                    " </MedicationData>" +
                //                    "</Medications>";

                //VisitData.Medication = medication;

                //string allergy =    "<Allergies>" +
                //                    " <AllergyData>" +
                //                    "  <CodeValue_Allergen>70618</CodeValue_Allergen>" +
                //                    "  <CodeSystemId_Allergen>6</CodeSystemId_Allergen>" +
                //                    "  <Allergen>Penicillin</Allergen>" +
                //                    "  <AllergenType>Drug</AllergenType>" +
                //                    "  <CodeValue_Reaction>247472004</CodeValue_Reaction>" +
                //                    "  <CodeSystemId_Reaction>5</CodeSystemId_Reaction>" +
                //                    "  <Reaction>Hives</Reaction>" +
                //                    "  <EffectiveDate>2008</EffectiveDate>" +
                //                    "  <ConditionStatusId>1</ConditionStatusId>" +
                //                    "  <Note>Patient breaks out in hives.</Note>" +
                //                    " </AllergyData>" +
                //                    " <AllergyData>" +
                //                    "  <CodeValue_Allergen>string</CodeValue_Allergen>" +
                //                    "  <CodeSystemId_Allergen>6</CodeSystemId_Allergen>" +
                //                    "  <Allergen>Aspirin</Allergen>" +
                //                    "  <AllergenType>Drug</AllergenType>" +
                //                    "  <CodeValue_Reaction>string</CodeValue_Reaction>" +
                //                    "  <CodeSystemId_Reaction>5</CodeSystemId_Reaction>" +
                //                    "  <Reaction>Wheezing</Reaction>" +
                //                    "  <EffectiveDate>2008</EffectiveDate>" +
                //                    "  <ConditionStatusId>1</ConditionStatusId>" +
                //                    "  <Note>Avoid these</Note>" +
                //                    " </AllergyData>" +
                //                    "</Allergies>";

                //VisitData.Allergy = allergy;

                //string vital =  "<VitalSigns>" +
                //                " <VitalSignData>" +
                //                "  <VitalDate>11/1/2013</VitalDate>" +
                //                "  <HeightIn>55</HeightIn>" +
                //                "  <WeightLb>170</WeightLb>" +
                //                "  <BloodPressurePosn>Left Arm</BloodPressurePosn>" +
                //                "  <Systolic>120</Systolic>" +
                //                "  <Diastolic>80</Diastolic>" +
                //                "  <Pulse>80</Pulse>" +
                //                "  <Respiration>50</Respiration>" +
                //                "  <Temperature>98.5</Temperature>" +
                //                " </VitalSignData>" +
                //                "</VitalSigns>";

                //VisitData.VitalSign = vital;

                //string family = "<FamilyHistories>" +
                //                " <FamilyHistoryData>" +
                //                "  <RelationshipId>5</RelationshipId>" +
                //                "  <Description>Depression</Description>" +
                //                "  <Qualifier></Qualifier>" +
                //                "  <CodeValue>192080009</CodeValue>" +
                //                "  <CodeSystemId>4</CodeSystemId>" +
                //                "  <ConditionStatusId>1</ConditionStatusId>" +
                //                "  <AgeAtOnset>34</AgeAtOnset>" +
                //                "  <Diseased>0</Diseased>" +
                //                "  <DiseasedAge>0</DiseasedAge>" +
                //                "  <Note>Father is depressed</Note>" +
                //                " </FamilyHistoryData>" +
                //                " <FamilyHistoryData>" +
                //                "  <RelationshipId>4</RelationshipId>" +
                //                "  <Description>Hypertension</Description>" +
                //                "  <Qualifier></Qualifier>" +
                //                "  <CodeValue>401.9</CodeValue>" +
                //                "  <CodeSystemId>1</CodeSystemId>" +
                //                "  <ConditionStatusId>1</ConditionStatusId>" +
                //                "  <AgeAtOnset>40</AgeAtOnset>" +
                //                "  <Diseased>0</Diseased>" +
                //                "  <DiseasedAge>0</DiseasedAge>" +
                //                "  <Note>Mother has issues</Note>" +
                //                " </FamilyHistoryData>" +
                //                "</FamilyHistories>";

                //VisitData.FamilyHistory = family;

                //string social = "<SocialHistories>" +
                //                " <SocialHistoryData>" +
                //                "  <Description>Smoking</Description>" +
                //                "  <Qualifier>Daily</Qualifier>" +
                //                "  <CodeValue></CodeValue>" +
                //                "  <CodeSystemId>0</CodeSystemId>" +
                //                "  <BeginDate>200812</BeginDate>" +
                //                "  <EndDate></EndDate>" +
                //                "  <Note>Has a hard time trying to quit.</Note>" +
                //                " </SocialHistoryData>" +
                //                "</SocialHistories>";

                //VisitData.SocialHistory = social;

                //string medical =    "<MedicalHistories>" +
                //                    " <MedicalHistoryData>" +
                //                    "  <Description>Microscopic Surgery On Right Index Finger</Description>" +
                //                    "  <DateOfOccurance>201211</DateOfOccurance>" +
                //                    "  <Note>Still has occasional soreness in finger.</Note>" +
                //                    " </MedicalHistoryData>" +
                //                    "</MedicalHistories>";

                //VisitData.MedicalHistory = medical;

                string procedure =  "<Procedures>" +
                                    " <ProcedureData>" +
                                    "  <Description>Preperation</Description>" +
                                    "  <CodeValue></CodeValue>" +
                                    "  <CodeSystemId>0</CodeSystemId>" +
                                    "  <Diagnosis>Successful</Diagnosis>" +
                                    "  <PerformedBy>Dr Smith</PerformedBy>" +
                                    "  <ServiceLocation>Eye institute</ServiceLocation>" +
                                    "  <ServiceDate>20051114</ServiceDate>" +
                                    "  <Note>Good</Note>" +
                                    " </ProcedureData>" +
                                    "</Procedures>";

                VisitData.Procedure = procedure;

                //string immunization = "<Immunizations>" +
                //    " <ImmunizationData>" +
                //    "  <ImmunizationDate>20121101</ImmunizationDate>" +
                //    "  <CodeValue>144</CodeValue>" +
                //    "  <CodeSystemId>7</CodeSystemId>" +
                //    "  <Vaccine>influenza, seasonal, intradermal, preservative free</Vaccine>" +
                //    "  <Amount>10mg</Amount>" +
                //    "  <Route>Injection</Route>" +
                //    "  <Site>Left Arm</Site>" +
                //    "  <Sequence>1</Sequence>" +
                //    "  <ExpirationDate>1/1/1900</ExpirationDate>" +
                //    "  <LotNumber></LotNumber>" +
                //    "  <Manufacturer></Manufacturer>" +
                //    "  <Note>Flue Shot</Note>" +
                //    " </ImmunizationData>" +
                //    "</Immunizations>";

                //VisitData.Immunization = immunization;

                //string plan =   "<PlanOfCares>" +
                //                " <PlanOfCareData> " +
                //                "  <InstructionTypeId>1</InstructionTypeId>" +
                //                "  <CodeValue></CodeValue>" +
                //                "  <CodeSystemId>0</CodeSystemId>" +
                //                "  <Instruction>Come back for follow up visit in 2 weeks</Instruction>" +
                //                "  <Goal>better health</Goal>" +
                //                "  <Note>Note 1</Note>" +
                //                " <AppointmentDateTime>12/2/2013 10:00AM</AppointmentDateTime>" +
                //                "  <ProviderId>5</ProviderId>" +
                //                " </PlanOfCareData>" +
                //                " <PlanOfCareData> " +
                //                "  <InstructionTypeId>7</InstructionTypeId>" +
                //                "  <CodeValue></CodeValue>" +
                //                "  <CodeSystemId>0</CodeSystemId>" +
                //                "  <Instruction>Go to website www.health.com and read article</Instruction>" +
                //                "  <Goal>better health</Goal>" +
                //                "  <Note>Note 2</Note>" +
                //                " <AppointmentDateTime>1/1/1900</AppointmentDateTime>" +
                //                "  <ProviderId>5</ProviderId>" +
                //                " </PlanOfCareData>" +
                //                "</PlanOfCares>";

                //VisitData.PlanOfCare = plan;


                ResponseData = PatinetWS.VisitPostXML(GlobalVar.FacilityId, GlobalVar.UserId, GlobalVar.Token, VisitData);

                if (ResponseData.Valid)
                    MessageBox.Show("Visit Saved");
                else
                    MessageBox.Show(ResponseData.ErrorMessage);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving");
            }
        }
    }
}
