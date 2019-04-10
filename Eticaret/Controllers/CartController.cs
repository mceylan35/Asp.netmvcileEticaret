using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eticaret.Entities;
using Eticaret.Models;
using Eticaret.UnitOfWork;

namespace Eticaret.Controllers
{
    public class CartController : Controller
    {
        EfUnitOfWork uow=new EfUnitOfWork();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }
        public ActionResult AddToCart(int id)
        {
            var product = uow.UrunRepository.GetById(id);
            if (product!=null)
            {
                GetCart().AddProduct(product,1);

            }

          
            return RedirectToAction("Index");
        }

        public Cart GetCart()
        {
            Cart cart =(Cart)Session["Cart"];
            if (cart==null)
            {
                cart=new Cart();
                Session["Cart"] = cart;

            }

            return cart;
        }

        public ActionResult RemoveFromCart(int Urun_Id)
        {
            var urun = uow.UrunRepository.GetById(Urun_Id);
            if (urun!=null)
            {
                GetCart().UrunSil(urun);
            }

            return RedirectToAction("Index");
        }

        public PartialViewResult SepetOzet()
        {
            return PartialView(GetCart());
        }

       
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut()
        {
            var cart = GetCart();
            if (cart.CartLines.Count==0)
            {
                ModelState.AddModelError("UrunYokError","Sepetinizde ürün bulunmamaktador.");
            }
          
                //siparişi veritabanına kayıt et.
                //cartı sıfırla
            //stoktan sil
                SiparisKaydet(cart);

                StokSil(cart);

                cart.Clear();
                return View("SiparisTamamlandi");
            
            
        }

        private void StokSil(Cart cart)
        {
            foreach (var cartline in cart.CartLines)
            {
                // int sayi = cartline.Adet;
                tbl_Urun satilanurun = new tbl_Urun();
                satilanurun = cartline.Urun;
                satilanurun.Stok = satilanurun.Stok - cartline.Adet;
                var urun = uow.UrunRepository.Get(i => i.Urun_Id == satilanurun.Urun_Id);
                urun.Stok = satilanurun.Stok;
                uow.UrunRepository.Update(urun);

                uow.SaveChanges();
            }
        }

        private void SiparisKaydet(Cart cart)
        {
            var girenkullanici = HttpContext.User.Identity.Name;
            var kullanici = uow.GetRepository<tbl_Kullanici>().Get(i => i.Kullanici_Adi == girenkullanici);
           Siparis siparis=new Siparis();
            siparis.SiparisNumarasi="O"+(new Random()).Next(1000000,9999999).ToString();
            siparis.ToplamTutar = cart.Total();
            siparis.KullaniciId = kullanici.Kullanici_Id;
            siparis.SiparisTarihi=DateTime.Now;
            siparis.SiparisDetay=new List<SiparisDetay>();
            foreach (var sd in cart.CartLines)
            {
                SiparisDetay siparisdetay=new SiparisDetay();
                siparisdetay.Adet = sd.Adet;
                siparisdetay.Fiyat = (sd.Urun.Fiyat * (decimal)sd.Adet);
                siparisdetay.UrunId = sd.Urun.Urun_Id;
                siparisdetay.UrunSiparisTarihi=DateTime.Now;
                siparis.SiparisDetay.Add(siparisdetay);
            }

          
           
            uow.GetRepository<Siparis>().Add(siparis);
            uow.SaveChanges();
        }
    }
}