using OsDsII.api.Dtos;

namespace OsDsII.api.Services.ServiceOrders
{
    public interface IServiceOrdersService
    {
        public Task<List<NewServiceOrderDto>> GetAllAsync(int customerId);
        public Task<NewServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrder);
    }
}
