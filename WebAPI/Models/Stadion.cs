using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Stadion
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string StadionName { get; set; }

        [JsonIgnore]
        public virtual List<Ekipa> StadionEkipa { get; set; }
    }
}