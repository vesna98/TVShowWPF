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
    public class TvShow : INotifyPropertyChanged
    {
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Showid { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int EpisodeCnt { get; set; }
        [DataMember]
        public List<Genre> Genres { get; set; }
        //[DataMember]
        //public string Zanrovi { get; set; }
        [DataMember]
        public List<Season> Sezone { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int SeasonCnt { get; set; }

        public TvShow()
        {
            Genres = new List<Genre>();
            Sezone = new List<Season>();
        }

        //deep copy
        public TvShow(string description, int episodeCnt, List<Genre> genres, List<Season> sezone, string name, int seasonCnt)
        {
            Description = description;
            EpisodeCnt = episodeCnt;
            Genres = new List<Genre>();
            Genres.AddRange(genres);
            Sezone = new List<Season>();
            Sezone.AddRange(sezone);
            Name = name;
            SeasonCnt = seasonCnt;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
