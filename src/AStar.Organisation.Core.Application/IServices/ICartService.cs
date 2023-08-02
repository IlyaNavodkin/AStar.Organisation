using AStar.Organisation.Core.Application.Dtos;

namespace AStar.Organisation.Core.Application.IServices
{
    public interface ICartService : ICrudable<CartDto>
    {
        Task<IEnumerable<CartRowProductDto>> GetCartRowProductsById(int cartId);
    }
}