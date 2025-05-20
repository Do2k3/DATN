using Microsoft.AspNet.Identity;

using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartupAttribute(typeof(TienThinhCandy.Startup))]
namespace TienThinhCandy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
           

            // Cấu hình đăng nhập cho Admin
           
        }


    }
}

