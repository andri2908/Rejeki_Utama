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
    public partial class groupAccessModuleForm : Form
    {
        private Data_Access DS = new Data_Access();
        private globalUtilities gutil = new globalUtilities();
        private int selectedGroupID = 0;
        private bool isLoading = false;

        private void fillInDummyData()
        {
            groupAccessDataGridView.Rows.Add("PENGATURAN LOKASI DATABASE", false);
            groupAccessDataGridView.Rows.Add("BACKUP/RESTORE DATABASE", false);
            groupAccessDataGridView.Rows.Add("TAMBAH USER", false);
            groupAccessDataGridView.Rows.Add("HAPUS USER", false);
            groupAccessDataGridView.Rows.Add("EDIT USER", false);
        }
            
        public groupAccessModuleForm()
        {
            InitializeComponent();
        }

        public groupAccessModuleForm(int groupID)
        {
            InitializeComponent();

            selectedGroupID = groupID;
        }

        public void setSelectedGroupID(int groupID)
        {
            selectedGroupID = groupID;
        }

        private void newGroupButton_Click(object sender, EventArgs e)
        {
            dataGroupDetailForm displayForm = new dataGroupDetailForm(globalConstants.PENGATURAN_GRUP_AKSES, this);
            displayForm.ShowDialog(this);

            loadGroupUserInformation();
        }

        private void loadGroupUserInformation()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();

            using (rdr = DS.getData("SELECT * FROM MASTER_GROUP WHERE GROUP_ID = " + selectedGroupID))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        namaGroupTextBox.Text = rdr.GetString("GROUP_USER_NAME");
                        deskripsiTextBox.Text = rdr.GetString("GROUP_USER_DESCRIPTION");
                    }
                }
            }
        }

        private void loadUserAccessInformation()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand = "";

            int moduleFeatures;
            int userAccess;

            DS.mySqlConnect();

            sqlCommand = "SELECT MM.MODULE_ID, MM.MODULE_NAME, MM.MODULE_FEATURES, IFNULL(UAM.USER_ACCESS_OPTION,0) AS USER_ACCESS_OPTION " +
                                "FROM MASTER_MODULE MM LEFT OUTER JOIN USER_ACCESS_MANAGEMENT UAM ON (MM.MODULE_ID = UAM.MODULE_ID AND UAM.GROUP_ID = " + selectedGroupID + ") " +
                                "WHERE MM.MODULE_ACTIVE = 1";

            isLoading = true;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    groupAccessDataGridView.Rows.Clear();
                    while (rdr.Read())
                    {
                        moduleFeatures = rdr.GetInt32("MODULE_FEATURES");
                        userAccess = rdr.GetInt32("USER_ACCESS_OPTION");

                        switch(moduleFeatures)
                        {
                            case 1: // ACCESS ONLY 
                                // ACCESS = 1
                                if (userAccess == 1)
                                    groupAccessDataGridView.Rows.Add(rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "1");                     
                                else
                                    groupAccessDataGridView.Rows.Add(rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "1");

                                accessGroupDataGrid.Rows.Add(rdr.GetString("MODULE_ID"), rdr.GetString("MODULE_NAME"), false, false, "0", rdr.GetString("MODULE_FEATURES"));

                                //accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["insertColumn"] = new DataGridViewTextBoxCell();
                                //accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["insertColumn"].Value = "";
                                //accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["insertColumn"].ReadOnly = true;

                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"] = new DataGridViewTextBoxCell();
                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"].Value = "";
                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"].ReadOnly = true;
                                
                                if (userAccess == 1)
                                { 
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["accessColumn"].Value = true;
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["moduleValueColumn"].Value = "1";
                                }
                                break;

                            case 3: // ACCESS + INSERT, UPDATE
                                // INSERT OPERATION = 2
                                // UPDATE OPERATION = 4
                                if ( (userAccess == 2) || (userAccess == 6) )
                                    groupAccessDataGridView.Rows.Add("[INSERT] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "2");  
                                else
                                    groupAccessDataGridView.Rows.Add("[INSERT] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "2"); 
                                
                                if ((userAccess == 4) || (userAccess == 6))
                                    groupAccessDataGridView.Rows.Add("[UPDATE] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "4"); 
                                else
                                    groupAccessDataGridView.Rows.Add("[UPDATE] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "4");

                                accessGroupDataGrid.Rows.Add(rdr.GetString("MODULE_ID"), rdr.GetString("MODULE_NAME"), false, false, "0", rdr.GetString("MODULE_FEATURES"));

                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["accessColumn"] = new DataGridViewCheckBoxCell();
                                if ((userAccess == 2) || (userAccess == 6))
                                { 
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["accessColumn"].Value = true;
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["moduleValueColumn"].Value = userAccess;
                                }
                                else
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["accessColumn"].Value = false;

                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["accessColumn"].ReadOnly = false;

                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"] = new DataGridViewCheckBoxCell();
                                if ((userAccess == 4) || (userAccess == 6))
                                {
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"].Value = true;
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["moduleValueColumn"].Value = userAccess;
                                }
                                else
                                    accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"].Value = false;
                                accessGroupDataGrid.Rows[accessGroupDataGrid.Rows.Count - 1].Cells["updateColumn"].ReadOnly = false;
                                break;

                            //case 4: // VIEW ONLY
                            //    // VIEW = 8
                            //    if (userAccess == 8)
                            //        groupAccessDataGridView.Rows.Add("[LAPORAN] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "8"); 
                            //    else
                            //        groupAccessDataGridView.Rows.Add("[LAPORAN] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "8"); 
                            //    break;

                            //case 7: // VIEW + ACCESS + INSERT, UPDATE, DELETE
                            //    if ((userAccess == 2) || (userAccess == 6) || (userAccess == 10) || (userAccess == 14))
                            //        groupAccessDataGridView.Rows.Add("[INSERT] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "2");    
                            //    else
                            //        groupAccessDataGridView.Rows.Add("[INSERT] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "2");   

                            //    if ((userAccess == 4) || (userAccess == 6) || (userAccess == 12) || (userAccess == 14))
                            //        groupAccessDataGridView.Rows.Add("[UPDATE] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "4");  
                            //    else
                            //        groupAccessDataGridView.Rows.Add("[UPDATE] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "4"); 

                            //    if ((userAccess == 8) || (userAccess == 10) || (userAccess == 12) || (userAccess == 14))
                            //        groupAccessDataGridView.Rows.Add("[LAPORAN] - " + rdr.GetString("MODULE_NAME"), true, rdr.GetString("MODULE_ID"), "8");
                            //    else
                            //        groupAccessDataGridView.Rows.Add("[LAPORAN] - " + rdr.GetString("MODULE_NAME"), false, rdr.GetString("MODULE_ID"), "8");
                            //    break;
                        }
                    }
                }
            }
            isLoading = false;
        }

        private void addColumnToDataGrid()
        {
            DataGridViewTextBoxColumn moduleIDTextBoxColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn moduleNameTextBoxColumn = new DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn accessCheckBoxColumn = new DataGridViewCheckBoxColumn();
            //DataGridViewCheckBoxColumn insertCheckBoxColumn = new DataGridViewCheckBoxColumn();
            DataGridViewCheckBoxColumn updateCheckBoxColumn = new DataGridViewCheckBoxColumn();
            //DataGridViewCheckBoxColumn deleteCheckBoxColumn = new DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn moduleAccessValueColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn moduleFeatureTextBoxColumn = new DataGridViewTextBoxColumn();

            accessGroupDataGrid.Columns.Clear();

            moduleIDTextBoxColumn.Name = "moduleID";
            moduleIDTextBoxColumn.HeaderText = "MODULE ID";
            moduleIDTextBoxColumn.Width = 150;
            moduleIDTextBoxColumn.Visible = false;
            accessGroupDataGrid.Columns.Add(moduleIDTextBoxColumn);

            moduleNameTextBoxColumn.Name = "moduleName";
            moduleNameTextBoxColumn.HeaderText = "NAMA MODULE";
            moduleNameTextBoxColumn.Width = 300;
            moduleNameTextBoxColumn.ReadOnly = true;
            accessGroupDataGrid.Columns.Add(moduleNameTextBoxColumn);

            accessCheckBoxColumn.Name = "accessColumn";
            accessCheckBoxColumn.HeaderText = "AKSES/INSERT";
            accessCheckBoxColumn.Width = 150;
            accessGroupDataGrid.Columns.Add(accessCheckBoxColumn);

            updateCheckBoxColumn.Name = "updateColumn";
            updateCheckBoxColumn.HeaderText = "UPDATE";
            updateCheckBoxColumn.Width = 100;
            accessGroupDataGrid.Columns.Add(updateCheckBoxColumn);

            moduleAccessValueColumn.Name = "moduleValueColumn";
            moduleAccessValueColumn.HeaderText = "moduleAccessValue";
            moduleAccessValueColumn.Width = 100;
            moduleAccessValueColumn.Visible = false;
            accessGroupDataGrid.Columns.Add(moduleAccessValueColumn);

            moduleFeatureTextBoxColumn.Name = "moduleFeatureColumn";
            moduleFeatureTextBoxColumn.HeaderText = "moduleFeatureValue";
            moduleFeatureTextBoxColumn.Width = 100;
            moduleFeatureTextBoxColumn.Visible = false;
            accessGroupDataGrid.Columns.Add(moduleFeatureTextBoxColumn);
        }

        private void groupAccessModuleForm_Load(object sender, EventArgs e)
        {
            Button[] arrButton = new Button[2];

            loadGroupUserInformation();


            addColumnToDataGrid();
            loadUserAccessInformation();

            arrButton[0] = saveButton;
            arrButton[1] = newGroupButton;
            gutil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gutil.reArrangeTabOrder(this);
        }

        private bool saveData()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;

            int i=0;
            int moduleIdValue = 0;
            int tempModuleID = 0;
            int userAccessValue = 0;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                i = 0;

                while (i<accessGroupDataGrid.Rows.Count)
                {
                    moduleIdValue = Convert.ToInt32(accessGroupDataGrid.Rows[i].Cells["moduleID"].Value);
                    userAccessValue = Convert.ToInt32(accessGroupDataGrid.Rows[i].Cells["moduleValueColumn"].Value);

                    if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM USER_ACCESS_MANAGEMENT WHERE MODULE_ID = " + moduleIdValue + " AND GROUP_ID = " + selectedGroupID)) == 0)
                    {
                        // INSERT MODE
                        sqlCommand = "INSERT INTO USER_ACCESS_MANAGEMENT (GROUP_ID, MODULE_ID, USER_ACCESS_OPTION) VALUES (" + selectedGroupID + ", " + moduleIdValue + ", " + userAccessValue + ")";

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                    else
                    {
                        // EDIT MODE
                        sqlCommand = "UPDATE USER_ACCESS_MANAGEMENT SET USER_ACCESS_OPTION = " + userAccessValue + " WHERE GROUP_ID = " + selectedGroupID + " AND MODULE_ID = " + moduleIdValue;

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }

                    i++;
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
                        gutil.showDBOPError(ex, "ROLLBACK");
                    }
                }
                gutil.showDBOPError(e, "INSERT");
                result = false;
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveData())
            {
                //MessageBox.Show("SUCCESS");

                gutil.saveUserChangeLog(globalConstants.MENU_MANAJEMEN_USER, globalConstants.CHANGE_LOG_UPDATE, "UPDATE GROUP ACCESS VALUE");
                gutil.showSuccess(gutil.UPD);
                MessageBox.Show("RE-LOGIN UNTUK MENGAKTIFKAN HAK AKSES YANG BARU", "INFORMASI",MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void groupAccessModuleForm_Activated(object sender, EventArgs e)
        {
            //if need something
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {
            //for (int i=0;i<groupAccessDataGridView.Rows.Count;i++)
            //    groupAccessDataGridView.Rows[i].Cells["hakAkses"].Value = checkAll.Checked;
            for (int i = 0; i < accessGroupDataGrid.Rows.Count; i++)
            {
                accessGroupDataGrid.Rows[i].Cells["accessColumn"].Value = checkAll.Checked;

                if (accessGroupDataGrid.Rows[i].Cells["updateColumn"].ReadOnly == false)
                    accessGroupDataGrid.Rows[i].Cells["updateColumn"].Value = checkAll.Checked;
            }
        }

        private void accessGroupDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var cell = accessGroupDataGrid[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = accessGroupDataGrid.Rows[e.RowIndex];
            string columnName = "";
            int tempVal = 0;
            string moduleFeatures = "";

            columnName = cell.OwningColumn.Name;

            if (isLoading)
                return;
            
            if (columnName == "accessColumn")
            {
                tempVal = Convert.ToInt32(selectedRow.Cells["moduleValueColumn"].Value);
                moduleFeatures = selectedRow.Cells["moduleFeatureColumn"].Value.ToString();

                if (cell.Value.ToString() == "True")
                {
                    if (moduleFeatures == "1")
                        tempVal = tempVal + 1;
                    else
                        tempVal = tempVal + 2;
                    selectedRow.Cells["moduleValueColumn"].Value = tempVal.ToString(); 
                }
                else
                {
                    if (moduleFeatures == "1")
                        tempVal = tempVal - 1;
                    else
                        tempVal = tempVal - 2;
                    selectedRow.Cells["moduleValueColumn"].Value = tempVal.ToString();
                }
            }
            else if (columnName == "updateColumn")
            {
                tempVal = Convert.ToInt32(selectedRow.Cells["moduleValueColumn"].Value);
                if (cell.Value.ToString() == "True")
                {
                    tempVal = tempVal + 4;
                    selectedRow.Cells["moduleValueColumn"].Value = tempVal.ToString();
                }
                else
                {
                    tempVal = tempVal - 4;
                    selectedRow.Cells["moduleValueColumn"].Value = tempVal.ToString();
                }
            }
        }

        private void accessGroupDataGrid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (accessGroupDataGrid.IsCurrentCellDirty)
            {
                accessGroupDataGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
