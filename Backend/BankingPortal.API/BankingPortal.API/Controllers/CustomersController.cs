using BankingPortal.API.Models;
using BankingPortal.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomersController (ICustomerService customerService)
        {
            _customerService = customerService;
        }
        //Get : api/customers
        /// <summary>
        /// Retrieves all customers.
        /// </summary>
        /// <returns>
        /// Returns a list of customers.
        /// </returns>
        /// <response code="200">
        /// Successfully retrieved customers.
        /// </response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public ActionResult <List<Customers>> GetCustomers()
        {
            var customer = _customerService.GetCustomers();
            return Ok (customer);
        }
        //Post : api/Customers
        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="customer">
        /// Customer details to create.
        /// </param>
        /// <returns>
        /// Returns the newly created customer.
        /// </returns>
        /// <response code="200">
        /// Customer created successfully.
        /// </response>
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public ActionResult AddCustomer(Customers customer)
        {
            var result = _customerService.AddCustomer(customer);
            return Ok(result);

        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customers customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var result = _customerService.UpdateCustomer(customer);

            if (!result)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // Delete : api/Customers/{id}
        /// <summary>
        /// Deletes a customer. Admin only.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var result = _customerService.DeleteCustomer(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
