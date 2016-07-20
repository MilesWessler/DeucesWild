using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DeucesWild.Startup))]
namespace DeucesWild
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
