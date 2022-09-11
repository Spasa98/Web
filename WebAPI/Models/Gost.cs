using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Gost
    {
        [Key]
        public int ID { get; set; }
        [JsonIgnore]
        public virtual Ekipa EkipaGost { get; set; }
        public int EkipaGostID { get; set; }

    }
}