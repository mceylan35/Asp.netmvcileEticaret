using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret.Models
{
    public class SiparisModel
    {
        public int Urun_Id { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public string Resim_Url { get; set; }
        public decimal Fiyat { get; set; }
    }
}