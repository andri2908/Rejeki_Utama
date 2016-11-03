namespace AlphaSoft
{
    partial class technicianDailyJobViewForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorLabel = new System.Windows.Forms.Label();
            this.jobStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.hourlyDataGrid = new System.Windows.Forms.DataGridView();
            this.jobDetailDataGrid = new System.Windows.Forms.DataGridView();
            this.listBoxStatus = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hourlyDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDetailDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(985, 28);
            this.panel1.TabIndex = 48;
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
            // jobStartDateTimePicker
            // 
            this.jobStartDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobStartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.jobStartDateTimePicker.Location = new System.Drawing.Point(186, 48);
            this.jobStartDateTimePicker.Name = "jobStartDateTimePicker";
            this.jobStartDateTimePicker.Size = new System.Drawing.Size(152, 27);
            this.jobStartDateTimePicker.TabIndex = 47;
            this.jobStartDateTimePicker.ValueChanged += new System.EventHandler(this.jobStartDateTimePicker_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FloralWhite;
            this.label7.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 18);
            this.label7.TabIndex = 46;
            this.label7.Text = "Tanggal Tugas      : ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hourlyDataGrid
            // 
            this.hourlyDataGrid.AllowUserToAddRows = false;
            this.hourlyDataGrid.AllowUserToDeleteRows = false;
            this.hourlyDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.hourlyDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.hourlyDataGrid.Location = new System.Drawing.Point(10, 91);
            this.hourlyDataGrid.Name = "hourlyDataGrid";
            this.hourlyDataGrid.ReadOnly = true;
            this.hourlyDataGrid.RowHeadersVisible = false;
            this.hourlyDataGrid.Size = new System.Drawing.Size(328, 404);
            this.hourlyDataGrid.TabIndex = 49;
            this.hourlyDataGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.hourlyDataGrid_CellEnter);
            // 
            // jobDetailDataGrid
            // 
            this.jobDetailDataGrid.AllowUserToAddRows = false;
            this.jobDetailDataGrid.AllowUserToDeleteRows = false;
            this.jobDetailDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.jobDetailDataGrid.DefaultCellStyle = dataGridViewCellStyle6;
            this.jobDetailDataGrid.Location = new System.Drawing.Point(344, 91);
            this.jobDetailDataGrid.Name = "jobDetailDataGrid";
            this.jobDetailDataGrid.ReadOnly = true;
            this.jobDetailDataGrid.RowHeadersVisible = false;
            this.jobDetailDataGrid.Size = new System.Drawing.Size(628, 404);
            this.jobDetailDataGrid.TabIndex = 50;
            this.jobDetailDataGrid.DoubleClick += new System.EventHandler(this.jobDetailDataGrid_DoubleClick);
            this.jobDetailDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.jobDetailDataGrid_KeyDown);
            // 
            // listBoxStatus
            // 
            this.listBoxStatus.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxStatus.FormattingEnabled = true;
            this.listBoxStatus.ItemHeight = 18;
            this.listBoxStatus.Location = new System.Drawing.Point(12, 501);
            this.listBoxStatus.Name = "listBoxStatus";
            this.listBoxStatus.Size = new System.Drawing.Size(960, 94);
            this.listBoxStatus.TabIndex = 51;
            this.listBoxStatus.DoubleClick += new System.EventHandler(this.listBoxStatus_DoubleClick);
            // 
            // technicianDailyJobViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(984, 604);
            this.Controls.Add(this.listBoxStatus);
            this.Controls.Add(this.jobDetailDataGrid);
            this.Controls.Add(this.hourlyDataGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.jobStartDateTimePicker);
            this.Controls.Add(this.label7);
            this.MaximizeBox = false;
            this.Name = "technicianDailyJobViewForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JADWAL HARIAN";
            this.Load += new System.EventHandler(this.technicianDailyJobViewForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hourlyDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jobDetailDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.DateTimePicker jobStartDateTimePicker;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView hourlyDataGrid;
        private System.Windows.Forms.DataGridView jobDetailDataGrid;
        private System.Windows.Forms.ListBox listBoxStatus;
    }
}