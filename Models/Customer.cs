namespace HandmadeMarket.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; } 

        public virtual List<Shipment>? Shipments { get; set; }

        public virtual List<Order_Item>? Orders { get; set; }

        // public virtual List<Card>? Cards { get; set; }

        //public virtual List<Wishlist>? Wishlist { get; set; }

       // public virtual List<Payment>? Payment { get; set; }



    }
}
