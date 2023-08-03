using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Domain.Poco;

namespace AStar.Organisation.Core.Application.IServices
{
    public interface IPaginationService
    {
        PaginateInfoDto<T> GetPaginationInfo<T>(int pageNumber, int pageSize, IEnumerable<T> items);
    }
}