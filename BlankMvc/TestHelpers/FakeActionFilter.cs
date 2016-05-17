using System.Web;
using System.Web.Mvc;
using BlankMvc.StructureMap;

namespace BlankMvc.TestHelpers
{
    public class FakeActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            AddEvent(filterContext.HttpContext, "Called in OnActionExecuting");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            AddEvent(filterContext.HttpContext, "Called in OnActionExecuting");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            AddEvent(filterContext.HttpContext, "Called in OnActionExecuting");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            AddEvent(filterContext.HttpContext, "Called in OnActionExecuting");
        }

        private static void AddEvent(HttpContextBase context, string lifeEvent)
        {
            var nestedContainer = context.GetNestedContainer();
            var fakeDependency = nestedContainer.GetInstance<IFakeDependency>();
            fakeDependency.AddEvent(lifeEvent);
        }
    }
}