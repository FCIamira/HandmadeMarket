using Microsoft.EntityFrameworkCore;

namespace HandmadeMarket.Models
{
    public class ITIContext:DbContext
    {
        DbSet<Shipment> Shipments { get; set; }

    }
}
