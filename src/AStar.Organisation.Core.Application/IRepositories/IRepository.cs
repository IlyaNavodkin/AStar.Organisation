using AStar.Organisation.Core.Domain.Poco;

namespace AStar.Organisation.Core.Application.IRepositories
{
    public interface IRepository<T>  where T : PocoBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> SkipAndTake(int startIndex, int andIndex);
        Task<T> GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}