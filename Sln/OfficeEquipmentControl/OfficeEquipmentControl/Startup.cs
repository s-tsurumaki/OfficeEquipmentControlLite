using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OfficeEquipmentControl.Startup))]
namespace OfficeEquipmentControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
