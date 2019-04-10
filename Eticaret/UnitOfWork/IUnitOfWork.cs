using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eticaret.Repository.Abstract;

namespace Eticaret.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}