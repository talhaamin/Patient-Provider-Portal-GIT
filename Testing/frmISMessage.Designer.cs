namespace Testing
{
    partial class frmISMessage
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
            this.label10 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRequest = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudProviderId = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cboMessageType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.dgMessages = new System.Windows.Forms.DataGridView();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudMessageId = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudApptProv = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.nudReceived1 = new System.Windows.Forms.NumericUpDown();
            this.nudReceived2 = new System.Windows.Forms.NumericUpDown();
            this.nudReceived3 = new System.Windows.Forms.NumericUpDown();
            this.nudReceived4 = new System.Windows.Forms.NumericUpDown();
            this.cmdReceived = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cmdCreate = new System.Windows.Forms.Button();
            this.txtPatientIds = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudProviderId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMessageId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudApptProv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived4)).BeginInit();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 505);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 60;
            this.label10.Text = "Reason";
            // 
            // txtReason
            // 
            this.txtReason.Enabled = false;
            this.txtReason.Location = new System.Drawing.Point(91, 502);
            this.txtReason.MaxLength = 100;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(764, 20);
            this.txtReason.TabIndex = 59;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 467);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 58;
            this.label9.Text = "Response";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(91, 464);
            this.txtResponse.MaxLength = 256;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(764, 20);
            this.txtResponse.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 441);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 56;
            this.label8.Text = "Request";
            // 
            // txtRequest
            // 
            this.txtRequest.Location = new System.Drawing.Point(91, 438);
            this.txtRequest.MaxLength = 256;
            this.txtRequest.Name = "txtRequest";
            this.txtRequest.Size = new System.Drawing.Size(764, 20);
            this.txtRequest.TabIndex = 55;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 53;
            this.label7.Text = "Provider Id";
            // 
            // nudProviderId
            // 
            this.nudProviderId.Location = new System.Drawing.Point(112, 293);
            this.nudProviderId.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudProviderId.Name = "nudProviderId";
            this.nudProviderId.Size = new System.Drawing.Size(75, 20);
            this.nudProviderId.TabIndex = 54;
            this.nudProviderId.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 329);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Messsage Type";
            // 
            // cboMessageType
            // 
            this.cboMessageType.AutoCompleteCustomSource.AddRange(new string[] {
            "Allergen",
            "Country",
            "EquipmentType",
            "Ethnicity",
            "FacilityType",
            "Gender",
            "MaritalStatus",
            "PreferredLanguage",
            "Race"});
            this.cboMessageType.Enabled = false;
            this.cboMessageType.FormattingEnabled = true;
            this.cboMessageType.Items.AddRange(new object[] {
            "Appointment Request",
            "Medication Refill",
            "Billing Message",
            "General Inquiry",
            "Referral Message"});
            this.cboMessageType.Location = new System.Drawing.Point(112, 329);
            this.cboMessageType.Name = "cboMessageType";
            this.cboMessageType.Size = new System.Drawing.Size(210, 21);
            this.cboMessageType.TabIndex = 48;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Messages";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 39;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(112, 267);
            this.nudPatientId.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 40;
            // 
            // dgMessages
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMessages.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgMessages.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgMessages.Location = new System.Drawing.Point(12, 46);
            this.dgMessages.Name = "dgMessages";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgMessages.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgMessages.Size = new System.Drawing.Size(591, 194);
            this.dgMessages.TabIndex = 41;
            this.dgMessages.Click += new System.EventHandler(this.dgMessages_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(12, 569);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 43;
            this.cmdSave.Text = "Send";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(12, 12);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 42;
            this.cmdLoad.Text = "Load";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 61;
            this.label3.Text = "MessgeId";
            // 
            // nudMessageId
            // 
            this.nudMessageId.Location = new System.Drawing.Point(112, 241);
            this.nudMessageId.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudMessageId.Name = "nudMessageId";
            this.nudMessageId.Size = new System.Drawing.Size(75, 20);
            this.nudMessageId.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Appt Start";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 402);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 64;
            this.label6.Text = "Appt. Provider Id";
            // 
            // nudApptProv
            // 
            this.nudApptProv.Location = new System.Drawing.Point(112, 400);
            this.nudApptProv.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudApptProv.Name = "nudApptProv";
            this.nudApptProv.Size = new System.Drawing.Size(75, 20);
            this.nudApptProv.TabIndex = 65;
            this.nudApptProv.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 13);
            this.label11.TabIndex = 63;
            this.label11.Text = "Appt End";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.CalendarForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(112, 357);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(91, 20);
            this.dtpStartDate.TabIndex = 67;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.CalendarForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpStartTime.Location = new System.Drawing.Point(209, 357);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(91, 20);
            this.dtpStartTime.TabIndex = 68;
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.CalendarForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpEndTime.Location = new System.Drawing.Point(209, 379);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(91, 20);
            this.dtpEndTime.TabIndex = 70;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CalendarForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(112, 379);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(91, 20);
            this.dtpEndDate.TabIndex = 69;
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(91, 537);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(213, 20);
            this.txtFileName.TabIndex = 72;
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFile.ForeColor = System.Drawing.Color.Navy;
            this.cmdOpenFile.Location = new System.Drawing.Point(310, 537);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(29, 23);
            this.cmdOpenFile.TabIndex = 71;
            this.cmdOpenFile.Text = "...";
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(24, 541);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 13);
            this.label12.TabIndex = 73;
            this.label12.Text = "Attachment";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(549, 534);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(213, 20);
            this.txtFullName.TabIndex = 74;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(128, 569);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 75;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(631, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(116, 13);
            this.label13.TabIndex = 76;
            this.label13.Text = "Received Message Ids";
            // 
            // nudReceived1
            // 
            this.nudReceived1.Location = new System.Drawing.Point(659, 69);
            this.nudReceived1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudReceived1.Name = "nudReceived1";
            this.nudReceived1.Size = new System.Drawing.Size(75, 20);
            this.nudReceived1.TabIndex = 77;
            // 
            // nudReceived2
            // 
            this.nudReceived2.Location = new System.Drawing.Point(659, 95);
            this.nudReceived2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudReceived2.Name = "nudReceived2";
            this.nudReceived2.Size = new System.Drawing.Size(75, 20);
            this.nudReceived2.TabIndex = 78;
            // 
            // nudReceived3
            // 
            this.nudReceived3.Location = new System.Drawing.Point(659, 121);
            this.nudReceived3.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudReceived3.Name = "nudReceived3";
            this.nudReceived3.Size = new System.Drawing.Size(75, 20);
            this.nudReceived3.TabIndex = 79;
            // 
            // nudReceived4
            // 
            this.nudReceived4.Location = new System.Drawing.Point(659, 147);
            this.nudReceived4.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudReceived4.Name = "nudReceived4";
            this.nudReceived4.Size = new System.Drawing.Size(75, 20);
            this.nudReceived4.TabIndex = 80;
            // 
            // cmdReceived
            // 
            this.cmdReceived.Location = new System.Drawing.Point(659, 185);
            this.cmdReceived.Name = "cmdReceived";
            this.cmdReceived.Size = new System.Drawing.Size(75, 23);
            this.cmdReceived.TabIndex = 81;
            this.cmdReceived.Text = "Received";
            this.cmdReceived.UseVisualStyleBackColor = true;
            this.cmdReceived.Click += new System.EventHandler(this.cmdReceived_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(112, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 82;
            this.button2.Text = "Cancelled";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmdCreate
            // 
            this.cmdCreate.Location = new System.Drawing.Point(264, 566);
            this.cmdCreate.Name = "cmdCreate";
            this.cmdCreate.Size = new System.Drawing.Size(75, 23);
            this.cmdCreate.TabIndex = 83;
            this.cmdCreate.Text = "Create";
            this.cmdCreate.UseVisualStyleBackColor = true;
            this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
            // 
            // txtPatientIds
            // 
            this.txtPatientIds.Location = new System.Drawing.Point(209, 267);
            this.txtPatientIds.MaxLength = 256;
            this.txtPatientIds.Name = "txtPatientIds";
            this.txtPatientIds.Size = new System.Drawing.Size(277, 20);
            this.txtPatientIds.TabIndex = 84;
            // 
            // frmISMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(855, 708);
            this.Controls.Add(this.txtPatientIds);
            this.Controls.Add(this.cmdCreate);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.cmdReceived);
            this.Controls.Add(this.nudReceived4);
            this.Controls.Add(this.nudReceived3);
            this.Controls.Add(this.nudReceived2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.nudReceived1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdOpenFile);
            this.Controls.Add(this.dtpEndTime);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.dtpStartTime);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.nudApptProv);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudMessageId);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRequest);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudProviderId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cboMessageType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.dgMessages);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdLoad);
            this.Name = "frmISMessage";
            this.Text = "frmISMessage";
            ((System.ComponentModel.ISupportInitialize)(this.nudProviderId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMessageId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudApptProv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceived4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRequest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudProviderId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboMessageType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.DataGridView dgMessages;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudMessageId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudApptProv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudReceived1;
        private System.Windows.Forms.NumericUpDown nudReceived2;
        private System.Windows.Forms.NumericUpDown nudReceived3;
        private System.Windows.Forms.NumericUpDown nudReceived4;
        private System.Windows.Forms.Button cmdReceived;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button cmdCreate;
        private System.Windows.Forms.TextBox txtPatientIds;
    }
}