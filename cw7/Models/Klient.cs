using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw7.Models
{
    public class Klient
    {
        [Key]
        public int KlientId { get; set; }
        [MaxLength(50)]
        public string Imie { get; set; }
        [MaxLength(60)]
        public string Nazwisko { get; set; }

        public ICollection<Zamowienie> Zamowienia { get; set; }
    }
}