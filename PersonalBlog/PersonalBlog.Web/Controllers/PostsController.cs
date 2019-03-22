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

        private Guid getUserId()
        {
            var userLogin = HttpContext.Request.Cookies["userLogin"];
            var userRepo = new UserRepository();
            return userRepo
                .GetUserByLogin(userLogin)
                .Id;
        }

        public IActionResult Add()
        {
            var form = HttpContext.Request.Form;
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var text = Markdown.ToHtml(form["text"], pipeline);
            var title = form["title"];
            _repository.AddPost(title, text, getUserId());
            return Show();
        }

        public List<Post> Get()
        {
            try
            {
                return _repository.GetAllPosts();
            }
            catch (Exception e)
            {
                return new List<Post>();
            }
        }

        public List<Post> Get(Guid userId)
        {
            try
            {
                return _repository.GetUsersPosts(userId);
            }
            catch (Exception e)
            {
                return new List<Post>();
            }
        }

        public bool Contains(string header) => _repository.ContainPost(header);

        public Post Get(string header) => _repository.GetPostByHeader(header);

        [HttpGet]
        public IActionResult Show()
        {
            var posts = Get();
            return View("~/Pages/ShowPosts.cshtml", new ShowPosts() {List = posts});
        }

        public IActionResult ShowById()
        {
            var posts = Get(getUserId());
            return View("~/Pages/ShowPosts.cshtml", new ShowPosts() {List = posts});
        }

        private readonly PostRepository _repository;
    }
}