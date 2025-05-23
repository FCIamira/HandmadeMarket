using HandmadeMarket.Helpers;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Mvc;

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
            return result.ToActionResult();
        }
        #endregion

        #region GetByID
        [HttpGet("{id:int}")]
        public IActionResult GetShipmentByID(int id)
        {
            var result = _shipmentServices.GetById(id);
            return result.ToActionResult();
        }
        #endregion

        #region AddShipment
        [HttpPost]
        public IActionResult AddShipment([FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _shipmentServices.Add(shipmentDto);
            if (result.IsSuccess)
                return CreatedAtAction(nameof(GetShipmentByID), new { id = result.Data.Id }, result.Data);

            return result.ToActionResult(); 
        }
        #endregion

        #region UpdateShipment    
        [HttpPut("{id:int}")]
        public IActionResult UpdateShipment(int id, [FromBody] ShipmentDTO shipmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _shipmentServices.Update(id, shipmentDto);
            return result.ToActionResult();
        }
        #endregion

        #region DeleteShipment
        [HttpDelete("{id:int}")]
        public IActionResult DeleteShipment(int id)
        {
            var result = _shipmentServices.Delete(id);
            return result.ToActionResult();
        }
        #endregion
    }
}
