using Application.DTOs;
using Application.Interfaces;
using NetResults.AspNetCore;

namespace WebApplication
{
    internal static class EndpointBuilder
    {
        public static void UseTodoEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var todos = endpoints.MapGroup("/api/todos");

            todos.MapGet("/", (ITodoService service) => service.GetAll().ToHttpResult());

            todos.MapGet("/{id}", (int id, ITodoService service) => service.GetById(id).ToHttpResult());

            todos.MapPost("/", (CreateTodoRequest command, ITodoService service) => service.Create(command).ToHttpResult());

            todos.MapPut("/{id}", (int id, UpdateTodoRequest command, ITodoService service) => service.Update(id, command).ToHttpResult());

            todos.MapDelete("/{id}", (int id, ITodoService service) => service.Delete(id).ToHttpResult());

        }
    }
}
