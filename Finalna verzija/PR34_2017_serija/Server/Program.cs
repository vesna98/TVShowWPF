using Common.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//[assembly: log4net.Config.XmlConfigurator(Watch =true)]
namespace Server
{
    class Program
    {
       // private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            Channels channels = new Channels();
            channels.Open();
            using (var db = new Serijica2Context())
            {
                User user = db.Users.ToList().Find(x => x.Username.Equals("admin"));
                if (user == null)
                {
                    db.Users.Add(new User { Username = "admin", Role = "admin", Name = "admin", Lastname = "admin", Password = "admin" });
                    db.SaveChanges();
                }

            }
            //log.Info("The program has started.");
            Console.ReadLine();
            channels.Close();
        }
    }
}
