using Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server.DBService
{
    public class ClientValidation : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            bool ok = false;
            
            IDBUserContract baza = new DatabaseUserProvider();
            List<User> users = baza.GetAllUsers();
            foreach (User x in users)
            {
                if (x.Username == userName && x.Password == password)
                {
                    ok = true;
                    break;
                }
            }
            if (!ok)
            {
                throw new FaultException();
            }
        }
    }
}
