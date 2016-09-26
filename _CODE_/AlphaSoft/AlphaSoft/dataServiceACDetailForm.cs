using System;
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

namespace AlphaSoft
{
    public partial class dataServiceACDetailForm : Form
    {
        private const string posTitle = "REJEKI UTAMA - SERVICE AC";
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
        private int selectedPaymentMethod = 0;
        private bool isLoading = false;
        private bool isLoadingDiscPercent = false;
        private double bayarAmount = 0;
        private double sisaBayar = 0;
        private int originModuleID = 0;
        private int custIsBlocked = 0;
        private double totalAfterDisc = 0;
        private string discJualPersenValueText = "";

        private Data_Access DS = new Data_Access();

        private globalUtilities gutil = new globalUtilities();
        private CultureInfo culture = new CultureInfo("id-ID");
        private List<string> salesQty = new List<string>();
        private List<string> disc1 = new List<string>();
        private List<string> disc2 = new List<string>();
        private List<string> discRP = new List<string>();

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

        public dataServiceACDetailForm()
        {
            InitializeComponent();
            titleLabel.Text = posTitle;
        }

        public dataServiceACDetailForm(int moduleID)
        {
            InitializeComponent();

            originModuleID = moduleID;
            titleLabel.Text = posTitle;
        }

        
    }
}
