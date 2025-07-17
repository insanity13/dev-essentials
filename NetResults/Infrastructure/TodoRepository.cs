using Domain.Entities;
using Domain.Interfaces;
using NetResults.Core;

namespace Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private static readonly List<Todo> _todos = [];

        public Result<Todo> GetById(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);

            if (todo == null)
                return new NotFound($"Todo with id {id} not found");

            return todo;
        }

        public Result<IReadOnlyCollection<Todo>> GetAll() => _todos.AsReadOnly();

        public Result<Todo> Create(Todo todo)
        {
            todo.Id = _todos.Count + 1;
            _todos.Add(todo);
            return todo;
        }

        public Result<Todo> Update(Todo todo)
        {
            var existing = _todos.FirstOrDefault(t => t.Id == todo.Id);

            if (existing == null)
                return new NotFound($"Todo with id {todo.Id} not found");

            existing.Title = todo.Title;
            existing.Description = todo.Description;
            existing.IsCompleted = todo.IsCompleted;

            return existing;
        }

        public Result Delete(int id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);

            if (todo != null)
                _todos.Remove(todo);

            return Result.Success();
        }
    }
}
