using DataLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataLibrary.Repositories
{
    public abstract class CRUDRepository<T> where T : class
    {

        private readonly HotelDbContext _context;
        private readonly DbSet<T> _dbSet;

        public CRUDRepository(HotelDbContext Context, DbSet<T> DBSet)
        {
            this._context = Context;
            this._dbSet = DBSet;
        }

        public bool Add(T item)
        {
            if (_dbSet.Contains(item))
            {
                return false;
            }
            _dbSet.Add(item);
            _context.SaveChanges();
            return true;

        }
        public bool Remove(T item)
        {
            if (!_dbSet.Contains(item))
            {
                return false;
            }
            _dbSet.Remove(item);
            _context.SaveChanges();
            return true;
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



        public IQueryable<T> Get(Expression<Func<T, bool>> expression)

        {
            return _dbSet.Where(expression).AsQueryable();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            return _dbSet.Find(id);
        }

        public IQueryable<T> OrderBy(Func<T, bool> expression)
        {
            return _dbSet.OrderBy(expression).AsQueryable();
        }



    }
}