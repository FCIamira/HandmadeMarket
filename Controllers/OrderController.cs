using HandmadeMarket.DTO;
using HandmadeMarket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices orderServices;

        public OrderController(OrderServices orderServices)
        {
            this.orderServices = orderServices;
        }

        #region Get All
        [HttpGet]
        public IActionResult GetAll()
        {
            Result<List<FlatOrder_OrderItems>> result = orderServices.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Get By Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Result<FlatOrder_OrderItems> result = orderServices.GetById(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Create Order
        [HttpPost]
        public IActionResult CreateOrder(AddOrderDTO orderDto)
        {

            Result<string> result = orderServices.CreateOrder(orderDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region Update Order

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, AddOrderDTO orderDto)
        {
            Result<string> result = orderServices.UpdateOrder(id,orderDto);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        #endregion

        #region Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            Result<string> result = orderServices.DeleteOrder(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion



    }
}
