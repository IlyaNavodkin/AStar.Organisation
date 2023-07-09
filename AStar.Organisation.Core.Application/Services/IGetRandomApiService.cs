using AStar.Application.Dtos;

namespace AStar.Application.Services
{
    public interface IGetRandomApiService
    {
        public Task<CardInfoDto> GetRandomName();
    }
}