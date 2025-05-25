using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HandmadeMarket.Data
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly HandmadeContext context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(HandmadeContext _context)
        {
            context = _context;
            _dbSet = context.Set<T>();
        }

        public async Task Add(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public  IEnumerable<T> GetAll()
        {
            return  _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return  _dbSet.Find(id);
        }

        public async Task Remove(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        public async Task<bool> RemoveByExpression(Expression<Func<T, bool>> predicate)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public async Task Update(int id, T obj)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity != null)
            {
                context.Entry(existingEntity).CurrentValues.SetValues(obj);
            }
        }

        public IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

       
    }
}
