using AStar.Organisation.Core.Domain.Entities;

namespace AStar.Organisation.Core.DomainServices.IRepositories
{
    public interface IRepository<T>  where T : EntityBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}