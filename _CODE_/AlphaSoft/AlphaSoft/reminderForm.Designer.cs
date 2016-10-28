namespace AlphaSoft
{
    partial class reminderForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.namaCustomerTextBox = new System.Windows.Forms.TextBox();
            this.dataReminderGridView = new System.Windows.Forms.DataGridView();
            this.reminderNonActiveOption = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.newButton = new System.Windows.Forms.Button();
            this.AllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataReminderGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FloralWhite;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 18);
            this.label1.TabIndex = 30;
            this.label1.Text = "Nama";
            // 
            // namaCustomerTextBox
            // 
            this.namaCustomerTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.namaCustomerTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.namaCustomerTextBox.Location = new System.Drawing.Point(89, 19);
            this.namaCustomerTextBox.Name = "namaCustomerTextBox";
            this.namaCustomerTextBox.Size = new System.Drawing.Size(283, 27);
            this.namaCustomerTextBox.TabIndex = 31;
            this.namaCustomerTextBox.TextChanged += new System.EventHandler(this.namaCustomerTextBox_TextChanged);
            // 
            // dataReminderGridView
            // 
            this.dataReminderGridView.AllowUserToAddRows = false;
            this.dataReminderGridView.AllowUserToDeleteRows = false;
            this.dataReminderGridView.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataReminderGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataReminderGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataReminderGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataReminderGridView.Location = new System.Drawing.Point(0, 133);
            this.dataReminderGridView.MultiSelect = false;
            this.dataReminderGridView.Name = "dataReminderGridView";
            this.dataReminderGridView.ReadOnly = true;
            this.dataReminderGridView.RowHeadersVisible = false;
            this.dataReminderGridView.Size = new System.Drawing.Size(642, 415);
            this.dataReminderGridView.TabIndex = 37;
            this.dataReminderGridView.DoubleClick += new System.EventHandler(this.dataReminderGridView_DoubleClick);
            this.dataReminderGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataReminderGridView_KeyDown);
            // 
            // reminderNonActiveOption
            // 
            this.reminderNonActiveOption.AutoSize = true;
            this.reminderNonActiveOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reminderNonActiveOption.Location = new System.Drawing.Point(90, 52);
            this.reminderNonActiveOption.Name = "reminderNonActiveOption";
            this.reminderNonActiveOption.Size = new System.Drawing.Size(200, 19);
            this.reminderNonActiveOption.TabIndex = 34;
            this.reminderNonActiveOption.Text = "Tampilkan Reminder Non Aktif?";
            this.reminderNonActiveOption.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.reminderNonActiveOption);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.namaCustomerTextBox);
            this.groupBox2.Controls.Add(this.newButton);
            this.groupBox2.Controls.Add(this.AllButton);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.FloralWhite;
            this.groupBox2.Location = new System.Drawing.Point(128, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(378, 117);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "FILTER ";
            // 
            // newButton
            // 
            this.newButton.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.newButton.Location = new System.Drawing.Point(9, 47);
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(56, 37);
            this.newButton.TabIndex = 35;
            this.newButton.Text = "NEW";
            this.newButton.UseVisualStyleBackColor = true;
            this.newButton.Click += new System.EventHandler(this.newButton_Click);
            // 
            // AllButton
            // 
            this.AllButton.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.AllButton.Location = new System.Drawing.Point(89, 77);
            this.AllButton.Name = "AllButton";
            this.AllButton.Size = new System.Drawing.Size(283, 28);
            this.AllButton.TabIndex = 34;
            this.AllButton.Text = "DISPLAY ALL";
            this.AllButton.UseVisualStyleBackColor = true;
            this.AllButton.Click += new System.EventHandler(this.AllButton_Click);
            // 
            // reminderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(642, 549);
            this.Controls.Add(this.dataReminderGridView);
            this.Controls.Add(this.groupBox2);
            this.Name = "reminderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DATA REMINDER";
            ((System.ComponentModel.ISupportInitialize)(this.dataReminderGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox namaCustomerTextBox;
        private System.Windows.Forms.DataGridView dataReminderGridView;
        private System.Windows.Forms.CheckBox reminderNonActiveOption;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button newButton;
        private System.Windows.Forms.Button AllButton;
    }
}