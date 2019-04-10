using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }
        EfUnitOfWork uow = new EfUnitOfWork();

        void Session_Start(object sender, EventArgs e)
        {
            uow.GetRepository<Ziyaretci>().Add(new Ziyaretci
            {
                ZiyaretTarihi = DateTime.Now
            });
            uow.SaveChanges();
        }
    }
}
