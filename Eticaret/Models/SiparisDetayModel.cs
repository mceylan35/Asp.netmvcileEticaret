using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eticaret.Models
{
    public class SiparisDetayModel
    {
        public int SipariId { get; set; }
        public string SiparisNumarasi { get; set; }
        public string UserName { get; set; }     
        public decimal ToplamTutar { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public virtual List<SiparisModel> SiparisModels { get; set; }
    }
}