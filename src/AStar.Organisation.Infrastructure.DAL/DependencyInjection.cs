using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Application.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organisation.Infrastructure.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDal(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartProductRepository, CartProductRepository>();
            services.AddScoped<IProductPhotoRepository, ProductPhotoRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            return services;
        }
    }
}