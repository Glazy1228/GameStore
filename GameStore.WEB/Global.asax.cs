using GameStore.WEB.Util;
using Ninject.Web.Mvc;
using Ninject.Modules;
using System.Web.Mvc;
using System.Web.Routing;
using GameStore.BLL.Infrastructure;
using Ninject;

namespace GameStore.WEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            NinjectModule orderModule = new OrderModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
