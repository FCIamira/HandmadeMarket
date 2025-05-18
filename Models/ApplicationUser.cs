using Microsoft.AspNetCore.Identity;

namespace HandmadeMarket.Models
{
    public class ApplicationUser: IdentityUser
    {
        public Seller? Seller { get; set; }
        public Customer? Customer { get; set; }
    }
}
