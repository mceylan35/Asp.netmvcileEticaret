using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Areas.Admin.Controllers
{
    public class KategoriController : BaseController
    {
        // GET: Admin/Kategori
        EfUnitOfWork uow =new EfUnitOfWork();

        public ActionResult Liste()
        {
            var kategoriler = uow.GetRepository<tbl_Kategori>().GetAll().ToList();
            if (kategoriler==null)
            {
                HttpNotFound();
            }
            return PartialView(kategoriler);
        }

        public ActionResult Ekle()
        {
            var kategori=new tbl_Kategori();
            return View(kategori);
        }
        [HttpPost]
        public ActionResult Ekle(tbl_Kategori kat)
        {
            
            if (ModelState.IsValid)
            {
                uow.GetRepository<tbl_Kategori>().Add(kat);
                uow.SaveChanges();
                return RedirectToAction("Liste");
            }
            return View(kat);
        }

        public void Sil(int id)
        {
            var kategori = uow.GetRepository<tbl_Kategori>().GetById(id);
            if (kategori!=null)
            {
                uow.GetRepository<tbl_Kategori>().Delete(kategori);
                uow.SaveChanges();
            }

        }

        public ActionResult Duzenle(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kategori = uow.GetRepository<tbl_Kategori>().GetById(id);
            if (kategori == null)
            {
                HttpNotFound();
            }
            return View(kategori);
        }
        [HttpPost]
        public ActionResult Duzenle(tbl_Kategori kat)
        {
            if (ModelState.IsValid)
            {
                uow.GetRepository<tbl_Kategori>().Update(kat);
                uow.SaveChanges();
                return RedirectToAction("Ekle");
            }

            return View(kat);
        }
    }
}