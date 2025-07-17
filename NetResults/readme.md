# Yet Another Result Monad for ASP.NET Core

## Overview

Yet another implementation of the Result monad pattern for ASP.NET Core applications with built-in conversion to ProblemDetails responses and HTTP status codes. This library provides a type-safe way to handle success and error cases in web applications.

## Features

- Strongly-typed error handling for ASP.NET Core
- Support for both generic (Result<T>) and non-generic (Result) results
- Automatic conversion to standardized ProblemDetails responses
- Predefined error types with appropriate HTTP status codes
- Built-in exception throwing capabilities
- Fluent implicit conversions for easy usage
- Mapping and type conversion utilities

## Core Types

### Result Hierarchy

- `Result` (base abstract record)
  - `Result<T>` (generic abstract record)
    - `Success<T>`
    - `NotFoundResult<T>`
    - `InternalErrorResult<T>`
    - `ValidationResult<T>`
    - `UserErrorResult<T>`
  - Non-generic implementations:
    - `Success`
    - `NotFoundResult`
    - `InternalErrorResult`
    - `ValidationResult`
    - `UserErrorResult`

### Error Types

| Error Type         | HTTP Code | Description                          |
|--------------------|-----------|--------------------------------------|
| `NotFound`         | 404       | Resource not found                   |
| `InternalError`    | 500       | Server/internal error with exception |
| `ValidationError`  | 400       | Validation errors with field details |
| `UserError`        | 400       | User-friendly business error         |

## Usage Patterns

### Web API Integration (Controller Style)

```csharp
using NetResults.AspNetCore;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet("{id}")]
    public IResult GetProduct(int id)
    {
        Result<Product> result = _service.GetProduct(id);
        return result.ToHttpResult();
    }
}
```

### Minimal API  (Controller Style)

```csharp
using NetResults.AspNetCore;

internal static class EndpointBuilder
{
    public static void UseTodoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var todos = endpoints.MapGroup("/api/todos");
        todos.MapGet("/{id}", (int id, ITodoService service) => 
            service.GetById(id).ToHttpResult());
    }
}
```

