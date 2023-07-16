using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using Dapper;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class ProductPhotoRepository : IProductPhotoRepository
    {
        private IDbConnection _connection;
     
        public ProductPhotoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
     
        public async Task<IEnumerable<ProductPhoto>> GetAll()
        {
            var query = "SELECT * FROM \"ProductPhoto\"";

            var entities = await _connection.QueryAsync<ProductPhoto>(query);
            return entities;
        }
     
        public async Task<ProductPhoto> GetById(int id)
        {
            var query = "SELECT * FROM \"ProductPhoto\" WHERE \"Id\" = @Id";

            var entity = await _connection.QueryFirstOrDefaultAsync<ProductPhoto>(query, new { Id = id });
            return entity;
        }
     
        public async Task Create(ProductPhoto entity)
        {
            var query = "INSERT INTO \"ProductPhoto\" (\"ProductId\", \"Url\") VALUES (@CustomerId, @Url)";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Update(ProductPhoto entity)
        {
            var query = "UPDATE \"ProductPhoto\" SET \"CustomerId\" = @CustomerId, \"Url\" = @Url WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"ProductPhoto\" WHERE \"Id\" = @Id";

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