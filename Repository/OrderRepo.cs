namespace HandmadeMarket.Repository
{
    public class OrderRepo : GenericRepo<Order>, IOrderRepo
    {
        private readonly HandmadeContext handmadeContext;
        public OrderRepo(HandmadeContext handmadeContext) : base(handmadeContext)
        {
            this.handmadeContext = handmadeContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return handmadeContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Order_Items)
                .ThenInclude(oi => oi.Product)
                .ToList();
        }
        public Order GetById(int id)
        {
            return handmadeContext.Orders
                 .Include(o => o.Customer)
                .Include(o => o.Order_Items)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefault(o => o.OrderId == id);
        }

        public void Update(Order order)
        {
            
            handmadeContext.Orders.Update(order);
            
        }
        public void RemoveOrder(int id)
        {
            var order = handmadeContext.Orders.FirstOrDefault(o=>o.OrderId == id);
            if (order != null)
            {
                handmadeContext.Orders.Remove(order);
            }
        }
        public void Save()
        {
            handmadeContext.SaveChanges();
        }

        public decimal CalcTotalPrice(decimal price, int quantity)
        {
            return price * quantity;
        }
    }
    
}
