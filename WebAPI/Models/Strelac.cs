using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;
namespace Models
{
    public class Strelac
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int minutGola { get; set; }
        public virtual Utakmica StrelacUtakmice { get; set; }
        public virtual Igrac StrelacIgrac { get; set; }

    }
}