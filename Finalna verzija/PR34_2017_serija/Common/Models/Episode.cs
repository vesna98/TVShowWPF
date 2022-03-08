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
    public class Episode : INotifyPropertyChanged
    {
        
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IDepisode{ get; set; }
        [DataMember]
        public int Index { get; set; }//i ovde omoguciti da to bude obicno polje i dodati idepiyode koji ce biti autogen
        [DataMember]
        public string PlotOutline { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public int Sezonaid { get; set; }

        public Episode()
        {

        }

        public Episode(int index, string plotOutline, string title, int sezona)
        {
            Index = index;
            this.PlotOutline = plotOutline;
            Title = title;
            Sezonaid = sezona;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
