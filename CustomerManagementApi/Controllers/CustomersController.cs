using CustomerManagementApi.Models;
using CustomerManagementApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerManagementApi.Validators;

namespace CustomerManagementApi.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerService customerService,
            ILogger<CustomersController> logger)
        {
            this._customerService = customerService;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer updatedCustomer)
        {
            if (id != updatedCustomer.Id)
            {
                return BadRequest();
            }

            var validator = new CustomerValidator();
            var validationResult = validator.Validate(updatedCustomer);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _customerService.UpdateCustomer(updatedCustomer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
