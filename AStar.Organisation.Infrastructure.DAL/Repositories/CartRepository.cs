using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IRepositories;
using Dapper;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CartRepository : ICartRepository
    {
        private IDbConnection _connection;
     
        public CartRepository(IDbConnection connection)
        {
            _connection = connection;
        }
     
        public async Task<IEnumerable<Cart>> GetAll()
        {
            var query = "SELECT * FROM \"Cart\"";

            var entities = await _connection.QueryAsync<Cart>(query);
            return entities;
        }
     
        public async Task<Cart> GetById(int id)
        {
            var query = "SELECT * FROM \"Cart\" WHERE \"Id\" = @Id";

            var entity = await _connection.QueryFirstOrDefaultAsync<Cart>(query, new { Id = id });
            return entity;
        }
     
        public async Task Create(Cart entity)
        {
            var query = "INSERT INTO \"Cart\" (\"CustomerId\") VALUES (@CustomerId)";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Update(Cart entity)
        {
            var query = "UPDATE \"Cart\" SET \"CustomerId\" = @CustomerId WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Cart\" WHERE \"Id\" = @Id";

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