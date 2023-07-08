using AStar.Organisation.Infrastructure.DAL.Repositories;
using AStar.Organization.Core.DomainServices.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDal(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddScoped<IPositionDapperRepository, PositionDapperRepository>
                (options => new PositionDapperRepository(connectionString));
            services.AddScoped<IPositionRepository, PositionRepository>();

            return services;
        }
    }
}