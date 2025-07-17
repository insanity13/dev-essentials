using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Domain.Interfaces;
using NetResults.Core;
using NetResults.Extensions;

namespace Application.Services
{
    internal class TodoService(ITodoRepository repository) : ITodoService
    {

        public Result<IEnumerable<TodoResponse>> GetAll()
        {
            var result = repository.GetAll();
            return result.Map(todos => todos.Select(t => t.ToResponse()));
        }

        public Result<TodoResponse> GetById(int id)
        {
            if (id <= 0)
                return new ValidationError(nameof(id), "Id must be positive");

            var itemResult = repository.GetById(id);
            return itemResult.Map(todo => todo.ToResponse());
        }

        public Result<TodoResponse> Create(CreateTodoRequest request)
        {
            var result = repository.Create(request.ToEntity());
            return result.Map(t => t.ToResponse());
        }

        public Result<TodoResponse> Update(int id, UpdateTodoRequest request)
        {
            var existingResult = repository.GetById(id);

            if (existingResult.IsFailed)
                return existingResult.AsTyped<TodoResponse>();

            request.UpdateEntity(existingResult.Value!);
            return existingResult.Map(t => t.ToResponse());
        }

        public Result Delete(int id) => repository.Delete(id);
    }
}
