using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TienThinhCandy.Startup))]
namespace TienThinhCandy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
