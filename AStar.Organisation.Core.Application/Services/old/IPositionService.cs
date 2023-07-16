using AStar.Organisation.Core.Application.Dtos.old;

namespace AStar.Organisation.Core.Application.Services.old
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