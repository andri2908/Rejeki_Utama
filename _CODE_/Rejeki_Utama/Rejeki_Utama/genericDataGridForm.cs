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
    public partial class genericDataGridForm : Form
    {
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private globalUtilities gUtil = new globalUtilities();
        private int originModuleID = 0;

        public genericDataGridForm()
        {
            InitializeComponent();
        }

        public genericDataGridForm(int moduleID)
        {
            InitializeComponent();
            originModuleID = moduleID;
        }

        private void jobDetailDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void displayData()
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string todayDate;

            switch (originModuleID)
            {
                case globalConstants.DISPLAY_UNASSIGNED_JOB:
                    sqlCommand = "SELECT SH.SALES_INVOICE AS 'INVOICE', SH.SALES_DATE AS 'TANGGAL INVOICE', MC.CUSTOMER_FULL_NAME AS PELANGGAN " + 
                                           "FROM MASTER_CUSTOMER MC, SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT MP " +
                                           "WHERE SD.PRODUCT_ID = MP.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE " +
                                           "AND SH.CUSTOMER_ID = MC.CUSTOMER_ID " +
                                           "AND MP.PRODUCT_IS_SERVICE = 1 AND SH.SALES_INVOICE NOT IN (SELECT SALES_INVOICE FROM SURAT_TUGAS_HEADER) " +
                                           "GROUP BY SH.SALES_INVOICE";
                    this.Text = "DATA INVOICE YANG BELUM DITUGASKAN";
                    break;
                case globalConstants.DISPLAY_JOB_WITH_NO_TECHNICIAN:
                    sqlCommand = "SELECT SALES_INVOICE AS INVOICE, CUSTOMER_FULL_NAME AS PELANGGAN, JOB_SCHEDULED_DATE AS 'TANGGAL TUGAS', JOB_SCHEDULED_TIME AS 'JAM TUGAS' " +
                                           "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC " +
                                           "WHERE TECHNICIAN_ID = 0 AND STH.CUSTOMER_ID = MC.CUSTOMER_ID";
                    this.Text = "DATA SURAT TUGAS TANPA TEKNISI";
                    break;
                case globalConstants.DISPLAY_LATE_JOB:
                    todayDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(DateTime.Now));
                    sqlCommand = "SELECT SALES_INVOICE AS INVOICE, CUSTOMER_FULL_NAME AS PELANGGAN, JOB_SCHEDULED_DATE AS 'TANGGAL TUGAS', JOB_SCHEDULED_TIME AS 'JAM TUGAS' " +
                                           "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC " +
                                           "WHERE STH.CUSTOMER_ID = MC.CUSTOMER_ID AND DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  < '" + todayDate + "' AND IS_JOB_FINISHED = 0";
                    this.Text = "DATA SURAT TUGAS YANG TERLAMBAT";
                    break;
                case globalConstants.DISPLAY_PENDING_JOB:
                    todayDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(DateTime.Now));
                    sqlCommand = "SELECT SALES_INVOICE AS INVOICE, CUSTOMER_FULL_NAME AS PELANGGAN, JOB_SCHEDULED_DATE AS 'TANGGAL TUGAS', JOB_SCHEDULED_TIME AS 'JAM TUGAS' " +
                                           "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC " +
                                           "WHERE STH.CUSTOMER_ID = MC.CUSTOMER_ID AND DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  >= '" + todayDate + "' AND IS_JOB_FINISHED = 0";
                    this.Text = "DATA SURAT TUGAS YANG AKAN DIKERJAKAN";
                    break;
            }

            using (rdr = DS.getData(sqlCommand))
            {
                detailDataGrid.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    detailDataGrid.DataSource = dt;
                }
            }
        }

        private void genericDataGridForm_Load(object sender, EventArgs e)
        {
            displayData();
        }

        private void displaySpecificForm(int rowIndex)
        {
            string noInvoice = "";
            switch (originModuleID)
            {
                case globalConstants.DISPLAY_UNASSIGNED_JOB:
                    noInvoice = detailDataGrid.Rows[rowIndex].Cells["INVOICE"].Value.ToString();

                    cashierForm displayedSuratTugasForm = new cashierForm(globalConstants.TUGAS_PEMASANGAN_BARU, noInvoice);
                    displayedSuratTugasForm.ShowDialog(this);
                    break;
                case globalConstants.DISPLAY_JOB_WITH_NO_TECHNICIAN:
                    noInvoice = detailDataGrid.Rows[rowIndex].Cells["INVOICE"].Value.ToString();

                    cashierForm editSuratTugasForm = new cashierForm(globalConstants.MONITOR_SURAT_TUGAS, noInvoice);
                    editSuratTugasForm.ShowDialog(this);
                    break;
                case globalConstants.DISPLAY_LATE_JOB:
                    noInvoice = detailDataGrid.Rows[rowIndex].Cells["INVOICE"].Value.ToString();

                    cashierForm lateSuratTugasForm = new cashierForm(globalConstants.MONITOR_SURAT_TUGAS, noInvoice);
                    lateSuratTugasForm.ShowDialog(this);
                    break;
                case globalConstants.DISPLAY_PENDING_JOB:
                    noInvoice = detailDataGrid.Rows[rowIndex].Cells["INVOICE"].Value.ToString();

                    cashierForm pendingSuratTugasForm = new cashierForm(globalConstants.MONITOR_SURAT_TUGAS, noInvoice);
                    pendingSuratTugasForm.ShowDialog(this);
                    break;
            }
        }

        private void detailDataGrid_DoubleClick(object sender, EventArgs e)
        {
            if (detailDataGrid.Rows.Count < 1)
                return;

            int rowSelectedIndex = (detailDataGrid.SelectedCells[0].RowIndex);

            displaySpecificForm(rowSelectedIndex);
        }
    }
}
