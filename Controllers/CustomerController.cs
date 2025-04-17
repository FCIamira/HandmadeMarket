using HandmadeMarket.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
 

            private readonly ICustomerRepo customerRepo;
            public CustomerController(ICustomerRepo customerRepo)
            {
                this.customerRepo = customerRepo;
            }
        #region Getall

        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> GetAll()
        {
            var customers = customerRepo.GetAll();
            var dtos = customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            }).ToList();

            return Ok(dtos);
        }
        #endregion


        #region AddCustomer
        [HttpPost]
            public IActionResult AddCustomer([FromBody] CustomerDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = new Customer
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = dto.Password,
                    Phone = dto.Phone,
                    Address = dto.Address
                };

                customerRepo.Add(customer);
                customerRepo.Save();

                var result = new CustomerDTO
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address
                };

                return CreatedAtAction(nameof(GetCustomerById),
                                       new { id = customer.Id },
                                       result);
            }
            #endregion

            // PUT: api/customers/{id}
            #region UpdateCustomer
            [HttpPut("{id:int}")]
            public IActionResult UpdateCustomer(int id, [FromBody] CustomerDTO dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var customer = customerRepo.GetById(id);
                if (customer == null)
                    return NotFound($"Customer with ID {id} not found.");

                customer.FirstName = dto.FirstName;
                customer.LastName = dto.LastName;
                customer.Email = dto.Email;
                customer.Password = dto.Password; 
                customer.Phone = dto.Phone;
                customer.Address = dto.Address;

                customerRepo.Update(id, customer);
                customerRepo.Save();

                var result = new CustomerDTO
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address
                };

                return Ok(result);
            }
        #endregion


        #region GetCustomerById

       
        // GET: api/customers/{id}
        [HttpGet("{id:int}")]
            public ActionResult<CustomerDTO> GetCustomerById(int id)
            {
                var customer = customerRepo.GetById(id);
                if (customer == null)
                    return NotFound($"Customer with ID {id} not found.");

                var dto = new CustomerDTO
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    Phone = customer.Phone,
                    Address = customer.Address
                };
                return Ok(dto);
            }
        #endregion

        #region GetCustomersByName


        [HttpGet("by-name/{name}")]
    public ActionResult<IEnumerable<CustomerDTO>> GetCustomersByName(string name)
    {
        var customers = customerRepo.GetAll()
            .Where(c =>
                c.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                c.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (!customers.Any())
            return NotFound($"No customers found matching '{name}'.");

        var dtos = customers.Select(c => new CustomerDTO
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Phone = c.Phone,
            Address = c.Address
        }).ToList();

        return Ok(dtos);
    }

        #endregion



        #region DeleteCustomer

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = customerRepo.GetById(id);
            if (customer == null)
                return NotFound($"Customer with ID {id} not found.");

            customerRepo.Remove(id);
            customerRepo.Save();

            return NoContent();
        }
        #endregion

      
    }
}


