using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eticaret.Entities;

namespace Eticaret.Areas.Admin.Controllers
{
    public class BaseController:Controller
    {
        public bool IsLogin { get; private set; }
        /// <summary>
        /// Giriş Yapmış kişinin IDsi
        /// </summary>
        public int LoginAdminID { get; private set; }
        /// <summary>
        /// Login User Entity
        /// </summary>
        public tbl_Kullanici LoginAdminEntity { get; private set; }
        protected override void Initialize(RequestContext requestContext)
        {
            //Session["LoginUserID"]
            // Session["LoginUser"]
            if (requestContext.HttpContext.Session["LoginUserID"] != null)
            {
                //Kullnıcı Giriş Yapmış
                IsLogin = true;
                LoginAdminID = (int)requestContext.HttpContext.Session["LoginAdminId"];
                LoginAdminEntity = (tbl_Kullanici)requestContext.HttpContext.Session["LoginAdmin"];
            }
            base.Initialize(requestContext);
        }
    }
}