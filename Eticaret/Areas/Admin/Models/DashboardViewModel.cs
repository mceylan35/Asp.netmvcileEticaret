using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int ToplamUrunSatisSayisi { get; set; }
        public int ToplamUyeKayitSayisi { get; set; }
        public int ToplamZiyaretciSayisi { get; set; }
    }
}