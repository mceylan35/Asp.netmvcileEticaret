using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.UnitOfWork;
using Eticaret.ViewModel;


namespace Eticaret.Controllers
{
    public class UrunDetayController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: UrunDetay
        public ActionResult Detay(int id)
        {
            tbl_Yorum varMi;
            ViewBag.Kullanici = HttpContext.User.Identity.Name;
            var favorideMi = uow.GetRepository<tbl_Favori>().Get(x =>
                x.Urun_ID == id && x.tbl_Kullanici.Kullanici_Adi == HttpContext.User.Identity.Name);
            var GirenKullanici = uow.GetRepository<tbl_Kullanici>()
                .Get(x => x.Kullanici_Adi == HttpContext.User.Identity.Name);
            if (HttpContext.User.Identity.Name != "")
            {
                varMi = uow.GetRepository<tbl_Yorum>()
                    .GetAll(x => x.Kullanici_ID == GirenKullanici.Kullanici_Id && x.Urun_ID == id).FirstOrDefault();
            }
            else varMi = null;
            
            var model = new UrunDetayModelView
            {
                Urun = uow.UrunRepository.GetById(id),
                Yorumlar = uow.GetRepository<tbl_Yorum>().GetAll().Where(x => x.Urun_ID == id),
                VarMi = varMi
            };
            if (model.Urun != null && favorideMi != null)
            {
                ViewBag.mesaj = ">";
                return View(model);
            }
            else if (model.Urun != null)
            {
                return View(model);
            }
            else
            {
                return HttpNotFound("Ürün Bulunamadı!");
            }






        }

        public ActionResult Begen(int id)
        {
            var urun = uow.UrunRepository.GetById(id);
            var kullanici = HttpContext.User.Identity.Name;
            var begenen_kullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Kullanici_Adi == kullanici);
            var favoriMi = uow.GetRepository<tbl_Favori>()
                .Get(x => x.Kullanici_ID == begenen_kullanici.Kullanici_Id && x.Urun_ID == id);
            if (favoriMi == null)
            {
                uow.GetRepository<tbl_Favori>().Add(new tbl_Favori
                {
                    Kullanici_ID = begenen_kullanici.Kullanici_Id,
                    Urun_ID = urun.Urun_Id
                });
            }
            else
            {
                uow.GetRepository<tbl_Favori>().Delete(favoriMi.Favori_Id);
            }
            uow.SaveChanges();
            return View();
        }


        //public ActionResult Yorumyap(tbl_Yorum yorum, int id, int yildiz1)
        //{
        //    var kullanici = HttpContext.User.Identity.Name;
        //    var YorumYapanKullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Kullanici_Adi == kullanici);

        //    var varMi = uow.GetRepository<tbl_Yorum>().GetAll(i => i.Kullanici_ID == YorumYapanKullanici.Kullanici_Id)
        //        .FirstOrDefault();
        //    //var varMi = uow.GetRepository<tbl_Yorum>().Get(x => x.Kullanici_ID == YorumYapanKullanici.Kullanici_Id);
        //    var urunId = uow.GetRepository<tbl_Yildiz>().Get(x => x.Urun_Id == id);
        //    if (varMi == null)
        //    {
        //        uow.GetRepository<tbl_Yorum>().Add(new tbl_Yorum
        //        {
        //            Kullanici_ID = YorumYapanKullanici.Kullanici_Id,
        //            Urun_ID = id,
        //            Icerik = yorum.Icerik,
        //            Yildiz = yorum.Yildiz,
        //            tarih = DateTime.Now
        //        });



        //        uow.SaveChanges();
        //    }
        //    var yorumsayisi = uow.GetRepository<tbl_Yorum>().GetAll().Where(x => x.Urun_ID == id);
        //    if (urunId == null)
        //    {
        //        uow.GetRepository<tbl_Yildiz>().Add(new tbl_Yildiz
        //        {
        //            Yildiz = yorum.Yildiz,
        //            Urun_Id = id

        //        });
        //    }
        //    else
        //    {
        //        var güncelleme = uow.GetRepository<tbl_Yildiz>().Get(x => x.Urun_Id == id);
        //        if (yorumsayisi.Count()==0)
        //        {
        //            güncelleme.Yildiz = (yildiz1 + yorum.Yildiz) / 1;
        //        }
        //        else
        //        {
        //            güncelleme.Yildiz = (yildiz1 + yorum.Yildiz) / yorumsayisi.Count();
        //        }


        //        uow.GetRepository<tbl_Yildiz>().Update(güncelleme);
        //    }
        //    uow.SaveChanges();
        //    return RedirectToAction("Detay", "UrunDetay", new { id = id });
        //}

        public ActionResult Yorumyap(tbl_Yorum yorum, int id, int yildiz1)
        {
            var kullanici = HttpContext.User.Identity.Name;
            var YorumYapanKullanici = uow.GetRepository<tbl_Kullanici>().Get(x => x.Kullanici_Adi == kullanici);
            var varMi = uow.GetRepository<tbl_Yorum>().Get(x => x.Kullanici_ID == YorumYapanKullanici.Kullanici_Id && x.Urun_ID == id);
            var urunId = uow.GetRepository<tbl_Yildiz>().Get(x => x.Urun_Id == id);
            if (varMi == null)
            {
                uow.GetRepository<tbl_Yorum>().Add(new tbl_Yorum
                {
                    Kullanici_ID = YorumYapanKullanici.Kullanici_Id,
                    Urun_ID = id,
                    Icerik = yorum.Icerik,
                    Yildiz = yorum.Yildiz,
                    tarih = DateTime.Now
                });
                uow.SaveChanges();
            }
            var yorumsayisi = uow.GetRepository<tbl_Yorum>().GetAll().Where(x => x.Urun_ID == id);
            if (urunId == null)
            {
                uow.GetRepository<tbl_Yildiz>().Add(new tbl_Yildiz
                {
                    Yildiz = yorum.Yildiz,
                    Urun_Id = id

                });
            }
            else
            {
                var güncelleme = uow.GetRepository<tbl_Yildiz>().Get(x => x.Urun_Id == id);

                güncelleme.Yildiz = (yildiz1 + yorum.Yildiz) / yorumsayisi.Count();
            }
            uow.SaveChanges();
            return RedirectToAction("Detay", "UrunDetay", new { id = id });
        }
    }
}