namespace Application.DTOs
{
    public record CreateTodoRequest(
        string Title,
        string? Description);

}
