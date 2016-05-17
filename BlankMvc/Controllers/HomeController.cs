using System.Web.Mvc;
using BlankMvc.TestHelpers;

namespace BlankMvc.Controllers
{   
    [FakeActionFilter]
    public class HomeController : Controller
    {
        private readonly IFakeDependency _fakeDependency;

        public HomeController(IFakeDependency fakeDependency)
        {
            _fakeDependency = fakeDependency;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "On Initial Load " + _fakeDependency.DescibeMyLifetime();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page. your count on postback is" + _fakeDependency.DescibeMyLifetime();;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your application description page. your count on postback is" + _fakeDependency.DescibeMyLifetime();;

            return View();
        }
    }
}