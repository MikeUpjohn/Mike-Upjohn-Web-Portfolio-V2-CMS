using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MikeUpjohnWebPortfolioV2CMS.Startup))]
namespace MikeUpjohnWebPortfolioV2CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
