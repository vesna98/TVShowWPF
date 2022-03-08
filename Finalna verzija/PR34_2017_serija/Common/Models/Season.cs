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
    public class Season : INotifyPropertyChanged
    {
        //[DataMember]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        //public int Seasonid { get; set; }
        [DataMember]
        //public TvShow TvShow { get; set; }
        public int TvShow { get; set; }
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IDseason { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public List<Episode> Epizode { get; set; }
       
        //public string Naziv { get; set; }

        public Season()
        {
            Epizode=new List<Episode>();
        }

        public Season(int tvShow, List<Episode> epizode)
        {
            TvShow = tvShow;
            Epizode = epizode;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
