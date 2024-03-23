using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult SubmitTag() 
        {
            //// Captura de inputs de forma manual
            //var name = Request.Form["name"];
            //var displayName = Request.Form["displayName"];

            return View("Add");
        }
    }
}
