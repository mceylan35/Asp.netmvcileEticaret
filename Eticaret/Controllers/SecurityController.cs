using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Controllers
{
    public class SecurityController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_Kullanici kullanici)
        {
            var Girenkullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Eposta == kullanici.Eposta && x.Sifre == kullanici.Sifre);
            if (Girenkullanici!=null)
            {
                FormsAuthentication.SetAuthCookie(Girenkullanici.Kullanici_Adi, false);
                return RedirectToAction("Anasayfa", "Anasayfa");
            }
            else
            {
                ViewBag.mesaj = "Geçersiz kullanıcı adı veya şifre";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Anasayfa","Anasayfa");
        }
        public ActionResult Uyeol()
        {
            var kullanici = new tbl_Kullanici();
            return View(kullanici);
        }
        [HttpPost]
        public ActionResult Uyeol(tbl_Kullanici kullanici)
        {
            uow.GetRepository<tbl_Kullanici>().Add(new tbl_Kullanici
            {
                Ad = kullanici.Ad,
                Soyad = kullanici.Soyad,
                Kullanici_Adi = kullanici.Kullanici_Adi,
                Sifre = kullanici.Sifre,
                Eposta = kullanici.Eposta,
                Telefon = kullanici.Telefon,
                UyeKayitTarihi = DateTime.Now,
                Rol_ID = 2
            });
            uow.SaveChanges();
            return RedirectToAction("Login","Security");
        }
    }
}