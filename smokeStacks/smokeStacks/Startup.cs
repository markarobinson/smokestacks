using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(smokeStacks.Startup))]
namespace smokeStacks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
