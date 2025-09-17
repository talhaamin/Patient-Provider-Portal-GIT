using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testing
{
    public partial class frmPatientData : Form
    {
        public frmPatientData()
        {
            InitializeComponent();
        }

        private void cmdRead_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            PatientDocumentService.PatientDocParms DocParms = new PatientDocumentService.PatientDocParms();
            PatientDocumentService.PatientDocTableData DocTableData = new PatientDocumentService.PatientDocTableData();

            DocParms.Option = 1;
            DocParms.Active = 2;   // 1-Active, 0-Inactive, 2-Both
            DocParms.PatientId = Convert.ToInt64(nudPatientId.Value);
            DocParms.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
            DocParms.VisitId = Convert.ToInt64(nudVisitId.Value);

            switch (cboTable.Text)
            {
                case "PatientAllergy":
                    DocTableData = PatinetDocumentWS.GetPatientAllergyData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientFamilyHist":
                    DocTableData = PatinetDocumentWS.GetPatientFamilyHistData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientMedicalHist":
                    DocTableData = PatinetDocumentWS.GetPatientMedicalHistData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientMedication":
                    DocTableData = PatinetDocumentWS.GetPatientMedicationData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientProblem":
                    DocTableData = PatinetDocumentWS.GetPatientProblemData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientProcedure":
                    DocTableData = PatinetDocumentWS.GetPatientProcedureData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientSocialHist":
                    DocTableData = PatinetDocumentWS.GetPatientSocialHistData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientSurgicalHist":
                    DocTableData = PatinetDocumentWS.GetPatientSurgicalHistData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientVitalSign":
                    DocTableData = PatinetDocumentWS.GetPatientVitalSignData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientImmunization":
                    DocTableData = PatinetDocumentWS.GetPatientImmunizationData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientInsurance":
                    DocTableData = PatinetDocumentWS.GetPatientInsuranceData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PlanOfCare":
                    DocTableData = PatinetDocumentWS.GetPatientPlanOfCareData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                case "PatientPharmacy":
                    DocTableData = PatinetDocumentWS.GetPatientPharmacyData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    break;
                    
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = DocTableData.dt;
            dgCodes.DataSource = bs;
            txtCntr.Text = "0";
            txtDescription.Text = "";
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();
            string DateVal = nudYear.Value.ToString();
            if (nudMonth.Value > 0)
            {
                DateVal = DateVal + nudMonth.Value.ToString().PadLeft(2, '0');
                if (nudDay.Value > 0)
                {
                    DateVal = DateVal + nudDay.Value.ToString().PadLeft(2, '0');
                }

            }

            switch (cboTable.Text)
            {
                case "PatientAllergy":
                    PatientDocumentService.PatientAllergyData AllergyData = new PatientDocumentService.PatientAllergyData();
                    try
                    {
                        AllergyData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        AllergyData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        AllergyData.PatientAllergyCntr = Convert.ToInt64(txtCntr.Text);
                        AllergyData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        AllergyData.CodeValue_Allergen = "";
                        AllergyData.CodeSystem_Allergen = 0;
                        AllergyData.Allergen = txtDescription.Text;
                        AllergyData.AllergenType = "Food";
                        AllergyData.CodeValue_Reaction = "";
                        AllergyData.CodeSystem_Reaction = 0;
                        AllergyData.Reaction = "Rash";
                        AllergyData.EffectiveDate = DateVal;
                        AllergyData.ConditionStatusId = 1;
                        PatinetDocumentWS.SavePatientAllergyData(AllergyData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientFamilyHist":
                    PatientDocumentService.PatientFamilyHistData FamilyHistData = new PatientDocumentService.PatientFamilyHistData();
                    try
                    {
                        FamilyHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        FamilyHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        FamilyHistData.PatFamilyHistCntr = Convert.ToInt64(txtCntr.Text);
                        FamilyHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        FamilyHistData.RelationshipId = 4;
                        FamilyHistData.FamilyMember = "Mother";
                        FamilyHistData.Description = txtDescription.Text;
                        FamilyHistData.Qualifier = "";
                        FamilyHistData.CodeValue = "";
                        FamilyHistData.CodeSystemId = 1;
                        FamilyHistData.ConditionStatusId = 1;
                        FamilyHistData.AgeAtOnset = 20;
                        FamilyHistData.Diseased = false;
                        FamilyHistData.DiseasedAge = 0;
                        FamilyHistData.Note = "";
                        PatinetDocumentWS.SavePatientFamilyHistData(FamilyHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientMedicalHist":
                    PatientDocumentService.PatientMedicalHistData MedicalHistData = new PatientDocumentService.PatientMedicalHistData();
                    try
                    {
                        MedicalHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        MedicalHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        MedicalHistData.PatMedicalHistCntr = Convert.ToInt64(txtCntr.Text);
                        MedicalHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        MedicalHistData.Description = txtDescription.Text;
                        MedicalHistData.DateOfOccurance = DateVal;
                        MedicalHistData.Note = "";
                        MedicalHistData.OnCard = false;
                        MedicalHistData.OnKeys = false;
                        PatinetDocumentWS.SavePatientMedicalHistData(MedicalHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientMedication":
                    PatientDocumentService.PatientMedicationData MedicationData = new PatientDocumentService.PatientMedicationData();
                    try
                    {
                        MedicationData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        MedicationData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        MedicationData.PatientMedicationCntr = Convert.ToInt64(txtCntr.Text);
                        MedicationData.FacilityId = Convert.ToInt64(nudFacilityId.Value);
                        MedicationData.CodeValue = "";
                        MedicationData.CodeSystemId = 0;
                        MedicationData.MedicationName = txtDescription.Text;
                        MedicationData.ExpireDate = dtpDate.Value;
                        MedicationData.Active = true;
                        MedicationData.Quantity = 30;
                        //MedicationData.Days = 30;
                        MedicationData.RouteId = "";
                        MedicationData.Refills = 1;
                        MedicationData.Frequency = "";
                        MedicationData.Sig = "";
                        MedicationData.Diagnosis = "";
                        MedicationData.Pharmacy = "";
                        MedicationData.Note = "";
                        PatinetDocumentWS.SavePatientMedicationData(MedicationData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientProblem":
                    PatientDocumentService.PatientProblemData ProblemData = new PatientDocumentService.PatientProblemData();
                    try
                    {
                        ProblemData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ProblemData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ProblemData.PatientProblemCntr = Convert.ToInt64(txtCntr.Text);
                        ProblemData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ProblemData.Condition = txtDescription.Text;
                        ProblemData.EffectiveDate = DateVal;
                        ProblemData.ConditionStatusId = 1;
                        ProblemData.Note = "";
                        PatinetDocumentWS.SavePatientProblemData(ProblemData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientProcedure":
                    PatientDocumentService.PatientProcedureData ProcedureData = new PatientDocumentService.PatientProcedureData();
                    try
                    {
                        ProcedureData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ProcedureData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ProcedureData.PatProcedureCntr = Convert.ToInt64(txtCntr.Text);
                        ProcedureData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ProcedureData.Description = txtDescription.Text;
                        ProcedureData.ServiceDate = DateVal;
                        ProcedureData.CodeValue = "";
                        ProcedureData.CodeSystemId = 1;
                        ProcedureData.Diagnosis = "";
                        ProcedureData.PerformedBy = "";
                        ProcedureData.ServiceLocation = "";
                        ProcedureData.Note = "";
                        PatinetDocumentWS.SavePatientProcedureData(ProcedureData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientSocialHist":
                    PatientDocumentService.PatientSocialHistData SocialHistData = new PatientDocumentService.PatientSocialHistData();
                    try
                    {
                        SocialHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        SocialHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        SocialHistData.PatSocialHistCntr = Convert.ToInt64(txtCntr.Text);
                        SocialHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        SocialHistData.Description = txtDescription.Text;
                        SocialHistData.BeginDate = DateVal;
                        SocialHistData.EndDate = "";
                        SocialHistData.Qualifier = "";
                        SocialHistData.CodeValue = "";
                        SocialHistData.CodeSystemId = 1;
                        SocialHistData.Note = "";
                        PatinetDocumentWS.SavePatientSocialHistData(SocialHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientSurgicalHist":
                    PatientDocumentService.PatientSurgicalHistData SurgicalHistData = new PatientDocumentService.PatientSurgicalHistData();
                    try
                    {
                        SurgicalHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        SurgicalHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        SurgicalHistData.PatSurgicalHistCntr = Convert.ToInt64(txtCntr.Text);
                        SurgicalHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        SurgicalHistData.Description = txtDescription.Text;
                        SurgicalHistData.ServiceDate = DateVal;
                        SurgicalHistData.CodeValue = "";
                        SurgicalHistData.CodeSystemId = 1;
                        SurgicalHistData.Diagnosis = "";
                        SurgicalHistData.PerformedBy = "";
                        SurgicalHistData.ServiceLocation = "";
                        SurgicalHistData.Note = "";
  
                        PatinetDocumentWS.SavePatientSurgicalHistData(SurgicalHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientVitalSign":
                    PatientDocumentService.PatientVitalSignData VitalSignData = new PatientDocumentService.PatientVitalSignData();
                    try
                    {
                        VitalSignData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        VitalSignData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        VitalSignData.PatientVitalCntr = Convert.ToInt64(txtCntr.Text);
                        VitalSignData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        VitalSignData.VitalDate = dtpDate.Value;
                        VitalSignData.HeightIn = 0;
                        VitalSignData.WeightLb = 0;
                        VitalSignData.BloodPressurePosn = txtDescription.Text;
                        VitalSignData.Systolic = 120;
                        VitalSignData.Diastolic = 80;
                        VitalSignData.Pulse = 80;
                        VitalSignData.Respiration = 0;
                        PatinetDocumentWS.SavePatientVitalSignData(VitalSignData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientImmunization":
                    PatientDocumentService.PatientImmunizationData ImmunizationData = new PatientDocumentService.PatientImmunizationData();
                    try
                    {
                        ImmunizationData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ImmunizationData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ImmunizationData.PatientImmunizationCntr = Convert.ToInt64(txtCntr.Text);
                        ImmunizationData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ImmunizationData.ImmunizationDate = DateVal;
                        ImmunizationData.CodeValue = "";
                        ImmunizationData.CodeSystemId = 0;
                        ImmunizationData.Vaccine = txtDescription.Text;
                        ImmunizationData.Amount = "";
                        ImmunizationData.Route = "";
                        ImmunizationData.Site = "Left Arm";
                        ImmunizationData.Sequence = "1";
                        ImmunizationData.ExpirationDate = Convert.ToDateTime("1/1/2020");
                        ImmunizationData.LotNumber = "1231132";
                        ImmunizationData.Manufacturer = "";
                        ImmunizationData.Note = "";
                        PatinetDocumentWS.SavePatientImmunizationData(ImmunizationData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientInsurance":
                    PatientDocumentService.PatientInsuranceData InsuranceData = new PatientDocumentService.PatientInsuranceData();
                    try
                    {
                        InsuranceData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        InsuranceData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        InsuranceData.PatientInsuranceId = Convert.ToInt64(txtCntr.Text);
                        InsuranceData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                         InsuranceData.PlanName = txtDescription.Text;
                        InsuranceData.MemberNumber = "TEST1234";
                        InsuranceData.GroupNumber = "5432TEST";
                        InsuranceData.Subscriber = "";
                        InsuranceData.Relationship = "1";
                        InsuranceData.EffectiveDate = dtpDate.Value;

                        PatinetDocumentWS.SavePatientInsuranceData(InsuranceData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PlanOfCare":
                    PatientDocumentService.PatientPlanOfCareData PlanOfCareData = new PatientDocumentService.PatientPlanOfCareData();
                    try
                    {
                        PlanOfCareData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        PlanOfCareData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        PlanOfCareData.PlanCntr = Convert.ToInt64(txtCntr.Text);
                        PlanOfCareData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        PlanOfCareData.Instruction = txtDescription.Text;
                        PlanOfCareData.InstructionTypeId = 1;
                        PlanOfCareData.Goal = "This is the goal";
                        PlanOfCareData.Note = "";
                        PlanOfCareData.AppointmentDateTime = dtpDate.Value;
                        PlanOfCareData.ProviderId = 1;

                        PatinetDocumentWS.SavePatientPlanOfCareData(PlanOfCareData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientPharmacy":
                    PatientDocumentService.PatientPharmacyData PatientPharmacyData = new PatientDocumentService.PatientPharmacyData();
                    try
                    {
                        PatientPharmacyData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        PatientPharmacyData.PharmacyCntr = Convert.ToInt64(txtCntr.Text);
                        PatientPharmacyData.PharmacyName = txtDescription.Text;
                        PatinetDocumentWS.SavePatientPharmacyData(PatientPharmacyData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
            }
            txtCntr.Text = "0";
            txtDescription.Text = "";
        }

        private void dgCodes_Click(object sender, EventArgs e)
        {
            if (dgCodes.Rows.Count > 0)
            switch (cboTable.Text)
            {
                case "PatientAllergy":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientAllergyCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Allergen"].Value.ToString();
                    break;
                case "PatientFamilyHist":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatFamilyHistCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Description"].Value.ToString();
                    break;
                case "PatientMedicalHist":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatMedicalHistCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Description"].Value.ToString();
                    break;
                case "PatientMedication":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientMedicationCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["MedicationName"].Value.ToString();
                    break;
                case "PatientProblem":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientProblemCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Condition"].Value.ToString();
                    break;
                case "PatientProcedure":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatProcedureCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Description"].Value.ToString();
                    break;
                case "PatientSocialHist":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatSocialHistCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Description"].Value.ToString();
                    break;
                case "PatientSurgicalHist":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatSurgicalHistCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Description"].Value.ToString();
                    break;
                case "PatientVitalSign":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientVitalCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["BloodPressurePosn"].Value.ToString();
                    break;
                case "PatientImmunization":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientImmunizationCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["Vaccine"].Value.ToString();
                    break;
                case "PatientInsurance":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PatientInsuranceId"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["PlanName"].Value.ToString();
                    break;
                case "PatientPharmacy":
                    txtCntr.Text = dgCodes.CurrentRow.Cells["PharmacyCntr"].Value.ToString();
                    txtDescription.Text = dgCodes.CurrentRow.Cells["PharmacyName"].Value.ToString();
                    break;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();


            switch (cboTable.Text)
            {
                case "PatientAllergy":
                    PatientDocumentService.PatientAllergyData AllergyData = new PatientDocumentService.PatientAllergyData();
                    try
                    {
                        AllergyData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        AllergyData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        AllergyData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        AllergyData.PatientAllergyCntr = Convert.ToInt64(txtCntr.Text);
                        
                        PatinetDocumentWS.DeletePatientAllergyData(AllergyData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientFamilyHist":
                    PatientDocumentService.PatientFamilyHistData FamilyHistData = new PatientDocumentService.PatientFamilyHistData();
                    try
                    {
                        FamilyHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        FamilyHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        FamilyHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        FamilyHistData.PatFamilyHistCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientFamilyHistData(FamilyHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientMedicalHist":
                    PatientDocumentService.PatientMedicalHistData MedicalHistData = new PatientDocumentService.PatientMedicalHistData();
                    try
                    {
                        MedicalHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        MedicalHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        MedicalHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        MedicalHistData.PatMedicalHistCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientMedicalHistData(MedicalHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientMedication":
                    PatientDocumentService.PatientMedicationData MedicationData = new PatientDocumentService.PatientMedicationData();
                    try
                    {
                        MedicationData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        MedicationData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        MedicationData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        MedicationData.PatientMedicationCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientMedicationData(MedicationData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientProblem":
                    PatientDocumentService.PatientProblemData ProblemData = new PatientDocumentService.PatientProblemData();
                    try
                    {
                        ProblemData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ProblemData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ProblemData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ProblemData.PatientProblemCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientProblemData(ProblemData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientProcedure":
                    PatientDocumentService.PatientProcedureData ProcedureData = new PatientDocumentService.PatientProcedureData();
                    try
                    {
                        ProcedureData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ProcedureData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ProcedureData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ProcedureData.PatProcedureCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientProcedureData(ProcedureData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientSocialHist":
                    PatientDocumentService.PatientSocialHistData SocialHistData = new PatientDocumentService.PatientSocialHistData();
                    try
                    {
                        SocialHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        SocialHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        SocialHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        SocialHistData.PatSocialHistCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientSocialHistData(SocialHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientSurgicalHist":
                    PatientDocumentService.PatientSurgicalHistData SurgicalHistData = new PatientDocumentService.PatientSurgicalHistData();
                    try
                    {
                        SurgicalHistData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        SurgicalHistData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        SurgicalHistData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        SurgicalHistData.PatSurgicalHistCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientSurgicalHistData(SurgicalHistData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientVitalSign":
                    PatientDocumentService.PatientVitalSignData VitalSignData = new PatientDocumentService.PatientVitalSignData();
                    try
                    {
                        VitalSignData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        VitalSignData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        VitalSignData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        VitalSignData.PatientVitalCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientVitalSignData(VitalSignData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
                case "PatientImmunization":
                    PatientDocumentService.PatientImmunizationData ImmunizationData = new PatientDocumentService.PatientImmunizationData();
                    try
                    {
                        ImmunizationData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        ImmunizationData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        ImmunizationData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        ImmunizationData.PatientImmunizationCntr = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientImmunizationData(ImmunizationData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving"); 
                    }
                    break;
                case "PatientInsurance":
                    PatientDocumentService.PatientInsuranceData InsuranceData = new PatientDocumentService.PatientInsuranceData();
                    try
                    {
                        InsuranceData.PatientId = Convert.ToInt64(nudPatientId.Value);
                        InsuranceData.FacilityId = Convert.ToInt64(nudFacilityId.Value); 
                        InsuranceData.VisitId = Convert.ToInt64(nudVisitId.Value);
                        InsuranceData.PatientInsuranceId = Convert.ToInt64(txtCntr.Text);

                        PatinetDocumentWS.DeletePatientInsuranceData(InsuranceData, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Saving");
                    }
                    break;
            }
            txtCntr.Text = "0";
            txtDescription.Text = "";
        }

        private void cboTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTable.Text == "PatientMedication" || cboTable.Text == "PatientVitalSign" || cboTable.Text == "PatientInsurance" || cboTable.Text == "PlanOfCare")
            {
                nudMonth.Visible = false;
                nudDay.Visible = false;
                nudYear.Visible = false;
                dtpDate.Visible = true;
            }
            else
            {
                nudMonth.Visible = true;
                nudDay.Visible = true;
                nudYear.Visible = true;
                dtpDate.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PatientDocumentService.PatientDocumentWS PatinetDocumentWS = new PatientDocumentService.PatientDocumentWS();

            PatientDocumentService.PatientSummaryTableData DocTableData = new PatientDocumentService.PatientSummaryTableData();



            PatientDocumentService.PatientSummaryParms DocParms = new PatientDocumentService.PatientSummaryParms();
            DocParms.PatientId = 14;
            DocParms.FacilityId = 4;
            DocParms.VisitId = 2;
            DocParms.Option = 1;
            DocParms.Active = 1;
            DocParms.Allergy = false;
            DocParms.Medication = true;
            DocParms.SocialHist = true;
            DocParms.FamilyHist = false;
            DocParms.MedicalHist = false;
            DocParms.Problem = true;
            DocParms.Procedure = false;
            DocParms.VitalSign = true;
            DocParms.Immunization = true;
            DocParms.PlanOfCare = false;
            DocParms.Lab = false;

            DocTableData = PatinetDocumentWS.GetPatientSummaryData(DocParms, GlobalVar.Token, GlobalVar.UserId, GlobalVar.FacilityId);

            DataTable dt = DocTableData.dtAllergy;
        }
    }
}
