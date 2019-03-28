using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Login.cshtml");
        }
    }
}