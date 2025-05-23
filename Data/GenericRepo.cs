﻿using HandmadeMarket.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace HandmadeMarket.Data
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        HandmadeContext context;
        DbSet<T> _dbSet;
        public GenericRepo(HandmadeContext _context)
        {
            context = _context;
            _dbSet = _context.Set<T>();

        }
        public async Task Add(T obj)
        {
            await context.Set<T>().AddAsync(obj);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public virtual T GetById(int Id)
        {
            return _dbSet.Find(Id);
        }


        public void Remove(int id)
        {

            _dbSet.Remove(_dbSet.Find(id));
        }
        public async Task<bool> RemovebyExpression(Expression<Func<T, bool>> Predicate)
        {
            T? result = await context.Set<T>().FirstOrDefaultAsync(Predicate);
            if (result is not null)
            {
                context.Set<T>().Remove(result);
                return true;
            }
            return false;
        }

        public  void Update(int id, T obj)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(obj);
            }
        }
        public IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> expression)
        {
            return context.Set<T>().Where(expression);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
