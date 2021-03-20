using DataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataLibrary.Repositories
{
    public abstract class CRUDRepository<T>  where T : class
    {

        private readonly HotelDbContext _context;
        private readonly DbSet<T> _dbSet;

        public CRUDRepository(HotelDbContext Context, DbSet<T> DBSet)
        {
            this._context = Context;
            this._dbSet = DBSet;
        }

        public void Add(T item)
        {
            _dbSet.Add(item);
            
        }
        public void Remove(T item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        
        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int Count()
        {
            return _dbSet.Count();
        }

         public int Count(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Count(expression);
        }

        public IQueryable Get(Expression<Func<T,bool>> expression)
        {
            return _dbSet.Where(expression).AsQueryable();
        }

        public T GetById(int id)
        {
           return _dbSet.Find(id);
        }


    }
}
