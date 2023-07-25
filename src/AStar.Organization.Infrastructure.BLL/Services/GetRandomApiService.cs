using System.Text.Json;
using AStar.Organisation.Core.Application.Dtos;
using AStar.Organisation.Core.Application.IServices;

namespace AStar.Organization.Infrastructure.BLL.Services
{
    public class GetRandomApiService : IGetRandomApiService
    {
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;
        
        public GetRandomApiService(HttpClient httpClient, IConfigurationService configurationService)
        {
            _httpClient = httpClient;
            _configurationService = configurationService;
        }

        public async Task<CardInfoDto> GetRandomName()
        {
            _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _configurationService.GetRandomApiKey());
            _httpClient.Timeout = TimeSpan.FromSeconds(3);
            var response = await _httpClient.GetAsync(_configurationService.GetRandomApiUrl());

            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var obj = JsonSerializer.Deserialize<CardInfoDto>(data, new JsonSerializerOptions{PropertyNameCaseInsensitive  = true});

            return obj;
        }
    }
}