using OsDsII.api.Models;

namespace OsDsII.api.Dtos
{
    public record CreateServiceOrderDto(string Description, double Price, StatusServiceOrder Status, DateTimeOffset OpeningDate, int CustomerId)
    {
    }
}
