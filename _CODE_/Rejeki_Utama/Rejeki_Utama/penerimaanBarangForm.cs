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
using System.Globalization;
using Hotkeys;

namespace AlphaSoft
{
    public partial class penerimaanBarangForm : Form
    {
        string selectedInvoice = "";
        string selectedMutasi = "";
        int originModuleId = 0;
        int selectedFromID = 0;
        int selectedToID = 0;
        double globalTotalValue = 0;
        bool isLoading = false;
        double POduration = 0;

        private Hotkeys.GlobalHotkey ghk_F1;
        private Hotkeys.GlobalHotkey ghk_F2;
        private Hotkeys.GlobalHotkey ghk_F8;
        private Hotkeys.GlobalHotkey ghk_F9;
        private Hotkeys.GlobalHotkey ghk_F11;

        private Hotkeys.GlobalHotkey ghk_CTRL_DEL;
        private Hotkeys.GlobalHotkey ghk_CTRL_ENTER;

        Button[] arrButton = new Button[3];
        int locationID = 0;

        globalUtilities gUtil = new globalUtilities();
        Data_Access DS = new Data_Access();
        private CultureInfo culture = new CultureInfo("id-ID");

        public penerimaanBarangForm()
        {
            InitializeComponent();
        }

        private void captureAll(Keys key)
        {
            switch (key)
            {
                case Keys.F1:
                    penerimaanBarangHelpForm displayHelp = new penerimaanBarangHelpForm();
                    displayHelp.ShowDialog(this);
                    break;

                case Keys.F2:
                    if (detailGridView.ReadOnly == false)
                    { 
                        barcodeForm displayBarcodeForm = new barcodeForm(this, globalConstants.PENERIMAAN_BARANG);

                        displayBarcodeForm.Top = this.Top + 5;
                        displayBarcodeForm.Left = this.Left + 5;//(Screen.PrimaryScreen.Bounds.Width / 2) - (displayBarcodeForm.Width / 2);

                        displayBarcodeForm.ShowDialog(this);
                    }
                    break;

                case Keys.F8:
                    if (detailGridView.ReadOnly == false)
                        //if (originModuleId != globalConstants.PENERIMAAN_BARANG_DARI_PO && originModuleId != globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
                        {
                            addNewRow();
                        }
                    break;

                case Keys.F9:
                    if (saveButton.Visible == true)
                        saveButton.PerformClick();
                    break;

                case Keys.F11:
                    if (detailGridView.ReadOnly == false)
                    { 
                        dataProdukForm displayProdukForm = new dataProdukForm(globalConstants.PENERIMAAN_BARANG, this);
                        displayProdukForm.ShowDialog(this);
                    }
                    break;
            }
        }

        private void captureCtrlModifier(Keys key)
        {
            switch (key)
            {
                case Keys.Delete: // CTRL + DELETE
                    if (detailGridView.ReadOnly == false)
                    { 
                        if (DialogResult.Yes == MessageBox.Show("DELETE CURRENT ROW?", "WARNING", MessageBoxButtons.YesNo))
                        {
                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "detailDataGridView_KeyDown ATTEMPT TO DELETE ROW");
                            deleteCurrentRow();
                            calculateTotal();
                        }
                    }
                    break;

                case Keys.Enter: // CTRL + ENTER
                    if (saveButton.Visible == true)
                        saveButton.PerformClick();
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                int modifier = (int)m.LParam & 0xFFFF;

                if (modifier == Constants.NOMOD)
                    captureAll(key);
                //else if (modifier == Constants.ALT)
                //    captureAltModifier(key);
                else if (modifier == Constants.CTRL)
                    captureCtrlModifier(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            ghk_F1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F1, this);
            ghk_F1.Register();

            //ghk_F2 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F2, this);
            //ghk_F2.Register();

            ghk_F8 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F8, this);
            ghk_F8.Register();

            ghk_F9 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk_F9.Register();

            ghk_F11 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F11, this);
            ghk_F11.Register();

            ghk_CTRL_DEL = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Delete, this);
            ghk_CTRL_DEL.Register();

            ghk_CTRL_ENTER = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Enter, this);
            ghk_CTRL_ENTER.Register();

        }

        private void unregisterGlobalHotkey()
        {
            ghk_F1.Unregister();
            //ghk_F2.Unregister();
            ghk_F8.Unregister();
            ghk_F9.Unregister();
            ghk_F11.Unregister();

            ghk_CTRL_DEL.Unregister();
            ghk_CTRL_ENTER.Unregister();
        }

        public void addNewRow(bool isActive = true)
        {
            int newRowIndex = 0;
            bool allowToAdd = true;

            for (int i=0;i<detailGridView.Rows.Count && allowToAdd;i++)
            {
                if (null != detailGridView.Rows[i].Cells["productID"].Value)
                {
                    if (!gUtil.isProductIDExist(detailGridView.Rows[i].Cells["productID"].Value.ToString()))
                    {
                        allowToAdd = false;
                        newRowIndex = i;
                    }
                }
                else
                {
                    allowToAdd = false;
                    newRowIndex = i;
                }
            }

            if (allowToAdd)
            { 
                detailGridView.Rows.Add();
                newRowIndex = detailGridView.Rows.Count - 1;
            }
            else
            {
                DataGridViewRow selectedRow = detailGridView.Rows[newRowIndex];
                clearUpSomeRowContents(selectedRow, newRowIndex);
            }

            detailGridView.CurrentCell = detailGridView.Rows[newRowIndex].Cells["productName"];
                detailGridView.Select();
                detailGridView.BeginEdit(true);

        }

        public void addNewRowFromBarcode(string productID, string productName, int rowIndex = -1)
        {
            int i = 0;
            bool found = false;
            int rowSelectedIndex = 0;
            bool foundEmptyRow = false;
            int emptyRowIndex = 0;
            double currQty;
            double subTotal;
            double hpp;

            if (detailGridView.ReadOnly == true)
                return;

            if (rowIndex >= 0)
            {
                rowSelectedIndex = rowIndex;
            }
            else
            {
                // CHECK FOR EXISTING SELECTED ITEM
                for (i = 0; i < detailGridView.Rows.Count && !found && !foundEmptyRow; i++)
                {
                    if (null != detailGridView.Rows[i].Cells["productName"].Value)
                    {
                        if (detailGridView.Rows[i].Cells["productName"].Value.ToString() == productName)
                        {
                            found = true;
                            rowSelectedIndex = i;
                        }
                    }
                    else
                    {
                        foundEmptyRow = true;
                        emptyRowIndex = i;
                    }
                }

                if (!found)
                {
                    if (foundEmptyRow)
                    {
                        rowSelectedIndex = emptyRowIndex;
                    }
                    else
                    {
                        addNewRow();
                        rowSelectedIndex = detailGridView.Rows.Count - 1;
                    }
                }
            }

            DataGridViewRow selectedRow = detailGridView.Rows[rowSelectedIndex];
            updateSomeRowContents(selectedRow, rowSelectedIndex, productID);

            if (!found)
            {
                selectedRow.Cells["qtyReceived"].Value = 1;
                currQty = 1;
            }
            else
            {
                currQty = Convert.ToDouble(selectedRow.Cells["qtyReceived"].Value) + 1;
                selectedRow.Cells["qtyReceived"].Value = currQty;
            }

            hpp = Convert.ToDouble(selectedRow.Cells["hpp"].Value);

            subTotal = Math.Round((hpp * currQty), 2);
            selectedRow.Cells["subtotal"].Value = subTotal.ToString();

            calculateTotal();

            detailGridView.Focus();

            detailGridView.CurrentCell = selectedRow.Cells["qtyReceived"];
            detailGridView.Select();
            detailGridView.BeginEdit(true);
        }

        public void setSelectedInvoice(string invoiceNo)
        {
            selectedInvoice = invoiceNo;
            noInvoiceTextBox.Text = selectedInvoice;
            noMutasiTextBox.Text = "";
            selectedMutasi = "";
            originModuleId = globalConstants.PENERIMAAN_BARANG_DARI_PO;

            initializeScreen();

            isLoading = true;

            addDataGridColumn();
            loadDataHeader();
            loadDataDetail();

            supplierCombo.Text = getSupplierName(selectedFromID);
            durationTextBox.ReadOnly = true;
            durationTextBox.Text = POduration.ToString();
            durationTextBox.Visible = true;
            label1.Visible = true;
            isLoading = false;

            if (selectedInvoice.Length > 0)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PO INVOICE TO RECEIVE [" + selectedInvoice + "]");
                supplierCombo.Enabled = false;
            }
        }

        public void setSelectedMutasi(string mutasiNo)
        {
            selectedMutasi = mutasiNo;
            noInvoiceTextBox.Text = "";
            selectedInvoice = "";
            noMutasiTextBox.Text = selectedMutasi;
            originModuleId = globalConstants.PENERIMAAN_BARANG_DARI_MUTASI;

            initializeScreen();

            isLoading = true;

            addDataGridColumn();
            loadDataHeader();
            loadDataDetail();

            //supplierCombo.Text = getBranchName(selectedFromID);
            branchToTextBox.Text = getBranchName(selectedToID);
            
            //durationTextBox.ReadOnly = true;
            durationTextBox.Visible = false;
            label1.Visible = false;
            isLoading = false;

            if (selectedMutasi.Length > 0)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "NO MUTASI TO RECEIVE [" + selectedMutasi + "]");
                supplierCombo.Enabled = false;
            }
        }

        private void initializeScreen()
        {
            switch (originModuleId)
            {
                case globalConstants.PENERIMAAN_BARANG_DARI_MUTASI:
                    //labelNo.Text = "NO MUTASI";
                    //labelTanggal.Text = "TANGGAL MUTASI";
                    labelTujuan.Text = "TUJUAN MUTASI";

                    labelAsal.Visible = false;
                    labelAsal_1.Visible = false;
                    supplierCombo.Visible = false;

                    labelTujuan.Visible = true;
                    labelTujuan_1.Visible = true;
                    branchToTextBox.Visible = true;

                    durationTextBox.Visible = false;
                    label1.Visible = false;
                    
                    break;

                case globalConstants.PENERIMAAN_BARANG_DARI_PO:
                    labelNo.Text = "NO PO";
                    //labelTanggal.Text = "TANGGAL PO";
                    labelAsal.Text = "SUPPLIER";
                    labelAsal.Visible = true;
                    labelAsal_1.Visible = true;
                    supplierCombo.Visible = true;

                    //labelTujuan.Text = "TUJUAN MUTASI";
                    labelTujuan.Visible = false;
                    labelTujuan_1.Visible = false;
                    branchToTextBox.Visible = false;

                    durationTextBox.Visible = true;
                    label1.Visible = true;
                    

                    break;
            }
        }

        private void loadDataHeader()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            switch (originModuleId)
            {
                case globalConstants.PENERIMAAN_BARANG_DARI_MUTASI:
                    sqlCommand = "SELECT * FROM PRODUCTS_MUTATION_HEADER WHERE PM_INVOICE = '" + selectedMutasi+ "'";
                    using (rdr = DS.getData(sqlCommand))
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                //noInvoiceTextBox.Text = rdr.GetString("PM_INVOICE");
                                //invoiceDtPicker.Value = rdr.GetDateTime("PM_DATETIME");
                                selectedFromID = rdr.GetInt32("BRANCH_ID_FROM");
                                selectedToID = rdr.GetInt32("BRANCH_ID_TO");

                                globalTotalValue = rdr.GetDouble("PM_TOTAL");
                                labelTotalValue.Text = globalTotalValue.ToString("C", culture);
                                labelAcceptValue.Text = globalTotalValue.ToString("C", culture);
                            }
                        }
                    }
                    break;

                case globalConstants.PENERIMAAN_BARANG_DARI_PO:
                    sqlCommand = "SELECT * FROM PURCHASE_HEADER WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'";
                    using (rdr = DS.getData(sqlCommand))
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                noInvoiceTextBox.Text = rdr.GetString("PURCHASE_INVOICE");
                                //invoiceDtPicker.Value = rdr.GetDateTime("PURCHASE_DATETIME");
                                selectedFromID = rdr.GetInt32("SUPPLIER_ID");
                                //selectedToID = rdr.GetInt32("BRANCH_ID_TO");
                                globalTotalValue = rdr.GetDouble("PURCHASE_TOTAL");
                                POduration = rdr.GetDouble("PURCHASE_TERM_OF_PAYMENT_DURATION");
                                labelTotalValue.Text = globalTotalValue.ToString("C", culture);
                                labelAcceptValue.Text = globalTotalValue.ToString("C", culture);
                            }
                        }
                    }
                    break;
            }
        }

        private void loadDataDetail()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            switch (originModuleId)
            {
                case globalConstants.PENERIMAAN_BARANG_DARI_MUTASI:
                    sqlCommand = "SELECT PM.*, M.PRODUCT_ID, M.PRODUCT_NAME FROM PRODUCTS_MUTATION_DETAIL PM, MASTER_PRODUCT M WHERE PM_INVOICE = '" + selectedMutasi + "' AND PM.PRODUCT_ID = M.PRODUCT_ID";
                    using (rdr = DS.getData(sqlCommand))
                    {
                        detailGridView.Rows.Clear();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                detailGridView.Rows.Add(rdr.GetString("PRODUCT_ID"), rdr.GetString("PRODUCT_NAME"), rdr.GetString("PRODUCT_QTY"), rdr.GetString("PRODUCT_BASE_PRICE"), rdr.GetString("PRODUCT_QTY"), rdr.GetString("PM_SUBTOTAL"));
                            }
                        }
                    }
                    break;

                case globalConstants.PENERIMAAN_BARANG_DARI_PO:
                    sqlCommand = "SELECT PO.*, M.PRODUCT_NAME FROM PURCHASE_DETAIL PO, MASTER_PRODUCT M WHERE PURCHASE_INVOICE = '" + selectedInvoice + "' AND PO.PRODUCT_ID = M.PRODUCT_ID";
                    using (rdr = DS.getData(sqlCommand))
                    {
                        detailGridView.Rows.Clear();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                detailGridView.Rows.Add(rdr.GetString("PRODUCT_ID"), rdr.GetString("PRODUCT_NAME"), rdr.GetString("PRODUCT_QTY"), rdr.GetString("PRODUCT_PRICE"), rdr.GetString("PRODUCT_QTY"), rdr.GetString("PURCHASE_SUBTOTAL"));
                            }
                        }
                    }
                    break;
            }
        }

        private string getBranchName(int branchID)
        {
            string result = "";

            if (branchID != 0)
                result = DS.getDataSingleValue("SELECT BRANCH_NAME FROM MASTER_BRANCH WHERE BRANCH_ID = " + branchID).ToString();

            return result;
        }

        private string getSupplierName(int suppID)
        {
            string result = "";

            if (suppID != 0)
                result = DS.getDataSingleValue("SELECT SUPPLIER_FULL_NAME FROM MASTER_SUPPLIER WHERE SUPPLIER_ID = " + suppID).ToString();

            return result;
        }

        private void fillInSupplierCombo()
        {
            MySqlDataReader rdr;
            string sqlCommand;

            sqlCommand = "SELECT SUPPLIER_ID, SUPPLIER_FULL_NAME FROM MASTER_SUPPLIER WHERE SUPPLIER_ACTIVE = 1";

            supplierCombo.Items.Clear();
            supplierHiddenCombo.Items.Clear();

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        supplierCombo.Items.Add(rdr.GetString("SUPPLIER_FULL_NAME"));
                        supplierHiddenCombo.Items.Add(rdr.GetString("SUPPLIER_ID"));
                    }
                }
            }
        }

        private void addDataGridColumn()
        {
            DataGridViewTextBoxColumn productID_textBox = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn namaProduct_textBox = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn qtyRequested_textBox = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn hpp_textBox = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn qty_textBox = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn subtotal_textBox = new DataGridViewTextBoxColumn();

            detailGridView.Columns.Clear();
            //detailRequestQty.Clear();
            //detailHpp.Clear();

            productID_textBox.Name = "productID";
            productID_textBox.HeaderText = "KODE PRODUK";
            productID_textBox.Width = 150;
            productID_textBox.DefaultCellStyle.BackColor = Color.LightBlue;
            productID_textBox.Visible = false;
            detailGridView.Columns.Add(productID_textBox);

			namaProduct_textBox.Name = "productName";
			namaProduct_textBox.HeaderText = "NAMA PRODUK";
            namaProduct_textBox.Width = 250;
            namaProduct_textBox.DefaultCellStyle.BackColor = Color.LightBlue;
            detailGridView.Columns.Add(namaProduct_textBox);

            if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO || originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
            {
                qtyRequested_textBox.Name = "qtyRequest";
                qtyRequested_textBox.HeaderText = "QTY";
                qtyRequested_textBox.ReadOnly = true;
                qtyRequested_textBox.Width = 150;
                detailGridView.Columns.Add(qtyRequested_textBox);
            }
            else
            {
                //detailRequestQty.Add("0");
                //detailHpp.Add("0");
                //subtotalList.Add("0");
            }

            hpp_textBox.Name = "hpp";
            hpp_textBox.HeaderText = "HARGA POKOK";
            hpp_textBox.Width = 200;
            hpp_textBox.DefaultCellStyle.BackColor = Color.LightBlue;
            detailGridView.Columns.Add(hpp_textBox);

            qty_textBox.Name = "qtyReceived";
            qty_textBox.HeaderText = "QTY DITERIMA";
            qty_textBox.Width = 200;
            qty_textBox.DefaultCellStyle.BackColor = Color.LightBlue;
            detailGridView.Columns.Add(qty_textBox);

            subtotal_textBox.Name = "subtotal";
            subtotal_textBox.HeaderText = "SUBTOTAL";
            subtotal_textBox.ReadOnly = true;
            subtotal_textBox.Width = 150;
            detailGridView.Columns.Add(subtotal_textBox);
        }

        private void penerimaanBarangForm_Load(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            PRDtPicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;
            int userAccessOption = 0;

            locationID = gUtil.loadlocationID(2);
            if (locationID <= 0)
            {
                MessageBox.Show("LOCATION ID BELUM DI SET");
                this.Close();
            }

            //initializeScreen();
            labelTotal.Visible = false;
            labelTotal_1.Visible = false;
            labelTotalValue.Visible = false;

            addDataGridColumn();
            detailGridView.EditingControlShowing += detailGridView_EditingControlShowing;
            
            fillInSupplierCombo();

            //userAccessOption = DS.getUserAccessRight(globalConstants.MENU_PENERIMAAN_BARANG_DARI_MUTASI, gUtil.getUserGroupID());
            //if (userAccessOption == 1)
            //    searchMutasiButton.Visible = true;
            //else
            //    searchMutasiButton.Visible = false;

            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_PENERIMAAN_BARANG_DARI_PO, gUtil.getUserGroupID());
            if (userAccessOption == 1)
                searchPOButton.Visible = true;
            else
                searchPOButton.Visible = false;

            arrButton[0] = saveButton;
            arrButton[1] = reprintButton;
            arrButton[2] = resetButton;
            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);
            
            gUtil.reArrangeTabOrder(this);

            //detailHpp.Add("0");
            //detailRequestQty.Add("0");
            //subtotalList.Add("0");

            prInvoiceTextBox.Select();
        }

        private double getHPPValue(string productID)
        {
            double result = 0;

            //DS.mySqlConnect();

            result = Convert.ToDouble(DS.getDataSingleValue("SELECT PRODUCT_BASE_PRICE FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

            return result;
        }

        private void setTextBoxCustomSource(TextBox textBox)
        {
            MySqlDataReader rdr;
            string sqlCommand = "";
            string[] arr = null;
            List<string> arrList = new List<string>();

            sqlCommand = "SELECT PRODUCT_ID FROM MASTER_PRODUCT WHERE PRODUCT_ACTIVE = 1";
            rdr = DS.getData(sqlCommand);

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    arrList.Add(rdr.GetString("PRODUCT_ID"));
                }
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                arr = arrList.ToArray();
                collection.AddRange(arr);

                textBox.AutoCompleteCustomSource = collection;
            }

            rdr.Close();
        }

        private void detailGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((detailGridView.CurrentCell.OwningColumn.Name == "hpp" || detailGridView.CurrentCell.OwningColumn.Name == "qtyReceived") && e.Control is TextBox)
            {
                TextBox textBox = e.Control as TextBox;
                textBox.AutoCompleteMode = AutoCompleteMode.None;
            }

            if ((detailGridView.CurrentCell.OwningColumn.Name == "productName") && e.Control is TextBox)
            {
                TextBox productIDTextBox = e.Control as TextBox;
                productIDTextBox.PreviewKeyDown -= TextBox_previewKeyDown;
                productIDTextBox.PreviewKeyDown += TextBox_previewKeyDown;

                productIDTextBox.CharacterCasing = CharacterCasing.Upper;

                productIDTextBox.AutoCompleteMode = AutoCompleteMode.None;
                //productIDTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //productIDTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //setTextBoxCustomSource(productIDTextBox);
            }
        }

        private void clearUpSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex)
        {
            isLoading = true;
            selectedRow.Cells["productName"].Value = "";
            selectedRow.Cells["hpp"].Value = "0";
            selectedRow.Cells["subtotal"].Value = "0";
            if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO || originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
            { 
                selectedRow.Cells["qtyRequest"].Value = "0";
                
            }
            selectedRow.Cells["qtyReceived"].Value = "0";

            calculateTotal();
            isLoading = false;
        }

        private void updateSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex, string currentValue)
        {
            int numRow = 0;
            string selectedProductID = "";
            string selectedProductName = "";

            double hpp = 0;
            string currentProductID = "";
            string currentProductName = "";
            bool changed = false;

            numRow = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + currentValue + "'"));

            if (numRow > 0)
            {
                selectedProductID = currentValue;

                if (null != selectedRow.Cells["productID"].Value)
                    currentProductID = selectedRow.Cells["productID"].Value.ToString();

                if (null != selectedRow.Cells["productName"].Value)
                    currentProductName = selectedRow.Cells["productName"].Value.ToString();

                selectedProductName = DS.getDataSingleValue("SELECT IFNULL(PRODUCT_NAME,'') FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + currentValue + "'").ToString();

                selectedRow.Cells["productId"].Value = selectedProductID;
                selectedRow.Cells["productName"].Value = selectedProductName;

                if (selectedProductID != currentProductID)
                    changed = true;

                if (selectedProductName != currentProductName)
                    changed = true;

                if (!changed)
                    return;

                hpp = getHPPValue(selectedProductID);
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, PRODUCT_BASE_PRICE [" + hpp + "]");
                isLoading = true;
                selectedRow.Cells["hpp"].Value = hpp.ToString();

                selectedRow.Cells["qtyReceived"].Value = 0;

                selectedRow.Cells["subtotal"].Value = 0;
                isLoading = false;
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, attempt to calculate total");

                calculateTotal();
            }
            else
            {
                clearUpSomeRowContents(selectedRow, rowSelectedIndex);
            }
        }

        private void TextBox_previewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string currentValue = "";
            int rowSelectedIndex = 0;
            DataGridViewTextBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            if (detailGridView.CurrentCell.OwningColumn.Name != "productName")
                return;

            if (e.KeyCode == Keys.Enter)
            {
                currentValue = dataGridViewComboBoxEditingControl.Text;
                rowSelectedIndex = detailGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = detailGridView.Rows[rowSelectedIndex];

                if (currentValue.Length > 0)
                {
                    POSSearchProductForm browseProduk = new POSSearchProductForm(globalConstants.PENERIMAAN_BARANG, this, currentValue, rowSelectedIndex);
                    browseProduk.ShowDialog(this);
                }
                else
                {
                }
            }
        }

        private void calculateTotal()
        {
            double total = 0;
            for (int i =0;i<detailGridView.Rows.Count;i++)
            {
                total = total + Convert.ToDouble(detailGridView.Rows[i].Cells["subTotal"].Value);
            }

            globalTotalValue = total;
            labelAcceptValue.Text = globalTotalValue.ToString("c", culture);
        }

        private bool isNoPRExist()
        {
            bool result = false;

            if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM PRODUCTS_RECEIVED_HEADER WHERE PR_INVOICE = '" + MySqlHelper.EscapeString(prInvoiceTextBox.Text) + "'")) > 0)
                result = true;

            return result;
        }

        private void prInvoiceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (prInvoiceTextBox.Text.Length > 0)
                if (isNoPRExist())
                {
                    errorLabel.Text = "NO PENERIMAAN SUDAH ADA";
                }
                else
                    errorLabel.Text = "";
        }

        private double getCurrentHpp(string productID)
        {
            double result = 0;

            result = Convert.ToDouble(DS.getDataSingleValue("SELECT PRODUCT_BASE_PRICE FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

            return result;
        }

        private bool dataValidated()
        {
            bool dataExist = true;
            bool validInput = true;
            int i = 0;

            if (prInvoiceTextBox.Text.Length <=0)
            {
                errorLabel.Text = "NO PENERIMAAN TIDAK BOLEH KOSONG";
                return false;
            }

            if (detailGridView.Rows.Count <= 0)
            {
                errorLabel.Text = "TIDAK ADA PRODUCT YANG DITERIMA";
                return false;
            }

            for (i = 0; i < detailGridView.Rows.Count && dataExist && validInput; i ++)
            {
                if (null != detailGridView.Rows[i].Cells["hpp"].Value)
                    validInput = gUtil.matchRegEx(detailGridView.Rows[i].Cells["hpp"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                else
                    validInput = false;

                if (null != detailGridView.Rows[i].Cells["qtyReceived"].Value)
                    validInput = gUtil.matchRegEx(detailGridView.Rows[i].Cells["qtyReceived"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                else
                    validInput = false;

                if (null != detailGridView.Rows[i].Cells["productName"].Value)
                    dataExist = gUtil.isProductNameExist(detailGridView.Rows[i].Cells["productName"].Value.ToString());
                else
                    dataExist = false;
            }

            if (!validInput)
            {
                errorLabel.Text = "INPUT PADA BARIS [" + i + "] INVALID";
                return false;
            }

            if (!dataExist)
            {
                errorLabel.Text = "NAMA PRODUK PADA BARIS ["+ i +"] INVALID";
                return false;
            }

            if (globalTotalValue <= 0)
            {
                errorLabel.Text = "NILAI BARANG YANG DITERIMA TIDAK BOLEH NOL";
                return false;
            }

            return true;
        }
       
        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;

            string PRInvoice = "";
            int branchIDFrom = 0;
            int branchIDTo = 0;
            string PRDateTime = "";
            double PRTotal = 0;
            double currentHPP = 0;
            double newHPP = 0;
            int priceChange = 0;
            double qtyRequest = 0;

            DateTime PODueDate;
            string PODueDateTime = "";
            double termOfPaymentDuration = 0;
            int termOfPayment = 0;

            double currentTaxTotal = 0;
            double currentPurchaseTotal = 0;
            double taxLimitValue = 0;
            double parameterCalculation = 0;
            int taxLimitType = 0; // 0 - percentage, 1 - amount
            string purchaseDateValue = "";
            bool addToTaxTable = false;

            string selectedDate = PRDtPicker.Value.ToShortDateString();
            PRDateTime = String.Format(culture, "{0:dd-MM-yyyy}", Convert.ToDateTime(selectedDate));
            
            PRInvoice = prInvoiceTextBox.Text;
            branchIDFrom = selectedFromID;
            branchIDTo = selectedToID;
            PRTotal = globalTotalValue;

            if (originModuleId == 0) // direct penerimaan barang
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "DIRECT PENERIMAAN BARANG, THEREFORE CALCULATE TAX LIMIT");

                termOfPaymentDuration = Convert.ToDouble(durationTextBox.Text);
                PODueDate = PRDtPicker.Value.AddDays(termOfPaymentDuration);
                PODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", PODueDate);
                termOfPayment = 1;
                selectedInvoice = PRInvoice;

                // TAX LIMIT CALCULATION
                // ----------------------------------------------------------------------
                purchaseDateValue = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);
                currentTaxTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PURCHASE_TOTAL), 0) AS TOTAL FROM PURCHASE_HEADER_TAX WHERE DATE_FORMAT(PURCHASE_DATETIME, '%Y%m%d') = '" + purchaseDateValue + "'"));
                currentPurchaseTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PURCHASE_TOTAL), 0) AS TOTAL FROM PURCHASE_HEADER WHERE DATE_FORMAT(PURCHASE_DATETIME, '%Y%m%d') = '" + purchaseDateValue + "'"));

                // CHECK WHETHER THE PARAMETER FOR TAX CALCULATION HAS BEEN SET
                taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PERSENTASE_PEMBELIAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "CHECK IF TAX LIMIT SET FOR PERCENTAGE PURCHASE [" + taxLimitValue + "]");

                if (taxLimitValue == 0)
                {
                    taxLimitType = 1;
                    taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(AVERAGE_PEMBELIAN_HARIAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "CHECK IF TAX LIMIT SET FOR AVERAGE DAILY PURCHASE [" + taxLimitValue + "]");

                    if (taxLimitValue != 0)
                        addToTaxTable = true;
                }
                else
                    addToTaxTable = true;

                // CHECK WHETHER THE PARAMETER HAS BEEN FULFILLED
                if (addToTaxTable)
                {
                    if (taxLimitType == 0) // PERCENTAGE CALCULATION
                    {
                        parameterCalculation = currentPurchaseTotal * taxLimitValue / 100;
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PERCENTAGE CALCULATION [" + parameterCalculation + "]");

                        if (currentTaxTotal > parameterCalculation)
                        {
                            addToTaxTable = false;
                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "CURRENT TAX TOTAL IS BIGGER THAN PARAMETER CALCULATION");
                        }
                    }
                    else // AMOUNT CALCULATION
                    {
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "AMOUNT CALCULATION [" + taxLimitValue + "]");
                        if (currentTaxTotal > taxLimitValue)
                        { 
                            addToTaxTable = false;
                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "CURRENT TAX TOTAL IS BIGGER THAN AMOUNT CALCULATION");
                        }
                    }
                }
                // ----------------------------------------------------------------------
            }

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // SAVE HEADER TABLE
                if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
                {
                    sqlCommand = "INSERT INTO PRODUCTS_RECEIVED_HEADER (PR_INVOICE, PR_FROM, PR_TO, PR_DATE, PR_TOTAL, PM_INVOICE) " +
                                        "VALUES ('" + PRInvoice + "', " + branchIDFrom + ", " + branchIDTo + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), " + gUtil.validateDecimalNumericInput(PRTotal) + ", '" + selectedMutasi + "')";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT PENERIMAAN BARANG DARI MUTASI [" + PRInvoice + "]");
                }
                else //if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO)
                {
                    sqlCommand = "INSERT INTO PRODUCTS_RECEIVED_HEADER (PR_INVOICE, PR_FROM, PR_TO, PR_DATE, PR_TOTAL, PURCHASE_INVOICE) " +
                                        "VALUES ('" + PRInvoice + "', " + branchIDFrom + ", " + branchIDTo + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), " + gUtil.validateDecimalNumericInput(PRTotal) + ", '" + selectedInvoice + "')";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT PENERIMAAN BARANG [" + PRInvoice + "]");
                }
                //else
                //    sqlCommand = "INSERT INTO PRODUCTS_RECEIVED_HEADER (PR_INVOICE, PR_FROM, PR_TO, PR_DATE, PR_TOTAL, PURCHASE_INVOICE) " +
                //                        "VALUES ('" + PRInvoice + "', " + branchIDFrom + ", " + branchIDTo + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), " + PRTotal + ")";

                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                if (originModuleId == 0 ) // direct penerimaan barang
                {
                    selectedInvoice = PRInvoice;
                    // CREATE ENTRY AT PO
                    sqlCommand = "INSERT INTO PURCHASE_HEADER (PURCHASE_INVOICE, SUPPLIER_ID, PURCHASE_DATETIME, PURCHASE_TOTAL, PURCHASE_TERM_OF_PAYMENT, PURCHASE_TERM_OF_PAYMENT_DURATION, PURCHASE_DATE_RECEIVED, PURCHASE_TERM_OF_PAYMENT_DATE, PURCHASE_SENT, PURCHASE_RECEIVED) " +
                                        "VALUES ('" + PRInvoice + "', " + branchIDFrom + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), " + gUtil.validateDecimalNumericInput(PRTotal) + ", 1, " + termOfPaymentDuration + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), STR_TO_DATE('" + PODueDateTime + "', '%d-%m-%Y'), 1, 1)";

                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT PO DATA BECAUSE OF DIRECT PENERIMAAN [" + PRInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    if (addToTaxTable)
                    {
                        sqlCommand = "INSERT INTO PURCHASE_HEADER_TAX (PURCHASE_INVOICE, SUPPLIER_ID, PURCHASE_DATETIME, PURCHASE_TOTAL, PURCHASE_TERM_OF_PAYMENT, PURCHASE_TERM_OF_PAYMENT_DURATION, PURCHASE_DATE_RECEIVED, PURCHASE_TERM_OF_PAYMENT_DATE, PURCHASE_SENT, PURCHASE_RECEIVED) " +
                                            "VALUES ('" + PRInvoice + "', " + branchIDFrom + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), " + gUtil.validateDecimalNumericInput(PRTotal) + ", 1, " + termOfPaymentDuration + ", STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), STR_TO_DATE('" + PODueDateTime + "', '%d-%m-%Y'), 1, 1)";

                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "ADD TO TAX TABLE [" + PRInvoice + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                }

                // SAVE DETAIL TABLE
                for (int i = 0; i < detailGridView.Rows.Count; i++)
                {
                    if (null != detailGridView.Rows[i].Cells["productID"].Value)
                    {
                        newHPP = Convert.ToDouble(detailGridView.Rows[i].Cells["hpp"].Value);
                        if (originModuleId != 0)
                        { 
                            currentHPP = getCurrentHpp(detailGridView.Rows[i].Cells["productID"].Value.ToString());

                            if (currentHPP > newHPP)
                                priceChange = -1;
                            else if (currentHPP == newHPP)
                                priceChange = 0;
                            else if (currentHPP < newHPP)
                                priceChange = 1;

                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PRICE CHANGE PARAM [" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "] [" + priceChange + "]");
                        }
                        else
                        {
                            priceChange = 0;
                        }

                        if (originModuleId != 0)
                            qtyRequest = Convert.ToDouble(detailGridView.Rows[i].Cells["qtyRequest"].Value);
                        else
                            // DIRECT PENERIMAAN,  QTY REQUEST = QTY RECEIVED
                            qtyRequest = Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value);

                        sqlCommand = "INSERT INTO PRODUCTS_RECEIVED_DETAIL (PR_INVOICE, PRODUCT_ID, PRODUCT_BASE_PRICE, PRODUCT_QTY, PRODUCT_ACTUAL_QTY, PR_SUBTOTAL, PRODUCT_PRICE_CHANGE) VALUES " +
                                            "('" + PRInvoice + "', '" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "', " + newHPP + ", " +  qtyRequest + ", " + Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value) + ", " + gUtil.validateDecimalNumericInput(Convert.ToDouble(detailGridView.Rows[i].Cells["subtotal"].Value)) + ", " + priceChange + ")";

                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "SAVE DETAIL PENERIMAAN [" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + ", "+newHPP+", " +qtyRequest+"]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                        
                        // UPDATE TO PRODUCT LOCATION
                        sqlCommand = "UPDATE PRODUCT_LOCATION SET PRODUCT_LOCATION_QTY = PRODUCT_LOCATION_QTY + " + Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value) + " WHERE PRODUCT_ID = '" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "' AND LOCATION_ID = "+ locationID;
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PRODUCT LOCATION DATA [" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "'" + locationID + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        // UPDATE TO MASTER PRODUCT
                        sqlCommand = "UPDATE MASTER_PRODUCT SET PRODUCT_BASE_PRICE = " + newHPP + ", PRODUCT_STOCK_QTY = PRODUCT_STOCK_QTY + " + Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value) + " WHERE PRODUCT_ID = '" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "'";
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE MASTER PRODUCT DATA [" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        if (originModuleId == 0) // DIRECT PENERIMAAN BARANG
                        {
                            sqlCommand = "INSERT INTO PURCHASE_DETAIL (PURCHASE_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_QTY, PURCHASE_SUBTOTAL) VALUES " +
                                                "('" + PRInvoice + "', '" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "', " + newHPP + ", " + Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value) + ", " + gUtil.validateDecimalNumericInput(Convert.ToDouble(detailGridView.Rows[i].Cells["subtotal"].Value)) + ")";

                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT TO PURCHASE DETAIL [" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;

                            if (addToTaxTable)
                            {
                                sqlCommand = "INSERT INTO PURCHASE_DETAIL_TAX (PURCHASE_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_QTY, PURCHASE_SUBTOTAL) VALUES " +
                                                    "('" + PRInvoice + "', '" + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "', " + newHPP + ", " + Convert.ToDouble(detailGridView.Rows[i].Cells["qtyReceived"].Value) + ", " + gUtil.validateDecimalNumericInput(Convert.ToDouble(detailGridView.Rows[i].Cells["subtotal"].Value)) + ")";

                                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT TO TAX DETAIL [" + PRInvoice + ", " + detailGridView.Rows[i].Cells["productID"].Value.ToString() + "]");

                                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                    throw internalEX;
                            }
                        }
                    }
                }
                
                // UPDATE PRODUCT MUTATION / PO TABLE
                if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
                {
                    sqlCommand = "UPDATE PRODUCTS_MUTATION_HEADER SET PM_RECEIVED = 1 WHERE PM_INVOICE = '" + selectedMutasi + "'";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PRODUCT MUTATION HEADER [" + selectedMutasi + "]");

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                else if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO)
                {
                    sqlCommand = "UPDATE PURCHASE_HEADER SET PURCHASE_RECEIVED = 1 WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PURCHASE HEADER [" + selectedInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // ATTEMPT UPDATE AT TAX TABLE
                    sqlCommand = "UPDATE PURCHASE_HEADER_TAX SET PURCHASE_RECEIVED = 1 WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PURCHASE HEADER TAX [" + selectedInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                //INSERT INTO DEBT TABLE and UPDATE DUE DATE FOR PO
                if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO)
                {
                    //termOfPayment = Convert.ToInt32(DS.getDataSingleValue("SELECT PURCHASE_TERM_OF_PAYMENT FROM PURCHASE_HEADER WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'"));

                    //if (termOfPayment == 1)
                    {
                        // UPDATE PURCHASE HEADER
                        termOfPaymentDuration = Convert.ToInt32(DS.getDataSingleValue("SELECT PURCHASE_TERM_OF_PAYMENT_DURATION FROM PURCHASE_HEADER WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'"));
                        PODueDate = PRDtPicker.Value.AddDays(termOfPaymentDuration);
                        PODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", PODueDate);

                        sqlCommand = "UPDATE PURCHASE_HEADER SET PURCHASE_DATE_RECEIVED = STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), PURCHASE_TERM_OF_PAYMENT_DATE = STR_TO_DATE('" + PODueDateTime + "', '%d-%m-%Y') WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'";
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PURCHASE HEADER DUE DATE [" + selectedInvoice + "]");

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                        // ATTEMPT UPDATE AT TAX TABLE
                        sqlCommand = "UPDATE PURCHASE_HEADER_TAX SET PURCHASE_DATE_RECEIVED = STR_TO_DATE('" + PRDateTime + "', '%d-%m-%Y'), PURCHASE_TERM_OF_PAYMENT_DATE = STR_TO_DATE('" + PODueDateTime + "', '%d-%m-%Y') WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'";
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "UPDATE PURCHASE HEADER TAX DUE DATE [" + selectedInvoice + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;

                    }
                }

                if (originModuleId != globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)// && termOfPayment == 1)
                {
                    int purchasePaid = 0;

                    purchasePaid = Convert.ToInt32(DS.getDataSingleValue("SELECT PURCHASE_PAID FROM PURCHASE_HEADER WHERE PURCHASE_INVOICE = '" + selectedInvoice + "'"));

                    // INSERT INTO DEBT TABLE
                    sqlCommand = "INSERT INTO DEBT (PURCHASE_INVOICE, DEBT_DUE_DATE, DEBT_NOMINAL, DEBT_PAID) VALUES ('" + selectedInvoice + "', STR_TO_DATE('" + PODueDateTime + "', '%d-%m-%Y'), " + gUtil.validateDecimalNumericInput(PRTotal) + ", " + purchasePaid + ")";
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "INSERT INTO DEBT [" + selectedInvoice + "]");

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "EXCEPTION THROWN [" + e.Message + "]");

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
            bool result;
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

            return false;
        }

        private bool updateDataToHQ(Data_Access DAccess)
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;

            string pmInvoice = "";
            pmInvoice = noMutasiTextBox.Text;

            DAccess.beginTransaction(Data_Access.HQ_SERVER);

            try
            {
                // UPDATE PM DATA AT HQ
                sqlCommand = "UPDATE PRODUCTS_MUTATION_HEADER SET PM_RECEIVED = 1 WHERE PM_INVOICE = '" + pmInvoice + "'";
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "ATTEMPT TO UPDATE PRODUCTS MUTATION HEADER TO INDICATE RECEIVED [" + pmInvoice + "]");
                if (!DAccess.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;
                
                DAccess.commit();
                result = true;
            }
            catch (Exception e)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "EXCEPTION THROWN ["+e.Message+"]");
                try
                {
                    DAccess.rollBack();
                }
                catch (MySqlException ex)
                {
                    if (DAccess.getMyTransConnection() != null)
                    {
                        gUtil.showDBOPError(ex, "ROLLBACK");
                    }
                }

                gUtil.showDBOPError(e, "INSERT");
                result = false;
            }
            finally
            {
                DAccess.mySqlClose();
            }

            return result;
        }

        private bool sendUpdateToHQ()
        {
            bool result = false;
            //Data_Access DS_HQ = new Data_Access();

            if (gUtil.checkLocalIPAddressFound(DS.getHQ_IPServer()) || DS.getHQ_IPServer() == "127.0.0.1")
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "HQ IP AND LOCAL IP ARE THE SAME");
                return true;
            }

            //if (saveData()) // SAVE TO LOCAL DATABASE FIRST
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "ATTEMPT TO CREATE CONNECTION TO HQ");
                // CREATE CONNECTION TO CENTRAL HQ DATABASE SERVER
                if (DS.HQ_mySQLConnect())
                {
                    // SEND REQUEST DATA TO HQ
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "CONNECTION CREATED, ATTEMPT TO UPDATE DATA AT HQ");
                    if (updateDataToHQ(DS))
                        result = true;
                    else
                    {
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "FAIL TO UPDATE DATA AT HQ");
                        MessageBox.Show("FAIL TO UPDATE DATA TO HQ");
                        result = false;
                    }
                    // CLOSE CONNECTION TO CENTRAL HQ DATABASE SERVER
                    DS.HQ_mySqlClose();
                }
                else
                {
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "FAIL TO CREATE CONNECTION");

                    MessageBox.Show("FAIL TO CONNECT");
                    result = false;
                }
            }

            return result;
        }

        private void printReport(string invoiceNo)
        {
            string sqlCommandx = "";
            if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
            {
                sqlCommandx = "SELECT '1' AS TYPE, '"+noMutasiTextBox.Text+"' AS ORIGIN_INVOICE, DATE(PH.PR_DATE) AS 'TGL', PH.PR_INVOICE AS 'INVOICE', MP.PRODUCT_NAME AS 'PRODUK', PD.PRODUCT_BASE_PRICE AS 'HARGA', PD.PRODUCT_ACTUAL_QTY AS 'QTY', PD.PR_SUBTOTAL AS 'SUBTOTAL' " +
                                     "FROM PRODUCTS_RECEIVED_HEADER PH, PRODUCTS_RECEIVED_DETAIL PD, MASTER_PRODUCT MP " +
                                     "WHERE PH.PR_INVOICE = '" + invoiceNo + "' AND PD.PR_INVOICE = PH.PR_INVOICE AND PD.PRODUCT_ID = MP.PRODUCT_ID";
            }
            else if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_PO)
            {
                sqlCommandx = "SELECT '2' AS TYPE, '" + noInvoiceTextBox.Text + "' AS ORIGIN_INVOICE, DATE(PH.PR_DATE) AS 'TGL', PH.PR_INVOICE AS 'INVOICE', MP.PRODUCT_NAME AS 'PRODUK', PD.PRODUCT_BASE_PRICE AS 'HARGA', PD.PRODUCT_ACTUAL_QTY AS 'QTY', PD.PR_SUBTOTAL AS 'SUBTOTAL' " +
                                     "FROM PRODUCTS_RECEIVED_HEADER PH, PRODUCTS_RECEIVED_DETAIL PD, MASTER_PRODUCT MP " +
                                     "WHERE PH.PR_INVOICE = '" + invoiceNo + "' AND PD.PR_INVOICE = PH.PR_INVOICE AND PD.PRODUCT_ID = MP.PRODUCT_ID";
            }
            else
            {
                sqlCommandx = "SELECT '0' AS TYPE, 'AA' AS ORIGIN_INVOICE, DATE(PH.PR_DATE) AS 'TGL', PH.PR_INVOICE AS 'INVOICE', MP.PRODUCT_NAME AS 'PRODUK', PD.PRODUCT_BASE_PRICE AS 'HARGA', PD.PRODUCT_ACTUAL_QTY AS 'QTY', PD.PR_SUBTOTAL AS 'SUBTOTAL' " +
                                     "FROM PRODUCTS_RECEIVED_HEADER PH, PRODUCTS_RECEIVED_DETAIL PD, MASTER_PRODUCT MP " +
                                     "WHERE PH.PR_INVOICE = '" + invoiceNo + "' AND PD.PR_INVOICE = PH.PR_INVOICE AND PD.PRODUCT_ID = MP.PRODUCT_ID";
            }

 
            DS.writeXML(sqlCommandx, globalConstants.penerimaanBarangXML);
            penerimaanBarangPrintOutForm displayForm = new penerimaanBarangPrintOutForm();
            displayForm.ShowDialog(this);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "SAVE PENERIMAAN BARANG");

            if (DialogResult.Yes == MessageBox.Show("SAVE DATA ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                if (saveData())
                {
                    gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PENERIMAAN BARANG SAVED");

                    if (originModuleId == globalConstants.PENERIMAAN_BARANG_DARI_MUTASI)
                    {
                        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PENERIMAAN BARANG FROM MUTASI, UPDATE HQ DATA");

                        // UPDATE DATA AT HQ
                        if (!sendUpdateToHQ())
                        {
                            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "FAILED TO UPDATE HQ DATA");
                            MessageBox.Show("KONEKSI KE PUSAT GAGAL");
                        }
                        gUtil.saveUserChangeLog(globalConstants.MENU_MUTASI_BARANG, globalConstants.CHANGE_LOG_INSERT, "PENERIMAAN BARANG [" + prInvoiceTextBox.Text + "] DARI MUTASI[" + noMutasiTextBox.Text + "]");
                    }
                    else
                        gUtil.saveUserChangeLog(globalConstants.MENU_MUTASI_BARANG, globalConstants.CHANGE_LOG_INSERT, "PENERIMAAN BARANG [" + prInvoiceTextBox.Text + "] NO PO [" + selectedInvoice + "]");

                    if (DialogResult.Yes == MessageBox.Show("PRINT RECEIPT ? ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        smallPleaseWait pleaseWait = new smallPleaseWait();
                        pleaseWait.Show();

                        //  ALlow main UI thread to properly display please wait form.
                        Application.DoEvents();
                        printReport(prInvoiceTextBox.Text);

                        pleaseWait.Close();
                    }

                    saveButton.Visible = false;
                    reprintButton.Visible = true;

                    gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

                    prInvoiceTextBox.Enabled = false;
                    PRDtPicker.Enabled = false;
                    detailGridView.ReadOnly = true;
                    gUtil.showSuccess(gUtil.INS);
                }
            }
        }

        private void deleteCurrentRow()
        {
            if (detailGridView.SelectedCells.Count > 0)
            {
                int rowSelectedIndex = detailGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = detailGridView.Rows[rowSelectedIndex];

                isLoading = true;
                detailGridView.Rows.Remove(detailGridView.Rows[rowSelectedIndex]);
                isLoading = false;

                //if (null != selectedRow && rowSelectedIndex != detailGridView.Rows.Count - 1)
                //{
                //    for (int i = rowSelectedIndex; i < detailGridView.Rows.Count; i++)
                //    {
                //        detailRequestQty[i] = detailRequestQty[i + 1];
                //        detailHpp[i] = detailHpp[i + 1];
                //        subtotalList[i] = subtotalList[i + 1];
                //    }

                //    isLoading = true;
                //    if (selectedRow.Index >= 0)
                //    { 
                //        detailGridView.Rows.Remove(detailGridView.Rows[rowSelectedIndex]);
                //        gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "deleteCurrentRow [" + rowSelectedIndex + "]");
                //    }
                //    isLoading = false;
                //}
            }
        }

        private void searchPOButton_Click(object sender, EventArgs e)
        {
            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "GET PO INVOICE TO RECEIVE");

            dataPOForm displayedForm = new dataPOForm(globalConstants.PENERIMAAN_BARANG_DARI_PO, this);
            displayedForm.ShowDialog(this);

            if (selectedInvoice.Length > 0)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PO INVOICE TO RECEIVE [" + selectedInvoice + "]");
                supplierCombo.Enabled = false;
            }
        }

        private void searchMutasiButton_Click(object sender, EventArgs e)
        {
            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "GET NO MUTASI TO RECEIVE");

            dataMutasiBarangForm displayedForm = new dataMutasiBarangForm(globalConstants.PENERIMAAN_BARANG, this);
            displayedForm.ShowDialog(this);

            if (selectedMutasi.Length > 0)
            {
                gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "NO MUTASI TO RECEIVE [" + selectedMutasi + "]");
                supplierCombo.Enabled = false;
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            isLoading = true;

            errorLabel.Text = "";

            gUtil.ResetAllControls(this);

            originModuleId = 0;
            detailGridView.Rows.Clear();

            addDataGridColumn();
            detailGridView.ReadOnly = false;

            labelTotal.Visible = false;
            labelTotal_1.Visible = false;
            labelTotalValue.Visible = false;

            durationTextBox.ReadOnly = false;
            durationTextBox.Text = "0";

            selectedInvoice = "";
            selectedMutasi = "";

            globalTotalValue = 0;

            labelAcceptValue.Text = "Rp.0";

            //labelAsal.Visible = false;
            //labelAsal_1.Visible = false;

            supplierCombo.Enabled = true;
            supplierCombo.Text = "";

            saveButton.Visible = true;
            reprintButton.Visible = false;
            gUtil.reArrangeButtonPosition(arrButton, arrButton[0].Top, this.Width);

            prInvoiceTextBox.Focus();
            prInvoiceTextBox.Enabled = true;

            PRDtPicker.Enabled = true;

            isLoading = false;
        }

        private void supplierCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFromID = Convert.ToInt32(supplierHiddenCombo.Items[supplierCombo.SelectedIndex]);
        }

        private void detailGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //detailHpp.Add("0");
            //detailRequestQty.Add("0");
            //subtotalList.Add("0");
        }

        private void durationTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                durationTextBox.SelectAll();
            });
        }

        private void penerimaanBarangForm_Activated(object sender, EventArgs e)
        {
            registerGlobalHotkey();
        }

        private void penerimaanBarangForm_Deactivate(object sender, EventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void reprintButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("PRINT RECEIPT ? ", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                smallPleaseWait pleaseWait = new smallPleaseWait();
                pleaseWait.Show();

                //  ALlow main UI thread to properly display please wait form.
                Application.DoEvents();
                printReport(prInvoiceTextBox.Text);

                pleaseWait.Close();
            }
        }

        private void detailGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void detailGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            detailGridView.SuspendLayout();
        }

        private void detailGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            detailGridView.ResumeLayout();
        }

        private void detailGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelectedIndex = 0;
            double productQty = 0;
            double hppValue = 0;
            string hppString = "0";
            string qtyString = "0";
            double subTotal = 0;
            string cellValue = "";
            string columnName = "";
            var cell = detailGridView[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = detailGridView.Rows[e.RowIndex];
            bool validInput = true;

            rowSelectedIndex = e.RowIndex;
            columnName = cell.OwningColumn.Name;

            if (isLoading)
                return;

            gUtil.saveSystemDebugLog(globalConstants.MENU_PENERIMAAN_BARANG, "PENERIMAAN BARANG : detailGridView_CellValueChanged [" + columnName + "]");

            if (null != selectedRow.Cells[columnName].Value)
                cellValue = selectedRow.Cells[columnName].Value.ToString();
            else
                cellValue = "";

            if (columnName == "hpp" ||
                columnName == "qtyReceived")
            { 
                if (cellValue.Length <= 0)
                {
                    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
                    isLoading = true;
                    selectedRow.Cells["subTotal"].Value = 0;
                    selectedRow.Cells[columnName].Value = "0";

                    calculateTotal();
                    isLoading = false;

                    return;
                }

                if (null != selectedRow.Cells["hpp"].Value)
                    hppString = selectedRow.Cells["hpp"].Value.ToString();

                if (null != selectedRow.Cells["qtyReceived"].Value)
                    qtyString = selectedRow.Cells["qtyReceived"].Value.ToString();

                if (!gUtil.matchRegEx(hppString, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL) || 
                    !gUtil.matchRegEx(qtyString, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL))
                {
                    validInput = false;
                    int rowIndex = rowSelectedIndex + 1;
                    errorLabel.Text = "INPUT PADA BARIS [" + rowIndex + "] TIDAK VALID";
                }
                else
                    errorLabel.Text = "";

                if (validInput)
                    try
                    {
                        hppValue = Convert.ToDouble(selectedRow.Cells["hpp"].Value);
                        productQty = Convert.ToDouble(selectedRow.Cells["qtyReceived"].Value);
                        subTotal = Math.Round((hppValue * productQty), 2);
                        selectedRow.Cells["subtotal"].Value = subTotal;

                        calculateTotal();
                    }
                    catch (Exception ex)
                    {
                    }
            }
        }

        private void detailGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (detailGridView.IsCurrentCellDirty)
            {
                detailGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void supplierCombo_Validated(object sender, EventArgs e)
        {
            if (!supplierCombo.Items.Contains(supplierCombo.Text))
            {
                errorLabel.Text = "SUPPLIER TIDAK VALID";
                supplierCombo.Focus();
            }
            else
                errorLabel.Text = "";
        }
    }
}