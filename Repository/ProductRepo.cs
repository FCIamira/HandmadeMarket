namespace HandmadeMarket.Repository
{
    public class ProductRepo:GenericRepo<Product>, IProductIRepo
    {
        ITIContext context;
        public ProductRepo(ITIContext context) : base(context)
        {
            this.context = context;
        }
    }
}
