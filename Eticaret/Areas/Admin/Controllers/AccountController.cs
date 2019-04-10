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
    public class AccountController : BaseController
    {
        EfUnitOfWork uow = new EfUnitOfWork();
        // GET: Admin/Account

        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_Kullanici kullanici)
        {
            var admin = uow.GetRepository<tbl_Kullanici>()
                .Get(k => k.Eposta == kullanici.Eposta && k.Sifre == kullanici.Sifre && k.Rol_ID==1);
            if (admin!=null)
            {
                Session["LogonAdmin"] = admin;
                return Redirect("/");
             
            }
            else
            {
                ViewBag.Error = "kullanıcı ve şifre yanlış";
                return View();
            }
           
        }

        public ActionResult LogOut()
        {
            Session["LogonAdmin"] = null;
            return RedirectToAction("Login");
        }

    }
}