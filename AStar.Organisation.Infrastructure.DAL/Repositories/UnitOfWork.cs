using System.Data;
using System.Diagnostics;
using AStar.Organisation.Core.DomainServices.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;
        private IDbTransaction _dbTransaction;
        private bool _disposed;

        public UnitOfWork(IConfiguration configuration)
        {
            _dbConnection = new NpgsqlConnection(configuration["DbConnection"]);
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
            _disposed = false;
        
            PositionRepository = new PositionRepository(_dbConnection);
            DepartmentRepository = new DepartmentRepository(_dbConnection);
        
            Debug.WriteLine("====================================");
            Debug.WriteLine($"CONNECTION OPEN - Id[{_dbConnection.GetHashCode()}]");
            Debug.WriteLine($"CONNECTION STATE - {_dbConnection.State}");
        }
    
        public IPositionRepository PositionRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
                Debug.WriteLine($"TRANSACTION COMMIT");
            }
            catch
            {
                _dbTransaction.Rollback();
                Debug.WriteLine($"TRANSACTION ROLLBACK");
                throw;
            }
            finally
            {
                Debug.WriteLine($"CONNECTION STATE2 - {_dbConnection.State}");
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
                
                Debug.WriteLine($"TRANSACTION DISPOSE");
                Debug.WriteLine($"CONNECTION DISPOSE - Id[{_dbConnection.GetHashCode()}]");
                Debug.WriteLine($"CONNECTION STATE - {_dbConnection.State}");
            }
        }
    }
}