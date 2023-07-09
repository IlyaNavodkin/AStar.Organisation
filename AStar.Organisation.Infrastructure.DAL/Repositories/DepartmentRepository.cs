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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;
 
        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration["DbConnection"];
        }
 
        public async Task<IEnumerable<Department>> GetAll()
        {
            var query = "SELECT * FROM \"Departments\"";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entity = await connection.QueryAsync<Department>(query);
                
                return entity;
            }
        }
 
        public async Task<Department> GetById(int id)
        {
            var query = "SELECT * FROM \"Departments\" WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entity = await connection.QueryFirstOrDefaultAsync<Department>(query, new { Id = id });
                
                return entity;
            }
        }
 
        public async Task Create(Department entity)
        {
            var query = "INSERT INTO \"Departments\" (\"Name\") VALUES (@Name)";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, entity);
            }
        }
 
        public async Task Update(Department entity)
        {
            var query = "UPDATE \"Departments\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, entity);
            }
        }
 
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Departments\" WHERE \"Id\" = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, new {Id = id});
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