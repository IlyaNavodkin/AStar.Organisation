using AStar.Organisation.Core.Domain.Entities.old;

namespace AStar.Organisation.Core.DomainServices.Repositories.old
{
    public interface IPositionRepository: IRepository<Position>
    {
        Task<IEnumerable<Position>> GetPositionsByDepartmentId(int departmentId);
        Task<IEnumerable<Position>> GetPositionsByDepartmentIdAndName(int departmentId, string name);
    }
}