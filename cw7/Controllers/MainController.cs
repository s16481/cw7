using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using cw7.DTO;
using cw7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;

namespace cw7.Controllers
{
    [ApiController]
    [Route("/")]
    public class MainController : ControllerBase
    {
        private DataContext _context;

        public MainController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult WszystkieZamowienia()
        {

            var zamowienia = _context.Zamowienia.Include(z => z.wyroby).ThenInclude(e=> e.WyrobCukierniczy).
                Select(
                e=>new {
                    e.ZamowienieId,
                    e.DataPrzyjecia,
                    e.DataRealizacji,
                    e.Uwagi,
                    wyroby=e.wyroby.Select(
                        e=>new {
                            e.WyrobCukierniczy.WyrobCukierniczyId,
                            e.WyrobCukierniczy.Nazwa,
                            e.WyrobCukierniczy.Typ,
                            e.WyrobCukierniczy.CenaZaSztuke,
                            e.Ilosc,
                            e.Uwagi}
                        )
                });
            return Ok(zamowienia);
        }

        [HttpGet("{Nazwisko}")]
        public IActionResult WszystkieZamowienia(string Nazwisko)
        {
            //var zamowienia = _context.Klienci.Include(e => e.Zamowienia).Where(k => k.Nazwisko == Nazwisko).Select(e => e.Zamowienia).ToArray();
            var zamowienia = _context.Klienci.Include(e => e.Zamowienia).ThenInclude(e => e.wyroby).Where(k => k.Nazwisko == Nazwisko).
                Select(e => 
                    e.Zamowienia.Select(e => new
                    {
                        e.ZamowienieId,
                        e.DataPrzyjecia,
                        e.DataRealizacji,
                        e.Uwagi,
                        wyrob = e.wyroby.Select(
                        e => new
                        {
                            e.WyrobCukierniczy.WyrobCukierniczyId,
                            e.WyrobCukierniczy.Nazwa,
                            e.WyrobCukierniczy.Typ,
                            e.WyrobCukierniczy.CenaZaSztuke,
                            e.Ilosc,
                            e.Uwagi
                        }
                        )
                    })
                );
            if(zamowienia.Count() > 0)
                return Ok(zamowienia);
            return NotFound();
        }

        [HttpPost("clients/{idClient}/orders")]
        public IActionResult dodajZamowienie(int idClient, ZamowienieProduktow zamowienie)
        {
            if(!_context.Klienci.Any(e => e.KlientId == idClient))
            {
                return BadRequest();
            }
            var zamowienieNew = new Zamowienie() { KlientId = idClient, DataPrzyjecia = zamowienie.dataPrzyjecia, Uwagi=zamowienie.Uwagi};
            IList<ZamowienieWyrobCukierniczy> listaWyrobow = new List<ZamowienieWyrobCukierniczy>();
            foreach(var wyrob in zamowienie.wyroby)
            {
                var wyrobNazwa = wyrob["wyrob"];
                var wyrobDb = _context.WyrobyCukiernicze.Where(e => e.Nazwa == wyrobNazwa).FirstOrDefault();
                if (wyrobDb == null)
                {
                    return BadRequest();
                }
                listaWyrobow.Add(
                    new ZamowienieWyrobCukierniczy() { Zamowienie = zamowienieNew, WyrobCukierniczy = wyrobDb, Ilosc = int.Parse(wyrob["ilosc"]), Uwagi = wyrob["uwagi"] }
                    );
            }
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.ZamowieniaWyrobowCukierniczych.AddRange(listaWyrobow);
                    _context.Zamowienia.Add(zamowienieNew);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

                return Created("zamowienia", zamowienieNew);
        }
    }
}