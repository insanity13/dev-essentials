﻿using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;
using NetValidator.Core;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddValidators();
            
            return services;
        }
    }
}
