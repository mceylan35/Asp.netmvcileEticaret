using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Eticaret.Entities;
using Eticaret.Repository.Abstract;
using Eticaret.Repository.Concrete.EntityFramework;

namespace Eticaret.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private bool disposed = false;
        private DbContextTransaction transaction;
      //  private readonly EticaretContext _context;
        EticaretContext context =new EticaretContext();

        private EfRepository<tbl_Urun> _urunRepository;

        public EfRepository<tbl_Urun> UrunRepository
        {
            get
            {
                if (_urunRepository==null)
                {
                    _urunRepository=new EfRepository<tbl_Urun>(context);
                }

                return _urunRepository;
            }
        }

        private EfRepository<tbl_Marka> _markaRepository;

        public EfRepository<tbl_Marka> MarkaRepository
        {
            get
            {
                if (_markaRepository == null)
                {
                    _markaRepository = new EfRepository<tbl_Marka>(context);
                }

                return _markaRepository;
            }
        }


        public EfUnitOfWork()
        {
            Database.SetInitializer<EticaretContext>(null);
            if (context==null)
            {
                throw new ArgumentNullException("Context null olamaz");
            }
           
        }

     
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
           Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new EfRepository<T>(context);
        }

        public int SaveChanges()
        {
            try
            {
                return context.SaveChanges();
            }
            catch

            {
                throw;
            } 
        }
    }
}