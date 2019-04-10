using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Eticaret.Entities;
using Eticaret.UnitOfWork;

namespace Eticaret.Models
{
    public class Cart
    {//tüm sepet bilgileri
        private List<Cartline> _cartlines=new List<Cartline>();

        public List<Cartline> CartLines
        {
            get { return _cartlines; }
        }
        EfUnitOfWork uow=new EfUnitOfWork();
        EticaretContext db = new EticaretContext();
        public void AddProduct(tbl_Urun product, int adet)
        {
            var line = _cartlines.FirstOrDefault(i => i.Urun.Urun_Id == product.Urun_Id);
            if (line==null)
            {
                _cartlines.Add(new Cartline
                {
                    Urun = product,
                    Adet = adet
                });
            }
            else
            {
                line.Adet += adet;
                
            }

            uow.GetRepository<tbl_Sepet>().Add(new tbl_Sepet
            {
                Urun_ID = product.Urun_Id,
                Adet = adet
            });
            uow.SaveChanges();

            //var varsa = uow.GetRepository<tbl_Satin_Alma>().Get(i => i.Urun_ID == product.Urun_Id);
            //if (varsa != null)
            //{


            //    tbl_Satin_Alma sa = new tbl_Satin_Alma();
            //    sa.Satis_Id = varsa.Satis_Id;
            //    sa.Adet = varsa.Adet + adet;
            //    sa.Urun_ID = varsa.Urun_ID;
            //    //  db.tbl_Satin_Alma.Attach(sa);

            //  //  db.Entry(sa).State = EntityState.Modified;
            //    db.SaveChanges();
            //    // db.SaveChanges();
            //   uow.GetRepository<tbl_Satin_Alma>().Update(sa);
            //    uow.SaveChanges();
            //}
            //else
            //{

            //  }







        }

        public void UrunSil(tbl_Urun urun)
        {
            _cartlines.RemoveAll(i => i.Urun.Urun_Id == urun.Urun_Id);
        }
        //toplam ücret hesaplama
        public decimal Total()
        {
            return _cartlines.Sum(i => (decimal)i.Urun.Fiyat * i.Adet);
        }

        public void Clear()
        {
            _cartlines.Clear();
        }

            
    }

    public class Cartline
    {//tek bir ürünün ayrıntısı
        public tbl_Urun Urun { get; set; }
        public int Adet { get; set; }

    }
}