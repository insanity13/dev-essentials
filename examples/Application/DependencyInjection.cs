using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Application.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();

            services.AddScoped<IValidator<CreateTodoRequest>, CreateTodoRequestValidator>();
            services.AddScoped<IValidator<UpdateTodoRequest>, UpdateTodoRequestValidator>();

            return services;
        }
    }
}
