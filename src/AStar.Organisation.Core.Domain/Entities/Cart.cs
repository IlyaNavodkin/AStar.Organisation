namespace AStar.Organisation.Core.Domain.Entities
{
    public class Cart : EntityBase
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}