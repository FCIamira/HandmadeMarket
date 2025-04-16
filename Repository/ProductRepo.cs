namespace HandmadeMarket.Repository
{
    public class ProductRepo:GenericRepo<Product>, IProductRepo
    {
        HandmadeContext context;
        public ProductRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }
    }
}
