using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Core.DomainServices.IRepositories
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}