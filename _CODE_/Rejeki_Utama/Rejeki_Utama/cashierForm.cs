﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotkeys;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;
using System.Drawing.Printing;
using System.Reflection;

namespace AlphaSoft
{
    public partial class cashierForm : Form
    {
        private string posTitle;// = "REJEKI UTAMA";
        private string selectedsalesinvoice = "";
        private string selectedsalesinvoiceTax = "";
        private string selectedsalesinvoiceRevNo = "";
        private string selectedSQInvoice = "";
        public static int objCounter = 1;
        private DateTime localDate = DateTime.Now;
        private double globalTotalValue = 0;
        private double discValue = 0;
        private double discPercentValue = 0;
        private int selectedPelangganID = 0;
        private int selectedTechnicianID = 0;
        private int selectedPaymentMethod = 0;
        private bool isLoading = false;
        private bool isLoadingDiscPercent = false;
        private double bayarAmount = 0;
        //private string bayarAmountText = "0";
       // private double discAmount = 0;
        //private string discAmountText = "0";
        private double sisaBayar = 0;
        private int originModuleID = 0;
        private int custIsBlocked = 0;
        private double totalAfterDisc = 0;
        private string discJualPersenValueText = "0";
        private int isJobFinished = 0;
        int salesActiveStatus = 1;
        string currencyFormat = "C2";

        private Data_Access DS = new Data_Access();
        private SMSapplication smsLib = new SMSapplication();

        private globalUtilities gutil = new globalUtilities();
        private CultureInfo culture = new CultureInfo("id-ID");

        //private List<string> salesQty = new List<string>();
        //private List<string> productPriceList = new List<string>();
        //private List<string> jumlahList = new List<string>();
        //private List<string> disc1 = new List<string>();
        //private List<string> disc2 = new List<string>();
        //private List<string> discRP = new List<string>();

        private Hotkeys.GlobalHotkey ghk_F1;
        private Hotkeys.GlobalHotkey ghk_F2;
        private Hotkeys.GlobalHotkey ghk_F3;
        private Hotkeys.GlobalHotkey ghk_F4;
        private Hotkeys.GlobalHotkey ghk_F5;
        private Hotkeys.GlobalHotkey ghk_F7;
        private Hotkeys.GlobalHotkey ghk_F8;
        private Hotkeys.GlobalHotkey ghk_F9;
        private Hotkeys.GlobalHotkey ghk_F10;
        private Hotkeys.GlobalHotkey ghk_F11;
        private Hotkeys.GlobalHotkey ghk_F12;
        
        private Hotkeys.GlobalHotkey ghk_CTRL_DEL;
        private Hotkeys.GlobalHotkey ghk_CTRL_Enter;
        private Hotkeys.GlobalHotkey ghk_CTRL_C;
        private Hotkeys.GlobalHotkey ghk_CTRL_U;

        private Hotkeys.GlobalHotkey ghk_ALT_F4;
        private Hotkeys.GlobalHotkey ghk_Add;
        private Hotkeys.GlobalHotkey ghk_Substract;

        //private adminForm parentForm;

        public cashierForm()
        {
            InitializeComponent();
        }

        public cashierForm(int counter)
        {
            InitializeComponent();

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MULTIPLE CASHIER INSTANCE [" + counter + "]");

            label1.Text = "Struk # : " + counter;
            titleLabel.Text = gutil.loadStoreName();//posTitle;

            objCounter = counter + 1;
        }

        public cashierForm(int moduleID, bool flag)
        {
            InitializeComponent();
            originModuleID = moduleID;

            if (originModuleID == globalConstants.SERVICE_AC)
            {
                posTitle = gutil.loadStoreName() + " - MODUL SERVICE AC";
            }
            else if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU)
            {
                posTitle = gutil.loadStoreName() + "  - SURAT TUGAS";
            }

            titleLabel.Text = posTitle;
        }

        public cashierForm(int moduleID, string noInvoice)
        {
            InitializeComponent();
            originModuleID = moduleID;
            selectedsalesinvoice = noInvoice;

            if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU)
            {
                posTitle = gutil.loadStoreName() + " - SURAT TUGAS";
            }

            titleLabel.Text = posTitle;
        }

        // TO HANDLE SALES REVISION
        public cashierForm(string noInvoice, string revNo, int moduleID = globalConstants.SALES_ORDER_REVISION)
        {
            InitializeComponent();
            originModuleID = moduleID;
            selectedsalesinvoiceRevNo = revNo;
            selectedsalesinvoice = noInvoice;
            titleLabel.Text = posTitle;
        }

        private void captureAll(Keys key)
        {
            switch (key)
            {
                case Keys.F1:
                    cashierHelpForm displayHelp = new cashierHelpForm();
                    displayHelp.ShowDialog(this);
                    break;

                case Keys.F2:
                    if (isJobFinished == 1 || salesActiveStatus == 0)// || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    totalAfterDiscTextBox.Focus();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY BARCODE FORM");

                    barcodeForm displayBarcodeForm = new barcodeForm(this, globalConstants.CASHIER_MODULE);

                    displayBarcodeForm.Top = this.Top + 5;// - displayBarcodeForm.Height;
                    displayBarcodeForm.Left = this.Left + 5;// (Screen.PrimaryScreen.Bounds.Width / 2) - (displayBarcodeForm.Width / 2);

                    displayBarcodeForm.ShowDialog(this);
                    break;

                case Keys.F3:
                    if (originModuleID == 0)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CREATE NEW INSTANCE [" + objCounter + "]");

                        cashierForm displayForm = new cashierForm(objCounter);
                        displayForm.Show();
                    }
                    break;

                case Keys.F4:
                    //MessageBox.Show("F4");
                    if (isJobFinished == 1 || salesActiveStatus == 0)// || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY PELANGGAN FORM");
                    dataPelangganForm pelangganForm = new dataPelangganForm(globalConstants.CASHIER_MODULE, this);
                    pelangganForm.ShowDialog(this);
                    break;

                case Keys.F5:
                    if (isJobFinished == 1 || salesActiveStatus == 0)
                        return;

                    if (DialogResult.Yes == MessageBox.Show("HAPUS DATA DATA DI LAYAR ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        clearUpScreen();
                    break;

                case Keys.F7:
                    if (selectedsalesinvoice != "")
                        if (DialogResult.Yes == MessageBox.Show("REPRINT INVOICE ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                            reprintInvoice();
                    break;

                case Keys.F8:
                    if (isJobFinished == 1 || salesActiveStatus == 0 || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO ADD NEW ROW PRESSED");

                    addNewRow();
                    break;

                case Keys.F9:
                    if (isJobFinished == 1 || salesActiveStatus == 0)// || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    if (custIsBlocked == 0)
                    { 
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO SAVE AND PRINT OUT INVOICE PRESSED");
                        
                        // MOVE FOCUS TO TRIGGER ANY UNCOMMITTED INPUT
                        totalPenjualanTextBox.Select();

                        saveAndPrintOutInvoice();
                    }
                    else
                    {
                        MessageBox.Show("CUSTOMER DIBLOK");
                    }
                    break;

                case Keys.F11:
                    if (isJobFinished == 1 || salesActiveStatus == 0)// || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    totalAfterDiscTextBox.Focus();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO OPEN PRODUK SEARCH FORM PRESSED");

                    POSSearchProductForm displayProdukForm = new POSSearchProductForm(globalConstants.CASHIER_MODULE, this);
                    displayProdukForm.ShowDialog(this);
                    break;

                case Keys.Add:
                    bayarTextBox.Focus();
                    break;

                case Keys.Subtract:
                    discJualMaskedTextBox.Focus();
                    break;

                case Keys.F10:
                    if (isJobFinished == 1 || salesActiveStatus == 0)// || originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                        return;

                    //MessageBox.Show("F10");
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISPLAY TEKNISI FORM");
                    dataTeknisiForm teknisiForm = new dataTeknisiForm(originModuleID, this);
                    teknisiForm.ShowDialog(this);
                    break;

                case Keys.F12:
                    //MessageBox.Show("F12");
                    break;
            }
        }

        private void captureAltModifier(Keys key)
        {
            switch (key)
            {
                case Keys.F4: // ALT + F4
                    MessageBox.Show("ALT+F4");
                    this.Close();
                    break;
            }
        }

        private void captureCtrlModifier(Keys key)
        {
            switch (key)
            {
                case Keys.Delete: // CTRL + DELETE
                    if (isJobFinished == 1 || (salesActiveStatus == 0 && originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU))
                        return;

                        //MessageBox.Show("CTRL+DELETE");
                    if (DialogResult.Yes == MessageBox.Show("DELETE CURRENT ROW?", "WARNING", MessageBoxButtons.YesNo))
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_KeyDown ATTEMPT TO DELETE ROW");
                        deleteCurrentRow();
                        updateRowNumber();
                        calculateTotal();
                    }
                    break;

                case Keys.Enter:
                    if (isJobFinished == 1 || (salesActiveStatus == 0 && originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU))
                        return;

                    if (custIsBlocked == 0)
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : HOTKEY TO SAVE AND PRINT OUT INVOICE PRESSED");

                        saveAndPrintOutInvoice();
                    }
                    else
                    {
                        MessageBox.Show("CUSTOMER DIBLOK");
                    }
                    break;

                case Keys.C: // CTRL + C
                    MessageBox.Show("CTRL+C");
                    break;
                case Keys.U: // CTRL + U
                    MessageBox.Show("CTRL+U");
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
                else if (modifier == Constants.ALT)
                    captureAltModifier(key);
                else if (modifier == Constants.CTRL)
                    captureCtrlModifier(key);
            }

            base.WndProc(ref m);
        }

        private void registerGlobalHotkey()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : REGISTER HOTKEY");

            ghk_F1 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F1, this);
            ghk_F1.Register();

            //ghk_F2 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F2, this);
            //ghk_F2.Register();

            ghk_F3 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F3, this);
            ghk_F3.Register();

            ghk_F4 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F4, this);
            ghk_F4.Register();

            ghk_F5 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F5, this);
            ghk_F5.Register();

            ghk_F7 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F7, this);
            ghk_F7.Register();

            ghk_F8 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F8, this);
            ghk_F8.Register();

            ghk_F9 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F9, this);
            ghk_F9.Register();

            ghk_F10 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F10, this);
            ghk_F10.Register();

            ghk_F11 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F11, this);
            ghk_F11.Register();

            if (originModuleID != globalConstants.SALES_QUOTATION)
            { 
                ghk_Add = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Add, this);
                ghk_Add.Register();
            }

            ghk_Substract = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.Subtract, this);
            ghk_Substract.Register();

            ghk_CTRL_DEL = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Delete, this);
            ghk_CTRL_DEL.Register();

            ghk_CTRL_Enter = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.Enter, this);
            ghk_CTRL_Enter.Register();



            //// ## F12 doesn't work yet ##
            ////ghk_F12 = new Hotkeys.GlobalHotkey(Constants.NOMOD, Keys.F12, this);
            ////ghk_F12.Register();



            //ghk_CTRL_C = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.C, this);
            //ghk_CTRL_C.Register();

            //ghk_CTRL_U = new Hotkeys.GlobalHotkey(Constants.CTRL, Keys.U, this);
            //ghk_CTRL_U.Register();

            //ghk_ALT_F4 = new Hotkeys.GlobalHotkey(Constants.ALT, Keys.F4, this);
            //ghk_ALT_F4.Register();

        }

        private void unregisterGlobalHotkey()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : UNREGISTER HOTKEY");

            ghk_F1.Unregister();
            //ghk_F2.Unregister();
            ghk_F3.Unregister();
            ghk_F4.Unregister();
            ghk_F5.Unregister();
            ghk_F7.Unregister();
            ghk_F8.Unregister();
            ghk_F9.Unregister();
            ghk_F10.Unregister();
            ghk_F11.Unregister();

            if (originModuleID != globalConstants.SALES_QUOTATION)
            {
                ghk_Add.Unregister();
            }

            ghk_Substract.Unregister();
            
            ghk_CTRL_DEL.Unregister();
            ghk_CTRL_Enter.Unregister();


            ////ghk_F12.Unregister();


            //ghk_CTRL_C.Unregister();
            //ghk_CTRL_U.Unregister();

            //ghk_ALT_F4.Unregister();
        }

        private void reprintInvoice()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "RE-PRINT INVOICE");
            PrintReceipt();
        }

        public void clearUpScreen()
        {
            isLoading = true;

            //while (cashierDataGridView.Rows.Count > 0 )
            //    cashierDataGridView.Rows.Remove(cashierDataGridView.Rows[0]);
            cashierDataGridView.Rows.Clear();
            isLoading = false;

            //salesQty.Clear();
            //disc1.Clear();
            //disc2.Clear();
            //discRP.Clear();


            //salesQty.Add("0");
            //disc1.Add("0");
            //disc2.Add("0");
            //discRP.Add("0");

            selectedPelangganID = 0;
            globalTotalValue = 0;
            discValue = 0;
            sisaBayar = 0;
            bayarAmount = 0;
            //bayarAmountText = "0";
            //discAmountText = "0";
        
            totalLabel.Text = globalTotalValue.ToString("C0", culture);
            gutil.ResetAllControls(this);

            totalPenjualanTextBox.Text = globalTotalValue.ToString("C0", culture);
            totalAfterDiscTextBox.Text = globalTotalValue.ToString("C0", culture);
            uangKembaliTextBox.Text = "0";

            customerComboBox.SelectedIndex = 0;
            customerComboBox.Text = customerComboBox.Items[0].ToString();

            paymentComboBox.SelectedIndex = 0;
            paymentComboBox.Text = paymentComboBox.Items[0].ToString();

            cashRadioButton.Checked = true;
            creditRadioButton.Checked = false;
        }

        public int getBlockedStatus(int ID)
        {
            int result = 0;

            if (ID > 0)
                result = Convert.ToInt32(DS.getDataSingleValue("SELECT CUSTOMER_BLOCKED FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + ID));

            return result;
        }

        public void setCustomerID(int ID)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SELECTED CUSTOMER ID [" + ID+ "]");

            selectedPelangganID = ID;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO SET CUSTOMER PROFILE");
            setCustomerProfile();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : FINISHED SET CUSTOMER PROFILE");

            custIsBlocked = getBlockedStatus(selectedPelangganID);

            if (custIsBlocked == 1)
            {
                MessageBox.Show("CUSTOMER DIBLOK , TRANSAKSI TIDAK BISA DILAKUKAN");
            }
            else
            { 
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO REFRESH PRODUCT PRICE");
                refreshProductPrice();
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE REFRESHED");
            }
        }

        public void setTechnicianID(int ID)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SELECTED TECHNICIAN ID [" + ID + "]");

            selectedTechnicianID = ID;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO SET CUSTOMER PROFILE");
            setTechnicianProfile();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : FINISHED SET CUSTOMER PROFILE");
        }

        private void setCustomerProfile()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            //DS.mySqlConnect();
            sqlCommand = "SELECT * FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedPelangganID;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();

                    pelangganTextBox.Text = rdr.GetString("CUSTOMER_FULL_NAME");
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET CUSTOMER PROFILE [" + pelangganTextBox.Text + "]");

                    isLoading = true;
                    customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                    customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CUSTOMER GROUP [" + customerComboBox.Text + "]");
                    isLoading = false;
                }
                else
                {
                    // RESET CUSTOMER 
                    pelangganTextBox.Text = "";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : RESET CUSTOMER PROFILE");

                    isLoading = true;
                    customerComboBox.SelectedIndex = 0;
                    customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : RESET CUSTOMER GROUP [" + customerComboBox.Text + "]");
                    isLoading = false;
                }
            }
            rdr.Close();
        }

        private void setTechnicianProfile()
        {
            MySqlDataReader rdr;
            string sqlCommand = "";

            //DS.mySqlConnect();
            sqlCommand = "SELECT * FROM MASTER_TECHNICIAN WHERE ID = " + selectedTechnicianID;
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    rdr.Read();

                    teknisiTextbox.Text = rdr.GetString("TECHNICIAN_NAME");
                }
            }
            rdr.Close();
        }

        private void refreshProductPrice()
        {
            double productPrice = 0;
            string productID = "";
            MySqlDataReader rdr;

            for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
            {
                if (null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    productID = cashierDataGridView.Rows[i].Cells["productID"].Value.ToString();
                    productPrice = getProductPriceValue(productID, customerComboBox.SelectedIndex);

                    cashierDataGridView.Rows[i].Cells["productPrice"].Value = productPrice;

                    if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION)
                    { 
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK IF THERE'S ANY ENTRY IN THE PRODUCT DISC");

                        if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'")) > 0)
                        {
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ENTRY IN THE PRODUCT DISC FOUND, RETRIEVE THE DISC VALUE");

                            // DATA EXIST, LOAD DISC VALUE
                            using (rdr = DS.getData("SELECT * FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'"))
                            {
                                if (rdr.HasRows)
                                {
                                    rdr.Read();

                                    cashierDataGridView.Rows[i].Cells["disc1"].Value = rdr.GetString("DISC_1");
                                    //disc1[i] = rdr.GetString("DISC_1");

                                    cashierDataGridView.Rows[i].Cells["disc2"].Value = rdr.GetString("DISC_2");
                                    //disc2[i] = rdr.GetString("DISC_2");

                                    cashierDataGridView.Rows[i].Cells["discRP"].Value = rdr.GetString("DISC_RP");
                                    //discRP[i] = rdr.GetString("DISC_RP");

                                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : DISC FOUND [" + rdr.GetString("DISC_1") + ", " + rdr.GetString("DISC_2") + ", " + rdr.GetString("DISC_RP") + "]");
                                }
                                rdr.Close();
                            }
                        }
                        else
                        {
                            cashierDataGridView.Rows[i].Cells["disc1"].Value = 0;
                            //disc1[i] = "0";

                            cashierDataGridView.Rows[i].Cells["disc2"].Value = 0;
                            //disc2[i] = "0";

                            cashierDataGridView.Rows[i].Cells["discRP"].Value = 0;
                            //discRP[i] = "0";
                        }
                    }
                    cashierDataGridView.Rows[i].Cells["jumlah"].Value = calculateSubTotal(i, productPrice);
                }
            }

            calculateTotal();
        }

        private void updateLabel()
        {
            localDate = DateTime.Now;
            dateTimeStampLabel.Text = String.Format(culture, "{0:dddd, dd-MM-yyyy - HH:mm}", localDate);
        }

        private void updateRowNumber()
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : UPDATE ROW NUMBER, TOTAL ROW ["+ cashierDataGridView.Rows.Count+"]");
            for (int i = 0;i<cashierDataGridView.Rows.Count;i++)
                cashierDataGridView.Rows[i].Cells["F8"].Value = i + 1;
        }

        private void addNewRow(bool isActive = true)
        {
            int prevValue = 0;
            bool allowToAdd = true;
            int newRowIndex = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  isActive [" + isActive.ToString() + "]");
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  ROW COUNT [" + cashierDataGridView.Rows.Count + "]");

            for (int i = 0; i < cashierDataGridView.Rows.Count && allowToAdd && !isLoading; i++)
            {
                if (null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    if (!productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
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

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW,  ALLOW TO ADD [" + allowToAdd.ToString() + "]");

            if (allowToAdd)
            {
                if (cashierDataGridView.Rows.Count > 0)
                    prevValue = Convert.ToInt32(cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["F8"].Value);

                cashierDataGridView.Rows.Add();

                //salesQty.Add("0");
                //disc1.Add("0");
                //disc2.Add("0");
                //discRP.Add("0");
                //productPriceList.Add("0");
                //jumlahList.Add("0");

                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["F8"].Value = prevValue + 1;
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["hpp"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["productPrice"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc1"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["qty"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["disc2"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["discRP"].Value = "0";
                cashierDataGridView.Rows[cashierDataGridView.Rows.Count - 1].Cells["jumlah"].Value = "0";
                newRowIndex = cashierDataGridView.Rows.Count - 1;
            }
            else
            {
                DataGridViewRow selectedRow = cashierDataGridView.Rows[newRowIndex];
                clearUpSomeRowContents(selectedRow, newRowIndex);
            }

            if (isActive)
            { 
                cashierDataGridView.Focus();
                cashierDataGridView.CurrentCell = cashierDataGridView.Rows[newRowIndex].Cells["productName"];
            }
        }

        public void addNewRowFromBarcode(string productID, string productName, int rowIndex = -1)
        {
            int i = 0;
            bool found = false;
            bool foundEmptyRow = false;
            int emptyRowIndex = 0;
            int rowSelectedIndex = 0;
            double currQty;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ADD NEW ROW FROM BARCODE [" + productName + "]");

            if (rowIndex >= 0)
            {
                rowSelectedIndex = rowIndex;
            }
            else
            {
                // CHECK FOR EXISTING SELECTED ITEM
                for (i = 0; i < cashierDataGridView.Rows.Count && !found && !foundEmptyRow; i++)
                {
                    if (null != cashierDataGridView.Rows[i].Cells["productName"].Value)
                    {
                        if (cashierDataGridView.Rows[i].Cells["productName"].Value.ToString() == productName)
                        {
                            found = true;
                            rowSelectedIndex = i;

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : EXISTING ROW FOUND [" + rowSelectedIndex + "]");
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
                    if (!foundEmptyRow)
                    {
                        addNewRow(false);
                        rowSelectedIndex = cashierDataGridView.Rows.Count - 1;

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NEW ROW ADDED [" + rowSelectedIndex + "]");
                    }
                    else
                    {
                        rowSelectedIndex = emptyRowIndex;
                    }
                }
            }

            DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

            updateSomeRowContents(selectedRow, rowSelectedIndex, productName);

            if (!found)
            {
                if (gutil.stockIsEnough(productID, 1, true))
                {
                    selectedRow.Cells["qty"].Value = 1;
                    //salesQty[rowSelectedIndex] = "1";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET QTY TO 1 BECAUSE OF NEW ROW");
                }
                else
                {
                    selectedRow.Cells["qty"].Value = 0;
                    //salesQty[rowSelectedIndex] = "0";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SET QTY TO 0 BECAUSE OF NEW ROW BUT STOCK IS NOT ENOUGH");
                }
            }
            else
            {
                //currQty = Convert.ToDouble(salesQty[rowSelectedIndex]) + 1;
                currQty = Convert.ToDouble(selectedRow.Cells["qty"].Value) + 1;

                if (gutil.stockIsEnough(productID, currQty, true))
                {
                    selectedRow.Cells["qty"].Value = currQty;
                    //salesQty[rowSelectedIndex] = currQty.ToString();
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : QTY FOR THE EXISTING ROW [" + currQty.ToString() + "]");
                }
                else
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : QTY FOR THE EXISTING ROW NOT CHANGED");
                }
            }

            selectedRow.Cells["jumlah"].Value = calculateSubTotal(rowSelectedIndex, Convert.ToDouble(selectedRow.Cells["productPrice"].Value));
            //jumlahList[rowSelectedIndex] = selectedRow.Cells["jumlah"].Value.ToString();
            calculateTotal();
            cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];
            //comboSelectedIndexChangeMethod(rowSelectedIndex, i, selectedRow);
            //cashierDataGridView.CurrentCell = cashierDataGridView.Rows[rowSelectedIndex].Cells["qty"];

            cashierDataGridView.Select();
            cashierDataGridView.BeginEdit(true);
        }

        private bool productIDValid(string productID)
        {
            bool result = false;

            if (0 < Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'")))
                result = true;

            return result;
        }

        private bool isCreditExceedLimit(double newCreditAmount = 0, string salesInvoiceID = "")
        {
            bool result = false;
            string sqlCommand = "";

            // CALCULATE TOTAL UNPAID TRANSACTION MINUS THE CURRENT SALES INVOICE FOR SALES ORDER REVISION
            double totalUnpaidTransaction = 0;
            sqlCommand = "SELECT IFNULL(SUM(SALES_TOTAL), 0) FROM SALES_HEADER WHERE CUSTOMER_ID = " + selectedPelangganID + " AND SALES_PAID = 0 AND SALES_VOID = 0";
            if (salesInvoiceID.Length > 0)
                sqlCommand = sqlCommand + " AND SALES_INVOICE <> '" + salesInvoiceID + "'";

            totalUnpaidTransaction = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            // CALCULATE TOTAL PAYMENT FOR UNPAID TRANSACTION
            double totalPaymentTransaction = 0;
            totalPaymentTransaction = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(PAYMENT_NOMINAL), 0) FROM PAYMENT_CREDIT PC, CREDIT C, SALES_HEADER SH WHERE SH.CUSTOMER_ID = " + selectedPelangganID + " AND SH.SALES_PAID = 0 AND SH.SALES_VOID = 0 AND SH.SALES_INVOICE = C.SALES_INVOICE AND PC.CREDIT_ID = C.CREDIT_ID"));

            // CALCULATE TOTAL CREDIT AT PRESENT
            double totalOutstandingCredit = 0;
            double maxCredit = 0;
            double creditTolerance = 0;
            totalOutstandingCredit = totalUnpaidTransaction - totalPaymentTransaction;
            maxCredit = Convert.ToDouble(DS.getDataSingleValue("SELECT MAX_CREDIT FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedPelangganID));
            creditTolerance = Math.Round(maxCredit * globalUtilities.MAX_CREDIT_TOLERANCE_PERCENTAGE / 100, 2);

            maxCredit = maxCredit + creditTolerance;
            totalOutstandingCredit = totalOutstandingCredit + newCreditAmount;

            if ((totalOutstandingCredit>0) && (maxCredit <= totalOutstandingCredit))
                result = true;

            return result;
        }

        private void validateUserCreditStatus()
        {
            string sqlCommand = "";
            MySqlException internalEX = null;
           
            if (isCreditExceedLimit())
            {
                // BLOCK USER
                DS.beginTransaction();

                try
                {
                    DS.mySqlConnect();

                    sqlCommand = "UPDATE MASTER_CUSTOMER SET CUSTOMER_BLOCKED = 1 WHERE CUSTOMER_ID = " + selectedPelangganID;
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    DS.commit();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private bool dataValidated()
        {
            bool validInput = true;

            if (originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && 
                originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.MONITOR_SURAT_TUGAS &&
                globalTotalValue <= 0)
            {
                errorLabel.Text = "NILAI TRANSAKSI 0";
                return false;
            }

            if (cashierDataGridView.Rows.Count <= 0)
            {
                errorLabel.Text = "TIDAK ADA BARANG";
                return false;
            }

            for (int i = 0; i < cashierDataGridView.Rows.Count && validInput; i++)
            {
                // CHECK INPUT FOR PRODUCT PRICE
                if (null != cashierDataGridView.Rows[i].Cells["productPrice"].Value)
                    validInput = gutil.matchRegEx(cashierDataGridView.Rows[i].Cells["productPrice"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                else
                    validInput = false;

                if (!validInput)
                {
                    errorLabel.Text = "INPUT HARGA DI BARIS " + (i + 1) + " TIDAK VALID";
                    continue;
                }

                // CHECK INPUT FOR QTY
                if (null != cashierDataGridView.Rows[i].Cells["qty"].Value)
                    validInput = gutil.matchRegEx(cashierDataGridView.Rows[i].Cells["qty"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                else
                    validInput = false;

                if (!validInput)
                {
                    errorLabel.Text = "INPUT QTY DI BARIS " + (i + 1) + " TIDAK VALID";
                    continue;
                }

                // CHECK QTY CAN'T BE NULL
                if (
                    ((null == cashierDataGridView.Rows[i].Cells["qty"].Value) ||
                    (0 == Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value))
                    ) && null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                {
                    errorLabel.Text = "JUMLAH PRODUK DI BARIS " + (i + 1) + " = 0";
                    validInput = false;
                    continue;
                }

                if (originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU &&
                    originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                    originModuleID != globalConstants.MONITOR_SURAT_TUGAS)
                {
                    if (
                        ((null == cashierDataGridView.Rows[i].Cells["jumlah"].Value) ||
                        (0 >= Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value))
                        ) && null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                    {
                        errorLabel.Text = "PEMBELIAN DI BARIS " + (i + 1) + " TIDAK VALID";
                        validInput = false;
                        continue;
                    }

                    if (null != cashierDataGridView.Rows[i].Cells["productID"].Value && !productIDValid(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString()))
                    {
                        errorLabel.Text = "PRODUK DI BARIS " + (i + 1) + " TIDAK VALID";
                        validInput = false;
                        continue;
                    }

                    // CHECK QTY IS ENOUGH
                    if (originModuleID == 0 || originModuleID == globalConstants.SALES_ORDER_REVISION)
                    {
                        if (!gutil.stockIsEnough(cashierDataGridView.Rows[i].Cells["productID"].Value.ToString(), Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value), true))
                        {
                            errorLabel.Text = "STOK PADA BARIS [" + (i + 1) + "] TIDAK CUKUP";
                            validInput = false;
                            continue;
                        }
                    }

                    if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION)
                    {
                        // CHECK INPUT FOR DISC 1
                        if (null != cashierDataGridView.Rows[i].Cells["disc1"].Value)
                            validInput = gutil.matchRegEx(cashierDataGridView.Rows[i].Cells["disc1"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                        else
                            validInput = false;

                        if (!validInput)
                        {
                            errorLabel.Text = "INPUT DISC1 DI BARIS " + (i + 1) + " TIDAK VALID";
                            continue;
                        }

                        // CHECK INPUT FOR DISC 2
                        if (null != cashierDataGridView.Rows[i].Cells["disc2"].Value)
                            validInput = gutil.matchRegEx(cashierDataGridView.Rows[i].Cells["disc2"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                        else
                            validInput = false;
                        if (!validInput)
                        {
                            errorLabel.Text = "INPUT DISC2 DI BARIS " + (i + 1) + " TIDAK VALID";
                            continue;
                        }

                        // CHECK INPUT FOR DISC RP
                        if (null != cashierDataGridView.Rows[i].Cells["discRP"].Value)
                            validInput = gutil.matchRegEx(cashierDataGridView.Rows[i].Cells["discRP"].Value.ToString(), globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL);
                        else
                            validInput = false;

                        if (!validInput)
                        {
                            errorLabel.Text = "INPUT DISC RP DI BARIS " + (i + 1) + " TIDAK VALID";
                            continue;
                        }
                    }
                }
            }

            if (!validInput)
            {
                return false;
            }

            if ( (originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.MONITOR_SURAT_TUGAS) && selectedPelangganID == 0)
            { 
                if (DialogResult.No == MessageBox.Show("PELANGGAN KOSONG, LANJUTKAN ?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    return false;
            }
            else if ( (originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.MONITOR_SURAT_TUGAS) && creditRadioButton.Checked == true)
            {
                int userStatus = 0;

                if (originModuleID == 0) // NORMAL TRANSACTION
                { 
                    userStatus = Convert.ToInt32(DS.getDataSingleValue("SELECT CUSTOMER_BLOCKED FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedPelangganID));
                    if (userStatus == 1)
                    {
                        errorLabel.Text = "CUSTOMER DIBLOK";
                        return false;
                    }
                }

                if (originModuleID != globalConstants.SALES_QUOTATION && 
                    originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && 
                    originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                    originModuleID != globalConstants.MONITOR_SURAT_TUGAS
                    ) // NORMAL TRANSACTION AND SALES ORDER REVISION
                {
                    string currentInvoiceID = "";
                    if (originModuleID == globalConstants.SALES_QUOTATION)
                        currentInvoiceID = selectedsalesinvoice;

                    if (isCreditExceedLimit(globalTotalValue - Convert.ToDouble(gutil.allTrim(discJualMaskedTextBox.Text)), currentInvoiceID))
                    {
                        errorLabel.Text = "PEMBELIAN MELEBIHI BATAS KREDIT";
                        return false;
                    }
                }

            }

            if (originModuleID != globalConstants.SALES_QUOTATION && originModuleID != globalConstants.EDIT_SALES_QUOTATION
                && originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU &&
                originModuleID != globalConstants.MONITOR_SURAT_TUGAS)
            { 
                if (cashRadioButton.Checked)
                {
                    double paymentAmount = 0;

                    // CHECK PAYMENT AMOUNT FOR CASH PAYMENT
                    if (bayarTextBox.Text.Length <= 0)
                    {
                        errorLabel.Text = "JUMLAH PEMBAYARAN 0";
                        return false;
                    }

                    // CHECK PAYMENT AMOUNT MUST BE MORE OR EQUALS THAN THE BILL
                    paymentAmount = Convert.ToDouble(bayarAmount);
                    if (paymentAmount < globalTotalValue - discValue)
                    {
                        errorLabel.Text = "JUMLAH PEMBAYARAN LEBIH KECIL DARI NOTA";
                        return false;
                    }
                }
                else
                {
                    // CHECK TEMPO
                    if (tempoMaskedTextBox.Text.Length <= 0 || tempoMaskedTextBox.Text == "0")
                    {
                        errorLabel.Text = "LAMA TEMPO TIDAK BOLEH NOL";
                        return false;
                    }
                }
            }

            if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU ||
                originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU ||
                originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
            {
                if (selectedTechnicianID == 0)
                {
                    errorLabel.Text = "TEKNISI BELUM DIPILIH";
                    return false;
                }
            }

            errorLabel.Text = "";
            return true;
        }

        private bool setJobAsFinished()
        {
            bool result = true;
            string sqlCommand = "";
            MySqlException internalEX = null;

            string jobFinishedDate = "";
            string jobFinishedTime = "";
            isJobFinished = 1;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();
                jobFinishedDate = gutil.getCustomStringFormatDate(finishedDateTimePicker.Value);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
                jobFinishedTime = finishedTimeMaskedTextBox.Text;

                // UPDATE HEADER TABLE
                sqlCommand = "UPDATE SURAT_TUGAS_HEADER SET " +
                                       "CUSTOMER_ID = " + selectedPelangganID + ", " +
                                       "TECHNICIAN_ID = " + selectedTechnicianID + ", " +
                                       "JOB_FINISHED_DATE = STR_TO_DATE('" + jobFinishedDate + "', '%d-%m-%Y %H:%i'), " +
                                       "JOB_FINISHED_TIME  = '" + jobFinishedTime + "', " +
                                       "IS_JOB_FINISHED = 1 " +
                                       "WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE JOB FINISHED SURAT TUGAS [" + selectedsalesinvoice + "]");
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "EXCEPTION THROWN [" + ex.Message + "]");

                try
                {
                    DS.rollBack(ref internalEX);
                }
                catch (MySqlException e)
                {
                    if (DS.getMyTransConnection() != null)
                    {
                        gutil.showDBOPError(e, "ROLLBACK");
                    }
                }

                gutil.showDBOPError(ex, "INSERT");
                result = false;
            }
            finally
            {
                DS.mySqlClose();
            }

            return result;
        }

        private void sendSMSConfirmation()
        {
            string phoneNo = "";
            string dateService = "";
            string timeService = "";
            int deliveredStatus = 0;
            string messageToSend = "";

            if (selectedPelangganID > 0)
            {
                messageToSend = "SEND SMS KONFIRMASI";

                phoneNo = DS.getDataSingleValue("SELECT IFNULL(CUSTOMER_PHONE, '') FROM MASTER_CUSTOMER WHERE CUSTOMER_ID = " + selectedPelangganID).ToString();
                dateService = String.Format(culture, "{0:dd-MM-yyyy}", jobStartDateTimePicker.Value);
                timeService = jobStartTimeMaskedTextBox.Text;

                smsLib.start_SMSGateWayPortConnection();

                smsLib.sendMessage(phoneNo, messageToSend, ref deliveredStatus);

                smsLib.stop_SMSGateWayPortConnection();
            }
        }

        private bool saveDataTransaction()
        {
            bool result = false;
            string sqlCommand = "";

            string salesInvoice = "0";
            string salesInvoiceTax = "0";
            string salesRevNo = "0";

            string SODateTime = "";
            DateTime SODueDateTimeValue;
            string SODueDateTime = "";
            string salesDiscountFinal = "0";
            int salesTop = 1;
            int salesPaid = 0;
            MySqlException internalEX = null;

            double disc1 = 0;
            double disc2 = 0;
            double discRP = 0;
            string productID = "";
            //int paymentMethod = 0;

            double currentTaxTotal = 0;
            double currentSalesTotal = 0;
            double taxLimitValue = 0;
            double parameterCalculation = 0;
            int taxLimitType = 0; // 0 - percentage, 1 - amount
            string salesDateValue = "";
            bool addToTaxTable = false;
            string jobScheduledDate = "";
            string jobScheduledTime = "";
            string jobFinishedDate = "";
            string jobFinishedTime = "";

            int salesPersonID = 0;
            double commissionPercentage = 0;
            double commissionValue = 0;
            string currentYear = "0";
            int numericCurrentYear = 0;
            string currentMonth = "0";
            int numericCurrentMonth = 0;
            int numRows = 0;

            SODateTime = gutil.getCustomStringFormatDate(DateTime.Now);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ATTEMPT TO SAVE SALES DATA [" + SODateTime + "]");

            if (discJualMaskedTextBox.Text.Length > 0)
            { 
                salesDiscountFinal = discValue.ToString();
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES DISC FINAL [" + discJualMaskedTextBox.Text + "]");
            }

            if (cashRadioButton.Checked)
            {
                salesTop = 1;
                salesPaid = 1;
                SODueDateTime = gutil.getCustomStringFormatDate(DateTime.Now, true); //String.Format(culture, "{0:dd-MM-yyyy}", DateTime.Now); ;
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CASH SALES");
                //paymentMethod = paymentComboBox.SelectedIndex;
            }
            else
            { 
                salesTop = 0;
                salesPaid = 0;
                SODueDateTimeValue = DateTime.Now;
                SODueDateTimeValue.AddDays(Convert.ToInt32(tempoMaskedTextBox.Text));
                SODueDateTimeValue = SODueDateTimeValue.AddDays(Convert.ToInt32(tempoMaskedTextBox.Text));
                SODueDateTime = String.Format(culture, "{0:dd-MM-yyyy}", SODueDateTimeValue);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NON CASH - SALES DUE DATE ["+ SODueDateTime + "]");
            }


            if (originModuleID == 0)
            {
                // TAX LIMIT CALCULATION
                // ----------------------------------------------------------------------
                salesDateValue = String.Format(culture, "{0:yyyyMMdd}", DateTime.Now);
                currentTaxTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(SALES_TOTAL), 0) AS TOTAL FROM SALES_HEADER_TAX WHERE DATE_FORMAT(SALES_DATE, '%Y%m%d') = '" + salesDateValue + "'"));
                currentSalesTotal = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(SUM(SALES_TOTAL), 0) AS TOTAL FROM SALES_HEADER WHERE DATE_FORMAT(SALES_DATE, '%Y%m%d') = '" + salesDateValue + "'"));

                // CHECK WHETHER THE PARAMETER FOR TAX CALCULATION HAS BEEN SET
                taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(PERSENTASE_PENJUALAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CHECK IF TAX LIMIT SET FOR PERCENTAGE PURCHASE [" + taxLimitValue + "]");

                if (taxLimitValue == 0)
                {
                    taxLimitType = 1;
                    taxLimitValue = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(AVERAGE_PENJUALAN_HARIAN, 0) FROM SYS_CONFIG_TAX WHERE ID = 1"));
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CHECK IF TAX LIMIT SET FOR AVERAGE DAILY PURCHASE [" + taxLimitValue + "]");

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
                        parameterCalculation = currentSalesTotal * taxLimitValue / 100;
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "PERCENTAGE CALCULATION [" + parameterCalculation + "]");

                        if (currentTaxTotal > parameterCalculation)
                        {
                            addToTaxTable = false;
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CURRENT TAX TOTAL IS BIGGER THAN PARAMETER CALCULATION");
                        }
                    }
                    else // AMOUNT CALCULATION
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "AMOUNT CALCULATION [" + taxLimitValue + "]");
                        if (currentTaxTotal > taxLimitValue)
                        {
                            addToTaxTable = false;
                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CURRENT TAX TOTAL IS BIGGER THAN AMOUNT CALCULATION");
                        }
                    }
                }
            }
            else if (originModuleID == globalConstants.DUMMY_TRANSACTION_TAX)
                addToTaxTable = true;
            else if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU ||
                originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU ||
                originModuleID == globalConstants.MONITOR_SURAT_TUGAS
                )
                // SURAT TUGAS DOESN'T NEED TO GO TO TAX TABLE
                addToTaxTable = false;
                // ----------------------------------------------------------------------

                DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                if (originModuleID == 0)   // NORMAL TRANSACTION
                { 
                    salesInvoice = getSalesInvoiceID();
                    //pass thru to receipt generator
                    selectedsalesinvoice = salesInvoice;
                    // SAVE HEADER TABLE
                    sqlCommand = "INSERT INTO SALES_HEADER (SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SALES_DISC_PERCENT_FINAL) " +
                                        "VALUES " +
                                        "('" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", " + gutil.validateDecimalNumericInput(discPercentValue) + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                else if (originModuleID == globalConstants.SALES_ORDER_REVISION)   // SALES ORDER REVISION
                {
                    salesInvoice = selectedsalesinvoice;
                    salesRevNo = gutil.getLatestRevisionNo(salesInvoice);
                    int prevRevNo = Convert.ToInt32(salesRevNo) - 1;
                    selectedsalesinvoiceRevNo = salesRevNo;
                    // VOID LAST REVISION NO
                    sqlCommand = "UPDATE SALES_HEADER SET SALES_VOID = 1, SALES_ACTIVE = 0 WHERE SALES_INVOICE = '"+salesInvoice+"' AND REV_NO = " + prevRevNo;
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE PREVIOUS REV_NO TO VOID [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // SAVE HEADER TABLE
                    sqlCommand = "INSERT INTO SALES_HEADER (SALES_INVOICE, REV_NO, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD, SQ_INVOICE, SALES_DISC_PERCENT_FINAL) " +
                                        "VALUES " +
                                        "('" + salesInvoice + "', " + salesRevNo + ", " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ", '" + selectedSQInvoice + "', " + gutil.validateDecimalNumericInput(discPercentValue) + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                }
                else if (originModuleID == globalConstants.SALES_QUOTATION)
                {
                    salesInvoice = getSalesQuotationID();
                    //pass thru to receipt generator
                    selectedsalesinvoice = salesInvoice;
                    // SAVE HEADER TABLE
                    sqlCommand = "INSERT INTO SALES_QUOTATION_HEADER (SQ_INVOICE, CUSTOMER_ID, SQ_DATE, SQ_TOTAL, SALES_DISCOUNT_FINAL, SQ_TOP, SQ_TOP_DATE, SQ_APPROVED, SALESPERSON_ID, SALES_DISC_PERCENT_FINAL) " +
                                        "VALUES " +
                                        "('" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), 0, " + gutil.getUserID() + ", " + gutil.validateDecimalNumericInput(discPercentValue) + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES QUOTATION HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                else if (originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                {
                    salesInvoice = selectedsalesinvoice;
                    // UPDATE HEADER TABLE
                    sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET CUSTOMER_ID = " + selectedPelangganID + ", " +
                                            "SQ_TOTAL = " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " +
                                            "SALES_DISCOUNT_FINAL = " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " +
                                            "SALES_DISC_PERCENT_FINAL = " + gutil.validateDecimalNumericInput(discPercentValue) + ", " +
                                            "SQ_TOP = " + salesTop + ", " +
                                            "SQ_TOP_DATE = STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y') " +
                                            "WHERE SQ_INVOICE = '" + salesInvoice + "'"; 

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE SALES QUOTATION HEADER [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // DELETE DETAIL TABLE CONTENT
                    sqlCommand = "DELETE FROM SALES_QUOTATION_DETAIL WHERE SQ_INVOICE = '" + salesInvoice + "'";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CLEAR SALES_QUOTATION_DETAIL [" + salesInvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                else if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU)
                {
                    jobScheduledDate = gutil.getCustomStringFormatDate(jobStartDateTimePicker.Value);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
                    jobScheduledTime = jobStartTimeMaskedTextBox.Text;

                    jobFinishedDate = gutil.getCustomStringFormatDate(finishedDateTimePicker.Value);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
                    jobFinishedTime = finishedTimeMaskedTextBox.Text;

                    // SAVE HEADER TABLE
                    sqlCommand = "INSERT INTO SURAT_TUGAS_HEADER (SALES_INVOICE, CUSTOMER_ID, SALES_DATE, TECHNICIAN_ID, JOB_SCHEDULED_DATE, JOB_SCHEDULED_TIME, JOB_FINISHED_DATE, JOB_FINISHED_TIME, IS_JOB_FINISHED) " +
                                        "VALUES " +
                                        "('" + selectedsalesinvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + selectedTechnicianID +
                                        ", STR_TO_DATE('" + jobScheduledDate + "', '%d-%m-%Y %H:%i'), '" + jobScheduledTime + "', STR_TO_DATE('" + jobFinishedDate + "', '%d-%m-%Y %H:%i'), '" + jobFinishedTime + "', 0)";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SURAT TUGAS HEADER [" + selectedsalesinvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }
                else if (originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                {
                    jobScheduledDate = gutil.getCustomStringFormatDate(jobStartDateTimePicker.Value);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
                    jobScheduledTime = jobStartTimeMaskedTextBox.Text;

                    jobFinishedDate = gutil.getCustomStringFormatDate(finishedDateTimePicker.Value);//String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);
                    jobFinishedTime = finishedTimeMaskedTextBox.Text;

                    // UPDATE HEADER TABLE
                    sqlCommand = "UPDATE SURAT_TUGAS_HEADER SET " +
                                           "CUSTOMER_ID = "+ selectedPelangganID + ", " +
                                           "TECHNICIAN_ID = " + selectedTechnicianID + ", " +
                                           "JOB_SCHEDULED_DATE = STR_TO_DATE('" + jobScheduledDate + "', '%d-%m-%Y %H:%i'), " +
                                           "JOB_SCHEDULED_TIME  = '" + jobScheduledTime + "', " +
                                           "JOB_FINISHED_DATE = STR_TO_DATE('" + jobFinishedDate + "', '%d-%m-%Y %H:%i'), " +
                                           "JOB_FINISHED_TIME  = '" + jobFinishedTime + "' " +
                                        "WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE SURAT TUGAS [" + selectedsalesinvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // DELETE DETAIL TABLE CONTENT
                    sqlCommand = "DELETE FROM SURAT_TUGAS_DETAIL WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CLEAR SURAT TUGAS [" + selectedsalesinvoice + "]");
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                if (addToTaxTable)
                {
                    salesInvoiceTax = getSalesInvoiceID(false);
                    selectedsalesinvoiceTax = salesInvoiceTax;
                    sqlCommand = "INSERT INTO SALES_HEADER_TAX (SALES_INVOICE, ORIGIN_SALES_INVOICE, CUSTOMER_ID, SALES_DATE, SALES_TOTAL, SALES_DISCOUNT_FINAL, SALES_TOP, SALES_TOP_DATE, SALES_PAID, SALES_PAYMENT, SALES_PAYMENT_CHANGE, SALES_PAYMENT_METHOD) " +
                                        "VALUES " +
                                        "('" + salesInvoiceTax + "', '" + salesInvoice + "', " + selectedPelangganID + ", STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i'), " + gutil.validateDecimalNumericInput(globalTotalValue) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(salesDiscountFinal)) + ", " + salesTop + ", STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + salesPaid + ", " + gutil.validateDecimalNumericInput(bayarAmount) + ", " + gutil.validateDecimalNumericInput(sisaBayar) + ", " + selectedPaymentMethod + ")";

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES HEADER TAX [" + salesInvoice + "]");

                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                // SAVE DETAIL TABLE
                for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
                {
                    if (null != cashierDataGridView.Rows[i].Cells["productID"].Value)
                    {
                        disc1 = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["disc1"].Value);
                        disc2 = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["disc2"].Value);
                        discRP = Convert.ToDouble(cashierDataGridView.Rows[i].Cells["discRP"].Value);
                        productID = cashierDataGridView.Rows[i].Cells["productID"].Value.ToString();

                        if (originModuleID == 0 || originModuleID == globalConstants.SERVICE_AC) // NORMAL TRANSACTION
                        {
                            sqlCommand = "INSERT INTO SALES_DETAIL (SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL) " +
                                                "VALUES " +
                                                "('" + salesInvoice + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL[" + productID + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }
                        else if (originModuleID == globalConstants.SALES_ORDER_REVISION) // SALES ORDER REVISION
                        {
                            sqlCommand = "INSERT INTO SALES_DETAIL (SALES_INVOICE, REV_NO, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL) " +
                                                "VALUES " +
                                                "('" + salesInvoice + "', " + salesRevNo + ", '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL[" + productID + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }
                        else if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
                        {
                            sqlCommand = "INSERT INTO SALES_QUOTATION_DETAIL (SQ_INVOICE, PRODUCT_ID, PRODUCT_SALES_PRICE, PRODUCT_QTY, SQ_SUBTOTAL) " +
                                                "VALUES " +
                                                "('" + salesInvoice + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES QUOTATION DETAIL[" + productID + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }

                        if (addToTaxTable)
                        {
                            sqlCommand = "INSERT INTO SALES_DETAIL_TAX (SALES_INVOICE, PRODUCT_ID, PRODUCT_PRICE, PRODUCT_SALES_PRICE, PRODUCT_QTY, PRODUCT_DISC1, PRODUCT_DISC2, PRODUCT_DISC_RP, SALES_SUBTOTAL) " +
                                                "VALUES " +
                                                "('" + salesInvoiceTax + "', '" + productID + "', " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["hpp"].Value) + ", " + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["productPrice"].Value) + ", " +
                                                gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ", " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value)) + ")";

                            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SALES DETAIL TAX [" + productID + "]");
                            if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                throw internalEX;
                        }

                        if (originModuleID == 0 || originModuleID == globalConstants.SERVICE_AC)  // NORMAL TRANSACTION
                        {
                            //// REDUCE STOCK QTY AT MASTER PRODUCT
                            //int locationID = gutil.loadlocationID(2);

                            //// REDUCE STOCK AT MASTER STOCK
                            //sqlCommand = "UPDATE MASTER_PRODUCT SET PRODUCT_STOCK_QTY = PRODUCT_STOCK_QTY - " + cashierDataGridView.Rows[i].Cells["qty"].Value.ToString() + " WHERE PRODUCT_ID = '" + productID + "'";
                            //if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            //    throw internalEX;

                            //// REDUCE STOCK AT PRODUCT LOCATION
                            //sqlCommand = "UPDATE PRODUCT_LOCATION SET PRODUCT_LOCATION_QTY = PRODUCT_LOCATION_QTY - " + cashierDataGridView.Rows[i].Cells["qty"].Value.ToString() + " WHERE PRODUCT_ID = '" + productID + "' AND LOCATION_ID = " + locationID;
                            //if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            //    throw internalEX;

                            // SAVE OR UPDATE TO CUSTOMER_PRODUCT_DISC
                            if (selectedPelangganID != 0 && originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU)
                            {
                                if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + cashierDataGridView.Rows[i].Cells["productID"].Value.ToString() + "'"))>0)
                                {
                                    // UPDATE VALUE
                                    sqlCommand = "UPDATE CUSTOMER_PRODUCT_DISC SET DISC_1 = " + gutil.validateDecimalNumericInput(disc1) + ", DISC_2 = " + gutil.validateDecimalNumericInput(disc2) + ", DISC_RP = " + gutil.validateDecimalNumericInput(discRP) + " WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + productID + "'";
                                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "UPDATE CUSTOMER PRODUCT DISC [" + productID + "]");
                                }
                                else
                                {
                                    // INSERT VALUE
                                    sqlCommand = "INSERT INTO CUSTOMER_PRODUCT_DISC (CUSTOMER_ID, PRODUCT_ID, DISC_1, DISC_2 , DISC_RP) VALUES " +
                                                        "(" + selectedPelangganID + ", '" + productID + "', " + gutil.validateDecimalNumericInput(disc1) + ", " + gutil.validateDecimalNumericInput(disc2) + ", " + gutil.validateDecimalNumericInput(discRP) + ")";
                                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT CUSTOMER PRODUCT DISC [" + productID + "]");
                                }

                                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                                    throw internalEX;
                            }
                        }
                    }

                    if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                    {
                        sqlCommand = "INSERT INTO SURAT_TUGAS_DETAIL (SALES_INVOICE, PRODUCT_ID, PRODUCT_QTY) " +
                                                "VALUES " +
                                                "('" + selectedsalesinvoice + "', '" + productID + "', " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + ")";

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT INTO SURAT TUGAS DETAIL[" + productID + ", " + gutil.validateDecimalNumericInput(Convert.ToDouble(cashierDataGridView.Rows[i].Cells["qty"].Value)) + "]");

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                }

                if (originModuleID == 0 || originModuleID == globalConstants.SALES_ORDER_REVISION)  // NORMAL TRANSACTION
                {
                    // CHECK WHETHER AN ENTRY FOR CREDIT HAS BEEN CREATED OR NOT
                    numRows = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CREDIT WHERE SALES_INVOICE = '" + salesInvoice + "'"));

                    if (numRows > 0)
                    {
                        // UPDATE CREDIT TABLE TO REFLECT REVISION
                        sqlCommand = "UPDATE CREDIT SET CREDIT_DUE_DATE = STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + "CREDIT_NOMINAL = " + gutil.validateDecimalNumericInput(globalTotalValue- Convert.ToDouble(salesDiscountFinal)) +
                                               ", CREDIT_PAID = " + salesPaid + " WHERE SALES_INVOICE = '" + salesInvoice + "'";

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO CREDIT TABLE [" + salesInvoice + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                    else
                    { 
                        // SAVE TO CREDIT TABLE
                        sqlCommand = "INSERT INTO CREDIT (SALES_INVOICE, CREDIT_DUE_DATE, CREDIT_NOMINAL, CREDIT_PAID) VALUES " +
                                            "('" + salesInvoice + "', STR_TO_DATE('" + SODueDateTime + "', '%d-%m-%Y'), " + gutil.validateDecimalNumericInput(globalTotalValue- Convert.ToDouble(salesDiscountFinal)) + ", " + salesPaid + ")";

                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO CREDIT TABLE [" + salesInvoice + "]");
                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }

                    if (selectedPaymentMethod == 0)
                    {
                        // PAYMENT IN CASH THEREFORE ADDING THE AMOUNT OF CASH IN THE CASH REGISTER
                        // ADD A NEW ENTRY ON THE DAILY JOURNAL TO KEEP TRACK THE ADDITIONAL CASH AMOUNT 
                        sqlCommand = "INSERT INTO DAILY_JOURNAL (ACCOUNT_ID, JOURNAL_DATETIME, JOURNAL_NOMINAL, JOURNAL_DESCRIPTION, USER_ID, PM_ID) " +
                                                       "VALUES (1, STR_TO_DATE('" + SODateTime + "', '%d-%m-%Y %H:%i')" + ", " + gutil.validateDecimalNumericInput(globalTotalValue) + ", 'PEMBAYARAN " + salesInvoice + "', '" + gutil.getUserID() + "', 1)";
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "INSERT TO DAILY JOURNAL TABLE [" + gutil.validateDecimalNumericInput(globalTotalValue) + "]");

                        if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                            throw internalEX;
                    }
                }

                DS.commit();
                result = true;
            }
            catch (Exception e)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "EXCEPTION THROWN [" + e.Message + "]");

                try
                {
                    DS.rollBack(ref internalEX);
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

        private void saveAndPrintOutInvoice()
        {
            string message = "";

            if (printoutCheckBox.Checked == false)
                message = "SAVE AND PRINT OUT ?";
            else
                message = "SAVE DATA ?";

            if (DialogResult.Yes == MessageBox.Show(message, "WARNING", MessageBoxButtons.YesNo,MessageBoxIcon.Warning))
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "ATTEMPT TO SAVE AND PRINT OUT INVOICE");

                totalPenjualanTextBox.Focus();
                if (saveData())
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "TRANSACTION SAVED");

                    gutil.saveUserChangeLog(globalConstants.MENU_PENJUALAN, globalConstants.CHANGE_LOG_INSERT, "NEW TRANSAKSI PENJUALAN [" + selectedsalesinvoice + "]");

                    if (printoutCheckBox.Checked == false)
                    { 
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "PRINT OUT INVOICE");
                        PrintReceipt();
                    }

                    if (( originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU) 
                        &&  sendSMSCheckbox.Checked == true)
                    {
                        sendSMSConfirmation();
                    }

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "VALIDATE USER CREDIT STATUS");

                    validateUserCreditStatus();

                    gutil.showSuccess(gutil.INS);

                    if (originModuleID == 0)
                    { 
                        generateDO.Visible = true;
                        generateSuratTugas.Visible = true;
                    }
                    //clearUpScreen();
                }
            }
        }

        private string getNoFaktur()
        {
            string rsult = "";

            rsult = DS.getDataSingleValue("SELECT IFNULL(NO_FAKTUR, '') FROM SYS_CONFIG WHERE ID = 1").ToString();

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : NO FAKTUR [" + rsult + "]");

            return rsult;
        }

        private string getSalesInvoiceID(bool mainSalesTable = true)
        {
            string salesInvoice = "";
            DateTime localDate = DateTime.Now;
            string maxSalesInvoice = "";
            double maxSalesInvoiceValue = 0;
            string salesInvPrefix;
            string sqlCommand = "";

            salesInvPrefix = getNoFaktur() + "-";//String.Format(culture, "{0:yyyyMMdd}", localDate);

            if (mainSalesTable == true)
                sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SALES_INVOICE, INSTR(SALES_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SALES_INVOICE FROM SALES_HEADER WHERE SALES_INVOICE LIKE '" + salesInvPrefix + "%'";
            else
                sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SALES_INVOICE, INSTR(SALES_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SALES_INVOICE FROM SALES_HEADER_TAX WHERE SALES_INVOICE LIKE '" + salesInvPrefix + "%'";

            maxSalesInvoice = DS.getDataSingleValue(sqlCommand).ToString();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MAX SALES INVOICE [" + maxSalesInvoice + "]");

            maxSalesInvoiceValue = Convert.ToInt32(maxSalesInvoice);
            if (maxSalesInvoiceValue > 0)
            {
                maxSalesInvoiceValue += 1;
                maxSalesInvoice = maxSalesInvoiceValue.ToString();
            }
            else
            {
                maxSalesInvoice = "1";
            }

            salesInvoice = salesInvPrefix + maxSalesInvoice;
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES INVOICE [" + salesInvoice + "]");

            return salesInvoice;
        }

        private string getSalesQuotationID()
        {
            string salesInvoice = "";
            DateTime localDate = DateTime.Now;
            string maxSalesInvoice = "";
            double maxSalesInvoiceValue = 0;
            string salesInvPrefix;
            string sqlCommand = "";

            salesInvPrefix = "SQ-";//String.Format(culture, "{0:yyyyMMdd}", localDate);

            sqlCommand = "SELECT IFNULL(MAX(CONVERT(SUBSTRING(SQ_INVOICE, INSTR(SQ_INVOICE,'-')+1), UNSIGNED INTEGER)),'0') AS SQ_INVOICE FROM SALES_QUOTATION_HEADER WHERE SQ_INVOICE LIKE '" + salesInvPrefix + "%'";

            maxSalesInvoice = DS.getDataSingleValue(sqlCommand).ToString();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : MAX SALES INVOICE [" + maxSalesInvoice + "]");

            maxSalesInvoiceValue = Convert.ToInt32(maxSalesInvoice);
            if (maxSalesInvoiceValue > 0)
            {
                maxSalesInvoiceValue += 1;
                maxSalesInvoice = maxSalesInvoiceValue.ToString();
            }
            else
            {
                maxSalesInvoice = "1";
            }

            salesInvoice = salesInvPrefix + maxSalesInvoice;
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SALES INVOICE [" + salesInvoice + "]");

            return salesInvoice;
        }

        private double getProductPriceValue(string productID, int customerType = 0, bool hppValue = false)
        {
            double result = 0;
            string priceType = "";

            //DS.mySqlConnect();

            if (customerType == 0)
                priceType = "PRODUCT_RETAIL_PRICE";
            else if (customerType == 1)
                priceType = "PRODUCT_BULK_PRICE";
            else
                priceType = "PRODUCT_WHOLESALE_PRICE";

            if (hppValue)
                priceType = "PRODUCT_BASE_PRICE";

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE TYPE [" + priceType + "]");

            result = Convert.ToDouble(DS.getDataSingleValue("SELECT IFNULL(" + priceType + ", 0) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRODUCT PRICE [" + result + "]");

            return result;
        }

        private double calculateSubTotal(int rowSelectedIndex, double productPrice)
        {
            double subTotal = 0;
            double productQty = 0;
            double hppValue = 0;
            double disc1Value = 0;
            double disc2Value = 0;
            double discRPValue = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CALCULATE SUBTOTAL");

            try
            {
                //productQty = Convert.ToDouble(salesQty[rowSelectedIndex]);
                productQty = Convert.ToDouble(cashierDataGridView.Rows[rowSelectedIndex].Cells["qty"].Value);

                if (productQty > 0)
                {
                    hppValue = productPrice;

                    disc1Value = Convert.ToDouble(cashierDataGridView.Rows[rowSelectedIndex].Cells["disc1"].Value);
                    disc2Value = Convert.ToDouble(cashierDataGridView.Rows[rowSelectedIndex].Cells["disc2"].Value);
                    discRPValue = Convert.ToDouble(cashierDataGridView.Rows[rowSelectedIndex].Cells["discRP"].Value);

                    subTotal = Math.Round((hppValue * productQty), 2);

                    if (disc1Value > 0)
                        subTotal = Math.Round(subTotal - (subTotal * disc1Value / 100), 2);

                    if (disc2Value > 0)
                        subTotal = Math.Round(subTotal - (subTotal * disc2Value / 100), 2);

                    if (discRPValue > 0)
                        subTotal = Math.Round(subTotal - discRPValue, 2);
                }

            }
            catch (Exception ex)
            {
                subTotal = 0;
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : SUBTOTAL [" + subTotal + "]");
            return subTotal;
        }

        private void calculateTotal()
        {
            double total = 0;
            double discJual = 0;
            double discJualPercent = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal");
            for (int i = 0; i < cashierDataGridView.Rows.Count; i++)
            {
                if (null != cashierDataGridView.Rows[i].Cells["jumlah"].Value)
                    total = total + Convert.ToDouble(cashierDataGridView.Rows[i].Cells["jumlah"].Value);
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal [" + total + "]");

            globalTotalValue = total;
            totalAfterDisc = total;
            //totalLabel.Text = total.ToString("c2", culture);

            totalPenjualanTextBox.Text = total.ToString("c2", culture);

            if (discJualPersenTextBox.Text.Length > 0)
            {
                discJualPercent = Convert.ToDouble(discJualPersenTextBox.Text);
                totalAfterDisc = totalAfterDisc - Math.Round((totalAfterDisc * discJualPercent) / 100, 2);
            }

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                discJual = discValue;// Convert.ToDouble(discJualMaskedTextBox.Text);
                totalAfterDisc = Math.Round(totalAfterDisc - discJual, 2);
            }
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateTotal, totalAfterDisc [" + totalAfterDisc + "]");

            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("c2", culture);
            totalLabel.Text = totalAfterDisc.ToString("c2", culture);

            calculateChangeValue();
        }

        private void calculateChangeValue()
        {
            double totalAfterDisc = 0;
            double discJualPercent = 0;
            double discJual = 0;

            string bayarTextBoxValue = gutil.allTrim(bayarTextBox.Text);
            if (bayarTextBox.Text.Length > 0)
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateChangeValue, bayarTextBox.Text [" + bayarTextBox.Text + "]");
                bayarAmount = Convert.ToDouble(bayarTextBoxValue);

                totalAfterDisc = globalTotalValue;
                if (discJualPersenTextBox.Text.Length > 0)
                {
                    discJualPercent = Convert.ToDouble(discJualPersenTextBox.Text);
                    totalAfterDisc = totalAfterDisc - Math.Round((totalAfterDisc * discJualPercent) / 100, 2);
                }

                if (discJualMaskedTextBox.Text.Length > 0)
                {
                    discJual = discValue;// Convert.ToDouble(discJualMaskedTextBox.Text);
                    totalAfterDisc = Math.Round(totalAfterDisc - discJual, 2);
                }

                if (bayarAmount > totalAfterDisc)
                    sisaBayar = bayarAmount - totalAfterDisc;
                else
                    sisaBayar = 0;

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculateChangeValue, sisaBayar [" + sisaBayar + "]");
                uangKembaliTextBox.Text = sisaBayar.ToString("C2", culture);
            }
        }

        //private bool stockIsEnough(string productID, double qtyRequested)
        //{
        //    bool result = false;

        //    //if (originModuleID == globalConstants.PRE_ORDER_SALES)
        //    //{
        //    //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PRE ORDER SALES, CHECK STOCK QTY ALWAYS TRUE");
        //    //    return true;
        //    //}

        //    //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK STOCK QTY IS ENOUGH [" + productID + "]");

        //    //if (gutil.productIsService(productID))
        //    //    result = true;
        //    //else
        //    //{
        //    //    if (productID.Length <= 0)
        //    //       result = true; // NO PRODUCT SELECTED YET
        //    //    else
        //    //    {
        //    //            double stockQty = 0;

        //    //            stockQty = Convert.ToDouble(DS.getDataSingleValue("SELECT (PRODUCT_STOCK_QTY - PRODUCT_LIMIT_STOCK) FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + productID + "'"));

        //    //            if (stockQty >= qtyRequested)
        //    //                result = true;
                
        //    //            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : CHECK STOCK QTY IS ENOUGH [" + stockQty + ", " + qtyRequested + "]");
        //    //    }
        //    //}
        //    return result;
        //}

        private void setTextBoxCustomSource(TextBox textBox)
        {
            MySqlDataReader rdr;
            string sqlCommand = "";
            string[] arr = null;
            List<string> arrList = new List<string>();

            sqlCommand = "SELECT PRODUCT_NAME FROM MASTER_PRODUCT WHERE PRODUCT_ACTIVE = 1";
            rdr = DS.getData(sqlCommand);

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    arrList.Add(rdr.GetString("PRODUCT_NAME"));
                }
                AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                arr = arrList.ToArray();
                collection.AddRange(arr);

                textBox.AutoCompleteCustomSource = collection;
            }

            rdr.Close();
        }

        private void cashierDataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((cashierDataGridView.CurrentCell.OwningColumn.Name == "productName")
                && e.Control is TextBox)
            {
                TextBox productIDTextBox = e.Control as TextBox;
                productIDTextBox.CharacterCasing = CharacterCasing.Upper;
                //productIDTextBox.TextChanged -= TextBox_TextChanged;
                productIDTextBox.PreviewKeyDown -= Combobox_previewKeyDown;
                productIDTextBox.PreviewKeyDown += Combobox_previewKeyDown;
                //productIDTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //productIDTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                productIDTextBox.AutoCompleteMode = AutoCompleteMode.None;
                //setTextBoxCustomSource(productIDTextBox);
            }

            if (
                (cashierDataGridView.CurrentCell.OwningColumn.Name == "productPrice" || cashierDataGridView.CurrentCell.OwningColumn.Name == "qty" || cashierDataGridView.CurrentCell.OwningColumn.Name == "disc1" || cashierDataGridView.CurrentCell.OwningColumn.Name == "disc2" || cashierDataGridView.CurrentCell.OwningColumn.Name == "discRP")
                && e.Control is TextBox)
            {
                TextBox textBox = e.Control as TextBox;
                //textBox.PreviewKeyDown -= Combobox_previewKeyDown;
                //textBox.TextChanged += TextBox_TextChanged;
                textBox.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void clearUpSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex)
        {
            selectedRow.Cells["productName"].Value = "";
            selectedRow.Cells["hpp"].Value = "0";
            selectedRow.Cells["productPrice"].Value = "0";
            selectedRow.Cells["disc1"].Value = "0";
            selectedRow.Cells["qty"].Value = "0";
            //salesQty[rowSelectedIndex] = "0";

            //disc1[rowSelectedIndex] = "0";
            selectedRow.Cells["disc2"].Value = "0";

            //disc2[rowSelectedIndex] = "0";
            selectedRow.Cells["discRP"].Value = "0";

            //discRP[rowSelectedIndex] = "0";
            selectedRow.Cells["jumlah"].Value = "0";
            //productPriceList[rowSelectedIndex] = "0";
            //jumlahList[rowSelectedIndex] = "0";

            calculateTotal();
        }

        private void updateSomeRowContents(DataGridViewRow selectedRow, int rowSelectedIndex, string currentValue)
        {
            int numRow = 0;
            string selectedProductID = "";
            string selectedProductName = "";
            //string selectedProductImg = "";
            double hpp = 0;
            //double subTotal = 0;
            MySqlDataReader rdr;
            string currentProductID = "";
            string currentProductName = "";
            bool changed = false;

            numRow = Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM MASTER_PRODUCT WHERE PRODUCT_NAME = '" + currentValue + "'"));

            if (numRow > 0)
            {
                selectedProductName = currentValue;

                if (null != selectedRow.Cells["productID"].Value)
                    currentProductID = selectedRow.Cells["productID"].Value.ToString();

                if (null != selectedRow.Cells["productName"].Value)
                    currentProductName = selectedRow.Cells["productName"].Value.ToString();

                selectedProductID = DS.getDataSingleValue("SELECT IFNULL(PRODUCT_ID,'') FROM MASTER_PRODUCT WHERE PRODUCT_NAME = '" + currentValue + "'").ToString();

                selectedRow.Cells["productId"].Value = selectedProductID;
                selectedRow.Cells["productName"].Value = selectedProductName;

                if (selectedProductID != currentProductID)
                    changed = true;

                if (selectedProductName != currentProductName)
                    changed = true;

                if (!changed)
                    return;

                //selectedProductImg = DS.getDataSingleValue("SELECT IFNULL(PRODUCT_PHOTO_1, '') FROM MASTER_PRODUCT WHERE PRODUCT_ID = '" + selectedProductID + "'").ToString();

                //if (selectedProductImg.Length > 0)
                //{
                //    string imagePath = Application.StartupPath + "\\PRODUCT_PHOTO\\" + selectedProductImg;
                //    Size imageSize = new Size(50, 50);
                //    Bitmap productImage = new Bitmap(imagePath);
                //    Bitmap resizedImage = new Bitmap(productImage, imageSize);
                //    //productImage.

                //    DataGridViewImageCell cell = (DataGridViewImageCell) selectedRow.Cells["productImage"];
                //    cell.Value = resizedImage;
                //}

                hpp = getProductPriceValue(selectedProductID, customerComboBox.SelectedIndex, true);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, PRODUCT_BASE_PRICE [" + hpp + "]");
                selectedRow.Cells["hpp"].Value = hpp;

                hpp = getProductPriceValue(selectedProductID, customerComboBox.SelectedIndex);
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, PRODUCT_SALES_PRICE [" + hpp + "]");
                selectedRow.Cells["productPrice"].Value = hpp;
                //productPriceList[rowSelectedIndex] = hpp.ToString();

                //if (null == selectedRow.Cells["qty"].Value)
                selectedRow.Cells["qty"].Value = 0;
                //salesQty[rowSelectedIndex] = "0";

                if (selectedPelangganID != 0)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, CHECK FOR CUST PRODUCT DISC, selectedPelangganID [" + selectedPelangganID + "]");
                    if (Convert.ToInt32(DS.getDataSingleValue("SELECT COUNT(1) FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + selectedProductID + "'")) > 0)
                    {
                        // DATA EXIST, LOAD DISC VALUE
                        using (rdr = DS.getData("SELECT * FROM CUSTOMER_PRODUCT_DISC WHERE CUSTOMER_ID = " + selectedPelangganID + " AND PRODUCT_ID = '" + selectedProductID + "'"))
                        {
                            if (rdr.HasRows)
                            {
                                rdr.Read();

                                selectedRow.Cells["disc1"].Value = rdr.GetString("DISC_1");
                                //disc1[rowSelectedIndex] = rdr.GetString("DISC_1");

                                selectedRow.Cells["disc2"].Value = rdr.GetString("DISC_2");
                               // disc2[rowSelectedIndex] = rdr.GetString("DISC_2");

                                selectedRow.Cells["discRP"].Value = rdr.GetString("DISC_RP");
                                //discRP[rowSelectedIndex] = rdr.GetString("DISC_RP");

                                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, LOAD DISC [" + rdr.GetString("DISC_1") + ", " + rdr.GetString("DISC_2") + ", " + rdr.GetString("DISC_RP") + "]");
                            }

                            rdr.Close();
                        }
                    }
                    else
                    {
                        selectedRow.Cells["disc1"].Value = 0;
                        //disc1[rowSelectedIndex] = "0";

                        selectedRow.Cells["disc2"].Value = 0;
                        //disc2[rowSelectedIndex] = "0";

                        selectedRow.Cells["discRP"].Value = 0;
                        //discRP[rowSelectedIndex] = "0";
                    }
                }

                //subTotal = calculateSubTotal(rowSelectedIndex, hpp);
                //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, subTotal [" + subTotal + "]");
                selectedRow.Cells["jumlah"].Value = "0";
                //jumlahList[rowSelectedIndex] = "0";

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ComboBox_SelectedIndexChanged, attempt to calculate total");

                calculateTotal();
            }
            else
            {
                clearUpSomeRowContents(selectedRow, rowSelectedIndex);
            }
        }

        private void Combobox_previewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            string currentValue = "";
            int rowSelectedIndex = 0;
            DataGridViewTextBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            if (cashierDataGridView.CurrentCell.OwningColumn.Name != "productName")
                return;

            if (e.KeyCode == Keys.Enter)
            {
                currentValue = dataGridViewComboBoxEditingControl.Text;
                rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

                if (currentValue.Length > 0)
                {
                    //updateSomeRowContents(selectedRow, rowSelectedIndex, currentValue);
                    //cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];
                    // CALL DATA PRODUK FORM WITH PARAMETER 
                    POSSearchProductForm browseProduk = new POSSearchProductForm(globalConstants.CASHIER_MODULE, this, currentValue, rowSelectedIndex);
                    browseProduk.ShowDialog(this);
                }
                else
                {
                    //clearUpSomeRowContents(selectedRow, rowSelectedIndex);
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            //int rowSelectedIndex = 0;
            //double subTotal = 0;
            //double productPrice = 0;
            //string productID = "";
            //string previousInput = "";
            //string tempString = "";

            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, isLoading [" + isLoading.ToString() + "]");

            //if (isLoading)
            //    return;

            //DataGridViewTextBoxEditingControl dataGridViewTextBoxEditingControl = sender as DataGridViewTextBoxEditingControl;

            //rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
            //DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, rowSelectedIndex [" + rowSelectedIndex + "]");

            ////if (cashierDataGridView.CurrentCell.ColumnIndex != 3 && cashierDataGridView.CurrentCell.ColumnIndex != 4 && cashierDataGridView.CurrentCell.ColumnIndex != 5 && cashierDataGridView.CurrentCell.ColumnIndex != 6)
            //if (cashierDataGridView.CurrentCell.OwningColumn.Name != "hpp" && 
            //    cashierDataGridView.CurrentCell.OwningColumn.Name != "qty" && 
            //    cashierDataGridView.CurrentCell.OwningColumn.Name != "disc1" && 
            //    cashierDataGridView.CurrentCell.OwningColumn.Name != "disc2" && 
            //    cashierDataGridView.CurrentCell.OwningColumn.Name != "discRP")

            //    return;

            //if (dataGridViewTextBoxEditingControl.Text.Length <= 0)
            //{
            //    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, empty texbox, reset ["+ cashierDataGridView.CurrentCell.OwningColumn.Name + "] value to 0");
            //    isLoading = true;
            //    // reset subTotal Value and recalculate total
            //    selectedRow.Cells["jumlah"].Value = 0;

            //    //if (detailRequestQtyApproved.Count >= rowSelectedIndex + 1)
            //    //    detailRequestQtyApproved[rowSelectedIndex] = "0";
            //    //switch (cashierDataGridView.CurrentCell.OwningColumn.Name)
            //    //{
            //    //    case "qty":
            //    //        salesQty[rowSelectedIndex] = "0";
            //    //        break;
            //    //    case "disc1":
            //    //        disc1[rowSelectedIndex] = "0";
            //    //        break;
            //    //    case "disc2":
            //    //        disc2[rowSelectedIndex] = "0";
            //    //        break;
            //    //    case "discRP":
            //    //        discRP[rowSelectedIndex] = "0";
            //    //        break;
            //    //}

            //    dataGridViewTextBoxEditingControl.Text = "0";
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, recalculate total value");
            //    calculateTotal();

            //    dataGridViewTextBoxEditingControl.SelectionStart = dataGridViewTextBoxEditingControl.Text.Length;
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, set the cursor to end of textbox");
            //    isLoading = false;
            //    return;
            //}

            //if (null != selectedRow.Cells["productID"].Value)
            //    productID = selectedRow.Cells["productID"].Value.ToString();

            ////switch (cashierDataGridView.CurrentCell.OwningColumn.Name)
            ////{
            ////    case "qty":
            ////        previousInput = salesQty[rowSelectedIndex];
            ////        break;
            ////    case "disc1":
            ////        previousInput = disc1[rowSelectedIndex];
            ////        break;
            ////    case "disc2":
            ////        previousInput = disc2[rowSelectedIndex];
            ////        break;
            ////    case "discRP":
            ////        previousInput = discRP[rowSelectedIndex];
            ////        break;
            ////}

            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, previousInput [" + previousInput + "]");

            //isLoading = true;
            //if (previousInput == "0")
            //{
            //    tempString = dataGridViewTextBoxEditingControl.Text;
            //    if (tempString.IndexOf('0') == 0 && tempString.Length > 1 && tempString.IndexOf("0.") < 0)
            //        dataGridViewTextBoxEditingControl.Text = tempString.Remove(tempString.IndexOf('0'), 1);

            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text [" + dataGridViewTextBoxEditingControl.Text + "]");
            //}
            
            //if (gutil.matchRegEx(dataGridViewTextBoxEditingControl.Text, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL)
            //    && (dataGridViewTextBoxEditingControl.Text.Length > 0 && dataGridViewTextBoxEditingControl.Text != ".")
            //    )
            //{
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text pass REGEX Checking");
            //    switch (cashierDataGridView.CurrentCell.OwningColumn.Name)
            //    {
            //        case "qty":                            
            //            if (gutil.stockIsEnough(productID, Convert.ToDouble(dataGridViewTextBoxEditingControl.Text)))
            //                salesQty[rowSelectedIndex] = dataGridViewTextBoxEditingControl.Text;
            //            else
            //                dataGridViewTextBoxEditingControl.Text = salesQty[rowSelectedIndex];
            //            break;
            //        case "disc1":
            //            disc1[rowSelectedIndex] = dataGridViewTextBoxEditingControl.Text;
            //            break;
            //        case "disc2":
            //            disc2[rowSelectedIndex] = dataGridViewTextBoxEditingControl.Text;
            //            break;
            //        case "discRP":
            //            discRP[rowSelectedIndex] = dataGridViewTextBoxEditingControl.Text;
            //            break;
            //    }
            //}
            //else
            //{
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text did not pass REGEX Checking");
            //    dataGridViewTextBoxEditingControl.Text = previousInput;
            //}

            //productPrice = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

            //subTotal = calculateSubTotal(rowSelectedIndex, productPrice);
            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, subtotal value [" + subTotal + "]");
            //selectedRow.Cells["jumlah"].Value = subTotal;

            //gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, attempt to calculate total value");
            //calculateTotal();
            //dataGridViewTextBoxEditingControl.SelectionStart = dataGridViewTextBoxEditingControl.Text.Length;

            //isLoading = false;
        }

        private void cashierForm_Shown(object sender, EventArgs e)
        {
            //registerGlobalHotkey();
        }

        private void cashierForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            unregisterGlobalHotkey();
        }

        private void creditRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (creditRadioButton.Checked == true)
            {
                paymentComboBox.Visible = false;
                tempoMaskedTextBox.Visible = true;
                bayarTextBox.Enabled = false;  
                labelCaraBayar.Text = "Tempo            :";
            }
        }

        private void loadNoFaktur()
        {
            string noFakturValue;

            noFakturValue = DS.getDataSingleValue("SELECT NO_FAKTUR FROM SYS_CONFIG").ToString();
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadNoFaktur, noFakturValue [" + noFakturValue + "]");
            noFakturLabel.Text = noFakturValue;
        }

        private void loadInvoiceData()
        {
            string sqlCommand;
            int TOPValue;
            int TOPDuration;
            MySqlDataReader rdr;
            int rowPos = 0;
            int numRow = 0;

           // salesQty.Clear();
            isLoading = true;
            switch (originModuleID)
            {
                case globalConstants.SERVICE_AC:
                    break;
                case globalConstants.TUGAS_PEMASANGAN_BARU:
                case globalConstants.MONITOR_SURAT_TUGAS:
                    // CHECK WHETHER SURAT TUGAS HAS BEEN GENERATED PREVIOUSLY
                    sqlCommand = "SELECT COUNT(1) AS 'NUM' FROM SURAT_TUGAS_HEADER WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";

                    rdr = DS.getData(sqlCommand);
                    rdr.Read();
                    numRow = rdr.GetInt32("NUM");
                    rdr.Close();

                    if (numRow > 0)
                    {
                        // PULL HEADER DATA
                        sqlCommand = "SELECT SH.IS_JOB_FINISHED, SH.SALES_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, '') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.TECHNICIAN_ID, IFNULL(MT.TECHNICIAN_NAME, '') AS 'TECHNICIAN_NAME', IFNULL(SH.JOB_SCHEDULED_DATE, '') AS JOB_SCHEDULED_DATE, IFNULL(SH.JOB_SCHEDULED_TIME, '') AS JOB_SCHEDULED_TIME, IFNULL(SH.JOB_FINISHED_DATE, '') AS JOB_FINISHED_DATE, IFNULL(SH.JOB_FINISHED_TIME, '') AS JOB_FINISHED_TIME " +
                                               "FROM SURAT_TUGAS_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) LEFT OUTER JOIN MASTER_TECHNICIAN MT ON (SH.TECHNICIAN_ID = MT.ID) WHERE SH.SALES_INVOICE = '" + selectedsalesinvoice + "'";

                        rdr = DS.getData(sqlCommand);
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                pelangganTextBox.Text = rdr.GetString("NAMA");
                                noFakturLabel.Text = rdr.GetString("NO_INVOICE");

                                customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                                customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                                selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                                selectedTechnicianID = rdr.GetInt32("TECHNICIAN_ID");
                                teknisiTextbox.Text = rdr.GetString("TECHNICIAN_NAME");

                                jobStartDateTimePicker.Value = rdr.GetDateTime("JOB_SCHEDULED_DATE");
                                jobStartTimeMaskedTextBox.Text = rdr.GetString("JOB_SCHEDULED_TIME");

                                if (rdr.GetString("JOB_FINISHED_DATE").Length > 0)
                                    finishedDateTimePicker.Value = rdr.GetDateTime("JOB_FINISHED_DATE");

                                if (rdr.GetString("JOB_FINISHED_TIME").Length > 0)
                                    finishedTimeMaskedTextBox.Text = rdr.GetString("JOB_FINISHED_TIME");

                                isJobFinished = rdr.GetInt32("IS_JOB_FINISHED");
                            }
                        }
                        rdr.Close();

                        //if (originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
                        //{
                        //    // DISABLE JOB START DATE AND TIME FOR MONITOR SURAT TUGAS
                        //    jobStartDateTimePicker.Enabled = false;
                        //    jobStartTimeMaskedTextBox.ReadOnly= true;
                        //}

                        // SURAT TUGAS HAS BEEN GENERATED PREVIOUSLY, PULL DATA FROM SURAT_TUGAS
                        originModuleID = globalConstants.EDIT_TUGAS_PEMASANGAN_BARU;

                        // PULL DETAIL DATA
                        sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_QTY AS QTY " +
                                               "FROM SURAT_TUGAS_DETAIL SD, MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                        rdr = DS.getData(sqlCommand);
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                addNewRow();
                                cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                                cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                                cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                                //salesQty[rowPos] = rdr.GetString("QTY");

                                rowPos += 1;
                            }
                        }
                        rdr.Close();

                        if (isJobFinished == 1)
                        {
                            finishedDateTimePicker.Enabled = false;
                            finishedTimeMaskedTextBox.ReadOnly = true;
                            finishedJobButton.Enabled = false;
                            cashierDataGridView.ReadOnly = true;
                        }
                    }
                    else
                    {
                        // NEW SURAT TUGAS, PULL DATA FROM SALES_ORDER
                        // PULL HEADER DATA
                        sqlCommand = "SELECT SH.SALES_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, '') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP " +
                                               "FROM SALES_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) WHERE SH.SALES_INVOICE = '" + selectedsalesinvoice + "'";

                        rdr = DS.getData(sqlCommand);
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                pelangganTextBox.Text = rdr.GetString("NAMA");
                                noFakturLabel.Text = rdr.GetString("NO_INVOICE");

                                customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                                customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                                selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");
                            }
                        }
                        rdr.Close();

                        // PULL DETAIL DATA
                        sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_QTY AS QTY " +
                                               "FROM SALES_DETAIL SD, MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                        rdr = DS.getData(sqlCommand);
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                addNewRow();
                                cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                                cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                                cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                                //salesQty[rowPos] = rdr.GetString("QTY");

                                rowPos += 1;
                            }
                        }
                        rdr.Close();
                    }

                    break;
                case globalConstants.EDIT_SALES_QUOTATION:
                    // PULL HEADER DATA
                    sqlCommand = "SELECT SH.SQ_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, 'P-UMUM') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.SQ_TOTAL AS TOTAL, SH.SALES_DISC_PERCENT_FINAL AS 'DISC_PERCENT', SH.SALES_DISCOUNT_FINAL AS DISC_FINAL, SH.SQ_TOP AS TOP, DATEDIFF(SH.SQ_TOP_DATE, SH.SQ_DATE) AS TOP_DURATION " +
                                           "FROM SALES_QUOTATION_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) WHERE SH.SQ_INVOICE = '" + selectedsalesinvoice + "'";
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pelangganTextBox.Text = rdr.GetString("NAMA");
                            noFakturLabel.Text = noFakturLabel.Text + " " + rdr.GetString("NO_INVOICE");

                            customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                            customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                            selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                            globalTotalValue = rdr.GetDouble("TOTAL");
                            totalPenjualanTextBox.Text = globalTotalValue.ToString("C2", culture);
                            totalAfterDisc = globalTotalValue;

                            discPercentValue = rdr.GetDouble("DISC_PERCENT");
                            discJualPersenTextBox.Text = discPercentValue.ToString();

                            discValue = rdr.GetDouble("DISC_FINAL");
                            discJualMaskedTextBox.Text = discValue.ToString();

                            double tempValue = 0;
                            
                            tempValue = Math.Round((globalTotalValue * discPercentValue) / 100);
                            totalAfterDisc = globalTotalValue - tempValue - discValue;

                            //totalLabel.Text = (globalTotalValue - discValue).ToString("C2", culture);
                            totalLabel.Text = totalAfterDisc.ToString("C2", culture);
                            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C2", culture);

                            TOPValue = rdr.GetInt32("TOP");
                            if (TOPValue == 1)
                                cashRadioButton.Checked = true;
                            else
                            { 
                                creditRadioButton.Checked = true;
                                TOPDuration = rdr.GetInt32("TOP_DURATION");
                                tempoMaskedTextBox.Text = TOPDuration.ToString();
                            }
                        }
                    }
                    rdr.Close();

                    // PULL DETAIL DATA               
                    sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, SD.PRODUCT_QTY AS QTY, SD.SQ_SUBTOTAL AS SUBTOTAL "+
                                           "FROM SALES_QUOTATION_DETAIL SD, MASTER_PRODUCT M WHERE SD.SQ_INVOICE = '" + selectedsalesinvoice + "' AND SD.PRODUCT_ID = M.PRODUCT_ID";
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            addNewRow();
                            cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productPrice"].Value = rdr.GetString("HARGA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                            //salesQty[rowPos] = rdr.GetString("QTY");
                            cashierDataGridView.Rows[rowPos].Cells["jumlah"].Value = rdr.GetString("SUBTOTAL");

                            rowPos += 1;
                        }
                    }
                    rdr.Close();
                    break;
                case globalConstants.SALES_ORDER_REVISION:
                    // PULL HEADER DATA
                    sqlCommand = "SELECT IFNULL(SH.SQ_INVOICE, '') AS SQ_INVOICE, SH.SALES_INVOICE AS NO_INVOICE, IFNULL(M.CUSTOMER_ID, 0) AS PELANGGAN_ID, IFNULL(M.CUSTOMER_FULL_NAME, 'P-UMUM') AS NAMA, IFNULL(M.CUSTOMER_GROUP, 1) AS CUSTOMER_GROUP, SH.SALES_TOTAL AS TOTAL, SH.SALES_DISCOUNT_FINAL AS DISC_FINAL, SH.SALES_DISC_PERCENT_FINAL AS DISC_PERCENT, SH.SALES_TOP AS TOP, DATEDIFF(SH.SALES_TOP_DATE, SH.SALES_DATE) AS TOP_DURATION, SH.SALES_PAYMENT " +
                                           "FROM SALES_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER M ON (SH.CUSTOMER_ID = M.CUSTOMER_ID) WHERE SH.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SH.REV_NO = " + selectedsalesinvoiceRevNo;
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            pelangganTextBox.Text = rdr.GetString("NAMA");
                            noFakturLabel.Text = selectedsalesinvoice + " / REV : " + selectedsalesinvoiceRevNo;

                            customerComboBox.SelectedIndex = rdr.GetInt32("CUSTOMER_GROUP") - 1;
                            customerComboBox.Text = customerComboBox.Items[customerComboBox.SelectedIndex].ToString();
                            selectedPelangganID = rdr.GetInt32("PELANGGAN_ID");

                            globalTotalValue = rdr.GetDouble("TOTAL");
                            totalPenjualanTextBox.Text = globalTotalValue.ToString("C2", culture);
                            totalAfterDisc = globalTotalValue;

                            discPercentValue = rdr.GetDouble("DISC_PERCENT");
                            discJualPersenTextBox.Text = discPercentValue.ToString();

                            discValue = rdr.GetDouble("DISC_FINAL");
                            discJualMaskedTextBox.Text = discValue.ToString();
                            totalLabel.Text = (globalTotalValue - discValue).ToString("C2", culture);

                            double tempValue = 0;

                            tempValue = Math.Round((globalTotalValue * discPercentValue) / 100);
                            totalAfterDisc = globalTotalValue - tempValue - discValue;

                            //totalLabel.Text = (globalTotalValue - discValue).ToString("C2", culture);
                            totalLabel.Text = totalAfterDisc.ToString("C2", culture);
                            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C2", culture);

                            TOPValue = rdr.GetInt32("TOP");
                            selectedSQInvoice = rdr.GetString("SQ_INVOICE");
                            bayarTextBox.Text = rdr.GetString("SALES_PAYMENT");

                            if (TOPValue == 1)
                                cashRadioButton.Checked = true;
                            else
                            {
                                creditRadioButton.Checked = true;
                                TOPDuration = rdr.GetInt32("TOP_DURATION");
                                tempoMaskedTextBox.Text = TOPDuration.ToString();
                            }
                            //totalAfterDiscTextBox.Text = (globalTotalValue - discValue).ToString("C2", culture);
                        }
                    }
                    rdr.Close();

                    // PULL DETAIL DATA               
                    sqlCommand = "SELECT M.PRODUCT_ID AS KODE_PRODUK, M.PRODUCT_NAME AS NAMA_PRODUK, SD.PRODUCT_PRICE AS HPP, SD.PRODUCT_SALES_PRICE AS HARGA_PRODUK, SD.PRODUCT_QTY AS QTY, SD.PRODUCT_DISC1, SD.PRODUCT_DISC2, SD.PRODUCT_DISC_RP, SD.SALES_SUBTOTAL AS SUBTOTAL " +
                                           "FROM SALES_DETAIL SD, MASTER_PRODUCT M WHERE SD.SALES_INVOICE = '" + selectedsalesinvoice + "' AND SD.REV_NO = " + selectedsalesinvoiceRevNo + " AND SD.PRODUCT_ID = M.PRODUCT_ID";
                    rdr = DS.getData(sqlCommand);
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            addNewRow();
                        //salesQty.Add("0");
                        //disc1.Add("0");
                        //disc2.Add("0");
                        //discRP.Add("0");
                        //productPriceList.Add("0");
                        //jumlahList.Add("0");
                            cashierDataGridView.Rows[rowPos].Cells["productID"].Value = rdr.GetString("KODE_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productName"].Value = rdr.GetString("NAMA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["productPrice"].Value = rdr.GetString("HARGA_PRODUK");
                            cashierDataGridView.Rows[rowPos].Cells["qty"].Value = rdr.GetString("QTY");
                            cashierDataGridView.Rows[rowPos].Cells["disc1"].Value = rdr.GetString("PRODUCT_DISC1");
                            cashierDataGridView.Rows[rowPos].Cells["disc2"].Value = rdr.GetString("PRODUCT_DISC2");
                            cashierDataGridView.Rows[rowPos].Cells["discRP"].Value = rdr.GetString("PRODUCT_DISC_RP");
                            cashierDataGridView.Rows[rowPos].Cells["hpp"].Value = rdr.GetString("HPP");

                        //    salesQty[rowPos] = rdr.GetString("QTY");
                        //productPriceList[rowPos] = rdr.GetString("HARGA_PRODUK");
                        //jumlahList[rowPos] = rdr.GetString("SUBTOTAL");
                        //disc1[rowPos] = rdr.GetString("PRODUCT_DISC1");
                        //disc2[rowPos] = rdr.GetString("PRODUCT_DISC2");
                        //discRP[rowPos] = rdr.GetString("PRODUCT_DISC_RP");
                            cashierDataGridView.Rows[rowPos].Cells["jumlah"].Value = rdr.GetString("SUBTOTAL");

                            rowPos += 1;
                        }
                    }
                    rdr.Close();
                    break;
            }
            isLoading = false;
        }

        private void cashierForm_Load(object sender, EventArgs e)
        {
            int userAccessOption = 0;

            registerGlobalHotkey();

            if (originModuleID == globalConstants.SALES_QUOTATION)
            {
                noFakturLabel.Text = "SALES QUOTATION";
                approvalButton.Visible = false;
                //rejectButton.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                bayarTextBox.Visible = false;
                uangKembaliTextBox.Visible = false;
            }
            else if (originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            {
                noFakturLabel.Text = "SALES QUOTATION";

                userAccessOption = DS.getUserAccessRight(globalConstants.MENU_SALES_QUOTATION, gutil.getUserGroupID());

                if (userAccessOption == 1)
                { 
                    approvalButton.Visible = true;
                    //rejectButton.Visible = true;
                }
                else
                { 
                    approvalButton.Visible = false;
                    //rejectButton.Visible = false;
                }

                label12.Visible = false;
                label13.Visible = false;
                bayarTextBox.Visible = false;
                uangKembaliTextBox.Visible = false;
            }
            else if (originModuleID == globalConstants.SALES_ORDER_REVISION)
            {
                noFakturLabel.Text = "";
                approvalButton.Visible = false;
                //rejectButton.Visible = false;
            }
            else if (originModuleID == 0) // NORMAL TRANSACTION
            { 
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierForm_Load, ATTEMPT TO LOAD NO FAKTUR");
                loadNoFaktur();
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierForm_Load, ATTEMPT TO addColumnToDataGrid");
                approvalButton.Visible = false;
            }
            else if (originModuleID == globalConstants.SERVICE_AC || 
                    originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || 
                    originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
            {
                loadNoFaktur();
                approvalButton.Visible = false;

                labelTeknisi.Visible = true;
                teknisiTextbox.Visible = true;

                if (originModuleID == globalConstants.SERVICE_AC)
                {
                    posTitle = gutil.loadStoreName() + " - MODUL SERVICE AC";
                }
                else if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
                {
                    posTitle = gutil.loadStoreName() + " - SURAT TUGAS PEMASANGAN";
                    panel6.Visible = false;
                    totalLabel.Visible = false;
                    label3.Visible = false;
                    customerComboBox.Visible = false;

                    panelSuratTugas.Visible = true;
                    label4.Visible = false;
                    cashRadioButton.Visible = false;
                    creditRadioButton.Visible = false;
                    labelCaraBayar.Visible = false;
                    tempoMaskedTextBox.Visible = false;
                    paymentComboBox.Visible = false;
                    printoutCheckBox.Visible = false;

                    jobStartDateTimePicker.Format = DateTimePickerFormat.Custom;
                    jobStartDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

                    finishedDateTimePicker.Format = DateTimePickerFormat.Custom;
                    finishedDateTimePicker.CustomFormat = globalUtilities.CUSTOM_DATE_FORMAT;

                    //sendSMSCheckbox.Visible = true;
                }
            }

            addColumnToDataGrid();

            if (selectedsalesinvoice != "")
                loadInvoiceData();
            else
            { 
                customerComboBox.SelectedIndex = 0;
                customerComboBox.Text = customerComboBox.Items[0].ToString();
            }

            cashierDataGridView.EditingControlShowing += cashierDataGridView_EditingControlShowing;

            gutil.reArrangeTabOrder(this);
            errorLabel.Text = "";

            userStatusLabel.Text = "Welcome, " + DS.getDataSingleValue("SELECT IFNULL(USER_FULL_NAME, 0) FROM MASTER_USER WHERE ID = " + gutil.getUserID()).ToString();

            //add double buffer
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(cashierDataGridView, true, null);
        }

        private void cashierForm_Activated(object sender, EventArgs e)
        {
            //if need something
            registerGlobalHotkey();

            updateLabel();
            timer1.Start();
        }

        private void cashierForm_Deactivate(object sender, EventArgs e)
        {
            timer1.Stop();
            unregisterGlobalHotkey();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateLabel();
        }

        private void addColumnToDataGrid()
        {
            MySqlDataReader rdr;
            bool discVisible = true;
            int userAccessOption = 0;

            DataGridViewTextBoxColumn F8Column = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn productIdColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productNameColumn = new DataGridViewTextBoxColumn();

            DataGridViewTextBoxColumn productHPPColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn productPriceColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn stockQtyColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn disc1Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn disc2Column = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn discRPColumn = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn subTotalColumn = new DataGridViewTextBoxColumn();

            DataGridViewImageColumn productImageColumn = new DataGridViewImageColumn();

            // F8 COLUMN
            F8Column.HeaderText = "F8";
            F8Column.Name = "F8";
            F8Column.Width = 44;
            F8Column.ReadOnly = true;
            cashierDataGridView.Columns.Add(F8Column);

            //// IMAGE COLUMN
            //productImageColumn.HeaderText = "GAMBAR";
            //productImageColumn.Name = "productImage";
            //productImageColumn.Width = 100;
            //productImageColumn.ReadOnly = true;
            ////productImageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //cashierDataGridView.Columns.Add(productImageColumn);

            productIdColumn.HeaderText = "KODE PRODUK";
            productIdColumn.Name = "productID";
            productIdColumn.Width = 150;
            productIdColumn.Visible = false;
            cashierDataGridView.Columns.Add(productIdColumn);

            // PRODUCT NAME COLUMN
            productNameColumn.HeaderText = "NAMA PRODUK";
            productNameColumn.Name = "productName";
            productNameColumn.Width = 200;
            //productNameColumn.ReadOnly = true;
            cashierDataGridView.Columns.Add(productNameColumn);

            productPriceColumn.HeaderText = "HARGA";
            productPriceColumn.Name = "productPrice";
            productPriceColumn.Width = 100;

            if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
                productPriceColumn.Visible = false; // DISABLE ALL PRICE RELATED INFORMATION FOR SURAT TUGAS PEMASANGAN

            // USER WHO HAS ACCESS TO PENGATURAN HARGA CAN EDIT THE PRODUCT PRICE MANUALLY
            userAccessOption = DS.getUserAccessRight(globalConstants.MENU_PENGATURAN_HARGA, gutil.getUserGroupID());
            if (userAccessOption != 1)
                productPriceColumn.ReadOnly = true;

            cashierDataGridView.Columns.Add(productPriceColumn);

            stockQtyColumn.HeaderText = "QTY";
            stockQtyColumn.Name = "qty";
            stockQtyColumn.Width = 80;
            cashierDataGridView.Columns.Add(stockQtyColumn);

            if (originModuleID == globalConstants.SALES_QUOTATION || originModuleID == globalConstants.EDIT_SALES_QUOTATION)
            {
                discVisible = false;
            }

            //disc1Column.HeaderText = "DISC 1 (%)";
            disc1Column.HeaderText = "DISC (%)";
            disc1Column.Name = "disc1";
            disc1Column.Width = 150;
            disc1Column.Visible = false;//discVisible;
            disc1Column.MaxInputLength = 5;
            
            cashierDataGridView.Columns.Add(disc1Column);

            disc2Column.HeaderText = "DISC 2 (%)";
            disc2Column.Name = "disc2";
            disc2Column.Width = 150;
            disc2Column.Visible = false;//discVisible;
            disc2Column.MaxInputLength = 5;
            cashierDataGridView.Columns.Add(disc2Column);

            discRPColumn.HeaderText = "DISC RP";
            discRPColumn.Name = "discRP";
            discRPColumn.Width = 150;
            if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
                discRPColumn.Visible = false;
            else
                discRPColumn.Visible = discVisible;
            cashierDataGridView.Columns.Add(discRPColumn);

            subTotalColumn.HeaderText = "JUMLAH";
            subTotalColumn.Name = "jumlah";
            subTotalColumn.Width = 150;
            if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || originModuleID == globalConstants.MONITOR_SURAT_TUGAS)
                subTotalColumn.Visible = false;
            else
                subTotalColumn.Visible = true;
            cashierDataGridView.Columns.Add(subTotalColumn);

            productHPPColumn.HeaderText = "HPP";
            productHPPColumn.Name = "hpp";
            productHPPColumn.Width = 200;
            productHPPColumn.Visible = false;
            cashierDataGridView.Columns.Add(productHPPColumn);
        }

        private void deleteCurrentRow()
        {
            if (cashierDataGridView.SelectedCells.Count > 0)
            {
                int rowSelectedIndex = cashierDataGridView.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = cashierDataGridView.Rows[rowSelectedIndex];

                if (null != selectedRow)
                {
                    //for (int i = rowSelectedIndex; i < cashierDataGridView.Rows.Count - 1; i++)
                    //{
                    //    salesQty[i] = salesQty[i + 1];
                    //    productPriceList[i] = productPriceList[i + 1];
                    //    jumlahList[i] = jumlahList[i + 1];
                    //    disc1[i] = disc1[i + 1];
                    //    disc2[i] = disc2[i + 1];
                    //    discRP[i] = discRP[i + 1];
                    //}

                    isLoading = true;
                    cashierDataGridView.Rows.Remove(selectedRow);
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : deleteCurrentRow [" + rowSelectedIndex + "]");
                    isLoading = false;
                }
            }
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void discJualMaskedTextBox_Validating(object sender, CancelEventArgs e)
        {
            //double totalAfterDisc = 0;
            double tempValue = 0;

            tempValue = Math.Round((globalTotalValue * discPercentValue) /100);

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                totalAfterDisc = globalTotalValue - tempValue - Convert.ToDouble(discJualMaskedTextBox.Text);
                discValue = Convert.ToDouble(discJualMaskedTextBox.Text);
            }
            else
            { 
                totalAfterDisc = globalTotalValue - tempValue;
                discValue = 0;
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : discJualMaskedTextBox_Validating, totalAfterDisc [" + totalAfterDisc + "]");
            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C2", culture);
            totalLabel.Text = totalAfterDisc.ToString("c2", culture);

            calculateChangeValue();
        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isLoading)
                return;

            refreshProductPrice();
            
        }

        private void cashRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (cashRadioButton.Checked)
            {
                tempoMaskedTextBox.Visible = false;
                labelCaraBayar.Text = "Cara Bayar       :";
                paymentComboBox.Visible = true;
                paymentComboBox.SelectedIndex = 0;
                paymentComboBox.Text = paymentComboBox.Items[0].ToString();
                bayarTextBox.Enabled = true;
            }
        }

        private void bayarTextBox_TextChanged(object sender, EventArgs e)
        {
            calculateChangeValue();
        }

        private void paymentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPaymentMethod = paymentComboBox.SelectedIndex;
        }

        private void ChangePrinterButton_Click(object sender, EventArgs e)
        {
            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : ChangePrinterButton_Click, DISPLAY PRINTER SELECTION FORM");
            SetPrinterForm displayedForm = new SetPrinterForm();
            displayedForm.ShowDialog(this);
        }

        private void loadInfoToko(int opt, out string namatoko, out string almt, out string telepon, out string email)
        {
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            namatoko = ""; almt = ""; telepon = ""; email = "";
            DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT IFNULL(BRANCH_ID,0) AS 'BRANCH_ID', IFNULL(HQ_IP4,'') AS 'IP', IFNULL(STORE_NAME,'') AS 'NAME', IFNULL(STORE_ADDRESS,'') AS 'ADDRESS', IFNULL(STORE_PHONE,'') AS 'PHONE', IFNULL(STORE_EMAIL,'') AS 'EMAIL' FROM SYS_CONFIG WHERE ID =  " + opt))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadInfoToko");
                    while (rdr.Read())
                    {
                        if (!String.IsNullOrEmpty(rdr.GetString("NAME")))
                        {
                            namatoko = rdr.GetString("NAME");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("ADDRESS")))
                        {
                            almt = rdr.GetString("ADDRESS");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("PHONE")))
                        {
                            telepon = rdr.GetString("PHONE");
                        }
                        if (!String.IsNullOrEmpty(rdr.GetString("EMAIL")))
                        {
                            email = rdr.GetString("EMAIL");
                        }
                    }
                }
            }
        }

        private void loadNamaUser(int user_id, out string nama)
        {
            nama = "";
            MySqlDataReader rdr;
            DataTable dt = new DataTable();
            DS.mySqlConnect();
            //1 load default 2 setting user
            using (rdr = DS.getData("SELECT USER_NAME AS 'NAME' FROM MASTER_USER WHERE ID="+user_id))
            {
                if (rdr.HasRows)
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : loadNamaUser");

                    rdr.Read();
                    nama = rdr.GetString("NAME");
                }
            }
        }

        private void PrintReceipt()
        {
            //pdoc = new PrintDocument();
            //pd.Document = pdoc;
            //pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            //    PrintPreviewDialog pp = new PrintPreviewDialog();
            //Font font = new Font("Courier New", 15);

            //cek paper mode
            int papermode = gutil.getPaper();
            int paperLength = 0;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt");


            //if (papermode == globalUtilities.PAPER_POS_RECEIPT && 
            //    originModuleID != globalConstants.TUGAS_PEMASANGAN_BARU && 
            //    originModuleID != globalConstants.EDIT_TUGAS_PEMASANGAN_BARU && 
            //    originModuleID != globalConstants.MONITOR_SURAT_TUGAS) //kertas POS
            //{
            //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, POS size Paper");
            //    //width, height
            //    paperLength = calculatePageLength();
            //    PaperSize psize = new PaperSize("Custom", 320, paperLength);//820);
            //    printDocument1.DefaultPageSettings.PaperSize = psize;
            //    DialogResult result;
            //    printPreviewDialog1.Width = 512;
            //    printPreviewDialog1.Height = 768;
            //    result = printPreviewDialog1.ShowDialog();
            //    if (result == DialogResult.OK)
            //    {
            //        printDocument1.Print();
            //    }
            //}
            //else
            {
                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, papermode [" + papermode + "]");

                //kertas 1/2 kwarto atau kwarto using crystal report
                //preview laporan
                DS.mySqlConnect();
                string sqlCommandx = "";
                    
                if (originModuleID == 0 || originModuleID == globalConstants.SERVICE_AC)
                {
                    // NORMAL TRANSACTION
                    sqlCommandx = "SELECT SH.SALES_DISCOUNT_FINAL, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', " +
                    "PRODUCT_SALES_PRICE AS 'PRICE', ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'" +
                    "UNION " +
                    "SELECT SH.SALES_DISCOUNT_FINAL, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', PRODUCT_SALES_PRICE AS 'PRICE', " +
                    "ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M WHERE SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'";
                }
                else if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || 
                          originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU)
                {
                    // PRINT OUT SURAT TUGAS
                    //sqlCommandx = "SELECT IFNULL(MT.TECHNICIAN_NAME, '') AS TECHNICIAN_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', MC.CUSTOMER_ADDRESS1, MC.CUSTOMER_ADDRESS2, MC.CUSTOMER_ADDRESS_CITY, MC.CUSTOMER_PHONE, M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY' " +
                    //"FROM MASTER_TECHNICIAN MT, SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE MT.ID = "+selectedTechnicianID+" AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'" +
                    //"UNION " +
                    //"SELECT IFNULL(MT.TECHNICIAN_NAME, '') AS TECHNICIAN_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', '' AS CUSTOMER_ADDRESS1, '' AS CUSTOMER_ADDRESS2, '' AS CUSTOMER_ADDRESS_CITY, '' AS CUSTOMER_PHONE, M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY' " +
                    //"FROM MASTER_TECHNICIAN MT, SALES_HEADER SH, SALES_DETAIL SD, MASTER_PRODUCT M WHERE MT.ID = " + selectedTechnicianID + " AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'";

                    sqlCommandx = "SELECT IFNULL(MT.TECHNICIAN_NAME, '') AS TECHNICIAN_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', MC.CUSTOMER_ADDRESS1, MC.CUSTOMER_ADDRESS2, MC.CUSTOMER_ADDRESS_CITY, MC.CUSTOMER_PHONE, M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY' " +
                        "FROM MASTER_TECHNICIAN MT, SURAT_TUGAS_HEADER SH, SURAT_TUGAS_DETAIL SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE SH.TECHNICIAN_ID = MT.ID AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'" +
                        "UNION " +
                        "SELECT IFNULL(MT.TECHNICIAN_NAME, '') AS TECHNICIAN_NAME, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', '' AS CUSTOMER_ADDRESS1, '' AS CUSTOMER_ADDRESS2, '' AS CUSTOMER_ADDRESS_CITY, '' AS CUSTOMER_PHONE, M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY' " +
                        "FROM MASTER_TECHNICIAN MT, SURAT_TUGAS_HEADER SH, SURAT_TUGAS_DETAIL SD, MASTER_PRODUCT M WHERE SH.TECHNICIAN_ID = MT.ID AND SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoice + "'";
                }
                else
                {
                    // GET DUMMY DATA
                    sqlCommandx = "SELECT SH.SALES_DISCOUNT_FINAL, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', MC.CUSTOMER_FULL_NAME AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', " +
                    "PRODUCT_SALES_PRICE AS 'PRICE', ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM SALES_HEADER_TAX SH, SALES_DETAIL_TAX SD, MASTER_PRODUCT M, MASTER_CUSTOMER MC WHERE SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = MC.CUSTOMER_ID AND SH.SALES_INVOICE='" + selectedsalesinvoiceTax + "'" +
                    "UNION " +
                    "SELECT SH.SALES_DISCOUNT_FINAL, SD.ID, SH.SALES_DATE AS 'DATE', SD.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', M.PRODUCT_NAME AS 'PRODUCT', PRODUCT_QTY AS 'QTY', PRODUCT_SALES_PRICE AS 'PRICE', " +
                    "ROUND((PRODUCT_QTY * PRODUCT_SALES_PRICE) - SALES_SUBTOTAL, 2) AS 'POTONGAN', SALES_SUBTOTAL AS 'SUBTOTAL', SH.SALES_PAYMENT AS 'PAYMENT', SH.SALES_PAYMENT_CHANGE AS 'CHANGE' " +
                    "FROM SALES_HEADER_TAX SH, SALES_DETAIL_TAX SD, MASTER_PRODUCT M WHERE SD.PRODUCT_ID = M.PRODUCT_ID AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SH.CUSTOMER_ID = 0 AND SH.SALES_INVOICE='" + selectedsalesinvoiceTax + "'";
                }


                if (originModuleID == globalConstants.TUGAS_PEMASANGAN_BARU || 
                    originModuleID == globalConstants.EDIT_TUGAS_PEMASANGAN_BARU ||
                    originModuleID == globalConstants.MONITOR_SURAT_TUGAS
                    )
                {
                    // PRINT OUT SURAT TUGAS, REUSE DELIVERY ORDER FORM
                    DS.writeXML(sqlCommandx, globalConstants.suratTugasXML);
                    suratTugasPrintOutForm displayedform = new suratTugasPrintOutForm();
                    displayedform.ShowDialog(this); ;
                }
                else
                {
                    DS.writeXML(sqlCommandx, globalConstants.SalesReceiptXML);
                    if (gutil.getPaper() == globalUtilities.PAPER_FULL_KWARTO) // kuarto
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, kuarto paper, display SalesReceiptKuartoForm");
                        SalesReceiptKuartoForm displayedform = new SalesReceiptKuartoForm();
                        displayedform.ShowDialog(this); ;
                    }
                    else
                    {
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : PrintReceipt, display SalesReceiptForm");
                        SalesReceiptForm displayedform = new SalesReceiptForm();
                        displayedform.ShowDialog(this);
                    }
                }
            }
        }
       
        private int calculatePageLength()
        {
            int startY = 0;
            int Offset = 5;
            int Offsetplus = 3;
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            int add_offset = rowheight;
            int totalLengthPage = startY + Offset;
            string nm, almt, tlpn, email;

            loadInfoToko(2, out nm, out almt, out tlpn, out email);

            //set printing area
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            if (!email.Equals(""))
                Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            //end of header

            //start of content

            //1. PAYMENT METHOD
            Offset = Offset + add_offset;

            //2. CUSTOMER NAME
            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset + Offsetplus;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            //DETAIL PENJUALAN
            
            DS.mySqlConnect();
            MySqlDataReader rdr;
            using (rdr = DS.getData("SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE' FROM sales_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoice + "'"))//+ "group by s.product_id") )
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        if (i == 0)
                        {
                            Offset = Offset + add_offset;
                        }
                        else
                        {
                            i = 1;
                            Offset = Offset + add_offset + Offsetplus;
                        }
                    
                        Offset = Offset + add_offset;
                        Offset = Offset + add_offset;
                    }
                }
            }
            DS.mySqlClose();

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            //end of content

            //FOOTER

            Offset = Offset + add_offset + Offsetplus;

            Offset = Offset + add_offset + Offsetplus; ;

            Offset = Offset + add_offset;

            Offset = Offset + add_offset;
            //end of footer

            totalLengthPage = totalLengthPage + Offset + add_offset;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : calculatePageLength, totalLengthPage [" + totalLengthPage + "]");
            return totalLengthPage;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            String ucapan = "";
            string nm, almt, tlpn, email;

            //event printing

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : printDocument1_PrintPage, print POS size receipt");

            Graphics graphics = e.Graphics;
            int startX = 0;
            int startY = 0;
            int colxwidth = 85; //old 75
            int totrowwidth = 255; //old 250
            Font font = new Font("Courier New", 9);
            int rowheight = (int)Math.Ceiling(font.GetHeight());
            //int inlineheader = 12;
            //int inlinetext = 10;
            int add_offset = rowheight;
            int Offset = 5;
            int offset_plus = 3;
            int fontSize = 8;
            //HEADER

            //set allignemnt
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            //set whole printing area
            System.Drawing.RectangleF rect = new System.Drawing.RectangleF(startX, startY + Offset, totrowwidth, rowheight);
            //set right print area
            System.Drawing.RectangleF rectright = new System.Drawing.RectangleF(totrowwidth - colxwidth - startX, startY + Offset, colxwidth, rowheight);
            //set middle print area
            System.Drawing.RectangleF rectcenter = new System.Drawing.RectangleF((startX + totrowwidth), startY + Offset, colxwidth, rowheight);

            loadInfoToko(2,out nm, out almt, out tlpn, out email);

            graphics.DrawString(nm, new Font("Courier New", 9),
                                new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(almt,
                     new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            graphics.DrawString(tlpn,
                     new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            if (!email.Equals(""))
            {
                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                graphics.DrawString(email,
                         new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            }

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            //String underLine = "--------------------------------";  //32 character
            String underLine = "------------------------------";  //32 character
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);
            //end of header

            //start of content
            MySqlDataReader rdr;
            DataTable dt = new DataTable();

            DS.mySqlConnect();
            //load customer id
            string customer = "";
            string tgl="";
            string group = "";
            double total = 0;
            string sqlCommand = "";
            
            if (originModuleID == 0)  // NORMAL TRANSACTION
            { 
            sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                " UNION " +
                "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'" +
                "ORDER BY DATE ASC";
            }
            else
            {
                // GET DUMMY TAX DATA
                sqlCommand = "SELECT S.SALES_INVOICE AS 'INVOICE', C.CUSTOMER_FULL_NAME AS 'CUSTOMER',DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE',S.SALES_TOTAL AS 'TOTAL', IF(C.CUSTOMER_GROUP=1,'RETAIL',IF(C.CUSTOMER_GROUP=2,'GROSIR','PARTAI')) AS 'GROUP' FROM SALES_HEADER_TAX S,MASTER_CUSTOMER C WHERE S.CUSTOMER_ID = C.CUSTOMER_ID AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax+ "'" +
                                        " UNION " +
                                        "SELECT S.SALES_INVOICE AS 'INVOICE', 'P-UMUM' AS 'CUSTOMER', DATE_FORMAT(S.SALES_DATE, '%d-%M-%Y') AS 'DATE', S.SALES_TOTAL AS 'TOTAL', 'RETAIL' AS 'GROUP' FROM SALES_HEADER_TAX S WHERE S.CUSTOMER_ID = 0 AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'" +
                                        "ORDER BY DATE ASC";
            }
            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows) 
                {
                    rdr.Read();
                    customer = rdr.GetString("CUSTOMER");
                    tgl = rdr.GetString("DATE");
                    total = rdr.GetDouble("TOTAL");
                    group = rdr.GetString("GROUP");
                }
            }
            DS.mySqlClose();

            //1. PAYMENT METHOD
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            //SET TO LEFT MARGIN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            if (creditRadioButton.Checked)
            {
                ucapan = "JUAL CREDIT KEPADA";
            } else
            {
                ucapan = "JUAL TUNAI KEPADA";
            }
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            //2. CUSTOMER NAME
            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "PELANGGAN : " + customer + " [" + group + "]";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.Width = totrowwidth;
            ucapan = "BUKTI PEMBAYARAN";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset + offset_plus;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            if (originModuleID == 0)
                ucapan = "NO. NOTA : " + selectedsalesinvoice;
            else
                ucapan = "NO. NOTA : " + selectedsalesinvoiceTax;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TANGGAL  : " + tgl;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            string nama = "";
            loadNamaUser(gutil.getUserID(), out nama);
            ucapan = "OPERATOR : " + nama;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            //DETAIL PENJUALAN
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            //read sales_detail

            //DS.mySqlConnect();
            string product_id = "";
            string product_name = "";
            double total_qty = 0;
            double product_qty = 0;
            double product_price = 0;
            float startRightX = totrowwidth - colxwidth - startX;
            
            if (originModuleID == 0)
            {
                // NORMAL TRANSACTION
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL FROM sales_detail S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoice + "'";
            }
            else
            {
                // GET DUMMY DATA
                sqlCommand = "SELECT S.ID, S.PRODUCT_ID AS 'P-ID', P.PRODUCT_NAME AS 'NAME', S.PRODUCT_QTY AS 'QTY',ROUND(S.SALES_SUBTOTAL/S.PRODUCT_QTY) AS 'PRICE', S.SALES_SUBTOTAL FROM sales_detail_tax S, master_product P WHERE S.PRODUCT_ID=P.PRODUCT_ID AND S.SALES_INVOICE='" + selectedsalesinvoiceTax + "'";
            }

            using (rdr = DS.getData(sqlCommand))//+ "group by s.product_id") )
            {
                if (rdr.HasRows)
                {
                    int i = 0;
                    while (rdr.Read())
                    {
                        product_id = rdr.GetString("P-ID");
                        product_name = rdr.GetString("NAME");
                        product_qty = rdr.GetDouble("QTY");
                        product_price = rdr.GetDouble("PRICE");
                        double subtotal = rdr.GetDouble("SALES_SUBTOTAL");
                        if (i == 0)
                        {
                            Offset = Offset + add_offset;
                        }
                        else
                        {
                            i = 1;
                            Offset = Offset + add_offset + offset_plus;
                        }
                        rect.Y = startY + Offset;
                        rect.X = startX + 10;
                        rect.Width = totrowwidth;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        //ucapan = product_qty + "X " + product_name;
                        ucapan = product_name;
                        if (ucapan.Length > 30 )
                        {
                            ucapan = ucapan.Substring(0, 30); //maximum 30 character
                        }
                        //
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);

                        //new line
                        Offset = Offset + add_offset;
                        rect.Y = startY + Offset;
                        rect.X = startX + 20;
                        rect.Width = totrowwidth;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        ucapan = product_qty + "X";
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);
                        //

                        rect.Y = startY + Offset;
                        rect.X = startX + 50;
                        rect.Width = totrowwidth;
                        sf.LineAlignment = StringAlignment.Near;
                        sf.Alignment = StringAlignment.Near;
                        ucapan = product_price.ToString(currencyFormat, culture);
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rect, sf);



                        rectright.X = startRightX - 35;
                        rectright.Y = rect.Y;

                        rectright.Width = colxwidth;// - 5;
                        sf.LineAlignment = StringAlignment.Far;
                        sf.Alignment = StringAlignment.Far;
                        ucapan = "@" + product_price.ToString("C2", culture);//" Rp." + product_price;
                        graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                                 new SolidBrush(Color.Black), rectright, sf);
                    }
                }
            }
            DS.mySqlClose();

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = rect.X + 70;//95;
            //rectcenter.X = rectcenter.X + 15;
            //rectcenter.Width = colxwidth;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "JUMLAH  : " + total.ToString(currencyFormat, culture);
            //rectcenter.Y = rect.Y;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            //sf.LineAlignment = StringAlignment.Far;
            //sf.Alignment = StringAlignment.Far;
            //ucapan = total.ToString("C2", culture);
            //rectright.X = rectright.X - 5;
            //rectright.Y = rect.Y-2;
            //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
            //         new SolidBrush(Color.Black), rectright, sf);

            if (cashRadioButton.Checked == true)
            {
                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                //rectcenter.X = rectcenter.X + 15;
                //rectcenter.Width = colxwidth;
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                double jumlahBayar = Convert.ToDouble(bayarAmount);
                ucapan = "TUNAI   : " + jumlahBayar.ToString(currencyFormat, culture);
                //rectcenter.Y = rect.Y;
                graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                         new SolidBrush(Color.Black), rect, sf);
                sf.LineAlignment = StringAlignment.Far;
                sf.Alignment = StringAlignment.Far;

                //ucapan = jumlahBayar.ToString("C2", culture);//"Rp." + String.Format("{0:C2}", bayarTextBox.Text);
                //rectright.X = rectright.X - 5;
                //rectright.Y = rect.Y-2;
                //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                //         new SolidBrush(Color.Black), rectright, sf);

                Offset = Offset + add_offset;
                rect.Y = startY + Offset;
                //rectcenter.X = rectcenter.X + 15;
                //rectcenter.Width = colxwidth;
                sf.LineAlignment = StringAlignment.Near;
                sf.Alignment = StringAlignment.Near;
                ucapan = "KEMBALI : " + uangKembaliTextBox.Text + ",00";
                //rectcenter.Y = rect.Y;
                graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                         new SolidBrush(Color.Black), rect, sf);
                //sf.LineAlignment = StringAlignment.Far;
                //sf.Alignment = StringAlignment.Far;
                //ucapan = uangKembaliTextBox.Text;
                //rectright.X = rectright.X - 5;
                //rectright.Y = rect.Y-2;
                //graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                //         new SolidBrush(Color.Black), rectright, sf);
            }

            if (originModuleID == 0)
            {
                // NORMAL TRANSACTION
                sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL S, MASTER_PRODUCT P WHERE S.PRODUCT_ID = P.PRODUCT_ID AND S.SALES_INVOICE = '" + selectedsalesinvoice + "'";
            }
            else
            {
                // GET DUMMY DATA
                sqlCommand = "SELECT IFNULL(SUM(PRODUCT_QTY), 0) FROM SALES_DETAIL_TAX S, MASTER_PRODUCT P WHERE S.PRODUCT_ID = P.PRODUCT_ID AND S.SALES_INVOICE = '" + selectedsalesinvoiceTax + "'";
            }

            total_qty = Convert.ToDouble(DS.getDataSingleValue(sqlCommand));

            Offset = Offset + add_offset + offset_plus + offset_plus; ;
            rect.Y = startY + Offset;
            rect.X = startX + 10;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Near;
            sf.Alignment = StringAlignment.Near;
            ucapan = "TOTAL BARANG : " + total_qty;
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            //eNd of content

            //FOOTER

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            rect.X = startX;
            rect.Width = totrowwidth;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            graphics.DrawString(underLine, new Font("Courier New", 9),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TERIMA KASIH ATAS KUNJUNGAN ANDA";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "MAAF BARANG YANG SUDAH DIBELI";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);

            Offset = Offset + add_offset;
            rect.Y = startY + Offset;
            ucapan = "TIDAK DAPAT DITUKAR/ DIKEMBALIKKAN";
            graphics.DrawString(ucapan, new Font("Courier New", fontSize),
                     new SolidBrush(Color.Black), rect, sf);
            //end of footer

        }

        private void tempoMaskedTextBox_Enter(object sender, EventArgs e)
        {
            BeginInvoke((Action)delegate
            {
                tempoMaskedTextBox.SelectAll();
            });
        }

        private void discJualMaskedTextBox_Enter(object sender, EventArgs e)
        {
            discJualMaskedTextBox.Text = discValue.ToString();
            BeginInvoke((Action)delegate
            {
                discJualMaskedTextBox.SelectAll();
            });
        }

        private void bayarTextBox_Enter(object sender, EventArgs e)
        {
            //bayarTextBox.Text = bayarAmount.ToString();
            BeginInvoke((Action)delegate
            {
                bayarTextBox.SelectAll();
            });
        }

        private void bayarTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { 
                //if (originModuleID != globalConstants.COPY_NOTA)
                {
                    //bayarAmount = Convert.ToDouble(bayarTextBox.Text);
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TRIGGER SAVE INVOICE FROM BAYAR TEXTBOX");

                    saveAndPrintOutInvoice();
                }
            }
        }

        private void bayarTextBox_Leave(object sender, EventArgs e)
        {
            isLoading = true;
            //bayarAmount = Convert.ToDouble(bayarTextBox.Text);
            //bayarTextBox.Text = bayarAmount.ToString("C", culture);
            isLoading = false;

        }

        private void cashierDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //var cell = cashierDataGridView[e.ColumnIndex, e.RowIndex];
            //DataGridViewRow selectedRow = cashierDataGridView.Rows[e.RowIndex];

            //if (cell.OwningColumn.Name == "productName")
            //{
            //    if (null != cell.Value)
            //    {
            //        if (cell.Value.ToString().Length > 0)
            //        {
            //            updateSomeRowContents(selectedRow, e.RowIndex, cell.Value.ToString());
            //            //cashierDataGridView.CurrentCell = selectedRow.Cells["qty"];
            //        }
            //        else
            //        {
            //            clearUpSomeRowContents(selectedRow, e.RowIndex);
            //        }
            //    }
            //}
        }

        private void rejectSQ(string selectedSO)
        {
            string sqInvoice = "";
            string sqlCommand = "";
            MySqlException internalEX = null;

            sqInvoice = selectedsalesinvoice;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // UPDATE SALES QUOTATION TABLE
                sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET SQ_APPROVED = -1 WHERE SQ_INVOICE = '" + sqInvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void approveSQ(string selectedSO)
        {
            string sqInvoice = "";
            string sqlCommand = "";
            MySqlException internalEX = null;
            string SQApprovedDate = "";

            // STORE SALES QUOTATION INVOICE NO
            sqInvoice = selectedsalesinvoice;

            if (cashRadioButton.Checked)
            {
                // FOR CASH QUOTATION, ASSUME THAT PAYMENT HAS ALREADY BEEN PAID
                bayarTextBox.Text = globalTotalValue.ToString();
                bayarAmount = globalTotalValue;
            }
            
            SQApprovedDate = gutil.getCustomStringFormatDate(DateTime.Now); //String.Format(culture, "{0:dd-MM-yyyy HH:mm}", DateTime.Now);

            originModuleID = 0;
            saveAndPrintOutInvoice();

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // UPDATE SALES HEADER TABLE
                sqlCommand = "UPDATE SALES_HEADER SET SQ_INVOICE = '" + sqInvoice + "' WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                // UPDATE SALES HEADER TAX TABLE
                sqlCommand = "UPDATE SALES_HEADER_TAX SET SQ_INVOICE = '" + sqInvoice + "' WHERE SALES_INVOICE = '" + selectedsalesinvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                // UPDATE SALES QUOTATION TABLE
                sqlCommand = "UPDATE SALES_QUOTATION_HEADER SET SQ_APPROVED = 1, SQ_APPROVED_DATE = STR_TO_DATE('" + SQApprovedDate + "', '%d-%m-%Y %H:%i') WHERE SQ_INVOICE = '" + sqInvoice + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                DS.commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DS.mySqlClose();
            }
        }

        private void approvalButton_Click(object sender, EventArgs e)
        {
            approveSQ(selectedsalesinvoice);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rejectSQ(selectedsalesinvoice);
        }

        private void cashierDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowSelectedIndex = 0;
            double subTotal = 0;
            double productPrice = 0;
            string productID = "";
            string previousInput = "";
            //string tempString = "";
            string cellValue = "";
            string columnName = "";
            var cell = cashierDataGridView[e.ColumnIndex, e.RowIndex];
            DataGridViewRow selectedRow = cashierDataGridView.Rows[e.RowIndex];
            bool validInput = true;

            if (isLoading)
                return;

            rowSelectedIndex = e.RowIndex;
            columnName = cell.OwningColumn.Name;

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged [" + columnName + "]");

            if (null != selectedRow.Cells[columnName].Value)
                cellValue = selectedRow.Cells[columnName].Value.ToString();
            else
                cellValue = "";

            if (columnName == "productName")
            {
                //if (cellValue.Length > 0)
                //{
                //    updateSomeRowContents(selectedRow, rowSelectedIndex, cellValue);
                //    //int pos = cashierDataGridView.CurrentCell.RowIndex;

                //    //if (pos > 0)
                //    //    cashierDataGridView.CurrentCell = cashierDataGridView.Rows[pos - 1].Cells["qty"];

                //    //forceUpOneLevel = true;
                //}
            }
            else if (columnName == "productPrice" ||
                columnName == "qty" ||
                columnName == "disc1" ||
                columnName == "disc2" ||
                columnName == "discRP")
            {
                if (cellValue.Length <= 0)
                {
                    // IF TEXTBOX IS EMPTY, DEFAULT THE VALUE TO 0 AND EXIT THE CHECKING
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : cashierDataGridView_CellValueChanged , empty texbox, reset [" + cashierDataGridView.CurrentCell.OwningColumn.Name + "] value to 0");
                    isLoading = true;

                    //if (detailRequestQtyApproved.Count >= rowSelectedIndex + 1)
                    //    detailRequestQtyApproved[rowSelectedIndex] = "0";
                    //switch (columnName)
                    //{
                    //    case "qty":
                    //        salesQty[rowSelectedIndex] = "0";
                    //        break;
                    //    case "disc1":
                    //        disc1[rowSelectedIndex] = "0";
                    //        break;
                    //    case "disc2":
                    //        disc2[rowSelectedIndex] = "0";
                    //        break;
                    //    case "discRP":
                    //        discRP[rowSelectedIndex] = "0";
                    //        break;
                    //    case "productPrice":
                    //        productPriceList[rowSelectedIndex] = "0";
                    //        break;
                    //}

                    selectedRow.Cells[columnName].Value = "0";

                    // reset subTotal Value and recalculate total
                    if (columnName == "qty" || columnName == "productPrice")
                    {
                        // reset subTotal Value and recalculate total
                        selectedRow.Cells["jumlah"].Value = 0;
                        //jumlahList[rowSelectedIndex] = "0";
                    }
                    else
                    {
                        productPrice = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

                        subTotal = calculateSubTotal(rowSelectedIndex, productPrice);
                        gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, subtotal value [" + subTotal + "]");
                        selectedRow.Cells["jumlah"].Value = subTotal;
                       // jumlahList[rowSelectedIndex] = subTotal.ToString();
                    }

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, recalculate total value");
                    calculateTotal();

                    isLoading = false;

                    return;
                }

                if (null != selectedRow.Cells["productID"].Value)
                    productID = selectedRow.Cells["productID"].Value.ToString();

                //switch (columnName)
                //{
                //    case "qty":
                //        previousInput = salesQty[rowSelectedIndex];
                //        break;
                //    case "disc1":
                //        previousInput = disc1[rowSelectedIndex];
                //        break;
                //    case "disc2":
                //        previousInput = disc2[rowSelectedIndex];
                //        break;
                //    case "discRP":
                //        previousInput = discRP[rowSelectedIndex];
                //        break;
                //    case "productPrice":
                //        previousInput = productPriceList[rowSelectedIndex];
                //        break;
                //}

                gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, previousInput [" + previousInput + "]");

                //if (previousInput == cellValue)
                //    return;

                isLoading = true;
                //if (previousInput == "0")
                //{
                //    tempString = cellValue;
                //    if (tempString.IndexOf('0') == 0 && tempString.Length > 1 && tempString.IndexOf("0.") < 0)
                //        selectedRow.Cells[columnName].Value = tempString.Remove(tempString.IndexOf('0'), 1);

                //    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text [" + cellValue + "]");
                //}

                if (gutil.matchRegEx(cellValue, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL)
                    && (cellValue.Length > 0 && cellValue != ".")
                    )
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text pass REGEX Checking");
                    if (columnName == "qty")
                    {
                        if (originModuleID == 0 || originModuleID == globalConstants.SALES_ORDER_REVISION)
                        {
                            if (!gutil.stockIsEnough(productID, Convert.ToDouble(cellValue), true))
                            {
                                errorLabel.Text = "STOK PADA BARIS [" + (rowSelectedIndex + 1) + "] TIDAK CUKUP";
                                validInput = false;
                            }
                        }
                    }
                    //switch (columnName)
                    //{
                    //    case "qty":
                    //        if (!gutil.stockIsEnough(productID, Convert.ToDouble(cellValue)))
                    //        //    salesQty[rowSelectedIndex] = cellValue;
                    //        //else
                    //            //selectedRow.Cells["qty"].Value = salesQty[rowSelectedIndex];
                    //        break;
                    //    case "disc1":
                    //        disc1[rowSelectedIndex] = cellValue;
                    //        break;
                    //    case "disc2":
                    //        disc2[rowSelectedIndex] = cellValue;
                    //        break;
                    //    case "discRP":
                    //        discRP[rowSelectedIndex] = cellValue;
                    //        break;
                    //    case "productPrice":
                    //        productPriceList[rowSelectedIndex] = cellValue;
                    //        break;
                    //}
                }
                else
                {
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, dataGridViewTextBoxEditingControl.Text did not pass REGEX Checking");
                    //selectedRow.Cells[columnName].Value = previousInput;
                    validInput = false;
                    if (errorLabel.Text.Length <= 0)
                        errorLabel.Text = "INPUT PADA BARIS [" + (rowSelectedIndex + 1) + "] TIDAK VALID";
                }

                if (validInput)
                {
                    errorLabel.Text = "";
                    //productPrice = Convert.ToDouble(productPriceList[rowSelectedIndex]);
                    productPrice = Convert.ToDouble(selectedRow.Cells["productPrice"].Value);

                    subTotal = calculateSubTotal(rowSelectedIndex, productPrice);
                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, subtotal value [" + subTotal + "]");
                    selectedRow.Cells["jumlah"].Value = subTotal;
                    //jumlahList[rowSelectedIndex] = subTotal.ToString();

                    gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : TextBox_TextChanged, attempt to calculate total value");
                    calculateTotal();
                }
            }

            isLoading = false;
        }

        private void cashierDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (cashierDataGridView.IsCurrentCellDirty)
            {
                cashierDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void cashierDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            cashierDataGridView.SuspendLayout();
        }

        private void cashierDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cashierDataGridView.ResumeLayout();
        }

        private void pelangganTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DialogResult.OK == MessageBox.Show("HAPUS PELANGGAN ?"))
                {
                    setCustomerID(0);
                }
            }
        }

        private void discJualPersenTextBox_TextChanged(object sender, EventArgs e)
        {
            string tempString;

            if (isLoadingDiscPercent)
                return;

            isLoadingDiscPercent = true;
            if (discJualPersenTextBox.Text.Length == 0)
            {
                // IF TEXTBOX IS EMPTY, SET THE VALUE TO 0 AND EXIT THE CHECKING
                discJualPersenValueText = "0";
                discJualPersenTextBox.Text = "0";

                discJualPersenTextBox.SelectionStart = discJualPersenTextBox.Text.Length;
                isLoadingDiscPercent = false;

                return;
            }
            // CHECKING TO PREVENT PREFIX "0" IN A NUMERIC INPUT WHILE ALLOWING A DECIMAL VALUE STARTED WITH "0"
            else if (discJualPersenTextBox.Text.IndexOf('0') == 0 && discJualPersenTextBox.Text.Length > 1 && discJualPersenTextBox.Text.IndexOf("0.") < 0)
            {
                tempString = discJualPersenTextBox.Text;
                discJualPersenTextBox.Text = tempString.Remove(0, 1);
            }

            if (gutil.matchRegEx(discJualPersenTextBox.Text, globalUtilities.REGEX_NUMBER_WITH_2_DECIMAL))
                discJualPersenValueText = discJualPersenTextBox.Text;
            else
                discJualPersenTextBox.Text = discJualPersenValueText;

            discJualPersenTextBox.SelectionStart = discJualPersenTextBox.Text.Length;
            isLoadingDiscPercent = false;

            //double totalAfterDisc = 0;
            double tempValue = 0;
            double tempDiscRPValue = 0;

            if (discJualMaskedTextBox.Text.Length > 0)
                tempDiscRPValue = Convert.ToDouble(discJualMaskedTextBox.Text);
            else
                tempDiscRPValue = 0;

            if (discJualPersenTextBox.Text.Length > 0)
            {
                discPercentValue = Convert.ToDouble(discJualPersenTextBox.Text);

                tempValue = Math.Round((globalTotalValue * discPercentValue) / 100);
                totalAfterDisc = globalTotalValue - tempValue - tempDiscRPValue;
            }
            else
            {
                totalAfterDisc = globalTotalValue - tempDiscRPValue;
                discPercentValue = 0;
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : discJualMaskedTextBox_Validating, totalAfterDisc [" + totalAfterDisc + "]");
            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C2", culture);
            totalLabel.Text = totalAfterDisc.ToString("c2", culture);

            calculateChangeValue();
        }

        private void discJualPersenTextBox_Validating(object sender, CancelEventArgs e)
        {

        }

        private void discJualPersenTextBox_Enter(object sender, EventArgs e)
        {
            discJualPersenTextBox.SelectionStart = discJualPersenTextBox.Text.Length;
        }

        private void discJualPersenTextBox_Click(object sender, EventArgs e)
        {
            discJualPersenTextBox.SelectionStart = discJualPersenTextBox.Text.Length;
        }

        private void finishedJobButton_Click(object sender, EventArgs e)
        {
            if (setJobAsFinished())
            { 
                MessageBox.Show("DONE");
                cashierDataGridView.ReadOnly = true;
                jobStartTimeMaskedTextBox.ReadOnly = true;
                jobStartDateTimePicker.Enabled = false;

                finishedTimeMaskedTextBox.ReadOnly = true;
                finishedDateTimePicker.Enabled = false;

                finishedJobButton.Enabled = false;
                isJobFinished = 1;
            }
            else
                MessageBox.Show("FAIL");
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected_1(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void jobStartTimeMaskedTextBox_Enter(object sender, EventArgs e)
        {
            jobStartTimeMaskedTextBox.SelectAll();
        }

        private void finishedTimeMaskedTextBox_Enter(object sender, EventArgs e)
        {
            finishedTimeMaskedTextBox.SelectAll();
        }

        private bool processSalesOrderToDO(string noInvoice, string revNo, int salesActiveStatus)
        {
            bool result = false;
            string sqlCommand = "";
            MySqlException internalEX = null;
            int locationID = 0;
            MySqlDataReader rdr;
            List<string> productIDList = new List<string>();
            List<string> productIDQty = new List<string>();

            if (salesActiveStatus == 0)
                return true;

            DS.beginTransaction();

            try
            {
                DS.mySqlConnect();

                // UPDATE SALES HEADER SET SALES ACTIVE TO 0
                sqlCommand = "UPDATE SALES_HEADER SET SALES_ACTIVE = 0 WHERE SALES_INVOICE = '" + noInvoice + "' AND REV_NO = '" + revNo + "'";
                if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                    throw internalEX;

                // GET LIST OF PRODUCT ID
                productIDList.Clear();
                productIDQty.Clear();

                sqlCommand = "SELECT PRODUCT_ID, PRODUCT_QTY FROM SALES_DETAIL WHERE SALES_INVOICE = '" + noInvoice + "' AND REV_NO = '" + revNo + "'";
                using (rdr = DS.getData(sqlCommand))
                {
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            productIDList.Add(rdr.GetString("PRODUCT_ID"));
                            productIDQty.Add(rdr.GetString("PRODUCT_QTY"));
                        }
                    }
                }
                rdr.Close();

                locationID = gutil.loadlocationID(2);

                for (int i = 0; i < productIDList.Count; i++)
                {
                    // REDUCE STOCK AT MASTER STOCK
                    sqlCommand = "UPDATE MASTER_PRODUCT SET PRODUCT_STOCK_QTY = PRODUCT_STOCK_QTY - " + productIDQty[i].ToString() + " WHERE PRODUCT_ID = '" + productIDList[i].ToString() + "'";
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;

                    // REDUCE STOCK AT PRODUCT LOCATION
                    sqlCommand = "UPDATE PRODUCT_LOCATION SET PRODUCT_LOCATION_QTY = PRODUCT_LOCATION_QTY - " + productIDQty[i].ToString() + " WHERE PRODUCT_ID = '" + productIDList[i].ToString() + "' AND LOCATION_ID = " + locationID;
                    if (!DS.executeNonQueryCommand(sqlCommand, ref internalEX))
                        throw internalEX;
                }

                DS.commit();
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        private void printOutDeliveryOrder(string SONo, string revNo, int salesActiveStatus)
        {
            string sqlCommandx = "SELECT '" + salesActiveStatus + "' AS 'SALES_STATUS', SH.SALES_DATE AS 'TGL', SH.SALES_INVOICE AS 'INVOICE', IFNULL(MC.CUSTOMER_FULL_NAME, '') AS 'CUSTOMER_NAME', MP.PRODUCT_NAME AS 'PRODUK', SD.PRODUCT_QTY AS 'QTY' " +
                                        "FROM SALES_HEADER SH LEFT OUTER JOIN MASTER_CUSTOMER MC ON (SH.CUSTOMER_ID = MC.CUSTOMER_ID) , SALES_DETAIL SD, MASTER_PRODUCT MP " +
                                        "WHERE SH.SALES_INVOICE = '" + SONo + "' AND SD.SALES_INVOICE = SH.SALES_INVOICE AND SD.PRODUCT_ID = MP.PRODUCT_ID AND SD.REV_NO = '" + revNo + "' AND SH.REV_NO = '" + revNo + "' AND MP.PRODUCT_IS_SERVICE = 0";

            DS.writeXML(sqlCommandx, globalConstants.deliveryOrderXML);
            deliveryOrderPrintOutForm displayForm = new deliveryOrderPrintOutForm();
            displayForm.ShowDialog(this);
        }

        private void generateDO_Click(object sender, EventArgs e)
        {
            string dialogMessage = "";
            string revNo = "0";

            // FOR REJEKI UTAMA, NO REVISION, SO THE REV NO VALUE IS ALWAYS 0
            salesActiveStatus = Convert.ToInt32(DS.getDataSingleValue("SELECT SALES_ACTIVE FROM SALES_HEADER WHERE SALES_INVOICE = '" + selectedsalesinvoice + "' AND REV_NO = '" + revNo + "'"));
            if (salesActiveStatus == 1)
            {
                dialogMessage = "TERBITKAN DELIVERY ORDER ?";
            }
            else
            {
                dialogMessage = "TERBITKAN COPY DELIVERY ORDER ?";
            }

            if (DialogResult.Yes == MessageBox.Show(dialogMessage, "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
            {
                // UPDATE SALES HEADER SET TO NON ACTIVE AND REDUCE STOCK
                if (processSalesOrderToDO(selectedsalesinvoice, revNo, salesActiveStatus))
                    printOutDeliveryOrder(selectedsalesinvoice, revNo, salesActiveStatus);

                gutil.setReadOnlyAllControls(this);

                generateDO.Enabled = true;
                generateSuratTugas.Enabled = true;
            }
        }

        private void generateSuratTugas_Click(object sender, EventArgs e)
        {
            cashierForm displayedSuratTugasForm = new cashierForm(globalConstants.TUGAS_PEMASANGAN_BARU, selectedsalesinvoice);
            displayedSuratTugasForm.ShowDialog(this);
        }

        private void discJualMaskedTextBox_TextChanged(object sender, EventArgs e)
        {
            double tempValue = 0;

            tempValue = Math.Round((globalTotalValue * discPercentValue) / 100);

            if (discJualMaskedTextBox.Text.Length > 0)
            {
                totalAfterDisc = globalTotalValue - tempValue - Convert.ToDouble(discJualMaskedTextBox.Text);
                discValue = Convert.ToDouble(discJualMaskedTextBox.Text);
            }
            else
            {
                totalAfterDisc = globalTotalValue - tempValue;
                discValue = 0;
            }

            gutil.saveSystemDebugLog(globalConstants.MENU_PENJUALAN, "CASHIER FORM : discJualMaskedTextBox_Validating, totalAfterDisc [" + totalAfterDisc + "]");
            totalAfterDiscTextBox.Text = totalAfterDisc.ToString("C2", culture);
            totalLabel.Text = totalAfterDisc.ToString("c2", culture);

            calculateChangeValue();
        }
    }
}
