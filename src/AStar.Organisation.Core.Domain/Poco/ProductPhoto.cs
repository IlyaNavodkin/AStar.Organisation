namespace AStar.Organisation.Core.Domain.Poco;

public partial class ProductPhoto : PocoBase
{
    public string Url { get; set; } = null!;

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
