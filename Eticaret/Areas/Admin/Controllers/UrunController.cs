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

namespace Eticaret.Areas.Admin.Controllers
{
    [Authorize]
    public class UrunController : BaseController
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Admin/Urun
        public ActionResult Liste()
        {
            var urunler = uow.UrunRepository.GetAll().Include("tbl_Kategori").Include("tbl_Marka");
            if (urunler == null)
            {
                HttpNotFound();
            }


            return View(urunler);

        }

        public ActionResult Ekle()
        {
            var urun = new tbl_Urun();
            ViewBag.Kategori_ID = new SelectList(uow.GetRepository<tbl_Kategori>().GetAll(), "Kategori_Id", "Kategori");
            ViewBag.Marka_ID = new SelectList(uow.MarkaRepository.GetAll(), "Marka_Id", "Marka_Adi");
            return View(urun);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(tbl_Urun urun, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0 || file != null)
                {


                    var extention = Path.GetExtension(file.FileName);
                    if (extention == ".jpg" || extention == ".png")
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload"), filename);
                        file.SaveAs(path);


                        uow.UrunRepository.Add(new tbl_Urun
                        {
                            Urun_Adi = urun.Urun_Adi,
                            Fiyat = urun.Fiyat,
                            Aciklama = urun.Aciklama,
                            Kategori_ID = urun.Kategori_ID,
                            Seri_No = urun.Seri_No,
                            Marka_ID = urun.Marka_ID,
                            Stok = urun.Stok,
                            Resim_URL = file.FileName,
                            Indirim_Oranı=1

                        });
                        uow.GetRepository<tbl_Yildiz>().Add(new tbl_Yildiz
                        {
                            Urun_Id = urun.Urun_Id,
                            Yildiz = 0
                        });
                        uow.SaveChanges();
                        return RedirectToAction("Liste");
                    }
                }
            }
            ViewBag.Kategori_ID = new SelectList(uow.GetRepository<tbl_Kategori>().GetAll(), "Kategori_Id", "Kategori");
            ViewBag.Marka_Id = new SelectList(uow.MarkaRepository.GetAll(), "Marka_Id", "Marka_Adi");
            return View(urun);
        }

        public void Sil(int id)
        {
            var urun = uow.UrunRepository.GetById(id);
            if (urun != null)
            {
                uow.UrunRepository.Delete(urun);
                uow.SaveChanges();
            }

        }

        public ActionResult Duzenle(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var urun = uow.UrunRepository.GetById(id);
            if (urun == null)
            {
                HttpNotFound();
            }
            ViewBag.Kategori_ID = new SelectList(uow.GetRepository<tbl_Kategori>().GetAll(), "Kategori_Id", "Kategori");
            ViewBag.Marka_Id = new SelectList(uow.MarkaRepository.GetAll(), "Marka_Id", "Marka_Adi");

            return View(urun);



        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Duzenle(tbl_Urun urun, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0 || file != null)
                {


                    var extention = Path.GetExtension(file.FileName);
                    if (extention == ".jpg" || extention == ".png" || extention == ".JPG" || extention == ".PNG")
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/upload"), filename);
                        file.SaveAs(path);
                        //var urun = uow.UrunRepository.Get(i => i.Urun_Id == satilanurun.Urun_Id);
                        //urun.Stok = satilanurun.Stok;
                        //uow.UrunRepository.Update(urun);

                       
                        var urunduzenle=new tbl_Urun
                        {
                            Urun_Id = urun.Urun_Id,
                            Urun_Adi = urun.Urun_Adi,
                            Fiyat = urun.Fiyat,
                            Aciklama = urun.Aciklama,
                            Kategori_ID = urun.Kategori_ID,
                            Seri_No = urun.Seri_No,
                            Marka_ID = urun.Marka_ID,
                            Stok = urun.Stok,
                            Resim_URL = file.FileName
                        };
                        uow.UrunRepository.Update(urunduzenle);
                        uow.SaveChanges();
                        return RedirectToAction("Liste");
                    }
                }
            }
            ViewBag.Kategori_ID = new SelectList(uow.GetRepository<tbl_Kategori>().GetAll(), "Kategori_Id", "Kategori");
            ViewBag.Marka_Id = new SelectList(uow.MarkaRepository.GetAll(), "Marka_Id", "Marka_Adi");
            return View(urun);
        }
        public void PromosyonSil(int id)
        {
            var urun = uow.UrunRepository.GetById(id);
            if (urun != null)
            {
                urun.Indirim_Oranı = 1;
                uow.SaveChanges();
            }
        }
        public void TumPromosyonSil(int id)
        {
            var urun = uow.UrunRepository.GetAll().ToList();
            foreach (var item in urun)
            {
                item.Indirim_Oranı = 1;
            }
            uow.SaveChanges();
        }

    }
}