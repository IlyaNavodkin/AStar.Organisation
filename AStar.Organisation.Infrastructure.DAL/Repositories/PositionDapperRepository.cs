using System.Data;
using AStar.Domain.Entities;
using AStar.Organisation.Infrastructure.DAL.Repositories.Contexts;
using AStar.Organization.Core.DomainServices.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AStar.Organisation.Infrastructure.DAL.Repositories
{
    public class PositionDapperRepository : IPositionDapperRepository
    {
        private readonly string _connectionString;
 
        public PositionDapperRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
 
        public IEnumerable<Position> GetAll()
        {
            var query = "SELECT * FROM \"Positions\"";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                var entity = connection.Query<Position>(query);
                
                return entity;
            }
        }
 
        public Position GetById(int id)
        {
            var query = "SELECT * FROM \"Positions\" WHERE Id = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<Position>(query, new { Id = id });
            }
        }
 
        public void Create(Position position)
        {
            var query = "INSERT INTO \"Positions\" (Name, DepartmentId) VALUES (@Name,@DepartmentId)";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute(query, position);
            }
        }
 
        public void Update(Position position)
        {
            var query = "UPDATE \"Positions\" SET Name = @Name, DepartmentId = @DepartmentId WHERE Id = @Id)";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute(query, position);
            }
        }
 
        public void Delete(int id)
        {
            var query = "DELETE FROM \"Positions\" WHERE Id = @Id";

            using (IDbConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Execute(query, new {Id = id});
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