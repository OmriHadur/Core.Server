using Microsoft.Extensions.Configuration;

namespace RestApi.Tests.Utils
{
    public class ConfigHandler : IConfigHandler
    {
        private TestConfig _config;
        public TestConfig Config
        {
            get
            {
                if (_config == null)
                    _config = new ConfigurationBuilder()
                        .AddJsonFile(".\\appsettings.json")
                        .Build().Get<TestConfig>();
                return _config;
            }
        }

    }
}
