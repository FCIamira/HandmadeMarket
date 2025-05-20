
using HandmadeMarket.Context;

namespace HandmadeMarket.Repository
{
    public class OrderItemRepo: GenericRepo<OrderItem>, IOrderItemRepo
    {
        HandmadeContext context;
        public OrderItemRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
           IEnumerable<OrderItem> items = context.Items
                .Include(o=>o.Order)
                .Include(p=>p.Product)
                .Where(o=>o.OrderId == orderId)
                .ToList();
            return items;
        }

        public virtual OrderItem GetById(int id)
        {
            OrderItem orderItem = context.Items
                .Include(o => o.Order)
                .Include(p => p.Product)
                .FirstOrDefault(o => o.OrderItemId == id);
            return orderItem;
        }

        public IEnumerable<OrderItem> GetAllBySellerId(string sellerId, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 5;//not fixed
            return context.Items
        .Where(i => i.Product.sellerId == sellerId)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        }
    }
    
}
