using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlacementHelper.Startup))]
namespace PlacementHelper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
