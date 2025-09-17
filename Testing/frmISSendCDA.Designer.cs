namespace Testing
{
    partial class frmISSendCDA
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
            this.cmdProcess = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtEmailTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudFacilityId = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudProviderId = new System.Windows.Forms.NumericUpDown();
            this.txtEmailFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmailFrom2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmailTo2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmailPatient2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFullName2 = new System.Windows.Forms.TextBox();
            this.cmdOpenFile2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFileName2 = new System.Windows.Forms.TextBox();
            this.cmdProcess2 = new System.Windows.Forms.Button();
            this.txtPatientName2 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProviderId)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdProcess
            // 
            this.cmdProcess.Location = new System.Drawing.Point(153, 198);
            this.cmdProcess.Name = "cmdProcess";
            this.cmdProcess.Size = new System.Drawing.Size(75, 23);
            this.cmdProcess.TabIndex = 0;
            this.cmdProcess.Text = "Process";
            this.cmdProcess.UseVisualStyleBackColor = true;
            this.cmdProcess.Click += new System.EventHandler(this.cmdProcess_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Enabled = false;
            this.txtFileName.Location = new System.Drawing.Point(79, 146);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(232, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "File Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(79, 20);
            this.nudPatientId.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 4;
            this.nudPatientId.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFile.ForeColor = System.Drawing.Color.Navy;
            this.cmdOpenFile.Location = new System.Drawing.Point(317, 144);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(29, 23);
            this.cmdOpenFile.TabIndex = 75;
            this.cmdOpenFile.Text = "...";
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.Enabled = false;
            this.txtFullName.Location = new System.Drawing.Point(79, 172);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(232, 20);
            this.txtFullName.TabIndex = 76;
            this.txtFullName.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtEmailTo
            // 
            this.txtEmailTo.Location = new System.Drawing.Point(79, 120);
            this.txtEmailTo.Name = "txtEmailTo";
            this.txtEmailTo.Size = new System.Drawing.Size(233, 20);
            this.txtEmailTo.TabIndex = 78;
            this.txtEmailTo.Text = "support@direct.amrpatientportal.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "To Email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "FacilityId";
            // 
            // nudFacilityId
            // 
            this.nudFacilityId.Location = new System.Drawing.Point(79, 46);
            this.nudFacilityId.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudFacilityId.Name = "nudFacilityId";
            this.nudFacilityId.Size = new System.Drawing.Size(75, 20);
            this.nudFacilityId.TabIndex = 80;
            this.nudFacilityId.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 81;
            this.label5.Text = "ProviderId";
            // 
            // nudProviderId
            // 
            this.nudProviderId.Location = new System.Drawing.Point(79, 72);
            this.nudProviderId.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudProviderId.Name = "nudProviderId";
            this.nudProviderId.Size = new System.Drawing.Size(75, 20);
            this.nudProviderId.TabIndex = 82;
            this.nudProviderId.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // txtEmailFrom
            // 
            this.txtEmailFrom.Location = new System.Drawing.Point(78, 95);
            this.txtEmailFrom.Name = "txtEmailFrom";
            this.txtEmailFrom.Size = new System.Drawing.Size(233, 20);
            this.txtEmailFrom.TabIndex = 84;
            this.txtEmailFrom.Text = "support@direct.amrpatientportal.com";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "From Email";
            // 
            // txtEmailFrom2
            // 
            this.txtEmailFrom2.Location = new System.Drawing.Point(546, 12);
            this.txtEmailFrom2.Name = "txtEmailFrom2";
            this.txtEmailFrom2.Size = new System.Drawing.Size(233, 20);
            this.txtEmailFrom2.TabIndex = 88;
            this.txtEmailFrom2.Text = "support@direct.amrpatientportal.com";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(485, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 87;
            this.label7.Text = "From Email";
            // 
            // txtEmailTo2
            // 
            this.txtEmailTo2.Location = new System.Drawing.Point(547, 37);
            this.txtEmailTo2.Name = "txtEmailTo2";
            this.txtEmailTo2.Size = new System.Drawing.Size(233, 20);
            this.txtEmailTo2.TabIndex = 86;
            this.txtEmailTo2.Text = "support@direct.amrpatientportal.com";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(495, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 85;
            this.label8.Text = "To Email";
            // 
            // txtEmailPatient2
            // 
            this.txtEmailPatient2.Location = new System.Drawing.Point(547, 63);
            this.txtEmailPatient2.Name = "txtEmailPatient2";
            this.txtEmailPatient2.Size = new System.Drawing.Size(233, 20);
            this.txtEmailPatient2.TabIndex = 90;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(475, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 13);
            this.label9.TabIndex = 89;
            this.label9.Text = "Patient Email";
            // 
            // txtFullName2
            // 
            this.txtFullName2.Enabled = false;
            this.txtFullName2.Location = new System.Drawing.Point(548, 149);
            this.txtFullName2.Name = "txtFullName2";
            this.txtFullName2.Size = new System.Drawing.Size(232, 20);
            this.txtFullName2.TabIndex = 95;
            this.txtFullName2.Visible = false;
            // 
            // cmdOpenFile2
            // 
            this.cmdOpenFile2.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFile2.ForeColor = System.Drawing.Color.Navy;
            this.cmdOpenFile2.Location = new System.Drawing.Point(786, 121);
            this.cmdOpenFile2.Name = "cmdOpenFile2";
            this.cmdOpenFile2.Size = new System.Drawing.Size(29, 23);
            this.cmdOpenFile2.TabIndex = 94;
            this.cmdOpenFile2.Text = "...";
            this.cmdOpenFile2.UseVisualStyleBackColor = true;
            this.cmdOpenFile2.Click += new System.EventHandler(this.cmdOpenFile2_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(488, 126);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 93;
            this.label10.Text = "CDA File";
            // 
            // txtFileName2
            // 
            this.txtFileName2.Enabled = false;
            this.txtFileName2.Location = new System.Drawing.Point(548, 123);
            this.txtFileName2.Name = "txtFileName2";
            this.txtFileName2.Size = new System.Drawing.Size(232, 20);
            this.txtFileName2.TabIndex = 92;
            // 
            // cmdProcess2
            // 
            this.cmdProcess2.Location = new System.Drawing.Point(622, 175);
            this.cmdProcess2.Name = "cmdProcess2";
            this.cmdProcess2.Size = new System.Drawing.Size(75, 23);
            this.cmdProcess2.TabIndex = 91;
            this.cmdProcess2.Text = "Process";
            this.cmdProcess2.UseVisualStyleBackColor = true;
            this.cmdProcess2.Click += new System.EventHandler(this.cmdProcess2_Click);
            // 
            // txtPatientName2
            // 
            this.txtPatientName2.Location = new System.Drawing.Point(548, 89);
            this.txtPatientName2.Name = "txtPatientName2";
            this.txtPatientName2.Size = new System.Drawing.Size(233, 20);
            this.txtPatientName2.TabIndex = 97;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(476, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 96;
            this.label11.Text = "Patient Name";
            // 
            // frmISSendCDA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 223);
            this.Controls.Add(this.txtPatientName2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtFullName2);
            this.Controls.Add(this.cmdOpenFile2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtFileName2);
            this.Controls.Add(this.cmdProcess2);
            this.Controls.Add(this.txtEmailPatient2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtEmailFrom2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmailTo2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmailFrom);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudProviderId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudFacilityId);
            this.Controls.Add(this.txtEmailTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.cmdOpenFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdProcess);
            this.Name = "frmISSendCDA";
            this.Text = "Send CDA";
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudProviderId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdProcess;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtEmailTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudFacilityId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudProviderId;
        private System.Windows.Forms.TextBox txtEmailFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmailFrom2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmailTo2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtEmailPatient2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFullName2;
        private System.Windows.Forms.Button cmdOpenFile2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFileName2;
        private System.Windows.Forms.Button cmdProcess2;
        private System.Windows.Forms.TextBox txtPatientName2;
        private System.Windows.Forms.Label label11;
    }
}