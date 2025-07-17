namespace Application.DTOs
{
    public record TodoResponse(
        int Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime CreatedAt);
}
