using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Hotkeys;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace AlphaSoft
{
    public partial class reminderDetailForm : Form
    {
        private int selectedIntervalIndex = 0;
        private int selectedCustomIntervalIndex = 0;
        private int originModuleID = 0;

        globalUtilities gUtil = new globalUtilities();
        private int selectedCustomerID = 0;
        private int selectedReminderID = 0;
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");

        private Hotkeys.GlobalHotkey ghk_F1;
        private Hotkeys.GlobalHotkey ghk_F3;
        private Hotkeys.GlobalHotkey ghk_F4;


        public reminderDetailForm()
        {
            InitializeComponent();
        }

        public reminderDetailForm(int moduleID)
        {
            InitializeComponent();

            originModuleID = moduleID;
        }

        public reminderDetailForm(int moduleID, string reminderID)
        {
            InitializeComponent();

            selectedReminderID = 0;

            originModuleID = moduleID;
            int.TryParse(reminderID, out selectedReminderID);
        }

        private void captureAll(Keys key)
        {
            switch (key)
            {
                case Keys.F1:
                    reminderDetailHelpForm displayHelpForm = new reminderDetailHelpForm();
                    displayHelpForm.ShowDialog(this);
                    break;

                case Keys.F3:
                    dataTemplateMessageForm displayTemplateForm = new dataTemplateMessageForm(globalConstants.REMINDER_FORM, this);
                    displayTemplateForm.ShowDialog(this);
                    break;

                case Keys.F4:
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "[REMINDER SMS] DISPLAY PELANGGAN FORM");
                    dataPelangganForm pelangganForm = new dataPelangganForm(globalConstants.REMINDER_FORM, this);
                    pelangganForm.ShowDialog(this);
                    break;
            }
        }

        private void captureAltModifier(Keys key)
        {
            switch (key)
            {
                case Keys.F4: // ALT + F4
                    MessageBox.Show("ALT+F4");
                    this.Close();
                    break;
            }
        }

        private void captureCtrlModifier(Keys key)
        {
            switch (key)
            {
                default:
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int modifier = (int)m.LParam & 0xFFFF;

                if (modifier == Constants.NOMOD)
                    captureAll(key);
                else if (modifier == Constants.ALT)
                    captureAltModifier(key);
                else if (modifier == Constants.CTRL)
                    captureCtrlModifier(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "[REMINDER SMS] REGISTER HOTKEY");

            ghk_F1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F1, this);
            ghk_F1.Register();

            ghk_F3 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F3, this);
            ghk_F3.Register();

            ghk_F4 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F4, this);
            ghk_F4.Register();
        }

        private void unregisterGlobalHotkey()
        {
            gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "[REMINDER SMS] UNREGISTER HOTKEY");

            ghk_F1.Unregister();
            ghk_F3.Unregister();
            ghk_F4.Unregister();
        }

        public void setCustomerID(int customerID)
        {
            string customerName;
            string customerPhone;

            if (customerID > 0)
            {
                pelangganTextBox.ReadOnly = true;
                selectedCustomerID = customerID;

                customerName = DS.getDataSingleValue("SELECT CUSTOMER_FULL_NAME FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedCustomerID).ToString();
                customerPhone = DS.getDataSingleValue("SELECT CUSTOMER_PHONE FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedCustomerID).ToString();

                pelangganTextBox.Text = customerName;
                pelangganPhoneTextBox.Text = customerPhone;
            }
            else
            {
                pelangganTextBox.ReadOnly = false;
                selectedCustomerID = 0;
            }
        }

        public void setTemplateMessage(int templateID)
        {
            string templateMessage = "";

            if (templateID > 0)
            {
                templateMessage = DS.getDataSingleValue("SELECT TEMPLATE_MESSAGE FROM MASTER_TEMPLATE WHERE ID = " + templateID).ToString();

                messageContentTextBox.Text = templateMessage;
            }
        }

        private void loadDataReminder()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";
            int intervalValue = 0;
            int intervalType = 0;

            // LOAD HEADER DATA
            sqlCommand = "SELECT * FROM MASTER_REMINDER WHERE REMINDER_ID = " + selectedReminderID;

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        jobStartDateTimePicker.Value = rdr.GetDateTime("START_DATE");
                        jobStartTimeMaskedTextBox.Text = rdr.GetString("START_TIME");
                        messageContentTextBox.Text = rdr.GetString("MESSAGE_CONTENT");

                        intervalValue = rdr.GetInt32("INTERVAL_VALUE");
                        intervalType = rdr.GetInt32("INTERVAL_TYPE");

                        if (intervalValue > 1)
                        {
                            //CUSTOM INTERVAL
                            intervalCombo.SelectedIndex = 4;
                            selectedIntervalIndex = 4;
                            intervalCombo.Text = intervalCombo.Items[4].ToString();

                            customIntervalCombo.SelectedIndex = intervalType;
                            selectedCustomIntervalIndex = intervalType;
                            customIntervalCombo.Text = customIntervalCombo.Items[4].ToString();

                            customIntervalMaskedTextBox.Text = intervalValue.ToString();
                        }
                        else
                        {
                            intervalCombo.SelectedIndex = intervalType;
                            selectedIntervalIndex = intervalType;
                            intervalCombo.Text = intervalCombo.Items[intervalType].ToString();
                        }

                        if (1 == rdr.GetInt32("REMINDER_ACTIVE"))
                            nonAktifCheckbox.Checked = false;
                        else
                            nonAktifCheckbox.Checked = true;

                        if (1 == rdr.GetInt32("IS_REPEATED"))
                            repeatCheckbox.Checked = true;
                        else
                            repeatCheckbox.Checked = false;
                    }
                }
            }
            rdr.Close();

            // LOAD CUSTOMER DATA

            sqlCommand = "SELECT MC.CUSTOMER_ID, MC.CUSTOMER_FULL_NAME, DRC.CUSTOMER_PHONE FROM DETAIL_REMINDER_CUSTOMER DRC, MASTER_CUSTOMER MC WHERE DRC.CUSTOMER_ID = MC.CUSTOMER_ID AND DRC.REMINDER_ID = " + selectedReminderID;

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        dataReminderCustomerGridView.Rows.Add(rdr.GetInt32("CUSTOMER_ID"), rdr.GetString("CUSTOMER_FULL_NAME"), rdr.GetString("CUSTOMER_PHONE"));
                    }
                }
            }
            rdr.Close();

        }

        private void reminderDetailForm_Load(object sender, EventArgs e)
        {
            Button[] arrButton = new Button[2];

            jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            jobStartTimeMaskedTextBox.Text = String.Format(culture, "{0:HH:mm}", DateTime.Now);

            arrButton[0] = saveButton;
            arrButton[1] = resetbutton;
            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gUtil.reArrangeTabOrder(this);

            errorLabel.Text = "";


            if (originModuleID == globalConstants.EDIT_REMINDER)
            {
                loadDataReminder();
            }

            registerGlobalHotkey();
        }

        private bool dataValidated()
        {
            if (dataReminderCustomerGridView.Rows.Count <=0 )
            {
                errorLabel.Text = "DAFTAR PELANGGAN TIDAK BOLEH KOSONG";
                return false;
            }

            if (messageContentTextBox.Text.Length <= 0)
            {
                errorLabel.Text = "ISI PESAN TIDAK BOLEH KOSONG";
                return false;
            }

            if (selectedIntervalIndex == 4)
            {
                // CHECKING FOR CUSTOM INTERVAL
                if (customIntervalMaskedTextBox.Text.Length <= 0)
                {
                    errorLabel.Text = "INTERVAL TIDAK BOLEH KOSONG";
                    return false;
                }
            }

            errorLabel.Text = "";
            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;
            string startReminderDate;
            string nextReminderDate;
            int intervalReminderValue = 0;
            int intervalType = 0;
            int reminderID = 0;
            int isRepeated = 0;
            int reminderActive = 0;

            string timeRemind = "";
            string hourNow = "";
            string minNow = "";
            int timeNowValue = 0;

            string hourRemind = "";
            string minRemind = "";
            int timeRemindValue = 0;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                startReminderDate = String.Format(culture, "{0:dd-MM-yyyy}", jobStartDateTimePicker.Value);

                if (selectedIntervalIndex == 4)
                {
                    intervalReminderValue = Convert.ToInt32(customIntervalMaskedTextBox.Text);
                    intervalType = selectedCustomIntervalIndex;
                }
                else
                { 
                    intervalType = selectedIntervalIndex;
                    intervalReminderValue = 1;
                }

                if (repeatCheckbox.Checked)
                    isRepeated = 1;

                if (
                    ((DateTime.Now.Date == jobStartDateTimePicker.Value.Date) && (timeNowValue < timeRemindValue)) ||
                    (DateTime.Now.Date < jobStartDateTimePicker.Value.Date)
                    )
                {
                    nextReminderDate = startReminderDate;
                }
                else
                {
                    nextReminderDate = gUtil.calculateNextReminderDate(jobStartDateTimePicker.Value, intervalReminderValue, intervalType);
                }

                if (originModuleID == globalConstants.NEW_REMINDER)
                {
                    reminderID = Convert.ToInt32(DS.getDataSingleValue("SELECT IFNULL(MAX(REMINDER_ID), 0) FROM MASTER_REMINDER")) + 1;

                    // SAVE HEADER DATA
                    sqlCommand = "INSERT INTO MASTER_REMINDER (REMINDER_ID, START_DATE, INTERVAL_VALUE, INTERVAL_TYPE, START_TIME, MESSAGE_CONTENT, REMINDER_ACTIVE, IS_REPEATED, SCHEDULED_DATE) " +
                                           "VALUES (" + reminderID + ", STR_TO_DATE('" + startReminderDate + "', '%d-%m-%Y'), " + intervalReminderValue + ", " + intervalType + ", '" + jobStartTimeMaskedTextBox.Text + "', '" + messageContentTextBox.Text + "', 1, " + isRepeated + ", STR_TO_DATE('" + nextReminderDate + "', '%d-%m-%Y'))";

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                }
                else if (originModuleID == globalConstants.EDIT_REMINDER)
                {
                    if (nonAktifCheckbox.Checked)
                        reminderActive = 0;
                    else
                        reminderActive = 1;

                    // UPDATE HEADER DATA
                    sqlCommand = "UPDATE MASTER_REMINDER SET " +
                                            "START_DATE = STR_TO_DATE('" + startReminderDate + "', '%d-%m-%Y'), " +
                                            "INTERVAL_VALUE = " + intervalReminderValue + ", " +
                                            "INTERVAL_TYPE = " + intervalType + ", " +
                                            "START_TIME = '" + jobStartTimeMaskedTextBox.Text +"', " +
                                            "MESSAGE_CONTENT = '" + messageContentTextBox.Text + "', " +
                                            "REMINDER_ACTIVE = " + reminderActive + ", " +
                                            "IS_REPEATED = " + isRepeated + ", " +
                                            "SCHEDULED_DATE = STR_TO_DATE('" + nextReminderDate + "', '%d-%m-%Y') " + 
                                           "WHERE REMINDER_ID = " + reminderID;

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // CLEAR DETAIL DATA
                    sqlCommand = "DELETE FROM DETAIL_REMINDER_CUSTOMER WHERE REMINDER_ID = " + reminderID;// + " AND REMINDER_ACTIVE = 1";

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                hourNow = String.Format(culture, "{0:HH}", DateTime.Now);
                minNow = String.Format(culture, "{0:mm}", DateTime.Now);
                timeNowValue = (60 * Convert.ToInt32(hourNow)) + Convert.ToInt32(minNow);

                timeRemind = jobStartTimeMaskedTextBox.Text;
                hourRemind = timeRemind.Substring(0, 2).Trim();
                minRemind = timeRemind.Substring(3, 2).Trim();
                timeRemindValue = (60 * Convert.ToInt32(hourRemind)) + Convert.ToInt32(minRemind);

                // SAVE CUSTOMER AND SCHEDULE DATA
                for (int i = 0; i < dataReminderCustomerGridView.Rows.Count; i++)
                {
                    sqlCommand = "INSERT INTO DETAIL_REMINDER_CUSTOMER (REMINDER_ID, CUSTOMER_ID, CUSTOMER_PHONE) " +
                                           "VALUES (" + reminderID + ", " + dataReminderCustomerGridView.Rows[i].Cells["customerID"].Value.ToString() + ", '" + dataReminderCustomerGridView.Rows[i].Cells["customerPhone"].Value.ToString() + "')";

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                try
                {
                    DS.rollBack();
                }
                catch (MySqlException ex)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gUtil.showDBOPError(ex, "ROLLBACK");
                    }
                }

                gUtil.showDBOPError(e, "INSERT");
                result = false;
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private bool saveData()
        {
            if (dataValidated())
            {
                return saveDataTransaction();
            }

            return false;
        }

        private void ChangePrinterButton_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                MessageBox.Show("SUCCESS");
            }
            else
            {
                MessageBox.Show("FAIL");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (intervalCombo.SelectedIndex == 4)
            {
                customIntervalMaskedTextBox.Visible = true;
                customIntervalCombo.Visible = true;
            }
            else
            {
                customIntervalMaskedTextBox.Visible = false;
                customIntervalCombo.Visible = false;
            }

            selectedIntervalIndex = intervalCombo.SelectedIndex;
        }

        private void resetbutton_Click(object sender, EventArgs e)
        {
            selectedCustomerID = 0;

            pelangganPhoneTextBox.Text = "";
            pelangganTextBox.Text = "";

            jobStartDateTimePicker.Value = DateTime.Now;
            jobStartTimeMaskedTextBox.Text = String.Format(culture, "{0:HH:mm}", DateTime.Now);

            intervalCombo.Text = intervalCombo.Items[0].ToString();
            customIntervalCombo.Text = customIntervalCombo.Items[0].ToString();

            selectedIntervalIndex = 0;
            selectedCustomIntervalIndex = 0;

            customIntervalMaskedTextBox.Clear();
            messageContentTextBox.Clear();

            while (dataReminderCustomerGridView.Rows.Count > 0)
                dataReminderCustomerGridView.Rows.Remove(dataReminderCustomerGridView.Rows[0]);

            pelangganTextBox.Select();
    }

        private void customIntervalCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCustomIntervalIndex = customIntervalCombo.SelectedIndex;
        }

        private void reminderDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void reminderDetailForm_Activated(object sender, EventArgs e)
        {
            registerGlobalHotkey();
        }

        private void reminderDetailForm_Deactivate(object sender, EventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void nonAktifCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dataReminderCustomerGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataReminderCustomerGridView.Rows.Count <= 0)
                return;

            int rowSelectedIndex = dataReminderCustomerGridView.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataReminderCustomerGridView.Rows[rowSelectedIndex];

            if (e.KeyCode == Keys.Delete)
            {
                // DELETE ROW
                dataReminderCustomerGridView.Rows.Remove(selectedRow);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            dataReminderCustomerGridView.Rows.Add(selectedCustomerID, pelangganTextBox.Text, pelangganPhoneTextBox.Text);
        }
    }
}
