using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.IRepositories;
using Dapper;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private IDbConnection _connection;
     
        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
     
        public async Task<IEnumerable<Product>> GetAll()
        {
            var query = "SELECT * FROM \"Product\"";

            var entities = await _connection.QueryAsync<Product>(query);
            return entities;
        }
     
        public async Task<Product> GetById(int id)
        {
            var query = "SELECT * FROM \"Product\" WHERE \"Id\" = @Id";

            var entity = await _connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
            return entity;
        }
     
        public async Task Create(Product entity)
        {
            var query = "INSERT INTO \"Product\" (\"Name\", \"Description\", \"Price\") VALUES (@Name,@Description,@Price)";

            await _connection.ExecuteAsync(query, entity);
        }
        
        public async Task Update(Product entity)
        {
            var query = "UPDATE \"Product\" SET \"Name\" = @Name, \"Description\" = @Description, \"Price\" = @Price WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Product\" WHERE \"Id\" = @Id";

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