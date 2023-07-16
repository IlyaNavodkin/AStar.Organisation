using System.Data;
using AStar.Organisation.Core.DomainServices.Repositories;
using AStar.Organisation.Core.DomainServices.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private bool _disposed;
        
        public ICustomerRepository CustomerRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICartRepository CartRepository { get; }
        public IProductPhotoRepository ProductPhotoRepository { get; }
        public ICartProductRepository CartProductRepository { get; }

        public UnitOfWork(IConfiguration configuration)
        {
            _dbConnection = new NpgsqlConnection(configuration["DbConnection"]);
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
            _disposed = false;
        
            CustomerRepository = new CustomerRepository(_dbConnection);
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch
            {
                _dbTransaction.Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _dbTransaction?.Dispose();
                _dbConnection?.Dispose();
                _disposed = true;
            }
        }
    }
}