using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.Models;
using Eticaret.UnitOfWork;

namespace Eticaret.Controllers
{
    public class MesajController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Mesaj
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MesajAt(tbl_Mesaj mesaj)
        {
            var Kullanici = uow.GetRepository<tbl_Kullanici>()
                .Get(x => x.Kullanici_Adi == HttpContext.User.Identity.Name);
            var body = new StringBuilder();
            if (HttpContext.User.Identity.Name!="")
            {
                body.AppendLine("Kullanıcı mesajı");
                body.AppendLine("Eposta: " + Kullanici.Eposta);
                body.AppendLine("isim: " + Kullanici.Ad+" "+Kullanici.Soyad);
            }
            else
            {
                body.AppendLine("Ziyaretçi mesajı");
                body.AppendLine("Eposta: " + mesaj.Eposta);
                body.AppendLine("isim: " + mesaj.AdSoyad);
            }
            body.AppendLine("Konu: " + mesaj.Konu);
            body.AppendLine("içerik: " + mesaj.Icerik);
            Eposta.SendMail(body.ToString());
            ViewBag.succes = true;

            return View("Index");
        }
    }
}