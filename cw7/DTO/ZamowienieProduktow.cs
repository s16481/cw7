using System;
using System.Collections.Generic;
using cw7.Models;

namespace cw7.DTO
{
    public class ZamowienieProduktow
    {
        public DateTime dataPrzyjecia { get; set; }
        public string Uwagi { get; set; }
        public IList<IDictionary<string, string>> wyroby { get; set; }
    }
}