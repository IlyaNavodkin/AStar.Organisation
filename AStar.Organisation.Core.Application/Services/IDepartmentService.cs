using AStar.Organisation.Core.Application.Dtos;

namespace AStar.Organisation.Core.Application.Services
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