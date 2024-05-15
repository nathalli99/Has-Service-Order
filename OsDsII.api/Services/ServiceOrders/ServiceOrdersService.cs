using AutoMapper;
using OsDsII.api.Dtos;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Repository.ServiceOrderRepository;

namespace OsDsII.api.Services.ServiceOrders
{
    public class ServiceOrdersService : IServiceOrdersService
    {
        private readonly IServiceOrderRepository _serviceOrderRepository; // IOC (INVERSION OF CONTROL)
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ServiceOrdersService(
            IServiceOrderRepository serviceOrderRepository,
            ICustomersRepository customersRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _serviceOrderRepository = serviceOrderRepository;
            _customersRepository = customersRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<ServiceOrderDto>> GetAllAsync(int customerId)
        {
            var serviceOrders = await _serviceOrderRepository.GetAllServiceOrderFromCustomer(customerId);
            if (serviceOrders == null)
            {
                throw new NotFoundException("Não existe nenhuma ordem de serviço atrelada ao usuário");
            }
            var serviceOrderDto = _mapper.Map<List<ServiceOrderDto>>(serviceOrders);
            return serviceOrderDto;
        }

        public async Task<NewServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrder)
        {
            if (serviceOrder is null)
            {
                string uriPath = _httpContextAccessor.HttpContext?.Request.Path;
                throw new NotFoundException("Service order cannot be null", uriPath);
            }

            Customer customer = await _customersRepository.GetByIdAsync(serviceOrder.CustomerId);

            if (customer is null)
            {
                string uriPath = _httpContextAccessor.HttpContext?.Request.Path;
                throw new BadRequestException("Não é possível atrelar uma ordem de serviço a um usuário inexistente", uriPath);
            }
            var mappedServiceOrder = _mapper.Map<ServiceOrder>(serviceOrder);
            await _serviceOrderRepository.AddAsync(mappedServiceOrder);

            return _mapper.Map<NewServiceOrderDto>(mappedServiceOrder);
        }

        public async Task<ServiceOrderDto> GetByIdAsync(int serviceOrderId)
        {
            var serviceOrder = await _serviceOrderRepository.GetServiceOrderFromUser(serviceOrderId);
            var serviceOrderDto = _mapper.Map<ServiceOrderDto>(serviceOrder);
            return serviceOrderDto;
        }
    }
}
