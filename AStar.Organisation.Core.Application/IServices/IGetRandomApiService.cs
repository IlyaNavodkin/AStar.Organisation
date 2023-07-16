using AStar.Organisation.Core.Application.Dtos;

namespace AStar.Organisation.Core.Application.IServices
{
    public interface IGetRandomApiService
    {
        public Task<CardInfoDto> GetRandomName();
    }
}