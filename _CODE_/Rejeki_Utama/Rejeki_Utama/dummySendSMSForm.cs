using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace AlphaSoft
{
    public partial class dummySendSMSForm : Form
    {
        private string phoneNo = "";
        private string message = "";
        SMSapplication smsLib = new SMSapplication();
        //SerialPort port = new SerialPort();

        public dummySendSMSForm()
        {
            InitializeComponent();
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            int deliveredStatus = 0;

            smsLib.start_SMSGateWayPortConnection();

            if (smsLib.SMSGateway_isConnected())
            {
                phoneNo = txtSIM.Text;
                message = txtMessage.Text;

                smsLib.sendMessage(phoneNo, message, ref deliveredStatus);
            }

            smsLib.stop_SMSGateWayPortConnection();
        }
    }
}
