using AStar.Application.Dtos;

namespace AStar.Application.Services
{
    public interface IPositionService
    {
        public IEnumerable<PositionDto> GetAll();
        public PositionDto GetById(int id);
        public void Create(PositionDto dto);
        public PositionDto Update(PositionDto dto);
        public void Delete(int id);
    }
}