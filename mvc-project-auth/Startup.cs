using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mvc_project_auth.Startup))]
namespace mvc_project_auth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
