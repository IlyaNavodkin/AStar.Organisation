namespace AStar.Organisation.Core.Application.Services
{
    public interface IConfigurationService
    {
        public string GetRandomApiKey();
        public string GetRandomApiUrl();
    }
}