using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cyclone.Startup))]
namespace Cyclone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
