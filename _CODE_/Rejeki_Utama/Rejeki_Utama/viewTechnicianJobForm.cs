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
    public partial class viewTechnicianJobForm : Form
    {
        private DateTime selectedJobDate = DateTime.Now;
        private Data_Access DS = new Data_Access();
        private int selectedTechnicianID = 0;
        private CultureInfo culture = new CultureInfo("id-ID");
        private globalUtilities gUtil = new globalUtilities();
        bool isLoading = false;

        public viewTechnicianJobForm()
        {
            InitializeComponent();
        }

        private void loadTechnician()
        {
            string sqlCommand = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            sqlCommand = "SELECT ID, TECHNICIAN_NAME AS 'NAMA TEKNISI' FROM MASTER_TECHNICIAN WHERE TECHNICIAN_ACTIVE  = 1";

            technicianDataGrid.DataSource = null;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    dt.Load(rdr);

                    technicianDataGrid.DataSource = dt;
                    technicianDataGrid.Columns["ID"].Visible = false;
                    technicianDataGrid.Columns["NAMA TEKNISI"].Width = 235;
                }
            }
            rdr.Close();

            if (technicianDataGrid.Rows.Count > 0)
            {
                selectedTechnicianID = Convert.ToInt32(technicianDataGrid.Rows[0].Cells["ID"].Value);
                technicianNameLabel.Text = "NAMA TEKNISI : " + technicianDataGrid.Rows[0].Cells["NAMA TEKNISI"].Value.ToString();
            }
        }

        private void loadTechnicianDetailJob()
        {
            string sqlCommand = "";
            string sqlCommand2 = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string jobDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(jobStartDateTimePicker.Value));

            sqlCommand = "SELECT STH.ID, SALES_INVOICE AS 'SALES INVOICE', MC.CUSTOMER_FULL_NAME AS PELANGGAN, JOB_SCHEDULED_TIME AS 'JAM TUGAS', JOB_FINISHED_TIME AS 'JAM SELESAI', IF(IS_JOB_FINISHED = 1, 'SELESAI', 'PENDING') AS STATUS " +
                                   "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC WHERE " +
                                   "STH.CUSTOMER_ID = MC.CUSTOMER_ID AND STH.TECHNICIAN_ID = " + selectedTechnicianID + " AND DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  = '" + jobDate + "'";

            sqlCommand2 = "SELECT STH.ID, SALES_INVOICE AS 'SALES INVOICE', 'P-UMUM' AS PELANGGAN, JOB_SCHEDULED_TIME AS 'JAM TUGAS', JOB_FINISHED_TIME AS 'JAM SELESAI', IF(IS_JOB_FINISHED = 1, 'SELESAI', 'PENDING') AS STATUS " +
                                   "FROM SURAT_TUGAS_HEADER STH WHERE " +
                                   "STH.CUSTOMER_ID = 0 AND STH.TECHNICIAN_ID = " + selectedTechnicianID + " AND DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  = '" + jobDate + "'";

            sqlCommand = "SELECT * FROM (" + sqlCommand + " UNION " + sqlCommand2 + ") TAB1 ORDER BY 'JAM TUGAS' ASC";

            jobDetailDataGrid.DataSource = null;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    dt.Load(rdr);

                    jobDetailDataGrid.DataSource = dt;
                    jobDetailDataGrid.Columns["ID"].Visible = false;
                    jobDetailDataGrid.Columns["SALES INVOICE"].Width = 150;

                }
            }
            rdr.Close();
        }

        private void viewTechnicianJobForm_Load(object sender, EventArgs e)
        {
            jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            jobStartDateTimePicker.Value = selectedJobDate;

            errorLabel.Text = "";

            isLoading = true;
            loadTechnician();
            isLoading = false;
            gUtil.reArrangeTabOrder(this);
        }

        private void technicianDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading)
                return;

            try { 
                int rowSelectedIndex = (technicianDataGrid.SelectedCells[0].RowIndex);
                DataGridViewRow selectedRow = technicianDataGrid.Rows[rowSelectedIndex];

                selectedTechnicianID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                technicianNameLabel.Text = "NAMA TEKNISI : " + selectedRow.Cells["NAMA TEKNISI"].Value.ToString();

                loadTechnicianDetailJob();
            }
            catch(Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[JADWAL TEKNISI] error on technicianDataGrid_CellEnter [" + ex.Message + "]");
            }
        }

        private void jobStartDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            loadTechnicianDetailJob();
        }

        private void technicianDataGrid_DoubleClick(object sender, EventArgs e)
        {
            //if (technicianDataGrid.Rows.Count > 0)
            //{
            //    int rowSelectedIndex = (technicianDataGrid.SelectedCells[0].RowIndex);
            //    DataGridViewRow selectedRow = technicianDataGrid.Rows[rowSelectedIndex];

            //    selectedTechnicianID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            //    technicianNameLabel.Text = "NAMA TEKNISI : " + selectedRow.Cells["NAMA TEKNISI"].Value.ToString();

            //    loadTechnicianDetailJob();
            //}
        }

        private void technicianDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            //if (technicianDataGrid.Rows.Count > 0)
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        int rowSelectedIndex = (technicianDataGrid.SelectedCells[0].RowIndex);
            //        DataGridViewRow selectedRow = technicianDataGrid.Rows[rowSelectedIndex];

            //        selectedTechnicianID = Convert.ToInt32(selectedRow.Cells["ID"].Value);
            //        technicianNameLabel.Text = "NAMA TEKNISI : " + selectedRow.Cells["NAMA TEKNISI"].Value.ToString();

            //        loadTechnicianDetailJob();
            //    }
            //}
        }
    }
}
