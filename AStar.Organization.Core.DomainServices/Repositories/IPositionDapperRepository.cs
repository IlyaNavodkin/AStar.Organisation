using AStar.Domain.Entities;

namespace AStar.Organization.Core.DomainServices.Repositories
{
    public interface IPositionDapperRepository: IDisposable
    {
        Task<IEnumerable<Position>> GetAll();
        Task<Position> GetById(int id);
        Task Create(Position item);
        Task Update(Position item);
        Task Delete(int id);
    }
}