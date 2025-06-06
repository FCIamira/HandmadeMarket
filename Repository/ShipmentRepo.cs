﻿using HandmadeMarket.Data;

namespace HandmadeMarket.Repository
{
    public class ShipmentRepo:GenericRepo<Shipment>,IShipmentRepo
    {
        HandmadeContext context;
        public ShipmentRepo(HandmadeContext context) : base(context)
        {
            this.context = context;
        }
        public  IEnumerable<Shipment> GetAll()
        {
            return context.Shipments
                .Include(s => s.Orders)
                      .ThenInclude(o => o.Order_Items)
                          .ThenInclude(oi => oi.Product)
                           
                          .ToList();
        }

        public  Shipment GetById(int id)
        {
            return context.Shipments
                 .Include(o => o.Customer)
                 .Include(s => s.Orders)
                      .ThenInclude(o => o.Order_Items)
                          .ThenInclude(oi => oi.Product)

                          .FirstOrDefault(s => s.Id == id);
        }
    }
}
