using System;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;
using PersonalBlog.Web.Pages;

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
				if (!Check(login, password)) return View("~/Pages/Error.cshtml",  new ErrorModel() { Exception = new Exception("Wrong login or password!")}); // TODO - вывести это 
				Response.Cookies.Append("userLogin", login);
				return View("~/Pages/ShowPosts.cshtml");
			}

			Add(login, password);
			Response.Cookies.Append("userLogin", login);
			return View("~/Pages/ShowPosts.cshtml");
		}

		[HttpGet]
		public IActionResult LogOut()
		{
			Response.Cookies.Delete("userLogin");
			return View("~/Pages/Login.cshtml");
		}
		
		private void Add(string login, string password) => _repository.AddUser(login, password);

		private bool Contains(string login) => _repository.ContainUser(login);

		private bool Check(string login, string password) => _repository.CheckPassword(login, password);

		private User Get(string login) => _repository.GetUserByLogin(login);

		private readonly UserRepository _repository;
	}
}