using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret.Areas.Admin.Models
{
    public class EnCokSepeteEklenenler
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
    }
}