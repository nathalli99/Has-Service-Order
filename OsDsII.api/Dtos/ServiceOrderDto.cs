using OsDsII.api.Models;

namespace OsDsII.api.Dtos
{
    public record ServiceOrderDto(
        int Id,
        string Description,
        double Price,
        StatusServiceOrder Status,
        DateTimeOffset OpeningDate,
        DateTimeOffset FinishDate,
        List<CommentDto> Comments);
}
