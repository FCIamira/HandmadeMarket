using Microsoft.EntityFrameworkCore;

namespace HandmadeMarket.Models
{
    public class ITIContext:DbContext
    {

        public ITIContext() { }
        public ITIContext(DbContextOptions<ITIContext> options) : base(options) { }
        DbSet<Shipment> Shipments { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> Items { get; set; }
        DbSet<Seller> Sellers { get; set; }

       
    }
}
