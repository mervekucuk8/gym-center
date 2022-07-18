using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SporSalonu.Models
{
    public class kayitolmodel
    {
     
        public int kullanici_id { get; set; }
        public string tcno { get; set; }
        public string kullaniciadi { get; set; }
        public string sifre { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string telno { get; set; }
        [DataType(DataType.Date)]
        public DateTime dtraih { get; set; }
        public string il { get; set; }
        public string ilce { get; set; }
        public string adres { get; set; }

    }
}
