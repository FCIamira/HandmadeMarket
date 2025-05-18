namespace HandmadeMarket.Repository
{
    public class RatingRepo : GenericRepo<Rating>, IRatingRepo
    {
        private readonly HandmadeContext context;

        public RatingRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }
      
    }
   
}
