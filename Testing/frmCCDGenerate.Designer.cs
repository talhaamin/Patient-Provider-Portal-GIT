namespace Testing
{
    partial class frmCCDGenerate
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudVisitId = new System.Windows.Forms.NumericUpDown();
            this.cmdSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFacilityId = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Visit Id";
            // 
            // nudVisitId
            // 
            this.nudVisitId.Location = new System.Drawing.Point(107, 59);
            this.nudVisitId.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudVisitId.Name = "nudVisitId";
            this.nudVisitId.Size = new System.Drawing.Size(75, 20);
            this.nudVisitId.TabIndex = 30;
            this.nudVisitId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(64, 111);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 28;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(107, 12);
            this.nudPatientId.Maximum = new decimal(new int[] {
            50099,
            0,
            0,
            0});
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 27;
            this.nudPatientId.Value = new decimal(new int[] {
            101,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Facility Id";
            // 
            // nudFacilityId
            // 
            this.nudFacilityId.Location = new System.Drawing.Point(107, 35);
            this.nudFacilityId.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudFacilityId.Name = "nudFacilityId";
            this.nudFacilityId.Size = new System.Drawing.Size(75, 20);
            this.nudFacilityId.TabIndex = 32;
            this.nudFacilityId.Value = new decimal(new int[] {
            130,
            0,
            0,
            0});
            // 
            // frmCCDGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 174);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudFacilityId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudVisitId);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Name = "frmCCDGenerate";
            this.Text = "frmCCDGenerate";
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFacilityId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudVisitId;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFacilityId;
    }
}