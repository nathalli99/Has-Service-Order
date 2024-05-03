using OsDsII.api.Dtos;

namespace OsDsII.api.Services.Customers
{
    public interface ICustomersService
    {
        public Task CreateAsync(CreateCustomerDto customer);
        public Task<CustomerDto> GetByIdAsync(int id);
        public Task<IEnumerable<CustomerDto>> GetAllAsync();
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, CreateCustomerDto customer);
    }
}
