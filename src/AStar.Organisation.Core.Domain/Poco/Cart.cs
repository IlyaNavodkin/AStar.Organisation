namespace AStar.Organisation.Core.Domain.Poco;

public partial class Cart : PocoBase
{
    public int CustomerId { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual Customer Customer { get; set; } = null!;
}
