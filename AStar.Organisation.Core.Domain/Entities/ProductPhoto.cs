namespace AStar.Organisation.Core.Domain.Entities
{
    public class ProductPhoto : EntityBase
    {
        public string Url { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}