using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eticaret.Repository.Abstract
{
    public interface IGenericRepository<T> where T:class
    {
        IQueryable<T> GetAll(); //hepsini getirir.
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate); //linq sorgu yazabilmek için
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);//gelen sınıfa göre silme
        void Delete(int id);//idye göre silme
    }
}
