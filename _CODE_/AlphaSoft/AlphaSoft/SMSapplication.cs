/*
 * Created by: Syeda Anila Nusrat. 
 * Date: 1st August 2009
 * Time: 2:54 PM 
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;

namespace AlphaSoft
{
    public partial class SMSapplication : Form
    {

        private static bool connectionStatus = false;
        private string portName;
        private int baudRate;
        private int dataBits;
        private int readTimeOut;
        private int writeTimeOut;

        private static StreamWriter sw = null;
        private globalUtilities gUtil = new globalUtilities();

        #region Constructor
        public SMSapplication()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Variables
        SerialPort port = new SerialPort();
        clsSMS objclsSMS = new clsSMS();
        ShortMessageCollection objShortMessageCollection = new ShortMessageCollection();
        #endregion

        #region Private Methods

        #region Write StatusBar
        private void WriteStatusBar(string status)
        {
            try
            {
                statusBar1.Text = "Message: " + status;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #endregion

        // CUSTOMIZED FUNCTIONS
        // ===================================================
        public bool SMSGateway_isConnected()
        {
            return connectionStatus;
        }

        private void loadPortDefaultSetting()
        {
            portName = "COM1";
            baudRate = 9600;
            dataBits = 8;
            readTimeOut = 300;
            writeTimeOut = 300;
        }

        private void loadPortSetting()
        {
            string line;

            loadPortDefaultSetting();

            try
            {
                using (StreamReader sr = new StreamReader(Application.StartupPath + "\\portSetting"))
                {
                    portName = sr.ReadLine();

                    line = sr.ReadLine();
                    int.TryParse(line, out baudRate);

                    line = sr.ReadLine();
                    int.TryParse(line, out dataBits);

                    line = sr.ReadLine();
                    int.TryParse(line, out readTimeOut);

                    line = sr.ReadLine();
                    int.TryParse(line, out writeTimeOut);
                }
            }
            catch(Exception ex)
            {
                gUtil.saveSystemDebugLog(0, "[SMS] ERROR WHEN READING PORT SETTING [" + ex.Message + "]");
            }
        }

        private void savePortSetting()
        {
            sw = File.CreateText(Application.StartupPath + "\\portSetting");

            sw.WriteLine(this.cboPortName.Text);
            sw.WriteLine(this.cboBaudRate.Text);
            sw.WriteLine(this.cboDataBits.Text);
            sw.WriteLine(this.txtReadTimeOut.Text);
            sw.WriteLine(this.txtWriteTimeOut.Text);

            sw.Close();
        }

        public bool start_SMSGateWayPortConnection()
        {
            loadPortSetting();
            return connectToPort(ref this.port, portName, baudRate, dataBits, readTimeOut, writeTimeOut);
        }

        public void stop_SMSGateWayPortConnection()
        {
            objclsSMS.ClosePort(this.port);
            connectionStatus = false;
        }

        private bool connectToPort(ref SerialPort portParam, string portNameParam, int baudRateParam, int dataBitsParam, int readTimeOutParam, int writeTimeOutParam)
        {
            bool result = false;

            portParam = objclsSMS.OpenPort(portNameParam, baudRateParam, dataBitsParam, readTimeOutParam, writeTimeOutParam);

            if (portParam != null)
                connectionStatus = true;
            else
                connectionStatus = false;

            return result;
        }

        public void sendMessage(string SIMParam, string txtMessageParam)
        {
            //.............................................. Send SMS ....................................................
            try
            {
                if (objclsSMS.sendMsg(this.port, SIMParam, txtMessageParam))
                {
                    //MessageBox.Show("Message has sent successfully");
                    //this.statusBar1.Text = "Message has sent successfully";
                    gUtil.saveSystemDebugLog(0, "[SMS] Message has sent successfully");
                }
                else
                {
                    //MessageBox.Show("Failed to send message");
                    //this.statusBar1.Text = "Failed to send message";
                    gUtil.saveSystemDebugLog(0, "[SMS] Failed to send message");
                }

            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message);
                gUtil.saveSystemDebugLog(0, "[SMS] " + ex.Message);
            }

        }
        // ===================================================

        #region Private Events

        private void SMSapplication_Load(object sender, EventArgs e)
        {
            try
            {
                #region Display all available COM Ports
                string[] ports = SerialPort.GetPortNames();

                // Add all port names to the combo box:
                foreach (string port in ports)
                {
                    this.cboPortName.Items.Add(port);
                }
                #endregion

                //Remove tab pages
                this.tabSMSapplication.TabPages.Remove(tbSendSMS);
                this.tabSMSapplication.TabPages.Remove(tbReadSMS);
                this.tabSMSapplication.TabPages.Remove(tbDeleteSMS);

                this.btnDisconnect.Enabled = false;
            }
            catch(Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            bool result = false;
            try
            {
                //Open communication port 
                //this.port = objclsSMS.OpenPort(this.cboPortName.Text, Convert.ToInt32(this.cboBaudRate.Text), Convert.ToInt32(this.cboDataBits.Text), Convert.ToInt32(this.txtReadTimeOut.Text), Convert.ToInt32(this.txtWriteTimeOut.Text));

                result = connectToPort(ref this.port, this.cboPortName.Text, Convert.ToInt32(this.cboBaudRate.Text), Convert.ToInt32(this.cboDataBits.Text), Convert.ToInt32(this.txtReadTimeOut.Text), Convert.ToInt32(this.txtWriteTimeOut.Text));

                //if (this.port != null)
                if (result)
                {
                    this.gboPortSettings.Enabled = false;

                    //MessageBox.Show("Modem is connected at PORT " + this.cboPortName.Text);
                    this.statusBar1.Text = "Modem is connected at PORT " + this.cboPortName.Text;

                    //Add tab pages
                    this.tabSMSapplication.TabPages.Add(tbSendSMS);
                    //this.tabSMSapplication.TabPages.Add(tbReadSMS);
                    //this.tabSMSapplication.TabPages.Add(tbDeleteSMS);

                    this.lblConnectionStatus.Text = "Connected at " + this.cboPortName.Text;
                    this.btnDisconnect.Enabled = true;

                    savePortSetting();
                }

                else
                {
                    //MessageBox.Show("Invalid port settings");
                    this.statusBar1.Text = "Invalid port settings";
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }

        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.gboPortSettings.Enabled = true;
                objclsSMS.ClosePort(this.port);

                //Remove tab pages
                this.tabSMSapplication.TabPages.Remove(tbSendSMS);
                //this.tabSMSapplication.TabPages.Remove(tbReadSMS);
                //this.tabSMSapplication.TabPages.Remove(tbDeleteSMS);

                this.lblConnectionStatus.Text = "Not Connected";
                this.btnDisconnect.Enabled = false;

                connectionStatus = false;
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {

            //.............................................. Send SMS ....................................................
            try
            {

                if (objclsSMS.sendMsg(this.port, this.txtSIM.Text, this.txtMessage.Text))
                {
                    //MessageBox.Show("Message has sent successfully");
                    this.statusBar1.Text = "Message has sent successfully";
                }
                else
                {
                    //MessageBox.Show("Failed to send message");
                    this.statusBar1.Text = "Failed to send message";
                }
                
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }
        private void btnReadSMS_Click(object sender, EventArgs e)
        {
            try
            {
                //count SMS 
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                if (uCountSMS > 0)
                {

                    #region Command
                    string strCommand = "AT+CMGL=\"ALL\"";

                    if (this.rbReadAll.Checked)
                    {
                        strCommand = "AT+CMGL=\"ALL\"";
                    }
                    else if (this.rbReadUnRead.Checked)
                    {
                        strCommand = "AT+CMGL=\"REC UNREAD\"";
                    }
                    else if (this.rbReadStoreSent.Checked)
                    {
                        strCommand = "AT+CMGL=\"STO SENT\"";
                    }
                    else if (this.rbReadStoreUnSent.Checked)
                    {
                        strCommand = "AT+CMGL=\"STO UNSENT\"";
                    }
                    #endregion

                    // If SMS exist then read SMS
                    #region Read SMS
                    //.............................................. Read all SMS ....................................................
                    objShortMessageCollection = objclsSMS.ReadSMS(this.port, strCommand);
                    foreach (ShortMessage msg in objShortMessageCollection)
                    {

                        ListViewItem item = new ListViewItem(new string[] { msg.Index, msg.Sent, msg.Sender, msg.Message });
                        item.Tag = msg;
                        lvwMessages.Items.Add(item);

                    }
                    #endregion

                }
                else
                {
                    lvwMessages.Clear();
                    //MessageBox.Show("There is no message in SIM");
                    this.statusBar1.Text = "There is no message in SIM";
                    
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }
        private void btnDeleteSMS_Click(object sender, EventArgs e)
        {
            try
            {
                //Count SMS 
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                if (uCountSMS > 0)
                {
                    DialogResult dr = MessageBox.Show("Are u sure u want to delete the SMS?", "Delete confirmation", MessageBoxButtons.YesNo);

                    if (dr.ToString() == "Yes")
                    {
                        #region Delete SMS

                        if (this.rbDeleteAllSMS.Checked)
                        {                           
                            //...............................................Delete all SMS ....................................................

                            #region Delete all SMS
                            string strCommand = "AT+CMGD=1,4";
                            if (objclsSMS.DeleteMsg(this.port, strCommand))
                            {
                                //MessageBox.Show("Messages has deleted successfuly ");
                                this.statusBar1.Text = "Messages has deleted successfuly";
                            }
                            else
                            {
                                //MessageBox.Show("Failed to delete messages ");
                                this.statusBar1.Text = "Failed to delete messages";
                            }
                            #endregion
                            
                        }
                        else if (this.rbDeleteReadSMS.Checked)
                        {                          
                            //...............................................Delete Read SMS ....................................................

                            #region Delete Read SMS
                            string strCommand = "AT+CMGD=1,3";
                            if (objclsSMS.DeleteMsg(this.port, strCommand))
                            {
                                //MessageBox.Show("Messages has deleted successfuly");
                                this.statusBar1.Text = "Messages has deleted successfuly";
                            }
                            else
                            {
                                //MessageBox.Show("Failed to delete messages ");
                                this.statusBar1.Text = "Failed to delete messages";
                            }
                            #endregion

                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }

        }
        private void btnCountSMS_Click(object sender, EventArgs e)
        {
            try
            {
                //Count SMS
                int uCountSMS = objclsSMS.CountSMSmessages(this.port);
                this.txtCountSMS.Text = uCountSMS.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message);
            }
        }

        #endregion

        #region Error Log
        public void ErrorLog(string Message)
        {
            StreamWriter sw = null;

            try
            {
                WriteStatusBar(Message);

                string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
                //string sPathName = @"E:\";
                string sPathName = @"SMSapplicationErrorLog_";

                string sYear = DateTime.Now.Year.ToString();
                string sMonth = DateTime.Now.Month.ToString();
                string sDay = DateTime.Now.Day.ToString();

                string sErrorTime = sDay + "-" + sMonth + "-" + sYear;

                sw = new StreamWriter(sPathName + sErrorTime + ".txt", true);

                sw.WriteLine(sLogFormat + Message);
                sw.Flush();

            }
            catch (Exception ex)
            {
                //ErrorLog(ex.ToString());
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                    sw.Close();
                }
            }
            
        }
        #endregion 
    
    }
}