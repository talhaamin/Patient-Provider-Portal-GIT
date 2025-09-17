namespace Testing
{
    partial class frmDocument
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
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nudVisitId = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.cmdSave = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dgCodes = new System.Windows.Forms.DataGridView();
            this.cmdRead = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.nudDocId = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optMedical = new System.Windows.Forms.RadioButton();
            this.optGeneral = new System.Windows.Forms.RadioButton();
            this.optCareProvider = new System.Windows.Forms.RadioButton();
            this.cmdClear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFacility = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDoctor = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(115, 302);
            this.txtNotes.MaxLength = 100;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(764, 20);
            this.txtNotes.TabIndex = 16;
            // 
            // txtType
            // 
            this.txtType.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtType.Location = new System.Drawing.Point(630, 276);
            this.txtType.Name = "txtType";
            this.txtType.ReadOnly = true;
            this.txtType.Size = new System.Drawing.Size(92, 20);
            this.txtType.TabIndex = 14;
            // 
            // txtFullName
            // 
            this.txtFullName.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFullName.Location = new System.Drawing.Point(385, 276);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(213, 20);
            this.txtFullName.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(30, 276);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Document";
            // 
            // txtFileName
            // 
            this.txtFileName.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileName.Location = new System.Drawing.Point(115, 272);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(213, 20);
            this.txtFileName.TabIndex = 11;
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Font = new System.Drawing.Font("Franklin Gothic Medium", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOpenFile.ForeColor = System.Drawing.Color.Navy;
            this.cmdOpenFile.Location = new System.Drawing.Point(334, 272);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(29, 23);
            this.cmdOpenFile.TabIndex = 12;
            this.cmdOpenFile.Text = "...";
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(30, 249);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(118, 246);
            this.txtDescription.MaxLength = 100;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(764, 20);
            this.txtDescription.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Visit Id";
            // 
            // nudVisitId
            // 
            this.nudVisitId.Location = new System.Drawing.Point(126, 38);
            this.nudVisitId.Name = "nudVisitId";
            this.nudVisitId.Size = new System.Drawing.Size(75, 20);
            this.nudVisitId.TabIndex = 4;
            this.nudVisitId.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(126, 10);
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 1;
            this.nudPatientId.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(145, 402);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 21;
            this.cmdSave.Text = "Send";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            this.dgCodes.Location = new System.Drawing.Point(126, 74);
            this.dgCodes.Name = "dgCodes";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCodes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgCodes.Size = new System.Drawing.Size(756, 149);
            this.dgCodes.TabIndex = 7;
            this.dgCodes.Click += new System.EventHandler(this.dgCodes_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(235, 12);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 2;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Location = new System.Drawing.Point(278, 402);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 23);
            this.cmdDelete.TabIndex = 22;
            this.cmdDelete.Text = "Delete";
            this.cmdDelete.UseVisualStyleBackColor = true;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // nudDocId
            // 
            this.nudDocId.Enabled = false;
            this.nudDocId.Location = new System.Drawing.Point(334, 38);
            this.nudDocId.Name = "nudDocId";
            this.nudDocId.Size = new System.Drawing.Size(75, 20);
            this.nudDocId.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(569, 339);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(119, 116);
            this.pictureBox1.TabIndex = 107;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optMedical);
            this.groupBox1.Controls.Add(this.optGeneral);
            this.groupBox1.Controls.Add(this.optCareProvider);
            this.groupBox1.Location = new System.Drawing.Point(488, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 45);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // optMedical
            // 
            this.optMedical.AutoSize = true;
            this.optMedical.Location = new System.Drawing.Point(274, 19);
            this.optMedical.Name = "optMedical";
            this.optMedical.Size = new System.Drawing.Size(114, 17);
            this.optMedical.TabIndex = 2;
            this.optMedical.Text = "Medical Document";
            this.optMedical.UseVisualStyleBackColor = true;
            this.optMedical.CheckedChanged += new System.EventHandler(this.optMedical_CheckedChanged);
            // 
            // optGeneral
            // 
            this.optGeneral.AutoSize = true;
            this.optGeneral.Location = new System.Drawing.Point(154, 19);
            this.optGeneral.Name = "optGeneral";
            this.optGeneral.Size = new System.Drawing.Size(114, 17);
            this.optGeneral.TabIndex = 1;
            this.optGeneral.Text = "General Document";
            this.optGeneral.UseVisualStyleBackColor = true;
            this.optGeneral.CheckedChanged += new System.EventHandler(this.optGeneral_CheckedChanged);
            // 
            // optCareProvider
            // 
            this.optCareProvider.AutoSize = true;
            this.optCareProvider.Checked = true;
            this.optCareProvider.Location = new System.Drawing.Point(7, 20);
            this.optCareProvider.Name = "optCareProvider";
            this.optCareProvider.Size = new System.Drawing.Size(141, 17);
            this.optCareProvider.TabIndex = 0;
            this.optCareProvider.TabStop = true;
            this.optCareProvider.Text = "Care Provider Document";
            this.optCareProvider.UseVisualStyleBackColor = true;
            this.optCareProvider.CheckedChanged += new System.EventHandler(this.optCareProvider_CheckedChanged);
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(404, 402);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(75, 23);
            this.cmdClear.TabIndex = 23;
            this.cmdClear.Text = "Clear";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 331);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Facility";
            // 
            // txtFacility
            // 
            this.txtFacility.Enabled = false;
            this.txtFacility.Location = new System.Drawing.Point(115, 328);
            this.txtFacility.MaxLength = 50;
            this.txtFacility.Name = "txtFacility";
            this.txtFacility.Size = new System.Drawing.Size(418, 20);
            this.txtFacility.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Doctor";
            // 
            // txtDoctor
            // 
            this.txtDoctor.Location = new System.Drawing.Point(115, 354);
            this.txtDoctor.MaxLength = 50;
            this.txtDoctor.Name = "txtDoctor";
            this.txtDoctor.Size = new System.Drawing.Size(418, 20);
            this.txtDoctor.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(743, 402);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 108;
            this.button1.Text = "Get All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 455);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDoctor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFacility);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.nudDocId);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.dgCodes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.cmdOpenFile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudVisitId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.cmdSave);
            this.Name = "frmDocument";
            this.Text = "frmDocument";
            ((System.ComponentModel.ISupportInitialize)(this.nudVisitId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDocId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudVisitId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dgCodes;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.NumericUpDown nudDocId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton optGeneral;
        private System.Windows.Forms.RadioButton optCareProvider;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.RadioButton optMedical;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFacility;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDoctor;
        private System.Windows.Forms.Button button1;
    }
}