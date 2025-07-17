using Application.DTOs;
using Application.Interfaces;
using Application.Mapping;
using Application.Validation;
using Domain.Interfaces;
using NetResults.Core;
using NetResults.Extensions;

namespace Application.Services
{
    internal class TodoService(
        ITodoRepository repository,
        IValidator<CreateTodoRequest> createValidator,
        IValidator<UpdateTodoRequest> updateValidator) : ITodoService
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
            var validation = createValidator.Validate(request);

            if (validation.IsFailed)
                return validation.Error;

            var result = repository.Create(request.ToEntity());
            return result.Map(t => t.ToResponse());
        }

        public Result<TodoResponse> Update(int id, UpdateTodoRequest request)
        {
            var validation = updateValidator.Validate(request);

            if (validation.IsFailed)
                return validation.Error;

            var existingResult = repository.GetById(id);

            if (existingResult.IsFailed)
                return existingResult.AsTyped<TodoResponse>();

            request.UpdateEntity(existingResult.Value!);
            return existingResult.Map(t => t.ToResponse());
        }

        public Result Delete(int id) => repository.Delete(id);
    }
}
