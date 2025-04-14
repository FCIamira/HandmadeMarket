
namespace HandmadeMarket.Context
{
    public class HandmadeContext : DbContext
    {
        DbSet<Shipment> Shipments { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> Items { get; set; }
        DbSet<Seller> Sellers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          //  Composite key for Order_Item
   
           modelBuilder.Entity<OrderItem>()
               .HasKey(oi => new { oi.OrderItemId, oi.OrderId });


               // composite key for Cart
               modelBuilder.Entity<Cart>()
                    .HasKey(c => new { c.CustomerId, c.Id });

            // composite key for Wishlist
            modelBuilder.Entity<Wishlist>()
                    .HasKey(w => new { w.CustomerId, w.Id });

           /// one - to - many relationship between Order and Order_Item
            modelBuilder.Entity<Order>()
              .HasMany(o => o.Order_Items)
              .WithOne(oi => oi.Order);

        }

        public HandmadeContext(DbContextOptions<HandmadeContext> options)
            : base(options)
        {
        }
    }
}
