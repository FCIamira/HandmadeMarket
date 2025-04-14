namespace HandmadeMarket.Repository
{
    public class SellerRepo : GenericRepo<Seller>, ISellerRepo
    {
        HandmadeContext context;
        public SellerRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }
    }
}
