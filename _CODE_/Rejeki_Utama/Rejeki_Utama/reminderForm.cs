using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace AlphaSoft
{
    public partial class reminderForm : Form
    {
        globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();

        public reminderForm()
        {
            InitializeComponent();
        }

        private void loadData(int options = 0)
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            if (options == 0)
            {
                // DISPLAY WITH FILTER
                sqlCommand = "SELECT MR.REMINDER_ID, MR.START_DATE, MR.START_TIME, SUBSTRING(MR.MESSAGE_CONTENT, 1, 10) AS PREVIEW FROM DETAIL_REMINDER_CUSTOMER DRC, MASTER_REMINDER MR, MASTER_CUSTOMER MC WHERE DRC.CUSTOMER_ID = MC.CUSTOMER_ID AND DRC.REMINDER_ID = MR.REMINDER_ID AND MC.CUSTOMER_FULL_NAME LIKE '%" + namaCustomerTextBox.Text + "%'";
            }
            else
            {
                // DISPLAY ALL
                sqlCommand = "SELECT MR.REMINDER_ID, MR.START_DATE, MR.START_TIME, SUBSTRING(MR.MESSAGE_CONTENT, 1, 10) AS PREVIEW FROM DETAIL_REMINDER_CUSTOMER DRC, MASTER_REMINDER MR, MASTER_CUSTOMER MC WHERE DRC.CUSTOMER_ID = MC.CUSTOMER_ID AND DRC.REMINDER_ID = MR.REMINDER_ID";
            }

            if (!reminderNonActiveOption.Checked)
            {
                sqlCommand = sqlCommand + " AND MR.REMINDER_ACTIVE = 1";
            }

            using (rdr = DS.getData(sqlCommand))
            {
                dataReminderGridView.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataReminderGridView.DataSource = dt;

                    dataReminderGridView.Columns["REMINDER_ID"].Visible = false;
                }
            }
        }

        private void AllButton_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            reminderDetailForm displayForm = new reminderDetailForm(globalConstants.NEW_REMINDER);
            displayForm.ShowDialog(this);
        }

        private void displaySpecificForm(string reminderID)
        {
            reminderDetailForm displayForm = new reminderDetailForm(globalConstants.EDIT_REMINDER, reminderID);
            displayForm.ShowDialog(this);
        }

        private void dataReminderGridView_DoubleClick(object sender, EventArgs e)
        {
            string reminderID = "";

            if (dataReminderGridView.Rows.Count <= 0)
                return;

            int rowSelectedIndex = (dataReminderGridView.SelectedCells[0].RowIndex);
            DataGridViewRow selectedRow = dataReminderGridView.Rows[rowSelectedIndex];
            reminderID = selectedRow.Cells["REMINDER_ID"].Value.ToString();

            displaySpecificForm(reminderID);
        }

        private void dataReminderGridView_KeyDown(object sender, KeyEventArgs e)
        {
            string reminderID = "";

            if (e.KeyCode == Keys.Enter)
            {
                int rowSelectedIndex = (dataReminderGridView.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = dataReminderGridView.Rows[rowSelectedIndex];
                reminderID = selectedRow.Cells["REMINDER_ID"].Value.ToString();

                displaySpecificForm(reminderID);
            }
        }

        private void namaCustomerTextBox_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
