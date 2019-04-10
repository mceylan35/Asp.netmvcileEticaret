using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Eticaret.Areas.Admin.Models;
using Eticaret.Entities;
using Eticaret.UnitOfWork;
using Eticaret.ViewModel;
using PagedList;

namespace Eticaret.Controllers
{
    public class AnaSayfaController : Controller
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: AnaSayfa
        public ActionResult Anasayfa(int page = 1, int pageSize = 12)
        {
          
            tbl_Duyuru popup = GetPopup();
            var urunler = uow.GetRepository<tbl_Urun>().GetAll().ToList();
            ViewBag.Duyuru = uow.GetRepository<tbl_Duyuru>().GetById(1);
            
            AnasayfaModelView anasayfamodel=new AnasayfaModelView
            {
                Duyuru = null,
                Urunler = urunler
            };
            PagedList<tbl_Urun> personModel = new PagedList<tbl_Urun>(urunler, page, pageSize);

            return View(personModel);
        }

        public ActionResult EnCokSatilanUrunler(int page=1,int pageSize=12)
        {
            List<tbl_Urun> urunler = new List<tbl_Urun>();
            using (var db = new EticaretContext())
            {
                var satilanurunler = db.Encoksatisyapilanurunler();

                foreach (var u in satilanurunler)
                {
                    var encoksatan = uow.UrunRepository.GetById((int)u.UrunId);
                    urunler.Add(encoksatan);
                }

            }
            PagedList<tbl_Urun> encoksatanurun=new PagedList<tbl_Urun>(urunler,page,pageSize);

            return View("Anasayfa",encoksatanurun);
        }

        public ActionResult IndirimdekiUrunler(int page=1,int pageSize=12)
        {
            var model = uow.GetRepository<tbl_Urun>().GetAll().Where(x => x.Indirim_Oranı != 1).ToList();
            ViewBag.mesaj = "İNDİRİMDEKİ ÜRÜNLERİMİZ";
            var duyuru = uow.GetRepository<tbl_Duyuru>().GetById(1);

            AnasayfaModelView anasayfamodel = new AnasayfaModelView
            {
                Duyuru = null,
                Urunler = model
            };
            PagedList<tbl_Urun> indirimmodel=new PagedList<tbl_Urun>(model,page,pageSize);
            return View("Anasayfa", indirimmodel);
        }

        public tbl_Duyuru GetPopup()
        {
            tbl_Duyuru popup = (tbl_Duyuru)Session["popup"];

            if (popup == null)
            {
                popup = new tbl_Duyuru();
                popup.Duyuru_Id = 1;
                Session["popup"] = popup;
            }
            else
            {
                popup.Duyuru_Id++;
            }

            return popup;
        }
      
      

       
        public ActionResult do_action(string arama,int page=10,int pagesize=10)
        {
            var model = uow.GetRepository<tbl_Yildiz>().GetAll().Where(x => x.tbl_Urun.Urun_Adi.Contains(arama));
            var liste = model.OrderByDescending(x => x.Yildiz).ToList();
            
            return View(liste);
        }
    }
}