using AStar.Organisation.Core.Application.Services;
using AStar.Organization.Infrastructure.BLL.Services;
using AStar.Organization.Infrastructure.BLL.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace AStar.Organization.Infrastructure.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBll(this IServiceCollection services)
        {
            services.AddTransient<CustomerValidator>();
            services.AddTransient<ProductValidator>();
            services.AddTransient<CartValidator>();
            services.AddTransient<ProductPhotoValidator>();
            services.AddTransient<CartProductValidator>();
            
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICartProductService, CartProductService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IProductPhotoService, ProductPhotoService>();
            
            services.AddTransient<IGetRandomApiService, GetRandomApiService>();

            return services;
        }
    }
}