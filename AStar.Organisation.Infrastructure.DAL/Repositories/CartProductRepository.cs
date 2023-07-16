using System.Data;
using AStar.Organisation.Core.Domain.Entities;
using AStar.Organisation.Core.DomainServices.Repositories;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class CartProductRepository : ICartProductRepository
    {
        private IDbConnection _connection;
     
        public CartProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
     
        public async Task<IEnumerable<CartProduct>> GetAll()
        {
            var query = "SELECT * FROM \"CartProduct\"";

            var entities = await _connection.QueryAsync<CartProduct>(query);
            return entities;
        }
     
        public async Task<CartProduct> GetById(int id)
        {
            var query = "SELECT * FROM \"CartProduct\" WHERE \"Id\" = @Id";

            var entity = await _connection.QueryFirstOrDefaultAsync<CartProduct>(query, new { Id = id });
            return entity;
        }
     
        public async Task Create(CartProduct entity)
        {
            var query = "INSERT INTO \"CartProduct\" (\"ProductId\", \"CartId\") VALUES (@ProductId, @CartId)";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Update(CartProduct entity)
        {
            var query = "UPDATE \"CartProduct\" SET \"ProductId\" = @ProductId, \"CartId\" = @CartId WHERE \"Id\" = @Id";

            await _connection.ExecuteAsync(query, entity);
        }
     
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"CartProduct\" WHERE \"Id\" = @Id";

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