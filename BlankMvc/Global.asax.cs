using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlankMvc.Controllers;
using BlankMvc.StructureMap;
using BlankMvc.TestHelpers;
using StructureMap;

namespace BlankMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Configure(x => x.For<IFakeDependency>().Use<FakeDependency>());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);   
            StructureMapMvcConfiguration.SetParentContainer(container);
            var activator = new StructureMapControllerActrivator();
            var controllerFactory = new DefaultControllerFactory(activator);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
