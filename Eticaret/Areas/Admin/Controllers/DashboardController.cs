using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Eticaret.Areas.Admin.Models;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Areas.Admin.Controllers
{  
    [Authorize]
    public class DashboardController : BaseController
    {
        EfUnitOfWork uow=new EfUnitOfWork();

        // GET: Admin/Dashboard

        public ActionResult Index()
        {
            DashboardViewModel dashboard=new DashboardViewModel();
            using (var db = new EticaretContext())
            {

                var toplamsatis = db.ToplamUrunSatis();
                foreach (var toplam in toplamsatis)
                {
                    dashboard.ToplamUrunSatisSayisi = (int)toplam;
                }

                var toplamuye = db.ToplamUyeKaydi();
                foreach (var uye in toplamuye)
                {
                    dashboard.ToplamUyeKayitSayisi = (int)uye;
                }

                var toplamziyaret = db.ToplamZiyaretSayisi();
                foreach (var ziyaret in toplamziyaret)
                {
                    dashboard.ToplamZiyaretciSayisi = (int)ziyaret;
                }
            }
            return View(dashboard);
        }

        public ActionResult HaftalikZiyaretciSayisi()
        {
            List<HaftalikZiyaretci> ziyaretci = new List<HaftalikZiyaretci>();

            using (var db = new EticaretContext())
            {
                var haftaliziyaretcisayisi = db.HaftalikZiyaretci();
                foreach (var result in haftaliziyaretcisayisi)
                {
                    ziyaretci.Add(new HaftalikZiyaretci
                    {
                        hafta = Convert.ToInt32(result.hafta),
                        HaftaZiyaretci = (int)result.HaftalikZiyaretci
                    });
                }
            }

            Chart chart = new Chart(500, 400);
            chart.AddTitle("Haftalik ziyaretci sayisi");
            chart.AddLegend("Ziyaretçiler");
           // chart.DataBindTable(dataSource: ziyaretci, xField: "ZiyaretTarihi");
             chart.DataBindCrossTable(dataSource: ziyaretci, groupByField: "hafta", xField: "hafta", yFields: "HaftaZiyaretci");
            return View(chart);
        }

        public ActionResult HaftalikSiparisSayisi()
        {
            List<HaftalikSiparis> siparis = new List<HaftalikSiparis>();

            using (var db = new EticaretContext())
            {
                var haftaliksiparissayisi = db.HaftalikSiparis();
                foreach (var result in haftaliksiparissayisi)
                {
                    siparis.Add(new HaftalikSiparis()
                    {
                        hafta = Convert.ToInt32(result.hafta),
                        HaftaSiparis = (int)result.HaftalikSiparis
                    });
                }
            }

            Chart chart = new Chart(500, 400);
            chart.AddTitle("Haftalik siparis sayisi");
            chart.AddLegend("Siparişler");
            // chart.DataBindTable(dataSource: ziyaretci, xField: "ZiyaretTarihi");
            chart.DataBindCrossTable(dataSource: siparis, groupByField: "hafta", xField: "hafta", yFields: "HaftaSiparis");
                return View(chart);
        }

        public ActionResult HaftalikUyeSayisi()
        {
            List<HaftalikUye> uye = new List<HaftalikUye>();

            using (var db = new EticaretContext())
            {
                var haftalikuyesayisi = db.HaftalikUye();
                foreach (var result in haftalikuyesayisi)
                {
                    uye.Add(new HaftalikUye
                    {
                        hafta = Convert.ToInt32(result.hafta),
                        HaftaUye = (int)result.HaftalikKullanici
                    });
                }
            }

            Chart chart = new Chart(500, 400);
            chart.AddTitle("Haftalik uye kayıt sayisi");
            chart.AddLegend("Uyeler");
            // chart.DataBindTable(dataSource: ziyaretci, xField: "ZiyaretTarihi");
            chart.DataBindCrossTable(dataSource: uye, groupByField: "hafta", xField: "hafta", yFields: "HaftaUye");
            return View(chart);
        }

        public ActionResult GunlukZiyaretciSayisi()
        {
            List<GunlukZiyaretciSayisi> ziyaretciler = new List<GunlukZiyaretciSayisi>();

            using (var db = new EticaretContext())
            {
                var gunlukziyaretsayisi = db.GunlukZiyaretciSayisi();
                foreach (var result in gunlukziyaretsayisi)
                {
                 
                    ziyaretciler.Add(new GunlukZiyaretciSayisi
                    {
                        ZiyaretSayisi =(int)result.ziyaretsayi,
                        ZiyaretTarihi =(DateTime)result.ZiyaretTarihi
                    });
                }
            }

            Chart chart = new Chart(500, 400);
            chart.AddTitle("Günlük Ziyaret Sayısı");
            chart.AddLegend("Ziyaret");
            chart.DataBindTable(dataSource: ziyaretciler, xField: "ZiyaretTarihi");
            // chart.DataBindCrossTable(dataSource: kategoriler,groupByField:"",xField: "KategoriAdi", yFields: "Adet");
            return View(chart);
        }

        public ActionResult EnCokSatisYapanKategori()
        {
            List<EnCokSatisYapanKategori> kategoriler=new List<EnCokSatisYapanKategori>();

            using (var db=new EticaretContext())
            {
                var encoksatisikategori = db.EnCokSatisYapanKategori();
                foreach (var result in encoksatisikategori)
                {
                    kategoriler.Add(new EnCokSatisYapanKategori
                    {
                        KategoriAdi = result.Kategori,
                        Adet = (int)result.adet
                    });
                }
            }

            Chart chart=new Chart(500,400);
            chart.AddTitle("En Çok Satış Yapan Kategoriler");
            chart.AddLegend("Kategoriler");
         chart.DataBindTable(dataSource: kategoriler, xField: "KategoriAdi");
           // chart.DataBindCrossTable(dataSource: kategoriler,groupByField:"",xField: "KategoriAdi", yFields: "Adet");
            return View(chart);
        }


        public ActionResult GunlukUrunSiparisSayisi()
        {
            List<GunlukUrunSiparisSayisi> gunsiparis = new List<GunlukUrunSiparisSayisi>();
            using (var db = new EticaretContext())
            {
                var gunluksiparis = db.GunlukUrunSiparisSayisi();


                foreach (var u in gunluksiparis)
                {
                    gunsiparis.Add(new GunlukUrunSiparisSayisi
                    {
                        UrunSiparisTarihi =(DateTime) u.UrunSiparisTarihi,
                        Adet = (int)u.adet
                        
                    });
                }

            }


            Chart chart = new Chart(500, 400);
            chart.AddTitle("Günlük Siparişler");
            chart.AddLegend("Ürünler");

              chart.DataBindTable(dataSource: gunsiparis, xField: "UrunSiparisTarihi");
          //  chart.DataBindCrossTable(dataSource: gunsiparis, groupByField: "Adet", xField: "UrunSiparisTarihi", yFields: "Adet");



            return View(chart);
        }



        public ActionResult EnCokSepeteEklenenler()
        {
            List<EnCokSepeteEklenenler> urunler = new List<EnCokSepeteEklenenler>();
            using (var db = new EticaretContext())
            {
                var sepeturunler = db.EnCokSepeteEklenenler();
                

                foreach (var u in sepeturunler)
                {
                    urunler.Add(new EnCokSepeteEklenenler
                    {
                        UrunAdi = u.Urun_Adi,
                        Adet = (int)u.adet,
                        UrunId = (int)u.Urun_ID
                    });
                }

            }


            Chart chart = new Chart(500, 400);
            chart.AddTitle("En Çok Sepete Eklenen 5 Ürün");
            chart.AddLegend("Ürünler");

         // chart.DataBindTable(dataSource: urunler, xField: "UrunAdi");
        chart.DataBindCrossTable(dataSource: urunler, groupByField: "UrunAdi", xField: "UrunAdi", yFields: "Adet");
            


            return View(chart);
        }

        public ActionResult GunlukKayitSayisi()
        {
            
            List<GunlukKayitSayisi> gunlukkayit = new List<GunlukKayitSayisi>();
            using (var db = new EticaretContext())
            {
                var kayit = db.GunlukKayitSayisi();


                foreach (var u in kayit)
                {
                    gunlukkayit.Add(new GunlukKayitSayisi
                    {
                       
                        Adet = (int)u.adet,
                        UyeKayitTarihi = (DateTime)u.UyeKayitTarihi

                    });
                }

            }


            Chart chart = new Chart(500, 400);
            chart.AddTitle("Günlük Kayıt");
            chart.AddLegend("Kullanıcılar");

            chart.DataBindTable(dataSource: gunlukkayit, xField: "UyeKayitTarihi");
            //  chart.DataBindCrossTable(dataSource: gunsiparis, groupByField: "Adet", xField: "UrunSiparisTarihi", yFields: "Adet");



            return View(chart);

        }

      

        public ActionResult EnCokSatisYapilanBesUrun()
        {

            List<EnCokSatilanUrunler> urunler = new List<EnCokSatilanUrunler>();
            using (var db = new EticaretContext())
            {
                var satilanurunler = db.Encoksatisyapilanurunler();

                foreach (var u in satilanurunler)
                {
                    urunler.Add(new EnCokSatilanUrunler
                    {
                        UrunAdi = u.Urun_Adi,
                        Adet = (int)u.adet,
                        UrunId = (int)u.UrunId
                    });
                }

            }


            Chart chart = new Chart(500, 400);
            chart.AddTitle("En Çok Satılan 5 Ürün");
            chart.AddLegend("Ürünler");
           
            chart.DataBindTable(dataSource: urunler,xField: "UrunAdi");
            chart.DataBindCrossTable(dataSource: urunler, groupByField: "UrunAdi", xField: "UrunAdi", yFields: "Adet");



            return View(chart);
        }
    }
}
//chart.AddSeries(name: "Şatış Miktarı", chartType: "Column",
//xValue: new[] { 20, 40, 60 },
//yValues: new[] { 800, 1200, 2300 });
//chart.AddSeries(name: "Bilsayar B", chartType: "Column",
//xValue: new[] { 20, 40, 60 },
//yValues: new[] { 900, 1600, 3000 });