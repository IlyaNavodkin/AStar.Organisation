using AStar.Domain.Entities;

namespace AStar.Organization.Core.DomainServices.Repositories
{
    public interface IPositionRepository: IRepository<Position>
    {
        Task<IEnumerable<Position>> GetPositionsByDepartmentId(int departmentId);
    }
}