namespace Testing
{
    partial class frmSocialSelf
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtCaffieneType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBirthplace = new System.Windows.Forms.TextBox();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.chkRetired = new System.Windows.Forms.CheckBox();
            this.nudSons = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.nudDaughters = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDaughters)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Caffiene Type";
            // 
            // txtCaffieneType
            // 
            this.txtCaffieneType.Location = new System.Drawing.Point(100, 193);
            this.txtCaffieneType.MaxLength = 50;
            this.txtCaffieneType.Name = "txtCaffieneType";
            this.txtCaffieneType.Size = new System.Drawing.Size(194, 20);
            this.txtCaffieneType.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Occupation";
            // 
            // txtOccupation
            // 
            this.txtOccupation.Location = new System.Drawing.Point(100, 65);
            this.txtOccupation.MaxLength = 20;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(194, 20);
            this.txtOccupation.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Birthplace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Patient Id";
            // 
            // txtBirthplace
            // 
            this.txtBirthplace.Location = new System.Drawing.Point(100, 39);
            this.txtBirthplace.MaxLength = 20;
            this.txtBirthplace.Name = "txtBirthplace";
            this.txtBirthplace.Size = new System.Drawing.Size(194, 20);
            this.txtBirthplace.TabIndex = 31;
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(100, 13);
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 29;
            // 
            // chkRetired
            // 
            this.chkRetired.AutoSize = true;
            this.chkRetired.Location = new System.Drawing.Point(100, 91);
            this.chkRetired.Name = "chkRetired";
            this.chkRetired.Size = new System.Drawing.Size(60, 17);
            this.chkRetired.TabIndex = 28;
            this.chkRetired.Text = "Retired";
            this.chkRetired.UseVisualStyleBackColor = true;
            // 
            // nudSons
            // 
            this.nudSons.Location = new System.Drawing.Point(100, 119);
            this.nudSons.Name = "nudSons";
            this.nudSons.Size = new System.Drawing.Size(75, 20);
            this.nudSons.TabIndex = 39;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 38;
            this.label7.Text = "Sons";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(132, 381);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 41;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(51, 381);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 40;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // nudDaughters
            // 
            this.nudDaughters.Location = new System.Drawing.Point(100, 145);
            this.nudDaughters.Name = "nudDaughters";
            this.nudDaughters.Size = new System.Drawing.Size(75, 20);
            this.nudDaughters.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Daughters";
            // 
            // frmSocialSelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 416);
            this.Controls.Add(this.nudDaughters);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCaffieneType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOccupation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBirthplace);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.chkRetired);
            this.Controls.Add(this.nudSons);
            this.Controls.Add(this.label7);
            this.Name = "frmSocialSelf";
            this.Text = "frmSocialSelf";
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDaughters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCaffieneType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBirthplace;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.CheckBox chkRetired;
        private System.Windows.Forms.NumericUpDown nudSons;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.NumericUpDown nudDaughters;
        private System.Windows.Forms.Label label6;
    }
}