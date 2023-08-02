using AStar.Organisation.Core.Application.IRepositories;

namespace AStar.Organisation.Core.Application.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        ICartRepository CartRepository { get; }
        IProductPhotoRepository ProductPhotoRepository { get; }
        ICartProductRepository CartProductRepository { get; }
        void SaveChanges();
    }
}