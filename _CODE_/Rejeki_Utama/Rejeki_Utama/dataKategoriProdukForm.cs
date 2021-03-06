﻿using System;
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
    public partial class dataKategoriProdukForm : Form
    {
        private int originModuleID = 0;
        private int selectedCategoryID = 0;

        private dataProdukDetailForm parentForm;
        private globalUtilities gutil = new globalUtilities();
        Data_Access DS = new Data_Access();

        public dataKategoriProdukForm()
        {
            InitializeComponent();
        }

        public dataKategoriProdukForm(int moduleID)
        {
            InitializeComponent();

            originModuleID = moduleID;

            //newButton.Visible = false;
        }

        public dataKategoriProdukForm(int moduleID, dataProdukDetailForm thisForm)
        {
            InitializeComponent();

            originModuleID = moduleID;
            parentForm = thisForm;
            //newButton.Visible = false;
        }

        private void loadKategoriData(int options = 0)
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            string sqlCommand;
            string categoryNameParam;

            if (options !=1 && categoryNameTextBox.Text.Equals(""))
                return;

            DS.mySqlConnect();
            if (options == 1)
            {
                sqlCommand = "SELECT CATEGORY_ID, CATEGORY_NAME AS 'NAMA KATEGORI', CATEGORY_DESCRIPTION AS 'DESKRIPSI KATEGORI' FROM MASTER_CATEGORY";
            }
            else
            {
                categoryNameParam = MySqlHelper.EscapeString(categoryNameTextBox.Text);
                if (tagnonactiveoption.Checked == true)
                {
                    sqlCommand = "SELECT CATEGORY_ID, CATEGORY_NAME AS 'NAMA KATEGORI', CATEGORY_DESCRIPTION AS 'DESKRIPSI KATEGORI' FROM MASTER_CATEGORY WHERE CATEGORY_NAME LIKE '%" + categoryNameParam + "%'";
                }
                else
                {
                    sqlCommand = "SELECT CATEGORY_ID, CATEGORY_NAME AS 'NAMA KATEGORI', CATEGORY_DESCRIPTION AS 'DESKRIPSI KATEGORI' FROM MASTER_CATEGORY WHERE CATEGORY_ACTIVE = 1 AND CATEGORY_NAME LIKE '%" + categoryNameParam + "%'";
                }
            }

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    dt.Load(rdr);

                    kategoriProdukDataGridView.DataSource = dt;

                    kategoriProdukDataGridView.Columns["CATEGORY_ID"].Visible = false;
                    kategoriProdukDataGridView.Columns["NAMA KATEGORI"].Width = 200;
                    kategoriProdukDataGridView.Columns["DESKRIPSI KATEGORI"].Width = 300;
                }
            }
        }

        private void displaySpecificForm()
        {
            int selectedrowindex = kategoriProdukDataGridView.SelectedCells[0].RowIndex;

            DataGridViewRow selectedRow = kategoriProdukDataGridView.Rows[selectedrowindex];
            selectedCategoryID = Convert.ToInt32(selectedRow.Cells["CATEGORY_ID"].Value);
            
            switch(originModuleID)
            {
                case globalConstants.PRODUK_DETAIL_FORM:
                    parentForm.addSelectedKategoriID(selectedCategoryID);
                    this.Close();
                    break;

                case globalConstants.PENGATURAN_KATEGORI_PRODUK:
                    pengaturanKategoriProdukForm pengaturanKategoriForm = new pengaturanKategoriProdukForm(selectedCategoryID);
                    pengaturanKategoriForm.ShowDialog(this);
                    break;

                default:
                    dataKategoriProdukDetailForm displayedForm = new dataKategoriProdukDetailForm(globalConstants.EDIT_CATEGORY, selectedCategoryID);
                    displayedForm.ShowDialog(this);
                    break;
            }
        }

        private void newButton_Click_1(object sender, EventArgs e)
        {
            dataKategoriProdukDetailForm displayForm = new dataKategoriProdukDetailForm(globalConstants.NEW_CATEGORY);
            displayForm.ShowDialog(this);
        }

        private void tagProdukDataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (kategoriProdukDataGridView.Rows.Count > 0)
                displaySpecificForm();
        }

        private void categoryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!categoryNameTextBox.Text.Equals(""))
            {
                loadKategoriData();
            }
        }

        private void dataKategoriProdukForm_Activated(object sender, EventArgs e)
        {
            if (!categoryNameTextBox.Text.Equals(""))
            {
                loadKategoriData();
            }
        }

        private void dataKategoriProdukForm_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;
            gutil.reArrangeTabOrder(this);

            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_KATEGORI, gutil.getUserGroupID());

            if (userAccessOption == 2 || userAccessOption == 6)
                newButton.Visible = true;
            else
                newButton.Visible = false;

            categoryNameTextBox.Select();
        }

        private void groupnonactiveoption_CheckedChanged(object sender, EventArgs e)
        {
            kategoriProdukDataGridView.DataSource = null;
            if (!categoryNameTextBox.Text.Equals(""))
            {
                loadKategoriData();
            }
        }

        private void kategoriProdukDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (kategoriProdukDataGridView.Rows.Count > 0)
                    displaySpecificForm();
        }

		private void AllButton_Click(object sender, EventArgs e)
        {
            loadKategoriData(1);
        }
		
    }
}
