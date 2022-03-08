using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.ClientContracts
{
    [ServiceContract]
    public interface IUserContract
    {
        [OperationContract]
        User Login(string username);

        [OperationContract]
        bool AddUser(User user);

        [OperationContract]
        bool ChangeUser(User user);

        [OperationContract]
        List<User> GetAllUsers();
    }
}
