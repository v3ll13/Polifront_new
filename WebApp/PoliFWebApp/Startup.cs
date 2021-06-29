using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PoliFWebApp.Startup))]
namespace PoliFWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
