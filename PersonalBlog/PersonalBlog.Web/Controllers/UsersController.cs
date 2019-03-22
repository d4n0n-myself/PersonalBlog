using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Controllers
{
	/// /// <summary>
	///	This class collects information about users.
	/// </summary>
	public class UsersController : Controller
	{
		public UsersController()
		{
			_repository = new UserRepository();
		}
		public void Add(string login, string password) => _repository.AddUser(login, password);

		public IActionResult AuthUser()
		{
			var form = HttpContext.Request.Form;
			var login = form["login"];
			var password = form["password"];
			if (Contains(login))
			{
				if (Check(login, password))
				{
					Response.Cookies.Append("userLogin", login);
					return View("~/Pages/AddPost.cshtml"); // TODO заменить на home
				}
			}
			else
			{
				Add(login, password);
				return View("~/Pages/AddPost.cshtml");// TODO заменить на home
			}
			return View("~/Pages/Login.cshtml");
		}

		public bool Contains(string login) => _repository.ContainUser(login);

		public bool Check(string login, string password) => _repository.CheckPassword(login, password);

		public User Get(string login) => _repository.GetUserByLogin(login);

		private readonly UserRepository _repository;
	}
}