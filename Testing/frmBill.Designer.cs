namespace Testing
{
    partial class frmBill
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
            this.cmdRead = new System.Windows.Forms.Button();
            this.nudRate = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudBillPmtId = new System.Windows.Forms.NumericUpDown();
            this.Amount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudBillRateId = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBillPmtId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBillRateId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(127, 235);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 73;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(46, 235);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 72;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // nudRate
            // 
            this.nudRate.DecimalPlaces = 2;
            this.nudRate.Location = new System.Drawing.Point(78, 146);
            this.nudRate.Name = "nudRate";
            this.nudRate.Size = new System.Drawing.Size(75, 20);
            this.nudRate.TabIndex = 81;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 74;
            this.label8.Text = "Bill Pmt Id";
            // 
            // nudBillPmtId
            // 
            this.nudBillPmtId.Location = new System.Drawing.Point(80, 15);
            this.nudBillPmtId.Name = "nudBillPmtId";
            this.nudBillPmtId.Size = new System.Drawing.Size(75, 20);
            this.nudBillPmtId.TabIndex = 75;
            // 
            // Amount
            // 
            this.Amount.AutoSize = true;
            this.Amount.Location = new System.Drawing.Point(14, 148);
            this.Amount.Name = "Amount";
            this.Amount.Size = new System.Drawing.Size(30, 13);
            this.Amount.TabIndex = 80;
            this.Amount.Text = "Rate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 76;
            this.label4.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(78, 120);
            this.txtName.MaxLength = 20;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(194, 20);
            this.txtName.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 88;
            this.label1.Text = "Bill Rate Id";
            // 
            // nudBillRateId
            // 
            this.nudBillRateId.Location = new System.Drawing.Point(80, 58);
            this.nudBillRateId.Name = "nudBillRateId";
            this.nudBillRateId.Size = new System.Drawing.Size(75, 20);
            this.nudBillRateId.TabIndex = 89;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 90;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(80, 36);
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 91;
            // 
            // frmBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudBillRateId);
            this.Controls.Add(this.nudRate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudBillPmtId);
            this.Controls.Add(this.Amount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdRead);
            this.Name = "frmBill";
            this.Text = "frmBill";
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBillPmtId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBillRateId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.NumericUpDown nudRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudBillPmtId;
        private System.Windows.Forms.Label Amount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudBillRateId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
    }
}