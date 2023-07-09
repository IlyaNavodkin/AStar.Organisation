using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Core.DomainServices.Repositories
{
    public interface IPositionRepository: IRepository<Position>
    {
        Task<IEnumerable<Position>> GetPositionsByDepartmentId(int departmentId);
    }
}