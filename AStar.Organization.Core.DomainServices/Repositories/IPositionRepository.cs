using AStar.Domain.Entities;

namespace AStar.Organization.Core.DomainServices.Repositories
{
    public interface IPositionRepository: IDisposable
    {
        IEnumerable<Position> GetAll();
        Position GetByid(int id);
        void Create(Position item);
        void Update(Position item);
        void Delete(int id);
        void Save();
    }
}