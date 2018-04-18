using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCartridgeJournalAuth.Startup))]
namespace WebCartridgeJournalAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
