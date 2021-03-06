using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Controllers
{
	[Route("[controller]/[action]")]
	public class CommentController : Controller
	{
		public CommentController()
		{
			_repository = new CommentRepository();
			_postRepository = new PostRepository();
			_userRepository = new UserRepository();
		}

		[HttpPost]
		public IActionResult Add([FromQuery] string textarea, string input)
		{
			var userId = Helper.GetUserId(Request.Cookies["userLogin"]);
			var login = _userRepository.GetUserById(userId).Login;
			var comment = _repository.Add(userId, Guid.Parse(input), textarea) ?? throw new ArgumentException();
			comment.UserLogin = login;
			return Json(comment);
		}

		[HttpGet]
		public IActionResult Show(Guid postId)
		{
			try
			{
				var comments = _repository.Get(postId);
				return Ok(comments);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return BadRequest();
			}
		}

		private readonly CommentRepository _repository;
		private readonly PostRepository _postRepository;
		private readonly UserRepository _userRepository;
	}
}