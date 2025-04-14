namespace HandmadeMarket.Repository
{
    public class ShipmentRepo:GenericRepo<Shipment>,IShipmentRepo
    {
        ITIContext context;
        public ShipmentRepo(ITIContext context) : base(context)
        {
            this.context = context;
        }

    }
}
