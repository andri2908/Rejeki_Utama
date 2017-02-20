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
    public partial class dataTeknisiForm : Form
    {
        private int selectedTeknisiID = 0;
        private int originModuleID = 0;
        cashierForm parentCashierForm = null;

        private globalUtilities gutil = new globalUtilities();

        private Data_Access DS = new Data_Access();

        public dataTeknisiForm()
        {
            InitializeComponent();
        }

        public dataTeknisiForm(int moduleID, cashierForm originForm)
        {
            InitializeComponent();
            originModuleID = moduleID;
            parentCashierForm = originForm;
        }

        private void loadSupplierData()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand;
            string namaTeknisiParam = "";

            DS.mySqlConnect();

            //if (namaSupplierTextbox.Text.Equals(""))
            //    return;
            namaTeknisiParam = MySqlHelper.EscapeString(namaSupplierTextbox.Text);

            if (suppliernonactiveoption.Checked == true)
            {
                sqlCommand = "SELECT ID, TECHNICIAN_NAME AS 'NAMA TEKNISI' FROM MASTER_TECHNICIAN WHERE TECHNICIAN_NAME LIKE '%" + namaTeknisiParam + "%'";
            }
            else
            {
                sqlCommand = "SELECT ID, TECHNICIAN_NAME AS 'NAMA TEKNISI' FROM MASTER_TECHNICIAN WHERE TECHNICIAN_ACTIVE = 1 AND TECHNICIAN_NAME LIKE '%" + namaTeknisiParam + "%'";
            }

            using (rdr = DS.getData(sqlCommand))
            {
                dataSupplierDataGridView.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataSupplierDataGridView.DataSource = dt;

                    dataSupplierDataGridView.Columns["ID"].Visible = false;
                    dataSupplierDataGridView.Columns["NAMA TEKNISI"].Width = 300;
                }
            }
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            dataTeknisiDetailForm displayedForm = new dataTeknisiDetailForm(globalConstants.NEW_TECHNICIAN);
            displayedForm.ShowDialog(this);
        }

        private void namaSupplierTextbox_TextChanged(object sender, EventArgs e)
        {
            //loadSupplierData();
            if (!namaSupplierTextbox.Text.Equals(""))
            {
                loadSupplierData();
            }
        }

        private void dataTeknisiForm_Activated(object sender, EventArgs e)
        {
            //loadSupplierData();
            if (!namaSupplierTextbox.Text.Equals(""))
            {
                loadSupplierData();
            }
        }

        private void dataSupplierDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (dataSupplierDataGridView.Rows.Count <= 0)
                return;

            int selectedrowindex = dataSupplierDataGridView.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = dataSupplierDataGridView.Rows[selectedrowindex];
            selectedTeknisiID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            
            if (originModuleID == globalConstants.SERVICE_AC || 
                originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || 
                originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
            {
                parentCashierForm.setTechnicianID(selectedTeknisiID);
                this.Close();
            }
            else
            { 
                dataTeknisiDetailForm displayedForm = new dataTeknisiDetailForm(globalConstants.EDIT_TECHNICIAN, selectedTeknisiID);
                displayedForm.ShowDialog(this);
            }
        }

        private void dataTeknisiForm_Load(object sender, EventArgs e)
        {
            gutil.reArrangeTabOrder(this);

            namaSupplierTextbox.Select();
        }

        private void suppliernonactiveoption_CheckedChanged(object sender, EventArgs e)
        {
            if (!namaSupplierTextbox.Text.Equals(""))
            {
                loadSupplierData();
            }
        }

        private void dataSupplierDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataSupplierDataGridView.Rows.Count <= 0)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                int selectedrowindex = dataSupplierDataGridView.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = dataSupplierDataGridView.Rows[selectedrowindex];
                selectedTeknisiID = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (originModuleID == globalConstants.SERVICE_AC || 
                    originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || 
                    originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                {
                    parentCashierForm.setTechnicianID(selectedTeknisiID);
                    this.Close();
                }
                else
                { 
                    dataTeknisiDetailForm displayedForm = new dataTeknisiDetailForm(globalConstants.EDIT_TECHNICIAN, selectedTeknisiID);
                    displayedForm.ShowDialog(this);
                }
            }
        }
    }
}
