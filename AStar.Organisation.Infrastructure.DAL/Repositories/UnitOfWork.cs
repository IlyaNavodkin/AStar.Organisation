using System.Data;
using AStar.Organisation.Core.DomainServices.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        public IDbConnection _connection;
        public IDbTransaction _transaction;
        private IPositionRepository _positionRepository;
        private IDepartmentRepository _departmentRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnection"];
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                    _connection = new NpgsqlConnection(_connectionString);

                return _connection;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = Connection.BeginTransaction();
                }

                return _transaction;
            }
        }

        public IPositionRepository PositionRepository
        {
            get
            {
                if (_positionRepository == null)
                {
                    _positionRepository = new PositionRepository(Connection, Transaction);
                }

                return _positionRepository;
            }
        }

        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_departmentRepository == null)
                {
                    _departmentRepository = new DepartmentRepository(Connection, Transaction);
                }

                return _departmentRepository;
            }
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            Transaction?.Dispose();
            Connection?.Dispose();
            _positionRepository = null;
            _departmentRepository = null;
        }
    }
}