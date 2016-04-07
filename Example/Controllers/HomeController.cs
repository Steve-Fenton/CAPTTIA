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
        [ValidateCapttia]
        public ActionResult Index(HomeModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Thanks", new { name = model.Name });
            }

            return View();
        }

        [HttpPost]
        [ValidateCapttia]
        public ActionResult Ajax(HomeModel model)
        {
            var response = new AjaxResponse();
            if (ModelState.IsValid)
            {
                response.Message = $"Thanks {model.Name}!";
                return Json(response);
            }

            response.Message = "No luck!";
            return Json(response);
        }

        [Route("Home/Thanks/{name}")]
        public ActionResult Thanks(string name)
        {
            return View("Thanks", (object)name);
        }
    }

    public class AjaxResponse
    {
        public string Message { get; set; }
    }
}