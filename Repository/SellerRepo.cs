namespace HandmadeMarket.Repository
{
    public class SellerRepo : GenericRepo<Seller>, ISellerRepo
    {
        ITIContext context;
        public SellerRepo(ITIContext context) : base(context)
        {
            this.context = context;
        }
    }
}
