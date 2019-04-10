using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eticaret.Entities;

namespace Eticaret.ViewModel
{
    public class AnasayfaModelView
    {
        public tbl_Duyuru Duyuru { get; set; }
        public List<tbl_Urun> Urunler { get; set; }
    }
}