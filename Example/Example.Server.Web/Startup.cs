using Microsoft.Extensions.Configuration;

namespace Product.Server.Web
{
    public class Startup : Core.Server.Web.Startup
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }
    }
}
