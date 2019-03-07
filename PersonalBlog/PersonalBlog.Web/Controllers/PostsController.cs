using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Core.Entites;
using PersonalBlog.Database;

namespace PersonalBlog.Web.Controllers
{
    /// /// <summary>
    ///	This class collects information about posts.
    /// </summary>
    public class PostsController : Controller
    {
        public void Add(HttpContext context)
        {
            var form = context.Request.Form;
            var text = form["text"];
            var title = form["title"];
            Guid userId;
            try
            {
                userId = new Guid(context.Request.Cookies["userId"]);
            }
            catch (Exception e)
            {
                context.Response.Redirect("/Home/Authentificate");
                return;
            }
			
            Repo.AddPost(title, text, userId);		
        }
        
        public List<Post> Get()
        {
            try
            {
                return Repo.GetAllPosts();
            }
            catch (Exception e)
            {
                return new List<Post>();
            }
        }
        
        public List<Post> Get(HttpContext context)
        {
            try
            {
                var userId = new Guid(context.Request.Cookies["userId"]);
                return Repo.GetUsersPosts(userId);
            }
            catch (Exception e)
            {
                return new List<Post>();
            }
        }
        
        public bool Contains(string header) => Repo.ContainPost(header);

        public Post Get(string header) => Repo.GetPostByHeader(header);
        
        private static readonly PostRepository Repo = new PostRepository();
    }
}