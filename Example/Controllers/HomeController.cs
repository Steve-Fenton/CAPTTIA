using Example.Models;
using Fenton.Capttia;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var id = Request.AnonymousID;
            ViewBag.Id = id;

            return View();
        }

        [HttpPost]
        [ValidateCapttia()]
        public ActionResult Index(HomeModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Thanks");
            }

            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}