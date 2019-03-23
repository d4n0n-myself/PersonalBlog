using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Controllers
{
	/// /// <summary>
	///	This class collects information about users.
	/// </summary>
	[Route("[controller]/[action]")]
	public class UsersController : Controller
	{
		public UsersController()
		{
			_repository = new UserRepository();
		}

		[HttpPost]
		public IActionResult LogIn()
		{
			var form = HttpContext.Request.Form;
			var login = form["login"];
			var password = form["password"];
			if (Contains(login))
			{
				TempData.Add("Error", "Error1");
				if (!Check(login, password))
//					return View("~/Views/Login.cshtml", new Login() { PasswordWrong = "1234567", TempData = this.TempData}); // TODO - заставить Login инициализораваться  
				Response.Cookies.Append("userLogin", login);
				return Redirect("/Posts/Show");
			}
			
			Add(login, password);
			Response.Cookies.Append("userLogin", login);
			return Redirect("/Posts/Show");
		}

		[HttpGet]
		public void LogOut()
		{
			Response.Cookies.Delete("userLogin");
//			return View("~/Views/Login.cshtml", new Login() { PasswordWrong = "logged out"});
		}
		
		private void Add(string login, string password) => _repository.AddUser(login, password);

		private bool Contains(string login) => _repository.ContainUser(login);

		private bool Check(string login, string password) => _repository.CheckPassword(login, password);

		private User Get(string login) => _repository.GetUserByLogin(login);

		private readonly UserRepository _repository;
	}
}