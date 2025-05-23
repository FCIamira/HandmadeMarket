using HandmadeMarket.Enum;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.UnitOfWorks;

namespace HandmadeMarket.Services
{
    public class ShipmentServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ShipmentServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region GetAll
        public Result<IEnumerable<ShipmentDTO>> GetAll()
        {
            var shipments = unitOfWork.Shipment.GetAll();
            if (shipments == null || !shipments.Any())
                return Result<IEnumerable<ShipmentDTO>>.Failure(ErrorCode.NotFound,"No shipment found");

            var shipmentDTOs = shipments.Select(s => new ShipmentDTO
            {
                Id = s.Id,
                ShipmentDate = s.ShipmentDate,
                Address = s.Address,
                City = s.City,
                State = s.State,
                ZipCode = s.ZipCode,
                Country = s.Country,
                CustomerId = s.CustomerId,
                Orders = s.Orders?.Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    Order_Date = o.Order_Date,
                    Total_Price = o.Total_Price,
                    Order_Items = o.Order_Items?.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList()
            }).ToList();

            return Result<IEnumerable<ShipmentDTO>>.Success(shipmentDTOs);
        }

        #endregion


        #region GetById
        public Result<ShipmentDTO> GetById(int id)
        {
            var shipment = unitOfWork.Shipment.GetById(id);
            if (shipment == null)
                return Result<ShipmentDTO>.Failure(ErrorCode.NotFound,$"No shipment found with ID {id}");

            var shipmentDTO = new ShipmentDTO
            {
                Id = shipment.Id,
                ShipmentDate = shipment.ShipmentDate,
                Address = shipment.Address,
                City = shipment.City,
                State = shipment.State,
                ZipCode = shipment.ZipCode,
                Country = shipment.Country,
                CustomerId = shipment.CustomerId,
                Orders = shipment.Orders?.Select(o => new OrderDTO
                {
                    OrderId = o.OrderId,
                    Order_Date = o.Order_Date,
                    Total_Price = o.Total_Price,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer?.FirstName,
                    Order_Items = o.Order_Items?.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity
                    }).ToList()
                }).ToList()
            };

            return Result<ShipmentDTO>.Success(shipmentDTO);
        }

        #endregion


        #region Add
        public Result<ShipmentDTO> Add(ShipmentDTO dto)
        {
            var shipment = new Shipment
            {
                ShipmentDate = dto.ShipmentDate,
                Address = dto.Address,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode,
                Country = dto.Country,
                CustomerId = dto.CustomerId
            };

            unitOfWork.Shipment.Add(shipment);
            unitOfWork.SaveChangesAsync();  

            dto.Id = shipment.Id;
            return Result<ShipmentDTO>.Success(dto);
        }

        #endregion

        #region Update
        public Result<ShipmentDTO> Update(int id, ShipmentDTO dto)
        {
            var shipment = unitOfWork.Shipment.GetById(id);
            if (shipment == null)
                return Result<ShipmentDTO>.Failure(ErrorCode.NotFound,$"Shipment with ID {id} not found");

            shipment.ShipmentDate = dto.ShipmentDate;
            shipment.Address = dto.Address;
            shipment.City = dto.City;
            shipment.State = dto.State;
            shipment.ZipCode = dto.ZipCode;
            shipment.Country = dto.Country;
            shipment.CustomerId = dto.CustomerId;

            unitOfWork.Shipment.Update(id, shipment);
            unitOfWork.SaveChangesAsync();

            return Result<ShipmentDTO>.Success(dto);
        }

        #endregion

        #region Delete
        public Result<string> Delete(int id)
        {
            var shipment = unitOfWork.Shipment.GetById(id);
            if (shipment == null)
                return Result<string>.Failure(ErrorCode.NotFound,$"Shipment with ID {id} not found.");

            unitOfWork.Shipment.Remove(id);
            unitOfWork.SaveChangesAsync();

            return Result<string>.Success("Shipment deleted successfully.");
        } 
        #endregion
    }
}
