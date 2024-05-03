using OsDsII.api.Models;

namespace OsDsII.api.Dtos
{
    public record CustomerDto(int Id, string Name, string Email, string Phone, List<ServiceOrderDto>? ServiceOrders)
    {
        public CustomerDto() : this(0, "", "", "", null) { }
    }

    public record ServiceOrderDto(string Description,
        double Price,
        StatusServiceOrder Status,
        DateTimeOffset OpeningDate,
        DateTimeOffset FinishDate);
}
