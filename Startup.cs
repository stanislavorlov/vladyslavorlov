using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VladyslavOrlovPromo.Startup))]
namespace VladyslavOrlovPromo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
