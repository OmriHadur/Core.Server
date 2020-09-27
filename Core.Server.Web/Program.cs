using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace Core.Server.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUnityServiceProvider(new UnityContainer())
                .UseStartup<CoreServerStartup>();
    }
}
