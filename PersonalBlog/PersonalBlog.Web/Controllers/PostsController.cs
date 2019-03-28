using System;
using Markdig;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Database;
using PersonalBlog.Web.Pages;

namespace PersonalBlog.Web.Controllers
{
    /// /// <summary>
    ///	This class collects information about posts.
    /// </summary>
    public class PostsController : Controller
    {
        public PostsController()
        {
            _repository = new PostRepository();
            _userRepository = new UserRepository();
            _commentRepository = new CommentRepository();
        }

        [HttpPost]
        public IActionResult Add()
        {
            var form = HttpContext.Request.Form;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var text = Markdown.ToHtml(form["text"], pipeline);
            var content = form["main-text"];
            var title = form["title"];
            try
            {
                _repository.AddPost(title, content, text, Helper.GetUserId(Request.Cookies["userLogin"]));
                return ShowAllPosts();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("~/Pages/Error.cshtml", new ErrorModel() {Exception = e});
            }
        }

        [HttpGet]
        public IActionResult ShowPostById(string postHeader)
        {
            var post = _repository.GetPostByHeader(postHeader);
            var comm = _commentRepository.Get(post.Id);
            return View("~/Views/ShowPost.cshtml", new PersonalBlog.Web.Views.ShowPost() { Post = post, Comments = comm});
        }

        [HttpGet]
        public IActionResult ShowAllPosts()
        {
            try
            {
                var posts = _repository.GetAllPosts();
                return View("~/Pages/ShowPosts.cshtml", new ShowPosts() {List = posts});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("~/Pages/Error.cshtml", new ErrorModel() {Exception = e});
            }
        }

        [HttpGet]
        public IActionResult ShowById()
        {
            try
            {
                var userId = Helper.GetUserId(Request.Cookies["userLogin"]);
                var posts = _repository.GetUsersPosts(userId);
                var imgLink = _userRepository.GetImgLink(userId);
                return View("~/Pages/ShowPostsByUser.cshtml", new ShowPostsByUser() {List = posts, ImgLink = imgLink});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("~/Pages/Error.cshtml", new ErrorModel() {Exception = e});
            }
        }

        private readonly PostRepository _repository;
        private readonly UserRepository _userRepository;
        private readonly CommentRepository _commentRepository;

        [Obsolete]
        private bool Contains(string header) => _repository.ContainPost(header);
    }
}