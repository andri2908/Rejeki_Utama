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
    public partial class dataTemplateMessageDetailForm : Form
    {
        private int originModuleID = 0;
        private int selectedTemplateID = 0;

        private Data_Access DS = new Data_Access();
        private globalUtilities gUtil = new globalUtilities();

        public dataTemplateMessageDetailForm()
        {
            InitializeComponent();
        }

        public dataTemplateMessageDetailForm(int moduleID, int templateID)
        {
            InitializeComponent();

            originModuleID = moduleID;
            selectedTemplateID = templateID;
        }

        private void dataTemplateMessageDetailForm_Load(object sender, EventArgs e)
        {
            Button[] arrButton = new Button[2];

            errorLabel.Text = "";

            arrButton[0] = saveButton;
            arrButton[1] = resetButton;
            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gUtil.reArrangeTabOrder(this);

            if (originModuleID == globalConstants.EDIT_TEMPLATE)
                loadData();
        }

        private void loadData()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            sqlCommand = "SELECT * FROM MASTER_TEMPLATE WHERE ID = " + selectedTemplateID;

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        templateNameTextBox.Text = rdr.GetString("TEMPLATE_NAME");
                        templateContentTextBox.Text = rdr.GetString("TEMPLATE_MESSAGE");
                    }
                }
            }
            rdr.Close();
        }

        private bool dataValidated()
        {
            if (templateNameTextBox.Text.Length <= 0)
            {
                errorLabel.Text = "NAMA TEMPLATE TIDAK BOLEH KOSONG";
                return false;
            }

            if (templateContentTextBox.Text.Length <= 0)
            {
                errorLabel.Text = "ISI TEMPLATE TIDAK BOLEH KOSONG";
                return false;
            }

            errorLabel.Text = "";
            return true;
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                if (originModuleID == globalConstants.NEW_TEMPLATE)
                {
                    sqlCommand = "INSERT INTO MASTER_TEMPLATE (TEMPLATE_NAME, TEMPLATE_MESSAGE) VALUES ('" + templateNameTextBox.Text + "', '" + templateContentTextBox.Text + "')";
                }
                else if (originModuleID == globalConstants.EDIT_TEMPLATE)
                {
                    sqlCommand = "UPDATE MASTER_TEMPLATE SET TEMPLATE_NAME = '" + templateNameTextBox + "', TEMPLATE_MESSAGE = '" + templateContentTextBox.Text + " WHERE ID = " + selectedTemplateID;
                }

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

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

        private void saveButton_Click(object sender, EventArgs e)
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
    }
}
