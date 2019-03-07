using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Database;
using WebApplication1.Database.Entites;

namespace WebApplication1.Controllers
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