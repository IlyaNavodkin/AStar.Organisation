namespace AStar.Organisation.Core.Domain.Poco;

public partial class Customer : PocoBase
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public decimal? Phone { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
