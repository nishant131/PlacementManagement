using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlacementManagement.Startup))]
namespace PlacementManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
