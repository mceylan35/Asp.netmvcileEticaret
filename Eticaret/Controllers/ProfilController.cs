using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.Models;
using Eticaret.UnitOfWork;

namespace Eticaret.Controllers
{
    public class ProfilController : Controller
    {
        EfUnitOfWork uow=new EfUnitOfWork();
        // GET: Profil
        public ActionResult Siparislerim()
        {
            var kullanici = HttpContext.User.Identity.Name;
            var kulid = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Adi == kullanici);
            var siparisler=  uow.GetRepository<Siparis>().GetAll().Where(i => i.KullaniciId == kulid.Kullanici_Id)
                .OrderByDescending(i => i.SiparisId);

            return View(siparisler);
        }

        public ActionResult SiparisDetay(int id)
        {
            var detaylar = uow.GetRepository<Siparis>().GetAll().Where(i=>i.SiparisId==id).Select(i=>new SiparisDetayModel()
            {
                SipariId = (int)i.SiparisId,
                SiparisNumarasi = i.SiparisNumarasi,
                SiparisTarihi = (DateTime)i.SiparisTarihi,
                ToplamTutar = (decimal)i.ToplamTutar,
                SiparisModels = i.SiparisDetay.Select(a=>new SiparisModel()
                {
                    Urun_Id = (int)a.UrunId,
                    UrunAdi = a.tbl_Urun.Urun_Adi,
                    Resim_Url = a.tbl_Urun.Resim_URL,
                    Adet = (int)a.Adet,
                    Fiyat = (decimal)a.Fiyat,
                    

                }).ToList()
                

            }).FirstOrDefault();
            return View(detaylar);
        }

        public ActionResult Favorilerim()
        {
            var kullanici = HttpContext.User.Identity.Name;
            var kulid = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Adi == kullanici);
            var favorilerim = uow.GetRepository<tbl_Favori>().GetAll().Where(i => i.Kullanici_ID == kulid.Kullanici_Id).Include(i=>i.tbl_Urun);
            return View(favorilerim);
        }
        public ActionResult Profilim()
        {
            var kullanici = HttpContext.User.Identity.Name;
            var kulid = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Adi == kullanici);
            var profilim = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Id == kulid.Kullanici_Id);
            return View(profilim);
        }
        [HttpGet]
        public ActionResult ProfiliDüzenle()
        {

            var kullanici = HttpContext.User.Identity.Name;
            var kulid = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Adi == kullanici);
            var profilim = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Id == kulid.Kullanici_Id);
            return View(profilim);
        }

        [HttpPost]
        public ActionResult ProfiliDüzenle(tbl_Kullanici k)
        {
            uow.GetRepository<tbl_Kullanici>().Update(k);
            uow.SaveChanges();
            if (HttpContext.User.Identity.Name != k.Kullanici_Adi)
            {
                return RedirectToAction("Login", "Security");
            }
            else
                return RedirectToAction("Profilim");

        }
    }
}