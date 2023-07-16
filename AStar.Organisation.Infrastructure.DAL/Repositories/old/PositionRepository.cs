using System.Data;
using AStar.Organisation.Core.Domain.Entities.old;
using AStar.Organisation.Core.DomainServices.Repositories.old;
using Dapper;

namespace AStar.Organisation.Infrastructure.DAL.Repositories.old
{
public class PositionRepository : IPositionRepository
{
    private IDbConnection _connection;
 
    public PositionRepository(IDbConnection connection)
    {
        _connection = connection;
    }
 
    public async Task<IEnumerable<Position>> GetAll()
    {
        var query = "SELECT * FROM \"Positions\"";

        var entities = await _connection.QueryAsync<Position>(query);
        return entities;
    }
 
    public async Task<Position> GetById(int id)
    {
        var query = "SELECT * FROM \"Positions\" WHERE \"Id\" = @Id";

        var entity = await _connection.QueryFirstOrDefaultAsync<Position>(query, new { Id = id });
        return entity;
    }
 
    public async Task Create(Position position)
    {
        var query = "INSERT INTO \"Positions\" (\"Name\", \"DepartmentId\") VALUES (@Name,@DepartmentId)";

        await _connection.ExecuteAsync(query, position);
    }
 
    public async Task Update(Position position)
    {
        var query = "UPDATE \"Positions\" SET \"Name\" = @Name, \"DepartmentId\" = @DepartmentId WHERE \"Id\" = @Id";

        await _connection.ExecuteAsync(query, position);
    }
 
    public async Task Delete(int id)
    {
        var query = "DELETE FROM \"Positions\" WHERE \"Id\" = @Id";

        await _connection.ExecuteAsync(query, new { Id = id });
    }
    
    public async Task<IEnumerable<Position>> GetPositionsByDepartmentId(int department)
    {
        var query = "SELECT * FROM \"Position\" WHERE \"DepartmentId\" = @Id";

        var entities = await _connection.QueryAsync<Position>(query, new { Id = department });
        return entities;
    }

    public async Task<IEnumerable<Position>> GetPositionsByDepartmentIdAndName(int departmentId, string name)
    {
        var query = "SELECT * FROM \"Positions\" WHERE \"DepartmentId\" = @DepartmentId AND \"Name\" = @Name";

        var entities = await _connection.QueryAsync<Position>(query, new { DepartmentId = departmentId, Name = name });
        return entities;
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
}}