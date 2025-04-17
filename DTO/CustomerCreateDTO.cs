namespace HandmadeMarket.DTO
{
    public class CustomerCreateDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual List<Shipment>? Shipments { get; set; }

        public virtual List<Order>? Orders { get; set; }

        public virtual List<Cart>? Carts { get; set; }

        public virtual List<Wishlist>? Wishlist { get; set; }
    }
}
