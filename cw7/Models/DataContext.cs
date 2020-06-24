using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace cw7.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<WyrobCukierniczy> WyrobyCukiernicze { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<ZamowienieWyrobCukierniczy> ZamowieniaWyrobowCukierniczych { get; set; }
        
        public DataContext() {}
        public DataContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZamowienieWyrobCukierniczy>()
                .HasKey(c => new {c.ZamowienieId, c.WyrobCukierniczyId});
            base.OnModelCreating(modelBuilder);


            var dictPracownicy = new List<Pracownik>();
            var dictKlient = new List<Klient>();
            var dictWyrobCukierniczy = new List<WyrobCukierniczy>();
            var dictZamowienie = new List<Zamowienie>();
            var dictZamowienieWyrob = new List<ZamowienieWyrobCukierniczy>();

            var klien1 = new Klient {KlientId = 1, Imie = "Adam", Nazwisko = "Kowalski"};
            dictKlient.Add(klien1);
            var pracownik1 = new Pracownik {PracownikId = 1, Imie = "Krystian", Nazwisko = "Teodorski"};
            dictPracownicy.Add(pracownik1);
            var wyrob1 = new WyrobCukierniczy {CenaZaSztuke = 10.0f, WyrobCukierniczyId = 1, Nazwa = "Pączek", Typ = "Słodycz"};
            dictWyrobCukierniczy.Add(wyrob1);
            var zamowienie1 = new Zamowienie
            {
                DataPrzyjecia = DateTime.Now, DataRealizacji = DateTime.Now, KlientId = klien1.KlientId,
                PracownikId = pracownik1.PracownikId,
                ZamowienieId = 1
            };
            dictZamowienie.Add(zamowienie1);
            var zamowienieWyrob1 = new ZamowienieWyrobCukierniczy
            {
                WyrobCukierniczyId = wyrob1.WyrobCukierniczyId, ZamowienieId = zamowienie1.ZamowienieId, Ilosc = 1
            };
            dictZamowienieWyrob.Add(zamowienieWyrob1);

            modelBuilder.Entity<Pracownik>().HasData(dictPracownicy);
            modelBuilder.Entity<Klient>().HasData(dictKlient);
            modelBuilder.Entity<WyrobCukierniczy>().HasData(dictWyrobCukierniczy);
            modelBuilder.Entity<Zamowienie>().HasData(dictZamowienie);
            modelBuilder.Entity<ZamowienieWyrobCukierniczy>().HasData(dictZamowienieWyrob);

        }
    }
}