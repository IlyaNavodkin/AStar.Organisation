using AStar.Organisation.Core.Application.IServices;

namespace AStar.Organisation.Infrastructure.API.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetRandomApiKey() => _configuration["RandomGenerationApiKey"];
        public string GetRandomApiUrl() => _configuration["RandomGenerationApiUrl"];
    }
}