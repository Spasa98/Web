using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Domacin
    {
        [Key]
        public int ID { get; set; }
        [JsonIgnore]
        public virtual Ekipa EkipaDomacin { get; set; }
        public int EkipaDomacinID { get; set; }
    }
}