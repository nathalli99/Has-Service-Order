using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OsDsII.api.Dtos;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Services.Customers;
using OsDsII.api.Http;

namespace OsDsII.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        //private readonly DataContext _dataContext;
        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersRepository customersRepository, ICustomersService customersService)
        {
            _customersRepository = customersRepository;
            _customersService = customersService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                IEnumerable<Customer> customers = await _customersRepository.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                CustomerDto customer = await _customersService.GetByIdAsync(id);
                return HttpResponse<CustomerDto>.Ok(customer);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type= typeof(ConflictException))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomerAsync(CreateCustomerDto customer)
        {
            try
            {
                await _customersService.CreateAsync(customer); // assíncrono porém void

                return Created(nameof(CustomersController), customer);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            try
            {
                Customer customer = await _customersRepository.GetByIdAsync(id);
                if (customer is null)
                {
                    return NotFound("Customer not found");
                }
                await _customersRepository.DeleteCustomer(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                Customer currentCustomer = await _customersRepository.GetByIdAsync(customer.Id);
                if (customer is null)
                {
                    return NotFound("Customer not found");
                }
                await _customersRepository.UpdateCustomerAsync(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}