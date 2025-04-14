namespace HandmadeMarket.Repository
{
    public class CategoryRepo: GenericRepo<Category>, ICategoryRepo
    {
        private readonly HandmadeContext handmadeContext;

        public CategoryRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }
    }
}
