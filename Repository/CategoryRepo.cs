namespace HandmadeMarket.Repository
{
    public class CategoryRepo: GenericRepo<Category>, ICategoryRepo
    {
        ITIContext context;
        public CategoryRepo(ITIContext context) : base(context)
        {
            this.context = context;
        }
    }
}
