using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Models
{
    public class Kolo
    {
        [Key]
        public int ID { get; set; }
        public int rbrKola { get; set; }
        public DateTime pocetakKola { get; set;}
        public DateTime krajkKola { get; set;}
        [JsonIgnore]
        public virtual List<Utakmica> KoloUtakmice { get; set; }
       

    }
}
