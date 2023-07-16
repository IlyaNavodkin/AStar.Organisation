using AStar.Organisation.Core.DomainServices.IRepositories;

namespace AStar.Organisation.Core.DomainServices.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        ICartRepository CartRepository { get; }
        IProductPhotoRepository ProductPhotoRepository { get; }
        ICartProductRepository CartProductRepository { get; }
        void Commit();
    }
}