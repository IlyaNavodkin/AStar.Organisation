using AStar.Organisation.Core.Application.Services;
using AStar.Organization.Infrastructure.BLL.Services;
using AStar.Organization.Infrastructure.BLL.Validators;
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