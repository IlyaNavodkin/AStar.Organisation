using Microsoft.AspNetCore;

namespace AStar.Organisation.Infrastructure.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            BuildWebHost(args).Run();
        }
        
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();
    }
}
