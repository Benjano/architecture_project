using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CouponsWeb.Startup))]
namespace CouponsWeb
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
