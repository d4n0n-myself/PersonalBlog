using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
		}

		[HttpPost]
		public void Add()
		{
			var form = HttpContext.Request.Form;
			var commentBody = form["comment_text"];
			var postId = Guid.Parse(form["post_id"]);

			var userId = Helper.GetUserId(Request.Cookies["userLogin"]);
			if (!_repository.Add(userId, postId, commentBody))
			{throw new Exception();}
//				return BadRequest();

			var postHeader = _postRepository
				.GetPostById(postId)
				.Header;
//			return Ok;()
//			return RedirectToAction("ShowPostById", "Posts", new {postHeader});
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
		
		[HttpGet]
		public string AjaxShow(Guid postId)
		{
			try
			{
				var comments = _repository.Get(postId).ToArray();
				return JsonConvert.SerializeObject(comments);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return "SERVER ERROR";
			}
		}

		private readonly CommentRepository _repository;
		private readonly PostRepository _postRepository;
	}
}