using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw7.Models
{
    public class Zamowienie
    {
        [Key]
        public int ZamowienieId { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public DateTime DataRealizacji { get; set; }
        [MaxLength(300)]
        public string Uwagi { get; set; }
        
        public int KlientId { get; set; }
        public Klient Klient { get; set; }
        public int PracownikId { get; set; }
        public Pracownik Pracownik { get; set; }
        public List<ZamowienieWyrobCukierniczy> wyroby { get; set; }
    }
}