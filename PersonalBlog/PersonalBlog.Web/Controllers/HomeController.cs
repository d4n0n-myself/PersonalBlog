using Microsoft.AspNetCore.Mvc;

namespace PersonalBlog.Web.Controllers
{
	/// <summary>
	///	This controller provides views.
	/// </summary>
	[ResponseCache(Duration = 60)]
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddPost()
		{
			return View();
		}
	}
}