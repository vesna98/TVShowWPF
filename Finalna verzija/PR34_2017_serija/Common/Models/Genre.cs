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
    public class Genre: INotifyPropertyChanged
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IDGenre { get; set; }
        [DataMember]
        public string GenreName { get; set; }
        [DataMember]
        public int IDshow { get; set; }

        public Genre()
        {

        }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{GenreName} ";
        }
    }
}
