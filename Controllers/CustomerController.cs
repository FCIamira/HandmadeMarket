using HandmadeMarket.DTO.CustomerDTOs;
using HandmadeMarket.Repository;
using HandmadeMarket.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
 

            private readonly IUnitOfWork unitOfWork;
            public CustomerController(IUnitOfWork unitOfWork)
            {
                this.unitOfWork = unitOfWork;
            }
        #region Getall

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
        {
            var customers = unitOfWork.Customer.GetAll();
            var dtos = customers.Select(c => new CustomerDTO
            {
                Id = c.UserId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            }).ToList();

            return Ok(dtos);
        }
        #endregion




      
    }
}


