namespace AStar.Organisation.Core.Domain.Poco;

public partial class Product : PocoBase
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<CartProduct> CartProducts { get; set; } = new List<CartProduct>();

    public virtual ICollection<ProductPhoto> ProductPhotos { get; set; } = new List<ProductPhoto>();
}
