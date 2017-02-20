namespace AlphaSoft
{
    partial class reminderDetailForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nonAktifCheckbox = new System.Windows.Forms.CheckBox();
            this.customIntervalMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.customIntervalCombo = new System.Windows.Forms.ComboBox();
            this.messageContentTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.intervalCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.jobStartTimeMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.jobStartDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.resetbutton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.pelangganTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.addButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pelangganPhoneTextBox = new System.Windows.Forms.TextBox();
            this.dataReminderCustomerGridView = new System.Windows.Forms.DataGridView();
            this.customerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repeatCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReminderCustomerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nonAktifCheckbox);
            this.groupBox1.Controls.Add(this.customIntervalMaskedTextBox);
            this.groupBox1.Controls.Add(this.customIntervalCombo);
            this.groupBox1.Controls.Add(this.messageContentTextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.repeatCheckbox);
            this.groupBox1.Controls.Add(this.intervalCombo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.jobStartTimeMaskedTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.jobStartDateTimePicker);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(490, 529);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reminder";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // nonAktifCheckbox
            // 
            this.nonAktifCheckbox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nonAktifCheckbox.AutoSize = true;
            this.nonAktifCheckbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nonAktifCheckbox.Location = new System.Drawing.Point(279, 231);
            this.nonAktifCheckbox.Name = "nonAktifCheckbox";
            this.nonAktifCheckbox.Size = new System.Drawing.Size(197, 22);
            this.nonAktifCheckbox.TabIndex = 69;
            this.nonAktifCheckbox.Text = "Non Aktif Reminder";
            this.nonAktifCheckbox.UseVisualStyleBackColor = true;
            this.nonAktifCheckbox.CheckedChanged += new System.EventHandler(this.nonAktifCheckbox_CheckedChanged);
            // 
            // customIntervalMaskedTextBox
            // 
            this.customIntervalMaskedTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customIntervalMaskedTextBox.Location = new System.Drawing.Point(174, 167);
            this.customIntervalMaskedTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.customIntervalMaskedTextBox.Mask = "000";
            this.customIntervalMaskedTextBox.Name = "customIntervalMaskedTextBox";
            this.customIntervalMaskedTextBox.Size = new System.Drawing.Size(54, 27);
            this.customIntervalMaskedTextBox.TabIndex = 67;
            this.customIntervalMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.customIntervalMaskedTextBox.Visible = false;
            // 
            // customIntervalCombo
            // 
            this.customIntervalCombo.FormattingEnabled = true;
            this.customIntervalCombo.Items.AddRange(new object[] {
            "Hari",
            "Minggu",
            "Bulan",
            "Tahun"});
            this.customIntervalCombo.Location = new System.Drawing.Point(238, 168);
            this.customIntervalCombo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.customIntervalCombo.Name = "customIntervalCombo";
            this.customIntervalCombo.Size = new System.Drawing.Size(121, 26);
            this.customIntervalCombo.TabIndex = 66;
            this.customIntervalCombo.Text = "Hari";
            this.customIntervalCombo.Visible = false;
            this.customIntervalCombo.SelectedIndexChanged += new System.EventHandler(this.customIntervalCombo_SelectedIndexChanged);
            // 
            // messageContentTextBox
            // 
            this.messageContentTextBox.Location = new System.Drawing.Point(18, 259);
            this.messageContentTextBox.MaxLength = 150;
            this.messageContentTextBox.Multiline = true;
            this.messageContentTextBox.Name = "messageContentTextBox";
            this.messageContentTextBox.Size = new System.Drawing.Size(458, 259);
            this.messageContentTextBox.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(18, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 18);
            this.label6.TabIndex = 64;
            this.label6.Text = "Isi Pesan :";
            // 
            // intervalCombo
            // 
            this.intervalCombo.FormattingEnabled = true;
            this.intervalCombo.Items.AddRange(new object[] {
            "Harian",
            "Mingguan",
            "Bulanan",
            "Tahunan",
            "Custom"});
            this.intervalCombo.Location = new System.Drawing.Point(174, 129);
            this.intervalCombo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.intervalCombo.Name = "intervalCombo";
            this.intervalCombo.Size = new System.Drawing.Size(302, 26);
            this.intervalCombo.TabIndex = 60;
            this.intervalCombo.Text = "Harian";
            this.intervalCombo.SelectedIndexChanged += new System.EventHandler(this.customerComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(15, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 59;
            this.label5.Text = "Interval :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(18, 130);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 18);
            this.label4.TabIndex = 58;
            this.label4.Text = "Jam :";
            // 
            // jobStartTimeMaskedTextBox
            // 
            this.jobStartTimeMaskedTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobStartTimeMaskedTextBox.Location = new System.Drawing.Point(177, 54);
            this.jobStartTimeMaskedTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.jobStartTimeMaskedTextBox.Mask = "90:00";
            this.jobStartTimeMaskedTextBox.Name = "jobStartTimeMaskedTextBox";
            this.jobStartTimeMaskedTextBox.Size = new System.Drawing.Size(97, 27);
            this.jobStartTimeMaskedTextBox.TabIndex = 57;
            this.jobStartTimeMaskedTextBox.Text = "0000";
            this.jobStartTimeMaskedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.jobStartTimeMaskedTextBox.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(18, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 18);
            this.label2.TabIndex = 56;
            this.label2.Text = "Tanggal Mulai :";
            // 
            // jobStartDateTimePicker
            // 
            this.jobStartDateTimePicker.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jobStartDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.jobStartDateTimePicker.Location = new System.Drawing.Point(177, 20);
            this.jobStartDateTimePicker.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.jobStartDateTimePicker.Name = "jobStartDateTimePicker";
            this.jobStartDateTimePicker.Size = new System.Drawing.Size(251, 27);
            this.jobStartDateTimePicker.TabIndex = 55;
            // 
            // resetbutton
            // 
            this.resetbutton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.resetbutton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetbutton.Location = new System.Drawing.Point(505, 595);
            this.resetbutton.Name = "resetbutton";
            this.resetbutton.Size = new System.Drawing.Size(95, 37);
            this.resetbutton.TabIndex = 68;
            this.resetbutton.Text = "RESET";
            this.resetbutton.UseVisualStyleBackColor = true;
            this.resetbutton.Click += new System.EventHandler(this.resetbutton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.Black;
            this.saveButton.Location = new System.Drawing.Point(408, 595);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(91, 37);
            this.saveButton.TabIndex = 63;
            this.saveButton.Text = "SAVE";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.ChangePrinterButton_Click);
            // 
            // pelangganTextBox
            // 
            this.pelangganTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pelangganTextBox.Location = new System.Drawing.Point(171, 23);
            this.pelangganTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pelangganTextBox.Name = "pelangganTextBox";
            this.pelangganTextBox.Size = new System.Drawing.Size(287, 27);
            this.pelangganTextBox.TabIndex = 52;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(8, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 18);
            this.label3.TabIndex = 51;
            this.label3.Text = "&Pelanggan [F4] : ";
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.errorLabel.AutoSize = true;
            this.errorLabel.BackColor = System.Drawing.Color.White;
            this.errorLabel.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(4, 5);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(23, 18);
            this.errorLabel.TabIndex = 0;
            this.errorLabel.Text = "   ";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.Controls.Add(this.errorLabel);
            this.panel1.Location = new System.Drawing.Point(2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(981, 29);
            this.panel1.TabIndex = 68;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.addButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.pelangganPhoneTextBox);
            this.groupBox2.Controls.Add(this.dataReminderCustomerGridView);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.pelangganTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(505, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(467, 529);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pelanggan";
            // 
            // addButton
            // 
            this.addButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addButton.ForeColor = System.Drawing.Color.Black;
            this.addButton.Location = new System.Drawing.Point(183, 111);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(91, 37);
            this.addButton.TabIndex = 64;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.Location = new System.Drawing.Point(8, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 56;
            this.label1.Text = "No Telepon :";
            // 
            // pelangganPhoneTextBox
            // 
            this.pelangganPhoneTextBox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pelangganPhoneTextBox.Location = new System.Drawing.Point(171, 60);
            this.pelangganPhoneTextBox.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.pelangganPhoneTextBox.MaxLength = 15;
            this.pelangganPhoneTextBox.Name = "pelangganPhoneTextBox";
            this.pelangganPhoneTextBox.Size = new System.Drawing.Size(287, 27);
            this.pelangganPhoneTextBox.TabIndex = 57;
            // 
            // dataReminderCustomerGridView
            // 
            this.dataReminderCustomerGridView.AllowUserToAddRows = false;
            this.dataReminderCustomerGridView.AllowUserToDeleteRows = false;
            this.dataReminderCustomerGridView.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dataReminderCustomerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataReminderCustomerGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerID,
            this.customerName,
            this.customerPhone});
            this.dataReminderCustomerGridView.Location = new System.Drawing.Point(11, 167);
            this.dataReminderCustomerGridView.MultiSelect = false;
            this.dataReminderCustomerGridView.Name = "dataReminderCustomerGridView";
            this.dataReminderCustomerGridView.ReadOnly = true;
            this.dataReminderCustomerGridView.RowHeadersVisible = false;
            this.dataReminderCustomerGridView.Size = new System.Drawing.Size(447, 351);
            this.dataReminderCustomerGridView.TabIndex = 55;
            this.dataReminderCustomerGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataReminderCustomerGridView_KeyDown);
            // 
            // customerID
            // 
            this.customerID.HeaderText = "customerID";
            this.customerID.Name = "customerID";
            this.customerID.ReadOnly = true;
            this.customerID.Visible = false;
            // 
            // customerName
            // 
            this.customerName.HeaderText = "NAMA PELANGGAN";
            this.customerName.Name = "customerName";
            this.customerName.ReadOnly = true;
            this.customerName.Width = 200;
            // 
            // customerPhone
            // 
            this.customerPhone.HeaderText = "NO TELEPON PELANGGAN";
            this.customerPhone.Name = "customerPhone";
            this.customerPhone.ReadOnly = true;
            this.customerPhone.Width = 200;
            // 
            // repeatCheckbox
            // 
            this.repeatCheckbox.AutoSize = true;
            this.repeatCheckbox.Checked = true;
            this.repeatCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.repeatCheckbox.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.repeatCheckbox.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repeatCheckbox.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.repeatCheckbox.Location = new System.Drawing.Point(177, 100);
            this.repeatCheckbox.Name = "repeatCheckbox";
            this.repeatCheckbox.Size = new System.Drawing.Size(182, 22);
            this.repeatCheckbox.TabIndex = 61;
            this.repeatCheckbox.Text = "Reminder Diulang";
            this.repeatCheckbox.UseVisualStyleBackColor = true;
            // 
            // reminderDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.resetbutton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.Name = "reminderDetailForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REMINDER";
            this.Activated += new System.EventHandler(this.reminderDetailForm_Activated);
            this.Deactivate += new System.EventHandler(this.reminderDetailForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.reminderDetailForm_FormClosed);
            this.Load += new System.EventHandler(this.reminderDetailForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataReminderCustomerGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox intervalCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox jobStartTimeMaskedTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker jobStartDateTimePicker;
        private System.Windows.Forms.TextBox pelangganTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox messageContentTextBox;
        private System.Windows.Forms.MaskedTextBox customIntervalMaskedTextBox;
        private System.Windows.Forms.ComboBox customIntervalCombo;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button resetbutton;
        private System.Windows.Forms.CheckBox nonAktifCheckbox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataReminderCustomerGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox pelangganPhoneTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerPhone;
        private System.Windows.Forms.CheckBox repeatCheckbox;
    }
}