using HandmadeMarket.DTO;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepo shipmentRepo;
        private readonly ICustomerRepo customerRepo;
        private readonly IOrderRepo orderRepo;

        public ShipmentController(IShipmentRepo shipmentRepo, ICustomerRepo customerRepo,IOrderRepo orderRepo)
        {
            this.shipmentRepo = shipmentRepo;
            this.customerRepo = customerRepo;
            this.orderRepo= orderRepo;
        }
        #region GetAll
        [HttpGet]
        public ActionResult GetAllShipment()
        {
            var shipments = shipmentRepo.GetAll();

            if (!shipments.Any())
            {
                return NotFound("No shipment found ");
            }
           
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
                        Quantity = oi.Quantity,
                        
                    }).ToList()
                }).ToList()
            }).ToList();
       
            return Ok(shipmentDTOs);
        }

        #endregion


        #region GetByID

        [HttpGet("{id:int}")]
        public ActionResult GetShipmentByID(int id)
        {
            Shipment shipment = shipmentRepo.GetById(id);

            if (shipment == null)
            {
                return NotFound($"No shipment found with ID {id}");
            }

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
                    CustomerId= o.CustomerId,
                    CustomerName=o.Customer.FirstName,
                    Order_Items = o.Order_Items?.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,

                    }).ToList()
                }).ToList()
            };

            return Ok(shipmentDTO);
        }

        #endregion


        #region AddShipment
        [HttpPost]
        public IActionResult AddShipment([FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            Shipment shipment = new Shipment
            {
                ShipmentDate = shipmentDto.ShipmentDate,
                Address = shipmentDto.Address,
                City = shipmentDto.City,
                State = shipmentDto.State,
                ZipCode = shipmentDto.ZipCode,
                Country = shipmentDto.Country,
                CustomerId = shipmentDto.CustomerId,
            };

            shipmentRepo.Add(shipment);
            shipmentRepo.Save();


            shipmentDto.Id = shipment.Id;
           

            return CreatedAtAction("GetShipmentByID", new { id = shipment.Id }, shipmentDto);
        }
        #endregion

      
        #region UdateShipment    
        [HttpPut("{id:int}")]
        public IActionResult UpdateShipment(int id, [FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipmentFromDb = shipmentRepo.GetById(id);
            if (shipmentFromDb == null)
            {
                return NotFound($"Shipment with ID {id} not found.");
            }

            shipmentFromDb.ShipmentDate = shipmentDto.ShipmentDate;
            shipmentFromDb.Address = shipmentDto.Address;
            shipmentFromDb.City = shipmentDto.City;
            shipmentFromDb.State = shipmentDto.State;
            shipmentFromDb.ZipCode = shipmentDto.ZipCode;
            shipmentFromDb.Country = shipmentDto.Country;
            shipmentFromDb.CustomerId = shipmentDto.CustomerId;


            shipmentRepo.Update(id, shipmentFromDb);
            shipmentRepo.Save();

            return Ok(shipmentFromDb);
        }


        #endregion


        #region DeleteShipment
        [HttpDelete("{id:int}")]
        public IActionResult DeleteShipment(int id)
        {
            var shipment = shipmentRepo.GetById(id);
            if (shipment == null)
            {
                return NotFound($"Shipment with ID {id} not found.");
            }

            shipmentRepo.Remove(id);
            shipmentRepo.Save();

            return NoContent();
        }
        #endregion



    }
}
