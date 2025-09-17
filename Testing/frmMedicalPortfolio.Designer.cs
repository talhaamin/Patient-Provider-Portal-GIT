namespace Testing
{
    partial class frmMedicalPortfolio
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgVisits = new System.Windows.Forms.DataGridView();
            this.dgDocs = new System.Windows.Forms.DataGridView();
            this.dgPatients = new System.Windows.Forms.DataGridView();
            this.cmdRead = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPatientId = new System.Windows.Forms.NumericUpDown();
            this.cmdVisitShare = new System.Windows.Forms.Button();
            this.cmdDocShare = new System.Windows.Forms.Button();
            this.cmdPatientShare = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgVisits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDocs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).BeginInit();
            this.SuspendLayout();
            // 
            // dgVisits
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVisits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgVisits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgVisits.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgVisits.Location = new System.Drawing.Point(25, 31);
            this.dgVisits.Name = "dgVisits";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgVisits.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgVisits.Size = new System.Drawing.Size(756, 149);
            this.dgVisits.TabIndex = 8;
            // 
            // dgDocs
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDocs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgDocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDocs.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgDocs.Location = new System.Drawing.Point(25, 225);
            this.dgDocs.Name = "dgDocs";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDocs.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgDocs.Size = new System.Drawing.Size(756, 149);
            this.dgDocs.TabIndex = 9;
            // 
            // dgPatients
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPatients.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgPatients.Location = new System.Drawing.Point(25, 417);
            this.dgPatients.Name = "dgPatients";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPatients.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgPatients.Size = new System.Drawing.Size(756, 149);
            this.dgPatients.TabIndex = 10;
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(226, 2);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(75, 23);
            this.cmdRead.TabIndex = 13;
            this.cmdRead.Text = "Read";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Patient Id";
            // 
            // nudPatientId
            // 
            this.nudPatientId.Location = new System.Drawing.Point(117, 0);
            this.nudPatientId.Name = "nudPatientId";
            this.nudPatientId.Size = new System.Drawing.Size(75, 20);
            this.nudPatientId.TabIndex = 12;
            this.nudPatientId.Value = new decimal(new int[] {
            11,
            0,
            0,
            0});
            // 
            // cmdVisitShare
            // 
            this.cmdVisitShare.Location = new System.Drawing.Point(117, 186);
            this.cmdVisitShare.Name = "cmdVisitShare";
            this.cmdVisitShare.Size = new System.Drawing.Size(75, 23);
            this.cmdVisitShare.TabIndex = 14;
            this.cmdVisitShare.Text = "Share";
            this.cmdVisitShare.UseVisualStyleBackColor = true;
            this.cmdVisitShare.Click += new System.EventHandler(this.cmdVisitShare_Click);
            // 
            // cmdDocShare
            // 
            this.cmdDocShare.Location = new System.Drawing.Point(117, 388);
            this.cmdDocShare.Name = "cmdDocShare";
            this.cmdDocShare.Size = new System.Drawing.Size(75, 23);
            this.cmdDocShare.TabIndex = 15;
            this.cmdDocShare.Text = "Share";
            this.cmdDocShare.UseVisualStyleBackColor = true;
            this.cmdDocShare.Click += new System.EventHandler(this.cmdDocShare_Click);
            // 
            // cmdPatientShare
            // 
            this.cmdPatientShare.Location = new System.Drawing.Point(117, 572);
            this.cmdPatientShare.Name = "cmdPatientShare";
            this.cmdPatientShare.Size = new System.Drawing.Size(75, 23);
            this.cmdPatientShare.TabIndex = 16;
            this.cmdPatientShare.Text = "Share";
            this.cmdPatientShare.UseVisualStyleBackColor = true;
            this.cmdPatientShare.Click += new System.EventHandler(this.cmdPatientShare_Click);
            // 
            // frmMedicalPortfolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 626);
            this.Controls.Add(this.cmdPatientShare);
            this.Controls.Add(this.cmdDocShare);
            this.Controls.Add(this.cmdVisitShare);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudPatientId);
            this.Controls.Add(this.dgPatients);
            this.Controls.Add(this.dgDocs);
            this.Controls.Add(this.dgVisits);
            this.Name = "frmMedicalPortfolio";
            this.Text = "frmMedicalPortfolio";
            ((System.ComponentModel.ISupportInitialize)(this.dgVisits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDocs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPatients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPatientId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgVisits;
        private System.Windows.Forms.DataGridView dgDocs;
        private System.Windows.Forms.DataGridView dgPatients;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPatientId;
        private System.Windows.Forms.Button cmdVisitShare;
        private System.Windows.Forms.Button cmdDocShare;
        private System.Windows.Forms.Button cmdPatientShare;
    }
}