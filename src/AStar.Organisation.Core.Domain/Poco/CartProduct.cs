namespace AStar.Organisation.Core.Domain.Poco;

public partial class CartProduct : PocoBase
{
    public int ProductId { get; set; }

    public int CartId { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
