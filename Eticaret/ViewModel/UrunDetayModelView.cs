using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eticaret.Entities;

namespace Eticaret.ViewModel
{
    public class UrunDetayModelView
    {
        public tbl_Urun Urun { get; set; }
        public IEnumerable<tbl_Yorum> Yorumlar { get; set; }
        public tbl_Sepet AlinanUrun { get; set; }
        public tbl_Yorum VarMi { get; set; }
    }
}