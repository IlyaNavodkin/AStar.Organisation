using System.Data;
using AStar.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts;
using AStar.Organization.Core.DomainServices.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly string _connectionString;
 
        public PositionRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnection"];
        }
 
        public async Task<IEnumerable<Position>> GetAll()
        {
            var query = "SELECT * FROM \"Positions\"";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entity = await connection.QueryAsync<Position>(query);
                
                return entity;
            }
        }
 
        public async Task<Position> GetById(int id)
        {
            var query = "SELECT * FROM \"Positions\" WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entity = await connection.QueryFirstOrDefaultAsync<Position>(query, new { Id = id });
                
                return entity;
            }
        }
 
        public async Task Create(Position position)
        {
            var query = "INSERT INTO \"Positions\" (\"Name\", \"DepartmentId\") VALUES (@Name,@DepartmentId)";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, position);
            }
        }
 
        public async Task Update(Position position)
        {
            var query = "UPDATE \"Positions\" SET \"Name\" = @Name, \"DepartmentId\" = @DepartmentId WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, position);
            }
        }
 
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Positions\" WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {Id = id});
            }
        }
        
        public async Task<IEnumerable<Position>> GetPositionsByDepartmentId(int department)
        {
            var query = "SELECT * FROM \"Position\" WHERE \"DepartmentId\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entities = await connection.QueryAsync<Position>(query, new {Id = department});

                return entities;
            }
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