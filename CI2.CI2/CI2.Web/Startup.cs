using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CI2.Web.Startup))]
namespace CI2.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
