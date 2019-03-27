using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;
using PersonalBlog.Web.Pages;
using PersonalBlog.Web.Views;

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
		public IActionResult AddImg()
		{
			var form = Request.Form;
			var img = form.Files["title"];

			if (img == null) 
				return View("~/Pages/Error.cshtml", new ErrorModel() {Exception = new Exception("Give me a file!!!")});
			
			var filePath = $"wwwroot/imgs/{img.FileName}";
			using (var fileStream = new FileStream(filePath, FileMode.Create))
				img.CopyTo(fileStream);

			var userId = _repository.GetUserByLogin(Request.Cookies["userLogin"]).Id;
			_repository.AddImg(userId, img.FileName);
			var postsRepository =  new PostRepository();
			var usersPosts = postsRepository.GetUsersPosts(userId);

			return View("~/Pages/ShowPostsByUser.cshtml", new ShowPostsByUser() { ImgLink = img.FileName, List = usersPosts});
		}

		[HttpPost]
		public IActionResult LogIn()
		{
			var form = HttpContext.Request.Form;
			var login = form["login"];
			var password = form["password"];
			if (Contains(login))
			{
				if (!Check(login, password))
					return View("~/Views/Login.cshtml",
						new Login() {PasswordWrong = "1234567"}); 
			}
			else
				Add(login, password);

			Response.Cookies.Append("userLogin", login);
			return Redirect("/Posts/ShowAllPosts");
		}

		[HttpGet]
		public IActionResult LogOut()
		{
			Response.Cookies.Delete("userLogin");
			return View("~/Views/Login.cshtml", new Login() {PasswordWrong = "logged out"});
		}
		
		private void Add(string login, string password) => _repository.AddUser(login, password);

		private bool Contains(string login) => _repository.ContainUser(login);

		private bool Check(string login, string password) => _repository.CheckPassword(login, password);

		private User Get(string login) => _repository.GetUserByLogin(login);

		private readonly UserRepository _repository;
	}
}