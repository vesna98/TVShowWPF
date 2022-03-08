using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Common.Models
{
    public class LogModel/*: INotifyPropertyChanged*/
    {
        public string LogTime { get; set; }
        public string LogType { get; set; }
        public string LogMessage { get; set; }
        public LogModel()
        {

        }

        public LogModel(string logTime, string logType, string logMessage)
        {
            LogTime = logTime;
            LogType = logType;
            LogMessage = logMessage;
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
