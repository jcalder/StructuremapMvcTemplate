using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlankMvc.StructureMap;
using BlankMvc.TestHelpers;
using StructureMap;

namespace BlankMvc
{
    public class MvcApplication : HttpApplication
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
            StructureMapMvcConfiguration.PerNestedConatinerConfiguration(IsMobileAction);


            var activator = new StructureMapControllerActrivator();
            var controllerFactory = new DefaultControllerFactory(activator);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static Action<ConfigurationExpression> IsMobileAction(HttpContextBase context)
        {
            return (x =>
            {
                if (context.Request.Url.Host.StartsWith("m."))
                {
                    x.For<IMobileDeterminer>().Use(new MobileDeterminer());
                }
                else
                {
                    x.For<IMobileDeterminer>().Use(new NotMobileDeterminer());
                }
            });
        }
    }
}
