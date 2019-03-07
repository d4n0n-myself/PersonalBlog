using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Add()
        {
            var form = HttpContext.Request.Form;
            var text = form["text"];
            var title = form["title"];
            
            //TODO - исправить как было
            Guid userId = new Guid();
//            try
//            {
//                userId = new Guid(HttpContext.Request.Cookies["userId"]);
//            }
//            catch (Exception e)
//            {
//                HttpContext.Response.Redirect("/Home/Authentificate");
//                return;
//            }

            _repository.AddPost(title, text, userId);
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

//        public List<Post> Get()
//        {
//            try
//            {
//                var userId = new Guid(HttpContext.Request.Cookies["userId"]);
//                return _repository.GetUsersPosts(userId);
//            }
//            catch (Exception e)
//            {
//                return new List<Post>();
//            }
//        }

        public bool Contains(string header) => _repository.ContainPost(header);

        public Post Get(string header) => _repository.GetPostByHeader(header);

        [HttpGet]
        public IActionResult Show()
        {
            var posts = _repository.GetAllPosts();
            return View("~/Pages/ShowPosts.cshtml", new ShowPosts() { List = posts});
        }
        
        private readonly PostRepository _repository;
    }
}