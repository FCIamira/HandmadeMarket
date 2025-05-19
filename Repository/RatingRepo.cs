
namespace HandmadeMarket.Repository
{
    public class RatingRepo : GenericRepo<Rating>, IRatingRepo
    {
        private readonly HandmadeContext context;

        public RatingRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Rating> GetRateingsByProductId(int productId)
        {
            IEnumerable<Rating> ratings =
                 context.Ratings.Where(r => r.ProductId == productId);

            return ratings;
        }
    }
   
}
