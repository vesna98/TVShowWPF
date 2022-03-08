using Common.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Common
{
    public class Logger
    {
        public ILog log { get; private set; }
        private static Logger instance;
        private static object padlock = new object();
        public static Logger Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new Logger();

                    return instance;
                }
            }
            set
            {
                instance = value;
            }
        }

        private Logger()
        {
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); ;
        }

        public List<LogModel> ReadLogs(string path)
        {
            List<LogModel> logs = new List<LogModel>();

            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
            {
                // Actions you perform on the reader.


                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] recenica = line.Split('-');
                    string[] tokens = recenica[0].Split(' ');

                    LogModel p = new LogModel(tokens[0], tokens[2], recenica[1]);
                    logs.Add(p);

                }
            }
            //stream.Close();

            return logs;
        }
    }
}
