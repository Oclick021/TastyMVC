using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TastyMVC.Startup))]
namespace TastyMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
