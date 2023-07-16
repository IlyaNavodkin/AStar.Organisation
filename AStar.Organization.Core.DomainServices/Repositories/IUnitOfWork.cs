namespace AStar.Organisation.Core.DomainServices.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        ICartRepository CartRepository { get; }
        IProductPhotoRepository ProductPhotoRepository { get; }
        void Commit();
    }
}