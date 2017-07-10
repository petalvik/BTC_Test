using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BTC.Startup))]
namespace BTC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
