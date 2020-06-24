using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw7.Models
{
    [Table("Zamowienie_WyrobCukierniczy")]
    public class ZamowienieWyrobCukierniczy
    {
        public int WyrobCukierniczyId { get; set; }
        public WyrobCukierniczy WyrobCukierniczy { get; set; }
        
        public int ZamowienieId { get; set; }
        public Zamowienie Zamowienie { get; set; }
        
        
        public int Ilosc { get; set; }
        [MaxLength(300)]
        public string Uwagi { get; set; }
    }
}