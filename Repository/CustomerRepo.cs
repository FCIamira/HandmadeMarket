namespace HandmadeMarket.Repository
{
    public class CustomerRepo:GenericRepo<Customer>, ICustomerRepo
    {
        private readonly HandmadeContext handmadeContext;

        public CustomerRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }

        

    }
}
