using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IRepositories;
using Dapper;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private IDbConnection _connection;
     
        public CustomerRepository(IDbConnection connection)
        {
            _connection = connection;
        }
     
        public async Task<IEnumerable<Customer>> GetAll()
        {
            var query = "SELECT * FROM \"Customer\"";

            var entities = await _connection.QueryAsync<Customer>(query);
            return entities;
        }
     
        public async Task<Customer> GetById(int id)
        {
            var query = "SELECT * FROM \"Customer\" WHERE \"Id\" = @Id";

            var entity = await _connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
            return entity;
        }
     
        public async Task Create(Customer entity)
        {
            var query = "INSERT INTO \"Customer\" (\"Name\", \"Email\", \"Phone\") VALUES (@Name,@Email,@Phone)";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Update(Customer entity)
        {
            var query = "UPDATE \"Customer\" SET \"Name\" = @Name, \"Email\" = @Email, \"Phone\" = @Phone WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Customer\" WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, new { Id = id });
        }

        private bool _disposed = false;
        
        public virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}