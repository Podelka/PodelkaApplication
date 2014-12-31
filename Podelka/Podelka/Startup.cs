using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Podelka.Startup))]
namespace Podelka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
