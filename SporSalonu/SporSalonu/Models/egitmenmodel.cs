using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SporSalonu.Models
{
    public class egitmenmodel
    {
       
        public int id { get; set; }     
        public string adi { get; set; }
        public string soyad { get; set; }
        public string  telefon { get; set; }
        [DataType(DataType.Date)]
        public DateTime dogumtarihi { get; set; }
        public string il { get; set; }
        public string ilce { get; set; }
        public string adres { get; set; }
    }
}
