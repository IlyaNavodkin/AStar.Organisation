using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Core.Application.Dtos
{
    public class ProductPhotoDto 
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
    }
}