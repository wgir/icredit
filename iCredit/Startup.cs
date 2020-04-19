using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrediAdmin.Startup))]
namespace CrediAdmin
{
    public partial class Startup
    {
        

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

           
        }
    }
}
