namespace HandmadeMarket.Interfaces
{
    public interface IRatingRepo : IGenericRepo<Rating>
    {
      public IEnumerable<Rating> GetRateingsByProductId(int productId);
    }
}
