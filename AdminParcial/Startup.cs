using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminParcial.Startup))]
namespace AdminParcial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
