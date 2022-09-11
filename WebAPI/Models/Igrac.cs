using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Igrac
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }
        public string Pozicija { get; set; }
        public int BrojNaDresu { get; set; }
        [JsonIgnore]
        public virtual Ekipa IgracEkipe { get; set; }
        [JsonIgnore]
        
        public int IgracEkipeID { get; set; }
        [JsonIgnore]
        public virtual List<Strelac> IgracStrelc { get; set; }



    }
}