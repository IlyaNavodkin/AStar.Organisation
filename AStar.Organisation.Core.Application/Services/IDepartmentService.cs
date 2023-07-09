using AStar.Application.Dtos;

namespace AStar.Application.Services
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDto>> GetAll();
        public Task<DepartmentDto> GetById(int id);
        public Task Create(DepartmentDto dto);
        public Task Update(DepartmentDto dto);
        public Task Delete(int id);
    }
}