using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Areas.Admin.Controllers
{
    [Authorize]
    public class PromosyonController : BaseController
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Admin/Promosyon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UrunBazliIndirim()
        {
            var model = uow.UrunRepository.GetAll().ToList();
            ViewBag.Urun_ID = new SelectList(uow.GetRepository<tbl_Urun>().GetAll(), "Urun_Id", "Urun_Adi");
            return View("UrunBazliIndirim", model);
        }
        public ActionResult UrunIndirim(tbl_Urun urun, decimal indirim)
        {
            if (urun.Urun_Id != 0)
            {
                var IndirimYapilacakUrun = uow.UrunRepository.Get(x => x.Urun_Id == urun.Urun_Id);
                if (IndirimYapilacakUrun.Indirim_Oranı == 1)
                {

                    IndirimYapilacakUrun.Indirim_Oranı += indirim;
                }
                else
                {
                    IndirimYapilacakUrun.Indirim_Oranı += indirim;
                }

                uow.SaveChanges();
                ViewBag.mesaj = "indirim işlemi tamamlandı";
            }
            return View("Index");
        }
        public ActionResult KategoriBazliIndirim()
        {
            var model = uow.GetRepository<tbl_Kategori>().GetAll().ToList();
            ViewBag.Kategori_ID = new SelectList(uow.GetRepository<tbl_Kategori>().GetAll(), "Kategori_Id", "Kategori");
            return View("KategoriBazliIndirim", model);
        }
        public ActionResult KategoriIndirim(tbl_Kategori kategori, decimal indirim)
        {
            var IndirimYapilacakUrunler = uow.GetRepository<tbl_Urun>().GetAll().Where(x => x.Kategori_ID == kategori.Kategori_Id);
            foreach (var urun in IndirimYapilacakUrunler)
            {
                if (urun.Indirim_Oranı==1)
                {
                    urun.Indirim_Oranı = indirim;
                }
                else
                {
                    urun.Indirim_Oranı += indirim;
                }
            }
          
            return View("Index");
        }
        public ActionResult TumUrunlerdeIndirim()
        {
            var model = uow.GetRepository<tbl_Urun>().GetAll().ToList();
            return View("TumUrunlerdeIndirim", model);
        }

        public ActionResult TumIndirim(decimal indirim)
        {
            var TumUrunler = uow.UrunRepository.GetAll();
            foreach (var urun in TumUrunler)
            {
                if (urun.Indirim_Oranı == 1)
                {
                    urun.Indirim_Oranı = indirim;
                }
                else
                {
                    urun.Indirim_Oranı += indirim;
                }

            }

            uow.SaveChanges();
            ViewBag.mesaj = "indirim işlemi tamamlandı";
            return View("Index");
        }
    }
}