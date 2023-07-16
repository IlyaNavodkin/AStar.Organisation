using AStar.Organisation.Core.Application.Dtos;

namespace AStar.Organisation.Core.Application.Services
{
    public interface ICrudable<T>
    {
        public Task<IEnumerable<T>> GetAll();
        public Task<T> GetById(int id);
        public Task Create(T dto);
        public Task Update(T dto);
        public Task Delete(int id);
    }
}