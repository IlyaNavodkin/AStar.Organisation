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
        private readonly IDbConnection _connection;

        public DepartmentRepository(IDbConnection connection)
        {
            _connection = connection;
        }
 
        public async Task<IEnumerable<Department>> GetAll()
        {
            var query = "SELECT * FROM \"Departments\"";

            using (_connection)
            {
                var entity = await _connection.QueryAsync<Department>(query);
                
                return entity;
            }
        }
 
        public async Task<Department> GetById(int id)
        {
            var query = "SELECT * FROM \"Departments\" WHERE \"Id\" = @Id";

            using (_connection)
            {
                var entity = await _connection.QueryFirstOrDefaultAsync<Department>(query, new { Id = id });
                
                return entity;
            }
        }
 
        public async Task Create(Department entity)
        {
            var query = "INSERT INTO \"Departments\" (\"Name\") VALUES (@Name)";

            using (_connection)
            {
                await _connection.ExecuteAsync(query, entity);
            }
        }
 
        public async Task Update(Department entity)
        {
            var query = "UPDATE \"Departments\" SET \"Name\" = @Name WHERE \"Id\" = @Id";

            using (_connection)
            {
                await _connection.ExecuteAsync(query, entity);
            }
        }
 
        public async Task Delete(int id)
        {
            var query = "DELETE FROM \"Departments\" WHERE \"Id\" = @Id";

            using (_connection)
            {
                await _connection.ExecuteAsync(query, new {Id = id});
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