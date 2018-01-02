using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_1.UI.Startup))]
namespace _1.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
