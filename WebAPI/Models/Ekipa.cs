using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Ekipa
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ImeEkipe { get; set; }
        public int Bodovi { get; set; }
        public int EkipaStadionID { get; set; }
        public string putanjaSlike { get; set; }
        [JsonIgnore]
        public virtual Stadion EkipaStadion { get; set; }
        [JsonIgnore]
        public virtual List<Gost> EkipaGost { get; set; }
        [JsonIgnore]
        public virtual List<Domacin> EkipaDomacin { get; set; }
        [JsonIgnore]
        public virtual List<Igrac> ListaIgraca { get; set; }
    }
}