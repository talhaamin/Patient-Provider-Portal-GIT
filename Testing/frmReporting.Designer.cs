namespace Testing
{
    partial class frmReporting
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
            this.label3 = new System.Windows.Forms.Label();
            this.nudFacilityId = new System.Windows.Forms.NumericUpDown();
            this.dtpSynchDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdSecureMessage = new System.Windows.Forms.Button();
            this.cmdPatientLogin = new System.Windows.Forms.Button();
            this.dgMessages = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 70;
            this.label3.Text = "Facility Id";
            // 
            // nudFacilityId
            // 
            this.nudFacilityId.Location = new System.Drawing.Point(119, 12);
            this.nudFacilityId.Name = "nudFacilityId";
            this.nudFacilityId.Size = new System.Drawing.Size(75, 20);
            this.nudFacilityId.TabIndex = 69;
            this.nudFacilityId.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // dtpSynchDate
            // 
            this.dtpSynchDate.CalendarForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.dtpSynchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSynchDate.Location = new System.Drawing.Point(119, 44);
            this.dtpSynchDate.Name = "dtpSynchDate";
            this.dtpSynchDate.Size = new System.Drawing.Size(91, 20);
            this.dtpSynchDate.TabIndex = 72;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 71;
            this.label5.Text = "Last Synch Date";
            // 
            // cmdSecureMessage
            // 
            this.cmdSecureMessage.Location = new System.Drawing.Point(33, 80);
            this.cmdSecureMessage.Name = "cmdSecureMessage";
            this.cmdSecureMessage.Size = new System.Drawing.Size(128, 23);
            this.cmdSecureMessage.TabIndex = 73;
            this.cmdSecureMessage.Text = "Secure Messages";
            this.cmdSecureMessage.UseVisualStyleBackColor = true;
            this.cmdSecureMessage.Click += new System.EventHandler(this.cmdSecureMessage_Click);
            // 
            // cmdPatientLogin
            // 
            this.cmdPatientLogin.Location = new System.Drawing.Point(176, 80);
            this.cmdPatientLogin.Name = "cmdPatientLogin";
            this.cmdPatientLogin.Size = new System.Drawing.Size(128, 23);
            this.cmdPatientLogin.TabIndex = 74;
            this.cmdPatientLogin.Text = "Patient Login";
            this.cmdPatientLogin.UseVisualStyleBackColor = true;
            this.cmdPatientLogin.Click += new System.EventHandler(this.cmdPatientLogin_Click);
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
            this.dgMessages.Location = new System.Drawing.Point(19, 110);
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
            this.dgMessages.TabIndex = 75;
            // 
            // frmReporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 317);
            this.Controls.Add(this.dgMessages);
            this.Controls.Add(this.cmdPatientLogin);
            this.Controls.Add(this.cmdSecureMessage);
            this.Controls.Add(this.dtpSynchDate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudFacilityId);
            this.Name = "frmReporting";
            this.Text = "frmReporting";
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFacilityId;
        private System.Windows.Forms.DateTimePicker dtpSynchDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSecureMessage;
        private System.Windows.Forms.Button cmdPatientLogin;
        private System.Windows.Forms.DataGridView dgMessages;
    }
}