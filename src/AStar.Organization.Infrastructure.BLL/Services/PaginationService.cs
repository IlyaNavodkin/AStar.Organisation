using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class PaginationService : IPaginationService
    {
        public PaginateInfoDto<T> GetPaginationInfo<T>(int pageNumber, int pageSize, IEnumerable<T> items)
        {
            var itemsCount = items.Count();
            
            var paginateItems = items.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            if (paginateItems.Count() == 0) throw new Exception("Пагинация вышла за пределы страницы");     
            
            var dto = new PaginateInfoDto<T>
            {
                TotalItems = itemsCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Items = paginateItems
            };

            return dto;
        }
    }
}