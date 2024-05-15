using OsDsII.api.Models;

namespace OsDsII.api.Dtos
{
    public record CreateServiceOrderDto(string Description, double Price, int CustomerId)
    {
    }
}
