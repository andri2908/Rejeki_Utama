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
    public partial class dataTeknisiDetailForm : Form
    {
        private int selectedTeknisiID;
        private int originModuleID;

        private string previousInputPhone = "";
        private string previousInputFax = "";

        private Data_Access DS = new Data_Access();
        private globalUtilities gUtil = new globalUtilities();
        private int options = 0;

        public dataTeknisiDetailForm(int moduleID)
        {
            InitializeComponent();
            originModuleID = moduleID;
        }

        public dataTeknisiDetailForm(int moduleID, int teknisiID)
        {
            InitializeComponent();

            originModuleID = moduleID;
            selectedTeknisiID = teknisiID;
        }

        private void loadTeknisiDataInformation()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();

            using (rdr = DS.getData("SELECT TECHNICIAN_NAME, IFNULL(DESCRIPTION, '') AS DESCRIPTION, TECHNICIAN_ACTIVE FROM MASTER_TECHNICIAN WHERE ID = " + selectedTeknisiID))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        namaGroupTextBox.Text = rdr.GetString("TECHNICIAN_NAME");
                        deskripsiTextBox.Text = rdr.GetString("DESCRIPTION");

                        if (rdr.GetInt32("TECHNICIAN_ACTIVE") == 1)
                            nonAktifCheckbox.Checked = false;
                        else
                            nonAktifCheckbox.Checked = true;
                    }
                }
            }
        }

        private bool dataValidated()
        {
            if (namaGroupTextBox.Text.Trim().Equals(""))
            {
                errorLabel.Text = "NAMA LOKASI TIDAK BOLEH KOSONG";
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

            string regionName = MySqlHelper.EscapeString(namaGroupTextBox.Text.Trim());
            string regionDesc = MySqlHelper.EscapeString(deskripsiTextBox.Text.Trim());
            byte regionStatus = 0;

            if (nonAktifCheckbox.Checked)
                regionStatus = 0;
            else
                regionStatus = 1;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                switch (originModuleID)
                {
                    case globalConstants.NEW_TECHNICIAN:
                        sqlCommand = "INSERT INTO MASTER_TECHNICIAN (TECHNICIAN_NAME, DESCRIPTION, TECHNICIAN_ACTIVE) VALUES ('" + regionName + "', '" + regionDesc + "', " + regionStatus + ")";
                        break;
                    case globalConstants.EDIT_TECHNICIAN:
                        sqlCommand = "UPDATE MASTER_TECHNICIAN SET TECHNICIAN_NAME = '" + regionName + "', DESCRIPTION= '" + regionDesc + "', TECHNICIAN_ACTIVE = " + regionStatus + " WHERE ID = " + selectedTeknisiID;
                        break;
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
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private bool saveData()
        {
            bool result = false;
            if (dataValidated())
            {
                smallPleaseWait pleaseWait = new smallPleaseWait();
                pleaseWait.Show();

                //  ALlow main UI thread to properly display please wait form.
                Application.DoEvents();
                result = saveDataTransaction();

                pleaseWait.Close();

                return result;
            }

            return result;
        }

        private void dataTeknisiDetailForm_Load(object sender, EventArgs e)
        {
            switch (originModuleID)
            {
                case (globalConstants.NEW_TECHNICIAN):
                    options = gUtil.INS;
                    nonAktifCheckbox.Enabled = false;
                    break;
                case (globalConstants.EDIT_TECHNICIAN):
                    options = gUtil.UPD;
                    nonAktifCheckbox.Enabled = true;
                    loadTeknisiDataInformation();
                    break;
            }
            errorLabel.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                switch (originModuleID)
                {
                    case globalConstants.NEW_TECHNICIAN:
                        gUtil.saveUserChangeLog(globalConstants.MENU_MANAJEMEN_USER, globalConstants.CHANGE_LOG_INSERT, "ADD NEW TECHNICIAN [" + namaGroupTextBox.Text + "]");
                        break;
                    case globalConstants.EDIT_TECHNICIAN:
                        if (nonAktifCheckbox.Checked)
                            gUtil.saveUserChangeLog(globalConstants.MENU_MANAJEMEN_USER, globalConstants.CHANGE_LOG_UPDATE, "UPDATE TECHNICIAN [" + namaGroupTextBox.Text + "] TECHNICIAN STATUS NON-AKTIF");
                        else
                            gUtil.saveUserChangeLog(globalConstants.MENU_MANAJEMEN_USER, globalConstants.CHANGE_LOG_UPDATE, "UPDATE TECHNICIAN [" + namaGroupTextBox.Text + "] TECHNICIAN STATUS AKTIF");
                        break;
                }

                gUtil.showSuccess(options);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gUtil.ResetAllControls(this);
        }
    }
}
