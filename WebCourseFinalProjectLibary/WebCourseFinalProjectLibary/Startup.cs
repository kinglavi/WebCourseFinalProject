using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebCourseFinalProjectLibary.Startup))]
namespace WebCourseFinalProjectLibary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
