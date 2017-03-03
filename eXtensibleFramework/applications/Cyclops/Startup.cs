using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cyclops.Startup))]
namespace Cyclops
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
