using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Core.Application.Dtos
{
    public class CartProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CartId { get; set; }
    }
}