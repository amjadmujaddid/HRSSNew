using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace HRSS.Utility
{
    public class LogFileException
    {
        /// <summary>
        /// Method for Exception Message - Save Log File.log
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="dtStart"></param>
        public static void LogError(Exception ex, DateTime dtStart)
        {
            string compName = Dns.GetHostName();
            string message = "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Start: {0}", dtStart.ToString("dd-MMM-yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("ErrorCode: {0}", ex.HResult.ToString());
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format(string.Format("End: {0}", DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt")));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string fileName = "ErrorMessage_" + compName + "_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".log";
            string path = Conf.LogErrorFolder + fileName;
            //string path = "H:\\Project\\HRSS\\HRSS.UI\\LogFile\\" + fileName;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
