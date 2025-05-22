using System.Linq.Expressions;

namespace HandmadeMarket.Interfaces
{
    public interface IGenericRepo<T>
    {
        public Task Add(T obj);
        public Task<bool> RemovebyExpression(Expression<Func<T, bool>> Predicate);
        public IQueryable<T> GetAllWithFilter(Expression<Func<T, bool>> expression);
        public IEnumerable<T> GetAll();
        public T GetById(int Id);
        public void Remove(int id);
        public void Update(int id ,T obj);
        public void Save();
    }
}
