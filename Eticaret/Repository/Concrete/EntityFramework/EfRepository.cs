using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Eticaret.Entities;
using Eticaret.Repository.Abstract;

namespace Eticaret.Repository.Concrete.EntityFramework
{
    public class EfRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EticaretContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(EticaretContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDelete")!=null)
            {
                T _entity = entity;
                _entity.GetType().GetProperty("IsDelete").SetValue(_entity,true);
                this.Update(_entity);
            }
            else
            {
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State!=EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
            
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity==null)
            {
                return;
            }
            else
            {
                if (entity.GetType().GetProperty("IsDelete")!=null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);
                    this.Update(_entity);
                    
                }
                else
                {
                    Delete(entity);
                }
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}