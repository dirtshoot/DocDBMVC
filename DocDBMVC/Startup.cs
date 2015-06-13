using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocDBMVC.Startup))]
namespace DocDBMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
