using OsDsII.api.Dtos;

namespace OsDsII.api.Services.ServiceOrders
{
    public interface IServiceOrdersService
    {
        public Task<List<ServiceOrderDto>> GetAllAsync(int customerId);
        public Task<ServiceOrderDto> GetByIdAsync(int serviceOrderId);
        public Task<NewServiceOrderDto> CreateAsync(CreateServiceOrderDto serviceOrder);
    }
}
