using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FirstSPA.Startup))]
namespace FirstSPA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
