using System.Data;
using AStar.Organisation.Core.Application.IRepositories;
using AStar.Organisation.Core.Application.IUnitOfWork;
using AStar.Organisation.Infrastructure.DAL.Contexts;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrganizationContext _context;
        public ICartRepository CartRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public IProductPhotoRepository ProductPhotoRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICartProductRepository CartProductRepository { get; }
        
        private bool _disposed;
        private readonly IDbConnection _dbConnection;

        public UnitOfWork(IConfiguration configuration,ICartRepository cartRepository,
            ICustomerRepository customerRepository, IProductPhotoRepository productPhotoRepository, 
            IProductRepository productRepository, ICartProductRepository cartProductRepository, OrganizationContext context)
        {
            _context = context;
            
            _dbConnection = new NpgsqlConnection(configuration["DbConnection"]);
            _dbConnection.Open();
            
            CartRepository = cartRepository;
            CustomerRepository = customerRepository;
            ProductPhotoRepository = productPhotoRepository;
            ProductRepository = productRepository;
            CartProductRepository = cartProductRepository;
            
            _disposed = false;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}