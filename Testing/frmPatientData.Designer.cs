namespace Testing
{
    partial class frmPatientData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.dgCodes = new System.Windows.Forms.DataGridView();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.nudVisitId = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCntr = new System.Windows.Forms.TextBox();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.nudMonth = new System.Windows.Forms.NumericUpDown();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.nudDay = new System.Windows.Forms.NumericUpDown();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nudFacilityId = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table";
            // 
            // cboTable
            // 
            this.cboTable.AutoCompleteCustomSource.AddRange(new string[] {
            "Allergen",
            "Country",
            "EquipmentType",
            "Ethnicity",
            "FacilityType",
            "Gender",
            "MaritalStatus",
            "PreferredLanguage",
            "Race"});
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Items.AddRange(new object[] {
            "PatientAllergy",
            "PatientFamilyHist",
            "PatientMedicalHist",
            "PatientMedication",
            "PatientProblem",
            "PatientProcedure",
            "PatientSocialHist",
            "PatientSurgicalHist",
            "PatientVitalSign",
            "PatientImmunization",
            "PatientInsurance",
            "PlanOfCare",
            "PatientPharmacy"});
            this.cboTable.Location = new System.Drawing.Point(77, 7);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(210, 21);
            this.cboTable.TabIndex = 1;
            this.cboTable.SelectedIndexChanged += new System.EventHandler(this.cboTable_SelectedIndexChanged);
            // 
            // dgCodes
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCodes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCodes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCodes.Location = new System.Drawing.Point(25, 86);
            this.dgCodes.Name = "dgCodes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCodes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgCodes.Size = new System.Drawing.Size(591, 223);
            this.dgCodes.TabIndex = 13;
            this.dgCodes.Click += new System.EventHandler(this.dgCodes_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(172, 314);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(91, 314);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 14;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(77, 34);
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 3;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(310, 38);
            this.txtDescription.MaxLength = 20;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(194, 20);
            this.txtDescription.TabIndex = 8;
            // 
            // nudVisitId
            // 
            this.nudVisitId.Location = new System.Drawing.Point(77, 60);
            this.nudVisitId.Name = "nudVisitId";
            this.nudVisitId.Size = new System.Drawing.Size(75, 20);
            this.nudVisitId.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Patient Id";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Visit Id";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(244, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(244, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Date";
            // 
            // txtCntr
            // 
            this.txtCntr.Enabled = false;
            this.txtCntr.Location = new System.Drawing.Point(190, 34);
            this.txtCntr.MaxLength = 20;
            this.txtCntr.Name = "txtCntr";
            this.txtCntr.Size = new System.Drawing.Size(48, 20);
            this.txtCntr.TabIndex = 4;
            this.txtCntr.Text = "0";
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(253, 315);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 16;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // nudMonth
            // 
            this.nudMonth.Location = new System.Drawing.Point(310, 64);
            this.nudMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudMonth.Name = "nudMonth";
            this.nudMonth.Size = new System.Drawing.Size(52, 20);
            this.nudMonth.TabIndex = 10;
            // 
            // nudYear
            // 
            this.nudYear.Location = new System.Drawing.Point(429, 64);
            this.nudYear.Maximum = new decimal(new int[] {
            2014,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(75, 20);
            this.nudYear.TabIndex = 12;
            this.nudYear.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            // 
            // nudDay
            // 
            this.nudDay.Location = new System.Drawing.Point(368, 65);
            this.nudDay.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudDay.Name = "nudDay";
            this.nudDay.Size = new System.Drawing.Size(52, 20);
            this.nudDay.TabIndex = 11;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(310, 67);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 17;
            this.dtpDate.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(550, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Summary";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "FacilityId";
            // 
            // nudFacilityId
            // 
            this.nudFacilityId.Location = new System.Drawing.Point(368, 8);
            this.nudFacilityId.Name = "nudFacilityId";
            this.nudFacilityId.Size = new System.Drawing.Size(75, 20);
            this.nudFacilityId.TabIndex = 20;
            // 
            // frmPatientData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 347);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudFacilityId);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.nudDay);
            this.Controls.Add(this.nudYear);
            this.Controls.Add(this.nudMonth);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.txtCntr);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudVisitId);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.dgCodes);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdRead);
            this.Name = "frmPatientData";
            this.Text = "frmPatientData";
            ((System.ComponentModel.ISupportInitialize)(this.dgCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.DataGridView dgCodes;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.NumericUpDown nudVisitId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCntr;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.NumericUpDown nudMonth;
        private System.Windows.Forms.NumericUpDown nudYear;
        private System.Windows.Forms.NumericUpDown nudDay;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudFacilityId;
    }
}