using HandmadeMarket.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HandmadeMarket.Repository
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
        public void Add(T obj)
        {
            _dbSet.Add(obj);
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

        public void Update(int id,T obj)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(obj);
            }
        }
        public void Save()
        {
           context.SaveChanges();
        }
    }
}
