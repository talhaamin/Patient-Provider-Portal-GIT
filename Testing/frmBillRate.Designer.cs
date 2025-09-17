namespace Testing
{
    partial class frmBillRate
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
            this.label8 = new System.Windows.Forms.Label();
            this.nudBillRateId = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPromoCode = new System.Windows.Forms.TextBox();
            this.chkIsFree = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.nudRate = new System.Windows.Forms.NumericUpDown();
            this.nudFreeMonth = new System.Windows.Forms.NumericUpDown();
            this.nudRenewal = new System.Windows.Forms.NumericUpDown();
            this.nudResidual = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudBillRateId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreeMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRenewal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResidual)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Bill Rate Id";
            // 
            // nudBillRateId
            // 
            this.nudBillRateId.Location = new System.Drawing.Point(82, 14);
            this.nudBillRateId.Name = "nudBillRateId";
            this.nudBillRateId.Size = new System.Drawing.Size(75, 20);
            this.nudBillRateId.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Free Months";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Residual";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Renewal";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Rate";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(127, 235);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 15;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(46, 235);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 14;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Promo Code";
            // 
            // txtPromoCode
            // 
            this.txtPromoCode.Location = new System.Drawing.Point(82, 40);
            this.txtPromoCode.MaxLength = 20;
            this.txtPromoCode.Name = "txtPromoCode";
            this.txtPromoCode.Size = new System.Drawing.Size(194, 20);
            this.txtPromoCode.TabIndex = 3;
            // 
            // chkIsFree
            // 
            this.chkIsFree.AutoSize = true;
            this.chkIsFree.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsFree.Location = new System.Drawing.Point(35, 89);
            this.chkIsFree.Name = "chkIsFree";
            this.chkIsFree.Size = new System.Drawing.Size(58, 17);
            this.chkIsFree.TabIndex = 5;
            this.chkIsFree.Text = "Is Free";
            this.chkIsFree.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkActive.Location = new System.Drawing.Point(37, 66);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // nudRate
            // 
            this.nudRate.DecimalPlaces = 2;
            this.nudRate.Location = new System.Drawing.Point(80, 113);
            this.nudRate.Name = "nudRate";
            this.nudRate.Size = new System.Drawing.Size(75, 20);
            this.nudRate.TabIndex = 7;
            // 
            // nudFreeMonth
            // 
            this.nudFreeMonth.Location = new System.Drawing.Point(80, 191);
            this.nudFreeMonth.Name = "nudFreeMonth";
            this.nudFreeMonth.Size = new System.Drawing.Size(75, 20);
            this.nudFreeMonth.TabIndex = 13;
            // 
            // nudRenewal
            // 
            this.nudRenewal.DecimalPlaces = 2;
            this.nudRenewal.Location = new System.Drawing.Point(80, 139);
            this.nudRenewal.Name = "nudRenewal";
            this.nudRenewal.Size = new System.Drawing.Size(75, 20);
            this.nudRenewal.TabIndex = 9;
            // 
            // nudResidual
            // 
            this.nudResidual.DecimalPlaces = 2;
            this.nudResidual.Location = new System.Drawing.Point(80, 165);
            this.nudResidual.Name = "nudResidual";
            this.nudResidual.Size = new System.Drawing.Size(75, 20);
            this.nudResidual.TabIndex = 11;
            // 
            // frmBillRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.nudResidual);
            this.Controls.Add(this.nudRenewal);
            this.Controls.Add(this.nudFreeMonth);
            this.Controls.Add(this.nudRate);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.chkIsFree);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudBillRateId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPromoCode);
            this.Name = "frmBillRate";
            this.Text = "frmBillRate";
            ((System.ComponentModel.ISupportInitialize)(this.nudBillRateId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreeMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRenewal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudResidual)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudBillRateId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPromoCode;
        private System.Windows.Forms.CheckBox chkIsFree;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.NumericUpDown nudRate;
        private System.Windows.Forms.NumericUpDown nudFreeMonth;
        private System.Windows.Forms.NumericUpDown nudRenewal;
        private System.Windows.Forms.NumericUpDown nudResidual;
    }
}