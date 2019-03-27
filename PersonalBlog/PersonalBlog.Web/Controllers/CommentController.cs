using System;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Database;
using PersonalBlog.Web.Views;

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
		public IActionResult Add()
		{
			var form = HttpContext.Request.Form;
			var commentBody = form["comment_text"];
			var postId = Guid.Parse(form["post_id"]);
			
			var userId = Helper.GetUserId(Request.Cookies["userLogin"]);
			if (!_repository.Add(userId, postId, commentBody))
				return BadRequest();

			var postHeader = _postRepository
				.GetPostById(postId)
				.Header;
			var postsController = new PostsController();
			return postsController.ShowPostById(postHeader);
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

		
	}
}