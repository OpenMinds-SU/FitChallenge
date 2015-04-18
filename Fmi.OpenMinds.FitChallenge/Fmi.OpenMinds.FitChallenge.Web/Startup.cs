using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fmi.OpenMinds.FitChallenge.Web.Startup))]
namespace Fmi.OpenMinds.FitChallenge.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
