namespace AStar.Organisation.Core.Domain.Entities
{
    public class CartProduct : EntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}