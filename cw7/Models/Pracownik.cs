using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw7.Models
{
    public class Pracownik
    {
        [Key]
        public int PracownikId { get; set; }
        [MaxLength(50)]
        public string Imie { get; set; }
        [MaxLength(60)]
        public string Nazwisko { get; set; }
        public ICollection<Zamowienie> Zamowienia { get; set; }
    }
}