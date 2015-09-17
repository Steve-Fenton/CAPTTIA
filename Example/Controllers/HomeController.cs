using Example.Models;
using Fenton.Capttia;
using System.Web.Mvc;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DynamicForm()
        {
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

        [HttpPost]
        [ValidateCapttia()]
        public ActionResult Ajax(HomeModel model)
        {
            var response = new AjaxResponse();
            if (ModelState.IsValid)
            {
                response.Message = "Thanks!";
                return Json(response);
            }

            response.Message = "No luck!";
            return Json(response);
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }

    public class AjaxResponse
    {
        public string Message { get; set; }
    }
}