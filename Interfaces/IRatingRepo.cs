namespace HandmadeMarket.Interfaces
{
    public interface IRatingRepo : IGenericRepo<Rating>
    {
       IEnumerable<Rating> GetRateingsByProductId(int productId);
    }
}
