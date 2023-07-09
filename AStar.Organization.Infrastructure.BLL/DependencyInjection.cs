using AStar.Application.Services;
using AStar.Organization.Core.DomainServices.Validators;
using AStar.Organization.Infrastructure.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organization.Infrastructure.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<PositionValidator>();
            services.AddTransient<DepartmentValidator>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IGetRandomApiService, GetRandomApiService>();

            return services;
        }
    }
}