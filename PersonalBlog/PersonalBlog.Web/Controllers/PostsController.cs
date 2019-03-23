using System;
using System.Collections.Generic;
using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using PersonalBlog.Core.Entites;
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
        }

        [HttpPost]
        public IActionResult Add()
        {
            var form = HttpContext.Request.Form;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var text = Markdown.ToHtml(form["text"], pipeline);
            var content = form["main-text"];
            var title = form["title"];
            _repository.AddPost(title, content, text, GetUserId());
            return Show();
        }

        [HttpGet]
        public IActionResult Show()
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
                var posts = _repository.GetUsersPosts(GetUserId()).Count != 0
                    ? _repository.GetUsersPosts(GetUserId())
                    : new List<Post>();
                return View("~/Pages/ShowPosts.cshtml", new ShowPosts() {List = posts});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View("~/Pages/Error.cshtml", new ErrorModel() {Exception = e});
            }
        }

        private readonly PostRepository _repository;

        private Guid GetUserId()
        {
            var userLogin = HttpContext.Request.Cookies["userLogin"];
            var userRepo = new UserRepository();
            return userRepo.GetUserByLogin(userLogin).Id;
        }

        [Obsolete]
        private bool Contains(string header) => _repository.ContainPost(header);

        [Obsolete]
        private Post Get(string header) => _repository.GetPostByHeader(header);
    }
}