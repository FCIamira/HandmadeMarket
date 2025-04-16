using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HandmadeMarket.Models;
using Microsoft.AspNetCore.Identity; // تأكد من وجود هذا الاستخدام لنماذجك

namespace HandmadeMarket.Context
{
    public class HandmadeContext : IdentityDbContext<ApplicationUser>
    {
        public HandmadeContext(DbContextOptions<HandmadeContext> options)
            : base(options)
        {
        }

        // DbSets يجب أن تكون public
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> Items { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // مهم جداً لتفعيل تكوين Identity

            // تكوين جداول Identity الأساسية
            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.HasKey(r => new { r.UserId, r.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            });

            // تكوين الكيانات الخاصة بك
            //modelBuilder.Entity<OrderItem>()
            //    .HasKey(oi => new { oi.OrderItemId, oi.OrderId });

            //modelBuilder.Entity<Cart>()

            //    .HasKey(c => new { c.CustomerId, c.Id });

            //modelBuilder.Entity<Wishlist>()
            //    .HasKey(w => new { w.CustomerId, w.Id });

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Order_Items)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}