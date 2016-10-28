using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using System.Globalization;


namespace AlphaSoft
{
    class globalReminderLib
    {
        SMSapplication smsLib = new SMSapplication();
        private CultureInfo culture = new CultureInfo("id-ID");
        private Data_Access DS = new Data_Access();
        globalUtilities gUtil = new globalUtilities();

        public void checkSMSReminderTable(bool isTrial = false)
        {
            string sqlCommand = "";
            string updateSqlCommand = "";
            string timeNow;
            string dateNow;
            int deliveredStatus = 0;
            string nextReminderDate;
            string lastReminderDate;

            List<int> reminderID = new List<int>();
            List<string> textMessage = new List<string>();
            List<DateTime> scheduledDate = new List<DateTime>();
            List<int> intervalValue = new List<int>();
            List<int> intervalType = new List<int>();
            List<int> isRepeat = new List<int>();
            int customerIndex = 0;

            Data_Access updateDS = new Data_Access();

            MySqlDataReader rdr;

            timeNow = String.Format(culture, "{0:HH:mm}", DateTime.Now);
            dateNow = String.Format(culture, "{0:yyyyMMdd}", Convert.ToDateTime(DateTime.Now));


            if (!isTrial)
                // POPULATE LIST OF MATCHING REMINDER
                sqlCommand = "SELECT * FROM MASTER_REMINDER WHERE " +
                                       "DATE_FORMAT(SCHEDULED_DATE, '%Y%m%d')  = '" + dateNow + "' AND START_TIME <= '" + dateNow + "' AND REMINDER_ACTIVE = 1";
            else
                sqlCommand = "SELECT * FROM MASTER_REMINDER WHERE REMINDER_ACTIVE = 1 LIMIT 10";

            using (rdr = DS.getData(sqlCommand))
            {
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        reminderID.Add(rdr.GetInt32("REMINDER_ID"));
                        textMessage.Add(rdr.GetString("MESSAGE_CONTENT"));
                        scheduledDate.Add(rdr.GetDateTime("SCHEDULED_DATE"));
                        intervalValue.Add(rdr.GetInt32("INTERVAL_VALUE"));
                        intervalType.Add(rdr.GetInt32("INTERVAL_TYPE"));
                        isRepeat.Add(rdr.GetInt32("IS_REPEATED"));
                    }
                }
            }
            rdr.Close();

            // SEND SMS TO ALL CUSTOMER LISTED AND UPDATE THE DELIVERED STATUS
            try
            {
                if (!isTrial)
                    smsLib.start_SMSGateWayPortConnection();

                for (customerIndex = 0; customerIndex < reminderID.Count; customerIndex++)
                {
                    sqlCommand = "SELECT * FROM DETAIL_REMINDER_CUSTOMER WHERE REMINDER_ID = " + reminderID[customerIndex];
                    using (rdr = DS.getData(sqlCommand))
                    {
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                if (!isTrial)
                                {
                                    if (!smsLib.SMSGateway_isConnected())
                                        smsLib.start_SMSGateWayPortConnection();

                                    smsLib.sendMessage(rdr.GetString("CUSTOMER_PHONE"), textMessage[customerIndex], ref deliveredStatus);
                                }

                                updateDS.beginTransaction();

                                try
                                {
                                    updateDS.update_mySqlConnect();

                                    updateSqlCommand = "UPDATE DETAIL_REMINDER_CUSTOMER SET LAST_SEND_STATUS = " + deliveredStatus + " WHERE REMINDER_ID = " + reminderID[customerIndex] + " AND CUSTOMER_ID = " + rdr.GetInt32("CUSTOMER_ID");
                                    updateDS.executeNonQueryCommand(updateSqlCommand);

                                    updateDS.commit();
                                }
                                catch (Exception e)
                                {
                                    gUtil.saveSystemDebugLog(0, "[SMS] FAILED UPDATE MESSAGE SENT STATUS [" + e.Message + "]");
                                }

                            }
                        }
                    }
                    rdr.Close();
                }
                smsLib.stop_SMSGateWayPortConnection();
            }
            catch(Exception ex)
            {
                 // FAIL TO CONNECT TO PORT OR FAIL ON SENDING SMS
                gUtil.saveSystemDebugLog(0, "[SMS] FAILED TO SEND SCHEDULED MESSAGE [" + ex.Message + "]");
                for (int i = customerIndex; i < reminderID.Count; i++)
                {
                    updateDS.beginTransaction();

                    try
                    {
                        updateDS.update_mySqlConnect();

                        updateSqlCommand = "UPDATE DETAIL_REMINDER_CUSTOMER SET LAST_SEND_STATUS = 0 WHERE REMINDER_ID = " + reminderID[i] + " AND CUSTOMER_ID = " + rdr.GetInt32("CUSTOMER_ID");
                        updateDS.executeNonQueryCommand(updateSqlCommand);

                        updateDS.commit();
                    }
                    catch (Exception e)
                    {
                        gUtil.saveSystemDebugLog(0, "[SMS] FAILED UPDATE MESSAGE SENT STATUS [" + e.Message + "]");
                    }
                }
            }

            // UPDATE THE NEXT REMINDER DATE
            for (int j = 0; j < reminderID.Count; j++)
            {
                nextReminderDate = gUtil.calculateNextReminderDate(scheduledDate[j], intervalValue[j], intervalType[j]);
                lastReminderDate = String.Format(culture, "{0:dd-MM-yyyy}", scheduledDate[j]);
                updateDS.beginTransaction();

                try
                {
                    updateDS.update_mySqlConnect();

                    if (isRepeat[j] == 1)
                    {
                        updateSqlCommand = "UPDATE MASTER_REMINDER SET SCHEDULED_DATE = STR_TO_DATE('" + nextReminderDate + "', '%d-%m-%Y'), LAST_SEND_DATE = STR_TO_DATE('" + lastReminderDate + "', '%d-%m-%Y') WHERE REMINDER_ID = " + reminderID[j];
                    }
                    else
                    {
                        updateSqlCommand = "UPDATE MASTER_REMINDER SET REMINDER_ACTIVE = 0 WHERE REMINDER_ID = " + reminderID[j];
                    }

                    updateDS.executeNonQueryCommand(updateSqlCommand);

                    updateDS.commit();
                }
                catch (Exception e)
                {
                    gUtil.saveSystemDebugLog(0, "[SMS] FAILED TO UPDATE NEXT SCHEDULED DATE [" + e.Message + "]");
                }
            }
        }

    }
}
