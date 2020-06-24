using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw7.Models
{
    public class WyrobCukierniczy
    {
        [Key]
        public int WyrobCukierniczyId { get; set; }
        [MaxLength(200)]
        public string Nazwa { get; set; }
        public float CenaZaSztuke { get; set; }
        [MaxLength(40)]
        public string Typ { get; set; }

        public ICollection<ZamowienieWyrobCukierniczy> zamowienia { get; set; }
    }
}