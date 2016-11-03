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
    public partial class technicianDailyJobViewForm : Form
    {
        private DateTime selectedJobDate = DateTime.Now;
        private Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");
        private globalUtilities gUtil = new globalUtilities();

        private bool isLoading = false;

        public technicianDailyJobViewForm()
        {
            InitializeComponent();
        }

        private void addHourlyDataGridColumn()
        {
            DataGridViewTextBoxColumn hourlyTextBoxColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn totalJobColumn = new DataGridViewTextBoxColumn();

            hourlyDataGrid.Columns.Clear();

            hourlyTextBoxColumn.HeaderText = "JAM";
            hourlyTextBoxColumn.Name = "hourlyColumn";
            hourlyTextBoxColumn.ReadOnly = true;
            hourlyTextBoxColumn.Width = 150;
            hourlyDataGrid.Columns.Add(hourlyTextBoxColumn);

            totalJobColumn.HeaderText = "JUMLAH TUGAS";
            totalJobColumn.Name = "jobColumn";
            totalJobColumn.Width = 150;
            hourlyDataGrid.Columns.Add(totalJobColumn);
        }

        private void fillInHourlyJobData()
        {
            int startTime = 8;
            int endTime = 9;
            string timeMarker = "";

            for (int i =0;i<12;i++)
            {
                if (startTime < 10)
                    timeMarker = "0" + startTime.ToString() + ":00 - ";
                else
                    timeMarker = startTime.ToString() + ":00 - ";

                if (endTime < 10)
                    timeMarker = timeMarker + "0" + endTime.ToString() + ":00";
                else
                    timeMarker = timeMarker + endTime.ToString() + ":00";

                hourlyDataGrid.Rows.Add(timeMarker, "");

                startTime++;
                endTime++;
            }
        }

        private void updateHourlyJobData()
        {
            string sqlCommand = "";
            string selectedDate;
            string jobCount = "0";
            int startTime = 8;

            selectedDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(jobStartDateTimePicker.Value));

            for (int i = 0; i < 12; i++)
            {
                sqlCommand = "SELECT COUNT(1) FROM SURAT_TUGAS_HEADER WHERE DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  = '" + selectedDate + "' AND HOUR(JOB_SCHEDULED_TIME) = " + startTime;
                jobCount = DS.getDataSingleValue(sqlCommand).ToString();

                hourlyDataGrid.Rows[i].Cells["jobColumn"].Value = jobCount + " TUGAS";

                startTime++;
            }

        }

        private void technicianDailyJobViewForm_Load(object sender, EventArgs e)
        {
            jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
            jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            jobStartDateTimePicker.Value = selectedJobDate;

            errorLabel.Text = "";

            isLoading = true;
            addHourlyDataGridColumn();
            fillInHourlyJobData();
            updateHourlyJobData();
            fillInStatusListbox();
            isLoading = false;

            gUtil.reArrangeTabOrder(this);
        }

        private void fillInStatusListbox()
        {
            int numOfUnassignedSO = 0;
            int numOfJobWithoutTechnician = 0;
            string sqlCommand;
            string todayDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(DateTime.Now));
            int numOfLateJob = 0;
            int numOfPendingJob = 0;

            // CALCULATE NUMBER OF SALES ORDER WITHOUT JOB ASSIGNMENT
            sqlCommand = "SELECT COUNT(1) FROM SALES_DETAIL SD, MASTER_PRODUCT MP WHERE SD.PRODUCT_ID = MP.PRODUCT_ID AND MP.PRODUCT_IS_SERVICE = 1 AND SD.SALES_INVOICE NOT IN (SELECT SALES_INVOICE FROM SURAT_TUGAS_HEADER) " +
                                   "GROUP BY SALES_INVOICE";
            numOfUnassignedSO = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));
            listBoxStatus.Items.Add(numOfUnassignedSO + " INVOICE BELUM DITUGASKAN");

            // CALCULATE NUMBER OF JOB ASSIGNMENT WITHOUT TECHNICIAN
            sqlCommand = "SELECT COUNT(1) FROM SURAT_TUGAS_HEADER WHERE TECHNICIAN_ID = 0";
            numOfJobWithoutTechnician = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));
            listBoxStatus.Items.Add(numOfJobWithoutTechnician + " SURAT TUGAS TANPA TEKNISI");

            // CALCULATE TOTAL NUMBER OF LATE JOB
            sqlCommand = "SELECT COUNT(1) FROM SURAT_TUGAS_HEADER WHERE DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  < '" + todayDate + "' AND IS_JOB_FINISHED = 0";
            numOfLateJob = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));
            listBoxStatus.Items.Add(numOfLateJob + " TUGAS TERLAMBAT");

            // CALCULATE TOTAL NUMBER OF PENDING JOB
            sqlCommand = "SELECT COUNT(1) FROM SURAT_TUGAS_HEADER WHERE DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  >= '" + todayDate + "' AND IS_JOB_FINISHED = 0";
            numOfPendingJob = Convert.ToInt32(DS.getDataSingleValue(sqlCommand));
            listBoxStatus.Items.Add(numOfPendingJob + " TUGAS BELUM DISELESAIKAN");
        }

        private void loadDetailHourJob(int startTime)
        {
            string sqlCommand;
            DataTable dt = new DataTable();
            MySqlDataReader rdr;
            string selectedDate = "";

            selectedDate = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(jobStartDateTimePicker.Value));

            sqlCommand = "SELECT STH.ID, SALES_INVOICE AS 'INVOICE', JOB_SCHEDULED_TIME AS 'JAM KERJA', CUSTOMER_FULL_NAME AS 'PELANGGAN', TECHNICIAN_NAME AS 'TEKNISI', IS_JOB_FINISHED, IF(IS_JOB_FINISHED =1, 'SELESAI', 'PENDING') AS STATUS " +
                                   "FROM SURAT_TUGAS_HEADER STH, MASTER_CUSTOMER MC, MASTER_TECHNICIAN MT " +
                                   "WHERE STH.CUSTOMER_ID = MC.CUSTOMER_ID AND STH.TECHNICIAN_ID = MT.ID AND DATE_FORMAT(JOB_SCHEDULED_DATE, '%Y%m%d')  = '" + selectedDate + "' AND HOUR(JOB_SCHEDULED_TIME) = " + startTime;

            using (rdr = DS.getData(sqlCommand))
            {
                jobDetailDataGrid.DataSource = null;
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                    jobDetailDataGrid.DataSource = dt;

                    jobDetailDataGrid.Columns["ID"].Visible = false;
                    jobDetailDataGrid.Columns["IS_JOB_FINISHED"].Visible = false;

                    jobDetailDataGrid.Columns["PELANGGAN"].Width = 200;
                    jobDetailDataGrid.Columns["TEKNISI"].Width = 200;

                }
                rdr.Close();
            }
        }

        private void hourlyDataGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int startTime = 8;

            if (isLoading)
                return;

            int rowSelectedIndex = (hourlyDataGrid.SelectedCells[0].RowIndex);
            DataGridViewRow selectedRow = hourlyDataGrid.Rows[rowSelectedIndex];

            for (int i = 0; i < rowSelectedIndex; i++)
                startTime++;

            loadDetailHourJob(startTime);

        }

        private void jobStartDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (hourlyDataGrid.Rows.Count < 1)
                return;

            updateHourlyJobData();
        }

        private void displaySpecificForm(int selectedRowIndex)
        {
            DataGridViewRow selectedRow = jobDetailDataGrid.Rows[selectedRowIndex];
            string noInvoice = "";

            noInvoice = selectedRow.Cells["INVOICE"].Value.ToString();

            cashierForm displayedSuratTugasForm = new cashierForm(globalConstants.MONITOR_SURAT_TUGAS, noInvoice);
            displayedSuratTugasForm.ShowDialog(this);

        }

        private void jobDetailDataGrid_DoubleClick(object sender, EventArgs e)
        {
            if (jobDetailDataGrid.Rows.Count < 1)
                return;

            int rowSelectedIndex = (jobDetailDataGrid.SelectedCells[0].RowIndex);

            displaySpecificForm(rowSelectedIndex);
        }

        private void jobDetailDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (jobDetailDataGrid.Rows.Count < 1)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                int rowSelectedIndex = (jobDetailDataGrid.SelectedCells[0].RowIndex);

                displaySpecificForm(rowSelectedIndex);
            }
        }

        private void listBoxStatus_DoubleClick(object sender, EventArgs e)
        {
            int selectedIndex = listBoxStatus.SelectedIndex;
            int moduleID = 0;

            if (selectedIndex == 0)
                moduleID = globalConstants.DISPLAY_UNASSIGNED_JOB;
            else if (selectedIndex == 1)
                moduleID = globalConstants.DISPLAY_JOB_WITH_NO_TECHNICIAN;
            else if (selectedIndex == 2)
                moduleID = globalConstants.DISPLAY_LATE_JOB;
            else
                moduleID = globalConstants.DISPLAY_PENDING_JOB;

            genericDataGridForm displayForm = new genericDataGridForm(moduleID);
            displayForm.ShowDialog(this);
        }
    }
}
