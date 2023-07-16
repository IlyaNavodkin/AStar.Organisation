using AStar.Organisation.Core.DomainServices.Repositories;

namespace AStar.Organisation.Core.DomainServices.UnitOfWork
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