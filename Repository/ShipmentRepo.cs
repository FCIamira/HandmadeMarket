namespace HandmadeMarket.Repository
{
    public class ShipmentRepo:GenericRepo<Shipment>,IShipmentRepo
    {
        HandmadeContext context;
        public ShipmentRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }

    }
}
