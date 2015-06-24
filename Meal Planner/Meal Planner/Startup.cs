using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Meal_Planner.Startup))]
namespace Meal_Planner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
