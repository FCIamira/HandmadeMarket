namespace HandmadeMarket.Interfaces
{
    public interface IGenericRepo<T>
    {
        public IEnumerable<T> GetAll();
       public T GetById(int Id);
        public void Add(T obj);
        public void Remove(int id);
        public void Update(int id ,T obj);
        public void Save();
    }
}
