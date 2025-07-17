using Application.DTOs;
using NetResults.Core;

namespace Application.Interfaces
{
    public interface ITodoService
    {
        Result<IEnumerable<TodoResponse>> GetAll();
        Result<TodoResponse> GetById(int id);
        Result<TodoResponse> Create(CreateTodoRequest request);
        Result<TodoResponse> Update(int id, UpdateTodoRequest request);
        Result Delete(int id);
    }
}
