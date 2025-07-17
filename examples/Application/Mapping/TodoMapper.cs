using Application.DTOs;
using Domain.Entities;

namespace Application.Mapping
{
    internal static class TodoMapper
    {
        public static TodoResponse ToResponse(this Todo todo)
        {
            return new TodoResponse(
                todo.Id,
                todo.Title,
                todo.Description,
                todo.IsCompleted,
                todo.CreatedAt);
        }

        public static Todo ToEntity(this CreateTodoRequest request)
        {
            return new Todo
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false
            };
        }

        public static void UpdateEntity(this UpdateTodoRequest request, Todo todo)
        {
            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.IsCompleted = request.IsCompleted;
        }
    }
}
