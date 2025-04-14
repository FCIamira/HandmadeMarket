
namespace HandmadeMarket.Context
{
    public class HandmadeContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for Order_Item
            modelBuilder.Entity<Order_Item>()
                .HasKey(oi => new { oi.OrderItemId, oi.OrderId });

            // one-to-many relationship between Order and Order_Item
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
