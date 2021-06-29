using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

[assembly: OwinStartup(typeof(POLIFRONT_WEB_API.Startup))]

namespace POLIFRONT_WEB_API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)

        {
            
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
        public void ConfigureServices(IServiceCollection services)
        {
            
        }
    }
}
