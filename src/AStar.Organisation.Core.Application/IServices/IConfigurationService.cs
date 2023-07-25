namespace AStar.Organisation.Core.Application.IServices
{
    public interface IConfigurationService
    {
        public string GetRandomApiKey();
        public string GetRandomApiUrl();
    }
}