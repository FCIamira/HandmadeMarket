using HandmadeMarket.DTO;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentRepo shipmentRepo;

        public ShipmentController(IShipmentRepo shipmentRepo) 
        {
            this.shipmentRepo = shipmentRepo;
        }
        #region GetAll
        [HttpGet]
        public ActionResult GetAllShipment()
        {
            var shipments = shipmentRepo.GetAll();

            if (shipments.Any())
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
                CustomerId = s.CustomerId
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

            ShipmentDTO shipmentDTOs = new ShipmentDTO
            {
                Id = shipment.Id,
                ShipmentDate = shipment.ShipmentDate,
                Address = shipment.Address,
                City = shipment.City,
                State = shipment.State,
                ZipCode = shipment.ZipCode,
                Country = shipment.Country,
                CustomerId = shipment.CustomerId
            };

            return Ok(shipmentDTOs);
        }

        #endregion

        //#region AddShipment
        //[HttpPost]
        //public IActionResult AddShipment([FromBody] ShipmentDTO shipmentDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    Shipment shipment = new Shipment
        //    {
        //        ShipmentDate = shipmentDto.ShipmentDate,
        //        Address = shipmentDto.Address,
        //        City = shipmentDto.City,
        //        State = shipmentDto.State,
        //        ZipCode = shipmentDto.ZipCode,
        //        Country = shipmentDto.Country,
        //        CustomerId = shipmentDto.CustomerId
        //    };

        //    shipmentRepo.Add(shipment);
        //    shipmentRepo.Save();

        //    shipmentDto.Id = shipment.Id;

        //    return CreatedAtAction("GetShipmentByID", new { id = shipment.Id }, shipmentDto);
        //}

        //#endregion

        ////#region UdateShipment    
        //[HttpPut("{id}")]
        //public IActionResult UpdateShipment(int id, [FromBody] ShipmentDTO shipmentDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var shipmentFromDb = shipmentRepo.GetById(id);
        //    if (shipmentFromDb == null)
        //    {
        //        return NotFound($"Shipment with ID {id} not found.");
        //    }

        //    shipmentFromDb.ShipmentDate = shipmentDto.ShipmentDate;
        //    shipmentFromDb.Address = shipmentDto.Address;
        //    shipmentFromDb.City = shipmentDto.City;
        //    shipmentFromDb.State = shipmentDto.State;
        //    shipmentFromDb.ZipCode = shipmentDto.ZipCode;
        //    shipmentFromDb.Country = shipmentDto.Country;
        //    shipmentFromDb.CustomerId = shipmentDto.CustomerId;

        //    shipmentRepo.Update(id, shipmentFromDb);
        //    shipmentRepo.Save();

        //    return Ok(shipmentFromDb);
        //}


        //#endregion




    }
}
