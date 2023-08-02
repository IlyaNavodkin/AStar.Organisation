namespace AStar.Organisation.Core.Application.Dtos
{
    public class CartRowProductDto
    {
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
    }
}