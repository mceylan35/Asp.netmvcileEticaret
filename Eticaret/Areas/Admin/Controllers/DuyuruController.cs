using System;
using System.Collections.Generic;
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
    public class DuyuruController : BaseController
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Admin/Duyuru
        public ActionResult Index()
        {

            var duyuru = uow.GetRepository<tbl_Duyuru>().GetById(1);

            if (duyuru == null)
            {
                HttpNotFound();
            }

            return View(duyuru);
        }
        [HttpPost]
        public ActionResult Index(tbl_Duyuru d, HttpPostedFileBase file)
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


                        uow.GetRepository<tbl_Duyuru>().Update(new tbl_Duyuru
                        {
                            Duyuru_Id = d.Duyuru_Id,
                            Link = d.Link,
                            Mesaj = d.Mesaj,
                            ResimURL = filename
                        });

                        uow.SaveChanges();
                        return View(d);
                        ViewBag.DuyuruGuncellendi = "Duyuru güncellendi.";
                    }
                }
            }

            return View(d);
        }
    }
}