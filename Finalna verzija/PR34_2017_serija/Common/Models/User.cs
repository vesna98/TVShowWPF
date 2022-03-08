using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    [DataContract]
    public class User : INotifyPropertyChanged
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Userid { get; set; }
        [DataMember]
        [Key]
        public string Username { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Password { get; set; }

        public User(string u, string p)
        {
            Username = u;
            Password = p;
        }

        public User()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
