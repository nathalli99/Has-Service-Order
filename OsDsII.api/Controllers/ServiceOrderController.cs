using Microsoft.AspNetCore.Mvc;
using OsDsII.api.Dtos;
using OsDsII.api.Exceptions;
using OsDsII.api.Http;
using OsDsII.api.Models;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Repository.ServiceOrderRepository;
using OsDsII.api.Services.ServiceOrders;

namespace OsDsII.api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public sealed class ServiceOrdersController : ControllerBase
    {
        private readonly IServiceOrderRepository _serviceOrderRepository; // IOC (INVERSION OF CONTROL)
        private readonly ICustomersRepository _customersRepository;
        private readonly IServiceOrdersService _serviceOrdersService;
        public ServiceOrdersController(
            IServiceOrderRepository serviceOrderRepository,
            ICustomersRepository customersRepository,
            IServiceOrdersService serviceOrdersService
            )
        {
            _serviceOrderRepository = serviceOrderRepository;
            _customersRepository = customersRepository;
            _serviceOrdersService = serviceOrdersService;
        }

        [HttpGet("Customers/{customerId}")]
        public async Task<IActionResult> GetAllServiceOrderAsync(int customerId)
        {
            try
            {
                List<ServiceOrder> serviceOrders = await _serviceOrderRepository.GetAllServiceOrderFromCustomer(customerId);
                if (serviceOrders == null)
                {
                    return NotFound();
                }
                return HttpResponse<List<ServiceOrder>>.Ok(serviceOrders);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceOrderById(int id)
        {
            try
            {
                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
                if (serviceOrder is null)
                {
                    throw new Exception("Service order not found");
                }
                return Ok(serviceOrder);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceOrderAsync(CreateServiceOrderDto serviceOrder)
        {
            try
            {
                var newServiceOrder = await _serviceOrdersService.CreateAsync(serviceOrder);
                return Created("CreateServiceOrderAsync", newServiceOrder);
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }

        }

        [HttpPut("{id}/status/finish")]
        public async Task<IActionResult> FinishServiceOrderAsync(int id)
        {
            try
            {
                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
                if (serviceOrder is null)
                {
                    throw new Exception("Service order cannot be null");
                }

                serviceOrder.FinishOS();
                await _serviceOrderRepository.FinishAsync(serviceOrder);
                return NoContent();

            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }

        [HttpPut("{id}/status/cancel")]
        public async Task<IActionResult> CancelServiceOrder(int id)
        {
            try
            {
                ServiceOrder serviceOrder = await _serviceOrderRepository.GetByIdAsync(id);
                if (serviceOrder is null)
                {
                    throw new Exception("Service order cannot be null");
                }

                serviceOrder.Cancel();
                await _serviceOrderRepository.CancelAsync(serviceOrder);

                return NoContent();
            }
            catch (BaseException ex)
            {
                return ex.GetResponse();
            }
        }
    }
}