using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
// JsonSerializerContext for AOT
[JsonSerializable(typeof(CreateTodoRequest))]
[JsonSerializable(typeof(UpdateTodoRequest))]
[JsonSerializable(typeof(TodoResponse))]
[JsonSerializable(typeof(IEnumerable<TodoResponse>))] 
[JsonSerializable(typeof(List<TodoResponse>))] 
[JsonSerializable(typeof(ProblemDetails))]
[JsonSerializable(typeof(Dictionary<string, string[]>))] 
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}