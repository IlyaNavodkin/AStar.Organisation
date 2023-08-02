using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Domain.Poco;

namespace AStar.Organisation.Core.Application.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<CartRowProductDto>> GetCartRowProductsById(int cartId);
    }
}