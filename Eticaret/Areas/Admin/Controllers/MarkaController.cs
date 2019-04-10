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
    public class MarkaController : BaseController
    {

        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Admin/Marka
        public ActionResult Liste()
        {
            var model = uow.MarkaRepository.GetAll().ToList();
            if (model == null)
            {
                HttpNotFound();
            }

            return View(model);
        }

        public ActionResult Ekle()
        {
            var model = new tbl_Urun();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Ekle(tbl_Marka marka)
        {
            uow.MarkaRepository.Add(new tbl_Marka
            {
                Marka_Adi = marka.Marka_Adi
            });
            uow.SaveChanges();
            return RedirectToAction("Liste");
        }

        public void Sil(int id)
        {
            tbl_Urun urunvarMi = new tbl_Urun();
            var marka = uow.MarkaRepository.GetById(id);
            if (marka != null)
            {
                uow.MarkaRepository.Delete(marka);
                uow.SaveChanges();
            }

        }


        public ActionResult Duzenle(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var marka = uow.MarkaRepository.GetById(id);
            if (marka == null)
            {
                HttpNotFound();
            }
            return View(marka);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Duzenle(tbl_Marka marka)
        {
            if (ModelState.IsValid)
            {
                uow.MarkaRepository.Update(marka);
                uow.SaveChanges();
                return RedirectToAction("Liste");
            }
            return View(marka);
        }
    }
}