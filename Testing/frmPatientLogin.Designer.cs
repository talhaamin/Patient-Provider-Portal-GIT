namespace Testing
{
    partial class frmPatientLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOld = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.cmdChangePassword = new System.Windows.Forms.Button();
            this.cmdSecurity = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboQuestions = new System.Windows.Forms.ComboBox();
            this.cmdSecurityGet = new System.Windows.Forms.Button();
            this.txtSecurityQuestion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdResetPassword = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSecurityAnswer = new System.Windows.Forms.TextBox();
            this.gbRepresentative = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.cmdCareProvider = new System.Windows.Forms.Button();
            this.chkLab = new System.Windows.Forms.CheckBox();
            this.chkFamily = new System.Windows.Forms.CheckBox();
            this.chkAllergy = new System.Windows.Forms.CheckBox();
            this.chkDemographics = new System.Windows.Forms.CheckBox();
            this.chkProcedure = new System.Windows.Forms.CheckBox();
            this.chkProblem = new System.Windows.Forms.CheckBox();
            this.chkMedication = new System.Windows.Forms.CheckBox();
            this.chkMedical = new System.Windows.Forms.CheckBox();
            this.chkEmergency = new System.Windows.Forms.CheckBox();
            this.chkInsurance = new System.Windows.Forms.CheckBox();
            this.chkClincal = new System.Windows.Forms.CheckBox();
            this.chkOrgan = new System.Windows.Forms.CheckBox();
            this.chkImmunization = new System.Windows.Forms.CheckBox();
            this.chkVital = new System.Windows.Forms.CheckBox();
            this.chkSurgical = new System.Windows.Forms.CheckBox();
            this.chkSocial = new System.Windows.Forms.CheckBox();
            this.chkMessaging = new System.Windows.Forms.CheckBox();
            this.chkPlanOfCare = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkVisit = new System.Windows.Forms.CheckBox();
            this.chkAppointment = new System.Windows.Forms.CheckBox();
            this.chkDownload = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.gbRepresentative.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(106, 60);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 34;
            this.cmdSave.Text = "Login";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // txtLogin
            // 
            this.txtLogin.Location = new System.Drawing.Point(121, 12);
            this.txtLogin.MaxLength = 20;
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(129, 20);
            this.txtLogin.TabIndex = 31;
            this.txtLogin.Text = "5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Login Id";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(121, 34);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(129, 20);
            this.txtPassword.TabIndex = 33;
            this.txtPassword.Text = "pass";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Old Password";
            // 
            // txtOld
            // 
            this.txtOld.Enabled = false;
            this.txtOld.Location = new System.Drawing.Point(121, 100);
            this.txtOld.MaxLength = 20;
            this.txtOld.Name = "txtOld";
            this.txtOld.Size = new System.Drawing.Size(129, 20);
            this.txtOld.TabIndex = 36;
            this.txtOld.Text = "pass";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 37;
            this.label3.Text = "New Password";
            // 
            // txtNew
            // 
            this.txtNew.Enabled = false;
            this.txtNew.Location = new System.Drawing.Point(121, 126);
            this.txtNew.MaxLength = 20;
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(129, 20);
            this.txtNew.TabIndex = 38;
            this.txtNew.Text = "pass";
            // 
            // cmdChangePassword
            // 
            this.cmdChangePassword.Enabled = false;
            this.cmdChangePassword.Location = new System.Drawing.Point(90, 152);
            this.cmdChangePassword.Name = "cmdChangePassword";
            this.cmdChangePassword.Size = new System.Drawing.Size(105, 23);
            this.cmdChangePassword.TabIndex = 39;
            this.cmdChangePassword.Text = "Change Password";
            this.cmdChangePassword.UseVisualStyleBackColor = true;
            this.cmdChangePassword.Click += new System.EventHandler(this.cmdChangePassword_Click);
            // 
            // cmdSecurity
            // 
            this.cmdSecurity.Enabled = false;
            this.cmdSecurity.Location = new System.Drawing.Point(90, 238);
            this.cmdSecurity.Name = "cmdSecurity";
            this.cmdSecurity.Size = new System.Drawing.Size(105, 23);
            this.cmdSecurity.TabIndex = 44;
            this.cmdSecurity.Text = "Set Secuirty ?";
            this.cmdSecurity.UseVisualStyleBackColor = true;
            this.cmdSecurity.Click += new System.EventHandler(this.cmdSecurity_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "Answer";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Enabled = false;
            this.txtAnswer.Location = new System.Drawing.Point(121, 212);
            this.txtAnswer.MaxLength = 20;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(129, 20);
            this.txtAnswer.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Security Question";
            // 
            // cboQuestions
            // 
            this.cboQuestions.AutoCompleteCustomSource.AddRange(new string[] {
            "Allergen",
            "Country",
            "EquipmentType",
            "Ethnicity",
            "FacilityType",
            "Gender",
            "MaritalStatus",
            "PreferredLanguage",
            "Race"});
            this.cboQuestions.Enabled = false;
            this.cboQuestions.FormattingEnabled = true;
            this.cboQuestions.Location = new System.Drawing.Point(121, 181);
            this.cboQuestions.Name = "cboQuestions";
            this.cboQuestions.Size = new System.Drawing.Size(210, 21);
            this.cboQuestions.TabIndex = 45;
            // 
            // cmdSecurityGet
            // 
            this.cmdSecurityGet.Location = new System.Drawing.Point(499, 34);
            this.cmdSecurityGet.Name = "cmdSecurityGet";
            this.cmdSecurityGet.Size = new System.Drawing.Size(105, 23);
            this.cmdSecurityGet.TabIndex = 48;
            this.cmdSecurityGet.Text = "Get Secuirty ?";
            this.cmdSecurityGet.UseVisualStyleBackColor = true;
            this.cmdSecurityGet.Click += new System.EventHandler(this.cmdSecurityGet_Click);
            // 
            // txtSecurityQuestion
            // 
            this.txtSecurityQuestion.Enabled = false;
            this.txtSecurityQuestion.Location = new System.Drawing.Point(476, 5);
            this.txtSecurityQuestion.MaxLength = 20;
            this.txtSecurityQuestion.Name = "txtSecurityQuestion";
            this.txtSecurityQuestion.Size = new System.Drawing.Size(225, 20);
            this.txtSecurityQuestion.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(380, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 46;
            this.label7.Text = "Security Question";
            // 
            // cmdResetPassword
            // 
            this.cmdResetPassword.Enabled = false;
            this.cmdResetPassword.Location = new System.Drawing.Point(499, 90);
            this.cmdResetPassword.Name = "cmdResetPassword";
            this.cmdResetPassword.Size = new System.Drawing.Size(105, 23);
            this.cmdResetPassword.TabIndex = 51;
            this.cmdResetPassword.Text = "Reset Password";
            this.cmdResetPassword.UseVisualStyleBackColor = true;
            this.cmdResetPassword.Click += new System.EventHandler(this.cmdResetPassword_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(387, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Answer";
            // 
            // txtSecurityAnswer
            // 
            this.txtSecurityAnswer.Enabled = false;
            this.txtSecurityAnswer.Location = new System.Drawing.Point(476, 63);
            this.txtSecurityAnswer.MaxLength = 20;
            this.txtSecurityAnswer.Name = "txtSecurityAnswer";
            this.txtSecurityAnswer.Size = new System.Drawing.Size(225, 20);
            this.txtSecurityAnswer.TabIndex = 50;
            // 
            // gbRepresentative
            // 
            this.gbRepresentative.Controls.Add(this.label11);
            this.gbRepresentative.Controls.Add(this.txtEmail);
            this.gbRepresentative.Controls.Add(this.chkDownload);
            this.gbRepresentative.Controls.Add(this.chkMessaging);
            this.gbRepresentative.Controls.Add(this.chkPlanOfCare);
            this.gbRepresentative.Controls.Add(this.chkUpload);
            this.gbRepresentative.Controls.Add(this.chkVisit);
            this.gbRepresentative.Controls.Add(this.chkAppointment);
            this.gbRepresentative.Controls.Add(this.chkEmergency);
            this.gbRepresentative.Controls.Add(this.chkInsurance);
            this.gbRepresentative.Controls.Add(this.chkClincal);
            this.gbRepresentative.Controls.Add(this.chkOrgan);
            this.gbRepresentative.Controls.Add(this.chkImmunization);
            this.gbRepresentative.Controls.Add(this.chkVital);
            this.gbRepresentative.Controls.Add(this.chkSurgical);
            this.gbRepresentative.Controls.Add(this.chkSocial);
            this.gbRepresentative.Controls.Add(this.chkProcedure);
            this.gbRepresentative.Controls.Add(this.chkProblem);
            this.gbRepresentative.Controls.Add(this.chkMedication);
            this.gbRepresentative.Controls.Add(this.chkMedical);
            this.gbRepresentative.Controls.Add(this.chkLab);
            this.gbRepresentative.Controls.Add(this.chkFamily);
            this.gbRepresentative.Controls.Add(this.chkAllergy);
            this.gbRepresentative.Controls.Add(this.chkDemographics);
            this.gbRepresentative.Controls.Add(this.cmdCareProvider);
            this.gbRepresentative.Controls.Add(this.label9);
            this.gbRepresentative.Controls.Add(this.txtLastName);
            this.gbRepresentative.Controls.Add(this.label10);
            this.gbRepresentative.Controls.Add(this.txtFirstName);
            this.gbRepresentative.Enabled = false;
            this.gbRepresentative.Location = new System.Drawing.Point(728, 8);
            this.gbRepresentative.Name = "gbRepresentative";
            this.gbRepresentative.Size = new System.Drawing.Size(398, 321);
            this.gbRepresentative.TabIndex = 52;
            this.gbRepresentative.TabStop = false;
            this.gbRepresentative.Text = "Patient Representative";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Last Name";
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(97, 45);
            this.txtLastName.MaxLength = 20;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(194, 20);
            this.txtLastName.TabIndex = 9;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "First Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(97, 19);
            this.txtFirstName.MaxLength = 20;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(194, 20);
            this.txtFirstName.TabIndex = 7;
            // 
            // cmdCareProvider
            // 
            this.cmdCareProvider.Location = new System.Drawing.Point(111, 288);
            this.cmdCareProvider.Name = "cmdCareProvider";
            this.cmdCareProvider.Size = new System.Drawing.Size(105, 23);
            this.cmdCareProvider.TabIndex = 52;
            this.cmdCareProvider.Text = "Save";
            this.cmdCareProvider.UseVisualStyleBackColor = true;
            this.cmdCareProvider.Click += new System.EventHandler(this.cmdCareProvider_Click);
            // 
            // chkLab
            // 
            this.chkLab.AutoSize = true;
            this.chkLab.Location = new System.Drawing.Point(34, 164);
            this.chkLab.Name = "chkLab";
            this.chkLab.Size = new System.Drawing.Size(82, 17);
            this.chkLab.TabIndex = 56;
            this.chkLab.Text = "Lab Results";
            this.chkLab.UseVisualStyleBackColor = true;
            // 
            // chkFamily
            // 
            this.chkFamily.AutoSize = true;
            this.chkFamily.Location = new System.Drawing.Point(34, 141);
            this.chkFamily.Name = "chkFamily";
            this.chkFamily.Size = new System.Drawing.Size(76, 17);
            this.chkFamily.TabIndex = 55;
            this.chkFamily.Text = "Family Hist";
            this.chkFamily.UseVisualStyleBackColor = true;
            // 
            // chkAllergy
            // 
            this.chkAllergy.AutoSize = true;
            this.chkAllergy.Location = new System.Drawing.Point(34, 118);
            this.chkAllergy.Name = "chkAllergy";
            this.chkAllergy.Size = new System.Drawing.Size(57, 17);
            this.chkAllergy.TabIndex = 54;
            this.chkAllergy.Text = "Allergy";
            this.chkAllergy.UseVisualStyleBackColor = true;
            // 
            // chkDemographics
            // 
            this.chkDemographics.AutoSize = true;
            this.chkDemographics.Location = new System.Drawing.Point(34, 95);
            this.chkDemographics.Name = "chkDemographics";
            this.chkDemographics.Size = new System.Drawing.Size(94, 17);
            this.chkDemographics.TabIndex = 53;
            this.chkDemographics.Text = "Demographics";
            this.chkDemographics.UseVisualStyleBackColor = true;
            // 
            // chkProcedure
            // 
            this.chkProcedure.AutoSize = true;
            this.chkProcedure.Location = new System.Drawing.Point(34, 256);
            this.chkProcedure.Name = "chkProcedure";
            this.chkProcedure.Size = new System.Drawing.Size(75, 17);
            this.chkProcedure.TabIndex = 60;
            this.chkProcedure.Text = "Procedure";
            this.chkProcedure.UseVisualStyleBackColor = true;
            // 
            // chkProblem
            // 
            this.chkProblem.AutoSize = true;
            this.chkProblem.Location = new System.Drawing.Point(34, 233);
            this.chkProblem.Name = "chkProblem";
            this.chkProblem.Size = new System.Drawing.Size(64, 17);
            this.chkProblem.TabIndex = 59;
            this.chkProblem.Text = "Problem";
            this.chkProblem.UseVisualStyleBackColor = true;
            // 
            // chkMedication
            // 
            this.chkMedication.AutoSize = true;
            this.chkMedication.Location = new System.Drawing.Point(34, 210);
            this.chkMedication.Name = "chkMedication";
            this.chkMedication.Size = new System.Drawing.Size(78, 17);
            this.chkMedication.TabIndex = 58;
            this.chkMedication.Text = "Medication";
            this.chkMedication.UseVisualStyleBackColor = true;
            // 
            // chkMedical
            // 
            this.chkMedical.AutoSize = true;
            this.chkMedical.Location = new System.Drawing.Point(34, 187);
            this.chkMedical.Name = "chkMedical";
            this.chkMedical.Size = new System.Drawing.Size(84, 17);
            this.chkMedical.TabIndex = 57;
            this.chkMedical.Text = "Medical Hist";
            this.chkMedical.UseVisualStyleBackColor = true;
            // 
            // chkEmergency
            // 
            this.chkEmergency.AutoSize = true;
            this.chkEmergency.Location = new System.Drawing.Point(145, 256);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Size = new System.Drawing.Size(79, 17);
            this.chkEmergency.TabIndex = 68;
            this.chkEmergency.Text = "Emergency";
            this.chkEmergency.UseVisualStyleBackColor = true;
            // 
            // chkInsurance
            // 
            this.chkInsurance.AutoSize = true;
            this.chkInsurance.Location = new System.Drawing.Point(145, 233);
            this.chkInsurance.Name = "chkInsurance";
            this.chkInsurance.Size = new System.Drawing.Size(73, 17);
            this.chkInsurance.TabIndex = 67;
            this.chkInsurance.Text = "Insurance";
            this.chkInsurance.UseVisualStyleBackColor = true;
            // 
            // chkClincal
            // 
            this.chkClincal.AutoSize = true;
            this.chkClincal.Location = new System.Drawing.Point(145, 210);
            this.chkClincal.Name = "chkClincal";
            this.chkClincal.Size = new System.Drawing.Size(87, 17);
            this.chkClincal.TabIndex = 66;
            this.chkClincal.Text = "Clincial Docs";
            this.chkClincal.UseVisualStyleBackColor = true;
            // 
            // chkOrgan
            // 
            this.chkOrgan.AutoSize = true;
            this.chkOrgan.Location = new System.Drawing.Point(145, 187);
            this.chkOrgan.Name = "chkOrgan";
            this.chkOrgan.Size = new System.Drawing.Size(55, 17);
            this.chkOrgan.TabIndex = 65;
            this.chkOrgan.Text = "Organ";
            this.chkOrgan.UseVisualStyleBackColor = true;
            // 
            // chkImmunization
            // 
            this.chkImmunization.AutoSize = true;
            this.chkImmunization.Location = new System.Drawing.Point(145, 164);
            this.chkImmunization.Name = "chkImmunization";
            this.chkImmunization.Size = new System.Drawing.Size(84, 17);
            this.chkImmunization.TabIndex = 64;
            this.chkImmunization.Text = "Immunizaion";
            this.chkImmunization.UseVisualStyleBackColor = true;
            // 
            // chkVital
            // 
            this.chkVital.AutoSize = true;
            this.chkVital.Location = new System.Drawing.Point(145, 141);
            this.chkVital.Name = "chkVital";
            this.chkVital.Size = new System.Drawing.Size(75, 17);
            this.chkVital.TabIndex = 63;
            this.chkVital.Text = "Vital Signs";
            this.chkVital.UseVisualStyleBackColor = true;
            // 
            // chkSurgical
            // 
            this.chkSurgical.AutoSize = true;
            this.chkSurgical.Location = new System.Drawing.Point(145, 118);
            this.chkSurgical.Name = "chkSurgical";
            this.chkSurgical.Size = new System.Drawing.Size(85, 17);
            this.chkSurgical.TabIndex = 62;
            this.chkSurgical.Text = "Surgical Hist";
            this.chkSurgical.UseVisualStyleBackColor = true;
            // 
            // chkSocial
            // 
            this.chkSocial.AutoSize = true;
            this.chkSocial.Location = new System.Drawing.Point(145, 95);
            this.chkSocial.Name = "chkSocial";
            this.chkSocial.Size = new System.Drawing.Size(76, 17);
            this.chkSocial.TabIndex = 61;
            this.chkSocial.Text = "Social Hist";
            this.chkSocial.UseVisualStyleBackColor = true;
            // 
            // chkMessaging
            // 
            this.chkMessaging.AutoSize = true;
            this.chkMessaging.Location = new System.Drawing.Point(252, 187);
            this.chkMessaging.Name = "chkMessaging";
            this.chkMessaging.Size = new System.Drawing.Size(77, 17);
            this.chkMessaging.TabIndex = 73;
            this.chkMessaging.Text = "Messaging";
            this.chkMessaging.UseVisualStyleBackColor = true;
            // 
            // chkPlanOfCare
            // 
            this.chkPlanOfCare.AutoSize = true;
            this.chkPlanOfCare.Location = new System.Drawing.Point(252, 164);
            this.chkPlanOfCare.Name = "chkPlanOfCare";
            this.chkPlanOfCare.Size = new System.Drawing.Size(86, 17);
            this.chkPlanOfCare.TabIndex = 72;
            this.chkPlanOfCare.Text = "Plan Of Care";
            this.chkPlanOfCare.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.AutoSize = true;
            this.chkUpload.Location = new System.Drawing.Point(252, 141);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(88, 17);
            this.chkUpload.TabIndex = 71;
            this.chkUpload.Text = "Upload Docs";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkVisit
            // 
            this.chkVisit.AutoSize = true;
            this.chkVisit.Location = new System.Drawing.Point(252, 118);
            this.chkVisit.Name = "chkVisit";
            this.chkVisit.Size = new System.Drawing.Size(45, 17);
            this.chkVisit.TabIndex = 70;
            this.chkVisit.Text = "Visit";
            this.chkVisit.UseVisualStyleBackColor = true;
            // 
            // chkAppointment
            // 
            this.chkAppointment.AutoSize = true;
            this.chkAppointment.Location = new System.Drawing.Point(252, 95);
            this.chkAppointment.Name = "chkAppointment";
            this.chkAppointment.Size = new System.Drawing.Size(85, 17);
            this.chkAppointment.TabIndex = 69;
            this.chkAppointment.Text = "Appointment";
            this.chkAppointment.UseVisualStyleBackColor = true;
            // 
            // chkDownload
            // 
            this.chkDownload.AutoSize = true;
            this.chkDownload.Location = new System.Drawing.Point(252, 210);
            this.chkDownload.Name = "chkDownload";
            this.chkDownload.Size = new System.Drawing.Size(125, 17);
            this.chkDownload.TabIndex = 74;
            this.chkDownload.Text = "Download / Transmit";
            this.chkDownload.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(31, 72);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 13);
            this.label11.TabIndex = 75;
            this.label11.Text = "EMail";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(97, 69);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(194, 20);
            this.txtEmail.TabIndex = 76;
            // 
            // frmPatientLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 363);
            this.Controls.Add(this.gbRepresentative);
            this.Controls.Add(this.cmdResetPassword);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtSecurityAnswer);
            this.Controls.Add(this.cmdSecurityGet);
            this.Controls.Add(this.txtSecurityQuestion);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboQuestions);
            this.Controls.Add(this.cmdSecurity);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmdChangePassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOld);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPassword);
            this.Name = "frmPatientLogin";
            this.Text = "frmPatientLogin";
            this.Load += new System.EventHandler(this.frmPatientLogin_Load);
            this.gbRepresentative.ResumeLayout(false);
            this.gbRepresentative.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Button cmdChangePassword;
        private System.Windows.Forms.Button cmdSecurity;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboQuestions;
        private System.Windows.Forms.Button cmdSecurityGet;
        private System.Windows.Forms.TextBox txtSecurityQuestion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdResetPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSecurityAnswer;
        private System.Windows.Forms.GroupBox gbRepresentative;
        private System.Windows.Forms.Button cmdCareProvider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.CheckBox chkLab;
        private System.Windows.Forms.CheckBox chkFamily;
        private System.Windows.Forms.CheckBox chkAllergy;
        private System.Windows.Forms.CheckBox chkDemographics;
        private System.Windows.Forms.CheckBox chkDownload;
        private System.Windows.Forms.CheckBox chkMessaging;
        private System.Windows.Forms.CheckBox chkPlanOfCare;
        private System.Windows.Forms.CheckBox chkUpload;
        private System.Windows.Forms.CheckBox chkVisit;
        private System.Windows.Forms.CheckBox chkAppointment;
        private System.Windows.Forms.CheckBox chkEmergency;
        private System.Windows.Forms.CheckBox chkInsurance;
        private System.Windows.Forms.CheckBox chkClincal;
        private System.Windows.Forms.CheckBox chkOrgan;
        private System.Windows.Forms.CheckBox chkImmunization;
        private System.Windows.Forms.CheckBox chkVital;
        private System.Windows.Forms.CheckBox chkSurgical;
        private System.Windows.Forms.CheckBox chkSocial;
        private System.Windows.Forms.CheckBox chkProcedure;
        private System.Windows.Forms.CheckBox chkProblem;
        private System.Windows.Forms.CheckBox chkMedication;
        private System.Windows.Forms.CheckBox chkMedical;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEmail;
    }
}