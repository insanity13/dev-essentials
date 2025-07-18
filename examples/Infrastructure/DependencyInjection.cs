using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NetOptions.Core;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {            
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddAllOptions();
            return services;
        }
    }
}
