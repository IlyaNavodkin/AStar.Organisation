using AStar.Organisation.Core.DomainServices.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}