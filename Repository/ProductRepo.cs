namespace HandmadeMarket.Repository
{
    public class ProductRepo:GenericRepo<Product>, IProductIRepo
    {
        HandmadeContext context;
        public ProductRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }
    }
}
