using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Utakmica
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime DatumIVremeUtakmice { get; set; }
        public int Pobednik { get; set;}
        public int brGolovaDomacin { get; set; }
        public int brGolovaGost { get; set; }
        public virtual Domacin DomacaEkipa { get; set; }
        public virtual Gost GostujucaEkipa { get; set; }
        [JsonIgnore]
        public virtual Kolo UtakmicaKolo { get; set; }
        [JsonIgnore]
        public virtual List<Strelac> UtakmicaStrelci { get; set; }




    }
}