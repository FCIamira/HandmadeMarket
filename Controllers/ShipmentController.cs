using HandmadeMarket.DTO;
using HandmadeMarket.Interfaces;
using HandmadeMarket.Models;
using HandmadeMarket.Repository;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentServices _shipmentServices;

        public ShipmentController(ShipmentServices shipmentServices)
        {
            _shipmentServices = shipmentServices;
        }

    
    #region GetAll
    [HttpGet]
        public IActionResult GetAllShipment()
        {
            var result = _shipmentServices.GetAll();
            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Error);
        }

        #endregion


        #region GetByID

        [HttpGet("{id:int}")]
        public IActionResult GetShipmentByID(int id)
        {
            var result = _shipmentServices.GetById(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Error);
        }


        #endregion


        #region AddShipment
        [HttpPost]
        public IActionResult AddShipment([FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _shipmentServices.Add(shipmentDto);
            return CreatedAtAction(nameof(GetShipmentByID), new { id = result.Data.Id }, result.Data);
        }

        #endregion


        #region UdateShipment    
        [HttpPut("{id:int}")]
        public IActionResult UpdateShipment(int id, [FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _shipmentServices.Update(id, shipmentDto);
            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Error);
        }


        #endregion


        #region DeleteShipment
        [HttpDelete("{id:int}")]
        public IActionResult DeleteShipment(int id)
        {
            var result = _shipmentServices.Delete(id);
            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Error);
        }

        #endregion



    }
}
