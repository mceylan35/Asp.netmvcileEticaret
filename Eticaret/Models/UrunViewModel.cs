using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;

namespace Eticaret.Models
{
    public class UrunViewModel
    {

        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }

        

    }
}