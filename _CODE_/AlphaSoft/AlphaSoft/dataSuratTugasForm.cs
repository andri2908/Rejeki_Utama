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
using System.Globalization;

namespace AlphaSoft
{
    public partial class dataSuratTugasForm : Form
    {
        private globalUtilities gUtil = new globalUtilities();
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private int customerID = 0;
        private int technicianID = 0;

        private int originModuleID = 0;

        public dataSuratTugasForm()
        {
            InitializeComponent();
        }

        public dataSuratTugasForm(int moduleID)
        {
            InitializeComponent();
            originModuleID = moduleID;
        }

        private void fillInCustomerCombo()
        {
            MySqlDataReader rdr;
            string sqlCommand;

            sqlCommand = "SELECT CUSTOMER_ID, CUSTOMER_FULL_NAME FROM MASTER_CUSTOMER WHERE CUSTOMER_ACTIVE = 1";

            customerCombo.Items.Clear();
            customerHiddenCombo.Items.Clear();

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        customerCombo.Items.Add(rdr.GetString("CUSTOMER_FULL_NAME"));
                        customerHiddenCombo.Items.Add(rdr.GetString("CUSTOMER_ID"));
                    }
                }
            }
            rdr.Close();
        }

        private void fillInTechnicianCombo()
        {
            MySqlDataReader rdr;
            string sqlCommand;

            sqlCommand = "SELECT ID, TECHNICIAN_NAME FROM MASTER_TECHNICIAN WHERE TECHNICIAN_ACTIVE = 1";

            teknisiCombo.Items.Clear();
            teknisiHiddenCombo.Items.Clear();

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        teknisiCombo.Items.Add(rdr.GetString("TECHNICIAN_NAME"));
                        teknisiHiddenCombo.Items.Add(rdr.GetString("ID"));
                    }
                }
            }
            rdr.Close();
        }

        private void loadInvoiceData()
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand = "";
            string dateFrom, dateTo;
            string noInvoiceParam = "";
            string whereClause1 = "";
            string sqlClause1 = "";
            string sqlClause2 = "";

            DS.mySqlConnect();

            if (originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
            {
                //sqlClause1 = "SELECT SH.ID, SH.SALES_INVOICE AS 'NO INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', DATE_FORMAT(SH.SALES_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', (SH.SALES_TOTAL - SH.SALES_DISCOUNT_FINAL) AS 'TOTAL', IF(STH.SALES_INVOICE IS NOT NULL, 'ST', '') AS 'SURAT TUGAS' " +
                //                       "FROM SALES_HEADER SH LEFT OUTER JOIN SURAT_TUGAS_HEADER STH ON (STH.SALES_INVOICE = SH.SALES_INVOICE), MASTER_CUSTOMER MC " +
                //                       "WHERE SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_VOID = 0";

                //sqlClause2 = "SELECT SH.ID, SH.SALES_INVOICE AS 'NO INVOICE', '' AS 'CUSTOMER', DATE_FORMAT(SH.SALES_DATE, '%d-%M-%Y') AS 'TGL INVOICE',(SH.SALES_TOTAL - SH.SALES_DISCOUNT_FINAL) AS 'TOTAL',  IF(STH.SALES_INVOICE IS NOT NULL, 'ST', '') AS 'SURAT TUGAS' " +
                //                       "FROM SALES_HEADER SH LEFT OUTER JOIN SURAT_TUGAS_HEADER STH ON (STH.SALES_INVOICE = SH.SALES_INVOICE) " +
                //                       "WHERE SH.CUSTOMER_ID = 0 AND SH.SALES_VOID = 0";

                sqlClause1 = "SELECT STH.ID, STH.SALES_INVOICE, MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', DATE_FORMAT(STH.SALES_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', DATE_FORMAT(STH.JOB_SCHEDULED_DATE, '%d-%M-%Y')  AS 'TGL TUGAS', DATE_FORMAT(STH.JOB_SCHEDULED_TIME, '%d-%M-%Y')  AS 'JAM TUGAS', DATE_FORMAT(STH.JOB_FINISHED_DATE, '%d-%M-%Y')  AS 'TGL SELESAI', DATE_FORMAT(STH.JOB_FINISHED_TIME, '%d-%M-%Y')  AS 'JAM SELESAI', STH.IS_JOB_FINISHED " +
                                    "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC " +
                                    "WHERE STH.CUSTOMER_ID = MC.CUSTOMER_ID ";

                sqlClause2 = "SELECT STH.ID, STH.SALES_INVOICE, '' AS 'CUSTOMER', DATE_FORMAT(STH.SALES_DATE, '%d-%M-%Y')  AS 'TGL INVOICE', DATE_FORMAT(STH.JOB_SCHEDULED_DATE, '%d-%M-%Y')  AS 'TGL TUGAS', DATE_FORMAT(STH.JOB_SCHEDULED_TIME, '%d-%M-%Y')  AS 'JAM TUGAS', DATE_FORMAT(STH.JOB_FINISHED_DATE, '%d-%M-%Y')  AS 'TGL SELESAI', DATE_FORMAT(STH.JOB_FINISHED_TIME, '%d-%M-%Y')  AS 'JAM SELESAI', STH.IS_JOB_FINISHED " +
                                    "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC " +
                                    "WHERE STH.CUSTOMER_ID = 0 ";
            }

            if (!showAllCheckBox.Checked)
            {
                if (noInvoiceTextBox.Text.Length > 0)
                {
                        whereClause1 = whereClause1 + " AND STH.SALES_INVOICE LIKE '%" + noInvoiceParam + "%'";
                }

                dateFrom = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(PODtPicker_1.Value));
                dateTo = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(PODtPicker_2.Value));

                whereClause1 = whereClause1 + " AND DATE_FORMAT(STH.JOB_SCHEDULED_DATE, '%Y%m%d')  >= '" + dateFrom + "' AND DATE_FORMAT(STH.JOB_SCHEDULED_DATE, '%Y%m%d')  <= '" + dateTo + "'";

                if (technicianID > 0)
                {
                    whereClause1 = whereClause1 + " AND STH.TECHNICIAN_ID = " + technicianID; 
                }

                if (customerID > 0)
                {
                    sqlCommand = sqlClause1 + whereClause1 + " AND STH.CUSTOMER_ID = " + customerID;
                }
                else
                {
//                    whereClause1 = whereClause1 + " ORDER BY STH.JOB_SCHEDULED_DATE, STH.JOB_SCHEDULED TIME";
                    sqlCommand = sqlClause1 + whereClause1 + " UNION " + sqlClause2 + whereClause1;
                }
            }
            else
            {
                sqlCommand = sqlClause1 + " UNION " + sqlClause2;
            }

            sqlCommand = "SELECT * FROM (" + sqlCommand + ") TAB1 ORDER BY 'TGL TUGAS', 'JAM TUGAS' ASC";

            using (rdr = DS.getData(sqlCommand))
            {
                dataPenerimaanBarang.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    dataPenerimaanBarang.DataSource = dt;
                    dataPenerimaanBarang.Columns["ID"].Visible = false;

                    dataPenerimaanBarang.Columns["SALES_INVOICE"].Width = 200;
                    dataPenerimaanBarang.Columns["TGL INVOICE"].Width = 200;
                    dataPenerimaanBarang.Columns["TGL TUGAS"].Width = 200;
                    dataPenerimaanBarang.Columns["TGL SELESAI"].Width = 200;
                    dataPenerimaanBarang.Columns["JAM TUGAS"].Width = 100;
                    dataPenerimaanBarang.Columns["JAM SELESAI"].Width = 100;
                    dataPenerimaanBarang.Columns["CUSTOMER"].Width = 200;
                }
            }

            rdr.Close();
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            loadInvoiceData();
        }

        private void customerCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            customerID = Convert.ToInt32(customerHiddenCombo.Items[customerCombo.SelectedIndex].ToString());
        }

        private void teknisiCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            technicianID = Convert.ToInt32(teknisiHiddenCombo.Items[teknisiCombo.SelectedIndex].ToString());
        }

        private void dataSuratTugasForm_Load(object sender, EventArgs e)
        {
            Button[] arrButton = new Button[1];

            PODtPicker_1.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            PODtPicker_2.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

            fillInCustomerCombo();
            fillInTechnicianCombo();

            arrButton[0] = displayButton;
            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            gUtil.reArrangeTabOrder(this);

            noInvoiceTextBox.Select();
        }

        private void displaySpecificForm(string noInvoice)
        {
            switch (originModuleID)
            {
                case globalConstants.MONITOR_SURAT_TUGAS:
                    cashierForm displayedSuratTugasForm = new cashierForm(globalConstants.TUGAS_PEMASANGAN_BARU, noInvoice);
                    displayedSuratTugasForm.ShowDialog(this);
                    break;
            }
        }

        private void dataPenerimaanBarang_DoubleClick(object sender, EventArgs e)
        {
            string noInvoice = "";

            if (dataPenerimaanBarang.Rows.Count <= 0)
                return;

            int rowSelectedIndex = (dataPenerimaanBarang.SelectedCells[0].RowIndex);
            DataGridViewRow selectedRow = dataPenerimaanBarang.Rows[rowSelectedIndex];
            noInvoice = selectedRow.Cells["SALES_INVOICE"].Value.ToString();

            if (originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
            {
                displaySpecificForm(noInvoice);
            }
        }

        private void dataPenerimaanBarang_KeyDown(object sender, KeyEventArgs e)
        {
            string noInvoice = "";
            string status = "";
            string revNo = "";

            if (e.KeyCode == Keys.Enter)
            {
                int rowSelectedIndex = (dataPenerimaanBarang.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = dataPenerimaanBarang.Rows[rowSelectedIndex];
                noInvoice = selectedRow.Cells["SALES_INVOICE"].Value.ToString();

                if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU)
                {
                    displaySpecificForm(noInvoice);
                }
            }

        }
    }
}
