namespace AlphaSoft
{
    partial class viewTechnicianJobForm
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
            this.technicianDataGrid = new System.Windows.Forms.DataGridView();
            this.jobDetailDataGrid = new System.Windows.Forms.DataGridView();
            this.jobStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.technicianNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.technicianDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDetailDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // technicianDataGrid
            // 
            this.technicianDataGrid.AllowUserToAddRows = false;
            this.technicianDataGrid.AllowUserToDeleteRows = false;
            this.technicianDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.technicianDataGrid.Location = new System.Drawing.Point(12, 91);
            this.technicianDataGrid.Name = "technicianDataGrid";
            this.technicianDataGrid.ReadOnly = true;
            this.technicianDataGrid.RowHeadersVisible = false;
            this.technicianDataGrid.Size = new System.Drawing.Size(240, 516);
            this.technicianDataGrid.TabIndex = 0;
            this.technicianDataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.technicianDataGrid_CellEnter);
            this.technicianDataGrid.DoubleClick += new System.EventHandler(this.technicianDataGrid_DoubleClick);
            this.technicianDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.technicianDataGrid_KeyDown);
            // 
            // jobDetailDataGrid
            // 
            this.jobDetailDataGrid.AllowUserToAddRows = false;
            this.jobDetailDataGrid.AllowUserToDeleteRows = false;
            this.jobDetailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.jobDetailDataGrid.Location = new System.Drawing.Point(272, 91);
            this.jobDetailDataGrid.Name = "jobDetailDataGrid";
            this.jobDetailDataGrid.ReadOnly = true;
            this.jobDetailDataGrid.RowHeadersVisible = false;
            this.jobDetailDataGrid.Size = new System.Drawing.Size(692, 515);
            this.jobDetailDataGrid.TabIndex = 1;
            // 
            // jobStartDateTimePicker
            // 
            this.jobStartDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobStartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.jobStartDateTimePicker.Location = new System.Drawing.Point(188, 49);
            this.jobStartDateTimePicker.Name = "jobStartDateTimePicker";
            this.jobStartDateTimePicker.Size = new System.Drawing.Size(152, 27);
            this.jobStartDateTimePicker.TabIndex = 44;
            this.jobStartDateTimePicker.ValueChanged += new System.EventHandler(this.jobStartDateTimePicker_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FloralWhite;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(9, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 18);
            this.label7.TabIndex = 43;
            this.label7.Text = "Tanggal Tugas      : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.White;
            this.errorLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(3, 6);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(23, 18);
            this.errorLabel.TabIndex = 25;
            this.errorLabel.Text = "   ";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 29);
            this.panel1.TabIndex = 45;
            // 
            // technicianNameLabel
            // 
            this.technicianNameLabel.AutoSize = true;
            this.technicianNameLabel.BackColor = System.Drawing.Color.FloralWhite;
            this.technicianNameLabel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.technicianNameLabel.ForeColor = System.Drawing.Color.Black;
            this.technicianNameLabel.Location = new System.Drawing.Point(589, 58);
            this.technicianNameLabel.Name = "technicianNameLabel";
            this.technicianNameLabel.Size = new System.Drawing.Size(141, 18);
            this.technicianNameLabel.TabIndex = 46;
            this.technicianNameLabel.Text = "Nama Teknisi : ";
            this.technicianNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // viewTechnicianJobForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(976, 618);
            this.Controls.Add(this.technicianNameLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.jobStartDateTimePicker);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.jobDetailDataGrid);
            this.Controls.Add(this.technicianDataGrid);
            this.MaximizeBox = false;
            this.Name = "viewTechnicianJobForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TECHNICIAN STATUS";
            this.Load += new System.EventHandler(this.viewTechnicianJobForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.technicianDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDetailDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView technicianDataGrid;
        private System.Windows.Forms.DataGridView jobDetailDataGrid;
        private System.Windows.Forms.DateTimePicker jobStartDateTimePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label technicianNameLabel;
    }
}