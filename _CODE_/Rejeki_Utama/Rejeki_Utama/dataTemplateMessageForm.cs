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
    public partial class dataTemplateMessageForm : Form
    {
        globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private int originModuleID = 0;
        private reminderDetailForm parentReminderForm;

        public dataTemplateMessageForm()
        {
            InitializeComponent();

        }

        public dataTemplateMessageForm(int moduleID, reminderDetailForm parentForm)
        {
            InitializeComponent();

            originModuleID = moduleID;
            parentReminderForm = parentForm;
        }


        private void loadData(int options = 0)
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            if (options == 0)
            {
                // DISPLAY WITH FILTER
                sqlCommand = "SELECT ID, TEMPLATE_NAME, SUBSTRING(TEMPLATE_MESSAGE, 1, 10) AS PREVIEW FROM  MASTER_TEMPLATE WHERE TEMPLATE_NAME LIKE '%" + templateNameTextBox.Text + "%'";
            }
            else
            {
                // DISPLAY ALL
                sqlCommand = "SELECT ID, TEMPLATE_NAME, SUBSTRING(TEMPLATE_MESSAGE, 1, 10) AS PREVIEW FROM  MASTER_TEMPLATE";
            }

            using (rdr = DS.getData(sqlCommand))
            {
                dataTemplateGridView.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataTemplateGridView.DataSource = dt;

                    dataTemplateGridView.Columns["ID"].Visible = false;
                }
            }
        }

        private void AllButton_Click(object sender, EventArgs e)
        {
            loadData(1);
        }

        private void templateNameTextBox_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void displaySpecificForm(int templateID)
        {
            switch (originModuleID)
            {
                case globalConstants.REMINDER_FORM:
                    parentReminderForm.setTemplateMessage(templateID);
                    this.Close();
                    break;

                default:
                    dataTemplateMessageDetailForm displayForm = new dataTemplateMessageDetailForm(globalConstants.EDIT_TEMPLATE, templateID);
                    displayForm.ShowDialog(this);
                    break;
            }
        }

        private void dataTemplateGridView_DoubleClick(object sender, EventArgs e)
        {
            int templateID = 0;

            if (dataTemplateGridView.Rows.Count <= 0)
                return;

            int rowSelectedIndex = (dataTemplateGridView.SelectedCells[0].RowIndex);
            DataGridViewRow selectedRow = dataTemplateGridView.Rows[rowSelectedIndex];
            templateID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

            displaySpecificForm(templateID);
        }

        private void dataTemplateGridView_KeyDown(object sender, KeyEventArgs e)
        {
            int templateID = 0;

            if (e.KeyCode == Keys.Enter)
            {
                int rowSelectedIndex = (dataTemplateGridView.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = dataTemplateGridView.Rows[rowSelectedIndex];
                templateID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                displaySpecificForm(templateID);
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            dataTemplateMessageDetailForm displayForm = new dataTemplateMessageDetailForm(globalConstants.NEW_TEMPLATE, 0);
            displayForm.ShowDialog(this);    
        }
    }
}
