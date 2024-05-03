using AutoMapper;
using OsDsII.api.Dtos;
using OsDsII.api.Exceptions;
using OsDsII.api.Models;
using OsDsII.api.Repository.CustomersRepository;

namespace OsDsII.api.Services.Customers
{
    public sealed class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomersService(ICustomersRepository customersRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomerDto> GetByIdAsync(int id)
        {
            Customer customer = await _customersRepository.GetByIdAsync(id);
            if (customer is null)
            {
                string uriPath = _httpContextAccessor.HttpContext?.Request.Path;
                throw new NotFoundException("Usuário não encontrado", uriPath);
            }
            var customerDto = _mapper.Map<CustomerDto>(customer);
            return customerDto;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _customersRepository.GetAllAsync();
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return customersDto;
        }

        public async Task CreateAsync(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            var customerExists = await _customersRepository.FindUserByEmailAsync(customer.Email);
            if (customerExists != null && !customerExists.Equals(customer))
            {
                throw new ConflictException("Customer already exists");
            }

            await _customersRepository.AddCustomerAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            Customer customer = await _customersRepository.GetByIdAsync(id);
            if (customer is null)
            {
                string uriPath = _httpContextAccessor.HttpContext?.Request.Path;
                throw new NotFoundException("Usuário não encontrado", uriPath);
            }

            await _customersRepository.DeleteCustomer(customer);
        }

        public async Task UpdateAsync(int id, CreateCustomerDto customer)
        {
            Customer customerExists = await _customersRepository.GetByIdAsync(id);
            if (customerExists is null)
            {
                string uriPath = _httpContextAccessor.HttpContext?.Request.Path;
                throw new NotFoundException("Usuário não encontrado", uriPath);
            }
            customerExists.Email = customer.Email;
            customerExists.Name = customer.Name;
            customerExists.Phone = customer.Phone;

            await _customersRepository.UpdateCustomerAsync(customerExists);
        }

    }
}
