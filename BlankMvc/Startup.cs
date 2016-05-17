using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlankMvc.Startup))]
namespace BlankMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
