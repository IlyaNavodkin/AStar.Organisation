using AStar.Application.Dtos;

namespace AStar.Application.Services
{
    public interface IPositionService
    {
        public Task<IEnumerable<PositionDto>> GetAll();
        public Task<PositionDto> GetById(int id);
        public Task Create(PositionDto dto);
        public Task Update(PositionDto dto);
        public Task Delete(int id);
    }
}