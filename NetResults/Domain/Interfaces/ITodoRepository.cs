using Domain.Entities;
using NetResults.Core;

namespace Domain.Interfaces
{
    public interface ITodoRepository
    {
        Result<Todo> GetById(int id);
        Result<IReadOnlyCollection<Todo>> GetAll();
        Result<Todo> Create(Todo todo);
        Result<Todo> Update(Todo todo);
        Result Delete(int id);
    }
}
