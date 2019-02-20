using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GOLD.Portal.MVC.Startup))]
namespace GOLD.Portal.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
